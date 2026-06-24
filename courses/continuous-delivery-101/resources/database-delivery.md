---
order: 35
---
# Database Delivery — Schema as Code, Through the Pipeline

We automate how application code reaches production: built once, promoted, deployed by a pipeline no human deploys around. Then we change the database by hand — someone opens a SQL client against shared `dev`, `qa`, or `prod` and runs `ALTER TABLE`. That hand-edit is the database equivalent of editing the running Lambda: an out-of-band change that bypasses the pipeline (CD minimum #2) and drifts from any source of truth.

The database is usually *the* reason small batches feel impossible. This guide treats schema and baseline data as what they are — code, delivered through the same pipeline, under the same minimums.

## The same minimums, applied to schema

- **#2 — the pipeline is the only way to deploy.** Schema changes run from the pipeline, not a laptop. No human holds DDL rights on a shared database.
- **#5 — immutable artifacts.** The migration runner is built once and promoted; the *same* scripts run against every environment.
- **#7 — a production-like test environment.** Migrations prove out against a real database (a local one, then `qa`) before `prod` sees them — production-like in engine and schema, though matching production *data* is a separate, harder problem.
- **#9 — config travels with the artifact.** The only thing that changes between environments is the connection string, supplied at deploy time — never baked in, never typed by hand.

## The mechanism: migrations as code

A *migration* is a small, ordered SQL script checked into the repo next to the code that needs it. A migration runner applies the scripts an environment has not seen yet, in order, and records each one in a **schema-history table** (the *journal*) so it never runs twice. RealManage uses **DbUp** (a .NET library): it reads ordered scripts, tracks them in a `SchemaVersions` table, and applies only the new ones.

Two properties matter for CD:

- **Idempotent.** The journal means re-running is safe — already-applied scripts are skipped. So *re-running the last good deployment* (our rollback action) is safe for schema too.
- **Forward-only.** DbUp has no down-migrations. You do not "undo" a schema change; you ship a new forward script. Reversibility comes from **expand/contract**, not from rolling the database back.

The worked example builds this end to end: [Database Migrations (DbUp)](../sessions/session-3/examples/db-migrations/README.md).

## The destination

The goal is simple to state: **schema and baseline data change only through automation.** No engineer has standing write/DDL access to shared `dev`, `qa`, or `prod`. Local databases are where you develop and test a migration before it merges — the database analog of the personal sandbox stack.

We are not there yet, and that is worth naming honestly: lower environments have drifted from production, there is no schema automation today, and local databases are still uncommon. Those gaps are real prerequisites, not afterthoughts. **DX owns and is driving the path** from here to the destination — standing up the runner-in-pipeline, the local-database workflow, and the eventual removal of direct access. This guide describes where we are going and why; your team's part is to adopt local databases and route schema changes through the pipeline as that path lands.

## Hazard: drift between environments

If `prod`, `qa`, and `dev` have diverged, a migration that works in one can fail in another. You cannot automate on top of unknown state.

The fix is to establish a single source of truth and converge to it:

1. **Baseline production.** Script the current `prod` schema as the starting migration and mark it already-applied in the journal there (DbUp's `MarkAsExecuted`), so the runner never tries to recreate existing objects.
2. **Reconcile lower environments up to the baseline.** Bring `dev` and `qa` to match `prod`, not the other way around — `prod` is the truth.
3. **Lock the door.** From then on, only migrations move schema forward. Because every change now flows through the journal, drift cannot quietly reaccumulate.

## Hazard: cross-database references

Our estate is one large database plus smaller ones that hold references across the boundary. That coupling is what makes change scary. Tactics:

- **Version each database independently.** Each database gets its own migration project and its own `SchemaVersions` journal. Do not manage several databases from one tangled script set.
- **Add no *new* cross-database references.** New coupling is the thing you can still prevent. Treat the boundary like a service seam (see [strangler fig in practice](../sessions/session-3/examples/strangler-fig-violations.md)).
- **Cross the boundary with expand/contract and ordered deploys.** When a shared reference must change, expand the referenced database first, migrate readers, then contract — deploying the databases in dependency order. Each step stays backward-compatible.

## Baseline (reference) data

Reference data — status codes, lookup tables, seed rows — is delivered exactly like schema: versioned scripts, run by the same runner. Write them **idempotently** (`MERGE` or `IF NOT EXISTS`) so the same script converges to the same rows whether the table is empty or already seeded. That is what makes re-running a deployment safe for data as well as structure. When a reference-data change alters what users see, treat the flip as a release and communicate it — see [communicating releases](communicating-releases.md).

## Reversibility and recovery

Because migrations are forward-only, recovery follows the course's default: **fail forward.** A bad schema change is corrected by a new forward migration, shipped through the pipeline in minutes — never by a hand-edit on a shared database (the database analog of "never hand-edit the Lambda alias").

For changes that must be reversible, **expand/contract** is the answer: keep every step backward-compatible so application code can roll back independently of the schema. Mind the **data trap** — rolling code back does not roll data back; only backward-compatible steps keep both directions safe. (See the [glossary](glossary.md) entries for expand/contract and the data trap.)

> **When an agent writes the migration.** AI strips away the friction that used to make a risky `ALTER TABLE` *feel* risky, so these guardrails matter more, not less — especially two: prove every migration against a local database before it merges, and have a human name the expand/contract steps the agent then executes. See [AI-assisted delivery](ai-assisted-delivery.md).
