# Week 5: Commands & Basic Skills - QA Track

**Consumer-focused track: How to USE commands and skills, not build them.**

As a QA engineer, you don't need to create commands and skills from scratch - developers will build those. Your focus is on **using** them effectively and **providing feedback** to improve them.

---

## Learning Objectives (QA-Specific)

By the end of this session, you will be able to:

- Discover available commands and skills in a project
- Invoke commands and skills correctly
- Understand what commands/skills do before running them
- Provide useful feedback to developers on command/skill quality

---

## Part 1: Discovering Commands and Skills (15 min)

### Finding What's Available

**Method 1: Ask Claude**

```
> What custom commands are available in this project?
> What skills are defined in .claude/skills?
```

**Method 2: Browse the File System**

```
> List files in .claude/commands/
> List directories in .claude/skills/
```

**Method 3: Use the Slash Menu**

- Type `/` in Claude Code
- Scroll through available commands
- Custom commands appear alongside built-ins

### Understanding What a Command Does

Before running a command, read its description:

```
> Show me the contents of .claude/commands/violation-report.md
```

Look for:

- **description** - What the command does
- **argument-hint** - What arguments it expects
- **The prompt body** - What Claude will actually do

---

## Part 2: Using Commands Effectively (20 min)

### Command Syntax

```bash
/command-name argument1 argument2
```

**Examples:**

```bash
# Generate a violation report for property 12345
/violation-report 12345

# Calculate late fees: $500 principal, 3 months overdue
/late-fee 500 3

# Run code review (no arguments)
/code-review
```

### Testing Commands for Quality

As QA, evaluate commands on:

| Criteria | Questions to Ask |
| -------- | ---------------- |
| **Accuracy** | Does it produce correct output? |
| **Completeness** | Are all expected elements present? |
| **Consistency** | Same input = same quality output? |
| **Edge Cases** | What happens with unusual inputs? |
| **Error Handling** | Does it fail gracefully with bad inputs? |

### QA Test Scenarios for Commands

**Test 1: Normal Case**

```
/violation-report 12345
Expected: Complete report with all 5 sections
```

**Test 2: Edge Case - New Property**

```
/violation-report 99999
Expected: "No violations found" or appropriate message
```

**Test 3: Invalid Input**

```
/violation-report abc
Expected: Clear error or reasonable handling
```

---

## Part 3: Using Skills (15 min)

Skills work like commands but have more context (supporting files).

### Invoking Skills

```bash
/skill-name arguments
```

**Example:**

```bash
/late-fee-report 12345
```

The skill has access to supporting files like `fee-schedule.txt` that provide additional context for better output.

### Skills vs Commands (QA Perspective)

| Aspect | Commands | Skills |
| ------ | -------- | ------ |
| Output quality | Basic | Enhanced (has templates/data) |
| Consistency | Varies | More consistent (uses templates) |
| Testing focus | Input/output | Input/output + template accuracy |

---

## Part 4: Providing Feedback (10 min)

### Effective Feedback Format

When a command or skill doesn't work well, report:

```markdown
## Command/Skill: /violation-report
## Input: 12345
## Expected: Complete violation report with fine calculations
## Actual: Missing fine calculation section
## Severity: Medium
## Suggested Fix: Add compound interest calculation to prompt
```

### What to Look For

- **Missing information** - Required fields not generated
- **Incorrect calculations** - Wrong numbers (especially late fees)
- **Inconsistent formatting** - Different runs produce different formats
- **Missing edge case handling** - Unusual inputs cause confusion
- **Unclear language** - Generated text is confusing

### Sharing Feedback

Post in `#ai-exchange` Slack:

- Command/skill name
- Your test case
- What went wrong
- Suggested improvement

---

## Hands-On Exercises (QA Track)

### Exercise 1: Discover Commands (5 min)

```
> What custom commands and skills are available in this project?
> Show me the description of each one
```

### Exercise 2: Test a Command (10 min)

Test `/violation-report` with these cases:

| Test Case | Input | Check For |
| --------- | ----- | --------- |
| Normal | `12345` | All 5 sections present |
| Edge | `00000` | Handles gracefully |
| Invalid | `abc` | Clear error message |

### Exercise 3: Evaluate Output Quality (10 min)

Run `/late-fee-report 12345` and evaluate:

- [ ] Correct compound interest calculation
- [ ] Proper escalation level shown
- [ ] Payment plan options included
- [ ] Professional formatting

Document any issues found.

---

## Key Takeaways (QA Track)

1. **You don't need to create** - Focus on using and testing
2. **Ask Claude** to discover available commands/skills
3. **Read the description** before running commands
4. **Test edge cases** - That's your superpower
5. **Provide feedback** to improve command quality

---

## Homework (QA Track)

1. **Test 3 commands** from your current project
2. **Document 1 edge case** that needs better handling
3. **Suggest 1 improvement** in `#ai-exchange`

---

*Return to [main README](../README.md)*
