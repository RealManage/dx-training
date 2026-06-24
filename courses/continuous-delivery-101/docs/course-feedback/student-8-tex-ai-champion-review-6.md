# Continuous Delivery 101 — Review 6 (round-7, DB/CI-internals trigger)

**Student:** Tex (AI Champion, RealManage)
**Stance going in:** AI now writes the bulk of our code. When the agent is the author, coding gets cheap and the bottleneck *moves* — off "develop" and onto review and the deploy window. The durable human job is designing the line, not turning the bolts. My touchstone is `resources/ai-assisted-delivery.md` (three seams). My durability test: every AI claim must survive 12 months — no dated/volume figure that ages. This round the course grew a whole new *database* surface (resource + worked example + glossary), which is mostly authorship-agnostic CD-internals work. My job is to ask the honest factory-view question — *does AI-authored schema change the DB guardrails?* — without inventing an AI angle where none exists.
**Review date:** 2026-06-23
**Overall rating:** 9.6/10 — hold. Would I adopt/champion this? Unconditionally, still.

## Verdict

The new DB content is reference-quality CD-internals work — and almost entirely authorship-agnostic, which I confirm honestly rather than resent. But the factory question has a real, non-manufactured answer: when an **agent** authors schema migrations, the DB guardrails (pipeline-only, local-DB playground, forward-only, expand/contract, idempotent re-runs, the `SchemaVersions` journal) matter **more**, for the *same* reason the test gate does — the human friction that used to cap a risky `ALTER TABLE` is gone. The app-code content already names this move (`ai-assisted-delivery.md`, four spine callouts). The new DB content does **not** — it is silent on AI authorship end to end, and `ai-assisted-delivery.md:71` mentions expand/contract but never links to the resource that now teaches it. That is the *same* gap I pushed into the app-code content in earlier rounds, now reopened on the DB side: one missing factory-view sentence plus one missing cross-link. It is a genuine seam, not a nitpick — but it is a single, additive, low-effort item, and the content it would attach to is already correct. So I hold at 9.6 rather than dock: nothing regressed, the DB work is right, and the gap is an *absence* in a brand-new surface, not a defect in shipped material. **R6-T1 verified intact — no regression.** **9.6 → 9.6 (0.0).**

## Score trajectory

| R1 | R2 | R3 | R4 | R5 | **R7** |
| -- | -- | -- | -- | -- | ------ |
| 8.5 | 9.5 | 9.5 | 9.5 | 9.6 | **9.6** |

**Delta vs R5 baseline (0.0):** An honest hold. The DB surface is new and well-built; no AI claim aged; R6-T1 held. The one factory-view gap (DB content is authorship-silent; no AI↔DB cross-link) is a missing *addition* to new material, not a flaw in it — the same kind of additive opportunity I named on the app-code side across rounds 3–5 before it was built. I do not lift for new content that is merely correct, and I do not dock for a missing sentence in a surface that did not exist last round. 0.0 is the truthful number.

---

## 1. R6-T1 — independent-test-review isolation rule — **VERIFIED, no regression**

The R6-applied clarification is present and intact at `resources/ai-assisted-delivery.md:30`:

> Reviewing the implementation is a *separate* pass afterward — by the same reviewer or another; the independence comes from not seeing the diff *during* the test review, not from needing a second person.

This is exactly what the round-6 synthesis recorded as applied. The impl-review pass is now attributed (same reviewer or another), and the isolation is correctly located in *not seeing the diff during the test pass* rather than in requiring a second person. The seam-1 mechanism is airtight and unchanged. No round-7 edit touched it; the whole seam-1 "what holds the line" list reads consistent end to end. **R6-T1 closed and holding.**

---

## 2. The factory question — do the DB guardrails matter MORE under AI authorship? — **yes, and the content does not say so**

This is the substantive finding. I worked the question honestly rather than assuming the answer.

**The DB content (`resources/database-delivery.md`, the `db-migrations` worked example, Session 3 §5.3, the new glossary entries) is excellent and authorship-agnostic.** Build-once-promote the runner, vary only the connection string, prove against an ephemeral SQL Server before `prod`, forward-only with expand/contract, idempotent baseline data via `MERGE`, the `SchemaVersions` journal — these are correct whether a human or an agent wrote the script. I am not going to invent an AI flavor for `MarkAsExecuted`. Most of this genuinely does not change.

**But two of the guardrails change in *stakes* under AI authorship, by the same mechanism the rest of the course already names** — "AI removes the human friction that used to keep delivery safe by accident" (`ai-assisted-delivery.md:8`):

- **The local-DB playground (`database-delivery.md:30`, worked example "Local playground").** The resource frames the local database as "where you develop and test a migration *before it merges* — the database analog of the personal sandbox stack." That is precisely the *prove-it-before-merge* gate that an agent will skip by default. A human hand-writing an `ALTER TABLE` feels the friction of standing up a local SQL Server and is tempted to test against shared `dev` instead; an agent emits a plausible migration with no instinct to prove it locally at all. The local-DB-before-merge step is the schema analog of seam 1's "green ≠ verified" — and it is *more* load-bearing, not less, when the author is fast and frictionless. The content states the rule; it never says *why an AI author makes skipping it the path of least resistance.*

- **Forward-only + expand/contract (`database-delivery.md:24`, :60; worked example "Recovery: forward-only").** `ai-assisted-delivery.md:71` already gets the shape of this exactly right: "Expand/contract — a schema-safety discipline the agent executes once a human names the steps." That is the cleanest factory-view sentence in the whole AI page applied to schema: the *agent can run the steps, but slicing a schema change into expand → migrate → contract is decomposition, and decomposition is the human's job* — the same conclusion seam 3 reaches for flags ("a flag explosion is still a *decomposition* failure"). An agent told "add the due-date column" will not, unprompted, split it into a backward-compatible nullable-column expand now and a contract later; it will do the direct thing and walk into the data trap. The new DB content teaches expand/contract beautifully but never connects it to *who names the steps* — which is the one place the human stays in the loop when the machine writes the SQL.

**The other guardrails (pipeline-only #2, immutable runner #5, idempotent re-runs, the journal) are stakes-neutral under AI** — they are mechanical properties of the runner, unaffected by authorship. I am explicit about that so the "matters more" claim stays scoped to the two guardrails where it is *true*: the prove-before-merge step and the human-owned slicing of expand/contract. Manufacturing an AI angle for the journal would be exactly the nitpick I was told not to invent.

**Severity: low, additive.** The DB content is correct; what is missing is one factory-view sentence — the schema analog of the line I pushed into the app-code content in rounds 3–5. The natural home is the close of `database-delivery.md`'s "Reversibility and recovery" or "The destination" section: a sentence to the effect of *"When an agent authors a migration, these guardrails matter more, not less — the friction that used to make a risky hand-edit feel risky is gone; the human's job moves to naming the expand/contract steps and proving the migration locally before it merges. See [ai-assisted-delivery](ai-assisted-delivery.md)."* That single sentence carries both the factory framing and the missing cross-link (see §3).

---

## 3. Missing cross-link between `ai-assisted-delivery.md` and the new DB content — **confirmed absent, both directions**

This is the cleaner half of the finding and verified by grep.

- **`ai-assisted-delivery.md` → DB content: absent.** `ai-assisted-delivery.md:71` says "Expand/contract — a schema-safety discipline the agent executes once a human names the steps" but does **not** link to `resources/database-delivery.md`, which is now the canonical page teaching expand/contract, forward-only, and the data trap. The reader who hits that bullet and wants the worked discipline has no pointer to it. The link should exist; the resource it would point to now exists for the first time this round.
- **`database-delivery.md` → AI content: absent.** Zero occurrences of `AI` / `agent` / `machine` anywhere in `database-delivery.md`, the `db-migrations` example, or its scripts (grepped). The DB surface does not acknowledge AI-authored migrations at all.

The asymmetry is the tell: the app-code surface has a whole AI resource plus four spine callouts (`session-1/README.md` ×2, `session-2/README.md`, the resource); the DB surface — equally a place where an agent now writes the artifact — has nothing. Closing it is one sentence + one link, and it makes the factory thesis whole across *both* kinds of artifact the machine now authors.

**Durability check on the proposed addition:** the suggested sentence is conditional and tool-neutral ("when an agent authors a migration… the human's job moves to naming the steps") — no dated figure, no volume claim, no percentage. It passes the 12-month test by construction, consistent with the rest of the AI spine.

---

## 4. Durability + consistency sweep across the new DB surface — **CLEAN**

I read the new material adversarially for any aged claim or over-claim that AI improves quality.

- **No dated/volume claims** in `database-delivery.md`, the worked example, or the new glossary entries (DbUp, DDL, forward-only, data trap, baseline data/script, environment drift, branch by abstraction, `SchemaVersions`). Grepped — zero `%`, zero "by 202X", zero volume figures.
- **No over-claim that AI improves schema quality.** Because the DB content does not mention AI at all, there is nothing to over-claim — and the *adjacent* AI bullet (`ai-assisted-delivery.md:71`) keeps the honest posture: the agent *executes* the steps a human *names*. Consistent with the whole-page thesis (cheap authorship → the gates matter *more*).
- **"Branch by abstraction" glossary entry (`glossary.md:103`) and the Session 2 note (`session-2/README.md:92`)** are accurate and authorship-agnostic — and quietly *more* relevant under AI: it is the structural-change analog of a flag, and large refactors are exactly what an agent will attempt in one breath. No AI claim is needed there; flagging only that it is internally correct.
- **DDL glossary entry (`glossary.md:138`)** correctly grounds "no standing human DDL access" — the pipeline-only #2 control for schema. Stakes-neutral under AI (mechanical), so no AI sentence belongs here; noted for completeness.

No claim aged. No DB callout conflicts with the AI spine. The DB thesis and the AI thesis are *compatible* — they simply have not been *connected* yet (§3).

---

## 5. New-regression hunt (adversarial) — **none found in any AI surface**

I checked specifically whether the round-7 DB additions disturbed the AI spine.

1. **Did any DB edit touch `ai-assisted-delivery.md`?** No — `ai-assisted-delivery.md:71`'s expand/contract bullet predates this round and is unchanged; the data trap it implies is now backed by a real resource, which only *strengthens* it (and is the argument for the cross-link, not against it).
2. **Did the rollback-correction sweep contradict the AI page's fail-forward stance?** No. The DB recovery story (`database-delivery.md:58`) and Session 3 §5.2 both land on fail-forward-first + expand/contract, never a hand-edit — identical to `ai-assisted-delivery.md:73`'s "fail-forward-first… the forward fix needs the *same gates*." The schema "never hand-edit a shared database" is the exact analog of "never hand-edit the Lambda alias." Fully consistent.
3. **Self-containment of the AI page:** intact. No `ai-101` / `bdd-101` / `sdlc` link introduced. (The cross-link I am asking for is *intra-course*, to `database-delivery.md` — it does not break self-containment.)

No regression in any AI surface from the round-7 work.

---

## Prioritized open items

None is an adoption blocker — the course has been champion-ready since R2 and the AI surface is reference-quality.

### Tier 2 — genuine factory-view gap (additive, low effort, not docked this round)

| ID | Item | Effort | Where |
| -- | ---- | ------ | ----- |
| R7-T1 (low) | **DB content is authorship-silent; no AI↔DB cross-link.** The new DB surface never names AI-authored migrations, and `ai-assisted-delivery.md:71` (expand/contract) does not link to the new `database-delivery.md`. One factory-view sentence — local-DB-prove-before-merge and human-named expand/contract steps matter *more* under an agent author — placed in `database-delivery.md` ("Reversibility and recovery" or "The destination"), carrying a link back to `ai-assisted-delivery.md`; and a forward link from `ai-assisted-delivery.md:71` to `database-delivery.md`. Same additive move I pushed into the app-code content in rounds 3–5, now reopened on the DB side because the DB surface is new. | one sentence + two links | `resources/database-delivery.md` (~:32 / :60); `resources/ai-assisted-delivery.md:71` |

### Tier 3 — explicitly NOT items

- The pipeline-only / immutable-runner / idempotent-journal guardrails are **stakes-neutral** under AI authorship. I deliberately do **not** ask for an AI sentence on those — that would be a manufactured nitpick.

### Blockers

**None.** R6-T1 holds; the DB surface is internally correct and durable; no regression. R7-T1 is an additive opportunity, not a defect.

---

## Bottom line for an AI champion

Still an unconditional champion, and the new DB surface is the right kind of new content — concrete, honest about where RealManage actually is (drift named, local DBs "still uncommon," DX owns the path), and mechanically correct. The factory question has a real answer I will not soften: when the agent writes the `ALTER TABLE`, the *prove-it-locally-before-merge* step and the *human-named expand/contract slicing* matter **more**, because the friction that used to make a risky hand-edit feel risky is exactly what AI removes. The course already knows how to say this — `ai-assisted-delivery.md:71` says it in one line for expand/contract — it simply has not said it *on the DB page* or *linked the two surfaces*. That is the lone gap (R7-T1): one factory-view sentence and two cross-links, additive to material that is already right. Nothing regressed, R6-T1 is intact, and I am not going to dock a brand-new, correct surface for a missing sentence. Hold at **9.6**. Apply R7-T1 and the factory thesis is whole across *both* artifacts the machine now authors — code and schema.
