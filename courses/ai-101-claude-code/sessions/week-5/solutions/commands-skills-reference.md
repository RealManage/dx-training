# Week 5 Solutions: Commands & Skills Reference

This document provides reference implementations and expected outputs for Week 5 exercises.

---

## Exercise 1: Create a Violation Report Command

### Expected File: `.claude/commands/violation-report.md`

```markdown
---
description: Generate a violation report for a property
---

Generate a violation report for property $ARGUMENTS including:

1. **Property Overview**
   - Address and owner info
   - Account status

2. **Active Violations**
   - Type, date reported, current status
   - Days since initial notice
   - Escalation level (Notice/Warning/Fine/Legal)

3. **Violation History**
   - Total violations past 12 months
   - Resolution rate
   - Average time to resolution

4. **Recommended Actions**
   - Based on current escalation levels
   - Suggested follow-up dates

Format as markdown with clear sections.
```

### How to Test
```bash
cd sessions/week-5/examples/violation-audit-api
claude
> /violation-report HOA-12345
```

### Success Criteria
- [ ] Command invokes with `/violation-report`
- [ ] Accepts property ID as argument
- [ ] Produces structured markdown output
- [ ] Includes all four sections

---

## Exercise 2: Create a Late Fee Calculator Skill

### Expected Structure
```
.claude/skills/late-fee-report/
├── SKILL.md
└── fee-schedule.txt
```

### SKILL.md Content
```markdown
---
name: late-fee-report
description: Calculate late fees with compound interest for a property
argument-hint: [property_id]
---

Calculate late fees for property $ARGUMENTS using the fee schedule below.

## Fee Schedule
!`cat .claude/skills/late-fee-report/fee-schedule.txt`

## Calculation Requirements
1. Apply 30-day grace period (no fee if ≤30 days)
2. Calculate 10% monthly compound interest after grace period
3. Cap maximum fee at 3x original balance (state law)
4. Show work with step-by-step calculation

## Output Format
| Field | Value |
|-------|-------|
| Property ID | {id} |
| Original Balance | ${amount} |
| Days Overdue | {days} |
| Compound Periods | {months} |
| Calculated Fee | ${fee} |
| Cap Applied | Yes/No |
| Final Fee | ${final} |
```

### fee-schedule.txt Content
```
RealManage Late Fee Schedule
Effective: January 2025

Grace Period: 30 days from due date
Interest Rate: 10% per month (compound)
Maximum Cap: 3x original balance

Escalation Timeline:
- Day 1-30: No fee (grace period)
- Day 31-60: Interest begins accruing
- Day 61-90: Second notice sent
- Day 91+: Legal referral consideration

Compound Interest Formula:
Fee = Principal × ((1 + 0.10)^months - 1)

Example:
$500 balance, 60 days late (2 compound periods)
Fee = $500 × ((1.10)^2 - 1) = $500 × 0.21 = $105
```

### Success Criteria
- [ ] Skill appears in Claude's auto-discovery
- [ ] Supporting file loads correctly
- [ ] Calculation matches HOA business rules
- [ ] Output is clear and auditable

---

## Exercise 3: Convert Command to Skill

### Before (Command)
```markdown
# .claude/commands/code-review.md
Review the code in $ARGUMENTS for issues.
```

### After (Skill with Supporting Files)
```
.claude/skills/code-review/
├── SKILL.md
├── checklist.md
└── severity-guide.md
```

### Key Differences Demonstrated
| Aspect | Command | Skill |
|--------|---------|-------|
| Auto-discovery | No | Yes |
| Supporting files | No | Yes |
| Reusable templates | No | Yes |

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
   ```
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

---

## Self-Check Questions

Before moving to Week 6, verify:

1. Can you create a command from scratch?
2. Can you create a skill with supporting files?
3. Do you understand when to use command vs skill?
4. Can you invoke both with `/` prefix?

---

*Solutions verified: January 2026*
