# Week 2: Prompting Foundations - Developer Exercise

**Duration:** 45-60 min
**Prerequisites:** Complete README.md through "Hands-On Exercises" section

---

## Your Mission

You'll practice crafting effective prompts that generate production-ready C# code with proper tests. By the end, you'll have a personal prompt library for your most common coding tasks.

---

## Before You Start

> **Learning Mode Recommended:** If you haven't already, run `/output-style` and select **Learning** or **Explanatory** mode for better understanding.

---

## Exercise 1: Prompt Refinement (20-25 min)

### Context

Most developers start with vague prompts and wonder why the output isn't great. You'll practice turning vague prompts into specific, actionable ones.

### The Task

1. Set up your sandbox and start Claude:

   ```bash
   cd courses/ai-101-claude-code/sessions/week-2
   cp -r examples/prompt-lab sandbox/
   cd sandbox/prompt-lab
   claude
   ```

2. Start with a vague prompt and observe the result:

   ```
   Create a payment service
   ```

3. Notice what's missing. Now try a specific prompt:

   ```
   Create a PaymentService class for our HOA system that:
   - Processes dues payments (monthly, quarterly, annual options)
   - Calculates late fees (10% monthly compound after 30-day grace)
   - Returns PaymentResult with success/failure and receipt number
   - Uses async/await for all operations
   - Include xUnit tests with 80-90% coverage
   - Tech: C# .NET 10, repository pattern
   ```

4. Compare the two results. What's different?

5. Ask Claude to improve your prompt:

   ```
   Review this prompt and suggest what's missing or unclear:
   "Create a violation tracking service for HOA management"
   ```

6. Apply Claude's suggestions and try the improved prompt

### Success Criteria

- [ ] Experienced the difference between vague and specific prompts
- [ ] Got working code with tests from specific prompt
- [ ] Had Claude critique and improve a prompt
- [ ] Understand why context matters

---

## Exercise 2: Build Your Prompt Library (20-25 min)

### Context

You'll create reusable prompt templates for your most common tasks. These go in your CLAUDE.md file or a separate prompt library.

### The Task

1. Ask Claude to help you identify common patterns:

   ```
   What are the most common types of C# code developers ask you to write?
   Give me 5 categories with examples.
   ```

2. Create a prompt template for "New Service Class":

   ```
   Help me create a reusable prompt template for generating C# service classes.
   It should include placeholders for:
   - [SERVICE_NAME]
   - [FEATURE_DESCRIPTION]
   - [DEPENDENCIES] (repositories, other services)
   - [BUSINESS_RULES]

   Include our standards: .NET 10, async/await, xUnit tests, 80-90% coverage
   ```

3. Test your template by filling it in:

   ```
   [Use the template to generate a ResidentNotificationService
   that sends violation notices via email and SMS]
   ```

4. Create a "Fix Bug with TDD" template:

   ```
   Create a prompt template for fixing bugs using TDD.
   Include placeholders for:
   - [CLASS_NAME]
   - [METHOD_NAME]
   - [BUG_DESCRIPTION]
   - [EXPECTED_BEHAVIOR]

   The workflow should be: write failing test → fix code → verify
   ```

5. Save your templates to a markdown file for future use

### Success Criteria

- [ ] Created at least 2 reusable prompt templates
- [ ] Templates include RealManage standards (coverage, patterns)
- [ ] Tested a template successfully
- [ ] Saved templates for future use

---

## Wrap-Up

### What You Completed

- [ ] Practiced prompt refinement (vague → specific)
- [ ] Built reusable prompt templates
- [ ] Used Claude to improve your prompts

### Key Takeaways

Return to the [main README](../README.md#-key-takeaways) for shared learning points.

### Before Next Week

Add your best prompts to your project's CLAUDE.md file. Practice using them on real tasks this week.

---

*Questions? Ask in `#ai-exchange` or during office hours.*
