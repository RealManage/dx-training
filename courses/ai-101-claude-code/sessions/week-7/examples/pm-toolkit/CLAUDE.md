# Week 7 Example: PM Toolkit Plugin

## Project Purpose
Training exercise for Week 7: Plugins.
This plugin demonstrates how to package skills, agents, and hooks into a distributable unit.

## What This Plugin Does
Generates project management artifacts in markdown format:
- User stories with acceptance criteria
- Epics with child story breakdown
- BDD/Gherkin scenarios from requirements
- Detailed acceptance criteria

## Plugin Structure
```
pm-toolkit/
├── .claude-plugin/
│   └── plugin.json          # Plugin manifest (required)
├── skills/
│   ├── write-story/SKILL.md
│   ├── write-epic/SKILL.md
│   ├── write-bdd/SKILL.md
│   └── acceptance-criteria/SKILL.md
├── agents/
│   └── requirements-analyst.md
├── hooks/
│   └── hooks.json
└── CLAUDE.md
```

## Available Skills
- `/write-story <description>` - Generate user story from plain English
- `/write-epic <feature>` - Generate epic with child stories
- `/write-bdd <requirement>` - Convert requirements to Gherkin scenarios
- `/acceptance-criteria <feature>` - Generate detailed acceptance criteria

## Available Agents
- `requirements-analyst` - Analyze requirements for completeness and ambiguity

## Testing the Plugin

```bash
# Copy to sandbox
cp -r pm-toolkit sandbox/
cd sandbox/pm-toolkit

# Start Claude
claude

# Try the skills
/write-story homeowners need to see their violation history

/write-bdd when a violation reaches 90 days it should escalate to board review

/write-epic self-service portal for homeowners to view and pay dues

/acceptance-criteria payment reminder email system

# Try the agent
> Use the requirements-analyst agent to review the acceptance criteria we just generated
```

## HOA Domain Context
When using these skills for RealManage work, consider:
- Violations have 30/60/90 day escalation
- Board meetings are first Tuesday monthly
- Late fees: 10% monthly compound interest
- Quorum requires 3+ board members

## Extending the Plugin

### Add a New Skill
1. Create `skills/<skill-name>/SKILL.md`
2. Add YAML frontmatter with name, description, argument-hint
3. Write the prompt template
4. Test with `/skill-name <args>`

### Add a New Agent
1. Create `agents/<agent-name>.md`
2. Define tools, model, permissionMode
3. Write the system prompt
4. Invoke with "use the <agent-name> agent"

### Add Hooks
1. Edit `hooks/hooks.json`
2. Add matchers for PreToolUse or PostToolUse
3. Define command hooks for automation

## Learning Objectives
- Understand plugin manifest structure
- See how skills, agents, and hooks package together
- Practice extending an existing plugin
- Create your own skills using the patterns shown
