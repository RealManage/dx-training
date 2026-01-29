# Code Review Summary: MR !4

**Title:** AI 101 Course Completion: Expand from 4 Weeks to Full 10-Week Curriculum (Weeks 0-9)

**Author:** cali.lafollett
**Reviewer:** edgar.sanchez
**Review Date:** 2026-01-23
**MR URL:** <https://gitlab.com/therealmanage/tools/dx/dx-training/-/merge_requests/4>

---

## Overview

This MR expands the AI 101 Claude Code course from a 4-week curriculum (Weeks 1-4) to a full 10-week program (Weeks 0-9). Key changes include:

- **6 new weeks** of content (Weeks 0, 5, 6, 7, 8, 9)
- **Role-specific tracks** for Developers, QA Engineers, and Product Managers
- **12 student feedback documents** simulating course reviews from 4 personas across 3 review passes
- **New resources**: glossary, decision trees, quick-start guides, PM toolkit plugin
- **Technical updates**: global.json additions, GitHub → GitLab CI migration, CLI docs to v2.1.17
- **Removed**: API cost documentation (subscription model - no per-token costs)
- **Replaced**: `/cost` references with `/usage`

**Diff Size:** ~869KB

---

## Issues by Severity

### CRITICAL

_None identified._

---

### HIGH

_None identified._

---

### MEDIUM

| # | Issue | Location | Description | Recommendation |
| --- | ----- | -------- | ----------- | -------------- |
| M1 | Absolute file paths in student reviews | `docs/course-feedback/student-*-review-*.md` | Reviews reference paths like `/home/calilafollett/repos/dx-training-student-1/courses/...` which are machine-specific and will confuse readers. | Replace with relative paths: `courses/ai-101-claude-code/...` |
| M2 | Persona consistency error | `student-1-alex-junior-dev-review-*.md` | Alex's experience level varies: "2 years" in Review 1, "1 year" in Review 2, back to "2 years" in Review 3. | Standardize to "2 years C#/.NET experience" across all documents. |
| M3 | Coverage target verification needed | Course materials | Student reviews correctly identify 95% vs 80-90% inconsistency. MR claims this is fixed but verification needed. | Search all course files for "95%" coverage references and ensure consistency at 80-90%. |
| M4 | Unverified test plan claims | MR Description | Claims "All example projects build without warnings" and "No broken internal links" cannot be verified from diff alone. | Run automated builds and link checker before merge. Document results in MR comments. |

---

### LOW

| # | Issue | Location | Description | Recommendation |
| --- | ----- | -------- | ----------- | -------------- |
| L1 | Future dates in student reviews | All `student-*-review-*.md` files | Reviews dated "January 22-23, 2026" - while clearly fictional, could cause minor confusion. | Consider using relative dates ("Week 1 of course") or obviously fictional past dates. |
| L2 | PM Toolkit plugin visibility | Week 7 content | MR description mentions "PM Toolkit plugin" but implementation details not visible in reviewed diff portions. | Verify plugin is properly documented with usage examples. |
| L3 | Fast Track documentation | Resources | "Experienced Developer Fast Track (4-hour path)" mentioned in MR description but implementation not verified. | Confirm fast track guide exists at expected location with clear instructions. |
| L4 | QA track glossary gaps | `resources/glossary.md` | Per Jordan's review, missing QA-specific terms: "regression testing", "smoke testing", "test pyramid", "exploratory testing". | Add these terms to glossary for completeness. |

---

## Strengths

1. **Thorough role-specific content** - QA and PM tracks genuinely address different learning needs rather than being superficial rebrands of developer content.

2. **Simulated student feedback is effective pedagogy** - The 12 review documents from 4 personas (Junior Dev, QA Engineer, Senior Dev Skeptic, PM) provide realistic feedback that future students will find relatable.

3. **Iterative improvement documented** - Reviews show course responding to feedback across passes, demonstrating quality improvement process.

4. **Well-structured progression** - Expanding dense Weeks 5-6 content into three weeks (Commands/Skills, Agents/Hooks, Plugins) addresses pacing concerns.

5. **Week 0 AI Foundations** - Directly addresses "terminology overload" complaint with glossary and foundational concepts.

6. **Decision trees and quick-start guides** - Practical resources that address common confusion points identified in feedback.

7. **Consistent GitLab CI migration** - Moving from GitHub Actions to GitLab CI aligns with company tooling.

---

## Test Plan Verification Status

| Item | Status | Notes |
| ---- | ------ | ----- |
| All example projects build without warnings | ⚠️ Unverified | Requires running builds |
| Week READMEs render correctly | ⚠️ Unverified | Requires markdown preview |
| Role-specific track links work | ⚠️ Unverified | Requires navigation testing |
| No broken internal links | ⚠️ Unverified | Recommend automated link checker |

---

## Recommendations

### Before Merge

1. **Fix M2** - Standardize Alex's experience level across all review documents
2. **Fix M1** - Replace absolute paths with relative paths in student reviews
3. **Verify M4** - Run builds and link checker, document results

### Post-Merge Follow-up

1. **Address L4** - Add missing QA glossary terms

---

## Verdict

**✅ Approve with minor fixes**

The content quality is high, the structure is well-thought-out, and the role-specific tracks add genuine value. The student feedback documents are a creative and effective teaching tool.

Address the MEDIUM severity items (M1, M2) before merge. M4 is a verification step to confirm the test plan claims.

---

## Sign-off

- [ ] Author has addressed feedback
- [ ] Builds verified passing
- [ ] Link checker run
- [ ] Ready to merge

**Reviewer:** Edgar Sanchez
**Date:** 2026-01-23
