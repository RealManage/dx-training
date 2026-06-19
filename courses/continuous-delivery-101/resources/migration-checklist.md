# CD Migration Checklist — From Weekly Releases to Deploy on Demand

A team's working template for adopting Continuous Delivery, organized as the five-phase migration path from the [MinimumCD migration guide](https://beyond.minimumcd.org/docs/migrate-to-cd/). Copy this into your team's repo or wiki and check items off as you go.

> **The trap to break:** infrequent deployment is self-reinforcing. Deploy rarely → each deploy is large → large deploys are risky → fear of risk makes you deploy even less. The only way out is *smaller, more frequent* releases. Every phase below pushes batch size down.
>
> **Engineering Lead note:** Foundations (Phase 1) look like "slowing down to invest." They are not delay — they are the prerequisite for speed. Budget for them explicitly, give one pilot team a change window and the authority to deploy, and measure *stability alongside speed* (DORA's four metrics) from day one.

---

## Phase 0 — Assess

Understand your current state and name the constraints holding you back. **Do this first.**

- [ ] Complete the [Current-State Assessment](../exercises/current-state-assessment.md) as a team
- [ ] Measure today's baseline: deployment frequency, lead time, change failure rate, time to restore
- [ ] Record how long branches actually live (look at merged MRs over the last month)
- [ ] Identify the single biggest constraint (branch lifetime? manual deploys? slow tests? no rollback?)
- [ ] Pick one service and one team to pilot
- [ ] Agree on what "done with Phase 1" looks like and who decides

**Exit question:** *Do we honestly know where we are and what's holding us back?*

---

## Phase 1 — Foundations

Build the CI capabilities: daily integration, automated testing, small batches, stop-on-red.

- [ ] Adopt trunk-based development: branches off `main`, merged within a day
- [ ] Set a team agreement: every engineer integrates to `main` at least daily
- [ ] Decompose work so changes are small enough to ship daily (see [decompose-a-branch](../exercises/decompose-a-branch.md))
- [ ] Automated unit tests run on every MR branch before merge
- [ ] Automated tests run on `main` after merge
- [ ] Adopt the stop-the-line rule: a red `main` build is the team's top priority
- [ ] Introduce feature flags so incomplete work can merge safely (off by default)
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
- [ ] **Failing forward is the default recovery move** — a small fix flows through the pipeline in minutes
- [ ] Rollback is implemented and **rehearsed** as the emergency lever for costly, time-sensitive problems (redeploy previous artifact / shift Lambda alias)
- [ ] Stop-the-line extends to the deployment pipeline, not just the build

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
- [ ] Releases are routine and low-drama, not events
- [ ] Deploy and release are decoupled — flags control feature exposure
- [ ] (Optional) Continuous Deployment: green commits auto-release with no human gate
- [ ] Metrics show the trend: frequency up, lead time down, failure rate steady or down, restore time down
- [ ] Roll the pattern out from the pilot team to the next team

**Exit question:** *Can we deploy any change to production whenever the business wants?*

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
