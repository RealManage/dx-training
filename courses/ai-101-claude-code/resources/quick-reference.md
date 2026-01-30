# Claude Code Quick Reference 🚀

Essential commands, shortcuts, and patterns for RealManage developers.

---

## Quick Navigation

| Section | Description |
| ------- | ----------- |
| [Essential Commands](#-essential-commands) | Starting Claude, session management, shortcuts |
| [Prompt Patterns](#-prompt-patterns) | TDD, code review, bug fix patterns |
| [Support Commands](#-support-commands--patterns) | Response drafting, fee explanation, escalation |
| [Project Structure](#️-project-structure) | CLAUDE.md template, folder layout |
| [Testing Commands](#-testing-commands) | .NET and Angular test commands |
| [Code Generation](#-code-generation) | Quick generators for services, components |
| [Productivity Shortcuts](#-productivity-shortcuts) | Multi-file ops, refactoring |
| [Advanced Features](#-advanced-features) | Custom commands, multi-Claude workflow |
| [Performance Tips](#-performance-tips) | Reduce tokens, speed up responses |
| [Debug Patterns](#-debug-patterns) | Troubleshooting errors and test failures |
| [Security Reminders](#-security-reminders) | What to never include, what to check |
| [Monitoring](#-monitoring) | Track progress with git and tests |
| [Learning Resources](#-learning-resources) | Official docs, RealManage resources |
| [Pro Tips](#-pro-tips) | Best practices |

---

## 🎯 Essential Commands

### Starting Claude

```bash
claude                   # Start in current directory
claude -c                # Start in current directory and resume last session
claude -r                # Start in current directory and pick from a list of recent sessions
claude ./src             # Start with specific directory
claude --model sonnet    # Use specific model (sonnet or opus)
claude --verbose         # Debug mode
claude doctor            # Check installation
```

### Session Management

```
/help         # Show all commands
/clear        # Reset conversation context
/usage        # Check plan usage
/context      # View current context usage
/usage        # View overall session usage
/memory       # Edit CLAUDE.md
/permissions  # Control file access
/model        # Switch AI model
/compact      # Compress conversation
/add-dir      # Add directories to context
```

### Quick Actions

```text
#             # Quick edit CLAUDE.md
Ctrl+C        # Exit Claude
Ctrl+D        # Exit Claude (alternative)
```

## 📝 Prompt Patterns

### TDD Pattern

```markdown
1. Write xUnit tests for <feature> with 80-90% coverage
2. [Run tests - confirm red]
3. Implement <feature> to pass tests
4. [Run tests - confirm green]
5. Refactor while keeping tests green
```

### Code Review Pattern

```markdown
Review this code for:
- SOLID principles
- Performance issues
- Security vulnerabilities
- Test coverage gaps
- RealManage standards compliance
```

### Bug Fix Pattern

```markdown
1. Reproduce the bug: <steps>
2. Write a failing test for the bug
3. Fix the bug
4. Verify test passes
5. Check for similar issues
```

## 📞 Support Commands & Patterns

### Support Skills

| Skill | Purpose | Example |
| ----- | ------- | ------- |
| `/draft-response` | Generate customer response | `/draft-response late-fee "customer confused about $542 balance"` |
| `/explain-fee` | Break down fee calculation | `/explain-fee late "$100 original, 3 months overdue"` |
| `/tone-check` | Validate response quality | `/tone-check` (then paste your draft) |
| `/escalation-note` | Create handoff documentation | `/escalation-note #12345 "repeated billing complaints"` |

### Response Draft Pattern

```markdown
Draft a response for this support ticket:
[PASTE TICKET]

Context:
- Customer type: [homeowner/board member]
- Issue type: [billing/violation/maintenance]
- Emotional state: [frustrated/confused/neutral]

Requirements:
- Acknowledge their specific concern
- Explain clearly (no jargon)
- Provide 2-3 next steps
- Keep under 150 words
```

### Fee Explanation Pattern

```markdown
Explain this fee calculation:
- Original balance: $[AMOUNT]
- Current balance: $[AMOUNT]
- Months overdue: [N]
- Fee rate: 10% monthly compound

Create a month-by-month breakdown the customer can understand.
```

### Escalation Documentation Pattern

```markdown
Create an escalation note for ticket #[NUMBER]:
[PASTE TICKET AND CONTEXT]

Include:
- Issue summary (2-3 sentences)
- Why this needs escalation
- Recommended resolution
- Urgency level
```

### Support Resources

- [Support Prompts Library](./support-prompts.md) - Full prompt templates
- [Quick Start: Support](./quick-start-support.md) - Track overview
- [n8n Integration Guide](./n8n-integration.md) - Automation concepts

## 🏗️ Project Structure

### CLAUDE.md Template

```markdown
# Project: <NAME>

## Tech Stack
- C# .NET 10, ASP.NET Core
- Angular 17, TypeScript
- SQL Server, EF Core 10
- Azure Services

## Requirements
- 80-90% test coverage minimum
- TDD for all new features
- Async/await for I/O
- OnPush for Angular components

## Current Sprint
- Feature: <description>
- Deadline: <date>
```

### Recommended Folders

```text
project/
├── CLAUDE.md          # Project context
├── src/              # Source code
├── tests/            # Test files
├── docs/             # Documentation
└── .claude/          # Claude settings
    ├── settings.json
    └── commands/     # Custom commands
```

## 🧪 Testing Commands

### C# / .NET

```bash
dotnet test                              # Run all tests
dotnet test --collect:"XPlat Code Coverage"  # With coverage
dotnet test --filter "FullyQualifiedName~Integration"  # Filter tests
```

### Angular

```bash
ng test                   # Run Karma tests
ng test --code-coverage  # With coverage
ng test --watch=false    # CI mode
ng e2e                   # E2E tests
```

## 🎨 Code Generation

### Quick Generators

```markdown
> Generate a C# service for <feature> with repository pattern and 80-90% test coverage

> Create an Angular component for <feature> with OnPush and signals

> Build a SQL migration for <schema change> with rollback

> Write integration tests for <API endpoint> using TestServer
```

## 🔧 Productivity Shortcuts

### Multi-File Operations

```markdown
> Update all controllers to use async/await

> Add null checks to all public methods

> Convert all Angular components to standalone

> Add XML documentation to all public APIs
```

### Refactoring

```markdown
> Extract interface from <class>

> Apply repository pattern to <service>

> Convert to async: <method>

> Add caching to <operation>
```

## 🚀 Advanced Features

### Custom Commands

Create `.claude/commands/violation-report.md`:

```markdown
Generate a violation report for the last 30 days including:
- Total violations by type
- Escalation statistics
- Fine collection rate
- Export to Excel format
```

Use: `/violation-report`

### Multi-Claude Workflow

```bash
# Terminal 1: API Development
cd api
claude

# Terminal 2: UI Development
cd ui
claude

# Terminal 3: Code Review (using custom agent)
claude --agent reviewer
```

## 📊 Performance Tips

### Reduce Token Usage

- Use `/clear` when switching contexts
- Be specific with `/add-dir`
- Use `/compact` periodically
- Exclude large files from context

### Speed Up Responses

- Provide clear, specific prompts
- Include examples in CLAUDE.md
- Use structured prompts (XML tags)
- Cache common patterns

## 🐛 Debug Patterns

### When Claude Generates Errors

1. Check CLAUDE.md for context
2. Verify versions in prompt
3. Provide error message
4. Ask for specific fix

### When Tests Fail

1. Never modify test to pass
2. Understand failure reason
3. Fix implementation
4. Verify edge cases

## 🔒 Security Reminders

### Never Include

- Passwords or API keys
- Connection strings
- PII or PHI data
- Credit card information
- Internal URLs

### Always Check

- Generated SQL for injection
- API endpoints for auth
- File operations for paths
- External calls for security

## 📈 Monitoring

### Track Progress

```bash
git diff --stat         # Changes made
dotnet test --logger    # Test results
ng test --code-coverage # Coverage report
/usage                   # Plan usage (subscription)
```

## 🎓 Learning Resources

### Official Docs

- [Claude Code Docs](https://docs.anthropic.com/en/docs/claude-code)
- [CLI Reference](https://code.claude.com/docs/en/cli-reference) - All CLI flags
- [Interactive Mode](https://code.claude.com/docs/en/interactive-mode) - Slash commands
- [Prompt Engineering](https://docs.anthropic.com/en/docs/build-with-claude/prompt-engineering)
- [MCP Protocol](https://docs.anthropic.com/en/docs/mcp)

### RealManage Resources

- Slack: `#ai-exchange`

## 💡 Pro Tips

1. **Start small:** Test on one file before bulk changes
2. **Version control:** Commit before major operations
3. **Review changes:** Always review with `git diff` before committing
4. **Document patterns:** Add successful prompts to CLAUDE.md
5. **Share knowledge:** Post wins in `#dx-wins`

---

**Remember:** Claude Code is a tool to amplify your capabilities, not replace your judgment!
