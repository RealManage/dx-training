# Continuous Delivery 101 — Review 4 (round-4 spot-check)

**Student:** Dana (.NET Framework Monolith Maintainer, 12 yrs)
**Stance going in:** "Round 3 closed both my gaps and I scored it 9.5 — but I'm here to spot-check two structural changes, not to re-confirm a number. Session 1 was rebuilt around a value stream map sold as 'platform-agnostic,' and a deploy/release terminology sweep went course-wide. Both are exactly the kind of change that can quietly re-break a monolith reader. Did they?"
**Review date:** 2026-06-22
**Overall rating:** 9.5/10 (was 6.0 → 8.5 → 9.5) — would I adopt/champion this?

## Verdict

The two structural changes are net-positive for my chair, and the VSM rebuild actually *fixes* my oldest standing complaint better than the old workshop did — the first concrete thing a student now meets in Session 1 is *their own flow*, not someone else's AWS pipeline. The terminology sweep is clean: no regressions in my monolith examples, and the "same file, two different levers" line survived intact. I'm holding at 9.5, not raising it, because the VSM worked example reintroduces — in a soft, non-blocking way — the one monolith-specific blind spot I've flagged since round 1 (batch-size-*per-deploy* is invisible), and R3-8 (prod-like SQL data) remains the same honestly-hard, un-worked item. Neither is a falsehood; both are scope/thoroughness, not error. The delta vs round 3 is zero: two good changes landed, one old soft gap got a new home, and nothing broke.

## Score trajectory

| Round | Score | Δ | One-sentence reason |
| ----- | ----- | - | ------------------- |
| R1 | 6.0 | — | Discipline applies to my monolith; the materials demonstrate it almost not at all (zero .NET artifacts). |
| R2 | 8.5 | +2.5 | The strangler-fig carve-out landed — a real worked artifact in my stack — but stored procs and a real .NET pipeline were still open. |
| R3 | 9.5 | +1.0 | Both gaps closed, and the pipeline was the *verified real* `ciranet-management-api` mechanism, not a plausible fabrication. |
| **R4** | **9.5** | **0.0** | VSM-in-S1 closes my long-standing "AWS-pipeline-first" complaint; terminology sweep is regression-free; the half-point I held back is unchanged (batch-size-per-deploy soft gap + R3-8). |

The delta is deliberately flat. I came in willing to move it either way. Up was blocked by the same two soft items I've named for four rounds (now joined by their VSM cousin); down was blocked by finding zero actual breakage in either change. A spot-check that changes nothing is still a result.

---

## The two new changes, scored

### Change 1 — Session 1 rebuilt around Value Stream Mapping

**Is it actually platform-agnostic? — YES, and it's the most genuinely-neutral artifact in the course.**

The old §5 routed me to `current-state-pipeline-walkthrough.md` to score the **`iac-baseline` CloudFormation pipeline** — someone else's AWS artifact, the recurring round-1/round-2 complaint that "the first concrete pipeline a student meets isn't mine." The new §5 ([`exercises/value-stream-map.md`](../../exercises/value-stream-map.md)) replaces that with mapping *my own* flow. A VSM has no stack in it. It's process-time / wait-time / %C/A on whatever steps a change in *my* system actually passes through. The explicit callout (`value-stream-map.md:9`) — "A value stream map doesn't care whether you ship a TypeScript Lambda or a .NET app to an IIS pool … If you run a long-lived monolith, this is where its real delivery pain shows up in figures you can defend" — isn't marketing; it's structurally true of the technique. This is the first Session-1 concrete activity in four rounds that is *mine by construction* rather than mine-by-translation.

**Does the worked example fit a monolith team? — YES, almost suspiciously well.**

The worked stream (`value-stream-map.md:93–121`) is a **weekly-release team**: `intake → refine → develop on a feature branch → code review → wait for the weekly release window → release-day batch deploy → verify`. That is my cadence verbatim. The biggest wait it surfaces — **"wait for the weekly release window, 32 h / 4 days"** — is *literally* the `release/YY.M.W` weekly branch cut that the strangler-fig pipeline section names as the monolith's actual CD gap (`strangler-fig-violations.md:200`). The two documents now tell one story: S1's VSM surfaces the weekly-window wait as the binding constraint; S3's real pipeline confirms that the same weekly release branch *is* the gap. A monolith reader walks from "my map says the window is the constraint" to "the real CiraNet pipeline says the same thing" without a seam. The ~16-day lead time / ~15% flow efficiency numbers are entirely plausible for a fortnightly-ish monolith and, crucially, are not AWS- or greenfield-flavoured anywhere — there's no Lambda, no SAM, no DynamoDB in the example. It is not implicitly greenfield. Credit.

**Does VSM-in-S1 close the "AWS-pipeline-first" complaint? — YES. This is the headline finding of round 4.**

For three rounds my single most-repeated structural gripe was that the *first concrete pipeline a student meets* is the AWS CloudFormation baseline, not anything in my world. The rebuild resolves it from two directions at once:

1. **S1's first concrete activity is now stack-neutral.** A student's first hands-on encounter is mapping their own value stream — no foreign pipeline at all. The AWS-pipeline-first problem can't exist if the first activity has no pipeline in it.
2. **The pipeline walkthrough moved to Session 3 §6.1 — but it did *not* resurface as "AWS-first."** I checked this specifically, because the obvious failure mode is "you deleted the AWS-first problem from S1 and recreated it in S3." It didn't recreate it. In S3 §6.1 (`session-3/README.md:126–140`) the walkthrough is now framed as *"read our own real one first, then the target,"* and the walkthrough's own instruction (`current-state-pipeline-walkthrough.md:56`, verified unchanged from the round-3 fix) explicitly tells the monolith reader to pull up *the GitLab pipeline that `include:`s the shared `ci-templates` (MSBuild → IIS)* and "score *your* shape against the minimums, not the AWS one." So in S3 the AWS baseline is the labelled *baseline to score*, the monolith pipeline is named alongside it, and by the time a student reaches it they've already read the *real* CiraNet pipeline section in the strangler-fig example. The AWS artifact is no longer the first or only concrete pipeline; it's one of three, correctly framed. The complaint is closed, not relocated.

One precision note for the record: the task framing said the walkthrough file "moved to Session 3." Mechanically it didn't move — it still lives at `sessions/session-1/examples/current-state-pipeline-walkthrough.md` and S3 links back to it. What moved is *where a student first encounters it* (S1 §5 → S3 §6.1). All links resolve (verified). The distinction matters only because "the file moved" would predict broken references; nothing is broken.

**The "no QA step, by design" note — does it land for a monolith team? — YES.**

`value-stream-map.md:38` handles this exactly right for my world: *"At RealManage the delivering team owns quality; there is no QA team to hand off to. If your stream has a 'throw it over to QA' wait, that *is* a finding — write it down."* This is the correct move. My monolith team, in practice, *does* have an informal QA-ish handoff (a senior dev's manual smoke pass before the release window), and the note turns that into a *measurable wait to surface* rather than pretending it doesn't exist or prescribing a QA step that RealManage doesn't have. It neither imposes a QA gate nor ignores the reality that big-batch monolith teams often have an ad-hoc one. Lands cleanly.

**Verdict on Change 1:** This is the change I'd have asked for in round 1 if I'd known to. **IMPROVED** — it retires my longest-running structural complaint. Marginal soft gap below.

### Change 2 — Deploy/release terminology pass + "deployable not releasable"

**Did the sweep break any monolith example? — NO. Verified clean.**

I audited every `releasab*` occurrence and every suspect `release`-as-deploy usage across the tree. Findings:

- **`releasability` is kept correctly** as MinimumCD #3's pipeline-verdict noun everywhere it appears (e.g. `session-1/README.md:142`, `minimums-reference.md:39`, `session-3/README.md:43`). No instance where it should now read "deployable." The `minimums-reference.md:7–14` wording note states the deviation explicitly and scopes the kept noun to #3. Disciplined.
- **No `release`-as-technical-deploy leaked into my examples.** The monolith material uses "deploy" for the technical act throughout. The one spot that looks suspect on a grep — `strangler-fig-violations.md:185`, `# auto on release/*` — is correct: it's a GitLab `rules` comment about the `release/YY.M.W` *branch*, not calling a deploy a "release." Legitimate uses ("release window/branch/train/day", "feature flags decouple deploy from release") are all intact.

**Did it make "same file, two different levers" cleaner or muddy it? — CLEANER (it's preserved verbatim and correct).**

This is the point I care about most, because my IIS app has the deploy-time `web.{env}.config` XDT transform (a *deploy*) sitting in the same file family as anything I might mistake for a runtime flag (a *release*). The R3-4 fix that drew this line is present and intact at `strangler-fig-violations.md:248–251`:

> "Don't conflate this with the deploy-time `web.{env}.config` transform from the pipeline section: that sets *per-environment config* and a recycle is fine (it's a deploy); a *runtime feature flag* you flip without redeploying must live in the refresh-interval store, not the transform. **Same file, two different levers.**"

The terminology sweep *strengthens* this, because the rest of the document now uses "deploy" with discipline, so the one sentence that says "the transform is a deploy, the flag is a release" sits inside a document where those two words mean exactly what they say everywhere else. A reader who internalizes the course's deploy≠release line earlier now arrives at this sentence primed to get it. The sweep didn't muddy my example; it gave the example a consistent vocabulary to lean on.

The `minimums-reference.md` .NET/IIS column (lines 85–96) is likewise untouched and still consistent with the verified pipeline — immutable artifacts = "the `a/` dir built once, versioned by GitVersion, promoted"; config-with-artifact = "`web.{env}.config` XDT transform applied **at deploy**." No terminology drift between the .NET column and the strangler-fig pipeline section. The two documents still tell one coherent story.

**Verdict on Change 2:** **IMPROVED (no regression).** The sweep is the rare course-wide find-and-replace that didn't collateral-damage a specialised example. My monolith examples read cleaner, not muddier.

---

## Newly-introduced problems (adversarial pass)

I went looking for fresh breakage in both changes. Here's everything real I found — all soft, none blocking.

1. **The VSM worked example re-hides batch-size-per-deploy — my oldest monolith-specific blind spot, in a new location.** *(Soft gap, recurring.)* Since round 1 I've argued that for a monolith, deploy *frequency* understates the pain because each deploy bundles dozens of changes — the number that shocks management is *batch size per deploy*. The new worked example (`value-stream-map.md:93–121`) traces **one small change** through the stream. That's the right pedagogy for teaching flow efficiency, but it means the headline artifact of the new Session 1 shows a *single* change waiting 4 days for the window — it never shows the release-day deploy carrying the other 39 changes that were also waiting. The "%C/A 70% on the batch deploy = one deploy in three needs a hotfix" line (`value-stream-map.md:118`) gestures at the big-batch failure rate, which is good, but the map's own numbers model a lone change, so a monolith reader doesn't *see* the bundle in their own figures. The assessment still carries the fix (`current-state-assessment.md:80`, "Changes bundled per prod deploy") — so the data point exists in the homework — but the *in-session* VSM, the new centerpiece, doesn't prompt for it. This is the same gap I flagged at R1 #8 and R2 #13, now wearing the VSM's clothes. **Not a regression** (the old workshop didn't capture it either) and not a blocker — but it's the one place the otherwise-excellent VSM rebuild leaves my specific reality slightly under-served. One row in the VSM math ("how many changes rode this deploy?") would close it.

2. **The VSM "develop on a feature branch" step models a short-lived branch implicitly.** *(Cosmetic.)* Step 3 (`value-stream-map.md:101`) is "Develop on a feature branch, 12 h process / 16 h wait." My monolith reality is a branch that lives 1–3 *weeks*, and the wait there is mostly the branch *aging against a drifting trunk*, not a queue. The example's 2-day branch wait undersells where a real long-lived-branch monolith bleeds time. Minor — the technique lets me put my real number in — but the worked example's branch figures are tuned to a team already closer to TBD than mine is. Cosmetic; the method is fine.

3. **`current-state-assessment.md` after repositioning — still serves the monolith. No new break.** I re-checked this because the VSM now front-runs the assessment and could have orphaned it. It didn't. The "Mixed estate?" callout (`current-state-assessment.md:11`), the ◆ Deliberate-control column, the controls scorecard (Part 2), and the "Changes bundled per prod deploy" row (Part 3) are all intact, and Part 3 now correctly says the VSM *estimated* lead time and this is where you replace it with the measured figure (`:70`). The map→assessment handoff is wired both ways (`value-stream-map.md:23,133` ↔ `current-state-assessment.md:7,70,88`). The repositioning strengthened the assessment's framing rather than stranding it.

4. **No AWS-centricity crept into the new VSM content.** I specifically hunted for it. The VSM exercise and S1 §5 contain zero AWS terms; the worked example is platform-free. Clean.

5. **No broken references from the S1→S3 reframing.** All links to `current-state-pipeline-walkthrough` and `value-stream-map` resolve (verified across the tree). Phase 0 of the migration checklist (`migration-checklist.md:15`) now correctly leads with the VSM as the first Phase-0 step, before the assessment.

---

## Standing open items (R3 carry-over)

- **R3-8 (prod-like *data* for a shared SQL Server) — STILL OPEN, still not docked.** Named-as-hard in the minimums table (`minimums-reference.md:94`, "the shared SQL Server is the hard part to make prod-like") and honest throughout, but never *worked*. Exactly where I left it at R3. I explicitly did not dock for it then and I don't now — it's arguably 101-out-of-scope and the course no longer pretends it's a one-liner. It remains the single thing standing between 9.5 and 10 for the monolith crowd.
- **R3-7 (six-VM two-wave rollout glosses drain timing) — STILL OPEN, nice-to-have.** Unchanged. The wave rollout (`strangler-fig-violations.md:192–193`) still states the drain as frictionless; for a not-fully-stateless tier the connection-drain interval is the fiddly bit. A nod, not a rework. Not a blocker.
- **R3-9 (real `.ps1` / `ci-templates` shown as *shape*, not lift-and-run) — leave as-is.** I called this scope-defensible at R3 and still do. The PowerShell is the boring part.

---

## Prioritized open-items list (tiered by effort; blocker vs polish)

### Tier 1 — one-line / few-line, clearly worth doing (POLISH, none blocking)

| ID | Item | Effort | Where |
| -- | ---- | ------ | ----- |
| R4-1 | **Add a "changes bundled in this deploy" prompt to the VSM.** One extra question in Part 4 or one column note — "for a batch deploy, how many changes rode it?" — so a monolith team *sees* its batch size in the in-session map, not only in the homework assessment. Closes my four-round batch-size-per-deploy blind spot at its new home. | 1–2 lines | `exercises/value-stream-map.md` (Part 2 table note or Part 4 Q) |

### Tier 2 — small additions (POLISH)

| ID | Item | Effort | Where |
| -- | ---- | ------ | ----- |
| R4-2 | **One line in the VSM worked example acknowledging the long-lived-branch case.** The 2-day branch wait models a near-TBD team; a half-sentence ("a long-lived-branch team will see this wait measured in *weeks*, mostly trunk drift") would keep the example honest for a team further from TBD than the sample. | 1 line | `exercises/value-stream-map.md:101` area |
| R3-7 | **Nod to drain timing in the six-VM wave rollout.** Carried from R3; unchanged. | 1 line | `strangler-fig-violations.md` wave section |

### Tier 3 — scope call (the only item tied to a perfect score)

| ID | Item | Effort | Where |
| -- | ---- | ------ | ----- |
| R3-8 | **Work the prod-like-SQL-data problem** with the same honesty as the stored-proc section. The one item between 9.5 and 10 for the monolith. A 101-scope judgment call and a bigger write than the rest. I do **not** dock for leaving it. | Larger | minimums table / a short worked note |

**Blockers:** none. Every item above is refinement. With the two structural changes landing clean, the course is adoption-ready for the monolith crowd exactly as it stands.

---

## Bottom line for a monolith owner

The two changes I came to spot-check are good and didn't break me. The VSM rebuild does something none of the prior three rounds managed: it makes the *first concrete thing a Session-1 student touches* their own flow, in their own stack, with their own numbers — which is the cleanest possible answer to my three-round "the first pipeline I meet isn't mine" complaint, and it doesn't resurface that problem in Session 3, where the AWS baseline is now correctly demoted to one labelled artifact among three. The terminology sweep is the rare course-wide edit that left my specialised examples cleaner instead of collateral-damaged, and "same file, two different levers" — the line my IIS world most needs — is intact and now sits in a document that uses deploy and release consistently everywhere else.

I'm holding at 9.5 honestly, not by inertia: the VSM's worked example softly re-hides batch-size-per-deploy (my oldest monolith-specific blind spot, now in the new centerpiece), and prod-like SQL data (R3-8) is still named-as-hard rather than worked. Neither is an error, both are scope, and one VSM line would retire the first. Close R3-8 with the stored-proc section's honesty and add that one batch-size prompt, and this is a 10 for my chair. As it stands, nothing here changes my round-3 answer: I hand it to my team unedited and start the conversation about shrinking that `release/YY.M.W` branch on Monday.
