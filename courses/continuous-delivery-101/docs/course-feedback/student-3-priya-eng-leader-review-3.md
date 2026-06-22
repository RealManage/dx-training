# Continuous Delivery 101 — Review 3

**Student:** Priya (Engineering Director, 15 yrs)
**Stance going in:** Round 2 moved me from "comply" to "champion" at 9/10, with two
named gaps: the assessment exercise scored zero governance dimensions, and the
course conflated a mandated business freeze with an engineering one. Round 3 is a
spot-check: did the R2-M3 (governance precision) and R2-M4 (release-comms authority)
fixes actually land for a director who must defend this to an auditor and an exec —
or did they paper over the gaps with prose?
**Review date:** 2026-06-22
**Round-1 rating:** 7/10 · **Round-2 rating:** 9/10
**Round-3 rating:** 9.5/10 — the governance story is now genuinely auditable, with one
residual implementation gap and one "named-but-not-shown."

## Verdict

The two gaps that held me at 9 are closed: the assessment now has a real controls
scorecard with a **Deliberate?** column (so a defensible control no longer reads as a
failing grade), and the freeze conflation is fixed with an explicit business-vs-engineering
carve-out. The R2-M3 precision items I raised as "an auditor would catch this" — self-approval
enforcement, artifact-retention, the managed-flag control-trade, break-glass mechanism — are
all now concrete enough that I would put them in front of a SOC 2 assessor. Half a point still
withheld for two real residuals, below.

## Resolved since round 2

### R2-M3(a) Artifact retention < audit-evidence window — **IMPROVED / defensible**

My round-2 catch: `.gitlab-ci.yml` expires the build artifact in 30 days while the
governance doc leans on that artifact as evidence — an auditor finds that in five
minutes. Now addressed in **both** places, correctly. The governance doc
(`resources/governance-and-compliance.md:73-79`) carries a blockquote: *"the pipeline's
build artifact in the examples expires in 30 days … a sensible promotion window, **not**
an evidence-retention policy. Set retention from your control framework (often years) …
Don't let a CI housekeeping default silently decide your audit window."* And the pipeline
itself (`.gitlab-ci.yml:126-129`) now carries the inline comment *"30 days is a PROMOTION
window, not an audit-retention policy. If you rely on the stored artifact as compliance
evidence, set this to your framework's retention (often years)."* This is exactly the right
framing — it names retention as a control *parameter* I own, not a default I inherit
blindly. Defensible to an auditor: yes. It even distinguishes the cheap-to-retain evidence
(commit/MR history, pipeline metadata) from the expensive stored artifact, which is the
distinction a real assessor draws.

### R2-M3(b) Self-approval prevention asserted, not enforced — **IMPROVED / defensible**

My round-2 catch: the entire segregation-of-duties claim rested on GitLab being
configured so an author can't approve their own MR, and that was a parenthetical aside.
Now it is named, enforceable, and specific (`governance-and-compliance.md:43-45`):
*"Enforce it in the GitLab project settings, don't leave it to habit: require ≥1 approval,
enable **Prevent approval by the author** and **Prevent approval by users who add
commits**, and require **Pipelines must succeed** before merge. SoD is then a setting the
tool applies to every MR, not a convention reviewers must remember."* These are the real
GitLab setting names, not invented ones — an auditor can ask me to screenshot each toggle,
and I can. This is the single most load-bearing control in the whole story and it is now
stated as enforced configuration. The one thing I'd still want (minor, below) is the same
verb in the *assessment* — "verify and evidence self-approval is disabled" as a scored line
— but the doc itself is now solid.

### R2-M3(c) Managed-flag-service graduation is an unnamed control downgrade — **IMPROVED / defensible**

This was my sharpest adversarial finding in round 2: env-var flags inherit the pipeline's
audit trail by construction (the flip *is* an MR + review + pipeline run), but graduating to
AppConfig/LaunchDarkly silently moves release authorization *out* of that trail, and the two
files covering flags never connected this. Now `governance-and-compliance.md:114-120` states
it plainly: *"Env-var / config flags inherit the pipeline's audit trail for free … A managed
flag service (AWS AppConfig / LaunchDarkly) … keeps its own flip log — but that log lives
**outside** the pipeline trail. That's a control trade: wire the service's flip log into your
release audit record deliberately, or your 'who released what, when' evidence is now split
across two systems."* That is precisely the hazard I described, named as a *trade* rather than
a free upgrade. The one genuinely new governance hazard CD introduces is now on the page. Good.

### R2-M3(d) Break-glass hand-wavy as mechanism — **IMPROVED (mostly) / one gap**

Round 2: the break-glass *policy* was sound (pre-authorized, scoped, time-boxed, logged,
reconciled, rare) but the *mechanism* was missing — for a course that shows its work in YAML
and IAM everywhere else, break-glass was the one hand-wavy control. Now
`governance-and-compliance.md:95-101` gives the concrete AWS mechanism: *"a pre-created
`break-glass-deploy` IAM role, assumable only by named on-call engineers … its assumption
requires a second person's recorded acknowledgement (in the incident channel), auto-expires
within a few hours, and alerts the security channel on every use."* That is a real,
defensible mechanism — scoped role, second-person approval (SoD preserved even in the
exception), auto-expiry, alerting. An auditor would accept the shape of this. **Residual gap
(see below):** it still names no *accountable owner* for the mandatory post-incident review,
and `rollback-on-aws.md` still doesn't cross-link to it.

### R2-M3(e) Business-vs-engineering freeze conflation — **IMPROVED / defensible**

This was one of my two reasons for holding at 9, and it is now fixed at the source. `branching-antipatterns.md:54`
adds a blockquote directly under Anti-pattern 5: *"Not every freeze is this anti-pattern. A
mandated business change window — a client-contractual moratorium, a fiscal close, a
peak-season lockdown — is a legitimate external control, not an admission the trunk is
untrustworthy. CD handles it better: because deploy ≠ release, engineering keeps merging to
trunk … and simply doesn't flip the flag during the window. You freeze the release, not the
integration."* This is the exact distinction I asked for, and it lands the bonus point I
flagged in round 2 — CD makes a mandated freeze *cheaper* (keep integrating, stop flipping).
A teammate who reads Anti-pattern 5 will no longer treat my contractual freeze as something to
argue me out of. Resolved.

### R2-M4 Release-comms authority / who owns flip timing — **IMPROVED / defensible**

Round 2: Session 3 §2.2 called the prod-timing decision the "Engineering Lead's" while
`communicating-releases.md` made flag-flip timing the release-comms owner's — unreconciled.
Now `session-3/README.md:47` splits them cleanly: *"The prod **deploy** gate above … is an
engineering authorization — owned by the Engineering Lead. The **release** — the flag flip
users actually feel — is a separate decision about which feature goes live when and to whom,
owned by whoever runs release communication and flag governance (our evolved release-manager
role), in coordination with the business."* That is the correct two-authority split: Eng Lead
authorizes the *deploy*, release-manager (evolved) authorizes the *release/flip*. It matches
`communicating-releases.md:99-114` ("The release manager's role, evolved → Governing flag
flips"). The authority question — *who signs off the release when it's no longer the deploy?* —
now has a single consistent answer across both files. This is the governance consequence of
decoupling, correctly assigned. Defensible.

### R2-H3 / round-2-still-open Assessment had no governance dimension — **IMPROVED / the gap I most wanted closed**

This was my **biggest** remaining complaint in both prior rounds — the assessment is the
artifact I actually fill out in the room, and it scored zero control dimensions. Now
`exercises/current-state-assessment.md` has a dedicated **Part 2 — Score your controls
(governance & communication)** with rows for audit trail, segregation of duties, least
privilege, mandated approvals identified, break-glass, release communication, and release
authorization (lines 50-64). Three things make this *operationally* good rather than
cosmetic:
- The **Deliberate?** column in Part 1 (line 19) with the ◆ convention: *"A ◆ is not a
  failing score; an unmarked No is a gap."* This is the single most important fix for a
  director — my mandated prod approval no longer scores as a maturity failure. It directly
  operationalizes the debt-vs-control test from the governance doc inside the exercise.
- The callout under Part 2 (line 64): *"For a regulated or client-facing team, a 'No' here
  often outranks a 'No' in Part 1: an ungoverned fast pipeline is harder to adopt than a slow
  one."* That is my exact worldview, written into the tool.
- Part 4's constraint question now reads *"Skip the ◆ deliberate controls — those aren't gaps
  to close"* (line 90), and the "name the constraint" prompt no longer pre-answers itself.

A director scoring her estate with this is now prompted to assess control maturity, not just
engineering maturity. The gap between "documents governance" and "makes me practice it" is
closed.

## Still open or newly noticed

Real scrutiny applied; these are genuine, not invented. Both are *minor* relative to where
the course was two rounds ago.

1. **Break-glass post-incident review still names no accountable owner.** This was my
   round-2 nice-to-have #9 and an auditor question I flagged explicitly ("review by whom?").
   The mechanism is now concrete (scoped role, second-person ack, auto-expiry, alerting) —
   but `governance-and-compliance.md:90-91` and the glossary entry (`glossary.md:158`) both
   say "mandatory post-incident review" / "mandatory post-incident reconciliation" without
   naming the accountable role. An assessor will ask "who owns that review, and where is it
   recorded?" The auditor paragraph (line 138) repeats "mandatory post-incident review" with
   the same omission. One clause — "owned by the on-call lead / service owner and recorded in
   the incident record" — closes it. Low effort, real audit value.

2. **The `customer-facing` / `no-user-impact` MR gate is still named-but-not-shown.**
   `communicating-releases.md:68-73` correctly elevates the label from a hope to a *check*:
   *"Back it with a light MR gate: an MR that adds or flips a flag must carry either a
   `customer-facing` label … or an explicit `no-user-impact` label — fail the check if it has
   neither."* That is the right control instinct, and it answers R2-M4's discretionary-data-source
   worry **in prose**. But there is no actual implementation anywhere in the tree — `grep`
   finds the labels only in `communicating-releases.md` and `governance-and-compliance.md`,
   never in `.gitlab-ci.yml` or any CI snippet. This is the same "named but not shown" pattern
   round 2 flagged for `smoke-test.sh` (which *was* made real). My release-notes data source —
   the thing I'd defend to a client as "how we guarantee you hear about changes" — still rides
   on a check that is described but not coded. For a course that shows its pipeline work in
   YAML, this one deserves a six-line `rules`/`grep`-the-labels job so the gate is real, not
   aspirational. It's the last instance of the round-2 "the precision trailed the prose" theme
   in *my* area.

Two things I checked and am satisfied are **not** gaps:
- **Glossary now backs the new vocabulary.** SoD, audit trail, break-glass, and emergency
  change all have substantive entries (`glossary.md:151-161`), each cross-linked to the
  governance doc. My round-1/round-2 nice-to-have is done.
- **The "controls are debt" framing is gone and stays gone.** Re-read end to end, the
  governance doc's thesis is "more governance, not less" (line 146), the manual-gate test is
  consistent across doc/glossary/troubleshooting/session-3, and the assessment's ◆ column
  enforces it in practice. I could not find a single place that still reads my contractual
  control as maturity debt.

## Bottom line for an eng leader

**Greenlight — unconditionally, for the whole estate, not just greenfield.** I would now
sponsor this course *and* hand the governance doc + the auditor paragraph to my compliance lead
with light tailoring, and the controls scorecard to every team lead as a required Phase-0
artifact. The two gaps that held me at 9 are closed at the source (assessment governance
dimension; freeze carve-out), and the four R2-M3 precision items an auditor would have caught
are now concrete and defensible. No remaining **blocker**. The only two residuals are a
one-clause break-glass-owner addition and making the `customer-facing` label gate real YAML —
both are finish-the-job polish, not adoption risks. That is why this is 9.5 and not 10: the
governance *story* is airtight; one of its controls is still described rather than enforced in
code, and the emergency-review accountability has no named owner.

**Round-3 rating: 9.5/10** (up from 9/10).
