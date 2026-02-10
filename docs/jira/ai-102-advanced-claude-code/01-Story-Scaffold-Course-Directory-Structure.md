# Story: Scaffold Course Directory Structure

## Priority / Epic

HIGH - AI 102 - Advanced Claude Code

## Effort Estimate

2 story points

## Prerequisites / Dependencies

None — this is the bootstrap story

## Description

Create the `courses/ai-102-advanced-claude-code/` directory structure following AI 101 patterns. Includes session directories for weeks 1-5, resources directory, certification docs, and track files per week.

### Tasks

1. **Create course root** — `courses/ai-102-advanced-claude-code/`
2. **Create session directories** — `sessions/week-1/` through `sessions/week-5/`
3. **Create track directories** — `tracks/` in each week with `developer.md`, `architect.md`, `pm.md`, `qa.md`
4. **Create resources directory** — `resources/` for reference materials
5. **Create certification directory** — `docs/certification/`
6. **Create course CLAUDE.md** — AI context for the course

### Design Decisions

- Same 4 tracks as AI 101 (Developer, Architect, PM, QA) but with deeper, role-specific exercises
- `runbook.md` per week for instructor facilitation notes
- Common core is the focus; tracks are lighter exercises

## Acceptance Criteria

- [ ] `courses/ai-102-advanced-claude-code/` directory exists
- [ ] `sessions/week-1/` through `sessions/week-5/` created
- [ ] Each week has `tracks/` with `developer.md`, `architect.md`, `pm.md`, `qa.md`
- [ ] `resources/` directory created
- [ ] `docs/certification/` directory created
- [ ] Course-level `CLAUDE.md` created with course context
- [ ] Directory structure follows AI 101 conventions

## Technical Details

### Directory Tree

```text
courses/ai-102-advanced-claude-code/
├── CLAUDE.md
├── README.md                    # Story 02
├── resources/                   # Story 03
├── sessions/
│   ├── week-1/
│   │   ├── README.md            # Story 04
│   │   ├── runbook.md
│   │   └── tracks/
│   │       ├── developer.md
│   │       ├── architect.md
│   │       ├── pm.md
│   │       └── qa.md
│   ├── week-2/                  # Same structure (Story 05)
│   ├── week-3/                  # Same structure (Story 06)
│   ├── week-4/                  # Same structure (Story 07)
│   └── week-5/
│       ├── README.md            # Story 08
│       ├── runbook.md
│       └── tracks/
│           ├── developer.md
│           ├── architect.md
│           ├── pm.md
│           └── qa.md
└── docs/
    └── certification/
        └── requirements.md      # Story 09
```

### CLAUDE.md Content

Course-level context including:

- Course name and purpose
- Target audience and prerequisites
- Week topics and key concepts
- Track descriptions
- Reference to dx-claude-code skills as teaching tools

## Testing

1. Verify all directories exist
2. Verify track files exist in each week
3. Verify CLAUDE.md is valid markdown
4. Compare structure against AI 101 for consistency
