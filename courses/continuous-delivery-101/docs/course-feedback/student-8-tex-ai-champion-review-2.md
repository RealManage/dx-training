# Continuous Delivery 101 — Review 2

**Student:** Tex (AI Champion, RealManage)
**Stance going in:** AI now writes the bulk of our code. The engineer's job has moved from turning bolts to designing the factory that turns them. In round 1 I championed this course conditionally: CD is the right discipline for an AI-first org, but the course was silent on the author. Round 2 promised one resource (`ai-assisted-delivery.md`, R2-H4) naming the three seams. This is the spot-check: did it deliver, or is it bolt-on hype?
**Review date:** 2026-06-22
**Overall rating:** 9.5/10 — would I adopt/champion this? (was 8.5)

## Verdict

The AI-authorship dimension is now woven into the spine, not stapled to the end: one substantive, honest resource plus six inline touches that each pull a *specific* course practice into the AI case. The condition I set in round 1 is met, and met well — I'm now an unconditional champion. +1.0, earned.

---

## Resolved since round 1

My round-1 verdict had one explicit condition and three "where it lost me" objections plus a list of seven silences. Against each:

### The headline condition — "add `ai-assisted-delivery.md` naming the three seams" — **IMPROVED (delivered)**

The resource exists and it answers the question I actually asked — *how does AI-authorship change CD?* — rather than dodging into hype. The framing thesis is the right one and it's mine, sharpened:

> "AI removes the human friction that used to keep delivery safe by accident. A tired human is a natural rate limiter. Remove that limiter and CD's practices stop being good habits you adopt to go faster and become the load-bearing controls that keep fast machine output from reaching production unverified." (`resources/ai-assisted-delivery.md:5`)

That is the whole argument in three sentences, and it's honest: it does *not* claim CD needs a rewrite. The closing accounting is the tell that this is engineering, not marketing:

> "CD did not need a rewrite for the age of AI-written code. It needed three of its practices promoted from 'good discipline' to 'the controls that keep machine output safe' — and one new check, **test independence**, that it never had to think about when a tired human was the only author." (`:71`)

And critically — **the three seams are concrete and actionable, not just named.** This was my round-1 fear (recommendation H1 warned "does it give a CONCRETE practice for each seam, or just name the risk?"). Each seam ships with a practice:

- **Test-gaming** (`:19-27`): not just "AI games tests" — it gives the *operational test of a real test* ("A test you could keep through a full rewrite of the implementation is a real test; a test that breaks on any refactor is just mirroring the code," `:25`), points at `handler.test.ts` as the model, prescribes specify-behaviour-first + independent review by a human-or-separate-agent, and explicitly forbids reading "coverage met" as "behaviour verified" (`:27`). That is actionable.
- **Review when you can't read every line** (`:29-33`): states the CD-native answer I demanded in round 1 — "review shifts from line-reading to **spec-checking and gate-trusting**" — and ties accountability to the provenance chain ("the reviewer who approved it plus the pipeline evidence that gated it," `:33`). It even closes the loop to seam 3 ("Keep MRs small … so review can stay substantive").
- **Flag explosion** (`:35-40`): points at the *existing mechanical cure* (birth certificate + CI stale-flag check) rather than inventing a new one, and adds the two AI-specific moves — tune the expiry tighter, and treat an explosion as a *decomposition* (human-design) failure. Exactly right: machines respect mechanical gates, not sermons.

### Objection 1 — "the author changed and the course didn't notice" — **IMPROVED (closed)**

The silence is gone, and gone in the right places — on the *spine*, not quarantined in one file. Six inline touches, each anchored to the practice it stresses:

- Session 1 §2.2, batch-size-as-containment, directly under the master-variable table: "the friction that used to cap batch size is gone … a *throttle you impose*" (`sessions/session-1/README.md:88`).
- Session 2 §4.2, gate-honesty, directly under the quality-gates list: "A gate is only as honest as the tests behind it … the tests can assert whatever the code already does, clear the coverage floor, and verify nothing" (`sessions/session-2/README.md:131`).
- `decompose-a-branch.md:9` — the durable-human-skill note, placed in the *opening* frame of the exercise, not a footnote: "deciding *what* the slices are … is the part that stays yours."
- `strangler-fig-violations.md:67-73` — the human/agent division-of-labour sidebar, dropped immediately after the slice table: "The judgement is human … The rote build is what an agent is good at — the dual-write shim, the idempotent backfill job … Naming the slices above is the work; writing any one of them is not."
- `current-state-assessment.md:32` (test-independence callout under CI minimums 3-4) and `:82` (median-MR-size reframed as *review burden*, not author effort).
- `what-cd-costs.md:111-114` — the honest-accounting line that AI *removes* some of CD's human costs (interruption tax, slow review) even as it raises the stakes on gate quality.

This is the difference between "bolted on" and "woven in." Each touch lands where a reader meets the underlying practice and links forward to the resource for the full treatment. The resource is reachable from all six plus the README — verified, no orphan.

### Objection 2 — "'automate the verdict' not followed to its AI conclusion" — **IMPROVED (closed)**

The resource's second section is titled exactly this and makes the argument I wanted: "When you can't read every line because there are too many and they all look plausible, the automated definition of deployable is the *only* verdict that scales" (`:13`). The adversarial-robustness question I said was "unasked" is now the entire first seam (can the author game the gate?).

### Objection 3 — "no answer to who reviews the AI's code" — **IMPROVED (closed)**

Seam 2 states the position outright (spec-checking + gate-trusting; reviewer ≠ committer is a tool setting; provenance is the accountability record). This was my single most-wanted answer and it's now on the page, defensible and CD-native.

### The seven silences from round 1 — scorecard

| # | Round-1 silence | Status | Evidence |
|---|-----------------|--------|----------|
| 1 | Test independence under same-author code+tests | **Closed** | Seam 1 + Session 2 §4.2 + assessment `:32` |
| 2 | Review under AI authorship | **Closed** | Seam 2 |
| 3 | Flag explosion as AI's default | **Closed** | Seam 3 + `what-cd-costs.md` mechanism |
| 4 | Batch size as containment, not virtue | **Closed** | Session 1 §2.2 blockquote + resource `:44-46` |
| 5 | DORA metric skew | **Closed** | "Metrics that still mean something" `:50-52` |
| 6 | Dependency provenance / hallucinated-package risk | **Closed** | "Dependency provenance" `:56-58` ("slopsquatting" named without the jargon) |
| 7 | Cross-link to AI 101 | **Deliberately not done** — see note | Per round-2 decision, AI content stays inside CD 101 |

On #7: I recommended bidirectional AI-101 links in round 1. Round 2 made the opposite call deliberately — keep all AI content self-contained in CD 101, no new cross-course links. **I accept it, and on reflection it's the better call for this course.** A self-contained CD-101 reader is never sent out of the building to understand a CD concept; the resource carries its own weight (it states the tests-as-spec discipline inline rather than punting to a sibling course). The one pre-existing AI-101 footer link in `README.md:211` is untouched and harmless. The self-contained treatment works — the resource does not read as if a chapter is missing.

### Durable framing — **PASSES the 12-month test**

I asked specifically: no dated "nearly all the code" percentage claims that will embarrass the course in a year. Verified — grepped the resource and all six inline touches: zero volume/percentage claims. The framing is conditional and timeless ("As more code is written by agents," "when the same agent writes," "once an agent is writing your `package.json`"). The factory metaphor is durable. **This will not date.** Good discipline — this was a real risk and they avoided it cleanly.

---

## Still open or newly noticed

I scrutinised before crediting. The treatment is genuinely strong; what's left is minor and mostly polish. I am not inventing gaps.

1. **Discoverability is thinner than the content deserves (minor).** The resource is linked from six spine locations and the README *file-tree comment* (`README.md:60`) — but there is no prose "when AI writes your code, read this" pointer in the README body or any reading-guide. A reader scanning the README narrative won't see it; they only meet it if they hit one of the inline touches first. Given this is now the course's answer to the org's single biggest strategic shift, it has earned one prose sentence in the README's resources framing, not just a tree comment. Cheap fix; not blocking.

2. **The seam I'd still add: the rollback/recovery path under AI (partial).** Fail-forward gets the AI treatment in "what doesn't change" (`:69`) — correctly: the forward fix is cheaper, same gates apply, don't let the agent hot-fix around the pipeline. Good. But there's a sharper AI-specific recovery risk left unsaid: when an agent produces the forward fix in minutes *under incident pressure*, the temptation is to merge it with the review substance hollowed out exactly when you can least afford it — the seam-2 erosion and the fail-forward speed compound at the worst moment. The resource treats those two ideas separately; one sentence connecting them ("the forward fix is the most dangerous place to let review become a rubber stamp") would close the last real gap. This is the only seam I'd genuinely add.

3. **"Separate agent reviews the tests" is asserted, not operationalised (minor).** Seam 1 and seam 2 both lean on "a human — or a *separate* agent that did not write the code" as the independent check (`:26`). That's the right shape, but it's the one place the resource states a practice without a concrete how (contrast the flag mechanism, which is fully mechanical). For an AI-first shop this *is* the daily question — how do you wire a second agent as an independent test reviewer without it sharing the first's blind spots? A two-line "what this looks like in the MR" would lift it from principle to practice. I'd accept it as nice-to-have, not a condition.

None of these move the score down. They're the difference between 9.5 and best-in-class.

---

## Bottom line for an AI champion

**I am now an unconditional champion.** Round 1's one condition is met substantively: the course names the author, the three seams are concrete and each carries a practice, the framing is honest (CD is mostly AI-ready; this is three promotions and one new check, not a rewrite) and durable (no dating percentage claims), and the AI case lives on the spine — at the batch-size table, at the quality-gate list, at the decomposition exercise, at the strangler-fig slices — not in an appendix. The factory framing I brought in round 1 is now the resource's own organising metaphor, used correctly: humans design the line and name the slices; the pipeline is quality control; the agent turns the bolts.

What would make it **best-in-class** (not required, but the ceiling):

1. A prose pointer to the resource in the README body, so a strategic reader finds it without spelunking the file tree (open item 1).
2. The compounding fail-forward × review-erosion sentence (open item 2) — the last unaddressed seam.
3. Operationalise the "separate agent reviews the tests" check with a concrete MR-level pattern (open item 3) — turn the one remaining principle into a mechanism, the same upgrade the flag-debt section already models.

Do those three and this is the reference text for "CD in an AI-first org." As it stands today, it's the course I'd hand every engineer at RealManage on day one — and mean it.
</content>
</invoke>
