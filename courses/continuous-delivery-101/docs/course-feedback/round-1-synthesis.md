# Continuous Delivery 101 — Round 1 Feedback Synthesis

**Date:** 2026-06-19
**Inputs:** 7 persona reviews (`student-1…7-*-review-1.md`)
**Method:** Each persona read the full course in character, attempted both
exercises, and wrote a structured review. This doc consolidates them into a
prioritized triage.

**Status (2026-06-19):** Quick wins applied — **M2** (glossary + clarity) and
**H2** (OIDC auth + canary alarm); **M4** reframed for the no-QA-team reality.
All other items remain open and awaiting your decision; no further course
content has been changed.

---

## Scorecard

| Persona | Rating | Verdict |
| ------- | ------ | ------- |
| Felix — Junior Dev | 8/10 | Champion (with a beginner's asterisk) |
| Riley — SRE / Platform | 8.5/10 | Champion (4 must-fix technical items) |
| Jordan — Tech Lead | 7.5/10 | Comply enthusiastically → champion |
| Priya — Eng Leader | 7/10 | Comply → champion |
| Sam — Senior Dev (skeptic) | 6.5/10 | Comply grudgingly (champion for greenfield only) |
| Dana — .NET Monolith | 6/10 | Comply (leaning champion if gaps close) |
| Marcus — Release Manager | 6/10 | Comply leaning champion (conditional on one fix) |

**Average ≈ 7.1.** No one rejected it. The course wins its central arguments
(batch size, deploy≠release, CD≠Continuous Deployment, fail-forward). It loses
points on the **same recurring gap**: it teaches greenfield-AWS CD beautifully
but under-serves *the messy realities the course's own mission says dominate the
estate* — the monolith, the migration, governance, and release communication.

---

## The one theme behind most of the criticism

> **The course asserts "CD applies everywhere" but only ever demonstrates it on
> a clean greenfield AWS Lambda service.** Every worked artifact
> (`feature-flag.ts`, `violations-api/`, both `.gitlab-ci.yml`s, both exercises)
> is conveniently-decomposable greenfield. The strangler-fig pattern — the one
> bridge from monolith to cloud-native — is name-dropped 3× and **never worked
> through**. Result: Sam, Dana, and Jordan each had to do all the hard
> translation themselves, which is exactly where they're least confident.

Five of seven reviews are downstream of this. Fixing it lifts the most ratings.

---

## Prioritized triage

### HIGH — address before the next cohort

| # | Issue | Raised by | Evidence | Proposed edit |
|---|-------|-----------|----------|---------------|
| H1 | **Release-communication gap — and it has a specific owner.** *Org context (Shane, 2026-06-19): our "release manager" personally performs the weekly production deploy **and** curates the client release notes from Jira stories that teams tag with a release number.* CD breaks both halves: the pipeline takes over deploys, and "release number" tagging collapses once deploys are continuous and release = flag flip ≠ weekly bundle — so his release-notes data source evaporates. The course's only nod is one clause "maybe alongside a comms email," and migration Phase 4 declares victory without re-establishing comms. | Marcus (headline) | `session-1/examples/cd-vs-continuous-deployment.md:43`; `resources/migration-checklist.md` Phase 4 | New section (Session 3 or a resource): communicating releases under CD. Replace weekly "release number" tagging with a flag-/release-anchored source for notes; show how the weekly email evolves into curated release notes (from releases, not deploys); cover the release manager's **role transition** (deploy execution → release-comms + flag governance) and customer/support notification. Add a Phase 4 checklist item: "re-establish release communication **and its data source**." (Intersects H4 — migration journey.) |
| H2 | **Technical errors in the flagship examples.** (a) OIDC `before_script` omits the credential export — as written, deploy jobs never authenticate. (b) Canary `DeploymentPreference` alarm watches **function-level** `Errors` (mixes stable 90% + canary 10%) on a hair-trigger (`Threshold:1`,`Period:60`,`EvaluationPeriods:1`) → rolls back healthy deploys, undermining the fail-forward thesis. | Riley; Sam (OIDC) | `sessions/session-3/examples/.gitlab-ci.yml`; `sessions/session-3/examples/violations-api/template.yaml` | (a) Use native `AWS_WEB_IDENTITY_TOKEN_FILE`/`AWS_ROLE_ARN` (or show the explicit `assume-role-with-web-identity` export). (b) Alarm on the **canary alias/version** metric; loosen to realistic thresholds. These are correctness fixes, not opinion. |
| H3 | **Greenfield-only; monolith under-served; strangler-fig never demonstrated.** "Applies everywhere" is asserted, not shown. Hardest realities (shared SQL Server + stored procs, expand/contract on a real DB, IIS rollback with no Lambda alias) get the least coverage. | Sam, Dana, Jordan | course-wide; `resources/glossary.md`/sessions (strangler-fig mentions) | Work the **strangler-fig pattern once end-to-end** (carve one endpoint out of the monolith). Add at least one **.NET-on-VMs / SQL** worked example or sidebar (expand/contract on a real schema; IIS-style recovery). |
| H4 | **Migration is a destination map, not a journey.** No guidance on what to do with the long-lived branches you *already* have open on day 1; checklist lacks sequencing, effort sizing, and a velocity-dip/"managing up" story; team buy-in / change management (the tech lead's hardest job) is absent. | Jordan, Dana, Sam | `resources/migration-checklist.md`; `exercises/decompose-a-branch.md` | Add to the checklist: day-1 handling of in-flight branches, phase sequencing, rough effort/timeline, expected velocity dip + how to set expectations upward, and a short change-management/buy-in section. |
| H5 | **Governance is not a first-class input.** Manual prod gate framed as "transitional debt to delete," which reads as hostile to mandated/contractual approvals. No segregation-of-duties argument (MR review *as* the control), no audit-trail/evidence mapping, no break-glass/emergency-change path, and no acknowledgement that decoupling deploy/release moves release **authorization** onto the flag flip. | Priya | `sessions/session-1/README.md` (manual-gate framing); `resources/troubleshooting.md` | Add a governance pass: legitimize a deliberate human prod authorization as a permanent control; SoD via MR review; pipeline as audit log; break-glass procedure; how to govern flag flips; a paragraph to hand an auditor/client. |

### MEDIUM — strengthens credibility and clarity

| # | Issue | Raised by | Evidence | Proposed edit |
|---|-------|-----------|----------|---------------|
| M1 | **CD's costs are never debited; the weekly ritual is strawmanned.** Daily integration, stop-the-line interrupts, the 4-hr review SLA, and perpetual flag maintenance are treated as obviously worth it. Flag-debt advice is "be disciplined" — a sermon, not a mechanism (one decompose attempt spawned 4 interacting flags). | Sam | `sessions/session-2` (flag sections); `sessions/session-2/examples/feature-flag.ts:96-101` | Add an honest "what CD costs you" subsection and steelman the weekly ritual before dismantling it. Give flag debt a **mechanism** (CI stale-flag check / mandatory expiry / flag inventory), or name it explicitly as an accepted cost. |
| M2 | **Glossary & clarity quick wins.** `expand/contract` is used 5+× and **cited to the glossary but isn't in it** (only inline in `troubleshooting.md:88`). Also missing/loose: `smoke test`, the `DORA` acronym (metrics defined, acronym never expanded), and `trunk` used before it's tied to `main`. README "What You'll Learn" dumps ~14 terms with no "the sessions teach these" anchor. | Felix | `resources/glossary.md`; `resources/troubleshooting.md:88`; `courses/continuous-delivery-101/README.md` | Add the missing glossary entries; tie trunk↔main on first use; add a one-line anchor to the README term list. Cheap, high-value for beginners. |
| M3 | **Exercises assume cloud-native.** `current-state-assessment.md` breaks on a mixed estate (legacy monolith + new Lambda); `decompose-a-branch.md` only teaches new-service slicing — no brownfield change. | Jordan, Dana | `exercises/current-state-assessment.md`; `exercises/decompose-a-branch.md` | Make the assessment handle mixed estates; add a brownfield decomposition variant (e.g., a monolith change behind expand/contract). |
| M4 | **Team-owned quality should be stated outright.** *Org context (Shane, 2026-06-19): RealManage has no QA team and no QA titles — by CTO direction, delivering teams own their own quality.* That makes the course's "the pipeline is the gate, not a person" stance **correct for us** and dissolves Marcus's premise (no release-manager role to defer to; "Engineering Lead owns timing" is fine). The real gap is that the course never *says* "your team owns its quality" out loud — it's only implied. | Marcus (premise now moot) | `sessions/session-1/README.md`; `CLAUDE.md` | Add one explicit line that quality is owned by the delivering team, not a separate QA function — turning an unstated assumption into a stated principle. Drop the "name a QA owner" idea. |

### NICE-TO-HAVE

| # | Issue | Raised by | Proposed edit |
|---|-------|-----------|---------------|
| N1 | `npm audit --audit-level=high` as a hard merge gate invites teams to route around it. | Riley, Sam | Make it advisory, or scope it; discuss the trade-off. |
| N2 | Build-once-promote has an unaddressed **cross-account S3 artifact-bucket** gap (breaks "same bytes" on multi-account AWS). | Riley | Add a note on shared/cross-account artifact storage. |

---

## Suggested fix order

1. **Quick wins first (hours):** M2 glossary/clarity + H2 technical fixes — small, unambiguous, and H2 is teaching *wrong* mechanics today.
2. **The credibility block (the big lift):** H3 + H4 + M3 are one body of work — add monolith/brownfield/strangler-fig material and make the migration a journey. This is what lifts Sam, Dana, and Jordan.
3. **The two missing chapters:** H1 (release communication) and H5 (governance) — each is essentially a new section; both are concrete adoption blockers for the exact people who approve adoption (Marcus, Priya).
4. **Balance pass:** M1 + M4 — debit CD's costs honestly, and state team-owned quality as an explicit principle. Cheap insurance against the "sales deck" read.

---

## Notes

- **What's already strong (don't touch):** the deploy≠release and CD≠Continuous
  Deployment distinctions, the fail-forward-first recovery model + data-trap, the
  immutable-artifact story, OIDC/no-standing-creds posture, and the beginner
  on-ramp (Felix followed it and the glossary rescued him ~8×). Riley called the
  recovery model "the rare internal CD course that gets it right."
- **Org context (Shane, 2026-06-19):** RealManage has **no QA team and no QA
  titles** — by CTO direction, delivering teams own their own quality. This
  *validates* the course's no-gatekeeper stance and makes Marcus's "QA / Release
  Manager" premise partly moot (see M4). His **release-communication** concern
  (H1) still stands — the weekly client email exists regardless of who owns it.
  For round 2, retire the QA-gatekeeper persona in favor of a **release-comms
  owner** (a tech lead or PM who sends the weekly email).
- **Re-run:** after fixes land, re-run the same 7 personas as `review-2`; they'll
  read their round-1 review and report improved / regressed / still-open.

*Full reviews: `docs/course-feedback/student-{1..7}-*-review-1.md`.*
