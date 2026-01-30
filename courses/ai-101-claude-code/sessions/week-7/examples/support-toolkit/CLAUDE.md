# Support Toolkit - Plugin Design for Support

## Context

This environment is for designing a **Support Plugin** that bundles skills, hooks, and context into a reusable package for the entire support team.

## What Is a Plugin?

A plugin packages related capabilities together:

- **Skills** - Custom commands for common tasks
- **Hooks** - Automated quality checks
- **Context** - CLAUDE.md with team knowledge
- **Templates** - Response patterns and formats

## Why Build a Support Plugin?

Without a plugin:

- Each person creates their own skills
- Quality checks are inconsistent
- New team members start from scratch
- Best practices aren't shared

With a plugin:

- Team shares proven skills
- Quality standards are automated
- Onboarding is faster
- Best practices are encoded

## Plugin Structure

```
support-toolkit/
├── CLAUDE.md           # Team context and policies
├── skills/
│   ├── draft-response.md
│   ├── explain-fee.md
│   ├── escalation-note.md
│   └── tone-check.md
├── hooks/
│   ├── pre-send-quality.md
│   └── escalation-detector.md
├── templates/
│   ├── late-fee-response.md
│   ├── violation-response.md
│   └── welcome-message.md
└── README.md           # How to use the plugin
```

## Plugin Components

### Core Skills (MVP)

Skills are namespaced when installed as a plugin:

| Skill | Purpose | Priority |
| ----- | ------- | -------- |
| `/support-toolkit:draft-response` | Generate responses | Must have |
| `/support-toolkit:explain-fee` | Fee calculations | Must have |
| `/support-toolkit:tone-check` | Review tone | Should have |
| `/support-toolkit:escalation-note` | Create escalation docs | Should have |
| `/support-toolkit:kb-article` | Generate KB content | Nice to have |

### Core Hooks (MVP)

| Hook | Purpose | Priority |
| ---- | ------- | -------- |
| Pre-send quality | Check before sending | Must have |
| Escalation detector | Flag escalations | Must have |
| Tone validator | Check language | Should have |

### Core Templates

| Template | Use Case |
| -------- | -------- |
| Late fee response | Fee questions/disputes |
| Violation response | Violation notices/appeals |
| Welcome message | New resident onboarding |
| General inquiry | Misc questions |

## Practice Tasks

1. Review the plugin structure in `plugin-spec.md`
2. Design components for your team's plugin
3. Prioritize MVP features
4. Create an installation guide
