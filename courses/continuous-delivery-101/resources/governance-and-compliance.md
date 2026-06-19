# Governance, Compliance & Control Under CD

> "We can't just let things deploy." Right instinct, wrong conclusion. Continuous
> Delivery does not remove control — it **relocates and strengthens** it, from
> control-by-meeting (slow, inconsistent, barely recorded) to control-by-pipeline
> (fast, uniform, auditable). This is the page for the Engineering Leader who is
> accountable for risk, change windows, and the audit, and for anyone who has to
> answer to a client or a compliance framework.

## First, the distinction that defuses the fear

Most "CD removes control" worry is actually about **Continuous Deployment** —
code auto-shipping to prod with no human in the loop. Continuous **Delivery** keeps
the human decision of *when* to release. You can adopt CD and never auto-ship
anything. (See [CD vs Continuous Deployment](../sessions/session-1/examples/cd-vs-continuous-deployment.md).)
So the question is not "do we keep control?" — you do — but "where does each
control live, and is it doing real work?"

## A manual gate is sometimes a control, sometimes debt

Two very different things wear the same "click to deploy" button:

- **Debt:** a gate that re-litigates *readiness* the pipeline already proved — a
  meeting asking "is this tested enough?" when automated tests, coverage, and
  security scans already answered. This is slow theatre. Remove it.
- **Legitimate, permanent control:** a single deliberate human *authorization* —
  accepting business risk, honoring a contractual change window, or a regulated
  environment that requires named sign-off. Keep it, deliberately and honestly.

> **The test:** *what is the human actually deciding?* If it's **readiness**, it's
> debt — automate it. If it's **timing, authorization, or risk acceptance**, it's a
> control — keep it. The course's "remove the gate" advice is about debt gates, not
> authorization gates.

## Segregation of duties: the MR review *is* the control

The classic requirement — the person who writes a change can't be the one who ships
it unchecked — is satisfied **more strongly** by mandatory review plus an automated
pipeline than by a manual deploy gate:

- The author **cannot merge unreviewed**; a second person approves the MR.
  (Configure the project so an author cannot approve their own MR.)
- The **pipeline**, not the author, performs the deploy — via OIDC, so the author
  holds **no standing production credentials**. Nobody hand-deploys.
- This is enforced by the tool, on **every** change, and recorded. A manual "ops
  clicks deploy" gate is often *weaker*: a rubber stamp, with no record of what was
  actually reviewed.

CD doesn't weaken segregation of duties — for most teams it's the first time it's
actually enforced rather than assumed.

## The pipeline is your audit log

Auditors want four things: *who changed what, when, who approved it, and what
verification ran.* CD produces all four automatically and immutably:

| Audit question | Where CD answers it |
| -------------- | ------------------- |
| What changed, and who wrote it? | the commit + merge request |
| Who approved it? | the MR approval (reviewer ≠ author) |
| What verification ran? | the pipeline run — tests, coverage, security scan — attached to the artifact |
| When was it deployed, by whom? | the deploy job: timestamp + the scoped OIDC role (not a person) |
| Are the prod bytes the bytes that were tested? | the immutable artifact, tagged by `CI_COMMIT_SHA` — same hash, dev through prod |

No manual change ticket matches this for completeness or tamper-resistance. The
practical work for a leader is to **map your control framework's evidence
requirements onto these artifacts** once — then every release produces the evidence
as a by-product, instead of someone assembling it by hand.

## Break-glass: controlled exceptions, not chaos

CD does not mean "no exceptions, ever." You need a documented emergency path for
when the normal flow genuinely can't run — the pipeline itself is down, or a Sev1
needs an out-of-band action. A sound **break-glass** procedure is:

- **Pre-authorized, narrowly scoped, time-boxed** elevated access — granted in
  advance to named roles, not improvised under pressure.
- **Logged and alerted** on every use — the exception is itself an audited event.
- **Reconciled afterward** by a mandatory post-incident review: re-apply the change
  through the pipeline, add the test or gate that was bypassed, close the loop.
- **Rare by design.** If you break glass weekly, the normal path is too slow — fix
  the path, don't widen the exception.

## Decoupling deploy from release moves authorization to the flag

This is the subtle shift leaders must not miss. When you stop authorizing at the
*deploy* and start releasing via *feature flags*, the authorization control should
move with the decision. So govern the flip:

- **Who** can flip a production flag — especially a customer-facing one?
- **Is the flip logged** (who, when, which flag, to whom)? That log is your
  *release* audit trail, distinct from the deploy log.
- **Does a customer-facing or regulated change need named sign-off** before the
  flip?

The flag system becomes a control surface; treat it like one. This is where release
management and governance meet — see [Communicating Releases](./communicating-releases.md).

## The paragraph to hand an auditor (or a nervous client)

Tailor to your framework, but this is the shape:

> Every production change flows through a single automated pipeline. No change
> reaches a shared environment without (a) peer review and approval on the merge
> request, (b) passing the automated quality and security gates that define
> "deployable," and (c) deployment performed by the pipeline under a scoped,
> credential-less role — never by an individual. The change, its reviewer, the
> gates it passed, and the time and identity of its deployment are recorded
> immutably and tied to the deployed artifact by content hash. Production releases
> are deliberate, authorized decisions, separate from deployment, and logged.
> Emergency changes follow a documented, logged break-glass procedure with
> mandatory post-incident review.

## The Engineering Leader's takeaway

Your job under CD is not to approve each release. It is to **design the controls the
pipeline enforces** — and to decide, gate by gate, which are *authorization* (keep,
permanently and honestly) and which are *readiness* (automate). You trade a slow,
uneven, lightly-documented control for a fast, uniform, fully-evidenced one. That is
more governance, not less.

## Related

- [CD vs Continuous Deployment](../sessions/session-1/examples/cd-vs-continuous-deployment.md) — you keep the human release decision
- [Communicating Releases](./communicating-releases.md) — governing the flag flip
- [Migration Checklist](./migration-checklist.md) — where controls get built (Phase 2)
- [Troubleshooting](./troubleshooting.md) — the compliance objection, answered
- [Glossary](./glossary.md) — *manual gate*, *OIDC*, *immutable artifact*
