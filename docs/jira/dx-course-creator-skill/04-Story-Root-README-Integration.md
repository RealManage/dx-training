# Story: Root README Integration

## Priority / Epic

LOW - DX Course Creator Skill

## Effort Estimate

1 story point

## Prerequisites / Dependencies

- Story 02 (Implement Course Scaffolding) — course must exist before updating root README

## Description

Extend the `/dx-course-creator` skill to automatically add a new course entry to the dx-training root `README.md` under the "Available Courses" section. The entry follows the existing pattern used by AI 101 and BDD 101.

### Tasks

1. **Parse root README** — Find the "Available Courses" section
2. **Generate course entry** — Title, duration, level, status, description, link
3. **Insert entry** — Add before the "Coming Soon" section
4. **Preserve formatting** — Match existing entry style exactly

### Design Decisions

- New courses are inserted with `**Status:** Draft` until manually promoted to `Active`
- Description is a placeholder that the course author fills in
- Link points to the generated course README

## Acceptance Criteria

- [ ] New course entry added to root README "Available Courses" section
- [ ] Entry follows existing AI 101 / BDD 101 format
- [ ] Status defaults to `Draft`
- [ ] Link resolves to the generated course README
- [ ] Existing README content not modified or corrupted

## Technical Details

### Generated Entry Example

```markdown
### [BDD 201: Advanced BDD](./courses/bdd-201-advanced/)

**Duration:** 4 sessions (2 hours each)
**Level:** Intermediate
**Status:** Draft

[Course description placeholder — update before publishing]

[**Start Course →**](./courses/bdd-201-advanced/README.md)
```

### Insertion Point

Insert after the last active course entry and before the "Coming Soon" section:

```markdown
### Coming Soon
```

## Testing

1. Verify entry appears in correct location in root README
2. Verify link resolves to generated course directory
3. Verify existing entries are unchanged
4. Verify markdown renders correctly
