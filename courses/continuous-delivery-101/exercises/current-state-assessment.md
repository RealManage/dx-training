# Exercise: Current-State Assessment

**When:** Session 1 (and again after Session 3 to measure progress)
**Format:** Team exercise, ~30 minutes
**Goal:** Score your team honestly against the MinimumCD practices and name your biggest constraint.

This is Phase 0 of the [migration checklist](../resources/migration-checklist.md): you cannot plan a journey without knowing where you start. Do this as a team, out loud. The value is in the disagreement — when one person says "we integrate daily" and another laughs, you've found something worth discussing.

---

## Part 1 — Score the minimums

For each practice, mark one: **Yes** (consistently true), **Partial** (sometimes / for some services), or **No** (not yet). Be honest — a generous score helps no one.

### Continuous Integration

| # | Practice | Yes / Partial / No | Evidence |
| - | -------- | ------------------ | -------- |
| 1 | We use trunk-based development | | |
| 2 | Every engineer integrates to trunk at least daily | | |
| 3 | Automated tests run before merge to trunk | | |
| 4 | Automated tests run on the merged result | | |
| 5 | A red build stops feature work until it's fixed | | |
| 6 | New work does not break delivered work | | |

### Continuous Delivery

| # | Practice | Yes / Partial / No | Evidence |
| - | -------- | ------------------ | -------- |
| 1 | We practice CI (all six above are Yes) | | |
| 2 | The pipeline is the only way to deploy to shared environments | | |
| 3 | The pipeline decides releasability; its verdict is definitive | | |
| 4 | Artifacts meet an automated definition of deployable | | |
| 5 | Artifacts are immutable (built once, never hand-edited) | | |
| 6 | A red deployment pipeline stops feature work | | |
| 7 | A production-like test environment exists | | |
| 8 | Rollback is available on demand and has been rehearsed | | |
| 9 | Application config is deployed with the artifact | | |

---

## Part 2 — Measure today's baseline

Pull the numbers; don't guess. (GitLab MR history, deployment logs, incident records.)

| Metric | Today | Where you want to be |
| ------ | ----- | -------------------- |
| Deployment frequency (deploys to prod / week) | | On demand, multiple/day |
| Lead time for changes (commit → prod) | | Hours |
| Change failure rate (% deploys needing remediation) | | Low and steady |
| Time to restore (how fast you recover) | | Minutes |
| **Median branch lifetime** (look at last month's merged MRs) | | < 1 day |
| **Median MR size** (lines / files changed) | | Small |

---

## Part 3 — Name the constraint

You will not fix everything at once. Find the *one* thing most responsible for slow, risky delivery.

1. Which single "No" or "Partial" above hurts the most? **____________**
2. What's the root cause? (Long branches? Manual deploys? Slow/missing tests? No rollback? No prod-like env?)
3. If you fixed only that one thing in the next month, what would improve?
4. Who owns the fix, and what does "fixed" look like?

> **Tip:** for most teams coming from weekly releases, the binding constraint is **branch lifetime / batch size**. Almost everything else gets easier once branches live hours instead of weeks. If you're unsure where to start, start there.

---

## Part 4 — Pick your pilot

CD spreads by proof, not mandate.

- **Pilot service:** ____________ (pick a small, active, low-blast-radius service — ideally a new AWS one)
- **Pilot team:** ____________
- **First milestone:** ____________ (e.g., "branches under a day for two weeks straight")
- **How we'll know it worked:** ____________ (which metric moves)

---

## Output

By the end you should have:

- A filled scorecard (two tables of Yes/Partial/No with evidence)
- Four baseline numbers plus branch lifetime and MR size
- One named constraint with an owner
- A pilot service, team, and first milestone

Carry these into Session 2 and 3, and into your [migration plan](../resources/migration-checklist.md). Re-run this assessment after the course to measure how far you moved.
