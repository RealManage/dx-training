# Course Feedback: Jordan (QA Engineer) - Third Review

**Student:** Jordan
**Role:** QA Engineer (4 years experience)
**Company:** RealManage
**Review Date:** January 23, 2026
**Review Type:** Third review after QA track additions

---

## Executive Summary

This is my third review of the AI-101 Claude Code course. The course has made significant progress since my last review.

### Rating Comparison

| Review | Rating | Key Issue |
|--------|--------|-----------|
| Review 1 (Initial) | 7.5/10 | Course assumed I write production code |
| Review 2 (After restructure) | 8/10 (4/5 stars) | Week 4 exercises still developer-focused |
| **Review 3 (Current)** | **9/10** | Minor polish needed, but solid QA experience |

### Verdict

The course is now genuinely QA-friendly. The addition of a dedicated Week 4 QA Track was the critical fix I needed. I can complete this course without feeling like I'm constantly fighting against developer-centric content.

---

## What Changed Since Review 2

### Issue Resolved: Week 4 QA Track Added

**Previous complaint:** "Week 4 is my 'power week' but still assumes I write production code."

**Status: FULLY RESOLVED**

The new file `/courses/ai-101-claude-code/sessions/week-4/tracks/qa.md` is exactly what I asked for:

- **Focus:** "Writing tests for existing code, not building production code"
- **Exercise 1:** Test an existing service (bring 40% coverage to 80%+)
- **Exercise 2:** Coverage gap analysis (identify what's missing)
- **Exercise 3:** Test data generation (using Bogus)

The key difference table in the QA track nails it:

| Aspect | Developer Track | QA Track |
|--------|----------------|----------|
| **Goal** | Write code + tests together | Write tests for existing code |
| **Starting point** | Empty project | Existing codebase with gaps |
| **Focus** | TDD cycle | Coverage analysis + gap filling |

This is how I actually work as a QA engineer. I verify code that developers wrote, not build features myself.

### Issue Resolved: QA Prerequisites Section

**Previous complaint:** "Prerequisites are dev-focused."

**Status: FULLY RESOLVED**

The main README now has a dedicated "QA Engineer Prerequisites" section:

```
### QA Engineer Prerequisites
If following the QA Track, you need less setup:
- [ ] Claude Code installed
- [ ] Basic terminal familiarity
- [ ] .NET SDK (for running tests, not writing production code)
- [ ] *Skip:* Deep C# experience not required
```

This removes the intimidation factor. I don't need to know Angular or npm for QA work.

### Issue Resolved: QA Success Metrics

**Previous complaint:** "Success metrics are code-centric."

**Status: FULLY RESOLVED**

New QA-specific success metrics in the README:

> **QA Track**
> You've completed the QA track when you can:
> - Generate comprehensive test suites for existing code
> - Identify coverage gaps and write tests to fill them
> - Use Claude to create test data and edge case scenarios
> - Review and improve existing test quality
> - Complete Option D capstone (Test Automation Suite)

These are MY metrics. This is what I do.

---

## Week-by-Week Experience (QA Track)

### Week 1: Setup & Orientation

**Rating:** 4.5/5

Straightforward setup. The QA-specific CLAUDE.md suggestions were helpful:

```markdown
## Testing Preferences
- Prefer xUnit with FluentAssertions
- Use Arrange-Act-Assert pattern
- Include edge cases and boundary tests
```

This gets me started with the right context for my QA work.

### Week 2: Prompting Foundations

**Rating:** 5/5

Still excellent for QA. The prompting skills translate directly to writing test cases. The QA quick-start guide has exactly the prompts I need:

- Test case generation
- Bug reproduction tests
- API contract testing
- Test refactoring

### Week 4: TDD - QA Track

**Rating:** 5/5 (MAJOR IMPROVEMENT)

This is now genuinely my power week. I ran through the exercises using the headless Claude CLI:

**Exercise 1: Test an Existing Service**

I asked Claude to analyze PropertyService.cs for coverage gaps. The response was exactly what I needed:

- Identified `UpdatePropertyAsync` as UNTESTED (zero coverage)
- Found missing edge cases for `SearchPropertiesAsync` (null, empty, no matches)
- Prioritized gaps by business risk
- Generated the missing tests with proper FluentAssertions syntax

**Exercise 2: Coverage Gap Analysis**

The prompts in the QA track guide work:

```
Run tests with coverage and show me which methods have less than 80% branch coverage.
For each gap, explain what scenarios aren't tested.
```

Claude identified the exact gaps and suggested tests to fill them.

**Key Improvement:** The exercises no longer ask me to "build a CLI application." They ask me to "test the existing PropertyService" and "fill coverage gaps." This is QA work.

### Week 5: Commands & Skills - QA Track

**Rating:** 4.5/5

The QA track properly positions me as a consumer, not a creator:

> "As a QA engineer, you don't need to create commands and skills from scratch - developers will build those. Your focus is on **using** them effectively and **providing feedback** to improve them."

I tested the `/violation-report` command and evaluated it for:
- Accuracy
- Completeness
- Edge case handling
- Error handling

This is QA work. I test things and provide feedback. I don't need to build the commands myself.

### Week 6: Agents & Hooks - QA Track

**Rating:** 4.5/5

The focus on test automation hooks is very practical:

```json
{
  "hooks": {
    "PostToolUse": [
      {
        "matcher": "Edit",
        "hooks": [
          {
            "type": "command",
            "command": "dotnet test --no-build --verbosity minimal"
          }
        ]
      }
    ]
  }
}
```

Auto-running tests after every edit? This is exactly what I want. The QA track simplifies agent usage to read-only analysis agents with `permissionMode: plan`.

### Week 8: Real-World Automation & CI/CD - QA Track

**Rating:** 4/5

The CI/CD test integration content is relevant for QA:

- Coverage reporting in GitLab
- Quality gates (fail if coverage < 80%)
- Coverage gap analysis skills

I appreciate the concrete `.gitlab-ci.yml` examples. Even though CI/CD isn't my primary job, understanding how tests integrate into pipelines helps me communicate with DevOps.

### Week 9: Capstone - QA Track (Option D)

**Rating:** 5/5

Option D: Test Automation Suite is perfect for QA:

- Build comprehensive test suites
- Create test data generators with Bogus
- Set up coverage dashboards
- Integrate with CI/CD

The success criteria align with my role:

```
[ ] Test suite covers all critical HOA workflows
[ ] Test data generation creates realistic scenarios
[ ] Coverage dashboard displays real-time metrics
[ ] CI/CD pipeline runs tests on every commit
[ ] Coverage >= 80% on critical paths
```

I can complete this capstone using my existing QA skills + what I learned about Claude Code.

---

## Comparison to Previous Reviews

### Issues From Review 1

| Issue | Review 1 Status | Review 2 Status | Review 3 Status |
|-------|-----------------|-----------------|-----------------|
| Course assumed I write production code | Complaint | Partially addressed | RESOLVED |
| Needed QA-specific use cases | Complaint | Addressed | RESOLVED |
| Needed clearer learning path | Complaint | Fully addressed | RESOLVED |
| Weeks 5-6 too advanced | Complaint | Partially addressed | RESOLVED with QA tracks |

### Issues From Review 2

| Issue | Review 2 Status | Review 3 Status |
|-------|-----------------|-----------------|
| Week 4 exercises still developer-focused | Complaint | RESOLVED - QA track added |
| Prerequisites and success metrics dev-focused | Complaint | RESOLVED - QA sections added |
| Missing QA-specific terms in glossary | Minor | Still minor - not critical |

### New Issues Found

1. **Minor:** Week 7 (Plugins) QA track exists but is very brief. Since QA is told to "skip" this week, it's acceptable.

2. **Minor:** The Week 4 QA track references `examples/homeowner-setup` for Exercise 1, but the example structure has `property-manager` with the actual test gaps. The instructions could be clearer about which example to use.

3. **Very Minor:** The glossary still doesn't include "regression testing," "smoke testing," or "test pyramid" - but these aren't critical for course completion.

---

## Session Log

### Commands Executed

```bash
# Week 4 QA Exercise - Coverage Analysis
cd /courses/ai-101-claude-code/sessions/week-4/examples/property-manager
claude --print "As a QA engineer, read Services/PropertyService.cs and Tests/PropertyServiceTests.cs then analyze..."

# Output: Detailed coverage gap analysis with prioritized findings
# - UpdatePropertyAsync: 0% coverage (HIGH risk)
# - SearchPropertiesAsync: Missing edge cases (MEDIUM risk)
# - Generated test code with FluentAssertions

# Week 5 QA Exercise - Command Evaluation
cd /courses/ai-101-claude-code/sessions/week-5/examples/violation-audit-api
claude --print "As a QA engineer testing this project's commands, evaluate the /violation-report command..."

# Output: QA evaluation with edge cases, test scenarios, and recommendations
# - Identified input validation gaps
# - Suggested normal/edge/error test cases
# - Found underlying service bugs that affect command output
```

### What Worked Well

1. Claude understood my QA perspective and generated appropriate test-focused output
2. The coverage gap analysis matched what I'd expect from a QA code review
3. Generated tests followed Arrange-Act-Assert pattern correctly
4. Edge case identification was thorough

### What Could Be Better

1. Session continuity across exercises would be nice (using --session-id)
2. Some exercises reference example folders that don't exactly match the instructions

---

## Recommendations

### For Future Iterations

1. **Week 4 QA Track Polish:**
   - Update Exercise 1 to clearly reference `property-manager` example
   - Add expected output screenshots for first-time QA students

2. **Glossary Enhancement:**
   - Add: regression testing, smoke testing, test pyramid, exploratory testing
   - Minor priority but would complete the QA terminology

3. **Week 7 QA Track:**
   - Either expand the track or explicitly state "Skip entirely for QA"
   - Current state is fine but inconsistent with other weeks

### For QA Students

**My recommended path through the course:**

| Week | Priority | Time | Notes |
|------|----------|------|-------|
| 1 | Must Do | 1 hr | Setup |
| 2 | Must Do | 1.5 hr | Prompting - applies to test cases |
| 4 | **CRITICAL** | 2.5 hr | Use QA track, not main README |
| 5 | Light | 1 hr | QA track - consumer focus |
| 6 | Light | 1 hr | QA track - hooks for test automation |
| 8 | Useful | 1.5 hr | CI/CD test integration |
| 9 | Must Do | 3-4 hr | Option D capstone |

**Total: ~12 hours** (vs 18+ hours for developer track)

---

## Final Assessment

### What I Gained

1. **Confidence:** I can use Claude Code effectively for QA work
2. **Skills:** Coverage analysis, test generation, edge case identification
3. **Integration:** Understanding how to work with developers using shared AI tools
4. **Value:** My QA expertise (specifying correct behavior) is amplified, not replaced

### Would I Recommend This Course to Other QA Engineers?

**Yes, absolutely.**

With the QA tracks now in place, this course is genuinely useful for QA professionals. Week 4 is worth the price of admission alone. The course no longer makes me feel like an outsider trying to fit into a developer-shaped box.

### Rating: 9/10

**Breakdown:**
- Content relevance for QA: 9/10
- Exercise quality: 9/10
- Learning path clarity: 10/10
- Practical applicability: 9/10
- Documentation polish: 8/10

**The 1-point deduction:** Minor inconsistencies in exercise instructions and glossary gaps. Not blockers, just polish items.

---

## Summary of Ratings Across Reviews

```
Review 1:  7.5/10  [=========-]  "Worthwhile with modifications"
Review 2:  8.0/10  [=========- ]  "Significant improvements, Week 4 needs QA track"
Review 3:  9.0/10  [==========]  "Genuinely QA-friendly, recommend with confidence"
```

The course listened to feedback and fixed the core issues. That's what good continuous improvement looks like.

---

**Submitted by:** Jordan, QA Engineer
**Date:** January 23, 2026

*"From feeling like an outsider to feeling like the course was built for me. That's the transformation."*
