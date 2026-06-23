# Worked Example: Scoring Our Current Pipeline Against the Minimums

The fastest way to understand CD is to hold a *real* pipeline up against the minimums and see what's already there and what's missing. This walkthrough uses the RealManage [`iac-baseline`](https://gitlab.com/therealmanage/infrastructure/aws/iac-baseline) GitLab pipeline — the canonical template every new AWS IaC project starts from. It is a genuinely good pipeline. It is also **not yet full CD**, and seeing *why* is the lesson.

> The baseline deploys infrastructure (CloudFormation). Your app services use SAM, but the pipeline *shape* is the same — which is exactly the point of having a baseline.

## The pipeline at a glance

```yaml
stages:
  - validate   # cfn-lint on every branch push
  - build      # build + push immutable, SHA-tagged Docker image
  - dev        # deploy to dev account   (when: manual)
  - qa         # deploy to qa account    (when: manual, needs dev)
  - prod       # deploy to prod account  (when: manual, needs qa)
```

Key facts about how it really works:

- **Auth is OIDC.** No static AWS credentials anywhere. The runner assumes `arn:aws:iam::${AWS_ACCOUNT}:role/gitlab-runner-role-${CI_ENVIRONMENT_NAME}` per environment.
- **Artifacts are immutable.** Images are tagged by `CI_COMMIT_SHA` and CI base images are **digest-pinned** (`@sha256:...`) because mutable tags have been re-pushed upstream before.
- **Config travels with the deploy.** Per-environment values live in `configuration/{dev,qa,prod}.config`, validated by a strict parser at deploy time.
- **Promotion is ordered.** `deploy:qa` has `needs: [deploy:dev]`; `deploy:prod` has `needs: [deploy:qa]`. You cannot skip an environment.
- **Every deploy is `when: manual`.** A human clicks `deploy:dev`, then `deploy:qa`, then `deploy:prod`.

## Score it against the minimums

| CD minimum | Baseline status | Notes |
| ---------- | --------------- | ----- |
| 1. Use CI | ⚠️ Partial | The pipeline supports it, but *daily integration / short branches* is a **team behavior**, not something the YAML can enforce. This is usually the real gap. |
| 2. Pipeline is the only way to deploy | ✅ Yes | Shared envs deploy only via the pipeline + OIDC. Laptops can only touch personal sandbox stacks. |
| 3. Pipeline decides releasability | ⚠️ Partial | The pipeline proves the artifact is *valid*, but a **human clicks every gate**, so a person — not the pipeline — pulls the trigger. Closing this gap is the heart of the migration. |
| 4. Definition of deployable | ⚠️ Partial | `cfn-lint` + `validate-template` + config validation are real gates. App services should add **unit tests, coverage thresholds, and dependency/security scans**. |
| 5. Immutable artifacts | ✅ Yes | SHA-tagged images, digest-pinned bases. Build once. |
| 6. Stop the line on red pipeline | ✅ Yes (by rule) | `allow_failure: false`; a red deploy blocks the chain. Whether the team *actually* drops everything to fix it is cultural. |
| 7. Production-like environment | ✅ Yes | `qa` mirrors `prod` via `configuration/qa.config`. |
| 8. Rollback on demand | ⚠️ Partial | Recovery = revert the commit and re-run the (manual) deploys, or admin break-glass. Works, but it isn't *fast, automated, rehearsed* rollback. |
| 9. Config with the artifact | ✅ Yes | `configuration/{env}.config` deploys with the stack. |

## What this tells us

**The baseline is strong on the "single trusted path" minimums** — one pipeline, OIDC, immutable artifacts, ordered promotion, config-with-artifact, prod-like qa. That's hard-won and worth protecting.

**The gaps are concentrated in three places:**

1. **Manual gates everywhere (minimum 3).** Today a human decides every deploy. That's a *valid Phase-2 state* — fine for early confidence or a compliance control — but it means the *pipeline* doesn't yet own releasability. The migration goal is to let green automatically flow at least to qa, and to make prod a single deliberate decision rather than three manual clicks per release.

2. **Definition of deployable is thin for app code (minimum 4).** The baseline validates *infrastructure*. A TypeScript Lambda service must add fast unit tests, a coverage threshold, and dependency/IaC security scanning to the `validate`/`test` stages so "green" actually means "deployable."

3. **Recovery isn't rehearsed (minimum 8).** "Revert and re-deploy manually" is a procedure, not on-demand recovery. Session 3 covers failing forward as the default plus Lambda alias shifting and CloudFormation rollback as the emergency lever.

**And the biggest gap isn't in the YAML at all.** Minimum 1 (real CI: daily integration, short-lived branches) is a *team* practice. A perfect pipeline pointed at week-old branches still isn't CD. That's why Session 2 comes before Session 3.

## How to use this in the workshop

1. Pull up *your* service's pipeline — for a new AWS service its `.gitlab-ci.yml`, for the .NET/IIS monolith the GitLab pipeline that `include:`s the shared `ci-templates` (MSBuild publish → IIS). Score *your* shape against the minimums, not the AWS one; use this baseline only if you have no pipeline at all yet.
2. Fill the same table for your pipeline in the [Current-State Assessment](../exercises/current-state-assessment.md).
3. For every ⚠️ or ❌, write one sentence: *what would it take to make this ✅?* That list is the start of your migration plan.

## Related

- The target pipeline we build toward: [Session 3](../README.md) and its [`.gitlab-ci.yml`](.gitlab-ci.yml)
- [Migration Checklist](../../../resources/migration-checklist.md)
