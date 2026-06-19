# Continuous Delivery When AI Writes the Code

Most of this course is **authorship-agnostic**. Small batches, an always-deployable trunk, an automated definition of deployable, build-once-promote, expand/contract, and fail-forward all work the same whether a person or an agent typed the source. This page is **not a rewrite**. It covers the handful of places where AI-assisted authorship *raises the stakes* on a practice you already have — and the one place it opens a genuinely new failure mode.

The one idea to keep: **AI removes the human friction that used to keep delivery safe by accident.** A tired human is a natural rate limiter. Remove that limiter and CD's practices stop being good habits you adopt to go faster and become the load-bearing controls that keep fast machine output from reaching production unverified.

> **The factory framing.** As more code is written by agents, the durable human job moves from turning the bolts to designing the line: deciding *what* to build and *how to slice it*, then letting the pipeline be quality control. Most of what follows is a consequence of taking that seriously.

---

## Why "automate the verdict" is now non-negotiable

The course's central rule — **the pipeline decides releasability, not a meeting** — is its most important idea for AI-assisted work. When you can't read every line because there are too many and they all look plausible, the automated definition of deployable is the *only* verdict that scales. Everything below is a consequence of taking that rule seriously once the author is fast.

---

## Three seams where AI authorship raises the stakes

### 1. The test-gaming trap (the new failure mode)

The course frames tests as the definition of deployable, and that is correct — but it quietly assumes the test is an *independent* statement of intent. When the **same agent writes the implementation and the tests** in one breath, that assumption can fail: the agent writes tests that assert whatever the code already does, hits the coverage number, goes green — and verifies nothing. Coverage thresholds make this *worse*, not better: "reach 80%" is a target an agent optimizes directly. It is Goodhart's law with a machine — when a measure becomes a target, it stops being a good measure.

What holds the line:

- **Tests must specify intent, not mirror implementation.** The course's own `sessions/session-3/examples/violations-api/src/handler.test.ts` is the model: it tests behaviour contracts and the flag-off/flag-on boundary, not the internals. A test you could keep through a full rewrite of the implementation is a real test; a test that breaks on any refactor is just mirroring the code.
- **Specify the behaviour before the code.** Write the acceptance criteria the change must satisfy *first*, then have a human — or a *separate* agent that did not write the code — check the tests against those criteria. The reviewer's question is: "do these tests encode what we agreed, or what the code happens to do?"
- **Never read "coverage met" as "behaviour verified."** Keep the coverage floor as a smoke alarm for wholly untested paths; do not let it stand in for the judgement that the tests pin real intent.

### 2. Review when you can't read every line

The governance story (see [Governance & Compliance](governance-and-compliance.md)) puts MR review at the centre: author ≠ approver, a second person approves, the pipeline deploys. That control is still **enforceable** under AI — reviewer ≠ committer is a tool setting, not a hope. What erodes is the **substance**: a human rubber-stamping 1,200 plausible machine-written lines is segregation of duties in form only.

The CD-native answer: review shifts from line-reading to **spec-checking and gate-trusting**. You review the *intent* (does this change do what we agreed?) and the *tests-as-spec* (do they pin that intent?), and you trust the pipeline for the mechanical rest — lint, types, the definition of deployable. The provenance chain the pipeline builds — commit → run → SHA-tagged artifact → deploy — is also your accountability record: when there may be no single human author to interrogate, "who is responsible for this code?" is answered by *the reviewer who approved it plus the pipeline evidence that gated it*. Keep MRs small (next seam) so that review can stay substantive instead of becoming a rubber stamp.

### 3. Flag explosion is the default, not the exception

Feature flags are the right tool for merging an agent's incomplete work to trunk safely — but an agent told to "ship this behind flags" will mint a flag per slice, per branch, per experiment, with no instinct that the inventory is becoming unmanageable. The course already has the cure (see [what CD costs](what-cd-costs.md) and `sessions/session-2/examples/feature-flag.ts`): a flag inventory, a birth certificate (owner + creation date + removal condition), a CI stale-flag check, and "delete the flag" as the last slice of the work. Two additions for AI-paced work:

- **Tune the stale-flag check tighter.** Machines create flags faster, so the inventory rots faster; shorten the expiry the check enforces.
- **A flag explosion is still a *decomposition* failure** — and decomposition is the human's job. If the agent produced four interacting flags for one feature, the slices were wrong. Re-slice. That is design-the-line work, not bolt-turning.

---

## Said once: batch size is now containment, not just virtue

The course teaches small batches as the master variable — the discipline that makes everything else easier. A human author self-limits by friction: 1,200 lines is a day's grind, so batches stay human-sized by accident. An agent emits 1,200 plausible lines before lunch. **The friction that used to cap batch size is gone.** So small batches and the under-a-day branch rule stop being habits you adopt to go faster and become **throttles you impose** to keep the firehose from drowning your trunk and your reviewers. Same practice; higher stakes.

---

## Metrics that still mean something

All four DORA metrics still matter, but AI skews which ones are *informative*. Deployment frequency and lead time get cheap to inflate — an agent ships faster, so those rise trivially. **Change failure rate and time to restore stay honest**, because they measure whether the fast-written code actually worked. In an AI-assisted org, watch the stability metrics; treat the speed metrics as table stakes, not achievements.

---

## One addition to the definition of deployable: dependency provenance

The course treats `npm audit` as advisory, which is right. AI-assisted work adds a risk the base course does not name: agents routinely propose plausible-but-wrong or even **non-existent** dependencies, and a hallucinated package name becomes a supply-chain hole the moment someone registers it. Add a **lockfile-integrity / dependency-provenance** check to your definition of deployable — verify that every dependency resolves to a known, pinned, expected source — once an agent is writing your `package.json`.

---

## What doesn't change (the honest accounting)

Most of CD is genuinely authorship-agnostic. These are unchanged whether a person or an agent wrote the code:

- **Build-once-promote and immutable artifacts** — provenance is, if anything, *more* valuable when there is no human author to ask.
- **Expand/contract** — a schema-safety discipline the agent executes once a human names the steps.
- **OIDC / no standing credentials, production-like qa, config-with-the-artifact** — entirely unaffected.
- **Fail-forward-first** — *more* clearly right, because an agent produces the forward fix in minutes. The one caveat: the forward fix needs the **same gates** as any change. The temptation under pressure is to let the agent hot-fix straight to prod because it is fast — that is exactly the side door the "pipeline is the only path" rule forbids, and AI makes the temptation stronger.

CD did not need a rewrite for the age of AI-written code. It needed three of its practices promoted from "good discipline" to "the controls that keep machine output safe" — and one new check, **test independence**, that it never had to think about when a tired human was the only author.
