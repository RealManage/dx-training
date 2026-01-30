# Week 1: Setup & Orientation - QA Exercise

**Duration:** 30-45 min
**Prerequisites:** Complete README.md through "Hands-On Exercises" section

---

## Your Mission

You'll use Claude Code to analyze code for testability and design test scenarios. By the end, you'll have a test plan document and experience using Claude for test strategy.

---

## Before You Start

> **Learning Mode Recommended:** Run `/output-style` and select **Learning** or **Explanatory** mode. This tells Claude to explain its thinking as it helps you - perfect for building your skills!

---

## Exercise 1: Code Analysis for Testing (15-20 min)

### Context

Before writing tests, you need to understand what to test. You'll use Claude Code to analyze the `hoa-cli` example and identify test scenarios.

### The Task

1. Set up your sandbox and start Claude:

   ```bash
   cd courses/ai-101-claude-code/sessions/week-1
   mkdir -p sandbox
   cp -r examples/hoa-cli sandbox/
   cd sandbox/hoa-cli
   claude
   ```

   > **Why sandbox?** We copy examples to sandbox so your practice doesn't modify the original files. The sandbox folder is git-ignored.

2. Ask Claude to analyze the codebase:

   ```
   Analyze this codebase from a QA perspective.
   What are the key functions that need testing?
   What are the business rules I should verify?
   ```

3. Deep dive on the fine calculation:

   ```
   What test cases would you write for CalculateFine?
   Consider edge cases, boundary conditions, and error scenarios.
   ```

4. Ask about the business rules:

   ```
   According to CLAUDE.md, what are the HOA business rules?
   How would you verify each rule is implemented correctly?
   ```

5. Identify gaps:

   ```
   What's NOT tested in this codebase? What could go wrong that
   wouldn't be caught by current tests?
   ```

### Success Criteria

- [ ] Identified key functions requiring tests
- [ ] Listed 5+ test scenarios for CalculateFine
- [ ] Understood business rules from CLAUDE.md
- [ ] Found at least one testing gap

---

## Exercise 2: Test Plan Documentation (15-20 min)

### Context

You'll create a test plan document using Claude Code. This demonstrates how Claude can help with QA documentation, not just code.

### The Task

1. Ask Claude to generate a test plan:

   ```
   Create a test plan document for the HOA Violation Tracker.

   Include:
   - Test objectives
   - Scope (what's in/out)
   - Test scenarios for CalculateFine
   - Test scenarios for violation tracking
   - Edge cases and boundary conditions
   - Expected results for each test

   Format it as a markdown document I can save.
   ```

2. Review the test plan. Ask for refinements:

   ```
   Add test scenarios for the 30/60/90 day escalation timeline.
   What should happen at each interval?
   ```

3. Ask Claude to prioritize:

   ```
   If we only had time to run 5 tests, which would you prioritize
   and why? Consider risk and business impact.
   ```

4. Save the test plan (copy to a file or document)

5. Ask Claude about testing tools:

   ```
   What testing tools does this project use according to CLAUDE.md?
   What's xUnit and FluentAssertions?
   ```

### Success Criteria

- [ ] Generated a structured test plan document
- [ ] Test plan includes edge cases and boundary conditions
- [ ] Identified top 5 priority test scenarios
- [ ] Understand the testing tools mentioned in CLAUDE.md

---

## Wrap-Up

### What You Completed

- [ ] Analyzed code from a QA perspective using Claude
- [ ] Created a test plan document
- [ ] Identified priority test scenarios
- [ ] Experienced how CLAUDE.md provides project context

### Key Takeaways

Return to the [main README](../README.md#-key-takeaways) for shared learning points.

### Before Next Week

Practice using Claude Code for test planning on a real project:

- Ask Claude to analyze a feature for testability
- Generate test scenarios for an upcoming ticket
- Document edge cases Claude identifies

---

*Questions? Ask in `#ai-exchange` or during office hours.*
