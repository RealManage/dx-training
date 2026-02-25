# Week 8: Real-World Automation - PM Track

**Track:** Product Manager
**Duration:** 45 minutes (exercises portion of shared session)
**Prerequisites:** Weeks 1-7 completed (especially Week 5 skills)

---

This track focuses on what automation actually looks like for PMs — interactive skill workflows, designing automation specifications, collaborating with engineers on headless implementations, and measuring ROI.

> **Key insight from the shared session:** Automation for PMs is about **interactive skills** and **workflow design**, not bash scripting. Engineers write the headless scripts; PMs define *what* gets automated and *how the output should look*.

## Learning Objectives

By the end of this track, you will be able to:

- Use interactive skills for real PM scenarios (release notes, sprint summaries, meeting actions)
- Design automation workflow specifications that engineers can implement
- Apply the handoff pattern: PM designs the skill, engineer wraps it in a script
- Measure and communicate automation ROI to stakeholders

## Exercise 1: Interactive Skill Mastery (15 min)

These skills are the PM's automation toolkit. You built skills in Week 5 — now apply them to real scenarios.

### Release Notes from Git History

Use the `/release-notes` skill interactively:

```text
/release-notes v2.3.0 v2.4.0
```

**If you haven't created this skill yet**, ask Claude to build it:

```text
Create a skill at .claude/skills/release-notes/SKILL.md that:
1. Takes two git tags as arguments
2. Runs git log between the tags
3. Groups commits by type (Features, Bug Fixes, Improvements)
4. Extracts DX-### ticket references
5. Formats for stakeholder communication — concise, no jargon
```

**Practice scenario:** Your team just shipped v2.4.0. Generate release notes and edit the output for a stakeholder email.

- Is the language appropriate for non-technical readers?
- Are the ticket references correct?
- Would you add or remove anything?

### Sprint Summary for Standup

```text
Create a skill at .claude/skills/sprint-summary/SKILL.md that:
1. Takes sprint_number as argument
2. Summarizes completed work from recent git history
3. Lists in-progress items
4. Identifies blockers and risks
5. Formats as a 2-minute standup update
```

**Practice scenario:** Run the skill for your current sprint. Does the output capture what you'd actually say in standup?

### Meeting Action Item Extraction

```text
Create a skill at .claude/skills/meeting-actions/SKILL.md that:
1. Takes meeting topic as argument
2. Reads meeting notes you provide in the conversation
3. Extracts action items with owners and due dates
4. Formats as: - [ ] [OWNER] - [TASK] - [DUE DATE]
5. Groups by assignee
```

**Practice scenario:** Paste in notes from a recent meeting (or use sample notes below):

```text
Sample meeting notes:
Sprint planning for Sprint 15. Sarah will finish the violation
workflow by Thursday. Mike needs to review the payment service
architecture before we can start the billing feature — target
Monday. PM should update stakeholders on the timeline change
by end of week. QA team will start regression testing on the
new letter generation once Sarah's PR is merged.
```

Run your skill and verify the action items are correctly extracted.

## Exercise 2: Workflow Design Patterns (15 min)

PMs don't need to write scripts — but they need to know how to **design automation workflows** that engineers can implement.

### Map a Manual Process

Pick a recurring task from your actual work (or use the example below) and map it:

**Example: Monthly Board Report Generation**

```text
CURRENT MANUAL PROCESS:
1. PM collects data from Jira (30 min)
2. PM writes summary in Google Docs (45 min)
3. PM formats violations table manually (20 min)
4. PM sends to property managers for review (5 min)
5. Property managers send corrections (1-2 days wait)
6. PM incorporates feedback and finalizes (30 min)

Total: ~2.5 hours + wait time
```

**Now identify automation opportunities:**

```text
AUTOMATION OPPORTUNITIES:
Step 1: Skill can pull from git/Jira → "Collect sprint data"
Step 2: Skill can draft summary from data → "Generate board summary"
Step 3: Skill can format violations from database → "Format violations table"
Step 4: Manual (human judgment needed)
Step 5: Manual (external dependency)
Step 6: Skill can apply feedback to draft → "Incorporate review feedback"

Automatable: Steps 1, 2, 3, 6
Manual: Steps 4, 5
Estimated savings: ~1.5 hours per report
```

### Write the Automation Spec

Now write a spec that an engineer could implement. Use this template:

```markdown
## Automation Spec: [Workflow Name]

### Goal
[What this automation achieves in one sentence]

### Trigger
[When does this run? Manual, scheduled, or event-driven?]

### Input
[What data does it need? Where does it come from?]

### Processing Steps
1. [Step with expected behavior]
2. [Step with expected behavior]
3. ...

### Output Format
[Exactly what the output should look like — be specific]

### Success Criteria
- [How do you know it worked?]
- [Quality bar for the output]

### Notes for Engineer
- [Constraints, edge cases, preferences]
- [Which parts need --max-turns or --max-budget-usd limits?]
```

**Your exercise:** Fill out this template for the board report workflow (or your own recurring task).

## Exercise 3: The Handoff Pattern (10 min)

This is the collaboration model between PMs and engineers for automation:

```text
PM DESIGNS                          ENGINEER IMPLEMENTS
-----------                         -------------------
1. Define the skill logic      -->  4. Wrap skill logic in -p script
2. Specify output format       -->  5. Add error handling, retries
3. Test interactively          -->  6. Add scheduling/triggers
   (iterate until good)             7. Add --max-turns, --max-budget-usd
```

### Practice the Handoff

**Step 1:** Design an interactive skill for generating a stakeholder update:

```text
Create a skill at .claude/skills/stakeholder-update/SKILL.md that:
1. Takes sprint_number and audience (board/exec/team) as arguments
2. Adjusts tone and detail level based on audience
3. Includes: progress summary, risks, upcoming milestones
4. Board: formal, metric-heavy
5. Exec: concise, decision-focused
6. Team: casual, action-oriented
```

**Step 2:** Test it interactively — run it a few times with different audiences.

**Step 3:** Write the handoff note for an engineer:

```markdown
## Handoff: Stakeholder Update Automation

### Skill Location
.claude/skills/stakeholder-update/SKILL.md

### What I've Tested
- Works well for board and exec audiences
- Team output sometimes too formal — may need prompt tuning

### Headless Requirements
- Run weekly on Mondays at 8am
- Output to ./reports/stakeholder-update-{date}.md
- Use Sonnet model (Opus not needed for this)
- Max budget: $0.50 per run
- Max turns: 5

### Output Destination
- Board reports: email to property-managers@realmanage.com
- Exec updates: post to #leadership Slack channel
- Team updates: post to #dev-team Slack channel
```

This is the PM's deliverable — the engineer handles the bash script, scheduling, and error handling.

## Exercise 4: Measuring Automation ROI (5 min)

### Time Savings Tracker

Start tracking time savings with this template:

| Task | Before (min) | After (min) | Savings | Frequency | Monthly Savings |
| ---- | ------------ | ----------- | ------- | --------- | --------------- |
| Release notes | 60 | 10 | 50 min | 2x/month | 1.7 hrs |
| Sprint summary | 45 | 10 | 35 min | 2x/month | 1.2 hrs |
| Meeting actions | 20 | 5 | 15 min | 4x/month | 1.0 hrs |
| Status reports | 30 | 5 | 25 min | 4x/month | 1.7 hrs |
| **Total** | | | | | **5.6 hrs** |

### Stakeholder Communication Template

Use this when reporting automation ROI:

```markdown
## Automation Impact Report — [Month]

### Time Savings
- Total hours saved this month: X
- Tasks automated: X
- Team members using automation: X

### Quality Improvements
- Consistency of outputs: [improved/same/declined]
- Stakeholder feedback: [positive/neutral/negative]
- Error rate in automated outputs: X%

### Cost
- Claude Code usage: $X
- ROI: X hours saved per $1 spent

### Recommendations
- [Tasks to automate next]
- [Skills to improve]
- [Workflows to redesign]
```

## Key Takeaways for PMs

1. **You don't need bash** — Interactive skills ARE your automation
2. **Design the workflow, not the script** — Engineers implement headless versions
3. **The handoff pattern works** — PM designs + tests interactively, engineer wraps in `-p` script
4. **Measure everything** — Time savings, quality improvements, stakeholder satisfaction
5. **Iterate on skills** — Run them, edit the output, improve the skill definition

## Homework (Before Week 9)

### Required Tasks

1. Create or refine three PM-focused skills (release notes, sprint summary, stakeholder update)
2. Map one manual workflow and write an automation spec
3. Start tracking time savings with the ROI template
4. Share your best workflow design in `#ai-exchange`

### Optional

1. Practice the handoff pattern with an engineer on your team
2. Build a monthly automation impact report
3. Identify three more workflows that could benefit from skill automation

---

*PM Track - Week 8*
*Interactive skill workflows and automation design for product managers*
