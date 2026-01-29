# Support Track - Capstone Guidance

## Recommended Option

**Option F: Knowledge Base & Response Templates** is designed specifically for Support team members.

## Why Option F?

This option lets you use Claude Code to build tools that make support work faster and more consistent:

- Generate FAQs from existing documentation
- Create professional response templates
- Build customer communication tools
- Improve self-service content

**This capstone focuses on creating support assets**, not writing code.

## What You'll Build

### 1. FAQ Document

- Generated from CCRs, policies, and procedures
- Covers top 20+ resident questions
- Clear, resident-friendly language
- Links to relevant documentation

### 2. Response Template Library

- 20+ templates for common issues
- Professional, consistent tone
- Personalization placeholders
- Escalation guidance included

### 3. Customer Communication Tools

- Email templates by scenario
- Phone script outlines
- Chat response suggestions
- Difficult conversation guides

### 4. Escalation Decision Tree

- Clear escalation criteria
- Contact information for each path
- Response time expectations
- Documentation requirements

## Skills You'll Use

| Week | Skill Applied |
| ---- | ------------- |
| Week 2 | Prompt engineering for content generation |
| Week 3 | Plan Mode for organizing templates |
| Week 5 | Custom skill for `/draft-response` |
| Week 6 | Workflow for content iteration |

## Getting Started

```bash
# Set up your workspace
cd courses/ai-101-claude-code/sessions/week-9
cp -r examples/capstone-templates/option-f-support-knowledge-base sandbox/
cd sandbox/option-f-support-knowledge-base

# Start Claude Code
claude
```

### First Steps

1. **Gather source materials**: Collect CCRs, policies, common tickets
2. **Start with FAQs**: Generate questions and answers from docs
3. **Build response templates**: Create templates for common scenarios
4. **Create escalation paths**: Document when and how to escalate
5. **Add your custom skill**: `/draft-response` for quick replies

## Common Support Scenarios to Cover

Your templates should address these categories:

### Account & Billing

- Dues questions and payment options
- Late fee explanations and waivers
- Payment plan requests
- Account balance disputes

### Violations

- Violation notice explanations
- Appeal process guidance
- Fine questions
- Cure period information

### Maintenance & Amenities

- Work order status inquiries
- Amenity reservation help
- Common area complaints
- Emergency maintenance

### General Inquiries

- HOA document requests
- Board meeting information
- Community rules clarification
- New resident onboarding

## Example Prompts

```
# FAQ generation
"Generate a FAQ document for HOA residents based on these CCR
sections: [paste relevant sections]. Write in friendly,
non-legal language that residents can easily understand."

# Response template
"Create an email template for explaining late fees to a
resident who is upset about charges. Include empathy, clear
explanation of the policy, and available options."

# Escalation criteria
"Create a decision tree for when support should escalate
issues to management. Include criteria for billing disputes,
violation appeals, and emergency situations."

# Difficult conversations
"Write a phone script for handling a resident who is angry
about a violation notice. Include de-escalation techniques
and key talking points."
```

## Custom Skill: `/draft-response`

Create this skill to quickly draft responses:

```markdown
---
name: draft-response
description: Draft a response for a resident inquiry
argument-hint: [issue_type] [brief_description]
---

Draft a professional response for: $1 - $2

## Response Guidelines
1. Start with empathy/acknowledgment
2. Provide clear, accurate information
3. Offer next steps or options
4. Close professionally with contact info

## Tone
- Professional but friendly
- Empathetic but firm on policies
- Clear, no jargon
- Action-oriented

## Include
- Relevant policy reference (if applicable)
- Timeline expectations
- Contact information for follow-up
```

## Success Criteria Checklist

```
[ ] FAQ covers top 20 resident questions
    - Account and billing questions
    - Violation and fine questions
    - Maintenance and amenity questions
    - General HOA questions
    - Answers are accurate and clear

[ ] Response templates are professional and accurate
    - Consistent tone across all templates
    - Accurate policy information
    - Appropriate empathy expressions
    - Clear calls to action

[ ] Templates include personalization placeholders
    - [Resident Name]
    - [Account Number]
    - [Property Address]
    - [Specific Issue Details]
    - [Date/Amount/etc.]

[ ] Escalation paths are clearly documented
    - Clear criteria for escalation
    - Contact information included
    - Response time expectations
    - Required documentation listed

[ ] All content generated with Claude assistance
    - Process documented
    - Effective prompts captured
    - Iteration process shown

[ ] Templates reviewed for tone and accuracy
    - No policy inaccuracies
    - Appropriate empathy level
    - Professional language
    - Actionable content
```

## Deliverables Summary

1. **FAQ Document** (`content/faq.md`)
   - 20+ questions and answers
   - Organized by category
   - Resident-friendly language

2. **Response Template Library** (`templates/`)
   - `billing-templates.md`
   - `violation-templates.md`
   - `maintenance-templates.md`
   - `general-templates.md`

3. **Communication Tools** (`tools/`)
   - `email-templates.md`
   - `phone-scripts.md`
   - `chat-responses.md`
   - `difficult-conversations.md`

4. **Escalation Decision Tree** (`content/escalation-tree.md`)
   - Visual decision tree (mermaid diagram)
   - Criteria descriptions
   - Contact information

5. **Quality Checklist** (`content/quality-checklist.md`)
   - Review criteria for responses
   - Common mistakes to avoid
   - Tone guidelines

## Tips for Success

- **Use real ticket examples**: Base templates on actual scenarios
- **Get the tone right**: Professional but human, not robotic
- **Include policy references**: Back up answers with sources
- **Think about edge cases**: What if the standard answer doesn't fit?
- **Test with colleagues**: Have others review for clarity

## Common Pitfalls

- Templates that sound robotic or impersonal
- Missing important policy details
- Forgetting personalization placeholders
- Escalation paths that are unclear
- Not covering enough scenarios (aim for 20+)

## Template Quality Checklist

Use this for each template you create:

```
[ ] Opens with acknowledgment/empathy
[ ] Provides accurate information
[ ] References policy (if applicable)
[ ] Includes personalization placeholders
[ ] Has clear next steps
[ ] Appropriate length (not too long/short)
[ ] Professional but friendly tone
[ ] No jargon or legalese
[ ] Grammatically correct
[ ] Reviewed by peer
```

---

**Questions?** Ask in `#ai-exchange` or during office hours.
