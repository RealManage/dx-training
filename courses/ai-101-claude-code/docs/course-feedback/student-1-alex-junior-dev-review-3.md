# RealManage AI 101: Claude Code - Third Review

**Student:** Alex (Junior Developer, 2 years experience)
**Review Date:** January 23, 2026
**Previous Reviews:** January 22 (7.5/10), January 23 (8.5/10)
**Review Focus:** Comprehensive evaluation of restructured 9-week course

---

## Executive Summary

This is my third time going through the AI 101 Claude Code course, and I have to say - the course has evolved significantly since my initial review. The team has addressed most of my major concerns, and the restructuring from 8 weeks to 9 weeks has made a real difference in pacing.

**Rating Comparison:**
| Review | Rating | Key Issue |
|--------|--------|-----------|
| Review 1 | 7.5/10 | Terminology overload in Weeks 5-6, missing glossary |
| Review 2 | 8.5/10 | Improvements noted, Weeks 5-6 not verified |
| Review 3 | 8.8/10 | Weeks 5-6 properly split, decision trees added |

**Overall Rating Improvement:** +1.3 points from initial review

---

## Week-by-Week Experience

### Week 0: AI Foundations (Optional)

**Status:** NEW - Directly addresses my feedback

This is exactly what I asked for in my first review! The "Week 0" concept provides:
- Clear definition of "agentic engineering" (finally!)
- Honest assessment of the "10x Developer" myth
- Realistic speedup expectations by task type (5-10x for boilerplate, 1-1.5x for novel algorithms)
- Hallucination awareness exercise - critical for safety

**What I tested:**
The Week 0 README is well-structured and doesn't assume AI knowledge. The "Expectation Calibration" exercise (rating statements 1-5) is particularly helpful for getting in the right mindset.

**Score: 9/10**

---

### Week 1: Setup & Orientation

**Status:** IMPROVED from original review

The "WHERE to Work" section with clear folder structure diagrams solved my confusion about where to put exercise files. The sandbox approach is still brilliant.

**What I tested:**
```bash
claude --print "What does this example application do?"
```
Got clear, concise output explaining the HOA Violation Tracker CLI.

**Bug identification worked:**
```bash
claude --print "Find and describe the bug in the CalculateFine method"
```
Claude correctly identified the compound interest bug (multiplying by 1.1 once instead of compounding).

**Score: 8.5/10**

---

### Week 2: Prompting Foundations

**Status:** GOOD - Slight improvements

The course now emphasizes "clarity beats structure" more strongly, which aligns with how I actually use Claude. The message that "Most prompts work fine with natural language" is refreshing and reduces anxiety about needing complex XML.

The "4C structure" (Context, Constraints, Criteria, Clarifications) is a helpful framework without being prescriptive.

**Score: 9/10**

---

### Week 3: Tactical Planning & Code Review

**Status:** GOOD - No major issues

Plan Mode explanation is clearer now. The "Plan mode is for thinking, not documenting" concept finally makes sense with the examples provided.

The addition of solutions in `sessions/week-3/solutions/` directory addresses my complaint about missing answer keys!

**Score: 8/10**

---

### Week 4: Test-Driven Development

**Status:** EXCELLENT - The crown jewel

Still the best week of the course. TDD with Claude really does prevent hallucinations. Coverage targets are now consistently stated as 80-90% throughout.

The `sessions/week-4/resources/tdd-cheatsheet.md` and `coverage-guide.md` are helpful additions.

**Score: 9.5/10**

---

### Week 5: Commands & Basic Skills

**Status:** SIGNIFICANTLY IMPROVED - Properly restructured

This is where the biggest improvement happened. Week 5 now focuses ONLY on:
- Slash Commands (`.claude/commands/`)
- Basic Skills (`.claude/skills/`)

The split from the old "everything in one week" approach is much more digestible. The Developer Track file (`tracks/developer.md`) provides focused guidance for my role.

**What I tested:**
The Week 5 example project at `sessions/week-5/examples/violation-audit-api/` has clear, working examples:
- `/violation-report <property_id>` command
- `/late-fee <principal> <months>` command
- `/late-fee-report` skill with supporting files

The decision tree at `resources/decision-trees.md` finally answers my question: "When do I use a command vs skill vs agent?"

```
Command: Simple, manually invoked, no auto-discovery
Skill: Auto-discoverable by Claude, can have supporting files
Agent: Specialized persona with tool restrictions
Hook: Automated actions (not manually invoked)
Plugin: Packaging for distribution
```

**Score: 8.5/10**

---

### Week 6: Agents & Hooks

**Status:** SIGNIFICANTLY IMPROVED - Now its own week

Week 6 now has dedicated focus on:
- Custom Agents (`.claude/agents/`)
- Hooks (PreToolUse, PostToolUse, etc.)
- Security patterns (blocking dangerous operations)
- SOC 2 audit trails

This used to be crammed into Week 5 with everything else. Having a full week for Agents and Hooks makes the complexity manageable.

The Developer Track explains practical uses:
- Code reviewer agent with `permissionMode: plan`
- Security auditor agent with `model: opus`
- Test writer agent with `permissionMode: acceptEdits`

Hook examples for auto-running tests after edits and blocking `rm -rf` are immediately practical.

**Score: 8.5/10**

---

### Week 7: Plugins - The Complete Package

**Status:** NEW WEEK (used to be Week 6 content)

Plugins are now properly separated as "packaging what you built in Weeks 5-6". This makes much more sense logically.

The week focuses on:
- Plugin manifest structure
- Packaging commands, skills, agents, hooks together
- Distribution (local and marketplace)

I appreciate that Week 7 starts with "You don't NEED plugins unless you're sharing" - reduces anxiety.

**Score: 8/10**

---

### Week 8: Real-World Automation & CI/CD

**Status:** IMPROVED - Better GitLab coverage

The GitLab CI/CD examples are more complete now with:
- Required CI/CD Variables clearly documented
- Pipeline structure explained step-by-step
- Cost monitoring skills
- Scheduled pipeline setup instructions

The CI/CD variable table is helpful:
| Variable | Description | Where to Set |
|----------|-------------|--------------|
| `CI_MERGE_REQUEST_DIFF_BASE_SHA` | Base commit of MR | Auto-provided |
| `ANTHROPIC_API_KEY` | Claude API key | Settings > CI/CD > Variables |

Still could use more screenshots, but the instructions are clearer.

**Score: 8/10**

---

### Week 9: Capstone Hackerspace

**Status:** IMPROVED - Role-specific options

The capstone now has clear options by role:
- **Options A, B, C:** Developer Track (backend, full-stack, data focus)
- **Option D:** QA Track (Test Automation Suite)
- **Option E:** PM Track (Product Design & Documentation)

This addresses my concern that the capstone assumed mastery of advanced features. Now QA and PM folks have appropriate alternatives.

**Score: 8.5/10**

---

## Comparison to Previous Reviews

### Issues Resolved

| Original Issue | Status | Notes |
|----------------|--------|-------|
| Missing glossary | FIXED | Comprehensive glossary at `resources/glossary.md` |
| Inconsistent coverage targets | FIXED | Now consistently 80-90% everywhere |
| Unclear where to work | FIXED | Clear folder structure diagrams in Week 1 |
| Missing CLAUDE.md minimal template | FIXED | `resources/claude-md-minimal.md` added |
| Week 0 foundations missing | FIXED | New Week 0 with AI basics |
| Weeks 5-6 terminology overload | FIXED | Split into two weeks (Commands/Skills vs Agents/Hooks) |
| Missing decision trees | FIXED | `resources/decision-trees.md` with flowcharts |
| Missing answer keys | PARTIALLY FIXED | Week 3 now has solutions directory |

### Regressions

None identified. The course has only improved since my first review.

### Remaining Minor Issues

1. **GitLab CI/CD still assumes familiarity** - Could use a "GitLab 101" sidebar or link
2. **Answer keys for all weeks** - Only Week 3 has solutions; other weeks could use similar
3. **More troubleshooting content** - The troubleshooting guide exists but could be expanded

---

## Recommendations

### High Priority (Still Needed)

1. **Add answer keys/solutions for Weeks 4-6 exercises**
   - Week 4 TDD exercises would benefit from reference implementations
   - Week 5-6 command/skill examples could show expected outputs

2. **Expand troubleshooting guide**
   - Common hook debugging scenarios
   - Agent permission issues
   - CI/CD pipeline failures

### Medium Priority

3. **More before/after prompt examples**
   - Show actual prompts that succeeded/failed
   - Include reasoning for why certain prompts worked better

4. **Video walkthroughs (optional)**
   - Even short 5-minute demos would help visual learners
   - Especially for Plan Mode and Agent creation

### Low Priority (Nice to Have)

5. **Alumni success stories**
   - Real RealManage examples with metrics
   - "How I automated X and saved Y hours"

---

## Session Log

### Commands/Tests Executed

1. **Week 1 Exercise:**
   ```bash
   cd sessions/week-1/example
   claude --print "What does this example application do?"
   # Result: Clear explanation of HOA Violation Tracker CLI
   ```

2. **Week 1 Bug Finding:**
   ```bash
   claude --print "Find and describe the bug in the CalculateFine method"
   # Result: Correctly identified compound interest bug
   ```

3. **Week 5 Commands/Skills Check:**
   ```bash
   cd sessions/week-5/examples/violation-audit-api
   claude --print "What custom commands and skills are available in this project?"
   # Result: Listed /violation-report, /late-fee commands and late-fee-report skill
   ```

### Files Reviewed

- Main README.md (course overview)
- Week 0 README.md (AI foundations)
- Week 1 README.md (setup)
- Week 2 README.md (prompting)
- Week 5 README.md + tracks/developer.md
- Week 6 README.md + tracks/developer.md
- Week 8 README.md + tracks/developer.md
- Week 9 README.md (capstone)
- resources/decision-trees.md
- resources/glossary.md
- resources/quick-start-developer.md

---

## Updated Ratings

### Overall Course Rating: 8.8/10 (up from 8.5/10, originally 7.5/10)

**Breakdown:**
| Category | Review 1 | Review 2 | Review 3 |
|----------|----------|----------|----------|
| Technical content | A | A | A |
| Practical application | A | A | A |
| Beginner accessibility | C+ | B+ | A- |
| Structure and pacing | B | B+ | A- |
| Support materials | B- | B+ | A- |

### What Changed My Mind

The restructuring of Weeks 5-7 made the biggest difference:
- **Old structure:** Commands + Skills + Agents + Hooks + Plugins all crammed into Weeks 5-6
- **New structure:**
  - Week 5: Commands & Skills (manageable!)
  - Week 6: Agents & Hooks (focused)
  - Week 7: Plugins (packaging)

The decision trees at `resources/decision-trees.md` are exactly what I asked for. Now when I wonder "should I use a command or skill?", I have a clear flowchart to follow.

---

## Would I Recommend Now?

**Yes, without major caveats!**

The course is now genuinely beginner-accessible for someone with 1-2 years of development experience. The learning path is clear:

1. **Week 0** - Optional foundations if new to AI
2. **Weeks 1-4** - Core skills (setup, prompting, planning, TDD)
3. **Weeks 5-7** - Automation building blocks (progressively)
4. **Week 8** - Real-world integration
5. **Week 9** - Capstone with role-appropriate options

**Prerequisites remain reasonable:**
- 1+ years professional development experience
- Comfortable with Git, command line, testing concepts
- Basic YAML and Markdown knowledge
- Willingness to experiment

---

## Final Thoughts

### What Impressed Me Most

1. **The DX team actually listened** - My feedback from Reviews 1 and 2 was implemented
2. **The 9-week restructure works** - Pacing is much better
3. **Decision trees are gold** - Finally clear guidance on "when to use what"
4. **Role-specific tracks** - QA, PM, Developer each have appropriate paths
5. **Week 0 foundations** - Sets realistic expectations upfront

### What I'll Use Daily

1. **TDD workflow from Week 4** - Tests prevent hallucinations
2. **Custom commands from Week 5** - Already planning `/code-review` and `/new-service`
3. **Code reviewer agent from Week 6** - `permissionMode: plan` for safe analysis
4. **Cost tracking from Week 8** - `/cost` and model selection

### Confidence Level After Course

**Before Course:** 1/10 (Never used AI coding tools)
**After Review 1:** 7/10 (Functional but struggled with Weeks 5-6)
**After Review 3:** 8.5/10 (Confident with all course material)

---

## Conclusion

The AI 101 Claude Code course has matured significantly. What was a good-but-overwhelming course is now a well-structured learning experience. The team took feedback seriously and made meaningful improvements.

**Grade: A- (8.8/10)**

I would now confidently recommend this course to any developer at RealManage with 1+ years of experience. The progression from basics to advanced topics is logical, the decision trees remove guesswork, and the role-specific tracks ensure everyone gets relevant content.

**What I'd tell a colleague starting today:**
"The course is solid. Start with Week 0 if you're new to AI, don't skip Week 4 (TDD is the killer feature), and use the decision trees when you're confused about commands vs skills vs agents. The Developer Track guide shows you exactly what to focus on."

---

**Thank you DX Team for the continued improvements. This course has genuinely made me a more effective developer.**

*- Alex, RealManage Junior Developer*
*Third Review: January 23, 2026*
