# Week 6: Agents & Hooks - Trainer Runbook

## Session Overview

**Duration:** 2 hours
**Format:** In-person or virtual
**Tracks:** Developer, QA, PM, Support

---

## Pre-Session Checklist

### For Instructors

- [ ] Test hoa-automation builds without warnings (`dotnet build && dotnet test`)
- [ ] Verify hook and agent examples work correctly in Claude Code
- [ ] Confirm `.claude/agents/` and `.claude/settings.json` examples are ready
- [ ] Prepare backup exercises for permission issues
- [ ] Monitor `#ai-exchange` Slack channel
- [ ] Have the decision tree resource open for reference

---

## Timing Guide

| Time | Segment | What Happens |
| ---- | ------- | ------------ |
| 0:00-0:05 | Welcome & Context | Set the stage: "Last week commands & skills, this week agents & hooks" |
| 0:05-0:30 | Part 1: Understanding Agents | What agents are, file structure, permission modes |
| 0:30-1:00 | Part 2: Creating Custom Agents | /agents command, RealManage examples, live demo |
| 1:00-1:25 | Part 3: Understanding Hooks | Hook types, matcher patterns, stdin JSON parsing |
| 1:25-1:55 | Part 4: Implementing Hooks | Security, audit, and automation examples + track exercises |
| 1:55-2:00 | Close | Key takeaways, homework, preview Week 7 |

> **Note:** Part 5 hands-on exercises (README) are self-paced. Encourage participants to complete during the session if time permits, otherwise as homework.

---

## Track Delivery Strategy

### Combined Session (Recommended)

All participants attend Parts 1-4 together. For the hands-on portion, participants work on their track-specific exercises:

- **Developers** follow the main README exercises or `tracks/developer.md` Part 3 (code-reviewer agent, test hook, audit system)
- **QA** follow `tracks/qa.md` Part 4 (test hooks, audit logging, test agent usage)
- **PMs** follow `tracks/pm.md` exercises (requirements analyst agent, quality check hook, combined workflow)
- **Support** follow `tracks/support.md` exercises (hook concepts, workflow automation, response validator)

### Instructor Tips for Mixed Audiences

- During Part 1 demo, use the code-reviewer agent concept -- all roles understand code review
- During Part 3 demo, use the audit logging hook -- compliance is universal at RealManage
- When splitting for exercises, briefly explain: "Find your track file and work through the exercises. Raise your hand if you get stuck."
- Pair non-developers with developers if anyone struggles with JSON or file creation
- For virtual sessions, use breakout rooms by track

---

## Live Demo Script

### Part 1 Demo: Agent File Structure (10 min)

```bash
# 1. Navigate to example project
cd courses/ai-101-claude-code/sessions/week-6/examples/hoa-automation

# 2. Show where agents live
ls -la .claude/agents/ 2>/dev/null || echo "No agents yet -- we'll create one"
```

**Talk through:**

- "Agents are markdown files with YAML frontmatter, just like commands and skills"
- "They live in `.claude/agents/` at project or user level"
- "The frontmatter defines tools, model, and permission mode"
- "The body is the system prompt -- what the agent knows and does"

```bash
# 3. Create a code-reviewer agent live
claude
> Create a code-reviewer agent in .claude/agents/code-reviewer.md that reviews code
> for quality and security using only Read, Grep, and Glob tools in plan mode.
```

**After agent is created:**

- "Notice `permissionMode: plan` -- this agent can only read, never modify"
- "The tools list restricts what it can access"
- "Let's test it"

```bash
> Use the code-reviewer agent to review the Services folder
```

**Key point:** "Agents are invoked via natural language -- just say 'use the code-reviewer agent' and Claude delegates to it."

### Part 3 Demo: Hook Configuration (10 min)

```bash
# 1. Show where hooks are configured
cat .claude/settings.json 2>/dev/null || echo "No settings yet"
```

**Talk through:**

- "Hooks live in `.claude/settings.json` -- same file as permissions"
- "The `matcher` field is a regex matching tool names: `Bash`, `Edit`, `Write`, etc."
- "To filter specific commands, you read the stdin JSON in a hook script"
- "Mac/Linux/WSL users: use Bash scripts with `jq` to parse stdin JSON"
- "Windows users: use PowerShell scripts with `ConvertFrom-Json` instead"

```bash
# 2. Create a simple audit hook
> Create a .claude/settings.json with a PreToolUse hook that logs all operations
> to .claude/audit.log with timestamps
```

**After hook is created:**

```bash
# 3. Test the hook
> Read the Program.cs file
# Check the audit log
> Show me the contents of .claude/audit.log
```

**Key points:**

- "The hook ran automatically before the Read operation"
- "Every tool invocation is now logged with a timestamp"
- "`exit 2` in a hook blocks the operation -- that's how you prevent dangerous commands"

### Expected Demo Output

**For code-reviewer agent:**

- Structured review with CRITICAL/WARNING/SUGGESTION categories
- References to specific files and line numbers
- Findings about the intentional bugs in ViolationWorkflowService.cs

**For audit hook:**

- `.claude/audit.log` file with ISO 8601 timestamps
- Entries like: `2026-02-24T10:15:30-06:00 | PRE | Read | {"file_path": "..."}`
- New entries appended for each operation

---

## Instructor Notes

### Key Points to Emphasize

- **Agents isolate context** -- Use them when you don't want to pollute main conversation
- **Tool restrictions are powerful** -- `permissionMode: plan` makes agents safe for analysis
- **Hooks enable automation** -- Pre/post operations without manual intervention
- **Matcher = tool name regex** -- To filter by command content, read stdin JSON in the hook script
- **Hooks receive JSON via stdin** -- Use `jq` (Mac/Linux/WSL) or `ConvertFrom-Json` (Windows PowerShell) to parse tool_name, tool_input, etc.
- **SOC 2 compliance** -- Hooks are essential for audit trails at RealManage
- **exit 2 blocks operations** -- This is how hooks prevent dangerous commands (exit 0 = allow, exit 1 = non-blocking error)

### Common Questions

> **"When should I use an agent vs just asking Claude?"**

- Agent for: isolated tasks, restricted tools, specific model, parallel work
- Main Claude for: iterative work, shared context, complex conversations

> **"Can hooks break my workflow?"**

- Yes, if they `exit 2` on valid operations
- Always test hooks in sandbox first
- Keep hook commands fast (<1 second)

> **"What's the difference between user and project agents?"**

- User agents (`~/.claude/agents/`) work everywhere
- Project agents (`.claude/agents/`) are project-specific
- Project settings override user settings

> **"How do I block specific commands like rm -rf?"**

- Set `matcher` to `"Bash"` (matches all Bash tool uses)
- In the hook script, read stdin JSON with `jq` and check for the pattern
- Bash example: `COMMAND=$(jq -r '.tool_input.command'); echo "$COMMAND" | grep -q 'rm -rf' && exit 2`
- PowerShell example: `$hookData = [Console]::In.ReadToEnd() | ConvertFrom-Json; if ($hookData.tool_input.command -match 'rm -rf') { exit 2 }`
- `exit 2` blocks the operation; stderr is shown to Claude

> **"Do I need jq for hook scripts?"**

- Mac/Linux/WSL: Yes, use Bash + `jq` to parse stdin JSON. Install with `brew install jq` (Mac) or `sudo apt install jq` (Linux/WSL)
- Windows: No -- use PowerShell scripts with `ConvertFrom-Json` instead. PowerShell 5.x is pre-installed on all Windows machines
- All track files show both Bash and PowerShell examples side-by-side

> **"How do I debug hooks?"**

- Bash: Add `set -x` to hook commands for verbose output
- PowerShell: Add `Set-PSDebug -Trace 1` at the top of the script
- Check `.claude/audit.log` for execution history
- Run hook commands manually in terminal first

---

## Troubleshooting

**Agent not working:**

- Verify frontmatter has required fields (`name`, `description`)
- Check tools are spelled correctly
- Ensure model name is valid

**Hooks not triggering:**

- Validate JSON syntax: `cat .claude/settings.json | jq .`
- Check matcher patterns (must match tool name, not command content)
- Verify hook command is executable
- Restart Claude session after changes

**Hooks blocking unexpectedly:**

- Check matcher is not too broad
- Verify exit codes in hook commands
- Add logging to see what's triggering

> **Still having issues?** See the [Troubleshooting Guide](../../resources/troubleshooting.md#-hooks-arent-triggering) for detailed debugging steps.

---

## Session Wrap-Up Checklist

- [ ] All participants understand agents vs hooks distinction
- [ ] All participants created at least one agent or hook
- [ ] Participants tested their configurations successfully
- [ ] At least 3 participants shared in `#ai-exchange`
- [ ] Homework assigned: 2 agents + audit hooks + block dangerous ops + share in Slack
- [ ] Preview of Week 7 (Plugins) delivered: "Plugins package agents, hooks, commands, and skills together"

---

*End of Week 6 Trainer Runbook*
