# Course Review: Round 6 - Sam (Senior Developer, Skeptic)

**Reviewer:** Sam, Senior Developer (8 years experience at RealManage)
**Review Date:** 2026-01-27
**Review Focus:** Final assessment and recommendation
**Previous Reviews:** Reviews 1-5 (see student-3-sam-senior-dev-skeptic-review-1/2/3/4/5.md)

---

## Executive Summary

This is my sixth and likely final review of the AI 101: Claude Code course. At this point, I have spent more time reviewing this course than most internal training deserves. But the team keeps improving it, so I keep looking.

**Previous Rating:** 4.5/5 (9/10) from Review 5
**Current Rating:** 4.5/5 (9/10)
**Change:** No change

The course has stabilized at a high level of quality. The fixes from Review #5 remain in place. One long-standing issue (pricing "last verified" dates) has been partially addressed in the troubleshooting guide. The remaining gaps are minor polish items, not structural problems.

---

## What I Reviewed This Round

### 1. Verification of Review #5 Fixes

All three issues I confirmed fixed in Review #5 remain fixed:

| Issue | Status |
| ----- | ------ |
| Plan mode consistency (Shift+Tab vs /plan) | Still consistent |
| Learning examples callout | Still present at Week 8 line 371 |
| GitLab references cleanup | Still appropriate |

No regressions found.

### 2. Outstanding Issues from Previous Reviews

#### Issue: Pricing "Last Verified" Dates

**My complaint since Review #2:**
> "Pricing tables may be outdated... Adding a 'Last verified' date..."

**Status: PARTIALLY ADDRESSED**

The troubleshooting guide now includes at line 750:

```markdown
> **Last verified:** January 2026. Run `claude doctor` to check your setup.
```

This addresses the setup/version verification concern. However, the model pricing tables in the decision trees and Week 8 content still lack "last verified" dates. The pricing comparison tables show:

- `courses/ai-101-claude-code/resources/decision-trees.md` - Model comparison table without pricing dates
- `courses/ai-101-claude-code/sessions/week-8/README.md` - Model selection table without pricing dates

**My Updated Assessment:**

Looking at this more carefully, I realize the course has **intentionally removed specific pricing from the decision trees**. The model comparison table now shows:

| Factor | Sonnet | Opus |
| ------ | ------ | ---- |
| Speed | Fast | Moderate |
| Code quality | Great | Excellent |
| Complex reasoning | Strong | Best |
| Context handling | Great | Excellent |
| Best for | Daily work | Hard problems |

This is actually the RIGHT approach. Instead of dollar figures that go stale, the course uses relative comparisons. The "last verified" dates are less critical when you're not quoting specific prices.

**Verdict: Appropriately addressed through design choice. Remove from my outstanding issues list.**

### 3. Current State of Developer Track Content

The Developer Track for Weeks 5-8 remains solid:

**Week 5 (Commands & Skills):**

- Clear distinction between commands and skills
- Decision tree reference is helpful
- Example projects are appropriate

**Week 6 (Agents & Hooks):**

- Permission modes well documented
- Hooks for SOC 2 compliance are practical
- Agent definitions are clear

**Week 7 (Plugins):**

- Plugin architecture documented
- Local development workflow (`--plugin-dir`) is practical
- Marketplace still feels conceptual (acceptable for internal training)

**Week 8 (Real-World Automation):**

- Headless CLI patterns are well-documented
- Anti-patterns section is excellent
- The "learning examples" callout remains appropriately placed
- Cross-functional skills are practical

**Week 9 (Capstone):**

- Role-specific options (A-F) are appropriate
- Plan mode instructions are consistent (Shift+Tab throughout)
- Success criteria are measurable

### 4. Technical Consistency Check

I verified several technical elements:

**Project Files:**

- All `.csproj` files target `net10.0` - Consistent
- Package versions are modern (xUnit 2.9.3, FluentAssertions 8.3.0)
- NuGet packages are reasonably current

**CLAUDE.md Files:**

- Intentional bugs clearly documented in example projects
- Domain rules accurate (10% monthly compound, 30 days grace, etc.)
- HOA-specific context appropriate

**Documentation:**

- Course version listed as 1.0.0 with "Last Updated: January 2025" (line 663 of main README)
- This is slightly outdated since we're in January 2026, but acceptable for a stable release

---

## Any New Issues Found?

### Minor Issues (Not Rating-Impacting)

1. **Course Version Date**
   - Main README line 663: "**Course Version:** 1.0.0 | **Last Updated:** January 2025"
   - Should probably be January 2026
   - Minor documentation hygiene issue

2. **Week 8 Developer Track Resume Example**
   - Line 205 shows `--resume abc123` as an example session ID
   - Week 8 main README line 282 shows `--resume <session-id>`
   - Both are correct; just different example formatting
   - Noted in Review #5, still present, still minor

3. **No Production Hardening Appendix Yet**
   - I suggested this in Review #5 as "nice to have"
   - Still not present
   - Not blocking; the in-line callout is sufficient for a training course

**None of these warrant a rating reduction.**

---

## What the Course Gets Right (Consistently)

Looking back at my six reviews, here's what has remained consistently good:

### 1. TDD Emphasis

The course is dogmatic about tests first. This is correct. The "Prime Directives" pattern for encoding TDD rules in CLAUDE.md is clever and practical.

### 2. CLAUDE.md Context System

Still the killer feature. Copilot doesn't have this. The hierarchical memory system (project vs user) is well-explained.

### 3. Plan Mode Integration

Shift+Tab for plan mode is now consistently documented across all weeks. The decision tree for when to use plan mode is practical.

### 4. Decision Trees

These remain the most useful reference material in the course. Quick, actionable, bookmarkable.

### 5. Realistic Domain Examples

HOA violations, late fees, board meetings. Not todo apps. This makes the learning stick.

### 6. Role-Based Tracks

Developer, QA, PM, and Support all have appropriate content. The "Experienced Developer Fast Track" (lines 15-25 of main README) is exactly what senior devs need.

---

## Score Progression Summary

| Review | Score | Key Finding |
| ------ | ----- | ----------- |
| 1 | 8.5/10 | Good foundation, missing answer keys |
| 2 | 7/10 | Raised standards, found gaps |
| 3 | 8/10 | Improvements recognized |
| 4 | 8.5/10 | Week 8 refactor to headless praised |
| 5 | 9/10 | Fixes verified, polish achieved |
| 6 | 9/10 | Stable at high quality |

The course has plateaued at 9/10. This is a good plateau.

---

## Why 9/10 and Not Higher?

**What would push it to 9.5/10:**

- Production Hardening appendix with full error handling patterns
- Video walkthroughs for complex exercises (CodeReviewPro, headless automation)
- Real published plugin examples from RealManage projects

**What would push it to 10/10:**

- All of the above, plus
- Case studies from teams using Claude Code in production at RealManage
- Measured productivity gains with actual data
- Community contributions section with real user tips

**Is 10/10 realistic?**
For internal corporate training? No. 9/10 is excellent. The course has exceeded my initial expectations by a significant margin.

---

## Recommendations

### No High Priority Items Remain

The course is in a mature state. The remaining suggestions are enhancements, not fixes.

### Medium Priority (Nice to Have)

1. **Update Course Version Date**
   - Change "January 2025" to "January 2026" in main README
   - 5-second fix

2. **Consider Production Hardening Appendix**
   - Consolidate error handling, timeout, retry patterns
   - Optional reading for those deploying to production
   - Estimated effort: 2-4 hours

3. **Add Video Content (Stretch)**
   - 2-3 minute walkthrough of headless automation
   - CodeReviewPro exercise demonstration
   - Capstone demo video

### Low Priority (Polish)

1. **Standardize Example Formatting**
   - `--resume abc123` vs `--resume <session-id>`
   - Purely cosmetic

2. **Real Plugin Examples**
   - Link to actual RealManage plugins if they exist
   - Helps Week 7 feel less conceptual

---

## Final Assessment: The Skeptic's Endorsement

Six reviews. Here's where I land:

**Should senior developers take this course?**
Yes. Use the Experienced Developer Fast Track:

- Decision Trees (30 min)
- Week 3: Plan Mode + Code Review (1 hour)
- Week 4: TDD with Claude (1 hour)
- Week 8: Headless Automation (1 hour)
- Week 9: Capstone if desired (90 min)

Total: 4-5 hours for material that will improve your daily workflow.

**Should RealManage continue investing in this course?**
Yes. It's the best internal training I've seen at this company. The team reads feedback and acts on it. That's rare.

**Would I use Claude Code after this course?**
Already do. Daily. TDD + Claude is my default for new features. Plan mode for complex code reviews. Headless automation for batch processing.

**Am I still skeptical of AI tools?**
Yes. But Claude Code has earned my trust through practical value. This course teaches how to use it effectively without the hype.

---

## The Bottom Line

The course is done. It's good. Ship it and maintain it.

After six reviews, I have no structural complaints left. The remaining suggestions are polish. The team should focus on:

1. Keeping the content current (SDK versions, CLI flags)
2. Collecting success stories from teams using Claude Code
3. Building the plugin ecosystem with real examples

The hard work is complete. Now it's about sustainability.

---

## Session Log

| Activity | Result |
| -------- | ------ |
| Review #5 fix verification | All fixes remain in place |
| Pricing date issue review | Resolved through design choice (relative comparisons) |
| Developer track review (Weeks 5-9) | Solid, no issues |
| Technical consistency check | .NET 10 consistent, packages current |
| New issue search | Minor polish items only |

---

## Final Score: 4.5/5 Stars (9/10)

### Why 9/10?

**What's excellent:**

- TDD integration prevents hallucinations
- Plan mode consistently documented
- Decision trees are actually useful
- Role-specific tracks work well
- Team demonstrates continuous improvement

**What's not quite 10/10:**

- No production hardening appendix
- No video content
- Plugin marketplace still conceptual
- Course version date slightly stale

### Recommendation

**Certified for Production Use**

This course is ready for organization-wide rollout. Senior developers should use the fast track. Everyone else should complete the full curriculum.

The skeptic endorses.

---

**Reviewed by:** Sam, Senior Developer
**Experience:** 8 years at RealManage
**Bias:** Skeptical of AI tools, high standards for production-readiness
**Final Verdict:** 9/10. The course delivers. Use it.

---

*P.S. - Six reviews. I'm officially done being skeptical about THIS course. The content is solid, the team listens, and it keeps improving. That's the best I can say about any internal training. Now let me get back to actually USING Claude Code.*

*P.P.S. - If someone on the team is tracking my feedback: you did good work. The progression from Review 1 to Review 6 shows real commitment to quality. Credit where it's due.*

---

## Addendum: Rating Reconsideration

**Date:** January 27, 2026

The course team pointed out that my critique "No production hardening appendix" was incorrect.

**What I Found:**

1. `resources/production-hardening.md` exists - 401 lines of solid, production-ready content
2. It's now properly linked from:
   - Week 8 Developer Track (Quick Resources section)
   - Week 8 README
   - Developer Quick Start Guide
3. Content includes exactly what I'd want: `set -euo pipefail`, timeout handling, retry with exponential backoff, rate limiting, cost controls, structured logging, batch processing patterns

**My Verdict:**

I was wrong. The production hardening appendix exists and is comprehensive. Either it was already there and I missed it, or they added it and properly linked it. Either way, this addresses my primary technical concern.

**Revised Rating: 9.5/10**

I still won't go to a perfect 10 because no course is perfect, but this removes my main critique. The course now has proper guidance for production deployment where reliability and cost control actually matter.

Good work, course team. I stand corrected. 💯
