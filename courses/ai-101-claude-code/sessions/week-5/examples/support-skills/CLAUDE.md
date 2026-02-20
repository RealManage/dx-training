# Support Skills - Custom Commands & Skills for Support

## Context

This environment is for building **custom commands and skills** that automate common support tasks. Commands and skills save time on repetitive work.

## Commands vs Skills

- **Commands** live in `.claude/commands/` as single markdown files -- great for simple prompts
- **Skills** live in `.claude/skills/<name>/SKILL.md` with supporting files -- great for workflows that need templates or data

## Support Skill Ideas

| Name | Type | Purpose | Arguments |
| ---- | ---- | ------- | --------- |
| `/faq` | Command | Quick FAQ answer | topic |
| `/draft-response` | Skill | Generate customer response | issue-type, description |
| `/explain-fee` | Skill | Explain fee calculation | fee-type, amount |
| `/escalation-note` | Skill | Create escalation document | ticket-id, summary |
| `/tone-check` | Command | Review response tone | (reads input) |

## Skill File Structure

Commands use a single file; skills use a directory:

```text
.claude/
  commands/
    faq.md                    # Simple command
  skills/
    draft-response/
      SKILL.md                # Main skill definition
      tone-guide.txt          # Supporting file
    explain-fee/
      SKILL.md
      fee-schedule.txt
```

## Starter Templates

See starter skill templates in this folder:

- `draft-response.md` - Response generation template (copy to `.claude/skills/draft-response/SKILL.md`)
- `explain-fee.md` - Fee explanation template (copy to `.claude/skills/explain-fee/SKILL.md`)

## Building Your Own

**Good candidates for automation:**

- Tasks you do 5+ times per day
- Responses that follow a consistent format
- Explanations that require accurate details
- Documents with standard structure

**How to create:**

1. Identify the repetitive task
2. Decide: command (simple) or skill (needs supporting files)?
3. Define the inputs (arguments)
4. Write clear instructions
5. Specify the output format
6. Test and refine

## Practice Tasks

1. Review the starter templates in this folder
2. Create a `/faq` command in `.claude/commands/`
3. Create a `/draft-response` skill in `.claude/skills/draft-response/SKILL.md`
4. Test both and compare the output quality
