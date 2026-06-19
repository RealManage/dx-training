# Continuous Delivery 101 — Review 1

**Student:** Dana (.NET Framework Monolith Maintainer, 12 yrs)
**Stance going in:** "This is for the cloud-native kids. I run an ASP.NET Framework Web API on Azure VMs with a real database and stored procedures. Show me it applies to *me* or I tune out."
**Review date:** 2026-06-19
**Overall rating:** 6/10 — would I adopt/champion this?

## Executive summary

The *practices* are genuinely platform-independent and the course says so repeatedly — small batches, short-lived branches, decouple deploy from release, automate the verdict. On that level it earned more of my respect than I expected: the principles survive translation to my VM monolith intact. But the course **asserts** "applies everywhere" far more than it **demonstrates** it. Every worked example, every line of YAML, every code snippet, the entire decompose exercise, and both rollback strategies live in TypeScript/Lambda/SAM/AWS land. My world — IIS on Azure VMs, MSBuild/MSDeploy, Azure DevOps or Octopus, a single shared SQL Server I cannot "promote," EF migrations and stored procs — appears as a sentence in the framing and then never again. The strangler-fig pattern is named three times and never shown. By Session 2 I had stopped trying to map the examples and started skimming. A skeptical monolith owner needs *one worked artifact in their own stack* to stay in the room, and this course doesn't provide it.

## Section-by-section

### Course framing (README)

The framing is more honest than I feared. `courses/continuous-delivery-101/README.md` line 10 explicitly names "our established .NET Framework APIs on Azure VMs will be with us for a long time" and frames new AWS work as *additional*, not a replacement. Good — that sentence is the only reason I kept reading past the title.

But then the same README undercuts it. The course goal (line 6) is literally: *"Move a development team from weekly deployments with long-lived branches to true Continuous Delivery on AWS cloud-native services."* On AWS cloud-native services. So which is it — a platform-independent discipline, or an AWS migration course? The Target Audience (lines 29-35) leads with "Software Engineers building the new cloud-native AWS services" and never lists "engineers maintaining the established .NET APIs." The Prerequisites (lines 79-85) want AWS SAM CLI, an AWS sandbox, and `iac-baseline` read access. Nothing about MSDeploy, IIS, Azure DevOps, or SQL Server. The implicit message to me is clear: *you're not the primary audience.* For a course whose thesis is "this applies to everyone," the framing keeps quietly telling the monolith crowd they're guests.

The "Not a tool rollout" callout (line 94) is the strongest thing here and it's correct: I *can* do most of this with the tools I already have. I wish the whole course were pitched at that altitude.

### Session 1 — Why CD & the Minimums

This is the best session for me, because it's mostly argument, not AWS. The self-reinforcing trap diagram (`sessions/session-1/README.md` §2.1, lines 49-62) and the batch-size table (§2.2, lines 76-82) describe *my* world precisely. My team releases the monolith roughly every two weeks, behind a change-advisory email, and yes — when it breaks, "which of 40 changes did it?" is exactly the diagnosis hell described. I didn't need a Lambda to feel that. The "deploy more often in smaller pieces" point lands regardless of platform. Credit where due.

The "We have GitLab CI, so we do CI — no" callout (§4.1, line 130) is fair and I've lived the failure: we have Azure DevOps pipelines and *still* run week-old branches. The distinction between owning a pipeline tool and doing the discipline is the most useful idea in the course for me.

Where Session 1 lost altitude: §1 (lines 21-39). It names the .NET-on-VMs reality, then immediately pivots — "the new cloud-native services are the *opportunity*... the natural place to establish CD first" (line 39). Read from my chair, that is: *go practice CD over there, on the greenfield stuff, where it's easy.* That's not wrong as advice, but it's the opposite of "this applies to your monolith." It tells me the monolith is where CD goes to be too hard.

The scoring workshop (§5) routes me to `examples/current-state-pipeline-walkthrough.md`, which scores the **`iac-baseline` AWS CloudFormation pipeline**. That is the only "real pipeline" the course shows me, and it isn't mine and never could be. The workshop says "pull up *your* service's `.gitlab-ci.yml` (or this baseline if you don't have one)" (walkthrough lines 56). I don't have a `.gitlab-ci.yml`. I have an Azure DevOps `azure-pipelines.yml` (or worse, a classic GUI release pipeline) deploying via MSDeploy to an IIS app pool. The assessment table maps cleanly enough in the abstract, but I am scoring my system against a worked example built for someone else's. No effort is made to show what the same scoring looks like for a VM/IIS/SQL deployment.

### Session 2 — Trunk-Based Development & CI

The *prose* of Session 2 is the most universally applicable content in the course, and it mostly works for me. Long-lived branches, drift, merge pain, "merge when it's done" being the largest batch (`sessions/session-2/examples/branching-antipatterns.md` Anti-pattern 2) — all platform-agnostic, all true of my team. Anti-pattern 4 (branch-per-environment, lines 38-44) is *literally my current process* — we have a `release` branch we cherry-pick into. Seeing it named as an anti-pattern with a clear fix was valuable and uncomfortable in the right way.

But this is also where the course stopped being for me, in two concrete places:

1. **Feature flags (§3).** The whole section, and the entire `feature-flag.ts` example, is TypeScript reading Lambda env vars resolved at cold start (`feature-flag.ts` lines 44-69: "Resolve once at cold start; cheap and predictable for a Lambda"). My monolith is a long-lived IIS process with `web.config` / `appSettings`, not a Lambda that re-reads env on every cold start. The *concept* transfers fine — a runtime switch defaulting off — but the worked pattern doesn't, and nobody tells me how a flag lives in a .NET Framework app. Do I use `ConfigurationManager.AppSettings`? An `IFeatureManager` (Microsoft.FeatureManagement)? Azure App Configuration? A row in SQL? Each has real gotchas (config reload without an app-pool recycle, per-server config drift across my VM fleet). The course hand-waves "or lives in a config store" (§3.3, README line 98) and moves on. For the one practice I most need to adopt, the example is useless to me.

2. **The CI gate (§4.2 + `ci-pipeline.gitlab-ci.yml`).** Lint/vitest/coverage/`sam validate`/`npm audit`. None of those verbs exist in my world. My equivalents are MSBuild, MSTest/NUnit/xUnit, ReportGenerator or coverlet for coverage, and... what's my "sam validate" for an IIS deploy? There isn't one shown. Again the *principle* (fast automated gates before merge) is sound; the *artifact* is not mine.

The decompose workshop (§5) hands me an exercise that is explicitly about building a *new AWS Lambda* (`exercises/decompose-a-branch.md` line 13). More on that under Exercises.

### Session 3 — The Pipeline

This is the session that confirmed my suspicion that the course is, underneath the disclaimers, an AWS course.

- **Immutable artifacts & promotion (§4).** This is the strongest CD idea and the one I most want — build once, promote the same bytes, vary only config. But every word is SAM-specific: `sam build` → `sam package` → packaged template promoted to each `sam deploy` (README §4.1, lines 81). For me, "build once, promote the same bytes" is achievable and I *want* it — a single MSDeploy package (`.zip` / web deploy package) built once and pushed to dev/qa/prod IIS with per-environment `setParameters.xml`. That maps beautifully! But *I* had to construct that mapping. The course never does. A .NET dev who hasn't already internalized web-deploy parameterization will not see that their world supports this, and will conclude "promotion is a SAM feature."

- **Production-like environment (§5.1).** "qa mirrors prod via `configuration/qa.config`" (README line 95). My problem isn't config files — it's that my qa SQL Server is a stale restore of prod from three weeks ago with scrubbed PII, sitting on a smaller VM, and my prod IIS farm has a load balancer and session affinity that qa doesn't. "Production-like" is a *much* harder, more expensive problem for a stateful VM monolith than for a stateless Lambda, and the course treats it as a one-liner about a config file.

- **Rollback (§5.2 + `rollback-on-aws.md`).** Five rollback strategies, four of them pure AWS (Lambda alias shift, canary auto-rollback, redeploy artifact, CloudFormation auto-rollback). The single platform-neutral one — feature-flag kill switch (Strategy 5) — is the one whose worked example (TypeScript) doesn't fit me either. The ECS note (lines 96-98) gets a paragraph; the IIS/Azure-VM monolith gets *nothing*. How do I roll back an MSDeploy to an IIS farm? Keep the prior web-deploy package and redeploy it? Blue-green with deployment slots (App Service) or two app pools behind the LB? IIS doesn't have a `live` alias I can repoint in seconds. This is the section where my reality is *hardest* and the course is *most* absent.

The fail-forward-first framing (§5.2, lines 99-104) is genuinely good and platform-independent. The "rollback data trap" (`rollback-on-aws.md` line 39) is the single most relevant paragraph in Session 3 for me, because I have a real database — but see my objections: it under-sells how much harder this is when "the database" is a shared SQL Server with stored procedures, not a per-service DynamoDB table.

### Resources (minimums, glossary, checklist, troubleshooting)

- **`minimums-reference.md`** is the best document in the course *for me*, precisely because it's the most platform-neutral. The minimums themselves are stated as discipline, not tooling. The "How this maps to RealManage" table (lines 74-85), though — every single row's right-hand column is AWS ("our AWS services," `npm test`, Lambda bundle, `configuration/qa.config`, shift the Lambda alias). There is no column, no row, no footnote for the .NET/VM estate that the README promised "will be with us for a long time." This table is where I most needed to see myself and most clearly didn't.

- **`troubleshooting.md`** is the one resource that actually reaches toward me, and I want to credit it. The "stateful change blocks small releases" entry (lines 82-89) names expand/contract for a schema shift — that's *my* problem, finally addressed. But it's a DynamoDB example again ("add the new shape, write to both, migrate, remove old") and four bullet points long. Expand/contract on a shared SQL Server with stored procedures and EF migrations across a multi-VM farm is a deep, scary topic; four bullets is a gesture, not guidance.

- **`glossary.md`** is fine and genuinely useful — clear deploy-vs-release, fail-forward, expand/contract definitions. Platform-neutral. No complaints.

- **`migration-checklist.md`** is mostly platform-neutral and usable, which I appreciate. But Phase 2 (lines 43-59) is studded with AWS specifics ("GitLab OIDC role assumption only," "shift Lambda alias," `configuration/{env}.config`) as if those are *the* way to satisfy each minimum, rather than *one* way. A monolith team reading the checklist literally will think they're failing Phase 2 because they don't use OIDC, when the actual minimum is "no static credentials in the deploy path" — a thing I can satisfy with an Azure DevOps service connection + managed identity.

### Exercises — my attempt

#### Current-state assessment (`exercises/current-state-assessment.md`)

I filled it out for the monolith. The *structure* works platform-independently, which is the best thing I can say about any exercise here. My honest scorecard:

**Continuous Integration**

| # | Practice | Score | Evidence |
| - | -------- | ----- | -------- |
| 1 | Trunk-based development | No | We have a long-lived `release` branch + cherry-picks (the course's Anti-pattern 4). |
| 2 | Integrate to trunk daily | No | Feature branches live 1-3 weeks. |
| 3 | Automated tests before merge | Partial | MSTest suite runs in the PR pipeline, but coverage is uneven and integration tests against SQL are flaky/skipped. |
| 4 | Automated tests on merged result | Partial | Runs on `develop`, not consistently green. |
| 5 | Red build stops feature work | No | A red build is "someone will get to it." |
| 6 | New work doesn't break delivered work | Partial | We try, but stored-proc changes have broken callers. |

**Continuous Delivery**

| # | Practice | Score | Evidence |
| - | -------- | ----- | -------- |
| 1 | We practice CI | No | See above. |
| 2 | Pipeline is the only way to deploy | Partial | Mostly via release pipeline, but emergency hotfixes go to prod IIS by hand via MSDeploy from a senior dev's box. |
| 3 | Pipeline decides releasability | No | A CAB-style sign-off meeting decides. |
| 4 | Automated definition of deployable | Partial | Build + tests, no security/coverage gate. |
| 5 | Immutable artifacts | No | We frequently rebuild per environment; config is sometimes hand-edited in `web.config` on the box. |
| 6 | Red pipeline stops feature work | No | |
| 7 | Production-like test environment | Partial | qa exists but its SQL is a stale, smaller, scrubbed restore. |
| 8 | Rollback on demand, rehearsed | No | "Rollback" = re-run the prior release pipeline and pray the DB migration is reversible. Never rehearsed. |
| 9 | Config deployed with the artifact | No | `web.config` transforms exist but get hand-patched on servers. |

That scorecard was *useful* and I'd do it again. Where it didn't fit:

- **Part 2 baseline metrics** assume per-service deploy frequency. My monolith is *one* deploy event for dozens of features bundled together. "Deploys to prod / week" is ~0.5, but that number hides that each deploy is enormous — the metric undersells my actual problem. The exercise doesn't prompt me to capture batch size *per deploy*, which is the number that would actually shock my management.
- **Part 4 "Pick your pilot"** (lines 70-77) explicitly says "ideally a new AWS one." So the exercise's own advice is: *don't pilot on the monolith.* Which is pragmatic, but it means the single most-prescribed action of the whole course steers me away from the system the course claims it applies to. If I follow the instructions literally, the monolith never gets touched.

#### Decompose a branch (`exercises/decompose-a-branch.md`)

This is where the course and I parted ways. The exercise *hands me a scenario* — build a new HOA Violations Lambda (line 13) — instead of letting me bring my own. The instructions say "use a current backlog item if you have one" (line 87) in the Output section, so I tried, in character, with a real monolith change:

> **My change:** Add a "delinquent assessment" flag to the homeowner-ledger endpoint. Requires (a) a new `IsDelinquent` computed column + an index on the `Assessments` table, (b) a change to the `usp_GetHomeownerLedger` stored procedure, (c) a new field on the API response DTO, (d) a new query path in the data-access layer.

Here's my decomposition attempt and where it got hard:

1. **Add `IsDelinquent` column to `Assessments`, nullable, no proc reads it yet.** *(Expand step. Backward-compatible — but this is a schema change to a shared SQL Server that the monolith AND three other internal apps read. "Independently shippable" assumes I own the database. I don't; it's shared.)*
2. **Backfill the column.** *(A data migration. The exercise's slice model assumes each slice is code that ships through a pipeline. A backfill is a long-running data job, not a deploy. Where does it go? The exercise has no concept for it.)*
3. **New version of `usp_GetHomeownerLedger` that populates the column.** *(Stored procs are the hard part. A proc isn't behind a feature flag — it's a shared DB object. If I `ALTER` it, every caller gets the new behavior at once. I can make `usp_GetHomeownerLedger_v2` and route the new code to it, but now I'm versioning stored procs by hand, which the course never mentions.)*
4. **Data-access layer reads the new column, behind a flag.** *(OK — but "behind a flag" in my IIS app means a `web.config` appSetting, and flipping it means... an app-pool recycle? Per-VM config drift across the farm? The `feature-flag.ts` example gave me zero help here.)*
5. **DTO exposes the field, behind the same flag.** *(Backward-compatible additive field — this slice is genuinely fine and the course's logic holds.)*
6. **Flip the flag on per environment.** *(Fine in principle; mechanically unspecified for IIS.)*

**Where it got hard or felt impossible:**

- **Steps 1-3 are the whole ballgame and the course barely has a vocabulary for them.** The "what good looks like" model decomposition (exercise lines 69-81) treats the DynamoDB table as slice 2 ("infra only, unused") and never revisits data. A DynamoDB table is schemaless and per-service; adding one breaks nobody. A *column + stored proc on a shared SQL Server* is the opposite: it's coupled, it's shared across consumers, and `ALTER PROC` has no "off" switch. The exercise's tidiest assumption — that schema changes are easy additive infra slices — is true for DynamoDB and false for my world.
- **Expand/contract on stored procs is unaddressed.** Pressure-test prompt "Where's the database change?" (exercise line 50) gives the DynamoDB four-step recipe. Nobody tells me whether to version procs, dual-write from a proc, or how to contract a proc that four apps call. This is genuinely the hardest part of decomposing monolith work and it's a one-liner.
- **The "feature flag hides the slice" model leaks for shared DB objects.** A flag hides *my* code path. It cannot hide an `ALTER`ed stored procedure from the other three apps that call it. The course's core safety mechanism — flag-it-off — has a blind spot exactly where my coupling lives, and the course doesn't acknowledge that the mechanism has a boundary.

I *could* ship this change incrementally. The discipline is real and I believe it. But I had to invent the monolith-specific moves myself, and on the hardest move (shared stored procs) I'm still not sure the course's model holds.

## Where it lost me / objections it didn't answer

1. **"Applies everywhere" is asserted, never demonstrated for my world.** Count the worked artifacts: `cd-vs-continuous-deployment.md` (Lambda), `current-state-pipeline-walkthrough.md` (CloudFormation), `branching-antipatterns.md` (neutral, credit), `feature-flag.ts` (Lambda), `ci-pipeline.gitlab-ci.yml` (SAM), all of Session 3's `violations-api/` (Lambda/SAM), `rollback-on-aws.md` (4/5 strategies AWS), and both exercises (the new Lambda). Of ~10 worked artifacts, **zero** are .NET/VM/SQL. The claim that this is platform-independent is true *in principle* and unproven *in practice*. A skeptic doesn't convert on principle; they convert on one example in their own stack. There isn't one.

2. **The strangler-fig pattern is name-dropped, never shown.** It appears in `README.md` line 10, `CLAUDE.md`, and `sessions/session-1/README.md` line 27 — always as "we gradually carve functionality out of the monoliths." That is the *one* idea that would directly connect my monolith to the AWS work, and it gets *zero* worked treatment. How does CD interact with a strangler migration? When I carve an endpoint out of my .NET monolith into a Lambda, who owns the shared SQL table during the transition? How do I deploy the monolith and the new service in lockstep, or decouple them? This is the most important question for my actual future and the course raises it three times and answers it never. If the course wants the monolith crowd, *this* is the bridge, and it's unbuilt.

3. **The course quietly tells me not to bother with the monolith.** Session 1 §1 says practice on greenfield. The assessment's Part 4 says pilot a new AWS service. That's defensible advice — but it contradicts the "applies everywhere" thesis. Either the monolith is in scope (then show me how) or it isn't (then stop telling me it is). Right now I get the rhetorical inclusion without the practical inclusion, which is the most annoying possible combination.

4. **Stateful, shared databases are treated as a footnote.** My single biggest CD obstacle is the shared SQL Server I cannot "promote" and cannot roll back. The course's data story is: expand/contract (4 bullets, DynamoDB), the rollback data trap (one paragraph, DynamoDB), and "config not data" reminders. A stateless-service author wrote this. The hardest thing in *my* migration is the thing the course is thinnest on.

## Confusing or assumed (clarity)

- **"Promote the same artifact" assumes I know my artifact format.** The course explains SAM packaging in detail and never says the general principle: *the artifact is whatever your platform's deployable unit is* — a web-deploy package, an MSI, a container, a Lambda zip. A .NET dev reading Session 3 §4 will think promotion is a `sam package` feature, not a universal one. One sentence generalizing the artifact concept would fix this.
- **"Config travels with the artifact" vs. my world.** `web.config` transforms and `setParameters.xml` *are* this pattern, but the course only shows `configuration/{env}.config`. I had to recognize the equivalence; a junior .NET dev won't.
- **OIDC is presented as *the* requirement, not *an instance* of "no static creds."** `migration-checklist.md` line 48 reads as a hard requirement. The actual minimum is no long-lived secrets in the deploy path. My Azure DevOps managed-identity service connection satisfies the *minimum* but fails the *letter* of the checklist. The course conflates the principle with its AWS implementation.

## Factual / technical concerns

I'm a .NET/SQL person, not an AWS expert, so I'll keep this narrow to things I can actually judge:

- **The DB/rollback claims are accurate but scoped to a forgiving model.** "Rolling code back does not roll data back" (`rollback-on-aws.md` line 39) is correct and important. But the implicit assumption throughout — that a service owns its datastore and a schema change is additive and isolated — is the *easy* case. In a shared-SQL monolith the same advice is true but radically harder to apply, and the course presents the easy case as if it's the general case. Not wrong; misleadingly comfortable.
- **`feature-flag.ts` resolves flags once at cold start** (lines 61-69). Fine for Lambda. If a reader copies that mental model into a long-running IIS process, "resolve once at startup" means a flag flip requires an app-pool recycle — a meaningfully different operational story the course never flags. The "graduating to a managed service" note (lines 104-114) helps, but it's still AWS AppConfig / LaunchDarkly, not anything in my stack.
- No AWS factual errors I can spot from my seat — the SAM/OIDC/canary mechanics read as coherent. I'll leave fact-checking those to someone who lives in them.

## Does this apply to the monolith?

This is the section I came to write. Going minimum by minimum, honestly:

| Practice / idea | Applies to my VM monolith? | Shown for it? |
| --------------- | -------------------------- | ------------- |
| Small batches, deploy more often | **Yes, fully.** My #1 takeaway. | Yes (prose, platform-neutral). |
| Trunk-based dev, short branches | **Yes, fully.** Nothing VM-specific blocks it. | Yes (prose), but examples are neutral-to-AWS. |
| Decouple deploy/release via flags | **Yes — conceptually.** | **No.** The only worked flag is a Lambda. My IIS/`web.config`/recycle story is unaddressed. |
| Pipeline is the only path to deploy | **Yes.** I can enforce this on Azure DevOps → IIS. | No (only OIDC/GitLab shown). |
| Pipeline decides releasability | **Yes.** Kill the CAB sign-off, keep a timing gate. | Neutral-ish; the example is AWS. |
| Automated definition of deployable | **Yes.** MSBuild + MSTest + coverage + scan. | No (lint/vitest/sam only). |
| Immutable artifact, build once promote | **Yes — web-deploy package is exactly this.** | **No, and this hurts.** Only `sam package` shown; I had to build the mapping. |
| Production-like environment | **Yes, but far harder for me.** Stateful, shared SQL, LB/session affinity. | No — treated as a config-file one-liner. |
| Rollback on demand | **Partially, and it's my hardest gap.** No alias to repoint; redeploy prior web-deploy package, slot swap, or 2-app-pool blue-green. | **No.** 4/5 strategies are AWS; IIS gets nothing. |
| Config with the artifact | **Yes — transforms / `setParameters.xml`.** | No (only `{env}.config` shown). |
| Expand/contract for schema | **Yes, but brutal on shared SQL + stored procs.** | **Barely** — 4 bullets, DynamoDB, no proc story. |
| Strangler-fig (monolith → services) | **This is my actual roadmap.** | **No.** Named 3×, never worked. |

**Verdict on the question:** The *discipline* applies to my monolith almost entirely — and that genuinely surprised me, in a good way. The course's *materials* support that translation almost not at all. Every place my world is *harder* than the cloud-native case (stateful shared DB, stored procs, IIS rollback, flags in a long-running process, strangler coupling) is precisely where the course is thinnest or silent. So: yes, it applies — but I had to prove that to myself, against the grain of the examples, and on the two hardest points (shared stored procs, IIS rollback) I'm still not fully convinced the model holds. A monolith owner less stubborn than me would have concluded "not for us" by Session 2.

## Recommendations

### High priority

1. **Add one .NET-on-VM worked thread that parallels the Lambda one.** It doesn't need to be every example — but the README promises this estate is in scope "for a long time," so prove it once: a web-deploy package built once and promoted to dev/qa/prod IIS with `setParameters.xml` (immutable artifact + config-with-artifact), an Azure DevOps pipeline as the single path (managed identity, not static creds), and an IIS rollback (redeploy prior package / slot swap / 2-app-pool blue-green). This single addition would move me from "comply" to "champion."
2. **Work the strangler-fig pattern, don't just name it.** Show one endpoint being carved from the .NET monolith into a Lambda *under CD*: who owns the shared table during transition, how the two deploy without lockstep, how a flag routes traffic between old and new. This is the literal bridge between the two audiences the course is trying to serve, and it's missing.
3. **Give the shared-relational-DB / stored-proc case real treatment.** Expand/contract on SQL Server with stored procs and multiple consumers deserves its own worked example, not four DynamoDB bullets. Address: versioning vs. `ALTER`ing procs, dual-write, backfills as non-deploy steps, and the fact that a feature flag *cannot* hide an `ALTER PROC` from other callers.

### Medium priority

4. **Generalize the principles above their AWS instances.** For each minimum, state the platform-neutral rule first, then "on AWS this looks like X, on .NET/IIS like Y." The "How this maps to RealManage" table (`minimums-reference.md` lines 74-85) should have a .NET/VM column.
5. **Fix the flag section for long-running processes.** Add a paragraph (or example) on flags in an IIS/.NET app: `IFeatureManager` / Azure App Configuration, the config-reload-vs-recycle gotcha, and per-server config drift across a VM farm.
6. **Reconcile the framing.** Either change the README goal line (line 6) and target audience to include the established estate explicitly, or stop saying "applies everywhere." Pick one and be consistent; right now the rhetoric and the materials disagree.

### Nice to have

7. **Let the decompose exercise default to the learner's own system,** with the Lambda scenario as a fallback — not the reverse. Add a stored-proc/shared-DB decomposition to the "what good looks like" gallery alongside the DynamoDB one.
8. **Capture batch-size-per-deploy in the assessment (Part 2),** not just deploy frequency. For a monolith, frequency hides the real problem; batch size reveals it.
9. **Reconsider the Part 4 pilot advice.** "Ideally a new AWS one" is pragmatic but, stated baldly, tells the monolith crowd the course isn't for them. At least add: "or one bounded slice of an established service."

## Verdict

**Comply (leaning toward champion if the high-priority gaps close).** I'm convinced the *discipline* is real and applies to my monolith — that's a genuine shift from where I started, and Sessions 1-2's arguments earned it. But the course made me do all the translation myself and went silent at exactly my three hardest points (shared SQL/stored procs, IIS rollback, strangler-fig). As written, it would lose most monolith owners before they got far enough to discover the practices do transfer. Show me *one* worked example in my own stack — especially the strangler-fig bridge — and I'll carry this to my team. Until then I'll adopt the parts I had to translate myself, and quietly assume the rest was written for the cloud-native kids.
