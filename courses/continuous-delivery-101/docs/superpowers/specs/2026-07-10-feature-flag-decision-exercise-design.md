# Design: "Flag it, Abstract it, or Ship it?" — Session 2 Feature-Flag Decision Exercise

**Date:** 2026-07-10
**Course:** Continuous Delivery 101 · Session 2 (Trunk-Based Development & CI)
**Status:** Approved

## Overview

Replace the *live* Session 2 workshop with a feature-flag decision exercise built on
the [beyond.minimumcd.org feature-flag decision tree](https://beyond.minimumcd.org/docs/migrate-to-cd/optimize/feature-flags/).
Learners are given realistic RealManage change scenarios and, for each, walk a
three-gate decision tree to a verdict: **feature flag**, **branch by abstraction**,
or **just ship it**. The existing `decompose-a-branch.md` exercise is retained but
demoted from the live workshop to homework.

## Goal

Teach the durable human skill of choosing the *cheapest mechanism that still keeps
trunk deployable*. Over-reaching for a flag creates debt; under-reaching keeps
branches long. An AI agent can implement any of the three mechanisms — deciding
*which* is the judgment that stays with the engineer. This is the hands-on payoff of
Session 2 §3.2 ("flags aren't the only technique").

## Locked decisions

1. **Format:** facilitator markdown guide **plus** an interactive `.tool.html`
   companion (matches the `value-stream-map` / `current-state-assessment` pattern).
2. **Verdicts:** exactly three — feature flag / branch by abstraction / just ship it.
   Flag *flavors* (release, kill-switch, permission, experiment) are taught in the
   answer-key rationale, not as separate verdicts.
3. **Old exercise:** `decompose-a-branch.md` stays in the repo, demoted to homework.
   The live §5 workshop becomes this new exercise. No cross-references are orphaned.
4. **Tool behavior:** decision-tree *walker* + scorecard — pick a scenario, answer
   the three gates, land on a verdict, compare to the intended answer with rationale,
   tally a scorecard, Copy-as-Markdown.

## The three-gate decision tree

Adapted from beyond.minimumcd.org, collapsed to the three verdicts:

- **Gate 1 — Ship-it test:** Can it merge to trunk today, *finished*,
  backward-compatible, exposing nothing half-built or risky? → **Just ship it.**
  A flag "just in case" is pure overhead and future debt.
- **Gate 2 — Structural vs behavioral:** Is what is not-ready a *structural swap* —
  replacing an implementation, dependency, or data-access layer where old and new
  cannot both live at one call site, with no intended user-visible change? →
  **Branch by abstraction** (seam → build behind it → flip → delete).
- **Gate 3 — Otherwise** it is user-facing behavior you must integrate before it is
  ready to reveal, or you need staged rollout / a kill switch / targeting →
  **Feature flag.** The answer key names the flavor: release (temporary),
  kill-switch (permanent), permission/entitlement (permanent), experiment.

Key nuance: **branch by abstraction and flags compose** — a seam's cutover can
itself be flag-gated.

## Scenario set

All grounded in the course domain (HOA Violations API on AWS; established .NET
Framework APIs; strangler-fig). `★` = used in the live workshop; others are tool
extras / self-study.

| # | Scenario | Verdict | Lesson / trap |
| - | -------- | ------- | ------------- |
| 1 ★ | Fix a typo in a validation error message | Just ship it | YAGNI; a flag is overhead + debt |
| 2 ★ | New `GET /violations/history`, revealed after the board meeting | Flag (release) | Textbook decouple deploy/release — worked example |
| 3 ★ | Replace hand-rolled DynamoDB access with a repository layer | Branch by abstraction | Structural swap; reflexive "flag it" is wrong |
| 4 ★ | Late-fee rewrite that is "done" but untrusted vs real data | Flag (dark launch/shadow) | Tests-pass ≠ trusted; money logic shadows |
| 5 ★ | Expensive report melts the DB at peak; need instant off-switch | Flag (kill-switch, permanent) | Flags are not all temporary |
| 6 | Premium analytics dashboard for top-tier communities only | Flag (permission, permanent) | Entitlement ≠ release flag ≠ config |
| 7 ★ | Hotfix: NRE when `propertyId` missing | Just ship it | Do not flag a hotfix (contrast #4) |
| 8 ★ | Swap SNS → managed email, gradual cutover + instant revert | BbA + Flag | The "pick one" trap — compose both (capstone) |
| 9 | Rename `level` → structured `escalationHistory` on live data | None cleanly → expand/contract (+ flag the read cutover) | Model boundary: data migrations use expand/contract |
| 10 | Internal ops CLI to backfill data, not in any user path | Just ship it | Internal/manual tools rarely need flags |

Live set: 1, 2, 3, 4, 5, 7, 8. Tool extras: 6, 9, 10.

## Facilitator markdown (`feature-flag-decision.md`)

Mirrors `decompose-a-branch.md` structure and tone. ~25 minutes:

- **Header** (When / Format / Goal) + intro on the skill and the AI-durability framing.
- **Part 0 — The decision tree** (~3 min): the three gates + verdicts; define
  deploy≠release, seam, YAGNI, temporary-vs-permanent flags.
- **Part 1 — Walk one together** (~5 min): scenario #2, reasoning aloud.
- **Part 2 — Classify** (individual/pairs, ~10 min): the live set via the tool or
  cards; each = verdict + one-line rationale + name the trap.
- **Part 3 — Debate the contested** (~7 min): #4 (ship vs flag), #5 (permanent
  flag), #8 (compose). Facilitator uses the key.
- **What "good" looks like:** reasoning quality — naming *why not the other two*,
  spotting permanent-vs-temporary, spotting compose. Not the label.
- **Answer key:** every scenario → verdict + rationale + why-not-others + nuance.
- **Output:** copyable classifications + one flag the learner will add/remove in
  their own service this week.

## Interactive tool (`feature-flag-decision.tool.html`)

Self-contained single file, cd101 palette (reuse `value-stream-map.tool.html` CSS
variables), wired identically to the existing tools. Decision-tree walker + scorecard:

- Scenario picker (all ~10) → step through the three gates as clickable choices →
  land on a verdict.
- Compare to intended verdict: MATCH / MISMATCH, then reveal rationale +
  why-not-others + nuance.
- Scorecard: X/N matched, per-scenario status.
- Reveal-answer-key toggle (facilitator / self-study).
- Copy-as-Markdown (course convention) — exports classifications, verdicts, notes.
- Load-worked-example / Start-fresh buttons. No external dependencies; works offline.

## Wiring changes (decompose-a-branch stays)

- **`sessions/session-2/README.md`:** §5 retitled "Workshop: Flag it, Abstract it,
  or Ship it?" pointing at the new exercise (+ tool); §6 Homework gains
  decompose-a-branch; objectives keep "decompose…" (now homework) and add a
  mechanism-choice objective; resources list gains the new exercise.
- **Course root `README.md`:** structure tree + Session 2 objective list gain the
  new exercise (decompose stays).
- **Slides:** repoint the Session 2 *workshop* slide to the new exercise (light
  touch; broader slide walkthrough is a separate effort).
- **Glossary:** verify `branch by abstraction` is defined; add if missing.
- No orphaned references — every existing decompose link stays valid.

## Deliverables

- **New:** `sessions/session-2/exercises/feature-flag-decision.md`,
  `sessions/session-2/exercises/feature-flag-decision.tool.html`.
- **Modified:** Session 2 README, course README, Session 2 workshop slide,
  glossary (if needed).
- **Untouched:** `decompose-a-branch.md` and all its cross-references.

## Out of scope

- The broader Session 2 slide-by-slide walkthrough (separate ongoing effort).
- Rewriting `decompose-a-branch.md` content.

## Verification

- markdownlint passes on new/changed markdown (except MD013).
- The `.tool.html` opens standalone, walks all scenarios, and Copy-as-Markdown
  produces valid output.
- All internal links resolve; the site build succeeds.
