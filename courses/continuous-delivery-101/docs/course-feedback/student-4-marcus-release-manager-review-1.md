# Continuous Delivery 101 — Review 1

**Student:** Marcus (QA / Release Manager, 8 yrs)
**Stance going in:** I own the release sign-off gate and I personally write the weekly release-notes email to clients every Friday. Two fears: (1) "the pipeline decides releasability" reads like it deletes my judgment; (2) if we ship in many small batches instead of one weekly release, nothing in the course tells me what replaces the weekly changelog email, or how users find out what changed.
**Review date:** 2026-06-19
**Overall rating:** 6/10 — would I adopt/champion this?

## Executive summary

This is a well-built, honest course and it genuinely reframed my gate better than I expected — it does not bulldoze QA, it relocates my work and even hands me a stronger lever (rollback rehearsal, definition-of-deployable ownership). I came in braced for "automate the humans away" and instead found a careful "the human approves *timing*, the pipeline proves *readiness*" split that I can live with. **But the course has a hole the size of my actual job:** in three sessions, ten resource files, and a worked service, there is exactly *one* half-sentence about communicating a release to anyone outside the engineering team (`sessions/session-1/examples/cd-vs-continuous-deployment.md:43`, "maybe alongside a comms email"). Release notes, changelogs, customer/support notification, and the fate of my weekly Friday email are simply not addressed. For a course that explicitly moves us from one weekly release to many small ones, that is not a nice-to-have — it breaks a live operational workflow and gives me no replacement. That gap is why this is a 6 and not an 8.

## Section-by-section

### Course framing (README)

Clear and honest. I appreciated `README.md:94` ("Not a tool rollout") and the warning-signs list (`README.md:168-176`) — "a human meeting, not the pipeline, decides whether a build is releasable" named my world without sneering at it. The Target Audience (`README.md:29-34`) lists Engineers, Tech Leads, Engineering Leaders, and Teams. **QA and Release Management are not on that list.** I am the person who today *is* the releasability gate, and the course's headline practice is literally about who owns releasability — yet my role isn't named as an audience. First small signal that the course was written from the builder's seat, not the gatekeeper's.

### Session 1 — Why CD & the Minimums

The strongest session for me. The self-reinforcing trap diagram (`session-1/README.md:49-62`) and the big-batch/small-batch table (`session-1/README.md:76-83`) are exactly the argument I'd make to a nervous board: small batches are *easier* to sign off, not harder. "Diagnosis hell" (`session-1/README.md:66`) is precisely why I dread the Friday release — a week of changes, and when prod breaks Monday I cannot tell which of thirty things did it.

The CD-vs-Continuous-Deployment split (`session-1/README.md:101-107`) directly defused fear #1. "A human decides *when* to release. This is our target." Good. The Engineering Lead note (`session-1/README.md:111`) — "does not mean losing control... releasing stays a deliberate decision" — is the sentence I needed.

Where it started to wobble for me: the current-state walkthrough (`session-1/examples/current-state-pipeline-walkthrough.md`) scores the pipeline against nine CD minimums, and *none of the nine is about communicating the release*. That's faithful to minimumcd.org, so I don't fault the scoring — but it's the first sign that "releasability" in this course means "is the artifact technically deployable," never "have we told the people who depend on knowing it shipped."

### Session 2 — Trunk-Based Development & CI

Solid, and the feature-flag material is the part that quietly answers half my anxiety (more below). The deploy-vs-release split (`session-2/README.md:68-71`) is the most useful single idea in the course for someone in my seat: "Deploy = code is in the environment, a technical act. Release = the feature is visible to users, a business decision." That reframes my gate from "approve every deploy" to "own the moment of user-visible release" — which is *more* aligned with what clients actually care about, not less.

The anti-patterns example (`session-2/examples/branching-antipatterns.md`) is good teaching. Anti-pattern 6, "long-open MR waiting for review" (`branching-antipatterns.md:54-60`), is a place my sign-off process is the bottleneck — and the course is right to call wall-clock age the thing that matters. Fair hit; I'll take it.

What's missing here is the bridge from "release = flip the flag" to "...and here's how the people who need to know learn that the flag flipped." The flag flip *is* the release event from a customer's perspective, and it produces no deploy, no pipeline run, no artifact — so it's invisible to any changelog tooling that watches deploys. The course never notices this.

### Session 3 — The Pipeline

Technically excellent and this is where my fear #1 was fully put to rest. Section 2.2 (`session-3/README.md:39-45`) and the Engineering Lead note (`session-3/README.md:45`) make the honest case: "a manual prod gate is a legitimate control — for compliance, or for early confidence... Keep it honest: it approves timing, the pipeline already proved readiness." That is a reframing of my gate I can actually defend to leadership. It doesn't delete me; it tells me *what I'm actually deciding* (timing) and takes the readiness drudgery off my plate. I respect that.

The target pipeline (`session-3/examples/.gitlab-ci.yml`) is clear: dev/qa auto-promote, only prod keeps `when: manual` (`.gitlab-ci.yml:155-161`). The smoke-test-after-deploy point (`.gitlab-ci.yml:139`) is the kind of automated verification that could *replace* parts of my manual QA pass — and I'd want it to. Good.

But note what the pipeline produces at the end of a successful prod deploy: nothing human-readable. It deploys the artifact, runs a smoke test, and stops. There is no job, no step, no artifact that emits "here is what changed in this release." The pipeline is treated as finished the moment prod is healthy. From my seat, that's the moment the *next* job starts — telling clients and support. The course's mental model ends one step before mine begins.

The recovery section (`session-3/README.md:97-114`) is genuinely good and I'll come back to it: fail-forward-by-default with rollback as the costly-and-time-sensitive exception is sound. The data trap (`session-3/README.md:110`) is the kind of nuance that tells me the authors have actually done this.

### Resources (minimums, glossary, checklist, troubleshooting)

`minimums-reference.md` is a clean, citable reference — I'd keep it open in reviews as suggested. The litmus test (`minimums-reference.md:90-93`) is sharp.

The **glossary** (`glossary.md`) defines *deploy*, *release*, *dark launch*, *feature flag* well — but the definition of **Release** (`glossary.md:79-80`) says "Exposing a feature to users. A business decision." It stops there. It never connects "exposing a feature to users" to "...which means someone has to *tell* the users." The vocabulary the course gives me has no word for "the artifact/communication that informs users and support what changed." There is no glossary entry for *release notes*, *changelog*, or *release communication*. The omission is consistent across every file.

The **migration checklist** (`migration-checklist.md`) is the place this gap hurts most operationally. Phase 4 — Deliver on Demand (`migration-checklist.md:77-86`) — lists "Releases are routine and low-drama, not events" and "Deploy and release are decoupled — flags control feature exposure." Nowhere in any phase is there a checklist item like "establish how release information reaches customers and support when releases stop being weekly." A team following this checklist literally to the letter would dismantle the weekly release (and with it, the natural cadence that produces my Friday email) and have *no migration step* that catches the dropped communication. That is a real adoption hazard, not a theoretical one.

The **troubleshooting** guide (`troubleshooting.md`) catalogs objections well — risk, time, "we have GitLab so we do CI," compliance, the stateful-change problem. But the objection *I* walked in with — "if we deploy continuously, how do customers and support learn what changed, and what happens to the weekly changelog?" — is not in the list. For a guide whose whole purpose is "the objections you will hear most" (`troubleshooting.md:3`), missing the release manager's objection is telling.

### Exercises — my attempt

**Current-state assessment** (`exercises/current-state-assessment.md`):

I filled it for my world — I sit across several teams as the release gate, so I scored the estate as I see it at sign-off.

Part 1, CI minimums: mostly **No/Partial**. Branches live a week-plus (we batch to the Friday release), so "integrate daily" is No, "trunk-based" is No. Tests-before-merge is Partial (varies by team). Part 1, CD minimums: #2 (pipeline-only deploy) Partial, #3 (pipeline decides releasability) **No — *I* decide releasability**, which is the whole point of my job and the whole point of this course's tension. #5 immutable artifacts Partial. #8 rollback rehearsed: No.

Where the exercise was hard / where my seat isn't represented:

- The scorecard has **no row for "how do downstream parties learn a release happened?"** It measures whether the artifact is deployable, never whether the release is *communicated*. From a release-management seat, "deployable but nobody downstream was told" is an incident waiting to happen, and this assessment would score that team green.
- Part 2's baseline metrics (`current-state-assessment.md:46-54`) are all DORA throughput/stability numbers. There is no metric for release-communication coverage, time-to-notify, or support-ticket spikes after unannounced changes. The things I'm measured on don't appear.
- Part 3 "name the constraint" (`current-state-assessment.md:58-66`) assumes the constraint is an *engineering* practice (branch lifetime, manual deploys, slow tests). For my function the binding constraint is "the weekly release is also the weekly communication event, and CD breaks the second thing while only addressing the first." The exercise gives me nowhere to write that.

So I can complete the exercise, but it quietly defines "current state" as engineering-pipeline state and treats my gatekeeping + comms role as out of scope rather than as something to migrate.

**Decompose a branch** (`exercises/decompose-a-branch.md`):

I did the Violations API decomposition and the nine-slice "good" answer (`decompose-a-branch.md:69-81`) is genuinely well-constructed. As a QA lead I actually *prefer* this: each slice is independently verifiable, and slices 1–7 ship dark so I can QA them in prod-like conditions before any customer sees anything. That's better testability than a two-week branch I get handed the day before release. Credit where due.

But run it forward to *my* deliverables and the gap is stark. Under the old model, this one feature = one line in one Friday email: "Board members can now record violations and view history." Under the decomposition, the user-visible moment is **slice 8 and slice 9 — the flag flips** (`decompose-a-branch.md:78-79`), which are explicitly "Release decision" steps that involve **no deploy and no pipeline run**. So:

- How do I QA-verify slices 1–7? Fine — smoke tests + the flag-off `501` path are testable, and `handler.test.ts:47-52` shows the dark path is already covered. I can sign those off continuously.
- How do I *communicate* the resulting changes? The exercise has no answer. The feature became real for customers at the flag flip, which is invisible to anything watching commits or deploys. If my changelog is generated from merged MRs or prod deploys, it would announce "violations recording" back at slice 3 (when it shipped dark, weeks early and doing nothing) and would have *nothing* to announce at slice 8 when it actually went live. The decomposition that's so good for engineering actively *decouples the release event from every artifact a changelog could be built from.* The course celebrates this decoupling and never notices it orphaned the communication.

## Where it lost me / objections it didn't answer

1. **The weekly email problem — unaddressed (headline).** See the dedicated section below. This is my single biggest issue and the one most likely to make me resist rollout until it's solved.

2. **"The pipeline decides releasability" — *mostly* answered, with a residual.** Sessions 1 and 3 did reframe this well: the pipeline owns *readiness*, the human owns *timing*. I buy that. The residual: the course frames the human-who-approves-timing as an "Engineering Lead" (`session-3/README.md:45`) or just "the team" (`cd-vs-continuous-deployment.md:43`). It never says "this is the Release Manager's decision" or maps the timing gate to an existing QA/release role. So I'm reassured my *judgment* survives, but the course quietly reassigns *who* exercises it to engineering, without acknowledging the handoff. If you want release managers to champion this, name us in the timing gate.

3. **My QA pass vs. automated definition-of-deployable — not reconciled.** Session 3 says "deployable is a checklist the pipeline runs, not a judgment a person makes" (`session-3/README.md:50`). I largely agree, and I *want* the routine checks automated. But there's exploratory and acceptance testing that isn't unit-testable. The course never says where human QA fits in a CD pipeline — is it a pre-merge activity? A canary observation? Gone entirely? It addresses the *automatable* half of my job and is silent on the *judgment* half, which leaves me guessing whether CD respects exploratory QA or assumes it away.

## Confusing or assumed (clarity)

- The course uses "release" in two senses without ever flagging it: the engineering sense (the artifact is released/deployable) and the business sense (the feature is exposed to users). The glossary (`glossary.md:76-80`) defines both *deploy* and *release* cleanly, but the body text slides between them — e.g., "release = flip the flag" (`session-2/README.md:79`) vs. the manual prod gate being called "release to prod" (`session-3/README.md:43`). For someone whose job sits on the word "release," this ambiguity matters. Which "release" triggers customer communication? The course never says because it never considers customer communication at all.
- "Low-drama release" / "release is a non-event" (`session-1/README.md:82`, `migration-checklist.md:80`) is used as an unalloyed good. From my seat a release being a "non-event" is *also* the risk: if it's a non-event internally, it's easy for it to be a non-event to the customer too — i.e., nobody told them. The course never distinguishes "low-drama for engineers" from "still a visible, communicated event for users."

## Factual / technical concerns

I'm QA, not infra, so I'll keep this short and defer the deep AWS fact-check to someone closer to the metal. Nothing jumped out as wrong:

- The fail-forward-vs-rollback decision table (`rollback-on-aws.md:101-110`) and the data trap (`rollback-on-aws.md:39`) match my operational experience — rolling code back doesn't roll data back, and that's bitten us. Good.
- The canary auto-rollback claim (`rollback-on-aws.md:65-74`, `template.yaml:88-92`) reads correctly: `Canary10Percent5Minutes` + a CloudWatch error alarm, CodeDeploy reverts on alarm. Plausible and well-explained.
- One verification-flavored note from my discipline, not a factual error: the course leans on "the smoke test proves the deploy works" (`.gitlab-ci.yml:139`). A smoke test proves the service is *up*, not that the *behavior is correct* for the change just shipped. The course occasionally lets "smoke test passed" stand in for "verified" (`.gitlab-ci.yml:171`, "Verified, not hoped"). As a QA lead I'd want that claim narrowed — smoke ≠ acceptance.

## Communicating releases to users (the weekly email problem)

This is the section the course should have and doesn't, so I'll write it from my seat.

**The current reality the course is asking us to dismantle:** every Friday I send a curated release-notes email to clients and to internal support summarizing everything that shipped that week. It is not decoration. It is how support knows what changed before the tickets come in, how account managers brief boards, and how clients trust that the product is maintained. The *weekly release* and the *weekly communication* are the same event today. CD splits the release into many small continuous deploys — and the course never asks what happens to the communication that was riding on the old cadence.

**Why this is a real hole, not my résumé talking:**

1. **The course explicitly creates the problem and never names it.** Decoupling deploy from release (`session-2/README.md:68-71`) and flag-flip releases (`session-2/README.md:79`, `decompose-a-branch.md:78-79`) mean the customer-visible event is a config toggle with no deploy and no pipeline run behind it. Any changelog built from commits or deploys will be wrong in both directions: it announces dark code that does nothing (slice 3) and stays silent at the flag flip that's the actual release (slice 8). The course celebrates this decoupling without noticing it severs release communication from every artifact you'd auto-generate notes from.

2. **It's absent everywhere I looked.** Searching the whole course, the only mention of communicating a release to anyone is one clause — "maybe alongside a comms email" (`cd-vs-continuous-deployment.md:43`) — offered as an example of *timing*, not as a practice to design. "Release notes," "changelog," "customer notification," and "support handoff" appear nowhere in the sessions, resources, exercises, or examples. "Customer" appears only as a *rollback trigger* ("active customer/board impact," `session-3/README.md:102`), i.e., customers exist in this course only when something breaks — never as people you proactively keep informed.

3. **The migration checklist would walk a team straight into it.** Phase 4 (`migration-checklist.md:77-86`) declares victory at "releases are routine and low-drama" with zero items about re-establishing release communication once the weekly cadence is gone. A team that executes this checklist faithfully decommissions the weekly release and never gets prompted that they just orphaned the weekly email.

**What I'd want the course to add (and what I'd answer myself in the room):**

- A short subsection in Session 2 or 3 — "Communicating continuous releases" — making the explicit point: when release = flag flip, the *communication* must hang off the **release decision** (the flag flip / the prod-timing gate), **not** off the deploy. That's the one design insight the course already has all the pieces for and just never assembles.
- Concrete patterns it could teach with its existing toolkit: (a) derive a customer-facing changelog from a curated source (release-noted MRs, a `CHANGELOG` fragment per user-facing slice, or flag-flip events tagged with release notes) rather than from raw deploys; (b) batch the *communication* weekly even when *deploys* are continuous — decoupling cadence-of-comms from cadence-of-deploy is the same deploy/release insight applied to notification; (c) make "release note written" part of the **definition of deployable** for any user-facing change, so it's a pipeline gate, not a Friday scramble. That last one actually *strengthens* the course's own thesis: it puts communication inside the automated definition of deployable instead of leaving it as out-of-band human heroics.
- One glossary entry (*release note / changelog*) and one troubleshooting objection ("If we ship continuously, how do customers and support learn what changed?") so the next release manager who takes this course sees their job represented.

Until that exists, my honest position is: the course wins the *engineering* argument and loses the *operational handoff*. I can be sold on continuous deploys; I cannot sign off on dismantling the release cadence until someone tells me what carries the customer communication afterward — and right now the course doesn't.

## Recommendations

### High priority

1. **Add release communication as a first-class topic.** A "Communicating continuous releases" subsection (Session 2 or 3) anchored on the insight the course already owns: communication hangs off the *release decision / flag flip*, not the deploy. Without this the course has an operational hole for any org that does customer-facing release notes today.
2. **Add a migration-checklist item** under Phase 3 or 4: "Establish how release information reaches customers and internal support once releases stop being weekly — make a user-facing release note part of the definition of deployable." This is the line that stops a team from silently dropping the weekly email.
3. **Name QA and Release Management in the audience and in the timing gate.** Add us to `README.md:29-34`, and in `session-3/README.md:45` say explicitly that the timing-gate decision can be a release manager's, not only an "Engineering Lead's." Costs nothing; converts a skeptic.

### Medium priority

4. **Say where human/exploratory QA lives in a CD pipeline.** Reconcile "deployable is not a human judgment" (`session-3/README.md:50`) with the reality that acceptance/exploratory testing isn't unit-automatable. One paragraph would do it.
5. **Add the release-manager objection to troubleshooting** (`troubleshooting.md`): "If we deploy continuously, how do customers and support learn what changed?" with the deploy≠release-cadence answer.
6. **Add a communication/notification row to the current-state assessment** so a team scoring itself notices whether release information actually reaches downstream parties.

### Nice to have

7. **Tighten "verified" language** so smoke-test ≠ acceptance (`.gitlab-ci.yml:171`, `session-3/README.md`).
8. **Glossary entries** for *release note* and *changelog*, cross-linked to *deploy* vs *release*.
9. **Distinguish "low-drama for engineers" from "still a communicated event for users"** wherever "release is a non-event" appears.

## Verdict

**Comply, leaning champion — conditional on one fix.** The course won the argument I was most defensive about: it does not delete my judgment, it sharpens it (I own *timing* and the human half of "deployable"; the pipeline takes the rote readiness checks I shouldn't be doing by hand anyway). I'd champion it tomorrow for the engineering practices. But it is silent on the operational half of my job — release communication — and worse, it actively dismantles the cadence that communication rides on without ever noticing. I will not put my name on retiring the weekly release until the course (or our rollout plan) answers what carries the customer-and-support communication afterward. Fix the release-comms gap and this is an 8–9 and I'm a vocal advocate. As written, it's a strong engineering course with a release-manager-shaped hole in it.
