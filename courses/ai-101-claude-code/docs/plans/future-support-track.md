# Future: Customer Support Track

## Status: Planned (Not Yet Implemented)

This document outlines plans for adding a dedicated track for the Customer Support team. This work will be done in a separate branch after the current AI Initiative docs are finalized.

---

## Why a Support Track?

The Customer Support team has unique needs:

| Characteristic | Implication |
| -------------- | ----------- |
| Non-technical background | No coding experience; focus on natural language AI interactions |
| Deep domain knowledge | Experts in HOA policies, customer issues, escalation paths |
| Already using AI | Have n8n automations and AI agents in production |
| QA-adjacent work | Test from customer perspective, validate responses |

---

## Proposed Use Cases

Based on Support team workflows:

| Use Case | Description |
| -------- | ----------- |
| **Ticket triage** | Categorize incoming tickets, identify priority, suggest routing |
| **Response drafting** | Generate professional responses following tone/policy guidelines |
| **KB article generation** | Convert resolved tickets into knowledge base articles |
| **n8n workflow design** | Use AI to help design and optimize automation workflows |
| **Quality assurance** | Test response templates for accuracy and consistency |

---

## Proposed Curriculum

**Duration:** 6 weeks (simplified from 9-week developer track)

| Week | Topic | Support Focus |
| ---- | ----- | ------------- |
| 0 | AI Foundations | Same (optional) |
| 1 | Setup & Orientation | Same |
| 2 | Prompting Foundations | Ticket responses, KB articles, customer communication |
| 3 | Planning & Organization | Template organization, escalation workflows |
| 4 | Quality Assurance | Response quality testing (not code testing) |
| 5 | Using Commands & Skills | Consumer focus - using existing tools, not building |
| 6 | Capstone | Support-specific project |

---

## Content to Create

### New Session Tracks

| File | Content |
| ---- | ------- |
| `sessions/week-2/tracks/support.md` | Prompts for tickets, KB, responses |
| `sessions/week-3/tracks/support.md` | Plan mode for templates and workflows |
| `sessions/week-4/tracks/support.md` | Testing response quality |
| `sessions/week-5/tracks/support.md` | Using commands/skills (consumer focus) |

### New Resources

| File | Content |
| ---- | ------- |
| `resources/n8n-integration.md` | How Claude helps with n8n workflows |
| `resources/support-prompts.md` | Prompt templates for Support use cases |

### Expanded Capstone

Expand `option-f-support-knowledge-base` to include:

- Ticket triage automation
- KB article generation workflow
- n8n integration patterns
- Response quality testing framework

---

## Success Metrics

Support-appropriate metrics (not code coverage):

| Metric | What We're Measuring |
| ------ | -------------------- |
| Response time | Are tickets resolved faster? |
| First-contact resolution | Fewer escalations needed? |
| KB coverage | Common issues documented? |
| Template accuracy | Responses follow policy? |
| Customer satisfaction | CSAT scores improving? |

---

## Certification Path

**Track:** Support
**Duration:** 6 weeks
**Capstone:** Support-focused project (Option F or custom)

Requirements:

- [ ] Complete Weeks 0-5 (Support track content)
- [ ] Submit capstone demonstrating AI-assisted support work
- [ ] Examples: FAQ generation, response templates, workflow design

---

## Implementation Notes

When implementing this track:

1. **Keep it non-technical** - No code, no TDD, no developer jargon
2. **Leverage domain expertise** - Support knows the product better than anyone
3. **Build on existing tools** - They already use n8n and AI agents
4. **Consumer not builder** - Focus on using AI tools, not creating them
5. **Practical examples** - Real tickets, real KB articles, real workflows

---

## Related Documents

- [AI Skills Rollout Plan](ai-acceleration-rollout.md) - Overall initiative
- [Training Schedule](training-schedule.md) - Group structure
- [Certification Overview](../certification/README.md) - Track requirements

---

## Timeline

| Phase | Status |
| ----- | ------ |
| Planning | Complete (this document) |
| Implementation | Pending (separate branch) |
| Review | TBD |
| Launch | TBD |
