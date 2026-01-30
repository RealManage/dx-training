# Week 1: Setup & Orientation - Support Exercise

**Duration:** 30-45 min
**Prerequisites:** Complete README.md through "Hands-On Exercises" section

---

## Your Mission

You'll use Claude Code to draft professional customer responses using HOA policy context. By the end, you'll have a reusable response template and experience iterating with Claude.

---

## Before You Start

> **Learning Mode Recommended:** Run `/output-style` and select **Learning** or **Explanatory** mode. This tells Claude to explain its thinking as it helps you - perfect for building your skills!

---

## Exercise 1: First Contact (15-20 min)

### Context

You've installed Claude Code and authenticated. Now let's put it to work on a real support scenario. The `examples/support` folder contains HOA policy context that Claude will use automatically.

### The Task

1. Set up your sandbox and start Claude:

   ```bash
   cd courses/ai-101-claude-code/sessions/week-1
   mkdir -p sandbox
   cp -r examples/support sandbox/
   cd sandbox/support
   claude
   ```

   > **Why sandbox?** We copy examples to sandbox so your practice doesn't modify the original files. The sandbox folder is git-ignored.

2. Ask Claude to draft a response for this ticket:

   ```
   A resident received their first violation notice for leaving trash cans out.
   They're upset and claim they didn't know about the rule.
   Draft a professional, empathetic response.
   ```

3. Review Claude's response. What's good? What's missing?

4. Iterate with follow-up prompts:

   ```
   Make it more empathetic - acknowledge their frustration first.
   ```

   ```
   Add the specific cure deadline (14 days from notice date).
   ```

   ```
   Include the contact number for questions: (555) 123-4567
   ```

5. Ask Claude to explain a policy detail:

   ```
   How do late fees work in our system? A resident owes $100 and now shows $133.
   ```

### Success Criteria

- [ ] Created sandbox and copied support example
- [ ] Claude Code started successfully
- [ ] Got an initial response draft from Claude
- [ ] Improved the response through 2-3 iterations
- [ ] Claude referenced policy details (like late fee compound interest)

---

## Exercise 2: Template Creation (15-20 min)

### Context

Creating reusable templates saves time and ensures consistency. You'll build a response template with placeholders that can be reused across tickets.

### The Task

1. Ask Claude to create a template:

   ```
   Create a response template for first-time violation notices.

   Include these placeholders:
   - [RESIDENT_NAME]
   - [PROPERTY_ADDRESS]
   - [VIOLATION_TYPE]
   - [VIOLATION_DATE]
   - [CURE_DEADLINE]
   - [YOUR_NAME]

   The template should:
   1. Acknowledge this is their first notice (empathetic)
   2. Clearly explain what needs to be fixed
   3. Provide the cure deadline
   4. Offer help if they have questions
   ```

2. Review the template. Does it match your team's tone and style?

3. Refine the template:

   ```
   Make the opening more human - less corporate-sounding.
   ```

4. Save the final template somewhere you can find it later (copy to a document, email yourself, etc.)

5. Test your understanding by asking Claude to explain something:

   ```
   What makes a CLAUDE.md file useful? I noticed there's one in this folder.
   ```

### Success Criteria

- [ ] Created a reusable template with all required placeholders
- [ ] Template has appropriate empathetic tone
- [ ] Saved the template for future use
- [ ] Understand what CLAUDE.md context files do

---

## Wrap-Up

### What You Completed

- [ ] Drafted and refined a customer response through iteration
- [ ] Created a reusable violation notice template
- [ ] Experienced how CLAUDE.md provides domain context

### Key Takeaways

Return to the [main README](../README.md#-key-takeaways) for shared learning points.

### Before Next Week

Practice using Claude Code with real tickets this week. Note:

- Which prompts worked well
- Which prompts gave poor results (we'll fix them in Week 2)
- Any questions that came up

---

*Questions? Ask in `#ai-exchange` or during office hours.*
