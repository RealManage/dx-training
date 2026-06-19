# Continuous Delivery 101 — Review 1

**Student:** Jordan (Tech Lead, 7 yrs)
**Stance going in:** Pragmatic and willing. I actually want CD. My problem is never "is this a good idea" — it's "can I make this real next sprint without blowing my team's commitments." I judge every "you should X" by whether it tells me HOW, in what order, and how I sell it to five ICs and a manager who counts story points.
**Review date:** 2026-06-19
**Overall rating:** 7.5/10 — would I adopt/champion this?

## Executive summary

This is the best-argued CD material I've read internally, and the conceptual spine is excellent: batch size as the master variable, CD ≠ Continuous Deployment, deploy ≠ release, fail-forward-first with a rehearsed rollback lever. The decompose-a-branch exercise is genuinely the high-value asset — I could run it in a room Monday and get usable output. But the course is strong on *what* and *why* and noticeably thinner on the operational *how* that a tech lead lives or dies by: it never handles the branch I already have open, it doesn't tell me how to get buy-in from ICs or my manager beyond an "Engineering Lead note," and the migration checklist is a correct destination map with no sequencing, no effort sizing, and no story-point reality. It convinced me of the goal. It did not yet equip me to run the migration.

## Section-by-section

### Course framing (README)

Strong. The "Warning Signs" (`README.md` lines 168-177) and "Pro Tips" (lines 213-219) are the kind of thing I'd paste into a team channel. The repeated "not a tool rollout" framing (line 94) is the right hill to die on and it'll save me the "which product gives us CD" conversation. Course Progress Tracker (lines 223-246) is a nice artifact — but it's a *completion* checklist, not a *team-state* checklist, and the two get conflated (more below).

One framing gap that matters for me specifically: the audience list (lines 29-34) includes "Tech Leads responsible for a team's branching and release workflow," but the leadership-specific guidance is delivered as occasional "Engineering Lead note" callouts aimed at the Director persona. There is no callout written *to the tech lead* — the person who has to do the actual sequencing, sell it sideways to peers, and protect the team's velocity while the foundations get built. That's my whole job and the course doesn't speak to it directly.

### Session 1 — Why CD & the Minimums

This session does its job: build conviction. The self-reinforcing trap diagram (`sessions/session-1/README.md` lines 49-62) and the big-batch/small-batch table (lines 76-83) are exactly what I need to win the room. `examples/cd-vs-continuous-deployment.md` is the single most useful page for managing-up — the "you can adopt CD without ever auto-shipping anything" line (line 63) defuses the objection I'll hear from my Director in the first thirty seconds.

The `current-state-pipeline-walkthrough.md` scoring table is excellent and honest — scoring the *real* baseline, calling the manual-gate state "a valid Phase-2 state" rather than a failure (line 46), is the credibility move that makes me trust the rest. Good.

Gap: the session assumes my team is a clean fit for the `iac-baseline` pipeline. My team co-owns a .NET monolith on Azure VMs *and* a new Lambda. Session 1 nods at the monolith in the framing (lines 23-27) and then the workshop is entirely about the AWS baseline. When I sit down to score, half my estate has no GitLab pipeline at all (it deploys through an Octopus job a release manager triggers). The assessment doesn't tell me whether to score the monolith, ignore it, or score them separately — and that ambiguity is the first thing my team will argue about.

### Session 2 — Trunk-Based Development & CI

The strongest *teaching* session for my purposes. `examples/branching-antipatterns.md` is a keeper — anti-pattern 3 (the shared `develop` branch, lines 30-36) and anti-pattern 6 (the short branch that *waits* two days for review, lines 54-60) are exactly the two my team does, and naming them precisely is half the battle. The "wall-clock age, not your intent" line (line 58) is going on a sticky note.

`feature-flag.ts` is the right altitude: dependency-free, default-off, and — critically — it tells me when to *stop* (the "FLAGS ARE TEMPORARY" block, lines 95-103) and when to graduate to AppConfig/LaunchDarkly (lines 104-114). That "graduate when you need X" guidance is the kind of HOW I keep wishing the rest of the course had.

Where it gets thin for a tech lead: the CI minimums table (lines 108-116) says "Each engineer merges to `main` every day." Fine as a target. But I have a senior dev (think the "Sam" persona) who will say "I can't always get a reviewable, mergeable slice done in a day on hard work." The course's answer is "decompose better + feature flags," which is *directionally* right but skips the messy middle: spikes, research tasks, a two-day refactor with no natural seam. There's no guidance on "what daily integration looks like for work that genuinely doesn't slice cleanly." That's the objection I'll actually face, and I'd want an answer in hand.

### Session 3 — The Pipeline

Technically the most satisfying. The target `.gitlab-ci.yml` is a real artifact I can diff against my actual file — dev/qa auto-promote, prod keeps a `when: manual` timing gate (lines 141-161), smoke test after every deploy. The reading-guide footer (lines 163-178) is genuinely good documentation. `rollback-on-aws.md` is the best single page in the course: the fail-forward-vs-rollback decision table (lines 102-110), the data trap (line 39), and "rehearse it or you don't have it" (lines 112-121) are all correct and actionable.

The honest problem for me: this session shows the *destination* pipeline beautifully and the *journey* barely. Going from "every stage `when: manual`" (today) to "dev/qa auto-promote" (target) is not a YAML edit — it's a trust negotiation with QA, my manager, and probably whoever owns the prod AWS account. The course treats removing the manual gates as a config change. The hard part isn't the config; it's getting permission and confidence to remove them, and the course doesn't coach that transition. "Revisit each gate as trust grows" (line 45) is the entire instruction, and it's the vaguest sentence in an otherwise sharp session.

### Resources (minimums, glossary, checklist, troubleshooting)

`minimums-reference.md` and `glossary.md` are excellent reference material — the litmus test (minimums lines 89-93) is the cleanest one-sentence statement of the goal in the whole course. `troubleshooting.md` is the resource I'd reach for most because it's framed as objections, which is how problems actually arrive at my desk. The expand/contract and "deploy from a laptop" entries (lines 82-98) are concrete fixes, not platitudes.

The `migration-checklist.md` is where my persona has the most to say, because it's the document I'd actually hand my team — and it's the weakest of the resources relative to what a tech lead needs. Detail in the dedicated section below.

### Exercises — my attempt

**Context for both:** my team is the "Resident Experience" squad — 5 engineers. We own one new AWS service (`aws-notifications-api`, a TS Lambda fronting SNS) and we co-own a slice of the legacy .NET resident-portal Web API on Azure VMs. Weekly release train. Median branch ~5 days. The mixed estate is the whole point of my lens.

**Current-state assessment** — `exercises/current-state-assessment.md`

I filled it out. It works, and Part 3 ("name the constraint," lines 57-66) is the single most valuable prompt — it forced my honest answer (branch lifetime) instead of letting me sprawl. The scorecard structure is sound and the "evidence" column (lines 17-38) is a good forcing function against generous self-scoring.

Where it was hard or under-specified:

- **The mixed estate breaks the scoring.** Almost every row was "Partial," but for two *different reasons* — the Lambda is genuinely partial (manual gates), the monolith is "No, and it can't be Yes on this pipeline at all." A single Yes/Partial/No per row hides that. The exercise needs an explicit instruction: *score per service, not per team*, or at minimum a note on how to handle a team that owns both a CD-able service and a legacy one. As written, my five people will spend the 30 minutes arguing about *what we're even scoring* instead of *where we are*.
- **Part 2's baseline numbers assume I can pull them.** "Pull the numbers; don't guess" (line 44) — except change failure rate and time-to-restore aren't tracked anywhere I can query. There's no pointer to *how* to derive them (incident tickets? deploy logs? a DORA tool?). For a team that's never measured DORA, this row is aspirational, and the exercise should say "estimate v1, instrument later" rather than imply the data exists.
- **Part 4 (pick a pilot) is good but the dependency is unstated:** it tells me to pick a low-blast-radius service, which for me is obviously the Lambda. But it never says the uncomfortable thing — *the monolith may never reach full CD, and that's allowed*. My team needs to hear that explicitly or they'll read "CD everywhere" as the bar and check out.

Verdict on this exercise: produces a usable picture and a clear single next step (shrink branches on the Lambda). It needs ~3 sentences on mixed estates and on estimating DORA numbers to be hand-off-ready.

**Decompose a branch** — `exercises/decompose-a-branch.md`

This is the best exercise in the course and the thing I'd run first with my team. I deliberately did *not* use the provided greenfield Violations API scenario — I used a real in-flight feature from my backlog to pressure-test it:

> "Add an SMS notification channel. Residents can choose SMS or email per notification type. We integrate Twilio for SMS. The existing email path stays working."

This is the realistic case the exercise's own scenario dodges: a *brownfield* change against a live service with an existing schema, an existing endpoint, and a third-party dependency. My decomposition:

1. Add `channelPreference` column/attribute to the prefs store, default `email` — expand step, nothing reads it. *(Infra/data, safe.)*
2. Pure function: given a notification type + prefs, return target channel(s). Unit-tested, unwired. *(Pure logic.)*
3. Twilio client behind an interface, injected, with a fake for tests; no caller yet. *(Dark, unused.)*
4. Wire SMS send behind `notify.sms` flag (off); email path untouched. *(Dark.)*
5. `PUT /preferences` accepts `channelPreference` behind `prefs.channel` flag (off). *(Dark.)*
6. Backfill/dual-write so old rows have a default. *(Expand/contract migrate step.)*
7. Turn on `prefs.channel` dev→qa→prod. *(Release.)*
8. Turn on `notify.sms`. *(Release.)*
9. Contract: remove any old single-channel assumption + dead flag paths. *(Cleanup.)*

It held up. The expand/contract framing in Part 2 (lines 49-51) directly produced slices 1, 6, and 9, and the flag discipline produced the dark ordering. **That's a genuine win — the exercise transfers to brownfield even though it never claims to.**

But two things the exercise should own and doesn't:

- **It only teaches greenfield.** Every example (the scenario, lines 11-20; "what good looks like," lines 67-82) is a *new* service starting from a `501` scaffold. My actual problem is almost never a new service — it's a change to something already in production with live data and live users. The hardest decomposition skill is finding the seam in *existing* code, and the exercise hands me a blank canvas instead. It should include at least one brownfield worked example (modify a live endpoint without breaking it), because that's 80% of what my team does.
- **The third-party dependency got no guidance.** Twilio is the riskiest slice (auth, rate limits, an external account that has to exist in each AWS env). The exercise's slicing rubric (lines 25-30) has nothing about external dependencies — when to stub, how to ship the integration "dark" when it calls a real external system, how the sandbox/qa story works for a paid third party. I figured it out, but a junior on my team wouldn't.

Verdict on this exercise: yes, I could hand this to my team and get good decomposition — *for new services*. For our in-flight brownfield work I'd have to coach the gaps myself. Add one brownfield example and an external-dependency note and it's a 9.

## Where it lost me / objections it didn't answer

These are the things I'd raise in the room, in priority order:

1. **"What do I do with the branch I already have open?"** This is my number-one operational question and the course never answers it. On the day I adopt this, my team has 3-4 multi-day branches in flight. Do I force-merge them behind flags? Abandon and re-slice? Finish them the old way and start the new discipline after? The migration checklist starts from a clean "adopt TBD" (Phase 1, line 30) as if there's no in-flight WIP. There always is. **A "migrating your in-flight branches" subsection is the single biggest missing piece for a tech lead.**

2. **The migration checklist has no sequencing, sizing, or velocity story.** It's a correct list of *what* must be true, grouped into phases, but: (a) within a phase the items aren't ordered — which do I do first? (b) nothing is sized — is "introduce feature flags" an afternoon or a sprint? (c) there's no acknowledgment that Phase 1 *costs velocity* while we learn, and no guidance on what to tell my manager when the burndown dips. The one nod to this is the Engineering Lead note (`migration-checklist.md` lines 6-8: "Foundations look like slowing down... budget for them explicitly"). That's advice for *my manager*, not for *me* — I'm the one who has to negotiate the dip and keep the team from reverting under deadline pressure (which the troubleshooting guide itself flags as the #1 failure mode, `troubleshooting.md` line 109). The course names the trap and doesn't hand me the tool to avoid it.

3. **No buy-in / change-management playbook for the person running the change.** "CD spreads by proof, not mandate" (`current-state-assessment.md` line 72) is true and unhelpful as instruction. *How* do I get my skeptical senior to try a one-day branch? *How* do I run the first stop-the-line without it feeling like blame? *How* do I get QA to accept that the pipeline, not their sign-off, decides releasability — without making them feel obsoleted? These are the conversations that actually determine whether the migration sticks, and the course delegates all of them to a single "pair with a teammate" line and the troubleshooting objections. The objections doc helps me win an *argument*; it doesn't help me run a *change*.

4. **Removing the manual gates is treated as easy.** Covered under Session 3 above — the journey from "manual everywhere" to "auto-promote to qa" is a trust/permission negotiation, and "revisit each gate as trust grows" is the only coaching offered.

## Confusing or assumed (clarity)

I'm experienced, so little was *confusing* — but a few leaps would trip my team:

- **"Stop-the-line" is stated as a rule, never operationalized.** It appears in the minimums (`minimums-reference.md` line 17), the glossary (line 114), and the checklist (Phase 1, line 35). Nowhere does it say what it actually *means* on a 5-person team: Does everyone literally stop? Just the author? What if `main` goes red at 5pm Friday? My team will nod at the principle and have no idea what to *do*. One concrete "here's what stop-the-line looks like Tuesday at 2pm" example would fix it.
- **The Progress Tracker conflates "I completed the course" with "my team does CD."** `README.md` lines 239-245 ("Branches reliably live less than a day," "Rollback is automated and rehearsed") are *team-state* outcomes that take months, sitting in the same checklist as "Session 1 completed." A learner will tick the session boxes and feel the others are also near-term. Separate "course completion" from "migration progress."
- **"Integrate to trunk daily" vs. real review latency.** Session 2 says merge daily; anti-pattern 6 says a branch waiting on review is a long branch. Both true, but together they assume sub-4-hour reviews (checklist Phase 1, line 37) that my team doesn't hit today. The dependency between "daily integration" and "fast review culture" is real and the course states the target without sequencing the change.

## Factual / technical concerns

The technical content is largely sound and I trust it (OIDC role assumption, SHA-tagged immutable artifacts, alias-shift rollback, canary auto-rollback, expand/contract). A few small things a careful tech lead would catch:

- **`.gitlab-ci.yml` `deploy:dev` needs (line 145):** `needs: [build:artifact, unit-tests, dependency-audit]` omits `lint`, `validate:sam`, and `iac-scan`. With GitLab `needs:`, a job runs as soon as its listed deps finish, so this could deploy to dev while lint/sam-validate are still running or even *failing* (depending on `needs` failure semantics). If the intent is "deploy only after the full definition-of-deployable is green," the `needs` list is under-specified and undercuts the course's own central point that green *means* deployable. Worth either listing all gate jobs or adding a note.
- **Smoke test script is referenced but never shown.** `.gitlab-ci.yml` line 139 and the reading guide both lean on `./scripts/smoke-test.sh`, and the workshop asks learners to find "where you'd add a smoke test" (Session 3 README line 128) — but there's no example smoke test anywhere in `violations-api/`. For a course this concrete elsewhere, that's a conspicuous gap; a 10-line example would complete the loop.
- **Canary alarm threshold is twitchy (`template.yaml` lines 112-125):** `Threshold: 1`, `EvaluationPeriods: 1`, `Period: 60` means a *single* error in any 60-second window trips the alarm and auto-rolls-back the canary. For a teaching example that's arguably fine, but it's not production-realistic (one transient client 500 reverts a good deploy), and the course presents the template as `iac-baseline`-aligned and real. A one-line caveat ("tune thresholds to your real error budget") would prevent someone copying it straight to prod.
- **"Config travels with the artifact" is slightly oversold for env vars.** The feature-flag pattern resolves env vars *at cold start* (`feature-flag.ts` lines 58-69). Flipping a flag therefore requires a deploy/update, not a runtime toggle — which the doc *does* acknowledge (lines 104-114), but Session 3's framing of flag-flip as a near-instant "release" (`session-3/README.md` line 80, "release = flip the flag, no deploy needed" in Session 2 line 80) overstates it for the *minimal* pattern. The instant-toggle story is only true once you've graduated to AppConfig/LaunchDarkly. The course knows this; the summary lines just blur it.

None of these are disqualifying. They're the kind of thing my SRE (the "Riley" persona) and I would want corrected before we point juniors at the examples as copy-paste references.

## Can I actually run this with my team next sprint?

This is the section I care about most, so I'm being blunt. Scoring each thing on "can I do it Monday."

**Yes, next sprint, low cost:**

- **Run the decompose-a-branch exercise in a 45-min team session.** Fully ready. I'd swap in a real brownfield feature and coach the external-dependency gap myself. *Highest-value, lowest-friction thing in the course.*
- **Run the current-state assessment as a team.** Ready, with my caveats — I'd pre-decide "we score the Lambda, we note the monolith separately" before the meeting so we don't burn the 30 minutes on scope.
- **Adopt the "wall-clock branch age" norm and the <4hr review target.** Concrete, no tooling, I can announce it Monday.
- **Add the missing CI gates to the Lambda (`coverage threshold`, `npm audit`, `cfn-lint`).** The Session 2 CI file is copy-paste-able. Half a day.

**Yes, but it's a multi-sprint negotiation, not a task:**

- **Kill long-lived branches / daily integration.** The behavior change. The course gives me the *why* and the *technique* (flags, decomposition) but not the *change-management*: handling my skeptic, handling in-flight branches, protecting velocity during the dip. I can run it, but the course leaves me to invent the rollout.
- **Auto-promote dev/qa (remove manual gates).** Requires QA buy-in, manager sign-off, and probably an account-permission conversation. The course shows the destination YAML and almost none of the negotiation.

**Not addressed at all, and I need it:**

- **What to do with my currently-open branches** on day one. Silence.
- **How to talk to my manager about the Phase-1 velocity dip** in story-point terms. The checklist tells *the manager* to budget for it; it doesn't arm *me* to ask.
- **How CD applies (or honestly doesn't) to the monolith half of my team's world.** The framing says "applies everywhere"; the operational content is 100% greenfield AWS. My team will notice.

**Net:** I can get real value in week one (the two exercises + the CI gates), and that genuinely lowers my risk of recommending it. But the course stops at the classroom door. The actual *migration* — sequencing, buy-in, in-flight WIP, the velocity conversation — is exactly the tech-lead job and exactly where the material thins out.

## Recommendations

### High priority

1. **Add a "Migrating your in-flight branches" subsection** to the migration checklist (Phase 0→1 boundary). Three options (merge-behind-flag / re-slice / finish-then-adopt) with a "when to use which." This is the #1 missing piece for any team that isn't greenfield.
2. **Make the migration checklist operational, not just correct:** order items within each phase, attach rough effort sizing (hours/days/sprint), and add an explicit "expect a velocity dip in Phase 1 — here's how to frame it to leadership *and* to the team" note written *to the tech lead*, not only the Director.
3. **Add a brownfield decompose example** to `decompose-a-branch.md` — modifying a live endpoint with existing data and an external dependency, expand/contract front and center. Greenfield-only undersells the exercise's real power.
4. **Fix the `deploy:dev` `needs:` list** (or annotate it) so the example doesn't contradict "green means deployable," and **add the missing `smoke-test.sh`** the pipeline and workshop both reference.

### Medium priority

5. **Add a short change-management / buy-in playbook** — how to run the first stop-the-line without blame, how to get a skeptic to try a one-day branch, how to bring QA along when "the pipeline decides releasability." Convert the troubleshooting objections from "win the argument" into "run the change."
6. **Make the current-state assessment mixed-estate-aware:** explicit "score per service," and a line giving teams permission that some legacy systems may never reach full CD.
7. **Operationalize stop-the-line** with one concrete worked scenario.
8. **Add a caveat to the canary alarm thresholds** and to the "flip the flag = instant release" claim for the minimal env-var pattern.

### Nice to have

9. **Split the README Progress Tracker** into "course completion" vs. "team migration state" so learners don't conflate finishing the reading with finishing the migration.
10. **A guidance note on work that doesn't slice cleanly** (spikes, research, seamless refactors) so "integrate daily" has an honest answer for the hard cases.
11. **A short "how to estimate DORA numbers you don't yet track"** pointer in the assessment's Part 2.

## Verdict

**Comply enthusiastically, leaning champion.** The argument convinced me and the two exercises are good enough to run next sprint — that alone earns it a recommendation. But it's not yet a migration playbook for a tech lead: it never touches my in-flight branches, the checklist is a destination without a route or a velocity budget, and the buy-in problem — my actual hardest job — is left as an exercise for the reader. Close those three gaps and I'd champion it without reservation; as it stands I'll champion the *practices* and improvise the *migration* myself.
