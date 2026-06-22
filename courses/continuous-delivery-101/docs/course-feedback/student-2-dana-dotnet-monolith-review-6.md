# Continuous Delivery 101 — Review 6 (round-6 spot-check)

**Student:** Dana (.NET Framework Monolith Maintainer, 12 yrs)
**Stance going in:** "Round 5 held at 9.5 and named one item in my lane — R5-4, annotating the batch size in the worked VSM deploy row so the exemplar finally *demonstrates* the Part 2 rule it states. The synthesis says it was applied as '(~20 changes)'. That's a smaller number than the '~38' I floated. So this round is narrow: did the annotation actually land for the monolith reader, is ~20 internally coherent, did it disturb the worked-example math, and did any AWS bias creep back while the file was edited? I am not here to manufacture a sixth-round nitpick."
**Review date:** 2026-06-22
**Overall rating:** 9.5/10 (was 6.0 → 8.5 → 9.5 → 9.5 → 9.5) — would I adopt/champion this?

## Verdict

R5-4 landed clean and — more than I expected — landed *well*. The worked-example deploy row now carries `(~20 changes)` (`value-stream-map.md:107`) and the reveal bullet ties to "all ~20 changes ship at once" (`:123`), so the course's headline exemplar finally *shows* the batch number its own Part 2 rule (`:57`) tells the reader to write, instead of only telling. The number is internally coherent: the example explicitly frames itself as "a team already close to trunk-based — its branches live two days" (`:128`), and ~20 changes on a weekly window is the right magnitude for *that* team — a window-batching problem, not yet a 1-3-week-branch monolith. My old "~38" would have contradicted that framing, so the synthesis chose the more consistent figure; I withdraw the ~38 suggestion. The annotation is purely presentational — I re-derived the math and every figure is byte-for-byte unchanged (19.5 / 108 / 127.5 h, 15.3% flow efficiency, 34.3% rolled %C/A). No AWS bias crept back into the VSM or Session-3 §6.1; the monolith framing I've praised since round 3 is intact verbatim. My one standing item — R3-8, prod-like *data* for the shared SQL Server — is exactly where I left it, still named-as-hard rather than worked, still a defensible 101-scope call I do **not** dock for. The monolith story is now as good as a 101 can honestly make it. I'm holding at 9.5, and I'll say plainly: I've run out of real fixes in my lane. The remaining half-point is a scope philosophy question, not a defect.

## Score trajectory

| Round | Score | Δ | One-sentence reason |
| ----- | ----- | - | ------------------- |
| R1 | 6.0 | — | Discipline applies to my monolith; the materials demonstrate it almost not at all (zero .NET artifacts). |
| R2 | 8.5 | +2.5 | Strangler-fig carve-out landed in my stack; stored procs and a real .NET pipeline still open. |
| R3 | 9.5 | +1.0 | Both gaps closed; the pipeline was the *verified real* `ci-templates`/MSBuild→IIS mechanism, not a fabrication. |
| R4 | 9.5 | 0.0 | VSM-in-S1 retired my "AWS-pipeline-first" complaint; held the half-point for the batch-size soft gap + R3-8. |
| R5 | 9.5 | 0.0 | R4-3 + R4-7 real fixes; walkthrough move clean; named the residual hair (exemplar didn't model its own batch rule). |
| **R6** | **9.5** | **0.0** | R5-4 closes that residual — the exemplar now models its own rule with a coherent ~20-change figure, math untouched, no bias regression; only R3-8 (scope call) remains. |

The flat delta is earned. Up is blocked by the single scope item I've named since round 3 (prod-like SQL *data*, arguably out of 101 scope) — and this round I found *no* new hair to add to it. Down is blocked by finding zero breakage: the annotation is consistent end-to-end, the arithmetic is untouched, and nothing regressed.

---

## R5-4 — batch size annotated in the worked-example deploy row — **LANDED (closes my five-round batch-size item)**

This is the demonstration half of an item I have carried, in some form, since round 1 (#8) and named again at R2, R4, and R5. The progression: R4-3 put the batch-size *prompt* in the live in-session map (`:57`, "write that number next to the deploy / release-window step"); at R5 I confirmed the prompt was real but flagged that the worked *exemplar* — the thing a team copies as a template — still showed a deploy row with no batch figure, so it told without showing. R5-4 closes exactly that.

What I verified on disk:

- **The annotation exists and is in the right place.** `value-stream-map.md:107`, row 6: "Release-day batch deploy (~20 changes) | 3 h | — | 70%". The batch number now sits *on the deploy row*, which is precisely where the Part 2 rule (`:57`) tells the reader to write it. The exemplar now practices what the rule preaches.
- **The reveal bullet is consistent with it.** `:123`: "all ~20 changes ship at once, so something breaks and is hard to isolate." The same figure, tied causally to the 70% deploy %C/A — the big-batch failure rate made visible. Prompt (`:57`) → exemplar annotation (`:107`) → causal reveal (`:123`) now form one coherent chain. A monolith reader following the example as a template sees the full move: *here is where the batch number goes, here is why it drags accuracy down, here is the fix.*

**Is ~20 internally coherent for a weekly-release team?** Yes — and this is the part I pressure-tested hardest, because it's the one place a wrong number would have undercut the fix. The worked example self-labels as "a team already close to trunk-based — its branches live two days" (`:128`) on a weekly release window (32 h wait, "done Monday waits for Thursday"). For that specific team — short branches, the batch pain concentrated in the *window* rather than in long-lived branches — ~20 changes per weekly deploy is the right order of magnitude. My round-1/round-5 "~38" described a *heavier* long-lived-branch monolith, which line 128 explicitly says this example is **not**. Annotating this exemplar with ~38 would have contradicted its own framing. So ~20 is the more internally consistent choice, and I formally withdraw the ~38 suggestion — the synthesis picked the better number for this exemplar. (The long-branch monolith's higher figure is handled separately and correctly by the line 128 note, which sends *that* reader to expect "weeks in step 3" and single-digit flow efficiency.)

**Does it disturb the worked-example math?** No — confirmed by re-derivation, not trust. The batch count is presentational; it appears only in the Step label and the prose reveal, never in the process/wait/%C/A columns. Holding all figures as written:

- Total process = 0.5 + 2 + 12 + 1 + 3 + 1 = **19.5 h**
- Total wait = 24 + 16 + 16 + 16 + 32 + 4 = **108 h**
- Total lead = **127.5 h** (≈ 16 working days)
- Flow efficiency = 19.5 ÷ 127.5 = **15.3%**
- Rolled %C/A = 0.90 × 0.85 × 0.90 × 0.75 × 0.70 × 0.95 = **34.3%**

Every number matches the published figures (`:112-116`) exactly. The annotation changed the narrative, not the arithmetic — which is correct, because deploy *batch size* and deploy *process time* are different quantities. A reader who tried to fold "20 changes" into the math would be wrong, and the example correctly keeps them separate.

**Does the reveal bullet read cleanly?** Yes. `:123` reads as one sentence: "nearly one deploy in three needs a hotfix. That's the big-batch failure rate made visible: all ~20 changes ship at once, so something breaks and is hard to isolate." It connects the 70% %C/A → batch size → hard-to-isolate failure in a single causal line. No awkward seam from the edit, no leftover reference to a different number, no double-counting.

**Verdict:** This retires the demonstration half of my longest-standing monolith item. The prompt was already right (R4-3); now the exemplar models it with a coherent figure and untouched math. Closed.

---

## Regression sweep — monolith framing intact, no AWS bias

I swept the VSM exercise and Session-3 §6.1 for any bias that could have slipped in while the file was edited.

- **VSM AWS-term sweep — clean.** The only platform-named occurrence in the whole exercise is the deliberate contrast at `value-stream-map.md:9` ("whether you ship a TypeScript Lambda or a .NET app to an IIS pool"), which names .NET/IIS *alongside* Lambda as equals — neutrality working, not bias. The worked example itself is entirely platform-free (no Lambda/SAM/CloudFormation/Dynamo/OIDC). The VSM remains the most genuinely stack-neutral artifact in the course.
- **Monolith pipeline framing in §6.1 — intact verbatim.** The walkthrough still reads (`current-state-pipeline-walkthrough.md:56`): "for the .NET/IIS monolith the GitLab pipeline that `include:`s the shared `ci-templates` (MSBuild publish → IIS). Score *your* shape against the minimums, not the AWS one; use this baseline only if you have no pipeline at all yet." The "read our *own* real one first, then the target" framing is intact at `session-3/README.md:128`, and the AWS baseline is still correctly demoted to one labelled artifact among three. Nothing in this lane moved.
- **Strangler-fig SQL treatment — intact.** The stored-procedure section (`strangler-fig-violations.md:121-147`) is unchanged and remains the strongest signal a real SQL owner wrote this: `usp_GetHomeownerLedger_v2`-not-`ALTER`-in-place, migrate callers one at a time, the `sys.sql_expression_dependencies`-misses-cross-process-`EXEC` trap, and the "a flag controls code you own; it cannot control a shared database object other code calls" boundary. The R3-4 "same file, two different levers" line — deploy-time `web.{env}.config` transform vs runtime flag in the refresh-interval store — is intact at `:248-251`.

---

## Standing open items (carry-over)

- **R3-8 (prod-like *data* for a shared SQL Server) — STILL OPEN, still not docked, confirmed the only real open monolith gap.** The minimums table still names it straight (`minimums-reference.md:94`: "the shared SQL Server is the hard part to make prod-like") and does not work it. Exactly where I left it at R3/R4/R5. Expand/contract on columns and procs is fully treated; prod-like *data* (stale/scrubbed/smaller qa restore: subset vs. full clone, PII scrub, refresh cadence) is the lone remaining un-worked thing. I do **not** dock for it — it is 101-scope-defensible, and the course no longer pretends it's a one-liner. It is the single item between 9.5 and 10 for my chair, and it is a scope-philosophy call, not a defect.
- **R3-7 (six-VM two-wave rollout glosses drain timing) — STILL OPEN, nice-to-have.** Unchanged. Not a blocker.

## New this round (adversarial pass)

I hunted for fresh breakage from the R5-4 edit and for any new place the monolith reader gets left behind. I found nothing actionable.

1. **The exemplar↔rule consistency is now complete.** The residual hair I logged at R5 (#1) — the worked table not modelling its own batch rule — is the thing R5-4 fixed. I re-checked that the fix didn't introduce a *new* inconsistency (e.g., a stray "38" somewhere, or the math accidentally counting the batch). It didn't: the only batch figures in the file are the rule (`:57`), the annotation (`:107`), and the two reveal references (`:123`), all saying ~20, none touching the columns.
2. **No AWS bias regression in either edited area.** Swept above. Clean.
3. **No broken references introduced.** The R5-4 edit was in-line text on rows already present; no links moved. The VSM↔assessment cross-links (`value-stream-map.md:23,140` ↔ `current-state-assessment.md`) still resolve.

I want to be honest about diminishing returns, because that's the most useful thing I can say in a sixth review: **I have run out of real fixes in the monolith lane.** Every concrete monolith gap I named across five rounds — zero .NET artifacts (R1), strangler-fig carve-out (R2), the real `ci-templates`/MSBuild→IIS pipeline (R3), batch-size-per-deploy in the live map (R4-3), the long-branch math note (R4-7), and now the exemplar modelling its own batch rule (R5-4) — has been closed with a substantive, verifiable fix. The only thing left is R3-8, and that is genuinely a judgment about how far a *101* should go into prod-like-data engineering, not a flaw in what's there. I decline to manufacture a sixth-round nitpick to justify motion in either direction.

---

## Prioritized open-items list (tiered; blocker vs polish)

### Tier 1 — one-line, clearly worth doing

None. The R5-4 annotation closed the last Tier-1 item in my lane.

### Tier 2 — small additions (POLISH)

| ID | Item | Effort | Where |
| -- | ---- | ------ | ----- |
| R3-7 | **Nod to drain timing in the six-VM wave rollout.** Carried from R3/R4/R5; unchanged. Genuinely optional. | 1 line | `strangler-fig-violations.md` wave section |

### Tier 3 — scope call (the only item tied to a perfect score)

| ID | Item | Effort | Where |
| -- | ---- | ------ | ----- |
| R3-8 | **Work the prod-like-SQL-*data* problem** with the honesty the stored-proc section already has (stale/scrubbed/smaller qa restore: subset vs. full clone, PII scrub, refresh cadence). The one item between 9.5 and 10 for the monolith. A 101-scope judgment call. I do **not** dock for leaving it. | Larger | minimums table / a short worked note |

**Blockers:** none. Every item is refinement, and the Tier-1 line is now empty.

---

## Bottom line for a monolith owner

The one item the round-5 synthesis attributed to me was applied as a real fix, not a placeholder. R5-4 put the batch number on the worked-example deploy row (`~20 changes`) and tied the reveal bullet to it, so the course's headline exemplar finally *demonstrates* the batch-size rule it has stated since round 4 — and the figure is internally coherent with the example's "already close to trunk-based" framing, where my earlier ~38 would not have been. I re-derived the math: untouched (19.5 / 108 / 127.5 h, 15.3%, 34.3%), exactly as it should be for a presentational annotation. No AWS bias crept back; the `ci-templates`/MSBuild→IIS framing, the stored-proc expand/contract treatment, and the "same file, two levers" line are all intact.

I'm holding at 9.5 honestly, and for the last time I'll name the reason: the single perfect-score item — prod-like *data* for the shared SQL Server — is still named-as-hard rather than worked, and it's the same defensible 101-scope call I declined to dock for in rounds 3, 4, and 5. That is now the *only* thing standing between this and a 10 for my chair, and it is a question about scope, not quality. The monolith story is as complete as a 101 can make it without becoming a database-migration course. I hand this to my team unedited, and I'm done finding things to fix in my lane.
