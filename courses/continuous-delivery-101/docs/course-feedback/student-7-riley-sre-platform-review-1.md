# Continuous Delivery 101 — Review 1

**Student:** Riley (SRE / Platform Engineer, 9 yrs)
**Stance going in:** I don't care if the prose is inspiring. I care whether the AWS/GitLab mechanics are *correct* and *safe*. Show me a rollback path I'd trust at 2am, an OIDC config that actually authenticates, and a canary that does what the doc says — or admit it's pseudocode.
**Review date:** 2026-06-19
**Overall rating:** 8.5/10 — yes, I'd champion this, with a short list of fixes I'd block on before it ships to a real on-call team.

## Executive summary

This is the rare CD course that doesn't lie about the mechanics. The recovery model (fail-forward-first, rollback as the costly-and-time-sensitive lever) is correct and well-argued, the rollback data trap is called out explicitly — which most courses miss — and the immutable-artifact / build-once-promote story is technically sound for SAM. The violations-api example is genuinely good: pure-logic-plus-injected-IO handler, fast flag-gated tests, a template with `AutoPublishAlias` + canary + an alarm wired in. I went in expecting plausible-sounding nonsense and mostly didn't find it.

What keeps it from a 9.5 is a handful of specific technical defects that an SRE *will* hit the moment they copy-paste: the OIDC `before_script` doesn't actually export credentials (it's elided in the one place it matters), the canary alarm is statically wired to a non-canary metric and will misfire, the alias-shift rollback example invites a CloudFormation drift trap the doc under-warns about, and the artifact promotion claim has a real subtlety (`sam package` uploads code to S3 but the *deploy* still references a single fixed bucket per account) that the "same bytes" framing glosses. None of these are fatal; all are fixable; and an SRE reviewing the MR would flag every one.

## Section-by-section

### Course framing (README)
Solid and honest. It refuses to call CD a product, frames manual gates as a transitional compromise rather than the goal, and keeps CD ≠ Continuous Deployment crisp. The "warning signs" and "litmus test" are the right instincts.

One nit that matters to me: the README links the baseline as `gitlab.com/therealmanage/infrastructure/aws/iac-baseline` (README.md line 84, 203). The actual baseline the tooling fetches is `therealmanage/tools/dx/dx-iac-baseline`. If a student clicks that link to "align with the baseline" and lands on a 404 or a stale repo, the whole "we align with iac-baseline" credibility takes a hit. Verify and fix the URL everywhere it appears (README, session-1 walkthrough, glossary, session-3).

### Session 1 — Why CD & the Minimums
The self-reinforcing-trap loop and batch-size-as-master-variable framing are correct and the DORA citation is accurate (frequency *and* stability move together — not a tradeoff; that's the actual *Accelerate* finding). No technical complaints.

The current-state pipeline walkthrough (`session-1/examples/current-state-pipeline-walkthrough.md`) is the strongest single artifact in the course for my lens. It scores the *real* baseline honestly: OIDC ✅, immutable SHA-tagged + digest-pinned images ✅, ordered promotion via `needs:` ✅, config-with-artifact ✅, and it correctly identifies that `when: manual` everywhere means the *human*, not the pipeline, owns releasability (line 32). The role ARN `arn:aws:iam::${AWS_ACCOUNT}:role/gitlab-runner-role-${CI_ENVIRONMENT_NAME}` (line 20) matches the convention. This is the part I'd hand to a skeptical platform team.

### Session 2 — Trunk-Based Development & CI
The feature-flag example (`session-2/examples/feature-flag.ts`) is clean: default-off, env-var driven, resolved once at cold start, and — importantly — it tells you *when to graduate* to AppConfig/LaunchDarkly (runtime toggle without a deploy, per-cohort targeting, audit trail). That last point is the one most flag tutorials omit, and it's exactly the question an SRE asks. Good.

The CI front-half YAML (`session-2/examples/ci-pipeline.gitlab-ci.yml`) is fast-feedback-correct: digest-pinning is called out, the SAM image is used for `sam validate`, coverage is gated via the runner config (not the regex — the doc correctly says the *threshold* is enforced in `vitest.config.ts`, line 78-80; the `coverage:` regex is just for the GitLab badge). That's a subtlety many people get wrong, and the course gets it right.

Two notes:
- The `workflow:rules` block disables MR pipelines and runs branch pipelines (lines 22-26). Defensible to avoid duplicate pipelines, but it means MR-scoped variables and `merge_request`-only features (e.g. merge-results pipelines, `Codequality` MR widgets) won't fire. For a course teaching "tests gate the *merge*," running only branch pipelines is a slightly awkward fit — the gate is on the branch, and the MR merge-blocking depends on GitLab's "pipelines must succeed" project setting, which the course never mentions. Worth a one-line callout.
- `npm audit --audit-level=high` is fine as a teaching gate, but it's notoriously noisy/flaky in CI (advisories appear overnight and turn `main` red with no code change). For a course that *also* preaches stop-the-line, that's a real tension. A sentence acknowledging "security advisory churn is why teams often make this `allow_failure: true` or pin to a scheduled job" would be honest.

### Session 3 — The Pipeline
This is where I focused. The teaching points are right: single path, definition-of-deployable-as-code, build-once-promote, prod-like qa, fail-forward-first. The `.gitlab-ci.yml` correctly auto-promotes dev/qa on green and keeps prod `when: manual` to demonstrate Continuous Delivery (not Deployment). The reading guide at the bottom is a nice touch for a workshop.

But the deploy template has concrete defects — see the audit table below. The headline ones: the OIDC credential export is elided exactly where it's load-bearing; the canary alarm is mis-wired; and the "same bytes" promotion story has a real gap between `sam package` (uploads code) and `sam deploy` (which the example runs per-environment with a per-account bucket).

The rollback doc (`session-3/examples/rollback-on-aws.md`) is the best recovery write-up I've seen in an internal course — fail-forward default, costly-AND-time-sensitive test for rollback, the data trap, ECS equivalents, and "rehearse it or you don't have it." My corrections to it are precision fixes, not rewrites.

### Resources (minimums, glossary, checklist, troubleshooting)
Accurate and consistent with the sessions. The glossary's Lambda alias/version and canary definitions are correct. The minimums reference correctly frames #8 as "capability required, fail-forward in practice." The migration checklist's Phase 2 and Phase 3 split rollback rehearsal correctly (implement + rehearse in Phase 2; automate-on-health-check in Phase 3). The troubleshooting expand/contract answer is correct. No factual issues in the resources.

### Exercises — my attempt

**Current-state assessment** — I filled it for a hypothetical platform-owned AWS service fleet (a dozen small Lambda/ECS services on the baseline):

CI: TBD *Partial* (new services yes, strangler-fig work off the monolith still spawns week-long branches); daily integration *Partial*; tests-before-merge *Yes*; tests-on-merge *Yes*; stop-the-line *Partial* (we say it, deadline pressure breaks it); don't-break-delivered *Partial* (no enforced contract testing between services).

CD: pipeline-only-deploy *Yes* (OIDC, no laptop creds to shared); pipeline-decides-releasability *No* (every stage `when: manual` on the baseline); def-of-deployable *Partial* (infra validated, app-code gates uneven across services); immutable *Yes*; red-pipeline-stops-work *Partial*; prod-like-env *Yes*; **rollback-on-demand-AND-rehearsed *No*** — this is my honest answer and it's the one that stings, because we *have* the alias/canary machinery but have never run a timed rollback drill; per the course's own bar (rollback-on-aws.md line 114-120), unrehearsed = not met. The exercise forced that admission, which is the point. Worked well.

Where it was hard: the assessment has no row for **observability** (deployment markers, SLOs, alerting on the four signals). For an SRE that's a glaring omission — you can hit every minimum and still be flying blind on whether a deploy regressed latency. More on this below.

**Decompose a branch (deploy/observability angle)** — I took the same violations-API feature and decomposed it, then looked at it through deploy/recovery eyes rather than just "can it merge." My nine slices matched the model answer closely, but the SRE lens surfaces things the exercise doesn't ask about:

- **Slice 2 (add DynamoDB table)** is *not* as harmless as "infra only, unused" implies. Adding a stateful resource with `DeletionPolicy: Retain` is fine; but the *order* matters for rollback — once that table exists and slice 3 starts writing, a code rollback past slice 3 strands you (the data trap the course teaches elsewhere but doesn't connect *here*). The decompose exercise should cross-reference the data trap explicitly at the DynamoDB slice.
- **Slice 6 (SNS publish)** crosses a service boundary — a downstream notifier consumes `ViolationRecorded`. The exercise treats the flag flip as a local release decision, but flipping `violations.notify` on changes the *contract* an external consumer sees. The decompose exercise never mentions that turning a flag on can be a breaking change for *consumers* even when it's backward-compatible for *you*. That's an expand/contract concern at the event-schema level, and it's exactly where real incidents come from.
- **Every flag flip needs a deployment marker and an alarm baseline**, or you can't tell whether the flip caused the regression. The exercise ends at "flip the flag = release decision" and never asks "how would you know if the flip broke something?" For a decomposition meant to make releases *safe*, the observability step is missing.

The exercise is good for what it claims to teach (batch decomposition). It just stops at the merge boundary; the *operate* half is absent.

## Where it lost me / objections it didn't answer

1. **Observability is treated as a footnote, not a minimum.** The template sets `Tracing: Active` (X-Ray) and there's one CloudWatch alarm, and the migration checklist mentions observability once in Phase 3 (line 69). But the whole CD safety argument — "fail forward is safe because the canary verifies it" — *depends* on observability you can trust. If your alarms are wrong (and one in this course is — see audit), the canary auto-rollback either never fires or fires constantly. The course asserts the safety net without teaching how to know the net is real. For an SRE, **observability is load-bearing for the entire fail-forward thesis** and it's under-weighted.

2. **"The pipeline decides releasability" is asserted but the *enforcement* is hand-waved.** Auto-promoting dev→qa on green is shown in YAML, but what actually *blocks a bad merge*? The branch pipeline runs after the merge to a feature branch, not on `main` pre-merge, and merge-blocking is a GitLab project setting ("Pipelines must succeed") the course never names. A reader could build this exact pipeline and still merge red to `main` if that box is unchecked. The mechanism behind the claim needs one concrete sentence.

3. **Smoke test is referenced but never shown.** `.gitlab-ci.yml` line 139 calls `./scripts/smoke-test.sh "${CI_ENVIRONMENT_NAME}"` and the reading guide says "verified, not hoped" — but there's no smoke-test file in `violations-api/`. The single most important "promotion is verified" claim has no implementation. At minimum, show a 10-line smoke test (curl the API Gateway URL from the stack output, assert 501-while-dark / 201-when-flagged). Right now "verified, not hoped" is itself a hope.

4. **No mention of in-flight messages / queue draining on rollback.** The rollback doc covers the DynamoDB data trap well, but SNS→SQS→consumer is the actual topology here, and rolling back the *producer* while messages with the new schema are mid-flight in a downstream queue is a classic poison-message incident. The data trap is taught as "data at rest"; the "data in flight" version is missing.

## Confusing or assumed (clarity)

- The artifact diagram (session-3 README lines 71-77 and violations-api/README lines 33-46) says "same artifact, tagged by `CI_COMMIT_SHA`" but for SAM the thing promoted is the **packaged template** plus the **code zip in S3 keyed by content hash** — the *SHA tag* is on the CI artifact/pipeline, not on the Lambda zip (SAM names the zip by its own content hash). The course mostly gets this right in prose (session-3 README line 81) but the diagrams blur "CI_COMMIT_SHA" and "content hash" as if they're the same key. They're related but not identical. An SRE debugging "which bytes are in prod" needs that distinction sharp.
- `CAPABILITY_IAM` is used (`.gitlab-ci.yml` line 132) but the template creates named IAM via SAM policy templates and a `FunctionName`/named resources; depending on what SAM generates you often need `CAPABILITY_NAMED_IAM` or `CAPABILITY_AUTO_EXPAND` (the latter is *required* for the `AWS::Serverless` transform with certain macros). A copy-paste deploy may fail on capabilities. Worth verifying against an actual `sam deploy` of this template.

## Technical accuracy audit

| # | Claim / artifact | Where | Verdict | Fix |
| - | ---------------- | ----- | ------- | --- |
| 1 | OIDC role assumption via `aws sts assume-role-with-web-identity ... > /tmp/creds.json` then "(export … elided for brevity)" | `session-3/.gitlab-ci.yml` 56-61 | **Oversimplified → effectively wrong** | The elided step is the only one that matters — without exporting `AWS_ACCESS_KEY_ID/SECRET/SESSION_TOKEN` from the JSON, every subsequent `aws`/`sam` call is unauthenticated and the job fails. Also, modern GitLab+AWS uses the `web_identity_token_file` / `AWS_WEB_IDENTITY_TOKEN_FILE` + `AWS_ROLE_ARN` env-var pattern so the SDK assumes the role natively — no manual `sts` call or `jq` juggling needed. Show that pattern; it's both correct and shorter. |
| 2 | `DeploymentPreference: Canary10Percent5Minutes` shifts 10% for 5 min then the rest; auto-rolls-back on alarm | `template.yaml` 89-92; `rollback-on-aws.md` 65-74 | **Correct (mechanism)** | The CodeDeploy traffic-shift semantics and auto-rollback-on-alarm are accurately described. Keep. |
| 3 | The canary alarm: `AWS/Lambda Errors`, `Threshold: 1`, dimension `FunctionName: !Ref ViolationsFunction` | `template.yaml` 112-125 | **Wrong (will misfire / won't guard the canary)** | Two defects: (a) `!Ref ViolationsFunction` resolves to the function *name*, but the canary shifts traffic on the **alias**, so the alarm should watch the aliased version's metric (`Resource` dimension = function:alias) or per-version metrics — a function-level Errors alarm includes the stable 90% and the canary 10% mixed together, defeating the point. (b) `Threshold: 1` / `Period: 60` / `EvaluationPeriods: 1` means **a single error in any minute trips rollback** — including one client 4xx-induced handler error or a cold-start blip. In prod that alarm rolls back healthy deploys constantly. Use a rate or a higher threshold, and scope the dimension to the alias. As written, the safety net is a hair-trigger pointed at the wrong target. |
| 4 | Alias-shift rollback: `aws lambda update-alias --function-version 7` | `rollback-on-aws.md` 51-63 | **Correct but under-warned (drift trap)** | The command works and is genuinely seconds-fast. But it puts the **alias out of sync with CloudFormation** — the next `sam deploy` will happily shift `live` back to the broken version because the stack still believes that's current. The doc says "reconcile afterward" (line 63) but buries the severity. For an SRE this is a *foot-gun*: an out-of-band alias change + an unrelated later deploy = silent re-introduction of the bug. Bold it, and state the rule: after any manual alias shift, the *very next action* is to redeploy the known-good artifact through the pipeline before any other deploy touches the stack. |
| 5 | "Build once, promote the same bytes" via `sam package` then per-env `sam deploy --template-file packaged.yaml` | `.gitlab-ci.yml` 97-139; READMEs | **Mostly correct, one real gap** | The packaged template + S3 content-hashed zip *is* immutable and promoted as-is — good. The gap: `sam package` uploads to `ARTIFACT_BUCKET` in the **dev account** (build job runs `environment: dev`, lines 101-102), but qa/prod deploys assume per-account roles. A Lambda in the prod account cannot pull its code zip from a dev-account S3 bucket without an explicit cross-account bucket policy. Either the artifact bucket must be a shared/central account with cross-account read, or you re-`sam package` per account (which breaks "same bytes"). The course never resolves this — it's the single most common real-world break of build-once-promote on multi-account AWS. Call out the cross-account artifact bucket requirement explicitly. |
| 6 | `AutoPublishAlias: live` creates an immutable version per deploy; API Gateway invokes the alias | `template.yaml` 87; `rollback-on-aws.md` 45-49 | **Correct** | Accurate. Minor: the SAM `Api` event integrates with the function's alias automatically when `AutoPublishAlias` is set, so "API Gateway invokes the alias" holds. Keep. |
| 7 | `git revert` the bad commit and let the pipeline build+promote the revert (Strategy 3) | `rollback-on-aws.md` 80-82 | **Correct, but it's fail-forward, not rollback** | A `git revert` re-builds a *new* artifact and runs the full pipeline — that's the fail-forward path, mislabeled under "rollback mechanisms." Re-running `deploy:prod` against the *prior stored* packaged template is the true rollback (and the doc lists both together). Separate them: redeploying the stored prior artifact = rollback (minutes, no rebuild); `git revert` = fail-forward (rebuild). Conflating them muddies the very distinction the course is built on. |
| 8 | CloudFormation auto-rollback on failed stack update (Strategy 4) | `rollback-on-aws.md` 86-88 | **Correct** | Accurate and correctly scoped to "failed update," not "deployed-but-behaves-badly." Keep. |
| 9 | Feature-flag kill switch: env-var flag flip = "config change + redeploy" | `rollback-on-aws.md` 90-94; `feature-flag.ts` 32-37 | **Correct** | Honest that the env-var pattern still needs a redeploy to flip (only a managed service gives true runtime toggle). This is the right caveat and many courses get it wrong. Keep. |
| 10 | ECS equivalents: CodeDeploy blue/green + prior immutable task-def revision | `rollback-on-aws.md` 96-98 | **Correct** | Accurate analogues. Keep. |
| 11 | Coverage threshold enforced in the runner, GitLab `coverage:` regex is only the badge | `ci-pipeline.gitlab-ci.yml` 68, 78-80 | **Correct** | Frequently-botched detail done right. Keep. |
| 12 | Digest-pinning CI images because mutable tags get re-pushed | `ci-pipeline.gitlab-ci.yml` 28-30; `.gitlab-ci.yml` 44 | **Correct** | Sound supply-chain reasoning and matches baseline. The `public.ecr.aws/sam/build-nodejs22.x:latest` is left unpinned with a `← pin before merging` note, which is honest for a teaching file. Keep. |
| 13 | `npm audit --audit-level=high` as a stop-the-line gate | `ci-pipeline.gitlab-ci.yml` 87-90; `.gitlab-ci.yml` 88-90 | **Oversimplified** | Technically valid; operationally it turns `main` red on overnight advisory churn with zero code change — in tension with stop-the-line. Note the common mitigations (scheduled audit job, or `allow_failure` with triage SLA). |
| 14 | Immutable-artifact key described as `CI_COMMIT_SHA` while SAM keys the zip by content hash | session-3 README 71-77; `violations-api/README.md` 33-46 | **Oversimplified** | The CI artifact and pipeline are keyed by SHA; the Lambda zip in S3 is keyed by SAM's own content hash. Related, not identical. Sharpen the diagrams so "which bytes" is traceable. |
| 15 | `CAPABILITY_IAM` sufficient for this template | `.gitlab-ci.yml` 132 | **Doubtful — verify** | Named resources / SAM transform commonly need `CAPABILITY_NAMED_IAM` and/or `CAPABILITY_AUTO_EXPAND`. Verify against a real `sam deploy` of `template.yaml`; fix the capability flags if it fails. |
| 16 | Role ARN, stack naming `${CI_ENVIRONMENT_NAME}-${SERVICE_NAME}`, `configuration/{env}.config`, SSM path `/${env}/stacks/${stack}/...`, stages validate→build→dev→qa→prod | throughout | **Correct (matches baseline)** | Cross-checked against the iac-scaffolder/reviewer conventions: config paths, SSM publish path, `IsPersonalStack`/Retain pattern, role-ARN shape, and stage order all align. Only the *baseline repo URL* in the README looks stale (see framing note). |
| 17 | violations-api handler + tests (pure logic, injected IO, flag-gated, lazy SDK import) | `src/handler.ts`, `src/handler.test.ts` | **Correct / good** | The lazy `await import` of the SDK so unit tests never touch AWS is a genuinely nice touch — keeps the CI gate fast, exactly as claimed. Tests cover dark-503-vs-201, escalation levels, notify-flag gating, and validation. Sound. One nit: handler returns `501 Not Implemented` for the dark path, but the route *is* implemented — `503` or a feature-specific code is arguably more honest; minor. |

## Recovery mechanics (persona-specific)

This is my home turf, so I went deep. Net: the *philosophy* is right and the *mechanisms* are real, but two of them are mis-implemented in a way that an SRE would catch in review and a junior would ship.

- **Fail-forward-first is the correct default** and the justification (small batch + tests + canary = same safety as any deploy) is sound. The course earns this claim *only if the canary actually guards the deploy* — and audit item #3 shows the supplied canary alarm doesn't (wrong dimension + hair-trigger threshold). Fix the alarm or the central thesis rests on a net with a hole in it.
- **The data trap is the best-taught recovery concept here.** Correct, well-placed, and tied to expand/contract. Extend it to data-*in-flight* (SNS/SQS messages with the new schema mid-stream during a producer rollback) — the topology in this very example creates that hazard.
- **Alias-shift drift (#4) is the scariest under-stated risk.** An out-of-band `update-alias` plus any later `sam deploy` silently reintroduces the bug. The doc mentions reconciliation; it should *lead* with the rule.
- **Rehearsal is correctly gated as the bar for minimum #8** (qa drill, time it, write the 2am runbook). This is exactly right and is the line I'd quote to my own team. The course made me admit, in the assessment, that we haven't met it.
- **Missing: deployment markers + SLO-based alerting.** None of the recovery flows can be triggered or trusted without them. "Rehearse rollback" and "watch the canary alarm" both assume an observability layer the course never teaches. For an SRE that's the gap between a tidy diagram and a real on-call posture.

## Recommendations

### High priority (I'd block the MR on these)
1. **Fix the OIDC `before_script` (audit #1).** Either show the credential export, or switch to the `AWS_WEB_IDENTITY_TOKEN_FILE` + `AWS_ROLE_ARN` native pattern. As written the deploy jobs cannot authenticate. This is the most copy-pasted snippet in the course.
2. **Fix the canary alarm (audit #3).** Scope the dimension to the alias/version and raise the threshold off a single-error hair-trigger. The entire fail-forward safety argument depends on this alarm being correct.
3. **Resolve the cross-account artifact-bucket gap (audit #5).** Build-once-promote on multi-account AWS *requires* a shared artifact bucket with cross-account read, or the "same bytes" claim is false in prod. State it.
4. **Ship the smoke test (objection #3).** "Verified, not hoped" needs ~10 real lines, or drop the claim.

### Medium priority
5. **Lead the alias-shift section with the CloudFormation drift rule (audit #4).** Out-of-band alias change → next action is redeploy-the-good-artifact, before anything else touches the stack.
6. **Re-file `git revert` under fail-forward, not rollback (audit #7).** It rebuilds; it's not a rollback. Conflating them muddies the course's own central distinction.
7. **Name the merge-blocking mechanism (objection #2).** "Pipeline decides releasability" needs the GitLab "Pipelines must succeed" setting called out, or a reader can merge red.
8. **Add data-in-flight to the data trap (recovery section).** SNS/SQS messages mid-stream during a producer rollback.
9. **Verify `sam deploy` capabilities (audit #15).** Likely needs `CAPABILITY_NAMED_IAM` / `CAPABILITY_AUTO_EXPAND`.
10. **Fix the stale baseline repo URL (framing).** Point it at the real `dx-iac-baseline`.

### Nice to have
11. Add an **observability row** to the current-state assessment (deployment markers, SLOs, alerting on the four DORA signals) — it's the one minimum-adjacent capability the scorecard omits and an SRE will miss immediately.
12. Sharpen the artifact diagrams to distinguish `CI_COMMIT_SHA` (pipeline key) from SAM's content-hash (zip key) — audit #14.
13. Acknowledge `npm audit` advisory churn vs stop-the-line (audit #13).
14. Add a one-line callout in decompose-a-branch that flipping a flag can be a breaking change for *consumers* even when backward-compatible for the producer.
15. Note that the dark path returning `501` is debatable; `503` reads more honestly.

## Verdict

**Champion** — with a four-item fix list I'd block on. This is the first internal CD course I've read where the recovery model is correct and the AWS mechanics are mostly real rather than aspirational hand-waving. The defects are concentrated and fixable (one broken OIDC snippet, one mis-wired alarm, one cross-account gap, one missing smoke test), not systemic. Fix the canary alarm and the OIDC export and I'd put this in front of every team adopting the baseline. Leave them and an SRE will lose trust the first time they copy-paste the pipeline and it neither authenticates nor protects them.
