# Week 5 Solutions: Commands & Skills Reference

This document provides reference implementations and expected outputs for Week 5 exercises.

---

## Exercise 1: Board Meeting Summary Command

### Expected File: `.claude/commands/board-summary.md`

```markdown
---
description: Generate board meeting summary for violations
argument-hint: <meeting_date>
---

Generate a board meeting summary for $ARGUMENTS.

Include:
1. Total open violations by status
2. New violations since last meeting
3. Resolved violations this period
4. Total fines assessed
5. Collection rate percentage
6. Recommended actions for board

Format as markdown table suitable for board packet.
```

### How to Test

```bash
cd sessions/week-5/sandbox/violation-audit-api
claude
> /board-summary 2026-01-15
```

### Expected Output Structure

The output should include:

```markdown
## Board Meeting Summary - January 15, 2026

### Violation Status Overview
| Status | Count |
| ------ | ----- |
| Warning | X |
| First Notice | X |
| Second Notice | X |
| Board Review | X |

### New Violations (Since Last Meeting)
- [List of new violations with property IDs and types]

### Resolved Violations
- [List of resolved violations with resolution dates]

### Financial Summary
| Metric | Amount |
| ------ | ------ |
| Total Fines Assessed | $X,XXX |
| Total Collected | $X,XXX |
| Collection Rate | XX% |

### Recommended Actions
1. [Action items based on current data]
```

### Success Criteria

- [ ] Command invokes with `/board-summary`
- [ ] Accepts meeting date as argument
- [ ] Produces structured markdown output
- [ ] Includes all six sections from the prompt
- [ ] Uses `$ARGUMENTS` for the date

---

## Exercise 2: Payment Reminder Skill

### Expected Structure

```text
.claude/skills/payment-reminder/
  SKILL.md
  reminder-templates.txt
```

### SKILL.md Content

```markdown
---
name: payment-reminder
description: Generate payment reminder with customized tone
argument-hint: <property_id> <days_overdue>
---

Generate a payment reminder for property $0, $1 days overdue.

Use ./reminder-templates.txt for tone guidance.

Select appropriate tone based on days overdue:
- 1-30 days: Friendly reminder
- 31-60 days: Firm but professional
- 61-90 days: Urgent, mention consequences
- 90+ days: Final notice before legal action

Include:
1. Amount due
2. Original due date
3. Current late fees
4. Payment options
5. Contact information
```

### reminder-templates.txt Content

```text
Payment Reminder Tone Guide
===========================

FRIENDLY (1-30 days):
- "Just a friendly reminder..."
- "We noticed your payment is a few days past due..."
- Focus on convenience and multiple payment options

FIRM (31-60 days):
- "This is an important notice regarding..."
- "Your account is now 30+ days past due..."
- Mention late fees clearly, offer payment plan

URGENT (61-90 days):
- "Immediate action required..."
- "Your account is significantly past due..."
- Warn of escalation to board review

FINAL (90+ days):
- "Final Notice Before Legal Action..."
- "Your account has been referred to..."
- Clear deadline, specific consequences
```

### How to Test

```bash
claude
> /payment-reminder 12345 45
```

### Expected Output Structure

```markdown
## Payment Reminder - Property 12345

Dear Homeowner,

This is an important notice regarding your HOA account.

**Account Details:**
| Field | Value |
| ----- | ----- |
| Property ID | 12345 |
| Original Balance | $XXX.XX |
| Days Overdue | 45 |
| Late Fees Accrued | $XX.XX |
| Total Due | $XXX.XX |

**Late Fee Calculation:**
- Grace period (30 days): No fee
- Compound interest: 10% monthly on overdue balance
- Current compound periods: 1

**Payment Options:**
1. Online portal: portal.realmanage.com
2. Phone: (XXX) XXX-XXXX
3. Mail: [Address]

**Payment Plan:** Available for balances over $500.

Please contact us within 15 days to avoid further escalation.

Sincerely,
RealManage HOA Management
```

### Success Criteria

- [ ] Skill appears in `/` menu with correct description
- [ ] Supporting file loads correctly (tone matches days overdue)
- [ ] Uses `$0` for property ID and `$1` for days overdue
- [ ] Output includes all five required sections
- [ ] Tone matches the days-overdue bracket

---

## Exercise 3: Explore Existing Commands and Skills

### What to Explore

```text
# Discover available commands
> What custom commands are available in this project?

# Test the violation-report command
/violation-report 12345

# Test the late-fee-report skill
/late-fee-report 67890
```

### Exploration Checklist

- [ ] Found all commands in `.claude/commands/`
- [ ] Found all skills in `.claude/skills/`
- [ ] Read the description of each command/skill
- [ ] Successfully invoked at least one command
- [ ] Successfully invoked at least one skill
- [ ] Noticed the difference in output quality (skill uses supporting files)

---

## Common Mistakes to Avoid

1. **Forgetting YAML frontmatter delimiters**

   ```markdown
   # Wrong - missing ---
   name: my-skill

   # Correct
   ---
   name: my-skill
   ---
   ```

2. **Wrong directory structure**

   ```text
   # Wrong
   .claude/skills/my-skill.md

   # Correct
   .claude/skills/my-skill/SKILL.md
   ```

3. **Not using $ARGUMENTS for inputs**

   ```markdown
   # Wrong
   Generate report for {property}

   # Correct
   Generate report for $ARGUMENTS
   ```

4. **Forgetting supporting file references**

   ```markdown
   # Wrong - Claude won't know about the file
   Use the fee schedule for calculations.

   # Correct - explicit path reference
   Use ./fee-schedule.txt for the fee structure.
   ```

---

## Command vs Skill Decision Guide

| Scenario | Use | Why |
| -------- | --- | --- |
| Simple prompt with arguments | Command | No extra files needed |
| Need a template or data file | Skill | Supporting files add context |
| Quick calculation or lookup | Command | Single-file simplicity |
| Multi-step workflow with resources | Skill | Directory structure organizes resources |
| One-liner automation | Command | Minimal setup |
| Reusable business process | Skill | Templates ensure consistency |
| Should only run when explicitly called | Skill | Use `disable-model-invocation: true` |

---

## Self-Check Questions

Before moving to Week 6, verify:

1. Can you create a command from scratch in `.claude/commands/`?
2. Can you create a skill with supporting files in `.claude/skills/`?
3. Do you understand when to use a command vs a skill?
4. Can you invoke both with the `/` prefix?
5. Can you use `$ARGUMENTS`, `$0`, and `$1` for arguments?

---

*Solutions verified: February 2026*
