# Exercise: Decompose a Long-Lived Branch

**When:** Session 2
**Format:** Individual then small-group, ~30 minutes
**Goal:** Take a real change that *would* have lived on a branch for a week or two, and break it into a sequence of small, safe changes that each merge to trunk within a day.

This is the single most important skill for CD. Trunk-based development isn't about banning branches — it's about making each change *small enough that it doesn't need a long branch*. The work is in the decomposition.

---

## The scenario

You're building a new cloud-native AWS service: an **HOA Violations API** (TypeScript Lambda behind API Gateway, storing to DynamoDB, publishing a `ViolationRecorded` event to SNS so a downstream notifier can email the homeowner).

The feature request:

> "Board members can record a violation against a property. The system stores it, assigns an escalation level based on prior violations (warning → 30-day → 60-day → 90-day), notifies the homeowner, and exposes the violation history for a property."

On the old monolith, this would have been one big branch open for two weeks, merged the day before the weekly release. **Your job: ship it in small daily increments to trunk instead.**

---

## Part 1 — Find the slices (individual, 10 min)

Break the feature into changes that are each:

- **Small** — reviewable in well under an hour, mergeable the same day
- **Safe** — does not break delivered work; backward-compatible
- **Independently shippable** — can merge to trunk even if the next slice isn't started
- **Hidden if needed** — incomplete user-facing behavior sits behind a feature flag, off by default

Write your slices in order. Aim for 6–10. Example starting points:

1. Scaffold the service: SAM template with an empty Lambda + API Gateway route returning `501 Not Implemented`. Deployable on day one.
2. Add the DynamoDB table to the SAM template (no code uses it yet).
3. Implement "record a violation" writing to DynamoDB — behind a flag, returning the stored record.
4. ...

Continue: escalation-level logic, the SNS publish, the history endpoint, turning the flag on.

---

## Part 2 — Pressure-test your slices (small group, 15 min)

Swap lists with a partner or small group. For each slice, challenge:

- **"Could this merge to `main` today without breaking anything?"** If no, it's too big or not backward-compatible — split it.
- **"What's hidden behind a flag, and what's safe to expose immediately?"** Infrastructure and dark code paths can often ship visibly; user-facing behavior hides until ready.
- **"Does any slice require a later slice to function?"** If slice 3 breaks without slice 5, reorder or guard it.
- **"Where's the database change?"** A schema/shape change should use expand/contract: add new, write both, migrate, remove old — each a separate small deploy.

Revise your list based on the challenges.

---

## Part 3 — Map to the practices (group discussion, 5 min)

Connect what you just did to the minimums:

- How does this keep **branches short-lived**? (Each slice = a branch open for hours.)
- How does the **feature flag** let you **decouple deploy from release**? (Code ships dark; the board sees it only when you flip the flag.)
- How does **"new work doesn't break delivered work"** shape your ordering?
- If slice 6 has a bug in prod, why is **rollback** trivial compared to reverting a two-week branch?

---

## What "good" looks like

A strong decomposition for the Violations API might be:

1. SAM scaffold: Lambda + API Gateway, route returns `501`. *(Visible, harmless.)*
2. Add DynamoDB table + IAM permissions in the template. *(Infra only, unused.)*
3. `POST /violations` writes a record to DynamoDB behind `violations.record` flag (off). *(Dark.)*
4. Escalation-level calculation as a pure function with unit tests; not wired in yet. *(Pure logic, fully testable.)*
5. Wire escalation level into the record path, still behind the flag. *(Dark.)*
6. Publish `ViolationRecorded` to SNS after a successful write, behind the flag. *(Dark.)*
7. `GET /violations?propertyId=...` history endpoint behind `violations.history` flag (off). *(Dark.)*
8. Turn on `violations.record` in dev → qa → prod after validation. *(Release decision.)*
9. Turn on `violations.history`. *(Release decision.)*

Nine slices, each merging to trunk the day it's written, each independently deployable, the feature revealed by flipping flags — not by a big-bang merge.

---

## Output

- A revised, ordered list of 6–10 small slices for your own real feature (use a current backlog item if you have one)
- Each slice labeled: visible-now vs behind-a-flag
- Any database change expressed as expand/contract steps

Bring your decomposition to Session 3 — you'll see how each slice flows through the pipeline.
