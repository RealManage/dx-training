# Design — Integrating Database Delivery into Continuous Delivery 101

- **Date:** 2026-06-23
- **Status:** Approved design (pending spec review)
- **Topic:** Add database-schema delivery to the CD 101 course
- **Author:** Shane Gibbons (with Claude)

## Problem

RealManage teams own a monolithic SQL Server database (plus several smaller
databases that hold cross-references to it). There is no automated way to
change schema or baseline (reference) data. The organizational goal is to make
automation the *only* way to change central-database schema — removing direct
engineer DDL/write access to shared `dev`, `qa`, and `prod` — with local
databases as the prerequisite playground for developing migrations. Two
constraints make this hard: lower environments have drifted from production,
and local databases are currently uncommon.

The course teaches CD for application code but treats the database as four
bullets of framing. The course's own persona testing flagged the gap: the
.NET-monolith reviewer (Dana) noted that schema delivery on shared SQL Server
"appears as a sentence and then never again." Schema-change automation
(migrations as code, run by the pipeline, against a production-like local DB)
is genuinely absent from live content.

## Goal and non-goals

**Goal.** Make database delivery concrete as *the CD minimums applied to
schema*, give the monolith owner a worked artifact in their own stack, and name
the end-state (automation-only schema, no direct DDL, local-DB playground) as a
direction — without overloading the three sessions.

**Non-goals.**

- No team-by-team rollout checklist for removing direct DB access. DX owns and
  drives that path; the course states the destination and signals DX ownership,
  not an execution sequence.
- No fourth session and no second worked artifact. Single-DB delivery is shown
  end to end; cross-DB coupling and drift are covered as principles plus
  tactics in the resource.
- No new exercise.

## Decisions (resolved during brainstorming)

| Question | Decision |
| -------- | -------- |
| Placement | Weave into the existing three sessions (resource + one worked example + spine touches + light checklist/assessment), mirroring how `ai-assisted-delivery.md` was threaded. |
| Migration tool | **DbUp** — named by the team. .NET-native, forward-only, journal-tracked; fits the established stack. |
| Hard-parts depth | Principles + tactics in the resource. The worked example covers single-DB delivery end to end; cross-DB coupling and drift are named hazards with concrete tactics, not a second artifact. |
| Access-removal framing | State the end-state as a goal; be honest about today's gaps; signal softly that DX owns the path. No prescribed team rollout sequence. |
| Worked-example shape | A full C#/.NET DbUp console migrator against SQL Server — a deliberate, documented exception to the "examples are TypeScript + SAM" rule, because database delivery lives in the established .NET estate. |

## Thesis

Database delivery is not a new topic. It is the CD minimums you already learned,
applied to schema:

- The pipeline is the only way to change a shared database (CD minimum #2).
- The migrator is an immutable artifact, built once and promoted (minimums #5, #9).
- Lower environments must be production-like (minimum #7).
- Recovery is fail-forward plus expand/contract, because schema is forward-only.

The course states the **end-state goal** — schema and baseline data change only
through automation; no direct human DDL on shared `dev`/`qa`/`prod`; local
databases are the playground — and signals that **DX owns and is driving the
path there**. The course states the destination, not a rollout sequence.

## Why DbUp maps cleanly onto the course

- **Build once, promote, vary only config.** `dotnet publish` builds the
  migrator once; the same artifact runs against `dev → qa → prod`, varying only
  the connection string (config with the artifact, minimum #9). This mirrors the
  `violations-api` SAM promotion story exactly.
- **Idempotent re-runs.** DbUp records applied scripts in a `SchemaVersions`
  journal table and skips them on re-run, so "re-run the last good deployment"
  (RealManage's rollback action) is safe for the migrator too.
- **Forward-only by default.** DbUp has no down-migrations. This reinforces the
  course's fail-forward default: rolling back a schema change is a new forward
  script plus expand/contract, never a hand-edit of shared schema (the database
  analog of "never hand-edit the Lambda alias").

## New artifacts

### 1. `resources/database-delivery.md`

A new resource alongside `ai-assisted-delivery.md`, with `order:` frontmatter
slotting it among the resources. Sections:

1. **The gap.** We automate application deploys but change schema by hand; the
   database is the elephant blocking small batches.
2. **The same minimums, applied to schema.** Map minimums #2, #5, #7, #9 onto
   the database.
3. **The mechanism.** Migrations as code with DbUp; the `SchemaVersions`
   journal; forward-only; run by the pipeline; build-once/promote. Points to the
   worked example.
4. **The end-state goal.** Automation-only schema and baseline data; no direct
   DDL on shared environments; local DBs as the playground. Honest about today's
   gaps (drift, no automation, local DBs uncommon). Short close: DX owns and is
   driving the path.
5. **Hazard — drift.** Baseline production as the source of truth; generate a
   baseline script and mark it already-applied in the journal; reconcile lower
   environments up to it. From then on only migrations move schema forward, so
   drift cannot reaccumulate.
6. **Hazard — cross-DB coupling.** Version each database independently (its own
   DbUp project and journal). Do not add *new* cross-database references. Where a
   cross-DB reference must change, treat the boundary like a service seam and use
   expand/contract with ordered deploys (expand the referenced DB first; contract
   in reverse).
7. **Baseline/reference data.** Idempotent scripts (`MERGE` / `IF NOT EXISTS`),
   versioned alongside schema, run by the same migrator.
8. **Reversibility and recovery.** Forward-only means fail-forward plus
   expand/contract. Cross-link the existing glossary "data trap" caveat (rolling
   code back does not roll data back).

### 2. `sessions/session-3/examples/db-migrations/`

A worked example parallel to `violations-api/` — an annotated teaching
reference, not a buildable project. Session 3 is the right home: the pipeline as
the single path to production and the immutable-artifact/promotion story are its
themes.

| File | What it teaches |
| ---- | --------------- |
| `README.md` | The schema build-once/promote story; the local playground; how it flows through the pipeline. Carries `order:` to slot after `violations-api`. |
| `Migrator.csproj` | A tiny .NET console project referencing DbUp and `Microsoft.Data.SqlClient`. |
| `Program.cs` | The DbUp runner: connection string from arg/env → `DeployChanges.To.SqlDatabase(...).WithScriptsEmbeddedInAssembly(...).LogToConsole().Build()` → `PerformUpgrade()` → non-zero exit on failure (fails the job). Surfaces the journal, forward-only, idempotent re-run. |
| `Scripts/Script0001__expand_add_column.sql` | Expand step: add a nullable column (backward-compatible). |
| `Scripts/Script0002__backfill_and_index.sql` | Backfill and index as a separate forward migration. |
| `Scripts/Script0003__seed_reference_data.sql` | Idempotent (`MERGE` / `IF NOT EXISTS`) baseline-data script. |
| `db-migrations.gitlab-ci.yml` | `build` (`dotnet publish` — build once) then `migrate:dev → qa → prod` running the *same* published artifact, connection string pulled per environment from SSM/Secrets Manager via OIDC. Gated like the rest of the pipeline; no static credentials. |
| Local-playground note (README section or `local-playground.md`) | `docker-compose` running `mcr.microsoft.com/mssql/server` + `dotnet run -- "<localhost connstr>"` — the database analog of the personal sandbox stack. |

Teaching beats to surface: build the migrator once and promote the same artifact
(minimum #9); the journal makes re-running the last good deployment safe;
forward-only means rollback is a new forward script plus expand/contract — never
a hand-edit of shared schema; shared schema changes only through the pipeline,
experimentation happens in the local playground.

## Spine touches (light)

- **`sessions/session-1/README.md`** — a callout near the small-batches / value-
  stream material: stateful database changes are the hardest batch to shrink;
  name the elephant; pointer to the resource.
- **`sessions/session-2/README.md`** — near CI / local-testing material:
  migrations are code, developed and tested in the local-DB playground (the
  database analog of the personal sandbox stack); a failing migration is a red
  build (stop-the-line).
- **`sessions/session-3/README.md`** — near the pipeline / immutable-artifact /
  rollback material: the pipeline is the only path to schema; the immutable
  migrator artifact; fail-forward plus expand/contract for schema; the end-state
  goal and DX-owns-the-path framing; pointers to the example and resource.

## Supporting edits

- **`resources/glossary.md`** — add: schema migration; schema-history table /
  journal (DbUp `SchemaVersions`); forward-only migration; baseline (reference)
  data; environment drift; baseline script; local-DB playground; DbUp.
  Cross-link existing entries (expand/contract, idempotent, system of record,
  reconciliation).
- **`resources/migration-checklist.md`** — two direction-setting items: schema
  and baseline-data changes go through the pipeline (DbUp), not by hand;
  engineers develop and test migrations against a local database before merge.
- **`resources/troubleshooting.md`** — point the existing "stateful change blocks
  small releases" entry to the new resource and to DbUp / expand-contract.
- **`sessions/session-3/exercises/current-state-assessment.md`** — one DB-delivery
  reflection line (are schema and baseline-data changes automated through the
  pipeline, or applied by hand? do engineers have a local DB?). Not a heavy
  rubric.
- **`CLAUDE.md`** (course) — document the scoped C#/.NET exception (the
  `db-migrations` example is intentionally C#/.NET + SQL Server + DbUp because
  database delivery lives in the established estate; do not convert it to
  TypeScript) and the DB end-state framing (state the destination; DX owns the
  path; no team rollout sequence).
- **`README.md`** (course landing) — add the resource and example to the file
  tree; optionally one learning-objective bullet ("Deliver database schema and
  baseline data as code through the pipeline").
- **`site.config.json`** — add `database-delivery.md` to the resources nav with
  `order` and label; add a label override for the `db-migrations` example folder
  (it auto-renders under the sessions directory walk).

## Verification

- `cd site && npm run build` succeeds; CD 101 page count grows by the new
  resource, example folder index, and code views; zero new "links outside the
  published site" beyond the pre-existing ai-101 links.
- `npx markdownlint-cli2` clean (MD013 excepted) on all touched and new Markdown,
  run from the repo root.
- Internal links resolve (resource ↔ example ↔ sessions ↔ glossary).
- `slides/` is untouched and unstaged.

## Open questions

None blocking. The optional learning-objective bullet and the exact `order`
values can be settled during implementation.
