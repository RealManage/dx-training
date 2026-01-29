---
name: write-epic
description: Generate a comprehensive epic with child stories from a feature description
argument-hint: <feature_description>
---

Create a comprehensive epic from this feature request:

$ARGUMENTS

## Output Format

Generate a markdown epic with these sections:

### Epic: [Title]

**Epic ID:** EPIC-XXX (placeholder)
**Priority:** High | Medium | Low
**Target Release:** [Quarter/Sprint]

### Vision Statement

A 2-3 sentence description of what this epic delivers and why it matters.

### Business Value

- Primary benefit to users/business
- Metrics that will improve
- Strategic alignment

### Scope

#### In Scope

- Bullet list of what's included

#### Out of Scope

- Bullet list of what's explicitly NOT included

### User Stories

Break this epic into 3-6 user stories:

#### Story 1: [Title]

**As a** [role], **I want** [action], **So that** [benefit]

- AC1: Given/When/Then
- AC2: Given/When/Then
- Estimate: X points

#### Story 2: [Title]

(repeat format)

### Dependencies

- External systems or teams
- Prerequisites that must be complete first

### Risks & Mitigations

| Risk | Impact | Mitigation |
| ---- | ------ | ---------- |
| [Risk] | High/Med/Low | [Mitigation strategy] |

### Success Metrics

- Metric 1: [Current] → [Target]
- Metric 2: [Current] → [Target]

## Guidelines

- Stories should be independently deliverable
- Each story should fit in a single sprint
- Consider MVP vs full feature scope
