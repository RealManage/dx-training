# Capstone Option D: Test Automation Suite

## Project Purpose

Build a comprehensive test automation framework for the HOA module that includes
test suites, data generation tools, coverage dashboards, and batch automation scripts.

## Tech Stack

- C# .NET 10
- xUnit, FluentAssertions, Moq
- Bogus (test data generation)
- Coverlet (code coverage)

## Requirements

### Core Features (Must Have)

1. Comprehensive test suite for HOA workflows
2. Test data generation using Bogus
3. Coverage tracking and reporting
4. Batch test automation scripts
5. Documentation of test patterns

### Domain Coverage

Tests should cover these HOA workflows:

- Violation lifecycle (create, escalate, resolve)
- Dues calculation and payment processing
- Late fee compound interest (10% monthly)
- Resident account management
- Board meeting integrations

### Custom Skill Required

Create `/generate-test-data <type> <count>` skill that:

1. Accepts data type (resident, violation, payment, property)
2. Generates realistic test data using Bogus
3. Includes edge cases and boundary values
4. Returns generated data for use in tests

### Test Automation Script Required

Create `scripts/run-tests.sh` that:

1. Builds the project
2. Runs all tests with coverage collection
3. Generates coverage report
4. Fails if coverage drops below 80%

## Getting Started

```bash
# Initialize the test project
dotnet new xunit -n HoaTestSuite
cd HoaTestSuite

# Add required packages
dotnet add package FluentAssertions
dotnet add package Moq
dotnet add package Bogus
dotnet add package coverlet.collector

# Start Claude Code
claude
```

## Suggested Implementation Order

1. Set up project structure with test folders
2. Create domain models for testing
3. Build data generators using Bogus
4. Write violation lifecycle tests
5. Write dues and payment tests
6. Write late fee calculation tests
7. Configure Coverlet for coverage
8. Create coverage report generation
9. Create test automation script
10. Document test patterns

## Success Criteria

```markdown
[ ] Test suite covers all critical HOA workflows
[ ] Test data generation creates realistic scenarios
[ ] Coverage dashboard displays real-time metrics
[ ] Batch test script automates test runs
[ ] Tests are maintainable and well-documented
[ ] Coverage >= 80% on critical paths
```

## Test Naming Convention

Use this pattern: `MethodName_Scenario_ExpectedResult`

Examples:

- `CalculateLateFee_PaymentOverdue30Days_Returns10PercentFee`
- `EscalateViolation_At30Days_TransitionsToFirstNotice`
- `CreateResident_WithValidData_ReturnsNewAccount`

## Starter Domain Models

Create these models in a `Models/` folder to get started:

```csharp
// Models/Violation.cs
public class Violation
{
    public Guid Id { get; set; }
    public string PropertyId { get; set; } = string.Empty;
    public ViolationType Type { get; set; }
    public ViolationStatus Status { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ResolvedDate { get; set; }
    public string Description { get; set; } = string.Empty;
}

public enum ViolationType { Landscaping, Parking, Noise, Architectural, Other }
public enum ViolationStatus { Open, FirstNotice, SecondNotice, FinalNotice, BoardReview, Resolved }

// Models/Resident.cs
public class Resident
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PropertyAddress { get; set; } = string.Empty;
    public AccountStatus AccountStatus { get; set; }
}

public enum AccountStatus { Active, Inactive, Suspended, PendingApproval }

// Models/DuesPayment.cs
public class DuesPayment
{
    public Guid Id { get; set; }
    public Guid ResidentId { get; set; }
    public decimal Amount { get; set; }
    public decimal LateFee { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? PaidDate { get; set; }
    public PaymentStatus Status { get; set; }
}

public enum PaymentStatus { Pending, Paid, Overdue, PartiallyPaid }
```

## Data Generator Example

```csharp
public class ViolationDataGenerator
{
    private readonly Faker<Violation> _faker;

    public ViolationDataGenerator()
    {
        _faker = new Faker<Violation>()
            .RuleFor(v => v.Id, f => Guid.NewGuid())
            .RuleFor(v => v.PropertyId, f => f.Random.AlphaNumeric(8))
            .RuleFor(v => v.Type, f => f.PickRandom<ViolationType>())
            .RuleFor(v => v.CreatedDate, f => f.Date.Past(1))
            .RuleFor(v => v.Status, f => f.PickRandom<ViolationStatus>())
            .RuleFor(v => v.Description, f => f.Lorem.Sentence());
    }

    public Violation Generate() => _faker.Generate();
    public List<Violation> Generate(int count) => _faker.Generate(count);
}
```

## Tips for Using Claude Code

- Start with the domain models and tests for those
- Use Claude to identify edge cases you might miss
- Generate test data that covers boundary conditions
- Document patterns for future QA team members
