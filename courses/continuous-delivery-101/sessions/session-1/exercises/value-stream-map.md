# Exercise: Map Your Value Stream

**When:** Session 1 (in-session workshop)
**Format:** Team exercise, ~25 minutes
**Goal:** Draw the path a change takes from *idea* to *running in production*, measure where the time actually goes, and let the map — not a hunch — name the one constraint most worth fixing.

This is the first step of Phase 0 in the [migration checklist](../../../resources/migration-checklist.md): before you score yourself against the minimums or plan a migration, see your *current* flow honestly. Almost every team discovers the same thing — the time is spent **waiting**, not **working** — and the biggest wait is the constraint the rest of the course exists to remove.

> **Platform-agnostic on purpose.** A value stream map doesn't care whether you ship a TypeScript Lambda or a .NET app to an IIS pool. It maps *your* flow, in *your* stack, with *your* numbers. If you run a long-lived monolith, this is where its real delivery pain shows up in figures you can defend.

**Build it in the browser.** Use the [Value Stream Mapper](./value-stream-map.tool.html) to enter each step, reorder them, and read the totals live — it computes lead time, flow efficiency, and rolled %C/A, and stars your binding constraint as you type. It works offline and your map autosaves in your browser.

---

## The three numbers

For every step a change passes through, capture three things:

- **Process time** — time someone is *actively working* the change at that step (writing the code, reviewing the MR, running the deploy).
- **Wait time** — time the change sits *idle*: in a backlog, in a review queue, blocked on an environment, waiting for the release window.
- **%C/A (percent complete & accurate)** — of the work that arrives at this step, what share is usable *as-is*, without being sent back for rework? A code-review step where one MR in four bounces back is 75%.

Two derived numbers fall out of those:

- **Total lead time** = sum of all process time **+** all wait time. (This is the DORA *lead time for changes* you'll measure in [Part 3 of the assessment](../../session-3/exercises/current-state-assessment.md).)
- **Flow efficiency** = total process time ÷ total lead time × 100. The share of lead time that is actual work. Most teams are shocked how low it is.

---

## Part 1 — List the steps, working backward (team, 5 min)

Start at **production** and work *backward* to the request. Beginning at delivery keeps you honest: teams that start at "idea" reliably forget the deploy-side steps where much of the waiting hides.

Name every step a change actually passes through. A weekly-release team's stream usually looks something like:

`intake & prioritize → refine & size → develop → code review → wait for the release window → release-day deploy → verify in prod`

Write *your* steps. Don't model the process you wish you had — model the one a change went through last week.

> **No separate QA step here, by design.** At RealManage the delivering team owns quality; there is no QA team to hand off to. If your stream has a "throw it over to QA" wait, that *is* a finding — write it down.

---

## Part 2 — Put numbers on each step (team, 8 min)

For each step, fill in the three numbers. Estimate from memory now; you'll replace estimates with measured figures in the homework assessment. Use one unit throughout (working hours is easiest; 1 day = ~8 working hours).

| # | Step (idea → prod) | Process time | Wait time | %C/A |
| - | ------------------ | ------------ | --------- | ---- |
| 1 | | | | |
| 2 | | | | |
| … | | | | |

Rules of thumb while you fill it in:

- **When in doubt, it's wait.** People over-count process time because waiting is invisible. If the change was *sitting*, it's wait.
- **A queue is a step.** "Waiting for a reviewer" and "waiting for the Thursday release window" are real steps with real wait time — give them their own rows.
- **Rework shows up twice.** A low %C/A at a step means work loops back — count the *extra* trip's time too.
- **Note the batch size at the deploy step.** How many changes ride one deploy? Write that number next to the deploy / release-window step. A big number there is itself a constraint — it's usually *why* the deploy's %C/A is low: many changes at once, so a break is hard to isolate.

---

## Part 3 — Do the math (team, 4 min)

Add it up:

- **Total process time** = sum of the process column.
- **Total wait time** = sum of the wait column.
- **Total lead time** = process + wait.
- **Flow efficiency** = total process ÷ total lead × 100.

Optionally, multiply all the %C/A figures together for **rolled %C/A** — the share of changes that flow all the way through with *no* rework. It's usually sobering.

---

## Part 4 — Find the constraint (team, 8 min)

Mark two kinds of step on your map (a star ⭐ or a "kaizen burst" scribble works):

1. The step with the **largest wait time** — where changes sit longest.
2. The step with the **lowest %C/A** — where the most rework is created.

Often they point at the same thing. That thing is your **binding constraint** — the one place where fixing it would move lead time most. Fixing anything else first just moves the queue.

> **When an agent writes the code, watch this map move.** *Develop* is the step AI collapses first — an agent drafts in minutes what took a day, so its process time falls toward zero. But that doesn't shorten lead time if the change still waits two days for review and four for the release window. It just shifts the constraint *downstream*, onto the review queue and the deploy window. Re-map once AI is in your loop: the biggest wait will have moved, and your fix should move with it. See [CD when AI writes the code](../../../resources/ai-assisted-delivery.md).

Then answer, as a team:

1. What is the single biggest wait, in hours? What causes it?
2. Where is the most rework created (lowest %C/A)? Why does work bounce there?
3. If you removed *only* that one constraint next month, what would lead time become?
4. Who owns the fix, and what does "fixed" look like?

> **Resist naming the constraint before you've measured.** Surfacing it from the numbers *is* the exercise. For teams coming off weekly releases the biggest wait is almost always "waiting for the release window" — but confirm that from your own map rather than assuming it.

---

## A worked example — a weekly-release team

A team ships a small change through this stream. Times are in working hours (1 day = 8 h):

| # | Step (idea → prod) | Process time | Wait time | %C/A |
| - | ------------------ | -----------: | --------: | ---: |
| 1 | Intake & prioritize | 0.5 h | 24 h (3 d) | 90% |
| 2 | Refine & size | 2 h | 16 h (2 d) | 85% |
| 3 | Develop on a feature branch | 12 h (1.5 d) | 16 h (2 d) | 90% |
| 4 | Code review | 1 h | 16 h (2 d) | 75% |
| 5 | Wait for the weekly release window | — | 32 h (4 d) | — |
| 6 | Release-day batch deploy (~20 changes) | 3 h | — | 70% |
| 7 | Verify in production | 1 h | 4 h | 95% |

The math:

- **Total process time** = 0.5 + 2 + 12 + 1 + 3 + 1 = **19.5 h**
- **Total wait time** = 24 + 16 + 16 + 16 + 32 + 4 = **108 h**
- **Total lead time** = 19.5 + 108 = **127.5 h ≈ 16 working days (~3 calendar weeks)**
- **Flow efficiency** = 19.5 ÷ 127.5 = **~15%**
- **Rolled %C/A** = 0.90 × 0.85 × 0.90 × 0.75 × 0.70 × 0.95 ≈ **34%** — only a third of changes flow through with no rework.

> **This trace is one clean pass — read 127.5 h as a floor, not an average.** It follows a change that bounced back *zero* times. But a 34% rolled %C/A means only about a third of changes are that lucky; the *typical* change loops back at least once (rework shows up twice — Part 2), adding another trip through develop and review. The real average runs higher. The map's job isn't a precise mean — it's to expose the waiting, and even the optimistic floor is ~85% wait.

What the map reveals:

- **Biggest wait: the weekly release window (32 h)** ⭐ — pure batching delay. A change that's *done* on Monday waits for Thursday. This is the constraint, and it's not a tooling problem — it's a batch-size decision.
- **Lowest %C/A: the release-day deploy (70%)** ⭐ — nearly one deploy in three needs a hotfix. That's the big-batch failure rate made visible: all ~20 changes ship at once, so something breaks and is hard to isolate.
- The fix for *both* stars is the same: **ship smaller batches, more often.** Shrinking the batch removes the window wait *and* raises deploy accuracy because each deploy carries one diagnosable change. That is the whole thesis of this course, now sitting in your own numbers.

Notice what 15% flow efficiency means: the change took three weeks, but only ~2.5 days of that was anyone working on it. You don't fix a number like that by working faster — you fix it by removing the waiting.

> **This example is a team already close to trunk-based** — its branches live two days. A long-lived-branch monolith would show *weeks* in step 3 (develop, plus its wait), pushing flow efficiency to ~10% or worse. If that's you, the map will look worse before the course makes it better — which is exactly why you measure first.

---

## Output

By the end you should have:

- A value stream map of your real flow, idea → prod, with process time, wait time, and %C/A on every step
- Three headline numbers: total lead time, flow efficiency, and (optionally) rolled %C/A
- Two starred steps — the biggest wait and the lowest %C/A — and the single binding constraint they point to, with an owner

Carry this into the homework [Current-State Assessment](../../session-3/exercises/current-state-assessment.md), where you'll back these estimates with measured numbers and score yourself against the minimums, and into your [migration plan](../../../resources/migration-checklist.md). Re-map after the course and watch the flow efficiency climb.
