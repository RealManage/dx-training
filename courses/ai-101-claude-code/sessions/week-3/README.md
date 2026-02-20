# Week 3: Tactical Planning & Code Review Excellence 🎯

**Duration:** 2 hours
**Format:** In-person or virtual
**Audience:** RealManage cross-functional team (engineers, PMs, QA, support staff)
**Prerequisites:** Completed Weeks 1-2

## Learning Tracks

This week has role-specific tracks:

- **[Developer Track](./tracks/developer.md)** - Debug, investigate bugs, and plan multi-phase builds
- **[QA Track](./tracks/qa.md)** - Plan test strategies, analyze defects, and design regression suites
- **[PM Track](./tracks/pm.md)** - Plan feature rollouts, phase delivery, and analyze trade-offs
- **[Support Track](./tracks/support.md)** - Triage issues, plan escalation workflows, and organize knowledge bases

---

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
- [ ] Ready for 2 hour session
- [ ] A real ticket/issue you're working on (optional)

## 🚀 The New Philosophy

> "Plan mode isn't about creating upfront documentation. It's your thinking partner - use it to iterate on complex tasks, organize code reviews, and ensure nothing gets forgotten. Plan tactically, execute systematically."

## 📚 Session Plan

### Part 1: Tactical Planning Fundamentals (40 min)

#### 1.1 Claude's Four Permission Modes (10 min)

**Understanding Claude's Modes:**

**Ask Permissions (Default):**

- Claude asks before editing files or running commands
- You see a diff and can accept or reject each change
- Perfect for learning what Claude does and maintaining control

**Auto Accept Edits:**

- Claude auto-accepts file edits without prompting
- Still asks before running terminal commands
- Great for trusted changes when you want faster iteration

**Plan Mode:**

- Claude analyzes code and creates a plan without modifying anything
- Allows iteration on the plan before execution
- Ideal for complex, multi-step tasks

**Bypass Permissions:**

- Claude runs without any permission prompts
- Only use this in sandboxed containers or VMs
- Equivalent to `--dangerously-skip-permissions` CLI flag

**Essential Controls:**

- **Shift+Tab**: Cycle through modes (ask permissions → auto accept edits → plan → bypass permissions → ...)
- **Esc**: Interrupt Claude's current response (returns to input prompt)
- **Ctrl+C**: Exit Claude Code

**The Old Way (Wrong):**

```text
Spend days creating elaborate planning documents →
Try to implement exactly as planned →
Reality doesn't match the plan →
Wasted time
```

**The New Way (Right):**

```text
Get a complex task →
Enter plan mode (Shift+Tab) →
Iterate on your approach through conversation →
Refine until the plan feels right →
Exit plan mode and execute with confidence
```

**Key Insight:** Plan mode is for **thinking through** problems, not documenting them.

#### 1.2 Plan Mode Demonstration (10 min)

**Activating Plan Mode:**

- **Shift+Tab**: Cycle through all four permission modes
- **`/plan`**: Jump directly to plan mode from any mode

> **Note:** Both methods work identically. Use whichever feels more natural. Track exercises use `/plan` for clarity in prompts, but `Shift+Tab` is often faster in practice.

**Before/After Example: Multi-File Refactoring**

Let's say you need to rename `CalculateFee` to `CalculateLateFee` across 5 files and update all tests.

**❌ WITHOUT Plan Mode:**

```text
You: "Rename CalculateFee to CalculateLateFee everywhere"

Claude: [immediately starts editing]
- Renamed in PaymentService.cs ✓
- Renamed in FeeCalculator.cs ✓
- Renamed in Tests... wait, found 3 more usages
- [edits more files]
- Oh, also found it in a comment... [edits]
- And in a string literal... [edits - maybe wrong!]
- Done! Made 12 changes across 7 files.

You: "Wait, that string literal was a log message, not code!"
```

**✅ WITH Plan Mode:**

```text
You: [Shift+Tab to enter plan mode]
You: "Rename CalculateFee to CalculateLateFee everywhere"

Claude: "Here's my plan:
1. Search for all usages of CalculateFee
2. Categorize by type:
   - Method definitions (2 files)
   - Method calls (3 files)
   - Test files (2 files)
   - Comments/strings (will skip unless you want)
3. Rename in this order:
   - Definition first (FeeCalculator.cs)
   - Callers (PaymentService.cs, Controller.cs)
   - Tests last (verify they still compile)
4. Run tests to verify"

You: "Good, but also update the XML doc comments"
Claude: "Updated plan - adding XML doc updates after step 2..."

You: "Looks good, execute it"
[Exit plan mode, Claude executes systematically]
```

**The Difference:**

| Without Plan Mode | With Plan Mode |
| ----------------- | -------------- |
| Immediate execution | Think first, act second |
| May miss edge cases | Comprehensive review |
| Hard to course-correct | Easy to refine approach |
| Surprises during execution | No surprises |

**When to Use Which Mode?**
See the [Decision Trees](../../resources/decision-trees.md) for a visual flowchart on choosing the right mode for your task.

**🎯 Mini-Exercise: Try Plan Mode (5 min)**

Try this right now in your Claude session:

```bash
# Enter plan mode
# Press Shift+Tab until you see "plan" indicator
# Or type: /plan

> I need to add a new property 'GracePeriodDays' (int, default 30)
> to the LateFeecalculator class, update its constructor,
> and add unit tests for it.
```

**What to look for:**

1. Does Claude's plan include all affected files?
2. Does it mention constructor changes?
3. Does it plan for test coverage?
4. Can you suggest improvements to the plan?

**After reviewing the plan:** Say "looks good" or refine it, then exit plan mode to execute.

#### 1.3 When to Use Plan Mode (10 min)

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

#### 1.4 The Iteration Pattern (10 min)

**In plan mode, you can refine your thinking:**

```text
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
# Switch to Opus for thorough code review
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

```text
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
- Going in circles without making progress

**How to Course-Correct:**

**Scenario 1: Claude starts over-engineering**

```text
You: "Fix the interest calculation bug"
Claude: [starts refactoring entire codebase]
You: *Hit Esc*
You: *Shift+Tab until you're in Ask Permissions mode*
You: "Just fix the bug on line 29, nothing else"
Claude: [shows next action, waits for approval]
```

**Scenario 2: Claude misses the point**

```text
You: "Review this payment code"
Claude: [focuses only on formatting]
You: *Hit Esc*
You: *Shift+Tab until you're in Plan mode*
You: "Focus on security vulnerabilities and business logic bugs"
Claude: [creates focused plan]
```

**Pro tip:** Don't wait for disaster - hit Esc the moment you see Claude going off-track. It interrupts the current response and returns you to the input prompt. Switch modes to get back on rails. Think of it like grabbing the wheel when your GPS tries to take you through a lake!

### Part 3: Hands-On Exercises (45 min)

Choose the exercise track that matches your role. All tracks practice plan mode with different focus areas:

| Track | Best For | Focus Area |
| ----- | -------- | ---------- |
| [Developer](tracks/developer.md) | Engineers | Debug, investigate bugs, and plan multi-phase builds |
| [QA](tracks/qa.md) | QA engineers | Plan test strategies, analyze defects, and design regression suites |
| [PM](tracks/pm.md) | Product managers | Plan feature rollouts, phase delivery, and analyze trade-offs |
| [Support](tracks/support.md) | Support staff | Triage issues, plan escalation workflows, and organize knowledge bases |

**Instructions:**

1. Click your track link above
2. Complete the 30-45 minute exercise
3. Return here for the wrap-up discussion

> **Note:** All tracks practice plan mode and systematic execution through your daily work lens.

---

### Part 3.5: Additional Exercises (Optional)

> Choose ONE additional exercise if time permits. Complete others as homework.

#### 3.1 Exercise A: BugHunter (15 min)

**Setup:**

```bash
cd courses/ai-101-claude-code/sessions/week-3
cp -r examples sandbox
cd sandbox/bug-hunter

claude
```

> **Not familiar with these commands?** No worries - Claude can walk you through them. Just ask!

**Your Mission:**

1. Users report: "Interest calculations are wrong after 90 days"
2. Enter plan mode to plan investigation
3. Execute investigation
4. Enter plan mode again to plan fix
5. Execute fix
6. Verify with tests

**The Pattern:**

```text
Bug Report → Plan Investigation → Investigate → Plan Fix → Fix → Verify
```

#### 3.2 Exercise B: CodeReviewPro (15 min)

> **Important:** This exercise contains intentional bugs AND 6 build warnings. Fix the warnings plus find at least 8 additional code issues!

**Setup:**

```bash
# If you haven't already created the sandbox:
cd courses/ai-101-claude-code/sessions/week-3
cp -r examples sandbox

# Navigate to this exercise
cd sandbox/codereview-pro

claude
```

**Your Mission:**

1. Review the payment processing code with Opus
2. Find at least 8 issues PLUS fix the 6 build warnings
3. Enter plan mode and organize all fixes
4. Iterate on your plan (group related fixes)
5. Execute the plan
6. Verify all issues are resolved AND builds without warnings

**Files to Review:**

- `PaymentService.cs` - Performance and validation issues
- `LateFeecalculator.cs` - Calculation bugs and typos
- `PaymentController.cs` - Security vulnerabilities
- `Tests/` - Missing coverage

**📊 CodeReviewPro Success Metrics**  

Without giving away the answers, here's how to measure your success:

**Issue Count by Category**  

- **Build Warnings:** 6 to fix
- **Security Issues:** 2+ to find
- **Performance Issues:** 3+ to find  
- **Logic Bugs:** 8+ to find
- **Code Smells:** 5+ to find
- **Typos:** 2+ to find

**Total: 28+ issues available to find and fix!**

**Success Levels**

- 🥉 **Bronze:** Fixed all 6 warnings + found 8 other issues (14 total)
- 🥈 **Silver:** Found and fixed 20+ issues
- 🥇 **Gold:** Found and fixed 25+ issues
- 💎 **Diamond:** Found all 28+ issues

Don't worry if you don't find them all in the session - the important thing is practicing the plan mode workflow and systematic code review!

#### 3.3 Exercise C: PhasedBuilder (15 min)

**Setup:**

```bash
# If you haven't already created the sandbox:
cd courses/ai-101-claude-code/sessions/week-3
cp -r examples sandbox

# Navigate to this exercise
cd sandbox/phased-builder

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

```text
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

### Required Tasks

1. ✅ Use plan mode for your next code review response
2. ✅ Practice the investigation → fix pattern on a real bug
3. ✅ Time yourself: planning vs execution
4. ✅ Share your experience in `#ai-exchange`
5. ✅ Try Opus model for reviewing your own code

### Stretch Goals

1. 🎯 Create a phased plan for a complex feature
2. 🎯 Use plan mode to organize a refactoring
3. 🎯 Teach someone else the tactical planning pattern

### Reflection Questions

- When did iterating on a plan save you time?
- What tasks actually benefited from plan mode?
- How did Opus reviews compare to your usual process?

### Real-World Application

1. **Choose a real ticket** from your current sprint
2. **Use Opus** (`/model opus`) to review any existing code
3. **Enter plan mode** to organize your approach
4. **Iterate on the plan** - refine it through conversation
5. **Execute systematically** - follow your plan
6. **Share in Slack** - Post your experience in #ai-exchange

## 🔗 Resources

### Official Documentation

- [Claude Code Interactive Mode](https://code.claude.com/docs/en/interactive-mode)
- [Model Configuration](https://code.claude.com/docs/en/model-config)
- [Opus 4.6 Features](https://www.anthropic.com/news/claude-opus-4-6)

### RealManage Resources

- [Week 3 Examples](./examples/) - All three practice projects
- Slack: `#ai-exchange` for planning help

### Additional Reading

- [Tactical vs Strategic Planning](https://en.wikipedia.org/wiki/Tactical_planning)
- [Just-In-Time Planning](https://www.agilealliance.org/glossary/just-in-time/)
- [Code Review Best Practices](https://google.github.io/eng-practices/review/)

## 📊 Success Metrics

### You're ready for Week 4 when you can

- [ ] Use plan mode to organize 3+ review comments
- [ ] Iterate on a plan before executing
- [ ] Plan phases separately, not all at once
- [ ] Switch to Opus for code reviews
- [ ] Execute multi-step tasks without forgetting items

### Red Flags (seek help if)

- [ ] Still creating long upfront plans
- [ ] Not iterating on plans in plan mode
- [ ] Forgetting code review items
- [ ] Planning taking longer than execution

## 🚀 Next Week Preview

**Week 4: Test-Driven Development with Claude**

- Write tests FIRST (Red phase)
- Let Claude implement to pass tests (Green phase)
- Refactor while keeping tests green
- Achieve 80-90% coverage naturally

**Pre-work:** Review your current testing practices

---

*End of Week 3 Session Plan*
*Next Session: Week 4 - Test-Driven Development with Claude*

---

**Next:** [Week 4: Test-Driven Development](../week-4/README.md)
