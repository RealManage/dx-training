# Week 6: Agents & Hooks - Trainer Runbook

## Pre-Session Checklist

### For Instructors

- [ ] Test all example projects build without warnings
- [ ] Verify hook and agent examples work correctly
- [ ] Prepare backup exercises for permission issues
- [ ] Monitor `#ai-exchange` Slack channel

---

## Instructor Notes

### Key Points to Emphasize

- **Agents isolate context** - Use them when you don't want to pollute main conversation
- **Tool restrictions are powerful** - `permissionMode: plan` makes agents safe for analysis
- **Hooks enable automation** - Pre/post operations without manual intervention
- **SOC 2 compliance** - Hooks are essential for audit trails at RealManage
- **exit 1 blocks operations** - This is how hooks prevent dangerous commands

### Common Questions

> **"When should I use an agent vs just asking Claude?"**

- Agent for: isolated tasks, restricted tools, specific model, parallel work
- Main Claude for: iterative work, shared context, complex conversations

> **"Can hooks break my workflow?"**

- Yes, if they `exit 1` on valid operations
- Always test hooks in sandbox first
- Keep hook commands fast (<1 second)

> **"What's the difference between user and project agents?"**

- User agents (`~/.claude/agents/`) work everywhere
- Project agents (`.claude/agents/`) are project-specific
- Project settings override user settings

> **"How do I debug hooks?"**

- Add `set -x` to hook commands for verbose output
- Check `.claude/audit.log` for execution history
- Run hook commands manually in terminal first

### Troubleshooting

**Agent not working:**

- Verify frontmatter has required fields (`name`, `description`)
- Check tools are spelled correctly
- Ensure model name is valid

**Hooks not triggering:**

- Validate JSON syntax: `cat .claude/settings.json | jq .`
- Check matcher patterns
- Verify hook command is executable
- Restart Claude session after changes

**Hooks blocking unexpectedly:**

- Check matcher is not too broad
- Verify exit codes in hook commands
- Add logging to see what's triggering

> **Still having issues?** See the [Troubleshooting Guide](../../resources/troubleshooting.md#-hooks-arent-triggering) for detailed debugging steps.

---

*End of Week 6 Trainer Runbook*
