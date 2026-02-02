# Week 6: Agents & Hooks - QA Track

**Duration:** 1.5 hours
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

```
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

Complete hook configuration for QA workflows:

```json
{
  "hooks": {
    "PreToolUse": [
      {
        "matcher": "*",
        "hooks": [
          {
            "type": "command",
            "command": "mkdir -p .claude && echo \"$(date -Iseconds) | $TOOL_NAME | $TOOL_INPUT\" >> .claude/qa-audit.log"
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
            "command": "echo '\\n=== RUNNING TESTS ===' && dotnet test --no-build --verbosity minimal 2>&1 | grep -E '(Passed|Failed|Total|Error)' || echo 'Test run complete'"
          }
        ]
      },
      {
        "matcher": "Write",
        "hooks": [
          {
            "type": "command",
            "command": "echo \"$TOOL_INPUT\" | grep -q 'Tests\\|Test' && echo '\\n=== NEW TEST FILE - Running Tests ===' && dotnet test --no-build --verbosity minimal || true"
          }
        ]
      }
    ]
  }
}
```

### Hook Variables for Test Hooks

| Variable | Use in Test Hooks |
| -------- | ----------------- |
| `$TOOL_INPUT` | File being edited - filter to run only relevant tests |
| `$TOOL_NAME` | Operation type - distinguish Edit vs Write |
| `$PROJECT_DIR` | Project root - construct test paths |

### Conditional Test Execution

Only run tests when C# files change:

```json
{
  "hooks": {
    "PostToolUse": [
      {
        "matcher": "Edit",
        "hooks": [
          {
            "type": "command",
            "command": "echo \"$TOOL_INPUT\" | grep -q '\\.cs$' && echo '=== C# FILE CHANGED - Running Tests ===' && dotnet test --no-build --verbosity minimal || echo 'Non-C# file edited, skipping tests'"
          }
        ]
      }
    ]
  }
}
```

---

## Part 3: QA-Specific Agents (20 min)

### Using the Test Writer Agent

If your team has a test-writer agent, use it like this:

```
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

```
> Use the testability-checker agent to analyze the Services folder
```

---

## Part 4: Hands-On Exercises (25 min)

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

```
> Edit Program.cs to add a comment
# Watch for automatic test execution
```

### Exercise 2: Audit Logging (10 min)

Add PreToolUse logging:

```json
{
  "hooks": {
    "PreToolUse": [
      {
        "matcher": "*",
        "hooks": [
          {
            "type": "command",
            "command": "mkdir -p .claude && echo \"$(date -Iseconds) | $TOOL_NAME\" >> .claude/qa-operations.log"
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

Test it:

```
> Read a file
> Edit a file
> cat .claude/qa-operations.log
```

### Exercise 3: Use a Test Agent (5 min)

If a test-writer agent is available:

```
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
