---
order: 45
---
# Testing and CD — Moving Forward When the Code Isn't Tested Yet

The course frames the definition of deployable as automated checks the pipeline runs. That assumes the tests exist. Across much of RealManage's established .NET estate they do not: the code is large, long-lived, and verified today by **hand**. This guide is how you adopt CD from there — without a year-long "add tests to everything" project, and without giving up the practice.

## The gap

CI's minimums ask for automated tests before and after every merge (minimums #3 and #4). A team whose verification is manual does not meet that bar yet — and pretending otherwise helps no one. But "we have no tests" is not a reason to stay on weekly, big-batch releases. It is a starting position with a concrete path forward. The path has four rules.

## The four rules

1. **New code gets automated tests.** Anything you write now ships with tests that pin its intended behavior — they are part of the definition of deployable for new work. The old objection ("no time to write tests") has largely evaporated: **AI makes drafting tests cheap.** The caveat is real, though — cheap to *write* is not the same as *correct*. An agent that writes the code and its tests in one breath can produce tests that assert whatever the code already does and verify nothing. A test still has to pin real *intent*, and something independent — a human or a separate agent — has to confirm it does, not just echo the code's current output. See [CD when AI writes the code](ai-assisted-delivery.md).
2. **Existing untested code is proven in production.** Years of real traffic are a form of verification. Do **not** launch a project to backfill tests across the whole estate — it is rarely the best use of effort, and it competes with delivery. Leave working, untested code alone until you have a reason to touch it.
3. **When you change legacy code, characterize it first.** Before you modify an untested method, write a **characterization test** — a test that pins its *current* behavior exactly as it is, quirks included. It is not a statement of what the code *should* do; it is a net that catches any change you didn't intend. Now you can refactor or extend safely. This is how the automated suite grows: at the seams where change actually happens, paid for by the change that needed it — worked end to end in [Characterization Test in Practice](../sessions/session-2/examples/characterization-test/README.md). Until that net exists, manual regression testing is the bridge.
4. **Exploratory testing is permanent.** Automating regression frees humans to do the testing only humans can: poking at the change, trying the unexpected, judging whether it *feels* right. This never goes away, and it is not debt. Under CD it gates the **release** — the flag flip — not the merge.

## How feature flags let you do CI while you still test by hand

CI asks you to integrate to trunk every day. The honest objection from a manual-testing team is: *"I can't merge — it isn't tested yet, and testing it by hand takes time."* Hold a branch open until the manual test is done and you are back to long-lived branches, big batches, and no CI.

The feature flag dissolves the objection. Merge the change **turned off**. Now it is *integrated* — on trunk, building, breaking nothing, because it is dark — but not *released*. The manual test then happens on the already-merged, already-deployed code in `qa` (or in production, still dark), and the **flag flip** becomes the moment manual verification guards.

That is the whole move: **manual testing shifts from a gate before the merge to a check before the release.**

```text
Manual test as a PRE-MERGE gate          Manual test as a PRE-RELEASE check
  branch held open until tested            merge dark (flag off) — CI satisfied
  long-lived branch, big batch             pipeline deploys it, still dark
  no CI                                     test the deployed code by hand
  release = the merge                       flip the flag when verified = release
```

You are doing CI immediately — integrating daily — and you *earn* the automated-test minimums over time as rule 3 grows your coverage where it counts.

## Where manual verification sits

Two kinds of manual testing, treated differently:

- **Manual regression** — re-checking that old behavior still works. This is the debt. Every time rule 3 adds characterization tests, a slice of it becomes automated and the manual burden shrinks. Aim it down.
- **Manual exploratory** — deliberate, time-boxed investigation of a change before its flag flips. This is permanent and valuable. Make it explicit: who does it, against which environment, and what they are looking for.

Neither is a *handoff*. RealManage has no separate QA team, and this does not reintroduce one: the delivering engineer owns the verification, the same way they own the change. The value-stream lesson still holds — a "throw it over to QA" *wait* is waste. An engineer verifying their own change before flipping its flag is not a handoff; it is owning quality.

## The trajectory

You are not choosing between "manual forever" and "automate everything now." You are moving along a line: new code tested from the start, legacy code characterized as you touch it, manual regression automated down release by release, and exploratory testing kept as the permanent human edge. Feature flags are what let you travel that line while already practicing CI — integrating continuously today, on the coverage you earn tomorrow.
