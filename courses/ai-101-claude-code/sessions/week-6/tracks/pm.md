# Week 6: Agents & Hooks - PM Track

**Duration:** 45-60 min
**Audience:** Product Managers, Project Managers, Business Analysts

---

## Learning Objectives

By the end of this session, you will be able to:

- Create agents specialized for PM workflows (requirements analysis, document review)
- Configure hooks for quality checks on PM deliverables
- Understand how agents and hooks improve consistency across your work

---

## Why This Matters for PMs

Agents and hooks aren't just for developers. As a PM, you can create:

- **Agents** that analyze requirements for completeness and ambiguity
- **Agents** that review user stories against team standards
- **Hooks** that validate acceptance criteria format before saving
- **Hooks** that check documentation quality automatically

This week, you'll build automation that improves your own workflow.

---

## Part 1: Understanding Agents for PM Work (15 min)

### What Can PM Agents Do?

| Agent Type | Purpose | Example Use |
| ---------- | ------- | ----------- |
| **Requirements Analyst** | Check requirements for gaps | "Analyze this PRD for missing edge cases" |
| **Story Reviewer** | Validate user story quality | "Review these stories against INVEST criteria" |
| **Document Analyzer** | Extract key info from docs | "Summarize decisions from this meeting notes file" |
| **Stakeholder Summarizer** | Create executive summaries | "Create a 1-page summary for leadership" |

### Agent Capabilities

Agents can be configured with:

- **Specific tools** - Read-only for analysis, or with edit capability
- **Custom prompts** - Your team's standards baked in
- **Model selection** - Fast models for simple tasks, powerful models for analysis

---

## Exercise 1: Create a Requirements Analyst Agent (20 min)

### Setup

```bash
cd courses/ai-101-claude-code/sessions/week-6
mkdir -p sandbox/pm-agents/.claude/agents
cd sandbox/pm-agents
claude
```

### Create the Agent

Ask Claude to help you create an agent:

```
Create a requirements analyst agent that:
1. Analyzes requirements documents for completeness
2. Identifies ambiguous language
3. Flags missing acceptance criteria
4. Checks for INVEST criteria compliance
5. Suggests edge cases that should be considered

Save it to .claude/agents/requirements-analyst.md
Use read-only tools (Read, Grep, Glob) and plan permission mode.
```

### Test Your Agent

Create a sample requirements file and test:

```
Create a sample requirements.md file with a feature request
that has some intentional gaps (missing edge cases, vague language).
Then use the requirements-analyst agent to analyze it.
```

### Success Criteria

- [ ] Agent created in `.claude/agents/requirements-analyst.md`
- [ ] Agent has read-only tools configured
- [ ] Agent successfully identifies gaps in sample requirements
- [ ] Output is organized by severity (Critical/Warning/Suggestion)

---

## Part 2: Understanding Hooks for PM Work (15 min)

### What Can PM Hooks Do?

Hooks run automatically when certain events happen:

| Hook Type | Trigger | PM Use Case |
| --------- | ------- | ----------- |
| **PreToolUse** | Before Claude edits a file | Validate format before saving |
| **PostToolUse** | After Claude finishes | Log changes, trigger notifications |

### Example: Acceptance Criteria Validator

A hook that checks if acceptance criteria follow Given/When/Then format before saving:

```json
{
  "hooks": {
    "PreToolUse": [
      {
        "matcher": "Edit",
        "hooks": [
          {
            "type": "command",
            "command": "echo \"$TOOL_INPUT\" | grep -qi 'acceptance' && echo 'Checking AC format...' || true"
          }
        ]
      }
    ]
  }
}
```

---

## Exercise 2: Create a Quality Check Hook (15 min)

### The Task

Create a hook that logs whenever you generate PM artifacts (user stories, requirements, etc.).

### Create the Hook

```
Create a .claude/settings.json file with a PostToolUse hook that:
1. Triggers after any Edit operation
2. Logs the timestamp and file edited to .claude/pm-activity.log
3. Echoes "PM artifact updated" as confirmation
```

### Test the Hook

```
Create a user story in stories/login-feature.md.
Check that .claude/pm-activity.log was updated.
```

### Success Criteria

- [ ] Hook configured in `.claude/settings.json`
- [ ] Log file created after edit operations
- [ ] Activity tracked with timestamps

---

## Exercise 3: Combine Agent + Hook (10 min)

### The Task

Create a workflow where:
1. You create/edit a requirements document
2. A hook automatically triggers review
3. The requirements-analyst agent checks quality

### Implementation

```
Update the PostToolUse hook to:
1. Detect when a requirements file is edited (*.md in requirements/ folder)
2. Log that a requirements review is recommended
3. Remind you to run the requirements-analyst agent
```

### Success Criteria

- [ ] Hook detects requirements file edits
- [ ] Reminder logged to activity file
- [ ] Agent can be invoked to review the changes

---

## Key Takeaways

### Agents for PMs

- Agents are specialized assistants you create for specific tasks
- Use read-only tools and `plan` permission mode for analysis agents
- Encode your team's standards in the agent prompt

### Hooks for PMs

- Hooks automate quality checks and logging
- PreToolUse hooks can validate before changes
- PostToolUse hooks can log activity and trigger follow-ups

### When to Use What

| Need | Solution |
| ---- | -------- |
| Analyze requirements for gaps | Requirements Analyst Agent |
| Validate acceptance criteria format | PreToolUse Hook |
| Track PM artifact changes | PostToolUse Hook |
| Review user stories against standards | Story Reviewer Agent |

---

## Homework

1. **Create one more PM agent** - Choose from: Story Reviewer, Meeting Summarizer, or Stakeholder Briefing Generator
2. **Configure a validation hook** - Check that user stories have acceptance criteria before saving
3. **Test with real work** - Use your agent/hook combination on an actual requirement or story

---

## Resources

- [Week 6 Full README](../README.md) - Complete agents & hooks documentation
- [Glossary: Agent](../../../resources/glossary.md) - Term definitions
- [pm-toolkit example](../../week-7/examples/pm-toolkit/) - See agents in action (Week 7 preview)

---

*Return to [Week 6 README](../README.md#-key-takeaways) | Next: [Week 7 - Plugins](../../week-7/tracks/pm.md)*
