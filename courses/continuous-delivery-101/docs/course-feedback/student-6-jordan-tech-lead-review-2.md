# Continuous Delivery 101 — Review 2

**Student:** Jordan (Tech Lead, 7 yrs)
**Stance going in:** Same as round 1. I want CD. My only question is "can I run the migration next sprint without blowing my team's commitments and without inventing the rollout myself." I judge every "you should X" by whether it tells me HOW, in what order, and how I sell it to five ICs and a manager who counts story points. Round 1 verdict: the migration was "a destination map, not a journey."
**Review date:** 2026-06-19
**Overall rating:** 9/10 — up from 7.5. The journey now exists.

## Executive summary

This is a different document than the one I reviewed in round 1. The single sentence that summarized my whole complaint — "a destination map, not a journey" — is no longer true. The migration checklist now has a Phase 0 that handles my in-flight branches *on day one*, a "journey, not just the destination" section that names the velocity dip out loud and tells me to say it out loud too, a `what-cd-costs.md` that gives me the managing-up vocabulary I was missing, and a `strangler-fig-violations.md` worked example that finally shows CD carrying the load on the monolith half of my estate instead of pretending it's all greenfield Lambda. The brownfield decompose example I had to invent myself in round 1 is now *in the exercise*. Effort sizing is present ("a quarter, not a sprint," "weeks in Phase 1 alone"). The buy-in story moved from one unhelpful line to a real "buy-in is earned, not announced" item plus the cost-honesty that lets me earn it. Two specific technical nits I raised are fixed (canary thresholds, multi-account immutability). What's left is mostly thin spots, not holes — and two small things that regressed or stayed broken. I'd champion this now.

## Round-1 point-by-point tracking

This is the core of a round-2 review, so I'm leading with it. Each significant round-1 point, with status and the specific change.

### 1. "What do I do with the branch I already have open?" — IMPROVED (was my #1 gap)

Round 1: silence. The checklist started from a clean "adopt TBD" as if there's no in-flight WIP.

Round 2: addressed in **three** places, consistently.
- `resources/migration-checklist.md` Phase 0 (line 20): *"Inventory in-flight long-lived branches; for each, decide merge now (decompose first if it's too big) or abandon — you can't adopt trunk-based development on top of a pile of week-old branches."*
- The "journey" section (line 103): *"Deal with your existing long-lived branches on day one... For each open branch, decide: merge it now (decompose first if it's too big...) or abandon it. Draw the line and start clean."*
- `resources/troubleshooting.md` (line 55): *"a branch open more than a day gets split or merged."*

Honest assessment: this is exactly the instruction I asked for, placed where I'd look (Phase 0, before TBD adoption), and it cross-links to the decompose exercise for the "too big to merge" case. My round-1 ask was for three options (merge-behind-flag / re-slice / finish-then-adopt) with "when to use which." The course gives me **two** (merge-now-decomposing-if-needed / abandon) and drops "finish-then-adopt" — which is correct, actually; "finish the old way then start the discipline" is the soft option that lets long branches survive, and the course is right not to bless it. The "decompose first if too big" path effectively *is* my "re-slice" option. I'm satisfied. The only thing I'd still want is one sentence on the awkward middle case — a branch that's 90% done and genuinely a day from merging — but that's a nicety, not a gap.

### 2. Migration checklist has no sequencing, sizing, or velocity story — IMPROVED (sizing/velocity), PARTIALLY ADDRESSED (intra-phase sequencing)

Round 1: correct list of *what*, no order within phases, nothing sized, no acknowledgment Phase 1 costs velocity, the only velocity nod was aimed at my manager not me.

Round 2, broken into the three sub-complaints:

- **Effort sizing — IMPROVED.** The new "journey" section (line 101) says it plainly: *"It's a quarter, not a sprint. A team coming from weekly releases typically spends weeks in Phase 1 alone, and the phases overlap."* `what-cd-costs.md` reinforces it ("front-loaded," "recurring"). That's the calendar honesty I wanted. It's coarse (quarter / weeks, not per-item hours/days), but for a tech lead it's the right altitude — I can take "weeks in Phase 1" to my manager Monday. I'd have *liked* per-item sizing ("introduce feature flags = an afternoon for the mechanism, a sprint for the habit"), and that's still absent, but the macro sizing is the load-bearing part and it's there.
- **The velocity dip — IMPROVED, and now aimed at me.** This was my sharpest round-1 complaint: the dip note was advice for my *manager* (the Engineering Lead note), not a tool for *me*. Round 2 fixes the audience. The "journey" section line 102: *"Expect a velocity dip in Phase 1 — and say so out loud. Learning to decompose work, integrate daily, and keep main green feels slower before it feels faster. Tell leadership to expect the dip and to watch stability (the DORA metrics) alongside speed."* That's written to the person *running* the change. Combined with `what-cd-costs.md` ("CD moves the cost up front, where it's smaller and predictable"), I now have the actual sentences to manage up with. This is the biggest single improvement for my persona.
- **Intra-phase sequencing — PARTIALLY ADDRESSED.** Phase 0 now has a clear "do this first" and an internal order (assess → measure → inventory branches → pick pilot → sequence estate → agree on done). Good. But Phases 1–2 are still flat checklists — within Phase 1, "adopt TBD," "introduce feature flags," "code review under ~4h" are not ordered, and they have a real dependency (you can't hold sub-day branches without fast reviews *and* a flag mechanism, so those two arguably come *before* the daily-integration mandate, not after). The "journey" section gives me macro sequencing across phases, but the *micro* sequencing inside a phase is still left to me. For "next sprint" that's fine — I sequence Phase 1 myself — but it's the one place the checklist is still a list rather than a route.

### 3. No buy-in / change-management playbook — PARTIALLY ADDRESSED

Round 1: "CD spreads by proof, not mandate" was true and unhelpful as instruction. I wanted the *how*: get a skeptic to try a one-day branch, run the first stop-the-line without blame, bring QA along.

Round 2: real movement, but not a full playbook.
- **The "why buy-in is hard" is now honestly costed.** `what-cd-costs.md` is the change-management asset I didn't have. "The weekly release ritual wasn't stupid" (line 8) is *exactly* the framing that disarms my skeptic — it gives Sam's attachment to the ritual its due before replacing it, which is how you change a behavior without making someone defensive. "Daily integration is an interruption tax" and "stop-the-line is disruptive on purpose" (lines 30, 38) name the costs my team will feel, so I'm not overselling. That honesty *is* change management — a team that's been told the truth doesn't revert when it gets hard.
- **The checklist now has a buy-in item written to me.** Line 105: *"Buy-in is earned, not announced. CD spreads by proof. Let the pilot team's numbers make the case to the next team rather than mandating... and let the engineers who'll live with trunk-based development help shape how it works on their service."* The "let the engineers shape it" clause is the participation lever I asked for.
- **Governance covers the QA/releasability conversation.** `governance-and-compliance.md` "segregation of duties: the MR review *is* the control" (line 36) gives me the script for "the pipeline, not your sign-off, decides releasability" without making anyone feel obsoleted — it reframes their control as *stronger*, not gone. (RealManage has no separate QA team, so for me this is really the "bring the release-sign-off habit along" conversation, and the framing works.)

What's **STILL OPEN**: the three *specific in-the-room moments* I named are still not operationalized as worked scenarios. "Run the first stop-the-line without it feeling like blame" — still no concrete "here's what that looks like Tuesday at 2pm" walkthrough (see point 6 below; this regressed-by-omission relative to my explicit round-1 ask). "Get your skeptic to try a one-day branch" — the *materials* to persuade now exist (cost honesty + ritual-respect), but there's no step-by-step "first conversation" script. So: I'm now *equipped* to run the change (round 1 I wasn't), but the course stops at giving me the arguments and the principle — it doesn't rehearse the hard conversations. For my persona that's the difference between 9 and 10.

### 4. Removing the manual gates is treated as easy — IMPROVED

Round 1: "revisit each gate as trust grows" was the entire instruction; the journey from "manual everywhere" to "auto-promote to qa" was treated as a config change, not a trust/permission negotiation.

Round 2: materially better.
- `governance-and-compliance.md` is a whole resource on exactly this. "A manual gate is sometimes a control, sometimes debt" with the explicit test (line 32: *"what is the human actually deciding? If it's readiness, it's debt — automate it. If it's timing, authorization, or risk acceptance, it's a control — keep it"*) gives me the *criterion* to decide which gates to fight for removal and which to defend. That's the negotiation tool I was missing — I can now walk into the room and say "this gate re-checks readiness the pipeline proved, so it's debt; that gate authorizes timing, so we keep it," instead of "trust me, remove them."
- "The paragraph to hand an auditor (or a nervous client)" (line 99) is a ready-made artifact for the managing-up half.
- Session 3's Engineering Lead note (line 45) now frames the prod gate as legitimate-by-default and links governance.

Still slightly thin: it gives me the *criterion and the script* but not a *sequencing* of the negotiation (who to convince first — my manager? the account owner? — and in what order). But this went from "vaguest sentence in the course" to a properly-resourced topic. Solid improvement.

### 5. Session 1 mixed-estate ambiguity / "score the monolith or not" — IMPROVED

Round 1: the assessment didn't tell me whether to score the monolith, ignore it, or score it separately; my team would burn 30 minutes arguing scope.

Round 2: `exercises/current-state-assessment.md` now has an explicit mixed-estate callout (lines 15-16): *"If you run a long-lived monolith and newer services, they're usually at very different CD maturity. Either score your weakest system... or fill the scorecard once per system; don't average them into a misleading middle. Note which system each answer refers to."* This is precisely the "score per service, not per team" instruction I asked for, and the "don't average into a misleading middle" line is the exact failure mode I described. The checklist Phase 0 (line 21) reinforces it: *"If your estate is mixed... sequence it deliberately."* My round-1 prediction — "my five people will spend 30 minutes arguing about what we're even scoring" — is now pre-empted. Resolved.

### 6. Stop-the-line stated but never operationalized — STILL OPEN

Round 1: appears in minimums, glossary, and checklist; nowhere does it say what it *means* on a 5-person team Tuesday at 2pm.

Round 2: `what-cd-costs.md` (line 38) adds a useful *characterization* — *"Stop-the-line is disruptive on purpose. A red main becomes everyone's problem and you drop feature work to fix it... it asks the team to value flow over individual progress, which is a cultural cost."* That's better than round 1 (it at least tells me what it *feels* like and that it's a cultural ask). But it's still a description, not an operationalization. The concrete questions I raised — does *everyone* literally stop or just the author? what about red at 5pm Friday? — are still unanswered. My round-1 recommendation #7 ("operationalize stop-the-line with one concrete worked scenario") was not taken. Minor for me personally (I'll define our team's rule), but it's the clearest "asked, not done" item.

### 7. Greenfield-only decompose exercise + no external-dependency guidance — IMPROVED (brownfield), STILL OPEN (external dep)

Round 1: every decompose example was a new service from a `501` scaffold; my real work is brownfield; Twilio-style external deps got no guidance.

Round 2:
- **Brownfield — IMPROVED, strongly.** `exercises/decompose-a-branch.md` now has a full "What good looks like — a brownfield change" section (lines 87-99): the escalation `level` → `escalationHistory` expand/contract, seven slices, dual-write window, shadow-read parity, contract-last. This is the worked brownfield example I had to *invent* in round 1 — and it's structurally identical to the decomposition I built (expand → dual-write → backfill → dark read → shadow → ramp → contract). The exercise also added a callout at line 41 pointing brownfield changers to find the *seam* and add the path *dark*, and the Output section now asks for "the seam you introduce and the dual-write window named explicitly" (line 108). And `strangler-fig-violations.md` is a second, harder brownfield worked example against the *monolith* with a shared SQL Server — which is the case my round-1 review said the course kept dodging. This is a genuinely large improvement; the exercise no longer "only teaches greenfield."
- **External third-party dependency (Twilio) — STILL OPEN.** The brownfield example uses a self-contained schema change (DynamoDB/SQL), not an external paid API. The specific gaps I named — when to stub, how to ship an integration "dark" when it calls a real external system, how the qa/sandbox story works for a paid third party that needs an account per environment — are still not covered. The strangler-fig example is all internal (SQL ↔ DynamoDB). So a junior on my team decomposing "integrate Twilio" still gets no guidance on the riskiest slice. This was a medium-priority round-1 ask and it wasn't addressed.

### 8. README Progress Tracker conflates "completed course" with "team does CD" — IMPROVED

Round 1: team-state outcomes ("branches reliably live less than a day") sat in the same checklist as "Session 1 completed."

Round 2: `README.md` now splits the tracker into three labeled groups (lines 230-250): **Session Completion**, **Practical Application**, and **Team Adoption** — with the team-state outcomes ("Branches reliably live less than a day," "Rollback is automated and has been rehearsed") correctly isolated under "Team Adoption." This is exactly the split I recommended (nice-to-have #9). A learner can no longer tick the session boxes and think the months-long outcomes are near-term. Resolved.

### 9. Technical nits — MIXED (2 fixed, 1 still broken, 1 regressed)

Round 1 I flagged four small things my SRE and I would want fixed before pointing juniors at the examples.

- **Canary alarm threshold too twitchy — IMPROVED (fixed well).** Round 1: `Threshold: 1`, `EvaluationPeriods: 1`, `Period: 60` rolled back on a single stray error. `template.yaml` is now a *rate*-based alarm: `100 * errors / invocations`, `Threshold: 5`, `EvaluationPeriods: 2` (lines 147-153), scoped to the `:live` alias via the `Resource` dimension rather than the bare function. The inline comment (lines 116-125) even explains *why* — calls out my exact two concerns (scope and sensitivity). This is a model fix.
- **Multi-account immutability — IMPROVED (added, unprompted-but-welcome).** Session 3 (line 85) and the pipeline variables block (`.gitlab-ci.yml` lines 40-43) now warn that "promote the same bytes" across separate AWS accounts requires a cross-account-readable artifact bucket and "rebuilding per account would silently break the immutability guarantee." Good catch for RealManage's real account topology.
- **`deploy:dev` `needs:` list under-specified — STILL OPEN.** This is the one I'm most annoyed survived. `.gitlab-ci.yml` line 155 still reads `needs: [build:artifact, unit-tests, dependency-audit]` — it still omits `lint` and `validate:sam`. With GitLab `needs:`, `deploy:dev` starts the moment its listed deps finish, so a deploy to dev can begin while `lint` and `validate:sam` are still running, or even after they've *failed*. That directly contradicts the course's own central claim that green *means* deployable and that "validate + test + security" *is* the definition of deployable (the reading guide at line 176 literally says so). So the example undercuts the lesson. Worse, it's now *more* confusing than round 1, because `dependency-audit` is in the `needs:` list but is `allow_failure: true` (line 100) — so the one security job that *is* wired in can't actually block the deploy, while the two validate-stage jobs that *would* block are *not* wired in. Net effect: nothing in the `needs:` list enforces lint or sam-validate before dev. For a course this precise everywhere else, a careful reader will notice. Either list all gate jobs in `needs:` or add a one-line comment explaining the stage-ordering assumption. (Note: GitLab does enforce *stage* order for jobs *without* `needs:`, but these jobs *have* `needs:`, which overrides stage gating — so the omission is real, not theoretical.)
- **`smoke-test.sh` referenced but absent — REGRESSED (worse).** Round 1 this was already a gap; round 2 it's *more* load-bearing and still missing. `.gitlab-ci.yml` line 149 calls `./scripts/smoke-test.sh "${CI_ENVIRONMENT_NAME}"` after *every* deploy, the reading guide leans on it ("Verified, not hoped," line 181), Session 3's workshop asks learners to find "where you'd add a smoke test" (line 132), and the pipeline header sells "smoke tests after each deploy" as a headline difference from `iac-baseline` (line 8). Yet there is no `scripts/` directory and no smoke test anywhere under `violations-api/` (I checked — the worked service has `handler.ts`, `handler.test.ts`, `template.yaml`, `package.json`, `README.md`, nothing else). The whole auto-promotion safety story (dev→qa auto-promote is only safe *because* a smoke test gates it) rests on a script that doesn't exist as an example. A 10-line `smoke-test.sh` (curl the API URL from stack outputs, assert 200) would close the loop and make the auto-promotion argument concrete. This is my top remaining technical fix.

## Exercises — my round-2 re-attempt

Same team as round 1: "Resident Experience" squad, 5 engineers, one new AWS Lambda (`aws-notifications-api`) plus a co-owned slice of the legacy .NET resident-portal Web API on Azure VMs. Weekly release train, median branch ~5 days.

### Current-state assessment — `exercises/current-state-assessment.md`

The mixed-estate callout (lines 15-16) changed my experience materially. In round 1 I had to *pre-decide* "we score the Lambda, note the monolith separately" before the meeting to stop my team arguing scope. Now the exercise tells us to do exactly that — "fill the scorecard once per system" — so the scope decision is made *for* me and my team won't litigate it. I filled two scorecards:
- **Lambda:** mostly Partial — CI gates exist, but every deploy is `when: manual`, branches live ~5 days, no rehearsed rollback. CD minimums 2-5 partial.
- **Monolith:** mostly No — no GitLab pipeline (Octopus job a release manager triggers), branches longer, no immutable artifact story. The strangler-fig example (which I read this round) gives me a *path* for this column that didn't exist for me in round 1 — I now know the monolith answer is "carve violations-style capabilities out incrementally," not "give up."

Part 2 (DORA baseline) — my round-1 complaint that change-failure-rate and time-to-restore "aren't tracked anywhere I can query" is **STILL OPEN**. Line 46 still says "Pull the numbers; don't guess. (GitLab MR history, deployment logs, incident records.)" — it now at least *names sources*, which is a small improvement, but for a team that's never tracked DORA there's still no "estimate v1, instrument later" permission. My nice-to-have #11 ("how to estimate DORA numbers you don't yet track") wasn't taken. Minor.

Verdict: hand-off-ready now. The two pre-meeting caveats I had to supply in round 1 (scope, and "the monolith may never reach full CD and that's allowed") are both in the materials — the mixed-estate callout for scope, and the checklist's "commit to carrying it to the monolith / later becomes never" (line 104) for the monolith's status. I'd run this as-is.

### Decompose a branch — `exercises/decompose-a-branch.md`

I re-ran my real brownfield feature from round 1: "Add an SMS notification channel; residents choose SMS or email per notification type; integrate Twilio; existing email path stays working."

This time the exercise *meets* me where my work actually is. The brownfield worked example (lines 87-99) models exactly the expand/contract shape my feature needs, and the Output section (line 108) now explicitly asks for "the seam you introduce and the dual-write window named explicitly" — which forced me to be precise about slice 6 (backfill/default) in a way round 1's greenfield-only framing didn't. My decomposition was effectively unchanged from round 1 (it held up then too), but now the *exercise produced it* instead of me coaching the gaps. A junior on my team running this would now get the brownfield method from the page.

The one slice the exercise *still* doesn't help with is the Twilio integration itself — slices 3 and 4 in my plan (Twilio client behind an interface with a fake; wire SMS send behind a flag). Shipping a paid external integration "dark" — needing a Twilio account/credentials per AWS env, deciding stub-vs-real in qa, rate limits — gets no guidance in either the exercise or the strangler-fig example (which is purely internal SQL↔DynamoDB). So my round-1 external-dependency gap is exactly as open as it was. I figured it out; a junior wouldn't.

Verdict: went from "an 8 for greenfield, I'd coach the brownfield gaps myself" to "a 9 — I'd hand it to my team for brownfield work directly." The remaining point is the external-dependency slice.

## Can I actually run this with my team next sprint? (the test I care about)

Re-scoring round 1's three buckets:

**Now actionable that wasn't (round 1 "not addressed at all"):**
- **What to do with my open branches on day one** — Phase 0 line 20 + journey line 103. *Resolved.* I can run the branch inventory in our next planning session.
- **How to talk to my manager about the Phase-1 velocity dip in story-point terms** — journey line 102 + `what-cd-costs.md`. *Resolved at the right altitude.* I have the sentences; I'd still translate "weeks in Phase 1" into our sprint points myself, but the course now arms me, where round 1 only armed my manager.
- **How CD applies to the monolith half** — `strangler-fig-violations.md` is the entire answer, and it's the best new asset in the course for my persona. It's the "applies everywhere" claim finally *demonstrated*, on the hardest case, with a shared database and money on the line. *Resolved.*

**Still a multi-sprint negotiation, but now resourced:**
- **Kill long-lived branches / daily integration** — I now have the cost-honesty (`what-cd-costs.md`), the ritual-respect framing, and the branch-inventory starting move. Still my rollout to run, but I'm no longer inventing it from scratch.
- **Auto-promote dev/qa (remove manual gates)** — `governance-and-compliance.md` gives me the readiness-vs-authorization criterion and the auditor paragraph. The negotiation is still mine, but I have the script.

**Yes, week one, low cost (unchanged from round 1, still true):**
- Run both exercises as team sessions — *now better*, because brownfield and mixed-estate are handled, so I coach less.
- Adopt wall-clock branch age + <4hr review norm.
- Add missing CI gates to the Lambda from Session 2's file.

**Net:** Round 1 I said "the course stops at the classroom door; the actual migration is left to me." Round 2 it walks me out the door and a good way down the road. I can start the migration next sprint *with the course's materials in hand* rather than improvising the rollout. The remaining gaps (intra-phase sequencing, the in-the-room conversation scripts, the external-dep slice, and two example bugs) are real but they're the difference between "very good" and "perfect," not between "usable" and "not."

## New issues introduced this round (nothing wrong, two confusing)

I looked specifically for content added without making it actionable, and for new errors. Findings:
- **No bloat.** The new resources (`what-cd-costs.md`, `governance-and-compliance.md`, `communicating-releases.md`) are each load-bearing and cross-linked, not filler. The checklist's "journey" section is dense with actionable instruction, not motivational padding. This is the failure mode I was told to watch for and it didn't happen.
- **Confusing:** the `deploy:dev` `needs:` list now contains `dependency-audit` (which is `allow_failure: true`, so it can't gate) while omitting `lint` and `validate:sam` (which *should* gate). A reader trying to map "definition of deployable" onto the `needs:` list will be actively misled. (See point 9.)
- **Confusing:** the pipeline and workshop both now hinge on `smoke-test.sh`, and it doesn't exist. The auto-promotion safety argument depends on it. (See point 9.)

## Recommendations

### High priority
1. **Add `scripts/smoke-test.sh`** to `violations-api/` (10 lines: read the API URL from stack outputs, curl it, assert 200/health). The auto-promotion story is sold on it and it's the only major piece referenced-but-absent.
2. **Fix the `deploy:dev` `needs:` list** (`.gitlab-ci.yml` line 155): add `lint` and `validate:sam`, or annotate why they're omitted. As written it contradicts the course's own "green means deployable."

### Medium priority
3. **Add an external-dependency note to `decompose-a-branch.md`** — one paragraph on shipping a paid third-party integration dark (stub vs real per env, credentials per environment, the integration as its own dark slice). The brownfield example covers schema change but not the external-API case, which is just as common.
4. **Operationalize stop-the-line** with one concrete "Tuesday at 2pm, main goes red" worked scenario — who stops, what happens at 5pm Friday. Still the clearest asked-not-done item from round 1.
5. **Add the in-the-room conversation scripts** the buy-in section gestures at — a short "first stop-the-line without blame" and "get a skeptic to try a one-day branch" walkthrough. The *arguments* now exist (cost honesty, ritual respect); the *rehearsed conversations* don't.

### Nice to have
6. **Sequence the items within Phase 1 and Phase 2** of the checklist (or note the dependency that fast reviews + a flag mechanism precede the daily-integration mandate).
7. **Add "estimate DORA v1, instrument later"** permission to the assessment's Part 2 for teams that don't yet track change-failure-rate / time-to-restore.

## Verdict

**Champion, no longer with reservation about the migration itself.** Round 1 I championed the *practices* and said I'd improvise the *migration* myself. Round 2 the migration is an actionable journey: my day-one branches, the velocity dip and how to say it out loud, the monolith path, and the gate-removal criterion are all in the materials now — I could start next sprint with the course in hand, not as a thing I have to fill the gaps around. The revisions added substance and made it actionable, which is the harder thing to get right. Two example bugs (missing `smoke-test.sh`, the `needs:` list) and the still-unscripted in-the-room conversations are what hold it at 9 instead of 10. **9/10, up from 7.5.**
