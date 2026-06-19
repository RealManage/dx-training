# Continuous Delivery 101 — Review 2

**Student:** Riley (SRE / Platform Engineer, 9 yrs)
**Stance going in:** Round 1 I rated this 8.5 and said I'd champion it *after* a short block-list of technical fixes — chiefly an OIDC `before_script` that never exported credentials (deploy jobs couldn't authenticate) and a canary alarm wired to a single-error hair-trigger on the wrong (function-level) metric, which would roll back healthy deploys and quietly gut the fail-forward thesis. This round I'm not taking "fixed" on faith. I read the YAML.
**Review date:** 2026-06-19
**Overall rating:** 9.5/10 (was 8.5) — the two thesis-critical defects are fixed *correctly*, not hand-waved. One round-1 high-priority item (the smoke test) is still a referenced-but-absent file, and the new alarm has one small zero-traffic edge worth a comment. Neither is thesis-breaking.

## Executive summary

Both items I'd have blocked the MR on — the OIDC export and the canary alarm — are genuinely fixed, and fixed the *right* way (native web-identity for OIDC; alias-scoped metric-math error-rate for the alarm), not patched to look fixed. I verified the alarm has exactly one `ReturnData: true`, watches the `:live` alias via the `Resource` dimension, and trips on a 5% rate over two periods instead of one stray error. The cross-account artifact-bucket gap and the `npm audit` hard-gate are both resolved cleanly. The alias-shift drift foot-gun, the `git revert` mislabel, the merge-blocking mechanism, and data-in-flight are all addressed in prose. What's *still* open: the smoke test the pipeline calls (`./scripts/smoke-test.sh`) still doesn't exist anywhere in the repo, so "verified, not hoped" remains a hope; and the new error-rate alarm divides by `invocations`, which is zero on an idle alias — a real-but-minor CloudWatch edge the comment doesn't mention. The new docs (governance break-glass, communicating-releases, strangler-fig) are technically sound; I found one nit, not an error.

## Round-1 item-by-item verdict

### Audit #1 — OIDC `before_script` didn't export credentials → **FIXED-CORRECTLY**

`sessions/session-3/examples/.gitlab-ci.yml:55-66`. The old elided-`sts`-call-plus-jq pattern is gone, replaced with the native web-identity pattern I recommended:

```yaml
.aws_oidc:
  id_tokens:
    GITLAB_OIDC_TOKEN: { aud: "https://gitlab.com" }
  before_script:
    - echo "$GITLAB_OIDC_TOKEN" > /tmp/gitlab-oidc-token
    - export AWS_WEB_IDENTITY_TOKEN_FILE=/tmp/gitlab-oidc-token
    - export AWS_ROLE_ARN="arn:aws:iam::${AWS_ACCOUNT}:role/gitlab-runner-role-${CI_ENVIRONMENT_NAME}"
    - export AWS_ROLE_SESSION_NAME="ci-${CI_PIPELINE_ID}"
    - aws sts get-caller-identity
```

Mechanics check: the AWS SDK/CLI credential-provider chain reads `AWS_WEB_IDENTITY_TOKEN_FILE` + `AWS_ROLE_ARN` (+ optional `AWS_ROLE_SESSION_NAME`) and calls `AssumeRoleWithWebIdentity` itself, caching and refreshing the session. No manual `sts` call, no `jq`, nothing stored. Every subsequent `aws`/`sam` invocation is authenticated. This is correct and is the shorter, idiomatic form. The `aws sts get-caller-identity` on line 66 is a real fail-fast (with the comment "fail fast if the role or its trust policy is wrong") — it forces credential resolution before any deploy step, so a broken trust policy errors in `before_script` instead of mid-`sam deploy`. Good SRE instinct, exactly what I'd want.

Two small notes, neither blocking:

- **`aud: "https://gitlab.com"`** is the conventional audience, but the value is only correct if the IAM role's trust policy `Condition` matches it (`gitlab.com:aud`). That's an out-of-repo coupling the course can't show, but a one-line "this `aud` must match the role's trust-policy condition" would close the loop for anyone debugging an `AssumeRoleWithWebIdentity` `InvalidIdentityToken`. Nice-to-have.
- The `.aws_oidc` `before_script` **overrides** `default.before_script` (the `npm ci` on line 52), so deploy jobs don't run `npm ci`. That's correct here — deploy jobs run in the SAM build image against the pre-built packaged template and never need node_modules — but it's an implicit dependency on YAML override semantics. Not a defect; just noting I checked it.

### Audit #3 — canary alarm mis-wired (wrong dimension + hair-trigger) → **FIXED-CORRECTLY**

This was my central objection — the whole fail-forward thesis rests on the canary actually guarding the deploy. `sessions/session-3/examples/violations-api/template.yaml:112-154`. The naive `Errors`/`Threshold:1`/`FunctionName` alarm is gone, replaced with proper metric-math error-rate scoped to the alias. I verified every part I flagged:

1. **Exactly one `ReturnData: true`.** `errors` (line 128) and `invocations` (line 138) are both `ReturnData: false`; only `errorRate` (line 150) is `true`. CloudWatch requires **exactly one** metric in a metric-math alarm to return data — this is correct. (A common failure mode is leaving the raw metrics `true`, which errors at stack-create; this template gets it right.)
2. **Resource dimension scopes to the `:live` alias.** Both metrics use `{ Name: Resource, Value: !Sub "${EnvironmentName}-${ServiceName}:live" }` (lines 134, 144). Lambda emits per-alias metrics under the `Resource` dimension as `functionName:aliasName` — so this watches the alias CodeDeploy is actually shifting traffic through, not the function-level aggregate that mixed the stable 90% with the canary 10%. Correct. The inline comment (lines 116-125) even explains *why* the bare `FunctionName` was wrong and notes that strict per-version isolation would add `ExecutedVersion` — accurate, and the right pragmatic call.
3. **Sensitivity is now a rate, not a hair-trigger.** `Expression: "100 * errors / invocations"`, `Threshold: 5`, `EvaluationPeriods: 2`, `ComparisonOperator: GreaterThanThreshold` (lines 148-153). So: roll back only if **>5% of alias traffic errors for two consecutive 60s periods**. That scales with traffic and won't trip on one cold-start blip or a single client-induced handler error. This is the fix I asked for, implemented properly.

**One new, minor edge the fix introduces — divide-by-zero on an idle alias.** `100 * errors / invocations` is `x/0` when the alias gets zero invocations in a period (low-traffic prod, or the quiet part of the canary window). CloudWatch metric math treats divide-by-zero / empty as *missing* data for that period rather than erroring, and `TreatMissingData: notBreaching` (line 154) then correctly keeps the alarm OK — so the **behavior is safe** (it won't false-trip on idleness, and it won't mask a real spike because errors-with-zero-invocations isn't a reachable state). But two things are worth a one-line comment: (a) the `notBreaching` choice is *load-bearing* for the zero-traffic case and isn't explained, and (b) on a genuinely idle prod alias the canary's "alarm stays OK → shift the rest" can complete on **no evidence at all** — the canary verified nothing because nothing called it. That's inherent to rate alarms on low-traffic functions, not a bug, but an SRE reading this should know the canary is only as strong as the traffic during the window. Verdict stays FIXED-CORRECTLY; this is a teaching-comment gap, not a wiring error.

### Audit #4 — alias-shift drift under-warned → **PARTIALLY ADDRESSED**

`sessions/session-3/examples/rollback-on-aws.md:63`. The reconciliation rule is stated: *"this is an emergency action; reconcile the stack afterward (redeploy the good artifact through the pipeline) so IaC and reality agree."* That's correct and names the right next action. But I asked for it to **lead** the section and be bolded as a foot-gun, because the failure mode is severe (an out-of-band `update-alias` + any later unrelated `sam deploy` silently reintroduces the bug — CloudFormation still believes the broken version is current). It's now a trailing bullet, not a led-with warning, and the specific "your *very next* deploy must be the known-good artifact, before anything else touches the stack" causation isn't spelled out. The information is present and accurate; the emphasis I argued for isn't. Down-grading from "open" to partial — the reader is no longer un-warned, just under-warned.

### Audit #5 — cross-account artifact-bucket gap → **FIXED-CORRECTLY**

Resolved in three places, consistently:

- `.gitlab-ci.yml:40-43` — the `ARTIFACT_BUCKET` variable now carries the explicit comment: *"Multi-account estates: this bucket must be readable by EVERY environment's deploy role (shared artifacts account or a cross-account bucket policy) so each stage promotes the SAME bytes instead of rebuilding per account."* That's exactly the requirement I said was missing.
- `sessions/session-3/README.md:85` — a "Multi-account note" callout states the same thing in prose and explicitly warns "Rebuilding per account would silently break the immutability guarantee. Make the bucket cross-account-readable; don't rebuild." Correct framing.

The single most common real-world break of build-once-promote on multi-account AWS is now called out. Fixed.

### Audit #7 — `git revert` filed under "rollback" → **FIXED-CORRECTLY (adequately)**

`rollback-on-aws.md:76-84`, Strategy 3 ("Redeploy the previous immutable artifact — the clean path"). The two paths are now distinguished within the strategy: re-running `deploy:prod` against the *prior stored* packaged template (true rollback, "a deploy, not a rebuild if you target the stored artifact," line 83) vs. `git revert` letting the pipeline "build + promote the revert" (line 81 — which is the fail-forward rebuild). The "Speed: minutes (a deploy, not a rebuild if you target the stored artifact)" line makes the distinction I asked for. It's not as surgically separated as I'd phrase it (the `git revert` bullet still lives under a heading that says "rollback"), but the rebuild-vs-redeploy difference — the one that matters — is now explicit. Good enough; the course's central distinction is no longer muddied.

### Audit #13 — `npm audit` hard-gate vs stop-the-line → **FIXED-CORRECTLY**

`.gitlab-ci.yml:93-100`. The `dependency-audit` job now has `allow_failure: true` (line 100) and runs `npm audit --omit=dev --audit-level=high` (line 99). Both changes are right: `allow_failure: true` makes it advisory so overnight advisory churn can't turn `main` red with zero code change, and `--omit=dev` scopes it to production dependencies so dev-only advisories (which don't ship) don't generate noise. The comment (lines 95-98) articulates the exact tension I raised — "blocking on a noisy signal ... just trains teams to route around it." This is the honest treatment I asked for. Note the cross-reference: `deploy:dev` still lists `dependency-audit` in `needs:` (line 155), but because the job is `allow_failure: true` a failed audit won't block promotion — consistent with the advisory intent.

### Audit #14 — `CI_COMMIT_SHA` vs SAM content-hash blur → **PARTIALLY ADDRESSED**

`sessions/session-3/README.md:83` now adds the correct prose: *"`sam package` uploads the code to an artifact bucket and emits a packaged template referencing it by a content hash."* And `violations-api/README.md:39` labels the artifact "by content hash" in the diagram. So the content-hash concept is now *present* and correct. But the headline diagram at README.md:75 still says "(same artifact, tagged by `CI_COMMIT_SHA`)" without distinguishing that the SHA keys the *pipeline/S3 prefix* while SAM's content hash keys the *zip*. The two keys are now both mentioned in the surrounding prose, so an attentive reader can reconstruct "which bytes," but the diagram itself still conflates them. Improved; not fully sharpened. Minor.

### Audit #15 — `CAPABILITY_IAM` may be insufficient → **STILL OPEN (low severity)**

`.gitlab-ci.yml:142` still uses `--capabilities CAPABILITY_IAM` only. The template (`template.yaml`) sets a named `FunctionName` (line 82) and named DynamoDB/SNS/SSM resources, and uses the `AWS::Serverless` transform. In practice the `AWS::Serverless-2016-10-31` transform is expanded server-side by CloudFormation and SAM-created IAM roles here are not custom-*named* (SAM auto-names the execution role), so `CAPABILITY_IAM` *may well* be sufficient for this specific template — but I flagged this as "verify against a real `sam deploy`," and there's no evidence that verification happened. If anyone adds a `RoleName` or a `CAPABILITY_AUTO_EXPAND`-requiring macro later, the deploy fails. Low severity for the template as-written; still technically unverified. Leave a note or add `CAPABILITY_NAMED_IAM CAPABILITY_AUTO_EXPAND` defensively (harmless if unneeded).

## Round-1 objections (the non-audit list)

### Objection #3 — smoke test referenced but never shown → **STILL OPEN (high priority)**

This is the one real miss. `.gitlab-ci.yml:149` still calls `./scripts/smoke-test.sh "${CI_ENVIRONMENT_NAME}"` and the reading guide (line 181) still lists "Verified, not hoped .......... smoke-test.sh after every deploy." I searched the whole course tree: there is **no `scripts/` directory and no `*.sh` file anywhere** in `continuous-delivery-101/`. The `violations-api/` tree is `README.md`, `package.json`, `template.yaml`, `src/handler.ts`, `src/handler.test.ts` — no smoke test. So the single most important "promotion is verified" claim still rests on a file that doesn't exist. The pipeline as written would fail at the smoke-test step (no such file) on every deploy, and "verified, not hoped" is still, itself, a hope. This was a high-priority round-1 item for me and Jordan both; it didn't land. A 10-line script (curl the `ApiUrl` stack output, assert 501-while-dark / 201-when-flagged) would close it. **Recommend blocking on this before the course ships, or dropping the smoke-test claim and the `needs`/reading-guide references to it.**

### Objection — data-in-flight (SNS/SQS mid-stream during producer rollback) → **FIXED-CORRECTLY**

`rollback-on-aws.md:39` extends the data-trap to in-flight: *"If the bad version already wrote a new DynamoDB shape, enqueued an SQS message, or published an SNS event, reverting the code can leave you reading data the old code doesn't understand."* And `session-3/README.md:114` echoes "emitted an SNS/SQS message." This is the data-in-flight hazard I said was missing — the SNS→SQS→consumer topology in this very example creates it, and it's now named. Good.

### Objection #2 — merge-blocking mechanism unnamed → **ADDRESSED (in governance doc)**

`resources/governance-and-compliance.md:43-44` names it: *"The author **cannot merge unreviewed** ... (Configure the project so an author cannot approve their own MR.)"* and the audit table ties approval to the MR. This is the segregation-of-duties / project-setting mechanism I said was hand-waved. It's not in the pipeline YAML comments (where a reader building the pipeline would look), but it is now explicitly stated in the governance resource. Adequate; a one-line pointer from `.gitlab-ci.yml` or session-3 §2.2 to "Pipelines must succeed / disallow self-approval is a GitLab project setting, not pipeline YAML" would make it bulletproof.

### Observability under-weighted → **ADDRESSED**

`resources/migration-checklist.md:73` now has an explicit Phase 2 line: "Add observability: metrics, logs, traces, deployment markers, alerts on the four DORA signals," and line 74 ties smoke tests/health checks/canary metrics to replacing manual gates. `resources/what-cd-costs.md:49` debits observability as a real cost. Deployment markers and the four signals are now named. Good enough to clear my round-1 complaint that observability was a footnote.

## New docs — technical fact-check

- **`resources/governance-and-compliance.md` (break-glass, lines 71-83):** Technically sound. Pre-authorized + narrowly-scoped + time-boxed elevated access, logged-and-alerted-on-use, mandatory post-incident reconciliation (re-apply through the pipeline, add the bypassed gate), rare-by-design. That's a correct break-glass definition — it matches how a real regulated shop runs it. The audit-table mapping (lines 57-63) is accurate: commit/MR = who+what, MR approval = who approved, pipeline run = what verification, deploy job = when+by-which-role, content-hash = bytes-tested-equal-bytes-shipped. No errors.
- **`resources/communicating-releases.md`:** No technical inaccuracies. The canary/dark-launch point for support (lines 84-86 — "a feature may be live for 10% of users before it's announced") is correct and is exactly the operational reality the flag inventory should track. Consistent with the alias/canary mechanics in the template.
- **`sessions/session-3/examples/strangler-fig-violations.md`:** This is strong, and the technical claims hold:
  - **IIS recovery (line 119-120):** "flip the routing flag — reads/writes return to the monolith instantly" as the fast lever, "redeploy the prior published artifact to IIS" as hard rollback. Both are accurate for an IIS/VM deploy; the routing-flag-as-fastest-recovery point (line 122-125) is the correct insight — the old path still exists until slice 7, so turning the new path off beats any redeploy.
  - **Expand/contract on SQL Server (lines 92-111):** Correct and concrete. `ALTER TABLE ... ADD ExternalViolationId UNIQUEIDENTIFIER NULL` as a backward-compatible expand (nullable add, old code ignores it, reversible), then write-both → migrate-readers → contract (`NOT NULL`/drop only after no reader depends on the old shape). That's textbook expand/contract and the SQL is valid T-SQL. The "other people read your tables" coordination point (lines 84-87) is the real-world reason contract is slow — accurately stated.
  - **Dual-write idempotency (lines 56-57, 78-83):** Correct. Keying every write on a stable `violationId` so retries and the backfill can't double-count, watermarking the resumable backfill by timestamp, and explicitly naming SQL as authoritative until slice 6 — that's the right design, and the fee-correctness warning (line 79-80) is the right thing to scare people with.
  - One nit, not an error: the table header says "eight slices" in prose (line 131) but the table is slices 0-7 (eight rows, correct) — internally consistent, just flagging I counted.

## Recovery mechanics (persona-specific)

My home turf, and the verdict has flipped from round 1. The fail-forward thesis now **rests on a net that actually holds**, because the canary alarm is correctly wired (alias-scoped, rate-based, exactly-one-ReturnData). That was the load-bearing fix and it's real. The data trap now covers data-in-flight. The alias-shift drift rule is present (if not led-with). Rehearsal is still correctly gated as the bar for minimum #8 (`rollback-on-aws.md:112-120` — "A recovery move you've never run is a hope, not a capability," now with a fail-forward-loop rehearsal step added at line 117, which is the right addition). The remaining hole is operational, not conceptual: the smoke test that's supposed to prove each promotion doesn't exist, and the new rate-alarm's zero-traffic behavior deserves one sentence so an on-call engineer doesn't assume "canary green = canary verified" on an idle alias.

## Recommendations

### High priority (I'd still block on this one)
1. **Ship `scripts/smoke-test.sh`** (Objection #3) — it's called on `.gitlab-ci.yml:149` and absent from the repo; the pipeline can't run as written and "verified, not hoped" is unbacked. ~10 lines: read the `ApiUrl` stack output, curl it, assert 501-dark / 201-flagged. Or remove the call and the reading-guide claim.

### Medium priority
2. **Add one comment to the canary alarm** about the zero-invocations edge: divide-by-zero → missing → `notBreaching` keeps it OK, and note that on an idle alias the canary verifies nothing. (template.yaml around line 148-154.)
3. **Lead the alias-shift section with the drift rule** and spell the causation (Audit #4) — out-of-band `update-alias` → next deploy reintroduces the bug → so redeploy the known-good artifact *first*. Information's there; emphasis isn't.
4. **Verify or defensively widen capabilities** (Audit #15) — confirm `CAPABILITY_IAM` deploys this template, or add `CAPABILITY_NAMED_IAM CAPABILITY_AUTO_EXPAND`.

### Nice to have
5. One line near the OIDC block: "`aud` must match the role's trust-policy `Condition`" — closes the most common `InvalidIdentityToken` debugging loop.
6. Sharpen the README.md:75 diagram to distinguish the `CI_COMMIT_SHA` (S3 prefix / pipeline key) from SAM's content hash (zip key) — the prose now says both; the diagram still conflates (Audit #14).
7. Cross-link the merge-blocking project-setting note from session-3 §2.2 or the pipeline YAML, not only the governance doc (Objection #2).

## Verdict

**Champion, with one block-item left.** The two defects I'd have blocked the MR on are fixed *correctly* — I read the YAML and the OIDC native-web-identity pattern authenticates, and the canary alarm now watches the `:live` alias error-rate with exactly one `ReturnData:true` and a sane 5%-over-two-periods threshold instead of a one-error hair-trigger. The fail-forward thesis is no longer resting on a hole. The cross-account bucket, the `npm audit` advisory gate, data-in-flight, governance/break-glass, and the strangler-fig SQL Server expand/contract are all technically sound. The remaining gap is the smoke-test file that's referenced but doesn't exist — fix that (or drop the claim) and there's nothing left I'd hold the MR on. Up from 8.5 to **9.5/10**.
