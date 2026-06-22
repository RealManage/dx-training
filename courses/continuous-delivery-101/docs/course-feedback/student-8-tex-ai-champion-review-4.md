# Continuous Delivery 101 — Review 4 (round-5 spot-check)

**Student:** Tex (AI Champion, RealManage)
**Stance going in:** AI now writes the bulk of our code. When the agent is the author, coding gets cheap and the bottleneck *moves* — off "develop" and onto review and the deploy window. Batch size becomes a throttle you *impose*; tests written by the same author that wrote the code verify nothing until something independent checks them; the durable human skill is deciding the seams. My touchstone is `resources/ai-assisted-delivery.md` (the three seams). My durability test: every AI claim must survive 12 months — no dated/volume figure that ages. My headline R4 ask was **R4-2** — make the value stream map *show* the constraint shift. My longest-standing item is **R3-6** ("a separate agent reviews the tests," asserted not operationalised).
**Review date:** 2026-06-22
**Overall rating:** 9.5/10 — would I adopt/champion this?

## Verdict

R4-2 landed, and it landed *well* — the value stream map is now the course's most vivid demonstration of my central thesis, written into the reader's own numbers rather than asserted abstractly. The constraint-shift logic is sound, the placement is right (Part 4, the "find the constraint" step, plus a one-line §5.2 pointer), the cross-link resolves and points at exactly the seam it should, and the whole thing passes the 12-month durability test cleanly. No regression in the AI spine. R3-6 is now formally deferred to the SDLC skill, and I judge that deferral defensible — I do not re-dock for it. The single highest-leverage AI move I have asked for across four reviews is done. **Held at 9.5** — the work that would have *lifted* it (R3-6 as an in-course mechanism) was, by a reasonable scope decision, moved out of this course's lane. No advance, but no reason to dock. A genuine hold.

## Score trajectory

| R1 | R2 | R3 | **R4** |
| -- | -- | -- | ------ |
| 8.5 | 9.5 | 9.5 | **9.5** |

**Delta vs R4 (0.0):** R4-2 — my strongest standing finding — is resolved well, which would normally lift the score. It doesn't, for one honest reason: the *other* item that separated 9.5 from the reference text (R3-6, operationalising the independent test review) was deferred out of scope rather than built in. That's a legitimate call (it lives in the Claude Code SDLC skill, which is process tooling, not course content), so I don't dock — but I also can't credit an advance for work that was moved off the board. The course is exactly as strong as it was, with its best AI illustration now switched on. 9.5, earned.

---

## 1. R4-2 — the VSM × AI constraint-shift callout — **RESOLVED, lands well**

This was my round-3 finding and the round-4 plan's "highest-leverage single addition." It's now in two places, both correct.

### The Part 4 callout (`exercises/value-stream-map.md:83`)

> "When an agent writes the code, watch this map move. *Develop* is the step AI collapses first — an agent drafts in minutes what took a day, so its process time falls toward zero. But that doesn't shorten lead time if the change still waits two days for review and four for the release window. It just shifts the constraint *downstream*, onto the review queue and the deploy window."

**The constraint-shift logic holds — I checked it against the worked example, not just the prose.** The argument is: AI collapses *develop process time*; the *waits* are unaffected; so lead time barely moves and the binding constraint relocates downstream. Run it against the example's own numbers:

- Develop process time is 12 h. The waits downstream of (and around) develop are 16 h (develop wait) + 16 h (review) + 32 h (release window) = 64 h. Collapsing the 12 h of develop *process* shaves ~9% off a 127.5 h lead time and moves nothing structural — the two starred steps (the 32 h window wait, the 70% deploy %C/A) are *already* downstream of develop. AI doesn't *create* the shift here; it *removes the one place a team might have wrongly blamed* (slow hand-coding), leaving the real constraint nakedly visible. That is precisely the lesson.
- The callout's figures — "two days for review and four for the release window" — are not invented; they are the example's own 16 h and 32 h waits expressed in days. The demonstration is grounded in the reader's table, which is exactly what makes the VSM the right instrument and not a slogan.

**Scope discipline I want to credit explicitly:** the callout confines its claim to *process time and waits*. It does **not** claim AI improves the develop step's %C/A (rework rate) — and it shouldn't, because an agent that writes plausible-but-wrong code can hold or even *worsen* rework while collapsing process time. A sloppier author would have written "AI makes develop faster and better"; this one says only what's defensible. Good.

**Placement:** correct. Part 4 is the "find the constraint" step — the exact moment a reader is staring at their two stars and asking "what moves lead time most?" The callout's "re-map once AI is in your loop: the biggest wait will have moved" is the right instruction at the right beat.

### The §5.2 pointer (`sessions/session-1/README.md:179`)

> "For the AI-assisted team: when an agent writes the code, *develop* shrinks but the waits don't — the constraint moves to the review queue and the deploy window. The map is how you catch that shift."

The compressed one-line form of the Part 4 callout, placed in the live workshop step where teams map their *own* stream. Consistent with the long form, no contradiction. It sits directly under "let your own numbers say so," which is the correct framing — the map catches the shift; the team confirms it from their figures.

### Cross-link coherence — **the best part of the fix**

Both callouts link to `resources/ai-assisted-delivery.md`. Both link paths resolve (`../resources/` from `exercises/`, `../../resources/` from `sessions/session-1/`). But the coherence is deeper than "the link works": the VSM callout says *the constraint moves onto the review queue* — and the resource's **seam 2 is titled "Review when you can't read every line."** A reader who follows the link from "the constraint moved to review" lands on the page that explains *what to do once it's there* (review shifts from line-reading to spec-checking + gate-trusting). The VSM shows *where*; the resource explains *what then*. That's a clean division of labour between the two surfaces, not a redundant restatement. The cross-link target still holds up — I re-read the full resource; seams 1–3, the batch-as-containment section, the DORA-skew section, the dependency-provenance addition, and the fail-forward compounding sentence are all intact and unchanged.

### Durability — **PASSES**

Grepped both callouts and the whole resource for dated/volume claims ("nearly all," "most of our code," "by 202X," "in a year," percentage-of-code figures). Zero in the new callouts. The framing is conditional and timeless throughout: "when an agent writes the code," "once AI is in your loop," "as more code is written by agents." The only numbers in the callouts are the example's own arithmetic (two days, four days) — hypothetical-team figures, not industry claims, so no rot risk. The lone "%" hit anywhere in the AI surface is `"reach 80%"` in the resource (`:21`), which is an *illustrative coverage threshold* for the Goodhart point, not an AI-adoption claim — present since R2, already passed, still fine.

**R4-2 verdict: resolved well. The VSM is now the course's most vivid "the bottleneck shifted" demonstration, the logic is arithmetically sound against its own example, and it's durable.**

---

## 2. Durability + consistency sweep across the AI spine — **CLEAN**

I re-checked the four spine callouts and the resource for aging or any conflict introduced by the new VSM callout. They form a consistent thesis, each anchored to the practice it stresses:

- **Session 1 §2.2** (`:88`) — batch-size-as-throttle: "the friction that used to cap batch size is gone … a *throttle you impose*." Unchanged, durable, no dated claim.
- **Session 1 §5.2** (`:179`) — the new constraint-shift pointer. Consistent with §2.2: §2.2 says *why* you throttle (machine output drowns the trunk); §5.2 says *where the constraint goes* if you don't fix the waits. Complementary, not contradictory.
- **Session 2 §4.2** (`:131`) — gate-honesty: "a gate is only as honest as the tests behind it … the same author writes the code *and* its tests — increasingly an AI agent … verify nothing." Durable. The "increasingly an AI agent" phrasing is conditional/trend language, not a dated figure — survives the 12-month test.
- **`ai-assisted-delivery.md`** — full re-read; the three seams, the batch-as-containment "said once" section, metrics-that-still-mean-something, dependency provenance, and the honest-accounting close are all intact and consistent with the new VSM framing. The new VSM callout's "constraint moves downstream onto review" is the *map-level* projection of seam 2's "review erodes under volume" — same idea, two altitudes, no conflict.

**No claim has aged. No new callout conflicts with an existing one. The AI thesis is coherent across the spine.**

---

## 3. R3-6 — "a separate agent reviews the tests" — deferred to the SDLC skill — **deferral accepted, no re-dock**

The wording still stands in `ai-assisted-delivery.md:26` ("have a human — or a *separate* agent that did not write the code — check the tests against those criteria") and is echoed in `current-state-assessment.md:32`. Round 4 deferred operationalising it on the grounds that the concrete mechanism lives in the Claude Code SDLC skill, outside this course.

**My call: the deferral is defensible. I do not re-dock, and the course should *not* be forced to add an SDLC-skill pointer.** Reasoning:

- This is a **CD practices/platform course**, deliberately self-contained (the round-2 decision to keep all AI content inside CD 101 and introduce no cross-course links). Pointing out to a Claude Code internal skill would be the *first* external-tooling dependency in the whole course — it would break the self-containment property I credited in R2 and R3, and it would tie a durable practices course to a specific tool's current feature set (a durability risk in itself: skill names and shapes change).
- The course already states the *principle* correctly and at the right altitude: tests must specify intent; something *independent* must confirm they do; that independent thing can be a human or a separate agent. For a practices course, naming the principle and the shape of the control is the correct stopping point. *How* you wire a second agent is implementation, and implementation belongs in the tool's own documentation, not in a vendor-neutral CD course.
- Silence on the specific *tool* is therefore not a gap — it's correct scope. The course is not silent on the *practice*; it's silent on the *product*, which is exactly the "practices over products" philosophy in the course's own CLAUDE.md.

So R3-6 leaves my list not as "resolved-in-course" but as "correctly scoped out." That's a legitimate close. It's the reason the score doesn't rise (the in-course mechanism that would have made this the reference text was moved out), but it's not a reason to dock.

---

## 4. New-regression hunt (adversarial) — **none found**

I went looking specifically for ways the new VSM callouts could have damaged the AI narrative:

1. **Over-claim check — does any callout assert AI improves quality/%C/A?** No. Both VSM callouts restrict themselves to process time and waits (see §1). The resource's seam 1 still warns the opposite — AI can *hold or worsen* correctness while looking green. No internal contradiction.
2. **Does "develop collapses toward zero" undercut the test-gaming seam?** No — and this is the subtle trap a careless edit could have sprung. If develop is "free," a naive reader might infer "so just let it rip." The callouts don't say that; they immediately redirect to *the waits and the constraint*, and the resource's seam 1 + Session 2 §4.2 keep the "fast green ≠ verified" warning load-bearing. The two ideas coexist: coding is cheap (VSM), *therefore the verdict gates matter more* (resource). That's the correct relationship and it's preserved.
3. **Self-containment** — grepped the new callouts for cross-course paths (`ai-101`, `bdd-101`). None. The round-2 self-containment decision survived the change.
4. **Link integrity** — all four AI cross-links resolve from their respective directories. No orphan, no broken anchor.

No regression in any AI surface from the round-4 work.

---

## Prioritized open items

None is an adoption blocker — the course is champion-ready and has been since R2. This is the gap between an excellent course and the reference text, and after R4-2 the gap is genuinely small.

### Tier 2 — small addition (more than a line)

| ID | Item | Effort | Where |
| -- | ---- | ------ | ----- |
| **R3-6** | **(Carried, now scope-deferred — *optional*, not docked.)** The independent-test-review mechanism lives in the SDLC skill by decision. If the team ever wants this course to stand fully alone as the AI-CD reference, the move is *not* a tool pointer (that breaks self-containment) but a two-line **tool-neutral** "what independent test review looks like in the MR" pattern in `ai-assisted-delivery.md:26` — criteria-first, reviewer/second-agent doesn't see the implementation diff, different context. That's a practice, not a product, so it stays in-scope. I accept the current deferral; this is the one upgrade that would lift the score. | 2 lines | `ai-assisted-delivery.md:26` |

### Blockers

**None.** R4-2 resolved; the AI spine is consistent and durable; no regression.

---

## Bottom line for an AI champion

Still an unconditional champion. The single thing I have pushed hardest for across four reviews — *make the value stream map show that the constraint moves when coding gets cheap* — is now built, and built honestly: it's grounded in the example's own arithmetic, it's scoped to exactly the claim that's defensible (process and waits, not quality), it links to the seam that explains what to do next, and it won't date. That converts the course's strongest authorship-agnostic exercise into its strongest illustration of my entire thesis.

The only thing standing between 9.5 and "the reference text for CD in an AI-first org" is R3-6 — and the team has made a defensible call to put the *mechanism* in the SDLC skill rather than the course. I respect that boundary; a practices course should teach the practice and leave the product to the product. If they ever want the course to carry that last weight alone, a two-line tool-neutral MR pattern does it without breaking the self-containment that makes this course so clean. As it stands today, it's still the course I'd hand every engineer at RealManage on day one — and the value stream map is now the page I'd point an AI skeptic at to *show* them, in their own numbers, where the bottleneck went.
