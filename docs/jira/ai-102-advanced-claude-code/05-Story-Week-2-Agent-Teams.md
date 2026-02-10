# Story: Week 2 Agent Teams

## Priority / Epic

HIGH - AI 102 - Advanced Claude Code

## Effort Estimate

5 story points

## Prerequisites / Dependencies

- Story 01 (Scaffold Course Directory Structure)

## Description

Create Week 2 session content: "From Solo to Squadron." Teaches Agent Teams fundamentals — enabling via environment variable, natural language team creation, display modes (in-process default vs split-pane), delegate mode, plan approval, shared task lists, and mailbox messaging. Agent Teams is experimental and must be explicitly enabled. Hands-on exercise uses a 3-teammate code review followed by implementation from Week 1's spec.

### Key Concept

**Solo → Squadron** — Agent Teams transforms Claude from a solo assistant into a coordinated team. The developer's role shifts from writing code to directing teammates.

### Session Outline

| Time | Section | Content |
| - | ------- | ------- |
| 20 min | Agent Teams Overview | What it is, when to use it, mental model |
| 15 min | Enabling & Display Modes | Enable via `CLAUDE_CODE_EXPERIMENTAL_AGENT_TEAMS=1` in settings.json, in-process mode (DEFAULT — all teammates in one terminal, navigate with Shift+Up/Down, works in any terminal), split-pane mode (each teammate gets own pane, requires tmux or iTerm2, NOT supported in VS Code terminal/Windows Terminal/Ghostty). PM Note: PMs will use the default in-process display mode. The split-pane and headless configurations are optional for developers who prefer visual separation or CI/CD pipelines. |
| 20 min | Team Creation & Delegation | Natural language team creation, delegate mode (Shift+Tab), plan approval mode |
| 10 min | Shared Task Lists & Messaging | Creating tasks with pending/in_progress/completed states and dependencies, mailbox messaging (message one or broadcast to all) |
| 30 min | Hands-On: 3-Teammate Review | Code review with 3 teammates on different modules |
| 15 min | Hands-On: Spec → Team Execution | Execute Week 1's spec with a 2-teammate team |
| 10 min | Track Exercises | Role-specific exercises |

### Tasks

1. **README.md** — Full session plan with learning objectives, agenda, exercises
2. **runbook.md** — Instructor facilitation notes, timing, demo scripts
3. **tracks/developer.md** — Coordinate a 3-agent team for feature implementation
4. **tracks/architect.md** — Design org-wide Agent Teams patterns and guidelines
5. **tracks/pm.md** — Orchestrate requirements gathering with competing hypotheses
6. **tracks/qa.md** — Parallel test review team across 3 modules

## Acceptance Criteria

- [ ] `sessions/week-2/README.md` has complete session plan
- [ ] Agent Teams enabling (environment variable), display modes (in-process default vs split-pane), and natural language team creation covered
- [ ] Delegate mode and plan approval explained with examples
- [ ] Shared task list creation and monitoring demonstrated
- [ ] Hands-on exercise uses 3 teammates for code review
- [ ] Second exercise executes Week 1's spec with a team
- [ ] All 4 track files have role-specific exercises
- [ ] Runbook includes demo scripts for Agent Teams workflows (enablement via settings.json, natural language team creation prompts, keyboard shortcuts: Shift+Up/Down navigation, Shift+Tab for delegate mode)
- [ ] Cost awareness section (token usage per teammate)
- [ ] Non-developer-friendly language used where possible (e.g., 'workspace' instead of 'worktree' until Week 3 introduction)
- [ ] Runbook includes per-exercise cost estimates and fallback plans if students hit rate limits
- [ ] Experimental status and known limitations documented (no session resumption, task status lag, one team per session, no nested teams)
- [ ] Windows compatibility notes included (in-process mode works natively, split-pane requires WSL for tmux, split panes NOT supported in Windows Terminal or VS Code integrated terminal)

## Technical Details

### Core Content: Agent Teams Mental Model

```text
Solo Claude:
  You ←→ Claude (1 session, 1 context)

Agent Teams (in-process mode — DEFAULT):
  You ←→ Lead ←→ Teammate 1   ← all run in your terminal
                ←→ Teammate 2   ← navigate with Shift+Up/Down
                ←→ Teammate 3

Agent Teams + Worktrees (COMPLEMENTARY — covered in Week 3):
  You ←→ Lead ←→ Teammate 1 (worktree: repo-DX-501/)
                ←→ Teammate 2 (worktree: repo-DX-502/)
                ←→ Teammate 3 (worktree: repo-DX-503/)
```

**Important distinction:** Agent Teams = coordination (shared task list, messaging, lead orchestration). Worktrees = file isolation (each teammate gets own repo checkout, own branch). They are complementary but different concepts. The "workspace" mental model applies when combining Agent Teams WITH worktrees for file isolation, introduced in Week 3.

Key teaching points:

- Agent Teams is EXPERIMENTAL — disabled by default, enabled via environment variable in settings.json
- Teams are created via natural language — tell Claude what team you want (e.g., "Create an agent team with 3 teammates...")
- Lead coordinates; teammates execute
- Shared task list with pending/in_progress/completed states and dependencies is the communication channel
- Mailbox messaging: message one teammate or broadcast to all
- Delegate mode (Shift+Tab) restricts lead to coordination only
- Plan approval mode: teammates must plan before implementing, lead approves
- Display modes: in-process (DEFAULT, any terminal, Shift+Up/Down navigation) vs split-pane (tmux/iTerm2 only)
- Cost scales linearly with teammates (budget awareness)
- Known limitations: no session resumption with in-process teammates, task status can lag, one team per session, no nested teams

### Hands-On Exercise 1: 3-Teammate Code Review

Provide a codebase with 3 modules that need review. Each teammate reviews one module:

```text
Teammate 1 → Review PaymentService (financial logic)
Teammate 2 → Review ResidentPortal (API endpoints)
Teammate 3 → Review NotificationService (email/SMS)
```

Students observe how teams parallelize work and consolidate findings.

### Hands-On Exercise 2: Spec → Team Execution

Take Week 1's spec and execute it with a 2-teammate team:

- Teammate 1: Backend implementation
- Teammate 2: Test suite

Students observe coordination between teammates working on related code.

### Track Exercises

| Track | Exercise | Deliverable |
| - | -------- | ----------- |
| Developer | Coordinate 3-agent team for feature implementation | Working feature with 3 parallel contributions |
| Architect | Design Agent Teams guidelines for engineering org | Guidelines doc with patterns and anti-patterns |
| PM | Requirements gathering with competing hypotheses | Requirements doc refined by 3 viewpoints |
| QA | Parallel test review across 3 modules | Consolidated test review report |

## Testing

1. README renders correctly with all sections
2. Hands-on exercises are feasible with current Agent Teams capabilities
3. Track exercises are appropriately scoped (15-20 min each)
4. Cost estimates are documented for exercises
5. Links to Week 1 spec exercise work correctly
