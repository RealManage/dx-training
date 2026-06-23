# What Continuous Delivery Costs You

> Most CD material sells the upside and hides the bill. If you've shipped weekly
> for years and it mostly works, you deserve an honest accounting before you change
> how your team operates. CD is cheaper than what you do today — but it is not free,
> and the costs are real, recurring, and front-loaded. Here they are.

## The weekly release ritual wasn't stupid

Before we dismantle it, give the weekly release its due. The cadence solved real
problems:

- A **predictable window** — everyone knew when things shipped and could plan around
  it.
- A **natural batching point** for release communication, coordination, and a
  last-look "is this ready?"
- A **rhythm** the team, support, and clients all settled into.

CD has to **replace** each of those, not merely delete them — and it does:
continuous small deploys instead of a window, communication anchored to the flag
flip instead of the weekly bundle (see [communicating-releases](./communicating-releases.md)),
an automated definition of deployable instead of the last-look meeting. But be
honest that you are *trading* a familiar ritual for better mechanisms, not shedding
pure waste. The trade is worth it; it still has a cost.

## The bill

What CD actually asks of a team, line by line:

- **Daily integration is an interruption tax.** Merging every day means resolving
  small conflicts and reviewing others' work *continuously* instead of batching it
  for Friday. It is far *less* total conflict pain — but it is *more frequent*, and
  frequent beats large only if the team accepts the interruptions.
- **The review-turnaround commitment is real.** Keeping branches short needs fast
  reviews (a ~4-hour target). That means you're interruptible — you can't disappear
  into deep focus for a day and ignore the MR queue. It's a genuine cost to
  heads-down work, and a team agreement, not a freebie.
- **Stop-the-line is disruptive on purpose.** A red `main` becomes everyone's
  problem and you drop feature work to fix it. That's the point — fast feedback —
  but it asks the team to value flow over individual progress, which is a cultural
  cost, not just a technical one.
- **Decomposition is a skill you have to build.** Slicing a two-week feature into
  daily, independently shippable increments is hard at first and feels slower before
  it feels faster — the Phase-1 velocity dip (see [migration-checklist](./migration-checklist.md)).
- **Feature flags are inventory you carry.** Every flag is a live branch in your
  code, an extra dimension in your test matrix, and a thing someone must remember to
  remove. Left alone they rot into the worst debt there is: conditional logic nobody
  understands. This is the cost that *compounds* — see the next section.
- **The automation itself.** Pipelines, gates, smoke tests, OIDC, observability —
  real upfront build and ongoing maintenance.

**Why the bill is still worth paying:** you are already paying *more* than this —
just later, and unpredictably. Merge hell, release-day firefighting, rollback panic,
and the slow grind of week-long feedback loops are the big-batch invoice, and it
arrives at the worst possible moment. CD moves the cost up front, where it's smaller
and predictable. Net cheaper — but front-loaded and ongoing, not zero.

## Paying down flag debt — a mechanism, not a sermon

"Flags are temporary, be disciplined" is advice, not a plan, and discipline loses to
deadlines every time. One feature spawning four interacting flags is not a discipline
failure — it's usually a *decomposition* failure. Make flag hygiene mechanical:

- **Birth certificate.** Every flag gets an **owner**, a **creation date**, and a
  **removal condition** the moment it's created — recorded in a **flag inventory**
  (the same one your release communication uses). A flag with no expiry is a bug.
- **A CI stale-flag check.** Flags past their removal condition (or older than N
  days) **fail or warn the pipeline**, so debt can't accumulate silently. Now the
  system, not someone's memory, enforces cleanup.
- **Deletion is a planned slice, not a someday-ticket.** The last step of a feature's
  decomposition is "delete the flag and the dead path" — scheduled in the same plan
  as the rest (see [decompose-a-branch](../sessions/session-2/exercises/decompose-a-branch.md)).
- **A flag explosion is a re-slicing signal.** If a feature needs several interacting
  flags, the slices probably aren't independent. Fix the decomposition rather than
  adding more switches.
- **Match the flag to its lifecycle.** Short-lived **release flags** (off → on once,
  then deleted) are cheap. Long-lived ops/kill-switch or experiment flags are a
  different animal with a different owner and lifecycle — don't let a release flag
  quietly become permanent. (When you need runtime toggles, targeting, or an audit
  trail of who flipped what, that's the cue to graduate to AWS AppConfig /
  LaunchDarkly — see [feature-flag.ts](../sessions/session-2/examples/feature-flag.ts).)

The stale-flag check is just another pipeline job — a few lines, not a product:

```yaml
flag-debt:
  stage: validate
  script:
    # flags.yaml is the flag inventory: each flag lists an owner and a remove_by date.
    # Fail the pipeline for any flag past its removal date — debt can't accumulate silently.
    - |
      today=$(date +%F)
      awk -v t="$today" '/remove_by:/ && $2 < t { print "stale flag, due " $2; n++ }
                          END { if (n) exit 1 }' flags.yaml
  allow_failure: true   # advisory at first; promote to a hard gate once the inventory is clean
```

Governing *who* may flip a production flag, and logging it, is the
[governance](./governance-and-compliance.md) side of the same inventory.

## What you don't pay

Honest accounting cuts both ways — some feared costs aren't real:

- **You don't have to auto-ship to prod.** That's Continuous *Deployment*; CD keeps
  your human release decision.
- **You don't have to buy a tool.** CD is working agreements, not a product.
- **You don't lose your release gate or your control** — see
  [governance-and-compliance](./governance-and-compliance.md). For most teams CD is
  the first time controls are actually enforced rather than assumed.
- **Some costs even fall as AI writes more of the code.** The daily-integration
  interruption tax and the slow-review burden are *human* costs an agent doesn't
  feel — but that saving moves the stakes onto gate quality, it doesn't remove them.
  See [CD when AI writes the code](./ai-assisted-delivery.md).

## Bottom line

CD is a front-loaded investment plus ongoing discipline, with flag maintenance as the
recurring line item. The return is smaller, safer, more predictable delivery and far
less unplanned firefighting. Pay the bill deliberately — and use the mechanisms above
so the one cost that compounds, flag debt, doesn't.

## Related

- [Migration Checklist](./migration-checklist.md) — the velocity dip is Phase 1
- [Decompose a Branch](../sessions/session-2/exercises/decompose-a-branch.md) — where "delete the flag" becomes a slice
- [Communicating Releases](./communicating-releases.md) — the flag inventory's other job
- [Governance & Compliance](./governance-and-compliance.md) — governing flag flips
- [CD When AI Writes the Code](./ai-assisted-delivery.md) — the seams AI authorship stresses
- [Troubleshooting](./troubleshooting.md) — the objections, answered
