# Continuous Delivery Glossary

Plain-language definitions for the terms used throughout this course.

## Core concepts

**Continuous Delivery (CD)**
The engineering discipline of keeping software in an always-deployable state so any change can be released to production safely, on demand. Releasing is a business decision; *being able to release* is an engineering guarantee.

**Continuous Deployment**
A step beyond CD: every change that passes the pipeline is released to production automatically, with no human gate. CD makes this *possible*; it is optional.

**Continuous Integration (CI)**
Every developer integrating their work to the trunk at least daily, with automated tests verifying the result is releasable. A team discipline, not a server.

**Trunk-based development (TBD)**
A branching model where all work integrates into one shared trunk (`main`), either directly or via branches that live less than a day.

**Trunk**
The single shared mainline of the repository (`main`). The source of truth that the pipeline builds and deploys from.

## Batches and flow

**Batch size**
How much change ships in one release. CD's central lever: smaller batches are faster to review, safer to deploy, and easier to debug when they fail.

**Big-batch / large batch**
A release accumulating many changes (e.g., a week of work on a long-lived branch). High risk: many things change at once, so a failure is hard to isolate and roll back.

**DORA metrics**
The four delivery-performance measures from the **D**evOps **R**esearch and **A**ssessment program (the *Accelerate* research) that this course uses to gauge progress: lead time for changes, deployment frequency, change failure rate, and mean time to restore. CD's practices improve all four.

**Lead time for changes**
A DORA metric: time from code committed to code running in production. CD shrinks it from weeks to hours.

**Deployment frequency**
A DORA metric: how often you deploy to production. High performers deploy on demand, multiple times a day.

**Change failure rate**
A DORA metric: the percentage of deployments that cause a failure requiring remediation.

**Mean time to restore (MTTR)**
A DORA metric: how quickly you recover from a failed deployment or incident. Fast fail-forward and rehearsed rollback both drive this down.

**Fail forward (roll forward / fix forward)**
The default response to a production problem under CD: ship a small fix *through the pipeline* instead of reverting. Because deploys are small, fast, and canary-verified, the fix lands in minutes and the defect is actually resolved. Preferred over rollback except when a problem is costly *and* time-sensitive.

**Rollback**
Returning a service to the last known-good version. CD requires the *capability* (minimum #8), but operationally it's the emergency lever — used when a problem is costly and time-sensitive and a forward fix won't land fast enough. On AWS: shift a Lambda alias, redeploy a prior immutable artifact, or rely on canary/CloudFormation auto-rollback. Caveat: rolling *code* back does not roll *data* back.

## Pipeline and artifacts

**Pipeline**
The automated sequence (validate → build → test → deploy) that takes a commit toward production. In our world, defined in `.gitlab-ci.yml`.

**Stage / job**
A pipeline is organized into *stages* (e.g., `build`, `test`, `dev`, `qa`, `prod`); each stage contains one or more *jobs* that do the work.

**Quality gate**
An automated check that must pass for a change to proceed — lint, unit tests, coverage threshold, security scan. Collectively, they form the *definition of deployable*.

**Definition of deployable**
The automated criteria that determine whether an artifact may be released. Criteria, not a meeting.

**Artifact**
The built, deployable thing produced from a commit — a Lambda bundle (`.zip`), a container image, a packaged SAM template.

**Immutable artifact**
An artifact built exactly once from a commit and never modified afterward. The same bytes promoted through dev, qa, and prod.

**Promotion**
Moving the *same* artifact from one environment to the next (dev → qa → prod), rather than rebuilding per environment.

**Manual gate / approval gate**
A pipeline step that waits for a human to click "deploy." A transitional compromise on the road to CD — useful for compliance or early confidence, but not the goal.

**Smoke test**
A fast, shallow check run immediately *after* a deploy to confirm the service is actually up and serving — a health endpoint or one real request — before promotion continues. It proves the deploy worked; it is not full testing.

## Decoupling deploy from release

**Deploy**
Putting code into an environment. A technical act.

**Release**
Exposing a feature to users. A business decision. CD decouples the two so you can deploy continuously and release when ready.

**Feature flag (feature toggle)**
A runtime switch that turns a code path on or off without a deploy. Lets you merge incomplete work to trunk (off by default) and reveal it later.

**Dark launch**
Deploying a feature to production turned off (or to a subset of users) to validate it before a full release.

**Expand/contract (parallel change)**
A technique for changing a shared resource (e.g., a database schema) without breaking running code: first *expand* — add the new shape and write to both old and new; then migrate readers; then *contract* — remove the old shape. Each step is a small, backward-compatible deploy, so code and data can move forward or back independently. This is the answer to the rollback "data trap."

**Release notes (changelog)**
The human-facing record of what changed *for users*. Under CD it is anchored to releases — feature-flag flips / user-facing changes — not to deploys. See [communicating-releases](communicating-releases.md).

## AWS and RealManage specifics

**SAM (Serverless Application Model)**
An AWS framework for defining serverless apps (Lambda, API Gateway, DynamoDB) as CloudFormation. `sam build` / `sam deploy`. RealManage's primary IaC tool for app services.

**CDK (Cloud Development Kit)**
An AWS framework for defining infrastructure in a programming language (e.g., TypeScript) that synthesizes to CloudFormation. Used in some RealManage deviations.

**CloudFormation**
AWS's native IaC service. SAM and CDK both compile down to it. The `iac-baseline` platform repo uses it directly.

**OIDC (OpenID Connect)**
The mechanism GitLab CI uses to assume an AWS IAM role for deploys without storing static AWS credentials. The only acceptable CI auth for our pipelines.

**Lambda alias / version**
A Lambda *version* is an immutable snapshot of function code; an *alias* (e.g., `live`) is a pointer to a version. Shifting the alias enables instant rollback and canary releases.

**Canary release**
Routing a small percentage of traffic to a new version, watching metrics, then shifting the rest if healthy — automated via CodeDeploy / SAM `DeploymentPreference`.

**`iac-baseline`**
RealManage's canonical AWS IaC repository. Defines our pipeline stages, naming, environment config, OIDC auth, and validation conventions. Our examples align with it.

**Personal / sandbox stack**
A developer's own copy of a stack (`${env}-${service}-${username}`) for local experimentation. Not a shared environment, so it does not need to go through the pipeline.

**Stop-the-line**
The rule that a red build or red pipeline becomes the team's top priority — no new feature work until the trunk is green again.
