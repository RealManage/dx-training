# Database Delivery Integration — Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Integrate database-schema delivery (DbUp schema-as-code, run by the pipeline) into Continuous Delivery 101 as the CD minimums applied to schema — without expanding the three-session arc.

**Architecture:** One new resource (`database-delivery.md`), one new worked example (a C#/.NET DbUp migrator under Session 3), light spine touches in all three session READMEs, glossary terms, and minimal checklist/assessment/wiring edits. Everything frames database delivery as the *same* minimums (#2, #5, #7, #9) applied to schema; states the end-state goal (automation-only schema, no direct DDL, local-DB playground) as direction owned by DX; and covers the two hard constraints (drift, cross-DB coupling) as principles plus tactics in the resource.

**Tech Stack:** Markdown (markdownlint, MD013 excepted); the repo's static site builder (`site/build.mjs`, nav-driven by `site.config.json`); annotated C#/.NET + DbUp + SQL Server + GitLab CI example files (teaching references, not built).

---

## Conventions for every task

**This is a documentation task, not a code project.** There is no unit-test suite. The verification loop that replaces red/green is:

- **Build:** `cd /home/shane/src/realmanage/tools/dx/dx-training/site && npm run build`
  Expect: CD 101 builds successfully; page/code-view/folder-index counts grow by the new files; the link-checker reports **no new** "links outside the published site" beyond the three pre-existing **ai-101** links (week-1 support README, track-exercise-template, support-track). Zero CD breakage = success.
- **Lint:** from the repo root (never from `site/` — relative globs break):
  `cd /home/shane/src/realmanage/tools/dx/dx-training && npx markdownlint-cli2 "courses/continuous-delivery-101/**/*.md"`
  Expect: clean (MD013 is disabled in `.markdownlint.json`).

**CWD gotcha:** Bash CWD persists between calls. After `cd site` for a build, you MUST `cd /home/shane/src/realmanage/tools/dx/dx-training` before linting or grepping `courses/`.

**Commits are user-gated.** The repo owner's standing rule is *commit only when explicitly asked* — this overrides the writing-plans default of per-task commits. So each task below ends with a **build + lint checkpoint**, not a commit. Commits happen once, at the end, in Task 7, and only on the user's go-ahead. Never `git add -A` — it sweeps in the pinned, untracked `slides/` directory. Stage explicit paths only.

**Pinned, do not touch:** `courses/continuous-delivery-101/slides/` and anything under `courses/continuous-delivery-101/docs/` (historical records; this plan and spec live there but no course content references them).

---

## File structure

**Create:**

- `courses/continuous-delivery-101/resources/database-delivery.md` — the core resource.
- `courses/continuous-delivery-101/sessions/session-3/examples/db-migrations/README.md` — example overview.
- `courses/continuous-delivery-101/sessions/session-3/examples/db-migrations/Program.cs` — DbUp runner.
- `courses/continuous-delivery-101/sessions/session-3/examples/db-migrations/Migrator.csproj` — project.
- `courses/continuous-delivery-101/sessions/session-3/examples/db-migrations/Scripts/Script0001__create_assessments.sql`
- `courses/continuous-delivery-101/sessions/session-3/examples/db-migrations/Scripts/Script0002__expand_add_due_date.sql`
- `courses/continuous-delivery-101/sessions/session-3/examples/db-migrations/Scripts/Script0003__seed_assessment_status.sql`
- `courses/continuous-delivery-101/sessions/session-3/examples/db-migrations/db-migrations.gitlab-ci.yml`

**Modify:**

- `sessions/session-1/README.md` — one callout + one resources-list entry.
- `sessions/session-2/README.md` — one callout + one resources-list entry.
- `sessions/session-3/README.md` — one callout + two resources-list entries (example + resource).
- `resources/glossary.md` — new terms in the "Migration and data" section.
- `resources/migration-checklist.md` — two Phase-2 items.
- `resources/troubleshooting.md` — extend the stateful-change entry.
- `sessions/session-3/exercises/current-state-assessment.md` — one DB reflection note.
- `CLAUDE.md` — scoped C#/.NET exception + DB end-state framing.
- `README.md` — file-tree line + one learning-objective bullet.
- `site.config.json` — two `labels` entries.

(All paths relative to `courses/continuous-delivery-101/`.)

---

### Task 1: New resource — `resources/database-delivery.md`

**Files:**

- Create: `courses/continuous-delivery-101/resources/database-delivery.md`

- [ ] **Step 1: Create the resource file with this exact content**

````markdown
---
order: 35
---
# Database Delivery — Schema as Code, Through the Pipeline

We automate how application code reaches production: built once, promoted, deployed by a pipeline no human deploys around. Then we change the database by hand — someone opens a SQL client against shared `dev`, `qa`, or `prod` and runs `ALTER TABLE`. That hand-edit is the database equivalent of editing the running Lambda: an out-of-band change that bypasses the pipeline (CD minimum #2) and drifts from any source of truth.

The database is usually *the* reason small batches feel impossible. This guide treats schema and baseline data as what they are — code, delivered through the same pipeline, under the same minimums.

## The same minimums, applied to schema

- **#2 — the pipeline is the only way to deploy.** Schema changes run from the pipeline, not a laptop. No human holds DDL rights on a shared database.
- **#5 — immutable artifacts.** The migration runner is built once and promoted; the *same* scripts run against every environment.
- **#7 — a production-like test environment.** Migrations prove out against a real database (a local one, then `qa`) before `prod` sees them.
- **#9 — config travels with the artifact.** The only thing that changes between environments is the connection string, supplied at deploy time — never baked in, never typed by hand.

## The mechanism: migrations as code

A *migration* is a small, ordered SQL script checked into the repo next to the code that needs it. A migration runner applies the scripts an environment has not seen yet, in order, and records each one in a **schema-history table** so it never runs twice. RealManage uses **DbUp** (a .NET library): it reads ordered scripts, tracks them in a `SchemaVersions` table, and applies only the new ones.

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

Reference data — status codes, lookup tables, seed rows — is delivered exactly like schema: versioned scripts, run by the same runner. Write them **idempotently** (`MERGE` or `IF NOT EXISTS`) so the same script converges to the same rows whether the table is empty or already seeded. That is what makes re-running a deployment safe for data as well as structure.

## Reversibility and recovery

Because migrations are forward-only, recovery follows the course's default: **fail forward.** A bad schema change is corrected by a new forward migration, shipped through the pipeline in minutes — never by a hand-edit on a shared database (the database analog of "never hand-edit the Lambda alias").

For changes that must be reversible, **expand/contract** is the answer: keep every step backward-compatible so application code can roll back independently of the schema. Mind the **data trap** — rolling code back does not roll data back; only backward-compatible steps keep both directions safe. (See the [glossary](glossary.md) entries for expand/contract and the data trap.)
````

- [ ] **Step 2: Build the site**

Run: `cd /home/shane/src/realmanage/tools/dx/dx-training/site && npm run build`
Expected: success; CD 101 resource count grows by one; no new external-link warnings beyond the three ai-101 ones.

- [ ] **Step 3: Lint**

Run: `cd /home/shane/src/realmanage/tools/dx/dx-training && npx markdownlint-cli2 "courses/continuous-delivery-101/resources/database-delivery.md"`
Expected: clean.

- [ ] **Step 4: Checkpoint** — confirm the new resource appears under Resources in the built nav and its internal links (to the example, strangler-fig, glossary) resolve. Do not commit yet.

---

### Task 2: New worked example — `sessions/session-3/examples/db-migrations/`

**Files:**

- Create: `.../db-migrations/Program.cs`
- Create: `.../db-migrations/Migrator.csproj`
- Create: `.../db-migrations/Scripts/Script0001__create_assessments.sql`
- Create: `.../db-migrations/Scripts/Script0002__expand_add_due_date.sql`
- Create: `.../db-migrations/Scripts/Script0003__seed_assessment_status.sql`
- Create: `.../db-migrations/db-migrations.gitlab-ci.yml`
- Create: `.../db-migrations/README.md`

- [ ] **Step 1: Create `Program.cs`**

```csharp
using System;
using System.Reflection;
using DbUp;
using DbUp.Engine;

// Teaching reference for CD 101 — NOT a restored/buildable project.
// Build ONCE in CI (`dotnet publish`), then run the SAME published artifact against
// dev -> qa -> prod, passing only the connection string. Config travels with the
// artifact (CD minimum #9); the bytes never change between environments.
internal static class Program
{
    private static int Main(string[] args)
    {
        // The ONE thing that varies per environment: the connection string.
        // In CI it comes from Secrets Manager via OIDC, never a baked-in value.
        var connectionString =
            args.Length > 0 ? args[0] : Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            Console.Error.WriteLine("Usage: Migrator <connection-string>  (or set DB_CONNECTION_STRING)");
            return 2;
        }

        // Safe no-op if the database already exists; convenient for a fresh local DB.
        EnsureDatabase.For.SqlDatabase(connectionString);

        var upgrader = DeployChanges.To
            .SqlDatabase(connectionString)
            // Ordered .sql files compiled INTO the assembly as embedded resources.
            // DbUp records each applied script in the SchemaVersions journal and skips
            // it next run — so re-running the last good deployment is safe (idempotent).
            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
            .LogToConsole()
            .Build();

        DatabaseUpgradeResult result = upgrader.PerformUpgrade();

        if (!result.Successful)
        {
            // Non-zero exit FAILS the pipeline job — a red migration stops the line,
            // exactly like a failing build. The pipeline, not a human, gates promotion.
            Console.Error.WriteLine(result.Error);
            return 1;
        }

        Console.WriteLine("Schema up to date.");
        return 0;
    }
}
```

- [ ] **Step 2: Create `Migrator.csproj`**

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <!-- Teaching reference for CD 101. A real migrator would `dotnet restore` these. -->
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
    <Nullable>enable</Nullable>
    <AssemblyName>Migrator</AssemblyName>
  </PropertyGroup>

  <!-- dbup-sqlserver pulls in DbUp.Engine and the SQL Server provider. -->
  <ItemGroup>
    <PackageReference Include="dbup-sqlserver" Version="5.0.40" />
  </ItemGroup>

  <!-- Compile the ordered SQL files INTO the assembly so the published artifact is
       self-contained: the same bytes carry the scripts to every environment. -->
  <ItemGroup>
    <EmbeddedResource Include="Scripts\*.sql" />
  </ItemGroup>

</Project>
```

Note: the migrator is a standalone modern .NET console tool. It targets SQL Server but does **not** need to match the monolith's .NET Framework version — the migrator is decoupled from the app it serves. State this in the README (Step 7).

- [ ] **Step 3: Create `Scripts/Script0001__create_assessments.sql`**

```sql
-- Baseline schema for a fresh database (the local playground starts empty).
-- Idempotent create: safe even though DbUp's journal already prevents re-application.
IF OBJECT_ID('dbo.Assessments', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Assessments (
        AssessmentId INT          NOT NULL PRIMARY KEY,
        HomeownerId  INT          NOT NULL,
        AmountCents  INT          NOT NULL,
        Status       VARCHAR(20)  NOT NULL
    );
END;

IF OBJECT_ID('dbo.AssessmentStatus', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.AssessmentStatus (
        Code        VARCHAR(20) NOT NULL PRIMARY KEY,
        DisplayName VARCHAR(50) NOT NULL
    );
END;
```

- [ ] **Step 4: Create `Scripts/Script0002__expand_add_due_date.sql`**

```sql
-- EXPAND (parallel change, step 1): add DueDate as NULLABLE so code that predates the
-- column keeps inserting rows without it. Backward-compatible -> deployable any time,
-- and the app can roll back independently of this schema change.
IF NOT EXISTS (
    SELECT 1 FROM sys.columns
    WHERE object_id = OBJECT_ID('dbo.Assessments') AND name = 'DueDate'
)
BEGIN
    ALTER TABLE dbo.Assessments ADD DueDate DATE NULL;
END;

-- CONTRACT (step 3) happens LATER, as its own forward migration, only after every
-- reader/writer uses DueDate. DbUp is forward-only: there is no down-script.
-- Reversibility comes from expand/contract, not from rolling the database back.
```

- [ ] **Step 5: Create `Scripts/Script0003__seed_assessment_status.sql`**

```sql
-- Baseline (reference) data, versioned and shipped exactly like schema. MERGE makes it
-- idempotent: the same script run against an empty or already-seeded database converges
-- to the same rows, so re-running the last good deployment never duplicates data.
MERGE dbo.AssessmentStatus AS target
USING (VALUES
    ('pending', 'Pending'),
    ('paid',    'Paid'),
    ('overdue', 'Overdue')
) AS source (Code, DisplayName)
ON target.Code = source.Code
WHEN MATCHED THEN
    UPDATE SET DisplayName = source.DisplayName
WHEN NOT MATCHED THEN
    INSERT (Code, DisplayName) VALUES (source.Code, source.DisplayName);
```

- [ ] **Step 6: Create `db-migrations.gitlab-ci.yml`**

```yaml
# =============================================================================
# Database migrations — DbUp, commit -> production (CD 101 worked example)
# =============================================================================
# Same shape as the app pipeline: build the migrator artifact ONCE, then promote
# the SAME bytes through dev -> qa -> prod, varying ONLY the connection string.
# The migrator is forward-only and idempotent (DbUp's SchemaVersions journal), so
# re-running the last good deployment is a safe rollback action.
#
# Auth: GitLab OIDC, no static AWS credentials. The DB connection string is pulled
# per-environment from AWS Secrets Manager at deploy time — never baked into the
# artifact, never typed by a human against a shared database.
# =============================================================================

stages:
  - validate
  - build
  - dev
  - qa
  - prod

variables:
  SERVICE_NAME: db-assessments-migrations

default:
  image: mcr.microsoft.com/dotnet/sdk:8.0 # pin by digest before merging

.aws_oidc:
  id_tokens:
    GITLAB_OIDC_TOKEN: { aud: "https://gitlab.com" }
  before_script:
    - echo "$GITLAB_OIDC_TOKEN" > /tmp/gitlab-oidc-token
    - export AWS_WEB_IDENTITY_TOKEN_FILE=/tmp/gitlab-oidc-token
    - export AWS_ROLE_ARN="arn:aws:iam::${AWS_ACCOUNT}:role/gitlab-runner-role-${CI_ENVIRONMENT_NAME}"
    - export AWS_ROLE_SESSION_NAME="ci-${CI_PIPELINE_ID}"
    - aws sts get-caller-identity # fail fast if the role or its trust policy is wrong

# validate: prove the migrations APPLY against a real, ephemeral SQL Server before any
# shared environment sees them — a production-like test (#7) for schema.
validate:migrations:
  stage: validate
  services:
    - name: mcr.microsoft.com/mssql/server:2022-latest
      alias: sqlserver
  variables:
    ACCEPT_EULA: "Y"
    MSSQL_SA_PASSWORD: "Your_local_dev_password_1" # ephemeral CI database only; not a real secret
  script:
    - dotnet publish Migrator.csproj -c Release -o publish
    - >
      dotnet publish/Migrator.dll
      "Server=sqlserver;Database=assessments;User Id=sa;Password=$MSSQL_SA_PASSWORD;TrustServerCertificate=True"

# build: assemble the IMMUTABLE migrator artifact ONCE on main; promote it as-is below.
build:migrator:
  stage: build
  rules:
    - if: $CI_COMMIT_BRANCH == $CI_DEFAULT_BRANCH
  script:
    - dotnet publish Migrator.csproj -c Release -o publish
  artifacts:
    paths: ["publish/"] # the same bytes deploy to every environment
    expire_in: 30 days

# Reusable migrate step. Each environment runs the SAME artifact; only the connection
# string differs, fetched at deploy time from Secrets Manager.
.migrate:
  extends: .aws_oidc
  rules:
    - if: $CI_COMMIT_BRANCH == $CI_DEFAULT_BRANCH
  script:
    - >
      export DB_CONNECTION_STRING="$(aws secretsmanager get-secret-value
      --secret-id ${CI_ENVIRONMENT_NAME}/db-assessments/connection-string
      --query SecretString --output text)"
    - dotnet publish/Migrator.dll "$DB_CONNECTION_STRING"

migrate:dev:
  extends: .migrate
  stage: dev
  environment: { name: dev }
  needs: [validate:migrations, build:migrator]
  # AUTO on green — the pipeline decides releasability.

migrate:qa:
  extends: .migrate
  stage: qa
  environment: { name: qa }
  needs: [migrate:dev]
  # AUTO after dev. Promotes the SAME artifact.

migrate:prod:
  extends: .migrate
  stage: prod
  environment: { name: prod }
  needs: [migrate:qa]
  when: manual # ONLY gate: a human approves TIMING, not readiness the pipeline proved
  allow_failure: false
```

- [ ] **Step 7: Create `README.md`**

````markdown
# Example: Database Migrations with DbUp

A deliberately small **schema-as-code** example for Continuous Delivery 101. It shows how a database change reaches production the same way application code does: built once, promoted, and applied by the pipeline — never by hand on a shared database. Like the [Violations API](../violations-api/README.md), it is a **teaching reference**, not a buildable project (there is no NuGet restore here; treat the files as annotated illustrations).

It is deliberately in **C#/.NET against SQL Server** — the one example in this course that is — because database delivery lives in RealManage's established .NET estate, and DbUp is the runner we use there. The migrator is a standalone modern .NET console tool; it targets SQL Server but does not need to match the monolith's .NET Framework version. The CD minimums are identical to the cloud-native pipeline; only the surface differs.

## What it does

A tiny .NET console **migrator** applies ordered SQL scripts to a SQL Server database and records each in a `SchemaVersions` journal so it runs exactly once.

| File | What it teaches |
| ---- | --------------- |
| [`Program.cs`](./Program.cs) | The DbUp runner: connection string in, embedded ordered scripts applied, non-zero exit on failure (a red migration stops the line). |
| [`Migrator.csproj`](./Migrator.csproj) | The scripts are embedded **into** the assembly, so the published artifact is self-contained and the same bytes carry to every environment. |
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

DbUp has no down-scripts. You do not roll a migration back — you ship a new forward one (**fail forward**). For reversible change, use **expand/contract**: `Script0002` adds the column as *nullable* so application code can deploy or roll back independently. A later forward script performs the **contract** (drop the old shape) once every reader uses the new one. Rolling code back never rolls data back — keep each step backward-compatible. See [database-delivery](../../../resources/database-delivery.md).

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

See [`db-migrations.gitlab-ci.yml`](./db-migrations.gitlab-ci.yml): the migrations first prove out against an ephemeral SQL Server service container (a production-like test, #7), the artifact is built once, then promoted dev → qa → prod. Only `prod` keeps a manual gate — a human approving *timing*, not *readiness* the pipeline already proved.
````

- [ ] **Step 8: Build the site**

Run: `cd /home/shane/src/realmanage/tools/dx/dx-training/site && npm run build`
Expected: success; a new `db-migrations` folder index renders under Session 3's examples; code views appear for `Program.cs`, `Migrator.csproj`, the three `.sql` files, and `db-migrations.gitlab-ci.yml`; no new external-link warnings.

- [ ] **Step 9: Lint**

Run: `cd /home/shane/src/realmanage/tools/dx/dx-training && npx markdownlint-cli2 "courses/continuous-delivery-101/sessions/session-3/examples/db-migrations/README.md"`
Expected: clean.

- [ ] **Step 10: Checkpoint** — confirm the example folder appears under Session 3 in the nav and the README's internal links (to sibling files, the resource, violations-api) resolve. Do not commit yet.

---

### Task 3: Spine touches — session READMEs

**Files:**

- Modify: `sessions/session-1/README.md`
- Modify: `sessions/session-2/README.md`
- Modify: `sessions/session-3/README.md`

- [ ] **Step 1: Read `sessions/session-1/README.md`**, locate the heading line `#### 2.3 The evidence (5 minutes)`.

- [ ] **Step 2: Insert the S1 callout** — replace the heading `#### 2.3 The evidence (5 minutes)` with the block below followed by the original heading:

```markdown
> **The elephant: the database.** The hardest batch to shrink is a schema change. Teams accept tiny code deploys and then bundle a quarter's worth of database changes into one dreaded release, because "the database can't be done incrementally." It can — schema and baseline data delivered as code, through the pipeline, in small backward-compatible steps. We return to this in [Database Delivery](../../resources/database-delivery.md); name it now as the constraint it usually is.

#### 2.3 The evidence (5 minutes)
```

- [ ] **Step 3: Add the S1 resources-list entry** — under `## 📚 Resources for This Session`, add this bullet to the existing list (read the section first; append it):

```markdown
- [Database Delivery](../../resources/database-delivery.md) — why the database is the elephant blocking small batches, and the shape of the fix
```

- [ ] **Step 4: Read `sessions/session-2/README.md`**, locate the heading line `#### 4.2 The quality gates that protect trunk (10 minutes)`.

- [ ] **Step 5: Insert the S2 callout** — replace that heading with the block below followed by the original heading:

```markdown
> **Migrations are code, too.** A database migration is reviewed, tested, and merged like any change. You develop and test it against a **local database** — the database analog of the personal sandbox stack — before it merges, and a migration that fails to apply is a **red build** that stops the line. See [Database Delivery](../../resources/database-delivery.md).

#### 4.2 The quality gates that protect trunk (10 minutes)
```

- [ ] **Step 6: Add the S2 resources-list entry** — under `## 📚 Resources for This Session`, add:

```markdown
- [Database Delivery](../../resources/database-delivery.md) — developing and testing schema migrations locally before merge
```

- [ ] **Step 7: Read `sessions/session-3/README.md`**, locate the heading line `### 6. Workshop: Read the Target Pipeline & Plan the Migration (30 minutes)`.

- [ ] **Step 8: Insert the S3 callout** — replace that heading with the block below followed by the original heading:

```markdown
#### 5.3 The database is no exception (bonus)

Everything in this session applies to schema. The pipeline is the **only** way to change a shared database; the migration runner is an **immutable artifact** built once and promoted (the *same* scripts run against dev → qa → prod, varying only the connection string); and recovery is **forward-only** — a bad migration is fixed by a new forward script (fail forward) plus expand/contract, never a hand-edit on a shared database. The end-state we are driving toward: schema and baseline data change only through automation, with local databases as the playground and no standing human DDL access. **DX owns that path**; here we make the mechanism concrete.

Worked end to end in [Database Migrations (DbUp)](./examples/db-migrations/README.md); the principles, drift, and cross-database tactics are in [Database Delivery](../../resources/database-delivery.md).

### 6. Workshop: Read the Target Pipeline & Plan the Migration (30 minutes)
```

- [ ] **Step 9: Add the S3 resources-list entries** — under `## 📚 Resources for This Session`, add both:

```markdown
- [Database Migrations (DbUp)](./examples/db-migrations/README.md) — schema as code, built once and promoted through the pipeline
- [Database Delivery](../../resources/database-delivery.md) — the minimums applied to schema; drift, cross-database coupling, and the end-state
```

- [ ] **Step 10: Build the site**

Run: `cd /home/shane/src/realmanage/tools/dx/dx-training/site && npm run build`
Expected: success; no new external-link warnings.

- [ ] **Step 11: Lint**

Run: `cd /home/shane/src/realmanage/tools/dx/dx-training && npx markdownlint-cli2 "courses/continuous-delivery-101/sessions/**/README.md"`
Expected: clean.

- [ ] **Step 12: Checkpoint** — verify the three callouts render and every new link resolves. Do not commit yet.

---

### Task 4: Glossary terms

**Files:**

- Modify: `resources/glossary.md`

- [ ] **Step 1: Read `resources/glossary.md`**, locate the heading line `## AWS and RealManage specifics`.

- [ ] **Step 2: Insert new terms** — replace the heading `## AWS and RealManage specifics` with the block below followed by the original heading (this adds the terms to the end of the existing "Migration and data" section):

```markdown
**Schema migration**
A small, ordered, version-controlled change to a database's structure (or baseline data), applied by a runner rather than by hand. The database analog of a code commit moving through the pipeline.

**Schema-history table (journal)**
The table a migration runner uses to record which scripts an environment has already applied, so each runs exactly once and re-running is safe. In DbUp it is `SchemaVersions`.

**Forward-only migration**
A migration scheme with no automated "down" step: you do not undo a change, you ship a new forward change. Reversibility comes from expand/contract, not from rolling the database back. DbUp is forward-only.

**Baseline (reference) data**
Lookup/seed rows (status codes, types, defaults) shipped and versioned exactly like schema, via idempotent scripts (`MERGE` / `IF NOT EXISTS`) so re-running converges to the same rows.

**Environment drift**
The state where `prod`, `qa`, and `dev` schemas have diverged, so a migration that works in one may fail in another. Resolved by baselining production as the source of truth and reconciling lower environments up to it.

**Baseline script**
The migration that captures an existing database's current schema as a starting point, marked already-applied (DbUp's `MarkAsExecuted`) so the runner does not try to recreate existing objects.

**Local-database playground**
A developer's own database (e.g., a containerized SQL Server) used to develop and test migrations before merge — the database analog of the personal/sandbox stack.

**DbUp**
A .NET library that applies ordered SQL scripts to a database and tracks them in a `SchemaVersions` journal. Forward-only and idempotent. RealManage's migration runner for SQL Server. See [Database Delivery](database-delivery.md).

## AWS and RealManage specifics
```

- [ ] **Step 3: Build and lint**

Run: `cd /home/shane/src/realmanage/tools/dx/dx-training/site && npm run build`
Then: `cd /home/shane/src/realmanage/tools/dx/dx-training && npx markdownlint-cli2 "courses/continuous-delivery-101/resources/glossary.md"`
Expected: build success; lint clean.

- [ ] **Step 4: Checkpoint** — confirm the new terms render in the glossary and the DbUp → database-delivery link resolves. Do not commit yet.

---

### Task 5: Supporting edits — checklist, troubleshooting, assessment

**Files:**

- Modify: `resources/migration-checklist.md`
- Modify: `resources/troubleshooting.md`
- Modify: `sessions/session-3/exercises/current-state-assessment.md`

- [ ] **Step 1: Read `resources/migration-checklist.md`**, locate the Phase 2 exit-question line `**Exit question:** *Can we deploy any commit automatically through one trusted path?*`.

- [ ] **Step 2: Add two Phase-2 items** — replace that exit-question line with the two bullets below, a blank line, then the original exit-question line:

```markdown
- [ ] Database schema and baseline data change **only** through the pipeline (a migration runner such as DbUp), never by hand on a shared environment — see [database-delivery](database-delivery.md)
- [ ] Engineers develop and test migrations against a **local database** (the database analog of a personal sandbox stack) before merge

**Exit question:** *Can we deploy any commit automatically through one trusted path?*
```

- [ ] **Step 3: Read `resources/troubleshooting.md`**, locate the entry heading `### Problem: a stateful change (e.g., a DynamoDB schema shift) blocks small deploys` and its fix bullet `- Keep changes backward-compatible so new work doesn't break delivered work (a CI minimum)`.

- [ ] **Step 4: Broaden the heading** — replace `### Problem: a stateful change (e.g., a DynamoDB schema shift) blocks small deploys` with:

```markdown
### Problem: a stateful change (e.g., a SQL Server or DynamoDB schema change) blocks small deploys
```

- [ ] **Step 5: Add fix bullets** — replace the bullet `- Keep changes backward-compatible so new work doesn't break delivered work (a CI minimum)` with that bullet followed by two new ones:

```markdown
- Keep changes backward-compatible so new work doesn't break delivered work (a CI minimum)
- Deliver schema as code: ordered migrations run by the pipeline (DbUp), built once and promoted — never hand-applied to a shared database. See [database-delivery](./database-delivery.md)
- Develop and test the migration against a local database first, the same way you'd use a personal sandbox stack
```

- [ ] **Step 6: Read `sessions/session-3/exercises/current-state-assessment.md`**, locate the heading line `## Part 2 — Score your controls (governance & communication)`.

- [ ] **Step 7: Add the DB reflection note** — replace that heading with the block below followed by the original heading:

```markdown
> **Apply the minimums to your database.** Run a quick gut-check on minimums #2, #5, and #9 *for schema*: do schema and baseline-data changes go through the pipeline, or by hand on a shared server? Is there one migration runner and history, or ad-hoc scripts? Do engineers have a local database to develop against? Where the answer is "by hand," that is a deliberate target DX is driving toward automation — note it, don't score it as a personal gap. See [Database Delivery](../../../resources/database-delivery.md).

## Part 2 — Score your controls (governance & communication)
```

- [ ] **Step 8: Build and lint**

Run: `cd /home/shane/src/realmanage/tools/dx/dx-training/site && npm run build`
Then: `cd /home/shane/src/realmanage/tools/dx/dx-training && npx markdownlint-cli2 "courses/continuous-delivery-101/resources/migration-checklist.md" "courses/continuous-delivery-101/resources/troubleshooting.md" "courses/continuous-delivery-101/sessions/session-3/exercises/current-state-assessment.md"`
Expected: build success; lint clean.

- [ ] **Step 9: Checkpoint** — confirm all new links resolve. Do not commit yet.

---

### Task 6: Wiring — CLAUDE.md, README.md, site.config.json

**Files:**

- Modify: `CLAUDE.md`
- Modify: `README.md`
- Modify: `site.config.json`

- [ ] **Step 1: Read `CLAUDE.md`**, locate the final Important-Notes bullet beginning `- Recovery: teach **fail forward**`.

- [ ] **Step 2: Add two bullets after it** — replace that entire bullet (it ends with `…keep it rehearsed.`) with itself followed by the two new bullets:

```markdown
- Recovery: teach **fail forward** (ship a small fix through the pipeline) as the default response to problems; rollback is the emergency lever for costly, time-sensitive issues. **The rollback action at RealManage is re-running the last known-good deployment in GitLab** (redeploys the prior immutable artifact through the pipeline) — never a hand-edited Lambda alias/version, which bypasses the pipeline (minimum #2) and drifts from IaC. Minimum #8 still requires the rollback *capability* — keep it rehearsed.
- Database delivery: schema and baseline data are delivered as code through the pipeline, the same minimums as app code. The end-state goal — automation-only schema, no standing human DDL on shared envs, local DBs as the playground — is stated as **direction owned by DX**, not a team rollout checklist. Recovery is forward-only: fail forward + expand/contract, never a hand-edit on a shared database. See `resources/database-delivery.md`.
- **Scoped C#/.NET exception:** the `sessions/session-3/examples/db-migrations/` example is intentionally C#/.NET + SQL Server + DbUp because database delivery lives in the established .NET estate. This is the one justified exception to "examples are TypeScript + SAM" — do **not** convert it to TypeScript.
```

- [ ] **Step 3: Read `README.md`**, locate the file-tree line `│   ├── migration-checklist.md         # The 5-phase migration path as a checklist`.

- [ ] **Step 4: Add the resource to the tree** — replace that line with itself followed by the new line (aligned to match the surrounding comment column):

```markdown
│   ├── migration-checklist.md         # The 5-phase migration path as a checklist
│   ├── database-delivery.md           # Schema & baseline data as code through the pipeline (DbUp)
```

- [ ] **Step 5: Add the learning-objective bullet** — locate the bullet `- ✅ Assess your team's current state and write a concrete CD migration plan` and replace it with the new bullet followed by it:

```markdown
- ✅ Deliver **database** schema and baseline data as code through the pipeline, the same way you ship application changes
- ✅ Assess your team's current state and write a concrete CD migration plan
```

- [ ] **Step 6: Read `site.config.json`**, locate the `labels` entry line `"resources/troubleshooting.md": "Troubleshooting",`.

- [ ] **Step 7: Add the resource label** — replace that line with itself followed by the new label line:

```json
    "resources/troubleshooting.md": "Troubleshooting",
    "resources/database-delivery.md": "Database Delivery",
```

- [ ] **Step 8: Add the example label** — locate the line `"sessions/session-3/exercises/current-state-assessment.md": "Current-State Assessment",` and replace it with itself followed by:

```json
    "sessions/session-3/exercises/current-state-assessment.md": "Current-State Assessment",
    "sessions/session-3/examples/db-migrations/README.md": "Database Migrations (DbUp)",
```

- [ ] **Step 9: Build and lint**

Run: `cd /home/shane/src/realmanage/tools/dx/dx-training/site && npm run build`
Then: `cd /home/shane/src/realmanage/tools/dx/dx-training && npx markdownlint-cli2 "courses/continuous-delivery-101/CLAUDE.md" "courses/continuous-delivery-101/README.md"`
Expected: build success (site.config.json parses; labels applied to nav); lint clean. If the build fails to parse `site.config.json`, fix the JSON (trailing commas / missing commas) and rebuild.

- [ ] **Step 10: Checkpoint** — confirm the Resources nav shows "Database Delivery" and Session 3's examples show "Database Migrations (DbUp)". Do not commit yet.

---

### Task 7: Final verification and (user-gated) commit

**Files:** none (verification + commit only)

- [ ] **Step 1: Full build**

Run: `cd /home/shane/src/realmanage/tools/dx/dx-training/site && npm run build`
Expected: CD 101 builds clean; the only "links outside the published site" are the three pre-existing ai-101 links. Record the page/code-view/folder-index counts.

- [ ] **Step 2: Full course lint**

Run: `cd /home/shane/src/realmanage/tools/dx/dx-training && npx markdownlint-cli2 "courses/continuous-delivery-101/**/*.md"`
Expected: clean (MD013 excepted).

- [ ] **Step 3: Verify slides untouched**

Run: `cd /home/shane/src/realmanage/tools/dx/dx-training && git status --porcelain courses/continuous-delivery-101/slides`
Expected: no output (the pinned slides are unchanged and unstaged).

- [ ] **Step 4: Review the diff**

Run: `cd /home/shane/src/realmanage/tools/dx/dx-training && git status --porcelain courses/continuous-delivery-101`
Confirm only the intended new/modified files are listed; `slides/` is absent.

- [ ] **Step 5: STOP — ask the user before committing.**

Per the standing rule (commit only when explicitly asked), do not commit until the user says so. When they do, stage **explicit paths only** (never `git add -A`):

```bash
cd /home/shane/src/realmanage/tools/dx/dx-training
git add courses/continuous-delivery-101/resources/database-delivery.md \
        courses/continuous-delivery-101/sessions/session-3/examples/db-migrations \
        courses/continuous-delivery-101/sessions/session-1/README.md \
        courses/continuous-delivery-101/sessions/session-2/README.md \
        courses/continuous-delivery-101/sessions/session-3/README.md \
        courses/continuous-delivery-101/resources/glossary.md \
        courses/continuous-delivery-101/resources/migration-checklist.md \
        courses/continuous-delivery-101/resources/troubleshooting.md \
        courses/continuous-delivery-101/sessions/session-3/exercises/current-state-assessment.md \
        courses/continuous-delivery-101/CLAUDE.md \
        courses/continuous-delivery-101/README.md \
        courses/continuous-delivery-101/site.config.json \
        courses/continuous-delivery-101/docs/superpowers/specs/2026-06-23-database-delivery-design.md \
        courses/continuous-delivery-101/docs/superpowers/plans/2026-06-23-database-delivery.md
git commit -m "$(cat <<'EOF'
feat(cd-101): integrate database delivery (DbUp schema-as-code)

Add a database-delivery resource, a C#/.NET DbUp worked example under
Session 3, spine touches across all three sessions, glossary terms, and
light checklist/assessment/wiring edits. Frames schema delivery as the CD
minimums applied to the database; states the automation-only end-state as
direction owned by DX.

Co-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>
EOF
)"
```

Then push only if asked. Recall: `git push origin main` pushes to GitLab; GitHub is a **manual** target via `git push github main`. Push both only when the user says "push both."

---

## Self-review

**Spec coverage** (against `2026-06-23-database-delivery-design.md`):

- New resource `database-delivery.md` with all eight sections → Task 1. ✔
- Worked example `db-migrations/` (README, Program.cs, csproj, 3 scripts, gitlab-ci, local playground) → Task 2. ✔
- Spine touches S1/S2/S3 → Task 3. ✔
- Glossary terms (schema migration, journal, forward-only, baseline data, drift, baseline script, local-DB playground, DbUp) → Task 4. ✔
- migration-checklist (2 items), troubleshooting (1 entry extended), current-state-assessment (1 note) → Task 5. ✔
- CLAUDE.md (scoped exception + end-state framing), README.md (tree + objective), site.config.json (labels) → Task 6. ✔
- Verification (build, markdownlint, links, slides untouched) → Task 7. ✔

**Placeholder scan:** No TBD/TODO; every step shows full content or an exact anchor + replacement.

**Type/name consistency:** `Migrator.csproj` sets `AssemblyName=Migrator` → published entry is `publish/Migrator.dll`, matching the run commands in `Program.cs` usage, the local-playground command, and all three pipeline jobs. Scripts are embedded (`EmbeddedResource Include="Scripts\*.sql"`) and loaded via `WithScriptsEmbeddedInAssembly`. Table/column names (`dbo.Assessments`, `dbo.AssessmentStatus`, `DueDate`, `Status`, `Code`, `DisplayName`) are consistent across the three scripts. The resource link `../sessions/session-3/examples/db-migrations/README.md` and the example link `../../../resources/database-delivery.md` are reciprocal and path-correct. Resource `order: 35` slots between migration-checklist (30) and what-cd-costs (40).

## Out of scope (flag to user separately)

- `resources/migration-checklist.md` line ~63 still reads "redeploy previous artifact / **shift Lambda alias**", which predates the rollback correction (rollback = re-run the last good GitLab deployment, not an alias shift). This is a pre-existing inconsistency unrelated to database delivery — note it for a separate one-line fix rather than bundling it here.
