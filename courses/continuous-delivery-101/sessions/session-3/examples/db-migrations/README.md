# Example: Database Migrations with DbUp

A deliberately small **schema-as-code** example for Continuous Delivery 101. It shows how a database change reaches production the same way application code does: built once, promoted, and applied by the pipeline — never by hand on a shared database. Like the [Violations API](../violations-api/README.md), it is a **teaching reference**, not a buildable project (there is no NuGet restore here; treat the files as annotated illustrations).

It is deliberately in **C#/.NET against SQL Server** — one of only two C#/.NET examples in the course (the other is [characterization testing](../../../session-2/examples/characterization-test/README.md)) — because database delivery lives in RealManage's established .NET estate, and DbUp is the runner we use there. The migrator is a standalone modern .NET console tool; it targets SQL Server but does not need to match the monolith's .NET Framework version. The CD minimums are identical to the cloud-native pipeline; only the surface differs.

## What it does

A tiny .NET console **migrator** applies ordered SQL scripts to a SQL Server database and records each in a `SchemaVersions` journal so it runs exactly once.

| File | What it teaches |
| ---- | --------------- |
| [`Program.cs`](./Program.cs) | The DbUp runner: connection string in, embedded ordered scripts applied, non-zero exit on failure (a red migration stops the line). |
| `Migrator.csproj` | The scripts are embedded **into** the assembly, so the published artifact is self-contained and the same bytes carry to every environment. |
| [`Scripts/Script0001__create_assessments.sql`](./Scripts/Script0001__create_assessments.sql) | Baseline schema for a fresh (local) database. Idempotent create. |
| [`Scripts/Script0002__expand_add_due_date.sql`](./Scripts/Script0002__expand_add_due_date.sql) | **Expand** step of a parallel change: a new nullable column, backward-compatible. |
| [`Scripts/Script0003__seed_assessment_status.sql`](./Scripts/Script0003__seed_assessment_status.sql) | Baseline (reference) data via an idempotent `MERGE`. |
| [`db-migrations.gitlab-ci.yml`](./db-migrations.gitlab-ci.yml) | Build the migrator **once**, prove it against an ephemeral SQL Server, then promote the same artifact dev → qa → prod, varying only the connection string. |

## Build once, promote, vary only the connection string (#5, #9)

```text
            ┌──────────────── build ONCE ────────────────┐
commit ───▶ │ dotnet publish -c Release -o publish        │  (scripts embedded in the DLL)
            └─────────────────────────────────────────────┘
                              │  the SAME publish/ artifact
        ┌─────────────────────┼─────────────────────┐
        ▼                     ▼                     ▼
   migrate:dev           migrate:qa           migrate:prod
   (dev conn str)        (qa conn str)        (prod conn str, manual timing gate)
        │                     │                     │
   SAME scripts          SAME scripts          SAME scripts
```

The connection string is fetched per environment from AWS Secrets Manager via OIDC at deploy time. The artifact never changes; no human runs DDL against a shared database.

## Recovery: forward-only

DbUp has no down-scripts. You do not roll a migration back — you ship a new forward one (**fail forward**). For reversible change, use **expand/contract**: `Script0002` adds the column as *nullable* so application code can deploy or roll back independently. A later forward script performs the **contract** (drop the old shape) once every reader uses the new one. Rolling code back never rolls data back — keep each step backward-compatible. See [database-delivery](../../../../resources/database-delivery.md).

## Local playground

Develop and test migrations against your **own** database first — the database analog of the personal sandbox stack. A containerized SQL Server is enough:

```yaml
# docker-compose.yml
services:
  sqlserver:
    image: mcr.microsoft.com/mssql/server:2022-latest
    environment:
      ACCEPT_EULA: "Y"
      MSSQL_SA_PASSWORD: "Your_local_dev_password_1" # local only; not a real secret
    ports:
      - "1433:1433"
```

```bash
docker compose up -d
dotnet run --project Migrator.csproj -- \
  "Server=localhost,1433;Database=assessments;User Id=sa;Password=Your_local_dev_password_1;TrustServerCertificate=True"
```

Run it twice: the second run applies nothing (the journal skips applied scripts) — that idempotency is what makes re-running the last good deployment safe.

## How it flows through the pipeline

See [`db-migrations.gitlab-ci.yml`](./db-migrations.gitlab-ci.yml): the migrations first prove out against an ephemeral SQL Server service container (a production-like test, #7 — same engine, not a data clone), the artifact is built once, then promoted dev → qa → prod. Only `prod` keeps a manual gate — a human approving *timing*, not *readiness* the pipeline already proved.
