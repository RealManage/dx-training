# Week 5: Commands & Basic Skills - Trainer Runbook

## Session Overview

**Duration:** 2 hours
**Format:** In-person or virtual
**Tracks:** Developer, QA, PM, Support

---

## Pre-Session Checklist

### For Instructors

- [ ] Test all example projects build without warnings (`dotnet build && dotnet test`)
- [ ] Verify command and skill examples work correctly in Claude Code
- [ ] Confirm `fee-schedule.txt` exists in `.claude/skills/late-fee-report/`
- [ ] Prepare backup exercises for permission issues
- [ ] Monitor `#ai-exchange` Slack channel
- [ ] Have the decision tree resource open for reference

---

## Timing Guide

| Time | Segment | What Happens |
| ---- | ------- | ------------ |
| 0:00-0:05 | Welcome & Context | Set the stage: "Last week TDD, this week automation" |
| 0:05-0:35 | Part 1: Slash Commands | Live demo + participants create their first command |
| 0:35-1:05 | Part 2: Skills | Live demo + participants create a skill with supporting files |
| 1:05-1:35 | Part 3: Hands-On Exercises | Track-specific exercises (see Track Delivery below) |
| 1:35-1:55 | Part 4: Wrap-Up & Review | Discussion, share work, homework review |
| 1:55-2:00 | Close | Preview Week 6, answer final questions |

---

## Track Delivery Strategy

### Combined Session (Recommended)

All participants attend Parts 1 and 2 together. For Part 3, participants work on their track-specific exercises:

- **Developers** follow the main README exercises (board-summary command, payment-reminder skill)
- **QA** follow `tracks/qa.md` Part 3 (test-plan command, regression-check skill, then test existing commands)
- **PMs** follow `tracks/pm.md` exercises (release-notes, meeting-actions, sprint-summary, user-stories)
- **Support** follow `tracks/support.md` exercises (draft-response skill, explain-fee skill)

### Instructor Tips for Mixed Audiences

- During Part 1 demo, use the `/violation-report` command -- all roles understand violations
- During Part 2 demo, use the `/late-fee-report` skill -- fees are universal domain knowledge
- When splitting for Part 3, briefly explain: "Find your track file and work through the exercises. Raise your hand if you get stuck."
- Pair non-developers with developers if anyone struggles with file creation
- For virtual sessions, use breakout rooms by track

---

## Live Demo Script

### Part 1 Demo: Creating a Command (10 min)

```bash
# 1. Navigate to example project
cd courses/ai-101-claude-code/sessions/week-5/examples/violation-audit-api

# 2. Show the existing command file
cat .claude/commands/violation-report.md
```

**Talk through:**

- "This is just a markdown file with YAML frontmatter"
- "The `description` shows up in the `/` menu"
- "The `argument-hint` tells users what to pass"
- "`$ARGUMENTS` gets replaced with whatever the user types after the command name"

```bash
# 3. Start Claude and demo the command
claude
> /violation-report 12345
```

**After output appears:**

- "Notice how the command structured Claude's response with all five sections"
- "This is the same prompt every time -- consistency is the value"

```bash
# 4. Show creating a new command live
> Help me create a command called /board-summary that generates a board meeting summary. Save it to .claude/commands/board-summary.md
```

**Key point:** "You can ask Claude to create commands for you -- you don't have to write the markdown by hand."

### Part 2 Demo: Creating a Skill (10 min)

```bash
# 1. Show the skill directory structure
ls -la .claude/skills/late-fee-report/
```

**Talk through:**

- "A skill is a directory with SKILL.md plus supporting files"
- "The supporting files give Claude extra context -- templates, data, checklists"

```bash
# 2. Show SKILL.md
cat .claude/skills/late-fee-report/SKILL.md
```

**Talk through:**

- "Same frontmatter as commands, plus extra fields like `context` and `allowed-tools`"
- "The `./fee-schedule.txt` reference tells Claude to load that file when the skill runs"
- "Claude only reads supporting files when the skill is invoked -- saves tokens"

```bash
# 3. Show the supporting file
cat .claude/skills/late-fee-report/fee-schedule.txt
```

```bash
# 4. Demo the skill
> /late-fee-report 67890
```

**After output appears:**

- "Notice how the output references the fee schedule data -- that's the supporting file at work"
- "This is why skills are better than commands for complex workflows"

### Expected Demo Output

**For `/violation-report 12345`:**

- A structured report with property overview, active violations, violation history, and recommended actions
- Should mention escalation levels and fine amounts
- Format varies per run but structure should be consistent

**For `/late-fee-report 67890`:**

- Report with original balance, months overdue, compound interest calculation
- Should reference the fee schedule (10% monthly, 30-day grace, 3x cap)
- Payment plan options included

---

## Instructor Notes

### Key Points to Emphasize

- **One concept at a time** -- Commands are simple, skills add supporting files
- **Start with commands** -- Don't over-engineer; use skills only when needed
- **Supporting files are key** -- That's the main difference from commands
- **Build progressively** -- Master commands first, then skills, then Week 6's agents/hooks
- **Commands still work** -- Claude Code has merged commands and skills under "skills," but `.claude/commands/` files are fully supported

### Common Questions

> **"When should I use a skill instead of a command?"**

- If you need a template file, data file, or script = Skill
- If it's just a prompt with arguments = Command
- When in doubt, start with a command and upgrade to a skill if needed

> **"Can I convert a command to a skill?"**

- Yes. Move the `.md` file to `.claude/skills/<name>/SKILL.md`
- Add supporting files to the same directory
- The `name` field in frontmatter is optional (defaults to directory name)
- If a skill and command share the same name, the skill takes precedence

> **"Where do skills go - user or project level?"**

- Project level (`.claude/skills/`) for project-specific workflows shared with the team
- User level (`~/.claude/skills/`) for personal productivity skills

> **"What's the difference between $ARGUMENTS and $0?"**

- `$ARGUMENTS` is the full argument string as-is
- `$0`, `$1`, `$2` are individual positional arguments (0-based indexing)
- Use `$ARGUMENTS` when you want the full text; use `$0`/`$1` when you need specific parts

---

## Troubleshooting

### Commands Not Recognized

- Check `.claude/commands/` directory exists
- Verify `.md` extension on the file
- Restart Claude session (exit and re-enter `claude`)
- Check YAML frontmatter syntax -- must have `---` delimiters on their own lines
- Check for YAML indentation errors (use spaces, not tabs)

### Skills Not Recognized

- Check `.claude/skills/<name>/SKILL.md` path is correct (case-sensitive `SKILL.md`)
- Verify YAML frontmatter has `description` field (needed for discovery)
- Supporting files must be in the same directory as SKILL.md
- Check directory name matches what you're typing after `/`

### $ARGUMENTS Not Being Substituted

- Verify you're using `$ARGUMENTS` (all caps, with dollar sign)
- Don't wrap in backticks -- `` `$ARGUMENTS` `` won't substitute
- For positional args, use `$0`, `$1`, `$2` (0-based indexing -- `$0` is the first argument)

### YAML Frontmatter Parse Errors

- Must start and end with `---` on their own lines
- Use `key: value` format (space after colon required)
- Strings with special characters need quoting: `description: "Calculate fees: HOA edition"`
- Boolean values: `true`/`false` (lowercase)

### Permission Issues with .claude/ Directory

- Verify `.claude/` directory exists in the project root
- Check file permissions: `ls -la .claude/`
- On WSL, check that the directory isn't read-only from Windows

### Supporting Files Not Found

- Files must be in the same directory as SKILL.md
- Use relative paths in SKILL.md: `./filename.txt` (not absolute paths)
- Check filename spelling and case sensitivity

---

## Session Wrap-Up Checklist

- [ ] All participants created at least one command
- [ ] All participants created at least one skill
- [ ] Participants tested their commands/skills successfully
- [ ] At least 3 participants shared in `#ai-exchange`
- [ ] Homework assigned: 2 commands + 1 skill + share in Slack
- [ ] Preview of Week 6 (Agents & Hooks) delivered

---

*End of Week 5 Trainer Runbook*
