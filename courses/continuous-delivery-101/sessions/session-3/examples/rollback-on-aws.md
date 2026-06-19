# Worked Example: Recovery on AWS — Fail Forward First, Roll Back When It's Costly

When a change misbehaves in production you have two moves: **fail forward** (ship a small fix through the pipeline) or **roll back** (return to the last good version). CD makes the first one the default — and keeps the second ready for emergencies.

> **Default — fail forward.** The pipeline already makes shipping small, fast, and safe. The normal response to a problem is to diagnose it, write a small fix, and deploy *forward* down the same canary-verified path every change takes.
>
> **Exception — roll back.** When the problem is **costly *and* time-sensitive** — active customer/board impact, data at risk, revenue or compliance on the line — and a forward fix won't land fast enough, roll back *now*, then fail forward once the pressure is off.

CD minimum #8 still requires you to *have* fast, rehearsed rollback. The goal isn't to avoid rollback — it's to reach for it deliberately, as the emergency lever, instead of as the reflex for every hiccup.

> **The golden prerequisite (both directions):** because artifacts are **immutable** (CD minimum #5), "the last good version" is a real, stored, redeployable thing — and a forward fix is just the next immutable artifact. Neither move depends on remembering what `main` used to look like.

## Fail forward — the default

What it looks like for the Violations API:

1. An alarm (or a report) shows the new version is misbehaving.
2. You reproduce it with a fast unit test — which now also guards against the regression returning.
3. You ship a small fix MR. The pipeline lints, tests, scans, builds one artifact, and promotes it dev → qa → prod, where the **canary** verifies it before full rollout.
4. Minutes later prod is healthy, and the defect is actually *fixed*.

Why fail-forward is usually the right first move:

- **It fixes the problem.** A rollback only postpones it — you still have to fix and re-ship later. Forward is the direction you were going anyway.
- **It's safe by the same logic as any deploy** — small batch, tests, canary are exactly what made the original change low-risk.
- **It sidesteps the rollback data trap (below).**

The fastest mitigation of all is often neither a rollback nor a redeploy: if the bad behavior is behind a **feature flag, flip it off** (Strategy 5) — no deploy — then fix forward behind the flag at a calm pace.

## When to roll back instead

Roll back when **both** are true:

- **Costly** — users, the board, data integrity, revenue, or a compliance obligation is being harmed *right now*; and
- **Time-sensitive** — even a small forward fix won't land fast enough to stop the harm.

Then: roll back immediately to stop the bleeding, and fail forward afterward to fix the underlying defect.

> **The rollback data trap:** rolling *code* back does **not** roll *data* back. If the bad version already wrote a new DynamoDB shape, enqueued an SQS message, or published an SNS event, reverting the code can leave you reading data the old code doesn't understand. This is why changes must stay backward-compatible (expand/contract) — it keeps *both* directions safe. Once state has already moved, **failing forward is often the only safe option**, even under pressure.

## Rollback mechanisms

When you do roll back, these are the AWS mechanisms, fastest first.

## Strategy 1 — Lambda alias shift (fastest; seconds)

Our `template.yaml` sets `AutoPublishAlias: live`. Every deploy publishes an immutable Lambda **version** (1, 2, 3, …) and points the `live` alias at the newest. API Gateway invokes the alias, not a raw version.

Rollback = repoint the alias to the previous version:

```bash
# Find the version the alias currently points at, and the prior one.
aws lambda get-alias --function-name prod-aws-violations-api --name live

# Repoint live to the known-good prior version (e.g. 7 instead of 8).
aws lambda update-alias \
  --function-name prod-aws-violations-api \
  --name live --function-version 7
```

- **Speed:** near-instant. No build, no CloudFormation.
- **Use when:** a freshly deployed version is misbehaving and the prior version was healthy.
- **Note:** this is an emergency action; reconcile the stack afterward (redeploy the good artifact through the pipeline) so IaC and reality agree.

## Strategy 2 — Canary with automatic rollback (no human needed)

`template.yaml` sets `DeploymentPreference: Canary10Percent5Minutes` with a CloudWatch error **alarm**. On deploy, CodeDeploy shifts 10% of traffic to the new version for 5 minutes while watching the alarm:

- Alarm stays OK → the remaining 90% shifts over. Done.
- Alarm fires → CodeDeploy **automatically rolls back** to the previous version. No human action.

- **Speed:** automatic during the deploy window; most bad deploys never fully roll out.
- **Use when:** always, as the default safety net for prod Lambda deploys.
- **Tradeoff:** deploys take a few minutes longer (the canary window). Worth it.

## Strategy 3 — Redeploy the previous immutable artifact (the clean path)

Because every artifact is packaged and kept (tagged by `CI_COMMIT_SHA`, retained 30 days), you can re-run the deploy job against the *previous* packaged template:

- Re-run `deploy:prod` from the last-good pipeline, **or**
- `git revert` the bad commit on `main` and let the pipeline build + promote the revert.

- **Speed:** minutes (a deploy, not a rebuild if you target the stored artifact).
- **Use when:** you want IaC and running state to stay consistent (preferred over a raw alias shift once the fire is out).

## Strategy 4 — CloudFormation automatic rollback (built in)

If a `sam deploy` / stack update *fails partway*, CloudFormation rolls the stack back to its last good state by default. You get this for free — it covers failed updates, not "deployed fine but behaves badly" (that's what canary + alarms catch).

## Strategy 5 — Feature-flag kill switch (rollback with no deploy)

If the bad behavior is behind a flag, the fastest "rollback" is to **turn the flag off** — no deploy at all. With env-var flags, that's a config change + redeploy; with a managed flag service (AppConfig/LaunchDarkly), it's a runtime toggle that takes effect in seconds.

- **Use when:** the regression is a new, flagged feature. This is a major reason to ship behind flags.

## ECS note

For ECS services the equivalents are: a **CodeDeploy blue/green** deployment (analogous to the Lambda canary, with alarm-based auto-rollback), or redeploying the previous **immutable task definition revision** (analogous to the alias shift — task defs are versioned and stored).

## Choosing your move

| Situation | Reach for |
| --------- | --------- |
| A problem you can fix with a small change | **Fail forward** — ship the fix through the pipeline *(default)* |
| Regression is behind a feature flag | Flip the flag off (Strategy 5), then fail forward |
| Costly **and** time-sensitive, forward fix too slow | **Roll back now**, then fail forward |
| → newest deploy is the culprit, need it gone in seconds | Alias shift (Strategy 1) |
| → want gradual, self-protecting prod deploys | Canary auto-rollback (Strategy 2) — on by default |
| → keep IaC and running state consistent | Redeploy prior artifact (Strategy 3) |
| A stack update failed partway through | CloudFormation auto-rollback (Strategy 4) — automatic |

## Make it boring — rehearse both

A recovery move you've never run is a hope, not a capability. In your migration:

1. Deploy a deliberately broken version to **qa**.
2. Practice the **fail-forward loop** (fix MR → pipeline → canary) and **time it** — this is your default, so it should be muscle memory.
3. Practice each relevant **rollback** strategy and **time it** too.
4. Write the steps where the on-call engineer will actually find them at 2am — including the rule for *when* to roll back versus fail forward.
5. Only then count minimum #8 as met.

## Related

- Immutable artifacts & promotion: [`violations-api/README.md`](./violations-api/README.md)
- The pipeline: [`.gitlab-ci.yml`](./.gitlab-ci.yml)
- [Migration Checklist](../../../resources/migration-checklist.md) — rollback appears in Phase 2
