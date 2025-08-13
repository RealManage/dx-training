# Week 3: Tactical Planning & Code Review Excellence 🎯

**Duration:** 2.5 hours  
**Format:** In-person or virtual  
**Audience:** RealManage cross-functional team  
**Prerequisites:** Completed Weeks 1-2

## 🎯 Learning Objectives

By the end of this session, participants will be able to:
- ✅ Use plan mode as a tactical thinking partner for complex tasks
- ✅ Iterate on plans in real-time before executing
- ✅ Organize code review fixes systematically
- ✅ Plan phases just-in-time rather than everything upfront
- ✅ Switch to Opus model for deep code analysis
- ✅ Execute multi-step tasks without forgetting anything

## 📋 Pre-Session Checklist

### For Participants
- [ ] Claude Code working smoothly from Weeks 1-2
- [ ] Comfortable with Shift+Tab for plan mode
- [ ] Ready for 2.5 hour session
- [ ] A real ticket/issue you're working on (optional)

### For Instructors
- [ ] Test all three example projects
- [ ] Practice the code review workflow
- [ ] Prepare real-world examples
- [ ] Have backup exercises ready

## 🚀 The New Philosophy

> "Plan mode isn't about creating upfront documentation. It's your thinking partner - use it to iterate on complex tasks, organize code reviews, and ensure nothing gets forgotten. Plan tactically, execute systematically."

## 📚 Session Plan

### Part 1: Tactical Planning Fundamentals (30 min)

#### 1.1 Claude's Three Modes: Step, Auto, and Plan (10 min)

**Understanding Claude's Modes:**

**Step Mode (Default):**
- Claude stops after each action for your approval
- Perfect for learning what Claude does
- Provides careful control over each operation

**Auto Mode:**
- Claude automatically decides when to plan vs execute
- Great for straightforward tasks and exploration
- Can sometimes overthink or go off-track

**Plan Mode:**
- Claude thinks through the approach first
- Allows iteration on the plan before execution
- Ideal for complex, multi-step tasks

**Essential Controls:**
- **Shift+Tab**: Toggle between auto/step/plan modes
- **Esc**: STOP execution immediately (your emergency brake!)
- **Ctrl+C**: Cancel current operation

**The Old Way (Wrong):**
```
Spend days creating elaborate planning documents →
Try to implement exactly as planned →
Reality doesn't match the plan →
Wasted time
```

**The New Way (Right):**
```
Get a complex task →
Enter plan mode (Shift+Tab) →
Iterate on your approach through conversation →
Refine until the plan feels right →
Exit plan mode and execute with confidence
```

**Key Insight:** Plan mode is for **thinking through** problems, not documenting them.

#### 1.2 When to Use Plan Mode (10 min)

**Perfect for:**
- 📝 Code review responses (3+ items to fix)
- 🔄 Multi-file refactoring
- 📋 Phased ticket implementation
- 🐛 Complex debugging sessions
- 🏗️ Features touching multiple components

**Skip plan mode for:**
- Single-line fixes
- Obvious bugs
- Adding a simple field
- Typo corrections
- Straightforward test additions

#### 1.3 The Iteration Pattern (10 min)

**In plan mode, you can refine your thinking:**
```
You: "I need to fix these 8 code review comments"
Claude: "Here's a plan..."
You: "Actually, let's group the security fixes together"
Claude: "Updated plan..."
You: "What if we fix the tests first to ensure safety?"
Claude: "Even better approach..."
```

This iteration BEFORE execution saves hours of rework!

### Part 2: Code Review Mastery (30 min)

#### 2.1 Using Opus for Deep Analysis (10 min)

**The `/model` command example:**
```bash
# Switch to Opus 4.1 for thorough code review
/model opus

# Now ask for deep analysis
> Review this payment processing code for:
> - Performance issues
> - Security vulnerabilities  
> - Missing error handling
> - Test coverage gaps
> - Code smells and tech debt

# IMPORTANT: Switch back to Sonnet after review to save costs
/model sonnet
```

**Why Opus for reviews?** Deeper analysis, catches subtle bugs, better at architecture.
**Why switch back?** Sonnet is more cost-effective for implementation work.

#### 2.2 The Code Review Workflow (10 min)

```
1. Set model to Opus: /model opus
2. Request comprehensive review
3. Switch back to Sonnet: /model sonnet
4. Enter plan mode (Shift+Tab)
5. Organize fixes by priority/type
6. Iterate on the plan until satisfied
7. Exit plan mode
8. Execute systematically
9. (Optional) Switch to Opus for final verification
10. Switch back to Sonnet for regular work
```

#### 2.3 Managing Claude When Things Go Wrong (10 min)

**Recognizing When to Intervene:**
- Claude doing unnecessary refactoring
- Fixing things that aren't broken
- Creating files you didn't ask for
- Missing the actual problem while fixing style issues
- Analysis paralysis in auto mode

**How to Course-Correct:**

**Scenario 1: Claude starts over-engineering**
```
You: "Fix the interest calculation bug"
Claude: [starts refactoring entire codebase]
You: *Hit Esc*
You: *Shift+Tab to switch to step mode*
You: "Just fix the bug on line 29, nothing else"
Claude: [shows next action, waits for approval]
```

**Scenario 2: Claude misses the point**
```
You: "Review this payment code"
Claude: [focuses only on formatting]
You: *Hit Esc*
You: *Shift+Tab to switch to plan mode*
You: "Focus on security vulnerabilities and business logic bugs"
Claude: [creates focused plan]
```

**Pro tip:** Don't wait for disaster - hit Esc the moment you see Claude going off-track. Switch modes to get back on rails. Think of it like grabbing the wheel when your GPS tries to take you through a lake!

### Part 3: Hands-On Workshop (75 min)

#### 3.1 Exercise C: BugHunter (15 min)

**Setup:**
```bash
cd courses/ai-101-claude-code/sessions/week-3
cp -r examples/bug-hunter sandbox-bug-hunter
cd sandbox-bug-hunter

claude
```

**Your Mission:**
1. Users report: "Interest calculations are wrong after 90 days"
2. Enter plan mode to plan investigation
3. Execute investigation
4. Enter plan mode again to plan fix
5. Execute fix
6. Verify with tests

**The Pattern:**
```
Bug Report → Plan Investigation → Investigate → Plan Fix → Fix → Verify
```

#### 3.2 Exercise A: CodeReviewPro (30 min)

**Setup:**
```bash
cd courses/ai-101-claude-code/sessions/week-3
cp -r examples/codereview-pro sandbox-codereview-pro
cd sandbox-codereview-pro

claude
```

**Your Mission:**
1. Review the payment processing code with Opus
2. Find at least 8 issues
3. Enter plan mode and organize all fixes
4. Iterate on your plan (group related fixes)
5. Execute the plan
6. Verify all issues are resolved

**Files to Review:**
- `PaymentService.cs` - Performance and validation issues
- `LateFeecalculator.cs` - Calculation bugs and typos
- `PaymentController.cs` - Security vulnerabilities
- `Tests/` - Missing coverage

#### 3.3 Exercise B: PhasedBuilder (30 min)

**Setup:**
```bash
cd courses/ai-101-claude-code/sessions/week-3
cp -r examples/phased-builder sandbox-phased-builder
cd sandbox-phased-builder

claude
```

**Your Mission:**
Work through a 3-phase ticket:

**Phase 1:** Add payment plan model
- Enter plan mode for Phase 1 only
- Plan the data model and basic CRUD
- Execute

**Phase 2:** Add business logic
- Enter plan mode for Phase 2
- Plan calculations and rules
- Execute

**Phase 3:** Add API endpoints
- Enter plan mode for Phase 3
- Plan controller and validation
- Execute

**Key Learning:** Plan each phase as you reach it, not all upfront!

### Part 4: Q&A and Wrap-Up (15 min)

- Questions about the exercises
- Troubleshooting any issues
- Preview of Week 4 (TDD with Claude)
- Reminder about homework assignment

### Common Pitfalls to Avoid

**❌ Don't:**
- Create 50-step plans
- Plan more than 1-2 phases ahead
- Spend more than 10 minutes planning
- Try to anticipate every edge case

**✅ Do:**
- Keep plans focused and tactical
- Iterate on plans before executing
- Plan phases just-in-time
- Use plan mode for memory/organization

## 🎯 Key Takeaways

### The Tactical Planning Principles
1. **Plan mode is for thinking, not documenting**
2. **Iterate on plans before executing**
3. **Plan phases as you reach them**
4. **Use Opus for deep code reviews**
5. **Switch back to Sonnet for implementation**
6. **Group related changes together**
7. **Execute systematically, verify completely**

### Quick Reference Card
```
CODE REVIEW PATTERN:
/model opus → Review → /model sonnet → Plan Mode → Execute → Verify

MODEL SWITCHING PATTERN:
Opus for: Deep reviews, security analysis, architecture decisions
Sonnet for: Implementation, refactoring, tests, regular coding
Remember: Always switch back to Sonnet after Opus reviews!

PHASED WORK PATTERN:
Phase 1: Plan → Do → Phase 2: Plan → Do → Phase 3: Plan → Do

BUG FIX PATTERN:
Report → Plan Investigation → Investigate → Plan Fix → Fix → Test

MULTI-STEP PATTERN:
Enter Plan Mode → List all tasks → Group/order → Iterate → Execute
```

## 📝 Homework (Before Week 4)

### Required Tasks:
1. ✅ Use plan mode for your next code review response
2. ✅ Practice the investigation → fix pattern on a real bug
3. ✅ Time yourself: planning vs execution
4. ✅ Share your experience in `#dx-training`
5. ✅ Try Opus model for reviewing your own code

### Stretch Goals:
1. 🎯 Create a phased plan for a complex feature
2. 🎯 Use plan mode to organize a refactoring
3. 🎯 Teach someone else the tactical planning pattern

### Reflection Questions:
- When did iterating on a plan save you time?
- What tasks actually benefited from plan mode?
- How did Opus reviews compare to your usual process?

### Real-World Application
1. **Choose a real ticket** from your current sprint
2. **Use Opus** (`/model opus`) to review any existing code
3. **Enter plan mode** to organize your approach
4. **Iterate on the plan** - refine it through conversation
5. **Execute systematically** - follow your plan
6. **Share in Slack** - Post your experience in #dx-training

## 🔗 Resources

### Official Documentation
- [Claude Code Plan Mode](https://docs.anthropic.com/en/docs/claude-code#plan-mode)
- [Model Selection](https://docs.anthropic.com/en/docs/claude-code#model-selection)
- [Opus 4.1 Features](https://www.anthropic.com/news/opus-4-1)

### RealManage Resources
- [Week 3 Examples](./examples/) - All three practice projects
- [Code Review Standards](../../resources/code-review-guide.md)
- Slack: `#dx-training` for planning help

### Additional Reading
- [Tactical vs Strategic Planning](https://en.wikipedia.org/wiki/Tactical_planning)
- [Just-In-Time Planning](https://www.agilealliance.org/glossary/just-in-time/)
- [Code Review Best Practices](https://google.github.io/eng-practices/review/)

## 📊 Success Metrics

### You're ready for Week 4 when you can:
- [ ] Use plan mode to organize 3+ review comments
- [ ] Iterate on a plan before executing
- [ ] Plan phases separately, not all at once
- [ ] Switch to Opus for code reviews
- [ ] Execute multi-step tasks without forgetting items

### Red Flags (seek help if):
- [ ] Still creating long upfront plans
- [ ] Not iterating on plans in plan mode
- [ ] Forgetting code review items
- [ ] Planning taking longer than execution

## 🚀 Next Week Preview

**Week 4: Test-Driven Development with Claude**
- Write tests FIRST (Red phase)
- Let Claude implement to pass tests (Green phase)
- Refactor while keeping tests green
- Achieve 95% coverage naturally

**Pre-work:** Review your current testing practices

---

## Instructor Notes

### Session Timing (2.5 hours)
- Part 1: 30 min - Mode controls and planning fundamentals
- Part 2: 30 min - Code review mastery with Opus
- Part 3: 75 min - Heart of session, all three exercises
- Part 4: 15 min - Q&A and wrap-up
- **Total: 2h 30min**
- Homework: Apply to real work (self-paced)

### Key Points to Emphasize
- **Iteration in plan mode** - This is the magic
- **Tactical not strategic** - Just-in-time planning
- **Opus for reviews** - Worth the model switch
- **Systematic execution** - The plan prevents forgotten items

### Common Questions

**"How long should I stay in plan mode?"**
- 5-10 minutes max for most tasks
- If you're iterating productively, continue
- If you're going in circles, exit and start

**"Should I plan everything in one session?"**
- No! Plan the next phase only
- Come back to plan mode between phases
- Keep context focused

**"When is Opus worth it?"**
- Code reviews always
- Security-sensitive code
- Complex architectural decisions
- When you need a second opinion
- Remember: Switch back to Sonnet after!

### Troubleshooting
- If students struggle with plan mode, show the Shift+Tab toggle
- If plans are too long, enforce 5-minute timer
- If they forget `/model opus`, add reminders
- If exercises run long, skip BugHunter

### Assessment
Quick check at end:
1. When should you use plan mode?
2. How do you switch to Opus model?
3. What's the benefit of iterating on plans?
4. Should you plan all phases upfront?

---

*End of Week 3 Session Plan*  
*Next Session: Week 4 - Test-Driven Development with Claude*