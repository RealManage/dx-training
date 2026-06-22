# Continuous Delivery 101 — Review 4

**Student:** Priya (Engineering Director, 15 yrs)
**Stance going in:** Round 3 was a greenlight at 9.5 — I'd sponsor this for the whole
estate and hand the governance doc to my compliance lead. Round 4 is a spot-check of two
structural changes (Session 1 rebuilt on Value Stream Mapping; a deploy/release
terminology sweep). My job is to find what the rework broke or weakened, not to re-bless a
score. My recurring theme across four rounds has been **"the precision trailed the prose"** —
promises made in prose but not made real or enforceable. I went hunting for it again.
**Review date:** 2026-06-22
**Round-1 rating:** 7/10 · **Round-2:** 9/10 · **Round-3:** 9.5/10
**Round-4 rating:** 9/10 — down half a point. Not a regression in the *ideas*; the rebuild
introduced one genuine dangling artifact and the synthesis recorded a fix (R3-5) that the
tree does not actually contain. The governance story I fought for is intact, but my own
recurring theme reopened.

## Verdict

The VSM rebuild is the right call and makes my Phase-0 business case *stronger*, not weaker —
flow efficiency is a number I would take to a board. The terminology sweep is correct and
helps my audit lens (deploy ≠ release is now sharp enough to hang release-authorization on).
But two real problems landed in *my* area: the Session 1 **slide deck still runs the
old pipeline-scoring workshop** that the rebuild moved out, and the `customer-facing` MR
gate — recorded as "made real YAML, parse-verified" in the round-3 synthesis — **exists
nowhere in any `.gitlab-ci.yml`**; it is a fenced code block inside a prose file. That is the
exact "precision trailed the prose" pattern I have flagged in every round, and the synthesis
marked it closed. Half a point off until both are real.

---

## The two new changes, scored

### Change 1 — Session 1 rebuilt around Value Stream Mapping — **IMPROVED (with one dangling artifact)**

**As a leader, is flow efficiency a number I'd take to my board? Yes — emphatically.**
This is the single best thing the rebuild did for *me*. "We score 6 of 9 minimums" is an
engineering self-assessment a board glazes over. "**A change takes three weeks to reach
production, and only ~2.5 days of that is anyone working on it — 15% flow efficiency**" is a
business finding. It reframes the entire program as *removing waste from a value stream* —
the language every operations-literate executive and every Lean-trained board member already
speaks. It converts CD from "an engineering practice my teams want" into "a 6x lead-time
opportunity sitting in our process." That is the strongest leadership-facing artifact added
since the governance doc itself.

**Does VSM make the Phase-0 / assess case stronger or weaker? Stronger.** Phase 0 used to
be "score yourself against a 15-item checklist" — useful but inward-facing. Now Phase 0
*opens* by quantifying where lead time is lost, then the scorecard explains *why* against the
minimums. The migration checklist Phase 0 (`migration-checklist.md:15-16`) sequences this
correctly: map the value stream first, then back it with the scored assessment. For a leader
selling a quarter-long investment, "here is the 108 hours of pure waiting we can attack"
is a far better opening than a maturity grade.

**I checked the worked-example math. It is exact.** (`value-stream-map.md:97-121`.)
- Process time 0.5+2+12+1+3+1 = **19.5 h** ✓
- Wait time 24+16+16+16+32+4 = **108 h** ✓
- Lead time 127.5 h = **15.94 working days** ✓ (the doc says "≈16 working days," correct)
- Flow efficiency 19.5 ÷ 127.5 = **15.29%** ✓ (doc says "~15%")
- Rolled %C/A 0.90 × 0.85 × 0.90 × 0.75 × 0.70 × 0.95 = **34.34%** ✓ (doc says "≈34%")
- The prose "~2.5 days being worked" = 19.5 h ÷ 8 = 2.44 d ✓; "three weeks" calendar from
  ~16 working days ✓; step 3's "12 h (1.5 d)" ✓.

The example also *rings true*: a weekly-release team whose biggest single wait is the
4-day release window and whose lowest-accuracy step is the batch deploy is exactly the
shape of every weekly-cadence team I have run. And it lands the thesis honestly — both
starred steps resolve to "smaller batches," which is the course's whole argument now sitting
in the team's own numbers rather than asserted. Pedagogically this is better than the old
pipeline-scoring opener: it surfaces the constraint from evidence instead of from a checklist.

**Did moving the controls scorecard out of the live Session-1 workshop weaken the governance
story I fought for in rounds 2-3? No — and I checked this carefully, because it was my first
worry.** The ◆ Deliberate? column and the Part 2 controls scorecard (the things I won in
round 3) are fully intact in `current-state-assessment.md:19,50-64`. They did not get
deleted or diluted; the assessment was *repositioned* from a live exercise to homework that
*pairs* with the in-session VSM (`current-state-assessment.md:7` makes the pairing explicit —
map shows *where time goes*, scorecard shows *how you measure against minimums and controls*).
The homework framing (`session-1/README.md:193`) explicitly says "score the CI/CD minimums
**and controls**." So the governance dimension survived the move.

I will register one *mild* concern as a leader, short of a finding: governance now lives
entirely in homework. The live, facilitated, argue-it-out-loud hour is the VSM; the controls
scorecard is done alone, later, by whoever does the homework. The round-3 win was that a
director scoring her estate is *prompted* to assess control maturity — that prompt still
exists, but it moved from "in the room with the facilitator" to "on your own afterward,"
which lowers the odds the disagreement-surfacing value (the thing the assessment intro brags
about) actually happens for the controls rows. This is a deliberate, defensible trade — the
VSM genuinely is the better live exercise — and the pairing is well-constructed, so I am not
docking for it. But if adoption data later shows the controls scorecard going unfilled, this
is why.

**The one real problem the rebuild introduced — the slide deck is dangling.**
`slides/session-1-outline.md` still runs the *old* workshop. Slide 8 (`:124-134`) is titled
**"Now: score OUR pipeline,"** its message is "We learn CD against our own real pipeline,
then you score yours," and its talking track walks the `iac-baseline` `when: manual` pipeline
and tells students to "Score honestly." Slide 1's "Say" (`:23-25`) promises that by the end
of the session students can "**score our real pipeline**." Both are now false: Session 1's
live workshop is the Value Stream Map (`session-1/README.md` §5), and the pipeline-scoring
walkthrough was moved to **Session 3 §6.1** (`session-3/README.md:126-140`). An instructor
who builds the deck from this outline will run the *replaced* workshop and contradict the
run-sheet. The outline's own header (`:9-10`) even says it covers "scoring the pipeline, the
assessment" as the hands-on portion — the description of Session 1 that the rebuild
invalidated. This is a concrete dangling reference in instructor-facing material, and it is
the kind of "the artifact didn't follow the prose" gap I keep finding. Fix: replace Slide 8
with a "Map our value stream" slide and correct Slide 1's promise.

### Change 2 — Deploy/release terminology sweep — **IMPROVED / helps my lens**

**Does this help or hurt my governance lens? It helps, materially.** My entire round-1→3
argument about *release authorization* rests on deploy ≠ release: the deploy is an
engineering act the Eng Lead authorizes; the *release* (flag flip) is the business-risk
decision that, for a regulated or client-facing change, is the one that needs my sign-off
and my audit record. A course that used "release" loosely to mean "the deploy" actively
undermined that distinction. Standardizing the trunk state on **"deployable"** and reserving
**"release"** for the user-facing flag flip makes the authority split I won in round 3
(`session-3/README.md:47`) *lexically* consistent with the rest of the course. The audit
story is sharper for it: "who authorized the *release*" is now unambiguously the flag flip,
not the deploy.

**Is the deviation note appropriate or pedantic? Appropriate — and well-judged.**
`minimums-reference.md:7-14` explains that the course says "deployable" where MinimumCD says
"releasable," keeps **releasability** only as MinimumCD's name for the pipeline's verdict
(minimum #3), and gives the *reason* (deploy is technical, release is a business decision,
the two are deliberately decoupled). For an auditor or a contracts lawyer who pulls
minimumcd.org and notices the course uses a different word, a single sourced note saying
"the bar is identical; only the word is sharper, and here's why" is exactly the right move —
it pre-empts the "you deviated from your own cited standard" question. That is governance
hygiene, not pedantry. I would not cut it.

**Did the sweep break a sentence or introduce awkwardness? Largely no — it is consistent.**
I checked every surviving `releasab*` occurrence:
- Every "the pipeline decides releasability" / "owns releasability" / "where releasability
  is decided" is the **kept** minimum-#3 usage — consistent across `minimums-reference.md`,
  `session-1/README.md:142`, `session-3/README.md`, `migration-checklist.md:115`,
  `README.md:171,226,244`, `CLAUDE.md`, `ai-assisted-delivery.md:13`, and the assessment.
  This includes `session-2/README.md:21` ("the pipeline (eventually) owns releasability"),
  which reads as a paraphrase of minimum #3 and is fine under the note's own carve-out.
- No surviving use of "releasable" as the old trunk-state word. The sweep landed.
- "release window," "release-day deploy," "weekly release" describe the **old, pre-CD
  world** where deploy *was* release — which is historically accurate and the right way to
  describe the thing CD dismantles. The course is internally consistent here; the VSM
  example's "wait for the weekly release window" is correctly the batching wait being
  measured, not a CD-world term.

Two **minor** awkwardnesses, both polish-tier, neither a regression:
- `glossary.md:8` — "Releasing is a business decision; *being able to release* is an
  engineering guarantee." Crisp, but "being able to release" momentarily blurs the capability
  (can flip the flag) with the technical state (artifact is deployable) the rest of the
  glossary works hard to separate. A one-word tighten ("being able to release *on demand*")
  would remove the wobble. Not worth a point.
- The course never states *once*, in plain words, "in the old model deploy *was* release —
  that's why we still say 'release window' for the pre-CD wait." A reader meeting "deployable"
  in the minimums and "release window" in the VSM could, briefly, think the sweep was
  incomplete (I had to confirm it wasn't). One framing sentence in the glossary's deploy/
  release section would close that gap for a careful reader. Polish.

Net: the sweep is ~95% clean, helps the governance lens specifically, and the deviation note
is the right professional move.

---

## Newly-introduced problems (adversarial pass)

I went looking for breakage, overlap, and inconsistency the rework could have caused.

1. **R3-5 was recorded as fixed; the tree does not contain the fix.** This is the headline
   finding and it is *in my area*. The round-3 synthesis (`round-3-synthesis.md:99-102`)
   states R3-5 was applied: "`communicating-releases.md` now shows the `customer-facing` /
   `no-user-impact` gate as a real, runnable GitLab job (`release-impact-label`, runs in the
   MR pipeline …). **YAML parse-verified.**" But a grep of *every* `.yml` in the course for
   `customer-facing`, `no-user-impact`, `release-impact-label`, or `CI_MERGE_REQUEST_LABELS`
   returns **nothing**. The job exists only as a **fenced ```yaml code block inside
   `communicating-releases.md`** (`:79-93`) — a markdown file. It is not in
   `session-3/examples/.gitlab-ci.yml` or any other real pipeline file. So the gate is now
   *shown* (better than round 3, where it was only described) but still not *wired into* the
   course's actual pipeline. "YAML parse-verified" is misleading: there is no `.yml` to parse;
   a fenced block in prose is not run, not included, and not parse-checked by `npm run build`.
   This is precisely my four-round theme — **the precision trailed the prose** — and it was
   marked closed. The release-notes data source, the thing I'd defend to a client as "how we
   guarantee you hear about changes," still rides on a check that lives in a doc, not in the
   pipeline. *Either* move the job into `session-3/examples/.gitlab-ci.yml` (where it is
   actually exercised) *or* correct the synthesis to say "shown as a snippet, not yet wired
   into the example pipeline." Right now the record overstates the state of the tree.

2. **The pipeline-walkthrough file is orphaned by location, not by link.** The moved activity
   lives at `sessions/session-1/examples/current-state-pipeline-walkthrough.md` but is now
   *consumed* by Session 3 §6.1. The in-prose links are all correct (Session 3 reaches it via
   `../session-1/examples/…`, the file's own "How to use this in the workshop" is neutral and
   no longer claims to be a Session-1 activity, its Related section points to Session 3). So
   no broken link — but a file whose *only* consumer is Session 3 sitting under `session-1/
   examples/` is a maintenance trap and mildly confusing to anyone browsing the tree. Cosmetic;
   I note it for completeness, not as a blocker. (It also undercuts the slide-deck fix in #1's
   sibling sense: the title still reads "Scoring Our Current Pipeline," reinforcing the
   stale "this is the Session 1 workshop" mental model the deck still carries.)

3. **VSM vs assessment overlap — checked, and it's coordinated, not conflicting.** I
   specifically tested whether the new VSM duplicates or contradicts the assessment's Part 3
   (DORA baseline) or Part 4 (name the constraint). It does not. The pairing is explicit and
   well-built: VSM *estimates* lead time as process + wait (`value-stream-map.md:23`) and
   *surfaces* a candidate constraint from the numbers; the assessment Part 3 then *replaces
   the estimate with measured* lead time and adds the other three DORA metrics
   (`current-state-assessment.md:70`), and Part 4 *confirms or revises* the VSM's candidate
   constraint against the scorecard (`:88-90`). They are deliberately complementary — map
   finds it from where time is lost, scorecard confirms which minimum/control it maps to.
   This is good instructional design, not redundancy. No finding.

4. **Deviation note accuracy — checked, accurate.** The note's claim that "releasability" is
   kept *only* for minimum #3 holds against the actual occurrences (item-by-item above). The
   note does not overstate.

---

## Standing open items — disposition

- **R3-5 (customer-facing label as real CI):** **REOPENED.** Recorded as applied + "parse-
   verified," but lives only as a prose code block; absent from every real `.yml`. My
   recurring theme, marked closed prematurely. (Detail in newly-introduced #1.)
- **R3-1 (break-glass review owner):** **CLOSED / verified.** The accountable owner ("on-call
   lead / service owner … recorded in the incident record") is present in all three places —
   `governance-and-compliance.md:90-92` and the auditor paragraph `:139`, and `glossary.md:176`.
- **"Controls are debt" framing:** **STILL CLOSED.** Re-read end to end after the rebuild;
   the debt-vs-legitimate-permanent-control test is intact in the governance doc, glossary,
   troubleshooting (`:29`), session-3 (`:45`), and is *operationalized* by the ◆ column, which
   survived the assessment's repositioning. The terminology sweep did not regress it.
- **Governance dimension in the assessment:** **STILL CLOSED**, with the mild "moved to
   homework" caveat noted above (not docked).

---

## Prioritized open items

Tiered by effort; adoption-blocker vs polish marked.

### Tier 1 — small fixes, clearly worth doing

| ID | Item | Blocker? | Where |
| -- | ---- | -------- | ----- |
| R4-1 | **Fix the Session 1 slide deck.** It still runs the *replaced* pipeline-scoring workshop (Slide 8 "Now: score OUR pipeline"; Slide 1 "Say" promises "score our real pipeline"; header `:9-10` describes the old hands-on flow). Replace with a "Map our value stream" slide; correct Slide 1's end-state promise. An instructor building from this contradicts the run-sheet. | **Adoption-blocker for any instructor-led delivery** (it mis-runs the session) | `slides/session-1-outline.md:9-10,23-25,124-134` |
| R4-2 | **Make the `customer-facing` gate real, or correct the record.** Either move the `release-impact-label` job into `session-3/examples/.gitlab-ci.yml` so it's actually exercised, or amend `round-3-synthesis.md:99-102` to stop claiming it's "real, runnable … YAML parse-verified." Today the gate exists only as a prose snippet. | Not a blocker; **credibility / my recurring theme** | `communicating-releases.md:79-93`; `session-3/examples/.gitlab-ci.yml`; `round-3-synthesis.md:99-102` |

### Tier 2 — polish

| ID | Item | Blocker? | Where |
| -- | ---- | -------- | ----- |
| R4-3 | **Relocate (or retitle) the pipeline-walkthrough file.** Its only consumer is Session 3 §6.1, but it sits under `session-1/examples/` and is titled "Scoring Our Current Pipeline." Move to a neutral/`session-3` location or note its ownership, so the tree stops implying it's the Session 1 workshop. | No | `sessions/session-1/examples/current-state-pipeline-walkthrough.md` |
| R4-4 | **One framing sentence on "old world: deploy = release."** State once (glossary deploy/release section) that the pre-CD model *equated* the two — which is why "release window" survives as the historical term — so a careful reader doesn't read it as an incomplete sweep. | No | `glossary.md:98-104` |
| R4-5 | **Tighten `glossary.md:8`** — "being able to release" → "being able to release *on demand*" to keep capability vs technical-state distinct. | No | `glossary.md:8` |

### Tier 3 — watch-item, not an action

- The controls scorecard now lives only in homework, not the facilitated hour. Defensible
  trade (VSM is the better live exercise) and the pairing is well-built — but if adoption
  data shows the Part 2 controls rows going unfilled, revisit whether a 5-minute controls
  touch belongs back in the room.

---

## Bottom line for an eng leader

**Still a greenlight — and the ideas are as strong as round 3 or stronger.** The VSM rebuild
hands me a board-grade number (flow efficiency) and makes my Phase-0 business case better; the
terminology sweep sharpens exactly the deploy ≠ release distinction my release-authorization
and audit story depends on, and its deviation note is the right professional move. The
governance work I won in rounds 2-3 — the ◆ column, the controls scorecard, the debt-vs-
control test, the break-glass owner — all survived the rebuild intact.

I am at **9, not 9.5**, for two concrete reasons, both in my lane: an instructor building
Session 1 from the supplied slide deck will run the *deleted* pipeline-scoring workshop, and
the `customer-facing` release-notes gate that the synthesis certifies as "real, parse-verified
YAML" exists only as prose — my four-round "precision trailed the prose" theme, marked closed
when it isn't. Neither is a *content* regression and neither threatens the governance
defensibility I greenlit. Fix the slide deck (Tier 1, blocks instructor-led delivery) and
make the label gate real or correct the record (Tier 1, credibility), and this goes straight
back to 9.5 — and with the polish items, finally to 10.

**Round-4 rating: 9/10** (down 0.5 from 9.5 — process/artifact gaps from the rebuild, not an
idea regression).
