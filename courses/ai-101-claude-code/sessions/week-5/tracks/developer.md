# Week 5: Commands & Basic Skills - Developer Track

**This is the full content track for developers.**

Developers should follow the [main README](../README.md) in its entirety. This track covers:

1. **Part 1: Slash Commands** - Creating custom commands in `.claude/commands/`
2. **Part 2: Skills** - Building skills with supporting files in `.claude/skills/`
3. **Part 3: Hands-On Exercises** - Practice creating both commands and skills

## Why This Matters for Developers

As a developer, you'll use commands and skills to:

- **Standardize team workflows** - Create commands that enforce coding standards, TDD patterns, and project conventions
- **Automate repetitive tasks** - Build skills for generating boilerplate, creating services, running test suites
- **Share domain knowledge** - Encode business rules (like HOA violation escalation) into reusable prompts
- **Improve consistency** - Everyone uses the same command = same output quality

## Developer-Specific Tips

### Command Design Patterns

**1. TDD-First Commands**
```markdown
---
description: Create new service with TDD
argument-hint: <ServiceName>
---

Create $1Service following TDD:

1. Write failing test first in Tests/$1ServiceTests.cs
2. Create interface I$1Service
3. Implement minimal code to pass
4. Refactor while green

Use xUnit, FluentAssertions, Moq.
```

**2. Code Review Commands**
```markdown
---
description: Review code changes
---

Review the current git diff for:
- SOLID principles violations
- Missing tests
- Security issues (SQL injection, XSS)
- RealManage coding standards
- SOC 2 compliance

Output: Critical/Warning/Suggestion format
```

**3. Refactoring Commands**
```markdown
---
description: Extract method refactoring
argument-hint: <MethodName>
---

Extract selected code into $ARGUMENTS method.

Ensure:
- Single responsibility
- Descriptive name
- Tests still pass
- No behavior changes
```

### Skill Architecture

For complex workflows, structure your skills like this:

```
.claude/skills/new-api-endpoint/
  SKILL.md                 # Main orchestration
  controller-template.cs   # C# controller boilerplate
  service-template.cs      # Service layer template
  test-template.cs         # Test file template
  openapi-template.yaml    # API documentation
```

### Integration with CI/CD

Commands and skills can be invoked in automation:

```bash
# In CI pipeline - run code review command
claude -p "Run /code-review on the changes in this PR"

# Generate documentation
claude -p "/generate-api-docs for Services/PaymentService.cs"
```

## Homework Extensions for Developers

Beyond the standard homework:

1. **Create a TDD command** that generates test-first scaffolding for your most common task
2. **Build a code review skill** with a checklist template file
3. **Share your best command** in `#dx-training` with the team

## Preparing for Week 6

Week 6 builds on this foundation:
- **Agents** - Specialized AI assistants with tool restrictions
- **Hooks** - Automated actions (logging, validation, blocking)
- **Audit trails** - SOC 2 compliance patterns

Start thinking about:
- What operations should be logged?
- What dangerous commands should be blocked?
- What tasks could a read-only agent handle?

---

*Return to [main README](../README.md)*
