# Week 2: Prompting Foundations - PM Exercise

**Duration:** 30-45 min
**Prerequisites:** Complete README.md through "Hands-On Exercises" section

---

## Your Mission

You'll practice crafting prompts that generate clear requirements, user stories, and product documentation. By the end, you'll have prompt templates for your most common PM tasks.

---

## Before You Start

> **Learning Mode Recommended:** If you haven't already, run `/output-style` and select **Learning** or **Explanatory** mode for better understanding.

---

## Exercise 1: Requirements Refinement (15-20 min)

### Context

Vague requirements lead to vague outputs. You'll practice turning rough ideas into clear prompts that generate actionable specs.

### The Task

1. Set up your sandbox and start Claude:

   ```bash
   cd courses/ai-101-claude-code/sessions/week-2
   mkdir -p sandbox
   cp -r examples/prompt-lab sandbox/
   cd sandbox/prompt-lab
   claude
   ```

2. Start with a vague prompt:

   ```
   Write user stories for a payment feature
   ```

3. Notice what's missing. Now try a specific prompt:

   ```
   Generate user stories for an HOA online payment feature:

   Context:
   - Residents need to pay monthly dues online
   - Currently they mail checks or call to pay by phone
   - We support credit card, ACH, and check payments

   Business rules:
   - 30-day grace period before late fees apply
   - 10% monthly compound interest after grace period
   - Minimum payment is $25 or full balance (whichever is less)
   - Auto-pay option available

   Generate user stories with:
   - Title
   - As a [role], I want [goal], so that [benefit]
   - Acceptance criteria (Given/When/Then)
   - Edge cases to consider
   - Suggested priority (MVP vs future)
   ```

4. Compare the results. What did the specific prompt include that the vague one missed?

5. Ask Claude to identify gaps:

   ```
   Review these user stories and identify:
   - Missing scenarios I should consider
   - Questions I should ask engineering
   - Potential risks or dependencies
   ```

### Success Criteria

- [ ] Experienced the difference between vague and specific prompts
- [ ] Got detailed user stories with acceptance criteria
- [ ] Had Claude identify gaps and questions
- [ ] Understand why context matters for PM prompts

---

## Exercise 2: PM Prompt Templates (15-20 min)

### Context

You'll create reusable prompt templates for your most common PM tasks.

### The Task

1. Ask Claude about common PM documentation needs:

   ```
   What are the most common types of documentation product managers create?
   Give me 5 categories with examples relevant to B2B SaaS.
   ```

2. Create a "User Story Generator" template:

   ```
   Help me create a reusable prompt template for generating user stories.
   Include placeholders for:
   - [FEATURE_NAME]
   - [USER_TYPES] (who uses this)
   - [CURRENT_STATE] (how it works today)
   - [DESIRED_STATE] (what we want)
   - [BUSINESS_RULES]

   The template should generate:
   - User stories in standard format
   - Acceptance criteria (Given/When/Then)
   - Edge cases and error scenarios
   - MVP scope recommendation
   ```

3. Test your template:

   ```
   [Use the template to generate user stories for a violation
   appeal feature where residents can appeal fines online,
   upload supporting documents, and track appeal status]
   ```

4. Create a "PRD Review" template:

   ```
   Create a prompt template for reviewing a PRD.
   Include placeholders for:
   - [PRD_CONTENT] (paste the PRD)
   - [TARGET_AUDIENCE] (who will use this)

   The template should identify:
   - Ambiguous requirements
   - Missing acceptance criteria
   - Technical questions to ask engineering
   - Edge cases not covered
   - Dependencies and risks
   ```

5. Save your templates for future use

### Success Criteria

- [ ] Created at least 2 PM-focused prompt templates
- [ ] Templates generate actionable documentation
- [ ] Tested a template successfully
- [ ] Saved templates for future use

---

## Wrap-Up

### What You Completed

- [ ] Practiced prompt refinement for PM documentation
- [ ] Built reusable PM prompt templates
- [ ] Used Claude to review and improve requirements

### Key Takeaways

Return to the [main README](../README.md#-key-takeaways) for shared learning points.

### Before Next Week

Use your templates on a real PRD or user story this week. Share your best prompt in `#ai-exchange`.

---

*Questions? Ask in `#ai-exchange` or during office hours.*
