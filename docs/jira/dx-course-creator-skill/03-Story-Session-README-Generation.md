# Story: Session README Generation

## Priority / Epic

MEDIUM - DX Course Creator Skill

## Effort Estimate

3 story points

## Prerequisites / Dependencies

- Story 02 (Implement Course Scaffolding) — session directories must exist

## Description

Extend the `/dx-course-creator` skill to generate self-contained session READMEs for each session directory. Each README follows the fenced markdown example from `templates/course-template.md` and is pre-populated with the session number, topic name, and placeholder content for objectives, agenda, and exercises.

### Tasks

1. **Parse session template** — Extract the fenced `````markdown` example from the course template
2. **Generate session READMEs** — Create `sessions/session-N/README.md` for each session
3. **Populate session metadata** — Fill session number, topic name, time allocation
4. **Add placeholder objectives** — 3-5 bracketed objective placeholders per session
5. **Add placeholder agenda** — Standard agenda structure with time blocks
6. **Link track files** — Reference `tracks/developer.md`, `tracks/qa.md`, etc. from each session README

### Design Decisions

- Session READMEs are fully self-contained — they don't depend on the course README for content
- Placeholder text uses `[bracketed]` convention for easy find-and-replace
- Agenda time blocks default to the template pattern (20/30/50/10/10 minutes)
- Track references are relative links that work from the session directory

## Acceptance Criteria

- [ ] Each session directory has a `README.md`
- [ ] Session number and topic name populated correctly
- [ ] Placeholder objectives included (3-5 per session)
- [ ] Agenda structure follows template pattern
- [ ] Track files referenced with working relative links
- [ ] Generated READMEs render correctly on GitHub and GitLab
- [ ] Structure matches the fenced example in `templates/course-template.md`

## Technical Details

### Generated Session README Example

For session 2 with topic "Writing Scenarios":

```markdown
# Session 2: Writing Scenarios

## Objectives

By the end of this session, you'll be able to:

- [Measurable outcome 1]
- [Measurable outcome 2]
- [Measurable outcome 3]

## Agenda (2 hrs)

### 1. [Introduction / Review] (20 min)

- [Key concept overview]
- [Why this matters for RealManage]

### 2. [Core Content Block] (30 min)

- [Conceptual overview]
- [Step-by-step guide]

### 3. [Hands-On Workshop] (50 min)

- Copy the [Example](./example/) to your sandbox
- [Specific exercise description]

### 4. [Deep Dive / Advanced] (10 min)

- [Extended concepts]

### 5. Reflection & Practice (10 min)

- Discuss takeaways
- Share insights in Slack

## Role-Specific Tracks

- [Developer Track](./tracks/developer.md)
- [QA Track](./tracks/qa.md)
- [PM Track](./tracks/pm.md)
- [Support Track](./tracks/support.md)
```

## Testing

1. Verify session numbers are sequential and correct
2. Verify topic names match the input topics list
3. Verify track links resolve to existing files
4. Run markdown linter on all generated session READMEs
