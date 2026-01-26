# QA Track - Capstone Guidance

## Recommended Option

**Option D: Test Automation Suite** is designed specifically for QA Engineers.

## Why Option D?

This option lets you leverage your testing expertise while learning how Claude Code can amplify your QA workflows:

- Build comprehensive test suites faster
- Generate realistic test data automatically
- Create coverage dashboards
- Integrate tests into CI/CD pipelines

## What You'll Build

### 1. Comprehensive Test Suite
- Unit tests for HOA domain logic
- Integration tests for workflows
- End-to-end test scenarios
- Edge case coverage

### 2. Test Data Generation Tools
- Realistic resident data (names, addresses, accounts)
- Violation scenarios with various states
- Financial data (dues, payments, late fees)
- Using Bogus or AutoFixture libraries

### 3. Coverage Dashboard
- Real-time coverage metrics
- Trend tracking over time
- Coverage by module/feature
- HTML report generation

### 4. CI/CD Integration
- GitLab pipeline configuration
- Test runs on every commit
- Coverage thresholds enforcement
- Failure notifications

## Skills You'll Use

| Week | Skill Applied |
|------|---------------|
| Week 2 | Prompting Claude to write test cases |
| Week 3 | Plan Mode for test architecture |
| Week 4 | TDD patterns and practices |
| Week 5 | Custom skill for `/generate-test-data` |
| Week 6 | Hook for test coverage tracking |
| Week 8 | CI/CD pipeline integration |

## Getting Started

```bash
# Set up your workspace
cd courses/ai-101-claude-code/sessions/week-9
cp -r examples/capstone-templates/option-d-qa-test-automation sandbox/
cd sandbox/option-d-qa-test-automation

# Start Claude Code
claude
```

### First Steps

1. **Plan your test strategy**: Use Plan Mode to design your test architecture
2. **Start with the domain tests**: Test violation escalation, late fees, etc.
3. **Build data generators**: Create tools for realistic test data
4. **Add coverage tracking**: Set up Coverlet and reporting
5. **Create your custom skill**: `/generate-test-data` for quick data creation

## Example Prompts

```
# Test case generation
"Write xUnit tests for the ViolationEscalationService covering
30-day, 60-day, and 90-day escalation paths"

# Test data generation
"Create a test data generator using Bogus that creates realistic
resident accounts with various payment statuses"

# Coverage analysis
"Set up Coverlet for code coverage and create a script to
generate HTML reports"

# CI/CD integration
"Create a GitLab CI pipeline that runs all tests and fails
if coverage drops below 80%"
```

## Success Criteria Checklist

```
[ ] Test suite covers all critical HOA workflows
    - Violation lifecycle (create, escalate, resolve)
    - Dues calculation and payment processing
    - Late fee compound interest
    - Resident account management

[ ] Test data generation creates realistic scenarios
    - Valid and invalid data combinations
    - Edge cases (boundary values)
    - Time-sensitive scenarios

[ ] Coverage dashboard displays real-time metrics
    - Overall coverage percentage
    - Coverage by namespace/class
    - Uncovered lines highlighted

[ ] CI/CD pipeline runs tests on every commit
    - Build passes with no warnings
    - All tests execute successfully
    - Coverage threshold enforced

[ ] Tests are maintainable and well-documented
    - Clear test naming conventions
    - Arrange-Act-Assert pattern
    - Test documentation in CLAUDE.md

[ ] Coverage >= 80% on critical paths
```

## Deliverables Summary

1. **Test Suite** (`Tests/` folder)
   - Unit tests for domain logic
   - Integration tests for services
   - End-to-end workflow tests

2. **Data Generators** (`TestDataGenerators/` folder)
   - ResidentDataGenerator
   - ViolationDataGenerator
   - FinancialDataGenerator

3. **Coverage Dashboard**
   - Coverlet configuration
   - HTML report generation script
   - Coverage threshold settings

4. **CI/CD Configuration**
   - `.gitlab-ci.yml` with test stages
   - Coverage artifact collection
   - Notification setup

5. **Documentation**
   - CLAUDE.md with test patterns
   - README for running tests
   - Data generator usage guide

## Tips for Success

- **Use Claude for test case ideas**: Ask Claude to identify edge cases you might miss
- **Focus on business logic first**: Test the HOA domain rules thoroughly
- **Make data generators reusable**: They'll be valuable beyond this capstone
- **Document your patterns**: Future QA team members will thank you

## Common Pitfalls

- Writing tests that are too tightly coupled to implementation
- Generating data that doesn't reflect real-world scenarios
- Forgetting to test error paths and exceptions
- Over-complicating the CI/CD pipeline

---

**Questions?** Ask in `#dx-training` or during office hours.
