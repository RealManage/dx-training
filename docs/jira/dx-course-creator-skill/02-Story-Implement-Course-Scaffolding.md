# Story: Implement Course Scaffolding

## Priority / Epic

HIGH - DX Course Creator Skill

## Effort Estimate

5 story points

## Prerequisites / Dependencies

- Story 01 (Design Skill Interface) — need finalized argument schema and output spec

## Description

Implement the core `/dx-course-creator` skill that generates the complete course directory structure, course README with curriculum table, and course-level CLAUDE.md. This is the main implementation story.

### Tasks

1. **Create skill file** — `.claude/commands/dx-course-creator.md` with YAML frontmatter and prompt
2. **Directory scaffolding** — Generate `courses/[name]/` with `sessions/`, `resources/`, `exercises/`
3. **Session directories** — Create `session-1/` through `session-N/` with `tracks/` subdirectories
4. **Course README generation** — Populate from `templates/course-template.md`:
   - Fill course name, level, session count in Overview section
   - Build curriculum table with session topics and links
   - Generate mermaid learning path diagram
   - Fill prerequisites placeholder
5. **CLAUDE.md generation** — Course context with name, audience, session topics, track descriptions
6. **Track file stubs** — Create `developer.md`, `qa.md`, etc. in each session's `tracks/`
7. **Resource stubs** — Create `troubleshooting.md` and `quick-reference.md` placeholders

### Design Decisions

- Skill reads `templates/course-template.md` as source of truth — not hardcoded structure
- Uses `session-<n>` folder naming convention
- Curriculum table auto-populated from topic list
- Mermaid diagram auto-generated from session count and topics
- All generated files are valid, lint-clean markdown

## Acceptance Criteria

- [ ] Skill file exists and is invocable via `/dx-course-creator`
- [ ] Complete directory structure generated from arguments
- [ ] Course README has populated curriculum table with correct session links
- [ ] Mermaid diagram reflects actual session count and topics
- [ ] CLAUDE.md contains course metadata
- [ ] Track files exist in each session directory
- [ ] Resource stubs created
- [ ] No overwrite of existing course directories
- [ ] All generated markdown renders on GitHub and GitLab

## Technical Details

### Skill File Location

```text
.claude/commands/dx-course-creator.md
```

### Template Reading

The skill should reference `templates/course-template.md` and use it as the structural guide. Key sections to populate:

- `## 📚 Course Overview` — fill duration, level, goal
- `## 📖 Training Curriculum` — build table rows from topics
- `## 🗺️ Learning Path` — generate mermaid nodes
- `## 📁 Course Structure` — generate directory tree

### Generated Curriculum Table Example

For input `topics: "BDD Foundations, Writing Scenarios, Automation, Capstone"`:

```markdown
| Session | Topic | What You'll Learn | Time | Link |
| ------- | ----- | ----------------- | ---- | ---- |
| 1 | BDD Foundations | [To be filled by author] | 2 hrs | [Start →](./sessions/session-1/README.md) |
| 2 | Writing Scenarios | [To be filled by author] | 2 hrs | [Start →](./sessions/session-2/README.md) |
| 3 | Automation | [To be filled by author] | 2 hrs | [Start →](./sessions/session-3/README.md) |
| 4 | Capstone | [To be filled by author] | 2 hrs | [Start →](./sessions/session-4/README.md) |
```

## Testing

1. Run skill with sample inputs and verify all files created
2. Verify curriculum table links resolve to created session directories
3. Verify mermaid diagram renders correctly
4. Run markdown linter on all generated files
5. Compare output structure against AI 101 and BDD 101 for consistency
