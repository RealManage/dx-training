# Story: Week 1 Spec Driven Development

## Priority / Epic

HIGH - AI 201 Advanced Claude Code

## Effort Estimate

5 story points

## Prerequisites / Dependencies

- Story 01 (Scaffold Course Directory Structure)

## Description

Create Week 1 session content: "From Code to Contracts." Teaches the shift from writing code to writing specs that agents execute. Covers machine-readable specs, CLAUDE.md as contracts, and spec-driven execution with auto-accept mode. Pre-session reading: Distribute `resources/spec-template.md` before the session.

### Key Concept

**Coder → Spec Writer** — The most valuable skill in agentic development is defining *what* to build, not *how* to build it.

### Session Outline

| Time | Section | Content |
| - | ------- | ------- |
| 20 min | The Spec-Driven Shift | Why specs matter more than prompts |
| 15 min | CLAUDE.md as Contract | Machine-readable project definitions |
| 30 min | Spec Anatomy | Structure, acceptance criteria, constraints |
| 30 min | Hands-On: Spec → Execute | Convert HOA feature request → spec → auto-accept execution |
| 15 min | Track Exercises | Role-specific exercises. PMs: the value of the auto-accept demo is seeing the OUTCOME difference between a good spec and a bad prompt. Focus on the spec quality, not the generated code. |
| 10 min | Reflection | Key takeaways, preview Week 2 |

### Tasks

1. **README.md** — Full session plan with learning objectives, agenda, exercises, homework
2. **runbook.md** — Instructor facilitation notes, timing, discussion prompts
3. **tracks/developer.md** — Write 2 specs for HOA features, execute each
4. **tracks/architect.md** — Review and critique the provided spec template, suggest 3 improvements
5. **tracks/pm.md** — Convert 2 product requirements into machine-readable specs
6. **tracks/qa.md** — Write test specs from acceptance criteria, generate test suites

## Acceptance Criteria

- [ ] `sessions/week-1/README.md` has complete session plan
- [ ] Learning objectives focus on spec writing, not prompting (that was AI 101)
- [ ] Hands-on exercise uses a real HOA feature request
- [ ] Auto-accept mode demonstrated with a well-written spec
- [ ] All 4 track files have role-specific exercises
- [ ] Runbook includes instructor timing and facilitation notes
- [ ] References `resources/spec-template.md` for template
- [ ] Homework prepares students for Week 2 (Agent Teams)

## Technical Details

### Core Content: Why Specs Beat Prompts

```text
Prompt-Driven:
  "Build me a payment service" → iterate 5 times → maybe works

Spec-Driven:
  spec.md → auto-accept → works first time (or fails clearly)
```

Key teaching points:

- Prompts are conversations; specs are contracts
- CLAUDE.md defines the "always" rules; specs define the "this time" rules
- Auto-accept mode only works when specs are precise
- Bad specs produce bad results predictably (which is still better than unpredictable)

### Hands-On Exercise

Convert this HOA feature request into a machine-readable spec:

> "We need residents to be able to submit maintenance requests through the portal. They should be able to describe the issue, attach photos, and track the status. The property manager needs to assign requests to vendors."

Students write the spec, then execute with `claude --auto-accept` to see the difference between vague prompts and precise specs.

### Track Exercises

| Track | Exercise | Deliverable |
| - | -------- | ----------- |
| Developer | Write 2 specs for HOA features | 2 spec files, each executed |
| Architect | Review and critique the provided spec template, suggest 3 improvements | Annotated spec template with improvement rationale |
| PM | Convert 2 product requirements into machine-readable specs | 2 feature specs with acceptance criteria |
| QA | Write test specs from acceptance criteria | Test spec → generated test suite |

## Testing

1. README renders correctly with all sections
2. Hands-on exercise is completable in 30 minutes
3. Track exercises are appropriately scoped (15-20 min each)
4. Runbook timing adds up to ~2 hours
5. No AI 101 content repeated
