# Continuous Delivery 101 — Review 7 (database-delivery round)

**Student:** Dana (.NET Framework Monolith Maintainer, 12 yrs)
**Stance going in:** "Round 6 closed the cycle at 9.5 with one open item explicitly in my lane — R6-D1 / R3-8: prod-like *data* for the shared SQL Server, named-as-hard and never worked, deferred as 201-level. The course just grew a whole database-delivery spine: a DbUp worked example in C#/.NET against SQL Server, a resource on schema-as-code, glossary terms, checklist and troubleshooting items, an assessment reflection. This is the most squarely-in-my-chair change in seven rounds. So the questions are sharp: does the DbUp migrator map to my *actual* 12-year-old shared SQL Server, or is it a greenfield toy? Are the drift and cross-DB tactics real or hand-wavy? And — the one I've carried since round 3 — does any of this finally *work* the prod-like-data problem, or just name it again with more words? I am licensed to declare reference quality, but the DB content is new and central to me, so I'm judging it hard."
**Review date:** 2026-06-23
**Overall rating:** 9.5/10 (was 6.0 → 8.5 → 9.5 → 9.5 → 9.5 → 9.5) — would I adopt/champion this?

## Verdict

The database spine is the real thing, not a greenfield toy — it is written by someone who has clearly baselined an existing SQL Server, not just `CREATE TABLE`d a fresh one. The DbUp mechanism is correct (`SchemaVersions` journal, forward-only, idempotent re-run = safe rollback), the build-once/promote/vary-only-the-connection-string pipeline is the *same* shape as the app pipeline applied to schema, and the two hazards in my lane — **drift** and **cross-database coupling** — are handled with concrete, correct tactics (`MarkAsExecuted` baseline + reconcile-up-to-prod; per-DB journals + expand/contract across the boundary in dependency order), not arm-waving. This is reference-quality 101 database content. **But it does *not* close R6-D1 / R3-8.** The new content works *schema* and *reference* data thoroughly; it does **not** work *prod-like application data* for the shared SQL Server — seeding/masking/subsetting a realistic QA dataset (PII scrub, subset-vs-full-clone, refresh cadence). In fact it sharpens the gap: the worked example's local playground starts from an **empty** database (`Script0001` comment: "a fresh database (the local playground starts empty)"), and `database-delivery.md:14` asserts "Migrations prove out against a real database (a local one, then `qa`)" — but an empty local SQL Server is not production-*like* in the dimension that bites my monolith, which is the *data*, not the structure. The course is now honest about this in exactly one place (`minimums-reference.md:97`, "the shared SQL Server is the hard part to make prod-like") and the new resource conspicuously routes *around* it: drift, coupling, baseline-*reference*-data all worked; prod-like *bulk* data still absent. So R6-D1 is **advanced in adjacency but not closed** — and I am holding at 9.5, for the same reason as rounds 3–6: it remains a defensible 101-scope boundary I decline to dock for, now with a far better neighbourhood around it. The half-point is still scope philosophy, not a defect. I found **zero regressions** and **one genuinely minor wording snag** (below) that I will not dock for either.

## Score trajectory

| Round | Score | Δ | One-sentence reason |
| ----- | ----- | - | ------------------- |
| R1 | 6.0 | — | Discipline applies to my monolith; the materials demonstrate it almost not at all (zero .NET artifacts). |
| R2 | 8.5 | +2.5 | Strangler-fig carve-out landed in my stack; stored procs and a real .NET pipeline still open. |
| R3 | 9.5 | +1.0 | Both gaps closed; the pipeline was the *verified real* `ci-templates`/MSBuild→IIS mechanism. |
| R4 | 9.5 | 0.0 | VSM-in-S1 retired my "AWS-pipeline-first" complaint; held the half-point for batch-size soft gap + R3-8. |
| R5 | 9.5 | 0.0 | R4-3 + R4-7 real fixes; named the residual hair (exemplar didn't model its own batch rule). |
| R6 | 9.5 | 0.0 | R5-4 closed that residual; only R3-8 (prod-like SQL *data*, scope call) remained. |
| **R7** | **9.5** | **0.0** | New DB spine is reference-quality and squarely in my lane — *schema* delivery fully worked. But R3-8 (prod-like *data*) is advanced, not closed; still the one item between 9.5 and 10, still a scope call I won't dock. |

The flat delta is, once more, earned — but for a *different* reason than R4–R6. Those rounds were flat because nothing new landed in my lane. This round, a great deal landed in my lane and is genuinely excellent — yet the single item that has gated my perfect score since round 3 is the *one* database problem the new content does not work. Up is blocked by that exact item; down is blocked by finding no breakage and a worked example that survives contact with my real database harder than I expected.

---

## Does the DbUp migrator map to MY shared SQL Server, or is it a greenfield toy? — **MAPS. Concretely.**

I went in expecting a toy: a `CREATE TABLE` against an empty database, a "look, migrations!" demo that evaporates the moment you point it at a 12-year-old schema with hundreds of objects, cross-DB FKs, and a decade of accreted procs. It is better than that, and the tell is *where the realism lives*.

What I verified on disk:

- **The runner is mechanically correct for SQL Server.** `Program.cs` uses `DeployChanges.To.SqlDatabase`, `.WithScriptsEmbeddedInAssembly`, a non-zero exit on `!result.Successful` (`Program.cs:39-45`) that fails the pipeline job — a red migration stops the line, exactly the framing I'd want. `dbup-sqlserver` 5.0.40 in `Migrator.csproj:13` is a real, current package. The `SchemaVersions` journal claim (`database-delivery.md:19`, `glossary.md:160`) is accurate to how DbUp actually behaves. No fabrication.
- **It does not pretend to *be* my monolith — and says so correctly.** `db-migrations/README.md:5`: "a standalone modern .NET console tool; it targets SQL Server but does not need to match the monolith's .NET Framework version." This is the right call and it pre-empts my first objection. My monolith is .NET Framework 4.x; a migrator that *had* to be 4.x would be a needless constraint. A `net8.0` console tool (`Migrator.csproj:6`) that talks to the *same* SQL Server is exactly how I'd actually do this — the runner is decoupled from the app's runtime. That sentence is the work of someone who has shipped DbUp against a legacy estate.
- **The greenfield-toy risk is contained to `Script0001`, deliberately, and the drift section handles the real case.** `Script0001` is an idempotent fresh-DB create (`Script0001__create_assessments.sql:3`, "the local playground starts empty"). On its own that *would* be a toy. But `database-delivery.md:40-42` carries the move that makes it real for me: **baseline production** by scripting the current `prod` schema as the starting migration and `MarkAsExecuted` so the runner never tries to recreate existing objects, then reconcile lower environments *up to* prod. That is precisely how you onboard DbUp onto a 12-year-old database that already exists — you do *not* run `Script0001` against prod; you baseline-and-mark. The course knows the difference between standing up a fresh DB and adopting automation onto a live one, and it documents both. That is the single most important thing it could get right for my chair, and it got it right.
- **Expand/contract is shown as a *nullable column*, the honest minimum.** `Script0002__expand_add_due_date.sql:9` adds `DueDate DATE NULL` guarded by a `sys.columns` existence check, with the contract explicitly deferred to "its own forward migration, only after every reader/writer uses `DueDate`" (`:12-14`). Nullable-add is the one expand step that is unambiguously backward-compatible on SQL Server without a table rewrite or a default-backfill lock. Choosing *that* as the worked expand — rather than something that would lock a large table — is again the realistic choice. (See the one caveat below on what nullable-add does *not* cover.)

**Verdict on the toy question:** not a toy. The fresh-create lives only in `Script0001` where a local playground genuinely starts empty; the moment the content turns to *my* world (existing prod, drift, coupling) it switches to baseline-and-reconcile. I would hand `database-delivery.md` and the DbUp example to a monolith engineer unedited.

---

## Drift and cross-DB coupling — realistic and sufficient, or hand-wavy? — **REALISTIC.**

These are the two hazards that actually keep me up, because my environments *have* diverged over twelve years and I *do* have smaller databases holding references across the boundary into the monolith. I pressure-tested both.

**Drift (`database-delivery.md:34-42`) — correct and sufficient for 101.** The three-step fix is the right one and in the right order: (1) baseline prod as the source of truth with `MarkAsExecuted`; (2) reconcile `dev`/`qa` *up to prod*, "not the other way around — `prod` is the truth" (`:41`); (3) "lock the door" so only migrations move schema forward and drift cannot quietly reaccumulate (`:42`). The causal claim at `:36` — "You cannot automate on top of unknown state" — is exactly the sentence I'd open the team meeting with. The glossary entry (`glossary.md:141-142`) defines drift and names the same resolution. This is not hand-wavy; it is the canonical convergence play, stated tightly. *What it doesn't do* — and correctly defers — is the harder reconciliation reality where lower envs have drifted in ways that *conflict* with prod (a column that exists in qa with a different type than prod). The course says "bring dev and qa to match prod"; for a genuinely messy estate that reconciliation is itself a project. But naming the direction (converge *to* prod) is the 101 job, and it does it. I do not dock.

**Cross-DB coupling (`database-delivery.md:44-50`) — the three tactics are exactly right.** This section reads like it was written for me specifically: "one large database plus smaller ones that hold references across the boundary" (`:46`) is my estate, verbatim. The tactics:

1. **Version each database independently** — own migration project, own `SchemaVersions` journal, "do not manage several databases from one tangled script set" (`:48`). Correct. A single journal across DBs is how you get a migrator that can't be promoted independently.
2. **Add no *new* cross-database references** — "New coupling is the thing you can still prevent" (`:49`), treating the boundary like a service seam and pointing at the strangler-fig example. This is the right *posture*: you cannot un-tangle twelve years of cross-DB FKs in a 101, but you can stop the bleeding, and the course says exactly that.
3. **Cross the boundary with expand/contract and ordered deploys** — expand the referenced DB first, migrate readers, then contract, "deploying the databases in dependency order" (`:50`). Correct, and the dependency-order point is the bit most treatments miss.

**Is it *sufficient*?** For 101, yes. The one thing a cross-DB owner will still ask that the course doesn't answer: SQL Server cross-database **referential integrity** is not enforceable by FK across databases anyway (you cannot `FOREIGN KEY` across a database boundary in SQL Server), so my real coupling is usually *procedural* — a proc or view in DB-A that `JOIN`s or `EXEC`s into DB-B. The expand/contract-across-the-boundary tactic still applies, but the course frames the coupling as "references" generically without naming that the enforcement is procedural, not declarative. That is a 201 nuance, not a 101 gap, and the strangler-fig stored-proc section (which I've praised since round 2, with its `sys.sql_expression_dependencies`-misses-cross-process-`EXEC` trap) already carries the procedural-coupling realism elsewhere. So: sufficient, with a known frontier. Not docked.

---

## R6-D1 / R3-8 (prod-like *data* for the shared SQL Server) — **ADVANCED, NOT CLOSED. Explicitly.**

This is the disposition I was asked to make explicit, and it is the heart of this review. Let me be precise about what moved and what didn't, because the new content makes the distinction *more* visible, not less.

**What the new content works (and works well):**

- *Schema* delivery for the shared SQL Server — fully worked, end to end (DbUp example + `database-delivery.md`).
- *Baseline / reference* data — status codes, lookups, seed rows — fully worked, idempotently, via the `MERGE` in `Script0003__seed_assessment_status.sql` and `database-delivery.md:52-54`. This is genuinely new since R6 and it is correct: `MERGE` on a stable key converges whether the table is empty or seeded.
- The *honesty* about the destination — `database-delivery.md:30-32` names plainly that "lower environments have drifted from production, there is no schema automation today, and local databases are still uncommon ... those gaps are real prerequisites, not afterthoughts." That candour is exactly the register I asked for in earlier rounds.

**What it still does *not* work — the actual R3-8 item:**

- *Prod-like application data* for QA on the shared SQL Server. The thing I named at R3/R4/R5/R6: a realistic, large, **non-reference** dataset for a production-*like* test — subset-vs-full-clone, PII scrub/masking, refresh cadence, how big and how stale. This is categorically different from baseline reference data (a dozen status codes) and from schema. It is the dimension in which *my* QA environment fails to resemble prod, and it is the one the course routes around.

**The new content sharpens, rather than closes, the gap — two concrete tells:**

1. `database-delivery.md:14`: "Migrations prove out against a real database (a local one, then `qa`) before `prod` sees them." And `Script0001` comment: the local playground "starts empty." An **empty** local SQL Server, or a `qa` seeded only with reference data, proves the *migration applies* — it does **not** prove the migration is safe against *prod-shaped data volumes and edge cases*. My real expand/contract pain isn't "does `ADD DueDate DATE NULL` apply"; it's "does the eventual `NOT NULL` contract, or the backfill, behave against 40 million rows with a decade of dirty values." The course's "production-like test environment (#7)" for schema (`database-delivery.md:14`, `db-migrations/README.md:67`) quietly equates "a real SQL Server instance" with "production-like" — but for *data*, instance parity is the easy half and data parity is the hard half. The course has now thoroughly worked the easy half and still names-but-doesn't-work the hard half.

2. The one place it's still named straight is unchanged from R6: `minimums-reference.md:97` — "the shared SQL Server is the hard part to make prod-like." Seven rounds in, that sentence is *still* the entire treatment of prod-like *data*. Everything around it got richer; it did not.

**Disposition:** R6-D1 / R3-8 is **advanced in its neighbourhood, not closed**. The schema/reference-data spine that now surrounds it is reference-quality, which makes the prod-like-*data* hole both smaller-feeling (because so much else is handled) and sharper-edged (because the empty-playground and "real database" framing now actively imply data parity is solved when it isn't). I am **not moving off 9.5.** I decline to dock, for the fourth straight round, because seeding/masking/subsetting a multi-terabyte shared SQL Server for QA is genuinely 201-level data engineering and the course is honest that it's hard. But I will not pretend it closed, either — it didn't. It is the *same* lone item, now with a much better house built around it.

**The one-line fix that would close it (offered, not demanded):** a short worked note, parallel to the drift section, on prod-like *data* for the shared SQL Server — subset-vs-full-clone, a PII-masking pass, and refresh cadence, with the same honesty the stored-proc section already models. Even three paragraphs naming the *shape* of the answer (not a full worked masking pipeline) would convert "named-as-hard" into "worked-at-101-depth" and would, for my chair, be the move from 9.5 to 10.

---

## "No standing human DDL access" + "DX owns the path" — credible for my reality? — **CREDIBLE, because it's framed as direction, not a Monday checklist.**

This was my sharpest skepticism going in. I have had direct `sa`-equivalent DDL access to my databases for twelve years. Local databases are *uncommon* on my team — most engineers point at a shared `dev`. An end-state of "no engineer has standing write/DDL access to shared `dev`, `qa`, or `prod`" (`database-delivery.md:30`) is, for my world, a large cultural and operational change. So the question is whether the course over-claims.

It does not, and the framing is the reason:

- It is explicitly a **destination owned by DX**, not a team rollout step. `database-delivery.md:32`: "We are not there yet ... DX owns and is driving the path ... your team's part is to adopt local databases and route schema changes through the pipeline *as that path lands*." `session-3/README.md:126` and `CLAUDE.md` carry the same "DX owns that path" framing. This is the correct ownership boundary — removing my standing DDL access is not something I can or should do unilaterally next sprint; it's a platform capability that has to be *provisioned* (runner-in-pipeline, secrets, local-DB workflow) before the access can be withdrawn. The course sequences it that way.
- The assessment reflection respects this precisely. `current-state-assessment.md:50`: "Where the answer is 'by hand,' that is a deliberate target DX is driving toward automation — **note it, don't score it as a personal gap.**" That is exactly right for my reality — scoring "I have DDL access" as a *failing* would be punishing me for the absence of a platform capability that isn't my job to build. Marking it as a known target owned by DX is honest and non-blaming. This mirrors the round-6-confirmed "no QA team / the team owns quality" register: it names the end-state without shaming the current state.
- The local-database prerequisite is named as *uncommon today*, not assumed. `database-delivery.md:32` ("local databases are still uncommon") and the containerized-SQL-Server playground in `db-migrations/README.md:43-55` give the concrete on-ramp. The `mcr.microsoft.com/mssql/server:2022-latest` container with a throwaway SA password (flagged "local only; not a real secret," `:52`) is exactly the low-friction local DB I'd actually stand up. It even tells me to run it twice and watch the journal skip applied scripts (`:63`) — the idempotency proof, hands-on.

**Verdict:** credible. The course does not tell me to revoke my own DDL access on Monday; it tells me where we're going, who owns getting us there, and what my part is when it lands. For a 12-year direct-access monolith owner, that framing is the difference between a credible direction and an insulting mandate. It chose credible.

---

## Regression sweep — no regressions; prior monolith wins intact

I swept everything I've praised across six rounds for breakage introduced by the database work.

- **Links resolve.** Every outbound link in the new files resolves on disk: `database-delivery.md` → db-migrations README, strangler-fig, glossary (all present); db-migrations `README.md` → `Program.cs`, three `Scripts/*.sql`, the CI YAML, violations-api README, and `../../../../resources/database-delivery.md` (all present). No broken cross-references introduced.
- **The R5-4 batch-size annotation and the worked-example math are untouched.** I didn't expect the DB work to touch the VSM, and it didn't — out of scope, no collateral edit.
- **Strangler-fig SQL treatment intact.** The stored-proc section (`usp_..._v2`-not-`ALTER`-in-place, the `sys.sql_expression_dependencies` cross-process-`EXEC` trap, "a flag controls code you own; it cannot control a shared database object other code calls") is unchanged — and the new cross-DB section now *points at it* (`database-delivery.md:49`), which is good cross-linking, not a regression.
- **`minimums-reference.md` monolith mapping table intact** (`:88-99`): the `qa`-mirrors-IIS/SQL row, the `web.{env}.config` XDT-at-deploy row, the GitVersion-stamped `a/` artifact row — all the .NET-specific realism I praised at R3 is verbatim.
- **TypeScript-only-exception is correctly scoped.** `CLAUDE.md` and `db-migrations/README.md:5` both flag the C#/.NET+SQL-Server+DbUp example as the *one* deliberate exception to "examples are TypeScript + SAM," justified because database delivery lives in the .NET estate. This is the right exception to make and it's documented in both the authoring rules and the example itself. No accidental TypeScript-bias leak into the DB content; no accidental .NET-bias leak out of it into the cloud-native examples.
- **No AWS bias crept in via the DB pipeline.** The DB pipeline uses OIDC + Secrets Manager (`db-migrations.gitlab-ci.yml:27-35, 72-74`), which is correct because the *secret* lives in AWS Secrets Manager even when the *database* is SQL Server — that's an accurate picture of a hybrid estate, not bias. The migrator targets SQL Server throughout. Neutrality holds.

---

## New this round (adversarial pass) — one minor wording snag, not docked

I hunted hard for fresh breakage in the new content. I found one thing worth naming and several things worth praising.

1. **Minor wording snag — "production-like test" overclaims for *data* (the same thing as R3-8, surfaced as a phrasing issue).** `database-delivery.md:14` ("Migrations prove out against a real database (a local one, then `qa`)") and `db-migrations/README.md:67` ("an ephemeral SQL Server service container (a production-like test, #7)") both use "production-like" to describe an environment that is production-like in *topology/engine* but not in *data*. For schema-application that's fine; the migration genuinely proves it *applies*. But a monolith reader could read "production-like test for schema = solved" and miss that the data dimension is exactly the unsolved R3-8. **Suggested one-word honesty:** somewhere in `database-delivery.md`, note that an ephemeral/empty instance proves a migration *applies*, while proving it's *safe against production-shaped data* is the harder, separately-named problem (link to the `minimums-reference.md:97` "hard part to make prod-like" line). This would make the existing R3-8 honesty *consistent* across the new content instead of contradicted by the "production-like" phrasing. **Not docked** — it's a phrasing seam on an already-acknowledged scope boundary, not a false claim.

2. **`Script0002`'s nullable-add is the honest expand — and the contract's data risk is correctly deferred but worth a future nod.** The example expands with `DueDate DATE NULL` and defers the contract. Correct. The thing a monolith owner learns *next* — that the eventual `NOT NULL` tightening or the backfill is where the *data* danger lives (locking, dirty historical rows, the data trap) — is named generically (`database-delivery.md:60`, the data trap) but not *worked* against volume. Same family as R3-8. Future, not now.

3. **Praise with reasons — the DbUp pipeline is the app pipeline, proven.** `db-migrations.gitlab-ci.yml` is structurally the *same* build-once/promote/manual-prod-timing-gate shape as the cloud-native pipeline: `validate:migrations` proves against an ephemeral SQL Server service container (`:41-51`), `build:migrator` publishes once on `main` (`:54-62`), `.migrate` is a reusable promote step varying only the Secrets-Manager-fetched connection string (`:66-75`), and only `migrate:prod` keeps `when: manual` with the correct comment "a human approves TIMING, not readiness the pipeline proved" (`:96`). The `needs:` chain (`:81,88,95`) enforces dev→qa→prod order. This is the course's central thesis — the pipeline is the single path, schema is no exception — demonstrated, not asserted. It earns its place.

I'll be honest about diminishing returns, as I have every round: the database spine is the largest genuinely-new thing to land in my lane since the round-2 strangler-fig carve-out, and it is *good*. It maps to my real database, handles my two real hazards correctly, and frames the cultural end-state credibly without shaming where I am. The one thing it does not do is the one thing I've named since round 3 — and that remains a defensible 101 boundary. I am not manufacturing a seventh-round nitpick; the wording snag (#1) is real but minor and is the *same* item as R3-8 wearing a different hat.

---

## Prioritized open-items list (tiered; blocker vs polish)

### Tier 1 — one-line, clearly worth doing

None. The DB content introduced no Tier-1 defect.

### Tier 2 — small additions (POLISH)

| ID | Item | Effort | Where |
| -- | ---- | ------ | ----- |
| R7-D1 | **Make the "production-like" phrasing honest for *data*.** Add a sentence noting an ephemeral/empty instance proves a migration *applies*, not that it's safe against production-shaped data — link to the `minimums-reference.md:97` "hard part to make prod-like" line. Removes the seam between the new content's "production-like test" framing and the still-open prod-like-*data* gap. | 1–2 lines | `database-delivery.md` (near `:14`) |
| R3-7 | **Nod to drain timing in the six-VM wave rollout.** Carried from R3/R4/R5/R6; unchanged. Genuinely optional. | 1 line | `strangler-fig-violations.md` wave section |

### Tier 3 — scope call (the only item tied to a perfect score)

| ID | Item | Effort | Where |
| -- | ---- | ------ | ----- |
| R3-8 / R6-D1 | **Work the prod-like-SQL-*data* problem** with the honesty the stored-proc and drift sections already model: subset-vs-full-clone, PII masking/scrub, refresh cadence — three paragraphs naming the *shape* of the answer, not a full masking pipeline. The schema/reference-data spine added this round makes this the *only* remaining un-worked database problem, and the lone item between 9.5 and 10 for the monolith chair. A 101-scope judgment call. I do **not** dock for leaving it; it is genuinely 201-level data engineering. | Larger (or a short note) | `database-delivery.md` (a "Hazard: production-like data" section parallel to the drift section) / `minimums-reference.md:97` |

**Blockers:** none. Every item is refinement or a scope call; the Tier-1 line is empty.

---

## Bottom line for a monolith owner

The course grew the database spine it was missing, and it is the real thing for my world, not a greenfield toy. The DbUp migrator is mechanically correct for SQL Server, decoupled from my .NET Framework runtime (`db-migrations/README.md:5`), and — critically — the content knows the difference between standing up a fresh database (`Script0001`, empty local playground) and adopting automation onto my 12-year-old live one (`database-delivery.md:40-42`, baseline prod with `MarkAsExecuted`, reconcile lower envs *up to* prod). My two real hazards are handled with correct, concrete tactics: drift (converge to prod, lock the door) and cross-database coupling (per-DB journals, add no new coupling, cross the boundary with expand/contract in dependency order). The "no standing human DDL access" end-state is framed credibly — as a direction DX owns, with local databases as the on-ramp and an explicit "note it, don't score it as a personal gap" (`current-state-assessment.md:50`) that respects my twelve years of direct access without shaming it. Zero regressions; the strangler-fig SQL treatment, the `web.{env}.config`-XDT-at-deploy row, and the R5-4 batch annotation are all intact.

I'm holding at 9.5, and the reason is the same single item I've named since round 3: prod-like *data* for the shared SQL Server — seeding/masking/subsetting a realistic QA dataset — is still named-as-hard and not worked. The new content *advances* it (everything around it is now reference-quality) but does **not** close it, and in two places ("production-like test," the empty playground) the new framing quietly implies data parity is solved when it's only structure parity that is. That is the lone gap between this and a 10 for my chair, it remains a defensible 101-scope boundary, and I decline to dock for it for the fourth straight round. The schema half of "the database is the elephant" is now as complete as a 101 can honestly make it. I hand this to my team unedited.
