# Student Feedback: Morgan (Product Manager) - Review #3

**Reviewer:** Morgan Chen
**Role:** Product Manager (3 years experience, former developer)
**Company:** RealManage
**Date:** January 23, 2026
**Review Type:** Third comprehensive review following course maturation

---

## Executive Summary

This is my third time through the AI 101 Claude Code course, and I have to say - this course has matured beautifully. What started as a developer-centric training that left PMs feeling like outsiders has evolved into a genuinely cross-functional learning experience.

### Rating Comparison

| Review | Rating | Key Finding |
| ------ | ------ | ----------- |
| Review #1 | 8/10 | Good content but too technical for PMs |
| Review #2 | 4/5 stars | Dramatic improvement with PM track |
| **Review #3** | **4.5/5 stars** | PM track is now excellent and practical |

The incremental improvement from my second review shows the course team is continuing to refine the PM experience. The course is now something I would enthusiastically recommend to other PMs without caveats.

---

## Week-by-Week Experience (PM Track)

### Week 0: AI Foundations (45 min)

**Experience: Excellent**

The AI foundations module remains a perfect on-ramp for non-developers. Key strengths:

- The "10x Developer" myth discussion is refreshingly honest - it sets realistic expectations
- Vocabulary definitions are PM-friendly (tokens, hallucinations, context window)
- The self-test questions help validate understanding without requiring code

**What I did:** Read through the content, completed the reflection exercises, understood key vocabulary.

**PM Value:** Understanding the technology helps me have informed conversations with developers about AI-assisted development timelines and capabilities.

### Week 1: Setup & Orientation (45 min)

**Experience: Good**

The PM Track Note at the top is helpful: "Focus on observing and understanding the developer experience. You don't need to complete the coding exercises."

**What I did:**

- Installed Claude Code (straightforward with npm)
- Had my first conversation with Claude
- Reviewed the example CLI app structure (didn't need to run it)

**CLI Test (Headless):**

```
> Hello! I'm Morgan, a Product Manager taking the AI 101 Claude Code course.
< Claude understood my role and offered PM-relevant guidance immediately.
```

**PM Value:** Understanding what Claude Code *is* and *does* helps me understand what developers are working with.

### Week 2: Prompting Foundations (1.5 hours)

**Experience: Excellent - This is the PM Power Week**

This week delivers enormous value for PMs. The CLEAR Framework is immediately applicable:

| Element | Application |
| ------- | ----------- |
| **C**ontext | Business background for feature specs |
| **L**ogic | Business rules that developers need |
| **E**dge cases | What happens when things go wrong |
| **A**cceptance | How to verify requirements |
| **R**estrictions | What NOT to do |

**CLI Exercise - Requirement Refinement:**

```
Prompt: Help me refine "We need a way for residents to pay their HOA dues online"
```

Claude's response was exceptional:

- Identified 9 specific ambiguities (payment methods, partial payments, fees, etc.)
- Suggested acceptance criteria in Gherkin format
- Listed 10 edge cases I hadn't considered
- Recommended a clear MVP scope

**PM Value:** I can use this approach for every feature spec I write. The structured thinking makes my specs more complete.

### Week 3: Plan Mode & Code Review (1 hour)

**Experience: Very Good**

Understanding Plan Mode helps me understand how developers approach complex tasks. Key insight: Plan Mode is for *thinking through* problems, not documenting them.

**CLI Exercise - Requirement Validation:**

```
Prompt: Create an implementation plan for my payment feature spec covering components, tests, and edge cases.
```

Claude provided:

- Component breakdown (backend, frontend, database)
- Test plan with unit and integration test cases
- Edge case handling table
- A "Spec Gap Analysis" that caught missing requirements!

**PM Insight:** I can ask developers to share Claude's implementation plan before coding starts. This helps catch misunderstandings early.

### Week 4: TDD (Concepts Only - 30 min)

**Experience: Good**

The PM Quick-Start guide correctly marks this as "Skim - Concepts only." I read about TDD philosophy without doing coding exercises.

**Key Takeaway for PMs:** TDD produces more reliable estimates because tests surface complexity early. I now ask developers "What tests will verify this requirement?"

### Weeks 5-7: Technical Weeks (Skipped per PM Track)

**Experience: Appropriate Guidance**

Each week has a dedicated PM track file that:

- Clearly states "This week is optional for PMs"
- Provides a 5-minute conceptual summary
- Suggests alternative activities
- Lists questions to ask engineering

**Week 5 (Commands & Skills):** Understood that these are "saved prompts" for standardization.
**Week 6 (Agents & Hooks):** Understood that hooks enable compliance automation.
**Week 7 (Plugins):** Understood that plugins package automation for team sharing.

**PM Value:** I can have intelligent conversations with developers about these concepts without needing to build them myself.

### Week 8: Real-World Workflows (45 min)

**Experience: Excellent**

The Week 8 PM track is particularly well-designed. It covers:

- CI/CD basics in PM-friendly language
- How Claude Code enhances automated workflows
- Key metrics to track (coverage, pipeline duration, success rate)
- Stakeholder communication templates

**The templates are immediately usable:**

```markdown
## Release Notes: v2.4.0
**Date:** January 22, 2025
**Status:** Deployed to Production

### New Features
- Violation auto-escalation after 30 days (DX-1234)
...

### Quality Metrics
- Test Coverage: 87% (up from 84%)
```

**PM Value:** I can communicate deployment status to stakeholders using these templates.

### Week 9: Capstone - Option E (PM Track)

**Experience: Excellent**

Option E is a non-coding capstone specifically for PMs. Deliverables:

1. Product Requirements Document (PRD)
2. User Story Mapping
3. Stakeholder Documentation
4. Process Documentation

**CLI Exercise - User Story Generation:**

```
Prompt: Generate user stories for the Violation Appeals feature following INVEST principles with Gherkin acceptance criteria.
```

Claude generated 9 complete user stories including:

- Submit Appeal Request (Must, M complexity)
- View My Appeals (Must, S complexity)
- Review Appeal Queue (Must, M complexity)
- Record Appeal Decision (Must, M complexity)
- Attach Evidence (Should, S complexity)
- Schedule Hearing (Should, M complexity)
- Withdraw Appeal (Should, S complexity)
- Deadline Notifications (Could, S complexity)
- Audit Trail (Could, M complexity)

Each story included proper Gherkin acceptance criteria and followed INVEST principles.

**PM Value:** This is exactly what I do in my daily work. The capstone produces real PM artifacts, not code.

---

## Comparison to Previous Reviews

### Issues Resolved

| Issue from Review #2 | Status | Evidence |
| -------------------- | ------ | -------- |
| Prerequisites too code-heavy | **Resolved** | PM/Support Prerequisites section added (lines 176-181 of README) |
| Week 1 assumes developer setup | **Resolved** | PM Track Note at top of Week 1 README |
| Success metrics code-centric | **Resolved** | PM Track Success Metrics added (lines 612-618) |
| Red flags mention test coverage | **Partially Resolved** | Now says "*(Developer/QA tracks)*" qualifier |
| Course structure shows 9 weeks for everyone | **Resolved** | Role selector table shows PM focus weeks |
| Week 2 light on PM examples | **Improved** | PM Quick-Start guide has extensive PM prompts |

### New Improvements Since Review #2

1. **Week 8 PM Track** - The CI/CD content for PMs is now comprehensive with templates
2. **Week 9 PM Track** - Detailed guidance for Option E capstone with success criteria checklist
3. **Terminology Explanations** - Course content now consistently explains technical terms in PM-friendly language
4. **PM Quick-Start Guide** - The CLEAR framework and spec templates are production-ready

### Regressions

None identified. The course has only improved since my last review.

---

## What I Can Now Do as a PM

After this course, I can:

1. **Write AI-ready specifications** using the CLEAR framework
2. **Validate requirements** by asking Claude for implementation plans
3. **Generate user stories** with proper acceptance criteria and edge cases
4. **Communicate with developers** about Claude Code workflows intelligently
5. **Set realistic expectations** about AI development speed with stakeholders
6. **Understand CI/CD metrics** and communicate deployment status
7. **Complete a meaningful capstone** that produces PM artifacts

---

## CLI Session Log

### Session Summary

All exercises completed using headless Claude CLI with `--print` flag:

| Exercise | Week | Outcome |
| -------- | ---- | ------- |
| Introduction | 1 | Claude recognized PM role immediately |
| CLEAR Framework - Requirement Refinement | 2 | Excellent spec analysis with 9 ambiguities identified |
| Plan Mode - Implementation Planning | 3 | Comprehensive plan with gap analysis |
| Terminology Understanding | All | PM-friendly explanations provided |
| User Story Generation | 9 | 9 complete stories with Gherkin AC |

### Sample Interaction Quality

**Prompt:** "Using the CLEAR framework, help me refine this rough feature idea into a better spec: 'We need a way for residents to pay their HOA dues online'"

**Response Quality:** 10/10

- Identified ambiguities (payment methods, fees, partial payments, etc.)
- Provided Gherkin acceptance criteria
- Listed 10 edge cases
- Recommended clear MVP scope
- Asked clarifying questions for stakeholders

---

## Recommendations

### For Course Team

1. **Add more PM examples to main Week 2 README** - While the PM Quick-Start guide is excellent, integrating some PM examples into the main content would help PMs who browse the main README
2. **Consider a PM learning path visualization** - A mermaid diagram showing the PM path specifically would help
3. **Add PM case studies** - Real examples of PMs using Claude for product work would be inspiring

### For Other PMs Taking This Course

1. **Start with Week 0** - The foundations are genuinely useful
2. **Read the PM Quick-Start guide first** - It's your roadmap
3. **Focus on Weeks 2-3** - These are your power weeks
4. **Use the CLEAR framework** - Apply it to your next feature spec
5. **Skip technical weeks guilt-free** - The PM track files give you what you need
6. **Do Option E for capstone** - It's designed for us

---

## Final Assessment

### Can a PM Complete This Course?

**Absolutely yes.** The PM track is now clear, navigable, and genuinely useful.

### Should a PM Complete This Course?

**Yes, if they want to:**

- Write better AI-ready specifications
- Understand developer workflows with Claude Code
- Use Claude for PM tasks (user stories, edge cases, requirement refinement)
- Communicate more effectively with engineering teams

### Is It Accessible?

**For the PM track, yes.** The combination of:

- Role selector table in main README
- PM Quick-Start guide
- PM track files for each week
- Non-coding Option E capstone

...makes this course fully accessible for PMs without technical background.

---

## Overall Rating: 4.5/5 Stars

**What would make it 5 stars:**

- More PM examples integrated into main Week 2 README
- PM-specific learning path visualization diagram
- Case studies of PM success stories

**What's excellent:**

- PM Quick-Start Guide with CLEAR framework
- Week 0 AI Foundations
- Week 2 Prompting Foundations
- Week 8 PM Track with templates
- Week 9 Option E Capstone
- PM track files for technical weeks
- Glossary with business context
- Honest expectations about AI capabilities

---

*This course has come a long way. What started as a developer training with PM afterthought has become a genuinely cross-functional learning experience. The PM track is now something I would enthusiastically recommend to my PM colleagues.*

**Morgan Chen**
Product Manager, RealManage
*3 years experience, former developer, now an AI-enabled PM*
*January 23, 2026*
