# Design — Testing & CD When the Code Isn't Tested Yet

- **Date:** 2026-06-24
- **Status:** Approved design (pending spec review)
- **Topic:** How CD 101 should address moving to CD from a large, mostly-untested
  estate where verification is manual today — and how feature flags enable CI
  while manual testing continues.
- **Author:** Shane Gibbons (with Claude)

## Problem

RealManage's established .NET estate is large and has very little automated test
coverage; verification today is largely **manual**. The CD 101 course assumes an
*automated* definition of deployable ("tests pass, scans pass, coverage met"),
states there is "no separate QA team or QA gate" (`CLAUDE.md`), and the
value-stream exercise treats a "throw it over to QA" wait as a *finding* to
eliminate (`sessions/session-1/exercises/value-stream-map.md`). The closest the
course comes to the reality is `ai-assisted-delivery.md` warning that coverage is
a weak signal — but it still assumes tests exist.

Two things are missing:

1. **A path to CD from a manual-testing start.** Nothing tells a team with an
   untested estate how to do CI/CD at all without either an impossible
   backfill-everything project or abandoning the practice.
2. **The flags→CI insight.** The course never says that feature flags let a team
   keep integrating daily even when a change is only manually verifiable.

## Goal and non-goals

**Goal.** Make CD reachable from a manual-testing starting point. Teach a clear
four-rule stance, make the flags→CI mechanism explicit, and ground rule 3
(test-when-you-change) in one concrete C# worked example — without overloading the
three sessions or contradicting the course's existing stances.

**Non-goals.**

- **No wholesale backfill mandate.** Existing untested code is "tested in
  production"; the course must not imply a project to retro-fit tests across the
  whole estate.
- **No revival of a separate QA team or QA gate.** Manual testing here is the
  delivering engineer's own verification, not a handoff.
- **No fourth session, no general testing course.** This is CD 101 — cover only
  the testing decisions CD forces, not test design at large.

## Decisions (resolved during brainstorming)

| Question | Decision |
| -------- | -------- |
| Stance on manual testing | **Split**: manual *regression* testing is debt to automate down; manual *exploratory* testing is a permanent, deliberate practice. Expressed as four rules (below). |
| Existing untested code | **Tested in production** — do not backfill wholesale. Earn tests only where you change it. |
| Placement | **Resource + spine touches + glossary**, mirroring `ai-assisted-delivery.md` and `database-delivery.md`. No new session. |
| Depth | Conceptual resource **plus one worked example** (the recommended option), making rules 1–3 concrete. |
| Example language | **C#/xUnit** — a scoped exception, because the untested code lives in the established .NET estate (the *second* such exception, after `db-migrations`). |
| Rule 1 framing | Reinforce "new code gets tests" with a reminder that **AI makes writing tests cheap**, so the time/cost objection is largely gone — with the `ai-assisted-delivery.md` caveat that cheap-to-write is not the same as correct (tests must still pin real intent, independently confirmed). |

## Thesis

### The four rules

1. **New code gets automated tests** — they are part of the definition of
   deployable for anything new. And the old "no time to write tests" objection is
   largely gone: **AI makes drafting tests cheap.** Caveat (from
   `ai-assisted-delivery.md`): cheap-to-write is not the same as correct — an
   AI-written test still has to pin real *intent*, independently confirmed, or it
   verifies nothing.
2. **Existing untested code is proven in production** — don't backfill wholesale.
   Years of production traffic is a form of verification; a mass retro-fit project
   is rarely the best use of effort.
3. **When you change legacy code, add characterization tests** around the part you
   touch — tests that pin the *current* behavior (warts and all) so you can change
   it safely. Manual regression is the bridge until that coverage exists. This is
   how the automated suite grows: at the seams where change actually happens.
4. **Exploratory manual testing is permanent** — a deliberate, valuable practice
   that gates the **release (the flag flip)**, not the merge.

### The flags→CI mechanism (the spine)

CI's bar is "integrate to trunk daily." The objection is "I can't merge — it
isn't verified yet." The feature flag dissolves it: merge the change **off**, so it
is *integrated* (on trunk, building, breaking nothing because it is dark) but not
*released*. Manual verification then happens on the already-integrated,
already-deployed code in `qa` or prod-dark, and the **flag flip** becomes the gate
that manual verification guards.

So manual testing moves **from a pre-merge gate (which forces long-lived branches
and kills CI) to a pre-release check (which preserves CI).** A team can therefore
"do CI" immediately in the integration sense, and *earn* the automated-test
minimums progressively as coverage grows where they change code.

## Reconciliation with the existing course (must not contradict)

- **"No QA team / the team owns quality."** Manual testing in this resource is the
  delivering engineer's own verification — owning quality *includes* doing that
  verification yourself when tests do not exist yet. Not a separate team, not a
  handoff.
- **Value-stream "QA wait is a finding."** Still true for *handoff* waits (work
  queued for a separate team/person). An engineer's own exploratory pass before a
  flip is not a handoff wait — the resource and the Session 1 touch draw this line
  explicitly.
- **CI minimums #3/#4 (automated tests before/after merge).** Honest framing: a
  fully manual estate does **not** yet meet these. The path there is rules 1–3 plus
  flags — not pretending the gap away. Consistent with the course's "honest about
  where we are" philosophy.
- **`ai-assisted-delivery.md` test-independence.** Rule 1's "AI makes tests cheap"
  explicitly carries that resource's caveat, so the two reinforce rather than
  collide. Characterization tests (rule 3) pin *existing* behavior, independent of
  any new intent — a natural complement.

## New artifacts

### 1. `resources/testing-and-cd.md`

A new resource alongside `ai-assisted-delivery.md`, with `order:` frontmatter
slotting it among the resources. Sections:

1. **The gap.** We deliver app code through a pipeline but verify the monolith by
   hand; the course has assumed tests exist.
2. **The four rules.** The stance above, stated plainly.
3. **Characterization tests — the legacy bridge.** Pin current behavior, then
   change safely; grow coverage at the seams. Points to the worked example.
4. **How flags let you do CI while you still test by hand.** The
   pre-merge→pre-release shift; merge dark, verify deployed-but-dark, flip.
5. **Where manual verification sits.** Exploratory vs regression; a deliberate,
   owned step — not a separate-team handoff, not ad hoc.
6. **The trajectory.** Automate regression down over time; keep exploratory
   permanently; honest about CI minimums #3/#4 as a gap you close.

### 2. `sessions/session-2/examples/characterization-test/`

A worked example parallel to `feature-flag.ts` and the Session 3 examples — an
annotated **teaching reference, not a buildable project** (no restore; C#/xUnit
illustrations). Session 2 is the home: it owns CI, the definition of deployable,
and feature flags.

| File | What it teaches |
| ---- | --------------- |
| `README.md` | The arc: legacy untested method → characterization test → change behind a flag → exploratory-verify in qa → flip. Carries `order:`. Names rules 1→3 in action and the flags→CI shift. |
| `LateFeeCalculator.cs` | A legacy HOA late-fee/assessment calculation with real quirks and **no tests** — "tested in production." Then the change, made behind a feature flag (old path preserved, new path dark). |
| `LateFeeCalculatorCharacterizationTests.cs` | xUnit tests that pin the *current* behavior (including the quirks) before the change, plus a test for the new flagged path. |

Teaching beats: characterization tests capture what the code *does*, not what it
*should* do; the flag lets the change integrate before it is verified; the new
behavior is exercised by a new test and by exploratory verification in `qa` before
the flip; manual regression covers what is not yet automated.

## Spine touches (light)

- **`sessions/session-1/README.md`** — by the value-stream "no QA step" note:
  name the untested-estate reality and distinguish a handoff-QA-wait (a finding)
  from an engineer's own verification (not a finding).
- **`sessions/session-2/README.md`** — near feature flags and the
  definition-of-deployable material: flags enable CI even when verification is
  manual; new code gets tests (cheap with AI); characterization tests when changing
  legacy. Pointer to the example and resource.
- **`sessions/session-3/README.md`** — near the pipeline / definition-of-deployable
  material: manual verification as an explicit **pre-release** step (gate the flip),
  never a side-channel; the automate-down trajectory.

## Supporting edits

- **`resources/glossary.md`** — add **Characterization test**, **Exploratory
  testing**, **Regression testing**; cross-link the existing **Seam** entry.
- **`resources/minimums-reference.md`** — a short honesty note on CI minimums #3/#4
  read from a manual start (flags + new-code-tested + characterization, not
  pretense).
- **`resources/migration-checklist.md`** — Phase 1 (CI foundations) items: new code
  ships with automated tests; add characterization tests when changing untested
  legacy; manual verification gates the flag flip, not the merge.
- **`CLAUDE.md`** (course) — generalize the scoped C#/.NET exception note to cover
  *both* `db-migrations` and `characterization-test` (database delivery and legacy
  testing live in the .NET estate; do not convert to TypeScript); record the
  four-rule testing stance.
- **`README.md`** (course landing) — add the resource and example to the file tree;
  a learning-objective bullet (e.g., "Adopt CD from a mostly-untested estate:
  test new code, characterize on change, and use flags to keep integrating").
- **`site.config.json`** — add `testing-and-cd.md` to the resources nav with
  `order` and label; add a label override for the `characterization-test` example
  folder.

## Verification

- `cd site && npm run build` succeeds; CD 101 page count grows by the new resource,
  the example folder index, and its code views; zero new "links outside the
  published site" beyond the pre-existing ai-101 links.
- `npx markdownlint-cli2` clean (MD013 excepted) on all touched and new Markdown,
  run from the repo root.
- Internal links resolve (resource ↔ example ↔ sessions ↔ glossary ↔
  ai-assisted-delivery).
- `slides/` is untouched and unstaged.

## Open questions

None blocking. Defaults chosen during brainstorming, override anytime: resource
filename `testing-and-cd.md`; example domain = HOA late-fee/assessment calculation;
test framework = xUnit. Exact `order:` values settled during implementation.
