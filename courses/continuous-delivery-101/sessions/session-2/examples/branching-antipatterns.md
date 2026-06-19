# Worked Example: Branching Anti-Patterns (and the Trunk-Based Fix)

The branching habits that feel safe under weekly releases are exactly what block CD. Here are the common ones, why they hurt, and the trunk-based alternative.

## Anti-pattern 1: The long-lived feature branch

```text
main    ──●────────────────────────────────────●──  (2 weeks pass)
           \                                    /
feature     ●──●──●──●──●──●──●──●──●──●──●──●──●   <- merged big-bang
```

**Why it hurts:** two weeks of drift, a brutal merge, and a two-week batch hitting prod at once. If it breaks, good luck isolating which of 13 commits did it.

**The fix:** decompose the feature; merge a thin slice to `main` daily, behind a flag if it's user-visible and unfinished.

```text
main  ──●──●──●──●──●──●──●──●──●──●──●──●──●──  (each ● is a small MR, same day)
        f  f  f  f  f  f  f  f        ^ flag flipped on here = release
```

## Anti-pattern 2: "Merge when it's done"

**The habit:** the branch stays open until the entire feature is complete and polished.

**Why it hurts:** "done" is the largest possible batch. It guarantees long branches and big-bang merges.

**The fix:** merge when each *slice* is correct and safe, not when the *feature* is finished. Unfinished but correct code lives in `main` behind an off flag. See [`feature-flag.ts`](./feature-flag.ts).

## Anti-pattern 3: The shared long-running `develop` branch

**The habit:** GitFlow-style — everything integrates to `develop`, which merges to `main` only at release time.

**Why it hurts:** `develop` *is* a second trunk that drifts from `main`. Real integration (and real testing of what ships) is deferred to release day — the opposite of CI.

**The fix:** one trunk. Integrate to `main` directly. If you need a place to stabilize a release, use a *short-lived* release branch cut from `main`, not a permanent `develop`.

## Anti-pattern 4: Branch-per-environment (`dev`, `qa`, `prod` branches)

**The habit:** promote code by merging `dev` → `qa` → `prod` branches.

**Why it hurts:** the branches drift, so each environment runs *different code*. "Works in qa, breaks in prod" becomes routine. It also violates immutable artifacts — you're rebuilding per environment.

**The fix:** **one** branch (`main`) produces **one** immutable artifact, *promoted* (not rebuilt) through environments by the pipeline. Environments differ by *config*, not by *code*. (This is Session 3.)

## Anti-pattern 5: Code freeze / stabilization periods

**The habit:** "no merges this week, we're stabilizing for the release."

**Why it hurts:** a freeze is an admission that the trunk isn't trustworthy. It blocks everyone, batches up work behind the freeze, and makes the *next* release even bigger.

**The fix:** keep the trunk releasable *continuously* via tests and flags. If `main` is always green and deployable, there's nothing to freeze.

## Anti-pattern 6: Long-open MR waiting for review

**The habit:** the branch is small, but the MR sits for two days waiting for a reviewer.

**Why it hurts:** a short-lived branch that *waits* is a long-lived branch. The clock that matters is wall-clock age, not your intent.

**The fix:** small MRs (easy to review fast) + a team norm of reviewing within ~4 hours. Small batches and quick review reinforce each other.

## The through-line

Every anti-pattern above produces the same thing: **a bigger batch and more drift.** Trunk-based development is the discipline of refusing both — keep changes small, keep them flowing to one trunk, and use *engineering* (tests, flags) instead of *process* (branches, freezes) to stay safe.

## Related

- [Decompose a Long-Lived Branch](../../../exercises/decompose-a-branch.md)
- [Feature flags in TypeScript](./feature-flag.ts)
- [Minimums Reference — Trunk-Based Development](../../../resources/minimums-reference.md)
