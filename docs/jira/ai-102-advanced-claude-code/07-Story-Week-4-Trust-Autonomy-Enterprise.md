# Story: Week 4 Trust Autonomy Enterprise

## Priority / Epic

HIGH - AI 102 - Advanced Claude Code

## Effort Estimate

5 story points

## Prerequisites / Dependencies

- Story 01 (Scaffold Course Directory Structure)

## Description

Create Week 4 session content: "Trust Calibration." Teaches the trust spectrum from full human control to high autonomy, strategies for legacy vs greenfield codebases, cost management, and security hooks for enterprise environments.

### Key Concept

**Control → Calibrated Trust** — Trust isn't binary. Learn to calibrate autonomy based on code maturity, risk level, test coverage, and business criticality.

### Session Outline

| Time | Section | Content |
| - | ------- | ------- |
| 20 min | The Trust Spectrum | Full control → calibrated trust → high autonomy |
| 15 min | Trust Signals | Test coverage, code age, criticality, reviewer familiarity |
| 20 min | Legacy Code Strategies | Step mode, incremental trust, characterization tests (tests that document what code currently does before you change it) |
| 15 min | Cost & Token Management | Model selection, budget strategies, optimization |
| 5 min | Security Guardrails Overview | What guardrails exist and why they matter (PM-friendly overview). PM sidebar: understand WHAT guardrails exist and WHY — configuration details are for developers. |
| 15 min | Trust-Calibrated Hook Configuration | Hooks whose behavior changes based on codebase trust level. NOTE: AI 101 Week 6 covers hook basics (PreToolUse, PostToolUse, etc.). This section focuses on TRUST-CALIBRATED hooks — do not re-teach hook fundamentals. |
| 20 min | Hands-On: Trust Calibration | Apply different trust levels to 3 codebases |
| 10 min | Track Exercises | Role-specific exercises |
| 5 min | Capstone Kickoff | Introduce take-home capstone project (Story 08), assign options, set Week 5 presentation expectations |

### Tasks

1. **README.md** — Full session plan with learning objectives, agenda, exercises
2. **runbook.md** — Instructor facilitation notes, discussion prompts for trust dilemmas
3. **tracks/developer.md** — Apply trust levels to legacy vs greenfield codebases
4. **tracks/architect.md** — Design trust policy framework for an engineering organization
5. **tracks/pm.md** — Create trust communication plan for stakeholders
6. **tracks/qa.md** — Design verification strategies per trust level

## Acceptance Criteria

- [ ] `sessions/week-4/README.md` has complete session plan
- [ ] Trust spectrum visualized (control → autonomy)
- [ ] Trust signals documented with concrete examples
- [ ] Legacy code strategy covers characterization tests and incremental trust
- [ ] Cost management section with model comparison and budget strategies
- [ ] Security hooks explained with practical examples
- [ ] Hands-on exercise uses 3 codebases with different trust profiles
- [ ] All 4 track files have role-specific exercises
- [ ] References `resources/trust-decision-tree.md` and `resources/cost-optimization-guide.md`
- [ ] Security hooks content explicitly differentiates from AI 101 Week 6 coverage

## Technical Details

### Core Content: The Trust Spectrum

```text
Full Control          Calibrated Trust          High Autonomy
├─ Step mode          ├─ Plan mode              ├─ Auto-accept
├─ Review every       ├─ Review plans           ├─ Spec-driven
│  change             │  then auto-execute      │  execution
├─ Manual testing     ├─ Auto-test with         ├─ CI/CD validates
│                     │  manual review          │
└─ Legacy code        └─ Maturing code          └─ Greenfield +
   No tests              Growing coverage           high coverage
```

### Trust Signals Matrix

| Signal | Low Trust | Medium Trust | High Trust |
| - | --------- | ------------ | ---------- |
| Test Coverage | <30% | 30-80% | >80% |
| Code Age | Legacy, undocumented | Maintained, some docs | Modern, well-documented |
| Business Criticality | Payments, auth, PII | Core features | Internal tools, scripts |
| Reviewer Familiarity | Unknown codebase | Familiar patterns | Own codebase |

### Hands-On Exercise

Three codebases representing different trust profiles:

1. **Legacy payments module** — No tests, handles money, complex business rules
   - Apply: Step mode, characterization tests first, incremental changes
2. **Maturing API service** — 60% coverage, well-structured, moderate complexity
   - Apply: Plan mode, review plans, auto-execute with test validation
3. **New internal CLI tool** — Greenfield, clear spec, no external dependencies
   - Apply: Auto-accept with comprehensive spec, high autonomy

### Track Exercises

| Track | Exercise | Deliverable |
| - | -------- | ----------- |
| Developer | Apply trust levels to legacy vs greenfield | Trust assessment + executed changes per level |
| Architect | Design trust policy framework for org | Policy document with trust tiers and criteria |
| PM | Create trust communication plan for stakeholders | Stakeholder-facing trust level explainer |
| QA | Design verification strategies per trust level | Verification matrix per trust tier |

## Testing

1. README renders correctly with all sections
2. Trust spectrum visualization is clear and actionable
3. Hands-on exercise codebases have realistic trust profiles
4. Track exercises are appropriately scoped
5. Cost management section uses current pricing information
