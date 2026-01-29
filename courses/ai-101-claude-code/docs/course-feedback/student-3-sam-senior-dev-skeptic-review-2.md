# Course Review: Round 2 - Sam (Senior Developer, Skeptic)

**Reviewer:** Sam, Senior Developer (12 years experience)
**Review Date:** 2026-01-23
**Previous Review:** See student-3-sam-senior-dev-skeptic.md
**Course Version Reviewed:** Post-update (9-week curriculum)

---

## Summary

The course has improved meaningfully since my last review. My original complaints were:

1. Some content was too basic/hand-holding
2. Wanted more advanced patterns
3. Answer keys missing for self-validation
4. Decision trees would help choose the right approach

**Verdict:** 3.5 out of 5 stars (up from 2.5)

The updates address most of my concerns, but there's still room for improvement for experienced developers.

---

## What Improved for Experienced Developers

### 1. Decision Trees - A Welcome Addition

**File:** `courses/ai-101-claude-code/resources/decision-trees.md`

Finally. This is exactly what I asked for. The flowcharts are practical and actionable:

- **Plan Mode Decision Tree:** Clear criteria for when to use plan mode. No more guessing. The "3+ files" and "multiple valid approaches" heuristics are solid.
- **Command vs Skill vs Agent vs Plugin:** The feature comparison table (lines 79-87) is the kind of reference I'll actually bookmark. No more wading through documentation to figure out which abstraction to use.
- **Model Selection Guide:** Haiku/Sonnet/Opus decision tree with actual cost comparisons. This will save money.

**What's Still Missing:**

- Decision tree for when to bail out of an AI session entirely and just write the code yourself
- Guidance on recognizing when Claude is going in circles

### 2. Answer Keys - Self-Validation Finally Possible

**File:** `courses/ai-101-claude-code/sessions/week-3/solutions/codereviewpro-answers.md`

This is a proper answer key. 34 issues documented with:

- Exact file paths and line numbers
- Clear explanations of why each issue matters
- Recommended fix order by priority

The categorization (Security, Performance, Logic Bugs, Code Quality, Testing) matches how I'd organize a real code review. The "Recommended Fix Order" section (lines 649-676) shows someone actually thought about workflow, not just content.

**File:** `courses/ai-101-claude-code/sessions/week-4/solutions/tdd-reference-implementations.md`

Over 1000 lines of detailed Red-Green-Refactor cycles. This is what I needed to validate my understanding. The progression from "compilation failure" to "assertion failure" to "green" is documented step by step.

The "Common Student Mistakes" table at the end (lines 1049-1058) is honest and useful.

### 3. Week 8 Advanced Patterns - Substantial Content

**File:** `courses/ai-101-claude-code/sessions/week-8/README.md`

This week actually has meat on the bones:

**GitLab CI/CD Integration:** Real YAML examples (lines 263-307, 311-345) that I could actually use. The MR review pipeline that posts comments via API is production-ready.

**Cost Optimization Strategies:** Finally someone addresses the elephant in the room. Token estimation table (lines 454-463) with actual dollar figures. The "efficient vs inefficient prompting" comparison (lines 496-509) makes the ROI case concrete.

**Cross-Functional Skills:** The skill examples for support, PM, and engineering workflows (lines 39-216) show how to create reusable automations. The YAML frontmatter format is documented clearly.

**File:** `courses/ai-101-claude-code/sessions/week-8/tracks/developer.md`

The developer track condenses Week 8 content appropriately. No fluff, just the technical material.

### 4. TDD Content - More Depth Than Before

**File:** `courses/ai-101-claude-code/sessions/week-4/README.md`

The "Advanced Techniques" section (lines 417-527) addresses my complaint about hand-holding:

- **Granular vs Batched Approach:** Acknowledges that experienced devs work differently than learners
- **Prime Directives:** The idea of encoding TDD rules into CLAUDE.md (lines 466-478) is clever
- **Evolution Path:** "Start granular -> Build confidence -> Create Prime Directives -> Work batched" (lines 493-500)

This shows awareness that one size doesn't fit all.

### 5. Expanded Troubleshooting

**File:** `courses/ai-101-claude-code/resources/troubleshooting.md`

The new sections on Skills (lines 303-383) and Hooks (lines 385-462) debugging are exactly what's needed. YAML frontmatter errors (lines 464-549) will save people hours.

The coverage target explanation (lines 551-605) is pragmatic. I appreciate the honesty about 80-90% vs 100%.

---

## What's Still Too Basic or Fluffy

### 1. Week 3 Plan Mode Section Still Has Some Filler

**File:** `courses/ai-101-claude-code/sessions/week-3/README.md`

The "Before/After Example" (lines 93-141) is useful, but the table at lines 135-141 restates the obvious. Senior devs don't need "Think first, act second" spelled out.

The "Mini-Exercise" (lines 145-166) is reasonable for learners but adds padding for experienced users.

### 2. Main README Still Pushes the 9-Week Linear Path

**File:** `courses/ai-101-claude-code/README.md`

The mermaid diagram (lines 38-49) implies a strict linear progression. An experienced developer could skip Weeks 0-2 entirely and go straight to Week 3 (Plan Mode), Week 4 (TDD), and Week 8 (CI/CD). There should be an "Experienced Developer Fast Track" section.

The role-based tracks table (lines 7-12) helps, but it's buried and not prominent enough.

### 3. Some Emoji/Motivation Overload Remains

Multiple files still have sections like:

- "Welcome to the comprehensive Claude Code training course designed specifically for RealManage teams. This self-paced course will transform how you write code..."

For a senior developer, this reads like marketing copy. Just tell me what the tool does and how to use it effectively.

### 4. Week 8 Could Go Deeper on Anti-Patterns

The cost optimization section covers what to do but not what NOT to do. Common mistakes I've seen:

- Letting Claude read entire codebases when only one file matters
- Using Opus for everything out of habit
- Not using /compact between unrelated tasks
- Repeating context that's already in CLAUDE.md

A "Cost Anti-Patterns" section would be valuable.

---

## Technical Accuracy Issues

### 1. Model Pricing May Be Outdated

**File:** `courses/ai-101-claude-code/resources/decision-trees.md` (lines 239-246)
**File:** `courses/ai-101-claude-code/sessions/week-8/README.md` (lines 427-432)

The pricing tables show Sonnet at $0.003/1K input. This may already be stale. Consider:

- Adding a "Last verified" date
- Linking to official Anthropic pricing page
- Using relative terms ("cheapest", "most expensive") instead of absolute prices

### 2. .NET Version Inconsistency

Main README (line 147) mentions ".NET 10 SDK" but some exercises reference .NET 9 patterns. Should be consistent throughout.

### 3. GitLab CI/CD Examples Missing Error Handling

**File:** `courses/ai-101-claude-code/sessions/week-8/README.md`

The pipeline examples (lines 263-388) don't show what happens when Claude fails. Real CI/CD needs:

- Timeout handling
- Retry logic
- Graceful degradation when API is unavailable
- Cost caps to prevent runaway spending

### 4. Missing Context on Claude Code CLI Flags

The examples use `claude --print` and `claude --model` but there's no comprehensive reference for all CLI flags. The troubleshooting guide mentions `claude doctor` and `claude --verbose` but a complete flag reference would help.

---

## Specific Suggestions by File

### `courses/ai-101-claude-code/README.md`

- Add "Experienced Developer Track" section at the top pointing to Weeks 3, 4, 8
- Reduce the prerequisites checklist - experienced devs know what a terminal is

### `courses/ai-101-claude-code/resources/decision-trees.md`

- Add decision tree: "When to give up on Claude and code it yourself"
- Include token cost estimates for common scenarios

### `courses/ai-101-claude-code/sessions/week-8/README.md`

- Add "CI/CD Anti-Patterns" section
- Include rollback procedures when Claude-generated code breaks production
- Show how to integrate with feature flags for gradual rollout

### `courses/ai-101-claude-code/resources/troubleshooting.md`

- Add section on debugging Claude's context window issues
- Include guidance on what to do when Claude starts hallucinating method names

---

## Rating Breakdown

| Category | Previous | Now | Notes |
| -------- | -------- | --- | ----- |
| Content Depth | 2/5 | 4/5 | Answer keys and Week 8 are substantial |
| Practical Value | 3/5 | 4/5 | Decision trees and CI/CD examples |
| Experienced Dev Focus | 2/5 | 3/5 | Better but still some padding |
| Technical Accuracy | 3/5 | 3/5 | Some pricing/version concerns |
| Documentation Quality | 3/5 | 4/5 | Reference implementations help |

**Overall: 3.5/5 Stars**

---

## Would I Recommend This Course?

**For junior developers:** Yes, absolutely. The hand-holding is appropriate for their level.

**For mid-level developers (3-7 years):** Yes, but skip Weeks 0-2 and skim Week 1.

**For senior developers (8+ years):** Conditionally. Read:

- Decision Trees doc (30 min)
- Week 3 README, skip exercises (30 min)
- Week 4 README + TDD reference implementations (1 hour)
- Week 8 full content (1 hour)
- Troubleshooting guide as reference

Total time investment: ~3 hours instead of 18+ hours. Worth it.

---

## Final Thoughts

This update shows the course authors listened to feedback. The decision trees, answer keys, and Week 8 CI/CD content address my core complaints. The TDD reference implementations are the kind of comprehensive documentation that proves someone actually used this material and refined it.

What keeps this from a 4+ rating:

1. **Still some filler content** that experienced devs have to wade through
2. **Linear curriculum assumption** when experienced devs want to jump to advanced topics
3. **Missing failure mode documentation** - what happens when Claude goes wrong

The course has evolved from "beginner-only" to "useful for everyone if you know where to look." That's meaningful progress.

I'll continue using the reference materials, especially the decision trees and troubleshooting guide. The Week 8 CI/CD patterns will go into our team's automation toolkit.

---

**Reviewed by:** Sam, Senior Developer
**Date:** 2026-01-23
**Time Spent on Review:** 2 hours
