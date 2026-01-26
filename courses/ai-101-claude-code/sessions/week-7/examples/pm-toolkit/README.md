# PM Toolkit Plugin

A Claude Code plugin for generating project management artifacts in markdown format.

## Skills

| Skill | Description | Usage |
|-------|-------------|-------|
| `/write-story` | Generate user story from description | `/write-story <description>` |
| `/write-epic` | Generate epic with child stories | `/write-epic <feature>` |
| `/write-bdd` | Convert requirements to Gherkin | `/write-bdd <requirement>` |
| `/acceptance-criteria` | Generate detailed ACs | `/acceptance-criteria <feature>` |

## Agent

- **requirements-analyst** - Analyzes requirements for completeness, ambiguity, and testability

## Installation

```bash
# Copy to your project
cp -r pm-toolkit /your/project/.claude/plugins/

# Or install from local path
claude plugin install ./pm-toolkit
```

## Examples

```bash
# Generate a user story
/write-story homeowners should be able to view their violation history online

# Generate BDD scenarios
/write-bdd payment is overdue by more than 30 days

# Generate an epic
/write-epic self-service portal for dues payment

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

MIT - RealManage DX Team
