# Decision Trees for Claude Code

When should you use what? These flowcharts help you make the right choice.

---

## 1. Should I Use Plan Mode?

Use **Plan Mode** (`Shift+Tab` or `/plan`) for complex changes. Skip it for quick fixes.

```
START: What are you asking Claude to do?
  │
  ├─► Single file, < 20 lines change?
  │     └─► NO plan mode needed. Just ask directly.
  │
  ├─► Multiple files involved?
  │     └─► YES, use plan mode.
  │
  ├─► Architectural decision needed?
  │     └─► YES, use plan mode.
  │
  ├─► You're unsure how to approach it?
  │     └─► YES, use plan mode. Get a plan first.
  │
  ├─► Refactoring across the codebase?
  │     └─► YES, definitely plan mode.
  │
  └─► Quick bug fix with obvious solution?
        └─► NO plan mode needed.
```

### Plan Mode Checklist

Use plan mode if ANY of these are true:
- [ ] Touching 3+ files
- [ ] Adding a new feature (not just fixing)
- [ ] You want to review the approach before Claude executes
- [ ] The task has multiple valid approaches
- [ ] You're working on unfamiliar code

### Plan Mode Commands

| Command | What It Does |
|---------|--------------|
| `Shift+Tab` | Toggle plan mode on/off |
| `/plan` | Enter plan mode explicitly |
| `/plan off` | Exit plan mode |

---

## 2. Command vs Skill vs Agent vs Plugin

Choose the right tool for your automation needs.

```
START: What do you need?
  │
  ├─► A reusable prompt I invoke manually?
  │     │
  │     ├─► Simple, no special features needed?
  │     │     └─► COMMAND (.claude/commands/)
  │     │
  │     └─► Needs auto-discovery by Claude?
  │           └─► SKILL (.claude/skills/)
  │
  ├─► A specialized persona for specific tasks?
  │     └─► AGENT (.claude/agents/)
  │
  ├─► Automation that runs without me asking?
  │     └─► HOOK (settings.json or .claude/hooks/)
  │
  └─► Share these tools with others/other projects?
        └─► PLUGIN (package everything)
```

### Feature Comparison

| Feature | Command | Skill | Agent | Hook |
|---------|---------|-------|-------|------|
| Manually invoked | Yes | Yes | Yes | No |
| Auto-discovered by Claude | No | Yes | No | N/A |
| Custom persona/instructions | No | No | Yes | No |
| Accepts arguments | Yes | Yes | Yes | N/A |
| Runs automatically | No | Optional | No | Yes |
| Can be packaged in plugin | Yes | Yes | Yes | Yes |

### When to Use What

**COMMAND** - Best for:
- Report generation (e.g., `/violation-report`)
- Code scaffolding (e.g., `/new-service`)
- Standardized prompts (e.g., `/code-review`)

**SKILL** - Best for:
- Tasks Claude should know about and suggest
- Complex operations that need user confirmation
- Anything you'd want Claude to offer proactively

**AGENT** - Best for:
- Specialized roles (code reviewer, security auditor)
- Different thinking styles for different tasks
- Personas with unique instructions/expertise

**HOOK** - Best for:
- Enforcement (run linter after every change)
- Logging/auditing (track all AI interactions)
- Validation (reject changes that break tests)

**PLUGIN** - Best for:
- Sharing your tools with teammates
- Distributing to other projects
- Publishing to the community

---

## 3. When to Add Structure to Prompts

Not all prompts need XML tags or elaborate formatting.

```
START: How complex is your request?
  │
  ├─► One-liner question or simple task?
  │     └─► NO structure needed. Just ask naturally.
  │
  ├─► Multiple requirements to track?
  │     └─► Use a NUMBERED LIST.
  │
  ├─► Need to separate context from instructions?
  │     └─► Use XML TAGS for clarity.
  │
  ├─► Providing examples for Claude to follow?
  │     └─► Use CODE BLOCKS or XML EXAMPLES.
  │
  └─► Complex multi-part task with data?
        └─► Use FULL STRUCTURED FORMAT.
```

### Structure Levels

**Level 0: No Structure**
```
Add a CreatedDate property to the Violation entity
```

**Level 1: Numbered List**
```
Create a payment service that:
1. Accepts credit card payments
2. Validates card number format
3. Stores transaction in database
4. Sends confirmation email
```

**Level 2: XML Tags**
```xml
<context>
Working on the violation tracking module.
Violations escalate at 30, 60, 90 days.
</context>

<task>
Create a method that calculates the current escalation level
based on violation date.
</task>

<requirements>
- Return enum: Notice, Warning, Fine, Legal
- Include unit tests
- Use DateTimeOffset, not DateTime
</requirements>
```

**Level 3: Full Structured Format**
```xml
<system>
You are implementing a late fee calculator for HOA payments.
</system>

<context>
- Late fees: 10% monthly compound interest
- Grace period: 30 days
- Maximum fee cap: $1000
</context>

<task>
Implement CalculateLateFee method
</task>

<requirements>
- Input: principal, dueDate, calculationDate
- Output: decimal lateFee
- TDD with 80-90% coverage
- Handle edge cases (same day, negative amounts)
</requirements>

<example>
Principal: $500, 60 days late
Fee: $500 × (1.10)^2 - $500 = $105
</example>
```

### When to Use Structure

| Situation | Recommended Structure |
|-----------|----------------------|
| Quick question | None |
| Simple code change | None or numbered list |
| Feature with requirements | Numbered list or XML |
| Multiple code examples | XML with code blocks |
| Complex domain context | Full XML structure |
| Reusable prompt template | Full XML structure |

---

## 4. Which Model? (Sonnet vs Opus)

Choose based on task complexity.

```
START: What's the task?
  │
  ├─► Standard development: features, bugs, tests, refactoring?
  │     └─► SONNET (default) ← Use this 90% of the time
  │
  ├─► Complex architecture, novel problems, nuanced analysis?
  │     └─► OPUS (most capable)
  │
  └─► Unsure?
        └─► Start with SONNET, switch to OPUS if needed
```

### Model Comparison

| Factor | Sonnet | Opus |
|--------|--------|------|
| Speed | Fast | Moderate |
| Code quality | Great | Excellent |
| Complex reasoning | Strong | Best |
| Context handling | Great | Excellent |
| Best for | Daily work | Hard problems |

### Model Selection Guide

**Use SONNET (default) when:**
- Writing features with tests
- Bug investigation and fixing
- Code review assistance
- Refactoring and cleanup
- Documentation generation
- Most daily development tasks

**Use OPUS when:**
- Designing system architecture
- Complex algorithmic problems
- Novel or unusual requirements
- Debugging mysterious issues
- Multi-file changes with complex dependencies
- Security analysis
- Tasks requiring nuanced judgment

### Switch Models in Session

```bash
# Check current model
/model

# Switch to Opus (complex tasks)
/model opus

# Back to default (Sonnet)
/model sonnet
```

---

## 5. When to Bail on Claude

Recognize when a conversation isn't productive and start fresh.

```
START: Is the conversation going well?
  │
  ├─► Claude keeps repeating the same approach that already failed?
  │     └─► BAIL. Use /clear and try a different angle.
  │
  ├─► 3+ attempts at same task with no progress?
  │     └─► BAIL. Break the problem down differently.
  │
  ├─► Claude is hallucinating APIs/methods that don't exist?
  │     └─► BAIL. Provide explicit context or code snippets.
  │
  ├─► Response quality degrading (getting worse, not better)?
  │     └─► BAIL. Context may be polluted. Start fresh.
  │
  ├─► Claude saying "you're right" but still doing the wrong thing?
  │     └─► BAIL. Reframe the problem from scratch.
  │
  └─► Making progress but slowly?
        └─► CONTINUE. Persistence often pays off.
```

### Signs of a Circular Conversation

🚩 **Red Flags:**
- "I apologize for the confusion" appears 3+ times
- Same code pattern keeps appearing despite corrections
- Claude agrees with your feedback but produces identical output
- Explanations get longer but code doesn't improve
- You're explaining the same constraint repeatedly

### Recovery Strategies

| Situation | Strategy |
|-----------|----------|
| Circular conversation | `/clear` and rephrase from scratch |
| Wrong approach | Try a completely different angle |
| Missing context | Add specific code/docs to prompt |
| Complex task | Break into smaller pieces |
| Hallucinations | Provide explicit examples |
| Quality degrading | `/compact` or start new session |

### The 3-Strike Rule

After 3 failed attempts at the same task:
1. **Stop** - Don't keep trying the same thing
2. **Analyze** - What's Claude missing?
3. **Reframe** - Try a fundamentally different approach
4. **Simplify** - Break into smaller steps

---

## 6. Quick Decision Reference

| I want to... | Use... |
|--------------|--------|
| Make Claude plan before acting | Plan Mode (`Shift+Tab`) |
| Create a reusable prompt | Command |
| Let Claude auto-suggest a task | Skill |
| Create a specialized persona | Agent |
| Auto-run something after changes | Hook |
| Share tools with team | Plugin |
| Complex reasoning needed | Opus model |
| Everything else | Sonnet model (default) |
| Conversation going in circles | `/clear` and start fresh |

---

*Still unsure? Ask in `#dx-training` or start with the defaults and adjust based on results!*
