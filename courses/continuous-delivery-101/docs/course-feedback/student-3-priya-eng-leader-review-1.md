# Continuous Delivery 101 — Review 1

**Student:** Priya (Engineering Director, 15 yrs)
**Stance going in:** "We can't just let things deploy whenever." I sign the change-control policy and I'm the one who sits across the table from a SOC 2 auditor and an angry client when prod breaks. CD smells like "remove the brakes."
**Review date:** 2026-06-19
**Overall rating:** 7/10 — would I adopt/champion this?

## Executive summary

The course made the single argument I most needed to hear, and made it well: Continuous *Delivery* is not Continuous *Deployment*, and adopting it does not mean code ships to prod without a human deciding. That distinction is drawn cleanly and repeated everywhere it matters (`sessions/session-1/README.md` §3, `sessions/session-1/examples/cd-vs-continuous-deployment.md`, `resources/glossary.md`, `resources/troubleshooting.md`). On the engineering merits, it also quietly *raises* my control posture rather than lowering it — single pipeline path, OIDC instead of human-held keys, immutable artifacts, full audit trail per change. Those are things I'd struggle to get an auditor to fault.

Where it loses me is governance literacy. The course treats compliance as a one-paragraph objection to defuse, not as a first-class design constraint. It never names segregation of duties, never shows what an audit trail actually looks like or where the evidence lives, never addresses change-advisory/change windows beyond "a human picks the timing," and — most alarming to me — the "manual gate is a transitional compromise" framing reads, to a director, as "your primary control is technical debt we're planning to delete." It tells me to keep the gate *honest*; it never tells me how to keep it *defensible*. I can champion this for greenfield AWS services tomorrow. I cannot yet take it to my compliance lead and the client without writing the governance chapter myself.

## Section-by-section

### Course framing (README)

The README is honest about scope ("not a tool rollout," practices over products) and the target audience explicitly includes "Engineering Leaders who must approve the change windows and process shifts CD requires" (`README.md` §Target Audience). Good — it claims my seat at the table. But then it never actually *serves* that seat. The "What You'll Learn" list (`README.md:16-26`) is entirely IC/tech-lead competencies. There is no leadership outcome like "explain to your auditor why a green pipeline is a stronger control than a manual gate" or "rewrite your change-management policy to reference pipeline evidence." If you invite the director, give the director a deliverable.

The "Warning Signs" list (`README.md:168-176`) flags "A human meeting — not the pipeline — decides whether a build is releasable" as a *bad* sign. I understand the engineering point. But stated that baldly to a leadership audience, it reads as "your CAB is an anti-pattern." My CAB exists because an auditor and a master service agreement say it must. The course needs to distinguish *a meeting that re-litigates technical readiness* (genuinely wasteful) from *a governance approval that authorizes a business-risk decision* (often contractually required). It conflates them.

### Session 1 — Why CD & the Minimums

The batch-size argument (`session-1/README.md` §2) is the most persuasive risk argument in the course, and it's a *risk* argument, which is my language. "A weekly release isn't one change — it's a week of changes released together... which one broke prod?" — that is exactly the incident-review pain I live. Reframing frequency as a *risk reducer* rather than a risk multiplier is counterintuitive and correct, and the DORA citation (throughput and stability are not a trade-off) gives me something to put in front of a board.

§3 (CD vs Continuous Deployment) is the heart of why I didn't walk out. The two-line diagram (`session-1/README.md:104-107`) showing the only difference is the last hop, plus the Engineering Lead note "Continuous Delivery does not mean losing control... Releasing stays a deliberate decision" — that's the reassurance I came for. It is repeated, not buried. Credit where due.

The pipeline walkthrough (`session-1/examples/current-state-pipeline-walkthrough.md`) is the strongest single artifact for *me*. It scores our real baseline honestly and — crucially — it explicitly says the all-manual-gate state is "a valid Phase-2 state — fine for early confidence **or a compliance control**" (line 46). That parenthetical is the first acknowledgment that a gate can exist for governance reasons, not just timidity. I wish that thought were developed into a section instead of a clause.

What's missing in Session 1: the word "audit" appears in the context of "audit trail" as a *benefit* (`session-3` §2.1) but Session 1 never frames CD's control improvements (provenance, no human keys, immutability) as *answers to specific compliance questions*. Those are my strongest selling points internally and the course leaves them on the floor.

### Session 2 — Trunk-Based Development & CI

This is the most IC-facing session and the one where I'm least the audience, but I read it for the control implications. Two things stood out for governance.

First, feature flags (`session-2/README.md` §3, `session-2/examples/feature-flag.ts`). The course treats flags purely as a delivery convenience. From where I sit, a feature flag is *also* a release authorization mechanism and therefore a control surface. Who is allowed to flip `FLAG_VIOLATIONS_RECORD=true` in prod? Is that change logged, attributed, and reviewable? The env-var pattern (`feature-flag.ts:32-48`) flips a flag via a **config change + redeploy**, which at least inherits the pipeline's audit trail — good, and the course should *say* so explicitly. But the file's "graduating to a managed service" note (`feature-flag.ts:104-114`) lists "an audit trail of who changed which flag when" as a *nice-to-have you might want someday*. For a regulated release decision, that audit trail is not optional — it's the whole control. The course has the right instinct (the env-var pattern is auditable by construction) but never connects it to compliance, and it understates the audit requirement when flags move to runtime toggles. A LaunchDarkly toggle that releases a feature to prod with no logged approval is a *weaker* control than my current CAB, and nobody flags that.

Second, the anti-patterns file (`session-2/examples/branching-antipatterns.md`) condemns "code freeze / stabilization periods." Reasonable engineering-wise. But many regulated orgs run mandated freeze windows (year-end, peak-season change moratoria for clients). The course should acknowledge that a *business* freeze window is different from an *engineering* stabilization freeze, and that CD actually makes mandated freezes *cheaper* (you stop releasing but keep integrating). It doesn't make that distinction, so the section reads as dismissive of a control I don't get to remove.

### Session 3 — The Pipeline

This is where the technical control story is strongest and the governance story is weakest — a frustrating combination.

Strong: §2.1 explicitly names my benefits — "every change to a shared env has the same provenance — a commit, a pipeline run, an audit trail" and "deploy permissions live with the pipeline (via OIDC), not in engineers' hands" (`session-3/README.md:33-35`). That paragraph, expanded, is my entire case to an auditor: provenance, least-privilege, no standing human access to prod. It's stated once, in passing, as an engineering benefit. It deserves to be the centerpiece of a leadership track.

The target pipeline (`session-3/examples/.gitlab-ci.yml`) makes a consequential governance decision and is honest about it: dev and qa auto-promote on green, **only prod keeps a manual gate**, and that gate "approves *timing*, not *readiness*" (lines 12-16, 160). The Engineering Lead note at §2.2 (`session-3/README.md:45`) says a manual prod gate "is a legitimate control — for compliance." Good. But here is my core problem, stated plainly:

**The course's settled position is that the manual gate is a transitional compromise to be removed once trust grows.** Every reference frames it that way — `glossary.md` ("A transitional compromise on the road to CD... not the goal"), `troubleshooting.md` ("A manual gate everywhere, forever, means you've stopped at Phase 2"), migration Phase 3 ("Replace manual approval gates with automated verification... where compliance allows"). For an engineer, "the gate is scaffolding we'll delete" is liberating. For me, it's alarming, because **my mandated approval is not scaffolding — it may be a permanent contractual and audit requirement.** The course's "where compliance allows" hedge (`migration-checklist.md:70`) is doing enormous load-bearing work in a single clause. I need the inverse framing offered as a legitimate, non-embarrassing end state: *"For some services, a deliberate human authorization on the prod deploy is a permanent control. CD still delivers full value — small batches, fast lead time, rehearsed recovery — with that gate in place. You are not 'stuck at Phase 2'; you have made a risk decision."* Without that, the course implicitly tells my teams that my control is a maturity failure. That will create friction between my engineers and my compliance obligations, and I'll be the one adjudicating it.

Recovery (§5.2 and `session-3/examples/rollback-on-aws.md`): the fail-forward-by-default framing is operationally sound and I appreciate the data-trap honesty (rolling code back doesn't roll data back). But "fail forward" has a governance edge the course never touches: **does a fail-forward fix to prod go through the same approval as a planned release, or is it an emergency change?** Every change-management framework I've operated under has a separate (lighter, but *documented and retroactively reviewed*) path for emergency/expedited changes. The course treats a forward fix as "just another deploy through the pipeline" — which is great for speed and silent on the control question of *who authorized an unplanned prod change at 2am and how it gets reviewed afterward.* That's a real gap.

### Resources (minimums, glossary, checklist, troubleshooting)

The minimums reference (`resources/minimums-reference.md`) is clean and I'd keep it open. The litmus test (line 91) — "Could a single small change... reach production today... with no meeting to decide if it's 'ready'?" — is a sharp engineering test, but as written it again equates *any* meeting with failure. I'd refine it to "...no meeting to **re-decide technical readiness**" — a governance authorization of timing/business-risk is a different thing and should pass the test.

The troubleshooting file (`resources/troubleshooting.md`) has the one objection written directly at me: "Compliance / change management requires a manual approval" (lines 27-29). The answer is *correct as far as it goes* — keep the gate, keep it honest, the human approves timing not readiness. But it's three sentences for the single biggest adoption blocker in any regulated org, and it ends on the discouraging note that a permanent gate means "you've stopped at Phase 2." The "when to ask for help" section (lines 102-109) says "Compliance requirements seem to forbid automation entirely (they usually don't — they constrain it)." That's the most useful sentence in the whole governance story — *constrain, not forbid* — and it's buried at the very bottom as an escalation footnote. It should be a headline.

The glossary (`resources/glossary.md`) is solid but has no entries for the vocabulary I think in: **segregation of duties, change advisory / approval authority, audit trail / evidence, emergency change.** A glossary aimed partly at engineering leaders that omits every governance term tells me who the course really pictures in the room.

The migration checklist (`resources/migration-checklist.md`) has good Engineering Lead notes (Phase 1 "Foundations look like 'slowing down to invest'... budget for them explicitly, give one pilot team a change window and the authority to deploy"). That's real leadership advice. But no phase has a "controls & evidence" line item — e.g., "document how pipeline runs satisfy your change-management evidence requirements," "define who authorizes prod releases and how it's logged," "map each former manual control to its automated replacement and get sign-off." Adopting CD *changes your controls*; the checklist never tells me to update my control documentation. An auditor will notice that gap before I do.

### Exercises — my attempt

#### Current-state assessment (`exercises/current-state-assessment.md`)

I filled this out from a portfolio/leadership view across the estate (the .NET monoliths on Azure VMs plus the newer AWS services), rather than one team:

*CI minimums:* TBD — **No** for the monoliths (long-lived branches, weekly cadence), **Partial** for new AWS services. Daily integration — **No** / **Partial** likewise. Tests before merge — **Partial**. Stop-the-line — **No** (we have a weekly release ritual, not a stop-the-line culture).

*CD minimums:* Single pipeline path — **Partial** overall (strong on new AWS per the baseline, weaker on VM deploys). Pipeline decides releasability — **No** (a human clicks every gate; honestly, a *meeting* — my release readiness review — decides). Immutable artifacts — **Partial**. Prod-like env — **Partial**. Rollback rehearsed — **No** (we have procedures, not rehearsed capability). Config with artifact — **Partial**.

Where it was hard: **the scorecard has no risk or governance dimension at all.** There is no row for "deploy access is least-privilege and logged," "releases are attributable and auditable," "segregation of duties between author and approver," "change-management evidence is captured automatically." From my seat those are the *most* important things to score, because they're what I'm accountable for and what an auditor will test. The assessment scores engineering maturity; it does not score *control maturity*. I'd add a third table — "Governance & control" — with rows like: every prod change is attributable to a commit + approver; no human holds standing prod deploy credentials; release approval is logged; rollback is rehearsed *and documented in the runbook on-call uses*; emergency-change path exists and is retroactively reviewed.

Part 3 ("name the constraint") assumes the binding constraint is technical (branch lifetime, manual deploys, slow tests). For a director, the binding constraint is often *organizational*: "compliance hasn't signed off that pipeline evidence replaces the CAB minute." The exercise has no place to record that, so the most important blocker in my world is invisible to the tool.

Part 4 (pick a pilot) is good and I agree with it — a new, low-blast-radius AWS service is exactly where I'd let my teams prove this. I'd add one criterion: "pick a service whose **client contract and compliance scope** let you experiment with reduced gating." Blast radius isn't only technical.

#### Decompose a branch (`exercises/decompose-a-branch.md`)

I worked the Violations API decomposition. As an engineering exercise it's genuinely good — the nine-slice "what good looks like" (lines 69-81) is clear, and slice 8/9 (turn the flags on as explicit *release decisions*, separate from the deploys) is exactly the deploy/release decoupling I'd want my teams to internalize. I would happily ask every team to do this; it directly attacks our biggest real problem (two-week branches).

Governance implications I'd flag for my teams:

- **Slices 8 and 9 are the actual releases**, and they're flag flips. That means *the release authorization moves from the deploy to the flag flip.* My change-control process is built around authorizing *deploys.* If the meaningful release is now a config toggle, my controls have to follow it there. The exercise (and the course) never says this out loud, but it's the single biggest governance consequence of the whole approach: **decoupling deploy from release also decouples the deploy from the approval.** If I don't move the control to the flag, I have an unapproved release. The course should make this explicit — it's not hard, but it's invisible right now and it's exactly the kind of thing that fails an audit.
- The expand/contract guidance for data changes is sound and, helpfully, makes each step independently reversible — which is a *control* benefit (smaller, safer changes are easier to authorize), not just an engineering one. The course could sell that to leaders and doesn't.

Net: I'd adopt the decomposition exercise unchanged for my teams, *with a leadership addendum* explaining where the approval now lives.

## Where it lost me / objections it didn't answer

1. **Segregation of duties is never mentioned.** In a trunk-based, daily-integration, auto-promote-to-qa world, who is the independent approver? If the author merges to main and the pipeline auto-promotes through qa with no second human, where is the four-eyes principle that my auditor expects? MR review is the obvious answer (the author can't approve their own MR), and that's probably *sufficient* — but the course never makes that argument, never says "MR approval IS your segregation of duties and here's why it satisfies the control." It's the most common auditor question and it has no answer in the material.

2. **The "manual gate is transitional debt" framing is unrelieved.** Covered above. I need the legitimate-permanent-control framing offered as a peer to the remove-the-gate path, not as a grudging "where compliance allows" footnote.

3. **No artifact for talking to auditors or clients.** The course teaches engineers to *do* CD. It gives me nothing to *defend* CD: no "here's how a pipeline run maps to your change-management evidence," no "here's the before/after control narrative," no sample language for a client who reads 'continuous delivery' and panics that we've gone cowboy. I'm the one who has to write that, and the course assumes it's not its job. For a course that explicitly invites engineering leaders, that's the central omission.

4. **Emergency-change governance is absent.** Fail-forward is the default and a 2am forward fix is "just another deploy." Operationally fine; from a controls view, an unplanned prod change needs *some* lightweight authorization and *mandatory* retrospective review. The course is silent.

5. **Client trust / external commitments.** I answer to clients with contractual change-notification and sometimes approval rights. "Release any change any day" can directly conflict with "client must be notified 5 business days before a change to their portal." The course never acknowledges that the *business* may impose release constraints that have nothing to do with engineering readiness. (Marcus's release-notes problem and mine overlap here.)

## Confusing or assumed (clarity)

Little was *confusing* — the writing is clear and the IC concepts are well taught. What's *assumed* is that the reader's only gatekeeper is internal timidity. The course repeatedly models the manual gate as a confidence problem ("early confidence in a new service," "as trust grows") and only secondarily as a compliance reality. A director reads that and hears: "the course thinks my controls are a phase I haven't grown out of." That's a framing problem, not a clarity problem, but it will cost adoption among exactly the leaders the README says it wants.

## Factual / technical concerns

I'm not the SRE on this panel, so I'll keep to what I can vouch for. The control-relevant technical claims hold up:

- **OIDC, no static credentials** (`session-3/examples/.gitlab-ci.yml:51-61`, walkthrough line 20) — this is a genuine control improvement and accurately described. Removing standing human-held prod credentials is exactly what I want to tell an auditor.
- **Immutable, SHA-tagged artifacts promoted (not rebuilt)** (`.gitlab-ci.yml:37-114`, `violations-api/README.md`) — correctly equates "build once" with "the bytes that passed qa are the bytes in prod," which is a real reproducibility/evidence property.
- **Canary with alarm-based auto-rollback** (`violations-api/template.yaml:87-92`, `rollback-on-aws.md`) — described consistently and plausibly; the data-trap caveat is unusually honest and correct.

One thing I'd have an auditor question: the doc says rollback via raw `aws lambda update-alias` is "an emergency action; reconcile the stack afterward" (`rollback-on-aws.md:63`). That out-of-band action *bypasses the single-pipeline-path control* the course spends Session 3 establishing. It's the right operational move, but it's an *exception to the control* and should be named as one — a break-glass procedure that itself needs logging and review. The course mentions "admin break-glass" once (walkthrough line 37) but never treats break-glass as the controlled, audited procedure it must be.

## Governance, risk & control

This is the section the course should have and doesn't. Here's what I, as the accountable director, need before I sponsor this — and where the course stands on each:

| Control concern | What I need | Course status |
| --------------- | ----------- | ------------- |
| **Provenance / audit trail** | Every prod change traceable to commit + approver + pipeline run | **Strong, but undersold.** The property exists (OIDC, immutable artifacts, single path); it's stated once as an engineering benefit. Needs to be the leadership headline. |
| **Segregation of duties** | An independent approver; author ≠ sole authorizer | **Absent.** MR review almost certainly satisfies it, but the course never makes the argument. |
| **Standing access to prod** | No human holds prod deploy credentials | **Strong.** OIDC removes them. Best auditor story in the course. Say it louder. |
| **Release authorization** | A logged, attributable decision to release | **Mixed.** Pipeline-gated deploys are auditable; *flag-flip releases* shift the authorization to a config toggle the course doesn't treat as a control. The biggest blind spot. |
| **Change windows / freeze** | Honor mandated business freezes and client notice periods | **Weak/dismissive.** Conflates engineering stabilization freezes with mandated business freezes. CD actually *helps* here (stop releasing, keep integrating) but the course never says so. |
| **Emergency change** | A lighter but documented + retro-reviewed path for unplanned prod fixes | **Absent.** Fail-forward is treated as ungoverned. |
| **Break-glass** | Out-of-band actions (alias shift, admin deploy) are logged and reviewed | **Mentioned, not governed.** Named once; never treated as a controlled procedure. |
| **The manual gate** | Recognized as a *legitimate permanent control* for some services, not just transitional debt | **The core framing problem.** Always framed as scaffolding to remove "where compliance allows." Needs a peer framing: a deliberate human authorization can be a permanent, non-embarrassing control. |
| **Auditor/client narrative** | Language and evidence mapping I can hand to compliance and clients | **Absent.** The single most valuable thing the course could give a director, and it isn't there. |

**The reassurance I needed and got:** CD ≠ Continuous Deployment, the human gate survives, and the underlying engineering controls (provenance, no human keys, immutability) are objectively stronger than what most of my teams do today. On that basis I am *not* a blocker.

**The reassurance I needed and didn't get:** that my mandated controls are first-class citizens of CD rather than maturity debt, that the course knows where release authorization moved when deploy and release decoupled, and that someone has thought about what I tell the auditor and the client. Until that's written, I have to write it.

## Recommendations

### High priority

1. **Add a "CD under governance" section (or short leadership track).** Cover, explicitly: segregation of duties (MR review as the control), audit trail / evidence (map a pipeline run to change-management evidence), no-standing-prod-access (OIDC as the headline auditor story), and the emergency-change + break-glass procedures. This is the missing chapter for the leaders the README invites.
2. **Reframe the manual gate.** Offer "a deliberate human authorization on prod is a legitimate *permanent* control for some services" as a peer to "remove the gate as trust grows." Stop implying a permanent gate means "stuck at Phase 2." Distinguish *re-litigating readiness* (waste) from *authorizing a business-risk release* (often required).
3. **Make the deploy/release decoupling's control consequence explicit.** State plainly — in Session 2, the flag example, and the decompose exercise — that when release moves to a flag flip, *the release authorization must move with it.* Who can flip a prod flag, and is it logged? An unapproved flag flip is an unapproved release.
4. **Give the director something to hand outward.** A one-page "talking to your auditor / your client about CD" artifact: the before/after control narrative and sample client-facing language. Highest-leverage addition for the leadership audience.

### Medium priority

5. **Add a "Governance & control" table to the current-state assessment**, scoring control maturity alongside engineering maturity (attributable releases, least-privilege deploy, logged approvals, rehearsed+documented rollback, emergency path).
6. **Address mandated freeze windows and client notice periods** honestly: separate engineering stabilization freezes (anti-pattern) from business/contractual freezes (a constraint to honor — and one CD makes cheaper).
7. **Add a "Controls & evidence" line item to each migration phase**, including "update your change-management documentation to reference pipeline evidence and get compliance sign-off."
8. **Promote "compliance constrains, it doesn't forbid"** from the troubleshooting footnote to a headline principle.

### Nice to have

9. Glossary entries for segregation of duties, change advisory/approval authority, audit trail/evidence, emergency change, break-glass.
10. Strengthen the feature-flag example's audit note: when flags graduate to a runtime service, an attributable change log is *mandatory* for any flag that gates a regulated release, not a "you might want this someday" bullet.
11. A short worked "talking to an auditor" dialogue, the way the course already does worked dialogues for engineers.

## Verdict

**Comply-toward-champion.** The CD-vs-Continuous-Deployment distinction is drawn well enough, and the engineering controls are genuinely strong enough, that I will not block this and will sponsor it for greenfield AWS services. But I cannot fully champion it to my compliance lead and my clients until the governance chapter exists — today the course treats my accountability as friction to be defused rather than a design input, and its "manual gate is transitional debt" framing actively works against the controls I'm contractually and legally required to keep. Write the governance story and the rating goes to 9; it's the only thing standing between "comply" and "champion."
