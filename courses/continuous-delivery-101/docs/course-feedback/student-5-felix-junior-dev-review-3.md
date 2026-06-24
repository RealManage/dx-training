# Continuous Delivery 101 — Review 3

**Student:** Felix (Junior Developer, 2 yrs)
**Stance going in:** Same lens as always — I'm the clarity canary. A term used
before it's explained loses me, and I say so. Round 2 I rated 9/10; the only place
the course out-ran me was the strangler-fig doc (idempotent, system of record,
watermark, seam, reconciliation — all undefined). My round-2 sticky note was
literally "idempotent = safe to run twice; watermark = how far a job got; seam =
where you intercept."
**Review date:** 2026-06-23
**Round:** 3 (triggered by the new database-delivery material)
**Overall rating:** 9.5/10 — up 0.5 from my last

## Executive summary

This round added a *lot* of new vocabulary — a whole database track — which is
exactly the thing that usually leaves me behind. It mostly didn't. Two things
happened that matter for a junior:

1. **My round-2 holes got closed as a side effect.** Every term that stranded me
   in the strangler-fig doc last round — **idempotent**, **system of record**,
   **watermark**, **seam**, **reconciliation** — now has a real glossary entry
   (`resources/glossary.md` L147, L168, L171, L162, L153). I didn't even ask for
   all five; the DB work pulled them in. That's the single biggest clarity win
   since round 1.
2. **The new DB jargon is defined where a junior first hits it, almost without
   exception.** DDL, schema migration, schema-history table/journal,
   forward-only, baseline data, environment drift, baseline script, DbUp,
   local-database playground, data trap — all of them are in the glossary in
   plain language, *and* the doc that introduces them (`resources/database-delivery.md`)
   defines or self-explains them inline as it goes. I followed it top to bottom.

Where it's not perfect: the **DbUp worked example assumes more C#/MSBuild fluency
than the rest of the course assumes of anything** — I could follow *what* it does
and *why*, but a couple of mechanics (embedded resources, `EnsureDatabase` vs the
journal, the `>` YAML folding) I had to take on faith. And one term — **"branch by
abstraction"** — is introduced well in Session 2 prose but the prose raises a
question it doesn't answer (how is the seam *switched*?). Both are narrow.

Net: the new material is the most jargon-dense addition since the course began, and
it's also the *best-glossed*. The DB authors clearly read the round-2 canary report.

## Round-2 carryover — status this round

| # | Round-2 item | Status | Evidence |
| - | ------------ | ------ | -------- |
| 1 | **idempotent** undefined (strangler-fig L57/74/83) | **FIXED** | Full glossary entry `resources/glossary.md` L147–148: "the same effect whether it runs once or many times… keying writes on a stable identifier… makes retries and backfills safe." This is *exactly* the "why" I said was missing. |
| 2 | **system of record / authoritative** undefined | **FIXED** | `glossary.md` L168–169 — "the store that is *authoritative*… the one you trust when copies disagree," with the dual-write framing I had to infer last round. |
| 3 | **watermark** undefined | **FIXED** | `glossary.md` L171–172 — "a marker of how far a resumable job has progressed… so it can stop and restart without redoing or skipping work." My exact sticky-note guess, confirmed. |
| 4 | **seam** only almost-defined | **FIXED** | `glossary.md` L162–163 — "a controlled insertion point where you can intercept calls… and redirect them." Now a real entry, not an inline almost. |
| 5 | **reconciliation** context-only | **FIXED** | `glossary.md` L153–154 — "compares two stores… and flags where they disagree — plus a defined response." |
| 6 | **"trunk" not glossed at first use in S1** | **STILL OPEN (softened)** | S1 L39 now reads "an always-deployable **trunk** (the shared `main` line everyone integrates into)" — so my exact round-1/2 ask is **actually fixed at L39**. But the *first* bare use is now L37-area framing and the word still appears un-pinned in the bullet at L25-ish context. Honestly: close enough. I'd call this resolved. |
| 7 | **IC/junior note on current-state assessment** | **STILL OPEN (but newly mitigated for DB)** | The DB gut-check at `current-state-assessment.md` L50 *does* add the IC-protective language I wanted — "that is a deliberate target DX is driving toward… note it, don't score it as a personal gap." That's the kind of "you're not expected to own this alone" note I asked for in round 2 — just scoped to the DB rows, not the DORA-number rows. |

**Scorecard:** 5 FIXED, 1 effectively resolved, 1 partially mitigated. Zero
regressions. My entire round-2 "where it lost me" list is gone.

## The new database material — the round-3 test

This is the bulk of what changed, so it's where I spent my time.

### `resources/database-delivery.md` — **followable, and well-taught**

I read this cold as a junior who has never run a migration tool. I was not lost.
Why it works:

- **DDL** is the one acronym I'd never seen. It's used at L12 ("No human holds DDL
  rights") *before* it's expanded — but the glossary entry (`glossary.md` L138)
  spells it out fully ("**D**ata **D**efinition **L**anguage… `CREATE`, `ALTER`,
  `DROP`… as opposed to DML"), and the README's standing "new to these terms? see
  the glossary" note (`README.md` L28) covers it. So it's a one-tab-flip, not a
  dead end. **Minor flag:** within `database-delivery.md` itself, DDL is never
  expanded in prose — a parenthetical "(Data Definition Language — `ALTER`,
  `CREATE`…)" on first use at L12 would save the flip.
- **schema migration / migration** — defined in place at L19 ("A *migration* is a
  small, ordered SQL script checked into the repo… A migration runner applies the
  scripts an environment has not seen yet"). Clear. I understood it immediately.
- **schema-history table / journal** — here's my one genuine in-doc stumble. L19
  introduces it as "records each one in a **schema-history table**." Then L23
  opens "**Idempotent.** The journal means re-running is safe…" — the word
  **"journal" is never equated to "schema-history table" in the prose.** I had to
  infer they're the same thing. The glossary title "Schema-history table
  (journal)" (`glossary.md` L159) makes it explicit, but a first-time reader of
  *this doc* meets "journal" at L23 as if it were already defined. **One word
  fixes it:** L19 "…in a **schema-history table** (the *journal*)…". This is the
  clearest used-before-defined I found this round.
- **forward-only** — taught in place at L24 ("DbUp has no down-migrations. You do
  not 'undo' a schema change; you ship a new forward script"). Plus a glossary
  entry. Got it.
- **environment drift** — the whole "Hazard: drift" section (L34–42) defines it by
  showing it ("If `prod`, `qa`, and `dev` have diverged, a migration that works in
  one can fail in another"). Self-explaining. Good.
- **baseline / reference data** and **baseline script** — both defined inline (L52,
  L40) and glossed. The MERGE-is-idempotent point at L54 connects straight back to
  the idempotent definition. Nicely woven.
- **data trap** — L60 defines it ("rolling code back does not roll data back") and
  points to the glossary. Clear, and it finally gave a *name* to the scary thing
  the rollback section kept hinting at in earlier rounds.

So: ten new-ish terms, nine defined-where-I-hit-them, one ("journal") a one-word
forward reference. For a junior that's a strong pass.

### The DbUp worked example — **can a junior follow it end to end?**

Mostly yes, with two honest soft spots. I traced `Program.cs` → the three Scripts
→ the CI YAML in order.

**What worked for me:**

- `Program.cs` is heavily commented and the comments teach the CD point, not just
  the C# (L31–34: "DbUp records each applied script in the SchemaVersions journal
  and skips it next run — so re-running the last good deployment is safe"). Even
  without C#, I followed: connection string in → apply new scripts → non-zero exit
  on failure stops the line. The `return 1`/`return 2` exit-code teaching (L21,
  L44) is the clearest "why a migration is a red build" I've seen.
- The three scripts are genuinely readable as a *story*: `Script0001` creates the
  table (idempotent `IF OBJECT_ID … IS NULL`), `Script0002` is the **expand** step
  (nullable column, with a comment block explaining that contract comes *later* as
  its own forward script), `Script0003` is the idempotent `MERGE` seed. The
  filenames (`Script0002__expand_add_due_date.sql`) and the inline `-- EXPAND
  (step 1)` / `-- CONTRACT (step 3) happens LATER` comments mean I never lost the
  thread. This is the first time expand/contract *clicked* for me as concrete SQL
  rather than an abstraction. Real praise.
- The README's ASCII "build ONCE → promote same bytes" diagram (L22–33) plus the
  "run it twice, second run applies nothing" line (L63) made idempotency tangible.

**Where I got lost / had to take it on faith (as a 2-yr dev, likely no C#):**

1. **Embedded resources.** `Migrator.csproj` L16–20 and `Program.cs` L33
   (`WithScriptsEmbeddedInAssembly`) lean on "the scripts are compiled *into* the
   DLL." The README says *why* this matters (self-contained artifact, same bytes
   everywhere — L14) which is great, but *how* a `.sql` file becomes part of a
   `.dll` is assumed. I believed it; I couldn't explain it. For the CD teaching
   point that's fine — but it's the one place the example assumes more .NET than
   the course's "deep AWS expertise not required" promise (`README.md` L39) would
   suggest for .NET.
2. **`EnsureDatabase.For.SqlDatabase` vs the journal.** `Program.cs` L26 creates
   the database if missing; the journal (L33) tracks *scripts*. A junior could
   conflate "ensure the DB exists" with "track which migrations ran." The comment
   at L25 ("Safe no-op if the database already exists") helps, but the two
   distinct safety mechanisms (DB-exists check vs script-history) aren't
   contrasted. Minor.
3. **The `>` block scalar in the YAML.** `db-migrations.gitlab-ci.yml` L49–51 and
   L71–75 use YAML folded-block (`>`) to wrap a long command. I've not seen that
   before; I first misread it as part of a shell heredoc. Not the course's job to
   teach YAML, but it's a real "what is that arrow?" moment for a junior reading a
   pipeline file closely. The Session-3 examples elsewhere don't use it, so it's
   new here.

**Verdict on the example:** I can follow it end to end for *understanding*. I could
not *write* one from it without learning C# embedded resources first — but the
README explicitly frames it as "a teaching reference, not a buildable project"
(L3), and the CLAUDE.md context confirms the C#/SQL Server choice is a deliberate,
justified exception. So the bar is "can a junior follow the CD argument," and yes.

### Session 2 "branch by abstraction" note — **understandable, but raises one question it doesn't answer**

`sessions/session-2/README.md` L92. As a standalone paragraph, this is *good*: it
contrasts a flag (gates a *behavior* at a call site) with branch by abstraction
(gates an *implementation* behind an interface), gives the four-step shape (seam →
build behind it → move callers → delete old path), and links the glossary. I
understood the *what* and the *when*.

**What it left me asking:** *how do callers actually get switched across?* With a
feature flag I now know the mechanism cold — it's a runtime `if`. With branch by
abstraction the note says "move callers across" but never says whether that's
another flag, a config change, a code edit per caller, or something the seam
itself routes. The glossary entry (`glossary.md` L103–104) repeats the same
four steps without resolving it either. For a junior, "introduce a seam, then move
callers across" is the load-bearing step and it's the one left as hand-wave. It's
not *wrong* or confusing — it just stops one sentence short of where my "okay but
how do I *do* it" question lands. One added clause — e.g., "switch callers across
one at a time (often itself behind a small flag), then delete the old path" —
would close it.

Also worth noting: the note says "(a *seam*)" at L92 as an inline gloss, and seam
*now* has its own glossary entry (L162) — so the term that was an "almost
definition" in round 2 is properly anchored now. Good.

### DB callouts in Sessions 1/2/3 — do they land for a beginner in reading order?

- **S1 L92** ("The elephant: the database") introduces the database-as-constraint
  idea and *names* expand/contract and "schema and baseline data delivered as
  code" before either is defined, but it explicitly says "We return to this in
  [Database Delivery]… name it now as the constraint it usually is." So it's a
  deliberate forward-reference with a signpost — I didn't feel stranded, because
  it told me the payoff comes later. Acceptable.
- **S2 L121** ("Migrations are code, too") uses "migration" and "local database"
  before `database-delivery.md` formally defines them, but self-explains both in
  the sentence ("a database migration is reviewed, tested, and merged like any
  change… develop and test it against a **local database**"). Followable.
- **S3 §5.3 L124–128** ("The database is no exception") is a clean recap that uses
  forward-only / immutable artifact / expand/contract — all of which I now know by
  the time I reach S3. Reading in order, this lands.

No hard used-before-defined wall in the session prose. The DB story is sequenced
so the *concept* is named early (S1) and the *mechanics* arrive later (the
resource + example), which is the right order for a beginner.

## Where it lost me / what's still open

1. **"journal" used before it's tied to "schema-history table"** in
   `resources/database-delivery.md` L23 (introduced as "schema-history table" at
   L19, never equated in prose). One-word fix. *This is my only true
   used-before-defined of the round.*
2. **Branch-by-abstraction note (`session-2` L92) stops one sentence short** of
   explaining *how* callers are switched across the seam — the load-bearing step.
3. **DbUp example assumes .NET embedded-resources fluency** (`Migrator.csproj`
   L16–20, `Program.cs` L33) that the rest of the course doesn't assume. Framed as
   a read-only reference, so survivable, but it's the densest single artifact for a
   junior.
4. **DDL not expanded in `database-delivery.md` prose** (L12) — only in the
   glossary. One parenthetical would save the tab-flip.

## New jargon the revisions introduced — my canary report

| Term | First hit (reading order) | Defined where used? | In glossary? |
| ---- | ------------------------- | ------------------- | ------------ |
| schema migration | `database-delivery.md` L19 | **Yes (inline)** | Yes (L156) |
| migration runner | `database-delivery.md` L19 | **Yes (inline)** | via DbUp/schema-migration |
| schema-history table | `database-delivery.md` L19 | **Yes (inline)** | Yes (L159) |
| journal | `database-delivery.md` L23 | **No — not equated to "schema-history table"** | Yes (as alias, L159) |
| DDL | `database-delivery.md` L12 | No (glossary only) | Yes (L138) |
| forward-only | `database-delivery.md` L24 | **Yes (inline)** | Yes (L144) |
| environment drift | `database-delivery.md` L34 | **Yes (by example)** | Yes (L141) |
| baseline data | `database-delivery.md` L52 | **Yes (inline)** | Yes (L126) |
| baseline script | `database-delivery.md` L40 | **Yes (inline)** | Yes (L129) |
| DbUp | `database-delivery.md` L19 | **Yes (inline)** | Yes (L135) |
| local-database playground | `database-delivery.md` L30 | **Yes (inline)** | Yes (L150) |
| data trap | `database-delivery.md` L60 | **Yes (inline)** | Yes (L132) |
| branch by abstraction | `session-2` L92 | **Yes, but mechanism unstated** | Yes (L103) |
| idempotent | `database-delivery.md` L23 / glossary | **Yes** | **Yes (newly, L147)** |
| MERGE / `IF NOT EXISTS` | `Script0003` / `database-delivery.md` L54 | Yes (shown as idempotent SQL) | n/a (SQL) |
| embedded resource | `Migrator.csproj` L16; `Program.cs` L33 | Why yes, how no | No |

Compared to round 2 — where the strangler-fig doc dropped six terms with only one
defined in place — this is a near-total reversal. Of ~16 newish terms, 13 are
defined where I hit them, all but one are in the glossary, and the lone in-doc
forward reference ("journal") is a one-word fix. The authors did the canary's job
for me.

## Factual / technical concerns

Same caveat as always — I can't authoritatively fact-check SQL Server / DbUp /
GitLab mechanics. Two beginner-seat observations, both nice-to-have:

1. The DbUp example README *does* carry the "teaching reference, not a buildable
   project" banner I asked for on other examples in prior rounds (`db-migrations/README.md`
   L3) — good, that's the banner I wanted, now present here.
2. The CI YAML hardcodes `MSSQL_SA_PASSWORD: "Your_local_dev_password_1"` in two
   places (`db-migrations.gitlab-ci.yml` L46, and the README docker-compose L52)
   with a "# local only / ephemeral; not a real secret" comment. As a junior I'd
   want that comment to be impossible to miss, because copy-paste is exactly what
   juniors do — but the comments *are* there, so this is a non-issue, just noting
   I looked.

## Recommendations

### High priority

1. **Tie "journal" to "schema-history table" at first use** —
   `resources/database-delivery.md` L19: "…records each one in a
   **schema-history table** (the *journal*)…". My one real used-before-defined.

### Medium priority

2. **Add one clause to the branch-by-abstraction note** (`session-2` L92 and/or
   `glossary.md` L104) explaining *how* callers move across the seam — the step a
   junior actually has to perform.
3. **Expand DDL inline on first use** in `database-delivery.md` L12 — a
   parenthetical, so the glossary flip isn't required mid-paragraph.

### Nice to have

4. **One sentence in the DbUp README on embedded resources** — what "compiled into
   the assembly" means for someone who's never seen it (it's the crux of "same
   bytes everywhere," so worth a plain-language line).
5. **A note that `EnsureDatabase` (DB exists) and the SchemaVersions journal
   (which scripts ran) are two different safety mechanisms** — `Program.cs` L26 vs
   L33 — so a junior doesn't conflate them.

## Verdict

**Champion — and the new database track is the best-taught jargon-dense material in
the course.** The revision that added the most new terminology since round 1 also
closed every term that stranded me last round, and defined its own new vocabulary
where a junior first hits it. The DbUp worked example lets me follow the CD
argument end to end even without C#, and expand/contract finally clicked as real
SQL. My remaining flags are one one-word forward reference ("journal"), one
note that stops a sentence short ("branch by abstraction"), and an example that
assumes a little .NET — all narrow, all in clearly-labeled reference material.
**9.5/10, up 0.5** — and this time the sticky note I'd hand the next junior is just:
"a migration = a tiny ordered SQL script the pipeline runs; the journal = the table
that remembers which ones already ran."
