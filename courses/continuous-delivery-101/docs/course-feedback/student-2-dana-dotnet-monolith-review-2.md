# Continuous Delivery 101 — Review 2

**Student:** Dana (.NET Framework Monolith Maintainer, 12 yrs)
**Stance going in:** "Round 1 told me the discipline applies to my VM monolith but never showed it. Did the revision actually build the bridge, or just add more disclaimers?"
**Review date:** 2026-06-19
**Overall rating:** 8.5/10 (was 6/10) — would I adopt/champion this?

## Executive summary

The big one landed. `sessions/session-3/examples/strangler-fig-violations.md` is exactly the worked artifact in my world I demanded in round 1, and it does not punt: dual-write keyed on a stable id, idempotent resumable backfill, explicit per-step authoritative store, reconciliation as a first-class deliverable, `ALTER TABLE ... ADD ... NULL` expand/contract on SQL Server, "other people read your tables" as the reason contract is last, and an IIS recovery story (flip the routing flag; redeploy the prior published artifact). The Session 1 reframing now says, in plain words, that the monolith *gets CD too* and is where the pain and payoff live — not that CD is something I do "over there on the greenfield." The decompose exercise grew a brownfield variant with a real dual-write/backfill/contract sequence. This is a different course for me than round 1.

What's still open is narrower but real: (1) **stored procedures** — my single hardest round-1 objection — are still unaddressed; the SQL example carves a *column*, never an `ALTER PROC` shared by four callers; (2) there is still **zero .NET/IIS pipeline artifact** — the monolith delivery story is entirely prose, no MSDeploy/MSBuild/`setParameters.xml` analogue to the SAM walkthrough; (3) two reference surfaces I flagged — the `minimums-reference.md` "How this maps" table and the `current-state-pipeline-walkthrough.md` — are still AWS-only, so a reader who hits those first still sees a course that isn't for them. I went from "would lose most monolith owners by Session 2" to "would keep them, and convert the stubborn ones, with two gaps I can name precisely."

---

## Round-1 points — disposition

For each significant point in my round-1 review: IMPROVED / PARTIALLY ADDRESSED / REGRESSED / STILL OPEN.

### 1. "Applies everywhere is asserted, never demonstrated for my world." — **IMPROVED (the headline fix)**

Round 1: of ~10 worked artifacts, zero were .NET/VM/SQL. Now there is one substantial, end-to-end worked artifact in my stack: `sessions/session-3/examples/strangler-fig-violations.md`. It explicitly frames itself as "CD applied to the **hardest** case we have, not the easiest one" (§"Why this example exists") and runs the full carve-out on ASP.NET/IIS/Azure VMs/SQL Server through the same GitLab CI. The eight-slice table (§"The journey, as shippable slices") gives each slice a flag, a data state, and a recovery move. This is the example I said in round 1 would move me "from comply to champion," and it largely does.

Caveat keeping this short of a 10: it is still *one* artifact, and it's prose + SQL snippets, not a runnable parallel to `violations-api/`. See points 5 and 6.

### 2. "The strangler-fig pattern is name-dropped, never shown." — **IMPROVED (fully resolved)**

Round 1: named 3×, worked 0×. Now worked end to end, and the naming is wired to the artifact everywhere it appears: `README.md` line 10 links "[Strangler Fig in Practice]"; `sessions/session-1/README.md` line 27 calls the Violations API "the *result* of this migration"; `resources/migration-checklist.md` line 104 ("the strangler-fig migration is how the two estates converge … 'we'll get to the monolith later' is how later becomes never"). It answers my round-1 questions directly: who owns the shared table during transition (SQL is authoritative until slice 6 — §"The hard parts"), how the two deploy without lockstep (both pipelines independently; the seam + routing flag decouple them — §"CD practices, visible the whole time"), how a flag routes traffic between old and new (slices 5–6, `FLAG_VIOLATIONS_ROUTE_NEW` % ramp). This was my #2 round-1 ask and it is built.

### 3. "Stateful, shared databases treated as a footnote." — **IMPROVED, with one hole** (see point 4)

Round 1: the whole data story was four DynamoDB bullets + one rollback paragraph. Now `strangler-fig-violations.md` §"The hard parts (no hand-waving)" and §"Expand/contract on SQL Server (the concrete schema dance)" give the shared-DB case real treatment: the dual-write window is named as where "correctness bugs cost money" (it even calls out fees on violations), reconciliation is "part of the work, not a nicety," and the four-step `ALTER TABLE ... ADD ExternalViolationId UNIQUEIDENTIFIER NULL` → write both → migrate readers → contract dance is concrete SQL. Critically, it names *why* contract is hard and slow on a shared DB: "Reports, integrations, and other monolith modules may `SELECT` from the violations tables directly. You cannot drop them … until those readers move … 'expand/contract' is a coordination practice, not just a schema trick." That is precisely the round-1 insight I had to supply myself. Genuinely addressed — for *columns and tables*. Not for procs (point 4).

### 4. "Expand/contract on shared stored procs is unaddressed; a flag can't hide an `ALTER PROC` from other callers." — **STILL OPEN (my hardest point, still my hardest point)**

This was the single objection I ended round 1 unconvinced on, and it survives. The new SQL treatment is entirely *column*-based. `strangler-fig-violations.md` adds a column (`ExternalViolationId`) and discusses readers of *tables*. It never touches a stored procedure. My round-1 problem stands verbatim: if I `ALTER usp_GetHomeownerLedger`, every caller — including three apps I don't own — gets the new behavior at once; a feature flag in *my* code cannot hide a changed shared DB object from *their* code. The course's safety mechanism (flag-it-off) still has a blind spot exactly at my worst coupling, and the course still doesn't acknowledge the boundary.

To be fair, the new material gives me *most* of the tools to reason about it: the "other readers must move before you contract" insight (§"The hard parts") is the same coordination problem as a shared proc, and `usp_..._v2` + route-new-code-to-it is the proc analogue of dual-write. But I'm connecting those dots myself again. For a course that just proved it *can* go deep on SQL Server, leaving the proc case out is a conspicuous omission — procs are the *more* common monolith coupling than a bare column, and they're where the "flag can't hide it" failure actually bites. This is the one place I'd still say "you went silent where my world is hardest."

### 5. "Promote the same artifact assumes I know my artifact format; only `sam package` shown." — **PARTIALLY ADDRESSED**

Round 1: a .NET dev would think promotion is a SAM feature. Now `strangler-fig-violations.md` §"Recovery on each side" puts the two side by side in a table: monolith "published build packaged and tagged by `CI_COMMIT_SHA`, promoted to each environment," directly parallel to the Lambda "versioned bundle in S3, promoted by SHA." §"And the parts you are not carving out" says build-once-promote runs "on the existing pipeline." So the *principle* is now generalized for me. What's still missing is the *mechanics*: there's no MSDeploy/web-deploy-package/`setParameters.xml` worked detail the way there's a full `sam build → sam package → sam deploy` walkthrough in `violations-api/README.md`. The concept is mine now; the concrete artifact recipe still isn't shown. Hence partial.

### 6. "No .NET-on-VM worked thread parallel to the Lambda one; immutable artifact + config-with-artifact + IIS rollback unshown." — **PARTIALLY ADDRESSED**

This was my round-1 high-priority recommendation #1. The *narrative* now exists (strangler-fig example covers all three: SHA-tagged published artifact, config-via-`web.config`/App Configuration, IIS rollback via routing flag / redeploy prior artifact). But there is still **no .NET pipeline artifact** anywhere in the repo — I checked; no `.yml`/`.yaml` mentions IIS/MSDeploy/MSBuild/dotnet, and the only file the build-once flow is *worked* in is `violations-api/` (SAM). The monolith reader gets a paragraph and a table cell where the Lambda reader gets a template, a handler, tests, a package.json, and an annotated pipeline. That asymmetry is smaller than round 1 (then it was nothing) but it's still there: the discipline is described for my world, not demonstrated in a file I could lift. Partial, trending positive.

### 7. "Feature flags: only worked example is a Lambda reading env at cold start; my IIS/`web.config`/recycle story is unaddressed." — **IMPROVED (concept), STILL OPEN (worked code)**

Round 1: `feature-flag.ts` resolves once at cold start; copy that mental model into a long-running IIS process and a flag flip needs an app-pool recycle, which the course never flagged. Now the course addresses the *concept* in three places: `sessions/session-1/README.md` line 41 ("feature flags (via config, not just Lambda env vars)"); `strangler-fig-violations.md` §"And the parts you are not carving out" ("for a long-lived IIS process this is `web.config`/`appSettings` or a config store (e.g., Azure App Configuration), read at startup or on refresh, rather than Lambda environment variables. Same idea, different switch"); and the round-1 gotcha is implicitly handled by "read at startup or on refresh" + the recovery table's note that the routing flag returns traffic "instantly." That's the right idea. What's still open: the actual `feature-flag.ts` is unchanged and still 100% Lambda, and nobody worked the IIS specifics I asked for — the config-reload-vs-recycle decision, per-server config drift across a VM farm, `IFeatureManager`/Microsoft.FeatureManagement. The course now *says* "read on refresh" but doesn't show me how to get refresh without a recycle, which is the actual operational trap. Concept improved; the worked .NET flag remains absent.

### 8. "The course quietly tells me not to bother with the monolith (Session 1 §1 'practice on greenfield'; assessment Part 4 'pilot a new AWS one')." — **IMPROVED (mostly resolved)**

Round 1: rhetorical inclusion, practical exclusion. The Session 1 framing is materially rewritten. The old "the new cloud-native services are the *opportunity* … the natural place to establish CD first" is gone; `sessions/session-1/README.md` line 41 now reads "CD is not something you do only on the new services … We start where the practices are easiest to *see*, then carry them straight back to the estate that needs them most." `exercises/current-state-assessment.md` Part 4 line 76 now offers "a new AWS one is easiest, **or a single capability you're carving out of the monolith via strangler-fig**" — the monolith is a first-class pilot option, not an afterthought. `migration-checklist.md` line 104 is the strongest: "carrying it back to the monolith, where most of the pain and most of the payoff live. 'We'll get to the monolith later' is how later becomes never." That's the reframe I wanted. Minor residue: greenfield is still consistently called "easiest," which is fair, but the phrasing leans on it a lot.

### 9. "minimums-reference 'How this maps to RealManage' table is AWS-only; no .NET/VM column." — **STILL OPEN**

`resources/minimums-reference.md` lines 74–85: the table header still reads "What it looks like for our AWS services" and every row is still AWS (`npm test`, Lambda bundle, `configuration/qa.config`, shift the Lambda alias). This is the document I said in round 1 was "the best document in the course for me" and "where I most needed to see myself and most clearly didn't." It's unchanged. A monolith owner keeping this page open during MRs (as the doc instructs) still has no row that reflects their stack. Cheapest high-value fix left on the board — add a third column or a parallel .NET table.

### 10. "current-state-pipeline-walkthrough scores someone else's pipeline (CloudFormation); no VM/IIS/SQL equivalent." — **STILL OPEN**

`sessions/session-1/examples/current-state-pipeline-walkthrough.md` is unchanged — entirely the `iac-baseline` CloudFormation pipeline. The workshop step (line 56) still says "pull up *your* service's `.gitlab-ci.yml` (or this baseline if you don't have one)." I still don't have a `.gitlab-ci.yml`; I have an MSDeploy-to-IIS pipeline, and I'm still scoring my system against a worked example built for someone else's. Less painful than round 1 only because the strangler-fig example later shows my world — but the *first* concrete pipeline a student meets is still not mine.

### 11. "migration-checklist Phase 2 reads OIDC / `configuration/{env}.config` as *the* way, not *one* way." — **PARTIALLY ADDRESSED**

`resources/migration-checklist.md` line 50 still says "No static AWS credentials anywhere — GitLab OIDC role assumption only" and line 52 still says "`configuration/{env}.config`" as the literal item. A monolith team reading Phase 2 literally still thinks they fail it because they use an Azure DevOps managed-identity service connection instead of OIDC. *However*, the surrounding context improved a lot: Phase 0 now has "If your estate is mixed (monolith + new services), sequence it deliberately" (line 21), and §"The journey, not just the destination" line 104 explicitly routes mixed estates to the strangler-fig migration. So the checklist now *acknowledges* my estate even though individual Phase-2 items are still written in AWS-specific letter. The principle ("no static creds in the deploy path") still hides behind its AWS instance.

### 12. "Production-like environment treated as a config-file one-liner; far harder for a stateful VM monolith." — **PARTIALLY ADDRESSED**

`sessions/session-3/README.md` §5.1 still treats prod-like as a one-liner (`configuration/qa.config`, "smaller scale"). But `strangler-fig-violations.md` indirectly does better by being honest that the *shared SQL Server* is the hard part of the whole migration ("the delivery habits and the shared database are" the hard part — §"The starting state"). My specific round-1 gripes — qa SQL is a stale scrubbed restore, LB/session affinity that qa lacks — still aren't named anywhere. The course is more honest that stateful is hard; it still doesn't work the prod-like-*data* problem for a shared SQL Server. Partial.

### 13. "Capture batch-size-per-deploy in the assessment, not just frequency." — **PARTIALLY ADDRESSED**

`exercises/current-state-assessment.md` Part 2 now has a "**Median MR size** (lines / files changed)" row (line 56) and "**Median branch lifetime**" — both new and both useful, and they get at batch size from the MR angle. My specific ask (batch size *per prod deploy* — the number that shocks management because one monolith deploy bundles dozens of features) still isn't a row; "deploys to prod / week" still undersells the monolith by hiding bundle size. Closer, not exact.

### 14. (New, credit) "Part 1 of the assessment didn't handle a mixed estate." — **IMPROVED**

Round 1 I noted the per-service metrics undersold a monolith. The assessment now opens Part 1 with a "Mixed estate?" callout (lines 15) telling me to "score your **weakest** system … or fill the scorecard once per system; don't average them into a misleading middle." That directly serves a monolith-plus-services shop and didn't exist before. Good addition.

---

## Scrutinizing the strangler-fig example for hand-waving

This is the artifact the whole revision rests on, so I pushed on it hard. Where it holds and where it thins:

**Holds up (no hand-waving):**

- **Dual-write + idempotency.** §"The hard parts" mandates "every write **idempotent** on `violationId` so retries and the backfill cannot double-count," and explicitly assigns authority per step ("SQL until slice 6, the new service after"). This is the correct, non-trivial answer and it's stated as required, not optional.
- **Reconciliation.** Named as a deliverable with a "documented answer for what you do when the two disagree," and tied to money ("if violations carried *fees* … this window is where correctness bugs cost money"). That's the part most write-ups skip; this one front-loads it.
- **Backfill as a non-deploy job.** Slice 3 is explicitly "a job, not a release," resumable, watermarked by timestamp, idempotent by key. This fixes my exact round-1 complaint that the slice model had "no concept for a backfill."
- **Contract is gated on external readers.** "You cannot drop them … until those readers move. This is *why* contract is last and often slow." Coordination framed as the real cost. Correct and honest.
- **Honest about duration.** "It is weeks, not an afternoon … CD does not make the migration small; it makes each *step* small." No overselling.
- **IIS recovery.** §"Recovery on each side" gives the monolith its own column: fail-forward default, SHA-tagged published artifact, "flip the routing flag — reads/writes return to the monolith instantly" as the fast lever, redeploy prior artifact as hard rollback, and the dual-write window itself as the data rollback path. This answers my round-1 "how do I roll back an MSDeploy to IIS?" — the answer is "usually you don't; you flip the flag," which is actually the right CD answer.

**Where it still hand-waves or omits:**

- **Stored procedures: absent.** Covered above (point 4). The SQL "concrete schema dance" is a column add. A proc is the more common and harder monolith coupling and it's not in the example.
- **"Same GitLab CI … targets IIS on the VMs instead of Lambda."** (§"The starting state"). This is asserted as if it's a solved, boring fact. For a real ASP.NET Framework app that's MSBuild + a web-deploy package + an IIS deploy step with `setParameters.xml` per environment — none of which is shown, and some of which (in-place IIS deploy vs. app-pool drain, multi-VM farm rollout order) is genuinely fiddly. The example treats "the CI tooling is not the hard part" as license to not show it. For me, the tooling *isn't* the hardest part, so I'll allow it — but a junior .NET dev will not be able to build that pipeline from this.
- **`web.config` flag refresh without a recycle** (point 7). "Read at startup or on refresh" glosses the one operational detail that actually bites a long-running IIS process.
- **Multi-VM config drift.** The example says flags live in `web.config` or a config store "read at startup or on refresh" but never names that a `web.config` flag has to be flipped on *every* VM in the farm consistently, which is exactly the drift problem I raised in round 1. A config store (App Configuration) solves it; the example mentions the store but doesn't connect it to the drift problem.

Net: the example is substantially honest and handles the genuinely hard SQL realities (dual-write, idempotency, reconciliation, backfill, reader coordination, IIS flag-rollback) that I expected it to punt on. Its omissions are specific and nameable: stored procs, the actual IIS pipeline mechanics, and the flag-refresh/farm-drift operational detail.

---

## Exercises — round-2 re-attempt

I re-ran both exercises for my monolith, this time using the new brownfield material.

### Current-state assessment — **better fit**

The "Mixed estate?" callout (Part 1) and the MR-size + branch-lifetime rows (Part 2) made the scorecard fit my world more honestly than round 1. My scores are unchanged from round 1 (we still cherry-pick a `release` branch, still hand-patch `web.config` on boxes, still "rollback = re-run the prior pipeline and pray"), but the exercise now *invites* me to record them as a monolith rather than forcing me into per-service framing. Part 4 now lets me pick "a single capability you're carving out of the monolith via strangler-fig" as the pilot — so for the first time the exercise's own advice points at *my* system, not away from it. Remaining gap: still no batch-size-per-prod-deploy row (point 13).

### Decompose a branch (brownfield) — **the change that mattered**

Round 1 I brought a real change — add an `IsDelinquent` flag to the homeowner-ledger endpoint, requiring a column, an index, an `ALTER` to `usp_GetHomeownerLedger`, a DTO field, and a new DAL path — and the exercise had no vocabulary for steps 1–3. This round the brownfield variant (`exercises/decompose-a-branch.md` lines 87–99) and the strangler-fig example gave me most of the vocabulary:

| Round-1 sticking point | Round-2 status |
| ---------------------- | -------------- |
| Add nullable column to shared table | **Covered.** Expand step, `ADD ... NULL`, backward-compatible (strangler-fig §"Expand/contract on SQL Server"). |
| Backfill the column | **Covered.** "A job, not a release," idempotent (brownfield slice 3; strangler-fig slice 3). |
| Column read by 3 other apps I don't own | **Covered.** "Other people read your tables … contract is last" (strangler-fig §"The hard parts"). This was the insight I had to invent in round 1. |
| Flag for my DAL read path on IIS | **Mostly covered.** Concept ("config, not Lambda env vars," read on refresh) is there; the recycle/farm-drift mechanics aren't. |
| Additive DTO field behind flag | **Covered (was already fine).** |
| **`ALTER usp_GetHomeownerLedger` shared by 4 callers** | **STILL not covered.** No proc versioning, no `_v2`-and-route guidance, no acknowledgement that a flag can't hide an `ALTER PROC` from other callers. |

So: five of my six round-1 sticking points now have a worked answer in the materials, and I no longer have to invent them. The sixth — the stored proc — is exactly where I ended round 1, and I'm ending round 2 the same way. The difference is that now the *rest* of the change decomposes cleanly using the course's own vocabulary, so the proc is an isolated remaining gap rather than the tip of a course-wide silence.

---

## New things that are wrong or confusing (round 2)

Nothing factually wrong jumped out, and I'm not the AWS fact-checker. Smaller flags:

- **README goal line still says "on AWS cloud-native services"** (`README.md` line 6: "Move a development team from weekly deployments … to true Continuous Delivery on AWS cloud-native services"). Round 1 I flagged this exact line as contradicting "applies everywhere." Line 10 and Session 1 are now much better, but the *goal statement* — the first sentence a skeptic reads — still scopes the course to AWS. With the strangler-fig example now in the box, this line undersells the course's own content. One-word-ish fix; high visibility.
- **"Same GitLab CI builds the monolith"** is stated as settled fact in two places (`strangler-fig-violations.md` §"The starting state", and §"CD practices, visible the whole time": "on *both* GitLab pipelines, the monolith's and the new service's"). If the org's monolith genuinely already builds through GitLab CI, fine — but in my lived experience the .NET Framework monolith is on Azure DevOps / a classic release pipeline, and asserting GitLab for it without showing it reads slightly aspirational. Either it's true (then a one-paragraph pipeline sketch would prove it) or it's simplification (then say "your CI — GitLab or Azure DevOps").
- **`current-state-pipeline-walkthrough.md` line 56 fallback** ("or this baseline if you don't have one yet") still presumes a missing pipeline means "use the AWS baseline," not "you have a different-shaped pipeline." Minor, but it re-centers the AWS artifact as the default again.

---

## Recommendations (round 2)

### High priority (the two remaining real gaps)

1. **Work the stored-proc case.** Add to `strangler-fig-violations.md` (or the brownfield decompose) a short section on expand/contract for a *shared stored procedure*: version it (`usp_X_v2`) and route new code to the new proc rather than `ALTER`-ing in place; treat the old proc exactly like the old column — keep it until every external caller migrates; and state plainly that **a feature flag in your code cannot hide an `ALTER`ed shared proc from other apps** — coordination, not a flag, is the safety mechanism there. This is the one round-1 objection that's fully survived, and the example is already 90% of the way to having the tools.
2. **Add a .NET/IIS column to `minimums-reference.md` (lines 74–85) and the walkthrough.** The reference table is the doc I'm told to keep open in MRs and it still has no row for my stack. Cheapest high-impact fix remaining: a parallel column ("…for our .NET/IIS estate": MSBuild + MSTest + coverage; web-deploy package tagged by SHA, promoted; flip routing/config flag or redeploy prior package; `web.config` transforms / `setParameters.xml`).

### Medium priority

3. **Show one .NET pipeline artifact**, even a 30-line annotated sketch, so build-once-promote and config-with-artifact are *demonstrated* for IIS, not only described. It would convert point 5 and point 6 from PARTIAL to IMPROVED. Doesn't need to rival `violations-api/`; it needs to exist.
4. **Fix the README goal line** (line 6) to match the course's own content — e.g., "to true Continuous Delivery across our estate, from new AWS services to the established .NET monoliths." The strangler-fig example earns this; the goal line should stop contradicting it.
5. **One paragraph on flags in a long-running IIS process**: config-store refresh vs. app-pool recycle, and per-VM config drift across a farm (and that a config store solves the drift). The course now says "read on refresh"; show what refresh costs.

### Nice to have

6. **Add a batch-size-per-prod-deploy row** to the assessment (Part 2) — for a monolith, frequency hides the problem; bundle size reveals it.
7. **Soften "same GitLab CI … the monolith"** to "your CI (GitLab or Azure DevOps)" unless the org truly has the monolith on GitLab — and if it does, prove it with rec #3.

---

## Verdict

**Champion, with two named gaps.** The revision built the bridge I said was missing — a worked strangler-fig carve-out on ASP.NET/IIS/SQL Server that handles the hard data realities (dual-write, idempotency, reconciliation, backfill, reader coordination, IIS flag-rollback) instead of punting — and reframed the monolith from "guest" to "where the payoff lives." That's a genuine 6→8.5. It stops short of 10 because my single hardest case (shared stored procedures) is still unaddressed, and the monolith story is told in prose where the Lambda story is shown in code. Close the stored-proc gap and add a .NET row to the minimums table and I'd call it a 9.5 and hand it to my whole team without translation.
