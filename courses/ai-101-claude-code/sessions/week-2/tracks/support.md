# Week 2: Prompting Foundations - Support Exercise

**Duration:** 30-45 min
**Prerequisites:** Complete README.md through "Hands-On Exercises" section

---

## Your Mission

You'll practice crafting prompts that generate professional, consistent customer communications. By the end, you'll have prompt templates for your most common support tasks.

---

## Before You Start

> **Learning Mode Recommended:** If you haven't already, run `/output-style` and select **Learning** or **Explanatory** mode for better understanding.

---

## Exercise 1: Response Quality (15-20 min)

### Context

The difference between a vague and specific prompt is the difference between a generic response and one that sounds like it came from a domain expert. You'll practice prompt refinement.

### The Task

1. Set up your sandbox and start Claude:

   ```bash
   cd courses/ai-101-claude-code/sessions/week-2
   mkdir -p sandbox
   cp -r examples/response-lab sandbox/
   cd sandbox/response-lab
   claude
   ```

2. Start with a vague prompt:

   ```
   Write a response to an upset customer
   ```

3. Notice how generic it is. Now try a specific prompt:

   ```
   Write a response to a resident who is upset about a $200 late fee.

   Context:
   - They owed $100 in dues 3 months ago
   - Balance is now $133 (compound interest)
   - They claim they never received the original invoice
   - This is their first complaint

   Response should:
   - Acknowledge their frustration first
   - Explain how the $133 balance was calculated (10% monthly compound)
   - Offer payment plan options
   - Include escalation path if they want to dispute
   - Be professional but empathetic
   - Stay under 200 words
   ```

4. Compare the results. What did the specific prompt include?

5. Ask Claude to improve it further:

   ```
   Review this response. Is the tone right? Is anything missing
   that would help resolve the resident's concern?
   ```

### Success Criteria

- [ ] Experienced the difference between vague and specific prompts
- [ ] Got a response that sounds professionally empathetic
- [ ] Had Claude review and improve the response
- [ ] Understand why context matters for support prompts

---

## Exercise 2: Support Prompt Templates (15-20 min)

### Context

You'll create reusable prompt templates for common support scenarios.

### The Task

1. Ask Claude about common support needs:

   ```
   What are the most common types of responses support teams need?
   Give me 5 categories with examples for HOA management.
   ```

2. Create a "Customer Response" template:

   ```
   Help me create a reusable prompt template for drafting customer responses.
   Include placeholders for:
   - [ISSUE_TYPE] (billing, violation, maintenance, etc.)
   - [CUSTOMER_EMOTION] (upset, confused, frustrated, etc.)
   - [SPECIFIC_SITUATION]
   - [POLICY_REFERENCE] (if applicable)
   - [AVAILABLE_OPTIONS]

   The template should generate:
   - Empathetic opening
   - Clear explanation
   - Available options/next steps
   - Professional closing
   ```

3. Test your template:

   ```
   [Use the template to draft a response for a resident who is
   confused about why their violation notice says "second offense"
   when they thought they fixed the first one - issue is their
   landscaping, they trimmed shrubs but weeds are still overgrown]
   ```

4. Create a "KB Article" template:

   ```
   Create a prompt template for generating knowledge base articles.
   Include placeholders for:
   - [TOPIC]
   - [COMMON_QUESTIONS] (3-5 questions residents ask)
   - [POLICY_DETAILS]

   The article should include:
   - Clear title
   - Brief overview
   - Step-by-step instructions (if applicable)
   - FAQ section
   - Contact information for help
   ```

5. Save your templates for future use

### Success Criteria

- [ ] Created at least 2 support-focused prompt templates
- [ ] Templates generate professional, empathetic responses
- [ ] Tested a template successfully
- [ ] Saved templates for future use

---

## Wrap-Up

### What You Completed

- [ ] Practiced prompt refinement for customer communication
- [ ] Built reusable support prompt templates
- [ ] Used Claude to improve response quality

### Key Takeaways

Return to the [main README](../README.md#-key-takeaways) for shared learning points.

### Before Next Week

Use your templates on real tickets this week. Track which prompts work best for different scenarios.

---

*Questions? Ask in `#ai-exchange` or during office hours.*
