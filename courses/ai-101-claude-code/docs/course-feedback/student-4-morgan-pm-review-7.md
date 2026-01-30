# Student Feedback: Morgan (Product Manager) - Review #7

**Reviewer:** Morgan Chen
**Role:** Product Manager (3 years experience, former developer)
**Company:** RealManage
**Date:** January 30, 2026
**Review Type:** Seventh review - Major course restructuring evaluation

---

## Executive Summary

This is my seventh review of the AI 101 Claude Code course. In Review #6, I gave the course its first perfect 10/10 score, declaring the PM track "complete and exemplary." Now I'm evaluating a significant restructuring that has changed the course organization.

**The verdict: The restructuring is a mixed bag for PMs.**

The new unified curriculum with role-specific tracks is a strong organizational improvement. However, the emphasis on Week 4 as a PM "Must Do" week (renamed to "Writing Testable Requirements") is a welcome addition that addresses one of my earlier concerns about PM-developer collaboration.

### Rating Progression

| Review | Rating | Key Finding |
| ------ | ------ | ----------- |
| Review #1 | 8/10 | Good content but too technical for PMs |
| Review #2 | 4/5 stars | Dramatic improvement with PM track |
| Review #3 | 4.5/5 stars | PM track excellent and practical |
| Review #4 | 4.5/5 stars (9.0/10) | Week 8 headless refactor improved accessibility |
| Review #5 | 4.8/5 stars (9.6/10) | Week 5 skill workshop transforms PM engagement |
| Review #6 | 5/5 stars (10/10) | PM track complete and exemplary |
| **Review #7** | **4.8/5 stars (9.6/10)** | Strong restructure, minor navigation issues |

**Rating decreased slightly:** The restructuring introduced some navigation inconsistencies and the PM Quick-Start guide learning path diagram no longer perfectly matches the new structure. However, the core content remains excellent.

---

## What Changed Since Review #6

### Major Structural Changes

1. **Unified Curriculum Model**
   - All roles now follow the same weekly README
   - Role-specific exercises moved to `tracks/` folders
   - This is cleaner and reduces duplicate content

2. **Week 4 Renamed and Repositioned**
   - Now called "Writing Testable Requirements" for PMs
   - Explicitly marked as "Must Do" in PM Quick-Start
   - New PM track file focuses on Given/When/Then format

3. **Consistent Track File Structure**
   - Every week now has a `tracks/pm.md` file
   - Clear navigation between main content and PM-specific exercises

### Issue Status from Review #6

| Issue | Status in Review #7 |
| ----- | ------------------- |
| All issues from #1-#5 | Still resolved |
| PM case studies request | **NOT ADDRESSED** |
| Video walkthrough option | **NOT ADDRESSED** |
| Community skill library | **NOT ADDRESSED** |

These remain nice-to-haves, not blockers.

---

## Week-by-Week PM Experience

### Week 0: AI Foundations (45 min)

**Rating: 10/10 - Unchanged**

The AI Foundations content remains excellent:

- "10x Developer" myth discussion with realistic speedup table
- Key vocabulary definitions (LLM, tokens, hallucination, agentic)
- Non-coding exercises (reflection, hallucination awareness, expectation calibration)

**PM Value:** Essential for setting stakeholder expectations about AI-assisted development.

### Week 1: Setup & Orientation (45 min)

**Rating: 9/10 - Improved**

The Week 1 PM track now has a clear, standalone exercise file (`sessions/week-1/tracks/pm.md`):

**Exercise 1: Codebase Understanding**

- Use Claude to understand the `hoa-cli` example application
- Ask questions in "non-technical terms"
- Identify features and business rules
- Find gaps in requirements

**Exercise 2: User Story Generation**

- Generate user stories from code
- Refine with acceptance criteria
- Ask for prioritization rationale
- Generate release notes

**What I Like:**

- The sandbox workflow is clearly explained
- Success criteria are explicit and checkable
- Exercises focus on PM outputs (user stories, release notes)

**Minor Issue:**
The path `courses/ai-101-claude-code/sessions/week-1` assumes you're in the repo root, but the quick-start guide doesn't make this explicit.

### Week 2: Prompting Foundations (1.5 hours)

**Rating: 10/10 - Unchanged**

The CLEAR Framework remains the crown jewel for PMs:

| Element | Application |
| ------- | ----------- |
| **C**ontext | Business background for feature specs |
| **L**ogic | Business rules that developers need |
| **E**dge cases | What happens when things go wrong |
| **A**cceptance | How to verify requirements |
| **R**estrictions | What NOT to do |

The PM track exercises are well-structured:

- Requirements refinement with vague vs. specific prompts
- PM prompt template creation
- Real-world application to HOA features

### Week 3: Plan Mode (1 hour)

**Rating: 9/10 - Good**

The PM track focuses on feature planning and requirements analysis:

**Exercise 1: Feature Planning with Plan Mode**

- Plan a payment plans feature
- Iterate on phasing ("Phase 1 seems too big for a 2-week sprint")
- Identify trade-offs and risks

**Exercise 2: Requirements Analysis with Plan Mode**

- Review rough feature requests for completeness
- Use Claude to identify gaps
- Generate clarifying questions and PRD outlines

**PM Value:** Understanding plan mode helps me understand how developers approach complex implementations.

### Week 4: Writing Testable Requirements (1.5 hours)

**Rating: 10/10 - NEW for PM Track**

This is a significant addition to the PM track. In previous reviews, I noted that Week 4 (TDD) felt too developer-focused. The new PM track file (`sessions/week-4/tracks/pm.md`) completely reimagines this week for product managers.

**Key Content:**

1. **Testable vs. Untestable Requirements Table**
   - "The system should be fast" vs. "Page loads in under 2 seconds"
   - "Late fees should be calculated correctly" vs. specific compound interest rules

2. **INVEST Criteria for Requirements**
   - Independent, Negotiable, Valuable, Estimable, Small, Testable
   - Each with HOA-specific examples

3. **Given/When/Then Format**
   - Multiple HOA examples (late fees, violation escalation, payments)
   - Shows how one feature needs multiple scenarios

4. **Edge Case Discovery with Claude**
   - Template for brainstorming edge cases
   - Payment processing example with 7 edge cases identified

5. **Three Amigos Collaboration**
   - How PM, Dev, and QA work together on requirements
   - "Your AC becomes their tests"

**PM Value:** This is exactly what was missing. I now understand that my acceptance criteria directly feed developer TDD. Bad requirements = developers guess what to test = bugs in production.

**Exercise Results:**
I tested the violation escalation exercise with Claude and got comprehensive Given/When/Then scenarios covering:

- Normal escalation at 30, 60, 90 days
- Edge case: exactly on threshold (day 30 exactly)
- Edge case: violation resolved between checks
- Error case: missing creation date

### Week 5: PM Skill Workshop (1.5 hours)

**Rating: 10/10 - Unchanged**

The four PM skills remain the highlight of the course:

| Skill | What It Does | Time Saved |
| ----- | ------------ | ---------- |
| `/release-notes` | Git log to stakeholder summary | 30 min/release |
| `/meeting-actions` | Meeting notes to Jira-ready tasks | 15 min/meeting |
| `/sprint-summary` | Sprint data to exec report | 45 min/sprint |
| `/user-stories` | Epic to broken-down stories | 20 min/epic |

The inclusion of output examples in the PM track makes expectations clear:

```markdown
### Release Notes: v2.1.0

**Release Date:** January 27, 2026

#### New Features
- Added violation auto-escalation after 30 days of inactivity
- New late fee compound interest calculator for accurate billing

#### Bug Fixes
- Fixed payment date calculation that caused off-by-one errors
```

### Weeks 6-7: Skip/Skim

**Rating: N/A - Appropriate**

Both weeks have PM track files that correctly state:

- "This week is optional for PMs"
- 5-minute conceptual summary provided
- Questions to ask engineering

**PM Value:** I understand that agents, hooks, and plugins exist without needing to build them.

### Week 8: Real-World Automation (45 min)

**Rating: 10/10 - Unchanged**

The connection between Week 5 skills and headless automation is clear:

```bash
# Use your release-notes skill from Week 5
claude -p "/release-notes 2.1.0 2024-01-01" --no-session-persistence > release-notes.md
```

Time savings table is realistic and validated:

| Task | Manual | Automated | Savings |
| ---- | ------ | --------- | ------- |
| Release notes | 60 min | 10 min | 50 min |
| Sprint summaries | 45 min | 10 min | 35 min |
| Meeting actions | 20 min | 5 min | 15 min |

### Week 9: PM Capstone - Option E (2-3 hours)

**Rating: 10/10 - Excellent**

Option E remains a complete non-coding capstone. The template at `sessions/week-9/examples/capstone-templates/option-e-pm-product-design/` is well-structured with:

- Complete project structure with `docs/` folders
- CLAUDE.md context file
- README with detailed guidance
- Example prompts for each deliverable
- Success criteria checklist

**Deliverables:**

1. PRD (`docs/PRD.md`)
2. User Story Map (`docs/user-stories.md`)
3. Stakeholder Docs (`docs/stakeholder/`)
4. Custom PM Skill (`.claude/skills/<skill-name>/SKILL.md`)
5. Process Documentation (`docs/process.md`)
6. Presentation Outline (`docs/presentation-outline.md`)

---

## Issues Identified

### Issue 1: Learning Path Diagram Inconsistency (Medium)

**Location:** `resources/quick-start-pm.md`

The mermaid diagram shows:

- Week 4 labeled "Requirements" (1.5 hrs)
- Weeks 6-7 in a single "Skip" block

But the main README shows Week 4 as "Test-Driven Development" and the PM track file calls it "Writing Testable Requirements."

**Recommendation:** Update the diagram labels to match the actual PM track experience.

### Issue 2: Week 4 PM Track Not Linked from Main README (Low)

**Location:** `README.md`

The main README Week 4 section mentions TDD exercises but doesn't explicitly call out the PM track option for "Writing Testable Requirements."

**Recommendation:** Add a PM callout like Week 1 has.

### Issue 3: Sandbox Path Assumptions (Low)

**Location:** Multiple `tracks/pm.md` files

Exercises assume you start from repo root:

```bash
cd courses/ai-101-claude-code/sessions/week-1
```

But this isn't explicitly stated, which could confuse users who start Claude Code from a different directory.

**Recommendation:** Add a brief "Start from repo root" instruction.

---

## Comparison to Previous Reviews

### What's Better

1. **Week 4 PM Track:** The "Writing Testable Requirements" content is exactly what was needed. PMs now have a complete understanding of how their acceptance criteria feeds developer TDD.

2. **Unified Structure:** All weeks following the same README + tracks pattern is cleaner and easier to navigate.

3. **Consistent PM Track Files:** Every week now has a `tracks/pm.md` file with appropriate guidance (even if it's "skip this week").

### What's Unchanged (Good)

- Week 5 PM Skill Workshop remains excellent
- Week 8 headless automation connection is clear
- Week 9 Option E capstone is comprehensive
- CLEAR Framework in Week 2 is still gold

### What Regressed Slightly

- Learning path diagram inconsistencies
- Some navigation clarity lost in restructuring

---

## Strategic Assessment: Would This Help PMs at RealManage Adopt Claude Code?

### Yes, Absolutely

**For Individual PM Productivity:**

- CLEAR Framework improves spec quality
- Four PM skills automate real work (release notes, meeting actions, sprint summaries, user stories)
- Headless automation enables batch processing
- ROI is immediate: ~10 hours/month saved

**For PM-Developer Collaboration:**

- Week 4 "Writing Testable Requirements" bridges the gap
- Understanding plan mode helps review developer approaches
- Shared vocabulary (Given/When/Then, INVEST) improves communication

**For Team Adoption:**

- Skills can be shared via `.claude/skills/` in project repos
- PM capstone (Option E) provides certification path
- Realistic time estimates help with training planning

### Adoption Recommendations for PM Team

1. **Week 2 is Mandatory:** The CLEAR Framework changes how you write specs
2. **Week 4 is New and Important:** Learn how your AC becomes their tests
3. **Week 5 is Your Power Week:** Create all four skills
4. **Week 8 Connects the Dots:** Use skills with CLI automation
5. **Option E for Certification:** Complete legitimate PM deliverables

---

## ROI Calculation (Updated)

### Time Investment

| Week | PM Time |
| ---- | ------- |
| 0 | 45 min |
| 1 | 45 min |
| 2 | 1.5 hrs |
| 3 | 1 hr |
| 4 | 1.5 hrs |
| 5 | 1.5 hrs |
| 6-7 | 15 min (skim) |
| 8 | 45 min |
| 9 | 2-3 hrs |
| **Total** | **~10-11 hrs** |

### Time Savings (Monthly)

| Task | Savings |
| ---- | ------- |
| Release notes | 2 hrs |
| Meeting actions | 4 hrs |
| Sprint summaries | 1.5 hrs |
| User stories | 2 hrs |
| **Total** | **~10 hrs/month** |

**Payback Period:** Less than 2 months
**Annual Savings:** ~120 hours (3 weeks of work)

---

## Final Assessment

### Can a PM Complete This Course?

**Yes, without question.** The PM track is comprehensive, accessible, and genuinely useful.

### Is the Restructuring an Improvement?

**Mostly yes.** The unified curriculum with role-specific tracks is cleaner. Week 4 becoming "Writing Testable Requirements" for PMs is a significant addition. Minor navigation inconsistencies can be fixed.

### What Would Make It Perfect Again?

1. Update learning path diagram to match new structure
2. Add PM callout to main README Week 4 section
3. Clarify sandbox path assumptions

---

## Overall Rating: 4.8/5 Stars (9.6/10)

**Why not 10/10 like Review #6:**

- Learning path diagram inconsistencies
- Minor navigation clarity issues from restructuring
- These are easily fixable

**What's Excellent:**

- Week 4 PM Track "Writing Testable Requirements" (new)
- Week 5 PM Skill Workshop
- Week 8 headless automation connection
- Week 9 Option E capstone
- CLEAR Framework in Week 2
- Unified curriculum structure (improvement)
- Comprehensive glossary with HOA terms

---

## Recommendations Summary

### High Priority

1. **Update PM Quick-Start learning path diagram** to show Week 4 as "Writing Testable Requirements"

### Medium Priority

1. **Add PM callout to main README Week 4** similar to other weeks
2. **Clarify sandbox starting directory** in exercise instructions

### Nice to Have

1. PM case studies or testimonials
2. Video walkthrough option for visual learners
3. Community skill library for sharing PM skills

---

## Summary for Course Team

The restructuring shows continued evolution of the course. The unified curriculum model is cleaner, and the addition of Week 4 "Writing Testable Requirements" for PMs addresses a gap I mentioned in earlier reviews.

**Key Achievement This Version:**
Week 4 PM track teaches PMs that "Your AC becomes their tests." This is the missing link that helps PMs understand why writing testable requirements matters for developer TDD.

**The PM Journey is Now Complete:**

| Week | What PMs Learn |
| ---- | -------------- |
| 0 | AI foundations and realistic expectations |
| 1 | Understand developer experience |
| 2 | CLEAR Framework for specs |
| 3 | Plan mode for feature planning |
| 4 | Writing testable requirements (NEW!) |
| 5 | Create PM automation skills |
| 8 | Use skills with headless CLI |
| 9 | PM capstone certification |

**Minor fixes needed** for navigation consistency, but the core content remains exemplary.

---

*This course continues to prove that AI-assisted development isn't just for engineers. The PM track offers genuine productivity gains, better collaboration with developers, and a legitimate certification path. The addition of Week 4 "Writing Testable Requirements" is exactly what PMs needed to understand how their work feeds developer TDD.*

**Morgan Chen**
Product Manager, RealManage
*3 years experience, former developer, AI-enabled PM with a skill library and testable requirements*
*January 30, 2026*

---

## Appendix: Rating Justification

| Criteria | Score | Notes |
| -------- | ----- | ----- |
| Content quality | 10/10 | Week 4 PM track is excellent addition |
| Practical value | 10/10 | 4 skills + testable requirements framework |
| Accessibility | 9/10 | Minor navigation inconsistencies |
| Clear navigation | 9/10 | Learning path diagram needs update |
| Time estimates | 10/10 | All weeks completed within stated times |
| PM relevance | 10/10 | All content is PM-focused, not developer-focused |

**Overall: 9.6/10**
