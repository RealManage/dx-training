# Course Feedback: Jordan (QA Engineer) - Second Review

**Student:** Jordan
**Role:** QA Engineer (4 years experience)
**Company:** RealManage
**Review Date:** January 2026
**Review Type:** Follow-up review after course restructure

---

## Background

This is my second review of the AI-101 Claude Code course. My original complaints were:
1. Course assumed I write production code
2. Wanted QA-specific use cases
3. Needed clearer path through the course
4. Wanted test automation focus, not just developer testing

---

## What Improved for QA Roles

### 1. Clear QA Learning Path in README.md

**File:** `courses/ai-101-claude-code/README.md`

The main README now has a proper role selector table that tells me exactly which weeks to focus on and which to skip:

| Your Role | Quick Start Guide | Focus Weeks | Skip |
|-----------|-------------------|-------------|------|
| QA Engineer | QA Track | 1, 2, 4, 8, 9 | 5, 6, 7 (skim) |

This is exactly what I asked for. I no longer have to wade through developer-focused content to find what's relevant to me.

**Improvement Rating:** 5/5 - Perfect

### 2. Dedicated QA Quick-Start Guide

**File:** `courses/ai-101-claude-code/resources/quick-start-qa.md`

This is a game-changer. The quick-start guide gives me:
- A visual learning path showing Week 4 (TDD) as my "power week"
- Week-by-week breakdown with QA-specific priorities
- QA-focused prompts for test case generation, bug reproduction, API contract testing
- A clear message: "You don't need to create - Focus on using and testing"
- Specific prompt templates I can actually use:
  - Test Case Generation prompts
  - Coverage Gap Analysis prompts
  - Test Data Generation prompts
  - Bug Reproduction Test prompts

The section "What to Tell Developers" is particularly useful - it helps me communicate with the dev team about testability without needing to write the code myself.

**Improvement Rating:** 5/5 - Exactly what I needed

### 3. QA Track for Week 5

**File:** `courses/ai-101-claude-code/sessions/week-5/tracks/qa.md`

The Week 5 QA track explicitly acknowledges my role: "As a QA engineer, you don't need to create commands and skills from scratch - developers will build those. Your focus is on **using** them effectively and **providing feedback** to improve them."

Great content includes:
- How to discover available commands and skills
- Testing commands for quality (accuracy, completeness, consistency, edge cases, error handling)
- QA test scenarios for commands
- Effective feedback format template
- Exercises focused on testing and evaluating, not building

**Improvement Rating:** 5/5 - Properly consumer-focused

### 4. Dedicated QA Capstone Option

**Files:**
- `courses/ai-101-claude-code/sessions/week-9/tracks/qa.md`
- `courses/ai-101-claude-code/sessions/week-9/examples/capstone-templates/option-d-qa-test-automation/README.md`
- `courses/ai-101-claude-code/sessions/week-9/examples/capstone-templates/option-d-qa-test-automation/CLAUDE.md`

Option D is specifically designed for QA Engineers and focuses on:
- Building comprehensive test suites
- Test data generation with Bogus
- Coverage dashboards
- CI/CD integration

This lets me use my testing expertise while learning Claude Code. The capstone deliverables make sense for my role:
- Test Suite with unit/integration/E2E tests
- Data Generators for realistic test data
- Coverage Dashboard with Coverlet
- CI/CD Configuration for GitLab

The CLAUDE.md template is pre-configured for a QA-focused project with xUnit, FluentAssertions, Moq, and Bogus.

**Improvement Rating:** 5/5 - Finally a QA-appropriate capstone

### 5. Glossary is Role-Neutral

**File:** `courses/ai-101-claude-code/resources/glossary.md`

The glossary has a dedicated "Testing & TDD" section that covers terms I care about:
- Code Coverage
- Mocking
- Red-Green-Refactor
- Test-Driven Development
- Unit Test
- Integration Test

The definitions are clear and not overly developer-centric.

**Improvement Rating:** 4/5 - Good, could add more QA-specific terms like "test automation", "regression testing", "smoke testing"

---

## What Still Assumes You Are a Developer

### 1. Week 4 TDD Content Leans Developer-Heavy

**File:** `courses/ai-101-claude-code/sessions/week-4/README.md`

While Week 4 is marked as my "power week," the content still assumes I am writing production code. The exercises have me:
- Building a complete CLI application using TDD
- Implementing HomeownerService
- Adding features to an existing property management system

As a QA engineer, I do not write production code. I write tests FOR production code, but I do not implement the services themselves. The exercises should have a QA variant where:
- The production code already exists
- My job is to write the test suite
- Focus is on finding gaps in existing coverage
- Test design and test case identification are the primary skills

The "Advanced Techniques" section talks about "Production Mode" development which assumes I am shipping code.

**Suggestion:** Add a QA track to Week 4 that focuses on:
- Writing tests for existing code (not writing the code itself)
- Test case design and edge case identification
- Coverage analysis of provided code
- Test data generation strategies

### 2. Prerequisites Still Developer-Focused

**File:** `courses/ai-101-claude-code/README.md` (Prerequisites section)

The prerequisites checklist includes:
- .NET 10 SDK
- Node.js 22 LTS via nvm
- npm 10+
- "C# and/or Angular experience"

For a QA engineer, I need to understand these technologies but I do not need the full development environment. A QA-specific prerequisite note would be helpful:
- "QA Engineers: You need .NET SDK for running tests, but deep C# experience is not required"
- "Focus on understanding test frameworks (xUnit) rather than application development"

### 3. Success Metrics Are Code-Centric

**File:** `courses/ai-101-claude-code/README.md` (Success Metrics section)

The success metrics include:
- "Generate working C# code with 80-90% test coverage"

For QA, the metric should be:
- "Generate comprehensive test suites for existing code achieving 80-90% coverage"
- "Identify edge cases and coverage gaps in existing codebases"

### 4. Week 4 Exercises Need QA Variants

**File:** `courses/ai-101-claude-code/sessions/week-4/README.md`

Exercise 1 says "Build a complete CLI application using pure TDD" - this is not my job as QA.
Exercise 2 says "Add features to an existing property management system" - also not my job.

I need exercises like:
- "Here is a PropertyService. Write a comprehensive test suite for it."
- "This service has 60% coverage. Identify the gaps and write tests to reach 80%."
- "Review this test suite for quality and suggest improvements."

### 5. Learning Objectives Are Developer-Oriented

**File:** `courses/ai-101-claude-code/README.md`

The "What You'll Learn" section promises:
- "Build custom skills and use sub-agents for parallel work"
- "Create production-ready C# and Angular code with proper testing"

These are developer objectives. QA objectives should include:
- "Generate comprehensive test suites from specifications"
- "Analyze code coverage and identify gaps"
- "Create effective test data for edge case testing"
- "Integrate automated tests into CI/CD pipelines"

---

## Specific Suggestions by File

| File Path | Issue | Suggestion |
|-----------|-------|------------|
| `courses/ai-101-claude-code/sessions/week-4/README.md` | Exercises assume QA writes production code | Add QA variant exercises focused on testing existing code |
| `courses/ai-101-claude-code/README.md` | Prerequisites and success metrics are dev-focused | Add QA-specific notes for prerequisites and success metrics |
| `courses/ai-101-claude-code/resources/glossary.md` | Missing QA-specific terms | Add: test automation, regression testing, smoke testing, test pyramid, exploratory testing |
| `courses/ai-101-claude-code/sessions/week-4/README.md` | Learning objectives are developer-centric | Add QA-specific learning objectives |

---

## Overall Assessment

### What Works Well
- Clear role selector in main README
- Dedicated QA quick-start guide with realistic prompts
- Week 5 QA track acknowledges I consume, not create
- Week 9 has a QA-specific capstone option
- Visual learning paths show which weeks matter for QA

### What Needs More Work
- Week 4 is my "power week" but still assumes I write production code
- Exercises need QA variants (test existing code, not build new code)
- Prerequisites and success metrics need QA-specific language

### Comparison to My First Review

| Original Complaint | Status |
|-------------------|--------|
| Course assumed I write code | Partially addressed - QA tracks exist but Week 4 exercises still developer-focused |
| Wanted QA-specific use cases | Addressed - Quick-start guide has QA prompts and templates |
| Needed clearer path through course | Fully addressed - Role selector table and visual learning path |
| Wanted test automation focus | Addressed - QA capstone is test automation focused |

---

## Overall Rating

**4 out of 5 stars**

The course has made significant improvements for QA roles. The dedicated quick-start guide, Week 5 QA track, and QA capstone option show real effort to include non-developers. However, Week 4 - which is supposed to be my "power week" - still assumes I write production code rather than tests for existing code.

If Week 4 gets a QA-specific exercise track (similar to Week 5), this would be a solid 5-star experience for QA engineers.

### Recommendation

I would recommend this course to other QA engineers with the caveat that Week 4 exercises will need adaptation. Follow the QA quick-start guide, use the QA capstone option, and ask developers to help with the Week 4 exercises if needed.

---

**Submitted by:** Jordan, QA Engineer
**Date:** January 23, 2026
