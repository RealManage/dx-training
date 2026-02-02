# Week 7: Plugins - PM Track

**Duration:** 60-75 min
**Audience:** Product Managers, Project Managers, Business Analysts

---

## Learning Objectives

By the end of this session, you will be able to:

- Understand plugin structure and why plugins matter for teams
- Use the PM Toolkit plugin to generate user stories, epics, and BDD scenarios
- Extend an existing plugin with new skills
- Create a basic plugin for your PM workflows

---

## Why Plugins Matter for PMs

Plugins package automation into shareable, reusable bundles. As a PM, plugins help you:

- **Standardize output** - Every user story follows the same format
- **Encode best practices** - Team standards baked into tools
- **Share across projects** - One plugin, many repositories
- **Version and update** - Improvements reach everyone

This week, you'll use the **PM Toolkit plugin** and learn to extend it for your needs.

---

## Part 1: Understanding Plugin Structure (15 min)

### What's in a Plugin?

A plugin bundles related automation:

```
pm-toolkit/
├── .claude-plugin/
│   └── plugin.json          # Plugin manifest (name, version, description)
├── skills/
│   ├── write-story/SKILL.md
│   ├── write-epic/SKILL.md
│   ├── write-bdd/SKILL.md
│   └── acceptance-criteria/SKILL.md
├── agents/
│   └── requirements-analyst.md
├── hooks/
│   └── hooks.json
└── README.md
```

### The PM Toolkit Plugin

This plugin includes skills designed for PM workflows:

| Skill | Purpose | Usage |
| ----- | ------- | ----- |
| `write-story` | Generate user stories from descriptions | `/pm-toolkit:write-story <description>` |
| `write-epic` | Generate epics with child stories | `/pm-toolkit:write-epic <feature>` |
| `write-bdd` | Convert requirements to Gherkin | `/pm-toolkit:write-bdd <requirement>` |
| `acceptance-criteria` | Generate detailed ACs | `/pm-toolkit:acceptance-criteria <feature>` |

---

## Exercise 1: Use the PM Toolkit Plugin (20 min)

### Setup

```bash
cd courses/ai-101-claude-code/sessions/week-7/examples
claude --plugin-dir ./pm-toolkit
```

### Try the Skills

Generate a user story:

```
/pm-toolkit:write-story homeowners should be able to view their violation history online
```

Generate BDD scenarios:

```
/pm-toolkit:write-bdd when a violation reaches 90 days it should escalate to board review
```

Generate an epic with child stories:

```
/pm-toolkit:write-epic self-service portal for homeowners to view and pay dues
```

Generate acceptance criteria:

```
/pm-toolkit:acceptance-criteria payment reminder email system
```

### Try the Agent

```
Use the requirements-analyst agent to review a sample requirements document
```

### Success Criteria

- [ ] Successfully ran all four skills
- [ ] Output follows consistent formatting
- [ ] Used the requirements-analyst agent

---

## Exercise 2: Explore Plugin Structure (15 min)

### Examine the Plugin Manifest

```
Show me the contents of .claude-plugin/plugin.json
```

Notice the fields:
- `name` - Plugin identifier
- `version` - Semantic versioning
- `description` - What the plugin does
- `skills`, `agents`, `hooks` - Component references

### Examine a Skill

```
Show me the contents of skills/write-story/SKILL.md
```

Notice the structure:
- YAML frontmatter (name, description, argument-hint)
- Prompt template with clear instructions
- Output format specification

### Success Criteria

- [ ] Reviewed plugin.json manifest
- [ ] Understood skill file structure
- [ ] Identified how skills define their output format

---

## Exercise 3: Extend the Plugin (20 min)

### Add a New Skill

Create a new skill for generating release notes:

```
Create a new skill called "release-notes" that:
1. Takes a date range or version number as input
2. Generates release notes from a list of completed features
3. Formats output for stakeholder communication
4. Includes sections: Summary, New Features, Improvements, Bug Fixes

Save it to skills/release-notes/SKILL.md following the same format as write-story.
```

### Test Your New Skill

```
/pm-toolkit:release-notes v2.5.0
```

### Add a Sprint Summary Skill

```
Create a skill called "sprint-summary" that:
1. Takes sprint number or date range as input
2. Generates a summary suitable for stakeholder updates
3. Includes: Goals, Completed, Carried Over, Metrics, Next Sprint

Save it to skills/sprint-summary/SKILL.md
```

### Success Criteria

- [ ] Created release-notes skill
- [ ] Created sprint-summary skill
- [ ] Both skills work when invoked
- [ ] Output follows consistent team formatting

---

## Exercise 4: Plan Your Own Plugin (10 min)

### Brainstorm Plugin Ideas

Think about your daily PM work. What repeated tasks could become plugin skills?

| Task | Potential Skill Name | Input | Output |
| ---- | -------------------- | ----- | ------ |
| Meeting notes → action items | `meeting-actions` | Meeting notes | Jira-ready tasks |
| Feature request → story | `write-story` | Description | User story |
| Sprint review → summary | `sprint-summary` | Sprint data | Stakeholder update |
| ? | ? | ? | ? |

### Document Your Plugin Concept

```
Help me design a plugin manifest for a PM toolkit that includes:
- 3-4 skills for my daily workflows
- 1 agent for requirement analysis
- Name, version, and description

Just outline the structure - don't create the files yet.
```

### Success Criteria

- [ ] Identified 3+ potential skills for your workflow
- [ ] Documented plugin concept
- [ ] Understand what a custom PM plugin would contain

---

## Key Takeaways

### Plugins for PMs

- Plugins bundle related skills, agents, and hooks into shareable packages
- The PM Toolkit provides ready-to-use skills for stories, epics, and BDD
- You can extend existing plugins with new skills for your needs

### Plugin Benefits

| Benefit | How It Helps PMs |
| ------- | ---------------- |
| Consistency | Every story/epic follows team format |
| Reusability | One plugin works across all projects |
| Sharing | Team members get the same tools |
| Versioning | Updates reach everyone automatically |

### When to Create vs Use Plugins

| Situation | Approach |
| --------- | -------- |
| Team already has PM plugin | Use it, suggest improvements |
| Need standard PM tools | Start with pm-toolkit, customize |
| Unique workflow needs | Create custom skills or full plugin |

---

## Homework

1. **Use the PM Toolkit** on a real feature or epic you're working on
2. **Add one custom skill** to the pm-toolkit for your specific needs
3. **Share your best skill** with the team in `#ai-exchange`
4. **Document plugin ideas** - What should a "team-pm-toolkit" include?

---

## Resources

- [PM Toolkit Example](../examples/pm-toolkit/) - Full plugin source
- [Week 7 Full README](../README.md) - Complete plugin documentation
- [Week 6: Agents & Hooks](../../week-6/tracks/pm.md) - Prerequisites

---

*Return to [Week 7 README](../README.md#-key-takeaways) | Next: [Week 8 - Automation](../../week-8/tracks/pm.md)*
