# Session 2: Trunk-Based Development & Continuous Integration

**Duration:** 2 hours
**Format:** Interactive workshop
**Prerequisites:** Session 1; a real backlog feature in mind (from Session 1 homework)

> 🖥️ **Presenting?** Open the [Session 2 slide deck](../../slides/session-2.html) — press `S` for speaker notes. The deck is built from [`slides/session-2-outline.md`](../../slides/session-2-outline.md).

## 🎯 Learning Objectives

By the end of this session, participants will be able to:

- ✅ Explain why long-lived branches cause merge pain, lost work, and big batches
- ✅ Apply trunk-based development with short-lived branches (< 1 day)
- ✅ Decompose a large feature into small, independently shippable changes
- ✅ Use a feature flag to merge incomplete work to trunk safely
- ✅ Choose the right mechanism to integrate incomplete work — feature flag, branch by abstraction, or just ship it
- ✅ Identify the minimum CI quality gates that keep the trunk deployable

## 📚 Session Agenda

### 1. Review & Connect (10 minutes)

Quick recap of Session 1: big batches cause risk; CD keeps every change deployable; the pipeline (eventually) owns releasability. This session tackles the **team practice** that makes all of it possible — and that no pipeline can do for you: **trunk-based development**.

Share one number from your homework assessment: *how long does your median branch actually live?* For most teams coming from weekly releases, it's days to weeks. That single number is usually the binding constraint on CD.

---

### 2. The Branching Problem (25 minutes)

#### 2.1 What long-lived branches actually cost (10 minutes)

A feature branch that lives two weeks feels safe — isolated, tidy, "I'll merge when it's done." In reality it's accumulating risk:

- **Drift:** trunk moves on without you. The longer you're away, the more the world changed under your branch.
- **Merge pain:** reconciling two weeks of divergence is error-prone. Conflict resolution silently drops or corrupts code.
- **Lost work:** branches that get too painful to merge get *abandoned*. Real work, thrown away.
- **It IS a big batch:** a two-week branch is a two-week batch. Everything Session 1 said about big-batch risk applies the moment you merge.

> **The reframe:** trunk-based development is not "no branches." It's "branches so short they never get the chance to drift." Worked anti-patterns: [`examples/branching-antipatterns.md`](./examples/branching-antipatterns.md).

#### 2.2 Trunk-based development (10 minutes)

Trunk-based development (TBD) is the branching pattern CI *requires*. Its minimums:

- **All changes integrate into the trunk** (`main`).
- **If you branch, the branch:** comes from trunk, returns to trunk, and lives **less than a day**.

Two acceptable workflows:

1. **Commit directly to trunk** — viable with strong tests and tight review discipline.
2. **Very short-lived branches** — branch from `main`, open a small MR, merge back within a day, delete the branch.

Healthy signals: branch age in *hours*, not days; no "code freeze" or "stabilization" periods; and few branches open at once. That last one is a *symptom*, not a target — it scales with team size (a small team lands under ~3), because the real invariants are short branch **lifetime** (< 1 day) and low work-in-progress (roughly one in-flight change per person). Chase lifetime and WIP; the branch count follows.

#### 2.3 RealManage flavor (5 minutes)

For our GitLab + AWS world: short-lived branches with small MRs is the natural fit. It pairs with our existing MR review and Jira branch-naming conventions — what changes is the *size* and *lifetime* of the branch, not the tooling.

---

### 3. Decoupling Deploy from Release: Feature Flags (30 minutes)

#### 3.1 The objection, answered (5 minutes)

> "I can't merge to `main` — my feature isn't finished. It'll break things or expose half-built UI."

This is the #1 reason teams cling to long branches. The answer is to **decouple deploy from release**:

- **Deploy** = code is in the environment. A technical act.
- **Release** = the feature is visible to users. A business decision.

A **feature flag** is a runtime switch that lets unfinished code live in `main` and in production — *turned off* — until you choose to turn it on. Now "not finished" is no longer a reason to keep a branch open.

#### 3.2 How a flag changes the game (10 minutes)

```text
Without flags:                      With flags:
  branch lives until "done"           merge daily, flag OFF
  big-bang merge + release            code ships dark, integrating continuously
  release = the merge                 release = flip the flag, no deploy needed
  rollback = revert + redeploy        "rollback" a feature = flip the flag back
```

Benefits:

- Merge incomplete work daily → branches stay short → batches stay small
- Reveal a feature exactly when the business wants (e.g., after the board meeting)
- Turn a feature *off* instantly if it misbehaves — no deploy required
- Test in production safely (dark launch) before exposing anyone

> Because the **release** is now the flag flip (not the deploy), the flip is also the moment you *communicate* — it's what a client-facing note should announce, and the flip date is the release date. See [Communicating Releases](../../resources/communicating-releases.md).

**Flags aren't the only technique.** A feature flag gates a *behavior* at a call site — ideal for "is this feature on?" But some work is *structural*: swapping a data-access layer, replacing a dependency, a large refactor a single switch can't cleanly wrap. There the technique is **branch by abstraction** — put an interface (a *seam*) over what you're changing, build the replacement behind it while the old path stays live, then flip which implementation the seam resolves to (a one-line wiring change, often itself behind a flag) once the replacement is ready, and delete the old path. Same goal as a flag (integrate on trunk before the work is done), different mechanism. See the [glossary](../../resources/glossary.md).

#### 3.3 A flag in TypeScript (15 minutes)

Walk through [`examples/feature-flag.ts`](./examples/feature-flag.ts): a minimal, dependency-free feature-flag pattern for our HOA Violations Lambda, plus notes on graduating to a managed service (AWS AppConfig / LaunchDarkly) when you need per-user targeting or runtime changes.

Key discipline points covered there:

- Default **off**; the flag's *absence* must be safe
- Flag config is environment config — it travels with the deploy (or lives in a config store), it is not hard-coded per branch
- Flags are **temporary**, and "be disciplined" isn't a plan — give each flag an owner, a creation date, and a removal condition in a flag inventory, let a CI **stale-flag check** fail the build when one outlives its expiry, and make "delete the flag" the last slice of the feature. The mechanism (and CD's other recurring costs) is in [what CD costs you](../../resources/what-cd-costs.md)

---

### 4. Continuous Integration in Practice (25 minutes)

#### 4.1 The CI minimums, concretely (10 minutes)

CI is *daily integration to trunk with automated verification*. The minimums and what they look like for a TypeScript Lambda:

| Minimum | In practice |
| ------- | ----------- |
| Trunk-based development | Short-lived branches, small MRs into `main` |
| Integrate at least daily | Each engineer merges to `main` every day |
| Tests before merge | `npm run lint && npm test` on the MR branch; MR can't merge red |
| Tests on merged result | The same suite runs on `main` after merge |
| Stop the line on red | A red `main` is the team's #1 priority — no new feature work |
| Don't break delivered work | Changes are backward-compatible (expand/contract for data) |

> **Migrations are code, too.** A database migration is reviewed, tested, and merged like any change. You develop and test it against a **local database** — the database analog of the personal sandbox stack — before it merges, and a migration that fails to apply is a **red build** that stops the line. See [Database Delivery](../../resources/database-delivery.md).

#### 4.2 The quality gates that protect trunk (10 minutes)

The trunk is only "always deployable" if merging proves it. Minimum gates for an app service:

- **Lint** — style and obvious errors (`eslint`)
- **Unit tests** — fast, run on every MR (`vitest` / `jest`)
- **Coverage threshold** — a floor so tests don't quietly erode
- **IaC validation** — `sam validate` / `cfn-lint` on the template
- **Security scan** — dependency audit + IaC scan

These are the *front half* of the pipeline you'll build in Session 3. Preview them in [`examples/ci-pipeline.gitlab-ci.yml`](./examples/ci-pipeline.gitlab-ci.yml).

> **A gate is only as honest as the tests behind it.** When the same author writes the code *and* its tests — increasingly an AI agent — the tests can assert whatever the code already does, clear the coverage floor, and verify nothing. The coverage threshold then becomes a target the author optimizes directly, not a guarantee. Tests must specify *intent* (behavior contracts), and something independent must confirm they do. See [CD when AI writes the code](../../resources/ai-assisted-delivery.md).

**Starting with little test coverage?** New code still gets tests — and with AI drafting them, that is cheap. You do not backfill the whole monolith: you add **characterization tests** when you change untested code, and you lean on **feature flags** to keep integrating daily while verification is still manual. A change merged behind an off flag is integrated (CI satisfied) but not released; the manual test then gates the *flip*, not the merge. The full path is in [Testing and CD](../../resources/testing-and-cd.md).

#### 4.3 Keep feedback fast (5 minutes)

CI is worthless if it's slow — people route around slow gates. Keep the pre-merge suite in *seconds*: unit tests pre-merge, slower integration tests in a later stage, parallelize jobs, cache `node_modules` and the SAM build.

---

### 5. Workshop: Flag it, Abstract it, or Ship it? (25 minutes)

Use the [Flag it, Abstract it, or Ship it?](./exercises/feature-flag-decision.md) exercise. For a series of real changes, decide the *cheapest mechanism that still keeps trunk deployable* — a feature flag, branch by abstraction, or just shipping it — and defend *why not the other two*. Walk each change through the [decision walker](./exercises/feature-flag-decision.tool.html).

1. **Together (5 min):** walk one scenario through the three gates out loud, to model the reasoning — not the label.
2. **Individually, then pairs (13 min):** classify the rest — verdict + one-line rationale + the trap each is testing. Argue disagreements from the *gates*, not from taste.
3. **Group (7 min):** debate the three contested scenarios — a "done" change that still needs a flag, a *permanent* flag (a kill switch), and the case where branch by abstraction and a flag compose.

This is the payoff of §3.2: flags are one tool among several, and the skill is choosing among them. Deciding the *slices* of a feature — the complementary skill — is this session's homework.

---

### 6. Wrap-up & Homework (5 minutes)

#### Key takeaways

- Long-lived branches are big batches in disguise — the cost is merge pain, lost work, and risk
- TBD = all work to trunk, branches < 1 day
- Feature flags decouple deploy from release, so "unfinished" is no longer a reason to hold a branch
- Real CI is daily integration + fast automated gates + stop-the-line, none of which a tool gives you for free

#### Homework

1. **Decompose a real feature.** Take the backlog feature you brought from Session 1 and work the [Decompose a Long-Lived Branch](./exercises/decompose-a-branch.md) exercise: break it into 6–10 slices, each mergeable to trunk within a day, each labeled *visible-now* or *behind-a-flag*, with any data change expressed as expand/contract. This is where you apply the mechanism you'd pick in the workshop to real work.
2. For one real change this week, try the full loop: short branch → small MR → merge to `main` same day
3. If your service has no flag mechanism, sketch where one belongs (start with the `feature-flag.ts` pattern)
4. List the quality gates your service runs today vs. the minimum set above — the diff is Session 3 work

#### Preview of Session 3

The pipeline that turns "trunk is always deployable" into "any change reaches prod on demand": single path to production, immutable artifacts promoted across environments, recovery on AWS (fail forward, with rollback for emergencies), and your team's migration plan.

## 📚 Resources for This Session

- [Flag it, Abstract it, or Ship it?](./exercises/feature-flag-decision.md) — the workshop, with its interactive decision walker
- [Decompose a Long-Lived Branch](./exercises/decompose-a-branch.md) — this session's homework
- [Minimums Reference](../../resources/minimums-reference.md)
- [Glossary](../../resources/glossary.md) — *feature flag*, *branch by abstraction*, *deploy* vs *release*, *expand/contract*
- [Feature flags decision tree](https://beyond.minimumcd.org/docs/migrate-to-cd/optimize/feature-flags/) (MinimumCD) — the source for the workshop's three gates
- [Trunk-Based Development](https://beyond.minimumcd.org/docs/reference/practices/trunk-based-development/) (MinimumCD)
- [Database Delivery](../../resources/database-delivery.md) — developing and testing schema migrations locally before merge

---

**Previous:** [Session 1 ←](../session-1/README.md) | **Next:** [Session 3: The Pipeline →](../session-3/README.md)
