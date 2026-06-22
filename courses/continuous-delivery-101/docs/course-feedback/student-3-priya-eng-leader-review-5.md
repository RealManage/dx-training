# Continuous Delivery 101 — Review 5

**Student:** Priya (Engineering Director, 15 yrs)
**Stance going in:** Round 4 dropped me half a point to 9.0 for exactly one reason in
my lane: R3-5's `customer-facing` release-impact gate had been *logged* as a "real,
parse-verified GitLab job" but existed only as a fenced snippet in a prose file. My
four-round theme is **"the precision trails the prose"** — I dock when the record and
the artifacts don't match the claims. Round 5 is a targeted verification: is the gate
genuinely real now, is the dual-pipeline `workflow:` recipe actually correct GitLab, and
did the R4-6 walkthrough move leave anything dangling. I verified on disk at `c9aa30f`
with Read, Grep, a YAML parser, and the site build.
**Review date:** 2026-06-22
**Round-1:** 7/10 · **Round-2:** 9/10 · **Round-3:** 9.5/10 · **Round-4:** 9/10
**Round-5 rating:** 9.5/10 — the gate is genuinely real and the workflow is correct
GitLab; the precision gap I docked for in round 4 is closed. Half a point still withheld
for two new precision/record gaps, both small, both in my lane.

## Verdict

The thing I have flagged in every round is finally fixed at the source, not papered
over. `release-impact-label` is now an actual, parsing job in
`sessions/session-3/examples/.gitlab-ci.yml` — not a fenced block in a markdown file —
and a grep of every `.yml` in the course finds it in exactly one place: the real
pipeline. The round-4 synthesis claim "Round-3's 'parse-verified job' claim is now true"
is itself now true. I verified the YAML parses, the workflow logic is correct GitLab, the
`communicating-releases.md` link to it resolves, and the site build is green for CD 101
(21 pages, 0 errors). The dual-pipeline `workflow:` recipe — the riskiest part of this
change — holds up under a job-by-job trace. R4-6 (the walkthrough move) is clean: no
dangling navigational links, all internal links resolve from the new location, build
link-checker passes. My recurring theme is retired for this gate.

I am at 9.5, not 10, for two **new** precision gaps the same instinct catches: (1) the
round-4 synthesis now contradicts itself about whether the walkthrough file physically
moved, and (2) the consequential `workflow:` change is explained only inside the YAML's
own comments and `communicating-releases.md` — the Session 3 README narrative, the
teaching prose a student actually reads, never mentions it. Neither is a content
regression or an adoption blocker. Both are finish-the-record polish.

---

## Step 1 — Is the gate genuinely real now? **YES — verified**

This was the whole reason for the round-4 dock, so I tested it hardest.

- **It exists in real YAML.** `release-impact-label` is a job in
  `sessions/session-3/examples/.gitlab-ci.yml:96-109`, with `stage: validate`, a
  `rules:` clause keyed on `merge_request_event`, `before_script: []`, and the
  `case`-statement script that fails an MR carrying neither `customer-facing` nor
  `no-user-impact`.
- **It parses.** `python3 -c "import yaml; yaml.safe_load(...)"` loads the file clean;
  the parsed top-level keys include `release-impact-label`, and its `rules` and `workflow`
  block are well-formed. This is no longer "a snippet was parsed" — there is a real
  pipeline file and it parses.
- **It lives in exactly one place.** `grep -rl` across every `*.yml`/`*.yaml` for
  `release-impact-label` / `CI_MERGE_REQUEST_LABELS` / `customer-facing` / `no-user-impact`
  returns **only** `sessions/session-3/examples/.gitlab-ci.yml`. The other hits are markdown
  (the prose copy in `communicating-releases.md` and the historical feedback/synthesis
  files). In round 4 the grep across YAML returned *nothing*; now it returns the real file.
- **The reference resolves.** `communicating-releases.md:76` links the job by path —
  `[`session-3/examples/.gitlab-ci.yml`](../sessions/session-3/examples/.gitlab-ci.yml)`.
  Resolved from `resources/`, the target exists. The build link-checker agrees (no CD 101
  link warnings).

This is the `smoke-test.sh` precedent applied correctly: a control that was "described in
prose" is now "enforced in code that the example actually exercises." The release-notes
data source — the thing I'd defend to a client as *how we guarantee you hear about
changes* — now rides on a job in the pipeline, not a paragraph in a doc. **Closed.**

## Step 2 — Is the dual-pipeline workflow recipe correct GitLab? **YES — traced, correct**

This is the part I was most prepared to find subtly wrong, because a botched
`workflow:rules` either double-runs every pipeline or silently runs nothing. I traced it
job-by-job against GitLab's first-match-wins rule evaluation.

**The workflow rules (`.gitlab-ci.yml:37-41`), in order:**

1. `if: $CI_PIPELINE_SOURCE == "merge_request_event"` — no `when`, so defaults to *run*.
2. `if: $CI_COMMIT_BRANCH && $CI_OPEN_MERGE_REQUESTS` → `when: never`.
3. `if: $CI_COMMIT_BRANCH` — *run*.

**Scenario A — push to a feature branch with an open MR (the case the recipe targets):**
two triggers fire.

- *MR pipeline* (`$CI_PIPELINE_SOURCE == "merge_request_event"`): rule 1 matches → runs.
  In an MR pipeline `$CI_COMMIT_BRANCH` is **empty** (GitLab exposes the branch as
  `$CI_MERGE_REQUEST_SOURCE_BRANCH_NAME`). So: `lint`, `validate:sam`, `unit-tests`,
  `dependency-audit` (no `rules:`) run; `release-impact-label` (rule: `merge_request_event`)
  **runs** — which is the entire point, because `$CI_MERGE_REQUEST_LABELS` is populated
  *only* here; `build:artifact` and `deploy:*` (rule: `$CI_COMMIT_BRANCH == $CI_DEFAULT_BRANCH`)
  **do not run** because `$CI_COMMIT_BRANCH` is empty. Correct — you never deploy from an MR.
- *Branch pipeline* (the push event, MR open): rule 1 no; rule 2 matches
  (`$CI_COMMIT_BRANCH` set **and** `$CI_OPEN_MERGE_REQUESTS` set) → `when: never` →
  **suppressed**. This is the duplicate-avoidance, and it is the correct GitLab idiom.

**Scenario B — push to a feature branch with no open MR:** rule 1 no; rule 2 no (no open
MR); rule 3 yes → one branch pipeline. Front-half runs; `release-impact-label` skipped
(not an MR); `build:artifact`/`deploy:*` skipped (not default branch). Correct.

**Scenario C — merge to main:** one branch pipeline (rule 3). Front-half runs;
`release-impact-label` skipped; `build:artifact` + `deploy:dev` + `deploy:qa` run
(`$CI_COMMIT_BRANCH == $CI_DEFAULT_BRANCH`); `deploy:prod` is `when: manual`. This is the
real deploy-and-promote path. Correct.

**The two traps I specifically checked, both clear:**

- **No broken `needs:` in the MR pipeline.** `deploy:dev` has
  `needs: [lint, validate:sam, unit-tests, build:artifact]`. In an MR pipeline `deploy:dev`
  itself does **not** run (its inherited rule `$CI_COMMIT_BRANCH == $CI_DEFAULT_BRANCH`
  fails), so the fact that `build:artifact` is also absent never produces a dangling-`needs`
  error. GitLab only validates `needs:` for jobs that are actually added to the pipeline.
  The deploy chain and its `needs:` only ever co-exist in the branch pipeline on main, where
  every member is present. Clean.
- **The deploy jobs really do carry the default-branch rule.** They use `extends: .deploy`,
  and `.deploy` holds `rules: [$CI_COMMIT_BRANCH == $CI_DEFAULT_BRANCH]`. GitLab merges
  `rules:` from the extended hidden job, so `deploy:dev/qa/prod` inherit it — they will not
  fire in an MR pipeline. (A reader skimming the deploy jobs sees no inline `rules:` and
  could briefly wonder; the rule is one `extends:` hop away. Not a defect — standard GitLab —
  but worth knowing it's load-bearing.)

**Verdict on the recipe: correct, and the workflow comment (`:31-36`) accurately
describes what it does** — including the explicit note that Session 2's excerpt uses the
simpler branch-only form and that the full pipeline opens MR pipelines *because* it hosts
the one MR-time gate. An engineer who reads the YAML and its comments will not be misled.

## Step 3 — Did the workflow change hurt teaching clarity? **One real gap (R5-P2)**

Session 2's `ci-pipeline.gitlab-ci.yml:21-26` keeps the simpler recipe — *suppress the MR
pipeline, run the branch pipeline*:

```yaml
workflow:
  rules:
    - if: $CI_PIPELINE_SOURCE == "merge_request_event"
      when: never
    - if: $CI_COMMIT_BRANCH
```

Session 3 inverts the first rule (now *runs* the MR pipeline) and adds the
`$CI_OPEN_MERGE_REQUESTS → never` middle rule. That divergence is real and consequential,
and it **is** explained — but only inside the Session 3 YAML's comment block (`:31-36`),
which names Session 2 by contrast and gives the reason (the full pipeline hosts an MR-time
gate, so it must open MR pipelines). For an engineer reading the file, that is sufficient
and well-judged.

**The gap (R5-P2):** the explanation lives *only* in the YAML comment and in
`communicating-releases.md`. I grepped `sessions/session-3/README.md` for `workflow`,
`merge_request`, `MR pipeline`, `CI_OPEN_MERGE`, `release-impact`, and `customer-facing`
— **zero hits**. The Session 3 teaching narrative — the prose a student actually reads in
the session, and the README that links to the walkthrough and the target pipeline — never
mentions that the pipeline now runs an MR-time gate, never mentions the workflow recipe,
and never flags that it deliberately differs from the Session 2 recipe the student just
learned. A student who reads Session 2's "run the branch pipeline, not a duplicate MR
pipeline," then opens the Session 3 file and sees the *opposite* first rule, has to
reverse-engineer the why from a comment. The comment is good; the session prose should
carry one sentence so the divergence reads as intentional, not as an inconsistency between
two files the course presents as one evolving pipeline. This is a clarity/precision gap,
not an error — the artifacts are right; the teaching layer doesn't surface them.

## Step 4 — R4-6 walkthrough move: **clean, no dangling links — verified**

The file moved from `session-1/examples/` to
`sessions/session-3/examples/current-state-pipeline-walkthrough.md` (confirmed: present at
the new path, gone from the old). I checked all three link surfaces:

- **Session 3 README links resolve.** `sessions/session-3/README.md:130,179` both link
  `examples/current-state-pipeline-walkthrough.md` — relative to the README, that resolves
  to the new location. Good.
- **The file's own internal links resolve from the new location.** `.gitlab-ci.yml`,
  `../README.md`, `../../../exercises/current-state-assessment.md`,
  `../../../resources/migration-checklist.md` — all four exist relative to
  `sessions/session-3/examples/`. Verified by hand.
- **No dangling navigational links to the old path.** The only references to
  `session-1/examples/current-state-pipeline-walkthrough` are in `docs/course-feedback/*`
  (prior reviews + the round-2/3/4 synthesis), and a `grep` for markdown-link syntax
  (`](`) against those finds **none** — they are all backtick prose spans recording what
  was true at the time. Not navigation; correctly left as historical record.
- **Build is green.** `npm run build` reports CD 101 at 21 pages, 8 code views, 0 errors;
  the only 3 "points outside the published site" warnings are all in the unrelated
  `ai-101-claude-code` course. CD 101's link-checker is clean.

R4-6 is **closed and verified.** This is the better outcome than the in-place retitle the
synthesis originally proposed — the tree no longer implies the walkthrough is a Session 1
artifact.

## New precision/record gaps my instinct caught

Real scrutiny; these are genuine, both small, both exactly the kind of "the record doesn't
match the artifact" gap I dock for.

### R5-P1 — The round-4 synthesis now contradicts itself about the walkthrough move (med)

`docs/course-feedback/round-4-synthesis.md` says two opposite things about the same file
~15 lines apart:

- **"Corrections to reviewer notes" (`:86-88`):** *"The walkthrough file did **not**
  physically move … the file stays at `session-1/examples/` so historical-feedback path
  references keep resolving. This is the basis for R4-6."*
- **"Resolution (applied)" (`:102`):** *"`current-state-pipeline-walkthrough.md`
  **moved** to `session-3/examples/` (`git mv`) … (Historical `docs/course-feedback/*`
  mentions of the old path are left as written-at-the-time records…)."*

The Resolution is what actually happened (the file *is* at the new path), and the
navigational links are all sound — so this is a *record* defect, not a broken-tree defect.
But a synthesis document that, on one page, both asserts a file "did not physically move"
and records that it was `git mv`'d, and grounds R4-6's *rationale* in the now-falsified
"stays in place" premise, is precisely the precision-trails-the-prose pattern in the audit
trail itself. If I hand a synthesis to my compliance lead as the change record, it cannot
contradict itself about whether a change occurred. **Fix:** strike or correct the
"Corrections" bullet (`:86-88`) so the record reflects that the file moved; the Resolution
row is already correct.

### R5-P2 — The workflow change is explained only in YAML comments, never in Session 3 prose (low/med)

Detailed under Step 3 above. The consequential `workflow:` recipe and its deliberate
divergence from Session 2 are documented in the right *engineering* place (the YAML
comment) and referenced from `communicating-releases.md`, but the Session 3 README — the
teaching narrative — is silent on all of it (verified by grep: zero hits for `workflow`,
`merge_request`, `release-impact`, `customer-facing`). A student following the prose meets
the new MR-time gate and the inverted workflow rule only by opening the file. **Fix:** one
sentence in `sessions/session-3/README.md` near the pipeline walkthrough/`.gitlab-ci.yml`
reference — "the full pipeline runs the MR pipeline too, so the `release-impact-label`
gate can read the MR's labels; that's why its `workflow:` differs from Session 2's." Low
effort, removes the "is this an inconsistency?" stumble.

### R5-P3 — Prose snippet drops `before_script: []` the real job needs (low)

The fenced snippet in `communicating-releases.md:82-95` is an honest excerpt of the real
job, but it omits `before_script: []` (present in the real job at `.gitlab-ci.yml:100`).
That line matters: it suppresses the `default.before_script` (`npm ci`), which would
otherwise run `npm ci` pointlessly on a pure label check (and could fail). The snippet's
`rules` and `script` match the canonical job exactly (verified by parsing both), and it's
framed as "six lines of CI," so the omission is defensible abbreviation — but a reader who
copy-pastes the snippet verbatim gets a job that behaves differently (runs `npm ci`) from
the one in the pipeline. Since the canonical job is now one click away by link, this is
genuinely minor. **Fix (optional):** add `before_script: []` to the snippet, or a one-word
"(excerpt)" note. Not worth a point; logged for completeness.

## Standing items — disposition

- **R4-4 / R3-5 (`customer-facing` gate as real CI):** **CLOSED / verified.** Real job in
  the real pipeline, parses, single source of truth, link resolves, build green. The
  round-4 dock is reversed.
- **R4-6 (walkthrough orphaned-by-location):** **CLOSED / verified.** File moved to
  `session-3/examples/`; all links resolve; no dangling navigation; build clean.
- **R4-1/R4-5 (Session 1 slide deck ran the replaced workshop):** out of my round-5 scope
  (slides are pinned/untracked per the synthesis); not re-verified.
- **"Controls are debt" framing / governance dimension in the assessment / break-glass
  owner:** not re-examined this round (the change set was the gate + workflow + file move);
  all were CLOSED/verified through round 4 and the diff did not touch them.

## Bottom line for an eng leader

**Greenlight stands, and the round-4 dock is earned back.** The single gap that cost the
half point in round 4 — a governance-adjacent control certified in the record but absent
from the tree — is closed honestly: `release-impact-label` is a real, parsing job in the
real pipeline, exercised by the example, referenced by a resolving link, and the
dual-pipeline `workflow:` that makes it work is correct GitLab under a job-by-job trace.
The release-notes data source I'd defend to a client is now enforced in code. R4-6 is
clean.

I hold at **9.5, not 10**, for two new precision/record gaps — the synthesis now
contradicts itself about whether the walkthrough moved (R5-P1, med), and the consequential
workflow change is invisible to a student reading the Session 3 prose (R5-P2). Both are
finish-the-record polish, not adoption risk and not content regression. Fix R5-P1 (the
record can't say a change both did and didn't happen) and add the one workflow sentence to
the Session 3 README, and this is a 10.

**Round-5 rating: 9.5/10** (up 0.5 from 9.0 — the precision finally caught up to the prose
in my lane; two small new record gaps keep it off 10).
