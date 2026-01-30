# Week 4: Test-Driven Development - Developer Exercise

**Duration:** 45-60 min
**Prerequisites:** Complete main README through Part 2 (TDD Fundamentals)

---

## Your Mission

You'll practice the Red-Green-Refactor TDD cycle with Claude to build HOA features. By the end, you'll have working code with 80-90% test coverage achieved through proper TDD.

---

## Before You Start

> **Learning Mode Recommended:** If you haven't already, run `/output-style` and select **Learning** or **Explanatory** mode. This helps Claude explain the TDD cycle as you work.

---

## Exercise 1: Build from Scratch with TDD (20-25 min)

### Context

True TDD means writing tests FIRST. You'll build a HomeownerService one test at a time, watching each test fail before implementing.

### The Task

1. Set up your sandbox and start Claude:

   ```bash
   cd courses/ai-101-claude-code/sessions/week-4
   mkdir -p sandbox
   cp -r examples/homeowner-setup sandbox/
   cd sandbox/homeowner-setup
   claude
   ```

2. Start with a failing test (RED):

   ```
   I need to build a HomeownerService using TDD.
   First, write a test for adding a new homeowner.
   The test should:
   - Use xUnit and FluentAssertions
   - Test that AddHomeowner returns true for valid input
   - Test that it throws ArgumentException for missing email
   - Follow AAA pattern (Arrange, Act, Assert)

   Don't implement the service yet - just the test.
   ```

3. Run the test and watch it fail:

   ```bash
   dotnet test
   # Should fail - method doesn't exist yet
   ```

4. Implement minimal code (GREEN):

   ```
   Now implement just enough code to make the test pass.
   Don't add any extra functionality.
   ```

5. Refactor if needed:

   ```
   The test passes. Can you refactor for better readability
   without breaking the test?
   ```

6. Repeat for the next requirement:

   ```
   Now write a test for preventing duplicate homeowner registrations
   (same email). Start with the failing test.
   ```

### Success Criteria

- [ ] Wrote at least 2 tests BEFORE implementation
- [ ] Saw each test fail (RED) before making it pass
- [ ] Each implementation was minimal (GREEN)
- [ ] Tests still pass after any refactoring
- [ ] Coverage is naturally 80%+ for the code you wrote

---

## Exercise 2: Extend Existing System with TDD (20-25 min)

### Context

Adding features to existing code still follows TDD. You'll add a property valuation feature to an existing system.

### The Task

1. Navigate to the property-manager example:

   ```bash
   cd courses/ai-101-claude-code/sessions/week-4/sandbox
   cp -r ../examples/property-manager ./  # Skip if already exists
   cd property-manager
   claude
   ```

2. Understand the current state:

   ```
   Show me the current PropertyService and its existing tests.
   What's the current test coverage?
   ```

3. Plan your feature with tests first:

   ```
   I need to add property valuation tracking to PropertyService.
   Requirements:
   - Record a valuation (propertyId, amount, date)
   - Get valuation history for a property
   - Get latest valuation for a property

   Write the tests first. Don't implement yet.
   ```

4. Implement one test at a time:

   ```
   Run the tests. They should fail.
   Now implement just enough code to make the first test pass.
   ```

5. Continue the cycle:

   ```
   First test passes. Move to the next failing test
   and implement minimal code for it.
   ```

6. Verify coverage:

   ```
   Run coverage report. Did the new valuation feature
   achieve 80%+ coverage naturally?
   ```

### Success Criteria

- [ ] Analyzed existing code before adding features
- [ ] Wrote tests for valuation feature BEFORE implementation
- [ ] Implemented one test at a time
- [ ] Existing tests still pass (no regressions)
- [ ] New feature has 80%+ test coverage

---

## Wrap-Up

### What You Completed

- [ ] Built a feature from scratch using TDD
- [ ] Extended existing code with TDD
- [ ] Achieved 80-90% coverage naturally (not by adding tests after)
- [ ] Experienced the Red-Green-Refactor cycle

### Key Takeaways

Return to the [main README](../README.md#-learning-objectives) for shared learning points.

**The TDD Mindset:**

- Tests are SPECIFICATIONS, not afterthoughts
- Failing tests tell you what to build
- Minimal implementation prevents over-engineering
- Refactor only when tests are green

### Before Next Week

Try TDD on a real task at work. Notice how it changes your approach - you'll think about requirements more clearly when you write tests first.

---

*Questions? Ask in `#ai-exchange` or during office hours.*
