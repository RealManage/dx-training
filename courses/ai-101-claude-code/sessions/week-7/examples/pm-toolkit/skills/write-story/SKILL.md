---
name: write-story
description: Generate a well-structured user story from a plain English description
argument-hint: <description>
---

Convert the following requirement into a properly formatted user story:

$ARGUMENTS

## Output Format

Generate a markdown user story with these sections:

### Title
A concise, action-oriented title (e.g., "Add Payment Reminder Emails")

### User Story
As a [specific user role],
I want [specific action/feature],
So that [specific benefit/value].

### Acceptance Criteria
Use the Given/When/Then format for each criterion:
- Given [precondition], When [action], Then [expected result]
- Include 3-5 acceptance criteria covering happy path and edge cases

### Technical Notes
- List any technical considerations
- Note dependencies on other systems
- Flag potential risks or unknowns

### Story Points Estimate
Suggest a point estimate (1, 2, 3, 5, 8, 13) with brief rationale.

## Guidelines
- Be specific, not vague
- Focus on user value, not implementation
- Include testable acceptance criteria
- Consider edge cases and error scenarios
