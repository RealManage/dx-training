# Continuous Delivery 101 — Review 1

**Student:** Felix (Junior Developer, 2 yrs)
**Stance going in:** Eager and want to do this the right way, but a lot of the
vocabulary is brand new to me. I've shipped code on a weekly process; I've never
thought hard about *how* we deliver. I'm here as a clarity detector — when a term
shows up before it's explained, I get lost, and I'm going to say so.
**Review date:** 2026-06-19
**Overall rating:** 8/10 — yes, I'd adopt this, and I mostly understood it

## Executive summary

This course taught me more real, usable concepts in three sessions than two
years of "merge it Friday" ever did. The writing is plain, the examples build on
each other, and most of the scary-sounding terms (CI vs CD vs Continuous
Deployment, feature flags, immutable artifacts, deploy vs release) are genuinely
*taught*, not assumed — and there's a glossary that rescued me several times.
Where it lost me: a few terms are used as if I already know them and the glossary
doesn't have them (**expand/contract** is the worst offender — it's used five
times and even appears in a "see the glossary" pointer, but it's *not in the
glossary*). And the very first page assumes I know what a "trunk," "strangler fig
pattern," and "DORA" are before anything defines them. For a "101" course aimed
partly at people like me, the front door is jargon-heavy even though the rooms
inside are well-lit.

## Section-by-section

### Course framing (README)

This is where I struggled the most, ironically, because it's the first thing I
read. In the first two paragraphs (`README.md` lines 10–12) I hit:
**Continuous Delivery**, **MinimumCD**, **strangler fig pattern**, **cloud-native**,
**Lambda / ECS / DynamoDB / SNS / SQS**, **monoliths**, **trunk-based development**,
**feature flags**, **expand/contract**, **immutable artifacts**, **DORA metrics**,
**OIDC**, **canary deploy**, **definition of deployable** — all in the "What You'll
Learn" list (`README.md` lines 18–25). I get that a learning-objectives list is a
preview, so I didn't *need* to understand them yet. But several are stated as if
they're common knowledge ("we're applying the **strangler fig pattern**"), and a
beginner can't tell which ones are "you'll learn this" versus "you should already
know this." The audience section (`README.md` line 36) says it assumes Git, basic
AWS, and GitLab CI/CD — that's fair and honest, and I appreciated it being
spelled out. The "Not a tool rollout" callout (line 94) landed cleanly for me:
even brand new, I understood "don't ask which product gives us CD."

**What helped:** the learning path diagram and the 3-row curriculum table
(`README.md` lines 100–104) gave me a map before the jargon buried me.

### Session 1 — Why CD & the Minimums

This is the section that actually *converted* me, and it did it without
vocabulary I didn't have. The self-reinforcing trap diagram (`session-1/README.md`
lines 49–62) is the single clearest thing in the course — "deploy rarely → each
deploy is large → large deploys are risky → deploy even less." I have *lived*
that loop and never had a name for it. The big-batch vs small-batch table (lines
76–83) made the whole argument click.

The CD vs Continuous Deployment section (lines 97–112) is excellent for a
beginner. The two-line ASCII diagram showing the *only* difference is the last
hop to prod (lines 104–107) is exactly the kind of thing I need — it turned two
words I'd have used interchangeably into two clearly different things. The worked
example `session-1/examples/cd-vs-continuous-deployment.md` reinforced it without
repeating it word-for-word, which I liked.

**First snags:**

- **"Trunk"** is used heavily ("integrates to the trunk," `session-1/README.md`
  line 121) before it's defined anywhere in the session. I *inferred* it means
  `main`, and the glossary confirmed it (`glossary.md` line 19), but Session 1
  never says so. A one-clause gloss the first time would have saved me a tab-flip.
- **DORA** (line 86) is introduced as "The DORA research (*Accelerate*)" and then
  four metrics are listed. I had no idea what DORA stands for or what it *is* (a
  research program? a company? a standard?). The glossary defines the four metrics
  individually but *also* never expands the acronym. See clarity list below.
- The litmus-test question (lines 144–146) is great and I could hold it in my head.

### Session 2 — Trunk-Based Development & CI

The feature-flag explanation here is the best teaching in the whole course for
someone at my level. The "Without flags / With flags" two-column block
(`session-2/README.md` lines 75–81) and the plain definition — "a runtime switch
that lets unfinished code live in `main` and in production, *turned off*"
(line 71) — made a concept I'd heard tossed around finally concrete. The deploy
vs release distinction right before it (lines 67–70) is clean: "Deploy = code is
in the environment. Release = the feature is visible to users." I will remember
that.

`feature-flag.ts` is a great read even for a junior — the comments explain *why*,
the default-off rule is stated and then *shown*, and the "flags are temporary /
flag debt is real debt" note (lines 95–113) is the kind of thing nobody told me
in two years. The anti-patterns example (`branching-antipatterns.md`) is
genuinely useful; the GitFlow `develop`-branch one (lines 30–36) described
exactly what my current team does, and I now understand why it's a problem.

**Snags:**

- **expand/contract** first appears here in the CI minimums table
  (`session-2/README.md` line 115): "Changes are backward-compatible
  (expand/contract for data)." It's dropped in with zero explanation, in
  parentheses as if it clarifies something — but it was the *most* confusing thing
  in the table for me. Then line 141 asks me to "share... how you handled any data
  change with expand/contract" in the workshop — I'd have had no idea how. Worst
  of all, line 168 literally points me to "[Glossary] — *feature flag*, *deploy*
  vs *release*, *expand/contract*" and **expand/contract is not in the glossary.**
  That's a broken promise to the exact reader who needs it.
- **"dark launch" / "ships dark"** (e.g. `feature-flag.ts` line 87, "ships to prod
  dark") — I guessed from context, and the glossary *does* define dark launch
  (`glossary.md` line 85), but the term is used in Session 2 before the glossary is
  formally introduced as a resource. Minor; context carried me.

### Session 3 — The Pipeline

Densest session, and the place where my AWS gaps showed — but the course
*warned* me AWS was assumed, so that's on me, not the course. The immutable
artifact / promotion diagram (`session-3/README.md` lines 71–77) plus the
"build once, promote the same bytes" line made the concept stick even though I've
never run `sam package`. The "if you rebuild for prod, you've deployed something
you never tested" sentence (line 79) is a perfect one-line justification.

The fail-forward vs rollback section (lines 97–114) is well-argued and I followed
the *reasoning* (fix forward is small and safe; rollback only defers the fix).
`rollback-on-aws.md` is clearly written and the "Choosing your move" table (lines
102–110) is beginner-friendly. The **rollback data trap** — "rolling code back
does not roll data back" (line 110, and `rollback-on-aws.md` line 39) — was a new
and slightly mind-bending idea for me, but it's *explained well enough* that I got
it. That's the course at its best: a hard idea, taught.

**Where my level showed (and where the course assumed knowledge):**

- The `.gitlab-ci.yml` example is heavily annotated, which helped, but `extends:`,
  `needs:`, `rules:`, `.aws_oidc` (the `.`-prefixed hidden job), `id_tokens`, and
  `assume-role-with-web-identity` (`session-3/examples/.gitlab-ci.yml` lines 52–61)
  are all GitLab/AWS mechanics I don't have yet. The course *says* GitLab CI is
  only "helpful, not required" (`README.md` line 82), so I won't dock it hard — but
  Session 3's workshop step 4 ("How OIDC gives each environment its own role,"
  `session-3/README.md` line 128) asks me to *locate* something in YAML I can
  barely read. A junior in the room would be quiet during that exercise.
- **smoke test** is used in the workshop ("Where you'd add a **smoke test**,"
  line 128) and all over the pipeline file, but it's never defined and it's not in
  the glossary. I *think* it means "a quick check that the deploy basically works,"
  and the YAML comment "prove the deploy actually works before promoting onward"
  (`.gitlab-ci.yml` line 138) basically confirms it — but a one-liner in the
  glossary would close the loop.
- The handler/test TypeScript (`violations-api/src/`) is honestly *reassuring* —
  dependency injection and pure-function testing are things I've seen, and the
  comments tie each choice back to "keeps the CI gate fast." Good for morale.

### Resources (minimums, glossary, checklist, troubleshooting)

- **`minimums-reference.md`** is the clearest statement of the whole course and I'd
  keep it open. The "We have a CI server, so we do CI — no" callout (lines 20–22)
  reframed something I believed wrongly.
- **`glossary.md`** rescued me repeatedly — trunk, immutable artifact, promotion,
  canary, OIDC, dark launch, deploy vs release, fail forward, manual gate were all
  there and all in plain language. This is a real strength of the course. But it
  has *gaps* that hit a beginner specifically: **no expand/contract**, **no smoke
  test**, **no strangler fig**, **no batch size... wait, yes batch size is there**
  (line 24, good), and **DORA is never expanded as an acronym** even though four
  DORA metrics are defined. Because the course explicitly sends me to the glossary
  for expand/contract, its absence is a bug, not a nice-to-have.
- **`migration-checklist.md`** — the five phases and the "Honest checkpoints"
  section (lines 92–100) are clear even to me, though they're aimed at someone
  running a migration, not a junior IC. That's fine; I know that's not my role yet.
- **`troubleshooting.md`** — the objections format is great. It uses expand/contract
  again (lines 79, 88) and at least line 88 *finally* explains it inline ("add the
  new shape, write to both, migrate, then remove the old"). That one sentence is
  what the glossary should have had from the start.

### Exercises — my attempt

#### Current-state assessment — my attempt

I tried to fill this out for the only thing I really know: my current team's
weekly-release web service. Here's how far a junior gets.

**Part 1 — Score the minimums.** I could answer the CI rows by gut:

| # | Practice | My answer | Why I could (or couldn't) answer |
| - | -------- | --------- | -------------------------------- |
| CI 1 | Trunk-based development | No | We keep feature branches alive for the whole sprint. |
| CI 2 | Integrate to trunk daily | No | We merge near release day. |
| CI 3 | Tests before merge | Partial | Some pipelines run tests; not all. |
| CI 4 | Tests on merged result | **Couldn't answer confidently** | I don't actually know what runs on `main` after merge — I've never looked. |
| CI 5 | Red build stops feature work | Partial | Sometimes we just keep going. |
| CI 6 | New work doesn't break delivered work | **Couldn't answer** — see below |

The CD table is where I stalled as a junior:

- **CD 3 "The pipeline decides releasability; its verdict is definitive."** I don't
  know who decides releasability on my team — I just get told "we're releasing
  Friday." The exercise assumes I have visibility into the *decision*, which a
  2-year dev often doesn't.
- **CD 4 "Artifacts meet an automated definition of deployable."** I now know what
  this *means* (thanks to Session 3), but I genuinely don't know what my team's
  pipeline checks. Not the exercise's fault — but a junior can't fill this in
  alone, which the exercise header (`current-state-assessment.md` lines 6–7,
  "Do this as a team, out loud") does acknowledge. Good that it says that.
- **CI 6 "New work does not break delivered work."** I marked this "Partial" but
  honestly I was guessing — the phrase "delivered work" and the parenthetical
  "(backward-compatible by default)" assume I understand backward compatibility at
  a deploy level, which expand/contract is the missing piece of. So my inability to
  answer this traces straight back to the undefined term.

**Part 2 — Baseline numbers.** I couldn't pull real numbers (deployment
frequency, lead time, change failure rate, time to restore) — those live in
GitLab MR history and incident records I don't have access to as a junior. The
exercise says "Pull the numbers; don't guess" (line 44), which is the *right*
instruction, but it quietly assumes the person filling it out has that access.
That's a "do it with your lead" exercise, and saying so explicitly would help.

**Part 3 / Part 4 — Constraint and pilot.** These are clearly lead-level
decisions ("Who owns the fix," "Pick your pilot"). I read them and understood
them, but couldn't *do* them. Again, not a flaw — just confirming this exercise
is team-shaped, not solo-junior-shaped.

**Verdict on this exercise:** I understood every *question* (a win — the
vocabulary was taught well enough that the scorecard made sense), but I could only
honestly answer the ones about my own daily habits. The blockers were
**access/role**, not **comprehension** — except CI 6, which was a comprehension
gap caused by the missing expand/contract definition.

#### Decompose a branch — my attempt

This one I could actually *do*, and it was the most satisfying part of the course.
I didn't use the provided Violations API; I picked a small real feature I could
imagine owning: **"add an optional middle-name field to the resident profile
form, save it, and show it on the profile page."** Tiny, but it has a UI bit, an
API bit, and a data bit — so it stress-tests the decomposition idea.

My slices:

1. Add `middleName` column to the data store as nullable / optional. *(Infra/data
   only, nothing reads or writes it yet. Visible-now, harmless.)*
2. API accepts and stores `middleName` if present; ignores it otherwise.
   *(Backward-compatible — old clients still work. Visible-now.)*
3. API returns `middleName` in the profile response. *(Additive field; old UI
   ignores unknown fields. Visible-now.)*
4. Add the input box to the form behind a `profile.middleName` flag, off.
   *(Behind-a-flag — users don't see it yet.)*
5. Show the middle name on the profile page, behind the same flag. *(Behind-a-flag.)*
6. Flip `profile.middleName` on in dev → qa → prod. *(Release decision, no deploy.)*

Six slices, each mergeable same-day. The exercise's "what good looks like" sample
(`decompose-a-branch.md` lines 68–82) was close enough to mine that I felt I'd
understood the skill. The labels "visible-now vs behind-a-flag" gave me a concrete
decision to make on each slice, which I liked.

**Where the instructions assumed knowledge I don't have:**

- **Part 2, "Where's the database change?" (line 50):** "A schema/shape change
  should use expand/contract: add new, write both, migrate, remove old." This is
  the *only* place in the whole course that expand/contract is actually defined
  inline — and it's buried in a pressure-test bullet of an exercise, not in
  Session 2 where the term first appears, and not in the glossary. I only
  understood my own slice 1 *after* reading this line. If I'd done the exercise
  before reading the troubleshooting guide, I'd have been stuck. **My data change
  (a nullable column) is so simple it's basically just the "expand" step** — but I
  only know that *now*. A junior with a harder data change (renaming a column,
  changing a type) would not know how to break it into expand/contract steps from
  anything taught in the sessions themselves.
- **"backward-compatible" (lines 27–30, 50):** used as a requirement for every
  slice. I sort of know what this means from the API world (don't break old
  callers), but the course never defines it for a beginner, and it's load-bearing
  for the whole decomposition. It's tightly coupled to expand/contract — define one,
  define both.

**Verdict on this exercise:** the *structure* of the exercise taught me the skill
even though one key term (expand/contract) was under-explained. I'd rate it the
strongest single piece of the course for a junior, because it's *do*-able solo and
the example to compare against is right there.

## Where it lost me / objections it didn't answer

I don't have strong opinions to defend — I'm a clarity detector, not a skeptic.
So instead of objections, here's where the *course assumed I'd keep up* and I
didn't:

1. **The README front door is jargon-dense for a "101."** By the time I finished
   the "What You'll Learn" list I'd met ~14 unfamiliar terms with no anchor for
   which were "to be taught" vs "assumed." A one-line "new to these terms? the
   sessions teach them; keep the glossary open" note at the top of the README would
   have lowered my blood pressure.
2. **The glossary is *almost* my safety net but has holes exactly where a beginner
   falls.** expand/contract, smoke test, strangler fig, and the DORA acronym are
   the four I reached for and didn't find (or found incomplete). Three of those are
   used in exercises I'm supposed to *do*.
3. **Some exercises assume role/access a junior doesn't have** (pulling DORA
   baseline numbers, knowing who owns releasability). The course says "do it as a
   team," which softens this — but it could say plainly: "if you're an IC, fill in
   the habit rows yourself and bring the rest to your lead."

## Confusing or assumed (clarity)

### Jargon & assumed knowledge (running list)

For each: where I *first* hit it, whether it was **taught**, **glossary-rescued**,
or **assumed (I had to already know it / had to guess)**.

| Term / concept | First hit | Status | Notes |
| -------------- | --------- | ------ | ----- |
| Continuous Delivery | `README.md` L10 | **Taught** (Session 1) + glossary L7 | Clearly distinguished from Continuous Deployment. Best-taught concept. |
| Continuous Integration (CI) | `README.md` L18 | **Taught** (S1 §4.1) + glossary L13 | The "CI server ≠ CI" callout fixed a wrong belief I held. |
| Continuous Deployment | `README.md` L102 | **Taught** (S1 §3) + glossary L10 | The "only difference is the last hop" diagram nailed it. |
| Trunk-based development | `README.md` L21 | **Taught** (Session 2) + glossary L16 | Well taught; the term *trunk* itself is used before defined (see below). |
| **Trunk** (= `main`) | `session-1` L121 | **Glossary-rescued** L19 | Used heavily in S1 before any definition; I inferred it, glossary confirmed. One inline gloss in S1 would fix it. |
| Feature flag | `README.md` L22 | **Taught** (Session 2, best in course) + glossary L82 | Definition + diagram + working `.ts`. Exemplary. |
| Deploy vs Release | `session-2` L67 | **Taught** + glossary L76–80 | Clean two-line definition. Stuck immediately. |
| Dark launch / "ships dark" | `feature-flag.ts` L87 | **Glossary-rescued** L85 | Guessed from context first; glossary backed it up. |
| **expand/contract** | `session-2` L115 | **ASSUMED** — *not in glossary* | Used 5×, in an exercise I must *do*, and the glossary is even *cited* for it (S2 L168) but doesn't contain it. Only defined inline in troubleshooting L88 and the decompose exercise L50. **Biggest clarity bug.** |
| **backward-compatible** | `session-2` L115 | **ASSUMED** | Load-bearing for decomposition; never defined for a beginner. Coupled to expand/contract. |
| Immutable artifact | `README.md` L23 | **Taught** (Session 3) + glossary L65 | "Same bytes through dev/qa/prod" + diagram made it concrete. |
| Promotion | `session-3` L69 | **Taught** + glossary L68 | Clear: move the same artifact, don't rebuild. |
| Definition of deployable | `README.md` L25 | **Taught** (Session 3 §3) + glossary L59 | "Criteria, not a meeting" is memorable. |
| **DORA (the acronym)** | `session-1` L86 | **ASSUMED / partial** | Four DORA *metrics* are defined (glossary L30–40) but DORA is never expanded or explained as a *thing*. I still don't know what D-O-R-A stands for. |
| DORA metrics (the four) | `session-1` L86 | **Taught** + glossary L30–40 | The individual metrics are fine; it's the umbrella term that's unexplained. |
| **OIDC** | `README.md` L36 | **Glossary-rescued** L99 | Glossary defines it well ("assume an IAM role without static creds"). But the *mechanics* in the YAML (`assume-role-with-web-identity`, `id_tokens`) are assumed. |
| Canary deploy / release | `README.md` (objectives) | **Glossary-rescued** L105 + Session 3 | Glossary + `Canary10Percent5Minutes` explanation in rollback-on-aws were enough. |
| Lambda alias / version | `rollback-on-aws.md` L45 | **Taught** + glossary L102 | The alias-shift rollback was clear with the glossary open. |
| Fail forward | `session-3` L101 | **Taught** + glossary L42 | Reasoning ("fixes vs defers") taught well. |
| Rollback / data trap | `session-3` L110 | **Taught** + glossary L45 | Hard idea, taught well. A highlight. |
| **smoke test** | `session-3` L128 | **ASSUMED** — *not in glossary* | Used in a workshop step and all over the pipeline; I guessed correctly from a YAML comment, but it should be a glossary line. |
| **Strangler fig pattern** | `README.md` L10 | **ASSUMED** — *not in glossary* | Stated as known fact in framing. Not essential to the CD lesson, but it's the first unfamiliar term a beginner meets and there's no anchor. |
| Manual / approval gate | `session-1` L158 | **Taught** + glossary L71 | "Approves timing, not readiness" is a clear distinction. |
| Stop-the-line | `session-1` L128 | **Taught** + glossary L114 | Clear. |
| SAM / `sam build` / `sam package` | `session-3` | **Glossary-rescued** L90 + annotations | Glossary + annotated example carried me despite no AWS depth. Course warned me AWS was assumed. |

**Summary of the running list:** Most core CD vocabulary is **taught** — that's
the course's biggest win for a beginner. The glossary is a genuine safety net and
caught me ~8 times. The clean misses, in priority order, are: **expand/contract**
(used in exercises, cited-but-absent in glossary), **smoke test** (in a workshop
step, not in glossary), **DORA** (acronym never expanded), **strangler fig**
(first scary term, no anchor), and the term **trunk** being used before it's
defined in Session 1.

## Factual / technical concerns

I'm a junior — I can't fact-check the AWS/GitLab mechanics with authority, and I'd
defer to someone senior on the OIDC role ARNs, the canary config, and whether the
`.gitlab-ci.yml` actually runs as written (the `before_script` says credentials
are "elided for brevity," `.gitlab-ci.yml` L61, so it's clearly illustrative, not
runnable — which is fine and labeled). The one thing I *can* flag from a beginner's
seat: `violations-api/README.md` L3 helpfully says "there is no `node_modules`;
treat the files as annotated illustrations." I wish the `.gitlab-ci.yml` had an
equally loud "this won't run as-is" banner at the top, because as a junior I might
otherwise try to copy it and wonder why the OIDC step does nothing.

## Junior-dev clarity scorecard (my persona's lens)

If the test is "are CI vs CD vs Continuous Deployment, feature flags,
expand/contract, and trunk-based *taught* or *assumed*?", here's my verdict:

- **CI vs CD vs Continuous Deployment:** *Taught, clearly.* The single best part
  of the course for a beginner. I came in conflating them; I leave able to explain
  the difference.
- **Feature flags:** *Taught, exemplary.* Definition, diagram, working code, and a
  "flags are debt" warning. I could implement the env-var pattern tomorrow.
- **Trunk-based development:** *Taught well*, though the word "trunk" itself is used
  before it's pinned to `main` in Session 1.
- **expand/contract:** ***Assumed.*** This is the one that fails the clarity test.
  It's the only listed concept I couldn't reliably define or apply from the
  sessions alone, and it's exactly the kind of term a junior most needs spelled out.

## Recommendations

### High priority

1. **Add `expand/contract` to the glossary** (and ideally a 3-sentence box in
   Session 2 §4.1 where it first appears). The course *already points readers to
   the glossary for it* (`session-2/README.md` L168) — the entry just doesn't
   exist. Reuse the good inline definition from `troubleshooting.md` L88: "add the
   new shape, write to both, migrate, then remove the old — each a small,
   backward-compatible deploy." Also define **backward-compatible** alongside it.
2. **Add `smoke test` to the glossary.** It's used in a Session 3 workshop step
   (L128) and throughout the pipeline. One line: "a fast post-deploy check that the
   service basically works before promoting it onward."
3. **Expand the DORA acronym once** (Session 1 L86 and/or glossary): say what DORA
   *is* (the research program / "DevOps Research and Assessment") so the four
   metrics have a parent concept.

### Medium priority

4. **Gloss "trunk" the first time Session 1 uses it** (around L121): "the trunk
   (`main`)." Small, removes a tab-flip for every beginner.
5. **Add a one-line "new to these terms?" note at the top of the README** pointing
   nervous beginners to the glossary and reassuring them the sessions teach the
   vocabulary. The objectives list (L18–25) is a wall of jargon on page one.
6. **Add a junior/IC note to the Current-State Assessment**: "If you're an
   individual contributor, fill in the rows about your own daily habits and bring
   the access-dependent rows (DORA numbers, releasability ownership) to your lead."
   The "do it as a team" header helps but doesn't tell a solo junior what they *can*
   do alone.

### Nice to have

7. **A loud "illustrative, won't run as-is" banner on `session-3/.gitlab-ci.yml`**,
   matching the helpful one already on `violations-api/README.md` L3.
8. **Define "strangler fig pattern" in the glossary** (or drop the term from the
   beginner-facing framing). It's the first unfamiliar word a 101 reader meets and
   it isn't essential to the CD argument.

## Verdict

**Champion (with a beginner's asterisk).** The course genuinely taught me how
delivery is *supposed* to work and gave me skills I can use next sprint —
decomposing a feature into daily slices, and putting unfinished work behind an
off-by-default flag. The core vocabulary is taught, not assumed, and the glossary
caught me most of the times the sessions didn't. I'd recommend it to the next
junior on my team — but I'd hand them a sticky note first that says
"expand/contract = add new, write both, migrate, remove old; smoke test = quick
'does it work' check after deploy; trunk = main; DORA = the four delivery
metrics," because those are the four places the course briefly let go of my hand.
