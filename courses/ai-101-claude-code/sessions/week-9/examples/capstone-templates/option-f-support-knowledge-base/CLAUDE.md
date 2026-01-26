# Capstone Option F: Knowledge Base & Response Templates

## Project Purpose
Build a comprehensive support toolkit using Claude Code including FAQs,
response templates, and customer communication tools.

## Deliverables
- FAQ Document (20+ questions)
- Response Template Library (20+ templates)
- Communication Tools (email, phone, chat)
- Escalation Decision Tree

## Requirements

### Core Features (Must Have)
1. FAQ generated from CCRs and policies
2. Professional response templates
3. Personalization placeholders in templates
4. Clear escalation criteria
5. Quality assurance checklist

### Domain Coverage
Templates should cover these categories:
- Account & Billing (dues, late fees, payment plans)
- Violations (notices, appeals, fines)
- Maintenance & Amenities (requests, reservations)
- General Inquiries (documents, board meetings)

### Custom Skill Required
Create `/draft-response <type> <description>` skill that:
1. Takes issue type and description
2. Generates professional response
3. Includes personalization placeholders
4. Follows tone guidelines

## Getting Started

```bash
# Set up your workspace
mkdir -p content templates tools
mkdir -p .claude/skills/draft-response

# Start Claude Code
claude
```

## Suggested Implementation Order

1. Gather source materials (CCRs, policies)
2. Generate FAQ document with Claude
3. Create billing-related templates
4. Create violation-related templates
5. Create maintenance templates
6. Create general templates
7. Build escalation decision tree
8. Create quality checklist
9. Document your process

## Success Criteria

```
[ ] FAQ covers top 20 resident questions
[ ] Response templates are professional and accurate
[ ] Templates include personalization placeholders
[ ] Escalation paths are clearly documented
[ ] All content generated with Claude assistance
[ ] Templates reviewed for tone and accuracy
```

## Template Structure

Each template should include:
- **Use When**: Scenario description
- **Subject Line**: Email subject
- **Body**: The response content
- **Personalization Points**: List of placeholders
- **Follow-up Actions**: Next steps

## Placeholder Format

Use these standard placeholders:
- `[RESIDENT_NAME]` - First and last name
- `[ACCOUNT_NUMBER]` - Account ID
- `[PROPERTY_ADDRESS]` - Full address
- `[AMOUNT]` - Dollar amounts
- `[DATE]` - Relevant dates
- `[YOUR_NAME]` - Support rep name

## Tone Guidelines

All templates should be:
- Professional but friendly
- Empathetic but firm on policies
- Clear, no jargon or legalese
- Action-oriented with next steps

## Tips for Using Claude Code

- Use real support scenarios as examples
- Have colleagues review templates for tone
- Include policy references for accuracy
- Cover edge cases where standard answers don't fit
- Test templates with realistic data
