# Epic: DX Course Creator Skill

## Priority

MEDIUM

## Effort Estimate

13 story points (across 5 stories)

## Description

Build a Claude Code skill (`/dx-course-creator`) that scaffolds new training courses from the dx-training course template. The skill takes a course name, session count, and topic list as input, then generates the complete directory structure, course README with curriculum table, individual session READMEs, CLAUDE.md, and resource stubs — all following established dx-training conventions.

### Current State

- Course template exists at `templates/course-template.md` with curriculum table pattern and fenced session README example
- New courses are created manually by copying files and replacing placeholders
- AI 102 was scaffolded by hand using AI 101 as a reference
- No automated or skill-driven way to create a new course

### Target State

- `/dx-course-creator` skill available in the dx-training repo
- Takes structured input (course name, ID, session count, topics, tracks)
- Generates complete course skeleton ready for content authoring
- Follows all dx-training conventions (folder naming, README structure, curriculum table)
- Updates root README to list the new course
- Output is lint-clean and renders correctly on GitHub and GitLab

### Business Value

- Reduces course creation bootstrap time from hours to minutes
- Enforces consistency across all dx-training courses
- Lowers the barrier for non-DX-team members to propose new courses
- Serves as a real-world skill example for AI 101 Week 5 (Commands & Skills)
- Eliminates manual copy-paste errors when starting from template

## Acceptance Criteria

- [ ] `/dx-course-creator` skill exists in `.claude/commands/` or `.claude/skills/`
- [ ] Skill accepts course name, session count, and topic list as input
- [ ] Generated directory structure matches dx-training conventions
- [ ] Course README includes populated curriculum table with session links
- [ ] Session READMEs follow the template's fenced example structure
- [ ] Course CLAUDE.md generated with correct metadata
- [ ] Root README updated with new course entry
- [ ] All generated markdown is lint-clean
- [ ] Skill is documented with usage examples

## Technical Details

### Skill Interface

```yaml
---
name: dx-course-creator
description: Scaffold a new dx-training course from template
arguments:
  - name: course-name
    description: "Course identifier (e.g., ai-102-advanced-claude-code)"
  - name: session-count
    description: "Number of sessions (e.g., 5)"
  - name: topics
    description: "Comma-separated session topics"
---
```

### Generated Structure

```text
courses/[course-name]/
├── CLAUDE.md
├── README.md                    # Curriculum table, prereqs, mermaid
├── resources/
│   └── troubleshooting.md
├── sessions/
│   ├── session-1/
│   │   ├── README.md            # Self-contained lesson plan
│   │   └── tracks/
│   │       ├── developer.md
│   │       └── [additional tracks]
│   ├── session-2/
│   └── ... session-N
└── exercises/
```

### Design Decisions

- Uses `session-<n>` folder naming (not `week-<n>`) per updated template convention
- Reads `templates/course-template.md` as the source of truth for structure
- Curriculum table is auto-populated from the topics argument
- Track files are stubs with placeholder content — content authoring is a separate task
- Skill lives in the dx-training repo, not a standalone plugin

## Dependencies

- `templates/course-template.md` (source template — already exists)
- Claude Code skill/command infrastructure (available in CLI)

## Out of Scope

- Content authoring for sessions (that's the course author's job)
- Jira story generation for the new course
- Publishing or deployment automation
- Video or multimedia content generation
