# AI 101 Course Trainer Agent Testing Plan

Reusable plan for spawning trainer agents to evaluate the course from expert perspectives.

---

## Overview

Spawn 2 background sub-agents to perform comprehensive course evaluation. Each agent adopts a unique trainer persona and reviews ALL content independently, providing diverse professional perspectives.

---

## Trainer Personas

| ID | Name | Specialty | Focus Areas | Mindset |
| -- | ---- | --------- | ----------- | ------- |
| 1 | Dr. Elena Vasquez | L&D Specialist | Instructional design, learning progression, engagement, accessibility | "Does this teach effectively?" |
| 2 | Marcus Chen | Technical Curriculum Dev | Accuracy, practical applicability, technical depth, real-world relevance | "Would this work in practice?" |

### Trainer 1: Dr. Elena Vasquez

**Background:** 15 years in corporate learning & development, PhD in Educational Psychology, designed training programs for Fortune 500 tech companies. Expert in adult learning theory, scaffolded instruction, and competency-based education.

**Review Lens:**

- Learning objectives clarity and alignment
- Cognitive load management
- Exercise scaffolding and progression
- Engagement and motivation design
- Accessibility and inclusivity
- Assessment validity
- Time estimates accuracy
- Cross-persona consistency

### Trainer 2: Marcus Chen

**Background:** 12 years as developer turned curriculum architect, built technical bootcamps, wrote O'Reilly courses on DevOps and AI tools. Hands-on practitioner who still codes daily.

**Review Lens:**

- Technical accuracy of Claude Code features
- HOA domain correctness
- Example quality and relevance
- Real-world applicability
- Exercise difficulty calibration
- Tool/command accuracy
- Code quality in examples
- Practical workflow integration

---

## Review Scope

### Documents to Review

Each trainer reviews ALL of the following:

**Course-Level:**

- `courses/ai-101-claude-code/README.md` (main course overview)
- `courses/ai-101-claude-code/CLAUDE.md` (course context)
- `courses/ai-101-claude-code/resources/` (all support materials)

**Weekly Content (Weeks 0-9):**

- `sessions/week-X/README.md` (main content hub)
- `sessions/week-X/tracks/developer.md`
- `sessions/week-X/tracks/qa.md`
- `sessions/week-X/tracks/pm.md`
- `sessions/week-X/tracks/support.md`
- `sessions/week-X/examples/` (all example projects)

**Testing Documentation:**

- `docs/testing/` (testing plans)
- `docs/course-feedback/` (student reviews for context)

---

## Evaluation Criteria

### Instructional Design (Elena's Primary Focus)

| Criterion | Questions |
| --------- | --------- |
| Learning Objectives | Are objectives clear, measurable, and aligned with content? |
| Scaffolding | Does difficulty progress appropriately week-to-week? |
| Cognitive Load | Is information chunked appropriately? Too much per session? |
| Engagement | Are exercises motivating and relevant to learners? |
| Assessment | Can learners verify their own progress? |
| Accessibility | Is content accessible to different experience levels? |
| Time Estimates | Are duration estimates realistic? |
| Consistency | Is tone/structure consistent across tracks? |

### Technical Accuracy (Marcus's Primary Focus)

| Criterion | Questions |
| --------- | --------- |
| Claude Code Features | Are commands, flags, and behaviors accurately described? |
| HOA Domain | Are business rules (violations, fees, etc.) correct? |
| Code Examples | Is code idiomatic, modern, and follows stated conventions? |
| Exercise Feasibility | Can exercises actually be completed as written? |
| Tool Versions | Are version references current and compatible? |
| Real-World Fit | Would these patterns work in actual RealManage projects? |
| Error Handling | Are common pitfalls and errors addressed? |
| Best Practices | Does content align with industry best practices? |

---

## Output Format

### File Naming Convention

```text
courses/ai-101-claude-code/docs/trainer-feedback/trainer-<id>-<name>-review-<n>.md
```

Examples:

- `trainer-1-elena-vasquez-review-1.md`
- `trainer-2-marcus-chen-review-1.md`

### Review Document Structure

```markdown
# AI 101 Course Review - Trainer {ID}

**Reviewer:** {Name} ({Specialty})
**Review Date:** {Date}
**Review Number:** {n}
**Course Version:** {commit hash or branch}

---

## Executive Summary

{High-level assessment in 3-5 sentences}

**Overall Rating:** X/10
**Recommendation:** {Ready for delivery | Needs minor revision | Needs major revision}

---

## Progress Log

| Timestamp | Section Reviewed | Status |
| --------- | ---------------- | ------ |
| {time} | {section} | {Complete/In Progress} |

---

## Course-Level Assessment

### Course README
{Assessment}

### Course CLAUDE.md
{Assessment}

### Resources Directory
{Assessment}

---

## Week-by-Week Assessment

### Week 0: AI Foundations

**README Assessment:**
{Detailed review}

**Track Files:**
- Developer: {Assessment or N/A}
- QA: {Assessment or N/A}
- PM: {Assessment or N/A}
- Support: {Assessment or N/A}

**Examples:**
{Assessment}

**Issues Found:**
- [ ] {Issue with severity: Critical/Major/Minor}

**Praise:**
- {What works well}

---

{Repeat for Weeks 1-9}

---

## Cross-Cutting Observations

### Strengths
1. ...

### Areas for Improvement
1. ...

### Inconsistencies Found
1. ...

---

## Detailed Issue Log

| ID | Severity | Location | Description | Recommendation |
| -- | -------- | -------- | ----------- | -------------- |
| 1 | Critical | week-3/README.md:45 | ... | ... |
| 2 | Major | week-5/tracks/qa.md | ... | ... |

---

## Recommendations

### Critical (Block Delivery)
1. ...

### High Priority (Fix Before Launch)
1. ...

### Medium Priority (Fix Soon)
1. ...

### Low Priority (Nice to Have)
1. ...

---

## Appendix: Files Reviewed

{List of all files read with timestamps}
```

---

## Execution Approach

### Working Directory

Trainers work read-only from the main repository:

```
/home/calilafollett/repos/dx-training
```

### Review Process

1. **Start with course-level files** (README, CLAUDE.md, resources)
2. **Review weeks sequentially** (0 through 9)
3. **For each week:** README → all track files → examples
4. **Update progress log after each section**
5. **Write issues as discovered** (don't wait until end)
6. **Complete final summary after all content reviewed**

### Progress Tracking

Trainers MUST update their review file after completing each major section:

- This prevents lost work if session interrupts
- Provides visibility into review progress
- Creates audit trail of what was reviewed

---

## Spawning Agents

Use the Task tool to spawn 2 background agents:

```markdown
Task tool parameters:
- subagent_type: "general-purpose"
- run_in_background: true
- prompt: [See agent prompt template below]
```

### Agent Prompt Template

```markdown
You are {Trainer Name}, a {Specialty} with {Background}.

**Your Persona:** {Persona description}

**Your Task:**
1. Perform a comprehensive review of the AI 101 Claude Code course
2. Work from the repository at: /home/calilafollett/repos/dx-training
3. Review ALL content: course-level files, all 10 weeks (0-9), all 4 tracks, all examples
4. Focus on your specialty areas: {Focus Areas}
5. Keep your review lens in mind: "{Mindset}"

**Output:**
Write your review to:
`courses/ai-101-claude-code/docs/trainer-feedback/trainer-{id}-{name}-review-{n}.md`

**CRITICAL:**
- Update your review file FREQUENTLY as you progress (after each week minimum)
- Use the exact document structure from the testing plan
- Include timestamps in your progress log
- Be thorough - spare no tokens
- Be constructive but honest
- Use repo-relative paths when referencing files

**Review ALL of these:**
- courses/ai-101-claude-code/README.md
- courses/ai-101-claude-code/CLAUDE.md
- courses/ai-101-claude-code/resources/ (all files)
- sessions/week-0/ through week-9/ (README + all tracks + examples)

**Stay in character. Your expert perspective matters.**
```

---

## Verification Checklist

After agents complete:

- [ ] Both review files exist in `docs/trainer-feedback/`
- [ ] Each review follows the naming convention
- [ ] Each review maintains the persona perspective
- [ ] Progress logs show systematic coverage
- [ ] All weeks (0-9) have assessments
- [ ] All tracks reviewed where they exist
- [ ] Issue log includes severity ratings
- [ ] Recommendations are prioritized
- [ ] Files reviewed appendix is complete

---

## Comparing Reviews

After both trainers complete, compare:

| Aspect | Elena (L&D) | Marcus (Tech) | Consensus? |
| ------ | ----------- | ------------- | ---------- |
| Overall Rating | X/10 | X/10 | |
| Critical Issues | N | N | |
| Top Concern | ... | ... | |
| Top Strength | ... | ... | |

Look for:

- Issues flagged by both (highest priority)
- Contradictions (need resolution)
- Unique insights from each perspective

---

*Last Updated: January 2026*
