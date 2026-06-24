---
order: 30
---
# CD Migration Checklist — From Weekly Releases to Deploy on Demand

A team's working template for adopting Continuous Delivery, organized as the five-phase migration path from the [MinimumCD migration guide](https://beyond.minimumcd.org/docs/migrate-to-cd/). Copy this into your team's repo or wiki and check items off as you go.

> **The trap to break:** infrequent deployment is self-reinforcing. Deploy rarely → each deploy is large → large deploys are risky → fear of risk makes you deploy even less. The only way out is *smaller, more frequent* deploys. Every phase below pushes batch size down.
>
> **Engineering Lead note:** Foundations (Phase 1) look like "slowing down to invest." They are not delay — they are the prerequisite for speed. Budget for them explicitly, give one pilot team a change window and the authority to deploy, and measure *stability alongside speed* (DORA's four metrics) from day one. On control and compliance: CD doesn't weaken governance, it makes it automated and auditable — see [governance-and-compliance](governance-and-compliance.md).

---

## Phase 0 — Assess

Understand your current state and name the constraints holding you back. **Do this first.**

- [ ] [Map your value stream](../sessions/session-1/exercises/value-stream-map.md) (idea → prod): process time, wait time, and %C/A per step; compute flow efficiency and mark the biggest wait / lowest-%C/A step
- [ ] Complete the [Current-State Assessment](../sessions/session-3/exercises/current-state-assessment.md) as a team to back the map with scored minimums and controls
- [ ] Measure today's baseline: deployment frequency, lead time, change failure rate, time to restore
- [ ] Record how long branches actually live (look at merged MRs over the last month)
- [ ] Confirm the single biggest constraint where the map and the scorecard agree (branch lifetime? manual deploys? slow tests? no rollback?)
- [ ] Pick one service and one team to pilot
- [ ] Inventory in-flight long-lived branches; for each, decide *merge now* (decompose first if it's too big) or *abandon* — you can't adopt trunk-based development on top of a pile of week-old branches
- [ ] If your estate is mixed (monolith + new services), sequence it deliberately: prove CD where it's easiest to see, but commit to carrying it to the monolith
- [ ] Agree on what "done with Phase 1" looks like and who decides

**Exit question:** *Do we honestly know where we are and what's holding us back?*

---

## Phase 1 — Foundations

Build the CI capabilities: daily integration, automated testing, small batches, stop-on-red.

- [ ] Adopt trunk-based development: branches off `main`, merged within a day
- [ ] Set a team agreement: every engineer integrates to `main` at least daily
- [ ] Decompose work so changes are small enough to ship daily (see [decompose-a-branch](../sessions/session-2/exercises/decompose-a-branch.md))
- [ ] Automated unit tests run on every MR branch before merge
- [ ] Automated tests run on `main` after merge
- [ ] Adopt the stop-the-line rule: a red `main` build is the team's top priority
- [ ] Introduce feature flags so incomplete work can merge safely (off by default)
- [ ] All **new** code ships with automated tests that pin intent; AI makes these cheap to draft — see [testing-and-cd](testing-and-cd.md)
- [ ] When you change untested legacy code, add **characterization tests** around the change first — do not backfill the whole estate
- [ ] Manual verification is an explicit, owned step gating the **flag flip** (release), not the merge; automate it down as characterization tests accumulate
- [ ] Code review turnaround target: under ~4 hours

**Exit question:** *Can we integrate safely every day?*

---

## Phase 2 — Pipeline

Build one automated path from commit to production, with security scanning.

- [ ] One pipeline (`.gitlab-ci.yml`) is the **only** way to deploy to shared environments
- [ ] No static AWS credentials anywhere — GitLab OIDC role assumption only
- [ ] Build the artifact **once**, tag it by `CI_COMMIT_SHA`, promote the same one through environments
- [ ] Application config deploys with the artifact (`configuration/{env}.config`)
- [ ] A production-like environment (`qa`) exists and mirrors `prod`
- [ ] Quality gates encode the definition of deployable: lint, unit tests, coverage threshold, security scan (`cfn-lint` / `sam validate` / dependency + IaC scanning)
- [ ] The pipeline's green/red verdict is authoritative — nothing ships around it
- [ ] Map change-control evidence to pipeline artifacts (MR review = segregation of duties; pipeline run + SHA-tagged immutable artifact = audit trail) — see [governance-and-compliance](governance-and-compliance.md)
- [ ] Document a break-glass procedure for emergencies: scoped, logged, with post-incident review
- [ ] **Failing forward is the default recovery move** — a small fix flows through the pipeline in minutes
- [ ] Rollback is implemented and **rehearsed** as the emergency lever for costly, time-sensitive problems (re-run the last known-good deployment in GitLab; canary and CloudFormation auto-rollback back it up)
- [ ] Stop-the-line extends to the deployment pipeline, not just the build

- [ ] Database schema and baseline data change **only** through the pipeline (a migration runner such as DbUp), never by hand on a shared environment — see [database-delivery](database-delivery.md)
- [ ] Engineers develop and test migrations against a **local database** (the database analog of a personal sandbox stack) before merge

> **A shared handoff on the two items above.** DX lays the groundwork — the production baseline, the local-database workflow, and the eventual removal of direct shared-DB access. Committing to the practices is still the team's: adopt the local database and route every schema change through the pipeline as that groundwork lands. Treat these as a handoff to drive together, not a box to wait on or a lift to carry alone.

**Exit question:** *Can we deploy any commit automatically through one trusted path?*

---

## Phase 3 — Optimize

Shrink deployment size and work-in-progress; improve observability.

- [ ] Reduce batch size further — more, smaller MRs per day
- [ ] Limit work in progress so changes flow instead of piling up
- [ ] Speed up the pipeline (parallelize tests, cache dependencies) so feedback stays fast
- [ ] Add observability: metrics, logs, traces, deployment markers, alerts on the four DORA signals
- [ ] Replace manual approval gates with automated verification (smoke tests, health checks, canary metrics) where compliance allows
- [ ] Make rollback automatic on failed health checks where safe

**Exit question:** *Can we deliver small changes quickly and see what's happening?*

---

## Phase 4 — Deliver on Demand

Deploy any change to production at any time, based on business need.

- [ ] Any green commit on `main` can reach production the same day with no re-typed commands
- [ ] Deploys are routine and low-drama, not events
- [ ] Deploy and release are decoupled — flags control feature exposure
- [ ] Release communication is re-established on the new footing: notes built from user-facing releases (flag flips), not the deploy log — see [communicating-releases](communicating-releases.md)
- [ ] (Optional) Continuous Deployment: green commits auto-deploy with no human gate
- [ ] Metrics show the trend: frequency up, lead time down, failure rate steady or down, restore time down
- [ ] Roll the pattern out from the pilot team to the next team

**Exit question:** *Can we deploy any change to production whenever the business wants?*

---

## The journey, not just the destination

The phases above are the destination map. Getting there is a journey, and a few realities are worth naming up front:

- **It's a quarter, not a sprint.** A team coming from weekly releases typically spends weeks in Phase 1 alone, and the phases overlap — you don't finish one to begin the next. Size the effort accordingly and protect the time.
- **Expect a velocity dip in Phase 1 — and say so out loud.** Learning to decompose work, integrate daily, and keep `main` green feels slower before it feels faster. Tell leadership to expect the dip and to watch *stability* (the DORA metrics) alongside speed, so a temporary slowdown isn't mistaken for failure. The payoff shows up in Phases 3–4.
- **Deal with your existing long-lived branches on day one.** You can't adopt trunk-based development on top of a pile of week-old branches. For each open branch, decide: merge it now (decompose first if it's too big — see [decompose-a-branch](../sessions/session-2/exercises/decompose-a-branch.md)) or abandon it. Draw the line and start clean.
- **Sequence a mixed estate deliberately.** Prove CD on a low-blast-radius service first — a new cloud-native one is easiest — but commit to carrying it back to the monolith, where most of the pain and most of the payoff live. The [strangler-fig migration](../sessions/session-3/examples/strangler-fig-violations.md) is how the two estates converge. "We'll get to the monolith later" is how later becomes never.
- **Buy-in is earned, not announced.** CD spreads by proof. Let the pilot team's numbers make the case to the next team rather than mandating the practice estate-wide on day one — and let the engineers who'll live with trunk-based development help shape how it works on their service.

---

## Honest checkpoints

These are the places teams quietly stall. Re-check them every phase:

- **Branches crept back to multiple days.** Re-shrink. Long branches are big batches in disguise.
- **A meeting still decides releasability.** The pipeline must own the verdict, or it isn't CD.
- **Different artifacts per environment.** Build once, promote the same bytes.
- **Rollback is theoretical.** If you've never rehearsed it, you don't have it.
- **"We'll add tests later."** Later never comes; the pipeline is only as trustworthy as its gates.

---

## Where each item is taught

| Phase | Primary session |
| ----- | --------------- |
| 0 — Assess | [Session 1](../sessions/session-1/README.md) |
| 1 — Foundations | [Session 2](../sessions/session-2/README.md) |
| 2 — Pipeline | [Session 3](../sessions/session-3/README.md) |
| 3 — Optimize | [Session 3](../sessions/session-3/README.md) |
| 4 — Deliver on Demand | [Session 3](../sessions/session-3/README.md) |
