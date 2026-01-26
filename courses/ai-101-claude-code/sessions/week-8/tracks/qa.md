# Week 8: Real-World Automation & CI/CD - QA Track

**Track:** QA (Test Integration Focus)
**Duration:** 1.5 hours
**Prerequisites:** Weeks 1-7 completed

---

This track focuses on CI/CD test integration, coverage in pipelines, and quality automation workflows relevant to QA engineers.

## Learning Objectives

By the end of this session, you will be able to:
- Integrate Claude Code into test automation pipelines
- Configure coverage reporting in GitLab CI/CD
- Build quality gates using Claude-powered analysis
- Create test generation workflows for new code
- Monitor test coverage trends and quality metrics

## Part 1: Test Integration in CI/CD Pipelines (30 min)

### 1.1 Basic Test Pipeline Structure

**Understanding the Test Stage:**

GitLab CI/CD pipelines can automatically run your test suite on every commit or merge request. Claude Code can enhance this by:
- Generating tests for uncovered code paths
- Analyzing test failures for root cause
- Suggesting additional test scenarios
- Reviewing test quality

**Basic .NET Test Pipeline:**
```yaml
# .gitlab-ci.yml
stages:
  - build
  - test
  - quality

variables:
  DOTNET_VERSION: "10.0"

build:
  stage: build
  image: mcr.microsoft.com/dotnet/sdk:10.0
  script:
    - dotnet restore
    - dotnet build --no-restore --warnaserror
  artifacts:
    paths:
      - "**/bin/"
      - "**/obj/"
    expire_in: 1 hour

test:
  stage: test
  image: mcr.microsoft.com/dotnet/sdk:10.0
  dependencies:
    - build
  script:
    - dotnet test --verbosity normal --collect:"XPlat Code Coverage" --results-directory TestResults
  coverage: '/Total\s*\|\s*(\d+(?:\.\d+)?%)/'
  artifacts:
    when: always
    paths:
      - TestResults/
    reports:
      coverage_report:
        coverage_format: cobertura
        path: TestResults/**/coverage.cobertura.xml
```

### 1.2 Coverage Reporting Configuration

**Enabling Coverage in GitLab:**

GitLab can display coverage badges and track coverage trends. Here's how to configure it:

**Coverage Extraction Pattern:**
```yaml
# Different patterns for different test frameworks
test:
  script:
    - dotnet test --collect:"XPlat Code Coverage"
  # Pattern to extract coverage percentage from output
  coverage: '/Total\s*\|\s*(\d+(?:\.\d+)?%)/'
```

**Coverage Report Artifact:**
```yaml
test:
  artifacts:
    reports:
      coverage_report:
        coverage_format: cobertura
        path: TestResults/**/coverage.cobertura.xml
```

**Coverage Thresholds (Quality Gates):**
```yaml
quality-gate:
  stage: quality
  rules:
    - if: $CI_PIPELINE_SOURCE == "merge_request_event"
  script:
    - |
      # Extract coverage from Cobertura XML
      COVERAGE=$(grep -oP 'line-rate="\K[^"]+' TestResults/**/coverage.cobertura.xml | head -1)
      COVERAGE_PCT=$(echo "$COVERAGE * 100" | bc)
      
      # Fail if coverage below threshold
      if (( $(echo "$COVERAGE_PCT < 80" | bc -l) )); then
        echo "Coverage ${COVERAGE_PCT}% is below 80% threshold"
        exit 1
      fi
      echo "Coverage ${COVERAGE_PCT}% meets threshold"
```

### 1.3 Claude-Powered Test Analysis

**Test Failure Analysis Pipeline:**
```yaml
claude-test-analysis:
  stage: quality
  image: node:22
  rules:
    - if: $CI_PIPELINE_SOURCE == "merge_request_event"
      when: on_failure  # Only runs when tests fail
  before_script:
    - npm install -g @anthropic-ai/claude-code
  script:
    - |
      # Get test results
      TEST_OUTPUT=$(cat TestResults/*.trx 2>/dev/null || echo "No test results found")
      
      claude --print "Analyze these test failures:
      
      $TEST_OUTPUT
      
      For each failure:
      1. Identify the root cause
      2. Suggest a fix
      3. Note if it's a test issue vs code issue
      4. Prioritize by impact
      
      Format as actionable markdown." > analysis.md
      
      # Post analysis to MR
      ANALYSIS=$(cat analysis.md | jq -Rs .)
      curl --request POST \
        --header "PRIVATE-TOKEN: $GITLAB_TOKEN" \
        --header "Content-Type: application/json" \
        --data "{\"body\": $ANALYSIS}" \
        "$CI_API_V4_URL/projects/$CI_PROJECT_ID/merge_requests/$CI_MERGE_REQUEST_IID/notes"
```

## Part 2: Coverage Gap Analysis (25 min)

### 2.1 Identifying Uncovered Code

**Coverage Gap Skill (.claude/skills/coverage-gaps/SKILL.md):**
```markdown
---
name: coverage-gaps
description: Analyze code coverage gaps and suggest tests
argument-hint: [coverage_report_path]
disable-model-invocation: true
---

Analyze the coverage report at $ARGUMENTS and identify:

## Analysis Steps:
1. Parse the Cobertura XML coverage report
2. Identify files with coverage below 80%
3. Find uncovered methods and branches
4. Prioritize by risk (public APIs, complex logic)

## Output:
### Coverage Gap Analysis

**Overall Coverage:** [X%]
**Files Below Threshold:** [Count]

#### Critical Gaps (High Risk)
| File | Coverage | Uncovered Methods |
|------|----------|-------------------|
| [file] | [%] | [methods] |

#### Suggested Tests
For each gap, suggest:
1. Test method name
2. Scenario to cover
3. Expected assertions
```

**Pipeline Job for Coverage Analysis:**
```yaml
coverage-analysis:
  stage: quality
  image: node:22
  rules:
    - if: $CI_PIPELINE_SOURCE == "merge_request_event"
  dependencies:
    - test
  before_script:
    - npm install -g @anthropic-ai/claude-code
  script:
    - |
      # Get changed files in this MR
      CHANGED_FILES=$(git diff --name-only $CI_MERGE_REQUEST_DIFF_BASE_SHA...HEAD | grep '\.cs$' | tr '\n' ' ')
      
      claude --print "Review coverage for these changed files:
      Files: $CHANGED_FILES
      
      Coverage Report: $(cat TestResults/**/coverage.cobertura.xml)
      
      Identify:
      1. New code without tests
      2. Modified code with coverage gaps
      3. Suggest specific test cases
      
      Format as markdown checklist." > coverage-review.md
```

### 2.2 Test Generation Workflow

**Automated Test Generation Pipeline:**
```yaml
generate-tests:
  stage: quality
  image: node:22
  rules:
    - if: $CI_PIPELINE_SOURCE == "merge_request_event"
  before_script:
    - npm install -g @anthropic-ai/claude-code
  script:
    - |
      # Get new/modified source files without corresponding tests
      CHANGED_FILES=$(git diff --name-only $CI_MERGE_REQUEST_DIFF_BASE_SHA...HEAD | grep -E '^src/.*\.cs$')
      
      for FILE in $CHANGED_FILES; do
        TEST_FILE="tests/${FILE#src/}"
        TEST_FILE="${TEST_FILE%.cs}Tests.cs"
        
        if [ ! -f "$TEST_FILE" ]; then
          echo "Missing tests for: $FILE"
          
          claude --print "Generate xUnit tests for:
          File: $FILE
          Content: $(cat $FILE)
          
          Requirements:
          1. Test all public methods
          2. Include happy path and edge cases
          3. Use FluentAssertions
          4. Use Moq for dependencies
          5. Follow AAA pattern (Arrange, Act, Assert)
          
          Output: Complete test file content" >> suggested-tests.md
        fi
      done
  artifacts:
    paths:
      - suggested-tests.md
    when: always
```

## Part 3: Quality Gates and Automation (25 min)

### 3.1 Setting Up Quality Gates

**Comprehensive Quality Gate Pipeline:**
```yaml
quality-gate:
  stage: quality
  image: node:22
  rules:
    - if: $CI_PIPELINE_SOURCE == "merge_request_event"
  dependencies:
    - test
  before_script:
    - npm install -g @anthropic-ai/claude-code
  script:
    - |
      # Quality checks
      PASSED=true
      REPORT=""
      
      # 1. Coverage check
      COVERAGE=$(grep -oP 'line-rate="\K[^"]+' TestResults/**/coverage.cobertura.xml | head -1)
      COVERAGE_PCT=$(echo "$COVERAGE * 100" | bc)
      if (( $(echo "$COVERAGE_PCT < 80" | bc -l) )); then
        REPORT="$REPORT\n- FAIL: Coverage ${COVERAGE_PCT}% < 80%"
        PASSED=false
      else
        REPORT="$REPORT\n- PASS: Coverage ${COVERAGE_PCT}%"
      fi
      
      # 2. Test count check
      TEST_COUNT=$(grep -c "test name" TestResults/*.trx || echo "0")
      if [ "$TEST_COUNT" -lt 10 ]; then
        REPORT="$REPORT\n- WARN: Only $TEST_COUNT tests"
      else
        REPORT="$REPORT\n- PASS: $TEST_COUNT tests"
      fi
      
      # 3. Claude quality review
      CHANGED_FILES=$(git diff --name-only $CI_MERGE_REQUEST_DIFF_BASE_SHA...HEAD | tr '\n' ' ')
      CLAUDE_REVIEW=$(claude --print "Quick quality check on: $CHANGED_FILES
      Rate 1-10 on: test coverage, edge cases, error handling
      One-line verdict: PASS/WARN/FAIL")
      REPORT="$REPORT\n- Claude Review: $CLAUDE_REVIEW"
      
      echo -e "## Quality Gate Report\n$REPORT" > quality-report.md
      
      # Post to MR
      BODY=$(cat quality-report.md | jq -Rs .)
      curl --request POST \
        --header "PRIVATE-TOKEN: $GITLAB_TOKEN" \
        --header "Content-Type: application/json" \
        --data "{\"body\": $BODY}" \
        "$CI_API_V4_URL/projects/$CI_PROJECT_ID/merge_requests/$CI_MERGE_REQUEST_IID/notes"
      
      if [ "$PASSED" = false ]; then
        exit 1
      fi
```

### 3.2 Test Review Skill

**Test Quality Review Skill (.claude/skills/test-review/SKILL.md):**
```markdown
---
name: test-review
description: Review test quality and coverage
argument-hint: [test_file_path]
disable-model-invocation: true
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
### Test Review: $ARGUMENTS

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

## Part 4: QA Workshop Exercises (20 min)

### Exercise 1: Configure Coverage Pipeline (10 min)

```
Set up a GitLab CI/CD pipeline that:
1. Runs dotnet test with coverage collection
2. Extracts coverage percentage
3. Fails if coverage < 80%
4. Posts coverage summary to MR

Test with the example project in the sandbox.
```

### Exercise 2: Create Test Gap Analysis (10 min)

```
Use Claude to:
1. Analyze the example project's coverage report
2. Identify the three biggest coverage gaps
3. Generate test skeletons for uncovered methods
4. Prioritize by risk level

Save findings to: coverage-analysis.md
```

## Key Takeaways for QA

### CI/CD Test Patterns
```
COVERAGE REPORTING:
- Use Cobertura format for GitLab integration
- Set coverage regex for badge extraction
- Store artifacts for trend analysis

QUALITY GATES:
- Coverage threshold: 80% minimum
- Test count validation
- Claude-powered code review

AUTOMATION:
- Test failure analysis on every failed build
- Coverage gap detection on MRs
- Suggested test generation
```

### QA-Specific Skills
```
/coverage-gaps <path>     -> Analyze coverage gaps
/test-review <file>       -> Review test quality
/generate-tests <file>    -> Generate test skeletons
```

### Quality Metrics Dashboard
```
Track in GitLab:
- Coverage trends over time
- Test count per sprint
- Failure rate trends
- Time to fix failing tests
```

## Homework (Before Week 9)

### Required Tasks:
1. Set up coverage reporting in your project's pipeline
2. Create a quality gate that blocks MRs below 80% coverage
3. Use Claude to analyze one coverage gap and generate tests
4. Document your QA automation workflow

### Stretch Goals:
1. Implement automated test failure analysis
2. Create a weekly coverage trend report
3. Build a test quality scorecard

---

*QA Track - Week 8*
*Focused on CI/CD test integration and coverage automation*
