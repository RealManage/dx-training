# Support Skills - Custom Commands for Support

## Context

This environment is for building **custom slash commands** (skills) that automate common support tasks. Skills save time on repetitive work.

## What Are Skills?

Skills are reusable commands you create in `.claude/skills/`. When you type `/skill-name`, Claude executes the skill's instructions.

## Support Skill Ideas

| Skill Name | Purpose | Arguments |
| ---------- | ------- | --------- |
| `/draft-response` | Generate customer response | issue-type, description |
| `/explain-fee` | Explain fee calculation | fee-type, amount |
| `/escalation-note` | Create escalation document | ticket-id, summary |
| `/kb-article` | Generate knowledge base article | topic |
| `/tone-check` | Review response tone | (reads clipboard/input) |

## Skill File Structure

Skills live in `.claude/skills/` as markdown files:

```
.claude/
  skills/
    draft-response.md
    explain-fee.md
    escalation-note.md
```

Each file contains:

1. Skill description (what it does)
2. Arguments it accepts
3. Instructions for Claude
4. Output format

## Example Skill Files

See starter skills in this folder:

- `draft-response.md` - Response generation skill
- `explain-fee.md` - Fee explanation skill

## Building Your Own Skills

**Good candidates for skills:**

- Tasks you do 5+ times per day
- Responses that follow a consistent format
- Explanations that require accurate details
- Documents with standard structure

**How to create:**

1. Identify the repetitive task
2. Define the inputs (arguments)
3. Write clear instructions
4. Specify the output format
5. Test and refine

## Practice Tasks

1. Review the starter skills in this folder
2. Test `/draft-response` with different scenarios
3. Create your own skill for a common task
4. Document your skill library
