# Week 3: Tactical Planning - QA Exercise

**Duration:** 30-45 min
**Prerequisites:** Complete README.md through Part 2 (Plan Mode fundamentals)

---

## Your Mission

You'll use plan mode to organize test planning, defect analysis, and regression planning. By the end, you'll understand how plan mode helps ensure nothing gets missed in complex QA work.

---

## Exercise 1: Test Planning with Plan Mode (15-20 min)

### Context

A new payment feature needs comprehensive testing. Plan mode helps you organize your test approach before diving in.

### The Task

1. Navigate to the example and start Claude:

   ```bash
   cd courses/ai-101-claude-code/sessions/week-3/examples/bug-hunter
   claude
   ```

2. Enter plan mode:

   ```
   /plan
   ```

3. Ask Claude to help plan test coverage:

   ```
   I need to create a test plan for the interest calculation feature.

   Business rules:
   - 30-day grace period
   - 10% monthly compound interest after grace
   - 30/60/90 day escalation timeline

   Help me plan comprehensive test coverage including:
   - Happy path tests
   - Edge cases
   - Boundary conditions
   - Error scenarios
   ```

4. Iterate on the test plan:

   ```
   Good start. Add regression test scenarios - what should we test
   to ensure bug fixes don't break existing behavior?
   ```

5. Ask for prioritization:

   ```
   If we only had time for 10 tests, which would you prioritize
   based on risk and business impact?
   ```

6. Exit plan mode and ask Claude to generate the test documentation:

   ```
   Generate a test case document based on our plan.
   Format as a table with: ID, Description, Steps, Expected Result, Priority
   ```

### Success Criteria

- [ ] Used plan mode to organize test approach
- [ ] Iterated on the test plan
- [ ] Identified priority test cases
- [ ] Generated test documentation from the plan

---

## Exercise 2: Defect Analysis with Plan Mode (15-20 min)

### Context

A bug was reported. Plan mode helps you systematically investigate and document your findings.

### The Task

1. Enter plan mode with a defect to analyze:

   ```
   /plan

   Bug report: "Interest calculations wrong after 90 days"
   - Reported by: Accounting team
   - Environment: Production
   - Impact: Incorrect billing amounts

   Help me plan a systematic investigation.
   ```

2. Review Claude's investigation plan. Add QA perspective:

   ```
   Add to the plan:
   - What test data do we need to reproduce this?
   - What related areas should we regression test?
   - How do we verify the fix didn't break anything?
   ```

3. Exit plan mode and investigate:

   ```
   Look at the interest calculation code in this project.
   What's the bug and how would you verify it?
   ```

4. Document findings with a defect report:

   ```
   Generate a defect report with:
   - Summary
   - Root cause
   - Steps to reproduce
   - Test data needed
   - Regression impact
   - Recommended fix verification steps
   ```

### Success Criteria

- [ ] Used plan mode to organize investigation
- [ ] Added QA-specific considerations to the plan
- [ ] Identified the bug and root cause
- [ ] Created comprehensive defect documentation

---

## Wrap-Up

### What You Completed

- [ ] Used plan mode for test planning
- [ ] Organized defect analysis systematically
- [ ] Generated test and defect documentation

### Key Takeaways

Return to the [main README](../README.md#-key-takeaways) for shared learning points.

### Before Next Week

Use plan mode when planning test coverage for your next feature. Note how it helps ensure comprehensive coverage.

---

*Questions? Ask in `#ai-exchange` or during office hours.*
