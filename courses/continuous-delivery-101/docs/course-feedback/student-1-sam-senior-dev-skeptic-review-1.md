# Continuous Delivery 101 — Review 1

**Student:** Sam (Senior Developer, 10 yrs)
**Stance going in:** Skeptic. I like long-lived branches that stay open until the feature is genuinely done and polished, I take pride in the reviewed weekly release ritual, I got burned by a bad batch deploy once, and I think trunk-based development is "half-done work on main" and feature flags are conditional spaghetti that never gets cleaned up.
**Review date:** 2026-06-19
**Overall rating:** 6.5/10 — would I adopt/champion this?

## Executive summary

This is a well-written, internally-consistent course that argues its case better than most CD material I've read — the batch-size framing is genuinely strong and the `iac-baseline` walkthrough (`sessions/session-1/examples/current-state-pipeline-walkthrough.md`) earned real credibility by grading our *own* pipeline honestly instead of a strawman. But it never seriously engages my actual lived objections. It asserts that "tests + flags keep `main` green" as if that's free, hand-waves the human cost of the discipline (review-within-4-hours, daily integration, stop-the-line), and treats flag debt and the rollback data trap as solved by a sentence telling me to be disciplined. The persuasion is aimed at a manager evaluating a process; it is not aimed at *me*, the engineer who has to live with a half-built feature sitting in production for three weeks behind a flag. I left convinced of the math on batch size and unconvinced that the day-to-day reality is as low-drama as advertised.

## Section-by-section

### Course framing (README)

`courses/continuous-delivery-101/README.md` is clean and the "Warning Signs" section (lines 168–177) is a fair, non-preachy list. The "Pro Tips" (lines 213–219) are reasonable. Two things grated:

- Line 12 and line 94: "CD is not a tool you install" / "Resist the urge to ask which product gives us CD." Fine — but this is repeated *six or seven times* across the course (README, CLAUDE.md philosophy, Session 1 §1, troubleshooting). I got it the first time. The repetition reads like the course is bracing for an objection nobody in my chair is actually making. My objection isn't "which tool" — it's "this changes how my whole team works and you're underselling that cost."
- Line 251, the closing quote ("You don't rise to the level of your goals; you fall to the level of your systems"). A motivational-poster epigraph attributed to "DX Team" — it's actually James Clear. Borrowing a self-help line and reattributing it does not strengthen an engineering argument; it slightly weakens the credibility of the rest.

### Session 1 — Why CD & the Minimums

This is the strongest session and it's where I gave the most ground.

- The self-reinforcing trap diagram (`sessions/session-1/README.md` §2.1, lines 49–62) is correct and I won't argue it. Deploy rarely → big deploys → risky → deploy less is a real loop. I've lived it.
- The batch-size table (§2.2, lines 76–82) is the single most convincing thing in the course. "Rollback reverts everything" vs "rollback reverts one thing" lands *because that's exactly how I got burned* — one bad change in a week-batch, and the revert took the other 30 with it. The course should know that this table is its best weapon and lead with it harder.
- **But the evidence section (§2.3, lines 84–93) is thin where it most needs to be thick.** It name-drops DORA/*Accelerate* and asserts "higher throughput AND higher stability — they are not a trade-off." For a skeptic, one sentence and a link is not evidence; it's an appeal to authority. I've watched fads cite *Accelerate* the way people cite studies they haven't read. If you want to convert me, show me the actual correlation, the sample, the caveat that DORA is survey/self-report data — *then* I trust you more for being honest about its limits. As written, the "evidence" section is the weakest link in the strongest session.
- CD vs Continuous Deployment (§3, lines 97–112) is well done and the worked example (`cd-vs-continuous-deployment.md`) is the right move. Separating "we keep a human gate" from "code auto-ships" defused my single biggest fear before I could even raise it. Credit where due: lines 59–63 of that file name my exact objection ("we're not comfortable with code auto-shipping") and correctly diagnose it as a Continuous *Deployment* fear, not a CD one. That's the one place the course truly out-argued me.

### Session 2 — Trunk-Based Development & CI

This is the session aimed straight at me, and it's where it loses me the most.

- §2.1 (lines 29–38) on what long-lived branches cost: drift, merge pain, lost work, "it IS a big batch." All true. The "branches so short they never get the chance to drift" reframe (line 38) is a good line.
- **The whole section assumes the conclusion it needs to prove.** The argument is: long branches are bad → so make branches short → to make branches short, decompose + use flags → and tests+flags keep `main` safe. Every load-bearing claim ("small changes + tests-before-merge + flags keep `main` green", troubleshooting line 20) is *asserted*, never demonstrated against a hard case. My branches aren't long because I'm lazy. They're long because the feature has a UI that looks broken at 60% done, a DB migration in the middle, and a dependency on another team's endpoint that isn't ready. Show me *that* feature decomposed, not the greenfield Violations API where every slice is conveniently independent.
- §3 feature flags (lines 60–99): The `feature-flag.ts` example is clean code, I'll grant that. And lines 96–101 of that file ("FLAGS ARE TEMPORARY... delete the flag AND the dead path") at least *names* my objection. But naming it isn't answering it. "Track flag removal like any other work" is precisely the sentence that has never once worked at any company I've been at. Flag debt is real *because* "remove the flag later" competes with feature work and always loses. The course needs a structural answer (flag expiry dates enforced in CI? a lint rule that fails the build on a flag older than N days? a flag inventory that blocks the release?) — not an exhortation to be disciplined. Telling a team "be disciplined about cleanup" is how you *get* the spaghetti I'm worried about.
- §4 CI in practice (lines 102–131): the gates table (lines 108–115) is sensible. §4.3 "keep feedback fast" (lines 129–131) is honest about the failure mode (people route around slow gates). Good.
- **What's missing from Session 2 entirely: the cost side of the ledger.** "Integrate at least daily," "stop-the-line is the team's #1 priority," "review within ~4 hours" (`branching-antipatterns.md` line 60). These are presented as obviously-worth-it. They are *significant* standing taxes on a team's day. Stop-the-line means any red `main` drops everyone's work — that's an interrupt-driven culture that some senior people (me) find *more* stressful than a calm weekly release, not less. The course never weighs that. It treats the weekly release ritual purely as anxiety to be eliminated; it never acknowledges that the ritual also provides a *predictable, batched, low-interrupt* cadence that some teams deliberately value.

### Session 3 — The Pipeline

Technically the most solid session; the one I have the fewest factual quarrels with.

- The single-path / OIDC / immutable-artifact argument (§2, §4) is correct and I already agree with it — that part isn't controversial to me. Build once, promote the same bytes (§4.1, lines 67–81) is just good engineering, CD or not.
- §5.2 "fail forward first, roll back when it's costly" (lines 97–114) is the most intellectually honest part of the whole course, and it's the part that *most* directly addresses the guy who got burned. The rollback data trap (line 110: "rolling *code* back does not roll *data* back") is exactly the thing nobody mentions when they evangelize "just roll back" — and it's the thing that bit me. Naming it raised my trust in the authors.
- **But "fail forward by default" is doing a lot of quiet work to make CD look safe, and it slightly contradicts the pitch.** The whole sell is "small changes are low-risk." Then the recovery story is "when it breaks, ship *another* change forward." If forward-fixing is the default response to a prod problem, then we're conceding that prod problems happen often enough to need a default playbook — and that the fix is *more deploys*, faster. To a burned skeptic that reads as "we'll outrun our mistakes." Fail-forward is right *when the system genuinely is fast and safe*, but the course uses it as evidence that the system is safe, which is circular. It needs to say plainly: fail-forward only works if your canary + alarms + rollback are real and rehearsed — and most teams' aren't on day one.
- The target `.gitlab-ci.yml`: dev/qa auto-promote on green, prod keeps a manual gate (lines 141–161). This is the most reassuring single artifact in the course for someone like me, because it shows CD *doesn't* mean removing the human from prod. I wish Session 1 pointed at this concrete file earlier — it would have lowered my defenses sooner.

### Resources (minimums, glossary, checklist, troubleshooting)

- `minimums-reference.md` is the cleanest summary and the litmus test (lines 90–93) is a good one-liner I'll actually remember.
- `glossary.md` is genuinely useful and unusually honest — "Manual gate ... A transitional compromise" (lines 71–72) doesn't pretend the gate I like is wrong, just transitional. Fair.
- `migration-checklist.md` is the most *actionable* document and the "Honest checkpoints" (lines 92–100) are the best thing in the resources: "Rollback is theoretical. If you've never rehearsed it, you don't have it" — that's the voice I trust, the one admitting where teams quietly fail.
- **`troubleshooting.md` is where the course is weakest as persuasion.** It's structured as "objection → **The reality:** ...". Every one of my objections gets a confident one-paragraph rebuttal that begins by telling me I'm wrong. That format is *built to dismiss*, not to engage. "Trunk-based development will break `main` constantly" → "that fear is what tests and feature flags are for" (lines 19–21). That's not an answer, that's a slogan. The honest version would concede: *yes, `main` will break sometimes; here's the blast radius, here's the recovery time, here's why it's still net-better than a freeze.* A skeptic reading "The reality:" over and over feels handled, not heard.

### Exercises — my attempt

**Current-state assessment** (`exercises/current-state-assessment.md`): I filled it out for my team — a mix of two .NET Framework Web APIs on Azure VMs and one newer AWS Lambda service we're carving out via strangler-fig.

| Practice | My honest score | Note |
| -------- | --------------- | ---- |
| CI-1 Trunk-based dev | No | Long-lived feature branches, by design and by my preference. |
| CI-2 Integrate daily | No | We integrate when the feature's done — weekly-ish. |
| CI-3 Tests before merge | Partial | Lambda yes; .NET monolith has slow, flaky integration tests. |
| CI-5 Stop-the-line | No | A red build waits until someone has time. |
| CD-2 Pipeline is only deploy path | Partial | True for Lambda; the .NET monolith still gets the occasional hand-deploy to a VM. |
| CD-3 Pipeline decides releasability | No | *I* decide, in the release review. That's the ritual I value. |
| CD-5 Immutable artifacts | Partial | Lambda yes; monolith rebuilds per environment. |
| CD-8 Rollback rehearsed | No | We've never rehearsed it. (The checklist line "if you've never rehearsed it, you don't have it" stung — correctly.) |

Where it was hard / didn't fit:
- **The scorecard has no "and that's a deliberate choice" column.** Every "No" is framed as a deficiency. For CI-2 and CD-3, my "No" is a *choice* I'd defend. The assessment assumes the target state is settled and I'm just measuring distance from it. As a skeptic, I'd want a column: "why we do it this way today" — so the conversation is about trade-offs, not just gaps.
- The DORA baseline numbers (Part 2) are real and useful, and gathering them was the most persuasive part of the exercise *for me* — when I actually pulled our median branch lifetime (11 days) and median MR size (1,400 lines), the batch-size argument stopped being abstract. **This is the course's best conversion tool and it's buried in an exercise.** Pulling your own real numbers does more than any slogan.
- Part 3 "name the constraint": the hint (line 66, "for most teams it's branch lifetime") pre-answers the exercise. For my team it's actually the *flaky monolith integration tests* — until those are fast and trustworthy, short branches just mean more frequent exposure to a test suite I don't believe. The course assumes branch lifetime is the universal bottleneck; for a monolith maintainer it often isn't.

**Decompose a branch** (`exercises/decompose-a-branch.md`): The exercise *hands you* the Violations API and even hands you the model answer (lines 68–82). I deliberately ignored the gift and used a real feature from my world to actually pressure-test the method:

> "Migrate the resident-portal payment screen from the legacy .NET monolith endpoint to a new AWS payments service, with a new fee-calculation rule, and switch the UI over — without double-charging anyone or showing a half-built screen."

Where I got stuck / resisted:
1. **Slice 1 (scaffold, returns 501) is free; slices 4–6 are where the method actually has to earn it, and the exercise stops helping there.** My slice 4 is "write payments to the *new* service while the monolith is still the source of truth" — that's a dual-write across two systems with money involved. The exercise's expand/contract bullet (Part 2, line 50) is one sentence: "add new, write both, migrate, remove old." For *money*, "write both" means reconciliation, idempotency keys, and a back-out plan for the window where the two stores disagree. Each of those is itself a multi-day branch. The decomposition method is real but the example is rigged to be easy — no money, no cross-system consistency, no external dependency.
2. **The flag count exploded.** Doing it honestly I ended with `payments.newService.write`, `payments.newService.read`, `payments.newFeeRule`, `payments.uiSwitch` — four flags, interacting. The combinatorial state (new-write on, new-read off, fee-rule on...) is exactly the conditional spaghetti I came in worried about, and the course's only guidance is "flags are temporary, remove them." Decomposing a *real* feature produced *more* of the thing I object to, not less.
3. **"Independently shippable" broke down.** The exercise insists each slice merge "even if the next slice isn't started" (line 30). My read-path slice is useless and arguably dangerous without the write-path slice live first. The course's own answer key sidesteps this because the Violations slices genuinely are independent. Real features have ordering dependencies the toy doesn't.

Net: the exercise taught me the *method* (which I respect more than I expected to), but by using a sanitized example it let itself off the hook on every hard part of the method. I finished it *more* skeptical that "6–10 daily slices" survives contact with a real cross-system, money-handling feature.

## Where it lost me / objections it didn't answer

This is the section I care about most. In priority order:

1. **The human cost of the discipline is never on the books.** Daily integration, stop-the-line as #1 priority, 4-hour review SLA, constant flag cleanup — these are real, recurring taxes on senior engineers' attention. The course presents the weekly ritual as *pure* cost (anxiety) and CD as *pure* benefit. It never concedes that the ritual buys predictability and uninterrupted focus time, and that stop-the-line culture trades batched anxiety for *continuous* low-grade interruption. Until the course honestly debits CD's costs, I read it as a sales deck.

2. **Flag debt gets a sermon, not a mechanism.** `feature-flag.ts` lines 96–101 and Session 2 line 98 both say "flags are temporary, remove them, track it like any work." That is the exact promise that fails in practice at every shop. My decompose attempt *generated four interacting flags for one feature.* If the course's answer to "feature flags become permanent conditional spaghetti" is "be disciplined," it has conceded my point. I need an enforced mechanism (CI fails on stale flags / mandatory expiry / flag inventory gating release), or an honest admission that flag debt is a real cost you're choosing to take on.

3. **Every worked example is greenfield and conveniently decomposable.** The Violations API is a brand-new, single-service, side-effect-isolated, no-money, no-cross-system, no-UI Lambda. *Of course* it slices into 9 clean independent pieces. My world is strangler-fig migrations off a monolith with money, shared databases, and other teams' dependencies. The course's own framing (README line 10, CLAUDE.md) admits the monolith "will be with us for a long time" — then quietly builds *every* example on the greenfield service that carries none of the monolith's hard problems. The hard case is named and then dodged.

4. **"Trunk-based won't break `main`" is asserted against my direct experience.** I've seen `main` broken by a "small, tested" change because the test didn't cover the integration. The course's answer (troubleshooting lines 19–21) is "that's what tests and flags are for" — which assumes the tests are good, which is the very thing in question. It never engages the case where your tests *aren't* trustworthy yet (my flaky monolith suite). For those teams, shortening branches *increases* exposure to a `main` you can't trust. The course needs a "what if our tests aren't good enough yet" path, because that's most teams coming from weekly releases.

5. **The burned-by-a-bad-deploy engineer is never directly addressed.** The batch-size table implies "your bad deploy was big, small ones are safe," and fail-forward implies "you can recover fast." Both are *arguments*, neither is *reassurance*. Nobody says: here's what a bad small deploy actually looks like, here's the worst case, here's why the blast radius is bounded. I came in burned; I leave with arguments, not with my scar addressed.

## Confusing or assumed (clarity)

I'm a 10-year senior dev, so little of this was *over my head* — but a few leaps were assumed rather than shown:

- **"Production-like qa" is asserted but the gap is waved away.** Session 3 §5.1 (lines 93–95) and `template.yaml` `Mappings` (lines 42–47) make qa differ from prod only by `LogRetentionDays`. Real prod-likeness is about *traffic, data volume, and concurrency* — the things that actually cause "works in qa, breaks in prod." Smaller-scale-but-same-shape (line 95) glosses the hardest part. For a serverless toy it's fine; for my monolith with a 200GB prod database, "production-like qa" is a budget line nobody wants to fund, and the course doesn't acknowledge that.
- **`expand/contract` is used as a load-bearing term but taught in one clause.** Glossary has it (line ~46 references), Session 3 line 110 leans on it, the decompose exercise depends on it — but it's never *worked through* with a real schema change. For the one technique that makes "small DB changes" possible, that's under-taught relative to its importance. A junior would be lost; even I'd want the worked four-step migration shown once.
- **"Smoke test" appears in the pipeline (`./scripts/smoke-test.sh`) and the reading guide but the script doesn't exist** and what it asserts is never defined. The pipeline treats it as the thing that makes "promotion verified, not hoped" (`.gitlab-ci.yml` line 139, 171) — that's a load-bearing claim resting on a file that isn't shown. What does a real smoke test check? That's the difference between "verified" and "we curled the health endpoint and called it a day."

## Factual / technical concerns

Most of the AWS/GitLab mechanics are accurate. A few concerns:

- **The OIDC block in the target pipeline is incomplete in a way that would mislead.** `.gitlab-ci.yml` lines 52–61: it runs `aws sts assume-role-with-web-identity ... > /tmp/creds.json` and then a comment says "export the returned credentials ... elided for brevity." A learner copying this gets a pipeline that authenticates and then *doesn't use the credentials*, because the export is the actual hard part. Worse, GitLab's normal OIDC pattern uses the role-assumption via the AWS CLI's web-identity *profile/env-var* mechanism, not a hand-rolled `assume-role-with-web-identity` piped to a temp file. The "elided for brevity" hides the one part most likely to go wrong. Either show it correctly or don't show a broken skeleton.
- **`npm audit --audit-level=high` as a release-blocking gate is operationally naive.** `.gitlab-ci.yml` line 90 and `ci-pipeline.gitlab-ci.yml` line 90 make a dependency audit a hard gate on every merge. In reality `npm audit` produces frequent unfixable-today advisories (transitive, no patch available) that would block `main` for reasons unrelated to your change — directly violating the course's own "stop-the-line" sanctity and "keep feedback trustworthy" principle. A course preaching "don't let people route around gates" just built a gate teams will *have* to route around. Needs at least a note about allowlisting/triage, or it's teaching a gate that erodes trust in gates.
- **Canary `Canary10Percent5Minutes` with `EvaluationPeriods: 1`, `Period: 60`, `Threshold: 1` (`template.yaml` lines 112–125) is a hair-trigger.** A single error in any 60-second window during the 5-minute canary auto-rolls-back. For a board-facing API a single 4xx-turned-5xx or a transient downstream blip would trigger a rollback of a *good* deploy. As a teaching default it teaches flappy rollbacks. The course presents canary auto-rollback as "the default safety net" (`rollback-on-aws.md` line 73) without noting that alarm *tuning* is the entire game — a too-sensitive alarm is its own outage source.
- **`AutoPublishAlias` + alias-shift rollback (`rollback-on-aws.md` Strategy 1) then "reconcile the stack afterward" (line 63):** correct, but the course undersells that a raw `update-alias` puts running state *out of sync with CloudFormation*, so the very next pipeline deploy can silently re-introduce the bad version if the revert commit isn't in yet. It's mentioned (line 63) but as a footnote to a "fastest, seconds!" pitch. The fast lever has a sharp edge the framing soft-pedals.
- **Minor:** README line 251 attributes a James Clear line ("you don't rise to the level of your goals; you fall to the level of your systems") to "DX Team." Misattribution.

## Persona-specific section: What it would actually take to convince me

I said I'm fair, so here's the honest list. Do these and I move from "comply grudgingly" toward "champion."

1. **Put CD's costs on the same page as its benefits.** A literal table: what daily integration / stop-the-line / flag maintenance / review-SLA *cost* a senior engineer per week, against what they save. If you can show the net is positive *with the costs visible*, I believe you. If you hide the costs, I assume they're bad.
2. **Decompose a hard feature, not the Violations API.** Money, a cross-system strangler-fig migration, a real DB shape change, an external dependency that's late. Show me the dual-write reconciliation slice. Show me four interacting flags and how you *actually* retire them. If the method survives *that*, I'm sold. If you can only show me the greenfield toy, I'll assume the method only works on toys.
3. **Give flag debt a mechanism, not a virtue.** CI gate that fails on a flag older than its expiry. A flag inventory that blocks release. Anything enforced. "Be disciplined" is what I've watched fail for ten years.
4. **Address the burned engineer directly.** One page: "You had a bad deploy. Here's the worst case under CD, here's the bounded blast radius, here's the measured recovery time, here's why small+canary+rehearsed-rollback makes your scar less likely, not more." Talk to the scar, don't argue around it.
5. **Concede the weekly ritual's legitimate value, then beat it.** Don't tell me the ritual is pure anxiety. Tell me it buys predictability and focus — and then show CD delivering *both* the predictability *and* the lower risk. An argument that acknowledges what I'd lose is one I can trust about what I'd gain.
6. **Show one real RealManage team that did this and the before/after numbers.** Not DORA-in-general. *Us.* The pilot framing (README line 215, exercise Part 4) gestures at this but there's no completed case study. One real internal "we cut branch lifetime from 9 days to 1 and here's what happened" beats every slogan in the course.

## Recommendations

### High priority

- Add an explicit **"costs of CD" treatment** (Session 2 and the migration checklist). The course's biggest credibility gap with senior engineers is that it only debits the old way and only credits the new one.
- Replace the greenfield-only worked example with (or add alongside it) **one genuinely hard decomposition**: cross-system, stateful, money or external dependency. The strangler-fig framing is in the course's mission statement; honor it in the examples.
- Give **flag debt an enforced mechanism**, not an exhortation (`feature-flag.ts`, Session 2 §3.3).
- Fix the **OIDC skeleton** in `sessions/session-3/examples/.gitlab-ci.yml` (lines 52–61) — show the credential export correctly or replace with the standard GitLab OIDC pattern; "elided for brevity" hides the failure-prone part.
- Add a note that **`npm audit --audit-level=high` as a hard gate needs a triage/allowlist policy**, or it becomes the gate everyone routes around — contradicting the course's own stop-the-line principle.

### Medium priority

- Rework `troubleshooting.md` from "objection → **The reality:** you're wrong" into "objection → here's the legitimate part of your fear → here's the bounded answer." Concede before you rebut.
- Strengthen Session 1 §2.3 evidence: summarize the actual DORA findings and *their limits* (self-report data) rather than name-dropping. Honesty about the evidence's weaknesses would make a skeptic trust the strong parts more.
- Work through **one full expand/contract migration** end-to-end. It's load-bearing across three documents and never demonstrated.
- Provide the actual **`smoke-test.sh`** (or a worked description). "Verified, not hoped" rests on a file that doesn't exist.
- Tune the teaching **canary alarm** (`template.yaml`) off its hair-trigger defaults and add one sentence: "alarm tuning is the whole game; a too-sensitive alarm is its own outage."

### Nice to have

- Add a **"why we do it this way today" column** to the current-state assessment so it reads as trade-off analysis, not a deficiency audit.
- Add one **real internal before/after case study** once a pilot completes — it would outperform every external citation.
- Fix the **misattributed quote** (README line 251).
- Lead Session 1 with a pointer to the concrete target `.gitlab-ci.yml` showing prod keeps a manual gate — it disarms the "CD = losing control" fear earlier than Session 3.

## Verdict

**Comply grudgingly, leaning toward champion *for greenfield AWS services only*.** On a brand-new Lambda with good tests, the course convinced me — I'd run trunk-based with flags there tomorrow and not lose sleep. But for my monolith and the hard strangler-fig migrations, the course argued past my real objections (cost of the discipline, flag debt, hard decompositions, my scar) rather than through them. It's a strong deck that wins the meeting and loses the engineer. Make CD's costs visible, decompose a *hard* feature, give flag debt teeth, and talk to the burned engineer directly — do that and I'll carry this to my team instead of merely surviving the workshop.
