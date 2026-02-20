# Week 5: Commands & Basic Skills - QA Track

**Duration:** 2 hours
**Audience:** QA Engineers, SDETs
**Prerequisites:** Complete Weeks 1-4

---

## Learning Objectives (QA-Specific)

By the end of this session, you will be able to:

- Create custom commands for test planning and bug reporting workflows
- Build skills with supporting files for regression checklists and test data
- Test and validate commands/skills created by other team members
- Provide structured feedback to improve command/skill quality

---

## Part 1: Commands for QA Workflows (30 min)

### Why QA Engineers Should Build Commands

QA engineers bring unique value to commands and skills:

| QA Skill | What It Automates | Time Saved |
| -------- | ----------------- | ---------- |
| `/test-plan` | Generate test plan from requirements | 20 min/feature |
| `/bug-report` | Create structured bug report | 10 min/bug |
| `/regression-check` | Generate regression checklist | 15 min/release |
| `/test-data` | Generate test data for scenarios | 10 min/scenario |

### 1.1 Create a Test Plan Command

Create `.claude/commands/test-plan.md`:

```markdown
---
description: Generate a test plan for an HOA feature
argument-hint: <feature_name>
---

Generate a comprehensive test plan for the $ARGUMENTS feature.

Include:
1. **Scope** - What is being tested
2. **Test Cases** - Numbered list with:
   - Preconditions
   - Steps
   - Expected result
   - Priority (P1/P2/P3)
3. **Edge Cases** - Boundary conditions and error paths
4. **HOA Business Rules** to verify:
   - 30/60/90 day escalation timelines
   - 10% monthly compound interest calculations
   - 30-day grace period handling
   - Fine cap at 3x original amount
5. **Test Data** - Sample data needed
6. **Regression Impact** - Areas that could be affected

Format as markdown with checkboxes for each test case.
```

**Test it:**

```text
/test-plan violation-escalation
```

### 1.2 Create a Bug Report Command

Create `.claude/commands/bug-report.md`:

```markdown
---
description: Generate structured bug report from description
argument-hint: <brief_description>
---

Create a structured bug report for: $ARGUMENTS

Format:
## Bug Report

**Title:** [Clear, specific title]
**Severity:** [Critical/High/Medium/Low]
**Component:** [Which part of the system]
**Environment:** [Dev/Staging/Production]

### Steps to Reproduce
1. [Specific steps]
2. [With exact data]
3. [That reproduce consistently]

### Expected Behavior
[What should happen]

### Actual Behavior
[What actually happens]

### Evidence
[Screenshots, logs, or data to capture]

### Impact
- Users affected: [scope]
- Workaround available: [Yes/No]
- Related violations/fees affected: [if applicable]

### HOA Context
[Any relevant business rules: escalation timelines, fee calculations, grace periods]
```

**Test it:**

```text
/bug-report "Late fee calculated as simple interest instead of compound"
```

---

## Part 2: Skills for QA Workflows (30 min)

### 2.1 Skills Add Supporting Files

Skills let you include checklists, templates, and test data alongside your prompt. This is the key upgrade over simple commands.

**Skill Structure:**

```text
.claude/skills/
  regression-check/
    SKILL.md                  # Main skill definition
    regression-checklist.md   # Standard checklist template
    critical-paths.md         # High-priority test paths
```

### 2.2 Create a Regression Checklist Skill

**Step 1:** Create the skill directory:

```bash
mkdir -p .claude/skills/regression-check
```

**Step 2:** Create `.claude/skills/regression-check/SKILL.md`:

```markdown
---
name: regression-check
description: Generate regression test checklist for a release area
argument-hint: <release_area>
---

Generate a regression test checklist for the $ARGUMENTS area.

Use ./regression-checklist.md for the standard checklist template.
Use ./critical-paths.md for high-priority test paths.

Include:
1. All critical paths from the template
2. Area-specific test cases
3. HOA business rule validations
4. Integration points to verify
5. Performance benchmarks to check

Format with checkboxes and priority tags [P1], [P2], [P3].
```

**Step 3:** Create `.claude/skills/regression-check/regression-checklist.md`:

```markdown
# Standard Regression Checklist

## Authentication & Authorization
- [ ] [P1] Login/logout flow
- [ ] [P1] Role-based access (board, manager, resident)
- [ ] [P2] Session timeout handling

## Violation Management
- [ ] [P1] Create new violation
- [ ] [P1] Escalation at 30/60/90 day boundaries
- [ ] [P1] Fine calculation (compound interest)
- [ ] [P2] Violation resolution flow
- [ ] [P2] Audit trail for all actions
- [ ] [P3] Bulk violation operations

## Financial
- [ ] [P1] Late fee calculation (10% monthly compound)
- [ ] [P1] Grace period (30 days) applied correctly
- [ ] [P1] Fine cap (3x original) enforced
- [ ] [P2] Payment plan generation
- [ ] [P2] Payment processing flow
- [ ] [P3] Financial report accuracy

## Notifications
- [ ] [P1] Violation notice delivery
- [ ] [P2] Payment reminder scheduling
- [ ] [P3] Board meeting notifications
```

**Step 4:** Create `.claude/skills/regression-check/critical-paths.md`:

```markdown
# Critical Test Paths

These paths MUST pass before any release:

1. Violation lifecycle: Create -> Escalate -> Fine -> Resolve
2. Payment flow: Invoice -> Payment -> Receipt -> Ledger update
3. Late fee: Grace period -> Compound calculation -> Cap enforcement
4. Board review: Escalation trigger -> Agenda item -> Resolution
5. Audit trail: Every action logged with timestamp, user, details
```

**Test it:**

```text
/regression-check payment-processing
```

---

## Part 3: Testing Other People's Commands and Skills (30 min)

### 3.1 Setup Your Sandbox

```bash
cd courses/ai-101-claude-code/sessions/week-5
cp -r examples sandbox
cd sandbox/violation-audit-api

# Build and verify
dotnet build
dotnet test

# Start Claude
claude
```

### 3.2 Discover Available Commands and Skills

```text
> What custom commands and skills are available in this project?
> Show me the description of each one
```

### 3.3 Test Commands for Quality

As QA, evaluate commands and skills on:

| Criteria | Questions to Ask |
| -------- | ---------------- |
| **Accuracy** | Does it produce correct output? |
| **Completeness** | Are all expected elements present? |
| **Consistency** | Same input = similar quality output? |
| **Edge Cases** | What happens with unusual inputs? |
| **Clarity** | Is the output clear and professional? |

> **Note:** Commands and skills are prompts to an LLM -- output will vary between runs. Focus on whether the *structure* and *key content* are consistently present, not exact word-for-word matches.

### 3.4 QA Test Scenarios

**Test 1: Normal Case**

```text
/violation-report 12345
```

Check for:
- [ ] All expected sections present
- [ ] Escalation levels referenced correctly
- [ ] Fine calculations use compound interest
- [ ] Professional formatting

**Test 2: Edge Case**

```text
/violation-report 99999
```

Check for:
- [ ] Handles unknown property gracefully
- [ ] Does not hallucinate fake data without noting it

**Test 3: Late Fee Skill**

```text
/late-fee-report 12345
```

Check for:
- [ ] Compound interest formula applied (not simple interest)
- [ ] 30-day grace period mentioned
- [ ] 3x cap referenced
- [ ] Payment plan options included

### 3.5 Providing Structured Feedback

When a command or skill needs improvement, use this format:

```markdown
## Command/Skill: /violation-report
## Input: 12345
## Expected: Complete violation report with fine calculations
## Actual: Missing fine calculation section
## Severity: Medium
## Suggested Fix: Add compound interest calculation to prompt
## Consistency: Reproduced in 3/3 runs
```

Post feedback in `#ai-exchange` Slack.

---

## Part 4: Wrap-Up and Review (30 min)

### Discussion

1. Which QA workflow would benefit most from a command or skill?
2. How do you test something with non-deterministic output?
3. What quality criteria matter most for commands your team creates?

### Share Your Work

Post your `/test-plan` or `/regression-check` skill to `#ai-exchange`:

- What it does
- Sample output
- What you'd improve

---

## Key Takeaways (QA Track)

1. **QA engineers should create commands and skills** -- your testing expertise makes excellent automation
2. **Commands** for simple prompts (bug reports, test plans)
3. **Skills** when you need supporting files (checklists, templates, test data)
4. **Test other people's commands** -- evaluate accuracy, consistency, and edge cases
5. **Provide structured feedback** to improve command quality across the team

---

## Homework (QA Track)

1. **Create 2 custom commands** for your daily QA workflow (e.g., `/test-plan`, `/bug-report`)
2. **Build 1 skill** with a supporting file (e.g., `/regression-check` with a checklist template)
3. **Test 2 commands** from your current project and document findings
4. **Share your best command/skill** in `#ai-exchange`

---

## What's Next

**Week 6: Agents & Hooks** - Learn about:

- Custom AI agents for specialized tasks (e.g., read-only analysis agents)
- Hooks for automated workflows (e.g., auto-run tests after edits)
- Compliance and audit logging patterns

---

*Return to [main README](../README.md#key-takeaways)*
