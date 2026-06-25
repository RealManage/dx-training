# Session 3: The Pipeline — Single Path to Production

**Duration:** 2 hours
**Format:** Interactive workshop + hands-on pipeline reading
**Prerequisites:** Sessions 1–2; your decomposed feature and current-state assessment

## 🎯 Learning Objectives

By the end of this session, participants will be able to:

- ✅ Treat the pipeline as the sole, definitive path to every environment
- ✅ Express "deployable" as automated criteria, not a meeting
- ✅ Build an immutable artifact once and promote the *same* one through environments
- ✅ Design fast, safe rollback for AWS Lambda/ECS services
- ✅ Sequence a realistic CD migration and write their team's plan

## 📚 Session Agenda

### 1. Review & Connect (10 minutes)

Sessions 1–2 gave us an always-deployable trunk: small batches, daily integration, feature flags, CI gates. Today we build the machine that turns "the trunk is deployable" into "any change reaches production on demand" — the **pipeline**.

The running example is the **HOA Violations API**: a TypeScript Lambda behind API Gateway that records a violation to DynamoDB and publishes a `ViolationRecorded` event to SNS. The full worked service lives in [`examples/violations-api/`](./examples/violations-api/) and the target pipeline in [`examples/.gitlab-ci.yml`](./examples/.gitlab-ci.yml).

---

### 2. The Pipeline as the Single Path to Production (20 minutes)

#### 2.1 One path, no side doors (10 minutes)

CD minimum #2: **the pipeline is the only way to deploy to any shared environment.** No `sam deploy` from a laptop to qa "just this once." Why this is non-negotiable:

- **Reproducibility:** every change to a shared env has the same provenance — a commit, a pipeline run, an audit trail.
- **Security:** deploy permissions live with the pipeline (via OIDC), not in engineers' hands. No static AWS keys anywhere.
- **Trust:** if there's exactly one path, the pipeline's verdict can *mean* something.

> Personal sandbox stacks (`dev-aws-violations-api-<username>`) are the escape valve for experimentation — they're not shared, so they don't go through the pipeline. Everything shared does.

#### 2.2 The pipeline decides releasability (10 minutes)

CD minimum #3: **the pipeline decides releasability and its verdict is definitive.** This is the one most teams (and our current `iac-baseline`) haven't fully reached, because every deploy is `when: manual`.

The shift: green should *mean* deployable, automatically. A human may still choose *when* to deploy to prod (that's Continuous Delivery, not Continuous Deployment), but they are approving **timing**, not re-litigating **readiness**. If a meeting decides whether the build is "ready," the pipeline doesn't own releasability yet.

> **Engineering Lead note:** a manual prod gate is a legitimate control — for compliance, or for early confidence in a new service. Keep it *honest*: it approves timing, the pipeline already proved readiness. The goal is to let green flow automatically at least through qa, and to make prod *one* deliberate decision, not three manual clicks per release. Revisit each gate as trust grows. (When a gate is a legitimate permanent control rather than debt — and for segregation of duties and audit evidence under CD — see [Governance & Compliance](../../resources/governance-and-compliance.md).)

Decoupling deploy from release also splits the *timing* decision in two. The prod **deploy** gate above (if you keep one) is an engineering authorization — owned by the Engineering Lead. The **release** — the flag flip users actually feel — is a separate decision about *which* feature goes live *when* and *to whom*, owned by whoever runs release communication and flag governance (our evolved release-manager role), in coordination with the business. And it moves *communication* too: you announce the release, not the deploy — the weekly client email doesn't die, it gets rebuilt from user-facing releases instead of the deploy log. See [Communicating Releases](../../resources/communicating-releases.md).

---

### 3. Definition of Deployable (15 minutes)

CD minimum #4: every artifact meets an **automated** definition of deployable. "Deployable" is a checklist the pipeline runs, not a judgment a person makes.

For the Violations API, "deployable" means all of these are green:

- **Lint** clean (`eslint`)
- **Unit tests** pass (`vitest`)
- **Coverage** at or above the team floor
- **IaC valid** (`sam validate`, `cfn-lint`)
- **Security** clean (dependency audit, IaC scan)

These are the `validate` / `test` / `security` stages from [Session 2's CI file](../session-2/examples/ci-pipeline.gitlab-ci.yml). They are the *front half* of the full pipeline. The discipline: if it isn't gated automatically, it isn't part of your definition of deployable — it's a hope.

**When the automated checks don't exist yet** — true across much of the estate — manual verification does not vanish, but it stays *outside* the automated definition of deployable: it gates the **release** (the feature-flag flip), never the merge. Make it an explicit, owned step rather than a side-channel sign-off, and automate it down over time by adding tests where you change code. See [Testing and CD](../../resources/testing-and-cd.md).

---

### 4. Immutable Artifacts & Promotion (20 minutes)

#### 4.1 Build once, promote the same bytes (10 minutes)

CD minimum #5: **artifacts are immutable.** Build the Lambda bundle *once*, from one commit, then promote the *identical* artifact through dev → qa → prod. Never rebuild per environment.

```text
                       ┌─ deploy to dev  ─┐
commit ─→ build ONCE ──┼─ deploy to qa   ─┤  (same artifact, tagged by CI_COMMIT_SHA)
   (sam build +        └─ deploy to prod ─┘
    sam package →
    artifact in S3)
```

> Two keys, same immutable bytes: `CI_COMMIT_SHA` labels the *pipeline artifact* (the S3 prefix and the GitLab job artifact), while *inside* the packaged template SAM references the code by its own **content hash** (see 4.1 below). The SHA is how *you* find the artifact; the content hash is how *CloudFormation* knows the bytes didn't change.

Why it matters: if you rebuild for prod, you've deployed *something you never tested*. "Works in qa, breaks in prod" is almost always a rebuild or a config-baked-into-build problem. The bytes that passed qa must be the bytes in prod.

For SAM specifically: `sam build` then `sam package` uploads the code to an artifact bucket and emits a packaged template referencing it by a content hash. Each environment's `sam deploy` consumes that **same** packaged template. Walk it in [`examples/violations-api/README.md`](./examples/violations-api/README.md).

> **Multi-account note:** if dev/qa/prod live in *separate AWS accounts* (common at RealManage), "promote the same bytes" requires the artifact bucket to be reachable across accounts — a shared artifacts account whose bucket policy grants each environment's deploy role read access, or one bucket the build writes to and every account can read. Rebuilding per account would silently break the immutability guarantee. Make the bucket cross-account-readable; don't rebuild.

#### 4.2 Config travels with the artifact (10 minutes)

CD minimum #9: **application configuration deploys with the artifact.** Environment differences live in `configuration/{dev,qa,prod}.config` (the `iac-baseline` convention) and in SAM parameters — *not* hand-patched into a running Lambda.

The artifact is identical across environments; only the *config applied at deploy* differs (table names, flag values, log levels, SNS topic ARNs). Secrets are never in config files — they're references to SSM Parameter Store / Secrets Manager ARNs.

---

### 5. Production-Like Environments & Rollback (20 minutes)

#### 5.1 Production-like test environment (5 minutes)

CD minimum #7: validate in something that resembles prod. Our `qa` environment mirrors `prod`'s shape via `configuration/qa.config` — same resource types, same topology, smaller scale. The closer qa is to prod, the more a green qa *means* something.

#### 5.2 Recovery: fail forward first, roll back when it's costly (15 minutes)

The whole point of the pipeline is that shipping a change is now small, fast, and safe. That changes the *default* response to a production problem:

- **Fail forward (the default).** Diagnose, write a small fix, and ship it through the same pipeline. Because batches are tiny and the canary verifies the deploy, the fix reaches prod in minutes with the same safety as any change — and the defect is actually *fixed*, not postponed.
- **Roll back (the exception).** When the problem is **costly *and* time-sensitive** — active customer/board impact, data at risk, revenue or compliance on the line — and a forward fix won't land fast enough, return to the last good version immediately to stop the bleeding, *then* fail forward at a calmer pace.

CD minimum #8 still requires you to **have** fast, rehearsed rollback — it's the emergency lever you pull when forward isn't fast enough. The shift is which one you reach for *first*.

Why fail-forward is usually right:

- A forward fix solves the problem; a rollback only defers it (you still have to fix and re-ship).
- It's safe by the same logic as any deploy — small batch, tests, canary.
- Rolling *code* back does not roll *data* back: if the bad version already wrote a new DynamoDB shape or emitted an SNS/SQS message, reverting code can strand you on data the old code can't read. Backward-compatible (expand/contract) changes keep both directions safe — and when state has already moved, failing forward is often the *only* safe option.

The fastest mitigation of all is often neither: if the bad behavior is behind a **feature flag, flip it off** — no deploy — then fix forward behind the flag.

Read [`examples/rollback-on-aws.md`](./examples/rollback-on-aws.md) for the decision guide and the AWS rollback mechanisms (re-running the last good GitLab deployment, plus canary and CloudFormation auto-rollback). Then **rehearse rollback in qa** so it's boring the day you actually need it.

---

#### 5.3 The database is no exception (bonus)

Everything in this session applies to schema. The pipeline is the **only** way to change a shared database; the migration runner is an **immutable artifact** built once and promoted (the *same* scripts run against dev → qa → prod, varying only the connection string); and recovery is **forward-only** — a bad migration is fixed by a new forward script (fail forward) plus expand/contract, never a hand-edit on a shared database. The end-state we are driving toward: schema and baseline data change only through automation, with local databases as the playground and no standing human DDL access. **DX owns that path**; here we make the mechanism concrete.

Worked end to end in [Database Migrations (DbUp)](./examples/db-migrations/README.md); the principles, drift, and cross-database tactics are in [Database Delivery](../../resources/database-delivery.md).

### 6. Workshop: Read the Target Pipeline & Plan the Migration (30 minutes)

#### 6.1 Score the baseline, then read the target (15 minutes)

We don't learn the pipeline against a strawman — we read our *own* real one first, then the target it's migrating toward.

First, **finalize your [Current-State Assessment](./exercises/current-state-assessment.md)** as a team. You started it after Session 1 with your real delivery numbers; now — with every CI and CD minimum and the governance controls in hand — score the rows you had to leave open. That completed scorecard, alongside your value stream map, is the Phase-0 baseline your migration plan builds on.

Then read the pipeline. Start with the [current-state walkthrough](examples/current-state-pipeline-walkthrough.md): an annotated tour of the **real** `iac-baseline` GitLab pipeline (`validate → build → dev → qa → prod`, OIDC auth, immutable SHA-tagged images). The headline finding is honest both ways — the baseline already does a *lot* right (single pipeline path, OIDC, immutable artifacts, config-with-artifact), but it gates **every** stage with `when: manual`, so a human, not the pipeline, owns releasability.

Now open the target — [`examples/.gitlab-ci.yml`](./examples/.gitlab-ci.yml), the full commit→prod pipeline for the Violations API. Locate:

1. Where the **definition of deployable** is enforced (which jobs gate the merge)
2. Where the **immutable artifact** is built and how it's promoted (not rebuilt)
3. Where **releasability is decided** — and how this differs from the baseline you just read (dev/qa auto-promote on green; only prod keeps a deliberate gate)
4. How **OIDC** gives each environment its own role with no static creds
5. Where you'd add a **smoke test** so promotion is verified, not just attempted

The full pipeline also runs one **merge-request-time** job — `release-impact-label` — which is why its `workflow:` enables MR pipelines alongside the branch pipeline (a deliberate step up from Session 2's branch-only form): it forces each change to declare its release impact before merge. Details in [Communicating Releases](../../resources/communicating-releases.md).

The gap between the two — all-manual gates versus green-flows-automatically — is the gap your migration plan closes.

#### 6.2 Write your migration plan (15 minutes)

Using the [Migration Checklist](../../resources/migration-checklist.md) and your completed [Current-State Assessment](./exercises/current-state-assessment.md), draft your team's plan:

- **Phase 0 (Assess):** your value stream map and scorecard + the one binding constraint they named
- **Phase 1 (Foundations):** the CI/TBD behaviors to adopt next (branch lifetime target, daily integration, flags, stop-the-line)
- **Phase 2 (Pipeline):** the gaps to close — automated definition of deployable, promote-don't-rebuild, rehearsed rollback, removing unnecessary manual gates
- **Phase 3–4 (Optimize / Deliver on demand):** what "done" looks like for your pilot service, and which DORA metric proves it

---

### 7. Wrap-up & Certification (5 minutes)

#### Key takeaways

- One pipeline, one path — shared environments deploy only through it, via OIDC
- "Deployable" is automated criteria; releasability belongs to the pipeline, timing to a human
- Communicate the *release* (the flag flip users feel), not the deploy — rebuild release notes from user-facing releases
- Build the artifact once; promote the same bytes; vary only config
- Fail forward by default; keep rollback fast and rehearsed, and reserve it for costly, time-sensitive problems
- The migration is phased: shrink batches, build the path, optimize, deliver on demand

#### Course completion

You've completed Continuous Delivery 101 when you can:

- Explain the MinimumCD practices without notes
- Decompose a feature into daily, flag-guarded increments
- Read a `.gitlab-ci.yml` and point to where releasability is decided
- Explain when to fail forward versus roll back, and how to do each on AWS
- Produce a phased migration plan for your team

Re-run the [Current-State Assessment](./exercises/current-state-assessment.md) in a month and measure how far you moved.

## 📚 Resources for This Session

- [Worked service: violations-api/](./examples/violations-api/)
- [Current-State Assessment](./exercises/current-state-assessment.md) — the Phase-0 scorecard you complete here
- [Current-state pipeline walkthrough](examples/current-state-pipeline-walkthrough.md) — the `iac-baseline` baseline we score
- [Target pipeline: .gitlab-ci.yml](./examples/.gitlab-ci.yml)
- [Rollback on AWS](./examples/rollback-on-aws.md)
- [Migration Checklist](../../resources/migration-checklist.md)
- [Communicating Releases](../../resources/communicating-releases.md)
- [Minimums Reference](../../resources/minimums-reference.md)
- [`iac-baseline`](https://gitlab.com/therealmanage/infrastructure/aws/iac-baseline) — our IaC conventions
- [Database Migrations (DbUp)](./examples/db-migrations/README.md) — schema as code, built once and promoted through the pipeline
- [Database Delivery](../../resources/database-delivery.md) — the minimums applied to schema; drift, cross-database coupling, and the end-state

---

**Previous:** [Session 2 ←](../session-2/README.md) | **Course Home:** [README ↑](../../README.md)
