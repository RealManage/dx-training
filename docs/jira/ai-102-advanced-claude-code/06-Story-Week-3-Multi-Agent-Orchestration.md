# Story: Week 3 Multi Agent Orchestration

## Priority / Epic

HIGH - AI 102 - Advanced Claude Code

## Effort Estimate

5 story points

## Prerequisites / Dependencies

- Story 01 (Scaffold Course Directory Structure)
- Pre-session reading: Review the dx-claude-code `epic-orchestrator` skill walkthrough (SKILL.md, worktree-manager.sh, lead-playbook.md). Also review `resources/worktree-cheatsheet.md`.

## Description

Create Week 3 session content: "Orchestration at Scale." Teaches how git worktrees and Agent Teams work together as COMPLEMENTARY tools: Agent Teams provides coordination (shared task list, messaging, lead orchestration) while worktrees provide file isolation (each teammate gets own repo checkout, own branch). Also covers headless pipelines, advanced skills with state management, and the dx-claude-code epic-orchestrator skill as a production example.

### Key Concept

**Single-track → Multi-track** — Move from one-at-a-time development to parallel execution by combining Agent Teams (coordination layer from Week 2) with git worktrees (file isolation layer). Agent Teams provides the shared task list, messaging, and lead orchestration; worktrees provide each teammate with its own repo checkout and branch. Together they enable truly parallel, isolated development coordinated by orchestration skills and state files.

### Pre-Session Reading (Required)

Students must complete before class:

- Review the dx-claude-code `epic-orchestrator` skill: SKILL.md, worktree-manager.sh, lead-playbook.md, teammate-workflow.md, epic-status.sh
- Review `resources/worktree-cheatsheet.md`
- Focus areas: How the lead coordinates teammates, worktree lifecycle, state file patterns

### Session Outline

| Time | Section | Content |
| - | ------- | ------- |
| 20 min | Git Worktrees for Agents | Why worktrees, lifecycle, naming conventions. A git worktree is a separate working directory linked to the same repository — think of it as having multiple copies of your project, each on a different branch, open simultaneously. |
| 15 min | Key Takeaways from epic-orchestrator | Discussion of pre-session reading. What patterns did you notice? How does the lead coordinate? What would you change? |
| 15 min | State Management | State files, progress tracking, resume patterns |
| 30 min | Hands-On: 3-Worktree Implementation | 3 worktrees implementing different HOA modules. PM Track Note: see parallel exercise below. |
| 15 min | Merging & Conflict Resolution | Combining parallel work, handling conflicts |
| 15 min | Track Exercises | Role-specific exercises |
| 10 min | Reflection | Key takeaways, preview Week 4 |

### PM Track: Parallel Exercise

While developers work in worktrees during the hands-on section, PMs complete a parallel facilitated exercise: Design a multi-workstream delivery plan for a 3-story epic. Identify dependencies, define integration checkpoints, create a status dashboard concept using a shared task list. No git or code required.

### Tasks

1. **README.md** — Full session plan with learning objectives, agenda, exercises
2. **runbook.md** — Instructor facilitation notes, worktree demo scripts
3. **tracks/developer.md** — Implement 3 modules in parallel worktrees, merge results
4. **tracks/architect.md** — Design orchestration patterns for a multi-team organization
5. **tracks/pm.md** — Orchestrate parallel workstreams with dependency management
6. **tracks/qa.md** — Parallel test execution across worktrees, consolidated reporting

## Acceptance Criteria

- [ ] `sessions/week-3/README.md` has complete session plan
- [ ] Git worktree concepts explained with diagrams
- [ ] State management patterns documented (`.jira-upload-state.json` example)
- [ ] dx-claude-code `epic-orchestrator` skill used as a real-world example
- [ ] Hands-on exercise creates 3 worktrees with different modules
- [ ] Merge and conflict resolution covered
- [ ] All 4 track files have role-specific exercises
- [ ] Runbook includes worktree demo scripts
- [ ] References `resources/worktree-cheatsheet.md`
- [ ] PM track has a parallel hands-on exercise that doesn't require git worktree commands
- [ ] Plain-English definition of git worktree included where the concept is first introduced

## Technical Details

### Core Content: Why Worktrees

> **What is a git worktree?** A git worktree is a separate working directory linked to the same repository — think of it as having multiple copies of your project, each on a different branch, open simultaneously.

```text
Without worktrees:
  main → branch-1 → stash → branch-2 → stash → branch-3 (context switching hell)

With worktrees:
  repo/           → main branch (coordinator)
  repo-DX-501/    → story 1 (teammate 1)
  repo-DX-502/    → story 2 (teammate 2)
  repo-DX-503/    → story 3 (teammate 3)
```

Key teaching points:

- **Agent Teams + Worktrees are complementary:** Agent Teams (Week 2) provides coordination; worktrees provide file isolation. Week 3 combines both for full parallel development.
- Worktrees eliminate branch switching — each agent has its own directory
- State files track progress across worktrees
- The `worktree-manager.sh` script automates lifecycle management
- Naming conventions matter for organization at scale
- Cleanup is essential — stale worktrees waste disk space

### Production Skill Walkthrough (Pre-Session Reading)

Students review the dx-claude-code `epic-orchestrator` skill before class. The 5 key artifacts:

1. SKILL.md structure and trigger keywords
2. `worktree-manager.sh` — setup, list, cleanup commands
3. `lead-playbook.md` — how the lead coordinates teammates
4. `teammate-workflow.md` — boundaries and completion criteria
5. `epic-status.sh` — unified dashboard across systems

In-session discussion (15 min) focuses on key takeaways, patterns observed, and questions from the reading.

### Hands-On Exercise

Create 3 worktrees for an HOA epic:

- Worktree 1: ResidentPortal API endpoints
- Worktree 2: PaymentService backend logic
- Worktree 3: NotificationService email/SMS

Each worktree gets a teammate. After completion, merge all three back to main.

### Track Exercises

| Track | Exercise | Deliverable |
| - | -------- | ----------- |
| Developer | Implement 3 modules in parallel worktrees | 3 merged branches with clean history |
| Architect | Design orchestration patterns for multi-team org | Architecture doc with worktree strategy |
| PM | Map parallel workstreams with dependency tracking | Workstream diagram with critical path |
| QA | Run parallel test suites, produce consolidated report | Test report across 3 modules |

## Testing

1. README renders correctly with all sections
2. Worktree exercise is feasible on standard dev machines
3. dx-claude-code skill references are accurate
4. Track exercises are appropriately scoped
5. Merge exercise handles realistic scenarios
