# Week 5: Commands & Basic Skills

**Duration:** 2 hours
**Format:** In-person or virtual
**Audience:** RealManage cross-functional team (engineers, PMs, support staff)

## Learning Tracks

This week has different tracks based on your role:

- **[Developer Track](./tracks/developer.md)** - Full content (default)
- **[QA Track](./tracks/qa.md)** - Consumer-focused: how to USE commands/skills
- **[PM Track](./tracks/pm.md)** - Optional week for PMs
- **[Support Track](./tracks/support.md)** - Create /draft-response and other support skills

---

## Learning Objectives

By the end of this session, participants will be able to:

- Create custom slash commands in `.claude/commands/`
- Build basic skills in `.claude/skills/` with supporting files
- Understand when to use commands vs skills
- Invoke commands and skills in Claude Code

---

## Pre-Session Checklist

### For Participants

- [ ] Claude Code working smoothly from Weeks 1-4
- [ ] Understanding of TDD from Week 4
- [ ] Reviewed [Slash Commands Docs](https://code.claude.com/docs/en/slash-commands)
- [ ] Ready for 1.5-hour session

---

## Part 1: Slash Commands (30 min)

Slash commands are reusable prompts you can invoke with `/command-name`. They're the simplest way to create custom automation in Claude Code.

### 1.1 What Are Slash Commands?

**The Basics:**

- Predefined prompts triggered by `/name`
- Can accept arguments (e.g., `/violation-report 12345`)
- Live in `.claude/commands/` directory

**Built-in vs Custom Commands:**

| Type | Examples | Source |
| ---- | -------- | ------ |
| Built-in | `/help`, `/clear`, `/compact`, `/model` | Claude Code |
| Custom | `/violation-report`, `/new-service` | Your `.claude/commands/` |

**File Structure:**

```
.claude/
  commands/
    violation-report.md    # /violation-report
    new-service.md         # /new-service
    code-review.md         # /code-review
    late-fee.md            # /late-fee
```

### 1.2 Creating Custom Commands

**Command File Format (YAML Frontmatter):**

> **Note:** YAML frontmatter is the metadata block between `---` delimiters at the top of a file. It uses simple `key: value` syntax. Don't worry about memorizing YAML - you can ask Claude to create these files for you! For details, see the [Claude Code Skill docs](https://code.claude.com/docs/en/skills).

```markdown
---
description: Generate HOA violation report for a property
argument-hint: <property_id>
---

Generate a comprehensive violation report for property $ARGUMENTS.

Include:
1. All open violations with dates
2. Escalation status (30/60/90 days)
3. Fine calculations with compound interest (10% monthly)
4. Recommended next actions
5. Communication history

Format as markdown suitable for board review.
```

**Frontmatter Fields:**

| Field | Required | Description |
| ----- | -------- | ----------- |
| `description` | Recommended | What the command does (shown in `/` menu) |
| `argument-hint` | No | Hint for autocomplete (e.g., `<property_id>`) |

**String Substitutions:**

| Variable | Description | Example |
| -------- | ----------- | ------- |
| `$ARGUMENTS` | All arguments as single string | `/cmd foo bar` -> `"foo bar"` |
| `$1`, `$2`, `$3` | Individual arguments | `/cmd foo bar` -> `$1="foo"`, `$2="bar"` |

**Using Commands:**

```bash
# Single argument
/violation-report 12345

# Multiple arguments
/new-service PaymentProcessor Payment

# No arguments
/code-review
```

### 1.3 RealManage Command Examples

**Example 1: New Service with TDD**

```markdown
---
description: Create new C# service with TDD approach
argument-hint: <ServiceName> <EntityName>
---

Create a new service called $1Service for managing $2 entities.

Requirements:
- Follow repository pattern
- Include interface I$1Service
- Write xUnit tests FIRST (Red-Green-Refactor)
- Target 80-90% code coverage
- Use FluentAssertions and Moq

Steps:
1. Create failing tests in Tests/$1ServiceTests.cs
2. Create interface in Services/I$1Service.cs
3. Implement Services/$1Service.cs to pass tests
4. Refactor while keeping tests green
```

**Example 2: Late Fee Calculator**

```markdown
---
description: Calculate late fees with compound interest
argument-hint: <principal> <months_overdue>
---

Calculate late fees for $1 principal over $2 months.

RealManage Rules:
- 30-day grace period (first month free)
- 10% monthly compound interest after grace period
- Formula: principal * (1.10 ^ (months - 1)) - principal
- Cap at 3x original amount per state regulations

Output:
1. Original amount
2. Months overdue
3. Interest calculation breakdown
4. Total amount due
5. Payment plan options (3, 6, 12 months)
```

**Example 3: Violation Escalation**

```markdown
---
description: Process HOA violation with proper escalation
argument-hint: <violation_type> <property_id> <days_since_report>
---

Process a $1 violation for property $2, reported $3 days ago.

Business Rules:
- 0-30 days: First notice (warning only)
- 31-60 days: Second notice with $50 fine
- 61-90 days: Third notice with $100 fine + 10% monthly compound
- 90+ days: Board review required, legal action possible

Generate:
1. Appropriate notice letter
2. Fine calculation if applicable
3. Audit trail entry
4. Next action date
5. Escalation path if unresolved
```

---

## Part 2: Skills - Commands with Superpowers (30 min)

Skills are enhanced commands that can include supporting files. While commands are single markdown files with prompts, skills support directories with templates, data files, and scripts.

### 2.1 Skills vs Commands

**What's the Difference?**

| Feature | Commands | Skills |
| ------- | -------- | ------ |
| File location | `.claude/commands/<name>.md` | `.claude/skills/<name>/SKILL.md` |
| Supporting files | No | Yes (templates, scripts, data) |
| Complexity | Simple prompts | Workflows with resources |
| Use case | Quick actions | Multi-step processes |

**When to Use Which:**

- **Commands** - Simple prompts, quick actions, no extra files needed
- **Skills** - Need supporting files (templates, data, scripts)

> **Need help deciding?** See the [Decision Trees](../../resources/decision-trees.md#2-command-vs-skill-vs-agent-vs-plugin) for a visual guide on when to use commands vs skills.

**File Structure:**

```
.claude/
  commands/
    late-fee.md              # Simple command
  skills/
    violation-workflow/      # Skill directory
      SKILL.md               # Main skill file (required)
      notice-template.txt    # Supporting template
      escalation-rules.json  # Supporting data
```

### 2.2 Creating Skills

**Skill File Format (YAML Frontmatter):**

```markdown
---
name: violation-workflow
description: Process HOA violations with notice templates
argument-hint: <property_id> <violation_type>
---

Process a $1 violation for property $2.

## Business Rules
- 0-30 days: First notice (warning)
- 31-60 days: Second notice ($50 fine)
- 61-90 days: Third notice ($100 + 10% monthly compound)
- 90+ days: Board review required

## Workflow Steps
1. Look up property in database
2. Determine current escalation level
3. Generate notice letter using ./notice-template.txt
4. Calculate fines if applicable
5. Update violation status

## Templates Available
- notice-template.txt - Standard notice letter format
```

**Key Frontmatter Fields:**

| Field | Description |
| ----- | ----------- |
| `name` | Unique identifier for the skill |
| `description` | What the skill does (shown in menu) |
| `argument-hint` | Arguments hint for autocomplete |

### 2.3 Supporting Files

Skills can include supporting files alongside SKILL.md:

**Example: Late Fee Report Skill**

```
.claude/skills/late-fee-report/
  SKILL.md                    # Main skill definition
  fee-schedule.txt            # Fee structure reference
  payment-plan-template.txt   # Payment plan format
```

**SKILL.md:**

```markdown
---
name: late-fee-report
description: Generate late fee report with compound interest calculations
argument-hint: <property_id>
---

Generate a late fee report for property $ARGUMENTS.

Use ./fee-schedule.txt for the fee structure.

Include:
1. Original balance
2. Months overdue
3. Interest calculation (10% monthly compound)
4. Total amount due
5. Payment plan options
```

**Supporting File: fee-schedule.txt**

```
RealManage Late Fee Schedule
============================
Grace Period: 30 days
Interest Rate: 10% monthly (compound)
Maximum Cap: 3x original amount

Escalation:
- 0-30 days: No fee
- 31-60 days: 10% of principal
- 61-90 days: 21% of principal (compound)
- 91+ days: Continues compounding, legal review
```

### 2.4 RealManage Skill Examples

**Example 1: Board Agenda Generator**

```markdown
---
name: board-agenda
description: Generate board meeting agenda with violation summaries
argument-hint: <meeting_date>
---

Generate a board agenda for the meeting on $ARGUMENTS.

Use ./agenda-template.md for the format.

Include:
1. Call to order
2. Roll call and quorum verification (need 3+ members)
3. Old business from previous meeting
4. Violation reports (90+ day escalations)
5. Financial summary
6. New business
7. Adjournment
```

**Example 2: Homeowner Communication Kit**

```markdown
---
name: homeowner-comm
description: Generate homeowner communications with appropriate tone
argument-hint: <communication_type> <property_id>
---

Generate a $1 communication for property $2.

Use ./letter-templates/ for the appropriate template.
Use ./tone-guide.txt for communication standards.

Communication types:
- welcome: New homeowner welcome packet
- reminder: Friendly payment reminder
- notice: Formal violation notice
- escalation: Final warning before legal action

Match tone to communication type per RealManage standards.
```

---

## Part 3: Hands-On Exercises (30 min)

### 3.1 Setup Your Sandbox

```bash
# Copy example project to sandbox
cd courses/ai-101-claude-code/sessions/week-5
cp -r examples sandbox
cd sandbox/violation-audit-api

# Build and verify
dotnet build
dotnet test

# Start Claude
claude
```

### 3.2 Exercise 1: Create a Custom Command (10 min)

Create `.claude/commands/board-summary.md`:

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

**Test it:**

```
/board-summary 2026-01-15
```

### 3.3 Exercise 2: Create a Skill with Supporting Files (15 min)

**Step 1:** Create the skill directory:

```bash
mkdir -p .claude/skills/payment-reminder
```

**Step 2:** Create `.claude/skills/payment-reminder/SKILL.md`:

```markdown
---
name: payment-reminder
description: Generate payment reminder with customized tone
argument-hint: <property_id> <days_overdue>
---

Generate a payment reminder for property $1, $2 days overdue.

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

**Step 3:** Create `.claude/skills/payment-reminder/reminder-templates.txt`:

```
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

**Test it:**

```
/payment-reminder 12345 45
```

### 3.4 Exercise 3: Explore Existing Commands and Skills (5 min)

Explore the example project's existing commands and skills:

```
# List available commands
> What custom commands are available in this project?

# Test the violation-report command
/violation-report 12345

# Test the late-fee-report skill
/late-fee-report 67890
```

---

## 🎯 Key Takeaways

### Commands Quick Reference

```
LOCATION: .claude/commands/<name>.md

FORMAT:
---
description: What it does
argument-hint: <args>
---
Prompt content with $ARGUMENTS or $1, $2, $3

INVOKE: /command-name args
```

### Skills Quick Reference

```
LOCATION: .claude/skills/<name>/SKILL.md

FORMAT:
---
name: skill-name
description: What it does
argument-hint: <args>
---
Prompt with access to supporting files via ./filename

SUPPORTING FILES:
- Same directory as SKILL.md
- Templates, data, scripts, etc.

INVOKE: /skill-name args
```

### Decision Guide: Command vs Skill?

| Scenario | Use |
| -------- | --- |
| Simple prompt with arguments | Command |
| Need a template file | Skill |
| Quick calculation or lookup | Command |
| Multi-step workflow with resources | Skill |
| One-liner automation | Command |
| Reusable business process | Skill |

---

## Homework (Before Week 6)

### Required Tasks

1. Create 2 custom commands for your daily workflow
2. Build 1 skill with at least one supporting file
3. Share your best command/skill in `#ai-exchange` Slack

### Stretch Goals

1. Create a skill with multiple supporting files
2. Build a command that uses all three argument types ($ARGUMENTS, $1, $2)

---

## Resources

### Official Documentation

- [Slash Commands](https://code.claude.com/docs/en/slash-commands)
- [Skills](https://code.claude.com/docs/en/skills)

### RealManage Resources

- [Week 5 Examples](./examples/) - Violation Audit API project
- Slack: `#ai-exchange`

---

## Next Week Preview

**Week 6: Agents & Hooks**

Building on commands and skills, Week 6 introduces:

- Custom subagents (specialized AI assistants)
- Lifecycle hooks (automated actions before/after tool use)
- Audit logging patterns for SOC 2 compliance
- Combining everything into complete workflows

---

## 📚 Quick Resources

- [Glossary](../../resources/glossary.md) - Key terms and definitions
- [Decision Trees](../../resources/decision-trees.md) - When to use what
- [Troubleshooting](../../resources/troubleshooting.md) - Common issues and fixes
- [CLI Commands](../../resources/cli-commands.md) - Command cheatsheet

---

*End of Week 5 Session Plan*
*Next Session: Week 6 - Agents & Hooks*

---

**Next:** [Week 6: Agents & Hooks](../week-6/README.md)
