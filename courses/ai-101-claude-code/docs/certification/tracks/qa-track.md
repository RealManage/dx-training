# QA Engineer Certification Track

## Overview

The QA track teaches you to use AI for testing workflows: analyzing existing code for testability, writing comprehensive test suites, generating test data, and improving test automation.

**You don't need to be a developer to succeed in this track.**

## Track Duration

**9 weeks (Week 0 optional)**

| Phase | Weeks | Focus |
| ----- | ----- | ----- |
| Foundations | 0-3 | Setup, prompting, plan mode for test planning |
| QA Power Week | 4 | Writing tests for existing code, coverage analysis |
| Skills & Automation | 5-6 | Commands, skills, hooks for test automation |
| Advanced | 7-8 | Plugins (skim), headless test automation |
| Capstone | 9 | Demonstrate AI-assisted testing |

## Week-by-Week Content

### Week 0: AI Foundations (Optional)

- What AI can and can't do
- How language models work
- Setting expectations

### Week 1: Setup & Orientation

- Install Claude Code
- Create your CLAUDE.md
- First conversations with Claude
- **QA Track:** Testability analysis exercise

### Week 2: Prompting Foundations

- Writing effective prompts
- Context and specificity
- Iterating on results
- **QA Track:** Test case generation prompts

### Week 3: Planning & Analysis

- Breaking down complex problems
- Using plan mode for structured analysis
- **QA Track:** Test planning with plan mode, defect analysis

### Week 4: TDD - QA Power Week ⭐

**This is your power week!** While developers learn TDD, you'll learn to:

- Analyze existing code to identify testable behaviors
- Write comprehensive test suites for existing code
- Identify coverage gaps and prioritize what to test
- Generate meaningful test data for edge cases
- Use Claude to suggest tests you might have missed

**Exercises:**

- Test an existing PropertyService (bring coverage from 60% to 80%+)
- Coverage gap analysis and prioritization
- Test data generation with Bogus
- API and integration testing patterns

**QA Track File:** `sessions/week-4/tracks/qa.md`

### Week 5: Commands & Skills

- Creating reusable commands
- Building QA-specific skills
- **QA Track:** Consumer focus - using skills, not creating them

**QA Track File:** `sessions/week-5/tracks/qa.md`

### Week 6: Agents & Hooks

- Understanding agentic workflows
- Using hooks for automation
- **QA Track:** PostToolUse hooks to auto-run tests after code changes

**QA Track File:** `sessions/week-6/tracks/qa.md`

### Week 7: Plugins

- Plugin concepts (skim only for QA)
- **QA Note:** Skip or skim - developer-focused week

### Week 8: Real-World Automation

- Context management, anti-patterns, model selection (shared session)
- **QA Track:** Test automation, coverage analysis, batch quality checks

**QA Track File:** `sessions/week-8/tracks/qa.md`

### Week 9: Capstone

Demonstrate AI fluency with a real project.

**QA Capstone Options:**

| Option | Description |
| ------ | ----------- |
| Option C | Create test automation skills suite |
| Option D | Full domain models with comprehensive test coverage |

**QA Track File:** `sessions/week-9/tracks/qa.md`

## What You'll Be Able to Do

After completing this track:

- [ ] Analyze code for testability without being a developer
- [ ] Write comprehensive test suites for existing code
- [ ] Identify coverage gaps and prioritize testing efforts
- [ ] Generate edge case test data efficiently
- [ ] Use hooks to automate test execution
- [ ] Run headless test automation scripts
- [ ] Know when AI helps QA and when human judgment is essential

## Capstone Requirements

Submit one of the following:

| Option | Description |
| ------ | ----------- |
| Test Suite | Comprehensive test suite for provided code (80%+ coverage) |
| Automation | Test automation skills + headless batch scripts |
| Domain Models | Complete HOA domain with full test coverage |

## This Track Is For You If

- You do manual or automated testing
- You write bug reports and test plans
- You want to identify coverage gaps faster
- You're curious how AI can augment (not replace) QA work

## This Track Isn't About

- Replacing QA engineers with AI
- Writing production code (that's the developer track)
- Pretending AI is perfect for everything

## Differences from Developer Track

| Week | Developer Track | QA Track |
| ---- | --------------- | -------- |
| Week 1 | Build CLI features | Analyze testability |
| Week 2 | Code generation prompts | Test case generation prompts |
| Week 3 | Implementation planning | Test planning, defect analysis |
| Week 4 | TDD (write tests + code) | Write tests for existing code |
| Week 5 | Create developer skills | Use skills (consumer focus) |
| Week 6 | Advanced hooks | Test automation hooks |
| Week 7 | Plugin development | Skip/skim |
| Week 8 | Headless automation | Test automation & coverage |
| Capstone | Code project | Test suite or automation |

## FAQ

**Q: Do I need to know how to code?**
A: Basic familiarity with code helps but isn't required. The track focuses on testing workflows, not production development. You'll read code more than write it.

**Q: Can I do the developer track instead?**
A: If you write code regularly, yes. But the QA track is specifically designed for testing workflows.

**Q: What if I do both manual and automated testing?**
A: Great! The track covers both. Focus on whichever is more relevant to your daily work.

**Q: Will AI replace QA engineers?**
A: No. AI can generate test cases, but it can't decide what's important to test. It can find patterns in bugs, but it can't understand user impact. QA judgment remains essential.

**Q: What's the difference between Week 4 for QA vs Developers?**
A: Developers write tests AND code using TDD. QA focuses on writing tests for code that already exists - which is exactly what QA does in real life.

## Getting Help

See [Getting Help](../../../resources/getting-help.md) for all support channels.
