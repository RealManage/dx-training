# Quick Start: QA Engineer Track

You ensure quality. You want Claude Code to accelerate test creation and automation. Here's your focused path.

**Estimated Time:** 10-12 hours total

---

## Your Learning Path

```
Week 0 ──► Week 1 ──► Week 2 ──────────► Week 4 ──────────────────────► Week 8 ──► Week 9
(opt)     Setup     Prompts             ★ TDD ★                        Workflows  Capstone
                      │                    │                               │
                      │                    │                               │
                   FOCUS:               THIS IS                         Batch
                   Test case            YOUR WEEK                       automation
                   generation
```

### What You'll Cover vs Skip

| Week | Topic | QA Priority | Notes |
|------|-------|-------------|-------|
| 0 | AI Foundations | Optional | Good for vocabulary |
| 1 | Setup & Orientation | Must Do | Get Claude working |
| 2 | Prompting Foundations | Must Do | Focus on test prompts |
| 3 | Plan Mode | Skim | Read concepts, skip deep dives |
| 4 | Test-Driven Development | **Critical** | Your power week |
| 5 | Commands & Skills | Light | Learn to consume, not create |
| 6 | Agents & Hooks | Light | Learn to consume, not create |
| 7 | Plugins | Skip | Developer-focused |
| 8 | Real-World Workflows | Must Do | Batch test automation |
| 9 | Capstone | Must Do | QA-specific option available |

---

## Week-by-Week Focus

### Week 0: AI Foundations (Optional - 30 min)
**Skip if:** You're comfortable with AI terminology.

Key terms for QA: hallucinations (why you MUST verify AI output), context windows, tokens.

---

### Week 1: Setup & Orientation (1 hour)
**Goal:** Claude Code installed and working.

Focus on:
- [ ] Installation (`claude doctor` to verify)
- [ ] First conversation
- [ ] Basic CLAUDE.md setup

**QA Focus:** When setting up your CLAUDE.md, include your test preferences:
```markdown
## Testing Preferences
- Prefer xUnit with FluentAssertions
- Use Arrange-Act-Assert pattern
- Include edge cases and boundary tests
- Test naming: Should_[Action]_When_[Condition]
```

---

### Week 2: Prompting Foundations (1.5 hours)
**Goal:** Write prompts that generate comprehensive test suites.

**QA-Specific Prompts to Learn:**

**Test Case Generation:**
```
Given this requirement: [PASTE REQUIREMENT]

Generate comprehensive test cases covering:
1. Happy path scenarios
2. Edge cases (empty, null, boundaries)
3. Error conditions
4. Integration points
5. Performance considerations

Format as Gherkin (Given/When/Then).
```

**Test Code Generation:**
```
Write xUnit tests for [METHOD/CLASS] that verify:
- [BEHAVIOR 1]
- [BEHAVIOR 2]
- [EDGE CASE 1]

Use FluentAssertions and Moq. Follow Arrange-Act-Assert.
Target 80-90% coverage including branches.
```

**Code Review for Testability:**
```
Review @Services/PaymentService.cs for testability issues.

Identify:
- Hard-to-mock dependencies
- Missing interfaces
- Complex branching that needs coverage
- Suggested test cases
```

> **Tip:** Use `@path/to/file` to reference code files instead of copy-pasting.

---

### Week 3: Plan Mode (30 min - Skim Only)
**Goal:** Understand the concept, don't deep dive.

Just know:
- Plan mode (`Shift+Tab`) makes Claude create a plan before acting
- Useful when Claude needs to modify multiple files
- You'll use it when generating test suites across a project

**Skip:** The detailed code review exercises (developer-focused).

---

### Week 4: Test-Driven Development (2.5 hours) ⭐⭐⭐
**Goal:** This is YOUR week. Master AI-assisted test creation.

**Everything Here is Relevant:**
- [ ] Red-Green-Refactor cycle
- [ ] Writing tests before implementation
- [ ] Coverage analysis and improvement
- [ ] Test-first design thinking

**QA Power Moves:**

**Generate Test Suite from Spec:**
```
Here's a feature specification:
[PASTE SPEC]

Generate a complete xUnit test suite that verifies ALL acceptance criteria.
Include:
- Unit tests for each business rule
- Integration tests for workflows
- Edge case tests
- Mock setup for external dependencies
```

**Coverage Gap Analysis:**
```
Analyze coverage gaps between @Tests/PaymentServiceTests.cs
and the implementation at @Services/PaymentService.cs

Identify gaps and generate tests to achieve 80-90% coverage.
Focus on untested branches and edge cases.
```

**Test Data Generation:**
```
Generate test data for [ENTITY] that covers:
- Valid examples (3 variations)
- Boundary values (min, max, edges)
- Invalid examples for error testing
- Edge cases (null, empty, special chars)

Format as C# test fixtures using AutoFixture patterns.
```

---

### Weeks 5-6: Commands, Skills, Agents, Hooks (1 hour - Light)
**Goal:** Learn to USE these tools, not BUILD them.

**What to Know:**
- Commands exist at `.claude/commands/` - you can invoke them with `/command-name`
- Skills exist at `.claude/skills/` - Claude may suggest them automatically
- Agents exist at `.claude/agents/` - specialized personas for tasks
- Hooks can run tests automatically after changes

**QA-Useful Pattern:** Ask your dev team to create:
- `/generate-tests` command for your project
- `test-reviewer` agent that focuses on test quality
- Post-change hook that runs tests automatically

---

### Week 7: Plugins (Skip)
This is packaging for distribution. Developer-focused. Skip entirely.

---

### Week 8: Real-World Workflows (1.5 hours)
**Goal:** Automate test generation and analysis with Claude CLI.

Focus on:
- [ ] Headless Claude automation with `-p` flag
- [ ] Batch test generation scripts
- [ ] Coverage analysis automation

**QA-Relevant Pattern:**
```bash
#!/bin/bash
# batch-test-coverage.sh - Analyze and improve test coverage

# Find files with low coverage and generate tests
for file in $(find src -name "*.cs" -type f); do
  coverage=$(get-coverage "$file")  # Your coverage tool
  if [ "$coverage" -lt 80 ]; then
    echo "Generating tests for $file (coverage: $coverage%)"
    claude -p "Analyze $file and generate xUnit tests to improve coverage. \
      Use FluentAssertions. Target 80-90% coverage." \
      --output-format json \
      --add-dir "$(dirname "$file")"
  fi
done
```

---

### Week 9: Capstone (3-4 hours)
**Goal:** Build something QA-focused.

**QA Capstone Option: Test Automation Suite**

Build a comprehensive test automation project:
1. Test data generator using Claude
2. API test suite for HOA endpoints
3. Coverage report dashboard
4. Automated test case generation from requirements

**Deliverables:**
- Working test suite with 80%+ coverage
- Documentation of your Claude prompts
- Batch automation script for test runs
- Presentation to team

---

## QA-Specific Prompt Library

### Test Case Generation
```
Convert this user story to test cases:
[STORY]

Include: acceptance criteria tests, edge cases, negative tests.
Format: Gherkin (Given/When/Then)
```

### Bug Reproduction Test
```
Write a failing test that reproduces this bug:
[BUG DESCRIPTION]

The test should fail now and pass when the bug is fixed.
```

### API Contract Testing
```
Generate API contract tests for this endpoint:
[ENDPOINT SPEC]

Test: request validation, response schema, error codes, edge cases.
```

### Test Refactoring
```
Refactor @Tests/ViolationServiceTests.cs to reduce duplication.
Extract common setup, use parameterized tests where appropriate.
```

---

## Key Commands for QA

| Command | What It Does |
|---------|--------------|
| `/model default` | Fast, capable test generation (default) |
| `/model sonnet` | Standard test work |
| `/model opus` | Advanced test work |
| `Shift+Tab` | Plan mode for multi-file test suites |
| `/clear` | Reset when switching test contexts |

---

## What to Tell Developers

As a QA engineer, you can help developers write better code by:

1. **Requesting test-first development** - "Can you write the tests first?"
2. **Asking for testability** - "Can you add an interface for this dependency?"
3. **Reviewing test coverage** - Use Claude to analyze gaps

---

## Resources

- [Glossary](/resources/glossary.md) - Term definitions
- [Decision Trees](/resources/decision-trees.md) - When to use what
- [Week 4 README](/sessions/week-4/README.md) - Your main resource

---

*Questions? Hit up `#dx-training` on Slack. Ask for QA-specific examples!*
