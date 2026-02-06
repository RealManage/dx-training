# Epic: AI 201 Advanced Claude Code

## Priority

HIGH

## Effort Estimate

34 story points (across 10 stories)

## Description

Create an advanced Claude Code training course for RealManage teams that builds on AI 101 foundations. Covers spec-driven development, Agent Teams, multi-agent orchestration with git worktrees, and trust calibration for enterprise environments. Uses the dx-claude-code `epic-orchestrator` skill as a real-world teaching example.

### Current State

- AI 101 course exists covering fundamentals through automation (9 weeks)
- "AI 102: Advanced Agent Development" listed as placeholder in dx-training README
- Anthropic released Agent Teams in Opus 4.6 (Feb 2026) — experimental feature, disabled by default, enabled via environment variable
- Industry shifting from prompt-driven iteration to spec-driven autonomous development
- No training material exists for Agent Teams, spec-driven dev, or trust calibration

### Target State

- 5-week course: 4 learning weeks + 1 capstone
- Strong common core everyone follows, with 4 lightweight role tracks per week
- Tracks: Developer, Architect, Product/Project Manager, QA
- "Orchestrator Mindset" as the through-line narrative
- Uses dx-claude-code epic-orchestrator skill as a production example
- "AI Orchestrator" certification as the 201 credential

### Course Curriculum

| Week | Topic | Key Concept |
| - | ----- | ----------- |
| 1 | Spec-Driven Development | Coder → Spec Writer |
| 2 | Agent Teams | Solo → Squadron |
| 3 | Multi-Agent Orchestration & Worktrees | Single-track → Multi-track |
| 4 | Trust, Autonomy & Enterprise Patterns | Control → Calibrated Trust |
| 5 | Capstone: Orchestration Project | Apply Everything |

### Business Value

- Enables teams to leverage Agent Teams for parallel development
- Reduces epic delivery time through orchestrated multi-agent workflows
- Builds trust calibration skills for enterprise AI adoption
- Provides certification path for advanced Claude Code competency
- Creates internal expertise that can scale across RealManage engineering

## Acceptance Criteria

- [ ] Course directory scaffolded following AI 101 patterns
- [ ] README with curriculum overview, mermaid diagram, prerequisites
- [ ] Resources directory with reference materials and templates
- [ ] Weeks 1-4 each have README, runbook, and 4 track files
- [ ] Week 5 capstone with 3 project options and evaluation rubric
- [ ] Certification requirements defined for all 4 tracks
- [ ] dx-training root README updated with AI 201 entry

## Technical Details

### Course Structure

```text
courses/ai-201-advanced-claude-code/
├── CLAUDE.md                         # Course-level AI context
├── README.md                         # Curriculum overview
├── resources/                        # Reference materials
│   ├── spec-template.md
│   ├── agent-teams-quickref.md
│   ├── worktree-cheatsheet.md
│   ├── trust-decision-tree.md
│   ├── orchestrator-glossary.md
│   ├── cost-optimization-guide.md
│   ├── troubleshooting-201.md
│   └── agent-teams-patterns.md
├── sessions/
│   ├── week-1/                       # Spec-Driven Development
│   │   ├── README.md
│   │   ├── runbook.md
│   │   └── tracks/
│   │       ├── developer.md
│   │       ├── architect.md
│   │       ├── pm.md
│   │       └── qa.md
│   ├── week-2/                       # Agent Teams
│   ├── week-3/                       # Multi-Agent Orchestration
│   ├── week-4/                       # Trust & Enterprise
│   └── week-5/                       # Capstone
│       ├── README.md
│       ├── runbook.md
│       └── tracks/
└── docs/
    └── certification/
        └── requirements.md
```

### Design Philosophy

- **Common core first** — everyone follows the same weekly README
- **Tracks are lightweight** — "apply to your role" exercises, not heavy parallel content
- **Orchestrator mindset through-line** — not a separate week, woven into every session
- **AI 101 prerequisite** — builds on foundations, doesn't repeat them

## Dependencies

- AI 101 course (prerequisite for students)
- dx-claude-code epic-orchestrator skill (teaching example)
- Anthropic Agent Teams (Opus 4.6+)

## Out of Scope

- Rewriting AI 101 content
- Building custom training platforms
- External certification (Anthropic-official)
- Video content production
