# Continuous Delivery 101 — Review 1

**Student:** Tex (AI Champion, RealManage)
**Stance going in:** AI now writes nearly all our code. The engineer's job has moved from turning bolts to designing the factory that turns them. So my question for *every* CD practice is: does it matter more, less, or differently when a machine is the author — and does this course even see that the author changed?
**Review date:** 2026-06-19
**Overall rating:** 8.5/10 — would I adopt/champion this?

## Executive summary

This is a strong course, and — to my genuine surprise — it is the *right* course
for an AI-first org even though it never once mentions AI. CD as taught here is
overwhelmingly authorship-agnostic: small batches, an always-deployable trunk,
an automated definition of deployable, build-once-promote, expand/contract, and
fail-forward are exactly the safety rails you need *more* of when code volume
explodes. The course's central thesis — **"automate the verdict," the pipeline
decides releasability, not a meeting** — gets *stronger*, not weaker, in my
world. That thesis is the factory's quality control, and when the bolts get
turned ten times faster, you need the line inspector to be a machine.

But "right course, never says it" is itself the finding. The course's worldview
is implicitly one human, one keyboard, days-long branches, a 4-hour human review
SLA. Three places that human-authorship assumption quietly load-bears: **review
as segregation of duties** (the governance story assumes a competent human
reviewer who eyeballs the diff — that breaks when the diff is 1,200 AI-written
lines a day), **tests-as-definition-of-deployable** (the course never warns that
when the same agent writes the code *and* the tests, the tests can be gamed into
passing themselves), and **flag debt** (the course's flag-explosion mechanism is
excellent but is sized for human-pace flag creation). None of these break CD —
they make CD *more* load-bearing — but the course should say so out loud, and it
should hard-link to AI 101 in both directions. Right now the two courses are
strangers that happen to teach two halves of the same job.

## Section-by-section (through the AI-authorship lens)

### Course framing (`README.md`, `CLAUDE.md`)

The README already links to AI 101 once, in "Internal References":
*"[AI 101: Claude Code] — using Claude Code to write tests, pipelines, and IaC"*
(`README.md:210`). That is the *only* acknowledgment in the entire course that
the code, tests, and pipelines it discusses might be machine-written. And it's
buried in a footer list. The course's mental model of the author is visible all
over the framing — "Tech Leads responsible for a team's branching workflow,"
"Software Engineers building the new services," the 4-hour human review target —
and the author is always a person.

That's not wrong, it's *incomplete*. The factory view: this course is the spec
for the assembly line. AI 101 is the spec for the robots that work the line.
Today they're written as if the other doesn't exist. At RealManage, where AI
writes nearly all the code, the line and the robots are the *same factory* and
the courses should reference each other as such — not as a "see also."

A second framing observation, and a point in the course's favor: the philosophy
list in `CLAUDE.md` and the README pro-tips are almost perfectly chosen for an
AI-first shop *by accident*. "Automate the verdict. If a human meeting decides
releasability, you don't have CD yet" (`README.md:223`) is the single most
important sentence in the course for my world, and it's already there. The
course just doesn't know *why* it's so important now.

### Session 1 — Why CD & the Minimums

The self-reinforcing trap (`session-1/README.md:47-72`) and "batch size is the
master variable" are pitched at human-pace delivery: *humans* deploy rarely
because *humans* fear big batches. Flip the actor to AI and the argument doesn't
weaken — it inverts into something more urgent. **AI removes the natural brake on
batch size.** A human author self-limits: writing 1,200 lines is a day's grind,
so batches stay human-sized by sheer friction. An agent will happily emit 1,200
plausible lines before lunch. The friction that *used* to cap batch size is gone.
So "small batches" stops being a discipline you adopt to go faster and becomes a
discipline you *impose* to stop the firehose from drowning your trunk and your
reviewers. The course teaches small batches as a virtue; in my world it's a
*containment strategy*. Same practice, much higher stakes. The course should say
this.

The DORA section (`session-1/README.md:88-97`) is untouched by AI and remains
correct — throughput and stability still aren't a trade-off. Worth noting one
quiet risk the course doesn't flag: AI inflates *deployment frequency* and
*lead time* trivially (it writes faster), so those two metrics can look great
while *change failure rate* silently climbs from plausible-but-wrong code. In an
AI-first org, change-failure-rate and time-to-restore become the metrics that
actually matter, because the speed metrics are now cheap to game. The course
treats all four DORA metrics as equally informative; under AI authorship they
are not.

The CD-vs-Continuous-Deployment distinction (`session-1/README.md:101-116` and
`examples/cd-vs-continuous-deployment.md`) is *more* important in my world, not
less. The temptation in an AI-first shop is to go straight to "let the agent's
green commits auto-ship" — full Continuous Deployment — because the agent is
fast and the pipeline is green. The course's insistence that you earn that, per
service, from a position of trust, is exactly the brake an over-eager AI shop
needs. Keep it. (And the course already frames it well — I'd just add one line:
"this matters even more when the author is an agent.")

### Session 2 — Trunk-Based Development & CI

This is where AI authorship bites hardest, and where the course is silent.

**Trunk-based / short-lived branches:** MORE important. An agent can keep a
branch coherent indefinitely — it doesn't get tired, doesn't lose the thread —
so the *old* reason for short branches (human cognitive drift) weakens, but the
*real* reason (a long branch is a big batch, and trunk drift makes the eventual
merge a roll of the dice) gets *worse*, because the agent has been generating
volume the whole time. The course's reframe — "branches so short they never get
the chance to drift" (`session-2/README.md:38`) — holds perfectly. I'd add: when
an agent is the author, the < 1-day rule is the throttle that stops it from
piling up a week of unreviewed machine output.

**Feature flags + deploy≠release:** MORE important AND a new failure mode. Flags
are the right tool to merge AI's incomplete work to trunk safely — the course is
correct. But the course's own `feature-flag.ts` cost note ("one feature spawning
four interacting flags is usually a *decomposition* failure," echoed in
`what-cd-costs.md:62`) is sized for *human* flag creation. An agent told to "ship
this behind flags" will cheerfully mint a flag per slice, per branch, per
experiment, with no instinct that the inventory is becoming unmanageable. **Flag
explosion is the default AI behavior, not the exception.** The course's mechanism
— flag inventory + birth certificate + CI stale-flag check
(`what-cd-costs.md:59-81`, `feature-flag.ts:95-106`) — is genuinely excellent and
is *exactly* what saves you here, because it's mechanical, not discipline-based,
and machines respect mechanical gates. The course should explicitly call out that
this mechanism is what makes AI-generated flags survivable, and that the
stale-flag check should be tuned tighter when the author is an agent.

**CI quality gates — the load-bearing crack.** Here is my sharpest concern, and
the course does not address it anywhere. The course frames tests as the
*definition of deployable* (`session-2/README.md:117-127`,
`ci-pipeline.gitlab-ci.yml`, and the lovely `handler.test.ts` that "is the
definition of deployable in action"). That framing is correct and gets *more*
important under AI — the pipeline is the only verdict that scales. **But the
course assumes the test is an independent specification of intent.** When the
*same agent* writes the implementation and the tests in the same session, that
assumption fails: the agent can (and routinely does) write tests that assert
whatever the code happens to do, achieve the coverage threshold, go green — and
verify *nothing*. Coverage thresholds (`ci-pipeline.gitlab-ci.yml:78-80`) make
this *worse*, because "hit 80%" is a target an agent optimizes for directly;
Goodhart's law with a robot. The course's `handler.test.ts` is a model of a *good*
test (it tests behavior contracts and the flag-off/flag-on boundary, exactly
right) — but nothing in the course teaches *why* it's good or warns that an agent
left to its own devices won't produce that. This is the AI-first org's single
biggest CD risk and the course is mute on it. (It's also precisely where this
course must hand off to AI 101 / BDD 101, where tests-as-spec and TDD red-green
discipline live.)

### Session 3 — The Pipeline

Almost entirely authorship-agnostic, and that's a feature. Build-once-promote
(`session-3/README.md:67-92`), immutable artifacts, config-with-artifact, OIDC,
production-like qa — none of this cares whether a human or an agent wrote the
source. The provenance chain the course builds (commit → pipeline run →
SHA-tagged artifact → deploy) is, if anything, *more* valuable when the author is
a machine, because it's the only audit trail you've got: you can no longer ask
"the engineer who wrote this" what they intended — there may not have been one in
any meaningful sense. The pipeline-as-audit-log story in
`governance-and-compliance.md:53-68` is the right answer to "who's accountable
for the robot's code" and the course should connect those dots.

**Fail-forward-first** (`session-3/README.md:101-118`, `rollback-on-aws.md`):
MORE important and beautifully suited to AI. The whole argument — shipping a fix
is small, fast, canary-verified, so fix forward by default — is *amplified* when
an agent can produce the forward fix in minutes. AI makes the forward fix cheaper,
which makes fail-forward even more clearly the right default. The data-trap
caveat (rolling code back doesn't roll data back) is unchanged and correct. One
addition I'd want: the course should note that an AI-generated forward fix needs
the *same* gates as any change — the temptation under pressure is to let the agent
hot-fix straight to prod because it's fast, which is exactly the side-door the
pipeline-is-the-only-path rule (`session-3/README.md:29-37`) forbids. AI makes
that temptation stronger. Restate the rule for the AI case.

**Governance / segregation of duties:** This is the practice that *changes
meaning* most under AI, and the course's current story has a gap. The SoD
argument (`governance-and-compliance.md:36-50`) is: the author can't merge
unreviewed; a second person approves the MR; the pipeline (not the author)
deploys. Clean — when the author is human. But when the *author is an agent*,
"the author can't approve their own MR" gets philosophically slippery: if a human
prompts an agent, the agent writes the code, and the same human approves the MR —
is that segregation of duties, or is it one human rubber-stamping a machine's
work they didn't write and may not fully understand? The control still *exists*
(reviewer ≠ committer can still be enforced), but the *substance* of review —
"a competent second human actually understood this change" — is what's at risk,
because the volume and plausibility of AI output makes genuine review harder. The
course's governance resource is otherwise the best chapter for my world; it just
needs to confront the AI-author case head-on.

### Resources

- **`what-cd-costs.md`** — the best resource in the course for my lens, and it
  doesn't know it. The flag-debt mechanism is the template for *every* AI-scale
  problem: don't rely on discipline, make it mechanical, let CI enforce it. That
  philosophy is the whole answer to "how do you keep AI output safe at volume."
  The "interruption tax" framing of daily integration (`what-cd-costs.md:30-34`),
  though, is a pure human-cost — an agent doesn't experience interruption. Under
  AI the cost of daily integration largely *evaporates*; the resource should note
  that AI removes some of CD's costs even as it raises the stakes on others.
- **`governance-and-compliance.md`** — strong; see SoD note above. The
  "paragraph to hand an auditor" (`:99-112`) should, in an AI-first org, also
  answer "and who is accountable when an AI wrote it?" — the answer is the
  reviewer + the pipeline evidence, but the course should say it.
- **`minimums-reference.md`, `glossary.md`, `migration-checklist.md`,
  `troubleshooting.md`, `communicating-releases.md`** — all authorship-agnostic
  and fine as-is. The migration checklist's Phase 0 ("inventory in-flight
  long-lived branches") is, amusingly, even more relevant when an agent has been
  generating branch content unsupervised.
- **`strangler-fig-violations.md`** — excellent worked example, fully
  authorship-agnostic. Worth noting: AI is *extremely* good at the rote parts of a
  strangler migration (writing the dual-write shim, the idempotent backfill job,
  the reconciliation check). This is where "human designs the factory, AI turns
  the bolts" is most literally true — the human names the seam and the slices
  (`strangler-fig-violations.md:52-65`); the agent writes each slice. The course
  could use this example to show the division of labor explicitly.

### Exercises — my attempt

**Current-state assessment** (`exercises/current-state-assessment.md`): I scored
an imagined AI-first squad. CI minimums 3 and 4 (automated tests before/after
merge) scored **Partial** for a reason the scorecard can't currently capture: the
tests *exist* and *run*, but a meaningful fraction were AI-written to pass
AI-written code, so "tests run" is true while "tests verify intent" is not. The
scorecard has no row for *test quality / independence*, which under AI authorship
is the row that matters most. I'd add a question: *"When the same author (human or
AI) writes code and its tests, what independent check confirms the tests specify
intent rather than mirror the implementation?"* The "median MR size" metric
(`:55`) is also quietly broken for AI: MR size in lines is no longer a proxy for
effort or risk when the author types at machine speed — a 600-line AI MR is not
"big" in author-effort terms but is enormous in review-burden terms. The metric
needs reframing around *review burden*, not author effort.

**Decompose a branch** (`exercises/decompose-a-branch.md`): The greenfield
Violations decomposition went fine, and here's the AI-first twist — *this exercise
is now the human's primary job.* In a factory where AI turns the bolts, the
human's value-add is exactly this: naming the seam, the slices, the flags, the
expand/contract steps. The decomposition *is* "designing the factory." The
exercise is well-built for that, but it's framed as "how to ship without a long
branch" rather than "this is the durable human skill once AI writes the code." A
one-paragraph reframe would make it land for an AI-first audience: the agent can
write any slice; deciding *what the slices are* is the part that's still yours.

## Where it lost me / objections it didn't answer

1. **The author changed and the course didn't notice.** Every worked artifact is
   written as if a careful human typed it. In an org where AI writes nearly all
   the code, that silence isn't neutral — it leaves the reader to figure out, on
   their own, the three places (review substance, test-gaming, flag explosion)
   where AI authorship stresses the practices. That's the same critique Sam, Dana,
   and Jordan made about the monolith in round 1 ("named and dodged"): the course
   asserts its practices are universal but only demonstrates one world. My world
   (machine authorship) is the *other* unaddressed world.

2. **"Automate the verdict" is the right thesis but the course doesn't follow it
   to its AI conclusion.** If the pipeline is the only verdict that scales — and
   it is — then in an AI-first org the *quality of the gates is everything*,
   because the gates are now the only thing standing between plausible-but-wrong
   machine output and prod. The course treats the gates as a solved checklist
   (lint, test, coverage, scan). Under AI, the *adversarial robustness* of those
   gates — can the author game them? — is the open question, and it's unasked.

3. **No answer to "who reviews the AI's code?"** The SoD story assumes human
   review carries real load. The course needs a position on what review *means*
   when a human can't eyeball every line because there are too many and they all
   look plausible. My position: review shifts from line-reading to *spec-checking
   and gate-trusting* — you review the intent and the tests-as-spec, and you trust
   the pipeline for the rest. That's a defensible CD-native answer. The course
   should state it.

## Confusing or assumed (clarity)

Nothing was confusing — the course is clear and well-written. The "assumed" part
is the whole point of this review: **human authorship is assumed everywhere and
stated nowhere.** A reader from an AI-first team will not be confused; they'll
just be quietly under-served, and won't know it until an agent games a coverage
threshold in production.

## Factual / technical concerns

No new technical errors beyond what round 1 already caught (the OIDC export and
the canary alarm, both noted as fixed in the synthesis — and indeed the current
`.gitlab-ci.yml:55-66` and `template.yaml:112-154` reflect the fixes). On my
lens specifically, the technical content is sound; my concerns are about
*coverage of the AI case*, not correctness.

One forward-looking note, not an error: the course rightly treats `npm audit` as
advisory (`.gitlab-ci.yml:93-100`). In an AI-first shop the *more* important scan
is one the course doesn't mention — AI routinely hallucinates plausible-but-wrong
or even non-existent dependencies ("slopsquatting" risk). A dependency-provenance
/ lockfile-integrity check belongs in the definition of deployable once AI writes
the `package.json`. Flag for the AI-and-CD resource, below.

## AI authorship × each CD practice (the persona table)

| CD practice | Under AI authorship | Why |
| ----------- | ------------------- | --- |
| **Small batches / daily integration** | **MORE important (changes from virtue to containment)** | AI removes the natural friction that capped batch size. Small batches go from "discipline to go faster" to "throttle to not drown the trunk and reviewers." |
| **Trunk-based dev / short branches** | **MORE important** | Agents can sustain a long branch indefinitely, so they pile up volume; the < 1-day rule is the throttle on unreviewed machine output. |
| **Pipeline AS definition of deployable** | **MORE important — the thesis gets stronger** | When humans can't eyeball every line, the automated verdict is the *only* verdict that scales. This is the course's best idea and AI makes it essential. |
| **Feature flags + deploy≠release** | **MORE important + new failure mode** | Right tool for merging AI's incomplete work safely — but flag explosion is AI's *default* behavior. The course's mechanical flag-inventory + stale-flag CI check is what saves you; tune it tighter. |
| **Expand/contract** | **Unchanged (authorship-agnostic)** | A schema-safety discipline; the agent executes it once a human specifies the steps. The "design the factory" work. |
| **Build-once-promote / immutable artifacts** | **Unchanged, slightly MORE valuable** | Provenance (commit→run→SHA→deploy) is the only audit trail when there's no human author to interrogate. |
| **Fail-forward-first** | **MORE clearly correct** | AI makes the forward fix cheaper/faster, strengthening it as the default. Caveat: same gates apply — don't let the agent hot-fix around the pipeline. |
| **Governance / SoD via MR review** | **CHANGES MEANING (the deepest shift)** | "Author ≠ approver" still enforceable, but the *substance* of review erodes when the diff is large, plausible, and machine-written. Review shifts from line-reading to spec-checking + gate-trusting. |
| **Flag-debt mechanism** | **MORE important, well-built already** | Mechanical enforcement (not discipline) is exactly right for machine-pace flag creation. Best-positioned practice in the course for AI. |
| **CI tests as quality gate** | **MORE important AND newly vulnerable** | Tests-as-spec scales the verdict — but when one agent writes code + tests, tests can be gamed to pass themselves. Coverage thresholds become Goodhart targets. The course's biggest blind spot. |

## Where the course is silent on AI / what's missing

1. **Test independence under same-author code+tests.** The single most important
   gap. No warning that AI-written tests against AI-written code can verify
   nothing; no guidance on coverage-as-Goodhart-target; no link to TDD/BDD
   discipline as the mitigation.
2. **Review under AI authorship.** No position on what MR review *means* when a
   human can't read every line. SoD story assumes a load-bearing human reviewer.
3. **Flag explosion as AI's default.** The mechanism exists; the framing that
   "machines need the mechanism more than humans do" doesn't.
4. **Batch size as containment, not just virtue.** The "AI removed the brake"
   inversion is unstated.
5. **DORA metric skew.** Speed metrics get cheap to game; failure/restore metrics
   become the real signal. Unstated.
6. **Dependency provenance / hallucinated-package risk** in the definition of
   deployable. Unmentioned.
7. **No bidirectional link to AI 101.** This course never tells the reader "the
   code you're delivering is probably AI-written — here's how to write it well
   (AI 101) and verify it (BDD 101)." AI 101 (`../ai-101-claude-code/`) likewise
   teaches AI to write code/tests/pipelines with no mention that there's a
   delivery discipline (this course) that catches what the agent gets wrong. The
   two halves of the AI-first job don't reference each other.

## Recommendations

### High priority

1. **Add a resource: `resources/ai-assisted-delivery.md` ("CD when AI writes the
   code").** This is the cleanest fix — one new resource, linked from Session 2
   (CI gates) and Session 3 (governance), mirroring how `what-cd-costs.md` and
   `governance-and-compliance.md` were added in round 1. Sketch of contents:
   - **Heading: "The author changed; the safety rails matter more."** One
     paragraph: AI removed the friction that used to cap batch size and pace flag
     creation, so CD's containment practices go from helpful to essential. The
     factory framing if you like it: humans design the line and name the slices;
     agents turn the bolts; the pipeline is quality control.
   - **"Why 'automate the verdict' is now non-negotiable."** When you can't
     eyeball every line, the pipeline is the only verdict that scales. Tie back to
     `README.md:223`.
   - **"The test-gaming trap."** The core warning: same-author code+tests can be
     green and meaningless; coverage thresholds become targets an agent optimizes
     directly. Mitigation: tests must specify *intent/behavior contracts* (point
     at `handler.test.ts` as the model — flag boundaries, not implementation
     mirroring); have a human or a *separate* agent review the tests against the
     spec; treat BDD/Gherkin acceptance criteria as the independent spec. Link to
     BDD 101 and AI 101.
   - **"Review when you can't read every line."** State the CD-native answer:
     review shifts to spec-checking + gate-trusting; enforce reviewer ≠ author in
     the tool (`governance-and-compliance.md` already covers the mechanics); the
     pipeline + SHA-tagged artifact is the accountability record when there's no
     human author to interrogate.
   - **"Flag explosion is the default, not the exception."** Point at the existing
     flag-inventory + CI stale-flag mechanism (`what-cd-costs.md`,
     `feature-flag.ts`) and say plainly: this mechanism is what makes AI-generated
     flags survivable; tune the stale-flag check tighter for agent-paced creation;
     a flag explosion is still a *decomposition* (i.e. human-design) failure.
   - **"Metrics that still mean something."** Speed metrics (frequency, lead time)
     are cheap for AI to inflate; watch change-failure-rate and time-to-restore.
   - **"Dependency provenance."** Add lockfile-integrity / dependency-provenance
     to the definition of deployable; name the hallucinated-package risk.
   - **"What doesn't change."** Honest accounting (mirroring `what-cd-costs.md`'s
     "what you don't pay"): expand/contract, build-once-promote, immutable
     artifacts, OIDC, fail-forward — all authorship-agnostic and unchanged. CD is
     genuinely *mostly* AI-ready; this resource is about the three seams, not a
     rewrite.

2. **Bidirectional cross-links with AI 101 (and BDD 101).** In CD 101: add a
   first-class callout (not a footer link) early in Session 2 and in the new
   resource: "At RealManage, most of this code is AI-written — see AI 101 for
   *how to write it* and BDD 101 for *how to specify it as tests the pipeline can
   trust*." In AI 101: a reciprocal callout that the code AI writes still has to
   survive the delivery discipline in CD 101. This is a content change to the
   sibling course but it's the one that makes the pair coherent for an AI-first
   org.

### Medium priority

3. **Two surgical sentences in existing sessions**, so the resource isn't the
   only place the AI case appears:
   - Session 1, after the batch-size section: "When an agent writes the code, the
     friction that used to keep batches small is gone — so small batches become a
     deliberate throttle, not just a good habit."
   - Session 2, in the CI-gates section: "When the same author writes the code and
     its tests, the tests can pass without verifying anything. The gate is only as
     honest as the tests behind it — see [AI-assisted delivery]."

4. **Patch the two exercises for AI authorship.**
   - `current-state-assessment.md`: add a test-independence question and reframe
     "median MR size" toward *review burden* rather than author effort.
   - `decompose-a-branch.md`: one paragraph reframing decomposition as the durable
     human skill once AI turns the bolts — "the agent can write any slice;
     deciding what the slices are is still yours."

### Nice to have

5. In `strangler-fig-violations.md`, a short sidebar showing the human/agent
   division of labor explicitly (human names seam + slices + expand/contract; agent
   writes the dual-write shim, the idempotent backfill, the reconciliation check).
   It's the most literal "design the factory / turn the bolts" example in the
   course.

6. In `what-cd-costs.md`, one line in "what you don't pay": AI removes some of
   CD's human costs (the daily-integration interruption tax, slow reviews) even as
   it raises the stakes on gate quality. Honest accounting cuts both ways.

## Verdict

Champion — enthusiastically, with one condition. CD as taught here is the right
discipline for an AI-first org and its core thesis ("automate the verdict") gets
*stronger* when machines write the code; but the course must stop being silent
about the author, add one `ai-assisted-delivery.md` resource naming the three
seams (test-gaming, review substance, flag explosion), and link arms with AI 101
in both directions — because at RealManage, designing the delivery factory and
programming the robots that work it are the same job, and right now the two
courses teach it as if they're not.
