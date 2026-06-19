# Continuous Delivery 101 — Round 2 Feedback Synthesis

**Date:** 2026-06-19
**Inputs:** 7 persona re-reviews (`student-1…7-*-review-2.md`) + 1 new persona
(`student-8-tex-ai-champion-review-1.md`)
**Method:** The 7 round-1 reviewers re-read their own prior review and the
revised course, then reported each point as IMPROVED / PARTIALLY ADDRESSED /
REGRESSED / STILL OPEN. Tex (AI Champion) is new this round and reviewed fresh.
This doc consolidates the delta into a prioritized round-2 triage.

**Status (2026-06-19):** Round 2 reviews gathered, triage below. Applied so far —
the **instant wins**: **R2-H1** (real `violations-api/scripts/smoke-test.sh` — a
side-effect-free probe, now documented in the service README) and **R2-H2** (the
`deploy:dev` `needs:` list now names the full definition of deployable — `lint`,
`validate:sam`, `unit-tests`, `build:artifact` — and drops the advisory
`dependency-audit`). And **R2-H3**: `current-state-assessment.md` reworked to
serve all four personas who hit it — a new Part 2 controls/governance scorecard
(audit trail, SoD, least privilege, mandated approvals, break-glass, release
communication, release authorization), a **Deliberate?** column so a defensible
control isn't scored as a gap, a reframed constraint section that no longer
pre-answers itself, and a solo/junior callout. And **R2-H4** (the new dimension
Tex opened): added `resources/ai-assisted-delivery.md` — the three seams
(test-gaming, review substance, flag explosion) plus an honest "what doesn't
change" — with durable framing (no "nearly all the code" claim that would date),
two surgical inline sentences (Session 1 batch-size-as-containment, Session 2
gate-honesty), and exercise/example patches (test-independence + review-burden in
the assessment, the durable-human-skill note in decompose-a-branch, a human/agent
division-of-labour sidebar in strangler-fig, an accounting line in what-cd-costs).
Per the decision, all references stay **inside CD 101** — AI 101 untouched, no new
cross-course links. Plus the **cheap sweep**: **R2-M1** (glossary gained two
sections — Migration & data: strangler fig, seam, system of record, idempotent,
watermark, reconciliation; and Governance & control: SoD, audit trail,
break-glass, emergency change), **R2-N1** (README goal rescoped to the whole
estate; James Clear quote re-attributed), **R2-N2** (DORA survey-data caveat in
Session 1; the stale-flag check shown as a runnable CI job in what-cd-costs),
**R2-N3** (Riley's tail: alias-shift drift now leads its section; divide-by-zero
teaching comment on the canary alarm; OIDC `aud` and `CAPABILITY_IAM` notes in the
pipeline; the build diagram now distinguishes `CI_COMMIT_SHA` from SAM's content
hash), and **R2-N4** (trunk glossed inline at first use in Session 1). And
**R2-M2** (Dana's monolith gaps): a stored-procedure expand/contract section in the
strangler-fig example (version `usp_X_v2`, never `ALTER` in place; a flag can't hide
a shared proc — coordination is the safety mechanism), a monolith pipeline sketch
proving "same GitLab CI" — later **verified against the real `ciranet-management-api`
pipeline** and corrected to the actual mechanism (MSBuild FileSystem publish → `a/`
artifact → deploy-time `web.{env}.config` XDT transform → IIS via per-server runners;
the earlier MSDeploy/`setParameters.xml` draft was fabricated), an expanded IIS
feature-flag bullet (app-pool recycle + multi-VM farm drift), a third **.NET / IIS**
column on the minimums-reference mapping table, a fix to the walkthrough's AWS-default
fallback, and a "changes bundled per prod deploy" metric in the assessment.
And the closing sweep — **R2-M3** (governance precision): self-approval prevention
promoted from a parenthetical to enforced GitLab settings (Prevent approval by author
/ by committers, ≥1 approval, pipelines-must-succeed); an artifact-retention caveat
(the 30-day `expire_in` is a promotion window, not an audit-evidence policy — set it
from your control framework) in both the governance doc and `.gitlab-ci.yml`; a
concrete break-glass mechanism (a scoped, second-approver, auto-expiring
`break-glass-deploy` IAM role); a managed-flag-service control-trade note (graduating
to AppConfig/LaunchDarkly moves the flip out of the pipeline audit trail into the
service's own log — wire it back deliberately); and a business-vs-engineering freeze
carve-out in `branching-antipatterns.md` (a mandated business change window freezes
the *release*, not the *integration*). And **R2-M4** (release-comms reconciliation):
Session 3 §2.2 now splits the timing decision in two (Engineering Lead owns the deploy
gate; the evolved release-manager role owns flag-flip/release timing); Session 2's
"release = flip the flag" gains a forward-link to the communication consequence; and
the `customer-facing` label is shown as a check (MR must carry `customer-facing` or
`no-user-impact`), not a hope. **Round 2 is now fully applied.**

---

## Scorecard — Round 1 → Round 2

| Persona | R1 | R2 | Δ | R2 verdict |
| ------- | -- | -- | - | ---------- |
| Sam — Senior Dev (skeptic) | 6.5 | **8.5** | +2.0 | Now champions for monolith + strangler-fig, not just greenfield |
| Dana — .NET Monolith | 6.0 | **8.5** | +2.5 | Champion, with two named gaps |
| Priya — Eng Leader | 7.0 | **9.0** | +2.0 | Comply-toward-champion → champion |
| Marcus — Release Manager | 6.0 | **9.0** | +3.0 | Conditional → champion |
| Felix — Junior Dev | 8.0 | **9.0** | +1.0 | Champion, much smaller beginner's asterisk |
| Jordan — Tech Lead | 7.5 | **9.0** | +1.5 | Champion, no longer reserved about the migration |
| Riley — SRE / Platform | 8.5 | **9.5** | +1.0 | Champion, one block-item left |
| **Tex — AI Champion (new)** | — | **8.5** | — | Champion, conditional on an AI-authorship resource |

**Returning-7 average: 7.07 → 8.93 (+1.86). With Tex: 8.88.** Every returning
persona's score rose; none regressed overall. The round-1 "big lift" landed: the
strangler-fig worked example, the governance pass, release communication, the
honest-costs/flag-debt mechanism, and the migration-as-journey rewrite are each
cited as the specific reason a score moved.

---

## What landed (round-1 triage → outcome)

| R1 item | Outcome | Evidence the personas cited |
| ------- | ------- | --------------------------- |
| H1 — release communication | **Closed** (Marcus 6→9) | `communicating-releases.md` gives an operable data source (flag inventory, flip date = release date); wired into Session 3, troubleshooting, glossary, checklist Phase 4 |
| H2 — technical errors | **Fixed correctly** (Riley verified the YAML) | OIDC web-identity pattern `.gitlab-ci.yml:55-66`; canary alarm rewired to a 5% error-*rate* on the `:live` alias, one `ReturnData:true`, `EvaluationPeriods:2` `template.yaml:112-154` |
| H3 — greenfield-only / strangler-fig never shown | **Closed** (Sam, Dana, Jordan) | `strangler-fig-violations.md` handles dual-write, idempotency, reconciliation, backfill, expand/contract on SQL Server, IIS recovery — "best new file in the course" |
| H4 — migration = destination, not journey | **Closed** (Jordan 7.5→9) | Checklist "journey" section, Phase 0 in-flight-branch handling, effort sizing, velocity-dip "say so out loud" |
| H5 — governance not first-class | **Closed** (Priya 7→9) | `governance-and-compliance.md`: debt-vs-permanent-control test, MR review as SoD, pipeline as audit log, break-glass, auditor paragraph |
| M1 — CD's costs never debited | **Closed** (Sam) | `what-cd-costs.md` is "the honest ledger I demanded"; flag debt now a mechanism (birth certificate + CI stale-flag check + delete-as-a-slice) |
| M2 — glossary/clarity | **Mostly closed** (Felix) | expand/contract, smoke test, DORA, release notes added; README anchor note added |
| M3 — exercises assume cloud-native | **Partially** | `decompose-a-branch.md` brownfield variant landed; `current-state-assessment.md` still lags (see R2-H3) |
| M4 — team-owned quality unstated | **Closed** | Stated in Session 1 + `CLAUDE.md` |
| N1/N2 — npm audit gate / cross-account bucket | **Fixed** (Riley verified) | `dependency-audit` now advisory + `--omit=dev`; cross-account note `.gitlab-ci.yml:40-43` |

---

## The one theme of round 2

> The prose grew up; the **scaffolding and the precision** trailed it. The big
> narrative gaps are closed, so what surfaces now is concrete and mostly small:
> one **referenced-but-missing artifact** (`smoke-test.sh`), one **exercise that
> didn't keep pace** with the prose (`current-state-assessment.md`), a **glossary
> that trails its own new advanced doc**, and a tail of **technical precision
> nits**. Plus one brand-new dimension Tex opened: the course never names *who
> writes the code*, and at RealManage that's now mostly AI.

The single most-cited new finding — by **Sam, Jordan, and Riley independently** —
is that `.gitlab-ci.yml:149` and the reading guide call `./scripts/smoke-test.sh`,
**which does not exist anywhere in the tree.** "Verified, not hoped" rests on a
vapor file. Jordan flags it as a *regression* (more load-bearing now that dev→qa
auto-promotes on its result). Riley calls it his one remaining block-item.

---

## Prioritized round-2 triage

### HIGH — address before the next cohort

| # | Issue | Raised by | Evidence | Proposed edit |
|---|-------|-----------|----------|---------------|
| R2-H1 | **`smoke-test.sh` is referenced but does not exist.** The pipeline calls it after every deploy and the reading guide points to it as where promotion is "verified, not hoped" — but there is no `scripts/` dir and no `*.sh` in the tree. The dev→qa auto-promote safety story rests on a missing file. | Sam, Jordan (REGRESSED), Riley (block-item) | `sessions/session-3/examples/.gitlab-ci.yml:149`, reading guide; `violations-api/` has no `scripts/` | Add a minimal real `violations-api/scripts/smoke-test.sh` (curl the deployed API Gateway health/record path, assert 2xx, exit non-zero on failure). Small, unambiguous, unblocks the pipeline's central claim. |
| R2-H2 | **`deploy:dev needs:` gates nothing meaningful.** The list is `[build:artifact, unit-tests, dependency-audit]` — it omits `lint` and `validate:sam`, and the one extra it includes (`dependency-audit`) is `allow_failure: true`, so it can't block. Contradicts the course's own "green means deployable." | Jordan; Riley (merge-blocking named in governance, not enforced in pipeline) | `.gitlab-ci.yml` `deploy:dev needs:` | Make the deploy depend on the full definition-of-deployable (lint, validate:sam, unit-tests, build), or rely on stage ordering and drop the misleading `needs`. Either way, stop implying an advisory job is a gate. |
| R2-H3 | **`current-state-assessment.md` didn't keep pace with the prose** — and four personas hit the same wall. No governance/controls dimension (Priya), no "how do downstream parties learn a release happened?" row (Marcus), no IC/junior "what can I fill in without MR-history/release access?" note (Felix), no "deliberate choice" column and it still pre-answers the binding constraint with "for most teams it's branch lifetime" (Sam). | Priya, Marcus, Felix, Sam; Jordan (partial) | `exercises/current-state-assessment.md` | One coordinated rework: add a governance/release-communication scoring dimension, a "deliberate choice vs not-yet" column, drop the pre-answered constraint, and add a short note for a solo/junior filler. Serves four personas at once. |
| R2-H4 | **NEW — the course never names the author, and at RealManage the author is now mostly AI.** Tex confirmed the course body has zero mentions of AI authorship (only the `CLAUDE.md` tooling note + one AI-101 footer link). CD as taught is genuinely AI-ready and "automate the verdict" gets *stronger* when machines write the code — but three seams go unaddressed: (1) **test-gaming** when the same agent writes code and its tests (coverage becomes a Goodhart target), (2) **review substance / SoD** erodes when diffs are large, plausible, and machine-written, (3) **flag explosion** at AI pace outruns a human-sized flag inventory. | Tex (headline) | course-wide silence; `README.md:210` (sole AI-101 link) | Add `resources/ai-assisted-delivery.md` naming the three seams and pointing each back to the practice that mitigates it (tests-as-spec written/reviewed by a human; MR review as the load-bearing control; the mechanical flag-debt check). Add bidirectional links with `../ai-101-claude-code/`. New strategic dimension; aligns with the org's AI push. |

### MEDIUM — strengthens credibility and reach

| # | Issue | Raised by | Evidence | Proposed edit |
|---|-------|-----------|----------|---------------|
| R2-M1 | **Glossary trails its own hardest new doc.** The strangler-fig example introduces `idempotent`, `system of record`, `watermark`, `reconciliation`, and `seam` with no glossary entries — and the course now routes beginners there from page one. Governance vocab (`segregation of duties`, `audit trail`, `break-glass`, `emergency change`) also still absent. | Felix, Priya | `glossary.md`; `strangler-fig-violations.md:57,83` | Add the missing terms. Cheap, high-value, and the strangler-fig terms are the only place the revision out-ran the course's clarity backbone. |
| R2-M2 | **The .NET/monolith story is 100% prose — no artifact, no shared-proc case.** Dana's three survivors: (a) no MSDeploy/MSBuild/`setParameters.xml` pipeline analogue to the worked SAM service; (b) `minimums-reference.md` "How this maps" table and `current-state-pipeline-walkthrough.md` are still AWS-only — the docs she's told to keep open have no row for her stack; (c) the SQL treatment is column-only and never touches an `ALTER PROC` shared by callers she doesn't own (the example is "90% of the way to having the tools"). | Dana | `minimums-reference.md`; `session-1/examples/current-state-pipeline-walkthrough.md`; `strangler-fig-violations.md` | Add a `.NET-on-IIS/SQL` pipeline sidebar or example; add a monolith row to the mapping table; extend the expand/contract section to the shared-stored-procedure case (the "other readers move first" insight is the proc analogue — just connect it). |
| R2-M3 | **Governance precision gaps an auditor would catch.** (a) Artifact `expire_in: 30 days` is shorter than typical audit-evidence retention, yet the doc leans on that artifact as evidence. (b) Self-approval prevention — the whole SoD claim — is asserted as a parenthetical, not shown as enforced GitLab config. (c) Graduating env-var flags to a managed flag service is an unnamed control *downgrade* (release authorization leaves the pipeline's audit trail); the two files covering flags never connect this. (d) Break-glass is sound policy but hand-wavy as mechanism (no named approver, no scoped-access implementation). (e) Mandated business freeze vs engineering freeze still conflated. | Priya | `.gitlab-ci.yml:124`; `governance-and-compliance.md`; `branching-antipatterns` Anti-pattern 5 | Tighten each: a retention note, a shown self-approval-prevention setting, an explicit "managed flag service moves authorization out of the pipeline" caveat, a concrete break-glass mechanism, and a carve-out distinguishing a contractual/business freeze from an engineering one. |
| R2-M4 | **Release-comms threads not reconciled with the rest of the course.** Session 3 §2.2 still calls the prod-timing decision the "Engineering Lead's," while `communicating-releases.md` makes flag-flip timing the release-comms owner's — unreconciled. Session 2's "release = flip the flag" never links the comms consequence. And the `customer-facing` MR label is a convention, not a pipeline gate, so the whole release-notes data source rides on discipline. | Marcus | `session-3/README.md:45`; `session-2/README.md:68-81`; `communicating-releases.md` | Reconcile who owns flip timing; add a one-line forward-link from Session 2's flag-flip to the comms consequence; note (or gate) the `customer-facing` label so the data source isn't purely discretionary. |

### NICE-TO-HAVE

| # | Issue | Raised by | Proposed edit |
|---|-------|-----------|---------------|
| R2-N1 | `README.md:6` still scopes the goal "to true Continuous Delivery on AWS cloud-native services," contradicting the now monolith-first content; `README.md:256` credits a James Clear quote to "DX Team." | Dana, Sam | Rescope the line to the whole estate; fix the attribution. |
| R2-N2 | DORA "evidence" (Session 1 §2.3) name-drops *Accelerate* without noting it's survey/self-report data; the CI stale-flag check is described but never shown as runnable YAML ("one notch short of its own 'mechanism, not a sermon' headline"). | Sam | Add a one-clause caveat on the data; show the stale-flag check as a small CI job. |
| R2-N3 | Riley precision tail: lead with the alias-shift-drift rule in `rollback-on-aws.md`; the build diagram still conflates `CI_COMMIT_SHA` tagging with the SAM content-hash; add a one-line teaching note that the canary error-rate math divides by zero on an idle alias (safe — CloudWatch treats it as missing → stays OK — but it means the canary can pass on zero evidence in a low-traffic window); `CAPABILITY_IAM` scope still unverified. | Riley | Small, exact edits; none are wiring bugs. |
| R2-N4 | `trunk` is glossed and tied to `main` in Session 2 and the glossary, but its *first* use (`session-1/README.md:37`) still isn't glossed inline; `strangler fig` still has no glossary entry (though it now links to the worked example). | Felix | Inline-gloss trunk on first use; add a strangler-fig glossary stub. |

---

## Suggested fix order

1. **Instant wins (minutes):** R2-H1 (`smoke-test.sh`) and R2-H2 (`needs:` list)
   — both are concrete, unambiguous, and fix the pipeline's own central claims.
   Three reviewers independently flagged the missing script.
2. **The exercise that four personas hit:** R2-H3 — one coordinated
   `current-state-assessment.md` rework closes Priya, Marcus, Felix, and Sam's
   shared STILL-OPEN.
3. **The new dimension:** R2-H4 — decide whether to add `ai-assisted-delivery.md`.
   This is a *direction* call, not just a fix: it commits the course to naming the
   AI-authorship reality and links it to AI 101. Worth a deliberate yes/no.
4. **Reach + precision:** R2-M1 (glossary), R2-M2 (the .NET artifact — the last
   thing keeping Dana off a 9+), R2-M3 (governance precision), R2-M4 (comms
   reconciliation).
5. **Polish:** the N-items, sweepable in one pass.

---

## Notes

- **Diminishing returns are visible.** Round 1 fixed *what the course was about*;
  round 2's list is *finish-the-job* work. The scores cluster at 8.5–9.5 — the
  course is past "does it convince" and into "is it airtight."
- **Tex is a keeper persona.** He surfaced the one structural gap none of the
  delivery-practice reviewers could see, because they all implicitly assume a
  human author. He framed it honestly (CD is mostly authorship-agnostic; the risk
  is concentrated in three named seams), not as AI hype.
- **The recurring "it's named but not shown" pattern** still has two instances
  left: `smoke-test.sh` (referenced, absent) and the CI stale-flag check
  (described, not coded). Both are cheap to make real.
- **Re-run:** after round-2 fixes land, a round-3 spot-check could re-run only the
  personas with STILL-OPEN items (Dana on the .NET artifact, Priya on the
  assessment governance dimension, Tex on the AI resource) rather than the full
  panel.

*Full reviews: `docs/course-feedback/student-{1..7}-*-review-2.md`,
`student-8-tex-ai-champion-review-1.md`.*
