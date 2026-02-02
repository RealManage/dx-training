# Week 7: Plugins - QA Track

**Duration:** 60-75 min
**Audience:** QA Engineers, Test Engineers, SDET

---

## Learning Objectives

By the end of this session, you will be able to:

- Understand plugin structure and how to test plugin functionality
- Design a QA-focused plugin for test automation
- Create skills for test generation and coverage analysis
- Package test automation into shareable plugins

---

## Why Plugins Matter for QA

Plugins help QA engineers standardize and share test automation:

- **Consistent test generation** - Team shares the same test patterns
- **Reusable test utilities** - Test data generators, coverage reporters
- **Quality hooks** - Automated checks packaged for every project
- **Team standards** - Testing conventions encoded in skills

This week, you'll design a **QA Toolkit plugin** for your team's testing needs.

---

## Part 1: Understanding Plugin Testing (15 min)

### What to Test in Plugins

When a developer creates a plugin, QA should verify:

| Component | What to Test |
| --------- | ------------ |
| **Skills** | Input validation, output format, edge cases |
| **Agents** | Permission modes respected, tool restrictions work |
| **Hooks** | Triggers fire correctly, don't break workflow |
| **Manifest** | Valid JSON, correct references |

### Testing a Plugin Skill

```bash
# Test with valid input
/plugin-name:skill-name valid-input

# Test with invalid input
/plugin-name:skill-name ""
/plugin-name:skill-name null
/plugin-name:skill-name very-long-input-that-exceeds-normal-length...

# Test edge cases
/plugin-name:skill-name special-chars-!@#$%
```

---

## Exercise 1: Test an Existing Plugin (20 min)

### Setup

```bash
cd courses/ai-101-claude-code/sessions/week-7/examples
claude --plugin-dir ./pm-toolkit
```

### Test the PM Toolkit Skills

Create test cases for each skill:

**Test 1: write-story skill**

```bash
# Happy path
/pm-toolkit:write-story user needs to reset their password

# Edge case: minimal input
/pm-toolkit:write-story login

# Edge case: complex input
/pm-toolkit:write-story as a property manager I need to send bulk violation notices to multiple homeowners with different violation types and automatically track escalation timelines based on HOA rules
```

**Test 2: write-bdd skill**

```bash
# Happy path
/pm-toolkit:write-bdd payment is more than 30 days overdue

# Edge case: multiple conditions
/pm-toolkit:write-bdd user logs in with valid credentials and has an outstanding balance and a violation on their account
```

### Document Your Findings

```
Create a test report for the pm-toolkit plugin:
1. Which skills worked correctly?
2. What edge cases caused issues?
3. Was output format consistent?
4. What improvements would you suggest?
```

### Success Criteria

- [ ] Tested all four pm-toolkit skills
- [ ] Documented at least 3 edge case tests
- [ ] Created test report with findings

---

## Exercise 2: Design a QA Toolkit Plugin (20 min)

### Brainstorm QA Skills

What test automation tasks do you repeat? These could become skills:

| Task | Potential Skill | Input | Output |
| ---- | --------------- | ----- | ------ |
| Generate test cases | `generate-tests` | Code file | Test file |
| Analyze coverage gaps | `coverage-gaps` | Coverage report | Missing tests list |
| Create test data | `test-data` | Schema/entity | Realistic test data |
| Write bug report | `bug-report` | Issue description | Formatted bug report |

### Design Your Plugin Structure

```
Ask Claude to help design a qa-toolkit plugin structure:

Design a QA toolkit plugin that includes:
1. Skills for test generation, coverage analysis, test data creation
2. An agent for code testability analysis
3. Hooks for auto-running tests after edits

Show me the directory structure and describe each component.
```

### Document Your Design

Create a plugin specification:

```markdown
# QA Toolkit Plugin Specification

## Skills
- `/qa-toolkit:generate-tests` - Generate unit tests for a file
- `/qa-toolkit:coverage-gaps` - Identify missing test coverage
- `/qa-toolkit:test-data` - Generate realistic test data
- `/qa-toolkit:bug-report` - Format bug reports consistently

## Agents
- `testability-analyzer` - Review code for testing anti-patterns

## Hooks
- PostToolUse on Edit → Run affected tests
- PreToolUse on Write → Validate test naming conventions
```

### Success Criteria

- [ ] Identified 3+ skills for QA workflows
- [ ] Designed plugin directory structure
- [ ] Documented plugin specification

---

## Exercise 3: Create a Test Generation Skill (20 min)

### Build the Skill

Create a skill that generates test cases:

```bash
mkdir -p qa-toolkit/skills/generate-tests
```

```
Create a SKILL.md file for a "generate-tests" skill that:
1. Takes a file path as input
2. Analyzes the code for testable methods
3. Generates xUnit test cases with FluentAssertions
4. Follows AAA pattern (Arrange, Act, Assert)
5. Includes happy path, edge cases, and error cases

Use the same YAML frontmatter format as pm-toolkit skills.
Save to qa-toolkit/skills/generate-tests/SKILL.md
```

### Test Your Skill

```bash
claude --plugin-dir ./qa-toolkit

/qa-toolkit:generate-tests Services/PaymentService.cs
```

### Iterate Based on Results

```
The generate-tests skill output was [describe issues].
Improve the skill to [specific improvement].
```

### Success Criteria

- [ ] Created generate-tests skill
- [ ] Skill produces valid test code
- [ ] Iterated to improve output quality

---

## Exercise 4: Create a Coverage Analysis Skill (10 min)

### Build the Skill

```
Create a "coverage-gaps" skill that:
1. Takes a coverage report or directory as input
2. Identifies methods/branches with low coverage
3. Prioritizes gaps by risk (critical paths first)
4. Suggests specific tests to fill gaps

Save to qa-toolkit/skills/coverage-gaps/SKILL.md
```

### Success Criteria

- [ ] Created coverage-gaps skill
- [ ] Skill identifies meaningful coverage gaps
- [ ] Output prioritizes by risk

---

## Key Takeaways

### Plugin Testing

- Test skills with valid, invalid, and edge case inputs
- Verify agents respect their permission restrictions
- Confirm hooks trigger at correct times
- Document findings for plugin developers

### QA Plugin Benefits

| Benefit | How It Helps QA |
| ------- | --------------- |
| Consistent test generation | Same patterns across team |
| Shared test utilities | Everyone uses same tools |
| Encoded standards | Test conventions automated |
| Easy distribution | Install once, use everywhere |

### QA Toolkit Components

| Component | Purpose |
| --------- | ------- |
| `generate-tests` | Create unit tests from code |
| `coverage-gaps` | Find missing test coverage |
| `test-data` | Generate realistic test data |
| `testability-analyzer` | Review code for testing issues |

---

## Homework

1. **Complete your QA toolkit design** - Finalize the plugin specification
2. **Create one more skill** - Add `test-data` or `bug-report` skill
3. **Test a team plugin** - Use and test any plugins your team has created
4. **Share feedback** - Post plugin testing findings in `#ai-exchange`

---

## Resources

- [PM Toolkit Example](../examples/pm-toolkit/) - Reference plugin structure
- [Support Toolkit Spec](../examples/support-toolkit/) - Another plugin design
- [Week 7 Full README](../README.md) - Complete plugin documentation
- [Week 6: Agents & Hooks](../../week-6/tracks/qa.md) - Prerequisites

---

*Return to [Week 7 README](../README.md#-key-takeaways) | Next: [Week 8 - Automation](../../week-8/tracks/qa.md)*
