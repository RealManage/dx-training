# Week 5: Commands & Skills - Support Exercise

**Duration:** 30-45 min
**Prerequisites:** Complete main README through Part 2 (Understanding Skills)

---

## Your Mission

You'll create custom slash commands that automate your common support tasks. By the end, you'll have a `/draft-response` skill that generates professional customer communications.

---

## Exercise 1: Create Your First Skill (20-25 min)

### Context

Skills are custom commands that save time on repetitive tasks. Support teams can use them to quickly draft responses, generate FAQs, or create templates.

### The Task

1. Start Claude in your workspace:

   ```bash
   cd courses/ai-101-claude-code/sessions/week-5
   mkdir -p sandbox
   cp -r examples/support-skills sandbox/
   cd sandbox/support-skills
   claude
   ```

2. Create a skill for drafting responses:

   ```
   Help me create a custom skill called /draft-response that:
   - Takes an issue type as the first argument
   - Takes a brief description as the second argument
   - Generates a professional, empathetic response
   - Uses our HOA context from CLAUDE.md

   Show me the skill file I need to create.
   ```

3. Create the skill file in `.claude/skills/`:

   ```
   Create the skill file at .claude/skills/draft-response.md
   ```

4. Test the skill:

   ```
   /draft-response late-fee "Resident upset about $50 charge"
   ```

5. Iterate on the skill if the output isn't right:

   ```
   The response is too long. Update the skill to keep
   responses under 150 words.
   ```

### Success Criteria

- [ ] Created a skill file in .claude/skills/
- [ ] Skill accepts arguments (issue type, description)
- [ ] Tested the skill successfully
- [ ] Iterated to improve the output

---

## Exercise 2: Build a Support Skill Library (15-20 min)

### Context

One skill is useful. A library of skills transforms your workflow.

### The Task

1. Identify common tasks that could be skills:

   ```
   What are 5 repetitive tasks support teams do that could
   become slash commands? Consider:
   - Response drafting
   - Information lookup
   - Template filling
   - Status explanations
   - Escalation documentation
   ```

2. Create a second skill (choose one):

   ```
   Create a skill called /explain-fee that:
   - Takes a fee type (late, processing, legal)
   - Generates a clear explanation of how it's calculated
   - Uses our HOA business rules
   ```

   OR

   ```
   Create a skill called /escalation-note that:
   - Takes a ticket ID and summary
   - Generates an internal escalation document
   - Includes recommended next steps
   ```

3. Test and refine your second skill

4. Document your skills:

   ```
   Create a README.md in .claude/skills/ that lists:
   - Each skill name and purpose
   - Example usage
   - What arguments it accepts
   ```

### Success Criteria

- [ ] Identified 5 potential skill ideas
- [ ] Created a second skill
- [ ] Tested the second skill
- [ ] Documented your skill library

---

## Wrap-Up

### What You Completed

- [ ] Created custom slash commands for support work
- [ ] Built a skill library with multiple commands
- [ ] Documented your skills for team use

### Key Takeaways

Return to the [main README](../README.md#-key-takeaways) for shared learning points.

### Before Next Week

Share your skills with the team! Post your best skill in `#ai-exchange` so others can use it.

---

*Questions? Ask in `#ai-exchange` or during office hours.*
