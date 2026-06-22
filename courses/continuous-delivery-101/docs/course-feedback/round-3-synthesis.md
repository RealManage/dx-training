# Continuous Delivery 101 — Round 3 Synthesis (Spot-Check)

**Date:** 2026-06-22
**Inputs:** Targeted re-reviews from the three personas whose gaps drove round 2 —
`student-3-priya-eng-leader-review-3.md`, `student-2-dana-dotnet-monolith-review-3.md`,
`student-8-tex-ai-champion-review-2.md`.
**Method:** Each persona re-read its own prior reviews and the *current committed*
course, scored the round-2 fixes (IMPROVED / PARTIAL / STILL OPEN), and was instructed
to hunt for newly-introduced problems — not to rubber-stamp.

## Verdict: pass

| Persona | R1 | R2 | **R3** | Δ vs R2 | Bottom line |
| ------- | -- | -- | ------ | ------- | ----------- |
| Priya — Eng Leader | 7.0 | 9.0 | **9.5** | +0.5 | Greenlight unconditionally, whole estate; hand governance doc to compliance lead |
| Dana — .NET Monolith | 6.0 | 8.5 | **9.5** | +1.0 | Champion with no spoken asterisk; will hand to team unedited |
| Tex — AI Champion | 8.5 | — | **9.5** | +1.0 (vs R1) | Unconditional champion; "the course I'd hand every engineer day one" |

All three round-2 workstreams landed at the source:

- **R2-M3 governance precision (Priya):** the four auditor-catchable items (enforced
  self-approval settings, artifact-retention-vs-audit caveat, concrete break-glass IAM
  mechanism, managed-flag control-trade) are now defensible; the assessment finally has
  a **controls scorecard + ◆ Deliberate? column**, closing her biggest two-round gap
  ("the assessment scored zero control dimensions").
- **R2-M2 monolith + the verification correction (Dana):** both her standing gaps
  closed — the stored-procedure case is complete, and the monolith pipeline is now the
  **real, verified** `ciranet-management-api` mechanism. She credits the *fabrication
  catch itself* for the last 1.5 points: "replacing a plausible MSDeploy/`setParameters.xml`
  story I'd have believed with the genuinely-awkward real mechanism is exactly what earns
  trust — it means the rest of the document was held to the same standard."
- **R2-H4 AI authorship (Tex):** the three seams each ship a concrete practice, framing
  passes the 12-month durability test (grep-verified: zero dated percentage claims), the
  self-contained-in-CD-101 constraint works, and the AI case lives on the course spine.

## Open items — all minor, none blocking

Consolidated and de-duplicated. Grouped by effort. Nothing here is an adoption blocker;
this is the gap between 9.5 and 10 / best-in-class.

### Tier 1 — one-line / few-line fixes, clearly worth doing

| ID | Item | Persona | Where |
| -- | ---- | ------- | ----- |
| R3-1 | **Break-glass review names no accountable owner.** Mechanism is concrete, but "mandatory post-incident review" never says *by whom / recorded where*. One clause: "owned by the on-call lead / service owner and recorded in the incident record." | Priya | `governance-and-compliance.md:90-91,138`; `glossary.md:158` |
| R3-2 | **AI resource has no prose pointer in the README.** Linked from six spine locations + a README file-tree *comment*, but a reader scanning the README narrative never meets it. One sentence in the resources framing. | Tex | `README.md` resources section |
| R3-3 | **The fail-forward × review-erosion compounding seam.** The one seam Tex would genuinely add: under incident pressure the agent's forward fix is fast *and* the place review most easily hollows out — they compound at the worst moment. One sentence: "the forward fix is the most dangerous place to let review become a rubber stamp." | Tex | `ai-assisted-delivery.md` (near `:69` "what doesn't change") |
| R3-4 | **Deploy-time config vs runtime flags both touch `web.config`.** Both statements are individually correct but sit apart; a careful .NET reader sees tension. One sentence: deploy-time per-env config (`web.{env}.config`, recycle is fine) and runtime feature flags (must NOT recycle) are different levers that happen to both touch `web.config`. | Dana | `strangler-fig-violations.md` (~`:236-242`) |

### Tier 2 — small additions (more than a line)

| ID | Item | Persona | Where |
| -- | ---- | ------- | ----- |
| R3-5 | **`customer-facing` / `no-user-impact` MR gate is named-but-not-shown.** The label is correctly elevated from hope to *check* in prose, but exists in no CI YAML — the same pattern round 2 flagged for `smoke-test.sh` (which *was* made real). A ~6-line `rules`/label-grep job would make the release-notes data-source guarantee real, not aspirational. **Recurring theme across both Priya rounds: "the precision trailed the prose."** | Priya | `communicating-releases.md:68-73`; `.gitlab-ci.yml` |
| R3-6 | **"Separate agent reviews the tests" is asserted, not operationalised.** Seams 1–2 lean on "a human — or a separate agent that did not write the code" as the independent check, but give no concrete how (contrast the fully-mechanical flag-debt section). A two-line "what this looks like in the MR" pattern. | Tex | `ai-assisted-delivery.md:26` |
| R3-7 | **Six-VM two-wave rollout glosses drain timing.** Good that it's there; it states the wave rollout as frictionless where, for a not-fully-stateless tier (session affinity, in-flight long requests), the connection-drain interval is the fiddly bit. A nod, not a rework. | Dana | `strangler-fig-violations.md` (wave section) |

### Tier 3 — scope calls (defensible to leave as-is)

| ID | Item | Persona | Note |
| -- | ---- | ------- | ---- |
| R3-8 | **Prod-like *data* for a shared SQL Server is named-as-hard but never worked.** Dana's "close it for a 10" item. She explicitly does *not* dock for it — "arguably out of scope for a 101, and the course no longer pretends it's a one-liner." Closing it with the same honesty as the stored-proc section would make it a 10 for the monolith crowd. Bigger lift than the rest. | Dana | Decision needed |
| R3-9 | **Real `.ps1` / `ci-templates` pipeline shown as *shape*, not lift-and-run file.** Dana calls this acceptable and scope-defensible ("the PowerShell is genuinely the boring part"), explicitly **not** a gap. Leave. | Dana | Recommend: leave |

## Recommended fix order

1. **Tier 1 sweep (R3-1…R3-4)** — four near-one-liners, each closes a named gap with
   real value; ~one edit pass.
2. **R3-5 (customer-facing gate as real YAML)** — highest-signal Tier 2 item: it's a
   *repeat* finding and the course has a precedent (smoke-test) for fixing exactly this
   "named but not shown" pattern. Doing it retires the round-2 theme in Priya's area
   entirely.
3. **R3-6, R3-7** — optional polish toward best-in-class.
4. **R3-8 (prod-like-data worked example)** — explicit scope decision; it's the only
   item any persona ties to a perfect score, but it's a 101-scope judgment call and a
   larger write than the rest.

After any edits: `npm run build`, YAML parse if `.gitlab-ci.yml` is touched, then commit.
With three unconditional 9.5s, the course is **adoption-ready as it stands** — the above
is refinement, not remediation.

## Applied (2026-06-22)

**Tier 1 + R3-5** applied per the post-round-3 decision:

- **R3-1** — break-glass post-incident review now names an accountable owner ("owned by
  the on-call lead / service owner and recorded in the incident record") in all three
  places: `governance-and-compliance.md` break-glass bullet + the auditor paragraph, and
  the `glossary.md` break-glass entry.
- **R3-2** — README "After the Course" now has a prose pointer to
  `ai-assisted-delivery.md` ("If agents write most of your code, read…"), so a reader
  scanning the narrative finds it without spelunking the file tree.
- **R3-3** — `ai-assisted-delivery.md` fail-forward bullet now connects the two AI risks:
  the incident fix is both where review decays to a rubber stamp (seam 2) and where speed
  tempts skipping it — the most dangerous place to let agent work merge unread.
- **R3-4** — `strangler-fig-violations.md` IIS feature-flag bullet now distinguishes the
  deploy-time `web.{env}.config` transform (per-env config; recycle fine) from a runtime
  feature flag (must live in the refresh-interval store) — "same file, two different levers."
- **R3-5** — `communicating-releases.md` now shows the `customer-facing` / `no-user-impact`
  gate as a real, runnable GitLab job (`release-impact-label`, runs in the MR pipeline
  where `$CI_MERGE_REQUEST_LABELS` exists), retiring the round-2 "named but not shown"
  theme in Priya's area. YAML parse-verified.

**Deferred (by decision):** R3-6, R3-7 (best-in-class polish) and R3-8 (prod-like-data
worked example — the 101-scope call). Documented here for any future pass.
