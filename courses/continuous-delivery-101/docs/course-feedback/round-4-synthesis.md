# Continuous Delivery 101 — Round 4 Synthesis (Spot-Check)

**Date:** 2026-06-22
**Inputs:** Targeted re-reviews from the three personas whose gaps drove rounds 2–3 —
`student-3-priya-eng-leader-review-4.md`, `student-2-dana-dotnet-monolith-review-4.md`,
`student-8-tex-ai-champion-review-3.md`.
**Method:** Each persona re-read its prior reviews and the *current committed* course, then
pressure-tested the two changes landed since round 3 — (1) Session 1 rebuilt around **value
stream mapping**, (2) the **deploy/release terminology** pass + "deployable not releasable"
deviation note — and was instructed to hunt for regressions, not rubber-stamp.

## Verdict: pass (adoption-ready); one precision/process dip

| Persona | R1 | R2 | R3 | **R4** | Δ vs R3 | Bottom line |
| ------- | -- | -- | -- | ------ | ------- | ----------- |
| Priya — Eng Leader | 7.0 | 9.0 | 9.5 | **9.0** | −0.5 | Greenlight stands; docked for two artifact/process gaps in her lane (not content) |
| Dana — .NET Monolith | 6.0 | 8.5 | 9.5 | **9.5** | 0 | VSM-in-S1 retires her oldest complaint; terminology sweep left her examples cleaner |
| Tex — AI Champion | 8.5 | — | 9.5 | **9.5** | 0 | Both changes land; VSM is the ideal place to show AI's constraint-shift but stays silent |

All three confirm **no adoption blockers**. The dip is Priya's, and it's about precision/process
artifacts, not the teaching: she re-flagged her four-round theme ("the precision trails the
prose") with a concrete, verified instance.

### What the changes achieved

- **VSM rebuild closed Dana's longest-standing gap.** Her four-round "the first concrete pipeline
  a student meets isn't mine" complaint is retired from two sides: S1's first activity is now
  mapping the team's own stack-neutral flow (no pipeline to be AWS-centric), and the walkthrough
  that moved to S3 §6.1 is correctly framed "read our own real pipeline first," names the
  monolith's `ci-templates`/MSBuild→IIS pipeline alongside the AWS baseline, and demotes the
  baseline to one labelled artifact among three. The worked example's biggest wait — "the weekly
  release window" — *is* the `release/YY.M.W` branch S3 names as the monolith's real CD gap. One story.
- **The VSM worked-example math is exact** — independently re-derived by all three: 19.5 h process,
  108 h wait, 127.5 h lead (~16 working days), 15.3% flow efficiency, 34.3% rolled %C/A.
- **The terminology sweep is clean and consistent** (~95%+): `releasability` survives only as
  MinimumCD minimum #3's pipeline verdict, `deployable` is the trunk/work state everywhere, the
  deviation note is accurate and appropriately scoped, and `ai-assisted-delivery.md` still passes
  the 12-month durability test (zero dated/volume claims). No cross-course links introduced.

## Open items — none blocking

Consolidated and de-duplicated, grouped by effort.

### Tier 1 — one-line / few-line fixes, clear value

| ID | Item | Persona | Where |
| -- | ---- | ------- | ----- |
| R4-1 | **VSM example: rework counted in the rule but not the headline.** Part 2 says "rework shows up twice — count the extra trip's time too," but the worked example's 127.5 h lead time is a clean single pass while its 34.3% rolled %C/A implies most changes bounce back at least once. One clause: the trace is an optimistic single pass; the 34% means the *typical* change incurs at least one extra loop, so 127.5 h is a floor. | Tex | `value-stream-map.md` (worked example) |
| R4-2 | **Tie the VSM to AI's constraint-shift.** The VSM is the ideal instrument to *show* the course's AI thesis — when an agent writes code, "develop" collapses and the constraint moves to the review queue / release window — yet §5 and the exercise never say so. The one new spine location where authorship is absent. One callout in Part 4 / §5.2. | Tex | `value-stream-map.md`, `session-1/README.md` §5 |
| R4-3 | **VSM missing a "changes bundled per deploy" prompt.** The new centerpiece softly re-hides batch-size-per-deploy (the monolith blind spot). The scorecard's Part 3 tracks it; the *map* doesn't. One prompt. | Dana | `value-stream-map.md` |
| R4-4 | **`customer-facing` MR gate still isn't real YAML — and was recorded as "parse-verified."** The `release-impact-label` job exists *only* as a fenced block in `communicating-releases.md`; a grep of every `.yml` returns nothing. Round-3 synthesis logged R3-5 as "real, runnable … YAML parse-verified," which is misleading (the snippet was parsed, not a pipeline file). Recurring "precision trailed the prose." Fix: move the job into `session-3/examples/.gitlab-ci.yml` (as was done for `smoke-test.sh`), or correct the record. | Priya | `communicating-releases.md`; `session-3/examples/.gitlab-ci.yml` |

### Tier 2 — small additions (more than a line)

| ID | Item | Persona | Where |
| -- | ---- | ------- | ----- |
| R4-5 | **Pinned Session-1 slide outline now contradicts the session.** `slides/session-1-outline.md` Slide 8 ("Now: score OUR pipeline") and Slide 1 ("score our real pipeline") describe the *replaced* workshop; the live S1 workshop is the VSM. Slides are **pinned by decision**, so this is latent, not active — flag for whenever slides resume. | Priya | `slides/session-1-outline.md` |
| R4-6 | **Walkthrough file orphaned-by-location.** `current-state-pipeline-walkthrough.md` lives under `session-1/examples/` but its only consumer is now Session 3, and it's still titled "Scoring Our Current Pipeline." (Kept in place deliberately — historical-feedback references point at this path.) A retitle, or a one-line "used in Session 3" header, resolves the confusion without moving it. | Priya | `session-1/examples/current-state-pipeline-walkthrough.md` |
| R4-7 | **VSM example models a near-TBD team, not a bleeding monolith.** Its 2-day branch wait is generous; a real long-lived-branch monolith bleeds *weeks* there. One line acknowledging the long-branch case would sharpen it for the monolith crowd. | Dana | `value-stream-map.md` |
| R3-6 | **(Carried) "Separate agent reviews the tests" still asserted, not operationalised.** A two-line MR-level pattern (criteria-first, reviewer/agent doesn't see the impl diff). Tex's longest-standing item. | Tex | `ai-assisted-delivery.md` |

### Tier 3 — scope call (defensible to leave)

| ID | Item | Persona | Note |
| -- | ---- | ------- | ---- |
| R3-8 | **(Carried) Prod-like *data* for a shared SQL Server named-as-hard, never worked.** Dana's "close it for a 10" item; she explicitly does **not** dock for it (101-scope-defensible). | Dana | Decision needed |

## Recommended fix order

1. **R4-1 + R4-3 (VSM rigor):** the rework/lead-time clause and the changes-bundled prompt — both
   one-liners in the file just authored, both close a named gap.
2. **R4-2 (VSM × AI):** highest-leverage single addition — turns the best authorship-agnostic
   exercise into the best illustration of "the bottleneck shifted." One callout.
3. **R4-4 (`customer-facing` gate as real YAML):** retires Priya's recurring theme honestly; the
   course has the precedent (`smoke-test.sh`). Doing it is what moves her back to 9.5.
4. **R4-6 (retitle/flag the orphaned walkthrough), R4-7, R3-6:** polish toward best-in-class.
5. **R4-5 (slides):** only when slides are un-pinned. **R3-8:** explicit scope decision.

With two unconditional 9.5s and a 9.0 driven by precision artifacts rather than content, the course
remains **adoption-ready as it stands** — the above is refinement, not remediation.

## Corrections to reviewer notes (for the record)

- A site build **does** exist (`site/`, `npm run build`); Dana ran it from the course root and saw
  none. Build is green (CD: 21 pages, 0 errors).
- The walkthrough file did **not** physically move (Dana's and the brief's wording) — only the
  *activity* moved to Session 3; the file stays at `session-1/examples/` so historical-feedback
  path references keep resolving. This is the basis for R4-6.

## Resolution (applied 2026-06-22)

R4-1 through R4-7 applied; R3-6 deferred (covered by the SDLC skill in Claude Code), R3-8 left as
a scope decision.

| ID | Resolution |
| -- | ---------- |
| R4-1 | `value-stream-map.md`: callout after the worked-example math — 127.5 h is an optimistic single pass, the 34% rolled %C/A means the typical change loops back ≥ once, so read it as a floor. |
| R4-2 | AI constraint-shift callout in the exercise (Part 4) and a one-line pointer in `session-1/README.md` §5.2: *develop* collapses, the constraint moves to review/the deploy window. |
| R4-3 | `value-stream-map.md` Part 2: a "note the batch size at the deploy step" rule of thumb ties batch-size-per-deploy to the low deploy %C/A. |
| R4-4 | **Made real (not just corrected).** `release-impact-label` is now an actual job in `session-3/examples/.gitlab-ci.yml`; that pipeline's `workflow:` was upgraded to the standard dual recipe (run the MR pipeline so `$CI_MERGE_REQUEST_LABELS` is populated, suppress the duplicate branch pipeline while an MR is open). `communicating-releases.md` now references the real job by path. Round-3's "parse-verified job" claim is now true. |
| R4-5 | Pinned `slides/session-1-outline.md` Slide 8 retitled "Map YOUR value stream" and Slide 1's promise updated to match the VSM workshop. Slides remain pinned/untracked — edited for correctness, not committed. |
| R4-6 | `current-state-pipeline-walkthrough.md` **moved** to `session-3/examples/` (`git mv`) — its only consumer is the Session 3 §6.1 workshop. The two active links in `session-3/README.md` and the file's own Session-3 cross-links were repointed; build link-checker clean. (Historical `docs/course-feedback/*` mentions of the old path are left as written-at-the-time records; they are backtick spans, not navigational links.) |
| R4-7 | `value-stream-map.md`: note that the example models a near-TBD team; a long-lived-branch monolith shows *weeks* at develop, pushing flow efficiency into single digits. |
