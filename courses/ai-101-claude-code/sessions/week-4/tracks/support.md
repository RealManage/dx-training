# Week 4: TDD Concepts - Support Exercise

**Duration:** 45-60 min
**Prerequisites:** Complete main README through Part 1 (Why TDD Matters)

---

## Your Mission

You'll learn how TDD principles apply to support work: defining clear acceptance criteria and testing your responses before sending them. By the end, you'll create a "test-first" approach to customer communication.

---

## Exercise 1: Test-First Response Writing (15-20 min)

### Context

In TDD, developers write tests before code. For support, you can define "success criteria" before drafting responses. This ensures your response addresses everything needed.

### The Task

1. Start Claude in the support example:

   ```bash
   cd courses/ai-101-claude-code/sessions/week-4
   cp -r examples/response-criteria sandbox/
   cd sandbox/response-criteria
   claude
   ```

2. Define acceptance criteria for a response before writing it:

   ```
   A resident is disputing a $150 late fee. Before I draft a response,
   help me define what a successful response MUST include:

   - What questions must be answered?
   - What information must be provided?
   - What tone criteria should be met?
   - What action items should be clear?

   Create a checklist I can use to "test" my response.
   ```

3. Now draft the response:

   ```
   Draft a response to the late fee dispute.
   Make sure it passes all the criteria we just defined.
   ```

4. "Test" the response against your criteria:

   ```
   Review the response against our acceptance criteria.
   Does it pass all requirements? What's missing?
   ```

5. Iterate until the response passes all criteria

### Success Criteria

- [ ] Defined acceptance criteria BEFORE writing
- [ ] Draft response created
- [ ] Reviewed response against criteria
- [ ] Iterated until all criteria passed

---

## Exercise 2: Response Template Testing (15-20 min)

### Context

Just like developers write test cases for code, you can create "test cases" for your response templates to ensure they work in various scenarios.

### The Task

1. Create a template with Claude:

   ```
   Create a response template for explaining late fees.
   Include placeholders for [AMOUNT], [DUE_DATE], [CURRENT_BALANCE].
   ```

2. Now create "test scenarios" for this template:

   ```
   Create 5 test scenarios for this template:
   1. Normal case: $50 fee on $500 balance
   2. Edge case: Fee larger than original amount due
   3. Edge case: Multiple months of fees accumulated
   4. Error case: Resident claims they already paid
   5. Special case: First-time offender requesting waiver

   For each, show what the filled-in template would look like.
   ```

3. Identify gaps in the template:

   ```
   Based on those test scenarios, does the template need adjustment?
   What edge cases don't work well with the current template?
   ```

4. Improve the template based on your "test results"

### Success Criteria

- [ ] Created a response template
- [ ] Defined 5 test scenarios
- [ ] Identified template gaps through testing
- [ ] Improved template based on findings

---

## Exercise 3: Quality Checklist Creation (15-20 min)

### Context

QA engineers use test suites to verify code quality. You can create similar quality checklists to verify response quality before sending.

### The Task

1. Build a universal quality checklist:

   ```
   Create a quality checklist that every customer response should pass.
   Consider:
   - Tone and empathy requirements
   - Information completeness
   - Action item clarity
   - Policy accuracy
   - Response length guidelines
   - Forbidden phrases to avoid

   Format as a yes/no checklist I can quickly scan.
   ```

2. Test the checklist against real responses:

   ```
   Here are 3 draft responses. Grade each against our quality checklist:

   Response 1: "Your late fee is $150 because you paid late. Pay by Friday."

   Response 2: "Hi Sarah, I understand the late fee was unexpected. Let me
   explain how it was calculated. Your payment of $500 was due on 1/1, and
   we received it on 1/15. Per policy section 4.2, a 10% late fee ($50)
   applies after the 5-day grace period. To resolve this, you can pay online
   at portal.realmanage.com or call 555-1234. Please let me know if you have
   questions!"

   Response 3: "I see you're upset about the fee. Unfortunately fees are
   non-negotiable per board policy. You'll need to pay it."

   Score each and explain what passed/failed.
   ```

3. Identify patterns in failures:

   ```
   Based on the responses that failed checklist items,
   what are the most common quality issues?
   How could I avoid these in future responses?
   ```

4. Create a "pre-flight" checklist for your workflow:

   ```
   Turn the quality checklist into a quick "pre-flight" format
   I can scan in under 30 seconds before sending any response.
   ```

### Success Criteria

- [ ] Created universal quality checklist
- [ ] Graded 3 sample responses
- [ ] Identified common failure patterns
- [ ] Created quick pre-flight checklist

---

## Wrap-Up

### What You Completed

- [ ] Applied "test-first" thinking to response writing
- [ ] Created acceptance criteria before drafting
- [ ] "Tested" templates with multiple scenarios
- [ ] Built a quality checklist for all responses

### Key Takeaways

The TDD mindset applies beyond code:

- **Define success criteria first** - Know what "good" looks like before starting
- **Test against criteria** - Check your work systematically
- **Iterate based on failures** - Improve when criteria aren't met

Return to the [main README](../README.md#-key-takeaways) for shared learning points.

---

*Questions? Ask in `#ai-exchange` or during office hours.*
