# Week 3: Tactical Planning - PM Exercise

**Duration:** 30-45 min
**Prerequisites:** Complete README.md through Part 2 (Plan Mode fundamentals)

---

## Your Mission

You'll use plan mode to organize feature planning, phased delivery, and trade-off analysis. By the end, you'll understand how plan mode helps ensure comprehensive thinking before execution.

---

## Exercise 1: Feature Planning with Plan Mode (15-20 min)

### Context

Stakeholders want a new payment plan feature. Plan mode helps you think through the requirements systematically.

### The Task

1. Copy the example to your sandbox and start Claude:

   ```bash
   cd courses/ai-101-claude-code/sessions/week-3
   cp -r examples/phased-builder sandbox/
   cd sandbox/phased-builder
   claude
   ```

2. Enter plan mode:

   ```text
   /plan
   ```

3. Ask Claude to help plan the feature:

   ```text
   We need to add payment plans to our HOA system.

   High-level requirements:
   - Residents can request payment plans for overdue balances
   - Plans split balance into monthly installments
   - Late fees pause while plan is active
   - System tracks plan status and payments

   Help me plan how to break this into phases for implementation.
   ```

4. Iterate on the phasing:

   ```text
   Good, but Phase 1 seems too big for a 2-week sprint.
   Can we break it down further? What's the absolute MVP?
   ```

5. Ask about trade-offs:

   ```text
   What are the trade-offs between these phasing options?
   What risks should we consider for each phase?
   ```

6. Exit plan mode and generate documentation:

   ```text
   Generate a phased delivery document with:
   - Phase name and goal
   - User stories (high-level)
   - Dependencies
   - Risks and mitigations
   - Success criteria
   ```

### Success Criteria

- [ ] Used plan mode to organize feature approach
- [ ] Iterated on the phasing until it felt right
- [ ] Identified trade-offs and risks
- [ ] Generated phased delivery documentation

---

## Exercise 2: Requirements Analysis with Plan Mode (15-20 min)

### Context

You need to review a rough feature request and identify gaps. Plan mode helps you think systematically.

### The Task

1. Enter plan mode with a requirements document:

   ```text
   /plan

   Feature request: "Online payment portal for residents"
   - Residents should pay dues online
   - Support credit card and ACH
   - Show payment history
   - Send email receipts

   Help me plan how to analyze this for completeness.
   What questions should I ask? What's missing?
   ```

2. Review Claude's analysis plan. Add PM concerns:

   ```text
   Also include:
   - What edge cases should we consider?
   - What error scenarios need handling?
   - What are the security/compliance requirements?
   - What's the MVP vs nice-to-have?
   ```

3. Exit plan mode and do the analysis:

   ```text
   Based on our plan, analyze this feature request.
   Generate a list of clarifying questions for stakeholders.
   ```

4. Create a mini-PRD:

   ```text
   Generate a PRD outline for this feature with:
   - Problem statement
   - User personas
   - Requirements (MVP vs future)
   - Acceptance criteria
   - Out of scope
   - Open questions
   ```

### Success Criteria

- [ ] Used plan mode to organize analysis approach
- [ ] Identified gaps in the feature request
- [ ] Generated clarifying questions
- [ ] Created a PRD outline

---

## Wrap-Up

### What You Completed

- [ ] Used plan mode for feature planning
- [ ] Analyzed requirements systematically
- [ ] Generated product documentation

### Key Takeaways

Return to the [main README](../README.md#-key-takeaways) for shared learning points.

### Before Next Week

Use plan mode when breaking down your next feature. Note how iterating on the plan before execution saves rework.

---

*Questions? Ask in `#ai-exchange` on Slack.*
