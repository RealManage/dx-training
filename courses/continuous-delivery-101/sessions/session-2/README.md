# Session 2: Trunk-Based Development & Continuous Integration

**Duration:** 2 hours
**Format:** Interactive workshop
**Prerequisites:** Session 1; a real backlog feature in mind (from Session 1 homework)

## 🎯 Learning Objectives

By the end of this session, participants will be able to:

- ✅ Explain why long-lived branches cause merge pain, lost work, and big batches
- ✅ Apply trunk-based development with short-lived branches (< 1 day)
- ✅ Decompose a large feature into small, independently shippable changes
- ✅ Use a feature flag to merge incomplete work to trunk safely
- ✅ Identify the minimum CI quality gates that keep the trunk deployable

## 📚 Session Agenda

### 1. Review & Connect (10 minutes)

Quick recap of Session 1: big batches cause risk; CD keeps every change deployable; the pipeline (eventually) owns releasability. Today we tackle the **team practice** that makes all of it possible — and that no pipeline can do for you: **trunk-based development**.

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

Healthy signals: fewer than ~3 active branches at once; branch age in *hours*; no "code freeze" or "stabilization" periods.

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

#### 4.2 The quality gates that protect trunk (10 minutes)

The trunk is only "always deployable" if merging proves it. Minimum gates for an app service:

- **Lint** — style and obvious errors (`eslint`)
- **Unit tests** — fast, run on every MR (`vitest` / `jest`)
- **Coverage threshold** — a floor so tests don't quietly erode
- **IaC validation** — `sam validate` / `cfn-lint` on the template
- **Security scan** — dependency audit + IaC scan

These are the *front half* of the pipeline you'll build in Session 3. Preview them in [`examples/ci-pipeline.gitlab-ci.yml`](./examples/ci-pipeline.gitlab-ci.yml).

> **A gate is only as honest as the tests behind it.** When the same author writes the code *and* its tests — increasingly an AI agent — the tests can assert whatever the code already does, clear the coverage floor, and verify nothing. The coverage threshold then becomes a target the author optimizes directly, not a guarantee. Tests must specify *intent* (behaviour contracts), and something independent must confirm they do. See [CD when AI writes the code](../../resources/ai-assisted-delivery.md).

#### 4.3 Keep feedback fast (5 minutes)

CI is worthless if it's slow — people route around slow gates. Keep the pre-merge suite in *seconds*: unit tests pre-merge, slower integration tests in a later stage, parallelize jobs, cache `node_modules` and the SAM build.

---

### 5. Workshop: Decompose a Real Feature (25 minutes)

Use the [Decompose a Long-Lived Branch](./exercises/decompose-a-branch.md) exercise with the feature you brought from Session 1 homework.

1. **Individually (10 min):** break your feature into 6–10 small slices, each mergeable to trunk within a day, each labeled *visible-now* or *behind-a-flag*.
2. **In pairs/small groups (10 min):** pressure-test each other's slices — "could this merge to `main` today without breaking anything?" Split anything that fails.
3. **Group (5 min):** share one decomposition. Identify where a feature flag let you merge incomplete work, and how you handled any data change with expand/contract.

---

### 6. Wrap-up & Homework (5 minutes)

#### Key takeaways

- Long-lived branches are big batches in disguise — the cost is merge pain, lost work, and risk
- TBD = all work to trunk, branches < 1 day
- Feature flags decouple deploy from release, so "unfinished" is no longer a reason to hold a branch
- Real CI is daily integration + fast automated gates + stop-the-line, none of which a tool gives you for free

#### Homework

1. For one real change this week, try the full loop: short branch → small MR → merge to `main` same day
2. If your service has no flag mechanism, sketch where one belongs (start with the `feature-flag.ts` pattern)
3. List the quality gates your service runs today vs. the minimum set above — the diff is Session 3 work

#### Preview of Session 3

The pipeline that turns "trunk is always deployable" into "any change reaches prod on demand": single path to production, immutable artifacts promoted across environments, recovery on AWS (fail forward, with rollback for emergencies), and your team's migration plan.

## 📚 Resources for This Session

- [Decompose a Long-Lived Branch](./exercises/decompose-a-branch.md)
- [Minimums Reference](../../resources/minimums-reference.md)
- [Glossary](../../resources/glossary.md) — *feature flag*, *deploy* vs *release*, *expand/contract*
- [Trunk-Based Development](https://beyond.minimumcd.org/docs/reference/practices/trunk-based-development/) (MinimumCD)

---

**Previous:** [Session 1 ←](../session-1/README.md) | **Next:** [Session 3: The Pipeline →](../session-3/README.md)
