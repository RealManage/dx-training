# Strangler Fig in Practice: Carving Violations Out of the Monolith

> The HOA Violations API you studied earlier in this session did not start life
> cloud-native. It was *carved out* of the established Community Management monolith,
> one safe step at a time. This is that journey — the worked example the course's
> framing kept promising. Every step is a small, independently shippable,
> reversible change. It is Continuous Delivery applied to the **hardest** case we
> have, not the easiest one.

## Why this example exists

The rest of the course builds CD on a greenfield Lambda service because a blank
canvas makes each practice easy to see. But our real estate is mostly the other
thing: long-lived **ASP.NET (.NET Framework) Web APIs on IIS across Azure VMs**,
backed by SQL Server. If CD only worked on the greenfield service, it would be a
party trick.

It is not. The same practices — small batches, an always-deployable trunk,
feature flags, build-once-promote, expand/contract, fail-forward — are exactly
what make a *migration* survivable. This example shows them carrying the load on
the monolith, not just the new service.

## The starting state

- **The Community Management monolith:** an ASP.NET Framework Web API on IIS,
  spread across Azure VMs, with one shared **SQL Server** database. "Violations"
  is one capability among many (assessments, work orders, ARC requests, ...).
- **Today's delivery:** weekly deploys, feature branches that live for days, a
  release day everyone braces for.
- **The same GitLab CI** that builds the new AWS services also builds and deploys
  the monolith — it just targets **IIS on the VMs** instead of Lambda. The CI
  tooling is not the hard part; the delivery habits and the shared database are.
- **The destination:** the cloud-native **HOA Violations API** (TypeScript Lambda +
  DynamoDB + SNS) you already studied. We are going to build it *beside* the
  monolith and move callers and data across — without a big-bang rewrite, and
  without a release freeze.

## The pattern in one line

> Put a **seam** in front of the capability; build the replacement **beside** it;
> move callers and data across in **small, reversible steps**; delete the old path
> **last**.

Both systems run in parallel for the whole migration. That coexistence is not a
failure to finish — it *is* the strategy. It is what lets every step be small.

## The journey, as shippable slices

Each slice is one merge to trunk, one deploy, and one thing you could undo. None
of them is "the big cutover," because there is no big cutover.

| # | Slice | Ships behind | Data state (expand/contract) | How you recover |
| - | ----- | ------------ | ---------------------------- | --------------- |
| 0 | **Seam.** The monolith's `/violations` handlers consult a routing flag before doing their work. Flag off → behave exactly as today. | `FLAG_VIOLATIONS_ROUTE_NEW` (off) | none | It's a no-op path; flip flag off |
| 1 | **New service, dark.** Deploy the Violations API to dev/qa/prod with its own flags off. No traffic, no callers. | service-level flags (off) | new DynamoDB table exists, empty | nothing routes to it yet |
| 2 | **Expand: dual-write.** On each write, the monolith *also* publishes the violation to the new service (or its event), keyed by a stable `violationId`. SQL Server remains the system of record. | `FLAG_VIOLATIONS_DUAL_WRITE` | both stores written; SQL authoritative | turn dual-write off; SQL untouched |
| 3 | **Backfill.** A one-off, resumable job copies historical violations from SQL into DynamoDB, using the same `violationId` so re-runs are idempotent. | a job, not a release | both stores converge | re-run; idempotent by key |
| 4 | **Shadow reads.** Reads still served from SQL, but a sampled copy is also fetched from the new service and the results compared/logged. You are measuring parity, not trusting it yet. | `FLAG_VIOLATIONS_SHADOW_READ` | both populated | flag off; zero user impact |
| 5 | **Cutover reads (canary).** Route a small **percentage** of read traffic to the new service via the seam; watch error rate and parity; ramp. | `FLAG_VIOLATIONS_ROUTE_NEW` (% ramp) | both populated, reads from new | drop the percentage to 0 — instant |
| 6 | **Cutover writes.** New service becomes system of record for writes; the monolith now reads/forwards. Dual-write reverses direction during the window. | same routing flag, writes | new authoritative; SQL mirrored | flip routing back; SQL still current |
| 7 | **Contract.** Stop dual-writing, delete the monolith's violations code path, and — only after every direct SQL reader has moved — drop the old columns/tables. | removal PRs | old shape removed | redeploy previous monolith artifact |

Notice slices 5 and 6 are the only ones that move user-visible behavior, and both
are governed by a flag you can turn down in seconds. That is the whole safety
argument of CD, applied to a migration.

> **Human/agent division of labour.** This migration is the clearest case of
> "design the line; let the machine turn the bolts." The judgement is human:
> *where the seam goes*, *what the slices are*, *which store is authoritative at
> each step*, *when to ramp*. The rote build is what an agent is good at — the
> dual-write shim, the idempotent backfill job, the reconciliation check, each
> slice's tests. Naming the slices above is the work; writing any one of them is
> not. See [CD when AI writes the code](../../../resources/ai-assisted-delivery.md).

## The hard parts (no hand-waving)

The greenfield decompose exercise slices cleanly because it has no money, no
shared database, and no other teams. A real strangler-fig migration has all
three. Here is where the difficulty actually lives:

- **The dual-write window (slices 2–6).** For a stretch of calendar time you have
  *two* stores that must agree. Make every write **idempotent** on `violationId`
  so retries and the backfill cannot double-count. Decide, explicitly, which store
  is authoritative *at each step* (SQL until slice 6, the new service after). Add a
  **reconciliation** check that flags divergence — and a documented answer for what
  you do when the two disagree. If violations carried *fees* (they often do), this
  window is where correctness bugs cost money, so treat reconciliation as part of
  the work, not a nicety.
- **Backfilling live data.** The table is changing while you copy it. A resumable,
  idempotent job (keyed by `violationId`, watermarked by timestamp) lets you run it
  repeatedly until the tail is empty, with no downtime and no lock.
- **Other people read your tables.** Reports, integrations, and other monolith
  modules may `SELECT` from the violations tables directly. You cannot drop them in
  slice 7 until those readers move. This is *why* contract is last and often slow —
  and why "expand/contract" is a coordination practice, not just a schema trick.
- **It is weeks, not an afternoon.** This is the honest cost. CD does not make the
  migration small; it makes each *step* small, so you are never more than one
  reversible deploy from safety, and you can pause between slices indefinitely.

## Expand/contract on SQL Server (the concrete schema dance)

The new service uses DynamoDB, but the monolith's changes land in **SQL Server**,
and that is where expand/contract earns its keep. Say slice 2 needs a new
`ExternalViolationId` to correlate the two stores:

1. **Expand** — `ALTER TABLE Violations ADD ExternalViolationId UNIQUEIDENTIFIER
   NULL;` A *nullable* add is backward-compatible: old code ignores it, the deploy
   is reversible, no rewrite of existing rows.
2. **Write both** — new writes populate the column; a backfill fills historical
   rows. Readers do not depend on it yet.
3. **Migrate readers** — switch the code that needs the correlation to read the new
   column, once it is fully populated.
4. **Contract** — only after nothing reads the old shape, make it `NOT NULL` or
   drop the superseded column/table.

Each numbered step is its own small deploy. Contrast this with the DynamoDB side,
where "schema" is per-item and the same discipline applies: add the new attribute,
write both, migrate, stop writing the old. The platform differs; the practice does
not.

## The stored-procedure case (where a flag can't save you)

Adding a column is the *easy* shared-database change. The harder — and more common —
one is a **shared stored procedure**: `usp_GetHomeownerLedger`, called by your code
*and* by three apps you do not own. Here the schema dance has a sharp edge: **a
feature flag in your code cannot hide an `ALTER PROC` from their code.** The instant
you change the proc in place, every caller gets the new behaviour at once — no dark
launch, no per-caller ramp, no flag to turn off.

So treat the proc exactly like the column: expand/contract by **versioning**, never
`ALTER` in place.

1. **Expand** — create `usp_GetHomeownerLedger_v2` beside the original. The old proc
   is untouched, so every existing caller keeps working, unchanged.
2. **Migrate readers, one at a time** — point *your* code at `_v2` behind your normal
   flag; then move each external caller onto `_v2` on *their* schedule. Finding those
   callers is the real work: in-database references show up in
   `sys.sql_expression_dependencies`, but apps that `EXEC` the proc over the wire do
   not — those you find by code search, a DBA audit, or Extended Events on the proc.
3. **Contract** — drop the original proc only after that caller list is empty. Until
   then, both versions live.

The general rule the column case only hinted at: **a flag controls code you own; it
cannot control a shared database object other code calls.** For anything multiple
apps share — a proc, a view, a table other modules `SELECT` from — the safety
mechanism is *a versioned copy plus caller coordination*, not a flag. That
coordination, not the SQL, is the slow part.

## The monolith's pipeline (the real one)

The claim that the monolith ships through the *same* GitLab CI deserves more than an
assertion — so here is the actual shape, not an analogy. The Management API builds an
MSBuild artifact instead of a SAM bundle and targets IIS instead of Lambda, but the
shape is identical: **build once, promote the same bytes, vary only config**.

> Throughout this section, **`a/`** is just the directory the build publishes into —
> the name the real pipeline happens to use (think "artifact"). GitLab uploads that one
> folder as the build's job artifact, and every deploy stage downloads the *same* `a/`.
> It's the monolith's equivalent of the Lambda's S3 bundle: the immutable thing you
> build once and promote unchanged.

```yaml
# Real shape (CiraNet Management API). The repo's .gitlab-ci.yml is thin — it
# `include:`s a shared template, ci-templates/dotnet/ciranet-api.yml, where this lives.
stages: [build, deploy_dev, deploy_qa, deploy_prod]

build_api: # runs ONCE, on a Windows runner
  stage: build
  script:
    # MSBuild publishes (WebPublishMethod=FileSystem) and robocopy collects
    # _PublishedWebsites into the artifact dir `a/`: the immutable artifact =
    # base web.config + EVERY web.{env}.config transform (unapplied) + bin.
    # The xUnit suite runs in THIS job, so a red test gates the merge and every deploy.
    - pwsh build-ciranet-api.ps1
  artifacts:
    paths: ["a/"] # promoted as-is to every deploy below — never rebuilt

.deploy: # the SAME `a/`; the runner lives on the target box
  script:
    # Apply the web.{env}.config XDT transform AT DEPLOY to the promoted artifact,
    # then install to IIS. Same bytes in every environment; only the transform differs.
    - pwsh deploy-ciranet-api.ps1 -ArtifactPath a -Environment $ENVIRONMENT

deploy_to_dev:  { extends: .deploy, stage: deploy_dev,  variables: { ENVIRONMENT: dev  } } # auto on main
deploy_to_qa:   { extends: .deploy, stage: deploy_qa,   variables: { ENVIRONMENT: qa   } } # auto on release/*
deploy_to_prod: { extends: .deploy, stage: deploy_prod, variables: { ENVIRONMENT: prod } } # manual gate, 6-VM waves
```

What the monolith **already nails** — verified, not aspirational. It builds the
artifact once and promotes the same bytes (every `deploy_*` consumes the build's `a/`,
none rebuilds); config is applied at *deploy*, not baked per build (CD min #5, #9).
Production rolls out **zero-downtime across a six-VM farm in two waves** — drain three
nodes from the Azure Application Gateway, deploy, restore, repeat (CD min #8's safe
rollout). And no credential is long-lived: the deploy runners are scoped to their own
box, and the App Gateway orchestration authenticates to Azure by **OIDC federated
token, no static secret** — the same no-static-creds rule the Lambda service follows
to AWS.

The real gap is **batch size, not tooling**. `main` flows continuously only as far as
**dev**; `qa` and `prod` are fed by a **`release/YY.M.W` branch cut on a weekly
schedule** — a weekly release train, the exact cadence this course exists to shrink.
The monolith's path to CD is not new infrastructure; it already has the immutable
artifact, the OIDC, the rehearsed rollout. It is making that release branch *smaller
and more frequent* until "release" stops being a weekly event.

## Recovery on each side

| | New service (Lambda) | Monolith (IIS on VMs) |
| - | -------------------- | --------------------- |
| **Default** | Fail forward — ship a small fix through the pipeline | Fail forward — same |
| **Immutable artifact** | versioned bundle in S3, promoted by SHA | the published `a/` artifact, built once and promoted to each environment (config applied at deploy) |
| **Fast lever** | shift the `live` alias / canary auto-rollback (see [rollback-on-aws.md](./rollback-on-aws.md)) | **flip the routing flag** — reads/writes return to the monolith instantly |
| **Hard rollback** | redeploy the prior bundle | redeploy the prior published artifact to IIS |
| **Data** | expand/contract keeps both directions safe | same — the dual-write window is itself the rollback path |

The routing flag is the migration's superpower: because the old path still exists
until slice 7, your fastest recovery is almost never a redeploy — it is turning the
new path back off.

## CD practices, visible the whole time

This migration is not a detour from the course; it is the course:

- **Trunk-based, small batches** — eight slices, each a short-lived branch, none a
  big merge.
- **Feature flags decouple deploy from release** — every slice deploys dark and is
  revealed (or ramped) by flipping a flag, not by shipping code.
- **The pipeline is the single path to prod** — on *both* GitLab pipelines, the
  monolith's and the new service's.
- **Build once, promote the same artifact** — the monolith's published build is
  immutable and SHA-tagged, exactly like the Lambda bundle.
- **Expand/contract** keeps the shared database safe in both directions.
- **Recovery is rehearsed**, and fail-forward is the default on both platforms.

## And the parts you are *not* carving out

Most of the monolith stays exactly where it is, by design. It gets CD too — without any
re-platforming:

- **Trunk-based development** and short-lived branches: a habit, not a platform.
- **Feature flags via configuration** — for a long-lived IIS process the switch is
  `web.config`/`appSettings` or a config store, not a Lambda env var. Two
  operational traps the Lambda case doesn't have: editing `web.config` **recycles
  the app pool** (a brief drop), so to flip a flag *without* a recycle you read from
  a config store on a refresh interval (e.g. `Microsoft.FeatureManagement` over Azure
  App Configuration); and a `web.config` flag must be set **identically on every VM**
  in the farm or you get drift — a central config store is what makes one flip apply
  everywhere at once. Don't conflate this with the deploy-time `web.{env}.config`
  transform from the pipeline section: that sets *per-environment config* and a recycle
  is fine (it's a deploy); a *runtime feature flag* you flip without redeploying must
  live in the refresh-interval store, not the transform. Same file, two different levers.
- **Expand/contract on SQL Server** for every schema change, carved-out or not.
- **Build-once-promote and redeploy-previous recovery** on the existing pipeline.

> CD is *how* you deliver, not *where* you deploy. The monolith can deliver small,
> safe, and often starting Monday — strangler-fig or no strangler-fig.

## Try it

Take a capability in *your* system that you would carve out (or simply change in
place) and run it through the brownfield variant of the
[decompose-a-branch](../../../exercises/decompose-a-branch.md) exercise. Name the
seam, the slices, the flags, and — the part that is never free — the
expand/contract steps and the dual-write window.
