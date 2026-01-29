# Week 8: Real-World Automation - QA Track

**Track:** QA (Test Automation Focus)
**Duration:** 1.5 hours
**Prerequisites:** Weeks 1-7 completed

---

This track focuses on test automation, coverage analysis, and quality workflows using Claude Code's headless automation capabilities.

## Learning Objectives

By the end of this session, you will be able to:

- Use Claude Code CLI for batch test analysis
- Analyze coverage reports and identify gaps
- Build quality analysis scripts using headless mode
- Create test generation workflows for new code
- Automate test quality reviews

## Part 1: Headless Test Automation (30 min)

### 1.1 Claude Code CLI for QA

**Key CLI Flags for Test Automation:**

| Flag | Description | QA Use Case |
| ---- | ----------- | ----------- |
| `-p, --print` | Non-interactive mode | Batch test analysis |
| `--output-format json` | Structured output | Parse results programmatically |
| `--model sonnet` | Fast model | Quick test reviews |
| `--model opus` | Thorough model | Deep failure analysis |
| `--no-session-persistence` | Ephemeral runs | Clean slate each time |
| `--add-dir` | Add context | Include test directories |

**What Claude Code Can Do for QA:**

- Analyze test failures for root cause
- Generate tests for uncovered code
- Review test quality and coverage
- Suggest additional test scenarios
- Audit tests against requirements

### 1.2 Test Failure Analysis Script

**Create `scripts/analyze-failures.sh`:**

```bash
#!/bin/bash
# analyze-failures.sh - Analyze test failures with Claude
# Usage: ./analyze-failures.sh TestResults/

set -e

RESULTS_DIR=${1:-"TestResults"}
OUTPUT_FILE="test-failure-analysis.md"

echo "# Test Failure Analysis" > "$OUTPUT_FILE"
echo "**Generated:** $(date)" >> "$OUTPUT_FILE"
echo "" >> "$OUTPUT_FILE"

# Find test result files
for trx in "$RESULTS_DIR"/*.trx; do
    if [[ ! -f "$trx" ]]; then
        echo "No .trx files found in $RESULTS_DIR"
        exit 0
    fi

    echo "📝 Analyzing: $trx"
    echo "## $(basename "$trx")" >> "$OUTPUT_FILE"
    echo "" >> "$OUTPUT_FILE"

    claude -p "Analyze these test results. For each failure:
1. Root cause
2. Is it a test bug or code bug?
3. Suggested fix
4. Priority (high/medium/low)

Format as markdown table.

Results: $(cat "$trx")" \
      --model sonnet \
      --no-session-persistence \
      >> "$OUTPUT_FILE" 2>/dev/null

    echo -e "\n---\n" >> "$OUTPUT_FILE"
done

echo "✅ Analysis saved to: $OUTPUT_FILE"
```

### 1.3 Batch Test Generation

**Create `scripts/generate-tests.sh`:**

```bash
#!/bin/bash
# generate-tests.sh - Generate tests for files without coverage
# Usage: ./generate-tests.sh src/Services/NewService.cs

set -e

for file in "$@"; do
    if [[ ! -f "$file" ]]; then
        echo "⚠️  File not found: $file"
        continue
    fi

    # Determine test file path
    TEST_FILE="${file%.cs}Tests.cs"
    TEST_FILE="${TEST_FILE/src\//tests/}"

    echo "📝 Generating tests for: $file"
    echo "   Output: $TEST_FILE"

    # Create directory if needed
    mkdir -p "$(dirname "$TEST_FILE")"

    claude -p "Generate comprehensive xUnit tests for this file.
Requirements:
1. Test all public methods
2. Include happy path and edge cases
3. Use FluentAssertions
4. Use Moq for dependencies
5. Follow AAA pattern (Arrange, Act, Assert)

Output ONLY the complete test file content, no explanations.

File: $file
Content:
$(cat "$file")" \
      --model sonnet \
      --no-session-persistence \
      > "$TEST_FILE"

    echo "✅ Generated: $TEST_FILE"
done
```

## Part 2: Coverage Gap Analysis (25 min)

### 2.1 Coverage Analysis Script

**Create `scripts/analyze-coverage.sh`:**

```bash
#!/bin/bash
# analyze-coverage.sh - Analyze coverage gaps with Claude
# Run from sandbox root after: dotnet test --collect:"XPlat Code Coverage"
# Usage: ./scripts/analyze-coverage.sh

set -e

COVERAGE_FILE=$(find . -name "coverage.cobertura.xml" -type f | head -1)
OUTPUT_FILE="coverage-gap-analysis.md"

if [[ ! -f "$COVERAGE_FILE" ]]; then
    echo "❌ No coverage file found. Run tests first:"
    echo "   dotnet test --collect:\"XPlat Code Coverage\""
    exit 1
fi

echo "📊 Analyzing coverage from: $COVERAGE_FILE"

cat > "$OUTPUT_FILE" << EOF
# Coverage Gap Analysis

**Generated:** $(date)
**Coverage File:** $COVERAGE_FILE

---

EOF

claude -p "Analyze this Cobertura coverage report and identify:

1. Overall coverage percentage
2. Files below 80% coverage (list each)
3. Uncovered methods (high risk)
4. Suggested test cases for top 3 gaps

Format as markdown with tables.

Coverage XML:
$(cat "$COVERAGE_FILE")" \
  --model sonnet \
  --no-session-persistence \
  >> "$OUTPUT_FILE"

echo "✅ Analysis saved to: $OUTPUT_FILE"
```

### 2.2 Coverage Gap Skill

**Create `.claude/skills/coverage-gaps/SKILL.md`:**

```markdown
---
name: coverage-gaps
description: Analyze code coverage gaps and suggest tests
argument-hint: [coverage_report_path]
---

Analyze the coverage report at $ARGUMENTS and identify:

## Analysis Steps:
1. Parse the Cobertura XML coverage report
2. Identify files with coverage below 80%
3. Find uncovered methods and branches
4. Prioritize by risk (public APIs, complex logic)

## Output Format:
### Coverage Gap Analysis

**Overall Coverage:** [X%]
**Files Below Threshold:** [Count]

#### Critical Gaps (High Risk)
| File | Coverage | Uncovered Methods |
| ---- | -------- | ----------------- |
| [file] | [%] | [methods] |

#### Suggested Tests
For each gap, suggest:
1. Test method name
2. Scenario to cover
3. Expected assertions
```

### 2.3 Batch Test Quality Review

**Create `scripts/review-tests.sh`:**

```bash
#!/bin/bash
# review-tests.sh - Review test quality for all test files
# Usage: ./scripts/review-tests.sh tests/**/*Tests.cs

set -e

OUTPUT_FILE="test-quality-report.md"

cat > "$OUTPUT_FILE" << EOF
# Test Quality Report

**Generated:** $(date)
**Files Reviewed:** $#

---

EOF

for file in "$@"; do
    if [[ ! -f "$file" ]]; then
        continue
    fi

    echo "📝 Reviewing: $file"
    echo "## $(basename "$file")" >> "$OUTPUT_FILE"
    echo "" >> "$OUTPUT_FILE"

    claude -p "Review this test file for quality:

1. Are all public methods tested?
2. Are edge cases covered (nulls, empty, boundaries)?
3. Are assertions specific and meaningful?
4. Are test names descriptive?
5. Score 1-10 with brief explanation

File: $file
Content:
$(cat "$file")" \
      --model sonnet \
      --no-session-persistence \
      >> "$OUTPUT_FILE"

    echo -e "\n---\n" >> "$OUTPUT_FILE"
done

echo "✅ Report saved to: $OUTPUT_FILE"
```

## Part 3: Test Quality Skills (25 min)

### 3.1 Test Review Skill

**Create `.claude/skills/test-review/SKILL.md`:**

```markdown
---
name: test-review
description: Review test quality and coverage
argument-hint: [test_file_path]
---

Review the tests in $ARGUMENTS for quality.

## Review Criteria:
1. **Coverage:** All public methods tested?
2. **Edge Cases:** Nulls, empty, boundary values?
3. **Error Paths:** Exceptions properly tested?
4. **Assertions:** Specific and meaningful?
5. **Naming:** Descriptive test names?
6. **Isolation:** No dependencies between tests?
7. **Performance:** Any slow tests?

## Output Format:
### Test Review: [filename]

**Overall Score:** [X/10]

**Strengths:**
- [What's good]

**Improvements Needed:**
- [Issue] -> [Suggestion]

**Missing Test Cases:**
- [Scenario not covered]

**Example Fix:**
[Code snippet for most critical improvement]
```

### 3.2 Generate Tests Skill

**Create `.claude/skills/generate-tests/SKILL.md`:**

```markdown
---
name: generate-tests
description: Generate tests for a source file
argument-hint: [source_file_path]
---

Generate comprehensive xUnit tests for $ARGUMENTS.

## Requirements:
1. Test all public methods
2. Include happy path and edge cases
3. Use FluentAssertions for assertions
4. Use Moq for mocking dependencies
5. Follow AAA pattern (Arrange, Act, Assert)
6. Use descriptive test names: MethodName_Scenario_ExpectedResult

## Output:
Complete test file ready to save.
```

## Part 4: QA Workshop Exercises (20 min)

### Exercise 1: Run Coverage Analysis (10 min)

```bash
# From your sandbox directory:
cd sandbox/hoa-workflow-automation

# Run tests with coverage
dotnet test --collect:"XPlat Code Coverage"

# Ask Claude to analyze
claude -p "Analyze the coverage report and identify the top 3 gaps:
$(find . -name 'coverage.cobertura.xml' -exec cat {} \;)"
```

> **💡 Edge Case: 0% Coverage**
> If you see 0% coverage, this usually means production code and test code are in the same assembly. Coverlet only tracks coverage for assemblies *different* from the test assembly. Solution: Ensure your production code is in a separate project (e.g., `HoaServices.csproj`) that the test project references.

### Exercise 2: Review Test Quality (10 min)

```bash
# Review a specific test file
claude -p "Review this test file for quality. Score 1-10.
File: tests/Services/LetterGenerationServiceTests.cs
$(cat tests/Services/LetterGenerationServiceTests.cs)"

# Or use the skill after creating it
/test-review tests/Services/LetterGenerationServiceTests.cs
```

## Exporting to Test Management Tools

> **Pro Tip:** If your team uses TestRail, Xray, or similar tools, Claude can format output for easy import.

```bash
# Generate test cases in TestRail CSV format
claude -p "Generate test cases for this feature in CSV format.
Columns: Title, Preconditions, Steps, Expected Result, Priority

Feature: $(cat src/Services/PaymentService.cs)" \
  --no-session-persistence > test-cases-import.csv

# Generate Xray-compatible JSON
claude -p "Generate test cases as JSON array with fields:
testKey, summary, steps (array of {action, result}), priority

Feature: $(cat src/Services/ViolationService.cs)" \
  --output-format json > xray-import.json
```

This lets you use Claude for test case generation while keeping your test management tool as the source of truth.

---

## Key Takeaways for QA

### Headless Automation Patterns

```markdown
BATCH ANALYSIS:
- Use -p flag for non-interactive runs
- Use --no-session-persistence for clean runs
- Pipe output to files for reports

COVERAGE ANALYSIS:
- Parse Cobertura XML with Claude
- Identify gaps and prioritize by risk
- Generate suggested test cases

TEST GENERATION:

- Provide source file as context
- Specify requirements (FluentAssertions, Moq, AAA)
- Review generated tests before committing

```

### QA-Specific Skills

```text
/coverage-gaps <path>     -> Analyze coverage gaps
/test-review <file>       -> Review test quality
/generate-tests <file>    -> Generate test skeletons
```

### Quality Metrics to Track

```markdown

- Coverage percentage per service
- Test count and growth over time
- Test quality scores from reviews
- Time to fix failing tests

```

## Homework (Before Week 9)

### Required Tasks

1. Run coverage analysis on the example project
2. Create the `/test-review` skill in your sandbox
3. Use Claude to analyze one coverage gap and generate tests
4. Document your QA automation workflow

### Stretch Goals

1. Build a batch test review script
2. Create a coverage trend tracking approach
3. Build a test quality scorecard

---

*QA Track - Week 8*
*Focused on headless test automation and coverage analysis*
