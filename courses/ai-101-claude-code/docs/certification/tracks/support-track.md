# Support Certification Track

## Overview

The Support track teaches you to use AI for customer service workflows: drafting responses, explaining policies, handling escalations, and building knowledge bases.

**You don't need to write code to succeed in this track.**

## Track Duration

**8 weeks typical**

| Phase | Weeks | Focus |
| ----- | ----- | ----- |
| Foundations | 0-3 | Setup, prompting, plan mode |
| Support-Specific | 4-6 | Quality criteria, skills, hooks |
| Advanced | 7-8 | Skip plugins, automation workflows |
| Capstone | 9 | Knowledge Base & Response Templates |

## Week-by-Week Content

### Week 0: AI Foundations (Optional)

- What AI can and can't do
- How language models work
- Understanding hallucination risks (critical for support accuracy!)
- Setting expectations

### Week 1: Setup & Orientation

- Install Claude Code
- Create your CLAUDE.md with support context
- First conversations with Claude
- Draft a simple customer response

### Week 2: Prompting Foundations

- Writing prompts that generate professional responses
- Context and specificity for accurate information
- The Support Response Formula
- Iterating on response quality

### Week 3: Planning & Analysis

- Using Plan Mode for complex tickets
- Breaking down multi-issue tickets
- Research-heavy responses
- Policy interpretation questions

### Week 4: Quality Criteria

**Support-Specific Content:**

- Test-first response writing
- Defining success criteria before drafting
- Response quality validation
- Checklist-driven communication

**Exercises:**

- Define quality criteria for a late fee response
- Write criteria-first for a violation appeal
- Have Claude check a response against your criteria

See [Week 4 Support Track](../../../sessions/week-4/tracks/support.md) for full content.

### Week 5: Commands & Support Skills

**Support-Specific Content:**

- Creating custom slash commands for support work
- Building reusable response drafting tools
- `/draft-response`, `/explain-fee`, `/tone-check`, `/escalation-note`

**Exercises:**

- Create a `/draft-response` skill
- Build an `/explain-fee` skill for fee calculations
- Create a `/tone-check` skill for response validation

See [Week 5 Support Track](../../../sessions/week-5/tracks/support.md) for full content.

### Week 6: Agents & Hooks

**Support-Specific Content:**

- Quality hooks for response validation
- Pre-send quality gates
- Escalation detection automation

**Exercises:**

- Design a quality hook that checks for required response elements
- Plan an escalation detection workflow
- Configure validation for common issues

See [Week 6 Support Track](../../../sessions/week-6/tracks/support.md) for full content.

### Week 7: Skip (Developer-Focused)

Week 7 covers Plugins, which is developer-focused. Support professionals can:

- Review the summary to understand concepts
- Skip hands-on exercises
- Proceed to Week 8

### Week 8: Real-World Automation

**Support-Specific Content:**

- Ticket triage automation design
- Response generation workflows
- Human-in-the-loop patterns
- Quality gates and thresholds

**Exercises:**

- Design a ticket triage workflow
- Create a response generation pipeline with quality checks
- Define human checkpoint criteria

See [Week 8 Support Track](../../../sessions/week-8/tracks/support.md) for full content.

### Week 9: Capstone

Demonstrate AI fluency with a complete support toolkit:

- FAQ document (20+ questions)
- Response template library (20+ templates)
- Escalation decision tree
- Custom `/draft-response` skill

## What You'll Be Able to Do

After completing this track:

- [ ] Draft professional customer responses 3-5x faster
- [ ] Explain complex fees and policies clearly
- [ ] Build reusable response templates
- [ ] Create custom skills for common tasks
- [ ] Design quality validation workflows
- [ ] Generate FAQ and knowledge base content
- [ ] Know when AI helps and when it doesn't

## Capstone Requirements

**Option F: Support Knowledge Base & Response Templates**

| Deliverable | Description |
| ----------- | ----------- |
| FAQ Document | 20+ questions with AI-assisted answers |
| Template Library | 20+ response templates covering billing, violations, maintenance |
| Escalation Tree | Decision criteria for when to escalate |
| Custom Skill | Working `/draft-response` skill |
| Presentation | Share toolkit with team |

See [Capstone Option F](../../../sessions/week-9/examples/capstone-templates/option-f-support-knowledge-base/CLAUDE.md) for full requirements.

## This Track Is For You If

- You handle customer inquiries daily
- You write email responses to residents or homeowners
- You explain policies, fees, and procedures
- You want to respond faster without sacrificing quality
- You need consistent tone across your team

## This Track Isn't About

- Replacing support staff with AI
- Sending responses without human review
- Pretending AI understands customer emotions
- Skipping the human judgment that matters

## Differences from Developer Track

| Topic | Developer Track | Support Track |
| ----- | --------------- | ------------- |
| Week 4 | TDD (writing code) | Quality criteria for responses |
| Week 5 | Commands & Skills (dev) | Commands & Support Skills |
| Week 6 | Agents & Hooks (dev) | Quality hooks for responses |
| Week 7 | Plugins | Skip |
| Week 8 | CI/CD integration | Ticket automation design |
| Capstone | Code project | Knowledge Base & Templates |

## The Support Toolkit

Week 9 course content includes ready-to-use skills:

| Skill | Purpose |
| ----- | ------- |
| `/draft-response` | Generate customer responses by issue type |
| `/explain-fee` | Break down fee calculations step-by-step |
| `/tone-check` | Validate response professionalism and empathy |
| `/escalation-note` | Create internal escalation documentation |

These skills are available after training and can be customized for your team's formats.

## Support-Specific Resources

| Resource | Description |
| -------- | ----------- |
| [Quick Start: Support](../../../resources/quick-start-support.md) | Condensed track overview |
| [Support Prompts](../../../resources/support-prompts.md) | Ready-to-use prompt templates |
| [n8n Integration Guide](../../../resources/n8n-integration.md) | Workflow automation concepts |
| [Glossary](../../../resources/glossary.md) | Support terminology |

## FAQ

**Q: Do I need to know how to code?**
A: No. The track focuses on customer communication, not development.

**Q: Will AI send responses automatically?**
A: No. AI drafts, humans review and send. Always maintain human oversight.

**Q: What about sensitive or complex issues?**
A: AI helps with drafting and research, but human judgment handles complexity. Week 8 covers when NOT to automate.

**Q: Can AI access our ticketing system?**
A: Not directly. You'll copy/paste ticket info into prompts. Week 8 discusses workflow integration concepts.

**Q: What if AI gives wrong policy information?**
A: It happens. That's why human review is mandatory. Week 0 covers hallucination risks.

**Q: How do I know when to use AI vs. write manually?**
A: Use AI for routine responses. Write manually for emotional situations, escalations, or anything requiring nuance. You'll develop judgment through the course.

## Getting Help

See [Getting Help](../../../resources/getting-help.md) for all support channels.
