# Week 6: Agents & Hooks - Support Exercise

**Duration:** 45-60 min
**Prerequisites:** Complete main README through Part 2 (Understanding Hooks)

---

## Your Mission

You'll learn how hooks can automate quality checks for your support work. By the end, you'll understand how to set up automated workflows that improve response consistency.

---

## Exercise 1: Understanding Hooks for Support (15-20 min)

### Context

Hooks run automatically when certain events happen. For support work, hooks could review responses before sending, check for missing information, or log communications.

### The Task

1. Start Claude in your workspace:

   ```bash
   cd courses/ai-101-claude-code/sessions/week-6
   cp -r examples/quality-hooks sandbox/
   cd sandbox/quality-hooks
   claude
   ```

2. Explore what hooks could do for support:

   ```text
   What types of hooks would be useful for support work?
   Consider:
   - PreToolUse hooks (before Claude executes an action)
   - PostToolUse hooks (after Claude completes an action)
   - Validation hooks (checking quality before proceeding)

   Give me 5 specific examples for customer support.
   ```

3. Design a response quality hook:

   ```text
   Help me design a hook that would review customer responses
   before they're sent. It should check:
   - Professional tone
   - All questions answered
   - Correct policy references
   - Appropriate length

   What would this hook look like conceptually?
   ```

4. Understand the workflow:

   ```text
   Walk me through how this hook would work:
   1. I draft a response
   2. What triggers the hook?
   3. What does the hook check?
   4. What feedback does it give?
   5. How do I address the feedback?
   ```

### Success Criteria

- [ ] Identified 5 potential support hooks
- [ ] Designed a response quality hook
- [ ] Understand the hook workflow

---

## Exercise 2: Workflow Automation Concepts (15-20 min)

### Context

Agents can handle multi-step tasks autonomously. For support, this could mean gathering information, researching policies, or preparing escalation packages.

### The Task

1. Explore agent possibilities:

   ```text
   If I could have an AI agent help with support tasks,
   what multi-step workflows would be most valuable?

   Consider:
   - Ticket triage (categorize, prioritize, route)
   - Research assistance (find relevant policies)
   - Response preparation (gather context, draft)
   - Escalation packaging (compile history, summarize)
   ```

2. Design an escalation agent workflow:

   ```text
   Design an agent workflow for preparing escalations:

   Input: Ticket number and issue summary

   Steps the agent should take:
   1. What information should it gather?
   2. What research should it do?
   3. What documents should it compile?
   4. What summary should it generate?

   Output: What should the final escalation package include?
   ```

3. Consider the human-in-the-loop:

   ```text
   Where should this agent pause for human review?
   What decisions should NOT be automated?
   What guardrails would you want?
   ```

### Success Criteria

- [ ] Identified valuable support automation workflows
- [ ] Designed an escalation agent workflow
- [ ] Identified appropriate human checkpoints

---

## Exercise 3: Build a Response Validator (15-20 min)

### Context

Now let's make it hands-on. You'll create a validation document that could be used to check response quality - similar to how developers write test suites.

### The Task

1. Create a validation rules document:

   ```text
   Help me create a "Response Validation Rules" document.
   For each rule, include:
   - Rule ID (RV-001, RV-002, etc.)
   - Rule name
   - What to check
   - Pass criteria
   - Fail criteria
   - Severity (Critical/Warning/Info)

   Create at least 8 rules covering:
   - Greeting and closing
   - Tone and empathy
   - Information accuracy
   - Next steps clarity
   - Response length
   - Forbidden content
   - Policy references
   - Contact information
   ```

2. Test the validator against real responses:

   ```text
   Use the validation rules to review this response:

   "Hi,

   Your account shows a late fee of $75. This was applied on 1/15
   because your payment was received after the due date.

   Per CC&R Section 4.2, late fees are 10% of the amount due.

   You can pay online at portal.realmanage.com or call us.

   Thanks,
   Support Team"

   Score each rule: PASS, FAIL, or N/A
   Calculate overall pass rate.
   ```

3. Improve a failing response:

   ```text
   Here's a response that likely fails several validation rules:

   "Fee is $75. Pay it."

   Rewrite this response to pass ALL validation rules.
   Show the before/after comparison with scores.
   ```

4. Create a validation summary template:

   ```text
   Create a template I can use to document validation results:

   RESPONSE VALIDATION REPORT
   --------------------------
   Response ID: ___
   Date: ___
   Validator: ___

   RULE RESULTS:
   [Table of all rules with PASS/FAIL]

   OVERALL SCORE: ___/8 rules passed

   ISSUES TO FIX:
   [List of failed rules with specific feedback]

   APPROVED FOR SEND: YES / NO
   ```

### Success Criteria

- [ ] Created 8+ validation rules
- [ ] Tested rules against a real response
- [ ] Improved a failing response to pass
- [ ] Created a validation summary template

---

## Wrap-Up

### What You Completed

- [ ] Explored hook concepts for support quality
- [ ] Designed automation workflows
- [ ] Understood human-in-the-loop principles
- [ ] Built a response validation system with rules and testing

### Key Takeaways

Return to the [main README](../README.md#-key-takeaways) for shared learning points.

### Before Next Week

Think about which manual support tasks would benefit most from automation. Note where quality checks could prevent errors.

---

*Questions? Ask in `#ai-exchange` or during office hours.*
