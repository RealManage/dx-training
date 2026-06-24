# Testing & CD (Mostly-Untested Estate) Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Teach the Continuous Delivery 101 course how to adopt CD from a large, mostly-untested estate — a four-rule testing stance, characterization tests, and how feature flags enable CI while manual testing continues — via a new resource, a C#/xUnit worked example, light spine touches, and supporting edits.

**Architecture:** One new resource (`resources/testing-and-cd.md`) carrying the conceptual spine; one new C#/xUnit worked example under Session 2 (a teaching reference, not a buildable project, mirroring `db-migrations`); light callouts in all three session READMEs; glossary terms; honesty/checklist edits; and wiring (CLAUDE.md, README.md, site.config.json). Everything frames testing as the CD decisions forced by a manual-verification starting point — not a general testing course.

**Tech Stack:** Markdown (markdownlint-clean except MD013); the static site builder (`cd site && npm run build` → repo-root `public/`); a C#/xUnit example rendered as code views (illustrations only — never compiled or run).

**Source design:** `courses/continuous-delivery-101/docs/superpowers/specs/2026-06-24-testing-and-cd-design.md`

---

## Conventions for every task

- **All paths are relative to the repo root** `/home/shane/src/realmanage/tools/dx/dx-training` unless noted. The course lives in `courses/continuous-delivery-101/`.
- **Verification gate (this is a docs project, so it replaces the usual unit-test loop):**
  - Lint: `npx markdownlint-cli2 "<file>"` run **from the repo root**, ignoring MD013. A `cd site` earlier in a shell breaks relative globs — always lint from the repo root.
  - Build: `cd site && npm run build` — output lands in **repo-root `public/`** (the `site/public` path does not exist). Success = CD-101 builds and the only "links outside the published site" warnings are the three pre-existing **ai-101** ones (zero CD-101).
- **Commits are USER-GATED.** Do **not** commit after each task. Leave all changes staged-or-unstaged for the user to review; the user will ask for the commit(s) at the end. (This overrides the generic "commit" step in the writing-plans template.)
- **Never `git add -A`** — the pinned `courses/continuous-delivery-101/slides/` directory must stay untracked.
- **`.cs` files are teaching illustrations**, like `db-migrations/Program.cs`. They are never compiled or `dotnet test`-ed. The builder renders `.cs` as code views; it hard-ignores `.csproj`, so this example includes **no** `.csproj`.

---

## File Structure

**Create:**

- `courses/continuous-delivery-101/resources/testing-and-cd.md` — the resource (conceptual spine; `order: 45`).
- `courses/continuous-delivery-101/sessions/session-2/examples/characterization-test/README.md` — worked-example narrative (no frontmatter — matches sibling examples db-migrations/branching-antipatterns).
- `courses/continuous-delivery-101/sessions/session-2/examples/characterization-test/LateFeeCalculator.cs` — legacy method + flag-gated new rule.
- `courses/continuous-delivery-101/sessions/session-2/examples/characterization-test/LateFeeCalculatorCharacterizationTests.cs` — characterization tests + new-path tests.

**Modify:**

- `courses/continuous-delivery-101/sessions/session-1/README.md` — one paragraph after the "no separate QA team" sentence.
- `courses/continuous-delivery-101/sessions/session-2/README.md` — one paragraph after the gate-honesty callout.
- `courses/continuous-delivery-101/sessions/session-3/README.md` — one paragraph in the Definition of Deployable section.
- `courses/continuous-delivery-101/resources/glossary.md` — three terms in "Pipeline and artifacts".
- `courses/continuous-delivery-101/resources/minimums-reference.md` — honesty note in the CI-minimums section.
- `courses/continuous-delivery-101/resources/migration-checklist.md` — three Phase-1 items.
- `courses/continuous-delivery-101/CLAUDE.md` — generalize the C#/.NET exception note; add the testing stance.
- `courses/continuous-delivery-101/README.md` — learning-objective bullet + file-tree entry.
- `courses/continuous-delivery-101/site.config.json` — two label overrides.
- `courses/continuous-delivery-101/sessions/session-3/examples/db-migrations/README.md` — consistency fix (done in Task 2): its "the one example in this course that is C#/.NET" claim is now false once the characterization-test example exists; reword to "one of only two C#/.NET examples" and cross-link the new example.

---

## Task 1: The resource — `resources/testing-and-cd.md`

**Files:**

- Create: `courses/continuous-delivery-101/resources/testing-and-cd.md`

- [ ] **Step 1: Write the resource file**

Write this exact content:

````markdown
---
order: 45
---
# Testing and CD — Moving Forward When the Code Isn't Tested Yet

The course frames the definition of deployable as automated checks the pipeline runs. That assumes the tests exist. Across much of RealManage's established .NET estate they do not: the code is large, long-lived, and verified today by **hand**. This guide is how you adopt CD from there — without a year-long "add tests to everything" project, and without giving up the practice.

## The gap

CI's minimums ask for automated tests before and after every merge (minimums #3 and #4). A team whose verification is manual does not meet that bar yet — and pretending otherwise helps no one. But "we have no tests" is not a reason to stay on weekly, big-batch releases. It is a starting position with a concrete path forward. The path has four rules.

## The four rules

1. **New code gets automated tests.** Anything you write now ships with tests that pin its intended behavior — they are part of the definition of deployable for new work. The old objection ("no time to write tests") has largely evaporated: **AI makes drafting tests cheap.** The caveat is real, though — cheap to *write* is not the same as *correct*. An agent that writes the code and its tests in one breath can produce tests that assert whatever the code already does and verify nothing. A test still has to pin real *intent*, and something independent — a human or a separate agent — has to confirm it does, not just echo the code's current output. See [CD when AI writes the code](ai-assisted-delivery.md).
2. **Existing untested code is proven in production.** Years of real traffic are a form of verification. Do **not** launch a project to backfill tests across the whole estate — it is rarely the best use of effort, and it competes with delivery. Leave working, untested code alone until you have a reason to touch it.
3. **When you change legacy code, characterize it first.** Before you modify an untested method, write a **characterization test** — a test that pins its *current* behavior exactly as it is, quirks included. It is not a statement of what the code *should* do; it is a net that catches any change you didn't intend. Now you can refactor or extend safely. This is how the automated suite grows: at the seams where change actually happens, paid for by the change that needed it — worked end to end in [Characterization Test in Practice](../sessions/session-2/examples/characterization-test/README.md). Until that net exists, manual regression testing is the bridge.
4. **Exploratory testing is permanent.** Automating regression frees humans to do the testing only humans can: poking at the change, trying the unexpected, judging whether it *feels* right. This never goes away, and it is not debt. Under CD it gates the **release** — the flag flip — not the merge.

## How feature flags let you do CI while you still test by hand

CI asks you to integrate to trunk every day. The honest objection from a manual-testing team is: *"I can't merge — it isn't tested yet, and testing it by hand takes time."* Hold a branch open until the manual test is done and you are back to long-lived branches, big batches, and no CI.

The feature flag dissolves the objection. Merge the change **turned off**. Now it is *integrated* — on trunk, building, breaking nothing, because it is dark — but not *released*. The manual test then happens on the already-merged, already-deployed code in `qa` (or in production, still dark), and the **flag flip** becomes the moment manual verification guards.

That is the whole move: **manual testing shifts from a gate before the merge to a check before the release.**

```text
Manual test as a PRE-MERGE gate          Manual test as a PRE-RELEASE check
  branch held open until tested            merge dark (flag off) — CI satisfied
  long-lived branch, big batch             pipeline deploys it, still dark
  no CI                                     test the deployed code by hand
  release = the merge                       flip the flag when verified = release
```

You are doing CI immediately — integrating daily — and you *earn* the automated-test minimums over time as rule 3 grows your coverage where it counts.

## Where manual verification sits

Two kinds of manual testing, treated differently:

- **Manual regression** — re-checking that old behavior still works. This is the debt. Every time rule 3 adds characterization tests, a slice of it becomes automated and the manual burden shrinks. Aim it down.
- **Manual exploratory** — deliberate, time-boxed investigation of a change before its flag flips. This is permanent and valuable. Make it explicit: who does it, against which environment, and what they are looking for.

Neither is a *handoff*. RealManage has no separate QA team, and this does not reintroduce one: the delivering engineer owns the verification, the same way they own the change. The value-stream lesson still holds — a "throw it over to QA" *wait* is waste. An engineer verifying their own change before flipping its flag is not a handoff; it is owning quality.

## The trajectory

You are not choosing between "manual forever" and "automate everything now." You are moving along a line: new code tested from the start, legacy code characterized as you touch it, manual regression automated down release by release, and exploratory testing kept as the permanent human edge. Feature flags are what let you travel that line while already practicing CI — integrating continuously today, on the coverage you earn tomorrow.
````

- [ ] **Step 2: Lint**

Run from repo root: `npx markdownlint-cli2 "courses/continuous-delivery-101/resources/testing-and-cd.md" 2>&1 | grep -v MD013`
Expected: `Summary: 0 error(s)`.

- [ ] **Step 3: Build and verify it renders**

Run: `cd site && npm run build` then (from repo root) confirm the page exists and links resolve:
`test -f public/continuous-delivery-101/resources/testing-and-cd.html && echo OK`
Expected: `OK`, build reports CD-101 page count up by 1, and **no new** "links outside the published site" beyond the three ai-101 ones. (The two intra-resource links — `ai-assisted-delivery.md`, `../sessions/.../characterization-test/README.md` — resolve once Task 2 lands; if Task 2 is not yet done, the example link will show as outside-the-site and is expected to clear after Task 2. Re-check at Task 7.)

---

## Task 2: The worked example — `sessions/session-2/examples/characterization-test/`

**Files:**

- Create: `courses/continuous-delivery-101/sessions/session-2/examples/characterization-test/LateFeeCalculator.cs`
- Create: `courses/continuous-delivery-101/sessions/session-2/examples/characterization-test/LateFeeCalculatorCharacterizationTests.cs`
- Create: `courses/continuous-delivery-101/sessions/session-2/examples/characterization-test/README.md`

- [ ] **Step 1: Write `LateFeeCalculator.cs`**

```csharp
// Teaching reference for CD 101 — NOT a restored/buildable project.
// A legacy assessment late-fee calculation from the .NET monolith. It has no
// tests: it has been "tested in production" by years of real HOA billing runs.
// We are about to change the late-fee rule — so first we CHARACTERIZE the current
// behavior (see LateFeeCalculatorCharacterizationTests.cs), then add the new rule
// behind a feature flag so the change can integrate to trunk before it is verified.

public interface IFeatureFlags
{
    bool IsEnabled(string key);
}

public class LateFeeCalculator
{
    private readonly IFeatureFlags _flags;

    public LateFeeCalculator(IFeatureFlags flags) => _flags = flags;

    // The amount a homeowner owes once an assessment is past due.
    public decimal CalculateLateFee(decimal balance, int daysLate)
    {
        if (_flags.IsEnabled("late-fee-v2"))
        {
            return CalculateLateFeeV2(balance, daysLate);
        }

        // --- The legacy rule, exactly as it has run for years -----------------
        // Quirks preserved on purpose — this is what the characterization test pins:
        //   - no fee unless STRICTLY more than 30 days late (30 days => no fee)
        //   - a flat $25 plus 1.5% of the balance, but the percentage part is
        //     silently capped at $100
        //   - zero or negative balances never incur a fee
        if (daysLate <= 30 || balance <= 0m)
        {
            return 0m;
        }

        decimal percentPart = System.Math.Min(balance * 0.015m, 100m);
        return 25m + percentPart;
    }

    // The NEW rule, dark until "late-fee-v2" is flipped on. It is new code, so it
    // arrives WITH tests. It deliberately changes real behavior: the grace period
    // drops to 15 days and the percentage cap rises to $250.
    private static decimal CalculateLateFeeV2(decimal balance, int daysLate)
    {
        if (daysLate <= 15 || balance <= 0m)
        {
            return 0m;
        }

        decimal percentPart = System.Math.Min(balance * 0.015m, 250m);
        return 25m + percentPart;
    }
}
```

- [ ] **Step 2: Write `LateFeeCalculatorCharacterizationTests.cs`**

```csharp
// Teaching reference for CD 101 — NOT a restored/buildable project.
// CHARACTERIZATION tests pin the CURRENT behavior of CalculateLateFee before we
// touch it. They assert what the code DOES today (quirks included), not what it
// SHOULD do — so any UNINTENDED change to the legacy path fails loudly. With the
// flag OFF, the calculator must behave exactly as it always has.
//
// The NEW behavior (flag ON) is NEW code, so it ships WITH its own intent-stating
// tests. This is rule 1 (new code gets tests) and rule 3 (characterize legacy
// before you change it) in one file.

using Xunit;

public class LateFeeCalculatorCharacterizationTests
{
    private static LateFeeCalculator Build(bool v2On) =>
        new LateFeeCalculator(new StubFlags(v2On));

    // ---- Legacy behavior, flag OFF: pin it exactly as it is today -----------

    [Theory]
    [InlineData(30)] // 30 days is NOT late enough — the quirk we must preserve
    [InlineData(16)] // also within the legacy grace period — the exact case V2 changes
    [InlineData(0)]  // not late at all
    public void Legacy_NoFee_WhenNotStrictlyOver30Days(int daysLate)
    {
        var fee = Build(v2On: false).CalculateLateFee(1000m, daysLate);
        Assert.Equal(0m, fee);
    }

    [Fact]
    public void Legacy_NoFee_WhenBalanceIsZeroOrNegative()
    {
        var fee = Build(v2On: false).CalculateLateFee(-50m, 90);
        Assert.Equal(0m, fee);
    }

    [Fact]
    public void Legacy_FlatPlusPercent_BelowTheCap()
    {
        // 31 days late, $1,000 balance: $25 + 1.5% of 1000 ($15) = $40
        var fee = Build(v2On: false).CalculateLateFee(1000m, 31);
        Assert.Equal(40m, fee);
    }

    [Fact]
    public void Legacy_PercentPart_IsCappedAt100()
    {
        // $20,000 balance: 1.5% = $300, but the legacy cap is $100 => $25 + $100 = $125
        var fee = Build(v2On: false).CalculateLateFee(20000m, 31);
        Assert.Equal(125m, fee);
    }

    // ---- New behavior, flag ON: state the INTENDED contract -----------------

    [Fact]
    public void V2_GracePeriodDropsTo15Days()
    {
        // 16 days late now incurs a fee (it would not under the legacy rule)
        var fee = Build(v2On: true).CalculateLateFee(1000m, 16);
        Assert.Equal(40m, fee);
    }

    [Fact]
    public void V2_PercentCapRisesTo250()
    {
        // $20,000 balance: 1.5% = $300, capped at the new $250 => $25 + $250 = $275
        var fee = Build(v2On: true).CalculateLateFee(20000m, 31);
        Assert.Equal(275m, fee);
    }

    private sealed class StubFlags : IFeatureFlags
    {
        private readonly bool _on;
        public StubFlags(bool on) => _on = on;
        public bool IsEnabled(string key) => _on; // single-flag stub for the example
    }
}
```

- [ ] **Step 3: Write `README.md`**

```markdown
# Example: Characterization Test Before a Legacy Change

A small **C#/xUnit** illustration of how to change untested legacy code under CD. Like the [Database Migrations](../../../session-3/examples/db-migrations/README.md) example, it is a **teaching reference, not a buildable project** — read the files as annotated illustrations.

It is in **C#** on purpose: the untested code that needs this technique lives in RealManage's established .NET estate. (This and `db-migrations` are the course's two deliberate C#/.NET exceptions to the "examples are TypeScript + SAM" rule.)

## The situation

`LateFeeCalculator.CalculateLateFee` has computed HOA late fees in the monolith for years. It has **no automated tests** — it has been "tested in production" by every billing run. Now the business wants to change the rule: shorten the grace period and raise the percentage cap. Changing untested code by hand is exactly the scary edit CD has to make routine.

## The four rules, in one change

| File | What it teaches |
| ---- | --------------- |
| [`LateFeeCalculator.cs`](./LateFeeCalculator.cs) | The legacy method (no tests, "tested in prod" — **rule 2**), with the new rule added **behind a feature flag** so it can merge to trunk dark. |
| [`LateFeeCalculatorCharacterizationTests.cs`](./LateFeeCalculatorCharacterizationTests.cs) | **Characterization tests** that pin the *current* behavior before the change (**rule 3**), plus intent-stating tests for the new flagged path (**rule 1**). |

## How it flows

1. **Characterize first (rule 3).** Before touching the method, write tests that pin what it does *today* — including the quirks (no fee at exactly 30 days; the legacy $100 percentage cap). These assert current behavior, not desired behavior, so an unintended change fails loudly.
2. **Add the new rule as new, tested code (rule 1).** `late-fee-v2` is written with its own tests stating the intended contract (15-day grace, $250 cap). AI makes those tests cheap to draft — but they must pin real intent, [independently confirmed](../../../../resources/ai-assisted-delivery.md), or they verify nothing.
3. **Merge dark — keep doing CI.** The change integrates to trunk with `late-fee-v2` **off**. It is integrated but not released, so there is no long-lived branch. The characterization tests prove the legacy path is untouched.
4. **Verify by hand, then flip (rule 4).** Exploratory-test the new fee in `qa` with the flag on — does it behave on real-looking accounts? When it is right, flip the flag in production. The flip is the release; manual verification gated it, not the merge.

See [Testing and CD](../../../../resources/testing-and-cd.md) for the full reasoning.
```

- [ ] **Step 4: Lint the README**

Run from repo root: `npx markdownlint-cli2 "courses/continuous-delivery-101/sessions/session-2/examples/characterization-test/README.md" 2>&1 | grep -v MD013`
Expected: `Summary: 0 error(s)`.

- [ ] **Step 5: Build and verify the example renders**

Run: `cd site && npm run build`. Then from repo root:
`ls public/continuous-delivery-101/sessions/session-2/examples/characterization-test/`
Expected: `index.html`, `LateFeeCalculator.cs.html`, `LateFeeCalculatorCharacterizationTests.cs.html`. Build shows CD-101 code-views count up by 2 and folder-indexes up by 1; no new CD-101 "links outside" warnings.

---

## Task 3: Spine touches (Sessions 1–3)

**Files:**

- Modify: `courses/continuous-delivery-101/sessions/session-1/README.md`
- Modify: `courses/continuous-delivery-101/sessions/session-2/README.md`
- Modify: `courses/continuous-delivery-101/sessions/session-3/README.md`

- [ ] **Step 1: Session 1 — after the "no separate QA team" sentence**

Find this exact line (≈ line 41):

```text
Note who owns quality in this picture: at RealManage there is no separate QA team or QA gate — the team that builds a change owns its quality, and the pipeline is where that ownership becomes automated, enforceable checks rather than a handoff to someone else.
```

Insert a blank line and this **new paragraph** immediately after it (normal paragraph, not a blockquote — avoids MD028 if the next line is a blockquote):

```text
What if there are no automated checks yet? Much of the established estate is verified by hand today — and that does not block CD. Owning quality can start as the delivering engineer testing their own change; a "throw it over to QA" *wait* is the waste, not the act of verifying your own work. The path from manual to automated — test new code, add characterization tests to legacy code as you touch it, and let feature flags keep you integrating meanwhile — is its own guide: [Testing and CD](../../resources/testing-and-cd.md).
```

- [ ] **Step 2: Session 2 — after the gate-honesty callout**

Find this exact line (the blockquote ending section 4.2, ≈ line 135):

```text
> **A gate is only as honest as the tests behind it.** When the same author writes the code *and* its tests — increasingly an AI agent — the tests can assert whatever the code already does, clear the coverage floor, and verify nothing. The coverage threshold then becomes a target the author optimizes directly, not a guarantee. Tests must specify *intent* (behavior contracts), and something independent must confirm they do. See [CD when AI writes the code](../../resources/ai-assisted-delivery.md).
```

Insert a blank line and this **new paragraph** immediately after it (normal paragraph — the line above is a blockquote, so a blockquote here would trip MD028):

```text
**Starting with little test coverage?** New code still gets tests — and with AI drafting them, that is cheap. You do not backfill the whole monolith: you add **characterization tests** when you change untested code, and you lean on **feature flags** to keep integrating daily while verification is still manual. A change merged behind an off flag is integrated (CI satisfied) but not released; the manual test then gates the *flip*, not the merge. The full path is in [Testing and CD](../../resources/testing-and-cd.md).
```

- [ ] **Step 3: Session 3 — in the Definition of Deployable section**

Find this exact line (end of section 3, ≈ line 63):

```text
These are the `validate` / `test` / `security` stages from [Session 2's CI file](../session-2/examples/ci-pipeline.gitlab-ci.yml). They are the *front half* of the full pipeline. The discipline: if it isn't gated automatically, it isn't part of your definition of deployable — it's a hope.
```

Insert a blank line and this **new paragraph** immediately after it:

```text
**When the automated checks don't exist yet** — true across much of the estate — manual verification does not vanish, but it stays *outside* the automated definition of deployable: it gates the **release** (the feature-flag flip), never the merge. Make it an explicit, owned step rather than a side-channel sign-off, and automate it down over time by adding tests where you change code. See [Testing and CD](../../resources/testing-and-cd.md).
```

- [ ] **Step 4: Lint the three READMEs**

Run from repo root:
`npx markdownlint-cli2 "courses/continuous-delivery-101/sessions/session-1/README.md" "courses/continuous-delivery-101/sessions/session-2/README.md" "courses/continuous-delivery-101/sessions/session-3/README.md" 2>&1 | grep -v MD013`
Expected: `Summary: 0 error(s)`. If MD028 appears, the inserted paragraph is adjacent to a blockquote — confirm it was added as a normal paragraph with a blank line on both sides.

- [ ] **Step 5: Build**

Run: `cd site && npm run build`. Expected: clean; the three new `[Testing and CD]` links resolve to `resources/testing-and-cd.html`; no new CD-101 "links outside" warnings.

---

## Task 4: Glossary terms

**Files:**

- Modify: `courses/continuous-delivery-101/resources/glossary.md`

The "Pipeline and artifacts" section is alphabetized. Add three terms in alphabetical position: **Characterization test** (after `Artifact`, before `Definition of deployable`), **Exploratory testing** (after `Definition of deployable`, before `Immutable artifact`), **Regression testing** (after `Quality gate`, before `Smoke test`).

- [ ] **Step 1: Add "Characterization test" after the `Artifact` entry**

Find:

```text
**Artifact**
The built, deployable thing produced from a commit — a Lambda bundle (`.zip`), a container image, a packaged SAM template.
```

Insert immediately after it (blank line between):

```text
**Characterization test**
A test written to pin the *current* behavior of existing code — quirks and all — before you change it, so any unintended change fails loudly. It captures what the code *does*, not what it *should* do. The tool for safely modifying untested legacy code, and how an automated suite grows where change actually happens — at the code's *seams* (see *Seam*). See [Testing and CD](testing-and-cd.md).
```

- [ ] **Step 2: Add "Exploratory testing" after the `Definition of deployable` entry**

Find:

```text
**Definition of deployable**
The automated criteria that determine whether an artifact may be deployed. Criteria, not a meeting.
```

Insert immediately after it (blank line between):

```text
**Exploratory testing**
Deliberate, time-boxed manual investigation of a change — trying the unexpected, judging whether it behaves well — as opposed to scripted regression checks. Under CD it is a permanent practice that gates the *release* (the feature-flag flip), not the merge. Distinct from a QA *handoff*: the delivering engineer does it. See [Testing and CD](testing-and-cd.md).
```

- [ ] **Step 3: Add "Regression testing" after the `Quality gate` entry**

Find:

```text
**Quality gate**
An automated check that must pass for a change to proceed — lint, unit tests, coverage threshold, security scan. Collectively, they form the *definition of deployable*.
```

Insert immediately after it (blank line between):

```text
**Regression testing**
Re-verifying that previously working behavior still works after a change. Where it is automated it belongs in the pipeline's definition of deployable; where it is still manual it is debt to automate down — not a permanent gate. See [Testing and CD](testing-and-cd.md).
```

- [ ] **Step 3b: Generalize the existing "Seam" entry so the Characterization-test cross-link is accurate**

The Characterization-test entry says "(see *Seam*)", but the existing **Seam** entry is worded strangler-fig-only ("a routing flag"), which would misdirect a reader to migration content. Generalize it to the fundamental (Feathers) sense that covers both routing seams and test seams. Find:

```text
**Seam**
A controlled insertion point where you can intercept calls to existing behavior and redirect them — the place a strangler-fig migration adds a routing flag so an old code path and its replacement can run side by side.
```

Replace with:

```text
**Seam**
A place where you can change or observe a program's behavior without editing in that place. A strangler-fig migration adds a routing flag at a seam so an old code path and its replacement run side by side; a characterization test exploits a seam to get otherwise-untestable legacy code under test.
```

- [ ] **Step 4: Lint and build**

Run from repo root: `npx markdownlint-cli2 "courses/continuous-delivery-101/resources/glossary.md" 2>&1 | grep -v MD013` → `Summary: 0 error(s)`.
Then `cd site && npm run build` → clean; the three new glossary `[Testing and CD]` links resolve.

---

## Task 5: Honesty note + checklist items

**Files:**

- Modify: `courses/continuous-delivery-101/resources/minimums-reference.md`
- Modify: `courses/continuous-delivery-101/resources/migration-checklist.md`

- [ ] **Step 1: minimums-reference — CI-minimums honesty note**

Find this exact line (end of the CI minimums list, ≈ line 30):

```text
6. **New work does not break delivered work.** Backward-compatible by default.
```

Insert a blank line and this **blockquote** immediately after it (the next content is the `### "We have a CI server…"` heading, so no adjacent blockquote — MD028-safe):

```text
> **Starting from little or no automated testing?** Minimums #3 and #4 ask for automated tests on every integration — a bar a manual-testing team does not meet yet. That is a gap to close, not a reason to wait: test all *new* code, add **characterization tests** when you change untested legacy, and use **feature flags** to keep integrating daily while verification is still manual. See [Testing and CD](testing-and-cd.md).
```

- [ ] **Step 2: migration-checklist — three Phase-1 items**

Find this exact line in Phase 1 (≈ line 42):

```text
- [ ] Introduce feature flags so incomplete work can merge safely (off by default)
```

Insert these **three list items** immediately after it (same indentation, no blank line — they join the list):

```text
- [ ] All **new** code ships with automated tests that pin intent; AI makes these cheap to draft — see [testing-and-cd](testing-and-cd.md)
- [ ] When you change untested legacy code, add **characterization tests** around the change first — do not backfill the whole estate
- [ ] Manual verification is an explicit, owned step gating the **flag flip** (release), not the merge; automate it down as characterization tests accumulate
```

- [ ] **Step 3: Lint and build**

Run from repo root: `npx markdownlint-cli2 "courses/continuous-delivery-101/resources/minimums-reference.md" "courses/continuous-delivery-101/resources/migration-checklist.md" 2>&1 | grep -v MD013` → `Summary: 0 error(s)`.
Then `cd site && npm run build` → clean; both new `[Testing and CD]` / `[testing-and-cd]` links resolve.

---

## Task 6: Wiring (CLAUDE.md, README.md, site.config.json)

**Files:**

- Modify: `courses/continuous-delivery-101/CLAUDE.md`
- Modify: `courses/continuous-delivery-101/README.md`
- Modify: `courses/continuous-delivery-101/site.config.json`

- [ ] **Step 1: CLAUDE.md — generalize the C#/.NET exception note**

Find this exact bullet:

```text
- **Scoped C#/.NET exception:** the `sessions/session-3/examples/db-migrations/` example is intentionally C#/.NET + SQL Server + DbUp because database delivery lives in the established .NET estate. This is the one justified exception to "examples are TypeScript + SAM" — do **not** convert it to TypeScript.
```

Replace it with:

```text
- **Scoped C#/.NET exceptions (two):** two examples are intentionally C#/.NET because the topic lives in the established .NET estate — `sessions/session-3/examples/db-migrations/` (database delivery + DbUp) and `sessions/session-2/examples/characterization-test/` (testing untested legacy code). These are the justified exceptions to "examples are TypeScript + SAM" — do **not** convert them to TypeScript.
```

- [ ] **Step 2: CLAUDE.md — add the testing stance bullet**

Immediately after the bullet you just edited, add this new bullet:

```text
- **Testing stance (mostly-untested estate):** four rules — (1) new code gets automated tests (cheap with AI, but they must pin real *intent*, independently confirmed — a human or a separate agent, not the one that wrote the code); (2) existing untested code is "tested in production," not backfilled wholesale; (3) characterize legacy code before changing it; (4) exploratory manual testing is permanent and gates the flag flip (release), not the merge — while manual regression is interim debt automated down via rule 3, never a QA handoff. Feature flags let a manual-testing team do CI (integrate daily) by merging dark and verifying before the flip. Manual testing here is the delivering engineer's own verification — NOT a revived QA team/gate. See `resources/testing-and-cd.md`.
```

- [ ] **Step 3: README.md — learning-objective bullet**

Find this exact line in the "What You'll Learn" list:

```text
- ✅ Deliver **database** schema and baseline data as code through the pipeline, the same way you ship application changes
```

Insert this line immediately after it:

```text
- ✅ Adopt CD from a mostly-untested estate: test new code, characterize legacy as you change it, and use flags to keep integrating
```

- [ ] **Step 4: README.md — file-tree entry**

Find this exact line in the file-tree code block:

```text
│   ├── database-delivery.md           # Schema & baseline data as code through the pipeline (DbUp)
```

Insert this line immediately after it (keep the box-drawing alignment):

```text
│   ├── testing-and-cd.md              # Adopting CD from a mostly-untested estate; characterization tests + flags
```

- [ ] **Step 5: site.config.json — two label overrides**

In the `"labels"` object, find:

```json
    "resources/database-delivery.md": "Database Delivery",
```

Add immediately after it:

```json
    "resources/testing-and-cd.md": "Testing & CD",
```

Then find:

```json
    "sessions/session-3/examples/db-migrations/README.md": "Database Migrations (DbUp)",
```

Add immediately after it:

```json
    "sessions/session-2/examples/characterization-test/README.md": "Characterization Test",
```

- [ ] **Step 6: Validate JSON, lint, build**

Validate JSON: `python3 -c "import json,sys; json.load(open('courses/continuous-delivery-101/site.config.json'))" && echo "valid JSON"` → `valid JSON`.
Lint: `npx markdownlint-cli2 "courses/continuous-delivery-101/CLAUDE.md" "courses/continuous-delivery-101/README.md" 2>&1 | grep -v MD013` → `Summary: 0 error(s)`.
Build: `cd site && npm run build` → clean; nav shows "Testing & CD" and "Characterization Test" labels.

---

## Task 7: Final verification (no commit — hand off to user)

**Files:** none (verification only).

- [ ] **Step 1: Full clean rebuild**

Run: `rm -rf public && cd site && npm run build`
Expected: the build succeeds. The **reliable gate** is the file-existence checks from Tasks 1–2 — the resource HTML, the example `index.html`, and both `.cs.html` code views are all present and non-empty. As a sanity check, `continuous-delivery-101` totals should rise by **+1 page**, **+2 code views**, **+1 folder index** versus whatever the build reported before Task 1 (capture that baseline number first if you want an exact before/after; do not treat any specific absolute total as the gate). The only "links outside the published site" warnings are the three pre-existing **ai-101** entries — zero from CD-101.

- [ ] **Step 2: Lint every touched/new Markdown file from the repo root**

```bash
npx markdownlint-cli2 \
  "courses/continuous-delivery-101/resources/testing-and-cd.md" \
  "courses/continuous-delivery-101/sessions/session-2/examples/characterization-test/README.md" \
  "courses/continuous-delivery-101/sessions/session-1/README.md" \
  "courses/continuous-delivery-101/sessions/session-2/README.md" \
  "courses/continuous-delivery-101/sessions/session-3/README.md" \
  "courses/continuous-delivery-101/resources/glossary.md" \
  "courses/continuous-delivery-101/resources/minimums-reference.md" \
  "courses/continuous-delivery-101/resources/migration-checklist.md" \
  "courses/continuous-delivery-101/CLAUDE.md" \
  "courses/continuous-delivery-101/README.md" 2>&1 | grep -v MD013
```

Expected: `Summary: 0 error(s)`.

- [ ] **Step 3: Spot-check rendered cross-links**

Confirm the example README's four-`../` links resolve and the resource ↔ example links work (tag-stripped grep if syntax highlighting interferes). Confirm no zero-byte HTML under `public/continuous-delivery-101`.

- [ ] **Step 4: Confirm `slides/` untouched and report status**

Run: `git status -s`
Expected: the new/modified files from this plan, plus the untracked `courses/continuous-delivery-101/slides/` — and nothing else. **Do not commit.** Report the file list to the user and await commit instructions (the user typically asks for the design spec + plan + implementation in one or more gated commits).

---

## Self-Review

**Spec coverage** (against `2026-06-24-testing-and-cd-design.md`):

- Four-rule stance → Task 1 §"The four rules"; reinforced in Task 6 (CLAUDE.md), Task 5 (checklist).
- AI-makes-tests-cheap reminder + caveat → Task 1 rule 1; Task 6 stance bullet; Task 2 README step 2.
- Flags→CI mechanism → Task 1 §"How feature flags…" (incl. the ASCII contrast); Task 3 §2.
- Reconciliation (no-QA-team, value-stream finding, CI #3/#4, ai-assisted independence) → Task 1 §"Where manual verification sits" + §"The gap"; Task 3 §1; Task 5 §1; cross-links to `ai-assisted-delivery.md`.
- Resource → Task 1. Worked example (C#/xUnit, teaching reference) → Task 2. Spine touches → Task 3. Glossary → Task 4. minimums-reference + migration-checklist → Task 5. CLAUDE.md + README + site.config.json → Task 6. Verification → Task 7. **All covered.**

**Placeholder scan:** none — every file body and edit is given in full; commands have expected output.

**Type/identifier consistency:** `IFeatureFlags.IsEnabled(string)`, `LateFeeCalculator(IFeatureFlags)`, `CalculateLateFee(decimal, int)`, flag key `"late-fee-v2"`, and the `StubFlags` helper are used identically across `LateFeeCalculator.cs` and `LateFeeCalculatorCharacterizationTests.cs`. Fee math checked: legacy 1000/31→40, 20000/31→125, 30d→0, ≤0 bal→0; v2 1000/16→40, 20000/31→275. Filename `testing-and-cd.md`, label "Testing & CD", and example label "Characterization Test" are consistent across resource links, glossary, touches, README tree, and site.config.json. Relative depth from the example (`characterization-test/`) to `resources/` is four `../`; to `session-3/examples/db-migrations/` is three `../` — matching the `db-migrations` precedent.
