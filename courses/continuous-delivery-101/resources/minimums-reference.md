---
order: 20
---
# The MinimumCD Practices — Reference

> This is the bar. Everything in this course exists to help a team meet it. Source: [minimumcd.org](https://minimumcd.org). Keep this page open during merge requests and pipeline reviews.

Continuous Delivery is **the engineering discipline of delivering all changes in a standard way, safely**. "Minimum" means these are not aspirational stretch goals — they are the floor. A team either meets a practice or it does not yet do CD.

> **A note on wording — this course says "deployable" where MinimumCD says "releasable."**
> [minimumcd.org](https://minimumcd.org) defines CI as keeping the trunk *releasable*. We say the
> trunk is always **deployable**, and reserve **release** for the user-facing moment a feature is
> turned on for people (the flag flip). The reason is the distinction at the heart of CD: you
> *deploy* code to an environment — a technical act — but *release* a feature to users — a business
> decision — and the two are deliberately decoupled. The bar is identical to MinimumCD's; only the
> word is sharper. The one place we keep the noun **releasability** is MinimumCD's own name for the
> pipeline's verdict (CD minimum #3, below).

---

## Continuous Integration — the minimums

CI is **each developer integrating their work to the trunk at least daily, and verifying that the work is, to the best of our knowledge, deployable.** It is a team working agreement, not a server.

1. **Use trunk-based development.** All work integrates into one shared trunk, with long-lived branches resisted — the branching model that makes integration *continuous* (detailed below).
2. **Integrate to trunk at least daily.** Every developer, every day.
3. **Automated tests run before merge to trunk.** The change proves itself first.
4. **Automated tests run on the merged result.** The combined work is verified together.
5. **When the build goes red, fixing it is the team's top priority.** Feature work stops.
6. **New work does not break delivered work.** Backward-compatible by default.

> **Starting from little or no automated testing?** Minimums #3 and #4 ask for automated tests on every integration — a bar a manual-testing team does not meet yet. That is a gap to close, not a reason to wait: test all *new* code, add **characterization tests** when you change untested legacy, and use **feature flags** to keep integrating daily while verification is still manual. See [Testing and CD](testing-and-cd.md).

### "We have a CI server, so we do CI" — no

Owning GitLab CI, Jenkins, or GitHub Actions is **not** CI. Those are pipeline tools. CI is the *discipline* of small, daily integrations to trunk with fast automated feedback. Many teams run pipelines for weeks-old feature branches and never integrate — that is the opposite of CI.

---

## Continuous Delivery — the minimums

1. **Use Continuous Integration.** CD is built on CI; you cannot have one without the other.
2. **The pipeline is the only way to deploy to any environment.** No manual `sam deploy` from a laptop to qa or prod. One path. (Personal sandbox stacks are fine; shared environments go through the pipeline.)
3. **The pipeline decides releasability, and its verdict is definitive.** Green means deployable. No side-channel sign-off overrides a red pipeline; nothing ships around a red one.
4. **Every artifact meets the organization's definition of deployable.** "Deployable" is automated criteria (tests pass, scans pass, coverage met), not a meeting.
5. **Artifacts are immutable.** Built once from a commit, never hand-edited afterward. The bytes that pass qa are the bytes that reach prod.
6. **When the deployment pipeline goes red, fixing it is the team's top priority.** Same stop-the-line rule as CI, extended to delivery.
7. **A production-like test environment exists.** You validate in something that resembles prod before prod.
8. **Rollback is available on demand.** You can return to the last good version quickly and predictably.
9. **Application configuration deploys with the artifact.** Config travels with the artifact, versioned alongside it — not patched into a running environment by hand.

> **Operational note on #8:** the minimum requires the rollback *capability*. In practice the *default* response to a problem is to **fail forward** — ship a small fix through the pipeline — because CD makes that fast and safe. Reach for rollback when a problem is costly *and* time-sensitive. See [Session 3](../sessions/session-3/examples/rollback-on-aws.md).

### CD is not Continuous Deployment

- **Continuous Delivery:** every change is *kept deployable* and *can* be deployed at any time on a human's decision.
- **Continuous Deployment:** every change that passes the pipeline *is automatically* deployed to production, with no human gate.

CD is the prerequisite. Continuous Deployment is an optional step beyond it. This course targets **Continuous Delivery**. See [`cd-vs-continuous-deployment.md`](../sessions/session-1/examples/cd-vs-continuous-deployment.md).

---

## Trunk-Based Development — the minimums

Trunk-based development is the branching model CI runs on — so closely bound to CI that the terms are often used almost interchangeably. The relationship is precise: CI *requires* TBD (you cannot integrate continuously while work sits on long-lived branches), and TBD is the first CI minimum above — but they are not synonyms. TBD is the source-control model; CI adds automated verification on every integration and stop-the-line. And TBD is defined as much by what it *resists* as by what it does: long-lived branches are replaced by engineering technique, and the trunk is kept releasable at all times.

- **All changes integrate into the trunk.**
- **If you use branches, they:** originate from trunk, reintegrate to trunk, and are **short-lived (less than a day)**.
- Healthy signal: fewer than three active branches at a time; branches measured in hours, not days; no code-freeze or "stabilization" periods.

### The two workflows

1. **Commit directly to trunk** (with strong tests and review discipline), or
2. **Very short-lived branches** that branch from trunk and merge back within a day.

Long-lived branches are replaced by *engineering* techniques — chiefly **feature flags**, with **branch by abstraction** for larger structural changes a call-site switch can't cleanly wrap — instead of *process* (branch isolation). See [Session 2](../sessions/session-2/README.md).

### Why long-lived branches fail

- **Merge pain / lost work:** the longer a branch lives, the more the trunk drifts, and the riskier the merge. Conflict resolution corrupts or loses code.
- **Abandonment:** branches that get too painful to merge get abandoned, blocking delivery.
- **Big batches:** a branch that lives two weeks *is* a two-week batch — the exact risk CD is trying to eliminate.

---

## How this maps to RealManage

The practices are identical across the estate; only the concrete tools differ.

| Practice | For our AWS services | For our .NET / IIS monolith |
| -------- | -------------------- | --------------------------- |
| Trunk-based development | Short-lived branches off `main`, merged via small MRs within a day | Same — trunk-based is a habit, not a platform |
| Daily integration | Each engineer merges to `main` at least once a day | Same |
| Tests before merge | `npm test` (Vitest/Jest) + `cfn-lint`/`sam validate` on the MR branch | MSBuild + MSTest/xUnit + coverage on the MR branch |
| Single path to prod | All shared-env deploys go through `.gitlab-ci.yml`; OIDC, no laptop creds | Same `.gitlab-ci.yml` (via the shared `ci-templates` include); a PowerShell deploy to IIS from a per-server runner, no laptop deploys |
| Pipeline decides releasability | A green pipeline on `main` *is* the releasability verdict | Same |
| Definition of deployable | Lint + unit tests + coverage threshold + security scan all green | MSBuild (warnings as errors) + tests + coverage + dependency scan all green |
| Immutable artifacts | Lambda bundle / container tagged by `CI_COMMIT_SHA`, built once, promoted | One MSBuild publish output (the `a/` dir) built once, versioned by GitVersion, promoted to every environment |
| Production-like environment | `qa` mirrors `prod` config via `configuration/qa.config` | `qa` mirrors the IIS/SQL topology — the shared SQL Server is the hard part to make prod-like |
| Rollback on demand | Fail forward; re-run the last good deployment in GitLab, with canary + CloudFormation auto-rollback as safety nets | Fail forward; re-run the last good deployment (redeploys the prior `a/` artifact to IIS), or flip the routing/feature flag |
| Config with the artifact | `configuration/{env}.config` deployed with the SAM/CloudFormation stack | `web.{env}.config` XDT transform applied **at deploy** to the one artifact — same bytes, per-env values |

---

## The one-question litmus test

> **Could a single small change, committed right now, reach production today through the pipeline, with no human re-typing commands and no meeting to decide if it's "ready"?**

If yes — you're doing CD. If no — the gap *is* your migration backlog. Use the [Current-State Assessment](../sessions/session-3/exercises/current-state-assessment.md) to find it and the [Migration Checklist](./migration-checklist.md) to close it.
