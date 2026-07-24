# Session 3 — Slide Outline (build in your tool)

**How to use this:** one entry per slide. *Message* is the single idea the slide must
land. *Visual* is an image-heavy suggestion you build in PowerPoint / Canva / Gamma (or
as inline SVG, as Sessions 1–2 did) — the slide should be mostly that image. *On-slide
text* is the few words that go on the slide (keep it to this). *Say* is your talking
track — it does **not** go on the slide.

The run-sheet is [`../sessions/session-3/README.md`](../sessions/session-3/README.md);
this covers the ~14 present-the-concept moments plus two Session-2 callbacks in the recap
block (2A flags-aren't-free, 2B the undeployable-trunk war story), three section dividers,
and a two-slide workshop hand-off (18 concept slides in all). The 30-minute workshop (score
the baseline → read the target pipeline → write the migration plan) is hands-on — the target
`.gitlab-ci.yml` opens *in the room*, not on a slide.

Target: **~18 concept slides + 3 dividers** for a 2-hour session — the same shape as
Session 2 (≈85 min teaching + a 30-min workshop). Same rule as before: if a slide needs
more than one sentence of on-slide text, it's doing too much — push it into *Say* or split
it.

Two code walkthroughs (slides 9 and 12) use Session 2's **framing slide → editor**
pattern: the slide sets up the *why* and the discipline points, then you switch to the
real file (`template.yaml`, then `rollback-on-aws.md`). Reuse the Session 2 layout
primitives already in `theme/cd101.css` (section dividers, the without/with contrast, the
`→ filename` editor hand-off pill). Session 1 drew the two-rail pipeline; Session 2 added
the git graph and flags. **This session layers deploys and promotion onto that base** — a
single track from commit to production with `dev → qa → prod` stations. This is the last
session, so the close carries course certification, not just homework.

---

## Slide 1 — Title

- **Message:** Session 3 is the machine that turns "the trunk is always deployable" into
  "any change reaches production on demand, safely" — the pipeline as the single,
  definitive path to prod.
- **Visual:** Full-bleed hero in the course palette, continuing the through-line: Session
  1's parcels-on-a-conveyor and Session 2's merges-onto-the-trunk now feed **one track that
  runs the whole way to production** — `commit → build → dev → qa → prod` stations along a
  single rail, a lone padlocked gate at `prod`. Reuse the course hero gradient; drop a real
  photo in later via reveal's `data-background-image` if you want.
- **On-slide text:** "Continuous Delivery 101" · "Session 3 — The Pipeline: Single Path to
  Production" · "Green means deployable."
- **Say:** Set the frame and objectives. By the end they can: treat the pipeline as the
  *sole* path to every shared environment; express "deployable" as automated criteria, not
  a meeting; build one immutable artifact and promote the *same* bytes dev → qa → prod;
  design fast, safe recovery on AWS (fail forward first, roll back when it's costly); and
  write their team's phased migration plan. The one-line arc of the course: Session 1 was
  the *why* (small batches, the minimums), Session 2 was the first *practice* (trunk-based
  dev, flags, CI) — today is the *system* those two make possible. And keep the course
  refrain: this is about *how* we deliver, independent of platform; the new AWS services are
  just where it's cheapest to establish.

---

## Slide 2 — Recap → Connect

- **Message:** Sessions 1–2 earned us an always-deployable trunk; what carries it to prod
  differs by stack — the new cloud-native services ship straight from trunk, while the
  established .NET apps still route `qa`/`prod` through a weekly release branch (the
  long-lived branch Session 2 told us to retire).
- **Visual:** Split panel. Left (small): a compressed recap glyph — the short-lived
  branches merging onto the trunk from Session 2, capped with a small teal "always
  deployable" tag. Right (the bigger half): **two paths to prod**, one per stack.
  **New — AWS cloud-native:** `trunk` → `dev` (auto) → `qa` (manual) → `prod` (manual);
  ships straight from trunk. **Established — .NET on Azure VMs:** `dev` also auto-deploys
  from trunk, but `qa` and `prod` deploy from a `release/YY.M.W` branch (orange) — `qa`
  auto on the branch, `prod` manual — annotated *weekly · many commits · cherry-picks*.
- **On-slide text:** "You have a deployable trunk — what carries it to prod?" (diagram only;
  the "release branch is the long-lived branch to retire" line is talk track, not on-slide).
- **Say:** 60-second recap only — don't re-teach Sessions 1–2. Small batches, daily
  integration, flags, CI gates gave us a trunk that is *always deployable*. Then the
  connect: have each person name, out loud, **how a change reaches prod on their stack
  today — and notice the two shapes.** New cloud-native services (our `iac-baseline`):
  straight from trunk — `dev` auto on green, `qa`/`prod` behind manual gates, no extra
  branch. Established .NET apps (verified against `ciranet-attorney-api`'s shared dotnet
  ci-template): `dev` also auto-deploys from trunk, but `qa` and `prod` deploy from a
  `release/YY.M.W` branch **cut weekly by a scheduled job** — `qa` auto on that branch,
  `prod` a manual rolling wave deploy across the app servers; a week of commits pile in and
  cherry-picks are routine. **The point:** that release branch is exactly the long-lived
  branch Session 2 told us to retire — weekly batching and cherry-picks are its cost. (For
  the older apps a *release manager* owns cutting and curating the weekly release — mention
  it, but the burden is the branch, not the person.) Don't fix it yet; surface it so the
  "pipeline decides releasability" slide lands on their own process. Name the running
  example once: the **HOA Violations API** — a TypeScript Lambda behind API Gateway that
  records a violation to DynamoDB and publishes a `ViolationRecorded` event to SNS. Every
  slide today is grounded in that one service.

---

## Slide 2A — A flag is not free (Session 2 callback)

- **Message:** You *can* merge unfinished work behind a flag (Session 2) — but a flag is
  complexity you carry in every environment until you remove it. Default to the cheapest
  mechanism that keeps the trunk deployable; reserve flags for when they earn their keep.
- **Visual:** Two-column contrast (reuse the Session 2 without/with primitive). Left, teal
  — **Default: no flag:** behavior change → branch by abstraction; new feature, nothing
  calls it yet → keystone (merge daily, reachable only from tests, wire the entry point in
  last); new API route → build it, expose it last; bug fix → just deploy. Right, orange — **A flag earns its keep when:** gradual rollout / test-in-prod
  (dark launch) · per-customer behavior · experiment (A/B) · cross-team coordination. Caption
  strip: flags double the test burden and nest into combinatorial state; unremoved Release
  toggle = flag debt. Pointer: full decision tree → beyond.minimumcd.org/…/feature-flags.
- **On-slide text:** the two columns + the cost caption + the decision-tree URL.
- **Say:** Callback to the Session 2 flag / branch-by-abstraction / ship decision — the
  beyond.minimumCD tree just formalizes it. Reach for a flag *last*, not first. Note the
  tension with later today: when a flag *is* warranted, it's also the fastest recovery lever
  (flip it off, no deploy). Powerful — but not free.

---

## Slide 2B — When the trunk isn't deployable (war story)

- **Message:** An always-deployable trunk isn't a nicety — it's what lets you ship the fix
  that matters, the moment it matters. Big batches + a red trunk take that away.
- **Visual:** `main` trunk (red, "won't go green") carrying **two large in-flight changes**,
  both broken. A prod-incident bell (orange) on the left needs a fix now; a **one-line fix**
  (teal) merges onto the same trunk but the deploy to prod is **blocked** by a no-entry gate —
  the broken work is in the way. Caption: *the fix was small — the trunk wasn't ready.*
- **On-slide text:** diagram labels + the two-line caption.
- **Say (talk-track):** Told blameless — no team, no names. Two large changes both in flight,
  both merged to main, both with issues; then a production problem surfaced and the one-line
  fix couldn't go out, because the pipeline ships the *trunk* and the trunk wouldn't go green.
  The small fix was trapped behind big broken batches. This is the whole case for small
  batches, stop-the-line when red (CI #5), new work not breaking delivered work (#6), and a
  trunk kept deployable at all times — exactly what today's single-path pipeline protects.
  Hands into Part 1.

---

## Divider — Part 1 of 3: The Single Path & Its Verdict

*(README agenda §2–§3. The three teaching blocks are numbered 1–3 for the audience; Review
/ Connect is the open and the workshop / wrap-up is the close, so they aren't part of the
count — same convention as Session 2.)*

- **Message:** Section marker: one path in, and a verdict that actually means something.
- **Visual:** Full-bleed section-title slide in the course palette (reuse the Session 2
  divider treatment). Large "The Single Path & Its Verdict", a faint single-rail-to-a-gate
  motif behind it. No prose.
- **On-slide text:** "Part 1 of 3 — The Single Path & Its Verdict"
- **Say:** One line: "Two ideas do the heavy lifting — there is exactly *one* way in, and
  the pipeline, not a meeting, decides what's deployable." Move to slide 3.

---

## Slide 3 — One path, no side doors

- **Message:** CD minimum #2 — the pipeline is the *only* way to deploy to any shared
  environment; the moment there's a second door, the verdict stops meaning anything.
- **Visual:** A shared environment (a `qa`/`prod` box) with **one guarded entrance** — the
  pipeline rail feeding it through an OIDC-badged gate — and a **side door bricked/​crossed
  out**: a laptop labelled `sam deploy` with a red no-entry over the path to the shared box.
  Off to the side, an *open* door the laptop *can* use: a small `dev-aws-violations-api-<you>`
  personal sandbox stack. Contrast: one way into shared, sandboxes are the escape valve.
- **On-slide text:** "One path to shared environments — via OIDC, no side doors" · "sandbox
  stacks are the escape valve"
- **Say:** The minimum: every change to a *shared* environment goes through the pipeline —
  no "just this once" `sam deploy` from a laptop to qa. Three reasons it's non-negotiable.
  **Reproducibility:** every shared-env change has the same provenance — a commit, a
  pipeline run, an audit trail. **Security:** deploy permissions live with the pipeline via
  GitLab **OIDC** — short-lived, per-environment role assumption, *no static AWS keys*
  anywhere, in anyone's hands or in CI. **Trust:** if there's exactly one path, the
  pipeline's verdict can *mean* something — which is the whole next slide. The escape valve
  that keeps this liveable: **personal sandbox stacks** (`dev-aws-violations-api-<username>`)
  are yours to `sam deploy` to and break freely — they're not shared, so they don't need the
  pipeline. Everything *shared* does. (AI note, one line: when an agent can emit a fix in
  seconds, the temptation to hot-fix straight to prod is *stronger* — that shortcut is
  exactly the side door this rule forbids.)

---

## Slide 4 — The pipeline decides releasability

- **Message:** CD minimum #3 — green should *mean* deployable, automatically; a human may
  choose *when*, but never re-litigate *whether*.
- **Visual:** Two panels over the same commit. **Left "Today" (muted):** green checks, then
  a **person** at a gate deciding "ready?" — the meeting/click owns the verdict. **Right
  "CD" (teal):** the same green checks flow straight through `dev` and `qa` **automatically**;
  only `prod` has a single deliberate gate, and that gate is labelled **"timing, not
  readiness."** A caption strip underneath differentiates the two words *Continuous
  **Delivery*** (deployable on demand; a human picks the moment) vs *Continuous
  **Deployment*** (every green commit auto-ships) — keep them distinct.
- **On-slide text:** "Green = deployable, automatically" · "a human approves *timing*, not
  *readiness*"
- **Say:** This is the minimum most teams haven't fully reached. Our own baseline now
  auto-promotes `dev` on green, but `qa` and `prod` are still `when: manual`, so the pipeline
  doesn't yet own the release verdict. The shift: **green should mean deployable, and the pipeline
  should say so without a meeting.** A human may still choose *when* prod happens — that's
  Continuous *Delivery*, not Continuous *Deployment*, and the distinction matters — but
  they're approving **timing**, not re-deciding **readiness** the pipeline already proved. The
  tell that you're not there yet: a meeting decides whether the build is "ready." In the
  target pipeline you'll read later, `dev` and `qa` **auto-promote on green** and only `prod`
  keeps a gate. **Engineering-Lead note** (say it, keep it honest): a manual prod gate can be
  a *legitimate, permanent* control — compliance, or early confidence in a new service —
  distinct from a *debt* gate that just re-litigates readiness the pipeline already proved.
  Keep the legitimate ones, name them as timing decisions, and remove the rest as trust
  grows. If someone gates `qa` to protect in-flight manual testing, that's an *isolation* gap,
  not a control — keep other work dark behind flags so it doesn't change the surface under
  test, and use an ephemeral env per change where the deploy itself disrupts; the gate's hidden
  cost is batching. Fix the isolation, don't enshrine the gate. And the stop-the-line rule from Session 2 now extends past the build to the
  **deployment pipeline**: a red pipeline is the team's #1 priority — no new feature work
  until it's green (minimum #6).

---

## Slide 5 — Definition of deployable

- **Message:** CD minimum #4 — "deployable" is a checklist the pipeline *runs*, not a
  judgement a person *makes*; if it isn't gated automatically, it isn't part of your
  definition of deployable, it's a hope.
- **Visual:** A **gate** labelled "Definition of deployable" with the five automated checks
  as lanes running through it — *lint · unit tests · coverage floor · IaC valid (`sam
  validate` / `cfn-lint`) · security scan* — all green, the change flowing through to
  "deployable." Small tag: "the front half of the pipeline (Session 2's `validate` / `test`
  / `security`)." A separate, clearly *outside-the-gate* sticky note: **manual verification →
  gates the flag flip, not the merge.**
- **On-slide text:** "Deployable = automated checklist, not a meeting" · "lint · tests ·
  coverage · IaC valid · security"
- **Say:** "Deployable" is exactly the set of checks the pipeline runs — for the Violations
  API: lint clean, unit tests pass, coverage at or above the team floor, IaC valid, security
  scan clean. These are the `validate` / `test` / `security` stages you already met in
  Session 2's CI file — the *front half* of the full pipeline. The discipline in one line:
  **if it isn't gated automatically, it isn't part of your definition of deployable.** Now
  the honest brownfield case — true across much of our estate: when those automated checks
  don't exist yet, manual verification does **not** vanish, but it stays *outside* the
  automated definition of deployable — it gates the **release** (the feature-flag flip),
  never the merge. Make it an explicit, owned step, not a side-channel sign-off, and automate
  it down over time by adding tests where you change code. (AI note: two cautions live right
  here. Coverage is a *smoke alarm*, not a target — when the same agent writes the code and
  its tests it will hit the number and verify nothing; tests must pin *intent*, confirmed
  *independently*. And add a **dependency-provenance / lockfile-integrity** check once an
  agent is editing `package.json` — hallucinated package names are a real supply-chain hole.)

---

## Slide 6 — Two decisions, two owners — communicate the release

- **Message:** Decoupling deploy from release splits the old "ship it" decision into two,
  with two different owners — and it's the *release*, not the deploy, that you communicate.
- **Visual:** One timeline, two clearly separated events (echo Session 2's deploy≠release
  timeline, now with owners attached). **Deploy** — code reaches prod, flag OFF, padlock —
  tagged *"engineering authorization · Eng Lead."* Later, **Release** — the flag flip, eye
  opens — tagged *"business decision · release-manager."* A small envelope on the *release*
  event: "the client email is built from *this*, not the deploy log."
- **On-slide text:** "Deploy = timing (engineering) · Release = the flag flip (business)" ·
  "announce the release, not the deploy"
- **Say:** Session 2 taught deploy ≠ release; here's what it does to *ownership and
  communication*. The prod **deploy** gate (if you keep one) is an **engineering
  authorization** — owned by the Engineering Lead: is this safe to put in prod now. The
  **release** — the flag flip users actually feel — is a separate decision about *which*
  feature goes live *when* and *to whom*, owned by whoever runs release comms and flag
  governance (our evolved **release-manager** role), with the business. Two decisions, two
  owners, often two different days. And it moves *communication*: you announce the
  **release**, not the deploy — the weekly client email doesn't die, it gets **rebuilt from
  user-facing releases instead of the deploy log.** That's why the target pipeline runs one
  merge-request-time job, `release-impact-label`, that forces every change to declare
  `customer-facing` (with a note) or `no-user-impact` before merge — so release notes have a
  reliable source. Pointer: `resources/communicating-releases.md`.

---

## Divider — Part 2 of 3: Build Once, Promote the Same Bytes

*(README agenda §4.)*

- **Message:** Section marker: the artifact discipline that makes "green in qa" actually
  predict "works in prod."
- **Visual:** Full-bleed section-title slide (reuse the divider treatment). Large "Build
  Once, Promote the Same Bytes", a faint one-box-copied-along-a-rail motif behind it.
- **On-slide text:** "Part 2 of 3 — Build Once, Promote the Same Bytes"
- **Say:** One line: "The verdict only means something if the thing you tested is the thing
  you ship — so build the artifact once and promote the *same bytes*." Move to slide 7.

---

## Slide 7 — Build once, promote the same bytes

- **Message:** CD minimum #5 — build the artifact *once* from one commit, then promote the
  *identical* bytes dev → qa → prod; rebuilding per environment ships something you never
  tested.
- **Visual:** The README's build-once diagram as the whole slide: `commit → build ONCE →`
  one artifact box that **forks unchanged** to `deploy dev` / `deploy qa` / `deploy prod`
  (three arrows off *one* box, not three build boxes). Label the box twice: outer tag
  `CI_COMMIT_SHA` ("how *you* find it"), inner tag `content hash` ("how *CloudFormation*
  knows the bytes didn't change"). A crossed-out "rebuild per env" variant, small, beneath.
- **On-slide text:** "Build once → promote the same artifact" · "rebuild per env = deploy
  something you never tested"
- **Say:** Build the Lambda bundle *once*, from one commit, then promote the *identical*
  artifact through dev → qa → prod — never rebuild per environment. Why it matters:
  **if you rebuild for prod, you've deployed something you never tested.** "Works in qa,
  breaks in prod" is almost always a rebuild, or config baked into the build. For SAM
  specifically: `sam build` then `sam package` uploads the code to an artifact bucket and
  emits a *packaged template* referencing it by a **content hash**; each environment's `sam
  deploy` consumes that **same** packaged template. Two keys, same bytes: `CI_COMMIT_SHA`
  labels the pipeline artifact so *you* can find and redeploy it; the content hash is how
  *CloudFormation* knows the bytes are unchanged. **Multi-account note** (common at
  RealManage): if dev/qa/prod are separate AWS accounts, the artifact bucket must be
  readable across accounts — a shared artifacts account or a cross-account bucket policy —
  so each stage promotes the same bytes. Rebuilding per account silently breaks the
  guarantee. (This immutable, stored artifact is also what makes rollback real in Part 3 —
  "the last good version" is a thing you can redeploy, not a memory of what `main` used to be.)

---

## Slide 8 — Config travels with the artifact

- **Message:** CD minimum #9 — the artifact is identical everywhere; only *config applied at
  deploy* differs, and it deploys *with* the artifact, never hand-patched into a running
  Lambda.
- **Visual:** The one artifact box from slide 7, now passing through three deploy stations;
  at each, a small `configuration/{dev,qa,prod}.config` card clips on — different values
  (table names, **flag values**, log levels, SNS topic ARN), same box. A separate locked
  card, *not* in the config files, points to "SSM / Secrets Manager ARN" for secrets.
- **On-slide text:** "Same bytes everywhere · only config differs at deploy" · "secrets are
  references, never values"
- **Say:** Environment differences live in `configuration/{dev,qa,prod}.config` (the
  `iac-baseline` convention) and arrive as SAM parameters — *not* hand-patched into a running
  Lambda. The artifact is identical across environments; only the config *applied at deploy*
  differs: table names, log levels, SNS topic ARNs — and **flag values**. Secrets are never
  in config files — they're *references* to SSM Parameter Store / Secrets Manager ARNs.
  Here's the payoff that ties back to Session 2: because a **flag's value is config, not
  baked into the immutable artifact, the same bytes run flag-ON in qa and flag-OFF in
  prod** — which is exactly what lets you test in qa while the change is already deployed
  *dark* to prod, then flip prod to release. Immutable artifact (#5) + config-with-the-artifact
  (#9) are what make deploy ≠ release mechanically true.

---

## Slide 9 — Read the artifact's contract (framing → editor)

- **Message:** In the real `template.yaml`, the whole Part-2 story is visible in one file —
  parameters carry the only per-environment differences, and the rollback machinery for Part
  3 is already wired in.
- **Visual:** Framing slide: three "look for these" chips over a faded SAM-template
  silhouette — *Parameters = the only thing that changes per env* · *flags arrive as
  parameters, default `false` (ships dark)* · *`AutoPublishAlias` + `DeploymentPreference` =
  rollback machinery (Part 3)* — then the `→ template.yaml` editor hand-off pill. Then switch
  to the actual file.
- **On-slide text:** "Parameters carry env differences · flags default off · canary wired
  in" · "`template.yaml` →"
- **Say:** Frame, then open the file. Three things to land at the editor. (1) The
  **`Parameters`** block is where environment differences arrive — `EnvironmentName`,
  `LogLevel`, the two flags — and the comment says it out loud: *the artifact is identical;
  only these change.* (2) `FlagViolationsRecord` / `FlagViolationsNotify` default to
  **`"false"`** — the artifact ships **dark**, and a flag flip (config), not a deploy,
  releases it — the mechanical cash-out of slide 8. (3) `AutoPublishAlias: live` plus
  `DeploymentPreference: Canary10Percent5Minutes` with a CloudWatch error **alarm** — that's
  the **rollback machinery** we build on in Part 3; every deploy creates an immutable version
  and shifts traffic gradually. Point out, briefly, that the alarm watches the **error
  *rate*** of the `live` alias, not a raw count — a threshold of 1 error would roll back a
  healthy deploy on a single stray error, and a canary over an idle window verifies nothing
  (missing data reads as OK). The canary is only as strong as the traffic during its window.
  Keep this file open next to Session 3's `.gitlab-ci.yml` in the workshop.

---

## Divider — Part 3 of 3: Production-Like Environments & Recovery

*(README agenda §5.)*

- **Message:** Section marker: prove it somewhere prod-like, and know exactly how you
  recover when it still goes wrong.
- **Visual:** Full-bleed section-title slide (reuse the divider treatment). Large
  "Production-Like Environments & Recovery", a faint qa-mirrors-prod / two-way-arrow motif
  behind it.
- **On-slide text:** "Part 3 of 3 — Production-Like Environments & Recovery"
- **Say:** One line: "Green only means something if it's green somewhere that looks like
  prod — and even then, things break, so the last thing we build is how we recover." Move to
  slide 10.

---

## Slide 10 — A production-like environment

- **Message:** CD minimum #7 — the closer qa mirrors prod's shape, the more a green qa
  actually predicts prod.
- **Visual:** Two stacked environment diagrams side by side — `qa` and `prod` — **same
  resource types and topology** (Lambda + API Gateway + DynamoDB + SNS), drawn identically,
  with qa visibly *smaller scale* (thinner, shorter log-retention tag: `qa 30d` vs `prod
  365d`). A dial between them: "how prod-like? = how much a green qa means."
- **On-slide text:** "qa mirrors prod's shape, at smaller scale" · "the closer it is, the
  more green means"
- **Say:** Keep this short — it's a 5-minute beat. Our `qa` mirrors `prod`'s *shape* via
  `configuration/qa.config`: same resource types, same topology, smaller scale (you saw it
  in the template's `Mappings` — log retention 7 / 30 / 365 days for dev / qa / prod). The
  principle is simple and unforgiving: **the closer qa is to prod, the more a green qa
  *means* something.** Divergence between qa and prod is exactly where "passed qa, broke in
  prod" is born — and it's the other half of the same guarantee as build-once: same bytes,
  prod-like shape.

---

## Slide 11 — Fail forward — the default

- **Message:** Because shipping a change is now small, fast, and safe, the *default* response
  to a production problem is to ship a small fix *forward* through the same pipeline — not to
  roll back.
- **Visual:** A production problem (alarm) on the timeline; the **default** path drawn bold
  and forward: `reproduce with a fast test → small fix MR → same pipeline → canary verifies →
  healthy in minutes`. A faint backward arrow ("roll back") sits greyed as the *exception*,
  to be earned on the next slide. Small callout: the actual *fastest* move when the feature is
  flagged — **flip the flag off** (no deploy).
- **On-slide text:** "Default = ship a small fix forward" · "the fix actually fixes it; a
  rollback only defers it"
- **Say:** The whole point of the pipeline is that shipping is now small, fast, and safe —
  and that changes your *default* when prod misbehaves. **Fail forward:** diagnose, reproduce
  it with a fast unit test (which now also guards the regression from returning), ship a small
  fix MR, and let the same canary-verified pipeline carry it to prod in minutes. Why this is
  usually right: a forward fix **solves** the problem — a rollback only **defers** it, you
  still have to fix and re-ship; it's safe by the same logic as any deploy (small batch,
  tests, canary); and it sidesteps the data trap on the next slide. The genuinely fastest
  mitigation, when the bad behavior is behind a **feature flag, is to flip it off** — no
  deploy at all — then fix forward behind the flag at a calm pace. (AI note, one line: an
  agent produces the forward fix in minutes, which makes fail-forward *more* clearly right —
  but the fix needs the **same gates** as any change; the incident is the most tempting, and
  most dangerous, place to let unreviewed work skip the pipeline.)

---

## Slide 12 — Roll back — the emergency lever (framing → editor)

- **Message:** CD minimum #8 — keep rollback fast and rehearsed, but reach for it only when a
  problem is *costly **and** time-sensitive*; our rollback is re-running the last good GitLab
  deployment, and rolling code back does not roll data back.
- **Visual:** Framing slide. A two-condition gate — **costly AND time-sensitive** → *roll
  back now, fail forward after* — then four labelled recovery mechanisms as a compact stack:
  *1 re-run last good GitLab deploy (our rollback)* · *2 canary auto-rollback (on by default)*
  · *3 CloudFormation auto-rollback (failed updates)* · *4 flag kill-switch (no deploy)*. A red
  warning band across it: **rolling *code* back ≠ rolling *data* back → expand/contract.** Then
  the `→ rollback-on-aws.md` editor hand-off pill; switch to the file.
- **On-slide text:** "Roll back when costly *and* time-sensitive" · "re-run the last good
  deployment · code back ≠ data back" · "`rollback-on-aws.md` →"
- **Say:** Minimum #8 still requires you to *have* fast, rehearsed rollback — it's the
  emergency lever, not the reflex. Reach for it only when **both** are true: **costly** (users,
  the board, data, revenue, or compliance being harmed *right now*) **and time-sensitive** (a
  forward fix won't land fast enough). Then roll back to stop the bleeding, and fail forward
  once the pressure's off. At the editor, walk the file: **our rollback is re-running the last
  known-good deployment in GitLab** — because every artifact is immutable and stored (tagged by
  `CI_COMMIT_SHA`, kept 30 days), that's a *redeploy of the previous good build through the
  same pipeline*, not a rebuild — **never a hand-edited Lambda alias**, which bypasses the
  pipeline (#2) and drifts from IaC. Behind it: canary auto-rollback (the `template.yaml`
  `DeploymentPreference` from slide 9), CloudFormation auto-rollback for failed updates, and
  the flag kill-switch. The trap to burn in: **rolling code back does not roll data back** — if
  the bad version already wrote a new DynamoDB shape or emitted an SNS event, reverting code
  strands you on data the old code can't read; that's why changes stay backward-compatible
  (**expand/contract**), and once state has moved, failing forward is often the *only* safe
  option. Close with the rule: **rehearse it in qa** — deploy a deliberately broken version,
  practice and *time* both the fail-forward loop and each rollback move. A recovery move you've
  never run is a hope, not a capability — only then does #8 count as met.

---

## Slide 13 — The database is no exception (bonus)

- **Message:** Every rule this session applies to schema too — pipeline-only, immutable
  runner promoted across envs, forward-only recovery.
- **Visual:** The three Part-1/2/3 icons (single path · one immutable artifact · forward-only
  recovery) re-drawn over a **database** cylinder instead of the Lambda — same shapes,
  different subject: one migration pipeline into a shared DB, one migration-runner artifact
  promoted dev → qa → prod, a "new forward script + expand/contract" arrow (no backward
  hand-edit). Small tag: "worked end-to-end with DbUp — deliberate C#/.NET example."
- **On-slide text:** "Same minimums for schema" · "pipeline-only · immutable runner ·
  forward-only recovery"
- **Say:** Time-permitting bonus — the database is not a special case. The **pipeline is the
  only way** to change a shared database (#2). The **migration runner is an immutable
  artifact** built once and promoted — the *same* scripts run against dev → qa → prod,
  varying only the connection string (#5). And recovery is **forward-only**: a bad migration
  is fixed by a new *forward* script plus expand/contract, **never a hand-edit on a shared
  database** — the exact analog of fail-forward for code. The end-state we're driving toward:
  schema and baseline data change *only* through automation, local databases as the
  playground, no standing human DDL on shared environments — and **DX owns that path**; here
  we just make the mechanism concrete. Worked end to end in the DbUp example (C# on purpose —
  the schema estate lives in .NET; one of the course's two deliberate non-TypeScript
  examples). Pointer: `resources/database-delivery.md`.

---

## Slide 14 — Workshop A: Score the baseline, read the target pipeline

- **Message:** We don't learn the pipeline against a strawman — we score our *own* real one,
  then read the target it migrates toward.
- **Visual:** "Your turn" workshop cue (mirror Session 2's hands-on signal — slides pause,
  hands-on starts). Two panels: **left**, the current-state scorecard (the 9 CD minimums with
  ✅/⚠️/❌ chips, the baseline's honest mix — strong on single-path/OIDC/immutable, ⚠️ on
  "pipeline decides releasability"); **right**, the target `.gitlab-ci.yml` with five numbered
  "find these" pins. A small note: "the target file opens in the editor, not on this slide."
- **On-slide text:** "① finalize your scorecard  ② read the target pipeline — find the 5
  things" · "green flows automatically ← the gap you'll close"
- **Say:** Transition to the workshop (15 min of the 30). **First, finalize your
  [Current-State Assessment](../sessions/session-3/exercises/current-state-assessment.md) as
  a team** — you started it after Session 1; now, with every minimum and the governance
  controls in hand, score the rows you left open. That completed scorecard plus your value
  stream map is your **Phase-0 baseline**. Then read pipelines — start with the [current-state
  walkthrough](../sessions/session-3/examples/current-state-pipeline-walkthrough.md): our real
  `iac-baseline` pipeline scores *honestly both ways* — it already nails single-path, OIDC,
  immutable artifacts, config-with-artifact, prod-like qa, ordered promotion, and already
  auto-promotes `dev` on green — but `qa` and `prod` are still `when: manual`, so a human still owns the release verdict. Now **open the target
  [`.gitlab-ci.yml`](../sessions/session-3/examples/.gitlab-ci.yml) in the editor** and locate
  five things: (1) where the **definition of deployable** is enforced (`validate` / `test` /
  `security`); (2) where the **immutable artifact** is built once and how it's promoted
  (`build:artifact` → `deploy:*` all consuming `${PACKAGED_TEMPLATE}`); (3) where
  **releasability is decided** and how it differs from the baseline (the target also auto-promotes
  `deploy:qa` via `needs:` — the baseline still gates qa by hand — leaving only `deploy:prod` `when: manual`); (4) how **OIDC** gives
  each env its own role with no static creds (`.aws_oidc`); (5) where a **smoke test** verifies
  each deploy (`smoke-test.sh`, so promotion is *verified*, not just attempted). The gap —
  automating the qa gate the way `dev` already is — **is** your migration plan.

---

## Slide 15 — Workshop B: Write your migration plan

- **Message:** Turn the scored gap into a phased plan your team can actually run on Monday.
- **Visual:** The five-phase migration path as a horizontal ramp — **Phase 0 Assess → 1
  Foundations → 2 Pipeline → 3 Optimize → 4 Deliver on demand** — each phase a rung with its
  one-line exit question underneath. A "start here" pin on Phase 0 (your completed scorecard),
  and a DORA badge on the far end (frequency ↑, lead time ↓, change-fail steady/↓, restore ↓).
- **On-slide text:** "Assess → Foundations → Pipeline → Optimize → Deliver on demand" · "name
  the one binding constraint · pick the DORA metric that proves it"
- **Say:** Second half of the workshop (15 min). Using the [Migration
  Checklist](../resources/migration-checklist.md) and your completed scorecard, draft your
  team's plan. **Phase 0 (Assess):** your value stream map + scorecard, and the *one* binding
  constraint they agree on (branch lifetime? manual deploys? slow tests? no rollback?).
  **Phase 1 (Foundations):** the CI/TBD behaviors to adopt next — branch-lifetime target,
  daily integration, flags, stop-the-line. **Phase 2 (Pipeline):** the gaps you just found —
  automated definition of deployable, promote-don't-rebuild, rehearsed rollback, removing
  *debt* manual gates. **Phase 3–4 (Optimize / Deliver on demand):** what "done" looks like
  for the pilot service and which **DORA** metric proves it. Two honesty notes to say aloud:
  it's **a quarter, not a sprint**, and expect a **velocity dip in Phase 1** — tell leadership
  to watch *stability* (DORA) alongside speed so the dip isn't mistaken for failure. And deal
  with existing long-lived branches on day one — you can't do trunk-based dev on top of a pile
  of week-old branches.

---

## Slide 16 — Wrap-up, takeaways & course certification

- **Message:** One pipeline, one verdict, one artifact, a rehearsed recovery, and a phased
  plan — that's Continuous Delivery, and you've now done the whole course.
- **Visual:** Six takeaway tiles (below), then a **course-completion panel**: "You've
  completed Continuous Delivery 101" with the five can-you-do-it criteria as a checklist, and
  a callout — **re-run the Current-State Assessment in a month and measure how far you moved.**
  Faint through-line motif tying Sessions 1 → 2 → 3 (batch size → trunk+flags → the pipeline).
- **On-slide text:** "Takeaways · you've completed CD 101 · re-score in a month"
- **Say:** Six takeaways: **one pipeline, one path** — shared environments deploy only through
  it, via OIDC; **"deployable" is automated criteria** — releasability belongs to the pipeline,
  *timing* to a human; **communicate the release** (the flag flip users feel), not the deploy;
  **build once, promote the same bytes**, vary only config; **fail forward by default**, keep
  rollback fast and rehearsed for costly, time-sensitive problems; and **the migration is
  phased** — shrink batches, build the path, optimize, deliver on demand. Then close the
  *course*: you've completed CD 101 when you can explain the minimums without notes; decompose
  a feature into daily, flag-guarded increments; read a `.gitlab-ci.yml` and point to where
  releasability is decided; explain when to fail forward vs roll back and how to do each on
  AWS; and produce a phased migration plan. The course refrain to end on: this was never a
  verdict on the stack or the people who built it — the .NET estate is itself a recent
  modernization, and CD is the next increment of that same journey, applied across the whole
  estate. Re-run the assessment in a month and measure the move.
