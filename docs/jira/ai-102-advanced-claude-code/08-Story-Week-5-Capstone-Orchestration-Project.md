# Story: Week 5 Capstone Orchestration Project

## Priority / Epic

MEDIUM - AI 102 - Advanced Claude Code

## Effort Estimate

3 story points

## Prerequisites / Dependencies

- Story 04 (Week 1)
- Story 05 (Week 2)
- Story 06 (Week 3)
- Story 07 (Week 4)

## Description

Create Week 5 capstone session: "Apply Everything." The capstone project is assigned at the end of Week 4. Students have one week to complete their chosen project option. Week 5 is the presentation and evaluation session. Students choose one of four project options that require spec-driven development, Agent Teams (natural language team creation, shared task lists, delegate/plan approval modes), worktree orchestration for file isolation, and trust calibration. Includes evaluation rubric covering all course competencies.

### Key Concept

**Apply Everything** — Demonstrate mastery by orchestrating a complete project from spec through delivery using all techniques learned in Weeks 1-4.

### Capstone Kickoff (Week 4)

The capstone project is introduced and assigned at the end of the Week 4 session. Students select their project option and have one full week to complete the work before the Week 5 presentation session.

### Session Outline (Week 5)

| Time | Section | Content |
| - | ------- | ------- |
| 10 min | Capstone Kickoff Recap & Office Hours | Review project requirements, answer final questions |
| 15 min | Final Work Time / Troubleshooting | Last opportunity for troubleshooting and polishing |
| 60 min | Presentations | 4-5 min each for ~12 students |
| 20 min | Peer Review and Scoring | Structured peer feedback using evaluation rubric |
| 15 min | Certification Ceremony and Course Wrap-Up | Award certificates, course retrospective, next steps |

### Project Options

| Option | Description | Best For |
| - | ----------- | -------- |
| A: Implementation Pipeline | Execute a single spec with a 2-teammate team in worktrees and document your trust calibration rationale | Developers, QA |
| B: Migration Pipeline | Write characterization tests for one legacy module and produce a migration spec with trust escalation document | Architects, Developers |
| C: Architecture Design | Design a trust policy framework with tier definitions and an Agent Teams guideline document | Architects, PMs |
| D: Product AI Adoption Strategy | Design an AI adoption strategy for a product team | Product Managers |

### Tasks

1. **README.md** — Capstone overview, project options, submission requirements
2. **runbook.md** — Instructor facilitation: coaching, office hours, evaluation process
3. **tracks/developer.md** — Developer-specific capstone requirements and rubric
4. **tracks/architect.md** — Architect-specific capstone requirements and rubric
5. **tracks/pm.md** — PM-specific capstone requirements and rubric
6. **tracks/qa.md** — QA-specific capstone requirements and rubric

## Acceptance Criteria

- [ ] `sessions/week-5/README.md` has capstone overview and all 4 project options
- [ ] Each project option has clear scope, deliverables, and time estimate
- [ ] Evaluation rubric covers: spec quality, agent coordination, trust calibration, code quality
- [ ] Track-specific requirements and rubric in each track file
- [ ] Submission process documented (branch, MR, or document)
- [ ] Runbook includes coaching notes and evaluation checklist

## Technical Details

### Option A: Implementation Pipeline

**Scope:** Execute a single spec with a 2-teammate team in worktrees and document your trust calibration rationale.

**Core Deliverable:** A completed spec execution with documented trust calibration rationale.

**Deliverables:**

1. Feature spec (machine-readable, per Week 1 standards)
2. Agent Teams execution with 2 teammates in worktrees
3. Trust calibration rationale document

**Evaluation:**

- Spec completeness and clarity (25%)
- Agent coordination effectiveness (25%)
- Code quality and test coverage (25%)
- Trust calibration rationale (25%)

### Option B: Migration Pipeline

**Scope:** Write characterization tests for one legacy module and produce a migration spec with trust escalation document.

**Core Deliverable:** Characterization tests + migration spec with trust escalation.

**Deliverables:**

1. Characterization test suite for one legacy module
2. Migration spec (incremental, per Week 1 standards)
3. Trust escalation document showing progression from low to higher trust

### Option C: Architecture Design

**Scope:** Design a trust policy framework with tier definitions and an Agent Teams guideline document.

**Core Deliverable:** Trust policy framework + Agent Teams guidelines.

**Deliverables:**

1. Trust policy framework with tier definitions and escalation criteria
2. Agent Teams guideline document (team sizing, patterns, anti-patterns)

### Option D: Product AI Adoption Strategy

**Scope:** Design an AI adoption strategy for a product team.

**Best For:** Product Managers

**Core Deliverable:** A complete AI adoption strategy with stakeholder communication plan.

**Deliverables:**

1. AI-ready feature spec for a real product area
2. Trust calibration framework for the product's codebases
3. Stakeholder communication plan
4. Cost-benefit analysis
5. 90-day adoption roadmap

### Evaluation Rubric

| Criterion | Excellent (4) | Good (3) | Developing (2) | Beginning (1) |
| - | ------------- | -------- | --------------- | -------------- |
| Spec Quality | Clear, complete, machine-readable | Mostly complete, minor gaps | Vague criteria, missing constraints | Prompt-style, not spec-driven |
| Agent Coordination | Effective delegation, clean boundaries | Good delegation, minor overlap | Some coordination, unclear boundaries | Solo execution, no team use |
| Trust Calibration | Justified trust levels, documented rationale | Reasonable trust levels | Some trust awareness | No trust consideration |
| Deliverable Quality | Production-ready, well-tested | Working, adequately tested | Partially working | Incomplete |

## Testing

1. Each project option is achievable as a 1-week take-home assignment
2. Evaluation rubric is objective and scorable
3. Track-specific requirements are appropriate per role
4. Submission process is clear
