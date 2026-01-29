# Week 7: Plugins - The Complete Package - Trainer Runbook

## Pre-Session Checklist

### For Instructors

- [ ] Test all example projects build without warnings
- [ ] Verify plugin examples work correctly
- [ ] Prepare backup exercises for network issues
- [ ] Monitor `#ai-exchange` Slack channel

---

## Instructor Notes

### Key Points to Emphasize

- **Plugins are the container** - Everything from Week 6 goes inside
- **Skills = enhanced commands** - Same concept, more capabilities
- **Agents + Hooks in plugins** - Complete packaging
- **Distribution** - Share automation with your team

### Common Questions

> **"Do I need a plugin for simple automation?"**

- No, you can use commands/agents/hooks directly
- Plugins are for packaging and distribution
- Use plugins when sharing with team or across projects

> **"Can I use Week 6 components without plugins?"**

- Yes! `.claude/commands/`, `.claude/agents/`, `.claude/settings.json` all work
- Plugins just package them for distribution

> **"What's the difference between plugin hooks and skill hooks?"**

- Plugin hooks (hooks.json): Apply to all plugin operations
- Skill hooks (embedded): Apply only when that skill runs

### Troubleshooting

**Plugin not loading:**

- Check `.claude-plugin/plugin.json` exists and is valid JSON
- Verify directory structure is correct
- Use `/plugin validate .` to check

**Skill not recognized:**

- Check path: `<plugin>/skills/<name>/SKILL.md`
- Verify YAML frontmatter syntax
- Restart Claude session

**Agent not spawning:**

- Verify agent file exists in `<plugin>/agents/`
- Check `agent:` field matches agent name
- Ensure `context: fork` is set in skill
