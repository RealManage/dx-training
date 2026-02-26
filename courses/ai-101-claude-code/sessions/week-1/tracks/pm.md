# Week 1: Setup & Orientation - PM Exercise

**Duration:** 30-45 min
**Prerequisites:** Complete README.md through "Hands-On Exercises" section

---

## Your Mission

You'll use Claude Code to understand a codebase and generate product documentation. By the end, you'll have user stories and experience using Claude for requirements analysis.

---

## Before You Start

> **Learning Mode Recommended:** Run `/output-style` and select **Learning** or **Explanatory** mode. This tells Claude to explain its thinking as it helps you - perfect for building your skills!

---

## Exercise 1: Codebase Understanding (15-20 min)

### Context

PMs often need to understand what's built before writing requirements. You'll use Claude Code to explore the `hoa-cli` example and extract product information.

### The Task

1. Set up your sandbox and start Claude:

   ```bash
   cd courses/ai-101-claude-code/sessions/week-1
   cp -r examples/hoa-cli sandbox/
   cd sandbox/hoa-cli
   claude
   ```

   > **Why sandbox?** We copy examples to sandbox so your practice doesn't modify the original files. The sandbox folder is git-ignored.

2. Ask Claude to explain the application:

   ```text
   I'm a PM trying to understand this application.
   Explain what it does in non-technical terms.
   What problem does it solve for users?
   ```

3. Identify features:

   ```text
   List all the features this application has.
   Which ones are implemented vs marked TODO?
   ```

4. Understand the business rules:

   ```text
   What are the business rules for HOA violations?
   Explain the late fee calculation and escalation timeline
   in terms a product person would understand.
   ```

5. Ask about gaps:

   ```text
   If I were writing a PRD (Product Requirements Document) for this product,
   what questions would I need answered?
   What's unclear or missing from the requirements?
   ```

### Success Criteria

- [ ] Can explain the application's purpose in plain language
- [ ] Listed implemented vs TODO features
- [ ] Understand the late fee and escalation rules
- [ ] Identified 3+ requirement gaps or questions

---

## Exercise 2: User Story Generation (15-20 min)

### Context

You'll use Claude Code to generate user stories from code. This shows how Claude can help translate technical implementations into product requirements.

### The Task

1. Ask Claude to generate user stories:

   ```text
   Generate user stories for the TODO features in this application.

   Use this format:
   As a [user type],
   I want to [action],
   So that [benefit].

   Include acceptance criteria for each story.
   ```

2. Refine a specific story:

   ```text
   The "Check overdue violations" feature (option 3) needs more detail.
   Write acceptance criteria that a developer could implement from.
   Include edge cases like "what if there are no overdue violations?"
   ```

3. Ask for a feature prioritization:

   ```text
   If we could only implement 2 of the 3 TODO features,
   which would you prioritize and why?
   Consider user value and implementation complexity.
   ```

4. Generate release notes:

   ```text
   Write release notes for the current implemented features
   as if this version just shipped. Keep it user-friendly,
   not technical.
   ```

5. Ask Claude about context:

   ```text
   How did you know the business rules like "30-day grace period"?
   Look at the CLAUDE.md file and explain how it helps.
   ```

### Success Criteria

- [ ] Generated user stories for TODO features
- [ ] Stories include clear acceptance criteria
- [ ] Provided a prioritization rationale
- [ ] Understand how CLAUDE.md provides domain context

---

## Wrap-Up

### What You Completed

- [ ] Explored a codebase from a product perspective
- [ ] Generated user stories with acceptance criteria
- [ ] Created release notes
- [ ] Experienced how CLAUDE.md provides project context

### Key Takeaways

Return to the [main README](../README.md#-key-takeaways) for shared learning points.

### Before Next Week

Practice using Claude Code for product work:

- Ask Claude to explain a technical ticket in plain language
- Generate acceptance criteria for an upcoming feature
- Create a mini-PRD from existing code

---

*Questions? Ask in `#ai-exchange` on Slack.*
