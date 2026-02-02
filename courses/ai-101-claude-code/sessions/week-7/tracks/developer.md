# Week 7: Plugins - Developer Track

**Duration:** 2 hours
**Audience:** Software Developers, SDETs

---

## Overview

This track covers the complete Week 7 material on Plugins - the packaging and distribution system for Claude Code automation.

## Why Developers Need Plugins

As a developer, you've built commands, agents, and hooks in Week 6. Now you need to:
- **Package** these components for distribution
- **Share** automation with your team
- **Version** your tools and track changes
- **Reuse** across multiple projects

Plugins solve all of these problems.

## What You'll Learn

### Part 1: Plugin Architecture (20 min)
- Plugin directory structure
- The plugin.json manifest
- Packaging commands, agents, and hooks together

### Part 2: Skills in Plugins (30 min)
- Skills vs Commands - the key differences
- SKILL.md frontmatter fields
- Supporting files (templates, scripts)
- Tool restrictions and model overrides

### Part 3: Agents in Plugins (25 min)
- Packaging custom agents
- Skills that spawn agents (`context: fork`)
- Dynamic context injection with `` !`command` ``

### Part 4: Hooks in Plugins (15 min)
- Plugin-level hooks (hooks.json)
- Skill-level embedded hooks
- When to use each approach

### Part 5: Distribution & Marketplace (20 min)
- Local plugin development with `--plugin-dir`
- Marketplace registration and installation
- Plugin validation and testing

### Part 6: Hands-On Workshop (30 min)
- Build a complete RealManage plugin from scratch
- Create skills with templates
- Add agents and hooks
- Test and validate

## Key Developer Skills

After this session, you'll be able to:

```bash
# Create a plugin structure
mkdir -p my-plugin/.claude-plugin
mkdir -p my-plugin/skills/my-skill
mkdir -p my-plugin/agents
mkdir -p my-plugin/hooks

# Build skills with templates
my-plugin/
└── skills/
    └── violation-notice/
        ├── SKILL.md
        └── templates/
            └── first-notice.md

# Package agents for reuse
my-plugin/
└── agents/
    └── security-auditor.md

# Configure plugin-wide hooks
my-plugin/
└── hooks/
    └── hooks.json

# Test locally
claude --plugin-dir ./my-plugin

# Share with team
/plugin marketplace add owner/repo
/plugin install my-plugin@team-marketplace
```

## Developer-Specific Exercises

### Exercise 1: Create a Code Review Plugin
Build a plugin with:
- `security-audit` skill that spawns a security-auditor agent
- `pr-review` skill with dynamic context injection for git diff
- Audit hooks that log all file modifications

### Exercise 2: Create a Testing Automation Plugin
Build a plugin with:
- `generate-tests` skill with TDD templates
- `coverage-report` skill that generates coverage summaries
- Hooks that validate test coverage on Edit operations

### Exercise 3: Create a Documentation Plugin
Build a plugin with:
- `api-docs` skill with OpenAPI templates
- `update-readme` skill with dynamic git context
- Agent for documentation style checking

## Reference Materials

See the main [Week 7 README](../README.md) for complete session content.

---

*Developer Track - Week 7: Plugins*
