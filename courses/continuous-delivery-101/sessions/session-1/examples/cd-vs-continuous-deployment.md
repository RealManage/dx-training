# Worked Example: Continuous Delivery vs Continuous Deployment

These two terms share an abbreviation (CD) and get used interchangeably. They are not the same thing, and the difference decides how much automation you build and how much human judgment you keep.

## The one-line difference

- **Continuous Delivery:** every change is *kept ready* to deploy. A human decides *when* to push the button.
- **Continuous Deployment:** every change that passes the pipeline *is pushed automatically*. No button.

Continuous Deployment is Continuous Delivery **plus** removing the final human gate. You cannot have the second without first achieving the first.

## Side by side

| | Continuous Delivery | Continuous Deployment |
| --- | ------------------- | --------------------- |
| Every commit kept deployable | ✅ | ✅ |
| Pipeline decides releasability | ✅ | ✅ |
| Reaches production... | when a human chooses | automatically, every green commit |
| Final human gate | yes (approve *timing*) | no |
| Good first target | **yes** | only after deep trust in the pipeline |
| Required for the other | — | requires CD first |

## The flow, drawn out

```text
Continuous Delivery
  commit → CI → build → test → scan → [ DEPLOYABLE ] → 🧑 "deploy now" → prod

Continuous Deployment
  commit → CI → build → test → scan → [ DEPLOYABLE ] ───── auto ─────────→ prod
```

The pipeline up to `[ DEPLOYABLE ]` is *identical*. The only difference is the last hop.

## A RealManage example

Take the **HOA Violations API** (TypeScript Lambda). A board-facing change — say, a new escalation rule — is committed.

**Under Continuous Delivery:**

1. Pipeline runs: lint, unit tests, coverage, `sam validate`, security scan — all green.
2. The artifact is now *deployable* and sitting ready.
3. The team deploys to prod **when it makes sense** — maybe immediately, maybe after the board's Tuesday meeting, maybe alongside a client communication. A human decides the *timing*; the pipeline already decided the *readiness*. And what you announce is the **release**, not the deploy — see [Communicating Releases](../../../resources/communicating-releases.md).

**Under Continuous Deployment:**

1. Same pipeline, same green result.
2. The change flows straight to prod automatically, minutes after merge.
3. Feature *exposure* is controlled by a **feature flag**, not by withholding the deploy — the code ships dark and is revealed later.

## Why CD (not Continuous Deployment) is the right first goal

- It delivers nearly all the benefit: small batches, fast feedback, low-risk releases, on-demand deploys.
- It keeps a human in the loop for *timing*, which satisfies most change-management and compliance needs.
- It's a smaller cultural leap from weekly releases — you're changing *how ready* you are, not yet removing the last gate.

Once a service has a trustworthy pipeline, strong tests, good observability, and rehearsed rollback, you *can* drop the final gate and move that service to Continuous Deployment. That's a per-service decision, made later, from a position of confidence — not a prerequisite for getting value today.

## Common confusion to avoid

> "We can't do CD — we're not comfortable with code auto-shipping to prod."

That objection is about Continuous **Deployment**. Continuous **Delivery** keeps the human gate. You can adopt CD without ever auto-shipping anything. Don't let the fear of the second stop you from getting the first.

## Related

- [Minimums Reference](../../../resources/minimums-reference.md)
- [Glossary](../../../resources/glossary.md) — see *deploy* vs *release*, *feature flag*
- Feature flags in practice: [Session 2](../../session-2/README.md)
- [Communicating Releases](../../../resources/communicating-releases.md) — what replaces the weekly release email under CD
