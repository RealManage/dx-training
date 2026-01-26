# Student Feedback: Morgan (Product Manager) - Review #2

**Reviewer:** Morgan Chen
**Role:** Product Manager (7 years experience)
**Company:** RealManage
**Date:** January 2025
**Review Type:** Follow-up review after course updates based on initial feedback

---

## Executive Summary

I was skeptical when I started this second review. My first experience with this course left me feeling alienated and wondering why a PM would even attempt it. The technical jargon was overwhelming, and I felt like an intruder in a developer's world.

**After reviewing the updated course: This is a dramatic improvement.**

The course team clearly listened to feedback. PMs now have a clear, navigable path through the material, and I can finally see how Claude Code applies to my daily work. This went from "completely inaccessible" to "genuinely useful for product work."

**Overall Rating: 4 out of 5 stars**

---

## What's Improved for PMs

### 1. Clear Role Selector in Main README

**File:** `/home/calilafollett/repos/dx-training/courses/ai-101-claude-code/README.md`

The new role table at the top of the README is exactly what I needed:

```
| Your Role | Quick Start Guide | Focus Weeks | Skip |
|-----------|-------------------|-------------|------|
| Product Manager | PM Track | 0, 1, 2, 3, 9 | 4-8 (concepts only) |
```

**Why this works:**
- I immediately know which weeks matter to me
- I know what to skip entirely
- I have a dedicated quick-start guide

This single table saved me hours of confusion. In my first review, I had no idea which weeks applied to me and tried to follow developer content, which was frustrating.

### 2. Dedicated PM Quick-Start Guide

**File:** `/home/calilafollett/repos/dx-training/courses/ai-101-claude-code/resources/quick-start-pm.md`

This 330-line guide is genuinely excellent. Highlights:

- **Clear time estimate:** 6-8 hours total (not 18+ hours like the developer track)
- **Visual learning path:** Shows exactly which weeks to do and which to skip
- **The CLEAR Framework:** Context, Logic, Edge cases, Acceptance, Restrictions - this is immediately applicable to writing better specs
- **AI-Ready Requirement Template:** I can use this tomorrow for my next feature spec
- **PM-Specific Prompt Library:** Requirement refinement, user story generation, edge case discovery, spec review prompts
- **Realistic expectations table:** "AI speeds up coding 2-5x, NOT 10x" - finally honest messaging

**Favorite section:** "Good Specs for AI" with examples of AI-friendly vs. AI-unfriendly specs. This is practical and actionable.

### 3. Week 0: AI Foundations

**File:** `/home/calilafollett/repos/dx-training/courses/ai-101-claude-code/sessions/week-0/README.md`

This is the optional "on-ramp" I desperately needed. Key improvements:

- **Non-technical language:** Explains LLMs without assuming CS background
- **Realistic "10x Developer" discussion:** Finally addresses the hype honestly with a breakdown by task type
- **Role-specific tracks section:** Points directly to PM quick-start guide
- **Non-coding exercises:** Reflection prompts, hallucination awareness, expectation calibration - all appropriate for PMs
- **Self-test questions:** "Why might an LLM hallucinate an API that doesn't exist?" - helps me understand the technology without writing code

The "Hallucination Awareness" exercise is particularly valuable. As a PM, I need to know when to trust AI-generated content and when to verify.

### 4. Comprehensive Glossary

**File:** `/home/calilafollett/repos/dx-training/courses/ai-101-claude-code/resources/glossary.md`

The glossary now covers terms I actually encounter:

- **AI & LLM Fundamentals:** Agentic, Context Window, Hallucination, Token - all explained clearly
- **HOA Domain Terms:** Assessment, CCR, Escalation, Grace Period, Late Fee, Violation - finally the business context!
- **Quick Reference Table:** One-line definitions I can scan quickly

The HOA domain section is particularly helpful. I can now connect technical capabilities to business concepts.

### 5. PM Track Files for Technical Weeks

**Files:**
- `/home/calilafollett/repos/dx-training/courses/ai-101-claude-code/sessions/week-5/tracks/pm.md`
- `/home/calilafollett/repos/dx-training/courses/ai-101-claude-code/sessions/week-6/tracks/pm.md`
- `/home/calilafollett/repos/dx-training/courses/ai-101-claude-code/sessions/week-7/tracks/pm.md`
- `/home/calilafollett/repos/dx-training/courses/ai-101-claude-code/sessions/week-8/tracks/pm.md`

Each technical week now has a PM-specific track file that:
- Clearly states "This week is optional for PMs"
- Provides a 5-minute conceptual summary of what developers are learning
- Suggests alternative activities (observe a developer session, review existing tools)
- Lists questions to ask your engineering team
- Explains why the content matters for product decisions

The Week 8 PM track on CI/CD is particularly well done - it explains pipeline status indicators, key metrics, and includes stakeholder communication templates. This is content I can use immediately.

### 6. PM-Specific Capstone (Option E)

**Files:**
- `/home/calilafollett/repos/dx-training/courses/ai-101-claude-code/sessions/week-9/README.md`
- `/home/calilafollett/repos/dx-training/courses/ai-101-claude-code/sessions/week-9/tracks/pm.md`
- `/home/calilafollett/repos/dx-training/courses/ai-101-claude-code/sessions/week-9/examples/capstone-templates/option-e-pm-product-design/`

**Option E: Product Design & Documentation** is a complete non-coding capstone. Deliverables include:

1. Product Requirements Document (PRD)
2. User Story Mapping with acceptance criteria
3. Stakeholder documentation package
4. Process documentation showing how Claude helped

**This is exactly what PMs do.** I can earn certification by producing PM artifacts, not by writing C# code.

The capstone template includes:
- Clear directory structure for deliverables
- Example prompts to get started
- Success criteria checklist
- INVEST principles reminder
- Gherkin format examples

The custom skill requirement (`/generate-user-stories`) is accessible - it's essentially creating a prompt template, not writing code.

---

## What's Still Too Technical

### 1. Prerequisites Checklist Still Code-Heavy

**File:** `/home/calilafollett/repos/dx-training/courses/ai-101-claude-code/README.md` (lines 144-163)

The prerequisites section lists:
- .NET 10 SDK
- Node.js 22 LTS via nvm
- npm 10+
- Git configured with GitLab access
- "Comfortable with command line"
- "Basic Git operations"
- "C# and/or Angular experience"

**Issue:** A PM following the PM track doesn't need .NET 10 SDK or Angular experience. This list will scare away PMs before they even start.

**Suggestion:** Add a "PM Prerequisites" section that's simpler:
- Claude Code installed (with link to simple install guide)
- Basic familiarity with terminal (opening it, running one command)
- Access to Slack channel for help

### 2. Week 1 Still Assumes Developer Setup

**File:** Main README Week 1 description

Week 1 agenda includes:
- "Fix the compound interest bug in CalculateFine"
- "Create RealManage-specific templates with TDD requirements"

The PM Quick-Start guide softens this ("Observe a developer using it"), but the main README Week 1 content is still developer-focused.

**Suggestion:** Add a brief PM sidebar in Week 1 README saying "PMs: Focus on observing and understanding the developer experience. You don't need to complete the coding exercises. See your track guide."

### 3. Success Metrics Are Code-Centric

**File:** `/home/calilafollett/repos/dx-training/courses/ai-101-claude-code/README.md` (lines 577-583)

"You're ready for the next week when you can... Generate working C# code with 80-90% test coverage"

This success metric doesn't apply to PMs at all. We're measuring PM success by whether they can generate C# code?

**Suggestion:** Add role-specific success metrics or note that these apply to the developer track.

### 4. Red Flags Section Mentions Test Coverage

**File:** `/home/calilafollett/repos/dx-training/courses/ai-101-claude-code/README.md` (lines 585-591)

"Seek help if... Test coverage drops below 80%"

Again, this is irrelevant for PMs.

### 5. Course Structure Still Shows 9 Weeks for Everyone

The course structure diagram (`Week 0 -> Week 1 -> ... -> Week 9`) doesn't indicate that PMs only need about half of it. A PM-specific learning path visualization would help.

### 6. Week 2 Is Light on PM Examples

While the PM Quick-Start guide has great PM prompts, Week 2's main README is still primarily about developer prompting. It would help to have PM-specific examples integrated into the main lesson.

---

## Specific Suggestions with File Paths

### Suggestion 1: Add PM Prerequisites Section

**File:** `/home/calilafollett/repos/dx-training/courses/ai-101-claude-code/README.md`

Add after the main prerequisites:

```markdown
### PM/Non-Developer Prerequisites
If you're following the PM or Support track:
- [ ] Claude Code installed ([Simple Install Guide](link))
- [ ] Can open a terminal and type a command
- [ ] Slack access to `#dx-training`
- [ ] Skip the SDK, npm, and testing tool requirements
```

### Suggestion 2: Add PM Success Metrics

**File:** `/home/calilafollett/repos/dx-training/courses/ai-101-claude-code/README.md`

Add a PM-specific success criteria section:

```markdown
### PM Track Success Metrics
You've completed the PM track when you can:
- Explain AI capabilities and limitations to stakeholders
- Write AI-ready feature specifications
- Use Claude to refine requirements and generate user stories
- Complete the Option E capstone (PRD + user stories)
```

### Suggestion 3: Update Course Overview Duration

**File:** `/home/calilafollett/repos/dx-training/courses/ai-101-claude-code/README.md`

Change:
```
- **Duration:** 9 weeks (self-paced, ~2 hours per week)
```

To:
```
- **Duration:** 9 weeks full course (~18 hours) | PM Track: 4-5 weeks (~8 hours)
```

### Suggestion 4: Add PM Note to Week 1 README

**File:** `/home/calilafollett/repos/dx-training/courses/ai-101-claude-code/sessions/week-1/README.md`

Add near the top:

```markdown
> **PM Track Note:** Focus on observing and understanding the developer experience.
> You don't need to complete the coding exercises. See [PM Quick-Start](../../resources/quick-start-pm.md).
```

### Suggestion 5: Create PM Learning Path Visualization

**File:** `/home/calilafollett/repos/dx-training/courses/ai-101-claude-code/README.md`

Add a PM-specific mermaid diagram:

```mermaid
graph LR
    A[Week 0: AI Basics] --> B[Week 1: Observe]
    B --> C[Week 2: Prompting]
    C --> D[Week 3: Plan Mode]
    D --> E[Week 9: PM Capstone]
    style E fill:#90EE90
```

---

## What I Can Now Do as a PM

After this updated course, I can:

1. **Write better feature specs** using the CLEAR framework
2. **Set realistic expectations** about AI development speed with stakeholders
3. **Ask smarter questions** in sprint planning ("What's Claude's implementation plan?")
4. **Use Claude directly** for requirement refinement, user story generation, and edge case discovery
5. **Understand developer conversations** about commands, skills, hooks, and CI/CD
6. **Complete a meaningful capstone** that produces PM artifacts, not code
7. **Explain AI limitations** including hallucination risks

---

## Comparison: Before vs. After

| Aspect | First Review | This Review |
|--------|--------------|-------------|
| Role guidance | None | Clear table + dedicated guide |
| PM track | Didn't exist | Complete path with skip guidance |
| Capstone option | All coding | Option E is non-coding |
| Glossary | Missing | Comprehensive with HOA terms |
| Week 0 | Didn't exist | Perfect on-ramp for non-devs |
| Time estimate | ~18 hours | ~8 hours for PM track |
| Practical value | Zero | High - prompts I can use tomorrow |

---

## Final Assessment

### Can a PM Complete This Course?

**Yes, absolutely.** The PM track is now clear, navigable, and genuinely useful.

### Should a PM Complete This Course?

**Yes, if they want to:**
- Write better AI-ready specifications
- Understand what their engineering team is doing with Claude Code
- Use Claude for PM tasks (user stories, edge cases, documentation)
- Stay current on AI-assisted development practices

### Is It Finally Accessible?

**For the PM track, yes.** The PM Quick-Start guide, Week 0, and PM track files make this accessible without technical background.

The main course content is still developer-focused (as it should be), but PMs now have clear signposting about what applies to them.

---

## Overall Rating: 4/5 Stars

**What would make it 5 stars:**
- PM-specific prerequisites section
- PM success metrics in main README
- PM learning path visualization
- More PM examples integrated into Week 2

**What's excellent:**
- PM Quick-Start Guide (genuinely useful)
- Week 0 AI Foundations (perfect on-ramp)
- PM Track files for each week
- Option E Capstone (appropriate for PMs)
- Glossary with business context
- Honest expectations about AI capabilities

---

## Recommendations for Other PMs

1. **Start with Week 0** - Don't skip it, even if you think you understand AI
2. **Read the PM Quick-Start guide first** - It's your roadmap
3. **Use the CLEAR framework** - Apply it to your next feature spec
4. **Keep the glossary bookmarked** - Reference it when developers use unfamiliar terms
5. **Do Option E for capstone** - It's designed for us
6. **Skip the technical weeks** - Skim the PM track files, but don't stress about the main content
7. **Pair with a developer** - Watch them use Claude Code in Week 1 to understand the tool

---

*Thank you to the course team for listening to PM feedback. This is a significant improvement and shows commitment to cross-functional accessibility.*

**Morgan Chen**
Product Manager, RealManage
*7 years experience, still not writing C# code, and now okay with that*
