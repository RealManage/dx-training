# Week 1: Setup & Orientation - Developer Exercise

**Duration:** 45-60 min
**Prerequisites:** Complete README.md through "Hands-On Exercises" section

---

## Your Mission

You'll use Claude Code to explore, debug, and extend a C# CLI application. By the end, you'll have fixed a bug, added a feature, and experienced how CLAUDE.md provides project context.

---

## Before You Start

> **Learning Mode Recommended:** Run `/output-style` and select **Learning** or **Explanatory** mode. This tells Claude to explain its thinking as it helps you - perfect for building your skills!

---

## Exercise 1: Bug Hunt (20-25 min)

### Context

The `hoa-cli` example has an intentional bug in the fine calculation. You'll use Claude Code to find and fix it, then write a test to prevent regression.

### The Task

1. Set up your sandbox and start Claude:

   ```bash
   cd courses/ai-101-claude-code/sessions/week-1
   cp -r examples/hoa-cli sandbox/
   cd sandbox/hoa-cli
   claude
   ```

2. Ask Claude to explore the codebase:

   ```text
   What does this application do? Walk me through the code structure.
   ```

3. Ask Claude about the business rules:

   ```text
   How should late fees work according to the CLAUDE.md file?
   ```

4. Find the bug:

   ```text
   Find the bug in CalculateFine. It should compound 10% monthly,
   not just multiply once. Show me the current code and what's wrong.
   ```

5. Have Claude fix the bug:

   ```text
   Fix the CalculateFine method to properly compound 10% monthly
   after the 30-day grace period.
   ```

6. Write a test to verify the fix:

   ```text
   Write an xUnit test that verifies the compound interest calculation.
   Test case: 60 days overdue on a $100 base fine should be $121.
   ```

7. Build and run the tests:

   ```bash
   dotnet test
   ```

### Success Criteria

- [ ] Found the bug in `ViolationService.CalculateFine()`
- [ ] Claude explained compound vs simple interest
- [ ] Bug is fixed with proper monthly compounding
- [ ] Test passes for the 60-day scenario

---

## Exercise 2: Feature Implementation (20-25 min)

### Context

The CLI has three unimplemented menu options (3-5). You'll implement one with Claude's help.

### The Task

1. Ask Claude what's missing:

   ```text
   What menu options are marked TODO in Program.cs?
   ```

2. Implement option 3 (Check overdue violations):

   ```text
   Implement the GetOverdueViolations method in ViolationService.
   A violation is overdue if it's been more than 30 days since reported.
   Then wire up menu option 3 to display them.
   ```

3. Review what Claude wrote. Does it match the coding style?

4. Ask for improvements:

   ```text
   Can you make this follow the same pattern as ViewAllViolations?
   Use the same console formatting.
   ```

5. Test the feature manually:

   ```bash
   dotnet run
   # Choose option 3 to verify it works
   ```

6. Ask Claude to explain a C# concept you saw:

   ```text
   What's a record type in C#? I noticed Violation is a record.
   ```

### Success Criteria

- [ ] `GetOverdueViolations()` method implemented
- [ ] Menu option 3 displays overdue violations
- [ ] Code follows existing style conventions
- [ ] Understand what a C# record is

---

## Wrap-Up

### What You Completed

- [ ] Found and fixed a bug using Claude Code
- [ ] Implemented a new feature with proper styling
- [ ] Wrote a unit test for the fix
- [ ] Experienced how CLAUDE.md provides project context

### Key Takeaways

Return to the [main README](../README.md#-key-takeaways) for shared learning points.

### Before Next Week

Try these stretch goals:

- Implement menu options 4 and 5
- Add more test coverage to ViolationService
- Create a CLAUDE.md for one of your own projects

---

*Questions? Ask in `#ai-exchange` on Slack.*
