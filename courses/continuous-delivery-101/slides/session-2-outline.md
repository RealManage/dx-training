# Session 2 — Slide Outline (build in your tool)

**How to use this:** one entry per slide. *Message* is the single idea the slide must
land. *Visual* is an image-heavy suggestion you build in PowerPoint / Canva / Gamma —
the slide should be mostly that image. *On-slide text* is the few words that go on the
slide (keep it to this). *Say* is your talking track — it does **not** go on the slide.

The run-sheet is [`../sessions/session-2/README.md`](../sessions/session-2/README.md);
this covers the ~15 present-the-concept moments plus three section dividers. The 25-minute
decompose workshop is hands-on — no concept slides, just the one transition slide (14).

Target: **~15 concept slides + 3 dividers** for a 2-hour session. Same rule as Session 1:
if a slide needs more than one sentence of on-slide text, it's doing too much — push it
into *Say* or split it. This session is more concept-heavy than Session 1 (≈90 min of
teaching vs a 25-min workshop), so it runs longer than Session 1's eight — that's expected.

Three code walkthroughs (slides 9, 11, 12) use **framing slide → editor**: the slide sets
up the *why* and the discipline points, then you switch to the real file. There is **no
single held device** this session (Session 1 held the litmus test all the way through);
instead the three teaching blocks each get a light section-divider slide.

---

## Slide 1 — Title

- **Message:** Session 2 is about the one thing no pipeline can do for you — the *team
  practice* of trunk-based development.
- **Visual:** Full-bleed hero echoing Session 1's flow imagery but zoomed to the team:
  many small parcels merging onto one steady conveyor (the trunk), set against one lone
  boulder waiting to drop. Reuse the course hero gradient; drop a real photo in later via
  reveal's `data-background-image` if you want.
- **On-slide text:** "Continuous Delivery 101" · "Session 2 — Trunk-Based Development & CI"
- **Say:** Set the frame and objectives. By the end they can: explain why long-lived
  branches cost merge pain, lost work, and big batches; apply TBD (branches < 1 day);
  decompose a feature into small, independently shippable slices; use a feature flag to
  merge unfinished work safely; and name the CI gates that keep the trunk deployable.
  The frame: Session 1 was the *why*; today is the first *practice* that makes it real —
  and it's a working agreement and engineering discipline, not a tool you buy.

---

## Slide 2 — Recap → Connect

- **Message:** Session 1 proved big batches are risky and CD keeps every change
  deployable; today's practice is the one the pipeline can't give you.
- **Visual:** Split panel. Left: tiny thumbnails of Session 1's vicious-cycle ring + an
  "always-deployable trunk." Right (the bigger half): a calendar with a single large
  question mark — "how long does your branch live?"
- **On-slide text:** "Recap: big batch = risk · trunk stays deployable" · "Your median
  branch life = ?"
- **Say:** 60-second recap only — don't re-teach Session 1. Then the connect: have each
  person name their **median branch life** out loud (the number from their homework
  assessment). For teams coming off weekly releases it's days to weeks — and that single
  number is usually the binding constraint on CD. Don't try to fix it yet; just surface
  it so the branching-cost slide lands on their own reality. (This is a connect, not a
  thread we return to — we deliberately chose no single held device this session.)

---

## Divider — Part 1 of 3: The Branching Problem

*(README agenda §2. The three teaching blocks are numbered 1–3 for the audience; Review /
Connect is the open and the workshop / wrap-up is the close, so they aren't part of the count.)*

- **Message:** Section marker: the practice, and what the old habit actually costs.
- **Visual:** Full-bleed section-title slide in the course palette. Large "The Branching
  Problem", faint diverging-branch line motif behind it. No prose.
- **On-slide text:** "Part 1 of 3 — The Branching Problem"
- **Say:** One line: "Start with the habit that quietly blocks everything — the
  long-lived branch." Move straight to slide 3.

---

## Slide 3 — What long-lived branches actually cost

- **Message:** A two-week branch isn't safe isolation — it's risk accumulating
  off-camera.
- **Visual:** Anti-pattern-1 made visual: `main` as a straight lane; a feature branch
  peels away and the gap **widens** as it travels, with four cost labels attaching along
  the way — *drift · merge pain · lost work · it's a 2-week batch*. The big-bang merge at
  the end drawn as a collision.
- **On-slide text:** "A long branch is a big batch in disguise" · "drift · merge pain ·
  lost work"
- **Say:** Walk the four costs. **Drift:** trunk moves on; the longer you're away, the
  more the world changed under you. **Merge pain:** reconciling two weeks of divergence
  is error-prone — conflict resolution silently drops or corrupts code. **Lost work:**
  branches too painful to merge get *abandoned* — real work thrown away. And the killer:
  **it IS a big batch** — everything Session 1 said about big-batch risk lands the moment
  you merge. Then the reframe (say it, don't put it on the slide): TBD is not "no
  branches" — it's branches so short they never get the *chance* to drift. Point to
  `branching-antipatterns.md` for the other five (shared `develop`, branch-per-env, code
  freeze, the long-open MR).

---

## Slide 4 — TBD: branches so short they never drift

- **Message:** Trunk-based development is the branching pattern CI *requires* — and it's
  tooling you already have, just smaller and shorter.
- **Visual:** An honest **git graph laid over day columns**: **Mon and Tue shown in full,
  Wed only beginning and fading off the right edge** (the pattern continues — and the
  partial day frees up width in the full ones). `main` is one trunk of commits; **each
  full day holds two short-lived branches** that merge back at a *later* commit **within
  the same day** — several integrations per day, not one. **Monday** runs its two branches
  *back to back*; **Tuesday** runs its two *concurrently* — overlapping in time, drawn at
  two heights and **tinted differently** (the second branch violet) so they read apart
  where they cross — which sets up the merge / merge-conflict talk (the overlap window is
  the only interval in which they can conflict). Each branch carries **one or two commit dots**
  = work committed on the branch. Dots on `main` are **only branch-points and merges** (no
  filler). Branch-point and merge-point are distinct, so "you branched from something
  earlier" reads correctly; the day bands carry the time context, so **no `< 1 day`
  label**; one exemplar branch is annotated *branch* / *merge* at its feet. Commit to the
  git-graph metaphor. **No deploy or flag markers yet** — this is the base we layer flags
  onto in Part 2 and deploys onto in Session 3. **Image + title only** — no caption, no
  chips; the RealManage-flavor point and the "healthy signals" are all **spoken** (see
  *Say*).
- **On-slide text:** none beyond the slide title — the git graph carries the slide.
- **Say:** The TBD minimums: all changes integrate to `main`; if you branch, it comes
  from trunk, returns to trunk, and lives **less than a day**. Two acceptable workflows:
  commit directly to trunk (viable with strong tests + tight review), or a very
  short-lived branch with a small MR merged the same day. **RealManage flavor** (fold in
  here): for our GitLab + AWS world, short-lived branches with small MRs is the natural
  fit — it pairs with our existing MR review and Jira branch-naming; what changes is the
  *size* and *lifetime* of the branch, not the tooling. Read the picture: Monday runs two
  branches back to back; **Tuesday runs two at once** — that overlap window is the only
  interval in which they can conflict, and because the branches are small and short-lived
  the window is tiny, so conflicts are rare and small. Long branches *maximize* the
  overlap — the fix for conflicts isn't better merging, it's smaller, shorter branches.
  Healthy signals to name out loud (not on the slide): branch age in *hours*; no
  code-freeze; few branches open at once. Caveat that last one — *"< ~3 active branches"*
  is a small-team heuristic, not a law: it scales with how many people are mid-change. The
  real invariants are short **lifetime** (< 1 day) and low **WIP** (~one in-flight change
  per person); low branch count is just the symptom. One trap from the anti-patterns: a
  small branch that *waits* two days for review IS a long branch — wall-clock age is what
  counts, so pair small MRs with a ~4-hour review norm.

---

## Divider — Part 2 of 3: Decoupling Deploy from Release (Feature Flags)

*(README agenda §3.)*

- **Message:** Section marker: the technique that makes daily merges of unfinished work
  safe.
- **Visual:** Full-bleed section-title slide. Large "Feature Flags", a faint on/off toggle
  motif behind it.
- **On-slide text:** "Part 2 of 3 — Decoupling Deploy from Release"
- **Say:** One line: "Now the objection everyone raises — and the one idea that dissolves
  it." Move to slide 5.

---

## Slide 5 — The objection

- **Message:** Give the skeptic's voice the mic — "I can't merge, it's not done" is the
  #1 reason teams cling to long branches.
- **Visual:** Near-empty, high-negative-space slide (Session 1 slide-7 energy). A large
  quotation mark; a half-drawn UI dissolving into wireframe on one side = "not finished."
- **On-slide text:** "I can't merge to `main` — my feature isn't finished. It'll break
  things."
- **Say:** Let it sit. This is the honest, near-universal objection, and it *feels*
  correct — merging half-built UI or a broken path to `main` sounds reckless. Don't answer
  it yet. Deliberate tension; the next slide is the unlock.

---

## Slide 6 — Deploy ≠ Release

- **Message:** The unlock is one distinction: deploying code and releasing a feature are
  two different events.
- **Visual:** A single timeline with **two** separate marked events: *Deploy* (code lands
  in prod, flag OFF — a padlock) then, later, *Release* (flag flip — a switch / eye-open).
  Highlight the gap between them: "unfinished code lives here, safely."
- **On-slide text:** "Deploy = code in the environment (technical)" · "Release = users
  see it (business)" · "a flag is the switch"
- **Say:** Deploy is a technical act; release is a business decision. A **feature flag**
  is a runtime switch that lets unfinished code live in `main` and in production — turned
  **off** — until you choose to turn it on. So "not finished" stops being a reason to hold
  a branch: you merge it *dark*. This is the pivot the whole session turns on. Flag the
  payoff for later: because the release is now the flag flip (not the deploy), the flip is
  also the moment you *communicate* to clients, and the flip date is the release date.

---

## Slide 7 — How a flag changes the game

- **Message:** The same four pains from long branches invert once release = a flag flip,
  not a deploy.
- **Visual:** The README's two-column contrast as the whole slide. Left "Without flags"
  (*branch until done · big-bang merge · release = the merge · rollback = revert +
  redeploy*) vs right "With flags" (*merge daily, flag off · ships dark · release = flip ·
  "rollback" = flip back*). Arrows crossing the dashed divider.
- **On-slide text:** "Without → With" · "release = flip, not deploy"
- **Say:** Walk the contrast. Merge incomplete work daily → branches stay short → batches
  stay small. Reveal the feature exactly when the business wants (e.g. after the board
  meeting). If it misbehaves, flip it *off* instantly — no deploy. And dark launch: you
  can test in production safely before exposing anyone. This is the concrete cash-out of
  deploy ≠ release.

---

## Slide 8 — Flags aren't the only tool: branch by abstraction

- **Message:** A flag gates a *behavior* at a call site; for structural change you can't
  wrap in one switch, put a *seam* over what you're changing.
- **Visual:** A seam/interface bar with the OLD implementation live below it and a NEW one
  being built behind it; a switch sits on the seam. Sequence hint along the bottom: *build
  behind → flip the seam → delete the old path.*
- **On-slide text:** "Structural change → branch by abstraction" · "seam · build behind ·
  flip · delete old"
- **Say:** Feature flags are perfect for "is this feature on?" But some work is
  *structural* — swapping a data-access layer, replacing a dependency, a large refactor a
  single `if` can't cleanly wrap. There the technique is **branch by abstraction**:
  introduce an interface (a seam) over what you're changing, build the replacement behind
  it while the old path stays live, flip which implementation the seam resolves to (a
  one-line wiring change, often itself behind a flag), then delete the old path. Same goal
  as a flag — integrate on trunk before the work is done — different mechanism. This also
  previews the strangler-fig migration you'll see in Session 3.

---

## Slide 9 — A flag in TypeScript (framing → editor)

- **Message:** The minimal, dependency-free flag pattern — and the discipline that keeps
  flags from rotting.
- **Visual:** A **flag-gate diagram** showing what the flag does at runtime: `POST
  /violations` → an `isEnabled("violations.record")` check that forks two ways — **OFF ▶
  `501` Not Implemented** (dashed/muted: *dark — shipped, not released*) and **ON ▶ `201`
  Created** (teal: *live — the flag is flipped on*). Below it, three discipline chips —
  *default OFF (absence is safe) · config not code (per environment, never per branch) ·
  flags are temporary* —
  and a "→ `feature-flag.ts`" call-to-editor. Then switch to the actual file. The diagram
  carries the deploy-dark / `501` point, so there is **no caption**. (Rendered as three
  **sticky notes** below the diagram to read apart from it — see the deck.)
- **On-slide text:** the gate (OFF ▶ `501` dark / ON ▶ `201` live) · "default off · config
  not code · temporary" · "`feature-flag.ts` →"
- **Say:** Frame first, then open the file. Three discipline points to land at the editor:
  (1) **default OFF** — the flag's absence or misconfiguration must be safe (`DEFAULTS`
  are all `false`); (2) flag state is **config, not code** — externalized and keyed to
  *environment*, never hard-coded per *branch*. Because the value is config, not baked into
  the immutable artifact, the same bytes run flag-ON in qa and flag-OFF in prod — which is
  what lets you *test in qa while the change is already deployed dark to prod*, then flip
  prod to release (config-with-the-artifact + immutable artifacts, both CD minimums). How
  the flip lands depends on mechanism: the simple env-var pattern makes it a config-only
  redeploy of prod; a managed store (AppConfig / LaunchDarkly) flips at runtime, no deploy;
  (3) flags are **temporary**
  scaffolding — give each an owner, a creation date, and a removal condition in a flag
  inventory, add a CI **stale-flag check** that fails when one outlives its expiry, and
  make "delete the flag" the *last planned slice* of the feature. Show `recordViolation`
  returning `501` while dark. Close with graduating to AWS AppConfig / LaunchDarkly when
  you need runtime flips without a deploy, per-cohort targeting, or an audit trail — the
  `isEnabled(...)` API stays the same; only `loadFeatureFlags` changes.

---

## Divider — Part 3 of 3: Continuous Integration in Practice

*(README agenda §4.)*

- **Message:** Section marker: what real CI is, and the gates that make "always
  deployable" true.
- **Visual:** Full-bleed section-title slide. Large "Continuous Integration", a faint
  green-check / pipeline-stage motif behind it.
- **On-slide text:** "Part 3 of 3 — Continuous Integration in Practice"
- **Say:** One line: "The pipeline half comes next session; this is the *discipline* half
  — the part no tool gives you for free." Move to slide 10.

---

## Slide 10 — Continuous Integration Minimum Practices

- **Message:** CI is a discipline, not a tool — "we have GitLab CI" is not "we do CI."
- **Visual:** Title **"Continuous Integration Minimum Practices"**; the six CI minimums as a
  even 3×2 tile grid (echo Session 1's slide-6 "floor" motif) resting on a horizontal slab;
  below the slab, a single caption line — *Real CI = daily integration + automated
  verification + discipline*. Deliberately spare. The myth ("a tool isn't CI") and the migrations point
  are **spoken**, not drawn (see *Say*), to keep the slide uncluttered.
- **On-slide text:** title "Continuous Integration Minimum Practices" · six CI-minimum
  tiles · below the line: "Real CI = daily integration + automated verification + discipline"
- **Say:** Don't read all six aloud — let them land as a *floor*. The six: trunk-based
  development · integrate at least daily · tests before merge · tests on the merged result
  · **stop the line on red** (a red `main` is the team's #1 priority — no new feature work)
  · don't break delivered work (backward-compatible; expand/contract for data). Puncture
  the myth again (carried over from Session 1): a pipeline tool running on week-old
  branches is a *build server*, not CI — CI is only as continuous as the integration
  behind it. And **migrations are code too**: reviewed, tested against a *local* database,
  merged like any change; a migration that fails to apply is a red build that stops the
  line.

---

## Slide 11 — The gates that protect trunk (framing → editor)

- **Message:** The trunk is "always deployable" only if merging *proves* it — five minimum
  gates, and they must be honest.
- **Visual:** Keep git and pipeline as **distinct** concepts (don't repeat slide 4's
  conflation). A **feature branch** feeds a boxed **merge gate** whose five checks — *lint ·
  automated tests · coverage floor · IaC validate · security scan* — are drawn as lanes
  between a fork bar and a join bar (parallelism shown by the lanes; the box carries **no
  title** and "runs in parallel" is spoken — see *Say*). The picture shows a **failure
  state**:
  *automated tests* is red (a red ✗ in its lane, the lane turned red) and the **join bar
  turns red**. The line continues right toward the **`main`** box but is stopped by a
  no-entry **"merge blocked"** barrier — one red check makes the whole gate red, so the
  merge can't reach main (which stays deployable). Then "→ `ci-pipeline.gitlab-ci.yml`".
  The coverage caveat is **spoken**, not drawn (see *Say*).
- **On-slide text:** the five checks (automated tests shown red ✗) · "merge blocked" →
  `main` · "→ `ci-pipeline.gitlab-ci.yml`"
- **Say:** Frame, then open the YAML. These are the front half of the pipeline you build
  in Session 3: `validate` (lint + `sam validate`) → `test` (automated tests + a coverage
  threshold that *fails the job* below the floor) → `security` (`npm audit` + `cfn-lint`).
  **Unit tests are just the fast layer, not the whole story** — CI cares about the whole
  test pyramid; what changes is *where* each type runs, governed by speed: fast layers
  (unit + narrow integration) run pre-merge as the blocking gate; broader integration,
  contract, and end-to-end / acceptance tests run in *later* stages (slide 13's
  keep-feedback-fast staging). More test types in CI, not fewer — just not all pre-merge.
  **On the coverage floor** — it catches *erosion*, it is not a target to maximize.
  Coverage measures which lines *ran*, not whether the tests *assert* anything: 100%
  coverage with empty assertions verifies nothing. Goodhart's law — once the number is the
  target, that is exactly what you get, worst when one author (increasingly an AI) writes
  both the code and its tests. Guards: keep the floor *modest*; tests must pin *intent*
  (behavior contracts), confirmed *independently* (a human or a different agent than the
  one that wrote the code); and *mutation testing* is what actually measures test
  effectiveness — break the code and a real test fails. Read the diagram deliberately: the
  git side is the branch that wants to merge to `main` (the goal); the pipeline side is the
  boxed merge gate whose checks run *in parallel* (fork → join) so it stays fast, and it's
  all-or-nothing. The picture shows a *failure* — automated tests went red, turning that
  check and the join bar red; one red check makes the whole gate red, and the path onward to
  `main` is stopped at the barrier, so the bad change never reaches the trunk (main stays
  deployable). Flip it green and, the rest being green, it would merge. What makes it real CI
  (the note at the bottom of the file): it runs on every branch, it's fast, a red result
  *blocks the merge*, and the branches feeding it are short-lived. Keep this file open next
  to Session 3's.

---

## Slide 12 — Brownfield: starting with little coverage (framing → editor)

- **Message:** You don't backfill the whole monolith — you characterize what you touch and
  let flags carry the rest.
- **Visual:** Framing: the four testing rules as a short ordered list, plus a
  "characterize → change → merge dark → verify → flip" mini-flow. Then "→ characterization
  test example (C#)". Small note: deliberate C#/.NET exception.
- **On-slide text:** "new code tested · don't backfill · characterize before you change ·
  manual test gates the flip"
- **Say:** The realistic case — a mostly-untested estate and no QA team. Four rules:
  (1) new code gets automated tests (cheap with AI, but they must pin real *intent*,
  independently confirmed); (2) existing untested code is "tested in production," not
  backfilled wholesale; (3) **characterize before you change** — pin current behavior,
  quirks included, so an unintended change fails loudly; (4) exploratory manual testing is
  permanent and gates the **flag flip** (release), not the merge. Walk the
  `LateFeeCalculator` example: characterize the legacy fee, add `late-fee-v2` as new,
  tested code behind a flag, merge it dark (still doing CI), hand-verify in `qa`, then
  flip. It's C# on purpose — the untested code lives in the .NET estate (one of the
  course's two deliberate exceptions to "examples are TypeScript").

---

## Slide 13 — Keep feedback fast

- **Message:** Slow CI is worthless — people route around gates that make them wait.
- **Visual:** A stopwatch over the pipeline: the pre-merge suite in "seconds" (green) vs a
  slow suite pushed to a "later stage" lane. Cache + parallel arrows.
- **On-slide text:** "Seconds pre-merge — or people route around it" · "parallelize ·
  cache · defer slow tests"
- **Say:** CI only works if it's fast enough that nobody is tempted to skip it. Keep the
  pre-merge suite in *seconds*: unit tests pre-merge, slower integration tests in a later
  stage, parallelize jobs, cache `node_modules` and the SAM build. It's why the example
  pipeline splits `validate` / `test` / `security` and pins images by digest. Fast
  feedback is also what makes "stop the line on red" bearable — a red trunk clears in
  minutes, not hours.

---

## Slide 14 — Now: decompose YOUR feature

- **Message:** The durable human skill — deciding the *slices* — is the workshop; slides
  stop here.
- **Visual:** A big feature block shattering into 6–10 small labeled slices, some tagged
  "flag / dark," ordered left → right; a "your turn" workshop cue (whiteboard photo).
  Signals: slides stop, hands-on starts (mirror Session 1's slide 8).
- **On-slide text:** "Break your feature into 6–10 daily slices" · "each: mergeable today?
  visible-now or behind-a-flag?"
- **Say:** Transition to the 25-minute workshop with the backlog feature they brought from
  Session 1 homework. **Individually (10 min):** 6–10 slices, each small, safe,
  independently shippable, hidden-if-needed, labeled *visible-now* vs *behind-a-flag*.
  **In pairs (10 min):** pressure-test — "could this merge to `main` today without breaking
  anything?" — split anything that fails; find the database change and express it as
  expand/contract. **Group (5 min):** share one; call out where a flag let you merge
  unfinished work. Emphasize the AI angle: an agent can write any one slice — designing the
  *decomposition* (the seam, the order, the flags, the expand/contract steps) is the
  "design the factory" work that stays yours. Greenfield *adds* behavior from a `501`
  scaffold; brownfield *replaces* it safely behind a seam with a named dual-write window —
  make them name both.

---

## Slide 15 — Wrap-up & homework

- **Message:** TBD + flags + real CI are one system; here's the week's practice and the
  bridge to the pipeline.
- **Visual:** Four takeaway tiles; a homework checklist; an arrow into a faded Session-3
  pipeline diagram (single path to prod).
- **On-slide text:** "Takeaways · homework · Session 3 = the pipeline"
- **Say:** Four takeaways: long-lived branches are big batches in disguise; TBD = all work
  to trunk, branches < 1 day; feature flags decouple deploy from release, so "unfinished"
  is no longer a reason to hold a branch; real CI is daily integration + fast gates +
  stop-the-line, none of which a tool gives you for free. Homework: (1) take one real
  change this week through the full loop — short branch → small MR → merge to `main` the
  same day; (2) if your service has no flag mechanism, sketch where one belongs, starting
  from the `feature-flag.ts` pattern; (3) list the quality gates your service runs today
  vs. the minimum set — that diff is Session 3 work. Preview Session 3: the pipeline that
  turns "trunk is always deployable" into "any change reaches prod on demand" — single path
  to production, immutable artifacts promoted across environments, recovery on AWS (fail
  forward, rollback for emergencies), and your team's migration plan.
