# Week 3: Tactical Planning - Developer Exercise

**Duration:** 45-60 min
**Prerequisites:** Complete README.md through Part 2 (Plan Mode fundamentals)

---

## Your Mission

You'll use plan mode to systematically investigate a bug, organize code review fixes, and execute multi-step tasks. By the end, you'll understand when and how to use plan mode effectively.

---

## Exercise 1: Bug Investigation with Plan Mode (20-25 min)

### Context

Users report "Interest calculations are wrong after 90 days." You'll use plan mode to investigate methodically before diving into code.

### The Task

1. Set up the bug-hunter project:

   ```bash
   cd courses/ai-101-claude-code/sessions/week-3
   cp -r examples/bug-hunter sandbox/
   cd sandbox/bug-hunter
   claude
   ```

2. Enter plan mode (Shift+Tab until you see "plan" indicator):

   ```
   /plan
   ```

3. Ask Claude to help plan the investigation:

   ```
   Users report: "Interest calculations are wrong after 90 days"
   Help me plan how to investigate this bug systematically.
   ```

4. Review Claude's investigation plan. Iterate on it:

   ```
   Good start. Also include checking the test coverage for
   the interest calculation method.
   ```

5. When satisfied, exit plan mode and execute the investigation

6. Once you find the bug, enter plan mode again:

   ```
   /plan
   I found the bug in [describe location]. Plan how to fix it
   with proper tests.
   ```

7. Execute the fix, then verify with tests:

   ```bash
   dotnet test
   ```

### Success Criteria

- [ ] Used plan mode to organize investigation
- [ ] Iterated on the plan before executing
- [ ] Found the calculation bug
- [ ] Used plan mode again to organize the fix
- [ ] Tests pass after fix

---

## Exercise 2: Code Review with Plan Mode (20-25 min)

### Context

You received 8+ code review comments. Plan mode helps you organize and execute them systematically.

### The Task

1. Navigate to codereview-pro:

   ```bash
   cd courses/ai-101-claude-code/sessions/week-3/sandbox
   cp -r ../examples/codereview-pro .
   cd codereview-pro
   claude
   ```

2. Switch to Opus for deep analysis:

   ```
   /model opus
   ```

3. Request a thorough code review:

   ```
   Review PaymentService.cs, LateFeecalculator.cs, and PaymentController.cs.
   Find all issues: bugs, security vulnerabilities, performance problems,
   code smells, and missing test coverage.
   ```

4. Switch back to Sonnet for implementation:

   ```
   /model sonnet
   ```

5. Enter plan mode and organize the fixes:

   ```
   /plan
   Organize these [N] issues into a systematic fix plan.
   Group related fixes together (e.g., all security issues, all perf issues).
   ```

6. Iterate on the plan:

   ```
   Let's do the security fixes first, then logic bugs, then performance.
   ```

7. Exit plan mode and execute the fixes systematically

8. Verify all fixes:

   ```bash
   dotnet build
   dotnet test
   ```

### Success Criteria

- [ ] Used Opus for thorough review
- [ ] Switched back to Sonnet for implementation
- [ ] Used plan mode to organize 8+ fixes
- [ ] Iterated on the grouping/order
- [ ] Executed systematically
- [ ] Build passes with no warnings

---

## Wrap-Up

### What You Completed

- [ ] Used plan mode for investigation before diving in
- [ ] Iterated on plans before execution
- [ ] Organized code review fixes systematically
- [ ] Experienced Opus vs Sonnet model selection

### Key Takeaways

Return to the [main README](../README.md#-key-takeaways) for shared learning points.

### Before Next Week

Practice using plan mode for your next real code review. Note how it helps you avoid forgetting items.

---

*Questions? Ask in `#ai-exchange` or during office hours.*
