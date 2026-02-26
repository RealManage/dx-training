# Week 7: Plugins - Support Exercise

**Duration:** 30-45 min
**Prerequisites:** Complete main README through Part 2 (Understanding Plugins)

---

## Your Mission

You'll learn how plugins package skills, hooks, and configurations into reusable toolkits. By the end, you'll design a "Support Plugin" that bundles your team's best practices.

---

## Exercise 1: Plugin Concept for Support (15-20 min)

### Context

Plugins bundle related capabilities together. A "Support Plugin" could package response templates, quality checks, and team conventions into one installable package.

### The Task

1. Start Claude in your workspace:

   ```bash
   cd courses/ai-101-claude-code/sessions/week-7
   cp -r examples/support-toolkit sandbox/
   cd sandbox/support-toolkit
   claude
   ```

2. Explore what a Support Plugin would contain:

   ```text
   If we were building a "RealManage Support Plugin", what should it include?

   Consider these categories:
   - Skills (slash commands)
   - Hooks (quality checks)
   - CLAUDE.md context (policies, guidelines)
   - Templates (response patterns)

   List 3-5 items for each category.
   ```

3. Design the plugin structure:

   ```text
   Show me what the folder structure for a Support Plugin would look like.
   What files would it contain?
   How would someone install and use it?
   ```

4. Prioritize the components:

   ```text
   If we could only build 5 components for this plugin MVP,
   which would provide the most value to support teams?
   Rank them and explain why.
   ```

### Success Criteria

- [ ] Identified components for a Support Plugin
- [ ] Designed the plugin folder structure
- [ ] Prioritized MVP components

---

## Exercise 2: Team Conventions Plugin (15-20 min)

### Context

Plugins can encode team conventions so everyone follows the same standards. This is especially valuable for support teams maintaining consistency.

### The Task

1. Define your team's response conventions:

   ```text
   Help me document our support team's response conventions:

   - Greeting style (formal vs friendly)
   - Signature format
   - Response length guidelines
   - Empathy statement patterns
   - Escalation triggers
   - What NOT to say (policy restrictions)

   Format this as a conventions document.
   ```

2. Turn conventions into CLAUDE.md context:

   ```text
   Convert these conventions into CLAUDE.md format that Claude
   would use when helping with support tasks.

   Include:
   - Tone guidelines
   - Required elements
   - Forbidden phrases
   - Response structure
   ```

3. Create a template library outline:

   ```text
   Design a template library for the plugin with:
   - Template categories (billing, violations, general)
   - 3 templates per category
   - Each template should have: name, use case, placeholders
   ```

### Success Criteria

- [ ] Documented team response conventions
- [ ] Created CLAUDE.md context for conventions
- [ ] Designed template library structure

---

## Wrap-Up

### What You Completed

- [ ] Designed a Support Plugin concept
- [ ] Documented team conventions
- [ ] Created reusable context and templates

### Key Takeaways

Return to the [main README](../README.md#-key-takeaways) for shared learning points.

### Before Next Week

Share your plugin ideas with the team! Discuss which conventions should be standardized across all support staff.

---

*Questions? Ask in `#ai-exchange` on Slack.*
