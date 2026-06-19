/**
 * Feature flags for the HOA Violations API (TypeScript Lambda).
 *
 * WHY THIS EXISTS
 * ---------------
 * Trunk-based development requires merging incomplete work to `main` daily.
 * A feature flag lets that work ship to production turned OFF, so we can
 * integrate continuously and reveal the feature when the business decides.
 * This decouples DEPLOY (code is in the environment) from RELEASE (users see it).
 *
 * This is a deliberately minimal, dependency-free pattern — the right starting
 * point. See "Graduating to a managed service" at the bottom for when to move
 * to AWS AppConfig or LaunchDarkly.
 */

// ---------------------------------------------------------------------------
// 1. Declare flags explicitly. The DEFAULT MUST BE SAFE (off) — the absence
//    or misconfiguration of a flag must never expose half-built behavior.
// ---------------------------------------------------------------------------

export type FlagName =
  | "violations.record" // write a violation to DynamoDB
  | "violations.history" // expose GET /violations history endpoint
  | "violations.notify"; // publish ViolationRecorded to SNS

const DEFAULTS: Record<FlagName, boolean> = {
  "violations.record": false,
  "violations.history": false,
  "violations.notify": false,
};

// ---------------------------------------------------------------------------
// 2. Read flag state from ENVIRONMENT configuration, not from code branches.
//    Flag values travel with the deploy via configuration/{env}.config ->
//    Lambda environment variables (e.g. FLAG_VIOLATIONS_RECORD=true). This keeps
//    config-with-the-artifact (a CD minimum) and avoids per-branch hard-coding.
// ---------------------------------------------------------------------------

function envVarFor(flag: FlagName): string {
  // "violations.record" -> "FLAG_VIOLATIONS_RECORD"
  return "FLAG_" + flag.toUpperCase().replace(/[.\-]/g, "_");
}

function readFlag(flag: FlagName, env: NodeJS.ProcessEnv = process.env): boolean {
  const raw = env[envVarFor(flag)];
  if (raw === undefined) return DEFAULTS[flag]; // safe default when unset
  return raw.trim().toLowerCase() === "true";
}

// ---------------------------------------------------------------------------
// 3. A tiny, testable API. `isEnabled` is what handler code calls.
// ---------------------------------------------------------------------------

export interface FeatureFlags {
  isEnabled(flag: FlagName): boolean;
}

export function loadFeatureFlags(
  env: NodeJS.ProcessEnv = process.env,
): FeatureFlags {
  // Resolve once at cold start; cheap and predictable for a Lambda.
  const resolved = Object.fromEntries(
    (Object.keys(DEFAULTS) as FlagName[]).map((f) => [f, readFlag(f, env)]),
  ) as Record<FlagName, boolean>;

  return {
    isEnabled: (flag) => resolved[flag],
  };
}

// ---------------------------------------------------------------------------
// 4. Using a flag in the handler. The new code path ships to prod merged into
//    `main`, but stays dark until FLAG_VIOLATIONS_RECORD=true is set for the env.
// ---------------------------------------------------------------------------

export interface RecordViolationResult {
  status: number;
  body: unknown;
}

export async function recordViolation(
  flags: FeatureFlags,
  input: { propertyId: string; description: string },
  deps: { save: (v: typeof input) => Promise<{ id: string }> },
): Promise<RecordViolationResult> {
  if (!flags.isEnabled("violations.record")) {
    // Dark: the endpoint exists and deploys, but does nothing user-visible yet.
    return { status: 501, body: { message: "Not implemented" } };
  }

  const saved = await deps.save(input);
  return { status: 201, body: { id: saved.id, ...input } };
}

/*
 * ---------------------------------------------------------------------------
 * FLAGS ARE TEMPORARY
 * ---------------------------------------------------------------------------
 * A flag is scaffolding, not architecture. Once a feature is fully released in
 * all environments, delete the flag AND the dead `if (!enabled)` path in the
 * same small MR. Stale flags are technical debt: they multiply code paths,
 * confuse readers, and rot into bugs. Don't rely on discipline: give each flag an
 * owner, a creation date, and a removal condition in a flag inventory, and add a CI
 * stale-flag check that fails when a flag outlives its expiry. Best of all, make
 * "delete the flag" the last planned slice of the feature, not a someday-ticket.
 *
 * ---------------------------------------------------------------------------
 * GRADUATING TO A MANAGED SERVICE
 * ---------------------------------------------------------------------------
 * This env-var pattern is perfect for on/off flags resolved at deploy time.
 * Move to AWS AppConfig or LaunchDarkly when you need:
 *   - flipping a flag WITHOUT a deploy (true runtime toggle / instant kill switch)
 *   - per-user / per-community targeting or percentage rollouts (canary by cohort)
 *   - an audit trail of who changed which flag when
 * The handler API (`flags.isEnabled(...)`) stays the same — only loadFeatureFlags
 * changes to fetch from the provider and cache with a short TTL.
 */
