# Continuous Delivery 101 — Review 3 (course round-4 spot-check)

**Student:** Tex (AI Champion, RealManage)
**Stance going in:** AI now writes the bulk of our code. When the agent is the author, coding gets cheap and the bottleneck *moves* — to review, to integration, to the decomposition decision. The friction that used to cap batch size is gone, so batch size becomes a throttle you *impose*; review can decay to a rubber stamp; the durable human skill is deciding the seams. My touchstone is `resources/ai-assisted-delivery.md` (the three seams). My durability test: the framing must survive 12 months — no dated percentage claims. My one standing deferred item is R3-6 ("a separate agent reviews the tests" asserted, not operationalised).
**Review date:** 2026-06-22
**Overall rating:** 9.5/10 — would I adopt/champion this?

## Verdict

The two big changes since round 3 — a Value Stream Mapping rebuild of Session 1 and a course-wide deploy/release terminology pass — both land cleanly in my areas, and neither regressed the AI spine. The terminology sweep actually *sharpens* my touchstone resource. But the VSM rebuild leaves a genuine, named opportunity on the table for an AI-first reader, and my R3-6 item is untouched. Held at 9.5 — earned, not coasting.

## Score trajectory

| R1 | R2 | R3 | **R4** |
| -- | -- | -- | ------ |
| 8.5 | *(skipped)* | 9.5 | **9.5** |

**Delta vs R3 (0.0):** The new work is solid and the AI spine survived both changes intact — that *protects* the 9.5. It doesn't *raise* it, because the single highest-leverage AI move available right now (connect VSM's constraint-finding to the constraint-shift AI causes) was not taken, and my one deferred operational item (R3-6) is still deferred. No regression, no advance. A genuine 9.5 hold, not a default.

---

## The two new changes, scored through the AI lens

### 1. Session 1 rebuilt around Value Stream Mapping — **STRONG, with a real missed AI opportunity**

The exercise (`exercises/value-stream-map.md`) is well-built and self-contained. The method is right: three numbers per step (process / wait / %C/A), two derived (lead time, flow efficiency), work *backward* from prod so the deploy-side waits don't get forgotten, surface the constraint from the numbers rather than the loudest voice. The "when in doubt it's wait," "a queue is a step," and "resist naming the constraint before you've measured" rules-of-thumb are exactly the discipline that keeps this from being theatre.

**The worked example holds up — I checked the math, every figure is exact:**

| Claim | Doc says | Verified |
| ----- | -------- | -------- |
| Total process | 19.5 h | 19.5 h ✓ |
| Total wait | 108 h | 108 h ✓ |
| Total lead | 127.5 h | 127.5 h ✓ |
| Lead in days | ~16 working days | 15.94 ✓ |
| Flow efficiency | ~15% | 15.29% ✓ |
| Rolled %C/A | ~34% | 34.34% ✓ |
| "~2.5 days being worked" | ~2.5 d | 19.5/8 = 2.44 d ✓ |
| "~3 calendar weeks" | ~3 wks | 127.5/40 = 3.19 work-weeks ✓ |

No rot risk in any of those — they are arithmetic on a hypothetical team, not a dated industry claim. The %C/A reasoning is sound: low %C/A on the release-day deploy (70%) is correctly read as the big-batch failure rate made visible, and the "fix for both stars is the same — smaller batches" conclusion is the course's thesis sitting in the reader's own numbers. That's a good piece of teaching.

**But here is my finding, and it's the strongest one in this review.** A value stream map is *the* tool that exposes my entire thesis — and the exercise never makes the connection. My whole argument is: **when an agent writes the code, the constraint moves.** "Develop" stops being the binding step. An agent emits a feature before lunch, so process time at the develop step collapses toward zero — and the wait that was always there (the review queue, the release window) now dominates even harder. The review step becomes the binding constraint precisely *because* coding got cheap. A VSM is the perfect instrument to *show* a team that, in figures they can defend: re-run the map with AI authorship and watch the develop bar shrink while the review-queue bar stays put and becomes the star. That is the single most vivid demonstration of "the bottleneck shifted" that exists in this whole course — and the exercise, Session 1 §5, and the example all stay silent on it.

This is not a demand to wedge AI into a deliberately platform-agnostic exercise. The exercise's "platform-agnostic on purpose" framing (`value-stream-map.md:9`) is correct and I would not touch it. But this is an *authorship* point, not a platform point — and the course has decided, everywhere else, that authorship belongs on the spine (six inline touches, per round 2). The VSM is the one new spine location where the AI case is conspicuously absent. The fix is small and consistent with how every other touch is done: one callout in the exercise (or in Session 1 §5.2) — "If an agent writes most of your code, re-map and watch where the constraint *moves*: the develop step shrinks, so the review queue or the release window becomes the binding wait. Coding got cheap; the bottleneck went somewhere else. See [CD when AI writes the code](../resources/ai-assisted-delivery.md)." That single sentence converts the strongest authorship-agnostic exercise in the course into the strongest *demonstration* of my thesis. Right now it stands fine on its own — but it leaves the course's best illustration of "the constraint moved" unspoken.

Phase 0 of the migration checklist (`migration-checklist.md:15`) and the assessment cross-reference (`current-state-assessment.md:70`, Part 3) both wire the VSM in correctly — the "Part 3 of the assessment" anchor resolves, the estimate→measured handoff is clean. No broken links, no orphan. Structurally this is a good integration; it's only the AI angle that's the gap.

**Net:** strong exercise, durable, correct — but a named, cheap, on-brand AI opportunity left on the table. That's why it holds the score rather than lifting it.

### 2. Deploy/release terminology pass — **CLEAN in my areas; it sharpens the AI resource**

I grepped my whole surface. The sweep is disciplined:

- `releasability` survives in exactly the right places — MinimumCD #3 (the pipeline's verdict) and the README "automate the verdict" thesis. It is *not* leaking back in as a trunk-state word. The deviation note in `minimums-reference.md:7-14` is honest and precise: "this course says 'deployable' where MinimumCD says 'releasable' … the bar is identical; only the word is sharper," and it explicitly carves out the one retained noun. That's the right call and it's defensible to an auditor or a pedant.
- `ai-assisted-delivery.md` came through consistent. It uses "always-deployable trunk" and "definition of deployable" for the technical state, and keeps "the pipeline decides releasability" (`:13`) only for the verdict — matching the rest of the course post-sweep. No mixed usage, no half-renamed sentence.
- The precision *matters* for my sections, and the resource gets it right: the test-gaming seam is about the *definition of deployable* (a gate the agent can game), seam 2's accountability chain ends at "deploy," and the batch-size-as-throttle framing is about keeping the trunk deployable, not about "releasing." Under AI, the deploy/release split is *more* load-bearing, not less — an agent's green commit auto-flowing to prod is the deploy act; whether the *feature* is on is still the human's release decision. The resource keeps those distinct, which is what makes the "let the agent's green commits auto-ship" temptation answerable. The sweep didn't blunt that; it kept it crisp.

**Durability re-check — still PASSES.** Grepped the AI resource, the VSM example, and Session 1 §5 for dated/volume claims ("nearly all the code," percentages, "by 202X," "in a year"). Zero hits. The framing stays conditional and timeless ("As more code is written by agents," "when the same agent writes," "once an agent is writing your `package.json`"). The factory metaphor is intact and durable. The terminology sweep introduced nothing that will date.

**No regressions found in my areas from the sweep.** This is the cleaner of the two changes.

---

## Standing open item — R3-6 (separate agent reviews the tests)

**STILL OPEN — untouched, neither addressed nor reopened.** `ai-assisted-delivery.md:26` still reads: "have a human — or a *separate* agent that did not write the code — check the tests against those criteria." That's the right *shape* and it's still the one place in the resource that states a practice without a concrete *how*, in deliberate contrast to the fully-mechanical flag-debt section three bullets later. The VSM and terminology work didn't touch it, and nothing in the new content reopens or worsens it. It remains exactly the deferred item from round 3: a two-line "what this looks like in the MR" pattern would lift it from principle to mechanism. For an AI-first shop this *is* the daily question — how do you wire a second agent as an independent test reviewer without it inheriting the first's blind spots (different context, criteria-first, no sight of the implementation diff). I still accept it as polish toward best-in-class, not a blocker — but it is the longest-standing thing on my list and I'd close it next.

---

## Newly-introduced problems (adversarial hunt)

I went looking. Two things, both minor; one is real, one is a deliberate-design call I'll only flag.

1. **VSM example: the lead-time figure ignores rework, but the exercise tells students to add it (minor, real).** Rule of thumb at `value-stream-map.md:56` says "Rework shows up twice — a low %C/A at a step means work loops back; count the *extra* trip's time too." The worked example then computes a 34% rolled %C/A (i.e. two changes in three loop back *somewhere*) but the 127.5 h lead-time figure is a single clean pass with **no rework trips added**. A sharp student following the rule will ask why the example's own low %C/A didn't inflate its lead time. It's defensible — the example is illustrating the *measurement method* on a happy-path change, and the rolled %C/A is presented as a separate sobering aside, not folded into the headline — but the example doesn't *say* that, so the rule and the worked numbers sit in quiet tension. One clause ("this happy-path pass assumes no bounce-back; a realistic stream adds the rework trips the %C/A predicts") would resolve it. Not a blocker.

2. **Self-containment holds — no cross-course links introduced (verified, not a problem).** I grepped the new and changed files for `ai-101` / `bdd-101` / parent-course paths. None. The round-2 decision to keep all AI content inside CD-101 survived both changes. Good — the resource still carries its own weight.

No other regressions. The terminology sweep did not introduce inconsistency in any of my surfaces, and the VSM rebuild didn't disturb the six existing AI spine touches (Session 1 §2.2 batch-as-throttle blockquote is intact at `session-1/README.md:88`).

---

## Prioritized open items

Tiered by effort. None is an adoption blocker — the course is champion-ready as it stands. This is the gap between 9.5 and the reference text.

### Tier 1 — one-line / few-line, clearly worth doing

| ID | Item | Effort | Where |
| -- | ---- | ------ | ----- |
| **R4-1** | **Connect the VSM to the constraint-*shift* AI causes.** The strongest finding. One callout: when an agent writes the code, re-map and watch the develop step shrink while the review queue / release window becomes the binding constraint — coding got cheap, the bottleneck moved. Converts the course's best authorship-agnostic exercise into its best *demonstration* of "the bottleneck shifted." On-brand with the existing six spine touches. | 1 sentence | `exercises/value-stream-map.md` (Part 4) or `sessions/session-1/README.md` §5.2 |
| **R4-2** | **VSM example: reconcile the rework rule with the happy-path lead time.** One clause noting the example assumes no bounce-back, so a realistic stream adds the rework trips the 34% %C/A predicts. Closes the quiet tension a sharp reader will spot. | 1 clause | `value-stream-map.md` (~`:113-121`) |

### Tier 2 — small addition (more than a line)

| ID | Item | Effort | Where |
| -- | ---- | ------ | ----- |
| **R3-6** | **(Carried, still open.) Operationalise "a separate agent reviews the tests."** A two-line "what this looks like in the MR" pattern — second agent, criteria-first, no sight of the implementation diff, different context — turning the one remaining principle into a mechanism the way the flag-debt section already models. My longest-standing item. | 2 lines | `ai-assisted-delivery.md:26` |

### Blockers

**None.** No regression from either change; the AI spine and the durability guarantee both survived. Tier 1 is refinement, not remediation.

---

## Bottom line for an AI champion

Still an unconditional champion. The terminology pass made my touchstone resource *sharper* — the deploy/release split is more load-bearing under AI, and the sweep kept it crisp instead of blunting it. The VSM rebuild is a genuinely good, durable, mathematically-exact exercise. My only real disappointment is that the VSM is the perfect instrument for my single biggest point — *the constraint moves when coding gets cheap* — and the course built it without ever turning it that way. That's one sentence away from being fixed, and it would be the most vivid "the bottleneck shifted" demonstration in the whole course. Do R4-1, close the long-standing R3-6, and this is the reference text for CD in an AI-first org. As it stands today, it's still the course I'd hand every engineer at RealManage on day one.
