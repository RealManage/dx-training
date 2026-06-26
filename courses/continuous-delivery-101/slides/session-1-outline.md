# Session 1 — Slide Outline (build in your tool)

**How to use this:** one entry per slide. *Message* is the single idea the slide must
land. *Visual* is an image-heavy suggestion you build in PowerPoint / Canva / Gamma —
the slide should be mostly that image. *On-slide text* is the few words that go on the
slide (keep it to this). *Say* is your talking track — it does **not** go on the slide.

The run-sheet is [`../sessions/session-1/README.md`](../sessions/session-1/README.md);
this only covers the ~8 present-the-concept moments. The other 60–70% (the value stream
mapping workshop, the assessment) is hands-on — no slides.

Target: **~8 slides** for a 2-hour session. If a slide needs more than one sentence of
on-slide text, it's doing too much — push it into *Say* or split it.

---

## Slide 1 — Title

- **Message:** This is about *how* we deliver, not which platform we're on.
- **Visual:** Full-bleed hero — a flowing assembly line / conveyor, or a calm container
  ship leaving port. Something that reads "steady, continuous flow," not "big event."
- **On-slide text:** "Continuous Delivery 101" · "Session 1 — Why CD & the Minimums"
- **Say:** Set the frame. By the end they can: explain why big batches are risky, tell
  CD from Continuous Deployment, recite the minimums and *why* each exists, and map our
  delivery value stream to find its biggest constraint. Tease the one question they'll
  hold all session (Slide 7).

---

## Slide 2 — The roots: from the factory floor to the trunk

- **Message:** Almost none of this was invented in software — the values and principles
  come from Lean manufacturing and the quality movement, relearned as Continuous Delivery.
- **Visual:** A horizontal **timeline** (the hero), each milestone shown as three fixed
  tiers — *idea · source · authors*: 1950s Lean (Toyota; Ohno · Deming) → 1984 Theory of
  Constraints (*The Goal*; Goldratt) → 1999 Continuous Integration (Extreme Programming;
  Beck) → 2010 Continuous Delivery (ThoughtWorks; Humble · Farley) → 2018 Hard evidence
  (DORA · *Accelerate*; Forsgren · Humble · Kim). **On a click**, five **principle chips** drop in
  below — *small batches · fast feedback · built-in quality · deploy/release separation ·
  continuous improvement* — the values the history distills to. Roots stay in the notes.
- **On-slide text:** "Principles with deep roots" · the timeline (1950s → 2018) ·
  "distills to five principles we live by"
- **Say:** Almost nothing here is new. The values come from Lean manufacturing — the
  Toyota Production System, refined on factory floors since the 1950s — and the postwar
  quality movement; software relearned them the hard way and wrote them down as CD. Three
  beliefs underneath: (1) *undeployed code is inventory, not value* — Ohno: inventory is
  waste, big batches only *feel* efficient; (2) *speed and safety rise together, not a
  trade-off* — DORA/Accelerate found the teams that deploy most often also fail least;
  (3) *you can't inspect quality in — you build it in* — Deming; at RealManage that's
  literal, there's no QA team, so the delivering team owns quality and the pipeline
  encodes it. The five principles trace to roots — small batches (Ohno; Reinertsen), fast
  feedback (Lean flow), built-in quality (Deming; Toyota *jidoka*), deploy/release
  separation (decouple technical risk from business risk — feature flags), continuous
  improvement (*Kaizen*; Goldratt's Theory of Constraints). Two factory images to use: the **andon
  cord** — any worker stops the line on a defect = our "stop the line on red"; and
  **genchi genbutsu**, "go and see" = the value-stream map you'll build today. When a
  practice seems arbitrary, trace it back: practice → principle → belief. Small batches is
  first on purpose — that's the next two slides.

---

## Slide 3 — The big-batch trap

- **Message:** Deploying rarely is a self-reinforcing loop that *manufactures* risk.
- **Visual:** A vicious-cycle ring — 4 nodes (*deploy rarely → deploys are large →
  large is risky → fear deploys less*) with the arrow curving back to the start. Make
  the loop the whole slide. Bold, circular, no prose.
- **On-slide text:** "Deploy rarely → large → risky → deploy even less ↺"
- **Say:** A weekly release isn't one change, it's a *week* of changes: diagnosis hell
  (which of 30 broke prod?), all-or-nothing rollback, release-day anxiety. Be fair to
  the weekly cadence — it gave a predictable window, a comms batching point, a forcing
  function for "is it ready?" CD must *replace* each of those, not just delete them. The
  escape is counterintuitive: deploy *more* often, in *smaller* pieces.

---

## Slide 4 — Batch size is the master variable

- **Message:** Almost every CD benefit traces back to one lever: shrink the batch.
- **Visual:** Two side-by-side panels, one per hand-off, each a "shrink the batch"
  contrast. Left — *integration batch* (branch → trunk): a tall diff/review page shrinks
  to a stack of small reviews — same work, many small commits (review size; conflicts on
  overlap). Right — *deploy batch* (trunk → prod): a boulder shrinks to pebbles
  (isolation · rollback · release). A dashed divider marks the two categories.
- **On-slide text:** "Batch size is the master variable" · "One lever, two hand-offs —
  shrink the batch at both"
- **Say:** "Batch" lives at two independent hand-offs — don't conflate them. *Integration
  batch* (branch → trunk) drives review size; conflicts only follow *if* work overlaps a
  moving trunk, so a team can rarely see conflicts yet still drown in oversized reviews.
  *Deploy batch* (trunk → prod) drives isolation, rollback blast radius, and release risk.
  Walk 2–3 contrasts max, leading with review burden (the reliable cost); conflicts are
  conditional. If the room is AI-forward, land the AI angle: a human self-limits by friction
  — 1,200 lines is a day's grind; an agent emits them before lunch, so small batches become a
  *throttle you impose*, not a habit — and review burden is where that bites first. Then the
  evidence: DORA shows frequent small
  deploys give higher throughput *and* stability (survey data — correlational, but
  strong and replicated). Make it actionable: DORA's official **Quick Check**
  ([dora.dev/quickcheck](https://dora.dev/quickcheck/?v=2025)) benchmarks your four
  metrics against this year's industry cohorts in ~2 minutes — re-run it after adopting
  these practices to see the climb.

---

## Slide 5 — CD ≠ Continuous Deployment

- **Message:** Both keep changes deployable; the difference is a human "go." We target
  Delivery.
- **Visual:** Two parallel pipeline rails. Top rail ends at a **gate / turnstile with a
  person** ("go") → prod = *Continuous Delivery*. Bottom rail runs straight through with
  a **lightning/auto icon** → prod = *Continuous Deployment*. The gate-vs-no-gate is the
  whole point — make it visual.
- **On-slide text:** "Delivery = human decides *when*  (our target)" · "Deployment =
  auto on green  (optional, later)"
- **Say:** This confusion derails adoption. Continuous Delivery does *not* mean losing
  control — you *gain* the ability to deploy on your schedule instead of a weekly window,
  and releasing a feature to users stays a separate, deliberate call. Deployment is a
  later, per-service choice once you trust the pipeline. You must reach Delivery first.

---

## Slide 6 — The minimums are the floor, not the ceiling

- **Message:** CD has a defined *minimum* bar — not stretch goals, the floor.
- **Visual:** A literal floor / foundation slab with the practices as bricks or icon
  tiles on it (6 CI + 9 CD). Or two tidy icon grids labeled "CI" and "CD." The feeling:
  solid baseline, not a wish-list.
- **On-slide text:** "MinimumCD — the floor" · "6 CI practices · 9 CD practices"
- **Say:** Don't read 15 items aloud — let them land as "a floor we either meet or we
  don't." Puncture the big myth: "we have GitLab CI, so we do CI" — *no*. A pipeline
  tool isn't the discipline; if branches live a week, you have a build server, not CI.
  Point them to the full reference for the list.

---

## Slide 7 — The litmus test

- **Message:** One question reframes everything — hold it all course.
- **Visual:** High-impact, near-empty slide. A stopwatch or a single clock on a bold
  background, the question large and centered. This is the slide that should *feel*
  different — lots of negative space.
- **On-slide text:** "Could a change committed **now** reach prod **today** — no
  re-typed commands, no readiness meeting?"
- **Say:** This is the sentence that reframes the whole course. Most teams answer "no" —
  and the reasons *why* are their migration backlog. Let it sit. Don't answer it for
  them.

---

## Slide 8 — Now: map YOUR value stream

- **Message:** Before fixing anything, see where the time actually goes — map your flow
  idea → prod and let the numbers name the constraint.
- **Visual:** A horizontal flow of step boxes idea → prod with thin "work" segments and
  fat "wait" gaps between them, or a "your turn" call-to-action with a workshop photo
  (people at a whiteboard). Signals: slides stop, hands-on starts.
- **On-slide text:** "Map idea → prod" · "Process vs wait vs %C/A" · "Name your #1
  constraint"
- **Say:** Transition to the workshop. For every step capture three numbers — process
  time, wait time, %C/A — then derive lead time and flow efficiency. Most teams are
  shocked how low flow efficiency is (the worked example lands near *15%*). The biggest
  wait is almost always the release window, not slow typing — confirm it from your own
  numbers. Homework: back the map with the assessment (real numbers + minimums score),
  read the minimums reference, bring one backlog feature for Session 2.
