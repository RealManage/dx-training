# Continuous Delivery 101 — Round 7 Synthesis (Material-Change Reopen)

**Date:** 2026-06-23
**HEAD reviewed:** `d591172`
**Inputs:** all eight personas re-reviewed — `student-1-sam…-review-3`, `student-2-dana…-review-7`,
`student-3-priya…-review-7`, `student-4-marcus…-review-3`, `student-5-felix…-review-3`,
`student-6-jordan…-review-3`, `student-7-riley…-review-3`, `student-8-tex…-review-6`.
**Trigger:** Round 6 closed the cycle at reference quality with the caveat *"reopen only if course content
changes materially."* It did: database delivery (the `database-delivery.md` resource + the DbUp
`db-migrations/` worked example + 10 glossary terms + spine touches), the CI/TBD glossary rewrite, branch
by abstraction, DDL, and a rollback-language correction sweep all landed after the close-out.
**Method:** each persona re-read its own history + the Round 6 synthesis, then pressure-tested the
material delta through its lens, swept for regressions, and was licensed to declare reference quality
rather than manufacture nitpicks.

## Verdict: pass, with two confirmed defects to fix

Re-running earned its keep. The new database spine is reference-quality across every lens that touches it
(Dana, Riley, Felix all say so explicitly), **but the round surfaced two genuine correctness defects** —
one introduced by the new DB example, one a pre-existing rollback-sweep gap the new content put back in
view — each independently confirmed by two personas. Both are mechanical fixes.

| Persona | R6 | **R7** | Δ | Bottom line |
| ------- | -- | ------ | - | ----------- |
| Tex — AI Champion | 9.6 | **9.6** | 0 | DB spine reference-quality but authorship-silent; one factory-view sentence + 2 cross-links would make the AI thesis whole. Not docked. |
| Dana — .NET Monolith | 9.5 | **9.5** | 0 | Schema delivery fully worked and survives contact with a real shared SQL Server; R3-8 (prod-like *data*) advanced, not closed — same 201-scope call. |
| Priya — Eng Leader | 10.0 | **9.5** | −0.5 | New DB governance is reference-quality; **docked for the incomplete rollback sweep (R7-2)**. |
| Sam — Senior Skeptic | 8.5 (R2) | **9.5** | +1.0 | Branch by abstraction is real engineering; DB delivery mostly answers the skeptic — **docked for the CI `needs:` defect (R7-1)**. |
| Felix — Junior Dev | 9.0 (R2) | **9.5** | +0.5 | Most jargon-dense addition yet and the best-glossed; one used-before-defined ("journal"). |
| Jordan — Tech Lead | 9.0 (R2) | **9.0** | 0 | Two prior dockers fixed, one new adoption gap spent the gain: **checklist contradicts DX-ownership (R7-3)**. |
| Riley — SRE | 9.5 (R2) | **9.0** | −0.5 | DbUp content technically accurate; **docked for the CI `needs:` defect (R7-1) + stale alias language (R7-2)**. |
| Marcus — Release Mgr | 9.0 (R2) | **9.0** | 0 | Low impact, correctly out of scope; no regressions. Proportionate round. |

Two personas docked, both for the **same two root causes** (R7-1, R7-2) — strong convergence that these
are real, not lens-specific taste.

## Prioritized triage

| ID | Issue | Sev | Raised by | Proposed edit |
| -- | ----- | --- | --------- | ------------- |
| **R7-1** | **`db-migrations.gitlab-ci.yml` breaks build-once/promote.** Only `build:migrator` produces the `publish/` artifact (line 61, "same bytes deploy to every environment"). `migrate:dev` lists it in `needs:` ✓, but `migrate:qa` `needs:[migrate:dev]` and `migrate:prod` `needs:[migrate:qa]` — with `needs:` present, GitLab delivers artifacts *only* from listed jobs, so qa/prod run `dotnet publish/Migrator.dll` against a missing `publish/`. The example that teaches immutability (#5/#9) is broken past dev, or invites a per-env rebuild — the exact violation it warns against. | **HIGH** | Sam #1, Riley #1 (independent) | Add `build:migrator` to both `migrate:qa` and `migrate:prod` `needs:` lists. Keeps dev→qa→prod ordering (via the existing entries); adds the one artifact source. Does **not** re-run build. |
| **R7-2** | **Rollback-correction sweep is incomplete — 3 files still name the forbidden hand-edit.** Worst: `violations-api/template.yaml:86` "Rollback = repoint the alias to the prior version." Also `current-state-pipeline-walkthrough.md:50` ("Lambda alias shifting … as the emergency lever") and `sessions/session-3/examples/.gitlab-ci.yml:226` (comment "canary auto-rollback / alias shift"). Each contradicts the now-canonical `rollback-on-aws.md:54` ("not a hand-edited Lambda alias") one click away. | **MED** | Priya #1 (2), Riley #2 (3) | Reword all three to the canonical framing: the rollback action is re-running the last good GitLab deployment; the canary repoints the `live` alias *automatically* — a human never hand-repoints it. Leave the benign canary alias-error-rate descriptions (`template.yaml:116–174`) untouched. |
| **R7-3** | **`migration-checklist.md:66-67` DB items contradict the DX-owned framing.** They read as unconditional Phase-2 *team* boxes, while `database-delivery.md:32`, `current-state-assessment.md:50` ("note it, don't score it as a personal gap"), `session-3/README.md:126`, and `CLAUDE.md:118` all frame schema-automation/local-DBs as DX-owned direction, not team sprint cadence. The checklist is the artifact teams copy and tick — the one place it pretends the DB items are theirs to drive. | **MED** | Jordan #1 | Mark the two DB items as DX-owned / adopt-as-it-lands (a "blocked-on-DX" qualifier), consistent with the assessment and resource wording. |
| **R7-4** | **"journal" used before it's equated to "schema-history table."** `database-delivery.md` introduces "schema-history table" (~L19) then refers to "the journal" (~L23) without joining them. Only true used-before-defined of the round. | **LOW** | Felix #1 | One-word fix: "…in a schema-history table (the *journal*)…". |
| **R7-5** | **Branch by abstraction is defined but never *worked*** — the Session 2 note + glossary explain *what* and *when* but hand-wave *how callers get switched across the seam* (the load-bearing step). | **LOW** | Felix #2, Jordan #3, Sam #3 | Add one concrete beat (how the switch happens — a flag/config at the seam) to the Session 2 note, or a short worked micro-example. Enhancement, not a defect. |
| **R7-6** | **New DB content is authorship-silent** — no AI angle anywhere in `database-delivery.md` / the example, and no cross-link to `ai-assisted-delivery.md:71` (which already frames "expand/contract — a discipline the agent executes once a human names the steps"). | **LOW** | Tex #1–3 | Add one factory-view sentence (local-DB-prove-before-merge + human-named expand/contract slicing matter *more* when AI authors migrations) + two intra-course links. Additive. |
| **R7-7** | **"production-like" conflates topology with data.** `database-delivery.md:14` and `db-migrations/README.md:67` call an empty/ephemeral SQL Server "production-like" — true for *structure*, not *data*. | NICE | Dana | Optional one-clause clarification (structure-parity, not data-parity). Same root as R3-8. |
| **R7-8** | **`EnsureDatabase` (Program.cs:26) implies server-level create rights** a least-privilege prod login shouldn't hold — mild tension with the "no standing DDL" end-state, though flagged in-comment as a local convenience. | NICE | Sam, Riley | Optional one-line note that shared envs use a pre-created database / least-privilege login. |
| **R7-9** | No cross-link from baseline-data (`database-delivery.md:52-54`) to `communicating-releases.md` for the edge case where a data change alters user-visible behavior. | NICE | Marcus | Optional cross-link. |

## Open / scope — not docked

- **R3-8 / R6-D1 — prod-like *data* for the shared SQL Server.** Dana: schema and *reference* data
  (the `MERGE` in `Script0003`) are now fully worked; prod-like *application* data (subset-vs-clone, PII
  masking, refresh cadence) is still named-as-hard and never worked. The rich new neighbourhood makes the
  hole smaller-feeling but sharper-edged. Remains a defensible 101 boundary / future 201 material — the
  same call upheld in Rounds 3–6.

## Recommendation

1. **Fix R7-1 and R7-2 before anything else** — they are correctness defects in shipped teaching
   artifacts, each confirmed by two personas, each a small mechanical edit. R7-1 in particular undermines
   the exact lesson (build-once/promote) the new example exists to teach.
2. **R7-3** is a one-qualifier edit that removes a real internal contradiction; worth doing in the same pass.
3. **R7-4** is a one-word fix; bundle it.
4. **R7-5 / R7-6** are worthwhile enhancements (branch-by-abstraction "how," and the AI/DB cross-link),
   not defects — apply if the scope is welcome.
5. **R7-7 / R7-8 / R7-9** are optional polish.
6. **R3-8** stays a scope decision, not a fix.

Course changes await human review of this triage (per the testing-plan process). Resolution to be recorded
below once applied.

## Resolution (applied 2026-06-23)

All findings fixed except R3-8 (held as a deliberate 201-scope boundary, per the maintainer).

- **R7-1 — fixed.** `db-migrations.gitlab-ci.yml`: `migrate:qa` and `migrate:prod` now list `build:migrator`
  in `needs:` (`needs: [migrate:dev, build:migrator]` / `[migrate:qa, build:migrator]`). The prior
  job in each chain still enforces dev→qa→prod ordering; adding `build:migrator` delivers the one
  published artifact to every promote stage. Inline comments state the reasoning so the example now
  *teaches* the GitLab `needs:`/artifact rule instead of violating it. Build re-run: clean.
- **R7-2 — fixed (all three spots).** `violations-api/template.yaml:86` now reads "the canary repoints
  the alias automatically; rollback is re-running the last good GitLab deployment, never a hand-edited
  alias." `current-state-pipeline-walkthrough.md:50` and `sessions/session-3/examples/.gitlab-ci.yml`
  recovery comment reworded to the canonical "re-run the last good deployment; canary + CloudFormation
  auto-rollback." Repo-wide grep for `repoint the alias` / `alias shift(ing)` / `shift the alias`
  returns zero hits in course content. Benign canary alias-error-rate descriptions left intact.
- **R7-3 — fixed (reframed, not demoted).** Per the maintainer: including the data items in the team
  checklist does **not** absolve DX of laying the groundwork, and teams remain responsible for
  committing to the practices. Added a "shared handoff" note under the two Phase-2 DB items
  (`migration-checklist.md`): DX lays the groundwork (prod baseline, local-DB workflow, eventual access
  removal); the team owns adopting the practices as it lands. Resolves Jordan's contradiction without
  reframing the items as "not yours."
- **R7-4 — fixed.** `database-delivery.md` now equates the terms at first use: "schema-history table
  (the *journal*)," so "journal" is defined before it is used.
- **R7-5 — fixed.** The Session 2 branch-by-abstraction note and the glossary entry now name the
  load-bearing step: "flip which implementation the seam resolves to (a one-line wiring change, often
  itself behind a flag)" — the *how* of switching callers across the seam.
- **R7-6 — fixed.** `database-delivery.md` gained a "When an agent writes the migration" callout
  (local-DB-prove-before-merge and human-named expand/contract matter *more* under AI authorship) +
  link to `ai-assisted-delivery.md`; the latter's expand/contract bullet now links back to
  `database-delivery.md`. Bidirectional cross-link established.
- **R7-7 — fixed.** `database-delivery.md` #7 now says "production-like in engine and schema, though
  matching production *data* is a separate, harder problem"; `db-migrations/README.md` qualifies the
  service container as "same engine, not a data clone."
- **R7-8 — fixed.** `Program.cs` `EnsureDatabase` comment notes shared envs use a pre-created database
  and a least-privilege login (no `CREATE DATABASE` / standing DDL), so the line no-ops there.
- **R7-9 — fixed.** `database-delivery.md` baseline-data section cross-links `communicating-releases.md`
  for reference-data changes that alter user-visible behavior.
- **R3-8 / R6-D1 — held.** Prod-like *data* for the shared SQL Server remains a deliberate 201-scope
  boundary, unchanged.

Verification: markdownlint clean (MD013 excepted) on all touched Markdown; site rebuilds at 23 pages /
13 code views / 11 folder indexes with zero CD-101 link breakage; new cross-links resolve; the
`needs:` fix is present in the rendered code view.
