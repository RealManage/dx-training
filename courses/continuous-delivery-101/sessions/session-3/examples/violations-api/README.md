# Example Service: HOA Violations API

A deliberately small cloud-native service used throughout Continuous Delivery 101. It exists to make the CD practices concrete — it is a **teaching reference**, not a production service (there is no `node_modules`; treat the files as annotated illustrations).

## What it does

A TypeScript Lambda behind API Gateway that:

1. Accepts `POST /violations` to record an HOA violation
2. Assigns an escalation level (warning → 30 → 60 → 90-day) based on prior violations
3. Stores the violation in **DynamoDB**
4. Publishes a `ViolationRecorded` event to **SNS** (so a downstream notifier can email the homeowner)

```text
client ──POST /violations──▶ API Gateway ──▶ Lambda ──▶ DynamoDB (ViolationsTable)
                                              │
                                              └──▶ SNS (ViolationEventsTopic) ──▶ (notifier service)
```

## Files

| File | What it teaches |
| ---- | --------------- |
| [`template.yaml`](./template.yaml) | SAM/IaC. Per-env **parameters** (config with the artifact), feature-flag env vars, `AutoPublishAlias` + canary `DeploymentPreference` (rollback), SSM publishing — all on `iac-baseline` conventions. |
| [`src/handler.ts`](./src/handler.ts) | Pure domain logic + injected I/O + flag gating, so the CI gate is fast and the code ships dark. |
| [`src/handler.test.ts`](./src/handler.test.ts) | The "definition of deployable" in practice: fast unit tests, no AWS. |
| [`package.json`](./package.json) | `lint` / `test` / `build` (esbuild) / `validate` (sam) — the verbs the pipeline runs. |
| [`scripts/smoke-test.sh`](./scripts/smoke-test.sh) | "Verified, not hoped": one side-effect-free probe the deploy stage runs after every `sam deploy`, so promotion is proven, not assumed. |

## The immutable-artifact / promotion flow (CD minimums #5 and #9)

This is the heart of Session 3. **Build once; promote the same bytes; vary only config.**

```text
            ┌─────────────────────────── build ONCE ───────────────────────────┐
commit ───▶ │ npm run build  (esbuild bundle)                                   │
            │ sam build      (assemble the deployable)                          │
            │ sam package    (upload code to artifact S3, emit packaged.yaml)   │
            └───────────────────────────────────────────────────────────────────┘
                                        │  packaged.yaml + artifact (by content hash)
              ┌─────────────────────────┼─────────────────────────┐
              ▼                         ▼                         ▼
        sam deploy --config dev    sam deploy --config qa    sam deploy --config prod
        (Flag..=false, retain 7d)  (mirror prod, retain 30d) (deliberate gate)
              │                         │                         │
        SAME artifact              SAME artifact             SAME artifact
```

Why it matters:

- **The bytes that pass qa are the bytes that reach prod.** No rebuild means no "works in qa, breaks in prod" from a different build.
- **Environments differ by parameters only** — table names, flag values, log retention — passed at *deploy* time from `configuration/{env}.config`. The artifact never changes.
- **Secrets are never baked in.** Config references SSM/Secrets Manager ARNs; the artifact carries no secrets.

Contrast with the anti-pattern: running `sam build` separately in each environment rebuilds the artifact three times, so prod runs something qa never saw.

## Recovery hooks built into the template

When something breaks, the default move is to **fail forward** — ship a small fix through this same pipeline. These hooks make the rare *rollback* fast for when a problem is costly and time-sensitive:

- `AutoPublishAlias: live` → every deploy creates an immutable Lambda **version** and the canary shifts the `live` alias onto it. Roll back = re-run the last good deployment in GitLab (redeploys the prior artifact through the pipeline) — not a hand-edited alias.
- `DeploymentPreference: Canary10Percent5Minutes` + a CloudWatch error **alarm** → traffic shifts gradually and **auto-rolls-back** if errors spike, so most bad deploys never fully roll out.

Fail-forward vs roll-back decision guide and full strategies: [`../rollback-on-aws.md`](../rollback-on-aws.md).

## How it flows through the pipeline

See [`../.gitlab-ci.yml`](../.gitlab-ci.yml) for the full commit→prod pipeline that lints, tests, scans, builds the artifact once, and promotes it dev → qa → prod.

## Local experimentation

Deploy to a **personal sandbox stack** (not a shared environment, so it doesn't go through the pipeline):

```bash
npm ci
npm run build
sam build
sam deploy --stack-name dev-aws-violations-api-$USER --resolve-s3 \
  --parameter-overrides EnvironmentName=dev FlagViolationsRecord=true
```

Shared `dev`/`qa`/`prod` deploys happen **only** through the pipeline via OIDC — never from a laptop.
