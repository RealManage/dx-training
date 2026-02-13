# Week 6 Solutions: Agents & Hooks Reference

This document provides reference implementations and expected outputs for Week 6 exercises.

---

## Exercise 1: Create a Code Reviewer Agent

### Expected File: `.claude/agents/code-reviewer.md`

```markdown
---
name: code-reviewer
description: Security-focused code review specialist
model: sonnet
permissionMode: plan
tools:
  - Read
  - Glob
  - Grep
---

You are a senior code reviewer specializing in security and code quality for C# .NET applications.

## Your Responsibilities
1. Review code for security vulnerabilities (OWASP Top 10)
2. Check for code quality issues (SOLID, DRY, KISS)
3. Verify error handling and null safety
4. Assess test coverage adequacy
5. Identify performance concerns

## Review Format
For each issue found, report:
- **File:Line** - Location
- **Severity** - Critical/High/Medium/Low
- **Issue** - What's wrong
- **Fix** - How to resolve
- **Reference** - Why this matters

## Review Priorities
1. Security vulnerabilities (Critical)
2. Logic bugs (High)
3. Error handling gaps (Medium)
4. Code style issues (Low)

## Constraints
- Only READ files, never modify
- Use plan mode for complex analysis
- Flag but don't fix - let developers own the changes
```

### How to Test

```bash
claude
> /agent code-reviewer
> Review the ViolationService for security issues
```

### Success Criteria

- [ ] Agent loads with correct permissions
- [ ] Uses plan mode (review-only)
- [ ] Cannot edit files (plan permission mode)
- [ ] Produces structured review output

---

## Exercise 2: Create an Audit Logging Hook

### Expected File: `.claude/settings.json`

```json
{
  "hooks": {
    "PostToolUse": [
      {
        "matcher": "Edit",
        "hooks": [
          {
            "type": "command",
            "command": "echo \"$(date -Iseconds) | EDIT | $TOOL_INPUT\" >> .claude/audit.log"
          }
        ]
      },
      {
        "matcher": "Write",
        "hooks": [
          {
            "type": "command",
            "command": "echo \"$(date -Iseconds) | WRITE | $TOOL_INPUT\" >> .claude/audit.log"
          }
        ]
      }
    ]
  }
}
```

### Expected Audit Log Output

```text
2026-01-23T10:15:30-06:00 | EDIT | {"file_path": "/src/Services/ViolationService.cs", "old_string": "...", "new_string": "..."}
2026-01-23T10:15:45-06:00 | WRITE | {"file_path": "/src/Models/Violation.cs", "content": "..."}
```

### Success Criteria

- [ ] Hook triggers after every Edit operation
- [ ] Hook triggers after every Write operation
- [ ] Timestamps are ISO 8601 format
- [ ] Audit log is append-only

---

## Exercise 3: Block Dangerous Operations

### Expected File: `.claude/settings.json` (hooks section)

```json
{
  "hooks": {
    "PreToolUse": [
      {
        "matcher": "Bash",
        "hooks": [
          {
            "type": "command",
            "command": ".claude/hooks/block-dangerous-commands.sh"
          }
        ]
      },
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

**Hook script:** `.claude/hooks/block-dangerous-commands.sh`

```bash
#!/bin/bash
# Reads $TOOL_INPUT from stdin (JSON), checks for dangerous patterns
INPUT=$(cat)
COMMAND=$(echo "$INPUT" | jq -r '.command // empty')

if echo "$COMMAND" | grep -qEi 'rm -rf|rm -r /|DROP TABLE|TRUNCATE|format c:|del /f /s'; then
  echo '{"hookSpecificOutput":{"hookEventName":"PreToolUse","permissionDecision":"deny","permissionDecisionReason":"BLOCKED: Dangerous operation detected. This action requires manual approval."}}'
fi
```

**Hook script:** `.claude/hooks/block-sensitive-files.sh`

```bash
#!/bin/bash
# Reads $TOOL_INPUT from stdin (JSON), checks for sensitive file paths
INPUT=$(cat)
FILE_PATH=$(echo "$INPUT" | jq -r '.file_path // empty')

if echo "$FILE_PATH" | grep -qEi '\.env|\.pem|credentials|secrets|password'; then
  echo '{"hookSpecificOutput":{"hookEventName":"PreToolUse","permissionDecision":"deny","permissionDecisionReason":"BLOCKED: Attempted edit of sensitive file. Security review required."}}'
fi
```

> **Note:** The `matcher` field is a regex that matches tool names only. To filter based on tool *input* (e.g., specific commands or file paths), use a hook script that inspects `$TOOL_INPUT` and returns a `permissionDecision`.

### Test Commands (Should Be Blocked)

```bash
# In Claude session, these should trigger blocks:
> Run rm -rf /tmp/test
# Expected: BLOCKED message

> Edit the .env file to add a new key
# Expected: BLOCKED message
```

### Success Criteria

- [ ] `rm -rf` patterns blocked
- [ ] Sensitive file edits blocked
- [ ] Clear error messages shown
- [ ] Legitimate operations still work

---

## Exercise 4: Auto-Run Tests After Edits

### Expected File: `.claude/settings.json` (hooks section)

```json
{
  "hooks": {
    "PostToolUse": [
      {
        "matcher": "Edit",
        "hooks": [
          {
            "type": "command",
            "command": ".claude/hooks/test-on-cs-edit.sh"
          }
        ]
      }
    ]
  }
}
```

**Hook script:** `.claude/hooks/test-on-cs-edit.sh`

```bash
#!/bin/bash
# Only run tests when a .cs file was edited
INPUT=$(cat)
FILE_PATH=$(echo "$INPUT" | jq -r '.file_path // empty')

if [[ "$FILE_PATH" == *.cs ]]; then
  dotnet test --no-build --verbosity minimal 2>&1 | tail -20
fi
```

> **Note:** Since `matcher` only filters by tool name, file-type filtering happens inside the hook script.

### Expected Behavior

After any `.cs` file is edited:

1. Hook triggers automatically
2. Script checks if the edited file is `.cs`
3. Tests run without full rebuild
4. Last 20 lines of output shown
5. Failures are immediately visible

### Success Criteria

- [ ] Hook triggers on Edit, script filters to C# files only
- [ ] Tests run automatically for `.cs` edits
- [ ] Non-C# edits are ignored
- [ ] Output is concise (last 20 lines)
- [ ] Failures are visible to Claude

---

## Complete settings.json Example

```json
{
  "permissions": {
    "allow": ["Read", "Glob", "Grep", "Edit", "Write", "Bash"],
    "deny": []
  },
  "hooks": {
    "PreToolUse": [
      {
        "matcher": "Bash",
        "hooks": [
          {
            "type": "command",
            "command": ".claude/hooks/block-dangerous-commands.sh"
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
            "command": ".claude/hooks/test-on-cs-edit.sh"
          }
        ]
      },
      {
        "matcher": "Edit",
        "hooks": [
          {
            "type": "command",
            "command": "echo \"$(date -Iseconds) | EDIT | $TOOL_INPUT\" >> .claude/audit.log"
          }
        ]
      }
    ]
  }
}
```

---

## Common Mistakes to Avoid

1. **Incorrect matcher syntax**

   ```json
   // Wrong - matcher as object
   "matcher": { "tool": "Edit" }

   // Correct - matcher is a regex string matching tool names
   "matcher": "Edit"

   // Match multiple tools with regex
   "matcher": "Edit|Write"
   ```

2. **Trying to filter tool input in the matcher**

   ```json
   // Wrong - "input" is not a matcher field
   "matcher": { "tool": "Bash", "input": "rm -rf" }

   // Correct - matcher filters by tool name only
   // Use a hook script to inspect $TOOL_INPUT for input filtering
   "matcher": "Bash",
   "hooks": [{ "type": "command", "command": ".claude/hooks/my-filter.sh" }]
   ```

3. **Missing hook type**

   ```json
   // Wrong - no type specified
   "hooks": [{ "command": "..." }]

   // Correct - type specified
   "hooks": [{ "type": "command", "command": "..." }]
   ```

4. **Agent permission mismatch**

   ```markdown
   # Wrong - trying to edit with plan mode
   permissionMode: plan
   tools:
     - Edit  # This won't work!

   # Correct - plan mode is read-only
   permissionMode: plan
   tools:
     - Read
     - Glob
   ```

---

## Self-Check Questions

Before moving to Week 7, verify:

1. Can you create an agent with specific tools and permissions?
2. Can you configure hooks for PreToolUse and PostToolUse?
3. Do you understand the difference between `block` and `command` hook types?
4. Can you explain when to use an agent vs a hook?

---

*Solutions verified: February 2026*
