# Continuous Delivery 101 — Student Agent Testing Plan

A reusable plan for pressure-testing the course by having persona agents "take"
it and write structured feedback. Inspired by the AI 101 course-feedback process,
adapted for a course that is **persuasion + light exercises**, not code you run.

---

## What's different from AI 101

CD 101 is a 3-session workshop about *working agreements and engineering
practices* — its job is to win over skeptics and change how a team delivers. So
the test isn't "did the code run." It's:

- **Did the arguments convince this persona?** Where did the course lose them?
- **Which of their objections went unaddressed?**
- **Can they actually apply it Monday** — to *their* context (monolith vs Lambda,
  team lead vs IC, gatekeeper vs builder)?
- **Is the technical content accurate** (AWS/SAM/GitLab/OIDC/canary, the
  iac-baseline alignment, fail-forward vs rollback)?

Because each agent only **reads** the committed course and **writes one distinct
review file**, no worktrees or headless CLI are needed (unlike AI 101). Agents
run in parallel; their files never collide.

---

## Personas (Round 1)

Skewed toward resistance — CD's value is in converting hard sells.

| # | Student | Role (yrs) | Stance | The lens only they bring |
| - | ------- | ---------- | ------ | ------------------------ |
| 1 | Sam | Senior Dev (10) | Skeptic; loves long-lived branches & the weekly release ritual; burned by a bad deploy | "Trunk-based is chaos; feature flags are tech debt." Attacks the core behavior change. |
| 2 | Dana | .NET Monolith Maintainer (12) | Owns an ASP.NET Framework Web API on Azure VMs | "This is for the cloud-native kids — does *any* of it apply to my VM-deployed monolith?" Tests the strangler-fig / "applies everywhere" framing. |
| 3 | Priya | Engineering Leader / Director (15) | Accountable for risk, change windows, compliance/audit | "We can't just let things deploy." Tests the CD ≠ Continuous Deployment distinction and the governance story. |
| 4 | Marcus | QA / Release Manager (8) | Owns the release sign-off gate **and the weekly release-notes email to clients** | Threatened by "the pipeline decides releasability." **Plus:** if we ship continuously, what replaces the weekly "here's everything that changed" email to users? |
| 5 | Felix | Junior Dev (2) | Eager, jargon-lost | Clarity check: are CI vs CD vs Continuous Deployment, feature flags, expand/contract, and trunk-based *taught* or *assumed*? |
| 6 | Jordan | Tech Lead (7) | Pragmatic; owns a team's branching/release workflow | Migration cost, half-measures, team buy-in. Scrutinizes the migration checklist, current-state assessment, and decompose-a-branch exercise: will this work next sprint? |
| 7 | Riley | SRE / Platform Engineer (9) | Cares about rollback, observability, the actual AWS/GitLab mechanics | Fact-checks technical accuracy: Lambda alias/version shift, `Canary10Percent5Minutes`, OIDC role ARNs, immutable artifacts, the manual-gate teaching point, fail-forward-first. |

---

## What each agent does

1. **Read the whole course in character**, in order:
   - `courses/continuous-delivery-101/README.md`
   - `sessions/session-1/README.md` (+ examples), `session-2`, `session-3`
   - `resources/minimums-reference.md`, `glossary.md`, `migration-checklist.md`, `troubleshooting.md`
2. **Attempt the two exercises in character:**
   - `exercises/current-state-assessment.md` — fill it out for *their* imagined team/system.
   - `exercises/decompose-a-branch.md` — decompose a realistic feature from *their* world.
3. **Stress-test the content** through their lens (above): unconvincing claims,
   unaddressed objections, jargon, missing context, factual errors, and the
   moments they'd push back in the room.
4. **Write a structured review** (format below) to `docs/course-feedback/`.

Agents stay in character throughout. Their job is honest, pointed feedback —
not praise. A "this convinced me" only counts if they say *why*.

---

## Review format

File: `courses/continuous-delivery-101/docs/course-feedback/student-{N}-{slug}-review-{round}.md`

```markdown
# Continuous Delivery 101 — Review {round}

**Student:** {Name} ({Role}, {Years} yrs)
**Stance going in:** {one line}
**Review date:** {date}
**Overall rating:** {X}/10 — would I adopt/champion this?

## Executive summary
{2–4 sentences: did it win me over, and where did it fall short?}

## Section-by-section
### Course framing (README)
### Session 1 — Why CD & the Minimums
### Session 2 — Trunk-Based Development & CI
### Session 3 — The Pipeline
### Resources (minimums, glossary, checklist, troubleshooting)
### Exercises — my attempt
- Current-state assessment: {what I filled in, where it was hard}
- Decompose a branch: {my feature, how it went}

## Where it lost me / objections it didn't answer
{The persona's specific pushback. This is the most valuable section.}

## Confusing or assumed (clarity)
{Jargon or leaps that lost me.}

## Factual / technical concerns
{Anything inaccurate or doubtful — especially AWS/GitLab specifics.}

## {Persona-specific section}
{e.g. Marcus: "Communicating releases to users"; Riley: "Recovery mechanics".}

## Recommendations
### High priority
### Medium priority
### Nice to have

## Verdict
{One line: would I champion this, comply grudgingly, or resist — and why?}
```

---

## Orchestration

- Spawn the 7 persona agents **in parallel** (one message, multiple agents).
- Each agent: reads the course, attempts the exercises, writes its own review file.
- No worktrees / no headless CLI — reads are shared, writes are disjoint.
- **Synthesis (separate step):** after all reviews land, consolidate into
  `docs/course-feedback/round-1-synthesis.md` — a prioritized triage table
  (issue → severity → which persona(s) raised it → proposed course edit). Course
  changes happen only after human review of that triage.

### Agent prompt template

```text
You are {Name}, a {Role} with {Years} years at RealManage.
Stance: {persona stance}.
Your unique lens: {the lens only they bring}.

Take the Continuous Delivery 101 course entirely in character. Read, in order:
README, sessions/session-1..3 (and their examples/), then resources/, then
attempt both exercises/ for your own context. Be honest and pointed — your job
is to find what fails to convince you, what's confusing, what's missing for your
world, and any technical inaccuracies. Praise only with reasons.

Write your review (using the format in docs/testing/student-agent-testing-plan.md)
to: courses/continuous-delivery-101/docs/course-feedback/student-{N}-{slug}-review-1.md
Use repo-relative paths when citing files. Stay in character throughout.
```

---

## Designed to re-run

Round 1 establishes the baseline. After fixes land, re-run with `review-2` files;
agents read their prior review and report what improved, what regressed, and
what's still open — the same improvement-tracking loop the AI 101 course used.

---

## Round 2 — added persona

| # | Student | Role | The lens only they bring |
| - | ------- | ---- | ------------------------ |
| 8 | Tex | AI Champion | "AI now writes nearly all our code — does that change CD, and does the course even see it?" The factory view: humans stop turning bolts and move to designing a better factory. Tests whether the course addresses AI-authored development at all, and whether CD's practices matter *more*, *less*, or *differently* when machines write the code. |

Returning personas (1–7) write `review-2`, reporting improved / regressed /
still-open against their round-1 review. Tex is new this round, so he writes
`review-1`.

---

*Round 1 personas chosen for resistance coverage. Adjust the roster per round.*
