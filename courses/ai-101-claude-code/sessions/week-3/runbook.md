# Week 3: Tactical Planning & Code Review Excellence - Trainer Runbook

## Session Overview

**Duration:** 2 hours
**Format:** In-person or virtual
**Tracks:** Developer, QA, PM, Support

---

## Pre-Session Checklist

### For Instructors

- [ ] Test all three example projects build (`dotnet build` in bug-hunter, codereview-pro, phased-builder)
- [ ] Practice the plan mode toggle (Shift+Tab) and `/plan` command
- [ ] Practice the `/model opus` → review → `/model sonnet` workflow
- [ ] Verify `sandbox/` directory pattern works (`cp -r examples sandbox`)
- [ ] Confirm you can demonstrate Esc to interrupt a response
- [ ] Prepare real-world examples of plan mode saving time
- [ ] Monitor `#ai-exchange` Slack channel
- [ ] Have the [decision tree resource](../../resources/decision-trees.md) open for reference

---

## Timing Guide

| Time | Segment | What Happens |
| ---- | ------- | ------------ |
| 0:00-0:05 | Welcome & Context | Set the stage: "Last week prompting, this week tactical planning" |
| 0:05-0:45 | Part 1: Tactical Planning Fundamentals | Modes demo, plan mode walkthrough, iteration pattern |
| 0:45-1:15 | Part 2: Code Review Mastery | Opus workflow, code review pattern, course-correction techniques |
| 1:15-2:00 | Part 3: Hands-On Exercises | Track-specific exercises (see Track Delivery below) |
| 1:45-2:00 | Part 4: Wrap-Up & Q&A | Discussion, share work, homework, preview Week 4 |

> **Note:** Parts 3 and 4 overlap — start wrapping up at 1:45 even if some participants are still working. They can finish exercises as homework.

---

## Track Delivery Strategy

### Combined Session (Recommended)

All participants attend Parts 1 and 2 together. For Part 3, participants split into track-specific exercises:

- **Developers** follow `tracks/developer.md` (bug investigation + code review with plan mode)
- **QA** follow `tracks/qa.md` (test planning + defect analysis with plan mode)
- **PMs** follow `tracks/pm.md` (feature planning + requirements analysis with plan mode)
- **Support** follow `tracks/support.md` (issue triage + escalation planning with plan mode)

### Instructor Tips for Mixed Audiences

- During Part 1 demo, use a payment-related example — all roles understand payments
- During Part 2, the Opus → review → Sonnet pattern is universal; emphasize that PMs/QA/Support can use Opus for deep analysis too (requirements, test plans, escalation docs)
- When splitting for Part 3, briefly explain: "Find your track file and work through the exercises. All tracks practice plan mode through your daily work lens."
- Pair non-developers with developers if anyone struggles with terminal commands
- For virtual sessions, use breakout rooms by track
- Remind ALL tracks to copy examples to `sandbox/` before editing (prevents git conflicts)

### Solo Delivery (Self-Paced)

If participants are working through the course independently:

- Have them read Parts 1-2 in the README, trying each example as they go
- Recommend they pick the track matching their role for Part 3
- Encourage trying at least one of the optional Part 3.5 exercises (BugHunter, CodeReviewPro, or PhasedBuilder)
- Point them to `#ai-exchange` for async questions

---

## Live Demo Script

### Part 1 Demo: Plan Mode Basics (10 min)

```bash
# 1. Open a Claude session in any project
cd courses/ai-101-claude-code/sessions/week-3/examples/codereview-pro
claude
```

**Demo 1: Shift+Tab Toggle**

- Press Shift+Tab — point out the mode indicator changing
- "See how it cycles through four modes: Ask Permissions → Auto Accept Edits → Plan → Bypass Permissions. Ask Permissions is the default, Auto Accept Edits speeds up file changes, Plan is thinking only, and Bypass is for sandboxed environments."
- Toggle back to normal mode

**Demo 2: /plan Command**

```text
/plan

I need to rename CalculateFee to CalculateLateFee across this project.
What files are affected and what's the safest order to make changes?
```

**Talk through:**

- "Notice Claude is analyzing, not editing. It's building a plan."
- "I can now iterate — 'What about XML doc comments?' or 'Do the tests first for safety.'"
- "When satisfied, I press Shift+Tab to go back to normal mode and say 'execute the plan.'"

**Demo 3: Esc to Interrupt**

- Start a long response from Claude
- Press Esc mid-response
- "Esc interrupts the current response and takes me back to the input prompt. Use it the moment Claude goes off-track."

### Part 2 Demo: Opus Code Review Workflow (10 min)

```bash
# In the codereview-pro example
/model opus
```

**Talk through:**

- "Opus is the deep-thinking model. More expensive, but catches subtle bugs that Sonnet might miss."
- "Use it for reviews, security analysis, and architecture decisions."

```text
Review PaymentService.cs for security vulnerabilities, performance issues, and logic bugs.
```

**After Opus responds:**

```text
/model sonnet
```

- "Always switch back to Sonnet after the review. Sonnet is faster and cheaper for implementation."

```text
/plan
Organize these findings into a prioritized fix plan. Security first, then logic bugs, then performance.
```

- "Now I'm in plan mode, organizing the fixes before touching any code."
- "This is the full pattern: Opus review → Sonnet implementation → Plan mode to organize → Execute systematically."

### Expected Demo Output

**For plan mode demo:**

- Claude should produce a structured plan with numbered steps
- Should identify affected files and suggest an order
- Should NOT make any edits while in plan mode

**For Opus review demo:**

- Should identify SQL injection vulnerability
- Should find missing authorization attributes
- Should catch at least 2-3 logic bugs
- Output will be more detailed than Sonnet would produce

---

## Instructor Notes

### Key Points to Emphasize

- **Iteration in plan mode** — This is the magic. Plans aren't static documents; they're conversations
- **Tactical not strategic** — Plan the next phase only, not the whole project
- **Four permission modes** — Ask Permissions (default), Auto Accept Edits, Plan, and Bypass Permissions. Shift+Tab cycles through them
- **Opus for reviews** — Worth the model switch for deep analysis
- **Systematic execution** — The plan prevents forgotten items
- **Esc is your friend** — Interrupt early when Claude goes off-track

### Common Questions

> **"How long should I stay in plan mode?"**

- 5-10 minutes max for most tasks
- If you're iterating productively, continue
- If you're going in circles, exit and start executing

> **"Should I plan everything in one session?"**

- No! Plan the next phase only
- Come back to plan mode between phases
- Keep context focused

> **"When is Opus worth it?"**

- Code reviews always
- Security-sensitive code
- Complex architectural decisions
- When you need a second opinion
- Remember: Switch back to Sonnet after!

> **"What's the difference between Shift+Tab and /plan?"**

- Same result — both enter plan mode
- Shift+Tab cycles through all four modes (ask permissions → auto accept edits → plan → bypass permissions → ...)
- `/plan` jumps directly into plan mode
- Track exercises use `/plan` for clarity in instructions

---

## Troubleshooting

### Plan Mode Issues

- **Can't find the mode indicator:** Look at the bottom of the Claude Code interface. The current mode is displayed there
- **Plan mode won't activate:** Try `/plan` as an alternative to Shift+Tab
- **Claude executes instead of planning:** Make sure you entered plan mode BEFORE typing the prompt. If you typed the prompt first in normal mode, Claude will execute

### Model Switching Issues

- **`/model opus` not working:** Check Claude Code version is up to date
- **Forgot to switch back to Sonnet:** No harm done, just switch with `/model sonnet`. The main cost is tokens/speed
- **Don't know current model:** Type `/model` with no arguments to see current model

### Exercise Issues

- **`dotnet build` fails in codereview-pro:** Expected! The project has intentional warnings. Students should find and fix them as part of the exercise
- **`dotnet build` fails in phased-builder:** This should build cleanly. Check .NET 10 SDK is installed: `dotnet --list-sdks`
- **Can't find sandbox directory:** Students need to run `cp -r examples sandbox` first. The sandbox is not checked into git
- **Exercises run long:** Skip the optional Part 3.5 exercises. The track-specific exercises in Part 3 are the priority

### Student Struggling with Plan Mode

- Show them the before/after example from Section 1.2 of the README
- Have them start with a simple task: "Plan how to add a property to this class"
- Emphasize: "You're having a conversation about what to do, not writing a spec"
- If plans are too long, enforce a 5-minute timer

---

## Session Wrap-Up Checklist

- [ ] All participants successfully entered and exited plan mode
- [ ] All participants completed at least one track exercise
- [ ] Participants experienced the Opus → Sonnet workflow (at least as a demo)
- [ ] Key takeaway reinforced: "Plan mode is for thinking, not documenting"
- [ ] At least 2-3 participants shared their experience in `#ai-exchange`
- [ ] Homework assigned: use plan mode for next real code review + practice investigation → fix pattern
- [ ] Preview of Week 4 (TDD with Claude) delivered
- [ ] Remind participants to try optional exercises (BugHunter, CodeReviewPro, PhasedBuilder) as homework

---

*End of Week 3 Trainer Runbook*
