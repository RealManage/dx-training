# Week 3: Tactical Planning - Support Exercise

**Duration:** 30-45 min
**Prerequisites:** Complete README.md through Part 2 (Plan Mode fundamentals)

---

## Your Mission

You'll use plan mode to organize issue triage, escalation planning, and knowledge base organization. By the end, you'll understand how plan mode helps ensure nothing gets missed in complex support work.

---

## Exercise 1: Issue Triage with Plan Mode (15-20 min)

### Context

Multiple tickets came in overnight about billing issues. Plan mode helps you organize your triage approach.

### The Task

1. Navigate to the support example and start Claude:

   ```bash
   cd courses/ai-101-claude-code/sessions/week-3
   mkdir -p sandbox
   cp -r examples/escalation-planner sandbox/
   cd sandbox/escalation-planner
   claude
   ```

2. Enter plan mode:

   ```
   /plan
   ```

3. Present the triage scenario:

   ```
   I have 5 tickets to triage this morning:

   1. Resident says late fee is wrong - owes $133, expected $110
   2. New resident can't access portal after moving in yesterday
   3. Board member needs urgent report on delinquent accounts
   4. Resident asking how to appeal a violation fine
   5. Payment didn't go through, resident says it worked before

   Help me plan how to prioritize and respond to these.
   Consider urgency, impact, and who's affected.
   ```

4. Iterate on the prioritization:

   ```
   Good analysis. But the board member request - is that really lower
   priority than the late fee question? What factors should we consider?
   ```

5. Exit plan mode and work through the highest priority:

   ```
   For ticket #[highest priority], draft a response that:
   - Acknowledges their concern
   - Explains what you'll do
   - Sets expectations for resolution time
   ```

### Success Criteria

- [ ] Used plan mode to organize triage approach
- [ ] Considered multiple prioritization factors
- [ ] Iterated on the prioritization rationale
- [ ] Drafted a response for the top priority ticket

---

## Exercise 2: Escalation Planning (15-20 min)

### Context

A complex issue requires escalation. Plan mode helps you organize what information to include and who to involve.

### The Task

1. Enter plan mode with an escalation scenario:

   ```
   /plan

   A resident's account shows:
   - They paid $500 last month (confirmed in system)
   - Balance still shows $500 owed
   - Late fees added this week ($50)
   - They're threatening legal action

   Help me plan how to escalate this properly:
   - What information do I need to gather?
   - Who should be involved?
   - What's the timeline expectation?
   - What do I tell the resident while investigating?
   ```

2. Iterate on the plan:

   ```
   Add to the plan: What documentation should I prepare
   in case this does go to legal? What audit trail do we need?
   ```

3. Exit plan mode and create the escalation:

   ```
   Generate an internal escalation email that includes:
   - Summary of the issue
   - Timeline of events
   - Customer sentiment
   - What we've tried
   - What we need from the escalation team
   - Recommended next steps
   ```

4. Draft the customer communication:

   ```
   Draft a response to the resident that:
   - Acknowledges the seriousness
   - Explains we're investigating urgently
   - Commits to a follow-up timeline
   - De-escalates without promising specific outcomes
   ```

### Success Criteria

- [ ] Used plan mode to organize escalation approach
- [ ] Identified all stakeholders and information needed
- [ ] Created internal escalation documentation
- [ ] Drafted appropriate customer communication

---

## Wrap-Up

### What You Completed

- [ ] Used plan mode for ticket triage
- [ ] Organized escalation systematically
- [ ] Created professional escalation documentation

### Key Takeaways

Return to the [main README](../README.md#-key-takeaways) for shared learning points.

### Before Next Week

Use plan mode when handling your next complex escalation. Note how organizing before acting helps ensure nothing is missed.

---

*Questions? Ask in `#ai-exchange` or during office hours.*
