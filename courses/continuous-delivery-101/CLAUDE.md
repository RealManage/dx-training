# Continuous Delivery 101: Course Context

## Course Overview

A 3-session, hands-on course teaching Continuous Delivery (CD) fundamentals to RealManage development teams. It is built on the practices cataloged at [minimumcd.org](https://minimumcd.org) and grounded in how RealManage builds today: established .NET Framework APIs on Azure VMs that remain core to the business, alongside new, small, cloud-native services on AWS — with the strangler fig pattern selectively carving functionality out into new services where that serves the product.

## Target Audience

- Software engineers building new cloud-native AWS services
- Tech leads who own a team's branching and release workflow
- Engineering leaders who approve the process and change-window shifts CD requires
- Teams currently on weekly releases with long-lived branches

## Course Structure

- **Duration:** 3 sessions, 2 hours each (6 hours total)
- **Format:** Interactive workshops with worked examples and hands-on labs
- **Level:** Intermediate (assumes Git, basic AWS, exposure to GitLab CI/CD)
- **Track:** Single developer-focused track with "Engineering Lead" callouts for leadership topics

## Session Breakdown

1. **Session 1:** Why Continuous Delivery & the Minimums (2 hours)
2. **Session 2:** Trunk-Based Development & Continuous Integration (2 hours)
3. **Session 3:** The Pipeline — Single Path to Production (2 hours)

## Learning Objectives

By completion, participants should be able to:

- Explain CD, distinguish it from Continuous Deployment, and justify small batches
- Recite and apply the MinimumCD practices for CI and CD
- Practice trunk-based development with short-lived branches
- Decouple deploy from release with feature flags
- Reason about a pipeline as the single, definitive path to production
- Build and promote immutable artifacts across environments
- Recover from a bad deploy: fail forward by default, roll back when costly and time-sensitive
- Write a phased CD migration plan for their team

## The MinimumCD Practices (Source of Truth)

### Continuous Integration minimums

1. Use trunk-based development
2. Integrate work to the trunk at least daily
3. Automated testing before merge to trunk
4. Automated testing of the merged work
5. All feature work stops when the build is red
6. New work does not break delivered work

### Continuous Delivery minimums

1. Use Continuous Integration
2. The application pipeline is the only way to deploy to any environment
3. The pipeline decides releasability and its verdict is definitive
4. Artifacts meet the organization's definition of deployable
5. Artifacts are immutable — no human changes after commit
6. All feature work stops when the deployment pipeline is red
7. A production-like test environment exists
8. Rollback is on demand
9. Application configuration is deployed with the artifact

### Trunk-based development minimums

- All changes integrate into trunk
- If branches are used, they originate from trunk, reintegrate to trunk, and are short-lived (< 1 day)

## RealManage Technical Context

### The landscape (a deliberate mix)

- **Established systems — core to the business:** ASP.NET (.NET Framework) Web APIs on Azure VMs. They are themselves a recent rewrite off a genuinely legacy platform, they run much of the business today, and they remain a large part of the estate.
- **New work — cloud-native where possible:** small services on **AWS** — **Lambda** (TypeScript), **ECS**, **DynamoDB**, **SNS**, **SQS**, **API Gateway**, **S3**.
- **Direction of travel:** the **strangler fig pattern** gradually carves functionality out of the monoliths into new cloud-native services. It is incremental, not a big-bang rewrite — both worlds coexist by design. A full worked migration (carving violations out of the monolith into the Lambda service) is in `sessions/session-3/examples/strangler-fig-violations.md`.
- **Tooling for new services:** IaC via **AWS SAM** (primary for app services) and **AWS CDK** (some deviations); the platform IaC baseline uses CloudFormation. CI/CD via **GitLab CI/CD** with **OIDC** auth (no static AWS credentials).

### The delivery-practice shift (what this course is actually about)

CD is about *how* we deliver, independent of platform:

- **From:** weekly deployment cadence, long-lived feature branches, painful merges, release-day anxiety.
- **To:** small batches integrated daily, an always-deployable trunk, and the ability to release *any* change *any* day, safely, with low drama.

The new cloud-native services are the natural place to establish these practices — they start from a clean slate, with no existing deploy process to retrofit — but the practices themselves apply everywhere.

### Alignment with `iac-baseline`

Examples align with the RealManage `iac-baseline` repo conventions:

- **Pipeline stages:** `validate → build → (test) → dev → qa → prod`
- **Auth:** GitLab OIDC; role ARN `arn:aws:iam::${AWS_ACCOUNT}:role/gitlab-runner-role-${CI_ENVIRONMENT_NAME}`
- **Stack naming:** pipeline `${ENV}-${SERVICE_NAME}` (e.g. `prod-aws-violations-api`); personal `${ENV}-${SERVICE_NAME}-${STAGE}`
- **Service naming:** `aws-` prefix, lowercase-kebab-case
- **Environments:** `dev`, `qa`, `prod`, parameterized via `configuration/{env}.config`
- **Validation gates:** `cfn-lint`, `aws cloudformation validate-template`, strict config validation
- **Immutability:** build artifacts tagged by commit SHA; digest-pinned CI images
- **Important teaching point:** the *current* baseline gates **every** stage with `when: manual`. That is a valid early migration state, not the end state. This course shows the gap to full CD and the path to closing it.

## Course Philosophy

- **Practices over products.** CD is working agreements and engineering discipline, not a tool you buy.
- **Smaller batches first.** Most of CD's value comes from shrinking the batch size.
- **Decouple deploy from release.** Merge daily; reveal features on your schedule.
- **Automate the verdict.** The pipeline, not a meeting, decides releasability.
- **The team owns its quality.** There is no separate QA team or QA gate at RealManage; the delivering team owns quality, encoded as the pipeline's automated definition of deployable.
- **Fail forward by default.** Ship a small fix through the pipeline; keep rollback rehearsed for costly, time-sensitive emergencies.
- **Honest about where we are.** Use our real pipeline as the starting point, not a strawman.
- **Continuous improvement, not escape.** The established .NET APIs are themselves a recent modernization; CD is the next increment of that same journey, applied across the whole estate — never a verdict on the stack or the people who built it.

## Important Notes

- The new services are **TypeScript on AWS**, not C#/.NET — examples use TypeScript + SAM.
- GitLab CI/CD is the CI/CD platform; do not introduce other CI tools in examples.
- Never recommend static AWS credentials in CI — always OIDC.
- "Continuous Delivery" (deployable on demand) ≠ "Continuous Deployment" (every commit auto-deployed to prod). Keep them distinct.
- Manual approval gates: distinguish *debt* gates (re-litigating readiness the pipeline already proved — remove) from *legitimate, permanent controls* (deliberate authorization of timing/risk — keep). CD relocates and *strengthens* governance: MR review is segregation of duties, the pipeline is the audit trail. See `resources/governance-and-compliance.md`. Frame gates honestly, not as uniformly bad.
- Recovery: teach **fail forward** (ship a small fix through the pipeline) as the default response to problems; rollback is the emergency lever for costly, time-sensitive issues. **The rollback action at RealManage is re-running the last known-good deployment in GitLab** (redeploys the prior immutable artifact through the pipeline) — never a hand-edited Lambda alias/version, which bypasses the pipeline (minimum #2) and drifts from IaC. Minimum #8 still requires the rollback *capability* — keep it rehearsed.
- Database delivery: schema and baseline data are delivered as code through the pipeline, the same minimums as app code. The end-state goal — automation-only schema, no standing human DDL on shared envs, local DBs as the playground — is stated as **direction owned by DX**, not a team rollout checklist. Recovery is forward-only: fail forward + expand/contract, never a hand-edit on a shared database. See `resources/database-delivery.md`.
- **Scoped C#/.NET exceptions (two):** two examples are intentionally C#/.NET because the topic lives in the established .NET estate — `sessions/session-3/examples/db-migrations/` (database delivery + DbUp) and `sessions/session-2/examples/characterization-test/` (testing untested legacy code). These are the justified exceptions to "examples are TypeScript + SAM" — do **not** convert them to TypeScript.
- **Testing stance (mostly-untested estate):** four rules — (1) new code gets automated tests (cheap with AI, but they must pin real *intent*, independently confirmed — a human or a separate agent, not the one that wrote the code); (2) existing untested code is "tested in production," not backfilled wholesale; (3) characterize legacy code before changing it; (4) exploratory manual testing is permanent and gates the flag flip (release), not the merge — while manual regression is interim debt automated down via rule 3, never a QA handoff. Feature flags let a manual-testing team do CI (integrate daily) by merging dark and verifying before the flip. Manual testing here is the delivering engineer's own verification — NOT a revived QA team/gate. See `resources/testing-and-cd.md`.
