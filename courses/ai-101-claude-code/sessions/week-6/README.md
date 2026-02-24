# Week 6: Agents & Hooks

**Duration:** 2 hours (advanced exercises are self-paced)
**Format:** In-person or virtual
**Audience:** RealManage cross-functional team (engineers, PMs, QA, support staff)

## Learning Tracks

This week has role-specific tracks:

- **Developer Track** - Debug, build features, and create agents with full hooks integration (this README)
- **[QA Track](./tracks/qa.md)** - Design test agents and automate quality workflows
- **[PM Track](./tracks/pm.md)** - Automate workflows and build process-driven agents
- **[Support Track](./tracks/support.md)** - Build quality hooks and automate support workflows

---

## Learning Objectives

By the end of this session, participants will be able to:

- Understand what agents are and when to use them
- Create custom agents in `.claude/agents/`
- Configure hooks for pre/post operation automation
- Implement security and audit patterns with hooks
- Build a "code-reviewer" agent for team workflows
- Create a post-change hook that runs tests automatically

## Pre-Session Checklist

### For Participants

- [ ] Completed Weeks 1-5 (CLI, prompts, Plan Mode, TDD, Commands & Skills)
- [ ] Claude Code working smoothly
- [ ] Understanding of `.claude/` configuration structure from Week 5
- [ ] Reviewed [Glossary](../../resources/glossary.md) for terms: Agent, Hook
- [ ] **Mac/Linux/WSL:** Install `jq` for JSON parsing (`brew install jq` or `apt install jq`)
- [ ] **Windows:** PowerShell 5.x+ is already installed (verify with `$PSVersionTable.PSVersion`)
- [ ] Ready for 2-hour session

---

## Session Plan

### Part 1: Understanding Agents (25 min)

#### 1.1 What Are Agents? (10 min)

**Agents** are specialized AI assistants with their own context, tools, and permissions. They handle isolated tasks without cluttering your main conversation.

Think of agents as team members with specific roles:

- A **security auditor** who only reads code and reports vulnerabilities
- A **test writer** who creates unit tests following TDD principles
- A **code reviewer** who checks changes against team standards

**Built-in vs Custom Agents:**

| Type | Description | Use Case |
| ---- | ----------- | -------- |
| **Explore** | Fast, read-only codebase analysis | Finding files, searching code |
| **Plan** | Architecture planning | Design decisions |
| **Bash** | Terminal commands in separate context | System operations |
| **general-purpose** | Complex multi-step tasks | Research, analysis |
| **Custom** | Your own agents with specific prompts/tools | Domain-specific tasks |

**Why Create Custom Agents?**

- **Isolated context** - Keeps main conversation clean
- **Tool restrictions** - Limit what the agent can do (read-only, no edits, etc.)
- **Permission control** - Set specific permission modes
- **Reusable** - Define once, use everywhere in your project
- **Model flexibility** - Use different models based on task complexity

> **When to use an agent vs command vs skill?** See the [Decision Trees](../../resources/decision-trees.md#2-command-vs-skill-vs-agent-vs-plugin) for a flowchart guide.

#### 1.2 Agent File Location and Structure (15 min)

**Where Agents Live:**

```text
~/.claude/agents/           # User-level (all projects)
.claude/agents/             # Project-level (this project only)
```

**Agent File Format (.claude/agents/code-reviewer.md):**

```markdown
---
name: code-reviewer
description: Reviews code for quality, security, and best practices
tools: Read, Glob, Grep
model: sonnet
permissionMode: default
---

You are a senior code reviewer ensuring high standards.

When invoked:
1. Run git diff to see recent changes
2. Focus on modified files
3. Review for readability, security, performance

Provide feedback organized by:
- Critical issues (must fix)
- Warnings (should fix)
- Suggestions (nice to have)

Follow RealManage coding standards and SOC 2 compliance requirements.
```

**Frontmatter Fields:**

| Field | Required | Description |
| ----- | -------- | ----------- |
| `name` | Yes | Unique identifier (lowercase, hyphens) |
| `description` | Yes | When Claude should use this agent |
| `tools` | No | Allowed tools (inherits all if omitted) |
| `disallowedTools` | No | Tools to deny |
| `model` | No | `sonnet`, `opus`, `default`, or `inherit` |
| `permissionMode` | No | `default`, `plan`, `acceptEdits`, `dontAsk`, `bypassPermissions` |
| `skills` | No | Skills to preload |
| `hooks` | No | Lifecycle hooks |

**Permission Modes Explained:**

| Mode | Description | Use Case |
| ---- | ----------- | -------- |
| `default` | Asks for approval before tool use | Normal operations - you approve each action |
| `plan` | No tool execution allowed | Analysis, code review, architecture planning |
| `acceptEdits` | Auto-approves file edits | Trusted refactoring in sandbox/isolated dirs |
| `dontAsk` | Auto-denies permission prompts | Read-only agents that shouldn't modify anything |
| `bypassPermissions` | Auto-approves all tools | Fully automated pipelines (use with caution) |

> **Note:** Permission modes can **restrict** agent behavior but cannot **escalate** beyond your session's permission settings. An agent with `bypassPermissions` still requires your approval if your session is in `default` mode.
>
> **Tip:** Use `plan` mode for analysis agents, `default` for normal work where you approve actions, and `acceptEdits` only in sandboxed/isolated environments.

**Tool Restrictions:**

```yaml
# Allow only these tools
tools: Read, Grep, Glob, Bash

# OR deny specific tools
disallowedTools: Write, Edit, NotebookEdit
```

---

### Part 2: Creating Custom Agents (30 min)

#### 2.1 Using the /agents Command (10 min)

**Interactive Agent Management:**

```bash
# Open agent manager
/agents

# Options:
# - Create new agent
# - Edit existing agent
# - Delete agent
# - Generate with Claude (AI-assisted creation)
```

**Creating via /agents:**

1. Run `/agents`
2. Select "Create new agent"
3. Choose scope (user or project)
4. Select "Generate with Claude" and describe what you need
5. Configure tools, model, and permissions
6. Save

#### 2.2 RealManage Agent Examples (20 min)

**Example 1: Read-Only Database Analyst**

```markdown
---
name: db-analyst
description: Analyze database queries and schema without making changes
tools: Read, Grep, Glob
model: default
permissionMode: plan
---

You are a database analyst with read-only access.

Capabilities:
- Analyze SQL queries for performance
- Review Entity Framework models
- Suggest index improvements
- Identify N+1 query patterns

Restrictions:
- Never suggest DROP or DELETE operations
- Always recommend backup before migrations
- Flag any PII exposure risks
```

**Example 2: Security Auditor**

```markdown
---
name: security-auditor
description: Audit code for security vulnerabilities and compliance
tools: Read, Grep, Glob
model: opus
permissionMode: plan
---

You are a security auditor checking for vulnerabilities.

Review for:
- OWASP Top 10 vulnerabilities
- SQL injection risks
- XSS attack vectors
- Hardcoded secrets or credentials
- PII exposure
- SOC 2 compliance violations

Output a security report with:
- Severity: CRITICAL / HIGH / MEDIUM / LOW
- Location: File and line number
- Issue: Description of vulnerability
- Remediation: How to fix it
```

**Example 3: Test Writer** *(Optional - review if time permits)*

```markdown
---
name: test-writer
description: Write unit tests following TDD principles
tools: Read, Write, Edit, Bash
model: sonnet
permissionMode: acceptEdits
skills:
  - tdd-patterns
---

You are a test-first developer specializing in xUnit.

When writing tests:
1. Analyze the code to understand behavior
2. Write failing tests FIRST
3. Use FluentAssertions for assertions
4. Use Moq for dependencies
5. Follow Arrange-Act-Assert pattern
6. Target 80-90% code coverage

Test naming: MethodName_Scenario_ExpectedBehavior
```

**Example 4: Violation Analyst (HOA Domain)** *(Optional - review if time permits)*

```markdown
---
name: violation-analyst
description: Analyze violation patterns and recommend process improvements
tools: Read, Grep, Glob
model: default
permissionMode: plan
---

You are a violation data analyst for HOA management.

When analyzing violations:
1. Identify patterns by violation type
2. Find repeat offenders
3. Calculate average resolution time
4. Identify bottlenecks in escalation
5. Recommend process improvements

Focus on data-driven insights only.
Do not modify any data - analysis only.
```

---

### Part 3: Understanding Hooks (25 min)

#### 3.1 What Are Hooks? (10 min)

**Hooks** are automated actions that run in response to Claude Code events. They enable compliance, logging, and workflow automation without manual intervention.

**Hook Types:**

| Hook | Trigger | Use Case |
| ---- | ------- | -------- |
| `PreToolUse` | Before any tool executes | Validation, logging, blocking |
| `PostToolUse` | After tool completes | Audit logging, notifications |
| `Notification` | When Claude sends notification | Alert forwarding |
| `Stop` | When Claude stops/pauses | Cleanup, summaries |

**Configuration Locations:**

```text
~/.claude/settings.json          # User-level (all projects)
.claude/settings.json            # Project-level (this project)
```

> **Note:** Settings follow a precedence hierarchy (highest to lowest): managed settings → CLI args → local project (`.claude/settings.local.json`) → project (`.claude/settings.json`) → user (`~/.claude/settings.json`). Project settings override user settings, allowing teams to enforce project-specific hooks.

**Basic Hook Structure:**

```json
{
  "hooks": {
    "PreToolUse": [
      {
        "matcher": "Bash",
        "hooks": [
          {
            "type": "command",
            "command": ".claude/hooks/log-bash.sh"
          }
        ]
      }
    ]
  }
}
```

**How Hooks Receive Data:**

Hook commands receive a **JSON object via stdin** containing details about the event. For tool-related hooks, this includes the tool name, input, and session context:

```json
{
  "session_id": "abc123",
  "cwd": "/home/user/my-project",
  "hook_event_name": "PreToolUse",
  "tool_name": "Bash",
  "tool_input": {
    "command": "dotnet test"
  }
}
```

Your hook scripts parse this JSON from stdin. Use the examples below for your platform:

**Bash (Mac/Linux/WSL):** `.claude/hooks/log-bash.sh`

```bash
#!/bin/bash
TOOL=$(jq -r '.tool_name')
CMD=$(jq -r '.tool_input.command // empty')
echo "$(date -Iseconds) | $TOOL | $CMD" >> .claude/audit.log
```

**PowerShell (Windows):** `.claude/hooks/log-bash.ps1`

```powershell
$hookData = [Console]::In.ReadToEnd() | ConvertFrom-Json
$tool = $hookData.tool_name
$cmd = $hookData.tool_input.command
Add-Content -Path ".claude/audit.log" -Value "$(Get-Date -Format 'o') | $tool | $cmd"
```

> **Note:** Hook data comes via stdin JSON, **not** environment variables. The one environment variable available is `$CLAUDE_PROJECT_DIR` (project root path, useful for referencing script files).

**Platform Setup:**

- **Mac/Linux/WSL:** Install `jq` for JSON parsing (`brew install jq`, `apt install jq`, or `sudo dnf install jq`)
- **Windows:** PowerShell 5.x+ is built in — use `ConvertFrom-Json` (no extra install needed)
- **Windows (settings.json):** Reference PowerShell scripts as: `"command": "powershell -NoProfile -File .claude/hooks/script-name.ps1"`

**Common Stdin Fields:**

| Field | Description | Available In |
| ----- | ----------- | ------------ |
| `tool_name` | Name of tool being used | PreToolUse, PostToolUse |
| `tool_input` | Input to the tool (JSON object) | PreToolUse, PostToolUse |
| `tool_response` | Output from tool | PostToolUse only |
| `session_id` | Current session identifier | All hooks |
| `cwd` | Working directory | All hooks |

#### 3.2 Matcher Patterns (15 min)

**Matcher Syntax:**

The `matcher` field is a **regex that matches tool names only** (e.g., `"Bash"`, `"Edit"`, `"Write"`). To filter by command content, read the stdin JSON in your hook script.

```json
{
  "hooks": {
    "PreToolUse": [
      {
        "matcher": "Bash",
        "hooks": [{ "type": "command", "command": "echo 'Bash detected'" }]
      },
      {
        "matcher": "Edit",
        "hooks": [{ "type": "command", "command": "echo 'Edit detected'" }]
      },
      {
        "matcher": "Edit|Write",
        "hooks": [{ "type": "command", "command": "echo 'File modification detected'" }]
      },
      {
        "matcher": "*",
        "hooks": [{ "type": "command", "command": "echo 'Any tool'" }]
      }
    ]
  }
}
```

**Common Matchers:**

| Matcher | Description |
| ------- | ----------- |
| `"*"` | Match all tools |
| `"Bash"` | All Bash commands |
| `"Edit"` | All file edits |
| `"Write"` | All file writes |
| `"Read"` | All file reads |
| `"Edit\|Write"` | Multiple tools (regex OR) |

> **Important:** To filter *within* a tool (e.g., only git commands or only rm commands), match on `"Bash"` and then read the stdin JSON in your hook script. See Part 4 for examples.

---

### Part 4: Implementing Hooks (30 min)

#### 4.1 Security Use Cases (15 min)

**Example 1: Block Dangerous Operations**

Since `matcher` only filters by tool name, we match on `"Bash"` and inspect the command in a hook script:

```json
{
  "hooks": {
    "PreToolUse": [
      {
        "matcher": "Bash",
        "hooks": [
          {
            "type": "command",
            "command": ".claude/hooks/block-dangerous.sh"
          }
        ]
      }
    ]
  }
}
```

**Bash (Mac/Linux/WSL):** `.claude/hooks/block-dangerous.sh`

```bash
#!/bin/bash
COMMAND=$(jq -r '.tool_input.command // empty')

if echo "$COMMAND" | grep -qEi 'rm -rf|rm -r /|git push.*--force|DROP TABLE|TRUNCATE'; then
  echo "BLOCKED: Dangerous operation detected: $COMMAND" >&2
  exit 2  # Exit 2 = blocking error, prevents tool execution
fi

exit 0  # Allow the command
```

**PowerShell (Windows):** `.claude/hooks/block-dangerous.ps1`

```powershell
$hookData = [Console]::In.ReadToEnd() | ConvertFrom-Json
$command = $hookData.tool_input.command

if ($command -match 'rm -rf|rm -r /|git push.*--force|DROP TABLE|TRUNCATE') {
    [Console]::Error.WriteLine("BLOCKED: Dangerous operation detected: $command")
    exit 2
}

exit 0
```

**Key Concept:** `exit 2` **blocks** the operation (stderr is shown to Claude). `exit 0` allows it. `exit 1` is a non-blocking error that is only shown in verbose mode.

> **Windows settings.json:** Use `"command": "powershell -NoProfile -File .claude/hooks/block-dangerous.ps1"` instead of the `.sh` path.

**Example 2: Protect Sensitive Files**

```json
{
  "hooks": {
    "PreToolUse": [
      {
        "matcher": "Edit|Write",
        "hooks": [
          {
            "type": "command",
            "command": ".claude/hooks/block-sensitive-files.sh"
          }
        ]
      }
    ]
  }
}
```

**Bash (Mac/Linux/WSL):** `.claude/hooks/block-sensitive-files.sh`

```bash
#!/bin/bash
FILE_PATH=$(jq -r '.tool_input.file_path // empty')

if echo "$FILE_PATH" | grep -qEi '\.env|\.pem|credentials|secrets|password'; then
  echo "BLOCKED: Cannot edit sensitive file: $FILE_PATH" >&2
  exit 2
fi

exit 0
```

**PowerShell (Windows):** `.claude/hooks/block-sensitive-files.ps1`

```powershell
$hookData = [Console]::In.ReadToEnd() | ConvertFrom-Json
$filePath = $hookData.tool_input.file_path

if ($filePath -match '\.(env|pem)|credentials|secrets|password') {
    [Console]::Error.WriteLine("BLOCKED: Cannot edit sensitive file: $filePath")
    exit 2
}

exit 0
```

#### 4.2 Audit and Compliance Use Cases (15 min)

**Example 3: SOC 2 Audit Trail**

```json
{
  "hooks": {
    "PreToolUse": [
      {
        "matcher": "*",
        "hooks": [
          {
            "type": "command",
            "command": ".claude/hooks/audit-log.sh"
          }
        ]
      }
    ],
    "PostToolUse": [
      {
        "matcher": "*",
        "hooks": [
          {
            "type": "command",
            "command": ".claude/hooks/audit-log-post.sh"
          }
        ]
      }
    ]
  }
}
```

**Bash (Mac/Linux/WSL):**

```bash
#!/bin/bash
# .claude/hooks/audit-log.sh (PreToolUse)
mkdir -p .claude
TOOL=$(jq -r '.tool_name')
INPUT=$(jq -r '.tool_input | tostring')
echo "$(date -Iseconds) | PRE | $TOOL | $INPUT" >> .claude/audit.log
```

```bash
#!/bin/bash
# .claude/hooks/audit-log-post.sh (PostToolUse)
TOOL=$(jq -r '.tool_name')
echo "$(date -Iseconds) | POST | $TOOL | SUCCESS" >> .claude/audit.log
```

**PowerShell (Windows):**

```powershell
# .claude/hooks/audit-log.ps1 (PreToolUse)
if (-not (Test-Path ".claude")) { New-Item -ItemType Directory -Path ".claude" | Out-Null }
$hookData = [Console]::In.ReadToEnd() | ConvertFrom-Json
$tool = $hookData.tool_name
$input = $hookData.tool_input | ConvertTo-Json -Compress
Add-Content -Path ".claude/audit.log" -Value "$(Get-Date -Format 'o') | PRE | $tool | $input"
```

```powershell
# .claude/hooks/audit-log-post.ps1 (PostToolUse)
$hookData = [Console]::In.ReadToEnd() | ConvertFrom-Json
$tool = $hookData.tool_name
Add-Content -Path ".claude/audit.log" -Value "$(Get-Date -Format 'o') | POST | $tool | SUCCESS"
```

**Example 4: Auto-Run Tests After Edits**

This hook doesn't need to read stdin — it just runs tests unconditionally after any Edit:

```json
{
  "hooks": {
    "PostToolUse": [
      {
        "matcher": "Edit",
        "hooks": [
          {
            "type": "command",
            "command": "dotnet test --no-build --verbosity quiet 2>/dev/null || echo 'Tests may need attention'"
          }
        ]
      }
    ]
  }
}
```

> **Tip:** Hooks that don't need the event data can use simple inline commands. Use scripts when you need to read stdin JSON.

**Example 5: Financial Data Access Logging**

```json
{
  "hooks": {
    "PreToolUse": [
      {
        "matcher": "Read",
        "hooks": [
          {
            "type": "command",
            "command": ".claude/hooks/log-financial-access.sh"
          }
        ]
      }
    ]
  }
}
```

**Bash (Mac/Linux/WSL):** `.claude/hooks/log-financial-access.sh`

```bash
#!/bin/bash
FILE_PATH=$(jq -r '.tool_input.file_path // empty')

if echo "$FILE_PATH" | grep -qi 'financial\|payment\|invoice'; then
  echo "$(date -Iseconds) | FINANCIAL_ACCESS | $FILE_PATH" >> .claude/finance-audit.log
fi
```

**PowerShell (Windows):** `.claude/hooks/log-financial-access.ps1`

```powershell
$hookData = [Console]::In.ReadToEnd() | ConvertFrom-Json
$filePath = $hookData.tool_input.file_path

if ($filePath -match 'financial|payment|invoice') {
    Add-Content -Path ".claude/finance-audit.log" -Value "$(Get-Date -Format 'o') | FINANCIAL_ACCESS | $filePath"
}
```

---

### Part 5: Hands-On Exercises (30 min)

#### 5.1 Setup Your Sandbox

```bash
# Copy example project to sandbox
cd courses/ai-101-claude-code/sessions/week-6
cp -r examples sandbox
cd sandbox/hoa-automation

# Build and verify
dotnet build
dotnet test

# Start Claude
claude
```

> **Not familiar with these commands?** No worries - Claude can walk you through them. Just ask!

#### 5.2 Exercise 1: Create a Code Reviewer Agent (10 min)

Create `.claude/agents/code-reviewer.md`:

```markdown
---
name: code-reviewer
description: Review code changes for quality, security, and RealManage standards
tools: Read, Grep, Glob, Bash
model: sonnet
permissionMode: plan
---

You are a senior code reviewer at RealManage.

## Review Checklist

### Code Quality
- Naming conventions (PascalCase for public, camelCase for private)
- SOLID principles adherence
- DRY - no code duplication
- Appropriate error handling

### Security
- No hardcoded secrets
- Input validation present
- SQL injection prevention (parameterized queries)
- No PII exposure in logs

### RealManage Standards
- 80-90% test coverage
- Async/await for I/O operations
- Repository pattern for data access
- SOC 2 compliance

## Output Format

Organize findings as:
1. **CRITICAL** - Must fix before merge
2. **WARNING** - Should fix, creates technical debt
3. **SUGGESTION** - Nice to have improvements

Include file path and line number for each finding.
```

**Test it:**

```text
> Use the code-reviewer agent to review the Services folder
```

#### 5.3 Exercise 2: Create a Post-Change Test Hook (10 min)

Create `.claude/settings.json` and `.claude/hooks/log-operation.sh`:

```json
{
  "hooks": {
    "PostToolUse": [
      {
        "matcher": "Edit",
        "hooks": [
          {
            "type": "command",
            "command": "echo '=== Running Tests After Edit ===' && dotnet test --no-build --verbosity minimal 2>&1 | tail -5"
          }
        ]
      }
    ],
    "PreToolUse": [
      {
        "matcher": "*",
        "hooks": [
          {
            "type": "command",
            "command": ".claude/hooks/log-operation.sh"
          }
        ]
      }
    ]
  }
}
```

**Bash (Mac/Linux/WSL):** `.claude/hooks/log-operation.sh`

```bash
#!/bin/bash
mkdir -p .claude
TOOL=$(jq -r '.tool_name')
echo "$(date -Iseconds) | $TOOL" >> .claude/operations.log
```

**PowerShell (Windows):** `.claude/hooks/log-operation.ps1`

```powershell
if (-not (Test-Path ".claude")) { New-Item -ItemType Directory -Path ".claude" | Out-Null }
$hookData = [Console]::In.ReadToEnd() | ConvertFrom-Json
Add-Content -Path ".claude/operations.log" -Value "$(Get-Date -Format 'o') | $($hookData.tool_name)"
```

**Test it:**

```text
> Edit Program.cs to add a comment at the top

# Watch for automatic test execution after the edit completes
```

#### 5.4 Exercise 3: Build an Audit System (10 min) - *Self-Paced Practice*

> **Note:** This exercise is self-paced. Complete it after the session or as homework. Exercises 1 and 2 are the in-session priority.

Expand `.claude/settings.json` with comprehensive auditing, plus create the hook scripts:

```json
{
  "hooks": {
    "PreToolUse": [
      {
        "matcher": "*",
        "hooks": [
          {
            "type": "command",
            "command": ".claude/hooks/audit-log.sh"
          }
        ]
      },
      {
        "matcher": "Bash",
        "hooks": [
          {
            "type": "command",
            "command": ".claude/hooks/block-dangerous.sh"
          }
        ]
      }
    ],
    "PostToolUse": [
      {
        "matcher": "Edit",
        "hooks": [
          {
            "type": "command",
            "command": "dotnet test --no-build --verbosity quiet 2>/dev/null || echo 'Tests need attention'"
          }
        ]
      }
    ]
  }
}
```

Create the hook scripts for your platform:

**Bash (Mac/Linux/WSL):**

```bash
#!/bin/bash
# .claude/hooks/audit-log.sh
mkdir -p .claude
TOOL=$(jq -r '.tool_name')
INPUT=$(jq -r '.tool_input | tostring')
echo "$(date -Iseconds) | PRE | $TOOL | $INPUT" >> .claude/audit.log
```

```bash
#!/bin/bash
# .claude/hooks/block-dangerous.sh
COMMAND=$(jq -r '.tool_input.command // empty')

if echo "$COMMAND" | grep -qEi '^rm '; then
  echo "BLOCKED: rm commands require manual approval" >&2
  exit 2
fi

exit 0
```

> **Remember:** Make scripts executable with `chmod +x .claude/hooks/*.sh`

**PowerShell (Windows):**

```powershell
# .claude/hooks/audit-log.ps1
if (-not (Test-Path ".claude")) { New-Item -ItemType Directory -Path ".claude" | Out-Null }
$hookData = [Console]::In.ReadToEnd() | ConvertFrom-Json
$tool = $hookData.tool_name
$input = $hookData.tool_input | ConvertTo-Json -Compress
Add-Content -Path ".claude/audit.log" -Value "$(Get-Date -Format 'o') | PRE | $tool | $input"
```

```powershell
# .claude/hooks/block-dangerous.ps1
$hookData = [Console]::In.ReadToEnd() | ConvertFrom-Json
$command = $hookData.tool_input.command

if ($command -match '^rm ') {
    [Console]::Error.WriteLine("BLOCKED: rm commands require manual approval")
    exit 2
}

exit 0
```

> **Windows:** Reference as `"command": "powershell -NoProfile -File .claude/hooks/block-dangerous.ps1"`

**Test the complete system:**

```text
> Edit a file to add a TODO comment

> Try to run rm test.txt (should be blocked!)

> Check the audit log: cat .claude/audit.log
```

---

## 🎯 Key Takeaways

### Agents Quick Reference

```text
LOCATION: .claude/agents/<name>.md

FORMAT:
---
name: agent-name
description: When to use this agent
tools: Read, Grep, Glob
model: sonnet | opus | default | inherit
permissionMode: default | plan | acceptEdits | dontAsk | bypassPermissions
---
System prompt for the agent

INVOKE: Claude auto-delegates or "use the <name> agent"
```

### Hooks Quick Reference

```text
LOCATION: .claude/settings.json

HOOK TYPES:
PreToolUse   -> Before execution (validate/block)
PostToolUse  -> After completion (log/notify)
Notification -> When Claude notifies
Stop         -> When session ends

MATCHERS (regex on tool name):
"*"              -> All tools
"Bash"           -> All bash commands
"Edit"           -> All file edits
"Edit|Write"     -> Multiple tools (regex OR)
Filter by content -> read stdin JSON in hook script

INPUT: JSON via stdin (use jq to parse)
  tool_name, tool_input, session_id, cwd

BLOCKING:
exit 2 + stderr message blocks the operation
exit 0 allows it
```

### When to Use What

| Need | Solution |
| ---- | -------- |
| Specialized task with limited tools | Custom Agent |
| Read-only analysis | Agent with `permissionMode: plan` |
| Block dangerous commands | PreToolUse hook with `exit 2` |
| Audit trail for compliance | Pre/Post hooks logging to file |
| Auto-run tests | PostToolUse hook on Edit |
| Notify on completion | PostToolUse or Notification hook |

---

## Homework (Before Week 7)

### Required Tasks

1. Create 2 custom agents for your daily workflow
2. Configure audit hooks in a project
3. Create a hook that blocks at least one dangerous operation
4. Create a hook that auto-runs tests after edits
5. Share your best agent or hook configuration in `#ai-exchange` Slack

### Stretch Goals

1. Create an agent that spawns from a skill (uses `agent:` field)
2. Build a complete SOC 2 audit trail with multiple hooks
3. Create an agent with embedded hooks

---

## Resources

### Official Documentation

- [Sub-agents](https://code.claude.com/docs/en/sub-agents)
- [Hooks](https://code.claude.com/docs/en/hooks)
- [CLI Reference](https://code.claude.com/docs/en/cli-reference)

### RealManage Resources

- [Week 6 Examples](./examples/) - HOA Automation project
- [Glossary](../../resources/glossary.md) - Term definitions
- Slack: `#ai-exchange`

---

## Next Week Preview

**Week 7: Plugins - The Complete Package**

Plugins package everything you've learned into distributable units:

- Plugin architecture and manifest files
- Packaging skills, agents, and hooks together
- Installing plugins from registries and git repos
- Building your own RealManage plugins

**The progression:** Commands (Week 5) + Skills (Week 5) + Agents (Week 6) + Hooks (Week 6) -> **Plugins (Week 7)**

---

## 📚 Quick Resources

- [Glossary](../../resources/glossary.md) - Key terms and definitions
- [Decision Trees](../../resources/decision-trees.md) - When to use what
- [Troubleshooting](../../resources/troubleshooting.md) - Common issues and fixes
- [CLI Commands](../../resources/cli-commands.md) - Command cheatsheet

---

*End of Week 6 Session Plan*
*Next Session: Week 7 - Plugins (The Complete Package)*

---

**Next:** [Week 7: Plugins](../week-7/README.md)
