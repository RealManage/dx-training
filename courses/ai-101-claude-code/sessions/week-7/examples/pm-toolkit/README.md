# PM Toolkit Plugin

A Claude Code plugin for generating project management artifacts in markdown format.

## Skills

Skills are namespaced when installed as a plugin:

| Skill | Description | Usage |
| ----- | ----------- | ----- |
| `write-story` | Generate user story from description | `/pm-toolkit:write-story <description>` |
| `write-epic` | Generate epic with child stories | `/pm-toolkit:write-epic <feature>` |
| `write-bdd` | Convert requirements to Gherkin | `/pm-toolkit:write-bdd <requirement>` |
| `acceptance-criteria` | Generate detailed ACs | `/pm-toolkit:acceptance-criteria <feature>` |

## Agent

- **requirements-analyst** - Analyzes requirements for completeness, ambiguity, and testability

## Installation

### Using the Plugin Manager (Recommended)

```bash
# Interactive: Open plugin manager
/plugin
# Navigate to Discover tab → Search for pm-toolkit → Install

# Or CLI: Install from marketplace
claude plugin install pm-toolkit@realmanage-plugins --scope project
```

### For Local Development

```bash
# Test plugin locally before publishing
claude --plugin-dir ./pm-toolkit

# Validate plugin structure
/plugin validate ./pm-toolkit
```

> **Note:** Skills are available immediately after installation - no restart required.

## Examples

```bash
# Generate a user story
/pm-toolkit:write-story homeowners should be able to view their violation history online

# Generate BDD scenarios
/pm-toolkit:write-bdd payment is overdue by more than 30 days

# Generate an epic
/pm-toolkit:write-epic self-service portal for dues payment

# Analyze requirements
> Use the requirements-analyst agent to review requirements.md
```

## Output

All skills generate markdown that can be pasted into any project management tool:

- Jira
- Azure DevOps
- Linear
- GitHub Issues
- Notion
- Confluence

## License

All Rights Reserved - RealManage DX Team
