# Week 5: Advanced CLI Features & Multi-Claude Workflows 🚀

**Duration:** 2 hours  
**Format:** Self-paced or instructor-led  
**Prerequisites:** Completed Weeks 1-4

## 🎯 Learning Objectives

By the end of this session, you'll be able to:
- ✅ Customize Claude Code with themes and permissions
- ✅ Create custom slash commands for RealManage workflows
- ✅ Run multiple Claude instances for parallel development
- ✅ Use safe YOLO mode appropriately
- ✅ Implement code review workflows with dual instances

## 📚 Session Content

Coming soon! This session will cover:

### Part 1: CLI Customization
- Settings management with `/config`
- Permission controls for production safety
- Custom themes and shortcuts
- Sharing team configurations

### Part 2: Custom Slash Commands
- Creating commands in `.claude/commands/`
- Passing arguments with `$ARGUMENTS`
- RealManage-specific commands
- HOA violation templates

### Part 3: Multi-Claude Workflows
- Git worktrees for parallel development
- Code writer + reviewer pattern
- C# API and Angular UI simultaneously
- Safe YOLO mode for repetitive tasks

### Part 4: Security & Compliance
- Protecting resident PII
- Audit trail requirements
- SOC 2 Type II compliance
- Permission best practices

## 🧪 Sandbox Exercises

Create your sandbox from the example (when available):
```bash
# When example is provided:
cp -r example sandbox
cd sandbox
claude
```

Exercises will include:
- Create `/violation-report` command
- Set up dual-instance review
- Configure team settings

## 📖 Resources

- [Claude Code Settings](https://docs.anthropic.com/en/docs/claude-code/settings)
- [Custom Slash Commands](https://docs.anthropic.com/en/docs/claude-code/slash-commands#custom-commands)
- [CLI Reference](https://docs.anthropic.com/en/docs/claude-code/cli-reference)

## 🚀 Next Week

[Week 6: MCP Servers & Integrations →](../week-6/README.md)