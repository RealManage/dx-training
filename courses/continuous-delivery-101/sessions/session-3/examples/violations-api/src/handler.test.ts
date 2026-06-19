/**
 * Unit tests for the Violations API.
 *
 * These are the "definition of deployable" in action: fast, no AWS, run on every
 * MR before merge. Note how injected `deps` and a pure domain function make this
 * trivial to test — that design choice is what keeps the CI gate fast and trusted.
 */

import { describe, it, expect, vi } from "vitest";
import {
  escalationLevel,
  recordViolation,
  type Deps,
  type RecordedViolation,
} from "./handler.js";

describe("escalationLevel (pure domain logic)", () => {
  it("starts at a warning with no prior violations", () => {
    expect(escalationLevel(0)).toBe("WARNING");
  });

  it("escalates with each prior violation", () => {
    expect(escalationLevel(1)).toBe("30_DAY");
    expect(escalationLevel(2)).toBe("60_DAY");
    expect(escalationLevel(3)).toBe("90_DAY");
  });

  it("caps at the 90-day notice", () => {
    expect(escalationLevel(99)).toBe("90_DAY");
  });
});

function fakeDeps(overrides: Partial<Deps> = {}): Deps {
  return {
    countPriorViolations: vi.fn().mockResolvedValue(0),
    save: vi.fn().mockResolvedValue(undefined),
    publishEvent: vi.fn().mockResolvedValue(undefined),
    newId: () => "fixed-id",
    env: { FLAG_VIOLATIONS_RECORD: "true" },
    ...overrides,
  };
}

const input = { propertyId: "PROP-1", description: "Trash cans left out" };

describe("recordViolation feature flags", () => {
  it("ships dark: returns 501 when the record flag is OFF", async () => {
    const deps = fakeDeps({ env: { FLAG_VIOLATIONS_RECORD: "false" } });
    const res = await recordViolation(input, deps);
    expect(res.statusCode).toBe(501);
    expect(deps.save).not.toHaveBeenCalled();
  });

  it("records and assigns escalation level when the flag is ON", async () => {
    const deps = fakeDeps({ countPriorViolations: vi.fn().mockResolvedValue(2) });
    const res = await recordViolation(input, deps);

    expect(res.statusCode).toBe(201);
    const saved = (deps.save as ReturnType<typeof vi.fn>).mock
      .calls[0][0] as RecordedViolation;
    expect(saved.level).toBe("60_DAY");
    expect(saved.violationId).toBe("fixed-id");
  });

  it("only publishes the SNS event when the notify flag is ON", async () => {
    const off = fakeDeps();
    await recordViolation(input, off);
    expect(off.publishEvent).not.toHaveBeenCalled();

    const on = fakeDeps({
      env: { FLAG_VIOLATIONS_RECORD: "true", FLAG_VIOLATIONS_NOTIFY: "true" },
    });
    await recordViolation(input, on);
    expect(on.publishEvent).toHaveBeenCalledOnce();
  });
});

describe("recordViolation validation", () => {
  it("rejects missing fields with 400", async () => {
    const res = await recordViolation(
      { propertyId: "", description: "" },
      fakeDeps(),
    );
    expect(res.statusCode).toBe(400);
  });
});
