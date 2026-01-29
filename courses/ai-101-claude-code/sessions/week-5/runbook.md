# Week 5: Commands & Basic Skills - Trainer Runbook

## Pre-Session Checklist

### For Instructors

- [ ] Test all example projects build without warnings
- [ ] Verify command and skill examples work correctly
- [ ] Prepare backup exercises for permission issues
- [ ] Monitor `#ai-exchange` Slack channel

---

## Instructor Notes

### Key Points to Emphasize

- **One concept at a time** - Commands are simple, skills add supporting files
- **Start with commands** - Don't over-engineer; use skills only when needed
- **Supporting files are key** - That's the main difference from commands
- **Build progressively** - Master commands first, then skills, then Week 6's agents/hooks

### Common Questions

> **"When should I use a skill instead of a command?"**

- If you need a template file, data file, or script = Skill
- If it's just a prompt with arguments = Command
- When in doubt, start with a command and upgrade to a skill if needed

> **"Can I convert a command to a skill?"**

- Yes! Move the .md file to `.claude/skills/<name>/SKILL.md`
- Add a `name` field to frontmatter
- Add any supporting files you need

> **"Where do skills go - user or project level?"**

- Project level (`.claude/skills/`) for project-specific workflows
- User level (`~/.claude/skills/`) for personal productivity skills

### Troubleshooting

**Commands not recognized:**

- Check `.claude/commands/` directory exists
- Verify `.md` extension
- Restart Claude session
- Check YAML frontmatter syntax

**Skills not recognized:**

- Check `.claude/skills/<name>/SKILL.md` path is correct
- Verify SKILL.md filename (case sensitive)
- Check YAML frontmatter has `name` and `description`
- Supporting files must be in same directory as SKILL.md

---

*End of Week 5 Trainer Runbook*
