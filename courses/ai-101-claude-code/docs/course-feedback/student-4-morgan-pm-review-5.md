# Student Feedback: Morgan (Product Manager) - Review #5

**Reviewer:** Morgan Chen
**Role:** Product Manager (3 years experience, former developer)
**Company:** RealManage
**Date:** January 27, 2026
**Review Type:** Fifth review - Focus on Week 5 PM Skill Workshop and Updated PM Track

---

## Executive Summary

This review focuses on the **major overhaul of Week 5 for PMs** and the updated PM track guidance. This is the most significant change to the PM experience since the course introduced dedicated track files.

**The verdict: This is a game-changer.**

Week 5 went from "Skip this week" to "This is your power week." PMs now have their own skill creation workshop with four practical skills we can build and use immediately. Combined with the Week 8 updates connecting these skills to headless CLI automation, the PM track now offers a complete automation journey from creation to execution.

### Rating Progression

| Review | Rating | Key Finding |
| ------ | ------ | ----------- |
| Review #1 | 8/10 | Good content but too technical for PMs |
| Review #2 | 4/5 stars | Dramatic improvement with PM track |
| Review #3 | 4.5/5 stars | PM track excellent and practical |
| Review #4 | 4.5/5 stars (9.0/10) | Week 8 headless refactor improved accessibility |
| **Review #5** | **4.8/5 stars (9.6/10)** | Week 5 skill workshop transforms PM engagement |

**Rating increase justified:** The PM track now has a complete automation journey. We're not just consumers anymore - we're creators. This fundamentally changes the PM value proposition of the course.

---

## Week 5 PM Skill Workshop: Detailed Review

### What Changed

**Before (Review #1-4):**

- Week 5 PM track said: "This week is optional for PMs"
- Provided a 5-minute conceptual summary
- Suggested observing developers or skipping entirely
- PMs were positioned as **consumers** of skills, not creators

**After (Current):**

- Week 5 PM track is now a **1.5-hour hands-on workshop**
- PMs create four complete skills from scratch
- No coding required - skills are just markdown files
- Clear instructions with copy-paste examples
- PMs are now **creators** of automation

### The Four PM Skills

| Skill | What It Does | Time to Create | Immediate Value |
| ----- | ------------ | -------------- | --------------- |
| `/release-notes` | Git history to stakeholder summary | 15 min | High - I do this every release |
| `/meeting-actions` | Meeting notes to Jira-ready tasks | 15 min | High - I do this after every meeting |
| `/sprint-summary` | Sprint data to exec report | 10 min | High - I do this every two weeks |
| `/user-stories` | Epic to broken-down stories | 10 min | Medium - Useful for backlog grooming |

### Skill-by-Skill Analysis

#### `/release-notes` Skill

**Structure:**

```markdown
---
name: release-notes
description: Generate stakeholder-friendly release notes from git history
argument-hint: [version] [start_date]
---
```

**What I Like:**

- Explicitly targets "stakeholder-friendly" output (not developer jargon)
- Includes emoji categorization for visual scanning
- Has a "What's Next" section for forward-looking context
- Style guide emphasizes business value over technical details

**What I Tested:**
I created this skill in my sandbox and ran it against our actual repo:

```bash
/release-notes 2.5.0 2026-01-01
```

**Result:** Generated clean, categorized release notes. The "write for stakeholders, not developers" instruction produced exactly the tone I need for board updates.

**Time saved (tested):** 30+ minutes per release. This alone justifies the workshop time.

#### `/meeting-actions` Skill

**What I Like:**

- Jira-ready output format with assignee, due date, priority
- Handles "TBD - needs assignment" for unassigned items
- Suggests due dates when none mentioned (smart!)
- Captures decisions and follow-ups separately

**What I Tested:**
Pasted actual meeting notes from a sprint planning session:

```bash
/meeting-actions "Sprint Planning"
```

**Result:** Extracted 7 action items from my messy notes, formatted perfectly for copy-paste into Jira. Caught two implicit action items I had missed.

**Time saved (tested):** 15 minutes per meeting. With 3+ meetings/week, that's significant.

#### `/sprint-summary` Skill

**What I Like:**

- Executive-friendly output with health indicators
- Trend arrows (up/down/sideways) for quick scanning
- "Next Sprint Focus" recommendations based on learnings
- Explicit "honest about challenges" tone guidance

**Practical Note:** This skill requires data input (completed items, carryover, blockers). In practice, I'd export from Jira and paste. The skill handles the analysis and formatting.

#### `/user-stories` Skill

**What I Like:**

- Story map visualization in ASCII format
- MVP vs Phase 2 recommendations with rationale
- Dependency identification between stories
- Flags stories over 8 points as too large

**What I Tested:**

```bash
/user-stories "Resident Self-Service Portal"
```

**Result:** Generated 6 well-formed stories with proper INVEST characteristics. The MVP recommendation was sensible and matched my intuition.

### Workshop Structure Assessment

| Aspect | Rating | Notes |
| ------ | ------ | ----- |
| Time estimate (1.5 hrs) | Accurate | I finished in about 1.5 hours including testing |
| Difficulty level | Appropriate | No coding, just markdown files |
| Practical value | Excellent | All four skills are things I actually do |
| Instructions clarity | Excellent | Copy-paste ready with clear file paths |
| Testing guidance | Good | Each skill has "Test it" section |

### What Could Be Better

1. **No troubleshooting section** - If a skill doesn't work, where do PMs look?
2. **No versioning guidance** - How do I update a skill over time?
3. **No sharing guidance** - How do I share my skills with other PMs on my team?

**Impact:** Minor - these are nice-to-haves, not blockers.

---

## Quick-Start PM Guide Updates

### What Changed

The `quick-start-pm.md` file has been significantly updated:

**Before:**

| Week | PM Priority |
| ---- | ----------- |
| 5 | Skim |
| 8 | Skim |

**After:**

| Week | PM Priority |
| ---- | ----------- |
| 5 | Must Do |
| 8 | Must Do |

This is a major shift! The PM track now includes two additional "Must Do" weeks, both focused on practical automation.

### Updated Learning Path Visualization

The mermaid diagram in quick-start-pm.md still shows Weeks 4-8 in a single "Skip (concepts only)" block. This is now **outdated** given that Weeks 5 and 8 are "Must Do."

**Recommendation:** Update the diagram to show:

- Weeks 4, 6, 7: Skip/Skim (gray)
- Week 5: Must Do (green)
- Week 8: Must Do (green)

### What's Excellent

1. **Week 5 description is clear:**
   > "Create your own PM automation skills. No coding required - Skills are just structured markdown files."

2. **Checkpoint added:**
   > "Can you run `/release-notes` on a project and get stakeholder-friendly output?"

3. **Week 8 description connects the dots:**
   > "Use your Week 5 skills with headless automation."

---

## Week 8 PM Track: Skills + Headless CLI Connection

### What's New

Week 8 PM track now explicitly connects the Week 5 skills to headless automation:

```bash
# Use your release-notes skill from Week 5
cd your-project
claude -p "/release-notes 2.1.0 2024-01-01" --no-session-persistence > release-notes.md

# Use your meeting-actions skill
claude -p "/meeting-actions 'Sprint Retro'" --no-session-persistence

# Use your sprint-summary skill
claude -p "/sprint-summary 14" --no-session-persistence > sprint-14-summary.md
```

**This is the missing link!** In Review #4, I noted the Week 8 PM track had good content but felt disconnected from earlier weeks. Now there's a clear narrative:

1. Week 5: Create your skills
2. Week 8: Run your skills in automation
3. Week 9: Include your skills in capstone

### Time Savings Table (Updated)

The PM track now includes realistic time savings:

| Task | Manual | Automated | Weekly Savings |
| ---- | ------ | --------- | -------------- |
| Status reports | 30 min | 5 min | 25 min |
| Sprint summaries | 45 min | 10 min | 35 min |
| Release notes | 60 min | 10 min | 50 min |
| Meeting actions | 20 min | 5 min | 15 min |
| **Monthly savings** | | | **~8 hours** |

I validated these numbers - they're accurate for my workflow.

---

## Week 9 PM Capstone: Skill Requirement

### What's New

The PM capstone (Option E) now **requires** a custom PM skill:

> **Required: Bring Your Week 5 Skills**
>
> Your capstone must include at least one custom PM skill from Week 5:
>
> - `/release-notes` - Generate stakeholder-friendly release notes
> - `/meeting-actions` - Extract action items from meeting notes
> - `/sprint-summary` - Create executive sprint summaries
> - `/user-stories` - Break epics into well-formed stories
>
> **Show how you used these skills in your capstone work.**

### Updated Deliverables

The capstone deliverables now explicitly include:

1. **PRD** (`docs/PRD.md`)
2. **User Story Map** (`docs/user-stories.md`)
3. **Stakeholder Docs** (`docs/stakeholder/`)
4. **Custom PM Skill** (`.claude/skills/<skill-name>/SKILL.md`) - **NEW!**
5. **Process Documentation** (`docs/process.md`)
6. **Presentation Outline** (`docs/presentation-outline.md`)

### Is the Requirement Clear?

**Yes, much clearer than before.** In Review #4, I flagged that the skill requirement "assumes knowledge from Weeks 5-7." Now:

1. Week 5 PM track teaches skill creation step-by-step
2. Week 8 PM track shows how to use skills with CLI
3. Week 9 PM track specifies exactly what's required

The `/generate-user-stories` skill example in Week 9 even includes the full SKILL.md content, so PMs have a working template.

---

## Issues from Review #4: Status Update

| Issue from Review #4 | Status | Evidence |
| -------------------- | ------ | -------- |
| Week 9 skill creation needs more hand-holding | **FIXED** | Week 5 now teaches skill creation in detail |
| "Quick Win" section in Week 8 PM track | **NOT ADDRESSED** | Still missing |
| Include CLI output examples in PM track | **PARTIALLY FIXED** | Week 8 has output examples, Week 5 does not |
| PM learning path visualization | **OUTDATED** | Diagram doesn't reflect new Must Do weeks |

### New Issues Found

| Issue | Severity | Description |
| ----- | -------- | ----------- |
| Learning path diagram outdated | Medium | Shows Weeks 4-8 as skip, but 5 and 8 are now Must Do |
| No skill troubleshooting guide | Low | What if a skill doesn't recognize? |
| No skill sharing guidance | Low | How do PMs share skills with teammates? |

---

## What This Changes for PMs

### Before Week 5 Rewrite (Reviews #1-4)

PMs in this course:

- Learned about AI capabilities
- Understood prompting and planning
- Observed developers using skills
- Consumed automation others built
- Produced PM artifacts (PRD, stories)

**Value proposition:** "Understand AI-assisted development to collaborate better with engineers."

### After Week 5 Rewrite (Review #5)

PMs in this course:

- Learn about AI capabilities
- Master prompting and planning
- **Create their own automation skills**
- **Run their skills with headless CLI**
- Produce PM artifacts using their skills
- **Demonstrate automation in capstone**

**Value proposition:** "Build your own PM automation toolkit and become more productive."

### The Shift

This is a fundamental shift from **understanding** to **doing**. PMs are no longer spectators in the automation journey - we're participants.

---

## Practical Value Assessment

### Skills I Can Use Tomorrow

| Skill | My Use Case | Frequency |
| ----- | ----------- | --------- |
| `/release-notes` | Board updates, customer comms | Every 2 weeks |
| `/meeting-actions` | Sprint planning, stakeholder meetings | 3-4x/week |
| `/sprint-summary` | Sprint reviews, exec updates | Every 2 weeks |
| `/user-stories` | Backlog grooming, epic breakdown | 1-2x/week |

### Time Investment vs. Return

**Investment:**

- Week 5 PM Workshop: 1.5 hours
- Week 8 PM Track: 45 minutes
- Week 9 Capstone (additional for skills): 30 minutes

**Total new investment:** ~3 hours

**Return (monthly savings):** ~8 hours

**ROI:** 2.7x in first month, pure savings every month after

### Comparison to Previous Reviews

| Review | PM Time Investment | PM Value Delivered |
| ------ | ------------------ | ------------------ |
| Review #1 | ~12 hours (full course) | Medium - understanding only |
| Review #2 | ~8 hours (PM track) | Good - prompting skills |
| Review #3 | ~8 hours | Good - clear path to capstone |
| Review #4 | ~8 hours | Good - headless understanding |
| **Review #5** | **~11 hours** (+3 for new weeks) | **Excellent - automation toolkit** |

The additional 3 hours investment delivers significantly more value.

---

## Recommendations

### High Priority

1. **Update the learning path diagram in quick-start-pm.md**
   - Show Week 5 and Week 8 as green "Must Do" boxes
   - Current diagram is misleading
   - Impact: Critical for PM navigation

### Medium Priority

1. **Add CLI output examples to Week 5 PM track**
   - Show what `/release-notes` output looks like
   - Sets expectations, builds confidence
   - Impact: Better learning experience

2. **Add skill troubleshooting section**
   - What if skill isn't recognized?
   - Common YAML frontmatter errors
   - Impact: Self-service problem solving

3. **Add "Quick Win" section to Week 8 PM track**
   - 5-minute exercise using their Week 5 skill
   - Example: "Run this command right now on your project"
   - Impact: Immediate value demonstration

### Nice to Have

1. **Skill sharing guide for teams**
   - How to share `.claude/skills/` with other PMs
   - Git-based workflow for team skills
   - Impact: Team-wide productivity gains

2. **PM case study or testimonial**
   - "Here's how Morgan automated release notes"
   - Impact: Social proof, inspiration

---

## Final Assessment

### Can a PM Complete the Updated Week 5?

**Absolutely yes.** The workshop is:

- Structured with clear time allocations (Part 1: 30 min, Part 2: 30 min, etc.)
- Copy-paste ready with no coding required
- Focused on PM tasks, not developer tasks
- Testable with real or sample data

### Is the Week 5 Rewrite a Game-Changer?

**Yes, significantly.** The shift from "observer" to "creator" fundamentally changes the PM value proposition. I went from:

- "I understand skills exist" to "I can build skills"
- "Automation is for developers" to "I automate my own work"
- "The capstone is documentation" to "The capstone includes my skills"

### Does Week 8 Connect the Dots?

**Yes.** The explicit connection between Week 5 skills and Week 8 headless automation creates a complete narrative:

1. Create skills (Week 5)
2. Use skills with CLI (Week 8)
3. Demonstrate skills in capstone (Week 9)

### Is the PM Track Now Complete?

**Almost.** The learning path diagram needs updating, and some minor additions (troubleshooting, output examples) would polish it. But the core content is excellent.

---

## Overall Rating: 4.8/5 Stars (9.6/10)

**What would make it 5 stars:**

- Updated learning path diagram (currently misleading)
- CLI output examples in Week 5 PM track
- Skill troubleshooting guide

**What's excellent:**

- Week 5 PM Skill Workshop (transformative)
- Four practical, immediately usable skills
- Clear connection between Week 5, 8, and 9
- Week 9 skill requirement with clear guidance
- Time savings that are realistic and validated
- No coding required throughout PM track

### Rating Justification

| Criteria | Score | Notes |
| -------- | ----- | ----- |
| Content quality | 10/10 | Week 5 workshop is exceptional |
| Practical value | 10/10 | Four skills I'll use immediately |
| Accessibility | 9/10 | Diagram needs update |
| Clear navigation | 9/10 | Must Do weeks not reflected in visual |
| Time estimates | 10/10 | Accurate across all weeks |
| PM relevance | 10/10 | All examples are PM work |

**Overall: 9.6/10**

---

## Summary for Course Team

The Week 5 PM Skill Workshop is the most significant improvement to the PM track since the course began. Here's why:

1. **PMs are now creators, not just consumers** - Building skills changes the entire value proposition
2. **The skills are genuinely useful** - I will use all four in my daily work
3. **The narrative is complete** - Create (Week 5) -> Use (Week 8) -> Demonstrate (Week 9)
4. **No coding barrier** - Skills are just markdown, exactly as described
5. **Time investment is justified** - 3 additional hours for 8+ hours monthly savings

### One Critical Update Needed

The learning path diagram in `quick-start-pm.md` still shows Weeks 4-8 as "Skip (concepts only)." This needs to be updated immediately to show Weeks 5 and 8 as "Must Do." PMs will see the diagram and skip Week 5, missing the most valuable new content.

---

*This course has evolved from a developer training with PM accommodation to a genuinely cross-functional learning experience with PM-specific value creation. The Week 5 rewrite proves that Claude Code isn't just about writing code - it's about automating work for everyone on the team. As a PM, I now have my own automation toolkit, and that's transformative.*

**Morgan Chen**
Product Manager, RealManage
*3 years experience, former developer, now an AI-enabled PM with my own skill library*
*January 27, 2026*
