# Week 5: Commands & Skills - Support Exercise

**Duration:** 1.5 hours
**Prerequisites:** Complete main README through Part 2

---

## Your Mission

You'll create custom commands and skills that automate your common support tasks. By the end, you'll have a `/draft-response` skill that generates professional customer communications.

> **Commands vs Skills:** Commands are single markdown files for simple prompts. Skills add a directory with supporting files (templates, data). This session starts with a command, then graduates to a skill.

---

## Part 1: Your First Command (15 min)

### Create a Quick FAQ Lookup Command

Start with something simple -- a command that doesn't need supporting files.

1. Start Claude in your workspace:

   ```bash
   cd courses/ai-101-claude-code/sessions/week-5
   cp -r examples/support-skills sandbox/
   cd sandbox/support-skills
   mkdir -p .claude/commands
   claude
   ```

2. Create a FAQ lookup command:

   ```text
   Help me create a command called /faq that takes a topic as an argument
   and generates a clear, resident-friendly answer. Save it to
   .claude/commands/faq.md

   Topics should include: late-fees, violations, payment-plans,
   board-meetings, architectural-review
   ```

3. Test the command:

   ```text
   /faq late-fees
   /faq payment-plans
   ```

### Success Criteria

- [ ] Created `.claude/commands/faq.md`
- [ ] Command accepts a topic argument
- [ ] Tested with at least 2 topics

---

## Part 2: Create a Skill with Supporting Files (30 min)

### The Task: `/draft-response` Skill

Now graduate to a skill that uses a supporting file for tone guidance.

1. Create the skill directory structure:

   ```text
   Help me create a skill called /draft-response with this structure:
   .claude/skills/draft-response/SKILL.md
   .claude/skills/draft-response/tone-guide.txt

   The skill should:
   - Take an issue type as the first argument ($0)
   - Take a brief description as the second argument ($1)
   - Generate a professional, empathetic response
   - Use the tone-guide.txt for response style
   - Use our HOA context from CLAUDE.md
   ```

2. Test the skill:

   ```text
   /draft-response late-fee "Resident upset about $50 charge"
   ```

3. Iterate on the skill if the output isn't right:

   ```text
   The response is too long. Update the skill to keep
   responses under 150 words.
   ```

### Success Criteria

- [ ] Created `.claude/skills/draft-response/SKILL.md`
- [ ] Created `.claude/skills/draft-response/tone-guide.txt`
- [ ] Skill accepts arguments (issue type, description)
- [ ] Tested the skill successfully
- [ ] Iterated to improve the output

---

## Part 3: Build a Support Skill Library (30 min)

### Context

One skill is useful. A library of skills transforms your workflow.

### The Task

1. Identify common tasks that could be skills:

   ```text
   What are 5 repetitive tasks support teams do that could
   become slash commands? Consider:
   - Response drafting
   - Information lookup
   - Template filling
   - Status explanations
   - Escalation documentation
   ```

2. Create a second skill (choose one):

   **Option A: `/explain-fee`**

   ```text
   Create a skill called /explain-fee in .claude/skills/explain-fee/SKILL.md that:
   - Takes a fee type (late, processing, legal)
   - Generates a clear explanation of how it's calculated
   - Uses a supporting file for the fee schedule
   - Uses our HOA business rules
   ```

   **Option B: `/escalation-note`**

   ```text
   Create a skill called /escalation-note in .claude/skills/escalation-note/SKILL.md that:
   - Takes a ticket ID ($0) and summary ($1)
   - Generates an internal escalation document
   - Uses a supporting file for the escalation template
   - Includes recommended next steps
   ```

3. Test and refine your second skill

### Success Criteria

- [ ] Identified 5 potential skill ideas
- [ ] Created a second skill with `SKILL.md` in a subdirectory
- [ ] Included at least one supporting file
- [ ] Tested the second skill

---

## Part 4: Wrap-Up (15 min)

### What You Completed

- [ ] Created a command (`.claude/commands/faq.md`)
- [ ] Created a skill with supporting files (`.claude/skills/draft-response/`)
- [ ] Built a second skill for your library
- [ ] Understand the difference between commands and skills

### Key Takeaways

1. **Commands** for simple, single-prompt automation (FAQ lookups, quick answers)
2. **Skills** when you need supporting files (tone guides, templates, fee schedules)
3. Start with a command -- upgrade to a skill when you need more context
4. Skills live in `.claude/skills/<name>/SKILL.md` (directory with SKILL.md inside)

Return to the [main README](../README.md#key-takeaways) for shared learning points.

### Before Next Week

Share your skills with the team! Post your best skill in `#ai-exchange` so others can use it.

### What's Next

**Week 6: Agents & Hooks** - Learn about:

- Custom AI agents for specialized tasks
- Hooks for automated workflows (e.g., audit logging)
- Compliance and SOC 2 patterns

---

## Homework

1. **Create 2 custom commands** for your daily support workflow
2. **Build 1 skill** with at least one supporting file
3. **Share your best command/skill** in `#ai-exchange`

---

*Questions? Ask in `#ai-exchange` on Slack.*
