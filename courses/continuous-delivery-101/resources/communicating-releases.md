---
order: 50
---
# Communicating Releases Under Continuous Delivery

> If we ship continuously, what happens to the weekly email that tells clients
> what changed? It doesn't die — it changes what it's built from. This is how
> release communication works once deploy and release are decoupled.

## The problem CD creates

Today's model is reasonable, and most teams run some version of it:

- We deploy on a weekly cadence. That weekly deploy **is** the release.
- Each deploy carries a **release number**, and teams tag their Jira stories with
  it.
- A release manager curates the client-facing "here's what changed this week"
  email from those tagged stories.

It works because deploy = release = a weekly bundle with a number.

Continuous Delivery breaks all three joints:

- **Deploys become continuous and automated.** There is no weekly bundle to
  number, and "what deployed this week" is now dozens of small, mostly invisible
  changes — refactors, infrastructure, code sitting dark behind flags. Users don't
  care about those, and shouldn't.
- **Deploy no longer equals release.** A change can deploy on Tuesday and be
  *released* (its flag flipped) three weeks later — or never. The deploy date is
  not the release date.
- **So "release number on the story" stops meaning anything.** It assumed a weekly
  bundle that no longer exists.

Keep generating notes from the deploy log and you get one of two failures: you spam
clients with noise (every deploy), or you announce things that aren't actually live
yet (deployed dark, flag still off).

## The reframe: communicate releases, not deploys

> Clients care about what changed *for them* — not how many times you deployed.

The unit of communication is the **user-facing release**: the moment a feature
becomes visible, which under CD is the **flag flip**, not the deploy. That moment is
a deliberate decision you already control, which makes it the ideal anchor for a
note — it's intentional, dated, and meaningful to the audience.

| | Deploy-anchored (today) | Release-anchored (CD) |
| - | ----------------------- | --------------------- |
| Trigger for a note | the weekly deploy | a user-facing change going live (flag on) |
| Data source | Jira stories tagged with a release number | the flag flip / the change marked customer-facing |
| What it lists | everything that shipped | what changed for the user |
| Cadence | weekly (tied to the deploy) | your choice (see below) |

## A replacement data source

Capture the release when it becomes *real* — when the user-facing change goes live
— not when code deploys:

- **Mark the user-facing change.** When an MR introduces or flips a flag that
  changes user-visible behavior, label it `customer-facing` and write one
  plain-language line: *what changed, and for whom*. That line is the release note.
- **Keep a flag inventory.** For each flag: what it does, who it's for, current
  state, and the date it was turned on. **The flip date is the release date.** The
  same inventory keeps flag debt visible.
- **Assemble the changelog at flip time**, from the customer-facing items — not from
  `git log` or the deploy history.
- **Retire the "release number."** Replace it with a per-release identifier on the
  user-facing change plus its flip date. You're numbering *releases* (what clients
  got), not *deploys* (what servers got).

> **Make the convention a check, not a hope.** A label only works if it's reliably
> applied. Back it with a light MR gate: an MR that adds or flips a flag must carry
> *either* a `customer-facing` label (with its one-line note) *or* an explicit
> `no-user-impact` label — fail the check if it has neither. That turns "remember to
> label it" into a gate, so the release-notes data source doesn't silently rot the
> first time someone forgets.

Six lines of CI enforce it — a real job in the Session 3 pipeline (`release-impact-label`
in [`session-3/examples/.gitlab-ci.yml`](../sessions/session-3/examples/.gitlab-ci.yml)).
It runs in the *merge-request* pipeline — the one place GitLab populates
`$CI_MERGE_REQUEST_LABELS` — which is why that pipeline enables MR pipelines alongside the
branch pipeline that builds and promotes the artifact:

```yaml
release-impact-label:
  stage: validate
  rules:
    - if: $CI_PIPELINE_SOURCE == "merge_request_event"
  before_script: [] # no npm install needed — this gate only reads the MR's labels
  script:
    - |
      case ",$CI_MERGE_REQUEST_LABELS," in
        *,customer-facing,*|*,no-user-impact,*)
          echo "Release impact declared." ;;
        *)
          echo "Label this MR 'customer-facing' (with a one-line note) or 'no-user-impact'."
          exit 1 ;;
      esac
```

Now forgetting to label fails the MR — not the client email three weeks later.

## Cadence is a separate decision

Deploying continuously does **not** force you to communicate continuously. Comms
cadence is a business choice, independent of deploy cadence:

- **Keep the weekly digest** if clients like it — it just summarizes the week's
  *user-facing releases* (flags flipped), not the deploys.
- **Or announce per feature** when something noteworthy goes live.
- **Or both:** an immediate notice for big changes, a weekly roll-up for the rest.

You decoupled deploy from release; now decouple *communication* from deploy too, and
align it to what users actually experienced.

## Internal vs external, and support

- **Internal (support/CS — and you):** support needs to know what changed and *when
  it went live, to whom* — especially with **canary** and **dark launch**, where a
  feature may be live for 10% of users before it's announced. Give support the flag
  inventory and flip dates so "is this rolled out yet?" has a real answer.
- **External (clients):** curated, audience-appropriate, built from the
  `customer-facing` items — fewer, clearer, tied to value.
- **Announce on the flag flip, not the deploy.** Tell people when it's actually on
  for the audience you're telling.

## The release manager's role, evolved

Worth saying plainly: the **pipeline** already performs the deploy. Today our release
manager presses its release button on a weekly schedule; under CD that *same* button is
pressed on demand instead. The mechanism doesn't change — the *cadence* does, and deploy
decouples from the user-facing release (the flag flip). That isn't the role disappearing —
it's the role moving up to the work that actually needs judgment:

- **Owning the release-communication data source** — the flag inventory, the
  `customer-facing` discipline, the curated changelog.
- **Governing flag flips** — coordinating *which* features go live *when* and *to
  whom*: the release-timing decisions CD makes routine but not automatic. (This is
  where release management meets governance.)
- **Customer and support communication** — the human, audience-facing work no
  pipeline can do.

Pressing the release button was never the hard part, and it doesn't go away. What changes
is that releasing stops being a weekly ritual to gatekeep and becomes routine and on-demand,
decoupled from the flag flip. CD leaves the judgment: which features go live, when, to whom,
and how it's communicated.

## Putting it in the migration

Don't declare the migration "done" (Phase 4) until communication is re-established
on the new footing — otherwise the weekly email quietly breaks the first week the
team stops doing a single weekly deploy. Before you call it done:

- [ ] Release notes are generated from user-facing releases (flag flips), not the deploy log
- [ ] A flag inventory exists, with flip dates, and support can see it
- [ ] The client-communication cadence is an explicit, agreed decision
- [ ] The release manager's evolved role (comms + flag governance) is named and owned

## Related

- [Glossary](./glossary.md) — *deploy* vs *release*, *feature flag*, *dark launch*
- [CD vs Continuous Deployment](../sessions/session-1/examples/cd-vs-continuous-deployment.md)
- [Migration Checklist](./migration-checklist.md) — Phase 4
- [Troubleshooting](./troubleshooting.md) — the objections you'll hear
