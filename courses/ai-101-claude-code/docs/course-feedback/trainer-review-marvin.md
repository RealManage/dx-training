# AI-101 Claude Code: Trainer Review

**Reviewer:** Marvin (AI Assistant)
**Date:** January 28, 2026
**Overall Rating:** 8.5/10

---

## Executive Summary

This course is **well-designed, thoughtful, and student-centric**. It demonstrates strong instructional design with clear scaffolding, role-specific paths, and practical HOA domain examples. The main opportunities are around timing adjustments, trainer preparation resources, and filling a few gaps in supporting materials.

**Bottom line:** Ready to deliver with minor prep work. Students will ship better code if they complete this.

---

## Section 1: Week-by-Week Assessment

### Weeks 0-4 (Foundations) - Strong

| Week | Content | Timing | Trainer Notes |
| ---- | ------- | ------ | ------------- |
| **0** | AI Foundations | 1hr (generous) | Optional but valuable for vocabulary. Skip for senior devs. |
| **1** | Setup | 2hr (optimistic) | **Biggest risk.** Windows users need 30-45 min buffer for setup issues. Have helpers. |
| **2** | Prompting | 1.5hr (realistic) | Anti-dogma approach ("XML rarely needed") is refreshing. Prompt Lab exercises are strong. |
| **3** | Plan Mode | 1.5hr (tight) | Shift+Tab muscle memory is critical. 28-issue CodeReview exercise is ambitious - set expectations at 14-20. |
| **4** | TDD | 2hr (realistic) | **Best week.** Role-specific tracks (Dev/QA/PM) are excellent. PM track on testable requirements is gold. |

**Key Observations:**

- Week 1 setup is the make-or-break session. Budget extra time.
- Week 4 role-specific tracks show mature instructional design.
- CLAUDE.md reinforcement throughout is effective.

---

### Weeks 5-9 (Advanced) - Good with Timing Concerns

| Week | Content | Stated | Realistic | Gap |
| ---- | ------- | ------ | --------- | --- |
| **5** | Commands/Skills | 1.5hr | 2hr | -30 min |
| **6** | Agents/Hooks | 2hr | 2.5-3hr | -30 to -60 min |
| **7** | Plugins | 2hr | 2hr | OK |
| **8** | Real-World | 2hr | 2.5hr | -30 min |
| **9** | Capstone | 2hr | 2.5hr | -30 min |

**Key Observations:**

- Course is overloaded by 15-25%. Plan to either extend sessions or reduce scope.
- Week 6 hooks are complex and error-prone. Students won't know if hooks work without debugging guidance.
- Week 7 marketplace content is speculative - focus on local plugins.
- Week 9 capstone templates must be verified before launch.

---

## Section 2: What Works Exceptionally Well

### Instructional Design

- **Role-specific tracks** prevent one-size-fits-all failure
- **Progressive complexity** - Foundations (1-4) to Advanced (5-9) to Synthesis (9)
- **HOA domain integration** - Violations, late fees, grace periods appear throughout
- **Hands-on > theory** - Every week has exercises, minimal lecture

### Content Quality

- **Decision trees** are excellent reference material
- **Glossary** is comprehensive without being overwhelming
- **Skeptics FAQ** is honest and transparent - addresses real concerns
- **PM track** doesn't assume coding knowledge (inclusive)
- **TDD emphasis** is appropriate and well-positioned

### Tone

- Supportive without being condescending
- Honest about AI limitations ("can't understand your domain")
- Growth mindset: "No penalty for going slower"
- "We're confirming value, not grading you"

---

## Section 3: Gaps and Concerns

### High Priority (Fix Before Launch)

| Issue | Impact | Recommendation |
| ----- | ------ | -------------- |
| **No trainer runbook** | Inconsistent delivery | Create week-by-week trainer prep checklists |
| **Timing is optimistic** | Sessions run over | Build 15-30 min buffer into each session |
| **Capstone templates unverified** | Week 9 chaos | Test all 6 capstone starter projects |
| **Meeting details TBD** | Confusion | Finalize times/links before announcement |
| **No quick reference** | Students lost | Create 1-page CLI commands + CLAUDE.md cheat sheet |

### Medium Priority (Important for Success)

| Issue | Impact | Recommendation |
| ----- | ------ | -------------- |
| **YAML not explained** | Week 5-6 errors | Add 5-min YAML primer before skills section |
| **Hook debugging absent** | Students stuck | Add "how to verify hooks work" section |
| **Capstone rubric vague** | Inconsistent reviews | Create explicit scoring criteria |
| **Bash assumed** | QA/PM struggle in Week 8 | Add bash primer for non-developers |
| **No "Getting Help" mega-section** | Support scattered | Single source of truth for all support channels |

### Low Priority (Nice to Have)

- Migration guides (from Copilot, ChatGPT, Cursor)
- Interactive web-based decision trees
- RealManage success stories document
- Accessibility checklist for inclusive delivery

---

## Section 4: Presentation Strategy

### How I'd Deliver This Course

**Week 1 (Setup) - HIGH TOUCH**

- Pre-session: Send `claude doctor` instructions, ask students to verify before Friday
- Have 2-3 helpers for Windows troubleshooting
- If someone is stuck at 20 min, intervention needed
- End with "Day 1 wins" - everyone runs one successful prompt

**Week 2 (Prompting) - INTERACTIVE**

- Live demo: Take a bad prompt, improve it together
- Let students fail - then show why natural language beats XML
- "Prompt Lab" exercises should be pair work if possible

**Week 3 (Plan Mode) - PRACTICE-HEAVY**

- 2 min of Shift+Tab practice (muscle memory)
- Live code review together before individual exercises
- Set expectations: "Finding 14-20 issues is excellent"

**Week 4 (TDD) - ROLE SPLIT**

- Clearly assign tracks before session (Dev/QA/PM)
- Developers: Enforce TDD discipline - don't let them skip tests
- QA: Position as "your superpower" not developer copycat
- PM: Focus on "how to write requirements devs can test"

**Weeks 5-7 (Skills/Agents/Plugins) - CHOOSE WISELY**

- Week 5: Walk through creating ONE command together before independent work
- Week 6: Hooks can break workflows - test in sandbox first
- Week 7: Soften marketplace section - focus on local plugins

**Week 8 (Real-World) - PRACTICAL**

- This is where it clicks for everyone
- QA/PM may struggle with bash - have snippets ready to copy
- Discuss cost implications of batch operations

**Week 9 (Capstone) - CELEBRATE**

- Verify templates before session
- Scope creep is the enemy - enforce MVP
- Even imperfect capstones are achievements

---

## Section 5: Student Pain Points to Anticipate

| When | Pain Point | What to Say |
| ---- | ---------- | ----------- |
| Week 1 | Windows auth fails | "Let's try the backup method. This happens to 30% of setups." |
| Week 2 | First prompt fails | "Perfect - now we know what to fix. Let's iterate." |
| Week 3 | Plan mode feels slow | "Upfront planning saves 3x rework later. Trust the process." |
| Week 4 | TDD feels tedious | "Week 8 you'll see why this matters. Stick with it." |
| Week 5 | YAML syntax errors | "YAML is picky. Here's a template - copy this exactly." |
| Week 6 | Hook doesn't work | "Let's verify it's triggering. Add a log line first." |
| Week 8 | Batch script fails | "Silent failures are the worst. Let's add error handling." |
| Week 9 | Capstone too ambitious | "MVP first. You can extend after certification." |

---

## Section 6: What's Missing for Trainers

### Trainer Prep Checklist (Create This)

**Before Each Week:**

- [ ] Test all exercises in sandbox
- [ ] Review common gotchas for this week
- [ ] Prepare 2-3 "what if" scenarios
- [ ] Have backup examples ready
- [ ] Know the "cut this if running late" sections

**Before Week 1 Specifically:**

- [ ] Test installation on Windows (Git Bash, WSL, PowerShell)
- [ ] Prepare "claude doctor" output examples
- [ ] Have backup network/hotspot for VPN issues
- [ ] Recruit 2-3 helpers for setup troubleshooting

**Before Week 9 Specifically:**

- [ ] Verify ALL capstone templates build and run
- [ ] Pre-assign capstone options by role
- [ ] Prepare "MVP scope" guidance for each option
- [ ] Test demo equipment and recording

### Trainer FAQ (Create This)

- **Q: How much should I let Claude fail?**
  A: Let them hit errors in Week 2-3. By Week 4, intervene faster - TDD frustration is real.

- **Q: When should I intervene in a student-Claude conversation?**
  A: If they're in a loop for >5 min, or if Claude is hallucinating and they don't notice.

- **Q: What's the hardest week to teach?**
  A: Week 4 (TDD) for developers who resist test-first. Week 6 (Hooks) for everyone.

- **Q: What if someone says "this isn't for me"?**
  A: Acknowledge, don't argue. Ask what would make it useful. Sometimes they just need a different example.

---

## Section 7: Redundancies to Clean Up

| Content | Appears In | Recommendation |
| ------- | ---------- | -------------- |
| "2 hours per week" | README, schedules, quick starts | State once, reference elsewhere |
| Course structure table | training-schedule.md, rollout.md | *(Already fixed in pending commit)* |
| Coverage rationale (80-90%) | Troubleshooting (twice), FAQ | Consolidate to single reference |
| Prerequisites | README, each track | Main list in README, tracks reference it |
| Support channels | Multiple docs | Create single "Getting Help" section |

---

## Section 8: Final Recommendations

### Before First Cohort

1. Finalize meeting times and send calendar invites
2. Test all capstone templates
3. Create trainer runbook (1 per week)
4. Build quick reference cheat sheet
5. Recruit 2-3 setup helpers for Week 1

### During Delivery

1. Buffer 15-30 min into each session
2. Use decision trees as teaching tools
3. Let students fail safely (especially Week 2-3)
4. Enforce TDD discipline in Week 4
5. Celebrate completions, even imperfect ones

### After First Cohort

1. Collect feedback (weekly pulse + post-program NPS)
2. Adjust timing based on actual session lengths
3. Document "what actually went wrong" for next cohort
4. Update trainer runbook with lessons learned

---

## Conclusion

This is a **genuinely good course** that will help RealManage teams ship better code faster. The instructional design is thoughtful, the content is practical, and the tone is supportive without being condescending.

**Main risks:**

- Week 1 setup issues derailing momentum
- Timing overruns in Weeks 5-9
- Capstone templates not being ready

**Mitigation:**

- Prep helpers for Week 1
- Build in buffers
- Verify templates now

**Confidence level:** High. With the prep work above, this course will succeed.

---

*Review conducted by Marvin. Maximum honesty. Minimum BS.*
