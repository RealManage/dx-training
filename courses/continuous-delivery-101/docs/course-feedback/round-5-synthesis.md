# Continuous Delivery 101 — Round 5 Synthesis (Spot-Check)

**Date:** 2026-06-22
**Inputs:** Targeted re-reviews from the three personas who carried open items into round 4 —
`student-3-priya-eng-leader-review-5.md`, `student-2-dana-dotnet-monolith-review-5.md`,
`student-8-tex-ai-champion-review-4.md`.
**Method:** Each persona re-read its full review history and the *current committed* course
(HEAD `c9aa30f`), then pressure-tested the round-4 fixes (R4-1…R4-7) — hunting regressions and
verifying that what round 4 *claimed* to fix is actually fixed in the artifacts, not just the prose.

## Verdict: pass (adoption-ready); the round-4 precision dip is recovered

| Persona | R1 | R2 | R3 | R4 | **R5** | Δ vs R4 | Bottom line |
| ------- | -- | -- | -- | -- | ------ | ------- | ----------- |
| Priya — Eng Leader | 7.0 | 9.0 | 9.5 | 9.0 | **9.5** | +0.5 | R4-4 genuinely fixed; she independently traced the dual-pipeline workflow as correct GitLab |
| Dana — .NET Monolith | 6.0 | 8.5 | 9.5 | 9.5 | **9.5** | 0 | R4-3 + R4-7 land as real monolith fixes; walkthrough move clean; no AWS bias regression |
| Tex — AI Champion | 8.5 | — | 9.5 | 9.5 | **9.5** | 0 | R4-2 VSM×AI callout is arithmetic-sound and durable; AI spine consistent end-to-end |

All three confirm **no adoption blockers**. The round-4 dip was Priya's, driven entirely by the
`customer-facing` gate being recorded as a real job when it was only prose. That is now resolved,
and she verified it the hard way (YAML parses, single source of truth, workflow rules trace
correctly), restoring 9.5.

### What the round-4 changes achieved (independently verified)

- **R4-4 — the gate is genuinely real.** `release-impact-label` is the only `$CI_MERGE_REQUEST_LABELS`
  hit course-wide and it lives in `session-3/examples/.gitlab-ci.yml`; `yaml.safe_load` parses it;
  `communicating-releases.md` references it by a resolving path. Priya traced the upgraded
  `workflow:` recipe: the MR pipeline runs the label gate, the branch pipeline is suppressed while
  an MR is open (no duplicate), `build:artifact`/`deploy:*` correctly do **not** run in MR pipelines
  (`$CI_COMMIT_BRANCH` empty there), and no `needs:` is broken. Round-3's "parse-verified job" claim
  is now true.
- **R4-3 + R4-7 — the monolith additions are substantive, not token.** Dana re-derived the long-branch
  claim: a 2–3-week branch drives the worked example to **8.4–10.2% flow efficiency** — the "single
  digits" R4-7 asserts. The batch-size prompt sits in the live in-session map with the causal tie to
  the low deploy %C/A. The VSM stays platform-neutral (math re-verified exact: 19.5 / 108 / 127.5 h,
  15.3% flow efficiency, 34.3% rolled %C/A).
- **R4-2 — the VSM×AI callout holds up.** Tex verified the constraint-shift in the example's own
  numbers (12 h develop process collapses; 64 h of downstream waits don't; lead time barely moves and
  the binding constraint relocates to review / the deploy window). Correctly scoped to process+waits,
  not %C/A; no dated/volume claims; cross-links resolve. The four AI spine callouts (S1 §2.2, S1 §5.2,
  S2 §4.2, the resource) are mutually consistent, and the new callout does not undercut the
  test-gaming seam (cheap coding → gates matter *more*).
- **R4-6 — the walkthrough move is clean.** File physically relocated; all inbound (`session-3/README.md`)
  and outbound links resolve; monolith framing intact. Stale `session-1/examples/...` paths survive
  only as backtick prose in historical feedback files (non-navigational).

## Open items — none blocking

All polish. Grouped by effort.

### Tier 1 — one-line / few-line fixes

| ID | Item | Persona | Where |
| -- | ---- | ------- | ----- |
| R5-1 | **Round-4 synthesis self-contradicts on the walkthrough.** The "Corrections" bullet says the file did **not** move and "stays at `session-1/examples/`"; the "Resolution" table (R4-6) records it **was** `git mv`'d to `session-3/examples/` — which is the truth. A record defect (introduced when the resolution line was updated but the older correction bullet wasn't). Strike/correct the bullet. | Priya | `docs/course-feedback/round-4-synthesis.md:86-88` |
| R5-3 | **Prose snippet drifts from the real job.** The `release-impact-label` block in `communicating-releases.md` omits the `before_script: []` the real job carries (it suppresses the default `npm ci`). A verbatim copy-paste behaves differently. Add the line, or mark the block "(excerpt)". | Priya | `resources/communicating-releases.md:82-94` |
| R5-4 | **Worked example doesn't follow its own new rule.** R4-3 tells readers to write the batch size next to the deploy step, but worked-example row 6 (release-day deploy) carries no such figure. Add an annotation (e.g. "~38 changes rode this deploy") so the exemplar models the rule. | Dana | `exercises/value-stream-map.md:104` |

### Tier 2 — small addition

| ID | Item | Persona | Where |
| -- | ---- | ------- | ----- |
| R5-2 | **Session-3 README is silent on the workflow divergence.** The MR-time gate, the upgraded `workflow:` recipe, and the deliberate divergence from Session 2's branch-only recipe are explained only in the YAML comments and `communicating-releases.md`; the README prose mentions none of it (grep: workflow / merge_request / release-impact / customer-facing all absent). One sentence in §6.1 would make the divergence read as intentional rather than inconsistent. | Priya | `sessions/session-3/README.md` (§6.1) |

### Tier 3 — optional (explicitly not docked)

| ID | Item | Persona | Note |
| -- | ---- | ------- | ---- |
| R5-5 | **(Carried R3-6) Operationalise "a separate agent reviews the tests."** Deferred to the team's SDLC skill (outside the course); Tex accepts the deferral and does **not** dock. Optional close: a two-line *tool-neutral* "what independent test review looks like in the MR" pattern (criteria-first; the second reviewer/agent doesn't see the impl diff). Keep it product-neutral — a tool pointer would break the course's self-containment. The only thing between 9.5 and reference text, per Tex. | Tex | `resources/ai-assisted-delivery.md` |

## Recommended fix order

1. **R5-1 (record defect):** correct the round-4 synthesis contradiction — it's a factual error in our own log, cheapest to fix.
2. **R5-4 + R5-3 (exemplar/snippet fidelity):** the batch-size annotation and the `before_script: []` line — both make an artifact match the rule/job it illustrates.
3. **R5-2 (README sentence):** make the Session-3 workflow divergence intentional in prose.
4. **R5-5:** optional polish toward reference-text; tool-neutral or skip.

With three unconditional 9.5s and zero blockers across five rounds, the course is **adoption-ready as
it stands.** The above is refinement, not remediation.

## Resolution (applied 2026-06-22)

All five round-5 items applied.

| ID | Resolution |
| -- | ---------- |
| R5-1 | `round-4-synthesis.md`: corrected the "Corrections" bullet — it now states the walkthrough had not moved *at review time* and that R4-6's resolution then `git mv`'d it; no longer contradicts the Resolution table. |
| R5-2 | `session-3/README.md` §6.1: added a sentence framing the `release-impact-label` MR-time job and the deliberate `workflow:` step-up from Session 2's branch-only form, linking to `communicating-releases.md`. |
| R5-3 | `communicating-releases.md`: the snippet now carries `before_script: []` (with a one-line reason), matching the real job — a verbatim copy behaves identically. |
| R5-4 | `value-stream-map.md`: worked-example deploy row annotated "(~20 changes)" and the reveal bullet tied to "all ~20 changes ship at once," so the exemplar models R4-3's own batch-size rule. |
| R5-5 | `ai-assisted-delivery.md` seam 1: added a tool-neutral operational bullet — the test reviewer sees the criteria and tests but *not* the implementation diff (independent review in practice, not just name). |
