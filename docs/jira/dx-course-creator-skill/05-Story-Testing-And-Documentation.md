# Story: Testing and Documentation

## Priority / Epic

MEDIUM - DX Course Creator Skill

## Effort Estimate

2 story points

## Prerequisites / Dependencies

- Story 02 (Implement Course Scaffolding)
- Story 03 (Session README Generation)
- Story 04 (Root README Integration)

## Description

End-to-end testing of the `/dx-course-creator` skill and documentation for course authors. Verify the skill produces valid, consistent output across different course configurations. Write usage guide with examples.

### Tasks

1. **End-to-end test: minimal course** — 2 sessions, default tracks, no custom options
2. **End-to-end test: full course** — 8+ sessions, custom tracks, all options specified
3. **End-to-end test: edge cases** — 1 session, special characters in names, duplicate course name
4. **Validate generated markdown** — Lint all output files, verify rendering
5. **Verify link integrity** — All internal links resolve correctly
6. **Write usage documentation** — README in the skill directory with examples
7. **Add skill to dx-training CLAUDE.md** — Reference the skill in repo-level AI context

### Design Decisions

- Testing is manual (run skill, inspect output) — no automated test framework needed
- Documentation lives alongside the skill file
- Usage examples cover common scenarios (3-session workshop, 8-week course, etc.)

## Acceptance Criteria

- [ ] Minimal course (2 sessions) generates correctly
- [ ] Full course (8+ sessions) generates correctly
- [ ] Edge cases handled gracefully (existing course, 1 session, special chars)
- [ ] All generated markdown passes linting
- [ ] All internal links resolve
- [ ] Usage documentation written with 3+ examples
- [ ] Skill referenced in dx-training CLAUDE.md

## Technical Details

### Test Matrix

| Test Case | Sessions | Tracks | Expected Result |
| --------- | -------- | ------ | --------------- |
| Minimal | 2 | default | 2 session dirs, course README, CLAUDE.md |
| Standard | 5 | default | 5 session dirs, full curriculum table |
| Large | 10 | custom (3) | 10 session dirs, 3 track files per session |
| Edge: exists | — | — | Error: course directory already exists |
| Edge: single | 1 | default | 1 session dir, degenerate mermaid |

### Usage Documentation Example

````markdown
# /dx-course-creator

Scaffold a new training course in the dx-training repo.

## Usage

```text
/dx-course-creator <course-name>
```

## Examples

### Quick 3-session workshop

```text
/dx-course-creator git-201-advanced
```
Then provide: 3 sessions, topics: "Rebasing & History, Worktrees & Workflows, Capstone"

### Full 8-session course

```text
/dx-course-creator devops-101-foundations
```
Then provide: 8 sessions, custom topics, custom tracks

## What Gets Created

- `courses/<name>/README.md` — Curriculum table, prereqs, mermaid
- `courses/<name>/CLAUDE.md` — AI context for the course
- `courses/<name>/sessions/session-N/README.md` — Lesson plans
- `courses/<name>/sessions/session-N/tracks/*.md` — Role-specific exercises
- `courses/<name>/resources/` — Reference material stubs
- Updated root `README.md` with new course entry
````

## Testing

1. Run all test cases from test matrix
2. Verify documentation examples match actual skill behavior
3. Peer review documentation for clarity
