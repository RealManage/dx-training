# Continuous Delivery 101 — Review 3

**Student:** Dana (.NET Framework Monolith Maintainer, 12 yrs)
**Stance going in:** "Round 2 left me a champion with two named gaps — the stored-proc case and a real .NET pipeline artifact. I'm told the pipeline sketch was verified against the actual `ciranet-management-api` repo and the fabricated MSDeploy/`setParameters.xml` draft was thrown out. So: did the bridge stop being prose, and is the pipeline finally *true* instead of plausible?"
**Review date:** 2026-06-22
**Overall rating:** 9.5/10 (was 6.0 → 8.5) — would I adopt/champion this?

## Verdict

Both of my round-2 gaps are closed, and the one I most expected to be hand-waved — "same GitLab CI" — is now a verified, honest artifact instead of an assertion. This is the first round where I have nothing left that would block me from handing it to my whole team; the half-point I'm holding back is for residual operational thinness, not for any falsehood.

## Resolved since round 2

### Gap 1 — Stored procedures (`ALTER PROC` shared by callers I don't own) — **IMPROVED (closed)**

This was the single objection that survived rounds 1 *and* 2 — "you went silent where my world is hardest." It is now a dedicated section, `## The stored-procedure case (where a flag can't save you)` (lines 121–147), and it says exactly the thing I had to invent for myself twice:

> "**a feature flag in your code cannot hide an `ALTER PROC` from their code.** The instant you change the proc in place, every caller gets the new behaviour at once — no dark launch, no per-caller ramp, no flag to turn off." (lines 126–128)

The fix is the right one and it's the one I sketched in round 1: version it (`usp_GetHomeownerLedger_v2`), never `ALTER` in place; migrate callers one at a time on *their* schedule; drop the original only when the caller list is empty (lines 132–141). And it goes one concrete step further than I did — it names *how you find the callers you don't own*:

> "in-database references show up in `sys.sql_expression_dependencies`, but apps that `EXEC` the proc over the wire do not — those you find by code search, a DBA audit, or Extended Events on the proc." (lines 137–139)

That `sys.sql_expression_dependencies`-won't-catch-cross-process-`EXEC` distinction is real, it's a trap I've personally been bitten by, and it's the detail that tells me a person who actually owns a SQL Server wrote this and not someone paraphrasing a blog. The closing generalization — "a flag controls code you own; it cannot control a shared database object other code calls … the safety mechanism is a versioned copy plus caller coordination, not a flag" (lines 143–147) — is precisely the boundary I said in round 2 the course "still doesn't acknowledge." It acknowledges it now, cleanly. This gap is closed.

### Gap 2 — No real .NET pipeline artifact; "same GitLab CI" asserted not shown — **IMPROVED (closed, and better than I asked for)**

In round 2 I wrote that the monolith reader "gets a paragraph and a table cell where the Lambda reader gets a template, a handler, tests, a package.json, and an annotated pipeline," and I flagged "same GitLab CI" as "slightly aspirational." I asked for "even a 30-line annotated sketch." What landed is better, because it isn't a sketch of an *imagined* pipeline — it's the shape of the **real** one (`## The monolith's pipeline (the real one)`, lines 149–198), and it explicitly says so: "here is the actual shape, not an analogy" (line 152).

What makes this credible rather than aspirational:

- **It corrects my own round-1 guess.** In round 1 I assumed the .NET artifact would be "a single MSDeploy package … with per-environment `setParameters.xml`." The course's earlier draft apparently agreed with me — and that was *wrong*. The verified mechanism is MSBuild `WebPublishMethod=FileSystem` → `_PublishedWebsites` collected by robocopy into an `a/` artifact carrying **every `web.{env}.config` transform unapplied** → the XDT transform applied **at deploy** (lines 164–176). That's not MSDeploy and it's not `setParameters.xml`. The fact that the course threw out the plausible-but-fabricated version I'd have nodded along to, and replaced it with the awkwardly-specific real one (`a/`, robocopy, `_PublishedWebsites`, GitVersion), is the strongest possible signal that this was checked against a real repo. I trust this section *more* because it doesn't match the tidy mental model I walked in with.

- **It demonstrates "build once, promote the same bytes" instead of asserting it.** `build_api` runs once on a Windows runner and produces `artifacts: paths: ["a/"]`; every `deploy_*` job `extends: .deploy` and consumes that same `a/` — "promoted as-is to every deploy below — never rebuilt" (lines 169–181). The prose under it is careful to say *why* this satisfies the minimums: config applied at deploy not baked per build (CD min #5, #9), tests run in the build job so "a red test gates the merge and every deploy" (line 168). This is the thing I said in round 1 a .NET dev "will not see that their world supports" — now they can read it off the YAML.

- **It is honest about where the real gap is.** "The real gap is **batch size, not tooling**" (line 193): `main` flows continuously only to **dev**; `qa` and `prod` are fed by a **`release/YY.M.W` branch cut weekly** (lines 193–196). *That* is the CD gap, and naming the weekly release branch as the thing to shrink is exactly right — it ties the monolith back to the course's whole thesis (small batches) instead of pretending the monolith is already there. It also correctly refuses to claim infrastructure is the problem: "it already has the immutable artifact, the OIDC, the rehearsed rollout" (lines 196–197).

This is no longer prose where the Lambda story is code. It's a real, annotated pipeline I could hold up next to my own and check line by line. Closed.

### Gap 3 — IIS rollback — **IMPROVED (closed; consistent across two sections now)**

This was my round-1 "section where my reality is hardest and the course is most absent." It was already answered in round 2 via the routing flag; round 3 makes it *stronger* and, crucially, *consistent with the now-verified pipeline*. The `## Recovery on each side` table (lines 202–208) gives the monolith its own column: fail-forward default, the published `a/` artifact promoted by SHA as the immutable unit, "flip the routing flag — reads/writes return to the monolith instantly" as the fast lever, "redeploy the prior published artifact to IIS" as hard rollback, and the dual-write window as the data rollback path. The "hard rollback" answer now matches the real artifact (`a/`, not a MSDeploy package), so the rollback story and the build story finally describe the *same* mechanism rather than two invented ones.

And the new pipeline section adds the operational rollout detail I never even got in round 2: "**zero-downtime across a six-VM farm in two waves** — drain three nodes from the Azure Application Gateway, deploy, restore, repeat" (lines 184–186). That is a real answer to "how do I roll across a farm without a window," and it's the safe-rollout half of min #8 that the Lambda canary gets but the monolith never had. Closed.

### Gap 4 — minimums-reference "How this maps" table had no .NET column — **IMPROVED (closed)**

In rounds 1 and 2 this was "the document I'm told to keep open in MRs and it still has no row for my stack." It now has a third column, "**For our .NET / IIS monolith**" (lines 76–87), and — this is what I want to flag as genuinely good — its cells are now *consistent with the verified pipeline* rather than with the old fabrication:

- Immutable artifacts: "One MSBuild publish output (the `a/` dir) built once, versioned by GitVersion, promoted to every environment" (line 84).
- Config with the artifact: "`web.{env}.config` XDT transform applied **at deploy** to the one artifact — same bytes, per-env values" (line 87).
- Single path to prod: "Same `.gitlab-ci.yml` (via the shared `ci-templates` include); a PowerShell deploy to IIS from a per-server runner" (line 81).

These match the strangler-fig pipeline section exactly — `a/`, GitVersion, deploy-time XDT, `ci-templates` include, per-server runner. The two documents tell one coherent story. A monolith owner with this page open during an MR finally sees their own stack, and what they see is true.

### Gap 5 — `current-state-pipeline-walkthrough.md` AWS-only / fallback re-centred AWS — **IMPROVED (closed)**

Round 2: "the *first* concrete pipeline a student meets is still not mine," and the line-56 fallback presumed a missing pipeline meant "use the AWS baseline." Line 56 is rewritten:

> "Pull up *your* service's pipeline — for a new AWS service its `.gitlab-ci.yml`, for the .NET/IIS monolith the GitLab pipeline that `include:`s the shared `ci-templates` (MSBuild publish → IIS). Score *your* shape against the minimums, not the AWS one; use this baseline only if you have no pipeline at all yet." (line 56)

This is the fix I asked for. It explicitly names the monolith's GitLab pipeline (the `ci-templates` include, MSBuild → IIS), tells me to score *my* shape, and demotes the AWS baseline to the genuine fallback ("if you have no pipeline at all"). It no longer hedges toward Azure DevOps and no longer presumes my pipeline is the AWS one. The walkthrough body is still the CloudFormation example — fine; it's labelled as the baseline and the instruction now points me at my own pipeline first. Closed.

## Still open or newly noticed

I scrutinized the corrected real-pipeline section specifically for *new* errors introduced by the correction, because a freshly-rewritten "this is the real one" section is exactly where an overconfident inaccuracy would hide. I could not find a falsehood. What remains is thinness, not wrongness, and none of it blocks me:

1. **The pipeline YAML is a faithful *shape*, but a .NET dev still can't lift it.** The section is honest that it's "the actual shape, not an analogy," and the real work lives in `build-ciranet-api.ps1` / `deploy-ciranet-api.ps1` and the `ci-templates/dotnet/ciranet-api.yml` include — none of which is shown. That's a defensible scope choice (the course's point is *shape and discipline*, and it says the tooling isn't the hard part), and it's a far cry from round 1's nothing. But it means the artifact demonstrates *that* the monolith does build-once-promote, not *how* to wire it from scratch. I'd call this acceptable, not a gap — the PowerShell is genuinely the boring part — but a reader hoping to copy a working pipeline will still have to open the real repo.

2. **`web.{env}.config` transform vs. the feature-flag refresh story sit in two places and could confuse.** The pipeline section applies the `web.{env}.config` XDT transform *at deploy* (correct, and that's config-with-artifact). Separately, the "parts you are not carving out" section (lines 236–242) correctly warns that editing `web.config` recycles the app pool, so flags you want to flip *without* a recycle should live in a config store read on a refresh interval. Both statements are individually correct, but a careful .NET reader will notice the tension: per-env *deploy-time config* lives in `web.{env}.config` (recycle is fine, it's a deploy), while *runtime feature flags* must NOT live there (recycle is not fine, it's a flip). The course never explicitly says "deploy-time config and runtime flags are different things that happen to both touch `web.config`." It's derivable, and both halves are right, but one sentence drawing the line would stop a reader from thinking the transform mechanism and the flag mechanism are the same lever. Minor.

3. **"Six-VM farm, two waves of three" is presented as the rollout, with no nod to draining order / in-flight sessions.** The zero-downtime wave description is good and I'm glad it's there. It glosses one thing my world actually hits: if the app isn't fully stateless (session affinity, in-flight long requests), draining three nodes still needs a connection-drain interval, not just "remove from gateway, deploy, restore." The example is entitled to assume a drainable, near-stateless web tier, and most of ours are — but it states the wave rollout as frictionless where, in practice, the drain timing is the fiddly bit. Nice-to-have, not a correction.

4. **Production-like *data* for a shared SQL Server is still the one honestly-hard thing left mostly un-worked.** The minimums table now says it straight — "the shared SQL Server is the hard part to make prod-like" (line 85) — and the strangler-fig example is honest that the shared DB is the hard part of the whole migration. That honesty is worth a lot. But "how do I get a prod-like qa when qa SQL is a stale, scrubbed, smaller restore" still isn't *worked* anywhere; it's named as hard and left there. I flagged this at PARTIAL in round 2 and it's the same now. I'm not docking for it — it's arguably out of scope for a 101 and the course is no longer pretending it's a one-liner — but it's the last place my reality is harder than the materials go.

None of these four is a blocker, and none is an inaccuracy introduced by the correction. The correction itself is clean.

## Bottom line for a monolith owner

I'm a champion now, without an asterisk I'd say out loud in the room. The two gaps I named at the end of round 2 — stored procs and a real .NET pipeline — are both closed, and the pipeline didn't just get *added*, it got *verified*: the course replaced a plausible MSDeploy/`setParameters.xml` story (one I'd have believed) with the genuinely-awkward real mechanism (`a/` artifact, deploy-time XDT, GitVersion, `ci-templates` include, six-VM two-wave rollout, OIDC to Azure). Catching and correcting a fabrication I wouldn't have caught myself is exactly what earns the last point and a half of trust from someone in my chair, because it means the *rest* of the document has been held to the same standard.

What would still, in fairness, stop me short of 10: the pipeline is shown as a true *shape* but not as a lift-and-run file (the PowerShell and the shared template are referenced, not shown), and prod-like *data* for a shared SQL Server remains named-as-hard rather than worked. Both are scope-defensible for a 101. Close the prod-like-data story with the same honesty the stored-proc section now has, and this is a 10 for the monolith crowd. As it stands, I'll hand it to my team unedited and start the conversation about shrinking that `release/YY.M.W` branch on Monday.
