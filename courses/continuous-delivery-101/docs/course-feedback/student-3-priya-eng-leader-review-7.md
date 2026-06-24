# Continuous Delivery 101 — Review 7

**Student:** Priya (Engineering Director, 15 yrs)
**Stance going in:** I closed the cycle at 10.0 in round 6 — the precision had finally caught up to
the prose, and I said so without hedging. Round 7 fires because the course changed *materially*
after I left: a new database-delivery resource that asserts **no standing human DDL access to
shared envs**, a new DbUp worked example with a manual prod gate, a `DDL` glossary entry, Phase-2
DB checklist items, and a **rollback-correction sweep** that reframes "rollback" as *re-run the
last good GitLab deployment* in place of the old hand-edited-alias language. My job is the
governance lens: is removing standing DDL a *defensible control with an audit trail* or an
unaccountable mandate; does the schema story stay on the CD (human-gated) side and preserve the
segregation-of-duties / pipeline-as-audit-trail spine I validated in rounds 2–3; and did the
rollback sweep land *completely* without regressing the recovery story. I read in character and
verified on disk at HEAD `d591172` with Read, Grep, and `git log -S`.
**Review date:** 2026-06-23
**Round-1:** 7/10 · **Round-2:** 9/10 · **Round-3:** 9.5/10 · **Round-4:** 9/10 · **Round-5:** 9.5/10 · **Round-6:** 10/10
**Round-7 rating:** 9.5/10 — **down 0.5 from 10.0.** The new database governance is genuinely
strong — accountable, audit-traceable, correctly human-gated. But the rollback-correction sweep
that runs alongside it **missed two files**, leaving the *superseded* "alias shift / Lambda alias
shifting as the emergency lever" framing alive in the exact two places a student meets the recovery
story concretely. It is my recurring theme — the precision trailing the prose — resurfacing in the
new work, and it is the kind of record/artifact disagreement I have docked for six rounds. One
targeted sweep restores the 10.

## Verdict

The database-delivery story is reference quality on governance: removing standing DDL is framed as
a *control* (named accountable owner, preserved audit trail, correctly human-gated at prod), not a
mandate, and the SoD / pipeline-as-audit-log spine is undisturbed and in fact extended to schema.
But the parallel rollback-correction sweep is **incomplete** — `current-state-pipeline-walkthrough.md:50`
and `examples/.gitlab-ci.yml:226` still name "Lambda alias shifting" / "alias shift" as the
rollback *emergency lever*, the precise framing the sweep replaced everywhere else with "re-run the
last good GitLab deployment." A student doing the workshop hits the walkthrough as the *first*
concrete recovery statement they read, and it now contradicts the canonical doc one click away.
That is a governance-adjacent record/artifact mismatch (minimum #8, recovery), and it docks the
half-point. Everything else holds.

---

## NEW: Database delivery — removing standing DDL as a *control*, not a mandate: **PASS**

This was the round's central governance test. An auditor's first reaction to "no engineer can
change the schema by hand on prod" is *"good — now show me who's accountable for that decision and
where the change-control evidence lives."* Hand-waving fails that. This does not.

- **It names who is accountable.** `database-delivery.md:32` states it plainly: *"**DX owns and is
  driving the path** … standing up the runner-in-pipeline, the local-database workflow, and the
  eventual removal of direct access."* `session-3/README.md:126` repeats *"**DX owns that path**."*
  This is the right call and it matches CLAUDE.md's instruction that the destination is stated as
  **direction owned by DX, not a team rollout checklist** — and crucially, `current-state-assessment.md:50`
  tells the team *"that is a deliberate target DX is driving toward automation — note it, **don't
  score it as a personal gap**."* So accountability is assigned to a named owner (DX), and teams are
  explicitly told *not* to self-flagellate over a state they don't own. An auditor gets a named
  accountable function, not a diffuse "the engineers should stop."
- **It preserves the change-control evidence trail for schema.** The migration *is* the audit
  record: `database-delivery.md:19` (the `SchemaVersions` journal records every applied script),
  and the Phase-2 checklist makes the mapping explicit — `migration-checklist.md:60` *"Map
  change-control evidence to pipeline artifacts (MR review = segregation of duties; pipeline run +
  SHA-tagged immutable artifact = audit trail)"* sits directly above `:66` *"Database schema and
  baseline data change **only** through the pipeline … never by hand on a shared environment."* A
  schema change now produces the same four audit answers (who/what/when/verification) as app code,
  by-product, because it flows through the same MR + pipeline + immutable-artifact path. That is the
  governance-and-compliance audit table (`governance-and-compliance.md:63-69`) applied to DDL with
  no special-casing.
- **The `DDL` glossary entry defines the control precisely** (`glossary.md:138-139`): *"'No
  standing human DDL access' means engineers cannot hand-alter schema on a shared database;
  structural change happens only through migrations the pipeline runs."* That is the control stated
  as a control — *what* is removed (standing structural write), *how* the legitimate path works
  (pipeline-run migrations). An auditor can read that sentence and map it to a least-privilege
  requirement directly. It also reconciles with the controls scorecard line I rely on —
  `current-state-assessment.md:60` "no standing human prod access" — now extended cleanly to schema.
- **It is honest about the gap rather than declaring victory.** `database-delivery.md:32` names the
  prerequisites *"lower environments have drifted from production, there is no schema automation
  today, and local databases are still uncommon … real prerequisites, not afterthoughts."* That is
  exactly the posture that survives audit scrutiny: the control is the *destination*, the owner is
  named, the current state is disclosed, and the team's part is bounded ("adopt local databases and
  route schema changes through the pipeline as that path lands"). No mandate dressed as fait
  accompli.

**Disposition: this is a defensible control with an audit trail and a named accountable owner. It
would survive an auditor's "who decided this and where's the evidence" — it does not read as an
unaccountable edict.** Strong, additive governance work.

## NEW: DbUp worked example — stays on the CD (human-gated) side: **PASS**

The pipeline (`db-migrations.gitlab-ci.yml`) is correctly on the *Continuous Delivery* side, not
Continuous Deployment, and the gate placement is exactly where I want it:

- **Prod is human-gated; lower envs are not.** `:96` `migrate:prod` carries `when: manual` with the
  comment *"ONLY gate: a human approves TIMING, not readiness the pipeline proved,"* while
  `migrate:dev` (`:82`) and `migrate:qa` (`:89`) auto-promote on green. This is the *legitimate
  permanent control* vs *debt gate* distinction from `governance-and-compliance.md:22-36` applied
  correctly: the human at prod is authorizing **timing** of a schema change against a shared
  production database — a textbook risk-acceptance/timing control to keep — not re-litigating
  readiness. The README (`db-migrations/README.md:67`) states the same: *"Only `prod` keeps a
  manual gate — a human approving *timing*, not *readiness* the pipeline already proved."*
- **SoD and no-standing-credentials carry to schema.** `:27-35` `.aws_oidc` assumes a per-env role
  via OIDC; `:72-74` pulls the connection string from Secrets Manager *at deploy time* — *"never
  baked into the artifact, never typed by a human against a shared database"* (`:11`). So the
  migrator deploys under a scoped credential-less role, not a person — the same
  OIDC-deploys-not-a-human claim from `governance-and-compliance.md:50` and `:68`, now true for DDL.
- **Build-once / immutable / promote** is intact: `build:migrator` (`:54`) builds once on
  `main`; `.migrate` (`:66`) promotes the *same* `publish/` artifact to every env, varying only the
  connection string. The immutable-artifact-as-audit-evidence claim (`governance-and-compliance.md:69`)
  holds for the migrator.

This is Continuous **Delivery**, with the human release/timing decision preserved at prod. It does
**not** drift toward Continuous Deployment. The CD ≠ Continuous Deployment distinction I have
guarded since round 1 is respected in the new example.

## REGRESSION: the rollback-correction sweep missed two files — **DOCKED (R7-1)**

The sweep (commit `c42a007 docs: rollback by re-running the last good GitLab deployment`) replaced
the old hand-edited-alias rollback framing with the canonical *"re-run the last known-good
deployment in GitLab"* across the documents I'd expect: `rollback-on-aws.md:43-54`,
`minimums-reference.md:98`, `glossary.md:61` (and `:189` correctly retains the benign *automatic
canary* alias description while disclaiming hand-edits), `troubleshooting.md:88`,
`migration-checklist.md:63`, and `violations-api/README.md:61`. Where it landed, it landed
cleanly — and notably it also closed the pre-existing `migration-checklist.md` "shift Lambda alias"
line the implementation plan itself flagged (`docs/.../2026-06-23-database-delivery.md:810`). Good.

But it **missed two files**, and `git log -S` confirms they were never touched by the sweep
(`current-state-pipeline-walkthrough.md` was last edited at `c9aa30f`, the round-4 commit, which
*predates* `c42a007`):

1. **`sessions/session-3/examples/current-state-pipeline-walkthrough.md:50`** —
   > "Session 3 covers failing forward as the default plus **Lambda alias shifting** and
   > CloudFormation rollback as the emergency lever."

   This names *Lambda alias shifting* as the rollback emergency lever — the exact superseded
   framing. The canonical action is now *re-run the last good GitLab deployment*; the alias is
   something the **automatic canary** shifts, and the docs now explicitly say routine/manual
   rollback is **not** a hand-edited alias (`rollback-on-aws.md:54`, `glossary.md:189`). This line
   contradicts that.

2. **`sessions/session-3/examples/.gitlab-ci.yml:226`** — the pipeline's own summary comment:
   > "Recovery … roll back only when costly + time-sensitive — canary auto-rollback / **alias
   > shift** (rollback-on-aws.md)"

   Lists "alias shift" as a rollback mechanism, alongside a pointer to `rollback-on-aws.md` — which,
   when followed, gives the *opposite* answer (Strategy 1: re-run the last good GitLab deployment;
   "Why this, **not** a hand-edited Lambda alias"). The summary contradicts the file it cites.

**Why this docks a half-point, and why it's in my lane:**

- It is the **same record/artifact-disagreement defect** I have docked for six rounds — the prose
  was corrected in the canonical doc but the precision didn't propagate to every surface. A reader
  copying the mental model from the walkthrough gets a recovery move the course elsewhere
  *explicitly forbids* (a hand-edited alias bypasses minimum #2 and drifts from IaC —
  `rollback-on-aws.md:54`, CLAUDE.md:117).
- It is **governance-adjacent**, not cosmetic. Recovery is CD minimum #8, and the *mechanism* of
  rollback is a control question: "re-run the pipeline deployment" *preserves* the audit trail and
  IaC consistency; "shift the alias by hand" is precisely the out-of-band change that breaks both.
  Naming the wrong one as the lever undercuts the pipeline-as-single-trusted-path story I validated.
- **Placement makes it worse.** `current-state-pipeline-walkthrough.md` is the workshop's baseline
  artifact — `:50` is in the "What this tells us / the gaps" section a facilitator pulls up *first*.
  It is one of the earliest concrete recovery statements a student reads, so the stale framing lands
  before the correct one in `rollback-on-aws.md`.

**The fix is one targeted edit per file** (mirror the language already in `glossary.md:189` /
`rollback-on-aws.md`): replace "Lambda alias shifting … as the emergency lever" with "re-running the
last good GitLab deployment, with canary and CloudFormation auto-rollback as automatic safety nets,"
and change the `.gitlab-ci.yml:226` comment's "alias shift" to "re-run the last good deployment." No
content beyond the sweep's own already-decided wording. This is a propagation miss, not a design
disagreement — closing it restores the 10.

## Regression sweep against my 10.0 — otherwise clean

I re-checked the governance spine I won in rounds 2–3 for any disturbance from the new DB content
or the rollback sweep. Apart from R7-1, no regression:

- **Manual-gate-as-control test** — intact and now *extended* to schema correctly: the new
  `migrate:prod when: manual` is a timing/authorization gate, consistent with
  `governance-and-compliance.md:22-36`, `troubleshooting.md:32`, `glossary.md` *manual gate*. No
  drift.
- **SoD / pipeline-as-audit-trail** — `governance-and-compliance.md:38-74` untouched; the Phase-2
  checklist (`migration-checklist.md:60`) and the DBmigrations example reinforce it for DDL. The
  audit-question→artifact table is undisturbed.
- **Break-glass with accountable owner (R3-1)** — still named: "owned by the on-call lead / service
  owner and recorded in the incident record" (`governance-and-compliance.md:93-95`, `:142-143`).
  Undisturbed.
- **CD ≠ Continuous Deployment** — preserved, and the new DbUp example is correctly Delivery, not
  Deployment (prod human-gated). No blur.
- **Break-glass / least-privilege / deploy-by-role-not-person** — the DB example uses OIDC + a
  per-env role + Secrets-Manager-at-deploy-time, consistent with the no-standing-credentials claim.
- **The benign canary-alias description is *correctly* retained** where it belongs:
  `glossary.md:189` and `violations-api/README.md:61` describe the *automatic* canary shifting the
  `live` alias and then explicitly disclaim hand-editing it for rollback. Those are right and I am
  **not** flagging them — the distinction between "the canary auto-shifts the alias" (fine) and
  "shift the alias by hand as your rollback lever" (the superseded framing) is exactly the line
  R7-1's two misses get wrong.

## Standing items — disposition

- **R5-1 / R5-2 / R5-3 (round-5 record/prose gaps):** **STILL CLOSED** — not re-adjudicated; the
  diff didn't touch them.
- **Governance spine (SoD, audit trail, break-glass owner, manual-gate-as-control):** **STILL
  CLOSED**; swept this round, no regression.
- **NEW database governance (standing-DDL removal, DX-owned path, schema audit trail):** **PASS /
  reference quality** — accountable, audit-traceable, correctly human-gated.
- **NEW DbUp example (CD vs Continuous Deployment, prod gate):** **PASS** — stays on the Delivery
  side.
- **R7-1 (rollback-correction sweep missed two files):** **OPEN / docked** — two stale
  hand-edited-alias references survive in the recovery story; one targeted sweep closes it.

## Bottom line for an eng leader

**Greenlight with one required edit.** The new database-governance work is the strongest *new*
governance content the course has added since I started reviewing it: removing standing DDL is
presented as an accountable, audit-traceable control with a named owner (DX), the schema-delivery
story preserves the SoD / pipeline-as-audit-trail spine and extends it to DDL, and the worked
example stays firmly on the Continuous *Delivery* side with a legitimate human timing gate at prod.
An auditor would accept the "who decided this and where's the evidence" answer. That alone earns
its place.

But I am not going to hand back a 10.0 while the rollback-correction sweep is visibly incomplete.
Two files — the workshop's first concrete recovery artifact and the example pipeline's own summary
comment — still name "alias shift" as the rollback lever, the precise framing the rest of the course
just corrected and now explicitly forbids. It is my six-round theme exactly: the prose got fixed,
the precision didn't propagate everywhere. A reader can walk away with a recovery move the course
tells them elsewhere not to do, in a governance-relevant minimum (#8). That is a half-point, no
more — it's a propagation miss with a mechanical fix, not a design flaw.

**Round-7 rating: 9.5/10** (down 0.5 from 10.0). Finish the sweep — `current-state-pipeline-walkthrough.md:50`
and `examples/.gitlab-ci.yml:226` — and it's back to reference quality.
