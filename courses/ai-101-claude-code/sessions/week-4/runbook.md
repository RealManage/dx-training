# Week 4: Test-Driven Development with Claude - Trainer Runbook

## Session Overview

**Duration:** 2 hours
**Format:** In-person or virtual
**Tracks:** Developer, QA, PM, Support

---

## Pre-Session Checklist

### For Instructors

- [ ] Test both example projects build without warnings (`dotnet build` in homeowner-setup and property-manager)
- [ ] Run `dotnet test` in property-manager (4 tests should pass)
- [ ] Verify coverage tools work: `dotnet test --collect:"XPlat Code Coverage"` in property-manager
- [ ] Practice the Red-Green-Refactor demo (see Live Demo Script below)
- [ ] Have the [TDD Cheat Sheet](./resources/tdd-cheatsheet.md) and [Coverage Guide](./resources/coverage-guide.md) open for reference
- [ ] Prepare backup exercises for coverage tool issues
- [ ] Monitor `#ai-exchange` Slack channel

---

## Timing Guide

| Time | Segment | What Happens |
| ---- | ------- | ------------ |
| 0:00-0:05 | Welcome & Context | Set the stage: "Last week planning, this week testing" |
| 0:05-0:15 | Part 1: Why TDD Prevents AI Hallucinations | Problem/solution framing, TDD cycle intro |
| 0:15-0:30 | Part 2: TDD Fundamentals with Claude | Live demo of Red-Green-Refactor (see demo script) |
| 0:30-1:30 | Part 3: Hands-On Exercises | Track-specific exercises (see Track Delivery below) |
| 1:30-1:45 | Part 4: Best Practices & Patterns | Test organization, FluentAssertions, mocking |
| 1:45-1:55 | Part 5: Reflection & Wrap-Up | Discussion, share work, homework review |
| 1:55-2:00 | Close | Preview Week 5, answer final questions |

> **Note:** Part 3 is the heart of the session (60 min). If Parts 1-2 run long, cut into Part 4 — participants can read best practices on their own.

---

## Track Delivery Strategy

### Combined Session (Recommended)

All participants attend Parts 1 and 2 together. For Part 3, participants split into track-specific exercises:

- **Developers** follow `tracks/developer.md` (build from scratch with TDD + enhance existing system)
- **QA** follow `tracks/qa.md` (test existing services, coverage gap analysis, test data generation)
- **PMs** follow `tracks/pm.md` (write testable requirements with Given/When/Then)
- **Support** follow `tracks/support.md` (test-first response writing, quality checklists)

### Instructor Tips for Mixed Audiences

- During Part 1, use the "without tests, AI can invent methods that don't exist" framing — resonates with all roles
- During Part 2 demo, the Red-Green-Refactor cycle is visual and engaging for everyone
- When splitting for Part 3: "Developers write code tests. QA writes code tests too but for existing code. PMs write testable requirements. Support writes quality criteria. Same principle: define success before doing the work."
- Pair non-developers with developers if anyone struggles with `dotnet test`
- For virtual sessions, use breakout rooms by track
- Remind ALL tracks to copy examples to `sandbox/` before editing

### Solo Delivery (Self-Paced)

If participants are working through the course independently:

- Have them read Parts 1-2 in the README, trying each example as they go
- Recommend they pick the track matching their role for Part 3
- Point them to the TDD Cheat Sheet and Coverage Guide as references
- Encourage posting in `#ai-exchange` when they get their first green test

---

## Live Demo Script

### Part 2 Demo: Red-Green-Refactor with Claude (10 min)

```bash
# 1. Navigate to homeowner-setup example
cd courses/ai-101-claude-code/sessions/week-4/examples/homeowner-setup
claude
```

**Step 1 — RED (Write a failing test):**

```text
> Write a test for a HomeownerService.IsValidEmail method.
> It should return false for null input.
> Use xUnit and FluentAssertions. Don't implement the service yet.
```

**Talk through:**

- "Notice I said 'don't implement the service yet' — that's key"
- "Claude writes the test, but there's no HomeownerService class, so it won't even compile"
- "That's RED — the test fails because the code doesn't exist"

```bash
# Show the failure
dotnet test
# Should get compilation error - class doesn't exist
```

**Step 2 — GREEN (Make it pass):**

```text
> Now implement just enough code to make this test pass.
> Minimal code only - don't add anything extra.
```

**Talk through:**

- "See how Claude only creates what the test requires? No extra methods, no over-engineering"
- "This is the power of TDD — tests constrain the AI to produce exactly what's needed"

```bash
# Show it passing
dotnet test
# Should see: 1 passed
```

**Step 3 — REFACTOR (Improve while green):**

```text
> The test passes. Can you refactor IsValidEmail to also use regex for proper format checking?
> Keep the test green.
```

**Talk through:**

- "We only refactor when tests are green — that's our safety net"
- "If the refactor breaks the test, we know immediately"

**Key message:** "This is the cycle. Write test, see it fail, make it pass, improve. Tests are your guardrails for AI-generated code."

### Expected Demo Output

- Compilation failure on first `dotnet test` (no HomeownerService class)
- Single green test after implementing minimal code
- Test stays green after refactoring

---

## Instructor Notes

### Key Points to Emphasize

- **Tests prevent AI hallucinations** — This is the hook. Without tests, Claude can invent plausible but wrong code. Tests force correctness
- **Red before Green** — Seeing the test fail first proves it's actually testing something
- **Minimal code** — "Just enough to pass" prevents over-engineering
- **Coverage emerges naturally** — Don't chase numbers; follow the cycle and 80-90% happens
- **TDD works for all roles** — PMs write testable requirements, QA writes test suites, Support writes quality criteria

### Common Questions

> **"Should I test private methods?"**

- No! Test public behavior only
- Private methods are implementation details
- If it's important, it should affect public behavior

> **"My tests are brittle"**

- Test behavior, not implementation
- Use interfaces and dependency injection
- Don't over-specify in assertions

> **"Coverage is stuck below 80%"**

- Check for untested error handling paths
- Look for early returns and guard clauses
- Consider edge cases (nulls, empty collections, boundary values)
- Run `dotnet test --collect:"XPlat Code Coverage"` to see exactly what's missed

> **"When do I use Moq?"**

- When a dependency is external (email, file storage, APIs)
- When setup is expensive (real databases in unit tests)
- Don't mock everything — in-memory DB is fine for data access tests

> **"TDD feels slow"**

- It IS slower at first — that's the learning curve
- Once internalized, it's actually faster (fewer bugs, less rework)
- The Advanced Techniques section in the README covers the "batched" approach for experienced devs

---

## Troubleshooting

### dotnet test Failures

- **"No test assemblies found":** Check `<IsTestProject>true</IsTestProject>` in csproj
- **Compilation errors in homeowner-setup:** Expected during RED phase! This is the point
- **Tests pass but no coverage report:** Need coverlet — `dotnet add package coverlet.collector`

### Coverage Tool Issues

- **Coverlet not installed:** Both example projects include it in csproj already
- **Coverage report empty:** Run with `--collect:"XPlat Code Coverage"` (exact casing matters)
- **HTML report generator missing:** `dotnet tool install -g dotnet-reportgenerator-globaltool`
- **Threshold failures:** The course target is 80% minimum. If `Threshold=80` fails the build, that's by design — write more tests

### In-Memory Database Issues

- **Tests interfering with each other:** Use `Guid.NewGuid().ToString()` for database name (each test gets isolated DB)
- **EF Core InMemory quirks:** InMemory doesn't enforce foreign keys or unique constraints. Mention this if students hit it

### Student Struggling with TDD

- Have them start with the simplest possible test: "test that 1 + 1 = 2"
- Then move to: "test that a method exists" (just the compilation check)
- Then: "test that it returns the right value"
- Build confidence with small wins before tackling business logic

---

## Session Wrap-Up Checklist

- [ ] All participants experienced the Red-Green-Refactor cycle (at minimum the demo)
- [ ] All participants completed at least one track exercise
- [ ] Developers achieved some test coverage on homeowner-setup
- [ ] Key takeaway reinforced: "Tests prevent AI hallucinations"
- [ ] At least 2-3 participants shared their experience in `#ai-exchange`
- [ ] Homework assigned: complete both exercises + try TDD on a real work task
- [ ] Preview of Week 5 (Commands & Basic Skills) delivered
- [ ] Remind participants: TDD Cheat Sheet and Coverage Guide are in `resources/`

---

*End of Week 4 Trainer Runbook*
