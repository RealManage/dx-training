# Week 2: Prompting Foundations - QA Exercise

**Duration:** 30-45 min
**Prerequisites:** Complete README.md through "Hands-On Exercises" section

---

## Your Mission

You'll practice crafting prompts that generate comprehensive test scenarios and coverage analysis. By the end, you'll have prompt templates for your most common QA tasks.

---

## Before You Start

> **Learning Mode Recommended:** If you haven't already, run `/output-style` and select **Learning** or **Explanatory** mode for better understanding.

---

## Exercise 1: Test Scenario Generation (15-20 min)

### Context

Effective prompts help Claude generate thorough test cases. You'll practice improving your prompts to get better test coverage.

### The Task

1. Set up your sandbox and start Claude:

   ```bash
   cd courses/ai-101-claude-code/sessions/week-2
   cp -r examples/prompt-lab sandbox/
   cd sandbox/prompt-lab
   claude
   ```

2. Start with a vague prompt:

   ```
   Generate test cases for a payment system
   ```

3. Notice what's missing. Now try a specific prompt:

   ```
   Generate test cases for an HOA payment processing feature:

   Feature: Monthly dues payment
   - Accepts credit card, ACH, and check payments
   - 30-day grace period before late fees
   - 10% monthly compound interest after grace period
   - Minimum payment is $25 or full balance

   Include:
   - Happy path scenarios
   - Edge cases (boundary conditions)
   - Error scenarios
   - Data requirements for each test
   - Expected results
   ```

4. Compare the results. What did the specific prompt catch that the vague one missed?

5. Ask Claude to identify gaps:

   ```
   Review these test cases and identify any scenarios I might have missed.
   Consider security, performance, and integration edge cases.
   ```

### Success Criteria

- [ ] Experienced the difference in test coverage between vague and specific prompts
- [ ] Got comprehensive test scenarios including edge cases
- [ ] Had Claude identify additional test scenarios
- [ ] Understand why specificity matters for QA prompts

---

## Exercise 2: QA Prompt Templates (15-20 min)

### Context

You'll create reusable prompt templates for your most common QA tasks.

### The Task

1. Ask Claude to help identify common QA patterns:

   ```
   What are the most common types of test documentation QA engineers need to create?
   Give me 5 categories with examples.
   ```

2. Create a "Test Case Generation" template:

   ```
   Help me create a reusable prompt template for generating test cases.
   Include placeholders for:
   - [FEATURE_NAME]
   - [FEATURE_DESCRIPTION]
   - [BUSINESS_RULES]
   - [ACCEPTANCE_CRITERIA]
   - [INTEGRATION_POINTS]

   The template should generate:
   - Happy path tests
   - Edge cases
   - Error scenarios
   - Required test data
   - Expected results
   ```

3. Test your template on a real feature:

   ```
   [Use the template to generate test cases for the violation
   escalation feature: violations escalate at 30/60/90 day intervals,
   with increasing fines and notification to board at 90 days]
   ```

4. Create a "Bug Analysis" template:

   ```
   Create a prompt template for analyzing a bug report.
   Include placeholders for:
   - [BUG_DESCRIPTION]
   - [STEPS_TO_REPRODUCE]
   - [EXPECTED_VS_ACTUAL]

   The template should help identify:
   - Root cause hypotheses
   - Additional test scenarios to verify
   - Regression test recommendations
   - Related areas to check
   ```

5. Save your templates for future use

### Success Criteria

- [ ] Created at least 2 QA-focused prompt templates
- [ ] Templates generate comprehensive test coverage
- [ ] Tested a template successfully
- [ ] Saved templates for future use

---

## Wrap-Up

### What You Completed

- [ ] Practiced prompt refinement for test generation
- [ ] Built reusable QA prompt templates
- [ ] Used Claude to improve test coverage

### Key Takeaways

Return to the [main README](../README.md#-key-takeaways) for shared learning points.

### Before Next Week

Use your templates on a real testing task this week. Note which prompts worked well and which need refinement.

---

*Questions? Ask in `#ai-exchange` or during office hours.*
