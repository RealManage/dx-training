---
order: 10
---
# Continuous Delivery Glossary

Plain-language definitions for the terms used throughout this course.

## Core concepts

**Continuous Delivery (CD)**
The engineering discipline of keeping software in an always-deployable state so any change can be deployed to production safely, on demand. Releasing is a business decision; *being able to release* is an engineering guarantee.

**Continuous Deployment**
A step beyond CD: every change that passes the pipeline is deployed to production automatically, with no human gate. CD makes this *possible*; it is optional.

**Continuous Integration (CI)**
Every developer integrating their work to the trunk at least daily, with automated tests verifying each integration is deployable and a red build stopping the line until it is fixed. A team discipline, not a server — and it is built on trunk-based development: real CI is impossible without it.

**Trunk**
The single shared mainline of the repository (`main`). The source of truth that the pipeline builds and deploys from.

**Trunk-based development (TBD)**
The branching model that Continuous Integration runs on: all work integrates into one shared trunk (`main`) — committed directly, or via branches that live less than a day — with long-lived branches deliberately *resisted*, not merely avoided. The discipline is the point, not the mechanism: keep trunk releasable at all times, and hide incomplete work behind feature flags or branch by abstraction so you can integrate *before* a feature is finished. TBD is the source-control foundation of CI — you cannot integrate *continuously* while work sits on week-old branches — but on its own (trunk commits without automated verification and stop-the-line) it is not yet CI.

## Batches and flow

**Batch size**
How much change ships in one deploy. CD's central lever: smaller batches are faster to review, safer to deploy, and easier to debug when they fail.

**Big-batch / large batch**
A deploy accumulating many changes (e.g., a week of work on a long-lived branch). High risk: many things change at once, so a failure is hard to isolate and roll back.

**Change failure rate**
A DORA metric: the percentage of deployments that cause a failure requiring remediation.

**Deployment frequency**
A DORA metric: how often you deploy to production. High performers deploy on demand, multiple times a day.

**DORA metrics**
The four delivery-performance measures from the **D**evOps **R**esearch and **A**ssessment program (the *Accelerate* research) that this course uses to gauge progress: lead time for changes, deployment frequency, change failure rate, and mean time to restore. CD's practices improve all four.

**Fail forward (roll forward / fix forward)**
The default response to a production problem under CD: ship a small fix *through the pipeline* instead of reverting. Because deploys are small, fast, and canary-verified, the fix lands in minutes and the defect is actually resolved. Preferred over rollback except when a problem is costly *and* time-sensitive.

**Flow efficiency**
Total process time ÷ total lead time × 100 — the share of lead time that is real work rather than waiting. Often shockingly low (single digits to ~15%); raising it is the point of removing waits.

**Lead time for changes**
A DORA metric: time from code committed to code running in production. CD shrinks it from weeks to hours.

**Mean time to restore (MTTR)**
A DORA metric: how quickly you recover from a failed deployment or incident. Fast fail-forward and rehearsed rollback both drive this down.

**Percent complete & accurate (%C/A)**
Of the work arriving at a step, the share usable *as-is*, without being sent back for rework. A low %C/A marks where rework is created; multiplying all steps' %C/A gives the *rolled* share of changes that flow through clean.

**Process time**
The time someone is *actively working* a change at a step (writing code, reviewing the MR, running a deploy) — as opposed to the change sitting idle.

**Rollback**
Returning a service to the last known-good version. CD requires the *capability* (minimum #8), but operationally it's the emergency lever — used when a problem is costly and time-sensitive and a forward fix won't land fast enough. On AWS: re-run the last known-good deployment in GitLab (it redeploys the prior immutable artifact through the pipeline); the canary and CloudFormation auto-rollback are automatic safety nets. Caveat: rolling *code* back does not roll *data* back.

**Value stream**
Every step a change passes through from *idea* to *running in production* — intake, refinement, development, review, deploy, and the waits between them.

**Value stream map (VSM)**
A diagram of the value stream annotated with process time, wait time, and %C/A at each step. The Phase-0 tool for seeing *where lead time is actually lost* and naming the binding constraint. Worked in the [value stream map exercise](../sessions/session-1/exercises/value-stream-map.md).

**Wait time**
The time a change sits *idle* between or within steps: in a backlog, a review queue, blocked on an environment, or waiting for a release window. On most teams it dwarfs process time.

## Pipeline and artifacts

**Artifact**
The built, deployable thing produced from a commit — a Lambda bundle (`.zip`), a container image, a packaged SAM template.

**Definition of deployable**
The automated criteria that determine whether an artifact may be deployed. Criteria, not a meeting.

**Immutable artifact**
An artifact built exactly once from a commit and never modified afterward. The same bytes promoted through dev, qa, and prod.

**Manual gate / approval gate**
A pipeline step that waits for a human before proceeding. It is *debt* when it re-litigates readiness the pipeline already proved; it is a *legitimate, permanent control* when it is a deliberate authorization of timing or risk (compliance, a contractual change window). Remove the former; keep the latter honestly. See [governance-and-compliance](governance-and-compliance.md).

**Pipeline**
The automated sequence (validate → build → test → deploy) that takes a commit toward production. In our world, defined in `.gitlab-ci.yml`.

**Promotion**
Moving the *same* artifact from one environment to the next (dev → qa → prod), rather than rebuilding per environment.

**Quality gate**
An automated check that must pass for a change to proceed — lint, unit tests, coverage threshold, security scan. Collectively, they form the *definition of deployable*.

**Smoke test**
A fast, shallow check run immediately *after* a deploy to confirm the service is actually up and serving — a health endpoint or one real request — before promotion continues. It proves the deploy worked; it is not full testing.

**Stage / job**
A pipeline is organized into *stages* (e.g., `build`, `test`, `dev`, `qa`, `prod`); each stage contains one or more *jobs* that do the work.

## Decoupling deploy from release

**Branch by abstraction**
A technique for making a large or structural change on trunk without a long-lived branch: introduce an abstraction (a *seam*) over the component you are changing, build the new implementation behind it while the old one keeps running, flip which implementation the seam resolves to (a one-line wiring change, often itself behind a flag), then delete the old path. Like a feature flag it lets you integrate before the work is finished — but it gates an *implementation* behind an interface, where a flag gates a *behavior* at a call site. The tool of choice for swapping a dependency or a large refactor. See [Session 2](../sessions/session-2/README.md).

**Dark launch**
Deploying a feature to production turned off (or to a subset of users) to validate it before a full release.

**Deploy**
Putting code into an environment. A technical act.

**Expand/contract (parallel change)**
A technique for changing a shared resource (e.g., a database schema) without breaking running code: first *expand* — add the new shape and write to both old and new; then migrate readers; then *contract* — remove the old shape. Each step is a small, backward-compatible deploy, so code and data can move forward or back independently. This is the answer to the rollback "data trap."

**Feature flag (feature toggle)**
A runtime switch that turns a code path on or off without a deploy. Lets you merge incomplete work to trunk (off by default) and reveal it later.

**Release**
Exposing a feature to users. A business decision. CD decouples the two so you can deploy continuously and release when ready.

**Release notes (changelog)**
The human-facing record of what changed *for users*. Under CD it is anchored to releases — feature-flag flips / user-facing changes — not to deploys. See [communicating-releases](communicating-releases.md).

## Migration and data

**Baseline (reference) data**
Lookup/seed rows (status codes, types, defaults) shipped and versioned exactly like schema, via idempotent scripts (`MERGE` / `IF NOT EXISTS`) so re-running converges to the same rows.

**Baseline script**
The migration that captures an existing database's current schema as a starting point, marked already-applied (DbUp's `MarkAsExecuted`) so the runner does not try to recreate existing objects.

**Data trap**
Rolling application *code* back does not roll *data* back — rows already written in the new shape remain. Both directions stay safe only if every change is backward-compatible (expand/contract). See [database-delivery](database-delivery.md).

**DbUp**
A .NET library that applies ordered SQL scripts to a database and tracks them in a `SchemaVersions` journal. Forward-only and idempotent. RealManage's migration runner for SQL Server. See [Database Delivery](database-delivery.md).

**DDL (Data Definition Language)**
The subset of SQL that changes a database's *structure* — `CREATE`, `ALTER`, `DROP` of tables, columns, indexes, and constraints — as opposed to DML (`INSERT` / `UPDATE` / `DELETE`), which changes *data*. "No standing human DDL access" means engineers cannot hand-alter schema on a shared database; structural change happens only through migrations the pipeline runs. See [database-delivery](database-delivery.md).

**Environment drift**
The state where `prod`, `qa`, and `dev` schemas have diverged, so a migration that works in one may fail in another. Resolved by baselining production as the source of truth and reconciling lower environments up to it.

**Forward-only migration**
A migration scheme with no automated "down" step: you do not undo a change, you ship a new forward change. Reversibility comes from expand/contract, not from rolling the database back. DbUp is forward-only.

**Idempotent**
An operation that has the same effect whether it runs once or many times. Keying writes on a stable identifier (e.g., `violationId`) makes retries and backfills safe — they cannot double-count.

**Local-database playground**
A developer's own database (e.g., a containerized SQL Server) used to develop and test migrations before merge — the database analog of the personal/sandbox stack.

**Reconciliation**
A check that compares two stores (or two computations) and flags where they disagree — plus a defined response for when they do. Essential during a dual-write window, especially when the data carries money.

**Schema migration**
A small, ordered, version-controlled change to a database's structure (or baseline data), applied by a runner rather than by hand. The database analog of a code commit moving through the pipeline.

**Schema-history table (journal)**
The table a migration runner uses to record which scripts an environment has already applied, so each runs exactly once and re-running is safe. In DbUp it is `SchemaVersions`.

**Seam**
A controlled insertion point where you can intercept calls to existing behaviour and redirect them — the place a strangler-fig migration adds a routing flag so an old code path and its replacement can run side by side.

**Strangler fig pattern**
Replacing a system incrementally by carving one capability at a time out of it into a new service, running both in parallel and moving callers and data across in small, reversible steps, until the old path can be deleted. Named for the vine that grows around a tree and gradually replaces it. The opposite of a big-bang rewrite. Worked end to end in [strangler-fig in practice](../sessions/session-3/examples/strangler-fig-violations.md).

**System of record**
The store that is *authoritative* for a piece of data — the one you trust when copies disagree. During a migration with dual writes, you name which store is the system of record at each step (e.g., SQL Server until cutover, the new service after).

**Watermark**
A marker of how far a resumable job has progressed (e.g., a timestamp or id), so it can stop and restart without redoing or skipping work. What makes a backfill both idempotent and resumable.

## AWS and RealManage specifics

**Canary release**
Routing a small percentage of traffic to a new version, watching metrics, then shifting the rest if healthy — automated via CodeDeploy / SAM `DeploymentPreference`.

**CDK (Cloud Development Kit)**
An AWS framework for defining infrastructure in a programming language (e.g., TypeScript) that synthesizes to CloudFormation. Used in some RealManage deviations.

**CloudFormation**
AWS's native IaC service. SAM and CDK both compile down to it. The `iac-baseline` platform repo uses it directly.

**`iac-baseline`**
RealManage's canonical AWS IaC repository. Defines our pipeline stages, naming, environment config, OIDC auth, and validation conventions. Our examples align with it.

**Lambda alias / version**
A Lambda *version* is an immutable snapshot of function code; an *alias* (e.g., `live`) is a pointer to a version. Each deploy publishes a new version and the canary shifts the `live` alias to it gradually, auto-rolling-back on alarm. (Routine rollback is re-running the last good deployment, not hand-editing the alias.)

**OIDC (OpenID Connect)**
The mechanism GitLab CI uses to assume an AWS IAM role for deploys without storing static AWS credentials. The only acceptable CI auth for our pipelines.

**Personal / sandbox stack**
A developer's own copy of a stack (`${env}-${service}-${username}`) for local experimentation. Not a shared environment, so it does not need to go through the pipeline.

**SAM (Serverless Application Model)**
An AWS framework for defining serverless apps (Lambda, API Gateway, DynamoDB) as CloudFormation. `sam build` / `sam deploy`. RealManage's primary IaC tool for app services.

**Stop-the-line**
The rule that a red build or red pipeline becomes the team's top priority — no new feature work until the trunk is green again.

## Governance and control

**Audit trail**
The durable record of who changed what, when, and what verified it. Under CD the pipeline *is* the audit trail: commit → MR approval → pipeline run → SHA-tagged artifact → deploy job, each attributable. See [governance-and-compliance](governance-and-compliance.md).

**Break-glass**
A pre-authorized, narrowly-scoped, time-boxed emergency procedure for bypassing a normal control when something is on fire — logged and alerted on use, with mandatory post-incident reconciliation (re-apply through the pipeline) owned by the on-call lead / service owner and recorded in the incident record. Rare by design. See [governance-and-compliance](governance-and-compliance.md).

**Emergency change**
A change made under incident pressure outside the normal flow. CD's answer is to keep the normal flow fast enough that emergencies rarely need to bypass it — and, when one must, to use break-glass (above) so the audit trail survives.

**Segregation of duties (SoD)**
A control requiring that the person who makes a change is not the only one who approves it. Under CD the merge-request review *is* the SoD control: the author cannot merge their own unreviewed work, a second person approves, and the pipeline — not the author — deploys. See [governance-and-compliance](governance-and-compliance.md).
