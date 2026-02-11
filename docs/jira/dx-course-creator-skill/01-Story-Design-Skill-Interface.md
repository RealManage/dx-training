# Story: Design Skill Interface

## Priority / Epic

MEDIUM - DX Course Creator Skill

## Effort Estimate

2 story points

## Prerequisites / Dependencies

None — this is the design story

## Description

Define the `/dx-course-creator` skill specification including YAML frontmatter, argument schema, interaction flow, and expected outputs. Establish the contract between the skill prompt and the generation logic.

### Tasks

1. **Define YAML frontmatter** — name, description, arguments, allowed tools
2. **Design argument schema** — required vs optional inputs, validation rules
3. **Map interaction flow** — what the skill asks for, what it assumes, what it generates
4. **Document expected outputs** — list every file and directory the skill creates
5. **Define error handling** — what happens if course already exists, invalid inputs, etc.

### Design Decisions

- Skill uses `$ARGUMENTS` for the course identifier, prompts for additional details
- Session topics can be provided upfront or interactively
- Tracks default to `[developer, qa, pm, support]` but can be customized
- Skill refuses to overwrite an existing course directory

## Acceptance Criteria

- [ ] YAML frontmatter defined with all arguments
- [ ] Argument validation rules documented
- [ ] Interaction flow documented (what's prompted, what's assumed)
- [ ] Expected output file list complete
- [ ] Error scenarios defined
- [ ] Design reviewed and approved

## Technical Details

### Proposed Arguments

| Argument | Required | Default | Description |
| -------- | -------- | ------- | ----------- |
| `course-name` | Yes | — | Course identifier (e.g., `bdd-201-advanced`) |
| `session-count` | Yes | — | Number of sessions to scaffold |
| `topics` | No | — | Comma-separated session topics (prompted if omitted) |
| `tracks` | No | `developer,qa,pm,support` | Comma-separated role tracks |
| `level` | No | `Beginner` | Course difficulty level |

### Interaction Flow

```text
User: /dx-course-creator bdd-201-advanced
Skill: How many sessions? → 4
Skill: Session topics? → [prompted or provided]
Skill: Role tracks? → [default or custom]
Skill: → Generates everything
Skill: → Reports what was created
```

## Testing

1. Validate YAML frontmatter parses correctly
2. Verify argument schema handles edge cases (spaces, special characters)
3. Confirm interaction flow is clear and unambiguous
