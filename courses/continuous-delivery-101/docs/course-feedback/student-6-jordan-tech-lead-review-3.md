# Continuous Delivery 101 — Review 3

**Student:** Jordan (Tech Lead, 7 yrs)
**Stance going in:** Unchanged. I want CD; my only question is "can I run this next sprint without blowing my team's commitments or inventing the rollout myself." This round I'm specifically testing the new database material against the same bar: the course *admits* local DBs are uncommon today — so does it hand me enough to stand up the playground, or just assert the prerequisite and leave me the cost? Round 2 landed at 9/10 ("the migration is an actionable journey"), held back by two example bugs and the unscripted in-the-room conversations.
**Review date:** 2026-06-23
**Overall rating:** 9/10 — flat vs round 2. Two technical regressions I docked for last round are now *fixed* (that alone would push to 9.5), but the new DB material introduces one genuine adoption-cost gap that costs the same half-point back. Net: still a 9, for different reasons.

## Executive summary

My two highest-priority round-2 technical asks both shipped, and shipped *well*: `scripts/smoke-test.sh` now exists (non-mutating, reads the URL from stack outputs — exactly the 10-liner I asked for), and the `deploy:dev` `needs:` list now reads `[lint, validate:sam, unit-tests, build:artifact]` with an inline comment that makes *precisely* the argument I made about why `dependency-audit` is deliberately excluded. That's the rare thing — a reviewer's two top nits closed cleanly, with the reasoning baked into the example so the next reader doesn't re-trip on it.

The new database material is good *content*. `resources/database-delivery.md` and the DbUp worked example are technically sound, correctly forward-only, and the expand/contract story is consistent end to end. Where I push back is squarely in my lane: the migration-checklist Phase-2 DB items read as **team checklist boxes** ("schema changes only through the pipeline," "engineers develop & test against a local database before merge"), but every *other* place the same topic appears tells me this is **DX-owned, not a team rollout** — "note it, don't score it as a personal gap," "as that path lands." Those two framings don't cohere, and the gap they leave is exactly the one I was sent to find: the course tells me local DBs are uncommon, asserts them as a Phase-2 prerequisite I check off, but the *only* standup guidance is a 16-line docker-compose buried in a C#/.NET example README — and even that doesn't address my real blocker, which isn't "how do I run a container," it's "how does an engineer get production-like *data and schema* into that local DB when there's drift and no baseline yet." So for the DB items specifically I'm back where round 1 left the whole migration: a destination, not a journey. The difference is the course *says* DX owns the journey — which is fine as a division of labor, but then the checklist shouldn't hand me the box to tick.

Everything else is solid. Branch-by-abstraction is a real addition (below). No bloat. I'd still champion this.

## What I was sent to check, point by point

### 1. Can my team actually DO the new Phase-2 DB items next sprint? — NO, and the course half-admits it

This is the crux. The two new checklist items are `resources/migration-checklist.md:66-67`:

- `[ ]` "Database schema and baseline data change **only** through the pipeline (a migration runner such as DbUp), never by hand on a shared environment"
- `[ ]` "Engineers develop and test migrations against a **local database** ... before merge"

As written, these are *my team's* Phase-2 boxes — same visual grammar as "build the artifact once" and "no static AWS credentials," which genuinely are things my team does. But they are **not** things my team can do next sprint, and the course knows it:

- `resources/database-delivery.md:32`: "there is no schema automation today, and local databases are still uncommon ... **DX owns and is driving the path** ... your team's part is to adopt local databases and route schema changes through the pipeline **as that path lands**." (emphasis on the conditional.)
- `sessions/session-3/exercises/current-state-assessment.md:50`: "Where the answer is 'by hand,' **that is a deliberate target DX is driving toward automation — note it, don't score it as a personal gap.**"
- `sessions/session-3/README.md:126`: "**DX owns that path**; here we make the mechanism concrete."
- `CLAUDE.md:118` (the course's own design intent): the end-state is "**direction owned by DX**, not a team rollout checklist."

So three resources plus the course's own stated intent say: *not your rollout, don't score it, it lands when DX delivers it.* And then the migration checklist — the one artifact I literally copy into my repo and check off — hands me two unconditional boxes for it, with no "(blocked on DX)" marker, no ordering against the DX work, sitting in the same Phase-2 list I'm supposed to execute. **That is a half-measure.** A tech lead copying that checklist into their wiki will either (a) try to tick those boxes and discover they can't, because there's no runner-in-pipeline and no local-DB workflow yet, or (b) leave them unticked indefinitely and quietly let "later become never" — the exact failure mode the course warns about for the monolith (`migration-checklist.md:111`).

The fix is small and the course already wrote the words for it elsewhere: mark those two checklist items as **DX-owned / adopt-as-it-lands**, the same hedge `database-delivery.md:32` and the assessment use, so the checklist agrees with the other four places. Right now the checklist is the one document that pretends the DB items are mine to drive on my sprint cadence.

### 2. Is the local-DB standup guidance enough to stand up the playground? — NO

The course explicitly flags local DBs as "uncommon" — i.e., this is the prerequisite most teams *don't* have. So the standup cost is the load-bearing question, and the answer is thin:

- The **only** concrete guidance is `sessions/session-3/examples/db-migrations/README.md:43-63`: a 16-line docker-compose for `mcr.microsoft.com/mssql/server:2022-latest` plus a `dotnet run` command. It's correct and it's a fine *illustration*. But it's buried in the one C#/.NET example, and it stops at "you have an empty SQL Server running locally."
- My actual blocker isn't the container. It's: **how does an engineer get a useful schema and baseline data into that local DB before there's a baseline migration?** The course's own drift hazard (`database-delivery.md:34-42`) says today's environments have *drifted from prod* and there's *no automation yet* — which means right now an engineer can't just "run the migrations against localhost," because the migrations don't exist as a converged set. The honest sequence is: DX baselines prod → produces `Script0001` baseline → *then* a local DB is `docker compose up` + `dotnet run`. Until that baseline lands, "develop against a local database" has nothing to develop *against*. The course never connects those two facts, so the docker-compose snippet reads as "look how easy" when the actual prerequisite (the baseline migration) is DX's unfinished work.
- For my team specifically: the entire local-DB story is SQL Server / DbUp / .NET. That's correct for our *monolith* — which is exactly the estate this matters for — so I'm not docking it for being .NET (the scoped exception is the right call). But it does mean the playground guidance is only legible to my engineers who work the .NET side; the new-Lambda-service folks see a C# example and tune out, even though the principle is meant to be universal.

Net: the playground is *asserted* as a prerequisite and *illustrated* with a container, but the cost of crossing drift → baseline → first usable local DB is left to DX (correctly) while the checklist still hands *me* the box (incorrectly, per point 1). I can't run this next sprint; I can only wait for DX and adopt when it lands — which is fine *if the materials said that consistently.*

### 3. Is the Phase-2 DB guidance concrete and ordered, or aspirational? — CONCRETE on mechanism, ASPIRATIONAL on rollout, UNORDERED

Split verdict:

- **Mechanism — concrete and good.** The DbUp example (`sessions/session-3/examples/db-migrations/`) is a genuinely strong worked reference. `Program.cs` is minimal and correct (non-zero exit on failure = red build = stop the line, line 41-44). The pipeline (`db-migrations.gitlab-ci.yml`) mirrors the app pipeline exactly: build once (line 54-62), promote the same artifact, vary only the connection string from Secrets Manager (line 71-75), ephemeral SQL Server as the production-like test (`validate:migrations`, line 39-51), prod the only manual gate (line 96). The expand/contract is real: `Script0002` adds `DueDate` as **nullable** (line 9) with the contract explicitly deferred to a later forward script (line 12-14), and `Script0003` seeds via idempotent `MERGE` (line 4-14). A junior could read this and understand schema-as-code. No complaints on the mechanism.
- **Rollout — aspirational.** As point 1 covers: the two checklist boxes have no sequencing relative to DX's work, no "blocked-on" marker, no "here's what your team does *first* (adopt local DBs) vs what waits on DX (runner-in-pipeline, access removal)." `database-delivery.md:32` *names* the three things DX is standing up (runner-in-pipeline, local-DB workflow, access removal) but doesn't tell me which of those gates which of my two checklist items. So even reading charitably — "these are DX-owned" — I still can't sequence my team's part against DX's deliverables, because the dependency isn't drawn.

### 4. Does "branch by abstraction" give me a real next-sprint tool? — YES, mostly

This one I'm pleased with. `sessions/session-2/README.md:92` introduces it at exactly the right moment — right after feature flags, framed as "flags gate a *behavior* at a call site; branch by abstraction gates an *implementation* behind an interface" — and gives the concrete shape: "put an interface (a *seam*) over what you're changing, build the replacement behind it while the old path stays live, move callers across, then delete the old path." The glossary entry (`glossary.md:103-104`) repeats the seam→build→switch→delete sequence and names the use case I care about: "the tool of choice for swapping a dependency or a large refactor."

For my world that's a real tool, not just a term. I have a payments-adapter swap coming that a single feature flag can't cleanly wrap — branch by abstraction is exactly the technique, and now I can point an engineer at the glossary and Session 2 §3.2 and they'll get the shape. **Where it stops short of "next-sprint actionable":** there's no *worked* branch-by-abstraction decomposition the way there's a worked expand/contract in `decompose-a-branch.md`. The brownfield example there (lines 89-101) is an expand/contract data change, not an interface-seam refactor. So a junior gets the four-step shape but no slice-by-slice example of "swap the data-access layer" the way they get a slice-by-slice escalation-history change. It's a real tool with a real definition; it's one worked example short of being something I'd hand over unsupervised. Medium-priority, not a blocker — the technique is well enough described that I can coach the slices myself.

### 5. Is the current-state-assessment DB reflection useful to run with my team, or a token line? — USEFUL

`current-state-assessment.md:50` is a single blockquote, but it's a *good* one. It does three things that make it run-able as a team prompt: (a) it scopes the gut-check to the right minimums (#2, #5, #9 for schema), (b) it asks the three questions that actually surface the state — "through the pipeline or by hand? one runner and history, or ad-hoc scripts? do engineers have a local DB?" — and (c) crucially, it tells my team *not to score it as a personal gap* because it's a DX-driven target. That last clause is what stops the reflection from becoming a morale sink: without it, my .NET engineers would score three honest "No"s and feel like they're failing the assessment for something that isn't theirs to fix. With it, it reads as "here's a known org-level gap, note where you sit on it." I'd run this line as-is in the assessment meeting. It's not a token line; it's appropriately sized for a gut-check that feeds a DX-owned workstream.

The one inconsistency — and it's the same root issue as point 1 — is that this line correctly says "don't score it as a personal gap," while the migration checklist hands me a personal box to tick for the same thing. The *assessment* gets the framing right; the *checklist* doesn't.

## Regression sweep — both round-2 regressions FIXED, no new ones

I specifically re-checked my two round-2 dockers:

- **`smoke-test.sh` — FIXED (was REGRESSED).** `sessions/session-3/examples/violations-api/scripts/smoke-test.sh` now exists. It's exactly the shape I asked for: reads `ApiUrl` from the stack's own CloudFormation output (line 30-33, no hard-coded endpoints), probes with an **empty body so it mutates nothing** (line 42-46 — important, since it runs against prod every deploy), and treats `400`/`501` as healthy (feature dark vs released) while failing on `000`/`403`/`5xx` (line 48-56). The deploy template calls it after every `sam deploy` (`.gitlab-ci.yml:183`). The auto-promotion safety argument now rests on a script that exists. Closed cleanly.
- **`deploy:dev` `needs:` list — FIXED (was STILL OPEN, my top annoyance).** `sessions/session-3/examples/.gitlab-ci.yml:194` now reads `needs: [lint, validate:sam, unit-tests, build:artifact]` — `lint` and `validate:sam` are in, and the comment (lines 189-193) makes *exactly* the argument I made: list every gating job, and `dependency-audit` is "deliberately NOT listed: an advisory signal must not look like a gate." It even anticipates the transitive-`needs` foot-gun. This is the model way to close a reviewer nit — fix it *and* encode the reasoning so it doesn't regress. (Note: the pipeline also moved from `violations-api/.gitlab-ci.yml` to `examples/.gitlab-ci.yml`; links in Session 3 README resolve.)

No new regressions found. The DbUp example links resolve, the glossary cross-links are intact, and the new DB content is cross-linked from Session 2 (§4.1 line 121), Session 3 (§5.3 line 124-128), the checklist, the assessment, and troubleshooting — wired in, not orphaned.

## My re-attempt with my team

Same squad as before: 5 engineers, a new AWS Lambda (`aws-notifications-api`) plus a co-owned slice of the legacy .NET resident-portal Web API on Azure VMs (SQL Server backend). Weekly train, median branch ~5 days.

**Current-state assessment + the new DB reflection.** I ran the DB gut-check (`current-state-assessment.md:50`) against the .NET/SQL side honestly: schema changes today = a DBA-ish lead runs `ALTER` against shared dev/qa by hand (No on pipeline-only); no migration runner, ad-hoc scripts in a folder (No on one-runner-and-history); no engineer has a local SQL Server (No on local DB). Three No's — and the "don't score it as a personal gap, DX is driving it" clause is what made that an *honest* line item rather than three demoralizing red marks. Useful as-run. **But** when I then went to the migration checklist to put the DB items into my Phase-2 plan, I hit the contradiction: the checklist wants me to tick "schema only through the pipeline" and "engineers develop against a local DB," and I can't — there's no runner in our pipeline yet and no baseline migration to develop against. I ended up writing "(DX-owned; adopt when the runner + baseline land)" next to both boxes *myself* — which is precisely the annotation the course should have shipped on those two lines.

**Decompose-a-branch with a refactor (branch by abstraction).** I tried the new technique against a real one: swapping our notification send path from a direct SMTP call to a provider abstraction (so we can add SMS later). Branch by abstraction is the right tool and the Session 2 framing got me the four steps — introduce `INotificationSender` seam, build the new SMTP impl behind it while the old direct path stays live, move callers, delete the old path. I could decompose it into ~5 daily slices off the glossary + §3.2 alone. What I *didn't* get from the materials was a worked interface-seam example to hand a junior — the worked brownfield example is a data expand/contract, not a code-seam refactor. I coached the slices; a junior running solo would get the shape but guess at the slice boundaries. Confirms point 4: real tool, one worked example short.

## Recommendations

### High priority
1. **Make the migration-checklist DB items agree with the rest of the course.** `migration-checklist.md:66-67` should carry the same DX-owned hedge the other four locations use — e.g., mark both boxes "(DX-owned; your team adopts local DBs and routes schema through the pipeline *as the runner + baseline land*)." As written they're the only place the course hands a tech lead a personal box for something it elsewhere says explicitly is *not* the team's rollout. This is the one genuine adoption-cost trap in the new material.

### Medium priority
2. **Connect drift → baseline → first usable local DB.** The docker-compose (`db-migrations/README.md:43-63`) shows an *empty* local SQL Server, but the course's own drift hazard means there's no converged migration set to run against it *until DX produces the prod baseline*. One or two sentences naming that dependency ("a useful local DB needs the prod baseline migration first — that's part of what DX is standing up") would stop the snippet from reading as "look how easy" when the real prerequisite is unfinished. This is the local-DB *cost* the prompt asked whether the course owns — right now it under-states it.
3. **Add one worked branch-by-abstraction decomposition** to `decompose-a-branch.md` — a code-seam refactor (swap a data-access layer or a dependency) sliced day-by-day, parallel to the existing data expand/contract example. The technique is well-defined; it's one worked example short of hand-it-to-a-junior.

### Nice to have
4. **Carry-over from round 2, still open:** the external-third-party-dependency slice (shipping a paid integration like Twilio dark, credentials per env). Unchanged this round; still the one decompose case a junior gets no guidance on.
5. **Carry-over:** intra-phase sequencing inside Phases 1–2 (the DB items make this sharper — they have a real dependency on DX deliverables that the flat checklist doesn't express).

## Verdict

**Still champion.** The DbUp mechanism, the database-as-code framing, and branch by abstraction are all genuine, technically sound additions, and my two top round-2 bugs are fixed exactly right. The one thing holding the new DB material back is in my exact lane: the migration checklist hands me two Phase-2 boxes the rest of the course tells me aren't mine to drive, with no marker for the DX dependency and only an empty-container snippet for the prerequisite the course itself calls uncommon. Make the checklist agree with the assessment and the resource, and this is a 9.5. **9/10, flat vs round 2 — two fixes earned the half-point, one new adoption-cost gap spent it.**
