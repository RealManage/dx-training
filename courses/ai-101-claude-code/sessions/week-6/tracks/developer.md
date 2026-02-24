# Week 6: Agents & Hooks - Developer Track

**Duration:** 2 hours (Part 3 exercises are self-paced)
**Audience:** Software Engineers, Full-Stack Developers, Backend Developers

This is the full content track for developers. All sections are relevant to your daily work.

---

## Track Overview

As a developer, Agents and Hooks are powerful tools for:

- **Code reviews** - Automated quality checks before commits
- **Security audits** - Continuous vulnerability scanning
- **Test automation** - Auto-run tests after every edit
- **Compliance** - SOC 2 audit trails for all operations

---

## Part 1: Custom Agents (55 min)

### What Are Agents?

Agents are specialized AI assistants with their own context, tools, and permissions. Think of them as team members with specific roles:

| Agent Type | Purpose | Tools Allowed |
| ---------- | ------- | ------------- |
| code-reviewer | Review PRs for quality | Read, Grep, Glob |
| security-auditor | Find vulnerabilities | Read, Grep, Glob |
| test-writer | Write unit tests | Read, Write, Edit, Bash |
| db-analyst | Analyze queries | Read, Grep, Glob |

### Agent File Structure

**Location:** `.claude/agents/<name>.md`

```markdown
---
name: code-reviewer
description: Reviews code for quality, security, and best practices
tools: Read, Glob, Grep, Bash
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

### Permission Modes

| Mode | Description | Use Case |
| ---- | ----------- | -------- |
| `default` | Asks for approval before tool use | Normal operations - you approve each action |
| `plan` | No tool execution allowed | Analysis, code review, architecture planning |
| `acceptEdits` | Auto-approves file edits | Trusted refactoring in sandbox/isolated dirs |
| `dontAsk` | Auto-denies permission prompts | Read-only agents that shouldn't modify anything |
| `bypassPermissions` | Auto-approves all tools | Fully automated pipelines (use with caution) |

> **Note:** Permission modes can **restrict** agent behavior but cannot **escalate** beyond your session's permission settings.

> **Tip:** Use `plan` mode for analysis agents, `default` for normal work where you approve actions, and `acceptEdits` only in sandboxed/isolated environments.

### Developer Agent Examples

**Security Auditor:**

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

**Test Writer:**

```markdown
---
name: test-writer
description: Write unit tests following TDD principles
tools: Read, Write, Edit, Bash
model: sonnet
permissionMode: acceptEdits
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

---

## Part 2: Hooks for Automation (55 min)

### What Are Hooks?

Hooks are automated actions that run before or after Claude Code operations. They enable:

- **Validation** - Block dangerous commands
- **Logging** - Audit trail for compliance
- **Automation** - Auto-run tests, lint, etc.

### Hook Configuration

**Location:** `.claude/settings.json`

Hooks receive a **JSON object via stdin** with event details. Use `jq` to extract fields in your hook scripts. For hooks that don't need to read input (like running tests), simple inline commands work fine.

```json
{
  "hooks": {
    "PreToolUse": [
      {
        "matcher": "Edit",
        "hooks": [
          {
            "type": "command",
            "command": "echo 'File edit starting...'"
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
            "command": "dotnet test --no-build --verbosity quiet"
          }
        ]
      }
    ]
  }
}
```

### Essential Hooks for Developers

**1. Auto-Run Tests After Edits:**

```json
{
  "hooks": {
    "PostToolUse": [
      {
        "matcher": "Edit",
        "hooks": [
          {
            "type": "command",
            "command": "echo '=== Running Tests ===' && dotnet test --no-build --verbosity minimal 2>&1 | tail -10"
          }
        ]
      }
    ]
  }
}
```

**2. Block Dangerous Operations:**

Since `matcher` only filters by tool name, match on `"Bash"` and read stdin JSON in a hook script:

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

if echo "$COMMAND" | grep -qEi 'rm -rf|git push.*--force'; then
  echo "BLOCKED: Dangerous operation detected" >&2
  exit 2  # Exit 2 blocks the tool call
fi

exit 0
```

**PowerShell (Windows):** `.claude/hooks/block-dangerous.ps1`

```powershell
$hookData = [Console]::In.ReadToEnd() | ConvertFrom-Json
$command = $hookData.tool_input.command

if ($command -match 'rm -rf|git push.*--force') {
    [Console]::Error.WriteLine("BLOCKED: Dangerous operation detected")
    exit 2
}

exit 0
```

> **Windows settings.json:** Use `"command": "powershell -NoProfile -File .claude/hooks/block-dangerous.ps1"`

**3. SOC 2 Audit Trail:**

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

**Bash (Mac/Linux/WSL):** `.claude/hooks/audit-log.sh`

```bash
#!/bin/bash
mkdir -p .claude
TOOL=$(jq -r '.tool_name')
INPUT=$(jq -r '.tool_input | tostring')
echo "$(date -Iseconds) | PRE | $TOOL | $INPUT" >> .claude/audit.log
```

**Bash (Mac/Linux/WSL):** `.claude/hooks/audit-log-post.sh`

```bash
#!/bin/bash
TOOL=$(jq -r '.tool_name')
echo "$(date -Iseconds) | POST | $TOOL | SUCCESS" >> .claude/audit.log
```

**PowerShell (Windows):** `.claude/hooks/audit-log.ps1`

```powershell
$hookData = [Console]::In.ReadToEnd() | ConvertFrom-Json
$tool = $hookData.tool_name
$input = $hookData.tool_input | ConvertTo-Json -Compress
$timestamp = Get-Date -Format "o"
if (-not (Test-Path ".claude")) { New-Item -ItemType Directory -Path ".claude" | Out-Null }
Add-Content -Path ".claude/audit.log" -Value "$timestamp | PRE | $tool | $input"
```

**PowerShell (Windows):** `.claude/hooks/audit-log-post.ps1`

```powershell
$hookData = [Console]::In.ReadToEnd() | ConvertFrom-Json
$tool = $hookData.tool_name
$timestamp = Get-Date -Format "o"
Add-Content -Path ".claude/audit.log" -Value "$timestamp | POST | $tool | SUCCESS"
```

> **Windows settings.json:** Use `"command": "powershell -NoProfile -File .claude/hooks/audit-log.ps1"` (and similarly for the post hook).

**4. Protect Sensitive Files:**

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

if echo "$FILE_PATH" | grep -qEi '\.env|\.pem|credentials|secrets'; then
  echo "BLOCKED: Cannot edit sensitive file: $FILE_PATH" >&2
  exit 2
fi

exit 0
```

**PowerShell (Windows):** `.claude/hooks/block-sensitive-files.ps1`

```powershell
$hookData = [Console]::In.ReadToEnd() | ConvertFrom-Json
$filePath = $hookData.tool_input.file_path

if ($filePath -match '\.(env|pem)|credentials|secrets') {
    [Console]::Error.WriteLine("BLOCKED: Cannot edit sensitive file: $filePath")
    exit 2
}

exit 0
```

> **Windows settings.json:** Use `"command": "powershell -NoProfile -File .claude/hooks/block-sensitive-files.ps1"`

### Hook Input (stdin JSON)

Hooks receive JSON via stdin, **not** environment variables. Use `jq` (Mac/Linux/WSL) or `ConvertFrom-Json` (Windows PowerShell) to parse:

| Stdin Field | Description | Available In |
| ----------- | ----------- | ------------ |
| `tool_name` | Name of tool being used | PreToolUse, PostToolUse |
| `tool_input` | Input to the tool (JSON object) | PreToolUse, PostToolUse |
| `tool_response` | Output from tool | PostToolUse only |
| `session_id` | Current session identifier | All hooks |
| `cwd` | Working directory | All hooks |

> **Blocking:** `exit 2` blocks the operation (stderr message is shown to Claude). `exit 0` allows it. `exit 1` is a non-blocking error.

---

## Part 3: Hands-On Exercises (30 min) - *Self-Paced*

> **Note:** Part 3 is self-paced practice. Complete during the session if time permits, otherwise finish as homework.

### Setup Your Sandbox

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

### Exercise 1: Create a Code Reviewer Agent

Create `.claude/agents/code-reviewer.md` with:

- Read-only tools (Read, Grep, Glob, Bash)
- `permissionMode: plan`
- Checklist covering code quality, security, and RealManage standards

Test it:

```text
> Use the code-reviewer agent to review the Services folder
```

### Exercise 2: Create Post-Edit Test Hook

Create `.claude/settings.json` with a PostToolUse hook that:

- Triggers on Edit operations
- Runs `dotnet test --no-build`
- Shows last 5 lines of output

Test it:

```text
> Edit Program.cs to add a comment
# Watch for automatic test execution
```

### Exercise 3: Build Complete Audit System

Expand your hooks to include:

- PreToolUse logging for all operations
- PostToolUse logging for edits
- Block rm commands
- Auto-run tests after edits

Test:

```text
> Edit a file
> Try rm test.txt (should be blocked)
> cat .claude/audit.log
```

---

## Key Takeaways

### Agents

- Use agents for isolated, specialized tasks
- `permissionMode: plan` for safe read-only analysis
- Define tool restrictions to limit capabilities
- Choose models based on task complexity

### Hooks

- PreToolUse for validation and blocking
- PostToolUse for logging and automation
- Hooks receive JSON via stdin (use `jq` to parse)
- `exit 2` blocks the operation; `exit 0` allows
- Keep hooks fast (<1 second)

### When to Use What

| Need | Solution |
| ---- | -------- |
| Code review before merge | code-reviewer agent |
| Security vulnerability scan | security-auditor agent |
| Write tests automatically | test-writer agent |
| Block dangerous commands | PreToolUse hook with `exit 2` |
| Auto-run tests | PostToolUse hook on Edit |
| Audit trail | Pre/Post hooks with scripts |

---

## Homework

### Required

1. Create a code-reviewer agent for your project
2. Create a security-auditor agent
3. Configure hooks that auto-run tests after edits
4. Configure hooks that block rm -rf and force push
5. Set up SOC 2 audit logging

### Stretch

1. Create an agent with embedded hooks
2. Build a complete CI-like hook chain (lint -> test -> coverage)
3. Create domain-specific agents for HOA workflows

---

*Next: Week 7 - Plugins (Package everything for distribution)*
