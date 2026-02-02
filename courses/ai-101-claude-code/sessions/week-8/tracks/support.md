# Week 8: Real-World Automation - Support Track

**Duration:** 30-45 min
**Audience:** Support Engineers, Customer Success
**Prerequisites:** Complete main README through Part 2 (Automation Patterns)

---

## Learning Objectives

By the end of this session, you will be able to:

- Design ticket triage automation workflows
- Create response generation automation for common issues
- Handle edge cases and messy real-world inputs
- Plan automation that improves support efficiency

---

## Your Mission

You'll design real-world automation workflows for support tasks. By the end, you'll have a plan for automating ticket triage and response generation.

---

## Exercise 1: Ticket Triage Automation (15-20 min)

### Context

Real-world automation handles messy inputs and edge cases. Ticket triage is a perfect example - inconsistent formats, missing information, and varying urgency.

### The Task

1. Start Claude in your workspace:

   ```bash
   cd courses/ai-101-claude-code/sessions/week-8
   cp -r examples/ticket-automation sandbox/
   cd sandbox/ticket-automation
   claude
   ```

2. Design an automated triage workflow:

   ```
   Design an automation workflow for ticket triage that:

   Input: Raw ticket text from email/form

   Processing steps:
   1. Extract key information (account, issue type, urgency)
   2. Categorize the issue (billing, violation, maintenance, etc.)
   3. Determine priority (urgent, normal, low)
   4. Identify required information that's missing
   5. Route to appropriate queue

   Output: Structured ticket with category, priority, and routing

   What edge cases should this handle?
   ```

3. Handle the messy real world:

   ```
   Here are 3 real ticket examples. Show how your triage
   workflow would handle each:

   1. "WHY IS MY BILL SO HIGH??? I PAID LAST MONTH!!!"
   2. "Question about the upcoming board meeting schedule and also
      I noticed a broken sprinkler on Elm Street"
   3. "Please call me back at 555-1234"

   What information is missing? How should each be categorized?
   ```

4. Define the human checkpoint:

   ```
   When should this automation pause for human review?
   What confidence thresholds make sense?
   What errors should trigger escalation?
   ```

### Success Criteria

- [ ] Designed a triage workflow with clear steps
- [ ] Identified edge cases and how to handle them
- [ ] Tested against messy real-world examples
- [ ] Defined human review checkpoints

---

## Exercise 2: Response Generation Automation (15-20 min)

### Context

Automated responses must be accurate, empathetic, and know when NOT to respond automatically.

### The Task

1. Design response automation criteria:

   ```
   When should we auto-generate a response vs. require human review?

   Consider:
   - Issue complexity
   - Customer sentiment
   - Policy sensitivity
   - Account history
   - Legal implications

   Create a decision matrix for when to automate.
   ```

2. Build a response generation workflow:

   ```
   Design an automation for generating first responses that:

   Input: Categorized ticket with extracted information

   Steps:
   1. Match to appropriate template
   2. Fill in ticket-specific details
   3. Add empathy statement based on sentiment
   4. Include relevant policy information
   5. Quality check against our standards

   Output: Draft response ready for review (or auto-send if simple)

   What guardrails prevent bad responses from going out?
   ```

3. Create a quality gate:

   ```
   Design a quality check that validates automated responses:

   - Tone appropriate for sentiment?
   - All questions addressed?
   - Policy information accurate?
   - No forbidden phrases used?
   - Length appropriate?

   What score should trigger human review?
   ```

### Success Criteria

- [ ] Created decision matrix for automation vs. human
- [ ] Designed response generation workflow
- [ ] Built quality gate criteria
- [ ] Defined guardrails and thresholds

---

## Wrap-Up

### What You Completed

- [ ] Designed ticket triage automation
- [ ] Created response generation workflow
- [ ] Defined quality gates and human checkpoints

### Key Takeaways

Return to the [main README](../README.md#-key-takeaways) for shared learning points.

### Before Next Week

Think about which parts of your daily workflow could benefit from automation. What quality checks would you want in place?

---

*Questions? Ask in `#ai-exchange` or during office hours.*
