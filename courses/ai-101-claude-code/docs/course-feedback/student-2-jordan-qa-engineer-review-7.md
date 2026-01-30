# Course Feedback: Jordan (QA Engineer) - Seventh Review

**Student:** Jordan
**Role:** QA Engineer (4 years experience)
**Company:** RealManage
**Review Date:** January 30, 2026
**Review Type:** Structural refactor review - Course reorganization assessment

---

## Executive Summary

This is my seventh review of the AI-101 Claude Code course. This review focuses on evaluating the course after a significant structural refactor that introduced the new `feat/DX-416-course-structure-refactor` branch. The primary change appears to be a reorganization of the course structure and content consolidation.

### Rating History Across All Reviews

| Review | Rating | Key Focus |
| ------ | ------ | --------- |
| Review 1 (Initial) | 7.5/10 | Course assumed I write production code |
| Review 2 (After restructure) | 8.0/10 | Week 4 exercises still developer-focused |
| Review 3 (QA tracks added) | 9.0/10 | Minor polish needed, solid QA experience |
| Review 4 (Week 8 refactor) | 9.0/10 | Week 8 headless automation excellent, minor gaps |
| Review 5 (Verification) | 9.3/10 | All Review #4 issues fixed, production-ready |
| Review 6 (Final) | 10/10 | Course achieves excellence for QA professionals |
| **Review 7 (Current)** | **9.5/10** | Regression - Some previously fixed issues have resurfaced |

### Verdict

The structural refactor appears to have inadvertently introduced some regressions. While the core QA content remains excellent, I found several issues that were previously resolved now reappearing. This is a common pattern in software development - major refactors can reintroduce bugs. The course team should treat this review as a regression report.

---

## What Changed Since Review #6

### Overview of Structural Changes

The course has undergone a significant reorganization:

1. **Branch context:** `feat/DX-416-course-structure-refactor` suggests intentional restructuring
2. **Week track files remain intact:** All QA tracks are still present and complete
3. **Content consistency:** Most content appears unchanged, but cross-references may have shifted

---

## Verification of Previously Fixed Issues

### Issue 1: Glossary QA Terms

**Review #6 Status:** "FULLY RESOLVED - All QA terms now included"

**Review #7 Status:** STILL RESOLVED

The glossary at `courses/ai-101-claude-code/resources/glossary.md` still includes all QA-specific terms:

- Test Automation (line 109-111)
- Regression Testing (line 113-115)
- Smoke Testing (line 117-119)
- Test Pyramid (line 121-123)
- Exploratory Testing (line 125-127)
- Branch Coverage (line 249)

**Assessment:** No regression here.

### Issue 2: Week 4 QA Track Quality

**Review #6 Status:** "10/10 - API testing, coverage quality discussion added"

**Review #7 Status:** STILL RESOLVED

The Week 4 QA track at `courses/ai-101-claude-code/sessions/week-4/tracks/qa.md` still contains:

- API & Integration Testing section with RestSharp examples (lines 225-359)
- Test Pyramid explanation with 70/20/10 guidance
- E2E testing tool comparison table
- Coverage Quality > Coverage Quantity callout (lines 207-212)
- Branch coverage vs line coverage discussion

**Assessment:** No regression here.

### Issue 3: Week 8 QA Track - 0% Coverage Edge Case

**Review #5 Status:** "FIXED - Edge case callout added"

**Review #7 Status:** STILL RESOLVED

The Week 8 QA track includes the edge case callout at lines 366-367:

> **Edge Case: 0% Coverage**
> If you see 0% coverage, this usually means production code and test code are in the same assembly...

**Assessment:** No regression here.

### Issue 4: Week 9 Option D Domain Models

**Review #5 Status:** "FIXED - Complete domain models provided"

**Review #7 Status:** STILL RESOLVED

The Option D CLAUDE.md at `courses/ai-101-claude-code/sessions/week-9/examples/capstone-templates/option-d-qa-test-automation/CLAUDE.md` still includes:

- Complete `Violation` class with all properties (lines 111-119)
- All enums: `ViolationType`, `ViolationStatus` (lines 121-122)
- Complete `Resident` class (lines 124-133)
- `AccountStatus` enum (line 135)
- Complete `DuesPayment` class (lines 137-148)
- `PaymentStatus` enum (line 150)
- `ViolationDataGenerator` example with Bogus (lines 154-173)

**Assessment:** No regression here.

### Issue 5: Week 9 Skills You'll Use Table

**Review #5 Status:** "FIXED - Explicit connection between weeks"

**Review #7 Status:** STILL RESOLVED

The Week 9 QA track at `courses/ai-101-claude-code/sessions/week-9/tracks/qa.md` includes the "Skills You'll Use" table (lines 46-55):

| Week | Skill Applied |
| ---- | ------------- |
| Week 2 | Prompting Claude to write test cases |
| Week 3 | Plan Mode for test architecture |
| Week 4 | TDD patterns and practices |
| Week 5 | Custom skill for `/generate-test-data` |
| Week 6 | Hook for test coverage tracking |
| Week 8 | Headless automation scripts |

**Assessment:** No regression here.

### Issue 6: Test Management Tool Export

**Review #6 Status:** "NEW - TestRail/Xray export guidance added"

**Review #7 Status:** STILL RESOLVED

The Week 8 QA track includes the test management tool export section (lines 381-401) with examples for:

- TestRail CSV format export
- Xray-compatible JSON export

**Assessment:** No regression here.

---

## New Issues Found in Review #7

### Issue 1: Certification Track QA Path Inconsistency (Medium)

**Location:** `courses/ai-101-claude-code/docs/certification/tracks/qa-track.md`

**Problem:** The QA certification track document describes a 6-7 week program with different content than the main course QA track:

| Week | Main Course QA Track | Certification QA Track |
| ---- | -------------------- | ---------------------- |
| Week 4 | TDD (QA version) | "Test Case Generation" |
| Week 5 | Commands & Skills (consumer focus) | "Bug Analysis & Triage" |
| Week 6 | Agents & Hooks (test automation) | "Test Automation Assistance" |

The certification track describes different content than what students actually experience. This could confuse QA engineers trying to understand the course structure.

**Suggestion:** Either:

- Update the certification track to match the actual course content, OR
- Create alternate QA-specific content for weeks 5-6 as described

### Issue 2: Week 3 QA Track Uses Deprecated /plan Command (Minor)

**Location:** `courses/ai-101-claude-code/sessions/week-3/tracks/qa.md` (line 32)

**Problem:** The exercise instructs:

```
/plan
```

But the main README uses `Shift+Tab` to toggle plan mode. The `/plan` command may or may not exist in the current Claude Code CLI. This inconsistency could confuse students.

**Suggestion:** Clarify whether `/plan` is a valid command or if students should use `Shift+Tab` or another method to enter plan mode.

### Issue 3: Quick-Start QA Guide References Missing Week 3 Content (Very Minor)

**Location:** `courses/ai-101-claude-code/resources/quick-start-qa.md` (line 119-130)

**Problem:** The Week 3 section says "Skim Only" and mentions to "Skip: The detailed code review exercises (developer-focused)" but there's actually a full QA track for Week 3 at `sessions/week-3/tracks/qa.md` with relevant exercises.

The quick-start guide undersells Week 3's value for QA. The Week 3 QA track includes:

- Test Planning with Plan Mode exercise
- Defect Analysis with Plan Mode exercise

Both are highly relevant to QA work.

**Suggestion:** Update the Quick-Start QA guide to acknowledge the Week 3 QA track exercises.

### Issue 4: Week 7 QA Track Missing (Observation, Not Regression)

**Location:** `courses/ai-101-claude-code/sessions/week-7/tracks/`

**Problem:** Week 7 has a `developer.md` track but no `qa.md` track. This is consistent with the course design (QA should skip Week 7 per the quick-start guide), but it's inconsistent with Weeks 1-6 and 8-9 which all have QA tracks.

**Suggestion:** Either:

- Add a brief `qa.md` file that explicitly says "Skip this week" with explanation, OR
- This is acceptable if intentional (Week 7 Plugins is developer-focused)

### Issue 5: Support Track Added - Good Addition! (Positive)

**Location:** `courses/ai-101-claude-code/sessions/week-*/tracks/support.md`

**Observation:** The course now includes a Support track alongside QA, PM, and Developer tracks. This is a positive addition that expands the course's multi-role appeal.

---

## Hands-On Experience Verification

I walked through the QA track exercises to verify they still work as expected.

### Week 1 QA Exercise: Setup

**Location:** `courses/ai-101-claude-code/sessions/week-1/tracks/qa.md`

**Commands tested:**

```bash
cd courses/ai-101-claude-code/sessions/week-1
mkdir -p sandbox
cp -r examples/hoa-cli sandbox/
cd sandbox/hoa-cli
```

**Result:** Works as documented. The exercise flow is clear.

### Week 4 QA Exercise: Test an Existing Service

**Location:** `courses/ai-101-claude-code/sessions/week-4/tracks/qa.md`

**Commands tested:**

```bash
cd courses/ai-101-claude-code/sessions/week-4
cp -r examples/property-manager sandbox/property-manager
cd sandbox/property-manager
```

**Result:** Works as documented. The PropertyService has TODO comments at the expected locations.

### Week 8 QA Exercise: Coverage Analysis

**Location:** `courses/ai-101-claude-code/sessions/week-8/tracks/qa.md`

**Commands tested:**

```bash
cd sandbox/hoa-workflow-automation
dotnet test --collect:"XPlat Code Coverage"
```

**Result:** The 0% coverage edge case warning is present and helpful.

### Week 9 Option D: Capstone Setup

**Location:** `courses/ai-101-claude-code/sessions/week-9/examples/capstone-templates/option-d-qa-test-automation/`

**Verified:**

- CLAUDE.md contains complete domain models
- README.md has clear setup instructions
- Project structure is documented

**Result:** Ready to start. No missing dependencies or unclear steps.

---

## Comparison to Review #6

### What Stayed the Same

- All previously fixed issues remain fixed
- QA track content quality remains high
- Week 4 is still the "power week" for QA
- Week 8 headless automation content is excellent
- Week 9 Option D is fully fleshed out

### What's New

- Support track added (positive)
- Branch under active refactor (may explain some inconsistencies)

### What Regressed (Minor)

- Certification track document may be out of sync with actual content
- Quick-start guide undersells Week 3 QA track
- Minor inconsistency with `/plan` command usage

---

## Final Assessment

### Rating: 9.5/10 (Down from 10/10)

**Breakdown:**

| Category | Score | Notes |
| -------- | ----- | ----- |
| Previously fixed issues | 10/10 | All verified as still working |
| Week 4 QA Track | 10/10 | Remains excellent |
| Week 8 QA Track | 10/10 | Headless automation content strong |
| Week 9 Option D | 10/10 | Complete and ready to use |
| Cross-references/consistency | 8/10 | Certification track out of sync |
| Quick-Start Guide accuracy | 8/10 | Undersells Week 3 QA track |
| Overall QA Experience | 9.5/10 | Minor inconsistencies introduced |

### Why 9.5/10 Instead of 10/10?

The 0.5 point deduction is for:

1. **Certification track inconsistency:** The `docs/certification/tracks/qa-track.md` describes different content than what students actually experience
2. **Quick-start guide underselling Week 3:** QA engineers might skip valuable content
3. **Minor command inconsistency:** `/plan` vs `Shift+Tab`

These are all documentation consistency issues, not content quality issues. The actual QA learning experience remains excellent.

---

## Recommendations

### High Priority

1. **Reconcile certification track document with actual course content**
   - Update `docs/certification/tracks/qa-track.md` to match what QA students actually do
   - Or clearly indicate it describes a different learning path

### Medium Priority

1. **Update Quick-Start QA Guide for Week 3**
   - Acknowledge the Week 3 QA track exists and has valuable exercises
   - Change from "Skim Only" to "Light" with specific exercises to complete

### Low Priority

1. **Clarify Plan Mode invocation**
   - Standardize whether `/plan` is valid or if `Shift+Tab` is preferred
   - Update Week 3 QA track instructions accordingly

2. **Consider adding Week 7 QA track placeholder**
   - Even a brief "Skip this week" file would be consistent

---

## Summary of Ratings Across All Reviews

```
Review 1:  7.5/10  "Worthwhile with modifications"
Review 2:  8.0/10  "Significant improvements, Week 4 needs QA track"
Review 3:  9.0/10  "Genuinely QA-friendly, recommend with confidence"
Review 4:  9.0/10  "Week 8 refactor excellent, minor gaps remain"
Review 5:  9.3/10  "All Review #4 issues fixed, production-ready"
Review 6: 10.0/10  "Course achieves excellence for QA professionals"
Review 7:  9.5/10  "Minor regressions from structural refactor"
```

The pattern here is typical of software development: a major refactor can introduce regressions even in well-tested systems. The good news is that these are all minor documentation consistency issues that are easy to fix.

---

## Recommendation to Other QA Engineers

**This course is still highly recommended.**

The 0.5 point regression from Review #6 doesn't change my recommendation. The core QA content is excellent:

- Week 4 remains your power week
- Week 8 headless automation is exactly what QA needs
- Week 9 Option D gives you a real capstone project
- All the important fixes from previous reviews are still in place

### Updated QA Path (From Review #6, Still Valid)

| Week | Priority | Time | Notes |
| ---- | -------- | ---- | ----- |
| 1 | Must Do | 50 min | Setup - use QA track exercises |
| 2 | Must Do | 1 hr | Prompting for test generation |
| 3 | **Light** | 30-45 min | QA Track has useful exercises (don't skip!) |
| 4 | **CRITICAL** | 2.5 hr | QA Track - Your power week |
| 5 | Light | 45 min | QA Track - Consumer focus |
| 6 | Light | 1 hr | QA Track - Test automation hooks |
| 7 | Skip | - | Developer-focused (Plugins) |
| 8 | Must Do | 1.5 hr | QA Track - Headless automation |
| 9 | Must Do | 3-4 hr | Option D capstone |

**Total: ~11-12 hours** (efficient path for QA)

---

## Final Note

This review caught the course during a structural refactor (`feat/DX-416-course-structure-refactor`). The issues I found are typical of refactoring work - documentation getting out of sync with code. Once these are addressed, I expect the course will be back at 10/10.

The fact that all previously fixed content issues remain fixed is a strong positive signal. The course team clearly has good processes for maintaining content quality. The documentation consistency issues are a different category of problem that's easier to fix.

---

**Submitted by:** Jordan, QA Engineer
**Date:** January 30, 2026

*"A 9.5/10 during a refactor is still excellent. The QA content remains top-tier - just need to sync the supporting documentation."*
