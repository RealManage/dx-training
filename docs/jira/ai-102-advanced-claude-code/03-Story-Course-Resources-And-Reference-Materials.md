# Story: Course Resources And Reference Materials

## Priority / Epic

HIGH - AI 102 - Advanced Claude Code

## Effort Estimate

3 story points

## Prerequisites / Dependencies

- Story 01 (Scaffold Course Directory Structure)

## Description

Create reference materials in the `resources/` directory that students use throughout the course. Each resource is a standalone document covering a specific topic area.

### Resources

| File | Purpose | Used In |
| - | ------- | ------- |
| `spec-template.md` | Machine-readable spec template for Claude | Week 1 |
| `agent-teams-quickref.md` | Agent Teams enablement, natural language team creation, display modes, keyboard shortcuts, and patterns quick reference | Week 2 |
| `worktree-cheatsheet.md` | Git worktree commands and workflow cheatsheet | Week 3 |
| `trust-decision-tree.md` | Decision tree for trust level calibration | Week 4 |
| `orchestrator-glossary.md` | Key terms and definitions for the course | All weeks |
| `cost-optimization-guide.md` | Token usage, model selection, budget strategies | Week 4 |
| `troubleshooting-102.md` | Common issues and solutions for Agent Teams, worktrees, and orchestration workflows | All weeks |
| `agent-teams-patterns.md` | When to use 2 vs 3+ teammates, scoping teammate work, common failure modes | Week 2, Week 3 |

### Tasks

1. **Spec template** — CLAUDE.md-as-contract format, acceptance criteria template, spec validation checklist
2. **Agent Teams quick ref** — enable via `CLAUDE_CODE_EXPERIMENTAL_AGENT_TEAMS=1` in settings.json, natural language team creation (no CLI flags), display modes (in-process default vs split-pane), delegate mode (Shift+Tab), plan approval mode, shared task list with states and dependencies, mailbox messaging, keyboard shortcuts (Shift+Up/Down navigation), team config at `~/.claude/teams/{team-name}/config.json`, `--teammate-mode in-process` flag, common patterns. Include experimental status and known limitations.
3. **Worktree cheatsheet** — create, list, remove, branch per worktree, cleanup patterns
4. **Trust decision tree** — visual decision tree for auto-accept, plan mode, step mode, manual review
5. **Glossary** — spec-driven dev, agent teams, orchestration, trust calibration, worktree terms
6. **Cost guide** — Opus vs Sonnet costs, teammate token budget, when to use which model
7. **Troubleshooting guide** — Common issues and solutions for Agent Teams, worktrees, and orchestration workflows
8. **Agent Teams patterns** — When to use 2 vs 3+ teammates, scoping teammate work, common failure modes

## Acceptance Criteria

- [ ] All 8 resource files created in `resources/`
- [ ] Spec template is actionable and follows CLAUDE.md conventions
- [ ] Agent Teams quick ref covers enablement (environment variable), natural language team creation, display modes, keyboard shortcuts, delegate/plan approval modes, shared task list, mailbox messaging, experimental limitations, and Windows compatibility
- [ ] Worktree cheatsheet covers full lifecycle (create → cleanup)
- [ ] Trust decision tree provides clear visual guidance
- [ ] Glossary covers all key terms introduced in the course
- [ ] Cost guide includes model comparison and budget strategies
- [ ] Troubleshooting guide covers Agent Teams, worktrees, and orchestration issues
- [ ] Agent Teams patterns guide covers team sizing and failure modes
- [ ] Each resource is self-contained and can be referenced independently

## Technical Details

### Spec Template Structure

```markdown
# Feature: {Feature Name}

## Context
{Why this feature exists, business value}

## Acceptance Criteria
- [ ] {Criterion 1}
- [ ] {Criterion 2}

## Technical Constraints
{Tech stack, patterns, conventions}

## Test Requirements
{Coverage targets, test types}

## Out of Scope
{What this spec does NOT cover}
```

### Trust Decision Tree

```text
Is this code well-tested?
├─ Yes → Is it greenfield or stable?
│   ├─ Greenfield → Auto-accept with spec
│   └─ Stable → Plan mode, review changes
└─ No → Is it legacy or critical?
    ├─ Legacy → Step mode, verify each change
    └─ Critical (payments, auth) → Manual review always
```

## Testing

1. Verify all 8 files exist in resources/
2. Each file is valid markdown
3. Spec template is usable for a real feature
4. Decision tree is unambiguous
5. Glossary terms align with course content
