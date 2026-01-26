# Option E: Product Design & Documentation

**Track:** Product Managers
**Focus:** PRDs, user stories, stakeholder documentation
**NOTE:** This is a NON-CODING capstone

## Overview

Use Claude Code to create complete product specifications and documentation:

- Feature specification for a new HOA capability
- User story mapping with acceptance criteria
- Stakeholder documentation and presentations
- Process documentation for AI-assisted PM work

## Getting Started

```bash
# Copy this template to your sandbox
cd courses/ai-101-claude-code/sessions/week-9
cp -r examples/capstone-templates/option-e-pm-product-design sandbox/
cd sandbox/option-e-pm-product-design

# Start Claude Code
claude
```

## Project Structure

```
option-e-pm-product-design/
├── README.md
├── CLAUDE.md                    # Your project context
├── docs/
│   ├── PRD.md                   # Product Requirements Document
│   ├── user-stories.md          # User story mapping
│   ├── stakeholder/
│   │   ├── executive-summary.md
│   │   ├── technical-spec.md
│   │   ├── training-outline.md
│   │   └── faq.md
│   ├── process.md               # How you used Claude
│   └── presentation-outline.md
└── .claude/
    └── skills/
        └── generate-user-stories/
            └── SKILL.md
```

## Suggested Features to Specify

Choose ONE of these (or propose your own with instructor approval):

### 1. Resident Self-Service Portal
Enable residents to manage their HOA interactions online:
- View account balance and payment history
- Submit maintenance requests
- Access community documents
- Receive and manage notifications

### 2. Board Meeting Management
Streamline board meeting workflows:
- Agenda generation from pending items
- Voting and decision tracking
- Minutes auto-generation
- Action item follow-up

### 3. Violation Appeals Process
Digital workflow for violation disputes:
- Online appeal submission
- Evidence upload and review
- Hearing scheduling
- Decision notification

### 4. Community Amenity Booking
Reservation system for shared spaces:
- Pool, clubhouse, tennis court reservations
- Availability calendar
- Deposit and fee collection
- Usage reporting

## Key Deliverables

### 1. Product Requirements Document (`docs/PRD.md`)

Include these sections:

```markdown
# [Feature Name] PRD

## Executive Summary
One paragraph overview of the feature.

## Problem Statement
- What problem does this solve?
- Who experiences this problem?
- What's the impact of not solving it?

## User Personas
- Primary persona with goals and pain points
- Secondary personas

## Functional Requirements
- FR-001: [Requirement]
- FR-002: [Requirement]
- ...

## Non-Functional Requirements
- Performance expectations
- Security requirements
- Accessibility requirements

## Success Metrics
- Metric 1: [How measured, target]
- Metric 2: [How measured, target]

## Timeline and Milestones
- Phase 1: MVP
- Phase 2: Enhancement
- Phase 3: Full release

## Dependencies and Risks
- Technical dependencies
- Business dependencies
- Key risks and mitigations
```

### 2. User Story Mapping (`docs/user-stories.md`)

Format each story properly:

```markdown
## Epic: [Epic Name]

### Story: [Story Title]
**As a** [user type]
**I want** [goal]
**So that** [benefit]

**Acceptance Criteria:**
```gherkin
Given [context]
When [action]
Then [expected result]
```

**Priority:** Must/Should/Could
**Estimate:** S/M/L
```

### 3. Stakeholder Documentation (`docs/stakeholder/`)

Create role-specific documents:

- **Executive Summary**: 1-page overview for leadership
- **Technical Spec**: Details for engineering team
- **Training Outline**: Learning objectives for users
- **FAQ**: Common questions and answers

### 4. Process Documentation (`docs/process.md`)

Document how you used Claude Code:

```markdown
# Claude Code PM Process

## Prompts That Worked Well
- [Prompt] -> [What it produced]

## Iteration Process
- First draft: [What Claude produced]
- Refinement: [How you improved it]
- Final: [The result]

## Lessons Learned
- What to do again
- What to do differently
```

## Custom Skill: `/generate-user-stories`

Create `.claude/skills/generate-user-stories/SKILL.md`:

```markdown
---
name: generate-user-stories
description: Generate user stories from a feature description
argument-hint: [feature_name]
---

Generate user stories for the $1 feature.

## Output Format
For each story, provide:
1. Story title
2. As a [user], I want [goal] so that [benefit]
3. Acceptance criteria (Given/When/Then)
4. Priority (Must/Should/Could)
5. Estimated complexity (S/M/L)

## Quality Standards
- Stories follow INVEST principles
- Acceptance criteria are testable
- No implementation details in stories
- Clear user benefit stated
```

## Success Criteria

```
[ ] PRD covers problem, solution, and success metrics
[ ] User stories follow INVEST principles
[ ] Acceptance criteria are testable and clear
[ ] Documentation is stakeholder-ready
[ ] All artifacts generated with Claude assistance
[ ] No coding required for submission
```

## Example Prompts

Use these prompts to get started:

```
# Starting the PRD
"I'm creating a PRD for a Resident Self-Service Portal for HOA
management. Start with a problem statement section that explains
why residents need this capability and what pain points it addresses."

# Generating user personas
"Create two user personas for the Resident Self-Service Portal:
a tech-savvy millennial homeowner and a retired homeowner who
prefers phone calls but is willing to try online tools."

# Writing user stories
"Generate user stories for the payment history feature of the
Resident Self-Service Portal. Follow INVEST principles and include
Gherkin-format acceptance criteria."

# Creating executive summary
"Summarize this PRD into a 1-page executive summary suitable for
a board presentation. Focus on business value and ROI."
```

## Tips for Success

- **Be specific with Claude**: More context = better output
- **Iterate on drafts**: First outputs are starting points
- **Add domain knowledge**: Include HOA-specific requirements
- **Review for accuracy**: Validate suggestions against business rules
- **Document your process**: Show how Claude helped

## Resources

- [Writing Great PRDs](https://www.productplan.com/learn/how-to-write-a-product-requirements-document/)
- [User Story Mapping](https://www.jpattonassociates.com/user-story-mapping/)
- [INVEST Principles](https://www.agilealliance.org/glossary/invest/)
- [Gherkin Syntax](https://cucumber.io/docs/gherkin/)
