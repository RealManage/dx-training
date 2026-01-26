# Capstone Option E: Product Design & Documentation

## Project Purpose
Use Claude Code to create complete product specifications, user stories, and
stakeholder documentation for a new HOA capability. This is a NON-CODING capstone.

## Deliverables
- Product Requirements Document (PRD)
- User Story Mapping
- Stakeholder Documentation
- Process Documentation

## Requirements

### Core Features (Must Have)
1. Complete PRD with problem statement, requirements, and success metrics
2. User stories following INVEST principles
3. Acceptance criteria in Gherkin format
4. Stakeholder-specific documentation
5. Documentation of Claude-assisted process

### Feature to Specify
Choose one of these HOA capabilities:
- Resident Self-Service Portal
- Board Meeting Management
- Violation Appeals Process
- Community Amenity Booking

### Custom Skill Required
Create `/generate-user-stories <feature>` skill that:
1. Takes a feature name as input
2. Generates properly formatted user stories
3. Includes acceptance criteria
4. Adds priority and estimates

## Getting Started

```bash
# Set up your workspace
mkdir -p docs/stakeholder
mkdir -p .claude/skills/generate-user-stories

# Start Claude Code
claude
```

## Suggested Implementation Order

1. Choose your feature to specify
2. Research and define the problem statement
3. Create user personas
4. Draft the PRD with Claude's help
5. Generate user stories for each capability
6. Write acceptance criteria in Gherkin
7. Create stakeholder documentation
8. Document your process

## Success Criteria

```
[ ] PRD covers problem, solution, and success metrics
[ ] User stories follow INVEST principles
[ ] Acceptance criteria are testable and clear
[ ] Documentation is stakeholder-ready
[ ] All artifacts generated with Claude assistance
[ ] No coding required for submission
```

## INVEST Principles for Stories

- **I**ndependent: Can be developed separately
- **N**egotiable: Details can be discussed
- **V**aluable: Delivers user value
- **E**stimable: Can be sized
- **S**mall: Fits in a sprint
- **T**estable: Has clear acceptance criteria

## Gherkin Format Example

```gherkin
Feature: View Payment History

  Scenario: Resident views their payment history
    Given I am a logged-in resident
    And I have made 5 payments in the past year
    When I navigate to Payment History
    Then I should see all 5 payments listed
    And each payment shows date, amount, and status
```

## PRD Template Structure

```markdown
# [Feature Name] PRD

## Executive Summary
## Problem Statement
## User Personas
## Functional Requirements
## Non-Functional Requirements
## Success Metrics
## Timeline and Milestones
## Dependencies and Risks
## Out of Scope
```

## Tips for Using Claude Code

- Be specific in your prompts for better output
- Iterate on Claude's suggestions - first drafts are starting points
- Add HOA domain knowledge that Claude might not know
- Review for accuracy against actual business rules
- Document your prompts and iteration process
