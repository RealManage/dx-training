# RealManage AI 101: Claude Code - Review 3

**Student:** Sam (Senior Developer, 8 years experience)
**Review Date:** 2026-01-23
**Previous Reviews:** 2026-01-23 (Review 2), 2026-01-23 (Review 1)
**Review Focus:** Deep dive into course structure, CLI testing, and developer track content

---

## Executive Summary

Third pass through this course. I've now done a thorough code review of the curriculum itself, tested the headless Claude CLI on actual exercises, and examined the answer keys and reference materials.

**Previous Rating (Review 1):** 8.5/10 (B+)
**Previous Rating (Review 2):** 3.5/5 (7/10 equivalent)
**Updated Rating:** 4/5 (8/10)
**Change:** +0.5 from Review 2

The course has matured. The decision trees are useful, answer keys are comprehensive, and the CodeReviewPro exercise is legitimately challenging (28+ issues to find). The TDD content is solid. What keeps this from a higher rating: some inconsistencies in week numbering/structure, occasional build warnings in examples, and the content could still use tightening for senior devs.

---

## Week-by-Week Experience

### Week 0-2: Foundations

**Skipped.** As a senior dev, I jumped to Week 3 per the "Experienced Developer Track" suggestion in Review 2. These weeks are fine for juniors but unnecessary for someone who's been prompting AI tools for years.

**Note:** The main README now has role-based track tables (lines 7-12), which is helpful for navigation.

### Week 3: Tactical Planning & Code Review Excellence

**What I tested:** The CodeReviewPro exercise in `/sessions/week-3/examples/codereview-pro/`

**CLI Test Results:**

```bash
claude --print "As a senior code reviewer, analyze LateFeeCalculator.cs for issues..."
```

Claude found 4 CRITICAL issues, 7 WARNINGs, and 5 SUGGESTIONs. I cross-referenced against the answer key (`sessions/week-3/solutions/codereviewpro-answers.md`) - comprehensive coverage of 28+ issues across Security, Performance, Logic Bugs, and Code Quality.

**The Good:**

- Answer key is detailed with file paths, line numbers, and fixes
- Success levels (Bronze/Silver/Gold/Diamond) give clear targets
- The exercise is actually challenging - not just obvious bugs

**Issues Found:**

- Build warning in project when running `dotnet build`: `CS7022: The entry point of the program is global code` in Week 5 example
- Class typo `LateFeecalculator` vs `LateFeeCalculator` is intentional but could confuse students not reading carefully

### Week 4: Test-Driven Development with Claude

**What I tested:** The homeowner-setup example project

**CLI Test Results:**

```bash
dotnet build  # Succeeded, 0 warnings, 0 errors
claude --print "Using TDD, write a failing test for a HomeownerService..."
```

Claude generated appropriate xUnit tests with FluentAssertions. The test naming convention (`MethodName_StateUnderTest_ExpectedBehavior`) was followed correctly.

**The Good:**

- Projects build cleanly (0 warnings)
- TDD content is dogmatic about tests first - as it should be
- "Advanced Techniques" section (lines 426-535) addresses my previous complaint about one-size-fits-all approach
- Prime Directives concept is clever for experienced devs

**Issues Found:**

- Week 4 README references `[QA Track](./tracks/qa.md)` but the track system isn't obvious from main README navigation
- The "Why 80-90% and not 100%?" link to troubleshooting guide is helpful

### Week 5: Commands & Basic Skills

**What I tested:** The violation-audit-api example project

**Structure verified:**

```
.claude/
  commands/violation-report.md
  commands/late-fee.md
  skills/late-fee-report/SKILL.md
  skills/late-fee-report/fee-schedule.txt
  agents/violation-analyst.md
  settings.json
```

**The Good:**

- Clear distinction between commands (simple) and skills (with supporting files)
- Decision Trees doc covers when to use what
- Developer track at `tracks/developer.md` is concise

**Issues Found:**

- Build warning: `CS7022` in the project
- Week 5 is titled "Commands & Skills" in main README but "Commands & Basic Skills" in session README - minor inconsistency

### Week 6: Agents & Hooks - Developer Track

**What I reviewed:** `sessions/week-6/tracks/developer.md`

**The Good:**

- Comprehensive hook examples (auto-run tests, block dangerous ops, SOC 2 audit)
- Permission modes table is clear
- Good balance of security-focused hooks

**Issues Found:**

- Week 6 renamed from "Plugins" (original) to "Agents & Hooks" (current) - good change
- Hook variable reference (`$TOOL_NAME`, `$TOOL_INPUT`, etc.) is helpful

### Week 7: Plugins

**What I reviewed:** Main README and `tracks/developer.md`

**The Good:**

- Plugin architecture is well documented
- Directory structure examples are clear
- `--plugin-dir` local testing workflow is practical

**Issues Found:**

- Marketplace still feels conceptual - no concrete examples of published plugins
- The progression Commands -> Skills -> Plugins is logical but could be emphasized more

### Week 8: Real-World Automation & CI/CD - Developer Track

**What I reviewed:** `sessions/week-8/tracks/developer.md`

**The Good:**

- GitLab CI/CD examples are production-ready
- Cost optimization section addresses the token cost elephant
- Cross-functional skills (support, PM, engineering) show real-world applicability
- Token estimation table is useful

**Issues Found:**

- Pricing tables may be outdated (I flagged this in Review 2 - still not addressed with "last verified" dates)
- Missing error handling in CI/CD examples (timeouts, retries, cost caps)

### Week 9: Capstone Hackerspace

**What I reviewed:** Full capstone structure with 6 options (A-F)

**The Good:**

- Role-specific options (Developer A/B/C, QA D, PM E, Support F)
- Success criteria are measurable and specific
- 90-minute time constraint is realistic
- Evaluation rubric is fair

**Issues Found:**

- Custom option (G) requires instructor approval - good guardrail
- Developer track guidance at `tracks/developer.md` is helpful but brief

---

## Comparison to Previous Reviews

### Issues Previously Raised

| Issue | Review 1 Status | Review 2 Status | Current Status |
| ----- | --------------- | --------------- | -------------- |
| Missing answer keys | Raised | Resolved | Still resolved |
| Missing decision trees | Raised | Resolved | Still resolved |
| Filler content for seniors | Raised | Partially addressed | Minor improvement |
| Linear curriculum assumption | Raised | Partially addressed | Role tracks help |
| Missing failure mode docs | Raised | Not addressed | Not addressed |
| Pricing may be outdated | Not raised | Raised | Not addressed |
| CI/CD missing error handling | Not raised | Raised | Not addressed |
| Build warnings in examples | Not raised | Not raised | NEW ISSUE |

### Improvements Since Review 2

1. **Developer tracks exist for Weeks 5, 6, 8, 9** - Can follow role-specific content now
2. **Decision trees are comprehensive** - Actually useful for quick reference
3. **Answer keys are thorough** - CodeReviewPro answer key is 200+ lines with detailed fixes
4. **Week structure is clearer** - Renamed weeks make more sense (e.g., Week 6 is now "Agents & Hooks")
5. **Troubleshooting guide expanded** - Skills and Hooks debugging sections added

### Regressions Found

1. **Build warning in Week 5 example** - `CS7022` warning about entry point; should be fixed for clean builds
2. **Minor title inconsistencies** - "Commands & Skills" vs "Commands & Basic Skills" between files

---

## Technical Observations

### What Works Well Technically

1. **Headless CLI (`--print` flag)** works smoothly for testing exercises
2. **Example projects build and run** on .NET 10
3. **Claude correctly identifies intentional bugs** in CodeReviewPro
4. **TDD workflow is enforced** through project structure (Tests folders included)

### Technical Issues Found

1. **Build Warning CS7022** in multiple projects (Week 5 violation-audit-api)
2. **No .editorconfig** in example projects - could enforce consistent coding style
3. **Missing global.json** - .NET version not pinned in some examples

---

## Recommendations

### High Priority

1. **Fix build warnings in example projects** - Clean builds should be the standard
2. **Add "last verified" dates to pricing tables** - Model pricing changes frequently
3. **Add error handling to CI/CD examples** - Timeout, retry, cost cap patterns

### Medium Priority

1. **Standardize week titles** across main README and session READMEs
2. **Add global.json** to all example projects for consistent .NET version
3. **Create "Experienced Developer Fast Track"** section at top of main README

### Nice to Have

1. **Add decision tree for "when to bail on Claude"** - Recognize circular conversations
2. **Include anti-pattern section in Week 8** - Common cost mistakes
3. **Video walkthroughs** for complex exercises (CodeReviewPro, Capstone)

---

## Verdict: From Skeptic to... Still Skeptical But Impressed

Three passes through this course. Here's where I land:

**What convinced me:**

- TDD integration is genuinely useful (tests as specs prevent hallucinations)
- Plan mode for code reviews saves real time
- Decision trees are the kind of quick reference I actually use
- Answer keys allow self-validation without instructor dependency

**What I'm still skeptical about:**

- Plugin marketplace feels like vaporware
- CI/CD examples lack production-grade error handling
- Pricing tables will age quickly without maintenance

**Would I use Claude Code after this?** Yes, I already do. TDD + Claude is my default workflow for new features.

**Would I recommend this course to other senior devs?** Yes, with the "Experienced Developer Track" approach:

1. Read Decision Trees (30 min)
2. Week 3 - Code Review exercise (1 hour)
3. Week 4 - TDD content + reference implementations (1 hour)
4. Week 8 - CI/CD integration (1 hour)
5. Capstone as needed

Total: ~4 hours instead of 18+. Worth it.

---

## Session Log

| Week | Activity | Commands Run | Notes |
| ---- | -------- | ------------ | ----- |
| 3 | CodeReviewPro test | `claude --print "review LateFeeCalculator..."` | Found 16/28+ issues |
| 4 | TDD exercise | `dotnet build`, `claude --print "write test..."` | Clean build, good output |
| 5 | Project structure | `dotnet build` | Warning CS7022 found |
| N/A | Course structure | File reads, glob searches | Verified all weeks exist |

---

## Final Score: 4/5 Stars (8/10)

**Why not 5 stars:**

- Some build warnings persist
- Pricing/version freshness not addressed
- CI/CD examples missing error handling

**Why not 3 stars:**

- Decision trees genuinely useful
- Answer keys comprehensive
- TDD content is solid
- Developer tracks exist and work

**Progress from Review 1:** B+ (8.5/10) -> 3.5/5 (7/10) -> 4/5 (8/10)

The course has stabilized. The core content is good. The reference materials are useful. For senior devs willing to skip the basics and focus on Weeks 3, 4, and 8, this is a worthwhile 4-hour investment.

---

**Reviewed by:** Sam, Senior Developer
**Experience:** 8 years at RealManage
**Bias:** Skeptical of AI tools, high standards for real-world applicability
**Verdict:** Use it, but verify everything

---

*P.S. - I've now done three reviews of this course. If the team wants more feedback, they know where to find me. The core complaint remains: make it faster for senior devs to get to the good stuff. The role tracks help, but we need a "Skip to Week 3" button.*
