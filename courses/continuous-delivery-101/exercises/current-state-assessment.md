# Exercise: Current-State Assessment

**When:** Session 1 (and again after Session 3 to measure progress)
**Format:** Team exercise, ~40 minutes
**Goal:** Score your team honestly against the MinimumCD practices and the controls CD relocates, then name your biggest constraint.

This is Phase 0 of the [migration checklist](../resources/migration-checklist.md): you cannot plan a journey without knowing where you start. Do this as a team, out loud. The value is in the disagreement — when one person says "we integrate daily" and another laughs, you've found something worth discussing.

> **Doing this solo, or new to the team?** You can still fill in most of it. The CI practices (Part 1) and your own branch/MR habits (Part 3's branch lifetime and MR size) you can answer from what you do day to day and from GitLab. The parts that need access or authority you may not have yet — deploy frequency and incident history (Part 3), the controls in Part 2, who authorizes a release — leave blank and bring to the team. A half-filled honest scorecard beats a fully-guessed one.

> **Mixed estate?** If you run a long-lived monolith *and* newer services, they're usually at very different CD maturity. Either score your **weakest** system — that's where the binding constraint lives — or fill the scorecard once per system; don't average them into a misleading middle. Note which system each answer refers to.

---

## Part 1 — Score the minimums

For each practice, mark one: **Yes** (consistently true), **Partial** (sometimes / for some services), or **No** (not yet). Be honest — a generous score helps no one.

In the **Deliberate?** column, mark ◆ when a Partial/No is a *conscious, defensible choice* — a control you keep on purpose (e.g., a mandated prod approval), not a gap you intend to close. A ◆ is not a failing score; an unmarked No is a gap. Telling the two apart is the point — see [Governance & Compliance](../resources/governance-and-compliance.md) for the debt-gate-versus-legitimate-control test.

### Continuous Integration

| # | Practice | Yes / Partial / No | Deliberate? | Evidence |
| - | -------- | ------------------ | ----------- | -------- |
| 1 | We use trunk-based development | | | |
| 2 | Every engineer integrates to trunk at least daily | | | |
| 3 | Automated tests run before merge to trunk | | | |
| 4 | Automated tests run on the merged result | | | |
| 5 | A red build stops feature work until it's fixed | | | |
| 6 | New work does not break delivered work | | | |

> **Same author for code and tests?** Increasingly that author is an AI agent. "Tests run" (minimums 3–4) can be *true* while "tests verify intent" is *false* — AI-written tests can mirror AI-written code and pass without checking anything. When you score 3 and 4, note whether something *independent* confirms your tests pin intent (a human, or a separate agent, reviewing tests against the agreed behaviour), not merely that they're green. See [CD when AI writes the code](../resources/ai-assisted-delivery.md).

### Continuous Delivery

| # | Practice | Yes / Partial / No | Deliberate? | Evidence |
| - | -------- | ------------------ | ----------- | -------- |
| 1 | We practice CI (all six above are Yes) | | | |
| 2 | The pipeline is the only way to deploy to shared environments | | | |
| 3 | The pipeline decides releasability; its verdict is definitive | | | |
| 4 | Artifacts meet an automated definition of deployable | | | |
| 5 | Artifacts are immutable (built once, never hand-edited) | | | |
| 6 | A red deployment pipeline stops feature work | | | |
| 7 | A production-like test environment exists | | | |
| 8 | Rollback is available on demand and has been rehearsed | | | |
| 9 | Application config is deployed with the artifact | | | |

---

## Part 2 — Score your controls (governance & communication)

CD doesn't remove governance — it *relocates* it into the pipeline and the flag flip, and usually *strengthens* it. Score how well your current controls are accounted for, so the migration carries them forward instead of quietly dropping them. Leaders and whoever owns release communication should weigh in here.

| Control | Yes / Partial / No | Evidence / who owns it |
| ------- | ------------------ | ---------------------- |
| **Audit trail** — we can show who deployed what, when, and from which commit | | |
| **Segregation of duties** — merges require a second person; author ≠ approver | | |
| **Least privilege** — deploy permissions live with the pipeline (OIDC); no standing human prod access | | |
| **Mandated approvals identified** — required (compliance/contractual) approvals are named and intentional, not habitual clicks | | |
| **Break-glass** — a documented emergency-change path exists that still records what happened | | |
| **Release communication** — clients/support learn that a release shipped, and there is a defined data source for it | | |
| **Release authorization** — when deploy ≠ release, someone authorizes the flag flip and it is recorded | | |

> For a regulated or client-facing team, a "No" here often outranks a "No" in Part 1: an ungoverned fast pipeline is harder to adopt than a slow one. Most of these are *strengthened* by CD — the work is making them explicit, not inventing them. See [Governance & Compliance](../resources/governance-and-compliance.md) and [Communicating Releases](../resources/communicating-releases.md).

---

## Part 3 — Measure today's baseline

Pull the numbers; don't guess. (GitLab MR history, deployment logs, incident records.)

| Metric | Today | Where you want to be |
| ------ | ----- | -------------------- |
| Deployment frequency (deploys to prod / week) | | On demand, multiple/day |
| Lead time for changes (commit → prod) | | Hours |
| Change failure rate (% deploys needing remediation) | | Low and steady |
| Time to restore (how fast you recover) | | Minutes |
| **Median branch lifetime** (look at last month's merged MRs) | | < 1 day |
| **Median MR size** (lines / files changed) | | Small |

> **Read median MR size as *review burden*, not author effort.** When an agent writes the code, a 600-line MR is cheap to produce and expensive to review — the old "big MR = lots of work" intuition breaks. Track it as a reviewability ceiling, and keep it small so review stays substantive.

---

## Part 4 — Name the constraint

You will not fix everything at once. Find the *one* thing most responsible for slow, risky delivery.

1. Which single unmarked "No" or "Partial" above hurts the most? (Skip the ◆ deliberate controls — those aren't gaps to close.) **____________**
2. What's the root cause? (Long branches? Manual deploys? Slow/missing tests? No rollback? No prod-like env? An ungoverned release?)
3. If you fixed only that one thing in the next month, what would improve?
4. Who owns the fix, and what does "fixed" look like?

> **Resist naming the constraint before you've scored** — surfacing it *is* the exercise, and pre-deciding the answer defeats the point. If the filled scorecard genuinely doesn't point anywhere, batch size / branch lifetime is the usual first domino for teams coming off weekly releases — but confirm that against your own evidence rather than defaulting to it.

---

## Part 5 — Pick your pilot

CD spreads by proof, not mandate.

- **Pilot service:** ____________ (a small, active, low-blast-radius service — a new AWS one is easiest, or a single capability you're carving out of the monolith via strangler-fig)
- **Pilot team:** ____________
- **First milestone:** ____________ (e.g., "branches under a day for two weeks straight")
- **How we'll know it worked:** ____________ (which metric moves)

---

## Output

By the end you should have:

- A filled scorecard (CI + CD minimums, with deliberate controls marked ◆)
- A controls scorecard (governance + release communication), each line with an owner
- Four baseline numbers plus branch lifetime and MR size
- One named constraint — a real gap, not a deliberate control — with an owner
- A pilot service, team, and first milestone

Carry these into Session 2 and 3, and into your [migration plan](../resources/migration-checklist.md). Re-run this assessment after the course to measure how far you moved.
