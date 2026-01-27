# Option D: Test Automation Suite

**Track:** QA Engineers
**Focus:** Comprehensive testing, data generation, batch automation

## Overview

Build a test automation framework for the HOA module that includes:

- Comprehensive test suite for violation, dues, and resident workflows
- Test data generation tools for realistic scenarios
- Coverage dashboard with real-time metrics
- Batch automation scripts for test runs

## Getting Started

```bash
# Copy this template to your sandbox
cd courses/ai-101-claude-code/sessions/week-9
cp -r examples/capstone-templates/option-d-qa-test-automation sandbox/
cd sandbox/option-d-qa-test-automation

# Initialize the project
dotnet new xunit -n HoaTestSuite
cd HoaTestSuite
dotnet add package FluentAssertions
dotnet add package Moq
dotnet add package Bogus
dotnet add package coverlet.collector
```

## Project Structure

```
option-d-qa-test-automation/
├── README.md
├── CLAUDE.md                    # Your project context
├── HoaTestSuite/
│   ├── HoaTestSuite.csproj
│   ├── Tests/
│   │   ├── ViolationTests/
│   │   ├── DuesTests/
│   │   └── ResidentTests/
│   └── DataGenerators/
│       ├── ResidentDataGenerator.cs
│       ├── ViolationDataGenerator.cs
│       └── FinancialDataGenerator.cs
├── .claude/
│   └── skills/
│       └── generate-test-data/
│           └── SKILL.md
├── coverage/
│   └── (generated reports)
└── scripts/
    └── run-tests.sh
```

## Key Deliverables

### 1. Test Suite (`Tests/`)

Create tests for these HOA workflows:

**Violation Lifecycle:**
- Create violation
- 30-day escalation
- 60-day escalation
- 90-day escalation
- Board review trigger
- Resolution and closure

**Dues Processing:**
- Assessment calculation
- Payment processing
- Late fee application (10% monthly compound)
- Payment plan creation
- Balance reconciliation

**Resident Management:**
- Account creation
- Property association
- Communication preferences
- Account status changes

### 2. Data Generators (`DataGenerators/`)

Use Bogus to create realistic test data:

```csharp
public class ResidentDataGenerator
{
    private readonly Faker<Resident> _faker;

    public ResidentDataGenerator()
    {
        _faker = new Faker<Resident>()
            .RuleFor(r => r.FirstName, f => f.Name.FirstName())
            .RuleFor(r => r.LastName, f => f.Name.LastName())
            .RuleFor(r => r.Email, (f, r) => f.Internet.Email(r.FirstName, r.LastName))
            .RuleFor(r => r.PropertyAddress, f => f.Address.StreetAddress())
            .RuleFor(r => r.AccountStatus, f => f.PickRandom<AccountStatus>());
    }

    public Resident Generate() => _faker.Generate();
    public List<Resident> Generate(int count) => _faker.Generate(count);
}
```

### 3. Coverage Dashboard

Configure Coverlet for coverage tracking:

```xml
<!-- In .csproj -->
<ItemGroup>
  <PackageReference Include="coverlet.collector" Version="6.0.0" />
</ItemGroup>
```

Generate HTML reports:

```bash
dotnet test --collect:"XPlat Code Coverage"
reportgenerator -reports:"**/coverage.cobertura.xml" -targetdir:"coverage" -reporttypes:Html
```

### 4. Test Automation Script (`scripts/run-tests.sh`)

```bash
#!/bin/bash
# run-tests.sh - Build, test, and generate coverage report
set -e

echo "🔨 Building project..."
dotnet build --warnaserror

echo "🧪 Running tests with coverage..."
dotnet test --collect:"XPlat Code Coverage" --results-directory ./TestResults

echo "📊 Generating coverage report..."
reportgenerator \
  -reports:"TestResults/**/coverage.cobertura.xml" \
  -targetdir:"coverage" \
  -reporttypes:Html

# Extract coverage percentage
COVERAGE=$(grep -oP 'line-rate="\K[^"]+' TestResults/**/coverage.cobertura.xml | head -1)
COVERAGE_PCT=$(echo "$COVERAGE * 100" | bc)

echo "📈 Coverage: ${COVERAGE_PCT}%"

# Fail if below threshold
if (( $(echo "$COVERAGE_PCT < 80" | bc -l) )); then
  echo "❌ Coverage ${COVERAGE_PCT}% is below 80% threshold"
  exit 1
fi

echo "✅ All tests passed with ${COVERAGE_PCT}% coverage"
```

## Custom Skill: `/generate-test-data`

Create `.claude/skills/generate-test-data/SKILL.md`:

```markdown
---
name: generate-test-data
description: Generate test data for HOA test scenarios
argument-hint: [data_type] [count]
---

Generate $2 instances of $1 test data.

## Available Data Types
- resident: Resident accounts with realistic data
- violation: Violations in various states
- payment: Payment records with amounts and dates
- property: Property records with addresses

## Output Format
Return C# code that uses Bogus to generate the data.
Include edge cases and boundary values.
```

## Success Criteria

```
[ ] Test suite covers all critical HOA workflows
[ ] Test data generation creates realistic scenarios
[ ] Coverage dashboard displays real-time metrics
[ ] Batch test script automates test runs
[ ] Tests are maintainable and well-documented
[ ] Coverage >= 80% on critical paths
```

## Tips

- Start with the domain models before writing tests
- Use the Arrange-Act-Assert pattern consistently
- Name tests descriptively: `MethodName_Scenario_ExpectedResult`
- Generate both valid and invalid test data
- Include boundary value testing

## Resources

- [xUnit Documentation](https://xunit.net/)
- [FluentAssertions Documentation](https://fluentassertions.com/)
- [Bogus Documentation](https://github.com/bchavez/Bogus)
- [Coverlet Documentation](https://github.com/coverlet-coverage/coverlet)
