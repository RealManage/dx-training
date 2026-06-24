# Continuous Delivery 101 — Review 3

**Student:** Marcus (Release Manager, 8 yrs)
**Stance going in:** I personally run the weekly prod deploy AND curate the
client-facing release-notes email. My live concern across all rounds: once the
weekly "release number" tagging collapses, where does the client email's data
come from, and what becomes of my role? Round 2 closed that hole
(`communicating-releases.md`); I rated it 9/10.
**Review date:** 2026-06-23
**Overall rating:** 9/10 (no change from review 2) — would I adopt/champion this?

## Executive summary

This round is database- and CI-internals-heavy: a new schema-as-code resource
(`resources/database-delivery.md`), a worked DbUp migrator
(`sessions/session-3/examples/db-migrations/`), a rollback-wording sweep, and
glossary additions (DDL, branch by abstraction, schema terms). **Almost none of
it touches my lens, and that is the correct outcome — I will not manufacture a
nitpick to pretend otherwise.** I verified the two things that *could* have hurt
me and neither did: (1) `communicating-releases.md` is untouched this cycle
(only a cosmetic `order:` frontmatter edit), and the round-2 nice-to-have I left
open — gate the `customer-facing` label instead of trusting it — was already
delivered as a real CI job; (2) the rollback "re-run the last good GitLab
deployment" reframing did **not** regress my deploy ≠ release story anywhere it
propagated. The one substantive question my seat raises: schema changes now ship
continuously through the pipeline, yet `database-delivery.md` says nothing about
client communication. I checked, and that silence is **correct, not a gap** — a
migration is a deploy, not a release; it surfaces to clients only when the flag
it supports flips, which my existing flow already captures. Net: a clean,
proportionate round with one small adjacency worth a sentence. Holding at 9.

## What I actually checked this round (and what I found)

### 1. Did the new DB-delivery story orphan my client-comms story? — **NO (correct scope)**

This was the obvious risk to my lens: schema and baseline-data changes now flow
continuously through the pipeline (`database-delivery.md:12-15`), the same way
app code does. If those changes are invisible to the people who write client
notes, that's a gap. I grepped the resource for release-notes / changelog /
client / communicate / announce — **zero matches**. So: are DB changes invisible
to release communication?

They are, and that is the **right** answer, for the exact reason the course
already taught me:

- A migration is a **deploy**, not a **release**. `database-delivery.md` is built
  entirely on the deploy/release decoupling I care about — schema moves through
  `dev → qa → prod` as a technical act (lines 12-15, 26), and reversibility comes
  from **expand/contract**, not rollback (lines 24, 56-60). A column added by
  `Script0002` is dark to users until the feature behind it is switched on.
- The user-facing moment is still the **flag flip**, which my existing data
  source already captures. The DbUp example makes this explicit: `Script0002`
  adds the column *nullable* so "application code can deploy or roll back
  independently" (`db-migrations/README.md:39`) — i.e. the schema lands invisibly,
  the feature releases later via the flag, and *that* flip is what I write a note
  about. Nothing new for me to instrument.

So `database-delivery.md` correctly stays silent on client comms: a schema
migration that needed its own client note would be a schema migration coupled to
a user-visible behavior change with no flag between them — exactly the
anti-pattern expand/contract exists to prevent. **Verified consistent**, not a
hole. I would not add a comms section to the DB resource; it would muddle the
deploy ≠ release line that makes the rest of the course work.

**One small adjacency, optional (Nice-to-have).** The audience for
`database-delivery.md` is engineers and DX, not me, so I don't need it to address
comms. But there is one reader who could trip: someone doing a schema change that
*does* alter user-visible behavior in the same step (e.g. a reference-data /
baseline-data change that flips a status label clients see — `database-delivery.md:52-54`).
A single cross-link — "user-visible data/behavior changes still release on a flag
flip and get a note; see [communicating-releases]" — would close the only seam
where a DB change could leak to clients unannounced. Cheap, and it joins the two
resources. Not docked: it's outside this resource's stated lane.

### 2. Did the rollback "re-run the last good GitLab deployment" sweep regress my deploy ≠ release story? — **NO**

The sweep (commit `c42a007`, plus two stale-reference fixes in `f3270d5`)
replaced the hand-edited-Lambda-alias rollback with "re-run the last known-good
deployment in GitLab" across `rollback-on-aws.md`, `minimums-reference.md:98`,
`migration-checklist.md:63`, `glossary.md:61,189`, and the session/strangler
files. My concern was whether any of that muddied the deploy-vs-release language
my comms story rests on. It didn't:

- `rollback-on-aws.md:28,71-76` still keeps the **feature-flag kill switch** as
  its own rollback strategy ("rollback with no deploy") and the fastest mitigation
  — which is *my* lever, the flip, intact and even foregrounded.
- The data-trap framing (`rollback-on-aws.md:39`) still ties reversibility to
  expand/contract and backward-compatibility, which is the same logic that keeps
  schema dark until the flip. Consistent with the new DB resource.
- Rollback is consistently "redeploy the prior immutable artifact through the
  pipeline" — a *deploy* action. It is never confused with a *release*. My
  release = flag-flip anchor is untouched.

No regression. If anything the sweep tightened the through-line: rollback is now
unambiguously a pipeline deploy, which keeps the deploy/release seam clean.

### 3. Is my round-2 data source still intact, and did the open nice-to-have land? — **YES (improved)**

`communicating-releases.md` was not edited this cycle except for an `order:`
frontmatter line (cosmetic, commit `7f29636`). Everything I praised in round 2 is
still there. And the one thing I left open in round 2 — recommendation #5, "make
'release note written' a gate rather than a convention" — has been **delivered**
(it landed in round 3, `ac84505`, before this cycle, but I confirm it survives):
`communicating-releases.md:71-101` now backs the `customer-facing` label with a
real six-line CI job (`release-impact-label`) that fails an MR carrying neither
`customer-facing` (with its one-line note) nor `no-user-impact`. That converts my
release-notes data source from a Friday-scramble hope into a pipeline gate — the
exact upgrade I asked for. My data source is now as trustworthy as the pipeline's
verdict. Good.

### 4. Glossary additions — skimmed for anything touching release communication

DDL (`glossary.md:138-139`), branch by abstraction (103-104), and the schema
cluster (baseline data, baseline script, data trap, DbUp, forward-only migration,
local-database playground, schema migration, schema-history table) are all
engineering vocabulary. None touch client communication, and none need to. The
two entries adjacent to my world are both correct:

- **Release notes (changelog)** (`glossary.md:121-122`) — unchanged, still anchors
  to flag flips, not deploys. Good.
- **Data trap** (`glossary.md:132-133`) — new; correctly frames rollback as a
  *code* action that doesn't move data, reinforcing expand/contract. No comms
  implication, but consistent with my deploy ≠ release model.

No glossary regression; nothing new I need to carry into the client email.

## Where it lost me / objections it didn't answer

Nothing new this round. My standing round-2 objections are unchanged because the
files that carry them weren't in scope:

- **Current-state assessment still can't see my function.** My round-2
  recommendation #1 (a Phase-0 prompt: "Where does release information reach
  customers/support today, and what is it built from?") is still not in
  `exercises/current-state-assessment.md`. The DB work even *added* assessment
  edits in `f3270d5`, so the file was open — but the comms prompt still isn't
  there. Not a regression, but the one place my seat remains invisible. **Carried,
  not re-litigated.**
- **Timing gate still split across two roles.** `session-3` still narrates the
  prod-timing gate as the Engineering Lead's; the resource makes flag-flip timing
  mine. Round-2 recommendation #2, still open, still half-credit. The new DB
  example actually *repeats* the Engineering-Lead-flavored framing
  (`db-migrations/README.md:67`: "a human approving *timing*, not *readiness*") —
  which is correct and well-put, but it's another place the timing-owner goes
  unnamed. Minor, carried.

These are finish-work I already logged, not findings this round produced.

## Factual / technical concerns

None in my lane. The DB-delivery model is operationally sound where it meets my
world: migrations-as-deploy, flag-flip-as-release, expand/contract for
reversibility, idempotent re-runs making "re-run the last good deployment" safe
for schema (`database-delivery.md:23`, `db-migrations/README.md:63`). The
flip-date-as-release-date model I rely on still holds end to end with a database
in the picture. Nothing for the infra reviewer from my seat.

## Recommendations (round 3)

### Nice to have (the only new item this round)

1. **Cross-link `database-delivery.md` to `communicating-releases.md` at the
   baseline-data section** (`database-delivery.md:52-54`): one sentence noting
   that a data/schema change altering *user-visible* behavior still releases on a
   flag flip and gets a client note. Closes the single seam where a DB change
   could reach clients unannounced. Optional — outside the resource's stated lane.

### Carried from round 2 (unchanged, not re-argued)

2. Add the release-communication Phase-0 prompt to the current-state assessment
   (round-2 rec #1). Still the last place the assessment can't see my function.
3. Reconcile the prod-timing gate across `session-3` and `communicating-releases.md:135-137`
   (round-2 rec #2); the new DB example is one more place the timing-owner goes
   unnamed.

## Verdict

**Champion, unchanged.** This round did engineering-internals work (schema
delivery, rollback wording, glossary) and did it without touching — or harming —
the release-communication spine I care about. The one risk my seat flags
(DB changes invisible to client comms) is correctly out of scope, not a gap: a
migration is a deploy, a flag flip is the release, and my data source already
sits on the flip. My round-2 open items survive untouched, and the one
nice-to-have I'd left (gate the label) was delivered. **9/10, flat — proportionate
to a round that barely entered my lane, and that's a compliment to the scoping,
not a complaint.**
