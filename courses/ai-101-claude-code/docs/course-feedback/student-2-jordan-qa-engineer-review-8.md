# Course Feedback: Jordan (QA Engineer) - Eighth Review

**Student:** Jordan
**Role:** QA Engineer (4 years experience)
**Company:** RealManage
**Review Date:** January 30, 2026
**Review Type:** Post-fix verification - Certification track and /plan clarification

---

## Executive Summary

This is my eighth review of the AI-101 Claude Code course. This review focuses on verifying that the two main issues I raised in Review #7 have been fixed:

1. **Certification track QA document was out of sync** with actual course content
2. **`/plan` vs `Shift+Tab`** confusion in Week 3

### Rating History Across All Reviews

| Review | Rating | Key Focus |
| ------ | ------ | --------- |
| Review 1 (Initial) | 7.5/10 | Course assumed I write production code |
| Review 2 (After restructure) | 8.0/10 | Week 4 exercises still developer-focused |
| Review 3 (QA tracks added) | 9.0/10 | Minor polish needed, solid QA experience |
| Review 4 (Week 8 refactor) | 9.0/10 | Week 8 headless automation excellent, minor gaps |
| Review 5 (Verification) | 9.3/10 | All Review #4 issues fixed, production-ready |
| Review 6 (Final) | 10/10 | Course achieves excellence for QA professionals |
| Review 7 (Structural refactor) | 9.5/10 | Regression - certification track out of sync |
| **Review 8 (Current)** | **10/10** | Both issues fixed - back to excellence |

### Verdict

**Both issues from Review #7 have been completely addressed.** The certification track document now accurately reflects the actual 9-week course content, and the `/plan` vs `Shift+Tab` confusion has been clarified with an excellent note explaining both methods work identically.

---

## Issue #1: Certification Track - FIXED

### The Problem (From Review #7)

The QA certification track document at `docs/certification/tracks/qa-track.md` described different content than the actual course:

| Week | Actual Course | Old Certification Doc (Review #7) |
| ---- | ------------- | --------------------------------- |
| Week 4 | TDD (QA version) | "Test Case Generation" |
| Week 5 | Commands & Skills | "Bug Analysis & Triage" |
| Week 6 | Agents & Hooks | "Test Automation Assistance" |

### What's Fixed Now

The certification track document has been completely rewritten. It now accurately reflects the 9-week course structure:

**Track Duration Section:**

```markdown
**9 weeks (Week 0 optional)**

| Phase | Weeks | Focus |
| ----- | ----- | ----- |
| Foundations | 0-3 | Setup, prompting, plan mode for test planning |
| QA Power Week | 4 | Writing tests for existing code, coverage analysis |
| Skills & Automation | 5-6 | Commands, skills, hooks for test automation |
| Advanced | 7-8 | Plugins (skim), headless test automation |
| Capstone | 9 | Demonstrate AI-assisted testing |
```

**Week-by-Week Content Now Matches:**

- **Week 4:** Correctly labeled "TDD - QA Power Week" with accurate exercises (PropertyService, coverage gap analysis, test data with Bogus, API/integration testing)
- **Week 5:** "Commands & Skills" with "Consumer focus - using skills, not creating them"
- **Week 6:** "Agents & Hooks" with "PostToolUse hooks to auto-run tests after code changes"
- **Week 7:** Correctly marked as "Skip or skim - developer-focused week"
- **Week 8:** "Real-World Automation" with "Headless test automation scripts, CI/CD integration"

**New Differences from Developer Track Table:**

The document now includes an accurate comparison table:

| Week | Developer Track | QA Track |
| ---- | --------------- | -------- |
| Week 1 | Build CLI features | Analyze testability |
| Week 2 | Code generation prompts | Test case generation prompts |
| Week 3 | Implementation planning | Test planning, defect analysis |
| Week 4 | TDD (write tests + code) | Write tests for existing code |
| Week 5 | Create developer skills | Use skills (consumer focus) |
| Week 6 | Advanced hooks | Test automation hooks |
| Week 7 | Plugin development | Skip/skim |
| Week 8 | Headless automation | Headless test automation |
| Capstone | Code project | Test suite or automation |

**Assessment:** This is exactly what I asked for. The certification document now matches reality. A QA engineer reading this document will know exactly what to expect from each week.

---

## Issue #2: /plan vs Shift+Tab - FIXED

### The Problem (From Review #7)

The Week 3 QA track instructed students to use `/plan` but the main README used `Shift+Tab`. This inconsistency could confuse students about which method to use.

### What's Fixed Now

The Week 3 README now includes an explicit clarification at lines 89-92:

```markdown
**Activating Plan Mode:**

- **Shift+Tab**: Toggle through modes (auto -> step -> plan -> auto...)
- **`/plan`**: Jump directly to plan mode from any mode

> **Note:** Both methods work identically. Use whichever feels more natural.
> Track exercises use `/plan` for clarity in prompts, but `Shift+Tab` is
> often faster in practice.
```

This is perfect! It:

1. Documents BOTH methods clearly
2. Explains they work identically
3. Justifies why track exercises use `/plan` (clarity in written prompts)
4. Recommends `Shift+Tab` for real-world use (faster in practice)

**Assessment:** No more confusion. Students now understand they can use either method and why the examples show `/plan`.

---

## Review #7 Issues - Status Summary

| Issue | Review #7 Status | Review #8 Status |
| ----- | ---------------- | ---------------- |
| Certification track out of sync | High Priority | **FIXED** |
| `/plan` vs `Shift+Tab` confusion | Low Priority | **FIXED** |
| Quick-start guide undersells Week 3 | Medium Priority | Not checked (minor) |
| Week 7 QA track placeholder | Low Priority (optional) | Not checked (minor) |

The two main issues are resolved. The remaining items from Review #7 were minor documentation consistency issues that don't impact the QA learning experience.

---

## Comparison to Review #7

### What Changed

1. **Certification track completely rewritten** - No longer describes a fictional 6-7 week program with different content
2. **Plan mode clarification added** - Both methods now documented with clear guidance
3. **Week-by-week accuracy** - Every week in the certification doc now matches actual course content

### What Stayed the Same

- All previously fixed issues (Reviews #1-6) remain fixed
- QA track content quality remains excellent
- Week 4 remains the "power week" for QA
- Week 8 headless automation content is still top-tier
- Week 9 Option D capstone is fully fleshed out

---

## Final Assessment

### Rating: 10/10 (Back to Excellence)

**Breakdown:**

| Category | Score | Notes |
| -------- | ----- | ----- |
| Previously fixed issues | 10/10 | All verified as still working |
| Certification track accuracy | 10/10 | Now matches actual course |
| Plan mode documentation | 10/10 | Both methods clearly explained |
| Week 4 QA Track | 10/10 | Remains excellent |
| Week 8 QA Track | 10/10 | Headless automation content strong |
| Week 9 Option D | 10/10 | Complete and ready to use |
| Overall QA Experience | 10/10 | No blocking issues remain |

### Why 10/10 Again?

The 0.5 point deduction from Review #7 was specifically for:

1. Certification track inconsistency - **FIXED**
2. `/plan` vs `Shift+Tab` confusion - **FIXED**

Both issues are resolved. The course is back to excellence for QA professionals.

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
Review 8: 10.0/10  "Issues fixed - back to excellence"
```

The pattern here shows healthy software development: structural refactor introduced regressions (Review #7), team fixed them quickly (Review #8). This is how good teams operate.

---

## Recommendation to Other QA Engineers

**This course is highly recommended.**

The temporary regression in Review #7 was quickly addressed. The course team clearly:

1. Reads and acts on feedback
2. Prioritizes documentation consistency
3. Maintains high quality standards

### Updated QA Path (From Review #6, Still Valid)

| Week | Priority | Time | Notes |
| ---- | -------- | ---- | ----- |
| 1 | Must Do | 50 min | Setup - use QA track exercises |
| 2 | Must Do | 1 hr | Prompting for test generation |
| 3 | Light | 30-45 min | QA Track has useful exercises |
| 4 | **CRITICAL** | 2.5 hr | QA Track - Your power week |
| 5 | Light | 45 min | QA Track - Consumer focus |
| 6 | Light | 1 hr | QA Track - Test automation hooks |
| 7 | Skip | - | Developer-focused (Plugins) |
| 8 | Must Do | 1.5 hr | QA Track - Headless automation |
| 9 | Must Do | 3-4 hr | Option D capstone |

**Total: ~11-12 hours** (efficient path for QA)

---

## Final Note

The fixes in this update demonstrate exactly what I want to see from course maintainers:

1. **They listened** - Both issues I raised were addressed
2. **They fixed correctly** - Not just patched, but properly rewritten with accurate content
3. **They added value** - The `/plan` clarification is better than what I suggested; it explains WHY exercises use one method while recommending another for daily use

This is a mature, well-maintained training program. As a QA engineer, I appreciate when teams respond to feedback with quality fixes rather than quick hacks.

---

**Submitted by:** Jordan, QA Engineer
**Date:** January 30, 2026

*"Back to 10/10. The course team fixed exactly what I asked for - and did it well. That's the kind of responsiveness that builds trust."*
