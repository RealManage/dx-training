# Week 9: Capstone Hackerspace & Future Roadmap - Trainer Runbook

## Session Overview

**Duration:** 2.5 hours
**Format:** In-person or virtual
**Structure:** Project selection, architecture planning, implementation sprint, testing, demos
**Tracks:** Developer (Options A-C), QA (Option D), PM (Option E), Support (Option F), Custom (Option G)

---

## Pre-Session Checklist

### For Instructors

- [ ] Verify all capstone starter templates build without warnings (`dotnet build` in each `option-*` directory)
- [ ] Confirm project selections received from participants (should have been submitted the day before)
- [ ] Print or share the evaluation rubric so participants know scoring criteria upfront
- [ ] Prepare team assignments (2-3 per team) or confirm individual scope selections
- [ ] Test that sandbox copy instructions work: `cp -r examples/capstone-templates sandbox/`
- [ ] Have certificates ready (digital badge template + PDF template)
- [ ] Prepare course feedback survey (link or QR code)
- [ ] Monitor `#ai-exchange` Slack channel for last-minute questions
- [ ] Review each participant's track history (Weeks 1-8) to anticipate skill gaps

### Environment Check

```bash
# Run these the morning of the session
cd courses/ai-101-claude-code/sessions/week-9/examples/capstone-templates

# Verify each template builds
for dir in option-*/; do
  echo "Testing $dir..."
  (cd "$dir" && dotnet build --warnaserror 2>&1 | tail -1)
done

# Verify Claude Code is available
claude --version
```

---

## Timing Guide

| Time | Segment | What Happens |
| ---- | ------- | ------------ |
| 0:00-0:05 | Welcome & Kickoff | Set the stage: "This is YOUR day — everything from Weeks 1-8 comes together" |
| 0:05-0:25 | Part 1: Project Selection & Planning | Confirm selections, form teams, review requirements |
| 0:25-0:40 | Part 2: Architecture & Planning | Plan Mode architecture, instructor circulates to review plans |
| 0:40-1:30 | Part 3: Implementation Sprint | 50 minutes of focused building with time checkpoints |
| 1:30-1:40 | Part 4: Testing & Quality Verification | Run tests, check coverage, security review |
| 1:40-1:50 | Part 5: Demo Preparation | Polish demos, practice presentations |
| 1:50-2:05 | Part 6: Presentations & Celebration | Demo day! 3-4 minutes per team/individual |
| 2:05-2:15 | Evaluation & Wrap-Up | Peer evaluation, rubric scoring, certificates |
| 2:15-2:30 | Course Close | Feedback survey, next steps, celebration |

> **Pacing note:** The implementation sprint (Part 3) is the critical path. Protect this time aggressively — do not let Parts 1-2 run over. If project selections were submitted in advance, Part 1 can be shortened to 10 minutes.

---

## Facilitation Guide by Part

### Part 1: Project Selection & Planning (25 min)

**Goal:** Everyone knows what they're building and who they're building with.

**How to run it:**

1. **Quick roll call** (2 min) — Confirm everyone has their project option selected
2. **Team formation** (5 min) — Group participants by option or let individuals confirm scope
3. **Requirements walkthrough** (10 min) — Have each team read their option's success criteria aloud
4. **Scope check** (8 min) — Circulate and help teams narrow scope to what's achievable in 50 minutes

**Watch for:**

- Participants who didn't pre-select — give them Option A (most structured) or Option E/F (non-coding)
- Teams that want to combine options — redirect to picking one and doing it well
- Option B teams — remind them of the scope note: API backend first, Angular frontend is stretch goal
- Solo participants — ensure they've scoped to one core feature, not the full option

### Part 2: Architecture & Planning (15 min)

**Goal:** Every team has a reviewed architecture plan before writing code.

**How to run it:**

1. Have everyone enter Plan Mode (`Shift+Tab`)
2. Circulate and check each team's plan within the first 10 minutes
3. Look for: data models defined, testing strategy included, implementation order logical

**Red flags to catch early:**

- No testing strategy in the plan → "Where are your tests?"
- Over-engineered architecture → "What's your MVP? Build that first."
- Missing custom skill → "Your rubric requires at least one skill"
- PM/Support tracks skipping Plan Mode → "Plan Mode works for documents too — plan your deliverable structure"

### Part 3: Implementation Sprint (50 min)

**Goal:** Maximum productive build time.

**Time checkpoints to announce:**

| Elapsed | Announce |
| ------- | -------- |
| 10 min | "You should have your first failing test by now" |
| 20 min | "Core models and first service should be taking shape" |
| 30 min | "Halfway! Focus on getting the happy path working" |
| 40 min | "10 minutes left in the sprint — start wrapping up features" |
| 50 min | "Sprint over! Shift to testing and quality checks" |

**How to handle teams finishing at different paces:**

- **Fast finishers:** Challenge them to add edge case tests or improve coverage percentage
- **Behind schedule:** Help them scope down — "What's the one thing that must work for your demo?"
- **Stuck teams:** Pair them with a faster team or do a quick 2-minute unblock session
- **Non-coding tracks (E, F):** Check that they're generating content, not just prompting — deliverables should be taking shape

**Circulate with these questions:**

- "Show me your test output — are you green?"
- "Where's your custom skill? Have you tested it?"
- "What's your demo going to show?"

### Part 4: Testing & Quality Verification (10 min)

**Goal:** Everyone has passing tests and knows their coverage number.

**Quick check:**

1. Ask each team to run `dotnet test` and share results
2. Coverage should be >= 80% on business logic
3. Non-coding tracks: review deliverables for completeness against checklist

**Common issues:**

| Issue | Quick Fix |
| ----- | --------- |
| Tests won't compile | Check namespace imports, missing test project reference |
| Coverage below 80% | Focus tests on the service layer, not controllers |
| No custom skill | Create a minimal skill now — it's 5 minutes and worth rubric points |
| PM/Support missing deliverables | Prioritize the PRD or FAQ — one strong deliverable beats three weak ones |

### Part 5: Demo Preparation (10 min)

**Goal:** Everyone has a 3-minute demo script ready.

**Time-boxing guidance:**

- "You have 3 minutes max. Plan for 2:30 and leave room for questions."
- Share the demo template from the README (Problem → Architecture → Live Demo → Lessons)
- Remind teams: show something WORKING, not a code walkthrough

**Offer practice runs** to anyone with demo anxiety — even 60 seconds of rehearsal helps.

### Part 6: Presentations & Celebration (15 min)

**How to run demo day:**

1. **Set the order** — volunteers first, then go around the room
2. **Time strictly** — use a visible timer, give 30-second warning
3. **Lead applause** — celebrate every demo regardless of completeness
4. **Facilitate Q&A** — ask one question per demo if the audience is quiet:
   - "What was the hardest part?"
   - "What would you build next?"
   - "How did Claude help most?"

**Evaluation during demos:**

- Use the peer evaluation grid from the README
- Score on the rubric while watching — don't delay this
- Take notes on standout work for recognition in the Engineering All-Hands

---

## Common Questions

| Question | Answer |
| -------- | ------ |
| "Can I change my project option?" | Yes, but only before Part 2 ends. After architecture planning, commit to your choice. |
| "What if I can't finish?" | Focus on having one working feature with tests. Partial implementations with good tests score well. The rubric values quality over breadth. |
| "How is it graded?" | 100-point rubric: Functionality (25), Code Quality (25), Test Coverage (20), Documentation (15), Innovation (15). Non-coding tracks use the alternate rubric. See README for details. |
| "Can I use external packages?" | Yes, as long as they're from NuGet and you can explain why you chose them. |
| "Do I need a frontend for Option B?" | No — the scope note says API-only is sufficient for full marks. Angular frontend is a stretch goal. |
| "Can I work alone?" | Absolutely. Scope to one core feature built end-to-end with full TDD coverage. |
| "What if my tests pass but coverage is below 80%?" | You'll lose points in the Test Coverage category but can still pass. Focus on testing business logic first. |
| "Can I use my Week 5 custom skill?" | Yes! Reusing and improving skills from earlier weeks is encouraged and shows growth. |

---

## Wrap-Up Checklist

### During Close (15 min)

- [ ] Announce evaluation results (or timeline for results if scoring takes longer)
- [ ] Distribute certificates for qualifying scores (70+)
- [ ] Share the course feedback survey — ask everyone to complete it before leaving
- [ ] Highlight standout projects for Engineering All-Hands recognition
- [ ] Share next steps: advanced courses coming soon, `#ai-exchange` channel for ongoing support

### Post-Session

- [ ] Complete all rubric scoring within 3-5 business days
- [ ] Send detailed feedback to each participant
- [ ] Send certificates to anyone who wasn't scored during the session
- [ ] Post highlights and photos (with permission) to `#ai-exchange`
- [ ] Follow up with conditional passes (score 70-79) — what they need to improve
- [ ] Archive capstone submissions for the course repo
- [ ] Update this runbook with any lessons learned from this cohort

---

## Assessment Notes

- Be generous with partial credit — this is a learning exercise, not a gate
- Value learning over perfection — a thoughtful approach with incomplete code beats a copy-paste solution
- Document specific feedback for each participant — they'll want to know what to improve
- Follow up with conditional passes within one week — give them a clear path to full certification
- Non-coding tracks (E, F) should be evaluated against the alternate rubric in the README
- Custom projects (Option G) should be evaluated against the criteria the participant proposed

---

## Emergency Procedures

| Situation | Action |
| --------- | ------ |
| Template won't build | Have a pre-built backup in your instructor sandbox |
| Claude Code is down | Pivot to architecture-only exercise: design + pseudocode + test plan on paper |
| Participant drops out mid-session | Their team continues; solo participants can submit later |
| Network issues | Have offline copies of all templates; tests can run locally |
| Time crunch | Cut Part 5 (demo prep) to 5 min; reduce demo time to 2 min each |
