# Course Feedback: Jordan (QA Engineer) - Fifth Review

**Student:** Jordan
**Role:** QA Engineer (4 years experience)
**Company:** RealManage
**Review Date:** January 27, 2026
**Review Type:** Fifth review - Verification of fixes from Review #4

---

## Executive Summary

This is my fifth and final review of the AI-101 Claude Code course. This review specifically focuses on verifying the fixes from my Review #4 issues and evaluating additional changes.

### Rating History Across All Reviews

| Review | Rating | Key Focus |
| ------ | ------ | --------- |
| Review 1 (Initial) | 7.5/10 | Course assumed I write production code |
| Review 2 (After restructure) | 8.0/10 | Week 4 exercises still developer-focused |
| Review 3 (QA tracks added) | 9.0/10 | Minor polish needed, solid QA experience |
| Review 4 (Week 8 refactor) | 9.0/10 | Week 8 headless automation excellent, minor gaps |
| **Review 5 (Current)** | **9.3/10** | All Review #4 issues fixed, course is production-ready |

### Verdict

All three issues I raised in Review #4 have been addressed. The course team continues to demonstrate responsiveness to QA feedback. This course is now genuinely ready for QA engineers at RealManage.

---

## Issues from Review #4: Verification Status

### Issue 1: Week 9 Option D Missing Domain Models

**Review #4 Complaint:**
> "The capstone asks me to test 'violation lifecycle, dues calculation, late fees, resident management' but the starter template doesn't include those domain models."

**Status: ✅ FIXED**

The Week 9 Option D CLAUDE.md now includes complete starter domain models:

- `Violation` class with Id, PropertyId, Type, Status, CreatedDate, ResolvedDate, Description
- `Resident` class with Id, FirstName, LastName, Email, PropertyAddress, AccountStatus
- `DuesPayment` class with Id, ResidentId, Amount, LateFee, DueDate, PaidDate, Status

All relevant enums are provided:

- `ViolationType` (Landscaping, Parking, Noise, Architectural, Other)
- `ViolationStatus` (Open, FirstNotice, SecondNotice, FinalNotice, BoardReview, Resolved)
- `AccountStatus` (Active, Inactive, Suspended, PendingApproval)
- `PaymentStatus` (Pending, Paid, Overdue, PartiallyPaid)

**Bonus:** A complete `ViolationDataGenerator` example using Bogus is also included.

**Assessment:** This is exactly what I asked for. I can now start the capstone immediately.

---

### Issue 2: Week 8 QA Track 0% Coverage Edge Case

**Review #4 Complaint:**
> "Exercise 1 doesn't mention what to do if the coverage report shows 0%"

**Status: ✅ FIXED**

The Week 8 QA track now includes a callout box in Exercise 1:

> **Edge Case: 0% Coverage**
> If you see 0% coverage, this usually means production code and test code are in the same assembly. Coverlet only tracks coverage for assemblies *different* from the test assembly. Solution: Ensure your production code is in a separate project (e.g., `HoaServices.csproj`) that the test project references.

**Assessment:** Future QA students won't be confused by the 0% coverage scenario.

---

### Issue 3: Week 9 Option D Should Reference Week 8 Headless Patterns

**Review #4 Complaint:**
> "The capstone Option D doesn't explicitly reference the headless automation skills learned in Week 8."

**Status: ✅ FIXED**

The Week 9 QA track now includes a "Skills You'll Use" table:

| Week | Skill Applied |
| ---- | ------------- |
| Week 2 | Prompting Claude to write test cases |
| Week 3 | Plan Mode for test architecture |
| Week 4 | TDD patterns and practices |
| Week 5 | Custom skill for `/generate-test-data` |
| Week 6 | Hook for test coverage tracking |
| Week 8 | Headless automation scripts |

**Assessment:** The connection between weeks is now explicit.

---

## Additional Changes Reviewed

### Week 5 PM Track Rewrite

- Now titled "PM Skill Creation Workshop"
- Four practical PM skills provided: `/release-notes`, `/meeting-actions`, `/sprint-summary`, `/user-stories`
- More hands-on, less theoretical

### Week 8 "Learning Examples" Callout

- Added note clarifying examples are for learning, not production-ready
- Sets appropriate expectations

### Week 9 Plan Mode Instructions Clarified

- Clearer instructions on entering/exiting Plan Mode
- Alternative prompt-based approach: "Use plan mode to..."

---

## Final Assessment

### Rating: 9.3/10 (Up from 9.0/10)

**Breakdown:**

| Category | Score | Notes |
| -------- | ----- | ----- |
| Review #4 Issue Resolution | 10/10 | All three issues fully addressed |
| Week 8 QA Track Quality | 9/10 | Excellent headless automation content |
| Week 9 Option D Clarity | 9/10 | Domain models and cross-references added |
| Documentation Polish | 9/10 | Learning example callout, plan mode clarity |
| Overall QA Experience | 9.5/10 | Genuinely QA-friendly throughout |

### Minor Items Still Outstanding (Very Low Priority)

1. Glossary missing QA-specific terms (regression testing, smoke testing, test pyramid)
2. Branch coverage not discussed alongside line coverage

These are polish items, not blockers.

---

## Summary of Ratings Across All Reviews

```
Review 1:  7.5/10  "Worthwhile with modifications"
Review 2:  8.0/10  "Significant improvements, Week 4 needs QA track"
Review 3:  9.0/10  "Genuinely QA-friendly, recommend with confidence"
Review 4:  9.0/10  "Week 8 refactor excellent, minor gaps remain"
Review 5:  9.3/10  "All Review #4 issues fixed, production-ready"
```

---

## Recommendation

**Would I recommend this course to other QA engineers?**

**Absolutely yes.**

The course has evolved from "developer-focused with QA afterthoughts" to "genuinely multi-role with dedicated QA experiences." All three issues from Review #4 have been fixed, and the additional improvements (Week 8 callout, Plan Mode clarity) show continued attention to quality.

**My Recommended QA Path:** Weeks 1, 2, 4 (critical), 5, 6, 8 (important), 9 - approximately 12 hours total.

---

**Submitted by:** Jordan, QA Engineer
**Date:** January 27, 2026

*"From five reviews to a 9.3/10 rating - the course listened, iterated, and delivered. That's what good continuous improvement looks like."*
