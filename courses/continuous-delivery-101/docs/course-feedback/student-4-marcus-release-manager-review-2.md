# Continuous Delivery 101 — Review 2

**Student:** Marcus (Release Manager, 8 yrs)
**Stance going in:** I personally run the weekly prod deploy AND curate the
client-facing release-notes email — I push teams to tag Jira stories with a
release number because that tagging is how I assemble the weekly "here's what
changed" email. My round-1 headline concern: if we ship continuously, what
replaces that email, and where does its data come from once "release number"
tagging collapses? (Note: RealManage has no QA team — delivering teams own
quality — so my round-1 "QA gate" framing is moot; my live concern is release
communication and my own role transition.)
**Review date:** 2026-06-19
**Overall rating:** 9/10 — would I adopt/champion this? (up from 6/10)

## Executive summary

They fixed the hole. The whole hole. There is now a dedicated resource —
`resources/communicating-releases.md` — that takes my exact round-1 objection,
states it in my own terms (deploy ≠ release, the release-number tag stops meaning
anything, the flag flip is the customer-visible event), and gives me an operable
replacement: anchor the note to the **flag flip**, capture it via a
`customer-facing` MR label plus a **flag inventory whose flip date is the release
date**, and rebuild the weekly digest from *user-facing releases* instead of the
deploy log. It even names my role transition explicitly and respects it rather
than automating me away. Sessions, glossary, troubleshooting, and the migration
checklist all now point at it, so a team can't faithfully execute the migration
and silently drop the email anymore. This is the difference between "strong
engineering course with a release-manager-shaped hole" and "course I will
champion." It's a 9, not a 10, only because two follow-throughs I asked for are
still loose: the current-state assessment still can't *see* my function, and the
timing-gate is still narrated as the "Engineering Lead's" decision in the
sessions even though the new resource clearly makes flag-flip timing mine.

## Round-1 point tracker

For each significant point from review 1: status + the specific change.

### Headline: the weekly email problem — **IMPROVED (resolved)**

Round 1 this was the single issue that would make me resist rollout. It is now
answered directly and operably.

- **The problem is named in my language.** `resources/communicating-releases.md:7-33`
  states the three joints CD breaks — deploys become continuous (no weekly bundle
  to number), deploy ≠ release (flip can lag the deploy by weeks), and therefore
  "release number on the story" stops meaning anything (lines 26-29). That is
  *exactly* my round-1 argument, including the failure mode I described: generate
  notes from the deploy log and you either spam every deploy or announce dark code
  that isn't live (lines 31-33). They didn't just patch around my objection — they
  adopted its logic.
- **The data source is real and operable.** `communicating-releases.md:52-66`:
  label the user-facing MR `customer-facing` and write one plain-language line
  (that line *is* the release note); keep a **flag inventory** where "the flip
  date is the release date" (line 61); assemble the changelog at flip time from
  the customer-facing items, not `git log`. This is the design insight I said the
  course "already had all the pieces for and just never assembled." Now it's
  assembled. Can I actually build next week's client email from it? Yes:
  the flag inventory is my source of truth, the flip dates give me the week's
  releases, and the `customer-facing` lines are the copy. That is a workflow I
  can run Monday.
- **Cadence is correctly decoupled.** `communicating-releases.md:68-79` makes the
  point I made — communication cadence is independent of deploy cadence: keep the
  weekly digest if clients like it, or per-feature, or both. This is the
  deploy/release insight applied to notification, which is precisely the
  recommendation I wrote in round 1.
- **Migration "done" now gates on it.** `migration-checklist.md:88` adds the Phase
  4 item: "Release communication is re-established on the new footing: notes built
  from user-facing releases (flag flips), not the deploy log." `communicating-releases.md:110-118`
  repeats a four-item Phase-4 gate (notes from flips; flag inventory with dates
  support can see; agreed cadence; evolved role named/owned). My round-1 fear was
  that a team executing the checklist literally would decommission the weekly
  release and never be prompted that they'd orphaned the email. That gap is closed.

This was my recommendation #1 and #2 from round 1. Both delivered.

### "Release number tagging collapses" — **IMPROVED (resolved)**

My specific operational anxiety — once we stop the weekly bundle, the Jira
release-number field I curate from has nothing behind it. `communicating-releases.md:64-66`
retires the release number explicitly and replaces it with "a per-release
identifier on the user-facing change plus its flip date… numbering *releases*
(what clients got), not *deploys* (what servers got)." That is the cleanest
possible answer to "where does the email's data come from now." Verified against
my actual workflow: it maps story-tagging → flag-flip-tagging without leaving me
a dead field.

### Customer appears only as a rollback trigger — **IMPROVED**

Round 1 I noted "customer" existed in the course only when something breaks
(rollback trigger). Now customers are first-class as people you proactively
inform: the entire `communicating-releases.md`, plus the internal/external/support
breakdown at lines 82-90 — including the genuinely sharp point that with canary
and dark launch a feature may be live for 10% of users before it's announced, so
support needs the flag inventory and flip dates to answer "is this rolled out
yet?" (lines 83-86). That's a real support-handoff design, not a hand-wave.

### My role transition — **IMPROVED, and it feels respected**

Round 1 I worried the course was "written from the builder's seat, not the
gatekeeper's" and quietly reassigned my judgment to engineering.
`communicating-releases.md:92-108` addresses this head-on and, frankly,
generously: it says plainly the pipeline takes over the manual deploy I run by
hand today, then — "That isn't the role disappearing — it's the role moving up to
the work that actually needs judgment": owning the release-communication data
source, governing flag flips (which features go live when and to whom), and
customer/support comms. Line 106-107: "The weekly manual deploy was the least
valuable part of the job. CD automates that and leaves the judgment." I'll be
honest — that lands. The least valuable part of my week genuinely *is* the
mechanical Friday deploy; the judgment is the curation and the timing. The course
saw that correctly.

### Communication hangs off the release, not the deploy — **IMPROVED**

This was the one design insight I said the course owned but never stated.
It's now stated in the spine of the course, not just a side resource:

- `session-3/README.md:47`: "Decoupling deploy from release also moves
  *communication*: under CD you announce the **release** — the flag flip users
  actually feel — not the deploy. The weekly client email doesn't die; it gets
  rebuilt from user-facing releases instead of the deploy log."
- `session-3/README.md:153` (wrap-up key takeaway): "Communicate the *release*
  (the flag flip users feel), not the deploy — rebuild release notes from
  user-facing releases." Putting it in the certification-level takeaways means
  every graduate carries it, not just the release manager who reads the appendix.

### Glossary had no word for release communication — **IMPROVED**

Round 1: no glossary entry for release notes / changelog. Now `glossary.md:97-98`
adds "**Release notes (changelog)** — The human-facing record of what changed
*for users*. Under CD it is anchored to releases — feature-flag flips /
user-facing changes — not to deploys." Cross-linked to the resource. The
vocabulary now contains my deliverable.

### Troubleshooting missing the release-manager objection — **IMPROVED (resolved)**

Round 1: the objection guide whose purpose is "the objections you'll hear most"
didn't contain mine. Now `troubleshooting.md:35-37` is literally titled "If we
ship continuously, what happens to our weekly release email?" and answers with
the deploy≠release / build-from-flag-flips logic, links to the playbook, and
names the evolved role. This is my recommendation #5, delivered verbatim in
spirit. The next release manager who takes this course will see their job in the
FAQ.

### cd-vs-continuous-deployment "maybe alongside a comms email" — **IMPROVED**

Round 1 this was the *only* mention of communication in the whole course — a
half-clause offered as a timing example. It's been upgraded: the example now ends
"And what you announce is the **release**, not the deploy — see [Communicating
Releases]" and adds a Related link "what replaces the weekly release email under
CD." The throwaway became a doorway.

### Audience doesn't name Release Management — **STILL OPEN (minor)**

Round 1 recommendation #3: add QA/Release Management to the target audience.
`README.md:30-36` still lists only Software Engineers, Tech Leads, Engineering
Leaders, and Teams; `CLAUDE.md` target audience is the same. Release Management is
not named as an audience. This costs almost nothing to fix and would convert the
exact skeptic this persona represents. It's lower-stakes now that the *content*
serves me, but it's still a visible omission: the role whose job the new resource
reshapes isn't invited to the course on the front page.

### Name the timing gate as the release manager's, not only "Engineering Lead's" — **PARTIALLY ADDRESSED**

Round 1 recommendation #3 (second half): the human who approves *timing* is
narrated as an "Engineering Lead" everywhere, never as a release manager.
Progress: `communicating-releases.md:100-102` now explicitly puts "governing flag
flips — coordinating *which* features go live *when* and *to whom*" in the release
manager's evolved role, and calls it "where release management meets governance."
So the *flag-flip timing* decision is now mine in the resource. **But the session
body still narrates the prod-timing gate as the Engineering Lead's**:
`session-3/README.md:45` ("Engineering Lead note: a manual prod gate is a
legitimate control… it approves timing"). The two threads aren't joined: Session
3 says an Engineering Lead approves prod-deploy timing; the resource says the
release manager governs flag-flip timing. Under CD those can be the *same*
release-timing decision, and a reader could conclude the Engineering Lead owns
"when it goes live" while I only write the note afterward. One sentence in
`session-3/README.md:45` acknowledging the release manager as a legitimate owner
of the flip/timing decision would close this. Half-credit: my role is named and
real, but the timing authority is still split across two roles without
reconciling them.

### Current-state assessment doesn't represent my function — **STILL OPEN**

Round 1 recommendation #6: add a communication/notification row so a team scoring
itself notices whether release information actually reaches downstream parties.
`exercises/current-state-assessment.md` is unchanged on this axis: Part 1 is the
nine CI/CD minimums (lines 19-40), Part 2 is DORA throughput/stability (lines
48-56), Part 3 names an *engineering* constraint (lines 63-66), Part 4 picks a
pilot. There is still **no row, metric, or prompt** for "how do downstream
parties learn a release happened?" A team scoring itself all-green on this
assessment can still have zero release-communication capability and the exercise
won't surface it. Given the migration checklist now gates Phase 4 on
re-establishing comms, the *assessment* should at least ask a Phase-0 question
about it so the team enters the migration knowing comms is in scope. This is the
one place my seat is still invisible. It's the main reason this isn't a 10.

### Re-attempting the assessment with my release-comms hat on

I ran Part 1-4 again as the cross-team release owner. Same result as round 1 on
the engineering rows (mostly No/Partial — week-plus branches, I-decide-timing,
rollback unrehearsed), and the gap is the same: nowhere to record that *the
weekly release is also the weekly communication event*. I can now answer that
concern by reaching for `communicating-releases.md` — which I couldn't in round 1
— but the assessment itself still doesn't *route* me there. The fix is one
Phase-0 prompt ("Where does release information reach customers and support
today, and what is it built from?") that hands off to the new resource. Small,
and it would make the exercise see the function the rest of the course now
serves.

## New problems / anything wrong or confusing this round

Mostly clean. Three small notes:

1. **Session 2 still doesn't link the comms story (PARTIALLY ADDRESSED gap).**
   `session-2/README.md:68-81` is where "release = flip the flag, no deploy
   needed" is *taught* — the exact moment a reader first learns the customer-visible
   event has no deploy behind it. That's the most natural place to plant "…and
   here's how people learn the flag flipped." Session 3 and the resources carry
   it, but the insight is introduced in Session 2 and the comms consequence isn't
   flagged until Session 3. Not wrong, just a missed adjacency — the reader sits
   with the unanswered question for a full session. A one-line pointer to
   `communicating-releases.md` at `session-2/README.md:79` would fix it.

2. **`customer-facing` label discipline is asserted, not enforced.**
   `communicating-releases.md:56-60` rests the whole data source on engineers
   reliably labeling the right MR `customer-facing` and writing a good line. That's
   a human-discipline dependency — the same class of risk the course elsewhere
   distrusts ("if it isn't gated automatically, it isn't part of your definition of
   deployable — it's a hope," `session-3/README.md:63`). My round-1 idea was to
   make "release note written" part of the *definition of deployable* so it's a
   pipeline gate, not a Friday scramble. The resource stops just short of that — it
   makes the label a convention, not a gate. Not a defect, but it leaves the data
   source's quality riding on discipline, and flag debt shows the same convention
   eventually rots. Worth a sentence: "consider gating user-facing MRs on a
   present `customer-facing` note." This would make my data source as trustworthy
   as the pipeline's verdict.

3. **No new factual errors spotted.** The flip-date-as-release-date model, the
   canary/dark-launch "live before announced" point (lines 83-86), and the
   internal-vs-external split are all operationally sound and match how this
   actually works. Nothing to flag for the infra reviewer here.

## Recommendations (round 2)

### High priority

1. **Add a release-communication prompt to the current-state assessment.** One
   Phase-0 question — "Where does release information reach customers/support
   today, and what is it built from?" — routing to `communicating-releases.md`.
   This is the last place the assessment can't see my function, and the migration
   checklist now expects comms in scope.

### Medium priority

2. **Reconcile the timing gate across Session 3 and the resource.** In
   `session-3/README.md:45`, acknowledge the release manager (not only an
   Engineering Lead) as a legitimate owner of the release-timing / flag-flip
   decision, linking to `communicating-releases.md:100-102`. Joins the two threads.
3. **Name Release Management in the audience** (`README.md:30-36`, `CLAUDE.md`).
   Cheap; converts the skeptic.
4. **Link the comms story from Session 2** at the flag-flip teaching moment
   (`session-2/README.md:79`).

### Nice to have

5. **Consider making "release note written" a gate** for `customer-facing` MRs,
   so the data source is enforced like the rest of the definition of deployable
   rather than left to convention (`communicating-releases.md:56-60`).

## Verdict

**Champion.** Round 1 I would not put my name on retiring the weekly release
until someone told me what carries the customer-and-support communication
afterward — the course now does, concretely, operably, and in my own language,
and it treats my role transition as a promotion rather than a deletion
(`resources/communicating-releases.md`, with hooks from Session 3, the glossary,
troubleshooting, and the migration checklist). The remaining gaps are
finish-work, not holes: the current-state assessment still can't see my function,
and the prod-timing gate is still narrated as the Engineering Lead's. Fix the
assessment prompt and I have nothing left to resist. **9/10, up from 6.**
