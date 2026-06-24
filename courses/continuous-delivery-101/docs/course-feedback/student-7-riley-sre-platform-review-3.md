# Continuous Delivery 101 — Review 3

**Student:** Riley (SRE / Platform Engineer, 9 yrs)
**Stance going in:** Round 2 I rated this 9.5 and called it Champion-with-one-block — the OIDC export and canary alarm were fixed correctly, but `scripts/smoke-test.sh` was referenced-but-absent. The cycle then "converged" at round 6. This round it re-opened because the course grew a technically dense new limb: a DbUp database-delivery worked example, a dedicated `db-migrations.gitlab-ci.yml`, a `database-delivery.md` resource, and a rollback-correction sweep. That's all my lane. I read the YAML, the C#, the SQL, and the `needs:` graph — I do not take "build-once/promote" on faith.
**Review date:** 2026-06-23
**Overall rating:** 9.0/10 (was 9.5) — the docking is for **one real CI defect that breaks the headline claim** of the new pipeline (qa and prod never receive the built artifact and would fail at runtime), plus **stale alias-shift language in three places** that directly contradicts the new canonical rollback framing the rest of the sweep got right. The DbUp content itself is accurate, the rollback-on-aws.md rewrite is correct, and my last-round block item (the smoke test) is genuinely resolved. None of this is thesis-breaking — but a platform engineer copying this pipeline would hit the qa failure on day one, and that's exactly the kind of "looks right, isn't wired right" error the build-once/promote section is supposed to model perfectly.

## Executive summary

The new database-delivery material is, on the technical merits, **good and mostly accurate** — DbUp's `SchemaVersions` journal / run-once / forward-only / idempotent-re-run claims are all correct, `MarkAsExecuted` for baselining is the right tool, expand/contract is textbook, and the SQL scripts are valid idempotent T-SQL. The rollback sweep landed the canonical framing ("re-run the last known-good deployment in GitLab; never hand-edit the alias") consistently across `rollback-on-aws.md`, `minimums-reference.md`, `migration-checklist.md`, and the glossary. And the smoke test I'd have blocked the MR on now exists and is well-built.

But there are two findings a platform engineer catches on first read:

1. **`db-migrations.gitlab-ci.yml` won't promote the artifact it claims to.** The README diagram says "the SAME `publish/` artifact" reaches `migrate:dev`, `migrate:qa`, `migrate:prod`. In the YAML only `migrate:dev` lists `build:migrator` in `needs:`. `migrate:qa` needs `[migrate:dev]` and `migrate:prod` needs `[migrate:qa]` — neither pulls `build:migrator`'s artifact, and neither (rightly) rebuilds. With `needs:` present, a job receives artifacts **only** from the jobs in its own `needs:` list. So qa and prod land with no `publish/` directory and die at `dotnet publish/Migrator.dll` ("Could not find …/Migrator.dll"). This is the precise build-once/promote failure mode the example is meant to teach correctly.

2. **Three stale "alias shift = rollback" statements survived the sweep** and contradict the new canonical framing: `template.yaml:86`, `current-state-pipeline-walkthrough.md:50`, and the reading-guide comment at `.gitlab-ci.yml:226`.

Everything else I re-checked held. Down 0.5 from 9.5; both findings are mechanical and one-line-ish to fix.

## Finding 1 (HIGH) — `db-migrations.gitlab-ci.yml`: qa/prod never receive the built artifact

**File:** `sessions/session-3/examples/db-migrations/db-migrations.gitlab-ci.yml:77-97`, against the claim in `sessions/session-3/examples/db-migrations/README.md:22-35`.

The artifact graph as written:

```
build:migrator         artifacts: paths: ["publish/"]     (produces the DLL)
migrate:dev    needs: [validate:migrations, build:migrator]   ✅ gets publish/
migrate:qa     needs: [migrate:dev]                            ❌ no publish/
migrate:prod   needs: [migrate:qa]                             ❌ no publish/
```

The `.migrate` script (line 75) runs `dotnet publish/Migrator.dll "$DB_CONNECTION_STRING"` — it does **not** rebuild (correct intent: promote, don't rebuild). But it depends on the `publish/` directory existing in the job workspace.

**The GitLab fact that breaks it:** once a job declares `needs:`, it downloads artifacts **only** from the jobs named in that `needs:` list (each `needs` entry defaults to `artifacts: true`). It does *not* inherit artifacts from all earlier stages the way a `needs`-less job does. `migrate:qa` names only `migrate:dev`; `migrate:dev` produces no artifact; therefore qa's workspace has no `publish/Migrator.dll`. Same for prod via qa. Both jobs fail at runtime, not at lint.

This is not a stylistic nit — it's the exact "the same bytes reach every stage" guarantee the README diagram draws, and as wired the bytes reach only dev.

**The correct fix (either):**

- Add `build:migrator` to every promote job's `needs:`, e.g.
  `migrate:qa: needs: [migrate:dev, build:migrator]` and
  `migrate:prod: needs: [migrate:qa, build:migrator]`.
  The `needs:` order does **not** force re-execution of `build:migrator` (it already ran once and is reused) — it just makes its artifact available. This keeps the strict dev→qa→prod *ordering* (via the prior migrate job) AND delivers the one built artifact to each stage. This is the idiomatic GitLab pattern for "build once, fan the artifact out to a promotion chain."
- Or, less cleanly, give each promote job an explicit `dependencies: [build:migrator]`. (`dependencies:` controls artifact download; but with `needs:` present, `dependencies:` must be a subset of `needs:`, so you'd still have to add `build:migrator` to `needs:` — making option 1 the real fix.)

Contrast with the app pipeline, which gets this right: in `sessions/session-3/examples/.gitlab-ci.yml` the deploy jobs all consume `${PACKAGED_TEMPLATE}` from the single `build:artifact` job and the reading guide (line 217) explicitly says "deploy:* all consume ${PACKAGED_TEMPLATE}." The db-migrations pipeline copied the *shape* (stages, OIDC, manual prod gate) but dropped the artifact wiring that makes promotion real. **Recommend blocking on this** — a learner who lifts this YAML ships a pipeline that green-lights dev and then red-fails qa.

**Everything else in this file is sound:**

- **OIDC** (`.aws_oidc`, lines 27-35) is the same correct native-web-identity pattern I verified in review 2 — `AWS_WEB_IDENTITY_TOKEN_FILE` + `AWS_ROLE_ARN` + the `gitlab-runner-role-${CI_ENVIRONMENT_NAME}` ARN pattern matching iac-baseline, `sts get-caller-identity` fail-fast, no static creds. Correct.
- **Service container** for `validate:migrations` (lines 41-46): an ephemeral `mssql/server:2022-latest` with `alias: sqlserver`, `ACCEPT_EULA`, a throwaway SA password explicitly commented "not a real secret." This is the right way to prove migrations apply against a real engine before any shared env — a genuine production-like test (#7) for schema. The connection string targets `Server=sqlserver` (the service alias) — correct GitLab services networking.
- **Connection string from Secrets Manager** at deploy time (lines 71-74), keyed by `${CI_ENVIRONMENT_NAME}` — config travels per-env, never baked in (#9). Correct.
- **Manual prod gate** (line 96) with `allow_failure: false` and the comment "a human approves TIMING, not readiness" — consistent with the course's gate philosophy. Correct.
- **`build:migrator` is `main`-only** (rule, line 56-57) and built once. Correct intent.

**One more platform-engineer caveat, not a defect:** `Program.cs:26` calls `EnsureDatabase.For.SqlDatabase(connectionString)` on every run, including prod. `EnsureDatabase` issues `CREATE DATABASE` if the DB is absent, which requires a server-level permission (`dbcreator`/`CREATE DATABASE`) that a least-privilege prod migrator login usually should *not* hold. Against a locked-down shared prod the DB already exists, so it's a harmless no-op *if* the login can see it — but if security scoped the login to the single database (the correct posture), `EnsureDatabase` can error on the permission check. The code comment ("convenient for a fresh local DB") hints at this, but a one-line "in shared envs the DB already exists and your migrator login should be scoped to it, not granted create-database" would stop someone over-privileging the prod login to satisfy this call. Worth a sentence; not blocking.

## Finding 2 (MEDIUM) — Stale "alias shift = rollback" language contradicts the new canonical framing

The sweep correctly established everywhere-else that the rollback **action** is "re-run the last known-good deployment in GitLab," that the canary alias-shift is an *automatic* mechanism (not a manual lever), and that you **never hand-edit the alias**. Three statements didn't get the memo and now contradict the canon:

1. **`sessions/session-3/examples/violations-api/template.yaml:86` — the worst one.**
   > `# alias pointing at it. Rollback = repoint the alias to the prior version.`

   This is *exactly* the hand-edit the course now repeatedly forbids ("never a hand-edited Lambda alias/version" — CLAUDE.md, glossary:189, rollback-on-aws.md:54). **Correct fact:** repointing the alias by hand is an out-of-band change that bypasses the pipeline (minimum #2) and drifts from IaC; the next unrelated `sam deploy` silently reverts it. The canary auto-rollback *does* repoint the alias, but CodeDeploy does it automatically inside the deploy window — a human never runs `update-alias`. Fix to: `# Rollback = re-run the last good deployment in GitLab (canary auto-repoints the alias on alarm).`

2. **`sessions/session-3/examples/current-state-pipeline-walkthrough.md:50`.**
   > "Session 3 covers failing forward as the default plus **Lambda alias shifting** and CloudFormation rollback as the emergency lever."

   **Correct fact:** the emergency lever in Session 3 is *re-running the last good deployment in GitLab*; the canary alias-shift and CloudFormation auto-rollback are *automatic safety nets*, not manual levers. As written it mislabels an automatic mechanism as the human emergency action and omits the canonical move entirely. Fix to: "…failing forward as the default, re-running the last good deployment as the emergency rollback, with canary and CloudFormation auto-rollback as automatic safety nets."

3. **`sessions/session-3/examples/.gitlab-ci.yml:226` (reading-guide comment).**
   > "roll back only when costly + time-sensitive — canary auto-rollback / **alias shift** (rollback-on-aws.md)"

   Same issue, lower stakes (it's a comment). "alias shift" reads as a discrete rollback option alongside canary, and the canonical "re-run the last good deployment" is absent. Fix to: "…re-run the last good GitLab deployment; canary + CloudFormation auto-rollback back it up."

Note `template.yaml:174` ("The live alias (rollback target)") is *fine* — the alias genuinely is what canary auto-rollback repoints to; that's a description of the target, not an instruction to hand-edit. Leave it.

## DbUp / database-delivery technical fact-check — accurate

I checked every load-bearing DbUp claim in `Program.cs`, `Migrator.csproj`, the three SQL scripts, `db-migrations/README.md`, and `resources/database-delivery.md`. They hold:

- **`SchemaVersions` journal** (database-delivery.md:19, glossary:160, README:9): correct — `SchemaVersions` is DbUp's default journal table, recording each applied script so it runs exactly once. Accurate.
- **Scripts run once, in order** (Program.cs:30-33): correct. `WithScriptsEmbeddedInAssembly` enumerates embedded `.sql` resources and DbUp applies un-journaled ones in ordinal name order — `Script0001` < `Script0002` < `Script0003` sort correctly, so the zero-padded ordering is right (and the right convention to teach; non-padded names bite people at script 10).
- **Forward-only / no down-migrations** (database-delivery.md:24, Script0002:12-14, README:37-39): correct. DbUp has no down-script concept; reversibility comes from expand/contract, not a rollback. Stated accurately everywhere.
- **Idempotent re-run safe** (Program.cs:30-33, README:63): correct. The journal skips applied scripts, so re-running the last good deployment is a no-op for already-applied scripts — which is what makes "re-run last good deployment" a valid schema rollback action. Accurate.
- **`MarkAsExecuted` for baselining** (database-delivery.md:40): correct. DbUp's `JournalTo`/`MarkAsExecuted` records a script as already-run *without executing it* — exactly right for scripting current prod as the baseline migration and marking it applied so the runner never tries to recreate existing objects. The "baseline prod, reconcile lower envs up, then lock the door" sequence is the correct drift-remediation order, and "prod is the truth" is the right call.
- **`EnsureDatabase`** (Program.cs:26): correct API and correctly described as a safe no-op when the DB exists. (See the prod-permission caveat in Finding 1.)
- **Non-zero exit fails the pipeline** (Program.cs:39-48): correct — `result.Successful == false` → `return 1`, and the usage error → `return 2`. A red migration stops the line. Accurate.
- **`dbup-sqlserver` / net8.0 / embedded Scripts** (Migrator.csproj): plausible and internally consistent. `dbup-sqlserver` 5.x is a real package line and pulls the SQL Server provider + `DbUp.Engine`; `<EmbeddedResource Include="Scripts\*.sql" />` is the correct way to embed the scripts so the published artifact is self-contained. The version pin (5.0.40) I can't verify offline to the exact patch, but it's in the right major range and the file flags itself as a teaching reference, not a restored build. No error.

### SQL scripts — valid, idempotent, correct expand/contract

- **Script0001** (idempotent create via `IF OBJECT_ID(...) IS NULL`): valid T-SQL, correctly idempotent. The belt-and-suspenders comment ("safe even though DbUp's journal already prevents re-application") is accurate and good practice.
- **Script0002** (expand): `ALTER TABLE … ADD DueDate DATE NULL` guarded by an `IF NOT EXISTS` against `sys.columns` — valid T-SQL, idempotent, and a *correct* expand step: nullable add is backward-compatible, so old code keeps inserting and the app can roll back independently of the schema. The inline note that contract is a *later, separate forward migration* is exactly right. Textbook.
- **Script0003** (idempotent seed via `MERGE`): valid T-SQL; the `MERGE … WHEN MATCHED UPDATE / WHEN NOT MATCHED INSERT` converges to the same rows whether the table is empty or seeded — correctly idempotent, so re-running never duplicates reference data. Accurate. (Pedant's footnote, *not* a finding: `MERGE` on SQL Server has had historical edge-case bugs and many shops prefer `IF NOT EXISTS`-style upserts; the course even offers `IF NOT EXISTS` as the alternative in database-delivery.md:54. For a tiny static lookup of three rows, `MERGE` is fine. No correction needed.)

### Cross-database coupling (database-delivery.md:44-50) — sound

Independent migration project + journal per database, "add no new cross-DB references," and crossing a shared reference via expand/contract in dependency order — all correct and the right pragmatic tactics for a coupled estate. Treating the DB boundary like a service seam is the correct mental model. No errors.

## Rollback-on-aws.md rewrite — correct

The rewrite (`sessions/session-3/examples/rollback-on-aws.md`) is technically accurate and the framing is now right:

- **Strategy 1 — re-run the last good deployment in GitLab** is correctly named as *our* rollback, with both forms (re-run `deploy:prod` from the last good pipeline = redeploy stored artifact, no rebuild; or `git revert` = build+promote the revert). The "why this, not a hand-edited alias" callout (line 54) is correct: through the pipeline → IaC and running state stay consistent → no out-of-band alias change for the next `sam deploy` to undo.
- **Strategy 2 — canary auto-rollback** (`Canary10Percent5Minutes` + alarm, CodeDeploy shifts 10% for 5 min, auto-rolls-back on alarm, no human): accurate, and correctly described as automatic. Consistent with the (correctly-wired, per review 2) alias-scoped rate alarm in template.yaml.
- **Strategy 3 — CloudFormation auto-rollback** correctly scoped to *failed stack updates*, explicitly distinguished from "deployed fine but behaves badly" (that's canary's job). Accurate — this is a real and commonly-confused boundary, and they drew it correctly.
- **Strategy 4 — feature-flag kill switch**: env-var flag = config change + redeploy; managed service (AppConfig/LaunchDarkly) = runtime toggle in seconds. Accurate.
- **Data trap** (line 39): rolling code back does not roll data back; once state moved, fail-forward is often the only safe option. Correct, and ties to expand/contract correctly.
- **ECS note** (line 79): blue/green CodeDeploy with alarm auto-rollback, or re-run to land on the prior immutable task-def revision. Accurate AWS mechanics.
- **Rehearsal gate** for minimum #8 (lines 92-100) retained, with both fail-forward and rollback timed. Correct.

This is the canonical framing the rest of the sweep aligned to — and three files (Finding 2) failed to align to it.

## Regression sweep on previously-validated technical content

- **Smoke test — my round-2 block item is RESOLVED.** `sessions/session-3/examples/violations-api/scripts/smoke-test.sh` now exists and is well-built: reads `ApiUrl` from the stack's own CloudFormation output (follows the artifact to whatever env deployed), probes with an empty body so it mutates nothing, asserts 400 (flag on, validation rejects empty) or 501 (deployed dark) as healthy and anything else (000/403/5xx) as fail-and-stop-promotion. This is exactly the ~10-line script I specified. "Verified, not hoped" is now backed by a real file. Good — and this alone would have moved my last-round score up if the new defects hadn't pulled it down.
- **OIDC native web-identity** (app `.gitlab-ci.yml` and db `.aws_oidc`): unchanged and still correct.
- **Canary alarm** (template.yaml:116-154): still alias-scoped (`Resource` dimension on `:live`), still exactly one `ReturnData: true` (`errorRate`), still rate-based (`100 * errors / invocations`, threshold 5, two periods), and the zero-traffic divide-by-zero → missing → `notBreaching` comment I asked for in review 2 is now present (line 154). My one round-2 medium nit is closed. No regression.
- **Build-once/promote in the app pipeline**: still correct (`deploy:*` consume `${PACKAGED_TEMPLATE}` from the single `build:artifact`). The *app* pipeline does artifact-passing right — which makes the *db* pipeline's Finding-1 miss all the more catchable by diffing the two.

## Recommendations

### High priority (I'd block on this)
1. **Fix the `needs:`/artifact flow in `db-migrations.gitlab-ci.yml`** (Finding 1). Add `build:migrator` to the `needs:` of both `migrate:qa` and `migrate:prod` so the one built artifact actually reaches every stage. As written, qa fails at runtime and the "same bytes everywhere" claim in the README diagram is false. This is the one item that makes the example not work as shipped.

### Medium priority
2. **Kill the three stale alias-shift statements** (Finding 2): `template.yaml:86` ("Rollback = repoint the alias to the prior version" — the most direct contradiction), `current-state-pipeline-walkthrough.md:50`, and the reading-guide comment `.gitlab-ci.yml:226`. Replace with the canonical "re-run the last good deployment; canary auto-repoints the alias on alarm."

### Nice to have
3. **One line on `EnsureDatabase` in shared environments** (Program.cs:26 or README): note the prod migrator login should be scoped to the existing database, not granted create-database rights, so nobody over-privileges prod to satisfy the `EnsureDatabase` call.

## Verdict

**Champion, with one CI defect to fix before it ships.** The DbUp content is technically accurate end-to-end, the rollback-on-aws.md rewrite is correct, the canonical "re-run the last good deployment, never hand-edit the alias" framing is right and consistent in the resources/glossary, and the smoke test I'd have blocked on last round is now real and well-made. But `db-migrations.gitlab-ci.yml` as written would green-light dev and then red-fail qa — qa and prod never receive the built artifact because they don't `needs:` the build job — which is precisely the build-once/promote guarantee the example exists to teach. Add `build:migrator` to the promote jobs' `needs:`, scrub the three stale alias-shift lines, and this is back to 9.5+. As it stands: **9.0/10** (−0.5 from 9.5).
