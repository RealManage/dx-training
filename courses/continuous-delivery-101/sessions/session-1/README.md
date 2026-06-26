# Session 1: Why Continuous Delivery & the Minimums

**Duration:** 2 hours
**Format:** Interactive workshop
**Prerequisites:** Git familiarity; you deploy *something* today, however painfully

> 🖥️ **Presenting?** Open the [Session 1 slide deck](../../slides/session-1.html) — press `S` for speaker notes. The deck is built from [`slides/session-1-outline.md`](../../slides/session-1-outline.md).

## 🎯 Learning Objectives

By the end of this session, participants will be able to:

- ✅ Trace the CD practices back to their roots in Lean manufacturing and the quality movement, and to the values and principles they serve
- ✅ Explain why infrequent, big-batch deployment is self-reinforcing and risky
- ✅ Distinguish Continuous Delivery from Continuous Deployment
- ✅ List the MinimumCD practices for CI and CD and say *why* each exists
- ✅ Map CD practices onto how we build today — established .NET APIs alongside new cloud-native services
- ✅ Map our delivery value stream, compute its flow efficiency, and name the one constraint most worth fixing

## 📚 Session Agenda

### 1. Welcome & Framing (15 minutes)

#### The landscape today

RealManage runs a mix, by design:

- **Established .NET Framework Web APIs on Azure VMs** — themselves a recent rewrite off an older platform; they run much of the business today and are core to the estate
- **New, small, cloud-native services on AWS** — Lambda (TypeScript), ECS, DynamoDB, SNS, SQS — built cloud-native where possible
- We use the **strangler fig pattern** to gradually carve functionality out of the monoliths into these new services — incrementally, not a big-bang rewrite (worked end to end in [Strangler Fig in Practice](../session-3/examples/strangler-fig-violations.md) — the Violations API you'll meet in Session 3 is the *result* of this migration)

This is a story of *continuous improvement*, not a bail-out. Those .NET APIs are themselves a recent rewrite off an older platform — real progress the team already delivered — and CD is the next increment of that same journey, applied across the whole estate rather than reserved for the new services.

#### What this course changes

This course is about *how we deliver*, not which platform we're on. Across the estate today, delivery tends to look like:

- Releases on a **weekly** cadence
- **Long-lived feature branches** kept alive until "the feature is done"
- Release day treated as an event to brace for

Continuous Delivery moves that to small batches integrated daily, an always-deployable **trunk** (the shared `main` line everyone integrates into), and the ability to release *any* change *any* day, safely, with low drama.

Note who owns quality in this picture: at RealManage there is no separate QA team or QA gate — the team that builds a change owns its quality, and the pipeline is where that ownership becomes automated, enforceable checks rather than a handoff to someone else.

What if there are no automated checks yet? Much of the established estate is verified by hand today — and that does not block CD. Owning quality can start as the delivering engineer testing their own change; a "throw it over to QA" *wait* is the waste, not the act of verifying your own work. The path from manual to automated — test new code, add characterization tests to legacy code as you touch it, and let feature flags keep you integrating meanwhile — is its own guide: [Testing and CD](../../resources/testing-and-cd.md).

> The new cloud-native services are an easy *place to start* — they begin from a clean slate, with no existing deploy process to retrofit. But CD is not something you do only on the new services: it is a set of working agreements that apply to the established .NET systems too. Trunk-based development, feature flags (via config, not just Lambda env vars), expand/contract on SQL Server, and build-once-promote all work on an IIS-on-VMs .NET app shipped through the same GitLab CI. We start where the practices are easiest to *see*, then carry them straight back to the systems that run the business — where the same discipline pays off just as much, including the strangler-fig work that selectively carves new services out where that serves the product ([worked end to end in Session 3](../session-3/examples/strangler-fig-violations.md)).

#### The roots: from the factory floor to the trunk

Almost nothing in this course was invented in software. The values below come from **Lean manufacturing** — the Toyota Production System, refined on factory floors since the 1950s — and from the **quality movement** that rebuilt postwar industry. The software industry relearned them the hard way and wrote them down as Continuous Delivery.

Everything in this course serves one idea:

> **Continuously shrink the distance — in time *and* in risk — between making a change and getting it safely into users' hands.**

**Three beliefs (the values).**

1. **Undeployed code is inventory, not value.** A change sitting on a branch is work-in-process inventory — paid for, not yet earning, quietly going stale as the trunk moves under it. Taiichi Ohno built Toyota's system on the insight that inventory is *waste* (*muda*), and that producing in large batches to feel efficient is one of the most expensive habits on the floor. Software's version: code delivers nothing until it runs for a user.
2. **Speed and safety rise together — they are not a trade-off.** The weekly-release instinct is "slow down to be careful." Over a decade of **DORA** research (*Accelerate*, 2018) found the opposite — the teams that deploy most often also have the lowest change-failure rate. Small and frequent is *easier* to get right. This is the central mindset shift — and the one most teams resist at first.
3. **You can't inspect quality in — you build it in.** W. Edwards Deming, the American statistician who helped rebuild Japanese industry, made it his third point: *"Cease dependence on inspection to achieve quality. Build quality into the product in the first place."* A separate quality gate at the end is the thing to design *out*. At RealManage that is literal — there is no QA team — so the delivering team owns quality, and the pipeline is where that ownership becomes automated checks instead of a handoff.

**Five principles — what those beliefs demand, and the practices each forces:**

| Principle | Rooted in | Practices it forces | Covered in |
| --------- | --------- | ------------------- | ---------- |
| **Work in small batches** | Lean batch-size economics (Ohno; Reinertsen) | Trunk-based development, daily integration, short-lived branches, decomposing work, frequent deploys | Big batches (next); Session 2 |
| **Get feedback fast** | Lean fast flow; the scientific method | Tests before merge, CI on every commit, a fast pipeline | CI minimums; Session 2 |
| **Build quality in** | Deming; Toyota *jidoka* | A definition of deployable, the pipeline decides releasability, immutable artifacts, one path to production | CD minimums; Session 3 |
| **Separate deploy from release** | Decouple technical risk from business risk | Feature flags, deploy dark, release on the business's schedule | CD vs Continuous Deployment; Session 2 |
| **Improve the whole, continuously** | *Kaizen*; Theory of Constraints (Goldratt) | Value-stream mapping, the four DORA signals, a phased migration, fail forward | Value-stream workshop; ongoing |

Two more practices we borrow by name — both recur later in the course:

- **Stop the line.** On a Toyota line any worker can pull the **andon cord** to halt production the moment a defect appears, because letting a defect flow downstream costs more than stopping. Our "stop the line on red" (CI minimum 5, CD minimum 6) is the same rule: a broken trunk is the whole team's top priority.
- **Go and see.** Toyota calls it *genchi genbutsu* — observe the real work where it happens instead of theorizing from a conference room. That is exactly the value-stream map you'll build in the workshop: measure where the time *actually* goes.

When a practice seems arbitrary — "why must branches be short-lived?" — trace it back to its root: practice → principle → belief. The reasoning holds up, because almost no one disputes that inventory is waste or that quality can't be inspected in.

---

### 2. The Problem with Big Batches (25 minutes)

#### 2.1 The self-reinforcing trap (10 minutes)

Why do teams deploy weekly? Usually this loop:

```text
        deploy rarely
             │
             ▼
   each deploy is large
             │
             ▼
   large deploys are risky
             │
             ▼
   risk makes us deploy even less  ──┐
             ▲                       │
             └───────────────────────┘
```

A weekly release isn't one change — it's *a week of changes* released together. When it breaks, you face:

- **Diagnosis hell:** dozens of changes shipped at once. Which one broke prod?
- **All-or-nothing rollback:** revert the bad change and you revert the other 30 too.
- **Release-day anxiety:** the bigger the batch, the scarier the button.

The only escape is counterintuitive: **deploy *more* often, in *smaller* pieces.** Ten one-change deploys a week are each trivially diagnosable and reversible.

> **In fairness to the weekly release:** the cadence wasn't irrational — it gave you a predictable window, a natural batching point for client communication, and a forcing function for "is it ready?" CD has to *replace* each of those (continuous small deploys, comms anchored to the flag flip, an automated definition of deployable), not just delete them. You're trading a familiar ritual for better mechanisms — and that trade has a real, front-loaded cost. See [what CD costs you](../../resources/what-cd-costs.md).

#### 2.2 Batch size is the master variable (10 minutes)

Almost every CD benefit traces back to shrinking the batch — but "batch" lives at **two independent hand-offs**, and lumping them together hides what actually bites:

- **Integration batch** — how much piles up on a branch before it merges to trunk. Bigger here means a **bigger review**. Merge conflicts only follow *if* your changes overlap a trunk that moved underneath you — which is why a team can rarely hit conflicts yet still drown in oversized reviews.
- **Deploy batch** — how much accumulates on trunk between production deploys. Bigger here is what makes **failure hard to isolate**, **rollback all-or-nothing**, and the **release a risky event**.

A long-lived branch inflates the first; a weekly deploy *cadence* inflates the second. They often travel together — but you can have one without the other, so shrink both.

| Big batch | Small batch |
| --------- | ----------- |
| Reviews balloon to 1000s of lines | Reviews are tens of lines |
| Conflicts pile up *if* work overlaps a drifting trunk | Trunk barely drifts |
| Failure is hard to isolate | Failure points at one change |
| Rollback reverts everything | Rollback reverts one thing |
| Release is a risky event | Release is a non-event |

**See it live.** Open the [Batch Size visualizer](./examples/batch-size.tool.html) and drag the deploy-frequency slider — batch size, rollback blast radius, and the number of suspects on a failure all move together. That single number *is* your blast radius.

> **When an agent writes the code, this is the practice that matters most.** A human author self-limits by friction — 1,200 lines is a day's grind. An agent emits them before lunch, so the friction that used to cap batch size is gone. Small batches stop being a habit you adopt to go faster and become a *throttle you impose* to keep machine output from drowning the trunk and your reviewers. See [CD when AI writes the code](../../resources/ai-assisted-delivery.md).
>
> **The elephant: the database.** The hardest batch to shrink is a schema change. Teams accept tiny code deploys and then bundle a quarter's worth of database changes into one dreaded release, because "the database can't be done incrementally." It can — schema and baseline data delivered as code, through the pipeline, in small backward-compatible steps. We return to this in [Database Delivery](../../resources/database-delivery.md); name it now as the constraint it usually is.

#### 2.3 The evidence (5 minutes)

The [DORA research](https://dora.dev/) (*Accelerate*) found that teams who deploy frequently in small batches have **both** higher throughput **and** higher stability — they are not a trade-off. (The finding comes from years of large-scale industry *survey* data — correlational and self-reported, not a controlled experiment — but the signal is strong, consistent year over year, and now widely replicated.) The four key metrics:

- **Deployment frequency** — how often you ship to prod
- **Lead time for changes** — commit → prod
- **Change failure rate** — % of deploys needing remediation
- **Time to restore** — how fast you recover

CD moves all four in the right direction at once.

> **Benchmark yourself.** DORA's official [Quick Check](https://dora.dev/quickcheck/?v=2025) asks a handful of questions about these four metrics and places you against this year's industry cohorts. It's a directional snapshot, not a grade (same survey caveats as above) — but it answers "where do we stand?" in about two minutes. Landing better than average isn't a reason to coast: it's evidence the improvements already made are working, with a higher tier still ahead. Re-run it after adopting these practices, and the climb between runs is your continuous improvement made visible — against the industry, not just your own past.

---

### 3. CD vs Continuous Deployment (15 minutes)

These get confused constantly. The distinction matters.

- **Continuous Delivery** — every change is *kept deployable* and *can* be deployed at any time. A human decides *when*. **This is our target.**
- **Continuous Deployment** — every change that passes the pipeline is deployed to production *automatically*, no human gate. An optional step *beyond* CD.

```text
commit → CI → build → test → [DEPLOYABLE] → (human says "go") → prod   ← Continuous Delivery
commit → CI → build → test → [DEPLOYABLE] ───────automatic───────→ prod ← Continuous Deployment
```

You must achieve Continuous *Delivery* first. Continuous *Deployment* is a choice you can make later, per service, once you trust the pipeline. Worked comparison: [`examples/cd-vs-continuous-deployment.md`](./examples/cd-vs-continuous-deployment.md).

> **Engineering Lead note:** "Continuous Delivery" does not mean losing control. It means you *gain* the ability to deploy on your schedule instead of being trapped in a weekly window. Releasing stays a deliberate decision.

---

### 4. The MinimumCD Practices (35 minutes)

This course is built on [minimumcd.org](https://minimumcd.org): the industry's *minimum* bar for CD. "Minimum" matters — these aren't stretch goals, they're the floor. Full reference: [`resources/minimums-reference.md`](../../resources/minimums-reference.md).

#### 4.1 Continuous Integration minimums (15 minutes)

CI is **each developer integrating to the trunk at least daily, verifying the work is deployable.** A team discipline — *not* a server.

1. **Trunk-based development** — all work integrates to one shared trunk
2. **Integrate to trunk at least daily** — every developer, every day
3. **Automated tests before merge** — the change proves itself first
4. **Automated tests on the merged result** — the combined work is verified
5. **Stop the line on red** — a broken build is the team's top priority
6. **New work doesn't break delivered work** — backward-compatible by default

> **"We have GitLab CI, so we do CI" — no.** A pipeline tool is not the discipline. If branches live for a week, you have a build server, not Continuous Integration.

#### 4.2 Continuous Delivery minimums (15 minutes)

1. **Use CI** — CD is built on it
2. **The pipeline is the only way to deploy** to any shared environment
3. **The pipeline decides releasability** — its verdict is definitive
4. **Artifacts meet a definition of deployable** — automated criteria, not a meeting
5. **Artifacts are immutable** — built once, never hand-edited
6. **Stop the line on a red pipeline** — same rule, extended to delivery
7. **A production-like test environment exists**
8. **Rollback is on demand**
9. **Config deploys with the artifact**

#### 4.3 Quick check (5 minutes)

The litmus test: *Could a small change committed right now reach production today, through the pipeline, with no human re-typing commands and no meeting to decide if it's "ready"?* Hold that question for the rest of the session.

---

### 5. Workshop: Map Your Value Stream (25 minutes)

Before we fix anything, we look honestly at where the time actually goes. A **value stream map** traces a change from *idea* to *running in production* and measures each step, so the biggest constraint surfaces from the numbers instead of from whoever argues loudest. It's platform-agnostic — it maps *your* flow, whether you ship a Lambda or a .NET app to IIS.

Run the [Map Your Value Stream](./exercises/value-stream-map.md) exercise as a team.

#### 5.1 Learn the method + walk the example (10 minutes)

For every step a change passes through, capture three numbers: **process time** (actively working), **wait time** (sitting idle — queues, approvals, the release window), and **%C/A** (the share that arrives usable, without rework). Then derive **total lead time** (process + wait) and **flow efficiency** (process ÷ lead). Map it *backward* from production so the deploy-side waits don't get forgotten.

Walk the worked weekly-release example in the exercise: it comes out at ~16 working days of lead time but only **~15% flow efficiency** — the change spends three weeks in the stream and ~2.5 days being worked. The two worst steps (the 4-day wait for the release window, and the 70%-accurate batch deploy) both point at the same fix: smaller batches, more often.

#### 5.2 Map your own stream (15 minutes)

Map *your team's* real flow from last week — not the process you wish you had. Capture:

- Every step, idea → prod, working backward from production
- Process time, wait time, and %C/A on each step
- Total lead time and flow efficiency
- The two starred steps — biggest wait, lowest %C/A — and the single binding constraint they point to

The point isn't a pretty diagram; it's the constraint. For most teams coming off weekly releases it's the batching wait — but let your own numbers say so.

> **For the AI-assisted team:** when an agent writes the code, *develop* shrinks but the waits don't — the constraint moves to the review queue and the deploy window. The map is how you catch that shift. (See [CD when AI writes the code](../../resources/ai-assisted-delivery.md).)

---

### 6. Wrap-up & Homework (10 minutes)

#### Key takeaways

- These practices are old and proven — Lean manufacturing and the quality movement, not software fashion; each serves a value (small batches, fast feedback, built-in quality)
- Big batches *cause* deployment risk; small, frequent deploys reduce it
- CD ≠ Continuous Deployment — CD keeps changes deployable; a human still decides *when*
- The MinimumCD practices are the floor, not the ceiling
- Most of your lead time is *waiting*, not working — flow efficiency exposes it, and the biggest wait is the constraint to fix first
- CD is practices, not a product

#### Homework

1. Start your [Current-State Assessment](../session-3/exercises/current-state-assessment.md): capture your real delivery numbers now — branch lifetime, MR size, deploy frequency — and score the practices you can already judge. You'll **complete** it in Session 3, once you've seen the full set of minimums and controls, where it becomes your migration-plan input.
2. Read [`resources/minimums-reference.md`](../../resources/minimums-reference.md) end to end
3. Pick one real feature from your backlog — you'll decompose it in Session 2

#### Preview of Session 2

Trunk-based development and CI in practice: killing long-lived branches, decomposing big work, and using feature flags to merge incomplete work to trunk safely.

## 📚 Resources for This Session

- [Map Your Value Stream](./exercises/value-stream-map.md) — the in-session workshop
- [Current-State Assessment](../session-3/exercises/current-state-assessment.md) — start it now; complete it in Session 3
- [Minimums Reference](../../resources/minimums-reference.md)
- [Glossary](../../resources/glossary.md)
- [Migration Checklist](../../resources/migration-checklist.md) (Phase 0)
- [MinimumCD.org](https://minimumcd.org) — the source
- [DORA / Accelerate research](https://dora.dev/)
- [DORA Quick Check](https://dora.dev/quickcheck/?v=2025) — benchmark your four metrics against this year's cohorts
- [Database Delivery](../../resources/database-delivery.md) — why the database is the elephant blocking small batches, and the shape of the fix

---

**Next Session:** [Session 2: Trunk-Based Development & CI →](../session-2/README.md)
