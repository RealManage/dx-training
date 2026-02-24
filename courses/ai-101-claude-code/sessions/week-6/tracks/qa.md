# Week 6: Agents & Hooks - QA Track

**Duration:** 2 hours (Part 4 exercises are self-paced)
**Audience:** QA Engineers, Test Engineers, SDET

This track focuses on test automation hooks and read-only analysis agents. Agent creation complexity is simplified.

---

## Track Overview

As a QA engineer, you'll focus on:

- **Hooks for test automation** - Auto-run tests after code changes
- **Analysis agents** - Analyze code for testability without modifying it
- **Audit hooks** - Track what Claude is doing during sessions
- **Custom QA agents** - Build specialized agents for test generation and coverage analysis

---

## Part 1: Understanding Agents (20 min)

### What Are Agents?

Agents are specialized AI assistants. For QA, the most useful agents are:

| Agent | Purpose | Your Use Case |
| ----- | ------- | ------------- |
| test-writer | Creates unit tests | Generate test cases |
| code-reviewer | Reviews code quality | Check testability |
| security-auditor | Finds vulnerabilities | Security testing |

### Using Pre-Built Agents

You don't need to create agents from scratch. Use team-provided agents:

```text
> Use the test-writer agent to create tests for ViolationService

> Use the code-reviewer agent to check this file for testability issues
```

### Key Agent Concepts for QA

**Permission Modes:**

| Mode | What It Means |
| ---- | ------------- |
| `default` | Standard - asks for approval before tool use |
| `plan` | Analysis only - no tool execution, cannot make changes |
| `acceptEdits` | Auto-approves file edits (use in sandbox/trusted contexts) |
| `dontAsk` | Auto-denies permission prompts (read-only, won't modify anything) |
| `bypassPermissions` | Auto-approves everything (dangerous - use with caution) |

> **Note:** Permission modes **restrict** agent behavior but cannot **escalate** beyond your session's permission settings.

> **Tip:** Use `plan` mode for code analysis and review agents. For test-writing agents, use `default` mode so you can approve each edit, or `acceptEdits` when working in a sandbox.

---

## Part 2: Test Automation Hooks (45 min)

This is the core content for QA engineers. Hooks let you automate test execution.

### Basic Test Hook

Create `.claude/settings.json`:

```json
{
  "hooks": {
    "PostToolUse": [
      {
        "matcher": "Edit",
        "hooks": [
          {
            "type": "command",
            "command": "echo '\\n=== AUTO-RUNNING TESTS ===' && dotnet test --no-build --verbosity minimal"
          }
        ]
      }
    ]
  }
}
```

**What this does:**

- After ANY file edit, automatically runs `dotnet test`
- Shows test results in the conversation
- Catches regressions immediately

### Enhanced Test Hook with Coverage

```json
{
  "hooks": {
    "PostToolUse": [
      {
        "matcher": "Edit",
        "hooks": [
          {
            "type": "command",
            "command": "echo '\\n=== RUNNING TESTS ===' && dotnet test --no-build --collect:\"XPlat Code Coverage\" --verbosity minimal 2>&1 | tail -15"
          }
        ]
      }
    ]
  }
}
```

### Test-Focused Hook Set

Complete hook configuration for QA workflows. Hooks that need to read event data use script files; simple hooks use inline commands:

```json
{
  "hooks": {
    "PreToolUse": [
      {
        "matcher": "*",
        "hooks": [
          {
            "type": "command",
            "command": ".claude/hooks/qa-audit-log.sh"
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
            "command": "echo '=== RUNNING TESTS ===' && dotnet test --no-build --verbosity minimal 2>&1 | grep -E '(Passed|Failed|Total|Error)' || echo 'Test run complete'"
          }
        ]
      },
      {
        "matcher": "Write",
        "hooks": [
          {
            "type": "command",
            "command": ".claude/hooks/test-new-files.sh"
          }
        ]
      }
    ]
  }
}
```

**Hook scripts:**

**Bash (Mac/Linux/WSL):**

```bash
#!/bin/bash
# .claude/hooks/qa-audit-log.sh
mkdir -p .claude
TOOL=$(jq -r '.tool_name')
INPUT=$(jq -r '.tool_input | tostring')
echo "$(date -Iseconds) | $TOOL | $INPUT" >> .claude/qa-audit.log
```

```bash
#!/bin/bash
# .claude/hooks/test-new-files.sh - Run tests when a new test file is written
FILE_PATH=$(jq -r '.tool_input.file_path // empty')

if echo "$FILE_PATH" | grep -qi 'test'; then
  echo '=== NEW TEST FILE - Running Tests ==='
  dotnet test --no-build --verbosity minimal
fi
```

**PowerShell (Windows):**

```powershell
# .claude/hooks/qa-audit-log.ps1
$hookData = [Console]::In.ReadToEnd() | ConvertFrom-Json
$tool = $hookData.tool_name
$input = $hookData.tool_input | ConvertTo-Json -Compress
$timestamp = Get-Date -Format "o"
if (-not (Test-Path ".claude")) { New-Item -ItemType Directory -Path ".claude" | Out-Null }
Add-Content -Path ".claude/qa-audit.log" -Value "$timestamp | $tool | $input"
```

```powershell
# .claude/hooks/test-new-files.ps1 - Run tests when a new test file is written
$hookData = [Console]::In.ReadToEnd() | ConvertFrom-Json
$filePath = $hookData.tool_input.file_path

if ($filePath -match 'test') {
    Write-Output '=== NEW TEST FILE - Running Tests ==='
    dotnet test --no-build --verbosity minimal
}
```

> **Windows settings.json:** Use `"command": "powershell -NoProfile -File .claude/hooks/qa-audit-log.ps1"` (and similarly for test-new-files).

### Hook Input for Test Hooks

Hooks receive JSON via stdin. Use `jq` to extract fields:

| Stdin Field | Use in Test Hooks |
| ----------- | ----------------- |
| `tool_input.file_path` | File being edited — filter to run only relevant tests |
| `tool_name` | Operation type — distinguish Edit vs Write |
| `cwd` | Project root — construct test paths |

### Conditional Test Execution

Only run tests when C# files change (uses a script to read stdin):

```json
{
  "hooks": {
    "PostToolUse": [
      {
        "matcher": "Edit",
        "hooks": [
          {
            "type": "command",
            "command": ".claude/hooks/test-cs-edits.sh"
          }
        ]
      }
    ]
  }
}
```

**Bash (Mac/Linux/WSL):** `.claude/hooks/test-cs-edits.sh`

```bash
#!/bin/bash
FILE_PATH=$(jq -r '.tool_input.file_path // empty')

if [[ "$FILE_PATH" == *.cs ]]; then
  echo '=== C# FILE CHANGED - Running Tests ==='
  dotnet test --no-build --verbosity minimal
else
  echo 'Non-C# file edited, skipping tests'
fi
```

**PowerShell (Windows):** `.claude/hooks/test-cs-edits.ps1`

```powershell
$hookData = [Console]::In.ReadToEnd() | ConvertFrom-Json
$filePath = $hookData.tool_input.file_path

if ($filePath -match '\.cs$') {
    Write-Output '=== C# FILE CHANGED - Running Tests ==='
    dotnet test --no-build --verbosity minimal
} else {
    Write-Output 'Non-C# file edited, skipping tests'
}
```

> **Windows settings.json:** Use `"command": "powershell -NoProfile -File .claude/hooks/test-cs-edits.ps1"`

---

## Part 3: QA-Specific Agents (20 min)

### Using the Test Writer Agent

If your team has a test-writer agent, use it like this:

```text
> Use the test-writer agent to create tests for the CalculateLateFee method

> Use the test-writer agent to add edge case tests for ViolationService
```

### Simple QA Analysis Agent

If you need to create a simple QA agent, use this template:

```markdown
---
name: testability-checker
description: Analyze code for testability issues
tools: Read, Grep, Glob
model: default
permissionMode: plan
---

You are a QA engineer analyzing code testability.

Check for:
1. Methods with too many dependencies (>3 constructor params)
2. Static method calls that are hard to mock
3. Missing interfaces for dependencies
4. Long methods (>50 lines)
5. Nested conditionals (>3 levels deep)

Report findings as:
- FILE: path/to/file.cs
- ISSUE: Description
- IMPACT: Why this hurts testability
- SUGGESTION: How to improve
```

**Usage:**

```text
> Use the testability-checker agent to analyze the Services folder
```

---

## Part 4: Hands-On Exercises (25 min) - *Self-Paced*

> **Note:** Part 4 is self-paced practice. Complete during the session if time permits, otherwise finish as homework.

### Exercise 1: Basic Test Hook (10 min)

Create `.claude/settings.json`:

```json
{
  "hooks": {
    "PostToolUse": [
      {
        "matcher": "Edit",
        "hooks": [
          {
            "type": "command",
            "command": "dotnet test --no-build --verbosity minimal 2>&1 | tail -5"
          }
        ]
      }
    ]
  }
}
```

Test it:

```text
> Edit Program.cs to add a comment
# Watch for automatic test execution
```

### Exercise 2: Audit Logging (10 min)

Add PreToolUse logging with a hook script:

```json
{
  "hooks": {
    "PreToolUse": [
      {
        "matcher": "*",
        "hooks": [
          {
            "type": "command",
            "command": ".claude/hooks/qa-operations-log.sh"
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
            "command": "dotnet test --no-build --verbosity minimal 2>&1 | tail -5"
          }
        ]
      }
    ]
  }
}
```

**Bash (Mac/Linux/WSL):** `.claude/hooks/qa-operations-log.sh`

```bash
#!/bin/bash
mkdir -p .claude
TOOL=$(jq -r '.tool_name')
echo "$(date -Iseconds) | $TOOL" >> .claude/qa-operations.log
```

**PowerShell (Windows):** `.claude/hooks/qa-operations-log.ps1`

```powershell
$hookData = [Console]::In.ReadToEnd() | ConvertFrom-Json
$tool = $hookData.tool_name
$timestamp = Get-Date -Format "o"
if (-not (Test-Path ".claude")) { New-Item -ItemType Directory -Path ".claude" | Out-Null }
Add-Content -Path ".claude/qa-operations.log" -Value "$timestamp | $tool"
```

> **Windows settings.json:** Use `"command": "powershell -NoProfile -File .claude/hooks/qa-operations-log.ps1"`

Test it:

```text
> Read a file
> Edit a file
> cat .claude/qa-operations.log
```

### Exercise 3: Use a Test Agent (5 min)

If a test-writer agent is available:

```text
> Use the test-writer agent to create edge case tests for late fee calculations
```

---

## Key Takeaways for QA

### Hooks Are Your Best Friend

- Auto-run tests after every edit
- Catch regressions immediately
- Log all operations for debugging

### Agents for Analysis

- Use `permissionMode: plan` for safe read-only analysis
- Pre-built agents are easier than creating your own
- Focus on testability-checker and code-reviewer agents

### Essential Hook Patterns

| Pattern | Hook Type | Use Case |
| ------- | --------- | -------- |
| Auto-test | PostToolUse on Edit | Run tests after changes |
| Audit log | PreToolUse on * | Track all operations |
| Coverage | PostToolUse on Edit | Check coverage after changes |

---

## Homework

### Required

1. Configure a PostToolUse hook that runs tests after edits
2. Configure a PreToolUse hook that logs all operations
3. Use a test-writer or code-reviewer agent on a codebase

### Optional

1. Create a testability-checker agent
2. Configure conditional test execution (only for .cs files)
3. Set up coverage reporting in hooks

---

*Return to [Week 6 README](../README.md#-key-takeaways) | Next: [Week 7 - Plugins](../../week-7/tracks/qa.md)*
