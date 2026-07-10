# Exercise: Flag it, Abstract it, or Ship it?

**When:** Session 2 (in-session workshop)
**Format:** Individual then small-group, ~25 minutes
**Goal:** For a series of real changes, choose the *cheapest mechanism that still keeps trunk deployable* — a feature flag, branch by abstraction, or just shipping it — and be able to say *why not the other two*.

A feature flag is powerful, but it is not free. Every flag is a branch in your code, a config to manage, and a line item of debt until you delete it. Reach for one when nothing cheaper keeps trunk deployable — and *don't* when something cheaper does. Under-reaching keeps branches long; over-reaching buries the codebase in dead toggles. The judgment between them is the skill.

> **This is the durable human skill.** As AI writes more of the code, an agent can implement any of these three mechanisms — the flag, the seam, the plain change. Deciding *which one the change calls for* is the part that stays yours. See [CD when AI writes the code](../../../resources/ai-assisted-delivery.md).

This exercise is the hands-on payoff of [§3.2 — "flags aren't the only technique"](../README.md). You'll also *decompose* a feature into slices in this session's [homework](../README.md#homework); today is about the mechanism, not the slicing.

**Walk it in the browser.** Use the [Flag-it Decision Walker](./feature-flag-decision.tool.html) to step each scenario through the three gates, land on a verdict, and check it against the intended answer with the reasoning. It keeps a scorecard and exports your calls as Markdown. Works offline; autosaves in your browser.

---

## The decision tree

Three gates, asked in order. Stop at the first that fires.

```text
                        ┌─────────────────────────────────────────────┐
   a new change ──────▶ │ GATE 1 — Ship-it test                        │
                        │ Can it merge to trunk today, FINISHED,       │
                        │ backward-compatible, exposing nothing        │──yes──▶ ✅ JUST SHIP IT
                        │ half-built or risky?                         │
                        └───────────────────┬─────────────────────────┘
                                            no
                        ┌───────────────────▼─────────────────────────┐
                        │ GATE 2 — Structural or behavioral?           │
                        │ Is what's-not-ready a STRUCTURAL swap —      │
                        │ replacing an implementation / dependency /   │──yes──▶ 🔀 BRANCH BY ABSTRACTION
                        │ data layer, no intended user-visible change, │
                        │ old & new can't both live at one call site?  │
                        └───────────────────┬─────────────────────────┘
                                            no
                        ┌───────────────────▼─────────────────────────┐
                        │ GATE 3 — Otherwise                           │
                        │ User-facing behavior you must integrate      │
                        │ before it's ready to reveal — or you need    │──yes──▶ 🚩 FEATURE FLAG
                        │ staged rollout, a kill switch, or targeting. │
                        └─────────────────────────────────────────────┘
```

**The vocabulary you're applying:**

- **Deploy ≠ release.** *Deploy* puts code in an environment (technical); *release* exposes it to users (business). A flag is what decouples them.
- **Seam.** The interface branch-by-abstraction puts *over* the thing you're replacing, so the old and new implementations can coexist while you build. A flag gates a *behavior* at a call site; a seam gates an *implementation* behind an interface.
- **YAGNI.** A flag "just in case" is not caution, it's debt. If the change is finished and safe, ship it.
- **Flags aren't all temporary.** A *release* flag is scaffolding you delete in weeks. A *kill switch* (operational) and a *permission/entitlement* toggle are permanent by design. Same verdict, different lifecycle — name which one.
- **The mechanisms compose.** Branch by abstraction and a flag are not rivals: a seam's cutover is often *itself* flag-gated for a gradual, reversible flip.

---

## Part 1 — Walk one together (facilitator-led, ~5 min)

Do **Scenario 2** as a group, out loud, against the tree:

- **Gate 1?** No — the endpoint isn't finished, and the board shouldn't see it until the meeting. Exposing it now is wrong.
- **Gate 2?** No — nothing structural is being swapped; this is new behavior at a new call site.
- **Gate 3?** Yes — user-facing behavior, integrate now, reveal on the business's schedule. → 🚩 **Feature flag** (a *release* flag: temporary, deleted once the board is live).

Notice the reasoning did the work, not the label. The point of every scenario is the *why*.

---

## Part 2 — Classify the rest (individual, then pairs, ~13 min)

**Individually (~7 min):** run scenarios **1, 3, 4, 5, 7, 8** through the three gates. For each, write: the **verdict**, a **one-line rationale**, and the **trap** it's testing (the tempting wrong answer). Use the [decision walker](./feature-flag-decision.tool.html) or work on paper.

**In pairs (~6 min):** compare. Where you disagree, argue it from the *gates*, not from taste. Force each other to answer **"why not the other two?"** for every verdict.

---

## Part 3 — Debate the contested ones (group, ~7 min)

Bring three to the whole room — these are where the learning is:

- **Scenario 4 (ship vs flag):** the code is "done" and green. Why isn't that *just ship it*?
- **Scenario 5 (a permanent flag):** if a flag is temporary scaffolding, how is a kill switch a flag at all?
- **Scenario 8 (compose):** is this a flag *or* a seam? Why is "both" the strongest answer?

---

## The scenarios

All from our world: the HOA Violations API (TypeScript Lambda on AWS) and the established .NET Framework APIs.

1. **The error-message typo.** The Violations API returns a validation error reading `"propertyId is requried"`. You fix the spelling — one line, one updated unit test, backward-compatible.
2. **The history endpoint, revealed after the board meeting.** Product wants `GET /violations/history` for board members, but not until the quarterly board meeting three weeks out. The work spans several days and you want to merge to trunk daily as you build it.
3. **Swap the data-access layer.** Persistence in the Violations API is scattered, hand-rolled AWS SDK calls. You're moving all of it behind a new `ViolationRepository` — no behavior change intended — but you can't have half the handlers on the old calls and half on the new mid-way.
4. **The late-fee rewrite you don't trust yet.** You've rewritten the late-fee escalation calculation. Unit tests pass, but a wrong fee charges a homeowner real money. Before trusting it you want to run the new logic against real production inputs *alongside* the old and compare — without changing what anyone is charged.
5. **The report that melts the database.** The monthly delinquency report runs an expensive query that has twice degraded the shared database at month-end. You want to switch report generation off *instantly* during an incident, without a deploy.
6. **The premium analytics dashboard.** A new violations-analytics dashboard is sold as part of the top-tier HOA management contract. Only communities on that tier should ever see it; everyone else should not — indefinitely.
7. **The null-reference hotfix.** The Violations API throws a `NullReferenceException` (HTTP 500) when a request omits `propertyId`. You add a guard that returns a `400` with a clear message, plus a test. Backward-compatible.
8. **Swap the notification provider, gradually.** Violation notifications go out via raw SNS today. You're moving them to a new managed email service with better deliverability tracking — and you want to cut over one community at a time and fall back instantly if bounce rates spike.
9. **Restructure escalation from a string to a history.** *(stretch)* Each violation stores its escalation as a single `level` string. Product now wants a full `escalationHistory` — every step, with a timestamp — on live data with live readers.
10. **The internal backfill script.** *(stretch)* You write a one-off CLI an engineer runs by hand to backfill a new `createdBy` field on old violation records. No request path calls it; it runs once, from a developer machine, then it's done.

---

## What "good" looks like

A strong answer is never just the label. It:

- **Names why the other two are wrong**, not only why this one is right.
- **States the flag's lifecycle** when the verdict is a flag — release (delete in weeks), kill switch (permanent), permission (permanent), experiment.
- **Spots the compose case** — a structural swap whose flip you want gradual and reversible is a seam *and* a flag.
- **Knows the model's edge** — a schema change on live data is *expand/contract*, the data cousin of a seam, not a call-site flag.

---

## Answer key

> Facilitator: don't hand this out until Part 2 is done. The verdict matters less than the reasoning beside it.

| # | Verdict | Why | Why not the other two | The trap |
| - | ------- | --- | --------------------- | -------- |
| 1 | ✅ Ship it | Finished, one line, backward-compatible, nothing to hide. | A flag/seam here is pure overhead and a future deletion chore. | "Shouldn't everything go behind a flag?" No — YAGNI. |
| 2 | 🚩 Flag (release) | User-facing, incomplete, reveal timing is a business call → decouple deploy from release. | Shipping exposes half-built UI; nothing structural to abstract. | The textbook case — use it as the shared worked example. |
| 3 | 🔀 Branch by abstraction | Structural swap of a whole layer; a seam lets old and new coexist until the swap is complete, then delete the old path. | A call-site flag can't cleanly wrap an entire data layer; too big/risky to swap atomically and just ship. | Reaching for a flag reflexively — structural swaps want a seam. |
| 4 | 🚩 Flag (dark launch / shadow) | "Done" ≠ trusted; a flag lets you run it in shadow / ramp exposure and validate against real data before it charges anyone. | Shipping it live risks wrong fees; it's a behavior to validate, not a subsystem to swap. | "Tests pass, just ship it" — high-blast-radius money logic shadows first. |
| 5 | 🚩 Flag (kill switch — **permanent**) | You need a runtime off-switch for incidents; that switch *is* an operational flag, and it never gets deleted. | Nothing is being replaced; shipping doesn't give you the switch. | Assuming every flag is temporary — this one is architecture. |
| 6 | 🚩 Flag (permission / entitlement — **permanent**) | Behavior gated by subscription tier, indefinitely — a permanent targeting flag tied to entitlement. | It's paid, so you can't ship it to everyone; it's not a structural swap. | Confusing a permanent entitlement toggle with a temporary release flag (or with mere config). |
| 7 | ✅ Ship it | A small, backward-compatible correctness fix with a test — nothing incomplete to hide. | A flag delays the fix and adds a dead path; nothing to abstract. | "It touches behavior, so flag it" — no. Contrast with #4: a *hotfix* ships; an untrusted *rewrite* shadows. |
| 8 | 🔀 BbA **+** 🚩 Flag | A structural provider swap (seam) whose cutover you want gradual and instantly reversible (flag on the flip). | A seam alone gives a big-bang flip; a flag alone can't wrap two whole provider implementations. Together they do. | The "pick one" instinct — the mechanisms compose. |
| 9 | ⚠️ *Neither cleanly* → expand/contract | A schema/data change on live readers uses [expand/contract](../../../resources/glossary.md) — the data cousin of a seam; the read-path cutover can be flag-gated. | A call-site flag doesn't migrate data; a code seam isn't a schema migration. | Forcing it into one of the three — the model has a boundary, and this is it. |
| 10 | ✅ Ship it | No user path calls it, nothing to reveal, backward-compatible. | Nothing to gate, nothing to abstract. | "It touches prod data, so flag it" — internal, manual, one-shot tools rarely need flags. |

---

## Output

- Your six live scenarios classified: **verdict + one-line rationale + the trap**, exported from the walker (Copy as Markdown) or on paper.
- For each flag verdict, its **lifecycle** named: release, kill switch, permission, or experiment.
- **One flag in your own service** you'll act on this week — either add where it's missing, or *delete* one that's outlived its purpose.

Bring your calls to Session 3 — you'll see how each mechanism flows through the pipeline and where a stale flag becomes a red build.
