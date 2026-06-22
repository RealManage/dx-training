# Continuous Delivery 101 — Review 6

**Student:** Priya (Engineering Director, 15 yrs)
**Stance going in:** Round 5 restored me to 9.5. The half-point I docked in round 4 — the
`customer-facing` gate certified in the record but absent from the tree — was closed at the
source: `release-impact-label` became a real, parsing job in the real pipeline. Round 5 then
left two small *record/prose* gaps in my lane (R5-1 the self-contradicting synthesis bullet,
R5-2 the silent Session 3 README) plus R5-3 (a snippet missing `before_script: []`). My theme
across six rounds is **"the precision trails the prose"** — I dock when the record and the
artifacts don't match the claims. Round 6 is a targeted verification that the three round-5
fixes in my lane landed cleanly and broke nothing in the governance spine. I verified on disk
at HEAD `ce1cdae` with Read, Grep, a YAML parser, and a byte-level field comparison.
**Review date:** 2026-06-22
**Round-1:** 7/10 · **Round-2:** 9/10 · **Round-3:** 9.5/10 · **Round-4:** 9/10 · **Round-5:** 9.5/10
**Round-6 rating:** 10/10 — all three round-5 fixes in my lane landed cleanly and exactly as
recorded; the governance spine is undisturbed; I can find no actionable item a real eng leader
would raise. The precision has caught up to the prose.

## Verdict

The two record/prose gaps that kept round 5 off 10 are both closed, correctly, and the snippet
drift is gone. The round-4 synthesis no longer contradicts itself about whether the walkthrough
moved — the "Corrections" bullet now reads "had not yet moved *at round-4 review time*," then
records the `git mv`, which is temporally consistent with the Resolution table rather than at war
with it. The Session 3 README §6.1 now carries one accurate sentence naming the MR-time
`release-impact-label` job and explaining why the pipeline opens MR pipelines — a deliberate step
up from Session 2's branch-only `workflow:` — so a student reading the prose meets the divergence
as *intentional*, not as an inconsistency between two files. The `communicating-releases.md`
snippet now carries `before_script: []` and matches the real job field-for-field, so a verbatim
copy behaves identically. I re-parsed the real pipeline, re-traced the workflow recipe against
Session 2's, and swept the governance spine (manual-gate-as-control test, SoD, audit trail,
break-glass owner) — nothing the round-5 edits touched regressed. This is reference quality in my
lane. I am not manufacturing a residual to hold a point back; there isn't one.

---

## R5-1 — Synthesis self-contradiction: **CLOSED / verified**

In round 5 I docked the round-4 synthesis for saying, ~15 lines apart, both that the walkthrough
"did **not** physically move" and that it was `git mv`'d. A change record that asserts a change
both did and didn't happen is the precision-trails-the-prose pattern in the audit trail itself,
and I said I could not hand that to a compliance lead.

It is fixed at the source. `round-4-synthesis.md:86-90` now reads: *"At round-4 review time the
walkthrough had not yet moved — only the activity had shifted to Session 3 … R4-6's resolution
then physically `git mv`'d the file to `session-3/examples/` and repointed all links (see the
Resolution table)."* That is **temporally** consistent with the Resolution table (`:104`, R4-6),
which records the move as what happened. The two statements no longer contradict; they describe
two points in time. A grep for the old contradiction phrasing (`did not move` / `stays at
session-1` / `not physically move`) returns only this corrected, now-consistent bullet. The file
itself is at the new path (verified). **Closed.** The record can now go to my compliance lead
without saying a change both did and didn't occur.

## R5-2 — Session 3 README silent on the workflow divergence: **CLOSED / verified, and accurate**

In round 5 I grepped `sessions/session-3/README.md` for `workflow`, `merge_request`, `MR
pipeline`, `release-impact`, and `customer-facing` and got **zero** hits: the consequential
`workflow:` step-up and the MR-time gate were explained only in the YAML comments and
`communicating-releases.md`, never in the teaching prose. A student who learned Session 2's
"suppress the MR pipeline, run the branch pipeline," then opened the Session 3 file and saw the
*opposite* first rule, had to reverse-engineer the why from a comment.

Now §6.1 (`:140`) carries one sentence:

> The full pipeline also runs one **merge-request-time** job — `release-impact-label` — which is
> why its `workflow:` enables MR pipelines alongside the branch pipeline (a deliberate step up
> from Session 2's branch-only form): it forces each change to declare its release impact before
> merge. Details in [Communicating Releases](../../resources/communicating-releases.md).

I pressure-tested this for **accuracy**, not just presence, because a wrong explanation is worse
than a missing one:

- **"runs one merge-request-time job — `release-impact-label`"** — correct. The job
  (`.gitlab-ci.yml:96-109`) carries `rules: [if: $CI_PIPELINE_SOURCE == "merge_request_event"]`,
  so it runs only in the MR pipeline. Parse-confirmed.
- **"which is why its `workflow:` enables MR pipelines alongside the branch pipeline"** — correct.
  The workflow rules (`:37-41`) are the dual recipe: rule 1 runs the MR pipeline, rule 2
  (`$CI_COMMIT_BRANCH && $CI_OPEN_MERGE_REQUESTS → never`) suppresses the duplicate branch
  pipeline while an MR is open, rule 3 runs the branch pipeline otherwise. The MR-time gate is
  the *reason* the pipeline opts into MR pipelines — `$CI_MERGE_REQUEST_LABELS` is populated only
  there. Causally accurate.
- **"a deliberate step up from Session 2's branch-only form"** — correct, and I verified the
  contrast. Session 2's `ci-pipeline.gitlab-ci.yml:22-26` is the simpler form (`merge_request_event
  → when: never`, then `$CI_COMMIT_BRANCH`). Session 3 *inverts* the first rule and *adds* the
  middle suppression rule. The divergence is real and consequential, exactly as the prose says.
- **The link resolves.** `../../resources/communicating-releases.md` from `sessions/session-3/`
  lands on the real file (verified).

The sentence reads as **intentional**, not inconsistent — it explicitly frames the divergence as
"a deliberate step up" and gives the reason. It sits one section below the §2.2 deploy≠release
authority split (`:45-47`) without disturbing it: §2.2 is *who authorizes* (Eng Lead authorizes
the deploy, evolved release-manager authorizes the flip); §6.1 is *workflow mechanics* (why the
pipeline opens MR pipelines). Complementary, not colliding. **Closed.**

## R5-3 — Snippet drops `before_script: []`: **CLOSED / verified byte-for-byte on what matters**

In round 5 the fenced snippet in `communicating-releases.md` omitted the `before_script: []` the
real job carries — meaning a verbatim copy-paste would run the default `npm ci` pointlessly on a
pure label check and behave differently from the pipeline. Minor (the canonical job is one link
away), logged for completeness.

It is fixed. The snippet (`:82-96`) now carries `before_script: []`. I did not eyeball this — I
parsed both the real job (`.gitlab-ci.yml:96-109`) and the fenced block, then compared field by
field:

- `stage` — match
- `rules` — match
- `before_script` — match (both `[]`)
- `script` — match (identical `case` statement)

All four keys present in both, all four equal. A verbatim copy now behaves identically to the
pipeline job. The one cosmetic difference is an inline comment — the snippet annotates
`before_script: [] # no npm install needed — this gate only reads the MR's labels`, while the real
job's equivalent explanation lives in the block comment above it (`:91-95`). That is a *teaching*
gloss (the snippet is self-explaining) with **zero** behavioral effect; comments don't parse into
the job. Not a finding — if anything it's the better choice for an excerpt. **Closed.**

I also re-confirmed the real job still parses: `python3 -c "import yaml; yaml.safe_load(...)"`
loads `.gitlab-ci.yml` clean, top-level keys include `release-impact-label` and `workflow`, the
job's `rules`/`before_script`/`script` are well-formed, and the workflow's three-rule recipe is
present as analyzed in round 5. The gate I traced job-by-job last round is byte-stable.

## Regression sweep in my lane — clean

I checked whether the round-5 edits (synthesis record, §6.1 prose, the snippet, the workflow
comment, plus the R5-4/R5-5 edits to `value-stream-map.md` and `ai-assisted-delivery.md`)
disturbed anything in the governance/compliance story I won across rounds 2-3. They did not.

- **Manual-gate-as-control test** — intact and consistent across all four homes:
  `glossary.md:93` (debt vs legitimate, permanent control), `governance-and-compliance.md:23-32`
  (debt vs legitimate authorization; "the course's 'remove the gate' advice is about debt gates,
  not [authorization gates]"), `troubleshooting.md:29`, and `session-3/README.md:45`. No
  regression; the §6.1 addition sits in a different section and doesn't touch the §2.2 note.
- **Segregation of duties / audit trail / no-standing-access** — `governance-and-compliance.md`
  §"the MR review *is* the control" (`:35-65`), the audit-question→artifact table, and the
  OIDC-deploys-not-a-person claim are all present and untouched.
- **Break-glass with accountable owner (R3-1)** — still named: "owned by the on-call lead /
  service owner and recorded in the incident record" (`governance-and-compliance.md:90-91`, and
  the auditor paragraph `:138-139`). Undisturbed.
- **Deploy ≠ release / release authorization** — `session-3/README.md:45-47` split is intact; the
  new §6.1 sentence is purely workflow mechanics and does not blur the authority split.

The R5-4 and R5-5 edits are outside my lane (Dana's exemplar-fidelity item; Tex's independent-
test-review bullet) and I did not re-adjudicate them, but I confirmed they touched no governance
prose I'm accountable for.

## Standing items — disposition

- **R4-4 / R3-5 (`customer-facing` gate as real CI):** **CLOSED / verified** (round 5). Re-parsed
  this round; byte-stable. The release-notes data source rides on a real pipeline job.
- **R5-1 (synthesis self-contradiction):** **CLOSED / verified** this round.
- **R5-2 (Session 3 README silent on workflow divergence):** **CLOSED / verified** this round;
  the new sentence is accurate, the link resolves, the prose reads as intentional.
- **R5-3 (snippet drops `before_script: []`):** **CLOSED / verified** this round; field-for-field
  match.
- **R4-6 (walkthrough move):** **CLOSED / verified** (round 5); not re-examined — the diff didn't
  touch it.
- **Governance spine (SoD, audit trail, break-glass owner, manual-gate-as-control):** **STILL
  CLOSED**; swept this round, no regression.

## Bottom line for an eng leader

**Greenlight, unconditional, at full marks for my lane.** Across six rounds my one recurring
complaint — the record and the artifacts disagreeing — has been the thing keeping me off 10. It is
now gone: the gate is real and parses, the synthesis no longer contradicts itself about whether a
change occurred, the teaching prose names the workflow divergence as deliberate and describes it
*accurately*, and the snippet a reader might copy now matches the pipeline byte-for-byte on every
field that runs. The governance work I won in rounds 2-3 survived the round-5 edits intact. I went
looking for a residual to justify holding a point back and could not honestly find one — every
surface in my lane is now reference quality. The precision has caught up to the prose.

**Round-6 rating: 10/10** (up 0.5 from 9.5 — the last two record/prose gaps in my lane are closed
and verified; nothing actionable remains).
