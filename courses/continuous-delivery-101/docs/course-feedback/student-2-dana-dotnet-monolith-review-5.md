# Continuous Delivery 101 — Review 5 (round-5 spot-check)

**Student:** Dana (.NET Framework Monolith Maintainer, 12 yrs)
**Stance going in:** "Round 4 held at 9.5 and named two of my own soft items — batch-size-per-deploy (R4-3) and the long-lived-branch case (R4-7) — both of which the synthesis says were applied. R4-6 also moved the pipeline walkthrough into Session 3. So this round is narrow and adversarial: did those two VSM additions actually land for *my* chair, or are they token sentences? Did moving the walkthrough break the monolith framing I praised? And is anything new leaving the monolith reader behind?"
**Review date:** 2026-06-22
**Overall rating:** 9.5/10 (was 6.0 → 8.5 → 9.5 → 9.5) — would I adopt/champion this?

## Verdict

The three changes I came to check all landed clean, and two of them — R4-3 and R4-7 — are genuine monolith-facing fixes, not box-ticking: the batch-size prompt now sits *in the in-session map* (Part 2) tied causally to the low deploy %C/A, and the long-branch note is *arithmetically honest* (I re-derived it — a 2-3 week branch really does push the worked example's flow efficiency to ~8-10%, the "single digits" it claims). The R4-6 move is fully executed: the walkthrough now lives at `sessions/session-3/examples/`, all four of its outbound links resolve from the new location, the only active inbound links are the two correct ones in Session 3 §6.1, and every stale `session-1/examples/...` reference is in old feedback files — backtick spans, not navigation. The monolith framing I praised (`ci-templates`/MSBuild→IIS, "score *your* shape, not the AWS one," AWS-baseline-is-one-of-three) survived the move verbatim. I'm holding at 9.5 — the same two scope items I've named for five rounds remain (R3-8 prod-like SQL *data*, which I don't dock for; and a hair I'll record below where the VSM's worked *table* still doesn't demonstrate its own batch-size rule). The delta is zero: two good fixes landed, nothing broke, and the one perfect-score blocker is still a defensible 101-scope call.

## Score trajectory

| Round | Score | Δ | One-sentence reason |
| ----- | ----- | - | ------------------- |
| R1 | 6.0 | — | Discipline applies to my monolith; the materials demonstrate it almost not at all (zero .NET artifacts). |
| R2 | 8.5 | +2.5 | Strangler-fig carve-out landed in my stack; stored procs and a real .NET pipeline still open. |
| R3 | 9.5 | +1.0 | Both gaps closed; the pipeline was the *verified real* `ci-templates`/MSBuild→IIS mechanism, not a plausible fabrication. |
| R4 | 9.5 | 0.0 | VSM-in-S1 retired my "AWS-pipeline-first" complaint; terminology sweep regression-free; held the half-point for the batch-size soft gap + R3-8. |
| **R5** | **9.5** | **0.0** | R4-3 and R4-7 are real fixes (re-derived the long-branch math; batch prompt is causal, not token); R4-6 move is clean with no broken links and no framing loss; same two scope items remain. |

The flat delta is earned, not inertial. Up was blocked by the same scope items I've named since round 3 (prod-like SQL data, which is arguably out of 101 scope) plus one new hair I found this round; down was blocked by finding zero breakage in any of the three changes and confirming the long-branch claim is mathematically true rather than hand-waved.

---

## R4-3 — "note the batch size at the deploy step" — **LANDED (real, in the in-session map)**

My oldest monolith-specific blind spot, named at R1 #8, R2 #13, and again at R4: for a monolith, deploy *frequency* understates the pain, because each deploy bundles dozens of changes — the number that shocks management is *batch size per deploy*. At R4 it lived only in the homework assessment; the in-session VSM (the new centerpiece) didn't prompt for it.

It now does. `exercises/value-stream-map.md:57`, in **Part 2** (the rules-of-thumb the team applies *while filling in the live map*), reads:

> "**Note the batch size at the deploy step.** How many changes ride one deploy? Write that number next to the deploy / release-window step. A big number there is itself a constraint — it's usually *why* the deploy's %C/A is low: many changes at once, so a break is hard to isolate."

This is not a token sentence — it does the two things I actually wanted:

1. **It's in the live in-session map, not just the homework.** The prompt fires while the team is drawing its own stream, so a monolith team *sees* its batch size in its own figures during the session — exactly the R4-1/R4-3 fix.
2. **It draws the causal line I had to draw myself for four rounds.** "A big number there is *why* the deploy's %C/A is low" connects batch size to the rolled-accuracy failure rate. That's the mechanism — many changes at once → a break is hard to isolate → hotfix → low %C/A. The worked-example narrative reinforces it (`:123-124`: "many changes ship at once, so something breaks and is hard to isolate … each deploy carries one diagnosable change"). The data point now exists *and* is explained, in the place a monolith reader meets it first.

**Verdict:** This retires the in-session half of my four-round batch-size complaint. One hair remains (below), but the prompt itself is exactly right.

## R4-7 — long-lived-branch note in the worked example — **LANDED (and the math checks out)**

At R4 I flagged that the worked example's 2-day branch wait (step 3) models a near-trunk-based team, while my reality is a branch that lives 1-3 *weeks* and bleeds time mostly to trunk drift — so the example undersold where a real long-lived-branch monolith hurts. The fix is at `value-stream-map.md:128`:

> "**This example is a team already close to trunk-based** — its branches live two days. A long-lived-branch monolith would show *weeks* in step 3 (develop, plus its wait), pushing flow efficiency into the low single digits. If that's you, the map will look worse before the course makes it better — which is exactly why you measure first."

I pressure-tested the quantitative claim rather than trusting it, because "single digits" is a falsifiable number. Holding the worked example's other figures fixed and replacing the 2-day develop wait with a realistic long-branch wait:

| Branch age at develop | Total lead | Flow efficiency |
| --------------------- | ---------- | --------------- |
| 2 days (the example) | 127.5 h (~16 wd) | **15.3%** |
| 2 weeks | 191.5 h (~24 wd) | **10.2%** |
| 3 weeks | 231.5 h (~29 wd) | **8.4%** |

The claim is accurate. A 3-week branch lands at 8.4% — solidly single-digit — and a 2-week branch is already at the 10% edge, so any additional queueing (which a long-branch monolith always has) tips it under. The note is honest arithmetic, not a comforting gesture. It correctly attributes the bleed to step 3 ("develop, plus its wait") and frames it the right way for my chair: the map *should* look worse for me, and that's the point of measuring. This is a real fix for the long-branch reader.

## R4-6 — pipeline walkthrough moved to `sessions/session-3/examples/` — **CLEAN (no framing loss, no broken links)**

The synthesis says R4-6 *moved* the file (a real `git mv`), reversing the R4 note that it had only relocated as an activity. I verified the move is complete and harmless:

- **The file physically moved.** `sessions/session-1/examples/` now contains only `cd-vs-continuous-deployment.md`; the walkthrough is at `sessions/session-3/examples/current-state-pipeline-walkthrough.md`.
- **The only active inbound links are the two correct ones**, both in `sessions/session-3/README.md` (§6.1 body, line 130; resources list, line 179), both pointing at the new `examples/` path. They resolve.
- **All four outbound links from the moved file resolve** from the new location (`../../../exercises/current-state-assessment.md:57`, `../README.md` and `.gitlab-ci.yml:62`, `../../../resources/migration-checklist.md`). The `../../../` depth is correct for the deeper location — a common move-bug, and it was handled.
- **Every stale `session-1/examples/...` reference is in `docs/course-feedback/*`** — old reviews (including my own R4 line 44) and synthesis files. These are written-at-the-time records in backtick spans, not navigational links, so leaving them is correct.

Most importantly for my lens, **the monolith framing survived the move verbatim.** I re-read the moved file end to end:

- The "read our own real one first, then the target" framing is intact in S3 §6.1 (`session-3/README.md:128`).
- The workshop instruction I praised at R3 is unchanged at `current-state-pipeline-walkthrough.md:56`: "for the .NET/IIS monolith the GitLab pipeline that `include:`s the shared `ci-templates` (MSBuild publish → IIS). Score *your* shape against the minimums, not the AWS one; use this baseline only if you have no pipeline at all yet."
- The AWS baseline is still correctly demoted to one labelled artifact among three (the real CiraNet pipeline in the strangler-fig section, the monolith's `ci-templates` pipeline named here, and the AWS baseline scored as *the baseline*).

The move strengthened the tree's logic (the walkthrough's only consumer is now co-located with it) without costing the monolith reader anything. Closed.

---

## Standing open items (carry-over)

- **R3-8 (prod-like *data* for a shared SQL Server) — STILL OPEN, still not docked, and confirmed the only real open monolith gap.** The minimums table still names it straight (`minimums-reference.md:94`: "the shared SQL Server is the hard part to make prod-like") and never works it. Exactly where I left it at R3 and R4. I checked the surrounding SQL treatment to confirm nothing *else* opened up: the stored-procedure section (`strangler-fig-violations.md:121-147`) is intact and unchanged — `usp_GetHomeownerLedger_v2`-not-`ALTER`-in-place, migrate callers one at a time, the `sys.sql_expression_dependencies`-misses-cross-process-`EXEC` trap, and the "a flag controls code you own; it cannot control a shared database object other code calls" boundary. That section remains the strongest signal a real SQL owner wrote this. Expand/contract on columns and procs is fully treated; prod-like *data* is the lone remaining un-worked thing. I do **not** dock for it — 101-scope-defensible, and the course no longer pretends it's a one-liner. It is the single item between 9.5 and 10 for my chair.
- **R3-7 (six-VM two-wave rollout glosses drain timing) — STILL OPEN, nice-to-have.** Unchanged. Not a blocker.
- **"Same file, two different levers" (R3-4) — confirmed intact.** `strangler-fig-violations.md:243-251` still draws the deploy-time `web.{env}.config` transform (a deploy, recycle is fine) vs. runtime feature flag (must live in the refresh-interval store) distinction. The line my IIS world most needs survived untouched.

## New this round (adversarial pass)

I hunted for fresh breakage in all three changes and for any new place the monolith reader gets left behind. One low-severity hair; nothing structural.

1. **The VSM worked-example *table* doesn't demonstrate its own batch-size rule.** *(Low. New observation, not a regression.)* Part 2's R4-3 rule (`:57`) tells the reader to "write that [batch] number next to the deploy / release-window step." But the worked example's own table (`:100-108`) row 6 — "Release-day batch deploy, 3 h, —, 70%" — carries *no* batch-size figure. The batch is described in prose below the table (`:123`, "many changes ship at once") but not modelled as the number the rule just told the reader to write. So the course's headline exemplar doesn't quite practice what its rule preaches. A monolith reader following the example as a template sees a deploy row with process/wait/%C/A but no batch column, even though the rule said to add one. **Not a blocker** — the prompt exists where it matters (the live map), and the prose makes the point — but one extra annotation in row 6 (e.g., a "(38 changes)" note beside the deploy step) would make the example model its own rule and would *show*, not just tell, a monolith team what their deploy row looks like. This is the residual of my four-round batch-size item: the in-session *prompt* is now right (R4-3 closed); the *exemplar* still under-demonstrates it.

2. **No AWS bias crept back into the VSM or S1 §5.** *(Clean.)* I swept both for AWS terms. The only occurrences are the two deliberate platform-contrast mentions ("a TypeScript Lambda or a .NET app to an IIS pool", `value-stream-map.md:9`; "whether you ship a Lambda or a .NET app to IIS", `session-1/README.md:158`). Both name .NET/IIS *alongside* Lambda as equals — that's the neutrality working, not bias. The worked example itself is entirely platform-free (no Lambda/SAM/CloudFormation/Dynamo/OIDC). The VSM remains the most genuinely stack-neutral artifact in the course.

3. **No broken references introduced by any of the three changes.** *(Clean.)* Verified the walkthrough move, the VSM↔assessment cross-links (`value-stream-map.md:23,140` ↔ `current-state-assessment.md:7,70,88`), and the assessment's batch-size row (`current-state-assessment.md:80`, "Changes bundled per prod deploy") all resolve and are consistent.

---

## Prioritized open-items list (tiered; blocker vs polish)

### Tier 1 — one-line, clearly worth doing (POLISH, none blocking)

| ID | Item | Effort | Where |
| -- | ---- | ------ | ----- |
| R5-1 | **Annotate the batch size in the worked-example deploy row.** Add a batch figure to row 6 of the worked VSM table (e.g. a "(~38 changes rode this deploy)" note beside "Release-day batch deploy") so the exemplar demonstrates the Part 2 batch-size rule it now states. Closes the *demonstration* half of my five-round batch-size item; the prompt half (R4-3) is already done. | 1 line | `exercises/value-stream-map.md:107` area |

### Tier 2 — small additions (POLISH)

| ID | Item | Effort | Where |
| -- | ---- | ------ | ----- |
| R3-7 | **Nod to drain timing in the six-VM wave rollout.** Carried from R3/R4; unchanged. | 1 line | `strangler-fig-violations.md` wave section |

### Tier 3 — scope call (the only item tied to a perfect score)

| ID | Item | Effort | Where |
| -- | ---- | ------ | ----- |
| R3-8 | **Work the prod-like-SQL-*data* problem** with the honesty the stored-proc section already has (stale/scrubbed/smaller qa restore: subset vs. full clone, PII scrub, refresh cadence). The one item between 9.5 and 10 for the monolith. A 101-scope judgment call. I do **not** dock for leaving it. | Larger | minimums table / a short worked note |

**Blockers:** none. Every item is refinement.

---

## Bottom line for a monolith owner

The two items the round-4 synthesis attributed to me were applied as real fixes, not placeholders. R4-3 put the batch-size prompt in the live in-session map and tied it causally to the deploy's low accuracy — the connection I had to make by hand for four rounds. R4-7's long-branch note is arithmetically honest: I re-derived it, and a 2-3 week branch genuinely drives the worked example's flow efficiency into the 8-10% range it claims. R4-6 moved the pipeline walkthrough cleanly into Session 3 with every link resolving and the `ci-templates`/MSBuild→IIS framing I praised fully intact — the move strengthened the tree without leaving my reader behind.

I'm holding at 9.5 honestly. The single perfect-score item — prod-like *data* for the shared SQL Server — is still named-as-hard rather than worked, and it's the same defensible 101-scope call I declined to dock for in rounds 3 and 4. The one new thing I found is a low hair: the VSM's worked *table* still doesn't model the batch number its own Part 2 rule now tells the reader to write, so the exemplar tells where it could show. Neither is an error; both are thoroughness. Add the batch annotation to row 6 and work the prod-like-data story, and this is a 10 for my chair. As it stands, nothing changes my round-3/round-4 answer: I hand it to my team unedited and keep the conversation going about shrinking that `release/YY.M.W` branch.
