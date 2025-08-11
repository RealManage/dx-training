# Claude Code Quick Reference 🚀

Essential commands, shortcuts, and patterns for RealManage developers.

## 🎯 Essential Commands

### Starting Claude
```bash
claude                    # Start in current directory
claude ./src             # Start with specific directory
claude --model sonnet-4  # Use specific model
claude --verbose         # Debug mode
claude doctor            # Check installation
```

### Session Management
```
/help         # Show all commands
/clear        # Reset conversation context
/cost         # Check token usage
/memory       # Edit CLAUDE.md
/permissions  # Control file access
/model        # Switch AI model
/compact      # Compress conversation
/add-dir      # Add directories to context
```

### Quick Actions
```
#             # Quick edit CLAUDE.md
Ctrl+C        # Exit Claude
Ctrl+D        # Exit Claude (alternative)
```

## 💰 Token Costs (Sonnet 4)

| Operation | Input | Output | Cache Hit |
|-----------|-------|--------|-----------|
| Per 1K tokens | $0.003 | $0.015 | $0.0003 |
| Daily average | ~$6 per developer | | |
| Claude Max | Included in subscription | | |

## 📝 Prompt Patterns

### TDD Pattern
```
1. Write xUnit tests for <feature> with 95% coverage
2. [Run tests - confirm red]
3. Implement <feature> to pass tests
4. [Run tests - confirm green]
5. Refactor while keeping tests green
```

### Code Review Pattern
```
Review this code for:
- SOLID principles
- Performance issues
- Security vulnerabilities
- Test coverage gaps
- RealManage standards compliance
```

### Bug Fix Pattern
```
1. Reproduce the bug: <steps>
2. Write a failing test for the bug
3. Fix the bug
4. Verify test passes
5. Check for similar issues
```

## 🏗️ Project Structure

### CLAUDE.md Template
```markdown
# Project: <NAME>

## Tech Stack
- C# .NET 8, ASP.NET Core
- Angular 17, TypeScript
- SQL Server, EF Core 8
- Azure Services

## Requirements
- 95% test coverage minimum
- TDD for all new features
- Async/await for I/O
- OnPush for Angular components

## Current Sprint
- Feature: <description>
- Deadline: <date>
```

### Recommended Folders
```
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
```
> Generate a C# service for <feature> with repository pattern and 95% test coverage

> Create an Angular component for <feature> with OnPush and signals

> Build a SQL migration for <schema change> with rollback

> Write integration tests for <API endpoint> using TestServer
```

## 🔧 Productivity Shortcuts

### Multi-File Operations
```
> Update all controllers to use async/await

> Add null checks to all public methods

> Convert all Angular components to standalone

> Add XML documentation to all public APIs
```

### Refactoring
```
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

# Terminal 3: Code Review
claude --role reviewer
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
```
/cost                    # Current session cost
git diff --stat         # Changes made
dotnet test --logger    # Test results
ng test --code-coverage # Coverage report
```

## 🎓 Learning Resources

### Official Docs
- [Claude Code Docs](https://docs.anthropic.com/en/docs/claude-code)
- [Prompt Engineering](https://docs.anthropic.com/en/docs/build-with-claude/prompt-engineering)
- [MCP Protocol](https://docs.anthropic.com/en/docs/mcp)

### RealManage Resources
- Slack: `#dx-training`
- Wiki: `wiki.realmanage.com`
- Office Hours: Thu 2-3 PM CT

## 💡 Pro Tips

1. **Start small:** Test on one file before bulk changes
2. **Version control:** Commit before major operations
3. **Review changes:** Always review with `/diff` or git
4. **Document patterns:** Add successful prompts to CLAUDE.md
5. **Share knowledge:** Post wins in `#dx-wins`

---

**Remember:** Claude Code is a tool to amplify your capabilities, not replace your judgment!