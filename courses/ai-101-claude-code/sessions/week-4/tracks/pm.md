# Week 4: TDD - PM Track

**Duration:** 2 hours
**Focus:** Writing Testable Requirements

---

## Why This Week Matters for PMs

Week 4 is where developers learn Test-Driven Development (TDD) - writing tests *before* code. For this to work, they need **clear, testable requirements**.

That's your job.

Bad requirements = developers guess what to test = bugs in production.
Good requirements = developers know exactly what to test = quality software.

**This week, you'll learn to write requirements that developers can directly translate into tests.**

---

## Learning Objectives

By the end of this session, you will be able to:

- [ ] Write acceptance criteria in Given/When/Then format
- [ ] Identify what makes requirements "testable"
- [ ] Use Claude to discover edge cases you missed
- [ ] Collaborate effectively with QA and developers on test scenarios

---

## Part 1: What Makes Requirements Testable? (20 min)

### Testable vs. Untestable Requirements

| Untestable | Testable |
|------------|----------|
| "The system should be fast" | "Page loads in under 2 seconds" |
| "Users can manage violations" | "Users can create, view, edit, and delete violations" |
| "Late fees should be calculated correctly" | "Late fees are 10% of balance, compounded monthly after 30-day grace period" |
| "The UI should be user-friendly" | "Users can complete payment in 3 clicks or fewer" |

### The INVEST Criteria

Good requirements are:

| Criteria | Meaning | Example |
|----------|---------|---------|
| **I**ndependent | Can be developed alone | "Create violation" doesn't depend on "Send notification" |
| **N**egotiable | Details can be discussed | Exact fee percentage can be adjusted |
| **V**aluable | Delivers user/business value | "Calculate late fees" helps collect revenue |
| **E**stimable | Team can estimate effort | Clear scope, no unknowns |
| **S**mall | Fits in a sprint | Not "build entire payment system" |
| **T**estable | Has clear pass/fail criteria | Given X, When Y, Then Z |

---

## Part 2: Given/When/Then Format (20 min)

### The Format

```gherkin
Given [precondition/context]
When [action/trigger]
Then [expected outcome]
```

### HOA Examples

**Late Fee Calculation:**

```gherkin
Given a homeowner has a balance of $500
And the balance is 31 days past due
When the system calculates late fees
Then a $50 late fee (10%) is added to the balance
And the new balance is $550
```

**Violation Escalation:**

```gherkin
Given a violation was created 30 days ago
And the violation status is "Open"
And no response has been received
When the escalation job runs
Then the violation status changes to "Warning Sent"
And a warning letter is generated
And the homeowner receives an email notification
```

**Payment Processing:**

```gherkin
Given a homeowner has a balance of $200
When they submit a payment of $150
Then their balance is reduced to $50
And a payment receipt is generated
And the payment appears in their transaction history
```

### Multiple Scenarios for One Feature

A single feature needs multiple Given/When/Then scenarios:

**Feature: Late Fee Calculation**

```gherkin
Scenario: Balance within grace period
Given a homeowner has a balance of $500
And the balance is 25 days past due
When the system calculates late fees
Then no late fee is added
And the balance remains $500

Scenario: Balance past grace period
Given a homeowner has a balance of $500
And the balance is 31 days past due
When the system calculates late fees
Then a $50 late fee is added
And the balance is $550

Scenario: Zero balance
Given a homeowner has a balance of $0
When the system calculates late fees
Then no late fee is added
And the balance remains $0

Scenario: Already has late fee this month
Given a homeowner has a balance of $550
And a late fee was already applied this month
When the system calculates late fees
Then no additional late fee is added
```

---

## Exercise 1: Write Acceptance Criteria (30 min)

### Scenario

You're writing requirements for the **Violation Escalation** feature:

> "Violations should automatically escalate based on time: Warning at 30 days, Fine at 60 days, Legal at 90 days."

### Your Task

Use Claude to help write comprehensive acceptance criteria.

### Prompt to Use

```
I need to write testable acceptance criteria for a violation escalation feature.

Business rules:
- Violations start in "Open" status
- At 30 days with no resolution: escalate to "Warning" and send warning letter
- At 60 days with no resolution: escalate to "Fine" and apply $100 fine
- At 90 days with no resolution: escalate to "Legal" and flag for attorney review
- If homeowner resolves at any point, escalation stops
- Days are calculated from violation creation date

Write Given/When/Then scenarios covering:
1. Happy path for each escalation level (30, 60, 90 days)
2. Escalation stops when resolved
3. Edge cases (exactly 30 days, 29 days, already escalated, etc.)
4. Error conditions (invalid dates, missing data)

Use Gherkin format. Be specific about dates, statuses, and outcomes.
```

### Success Criteria

Your acceptance criteria should cover:

- [ ] Normal escalation at 30, 60, 90 days
- [ ] No escalation before 30 days
- [ ] Escalation stops when resolved
- [ ] Edge case: exactly on the threshold (day 30 exactly)
- [ ] Edge case: violation resolved between checks
- [ ] Error case: missing creation date

---

## Part 3: Edge Case Discovery with Claude (20 min)

### Why Edge Cases Matter

Developers test the happy path. QA finds some edge cases. But **PMs who think about edge cases upfront save everyone time.**

### Using Claude for Edge Case Discovery

**Prompt Template:**

```
I'm writing requirements for [FEATURE].

The basic flow is:
[DESCRIBE HAPPY PATH]

What edge cases should I consider? Think about:
- Boundary conditions (zero, negative, maximum values)
- Timing issues (concurrent actions, race conditions)
- Invalid inputs (null, empty, wrong format)
- State transitions (what if already in target state?)
- Integration points (what if external system is down?)
- Business rule conflicts (what if two rules contradict?)

For each edge case, tell me:
1. What the edge case is
2. Why it matters
3. What the expected behavior should be
```

### Example: Payment Processing Edge Cases

**Prompt:**

```
I'm writing requirements for payment processing.

The basic flow is:
1. Homeowner enters payment amount
2. System validates payment
3. Payment is processed
4. Balance is updated

What edge cases should I consider?
```

**Claude might identify:**

| Edge Case | Why It Matters | Expected Behavior |
|-----------|----------------|-------------------|
| Payment > Balance | Overpayment handling | Apply as credit or reject? |
| Payment = $0 | Invalid input | Reject with error message |
| Negative payment | Invalid input | Reject, don't create refund |
| Payment during batch processing | Race condition | Queue or reject? |
| Balance changed since page load | Stale data | Refresh and confirm |
| Payment method declined | External failure | Show error, don't update balance |
| Duplicate submission | Double-click | Process once, ignore duplicates |

---

## Exercise 2: Edge Case Discovery (20 min)

### Scenario

You're writing requirements for **Assessment Due Date Calculation**:

> "Assessments are due on the 1st of each month. Late fees apply after the grace period."

### Your Task

Use Claude to identify edge cases, then write acceptance criteria for 3 of them.

### Prompt to Use

```
I'm writing requirements for HOA assessment due date calculation.

Business rules:
- Assessments are due on the 1st of each month
- Grace period is 30 days (no late fee until day 31)
- Late fee is 10% of assessment amount
- Late fees compound monthly

What edge cases should I consider for:
1. Due date calculation
2. Grace period boundaries
3. Late fee calculation
4. Month/year transitions

For each edge case, tell me why it matters and what should happen.
```

### Then Write Given/When/Then

Pick 3 edge cases and write full acceptance criteria for each.

---

## Part 4: Collaborating with QA and Dev (10 min)

### The Handoff

Your acceptance criteria become:

| Your AC | Developer Uses For | QA Uses For |
|---------|-------------------|-------------|
| Given/When/Then | Writing unit tests | Writing test cases |
| Edge cases | Defensive coding | Exploratory testing |
| Business rules | Implementation logic | Validation testing |

### What Developers Need from You

1. **Specific numbers** - Not "quickly" but "under 2 seconds"
2. **All scenarios** - Happy path AND edge cases AND errors
3. **Business context** - WHY this rule exists (helps them make judgment calls)
4. **Priority** - Which scenarios are must-have vs nice-to-have

### What QA Needs from You

1. **Testable criteria** - Clear pass/fail, not subjective
2. **Test data hints** - "Use balance of $500" is helpful
3. **Edge case awareness** - Don't make them discover everything
4. **Risk areas** - What breaks if this fails?

### The Three Amigos Meeting

Before development starts, PMs, Devs, and QA meet to review requirements:

| Role | Brings | Asks |
|------|--------|------|
| PM | Acceptance criteria, business context | "Does this make sense?" |
| Dev | Technical constraints, estimates | "How should edge case X behave?" |
| QA | Test scenarios, risk assessment | "What about scenario Y?" |

**Use Claude in the meeting** to quickly draft additional scenarios as they come up.

---

## Key Takeaways

1. **Testable requirements have clear pass/fail criteria** - Given/When/Then format ensures this

2. **Edge cases discovered early save time** - Use Claude to brainstorm before development starts

3. **Your AC becomes their tests** - Write requirements like you're writing test cases

4. **Collaboration beats documentation** - Three Amigos meeting > perfect spec document

---

## Homework

1. **Take a current feature you're working on** - Write Given/When/Then acceptance criteria
2. **Use Claude to find edge cases** - Add scenarios for at least 3 edge cases
3. **Review with a developer** - Ask: "Could you write tests from this?"

---

## What's Next

**Week 5: Commands & Skills** - You'll create PM automation tools:

- `/user-stories` - Break epics into stories
- `/release-notes` - Generate stakeholder updates
- `/meeting-actions` - Extract action items

The acceptance criteria skills you learned today will make your `/user-stories` output much better!

---

## Quick Reference: Given/When/Then Cheat Sheet

```gherkin
# Basic structure
Given [context/precondition]
When [action/trigger]
Then [outcome/result]

# With And for multiple conditions
Given [context 1]
And [context 2]
When [action]
Then [outcome 1]
And [outcome 2]

# With But for negative assertions
Given [context]
When [action]
Then [expected outcome]
But [should NOT happen]
```

---

*Return to [Week 4 README](../README.md) | See also: [QA Track](qa.md)*
