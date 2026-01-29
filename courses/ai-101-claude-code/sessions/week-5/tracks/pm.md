# Week 5: Commands & Basic Skills - PM Track

## PM Skill Creation Workshop

**Time:** 1.5 hours

This week, PMs learn to create their own skills to automate common PM workflows. No coding required - skills are just structured prompts in markdown files.

---

## Learning Objectives

By the end of this session, you'll be able to:

- [ ] Create skills to automate PM workflows
- [ ] Use skills with the Claude CLI
- [ ] Build a personal PM automation toolkit

---

## Why PMs Should Create Skills

Skills aren't just for developers. PMs can create skills that:

| Skill | What It Automates | Time Saved |
| ----- | ----------------- | ---------- |
| `/release-notes` | Git log → stakeholder summary | 30 min/release |
| `/meeting-actions` | Meeting notes → Jira-ready tasks | 15 min/meeting |
| `/sprint-summary` | Sprint data → exec report | 45 min/sprint |
| `/user-stories` | Epic → broken-down stories | 20 min/epic |

**The key:** Skills capture your business knowledge and standardize output.

---

## Part 1: Your First PM Skill (30 min)

### 1.1 Understanding Skill Structure

Skills live in `.claude/skills/` and are just markdown files with YAML frontmatter:

```
your-project/
├── .claude/
│   └── skills/
│       └── release-notes/
│           └── SKILL.md    ← Your skill definition
```

### 1.2 Create the Release Notes Skill

Create `.claude/skills/release-notes/SKILL.md`:

```markdown
---
name: release-notes
description: Generate stakeholder-friendly release notes from git history
argument-hint: [version] [start_date]
---

Generate release notes for version $1 starting from $2.

## Input Required
I'll need the git log output. Run this command and paste the results:
\`\`\`bash
git log --oneline --since="$2" --until="today"
\`\`\`

## Output Format

### Release Notes: v$1

**Release Date:** [Today's date]

#### 🚀 New Features
- [Feature descriptions in business language]

#### 🐛 Bug Fixes
- [Fix descriptions, no technical jargon]

#### ⚠️ Breaking Changes
- [Only if applicable]

#### 📋 What's Next
- [Upcoming features if known]

## Style Guide
- Write for stakeholders, not developers
- Focus on business value, not technical details
- Use clear, jargon-free language
- Group related changes together
```

### 1.3 Test Your Skill

```bash
cd your-project
claude

# In Claude Code
/release-notes 2.1.0 2026-01-01
```

**Example Output:**

```markdown
### Release Notes: v2.1.0

**Release Date:** January 27, 2026

#### 🚀 New Features
- Added violation auto-escalation after 30 days of inactivity
- New late fee compound interest calculator for accurate billing

#### 🐛 Bug Fixes
- Fixed payment date calculation that caused off-by-one errors
- Resolved duplicate notification issue for board members

#### ⚠️ Breaking Changes
- None

#### 📋 What's Next
- Resident self-service portal (v2.2.0)
- Mobile app notifications (v2.3.0)
```

This is what your stakeholders will see - clean, jargon-free, business-focused!

---

## Part 2: Meeting Actions Skill (30 min)

### 2.1 Create the Meeting Actions Skill

Create `.claude/skills/meeting-actions/SKILL.md`:

```markdown
---
name: meeting-actions
description: Extract action items from meeting notes and format for Jira
argument-hint: [meeting_type]
---

Extract action items from the meeting notes I'll provide.

## Meeting Type: $1

## What I Need
Paste your meeting notes below (raw is fine - bullet points, sentences, whatever format you have).

## Output Format

### Action Items from $1

| # | Action | Owner | Due Date | Priority |
| --- | ------ | ----- | -------- | -------- |
| 1 | [Specific, actionable task] | [Name] | [Date] | [High/Medium/Low] |

### Jira-Ready Format
For each action item, provide copy-paste ready text:

**Title:** [Short, clear title]
**Description:**
- Context: [Why this matters]
- Acceptance Criteria: [How to know it's done]
- Related to: [Meeting name/date]

### Decisions Made
- [List any decisions captured in the meeting]

### Follow-ups Needed
- [Items that need clarification or future discussion]

## Rules
- If no owner is mentioned, flag as "TBD - needs assignment"
- If no due date is mentioned, suggest one based on urgency
- Convert vague items into specific, actionable tasks
```

### 2.2 Test It

```bash
claude

# In Claude Code
/meeting-actions "Sprint Planning"
# Then paste your meeting notes
```

---

## Part 3: Sprint Summary Skill (20 min)

### 3.1 Create the Sprint Summary Skill

Create `.claude/skills/sprint-summary/SKILL.md`:

```markdown
---
name: sprint-summary
description: Generate executive sprint summary from sprint data
argument-hint: [sprint_number]
---

Generate an executive summary for Sprint $1.

## Data I'll Need
Provide the following (paste from Jira export or type manually):

1. **Completed items:** [list]
2. **Carried over items:** [list]
3. **Blockers encountered:** [list]
4. **Team capacity:** [points planned vs delivered]

## Output Format

### Sprint $1 Summary

**Sprint Goal:** [Inferred from completed work]
**Delivery:** [X of Y points] ([percentage]%)

#### ✅ Completed
- [Business-value focused descriptions]

#### ➡️ Carried Over
- [Item] - Reason: [why it didn't complete]

#### 🚧 Blockers & Risks
- [Blocker] - Impact: [what it affected] - Status: [resolved/ongoing]

#### 📊 Health Indicators
| Metric | This Sprint | Trend |
| ------ | ----------- | ----- |
| Velocity | X points | ↑/↓/→ |
| Carryover | X% | ↑/↓/→ |
| Bug Ratio | X% | ↑/↓/→ |

#### 📅 Next Sprint Focus
- [Recommendations based on this sprint's learnings]

## Tone
- Executive-friendly (no technical jargon)
- Honest about challenges
- Forward-looking recommendations
```

---

## Part 4: User Story Breakdown Skill (10 min)

### 4.1 Create the Skill

Create `.claude/skills/user-stories/SKILL.md`:

```markdown
---
name: user-stories
description: Break down an epic into well-formed user stories
argument-hint: [epic_name]
---

Break down the epic "$1" into user stories.

## Epic Details
Provide the epic description, and I'll generate stories.

## Output Format

### Epic: $1

#### Story 1: [Short title]
**As a** [user type]
**I want to** [action]
**So that** [benefit]

**Acceptance Criteria:**
- [ ] [Specific, testable criterion]
- [ ] [Another criterion]

**Story Points:** [Estimate with rationale]
**Dependencies:** [Other stories this depends on]

---

[Repeat for each story]

### Story Map

\`\`\`
Epic: $1
├── Story 1 (X pts) - [status]
├── Story 2 (X pts) - [depends on Story 1]
└── Story 3 (X pts) - [independent]
\`\`\`

### MVP Recommendation
Stories for MVP: [list]
Stories for Phase 2: [list]
Rationale: [why this split]

## Guidelines
- Stories should be independently deliverable
- Each story should have clear acceptance criteria
- Estimate based on complexity, not time
- Flag any stories that seem too large (>8 points)
```

---

## Exercise: Build Your PM Toolkit

**Time:** 15 min

1. Create all four skills in your sandbox project
2. Test each skill with real or sample data
3. Customize the output formats to match your team's templates

**Bonus:** Think of one more PM workflow you'd like to automate and draft the skill.

---

## Homework

1. **Use your skills this week** - Try `/release-notes` or `/meeting-actions` on real work
2. **Refine based on usage** - Edit the SKILL.md files to improve output
3. **Share with your team** - Skills in `.claude/skills/` work for anyone with the project

---

## What's Next

**Week 6: Agents & Hooks** - Learn about:

- Custom AI agents for specialized tasks
- Hooks for automated workflows
- Compliance and audit logging patterns

**Week 8: Real-World Automation** - Use your skills with:

- Headless CLI automation (`claude -p`)
- Batch processing scripts
- Scheduled automation

---

*Return to [main README](../README.md)*
