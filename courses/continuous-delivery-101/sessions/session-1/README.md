# Session 1: Why Continuous Delivery & the Minimums

**Duration:** 2 hours
**Format:** Interactive workshop
**Prerequisites:** Git familiarity; you deploy *something* today, however painfully

## 🎯 Learning Objectives

By the end of this session, participants will be able to:

- ✅ Explain why infrequent, big-batch deployment is self-reinforcing and risky
- ✅ Distinguish Continuous Delivery from Continuous Deployment
- ✅ List the MinimumCD practices for CI and CD and say *why* each exists
- ✅ Map CD practices onto how we build today — established .NET APIs alongside new cloud-native services
- ✅ Score our current `iac-baseline` pipeline against the minimums and name the gaps

## 📚 Session Agenda

### 1. Welcome & Framing (10 minutes)

#### The landscape today

RealManage runs a mix, and will for a long time:

- **Established .NET Framework Web APIs on Azure VMs** — large, long-lived, and not going anywhere soon
- **New, small, cloud-native services on AWS** — Lambda (TypeScript), ECS, DynamoDB, SNS, SQS — built cloud-native where possible
- We use the **strangler fig pattern** to gradually carve functionality out of the monoliths into these new services — incrementally, not a big-bang rewrite (worked end to end in [Strangler Fig in Practice](../session-3/examples/strangler-fig-violations.md) — the Violations API you'll meet in Session 3 is the *result* of this migration)

#### What this course changes

This course is about *how we deliver*, not which platform we're on. Across the estate today, delivery tends to look like:

- Releases on a **weekly** cadence
- **Long-lived feature branches** kept alive until "the feature is done"
- Release day treated as an event to brace for

Continuous Delivery moves that to small batches integrated daily, an always-deployable trunk, and the ability to release *any* change *any* day, safely, with low drama.

Note who owns quality in this picture: at RealManage there is no separate QA team or QA gate — the team that builds a change owns its quality, and the pipeline is where that ownership becomes automated, enforceable checks rather than a handoff to someone else.

> The new cloud-native services are an easy *place to start* — they carry no legacy deploy process to fight. But CD is not something you do only on the new services: it is a set of working agreements that apply to the monolith too. Trunk-based development, feature flags (via config, not just Lambda env vars), expand/contract on SQL Server, and build-once-promote all work on an IIS-on-VMs .NET app shipped through the same GitLab CI. We start where the practices are easiest to *see*, then carry them straight back to the estate that needs them most — including the strangler-fig migrations that carve new services out of the monolith ([worked end to end in Session 3](../session-3/examples/strangler-fig-violations.md)).

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

> **In fairness to the weekly release:** the cadence wasn't irrational — it gave you a predictable window, a natural batching point for client communication, and a forcing function for "is it ready?" CD has to *replace* each of those (continuous small releases, comms anchored to the flag flip, an automated definition of deployable), not just delete them. You're trading a familiar ritual for better mechanisms — and that trade has a real, front-loaded cost. See [what CD costs you](../../resources/what-cd-costs.md).

#### 2.2 Batch size is the master variable (10 minutes)

Almost every CD benefit traces back to shrinking the batch:

| Big batch (weekly branch) | Small batch (daily trunk) |
| ------------------------- | ------------------------- |
| Merge conflicts pile up | Few conflicts; trunk barely drifts |
| Hard to review (1000s of lines) | Easy to review (tens of lines) |
| Failure is hard to isolate | Failure points at one change |
| Rollback reverts everything | Rollback reverts one thing |
| Release is a risky event | Release is a non-event |

> **When an agent writes the code, this is the practice that matters most.** A human author self-limits by friction — 1,200 lines is a day's grind. An agent emits them before lunch, so the friction that used to cap batch size is gone. Small batches stop being a habit you adopt to go faster and become a *throttle you impose* to keep machine output from drowning the trunk and your reviewers. See [CD when AI writes the code](../../resources/ai-assisted-delivery.md).

#### 2.3 The evidence (5 minutes)

The [DORA research](https://dora.dev/) (*Accelerate*) found that teams who deploy frequently in small batches have **both** higher throughput **and** higher stability — they are not a trade-off. The four key metrics:

- **Deployment frequency** — how often you ship to prod
- **Lead time for changes** — commit → prod
- **Change failure rate** — % of deploys needing remediation
- **Time to restore** — how fast you recover

CD moves all four in the right direction at once.

---

### 3. CD vs Continuous Deployment (15 minutes)

These get confused constantly. The distinction matters.

- **Continuous Delivery** — every change is *kept deployable* and *can* be released at any time. A human decides *when* to release. **This is our target.**
- **Continuous Deployment** — every change that passes the pipeline is released to production *automatically*, no human gate. An optional step *beyond* CD.

```text
commit → CI → build → test → [DEPLOYABLE] → (human says "go") → prod   ← Continuous Delivery
commit → CI → build → test → [DEPLOYABLE] ───────automatic───────→ prod ← Continuous Deployment
```

You must achieve Continuous *Delivery* first. Continuous *Deployment* is a choice you can make later, per service, once you trust the pipeline. Worked comparison: [`examples/cd-vs-continuous-deployment.md`](./examples/cd-vs-continuous-deployment.md).

> **Engineering Lead note:** "Continuous Delivery" does not mean losing control. It means you *gain* the ability to release on your schedule instead of being trapped in a weekly window. Releasing stays a deliberate decision.

---

### 4. The MinimumCD Practices (35 minutes)

This course is built on [minimumcd.org](https://minimumcd.org): the industry's *minimum* bar for CD. "Minimum" matters — these aren't stretch goals, they're the floor. Full reference: [`resources/minimums-reference.md`](../../resources/minimums-reference.md).

#### 4.1 Continuous Integration minimums (15 minutes)

CI is **each developer integrating to the trunk at least daily, verifying the work is releasable.** A team discipline — *not* a server.

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

### 5. Workshop: Score Our Current Pipeline (25 minutes)

We don't learn CD against a strawman — we learn it against *our own* real pipeline.

#### 5.1 Walk the baseline (10 minutes)

Read [`examples/current-state-pipeline-walkthrough.md`](./examples/current-state-pipeline-walkthrough.md): an annotated tour of the **real** `iac-baseline` GitLab pipeline (`validate → build → dev → qa → prod`, OIDC auth, immutable SHA-tagged images) — and where it meets the minimums versus where it falls short.

The headline finding: the baseline already does a *lot* right (single pipeline path, OIDC, immutable artifacts, config-with-artifact), but it gates **every** stage — `deploy:dev`, `deploy:qa`, `deploy:prod` — with `when: manual`. A human clicks every deploy.

#### 5.2 Score it as a team (15 minutes)

Open the [Current-State Assessment](../../exercises/current-state-assessment.md) and score *your team's* services against the minimums. Be honest. Capture:

- Which minimums you already meet
- Which are Partial (manual gates → does the *pipeline* decide releasability, or the human?)
- The single biggest constraint for your team (hint: for most, it's branch lifetime)
- A pilot service and team

---

### 6. Wrap-up & Homework (10 minutes)

#### Key takeaways

- Big batches *cause* deployment risk; small, frequent deploys reduce it
- CD ≠ Continuous Deployment — CD keeps changes deployable; a human still decides *when*
- The MinimumCD practices are the floor, not the ceiling
- Our `iac-baseline` already nails several minimums; the main gap is manual gates and (team-side) branch lifetime
- CD is practices, not a product

#### Homework

1. Finish the [Current-State Assessment](../../exercises/current-state-assessment.md) with real numbers (branch lifetime, MR size, deploy frequency)
2. Read [`resources/minimums-reference.md`](../../resources/minimums-reference.md) end to end
3. Pick one real feature from your backlog — you'll decompose it in Session 2

#### Preview of Session 2

Trunk-based development and CI in practice: killing long-lived branches, decomposing big work, and using feature flags to merge incomplete work to trunk safely.

## 📚 Resources for This Session

- [Minimums Reference](../../resources/minimums-reference.md)
- [Glossary](../../resources/glossary.md)
- [Migration Checklist](../../resources/migration-checklist.md) (Phase 0)
- [MinimumCD.org](https://minimumcd.org) — the source
- [DORA / Accelerate research](https://dora.dev/)

---

**Next Session:** [Session 2: Trunk-Based Development & CI →](../session-2/README.md)
