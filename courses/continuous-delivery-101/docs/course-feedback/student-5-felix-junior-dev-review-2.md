# Continuous Delivery 101 — Review 2

**Student:** Felix (Junior Developer, 2 yrs)
**Stance going in:** Same as round 1 — eager, want to do this right, but I'm the
clarity canary. A term used before it's explained loses me, and I say so. Round 1
I rated it 8/10; the glossary rescued me ~8 times but had holes exactly where a
beginner falls.
**Review date:** 2026-06-19
**Round:** 2 (course revised after round-1 feedback, mine included)
**Overall rating:** 9/10 — yes, and it's noticeably more followable than round 1

## Executive summary

They fixed almost everything I flagged. The four glossary holes that hurt me most
last time — **expand/contract**, **smoke test**, **DORA (the acronym)**, and
**release notes** — are now all in the glossary, in plain language, and the
manual-gate entry has been reframed honestly. The README now has the exact
"new to these terms? the sessions teach them, keep the glossary open" note I
asked for (`README.md` L27), so the jargon wall on page one no longer spikes my
blood pressure. The most satisfying single fix: the README *promised* the
glossary for expand/contract last round and the entry didn't exist — that broken
promise is closed (`resources/glossary.md` L94–95).

The risk this round was the **new, more advanced material**. I read all four new
docs as a beginner. Three of them (`what-cd-costs.md`, `communicating-releases.md`,
`governance-and-compliance.md`) are genuinely readable for a junior — they define
or self-explain their harder terms. The fourth, `strangler-fig-violations.md`, is
the most advanced thing in the course and it lost me in a few specific places:
it leans on **idempotent**, **system of record**, **shadow reads**,
**reconciliation**, and **watermark** as if I already know them, and none of those
are in the glossary. I could follow the *shape* of the migration but not every
mechanic. That's the one place the revision out-ran me.

Net: more followable for a beginner on the core, and the new advanced docs only
leave me behind on the single hardest one — which is also the one most clearly
labeled as "the hardest case."

## Round-1 clarity points — status this round

This is the heart of my review. For each clarity gap I raised in round 1, here's
where it landed.

| # | Round-1 gap | Status | Evidence |
| - | ----------- | ------ | -------- |
| 1 | **expand/contract** not in glossary (used 5×, cited-but-absent) | **IMPROVED** | Now a full entry: `resources/glossary.md` L94–95 — "first *expand*… then migrate readers; then *contract*… each step a small, backward-compatible deploy." Exactly the definition I had to dig out of troubleshooting last time. |
| 2 | **backward-compatible** load-bearing, undefined | **PARTIALLY ADDRESSED** | Still no standalone glossary entry, but it's now bundled into the expand/contract definition ("each step is a small, backward-compatible deploy," `glossary.md` L95) and the decompose exercise defines its requirement inline (`exercises/decompose-a-branch.md` L29, L52). I can infer it cleanly now; a one-line glossary entry of its own would fully close it. |
| 3 | **smoke test** not in glossary (used in a workshop step) | **IMPROVED** | New entry `resources/glossary.md` L77–78 — "a fast, shallow check run immediately *after* a deploy… proves the deploy worked; it is not full testing." That's the one-liner I asked for. Still used in the Session 3 workshop (`sessions/session-3/README.md` L132) but now it's defined. |
| 4 | **DORA** acronym never expanded | **IMPROVED** | `resources/glossary.md` L30–31 now expands it: "**D**evOps **R**esearch and **A**ssessment program (the *Accelerate* research)." I finally know what D-O-R-A stands for. Note: Session 1 itself still just says "The DORA research (*Accelerate*)" inline (`sessions/session-1/README.md` L90) without expanding — but the glossary now backs it, and the README flags the glossary, so this is closed for me. |
| 5 | **README front door** jargon-dense, no "to-be-taught" anchor | **IMPROVED** | `README.md` L27 adds exactly the note I recommended: "**New to any of these terms?** Each is defined in plain language in the Glossary and introduced in the session that uses it — you don't need to know them going in." This is the single change that most lowered my anxiety on page one. |
| 6 | **trunk** used before it's tied to `main` in Session 1 | **PARTIALLY ADDRESSED** | The glossary entry is now explicit and good (`glossary.md` L19–20, "`main`"), Session 2 ties it inline ("the trunk (`main`)," `sessions/session-2/README.md` L44), and the README's new note points me to the glossary. **But Session 1's *first* use is still un-glossed:** `sessions/session-1/README.md` L37 says "an always-deployable trunk" and section 4.1 (L127) says "one shared trunk" — neither pins it to `main` at the point of first use. A junior reading Session 1 in order still has to tab-flip on first contact. Small, but it's the exact thing I asked for and it's still open in S1. |
| 7 | **strangler fig** first scary term, no anchor | **PARTIALLY ADDRESSED** | Still **not in the glossary**. But it's no longer a dead-end: the README (`README.md` L10) and Session 1 (`sessions/session-1/README.md` L27) now both link the term to a full worked example, `sessions/session-3/examples/strangler-fig-violations.md`, which defines the pattern in one line ("Put a **seam** in front of the capability; build the replacement **beside** it; move callers and data across in small, reversible steps; delete the old path last," L38–43). So the anchor exists — it's just a click away, not a glossary line. For a beginner meeting it on page one, a one-line glossary stub *plus* the link would be ideal. |
| 8 | exercises assume junior **access/role** (DORA numbers, releasability ownership) | **STILL OPEN** | The current-state assessment still says "Pull the numbers; don't guess" (`exercises/current-state-assessment.md` L46) with no IC-specific note. The new "Mixed estate?" callout (L15) is a good addition but addresses a *different* problem (monolith vs new service), not "I'm a junior without access to MR history or release decisions." My round-1 ask — a line saying "if you're an IC, fill the habit rows yourself and bring the access-dependent rows to your lead" — is still not there. The exercise header does say "Do this as a team, out loud" (L7), which softens it, but doesn't tell a solo junior what they *can* do alone. |

**Round-1 → round-2 scorecard:** 4 IMPROVED, 3 PARTIALLY ADDRESSED, 1 STILL OPEN,
0 REGRESSED. Every one of my top-three priorities (expand/contract, smoke test,
DORA) is fully fixed.

## Section-by-section (what changed for me)

### Course framing (README)

The new note at `README.md` L27 is the fix I most wanted. Last round I met ~14
unfamiliar terms in the objectives list with no signal for which were "you'll
learn this" vs "you should already know this." Now there's an explicit promise
that they're all taught and glossary-defined. The objectives list itself (L18–25)
is unchanged in density, but the note disarms it. The strangler-fig mention
(L10) now links to a worked example instead of being a bare assertion — better.

### Session 1 — Why CD & the Minimums

Still the section that converts me. The self-reinforcing-trap diagram and the
big-batch/small-batch table are intact and still the clearest things in the
course. The DORA metrics block (L88–97) is fine on its own; I just had to go to
the glossary to learn what DORA *is* (now possible — round 1 it wasn't).

One genuinely nice new addition for me: the "In fairness to the weekly release"
callout (`sessions/session-1/README.md` L74) and its link to `what-cd-costs.md`.
As a junior I don't have a strong opinion to defend, but reading "the cadence
wasn't irrational" made the whole argument feel honest rather than salesy.

The one still-open S1 item: "trunk" at L37 is my first contact with the word and
it isn't glossed inline (see point 6 above).

### Session 2 — Trunk-Based Development & CI

Feature-flag teaching is still the best in the course. New for me: the flag-debt
discipline at L98 now points to `what-cd-costs.md` for the "stale-flag check"
mechanism. That sentence is dense — "let a CI **stale-flag check** fail the build
when one outlives its expiry" — but I could follow it because it's spelled out,
and the linked doc explains the whole mechanism plainly.

The CI minimums table still has "Changes are backward-compatible (expand/contract
for data)" at L115 — the exact line that confused me most last round. It still
drops the term in parentheses, **but now it's no longer a dead end:** the glossary
has expand/contract, and L168 still points me there — and this time the entry
exists. The broken promise is fixed.

### Session 3 — The Pipeline

Densest session; my AWS gaps still show, but the course still warned me. New
material here that I could follow: the multi-account note (L85) is clearly written
even though multi-account AWS is over my head — it explains *why* ("Rebuilding per
account would silently break the immutability guarantee"). The communication
shift (L47, linking `communicating-releases.md`) made sense to me as a reader.

"smoke test" is still used in the workshop (L132) — now defined in the glossary,
so the loop is closed.

### Resources (glossary, the new docs, troubleshooting)

The **glossary** is now a much stronger safety net. It rescued me again, and this
round it caught the terms it missed last time: DORA (L30), smoke test (L77),
expand/contract (L94), release notes (L97), and the reframed manual gate (L74–75,
now distinguishing debt gates from legitimate permanent controls — a distinction
I could actually follow). Remaining holes a beginner could still hit: **strangler
fig**, **backward-compatible** (own entry), and the advanced strangler-fig terms
(below).

**`troubleshooting.md`** now defines expand/contract inline in *two* places (L96,
L87) — so even if I never opened the glossary, I'd be covered. Good redundancy.

## The NEW advanced docs — could a beginner follow them?

This was the round-2 test. Verdict per doc:

### `resources/what-cd-costs.md` — **followable**

Honestly my favorite new doc. Plain language, no undefined jargon. The one term
that could trip a junior — "stale-flag check" — is explained in place (L67–69:
"Flags past their removal condition… **fail or warn the pipeline**"). "Interruption
tax," "review-turnaround commitment," "velocity dip" are all either self-evident
or explained. I followed every line. No new unexplained jargon.

### `resources/governance-and-compliance.md` — **mostly followable**

Aimed at the Engineering Leader, not me, and it says so (L6–8). I still followed
it. It introduces two terms I didn't know:

- **Segregation of duties** — but it *defines it in the same sentence* ("the
  person who writes a change can't be the one who ships it unchecked," L37–38).
  Good — taught, not assumed.
- **Break-glass** — also defined in place (L72–74, "a documented emergency path
  for when the normal flow genuinely can't run") with a clear bulleted procedure.
  Taught, not assumed.

So no *unexplained* jargon — both hard terms are glossed inline. Neither is in the
glossary, which is fine since they're defined where used, though "break-glass" is
a memorable enough term that a glossary line wouldn't hurt.

### `resources/communicating-releases.md` — **followable**

This one's clearly written for the release manager, but as a junior I followed the
argument completely: deploy ≠ release, so notes anchor to the flag flip, not the
deploy log. "Flag inventory," "customer-facing label," "changelog" are all
explained as introduced. "Canary" and "dark launch" appear (L84–86) but both are
already in the glossary, and dark launch was taught in Session 2. No new
unexplained jargon for me.

### `sessions/session-3/examples/strangler-fig-violations.md` — **partially followable; this is where it lost me**

This is the most advanced doc in the whole course, and it's where the revision
out-ran my level. I could follow the *narrative* — put a seam in front, build
beside, move across in reversible steps, delete last — and the one-line pattern
(L38–43) and the slice table (L52–61) are genuinely well-structured. But several
mechanics are stated as if I already know them, and **none of these is in the
glossary**:

- **"idempotent"** — used at L57, L74, L83 ("idempotent by key," "Make every
  write **idempotent** on `violationId`"). I half-know this means "running it twice
  is safe," but the doc never says so. For a junior this is a real gap — it's
  load-bearing for understanding why the backfill is safe. **Exact sentence that
  lost me:** *"A one-off, resumable job copies historical violations from SQL into
  DynamoDB, using the same `violationId` so re-runs are idempotent."* (L57) — I
  understood *that* re-runs are safe, but not *why* "idempotent" guarantees it.
- **"system of record" / "authoritative"** — L56, L78 ("SQL Server remains the
  system of record," "which store is authoritative *at each step*"). I inferred
  "the store that's the source of truth," but it's never defined, and the whole
  dual-write window's safety argument hinges on it.
- **"shadow reads"** — L58 ("Reads still served from SQL, but a sampled copy is
  also fetched from the new service and the results compared/logged"). This one
  the doc *does* explain in the same row ("measuring parity, not trusting it yet"),
  so I got it — but "shadow" as a term is assumed.
- **"reconciliation"** — L78–80 ("Add a **reconciliation** check that flags
  divergence"). Inferable from context (re-checking the two stores agree) but not
  defined.
- **"watermark" / "watermarked by timestamp"** — L83. I have no idea what a
  watermark is in this context. **Exact phrase that lost me:** *"A resumable,
  idempotent job (keyed by `violationId`, watermarked by timestamp)…"* (L83) — two
  undefined terms in one parenthetical.
- **"seam"** — used throughout; the one-line pattern (L38) introduces it as "Put a
  **seam** in front of the capability," which is *almost* a definition but assumes
  I know a seam is an interception/routing point. The decompose exercise also now
  asks me to "Name the seam" (`exercises/decompose-a-branch.md` L108) — so it's a
  term I'm expected to *use*, not just read.

I want to be fair: this doc is explicitly framed as "Continuous Delivery applied
to the **hardest** case we have, not the easiest one" (L7–8), and it's an *example*,
not a core session. A junior is not the target reader. But the course now links to
it from page one of the README and from Session 1, and the decompose exercise sends
me here for the brownfield case — so a beginner *will* arrive here, and when I did,
five terms went undefined. If even three of them (idempotent, system of record,
watermark) got a glossary line, I'd have followed the whole thing.

## Exercises — my re-attempt

### Current-state assessment

The big round-1 change for me: **I can now answer CI 6** ("new work does not break
delivered work"). Last round I marked it "Partial" while guessing, because the
phrase depended on backward-compatible / expand/contract, which were undefined. Now
that expand/contract is in the glossary and bundled with backward-compatible, I
understand the row and can reason about it. That's a direct, traceable win from the
glossary fix.

Still open: I'm a junior. I still can't pull the Part-2 baseline numbers
(deployment frequency, lead time, change failure rate, time to restore) — those
live in GitLab MR history and incident records I don't have. The exercise still
says "Pull the numbers; don't guess" (L46) without telling a solo IC what they
*can* do alone (point 8 above). The new "Mixed estate?" note (L15) helped me think
about *which* system to score, but didn't address my access problem.

### Decompose a branch

Round 1 this was the strongest piece for a junior, and it's even better now. Two
improvements I felt directly:

1. The **brownfield worked example** (`exercises/decompose-a-branch.md` L87–99) is
   new and excellent — it shows expand/contract on an *existing* service with live
   data, step by step (expand → dual-write → backfill → flag → shadow → flip →
   contract). Round 1 I noted that a junior with a *harder* data change wouldn't
   know how to break it into expand/contract steps from the sessions alone. This
   example fixes exactly that. I re-did my round-1 feature (add an optional
   middle-name field) and could now also reason about a *replacement* change, not
   just an additive one.
2. expand/contract is now defined in the glossary, so when the exercise uses it
   (L52, L108) I'm not stranded.

The one carryover: the brownfield example and the strangler-fig doc both use
"shadow" and "dual-write" — I followed them here because the exercise explains
dual-write in context, but the strangler-fig doc assumes more (see above).

## Where it lost me / what's still open

1. **strangler-fig-violations.md assumes 5 terms a junior doesn't have**
   (idempotent, system of record, shadow reads, reconciliation, watermark). This
   is the new material out-running me. Not fatal — it's the hardest doc and I'm not
   its audience — but the course now routes beginners to it.
2. **"trunk" still isn't glossed at first use in Session 1** (`session-1` L37).
   The glossary and README note soften it, but the in-order reader still tab-flips.
3. **No IC/junior note on the current-state assessment.** A solo junior still
   can't tell what they're expected to fill in alone.
4. **backward-compatible has no standalone glossary entry** (it's only bundled
   into expand/contract). Minor now that expand/contract exists.

## New jargon the revisions introduced without defining it (my canary report)

The new docs added advanced vocabulary. Most was defined in place; these were not,
and a beginner who is routed to these docs will hit them:

| Term | Where | Defined where used? | In glossary? |
| ---- | ----- | ------------------- | ------------ |
| idempotent | `strangler-fig-violations.md` L57, L74, L83 | No | No |
| system of record / authoritative | `strangler-fig-violations.md` L56, L78 | No | No |
| watermark / watermarked | `strangler-fig-violations.md` L83 | No | No |
| reconciliation | `strangler-fig-violations.md` L78 | Loosely (context only) | No |
| shadow reads | `strangler-fig-violations.md` L58 | Yes (same row) | No |
| seam | `strangler-fig-violations.md` L38; exercise L108 | Almost (one-line) | No |
| segregation of duties | `governance-and-compliance.md` L37 | **Yes (inline)** | No |
| break-glass | `governance-and-compliance.md` L72 | **Yes (inline)** | No |
| stale-flag check | `what-cd-costs.md` L67; `session-2` L98 | **Yes (inline)** | No |

The governance and cost docs did this *right* — they define their hard terms where
they use them. The strangler-fig doc is the outlier: six terms, only one defined in
place.

## Factual / technical concerns

Same caveat as round 1 — I can't fact-check AWS/GitLab/SQL mechanics with
authority. One beginner-seat observation carries over: I still didn't see a loud
"illustrative, won't run as-is" banner on `sessions/session-3/examples/.gitlab-ci.yml`
(I noted in round 1 that `violations-api/README.md` has a helpful one and the YAML
should match). Nice-to-have, not a blocker, and I didn't re-verify the YAML this
round.

## Recommendations

### High priority

1. **Add the strangler-fig doc's load-bearing terms to the glossary:**
   **idempotent**, **system of record**, **watermark**. One line each. These are
   the terms that actually stopped me, and the course now routes beginners to that
   doc from page one. (`resources/glossary.md`)

### Medium priority

2. **Gloss "trunk" at first use in Session 1** — `sessions/session-1/README.md`
   L37 / L127: "an always-deployable trunk (`main`)". The exact round-1 ask, still
   open in S1.
3. **Add a one-line glossary entry for "seam"** and "reconciliation" — both are
   used in the brownfield exercise a junior is asked to *do*.
4. **Add an IC/junior note to the current-state assessment** (round-1 carryover):
   "If you're an individual contributor, fill the rows about your own daily habits
   and bring the access-dependent rows — DORA numbers, releasability ownership — to
   your lead."

### Nice to have

5. **Give "backward-compatible" its own short glossary line** (currently only
   bundled into expand/contract).
6. **A one-line "strangler fig" glossary stub** pointing to the worked example, so
   the first-page reader has an anchor without a click.
7. **The "won't run as-is" banner on `.gitlab-ci.yml`** (round-1 carryover).

## Verdict

**Champion — and the beginner's asterisk is much smaller this round.** Every one
of my top clarity bugs from round 1 is fixed: expand/contract, smoke test, DORA,
and the README anchor note are all in. The core course is now genuinely
followable in order for a junior, and the broken "see the glossary" promise is
closed. The only place the revision out-ran me is the new strangler-fig example —
the hardest, most advanced doc in the course — which assumes a handful of terms
(idempotent, system of record, watermark) a junior won't have. That's a narrow,
fixable gap in clearly-labeled advanced material, not a problem with the spine of
the course. **9/10, up from 8** — and I'd hand the next junior a much shorter
sticky note than last time: just "idempotent = safe to run twice; watermark =
a marker of how far a job got; seam = the spot you intercept to reroute."
