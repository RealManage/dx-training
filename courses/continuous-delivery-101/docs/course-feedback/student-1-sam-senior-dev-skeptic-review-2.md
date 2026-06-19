# Continuous Delivery 101 — Review 2

**Student:** Sam (Senior Developer, 10 yrs)
**Stance going in:** Skeptic. I like long-lived branches kept open until a feature is genuinely done, I take pride in the reviewed weekly release ritual, I got burned by a bad batch deploy once, and I came in believing trunk-based is "half-done work on main" and feature flags are conditional spaghetti nobody cleans up.
**Review date:** 2026-06-19
**Round-1 rating:** 6.5/10
**Overall rating (Round 2):** 8.5/10 — would I adopt/champion this?

## Executive summary

They actually did the work. The two changes I said would move me from "comply grudgingly" toward "champion" — debit CD's costs honestly, and decompose a *hard* feature — both landed, and landed well. `resources/what-cd-costs.md` is the page I asked for: it gives the weekly ritual its due, lists the recurring taxes line by line, and — critically — replaces the flag-debt sermon with an actual mechanism (birth certificate + CI stale-flag check + deletion-as-a-slice + flag-explosion-as-reslicing-signal). `sessions/session-3/examples/strangler-fig-violations.md` is the cross-system, money-handling, shared-database carve-out I said I needed to believe the method; it names the dual-write window, idempotency, reconciliation, and contract-last without hand-waving. The OIDC skeleton is fixed, the canary alarm is retuned off its hair-trigger, and `npm audit` is now an advisory, not a hard gate. That's four of my five top objections genuinely addressed and three of my four technical concerns fixed.

What's still open is smaller but real: the `smoke-test.sh` the pipeline depends on *still doesn't exist* (the glossary now defines the term, but "verified, not hoped" still rests on a missing file), the DORA "evidence" section still name-drops *Accelerate* with zero honesty about its survey/self-report limits, the misattributed James Clear quote is still credited to "DX Team," and the current-state assessment still has no "this is a deliberate choice" column and still pre-answers the constraint with "for most teams it's branch lifetime." None of those is a dealbreaker. The course no longer argues *past* me; it argues *through* me. I'd carry this to my team now — including the monolith — with a short list of nits, not a list of grievances.

## What changed since Round 1 — point by point

For each significant Round-1 point, the verdict and the change that earned it.

### 1. "The human cost of the discipline is never on the books." → **IMPROVED — sufficient.**

This was my #1 objection: the course only debited the old way and only credited the new one, reading like a sales deck. It's fixed at the source.

- `resources/what-cd-costs.md` ("The bill", lines 27–50) puts CD's costs on the page in my own terms: daily integration as an "interruption tax," the ~4-hour review turnaround as a genuine cost to heads-down work ("you can't disappear into deep focus for a day and ignore the MR queue"), stop-the-line as "disruptive on purpose" and a *cultural* cost not just a technical one, the decomposition learning curve, and the Phase-1 velocity dip. That is the honest ledger I asked for.
- The "Why the bill is still worth paying" close (lines 52–56) makes the argument I'd actually accept: you're already paying *more*, later and unpredictably; CD moves the cost up front where it's smaller and predictable. Net cheaper *with the costs visible* is the only version of this pitch I trust.
- `sessions/session-1/README.md` §2.1 (line 74, "In fairness to the weekly release") and the entire "The weekly release ritual wasn't stupid" section of `what-cd-costs.md` (lines 8–24) do the thing I explicitly demanded under "Concede the weekly ritual's legitimate value, then beat it": predictable window, batching point for comms, a forcing function for "is it ready?" — each named, each given a replacement. It says outright "you are *trading* a familiar ritual for better mechanisms, not shedding pure waste." That's not a strawman of my ritual; it's a fair account of it.
- `resources/troubleshooting.md` even adds a dedicated objection (line 39, "Isn't CD a lot of ongoing overhead — daily reviews, flag upkeep, stop-the-line?") whose answer opens with "yes, and we don't pretend otherwise." A year ago that section told me I was wrong; now it concedes first.

This is sufficient for me. The costs are no longer hidden, so I no longer assume they're bad.

### 2. "Flag debt gets a sermon, not a mechanism." → **IMPROVED — sufficient, with one caveat.**

This was the objection I was most certain they'd fumble, because every shop answers it with "be disciplined." They didn't fumble it.

- `resources/what-cd-costs.md` "Paying down flag debt — a mechanism, not a sermon" (lines 58–84) opens by conceding my exact point — "discipline loses to deadlines every time" — and then gives four enforced mechanisms: a **birth certificate** (owner + creation date + removal condition recorded in a flag inventory, "a flag with no expiry is a bug"); a **CI stale-flag check** that fails/warns the pipeline past expiry so "the system, not someone's memory, enforces cleanup"; **deletion as a planned slice**, not a someday-ticket; and — the one that most directly answers my decompose attempt — **a flag explosion is a re-slicing signal**: "If a feature needs several interacting flags, the slices probably aren't independent. Fix the decomposition rather than adding more switches."
- That last bullet is the answer to my Round-1 complaint that decomposing my real feature *generated* four interacting flags. The course's position is now: that's a decomposition smell, not an inevitability — and the strangler-fig example proves it by carving the whole migration behind essentially *one* routing flag plus per-step expand flags, not a combinatorial mess.
- `sessions/session-2/examples/feature-flag.ts` (lines 95–106) and §3.3 (line 98) now echo the mechanism instead of the old "track it like any work" exhortation, and add the lifecycle distinction (short-lived release flags vs long-lived ops/kill-switch flags) plus when to graduate to AppConfig/LaunchDarkly.

**Caveat (not a blocker):** the CI stale-flag check is described, never *shown*. For a course that's otherwise concrete to the YAML line, a 15-line CI job that greps for flags and fails on ones past expiry would close this completely. As written it's a real plan, which is a night-and-day improvement over a virtue — but it's still one notch short of "here's the gate, copy it." Mechanism described, mechanism not demonstrated.

### 3. "Every worked example is greenfield and conveniently decomposable." → **IMPROVED — fully sufficient. The single biggest win of this revision.**

This is the one I was most skeptical they'd actually do, and it's the one they did best.

- `sessions/session-3/examples/strangler-fig-violations.md` is the hard decomposition I asked for, almost to the letter of my Round-1 "what it would take to convince me" list ("Money, a cross-system strangler-fig migration, a real DB shape change... Show me the dual-write reconciliation slice"). It carves Violations out of an ASP.NET-on-IIS-on-Azure-VMs monolith backed by SQL Server: seam (slice 0) → new service dark (1) → **expand: dual-write** keyed by a stable `violationId` (2) → idempotent resumable **backfill** (3) → **shadow reads** for parity (4) → **canary read cutover** by percentage (5) → **write cutover** (6) → **contract last** (7).
- The "The hard parts (no hand-waving)" section (lines 67–90) names every single thing I said the toy dodged: the dual-write window where two stores must agree, idempotency on `violationId` so retries/backfill can't double-count, an explicit authoritative-store-at-each-step decision, a **reconciliation** check *plus a documented answer for what you do when the two disagree*, and — this is the line that won me — "If violations carried *fees* (they often do), this window is where correctness bugs cost money, so treat reconciliation as part of the work, not a nicety." That is my dual-write-money objection, answered in my own terms.
- It also answers my Round-1 complaint that "independently shippable" broke down on ordering dependencies: the strangler narrative is explicitly *ordered* (you cannot shadow-read before you dual-write), and it reframes the coexistence of both systems as "not a failure to finish — it *is* the strategy." That's a better answer than the greenfield exercise's "every slice is independent" fiction.
- `exercises/decompose-a-branch.md` now carries a full brownfield/expand-contract worked answer of its own ("What 'good' looks like — a brownfield change", lines 87–99) and a Part-1 callout (line 41) plus an Output requirement to name "the **seam** you introduce and the **dual-write window**" (line 108). When I re-ran my own payment-migration feature against it this round, the exercise *helped at slices 4–6* instead of abandoning me there — which is exactly where Round-1 said it stopped helping.

This is fully sufficient. The course's mission statement promised the monolith and strangler-fig; it now honors that in the examples instead of dodging to the greenfield Lambda. If I could only point a teammate at one new file, it's this one.

### 4. "'Trunk-based won't break `main`' is asserted against my direct experience." → **PARTIALLY ADDRESSED.**

I argued the course never engages the case where your tests *aren't trustworthy yet* (my flaky monolith suite), so shortening branches just increases exposure to a `main` you can't trust.

- Improved: `resources/troubleshooting.md` line 21 now reads less like a slogan ("Long-lived branches don't prevent breakage — they *defer and concentrate* it into one painful merge") and the migration-checklist "We'll add tests later" / "the pipeline is only as trustworthy as its gates" checkpoint (lines 117) at least acknowledges that gate quality is load-bearing.
- Still open: there's no "what if our tests aren't good enough yet" path. The course still assumes the gates are trustworthy and reasons from there. For a team coming off weekly releases with a flaky integration suite — which is *most* teams it's aimed at — the honest sequence is "fix/quarantine the flaky tests *before* you shorten branches, or short branches will just expose you to a `main` you can't trust faster." That ordering advice still isn't anywhere. Partially addressed because the language is less sloganeering; still open because the actual failure mode (untrustworthy tests as the binding constraint, not branch lifetime) goes unhandled. See also point 9.

### 5. "The burned-by-a-bad-deploy engineer is never directly addressed." → **PARTIALLY ADDRESSED.**

I asked for one page that talks to the scar: "here's the worst case under CD, here's the bounded blast radius, here's the measured recovery time."

- Improved indirectly: the batch-size table, fail-forward-first (`sessions/session-3/README.md` §5.2), the retuned canary that *auto-rolls-back most bad deploys before they fully roll out* (`rollback-on-aws.md` Strategy 2), and the "rehearse it or you don't have it" framing collectively bound the blast radius better than Round 1 did. `rollback-on-aws.md` "Make it boring — rehearse both" (lines 112–121) now tells you to *time* both the fail-forward and rollback loops, which is closer to "measured recovery time."
- Still open: there's no single artifact that addresses the burned engineer *directly and emotionally* — "you had a bad deploy; here is the bounded worst case under CD." The pieces exist scattered across three files; nobody assembles them into the reassurance I asked for. This is a softer miss than Round 1 (the pieces are now all present and correct), but the deliberate "talk to the scar" page I requested wasn't written. Partially addressed.

### 6. "fail-forward is doing quiet work / circular safety argument." → **IMPROVED — sufficient.**

Round 1 I argued fail-forward-by-default is circular — using "we recover fast" as evidence the system is safe. The revision is honest about the precondition.

- `sessions/session-3/README.md` §5.2 and `rollback-on-aws.md` now state plainly that fail-forward is "safe by the same logic as any deploy — small batch, tests, canary," and the "Make it boring — rehearse both" section makes the prerequisite explicit: "A recovery move you've never run is a hope, not a capability... Only then count minimum #8 as met." That's the "fail-forward only works if your canary + alarms + rollback are real and rehearsed" concession I asked for. Sufficient.

### 7. README repetition of "CD is not a tool you install." → **STILL OPEN (minor).**

Round 1 I noted this is repeated six or seven times. It's still all over: `README.md` line 12 and line 99, `sessions/session-1/README.md` §1/§4.1, CLAUDE.md philosophy. `what-cd-costs.md` adds another ("You don't have to buy a tool", line 92). It's a minor irritation, not a substantive flaw — and at least `what-cd-costs.md`'s version is in service of the honest ledger now. Leaving it flagged but I won't die on this hill.

### 8. Misattributed James Clear quote. → **STILL OPEN.**

`README.md` line 256 *still* attributes "You don't rise to the level of your goals; you fall to the level of your systems" to "DX Team." It's James Clear, *Atomic Habits*. I flagged this in Round 1 as a small credibility ding; it's a one-line fix and it's still wrong. Cheap to fix, mildly corrosive to leave.

### 9. Current-state assessment lacks a "deliberate choice" column / pre-answers the constraint. → **STILL OPEN.**

- `exercises/current-state-assessment.md` Part 1 still has only `Yes / Partial / No | Evidence` (lines 19–40) — no column for "and that's a deliberate trade-off we'd defend." My CI-2 and CD-3 "No"s are still framed purely as gaps, not as choices to be discussed. The new "Mixed estate?" guidance (line 15) is a genuine improvement (it tells me to score my weakest system or score per-system rather than averaging — that's good), but it doesn't address the deficiency-audit framing.
- Part 3 still hints "for most teams it's branch lifetime" (line 68). For my monolith the real constraint is the flaky integration suite, and the exercise still pre-answers around that. Tied to point 4.

## New material — is any of it wrong, overclaimed, or confusing?

I went looking for new errors the revisions might have introduced. Mostly clean. A few flags:

- **The CI stale-flag check is asserted as a mechanism but never shown (overclaim-adjacent).** Covered under point 2. The heading literally says "a mechanism, not a sermon" (`what-cd-costs.md` line 58) — so show the mechanism. A described CI job is a plan; a 15-line YAML job is a mechanism. Right now it's one notch short of its own headline.
- **`smoke-test.sh` regression-by-omission.** New since Round 1, the glossary added a clean "Smoke test" definition (`resources/glossary.md` lines 77–78) and Session 3 added a workshop step "Where you'd add a smoke test" (`sessions/session-3/README.md` line 132). Good — *except* the actual file `./scripts/smoke-test.sh` that the pipeline calls (`sessions/session-3/examples/.gitlab-ci.yml` line 149) and the reading guide leans on ("Verified, not hoped", line 181) **still does not exist** anywhere in the repo. I confirmed: no `scripts/` dir, no `*.sh` file in the course. So the revision *defined the word* but left the load-bearing claim resting on a missing file. From a skeptic's chair that's worse than Round 1 in one narrow sense: the course now teaches the concept confidently while the artifact proving it is still vapor. A 10-line script (curl the API Gateway URL from the stack output; assert 501-while-dark / 201-when-flagged) would close it. STILL OPEN.
- **The canary comment slightly overstates what the alarm proves (minor).** `template.yaml` line 152 says "roll back if >5% of alias traffic errors for two straight minutes" — correct and a *huge* improvement over the old `Threshold:1` hair-trigger. But on a low-traffic prod alias, `errors/invocations` can still swing wildly on tiny denominators (one error out of three invocations in a 60s window = 33%). The comment is honest that "a rate that scales with traffic is the right signal" but doesn't mention the low-traffic-denominator failure mode. Minor; the alarm is now *defensible*, where before it was actively bad.
- **`what-cd-costs.md` "What you don't pay" is well-judged, not overclaimed.** I braced for this section to wave away real costs as imaginary. It doesn't — it limits itself to three genuinely-not-real fears (auto-ship, buying a tool, losing the gate) and correctly routes each to governance. No complaint.
- **No new factual errors introduced in the OIDC fix or the strangler example.** Both read as correct (see technical section).

## Factual / technical concerns — Round-1 items revisited

- **OIDC skeleton "elided for brevity." → FIXED.** `sessions/session-3/examples/.gitlab-ci.yml` lines 54–66 now use the standard web-identity pattern: write the token to a file, `export AWS_WEB_IDENTITY_TOKEN_FILE`, `AWS_ROLE_ARN`, `AWS_ROLE_SESSION_NAME`, then `aws sts get-caller-identity` to "fail fast if the role or its trust policy is wrong." That is exactly the correct GitLab→AWS OIDC mechanism I said they should show instead of the hand-rolled `assume-role-with-web-identity` piped to a temp file. A learner can now copy this and it will actually authenticate *and* use the credentials. Fully addressed.
- **`npm audit --audit-level=high` as a release-blocking hard gate. → FIXED.** Same file, `dependency-audit` job (lines 93–100): now `allow_failure: true`, scoped to `--omit=dev`, with a comment that nails my exact argument — "blocking on a noisy signal... just trains teams to route around it. Surface it every pipeline; escalate specific CVEs deliberately." A course preaching "don't let people route around gates" no longer ships a gate teams would be forced to route around. Fully addressed.
- **Hair-trigger canary alarm. → FIXED.** `template.yaml` lines 112–154: replaced `EvaluationPeriods:1 / Period:60 / Threshold:1` (one stray error = rollback of a healthy deploy) with an error-*rate* metric-math expression (`100 * errors / invocations`) scoped to the `:live` alias `Resource` dimension, `EvaluationPeriods: 2`, `Threshold: 5`. The inline comment even explains the two fixes (scope and sensitivity) and notes "alarm tuning is the right signal" — which is the "alarm tuning is the whole game" sentence I asked for. Addressed (modulo the low-traffic-denominator nuance above).
- **Alias-shift puts running state out of sync with CloudFormation. → STILL PRESENT, still soft-pedaled but adequately noted.** `rollback-on-aws.md` Strategy 1 line 63 still carries the "reconcile the stack afterward... so IaC and reality agree" note as a bullet under the "fastest, seconds!" pitch, and the choosing-your-move table (line 109) now routes "keep IaC and running state consistent" to Strategy 3 (redeploy prior artifact) as the *preferred* clean path. That's a reasonable handling — the sharp edge is named and a cleaner alternative is offered. I'll downgrade this from "soft-pedaled" to "adequately flagged."

## Persona-specific: did the six things I said would convince me actually happen?

My Round-1 "What it would take to convince me" list, scored:

1. **Put CD's costs on the same page as its benefits.** → **DONE.** `what-cd-costs.md`. The literal honest ledger I asked for.
2. **Decompose a hard feature, not the Violations API.** → **DONE.** `strangler-fig-violations.md`. Money, cross-system, real SQL Server schema dance, dual-write reconciliation, the lot.
3. **Give flag debt a mechanism, not a virtue.** → **MOSTLY DONE.** Mechanism described (birth certificate + CI stale-flag check + deletion-as-slice + reslicing signal); not shown as runnable CI. One notch short.
4. **Address the burned engineer directly.** → **PARTIAL.** Pieces all present and correct; the dedicated "talk to the scar" page wasn't written.
5. **Concede the weekly ritual's legitimate value, then beat it.** → **DONE.** "The weekly release ritual wasn't stupid" + "In fairness to the weekly release." Names what I'd lose before selling what I'd gain.
6. **Show one real RealManage team's before/after numbers.** → **NOT DONE.** Still no completed internal case study; the current-state-assessment's own-numbers exercise remains the closest thing (and remains the best conversion tool — when I pulled my real branch lifetime in Round 1 it did more than any slogan). Understandable if no pilot has finished, but still the one external-vs-internal-evidence gap.

Four of six fully, one mostly, one partial, one not-done-but-understandable. That's the difference between a 6.5 and an 8.5.

## Recommendations

### High priority

- **Ship the actual `smoke-test.sh`** the pipeline and reading guide both depend on (`sessions/session-3/examples/.gitlab-ci.yml` line 149; reading guide line 181). 10 lines: read the stack's `ApiUrl` output, curl it, assert 501-while-dark / 201-when-flagged. "Verified, not hoped" cannot rest on a file that doesn't exist — this is the one remaining place the course claims more than it shows.
- **Show the CI stale-flag check** as runnable YAML, not just prose (`resources/what-cd-costs.md` line 67). The section is titled "a mechanism, not a sermon" — finish the job and make it a mechanism on the page.

### Medium priority

- **Strengthen the DORA evidence with its limits** (`sessions/session-1/README.md` §2.3). STILL OPEN from Round 1: it name-drops *Accelerate* / DORA and asserts "both higher throughput AND higher stability" with zero acknowledgment that DORA is survey/self-report data. For a skeptic, honesty about the evidence's weakness would make me trust the strong parts *more*. One sentence on the methodology and its caveat closes it.
- **Add a "what if our tests aren't trustworthy yet" path** (point 4). For teams whose binding constraint is a flaky suite, not branch lifetime, the honest sequence is fix/quarantine the tests *before* shortening branches. The course still assumes good gates.
- **Add a "deliberate choice" column** to `exercises/current-state-assessment.md` Part 1, and soften the "for most teams it's branch lifetime" pre-answer in Part 3 (line 68) so it reads as trade-off analysis, not a deficiency audit.

### Nice to have

- **Fix the misattributed quote** (`README.md` line 256 — it's James Clear, not "DX Team").
- **Note the low-traffic-denominator nuance** on the canary error-rate alarm (`template.yaml` line 152).
- **Add a single "to the burned engineer" page** assembling the (now-present) blast-radius/recovery pieces into the direct reassurance I asked for.
- **Dial back the "CD is not a tool" repetition** by one or two instances.
- **A completed internal pilot case study** when one exists — still the one thing that would outperform every external citation.

## Verdict

**Champion — for greenfield AWS services unreservedly, and now for the monolith and strangler-fig migrations too, with a short fix-list I'd hand them on the way in.** Round 1 I said this was "a strong deck that wins the meeting and loses the engineer." This round they put CD's costs on the books, decomposed the hard feature I dared them to, gave flag debt teeth, and fixed every technical defect I cared about except one missing script. It no longer argues past my objections; it argues through them — and concedes what I'd lose before selling what I'd gain, which is the only register a skeptic actually trusts. Ship the smoke test, show the stale-flag CI job, and be honest about DORA's limits, and I've got nothing left but nits. 8.5/10, up from 6.5.
