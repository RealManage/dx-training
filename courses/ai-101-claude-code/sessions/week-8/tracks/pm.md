# Week 8: Real-World Automation - PM Track

**Track:** Product Manager (Concepts Overview)
**Duration:** 45 minutes
**Prerequisites:** Weeks 1-7 completed (skim is fine)

---

This track provides a conceptual overview of Claude Code automation for product managers. Focus is on understanding how automation can accelerate delivery and improve quality.

## Learning Objectives

By the end of this session, you will understand:

- How Claude Code automation helps teams deliver faster
- What headless automation enables for batch processing
- Key metrics to track for quality and productivity
- How to communicate AI-assisted development to stakeholders
- Planning considerations for automation initiatives

## What is Headless Automation? (10 min)

### The Basics

**Headless Mode:**

- Claude Code runs without interactive prompts
- Can process multiple files or tasks in batch
- Outputs can be saved to files for reports

**Why This Matters:**

- Engineers can automate repetitive analysis
- Batch code reviews before MRs
- Generate reports and documentation automatically
- Process meeting notes into action items

### PM-Relevant Use Cases

| Task | Manual Approach | With Automation |
| ---- | --------------- | --------------- |
| Release notes | Engineer writes manually | Generated from commits |
| Code review | Wait for reviewer | Pre-review catches issues |
| Meeting notes | PM writes summary | Auto-extracted action items |
| Sprint planning | Manual estimation | AI-assisted complexity analysis |

## How Engineers Use Claude Automation (10 min)

### Batch Code Review

Engineers can review multiple files before creating an MR:

- **What it does:** Analyzes code for bugs, security, style
- **PM benefit:** Fewer issues found during formal review
- **Metric impact:** Faster MR turnaround

### Release Notes Generation

Claude can generate release notes from git history:

- **Input:** Commits between version tags
- **Output:** Organized changelog by category
- **PM benefit:** Accurate release communication

### Meeting Notes Processing

- Extract action items from transcripts
- Generate structured summaries
- Create follow-up task lists

## PM Automation Examples (10 min)

### Release Notes from Git History

```bash
# Ask Claude to generate release notes
claude -p "Generate release notes from these commits.
Group by: Features, Bug Fixes, Improvements.
Format for stakeholder communication.

Commits:
$(git log v1.0.0..v1.1.0 --oneline)"
```

**Output:**

```markdown
## Release Notes: v1.1.0

### New Features
- Violation auto-escalation after 30 days (DX-1234)
- Late fee compound interest calculator (DX-1235)

### Bug Fixes
- Fixed payment date calculation (DX-1240)

### Improvements
- Dashboard loading optimized
```

### Meeting Notes → Action Items

```bash
# Process meeting transcript
claude -p "Extract action items from this meeting.
Format: - [ ] [OWNER] - [TASK] - [DUE DATE]

Transcript:
$(cat meeting-notes.txt)"
```

**Output:**

```markdown
## Action Items - Sprint Planning 1/22

- [ ] Sarah - Complete violation workflow design - 1/25
- [ ] Mike - Review payment service architecture - 1/24
- [ ] PM - Update stakeholders on timeline change - 1/23
```

### Sprint Summary Generation

```bash
# Generate sprint summary from project tracker data
claude -p "Summarize this sprint for stakeholder update.
Include: completed work, blockers, next sprint focus.

Completed issues: [list]
In progress: [list]
Blocked: [list]"
```

## Planning for Automation (10 min)

### PM-Relevant Skills

| Skill | What It Does | Time Saved |
| ----- | ------------ | ---------- |
| `/release-notes` | Generate changelog from commits | 30-60 min/release |
| `/sprint-summary` | Summarize sprint for stakeholders | 20-30 min/sprint |
| `/meeting-actions` | Extract action items from notes | 15-20 min/meeting |
| `/status-update` | Draft status report | 15-20 min/report |

### Using Your Week 5 Skills with Headless CLI

Remember the skills you created in Week 5? Now you can use them in automation:

```bash
# Use your release-notes skill from Week 5
cd your-project
claude -p "/release-notes 2.1.0 2024-01-01" --no-session-persistence > release-notes.md

# Use your meeting-actions skill
claude -p "/meeting-actions 'Sprint Retro'" --no-session-persistence
# Then provide the meeting notes

# Use your sprint-summary skill
claude -p "/sprint-summary 14" --no-session-persistence > sprint-14-summary.md
```

**The power of skills + headless CLI:**

- Skills capture your business knowledge and output format
- Headless CLI runs them without interactive prompts
- Output can be saved directly to files
- Can be scheduled or triggered by other tools

### Value Proposition

**Time Savings Example:**

```text
Weekly status reports: 30 min → 5 min
Sprint summaries: 45 min → 10 min
Release notes: 60 min → 10 min
Meeting action items: 20 min → 5 min

Weekly savings: ~2 hours
Monthly savings: ~8 hours

```

### Success Metrics to Track

**Documentation Metrics:**

- Time to produce release notes
- Stakeholder satisfaction with updates
- Accuracy of generated summaries

**Productivity Metrics:**

- Meeting follow-up turnaround
- Status report consistency
- Documentation coverage

## Communicating AI-Assisted Work (5 min)

### Release Communication Template

```markdown
## Release Notes: v2.4.0
**Date:** January 22, 2025

### New Features
- Violation auto-escalation after 30 days (DX-1234)
- Late fee compound interest calculator (DX-1235)

### Improvements
- Dashboard loading 40% faster
- Mobile responsive improvements

### Quality
- Test Coverage: 87%
- Pre-merge AI review: All issues addressed
- Zero critical findings
```

### Status Update Template

```markdown
## Sprint 14 Status

**Completed:** 8 stories (34 points)
**Coverage:** 84% -> 87% (+3%)

**AI-Assisted Quality:**
- 12 MRs pre-reviewed with Claude
- 18 issues caught before formal review
- 4 test files auto-generated

**Highlights:**
- Payment service refactored with full coverage
- New violation workflow fully tested
```

## Key Takeaways for PMs

### What You Need to Know

1. **Automation accelerates delivery** - Batch processing saves time
2. **Pre-review catches issues early** - Before formal code review
3. **AI generates documentation** - Release notes, reports, summaries
4. **Skills standardize workflows** - Repeatable, consistent output

### Questions to Ask in Planning

- "Can we automate the release notes for this?"
- "Should we add a pre-review step before MRs?"
- "What recurring tasks could become skills?"
- "How are we measuring automation ROI?"

### Red Flags to Watch For

- Engineers not using available automation
- Same issues found repeatedly in reviews
- Manual processes that could be automated
- No measurement of automation impact

## Homework (Before Week 9)

### Required Tasks

1. Try generating release notes using the git log command example
2. Identify one PM task that could be automated (sprint summaries, status reports)
3. Draft a release communication using the template above

### Optional

1. Create a meeting notes template for action item extraction
2. Discuss automation opportunities with your team

---

*PM Track - Week 8*
*Conceptual overview for understanding AI-assisted automation*
