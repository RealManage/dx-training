# AI 101 Course Student Agent Testing Plan

Reusable plan for spawning student agents to test the course from different persona perspectives.

---

## Overview

Spawn 4 background sub-agents (one per worktree) to attempt the full AI 101 course. Each agent adopts a unique student persona and follows their role-specific track.

---

## Student Personas & Tracks

| Worktree | Student | Persona | Years Exp | Track | Perspective |
| -------- | ------- | ------- | --------- | ----- | ----------- |
| dx-training-student-1 | Alex | Junior Dev | 2 | Developer | AI noob, skeptical but open |
| dx-training-student-2 | Jordan | QA Engineer | 4 | QA | Non-developer, worried about AI |
| dx-training-student-3 | Sam | Senior Dev | 8 | Developer | Skeptic, tried Copilot, high standards |
| dx-training-student-4 | Morgan | PM | 3 | PM | Former dev, enthusiastic, roadmap planning |

---

## Worktree Setup

Ensure worktrees exist and are at the correct commit:

```bash
# List current worktrees
git worktree list

# If needed, create worktrees
git worktree add ../dx-training-student-1 HEAD --detach
git worktree add ../dx-training-student-2 HEAD --detach
git worktree add ../dx-training-student-3 HEAD --detach
git worktree add ../dx-training-student-4 HEAD --detach

# Update all worktrees to latest (from repo root)
for i in 1 2 3 4; do
  cd ../dx-training-student-$i
  git fetch origin
  git checkout origin/feature/ai-101-course-completion
done
```

---

## Execution Approach

### Headless Claude CLI Usage

Each agent uses the `claude` CLI in headless mode to actually attempt the coursework.

**CRITICAL: Session ID Handling**

1. The FIRST `claude` command returns a UUID in its output
2. Capture that UUID exactly as returned
3. ALL subsequent commands MUST use `--session-id <exact-uuid>`
4. Do NOT modify, append to, or alter the UUID in any way

```bash
# First command - captures session ID from output
claude "first prompt" --print
# Output includes: Session ID: a1b2c3d4-e5f6-7890-abcd-ef1234567890

# ALL subsequent commands use that exact ID
claude --session-id a1b2c3d4-e5f6-7890-abcd-ef1234567890 "next prompt" --print
claude --session-id a1b2c3d4-e5f6-7890-abcd-ef1234567890 "continue" --print
```

### What Each Agent Does

1. **Navigate to their worktree directory**
2. **Follow their role-specific track** (found in `sessions/week-X/tracks/`)
3. **Actually attempt the coursework exercises** using headless CLI
4. **Track their session ID** for continuity across exercises
5. **Read their previous reviews** to compare improvements/regressions
6. **Write their new review** in the specified format

---

## Role-Specific Tracks by Week

| Week | Developer Track | QA Track | PM Track |
| ---- | --------------- | -------- | -------- |
| 0 | Main README | Main README | Main README |
| 1 | Main README | Main README | Main README |
| 2 | Main README | Main README | Main README |
| 3 | Main README | Main README | Main README |
| 4 | Main README | tracks/qa.md | Optional |
| 5 | tracks/developer.md | tracks/qa.md | tracks/pm.md |
| 6 | tracks/developer.md | tracks/qa.md | tracks/pm.md |
| 7 | Main README | Main README | Main README |
| 8 | tracks/developer.md | tracks/qa.md | tracks/pm.md |
| 9 | tracks/developer.md | tracks/qa.md | tracks/pm.md |

---

## Output Format

### File Naming Convention

```text
courses/ai-101-claude-code/docs/course-feedback/student-{N}-{name}-review-{X}.md
```

Examples:

- `student-1-alex-junior-dev-review-3.md`
- `student-2-jordan-qa-engineer-review-3.md`
- `student-3-sam-senior-dev-skeptic-review-3.md`
- `student-4-morgan-pm-review-3.md`

### Review Structure

```markdown
# RealManage AI 101: Claude Code - Review {X}

**Student:** {Name} ({Role}, {Years} experience)
**Review Date:** {Date}
**Previous Review:** {Date of last review}
**Review Focus:** {What this pass focused on}

---

## Executive Summary

{Brief overview of experience}

**Previous Rating:** X/10
**Updated Rating:** X/10
**Change:** +/- X

---

## Week-by-Week Experience

### Week 0: AI Foundations
{Experience, issues, praise}

### Week 1: Setup & Orientation
{Experience, issues, praise}

... (continue for all weeks)

---

## Comparison to Previous Reviews

### Issues Previously Raised
| Issue | Previous Status | Current Status |
|-------|-----------------|----------------|
| ... | ... | ... |

### Improvements Noticed
- ...

### Regressions Found
- ...

---

## Recommendations

### High Priority
1. ...

### Medium Priority
1. ...

### Nice to Have
1. ...

---

## Session Log

| Week | Session ID | Commands Run | Notes |
| ---- | ---------- | ------------ | ----- |
| ... | ... | ... | ... |
```

---

## CLI Reference

### Key Flags

| Flag | Description |
| ---- | ----------- |
| `--print` / `-p` | Print response and exit (headless mode) |
| `--session-id <uuid>` | Use specific session UUID |
| `--resume [id]` | Resume by session ID |
| `--continue` / `-c` | Continue most recent conversation |
| `--output-format <fmt>` | `text`, `json`, or `stream-json` |
| `--model <model>` | Set model (`sonnet`, `opus`) |

### Common Workflows

```bash
# Start a week's work
cd /path/to/worktree/courses/ai-101-claude-code/sessions/week-1
claude "I'm starting Week 1. Show me what I need to do." --print

# Continue with session ID
claude --session-id <uuid> "Let me try the first exercise" --print

# Check usage
claude --session-id <uuid> "/usage" --print
```

---

## Verification Checklist

After agents complete:

- [ ] All 4 review files exist in `docs/course-feedback/`
- [ ] Each review follows the naming convention
- [ ] Each review maintains the persona voice
- [ ] Each review references previous reviews
- [ ] Each review includes session log
- [ ] Actionable feedback is documented

---

## Spawning Agents

Use the Task tool to spawn 4 background agents:

```markdown
Task tool parameters:
- subagent_type: "general-purpose"
- run_in_background: true
- prompt: [See agent prompt template below]
```

### Agent Prompt Template

```markdown
You are {Student Name}, a {Role} with {X} years of experience at RealManage.

**Your Persona:** {Persona description}

**Your Task:**
1. Work through the AI 101 Claude Code course from your worktree at: `../dx-training-student-{N}` (relative to main repo)
2. Follow your role-specific track ({Track} track)
3. Use headless `claude` CLI to attempt exercises
4. CRITICAL: Capture the session ID from your first claude command and use it for ALL subsequent commands with --session-id <exact-uuid>
5. Read your previous reviews at:
   - courses/ai-101-claude-code/docs/course-feedback/student-{N}-{name}.md
   - courses/ai-101-claude-code/docs/course-feedback/student-{N}-{name}-review-2.md
6. Write your new review to:
   - courses/ai-101-claude-code/docs/course-feedback/student-{N}-{name}-review-3.md

**IMPORTANT:** When referencing files in your review, use repo-relative paths (e.g., `courses/ai-101-claude-code/sessions/week-4/tracks/qa.md`), NOT absolute paths.

**Stay in character throughout. Your perspective matters.**
```

---

*Last Updated: January 2026*
