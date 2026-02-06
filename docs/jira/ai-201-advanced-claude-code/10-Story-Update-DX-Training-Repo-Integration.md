# Story: Update DX Training Repo Integration

## Priority / Epic

MEDIUM - AI 201 Advanced Claude Code

## Effort Estimate

1 story point

## Prerequisites / Dependencies

- Story 02 (Course README And Curriculum Overview)

## Description

Update the dx-training root `README.md` to add AI 201 as an active course, replacing the "AI 102: Advanced Agent Development" placeholder. Update version history and any cross-references.

### Tasks

1. **Update Available Courses** — Add AI 201 entry with description, duration, level, status
2. **Remove placeholder** — Replace "AI 102: Advanced Agent Development" from "Coming Soon"
3. **Update version history** — Add entry for AI 201 addition
4. **Cross-references** — Ensure any links to AI 102 now point to AI 201

## Acceptance Criteria

- [ ] Root `README.md` has AI 201 listed as an active course
- [ ] "AI 102: Advanced Agent Development" placeholder removed from Coming Soon
- [ ] Course description, duration, level, and status are accurate
- [ ] Link to `courses/ai-201-advanced-claude-code/README.md` works
- [ ] Version history updated with AI 201 entry
- [ ] No broken links in README

## Technical Details

### README Changes

**Add to Available Courses (after AI 101):**

```markdown
### [RealManage AI 201: Advanced Claude Code](./courses/ai-201-advanced-claude-code/)

**Duration:** 5 weeks (2 hours/week)
**Level:** Advanced
**Prerequisites:** AI 101 completion required
**Status:** ✅ Active

Master Agent Teams, spec-driven development, multi-agent orchestration, and trust
calibration. Build on AI 101 foundations to become an AI Orchestrator — coordinating
teams of agents across parallel worktrees for production-grade delivery.

[**Start Course →**](./courses/ai-201-advanced-claude-code/README.md)
```

**Remove from Coming Soon:**

```markdown
- ~~AI 102: Advanced Agent Development~~
```

**Update Version History:**

```markdown
| 1.1.0 | 2026-02 | Added AI 201: Advanced Claude Code |
```

## Testing

1. Verify README renders correctly in GitLab
2. Verify link to AI 201 course resolves
3. Verify AI 102 placeholder is removed
4. Verify version history entry is present
