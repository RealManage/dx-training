# Week 4: TDD - QA Track

**Duration:** 2.5 hours
**Focus:** Writing tests for existing code, not building production code

---

## Why This Week Matters for QA

Week 4 is your **power week**. While developers learn to write tests *before* code (TDD), you'll learn to write comprehensive test suites for *existing* code. This is exactly what QA does:

- Analyze code that developers have written
- Identify gaps in test coverage
- Write tests that catch edge cases developers missed
- Ensure quality through systematic testing

---

## QA-Specific Learning Objectives

By the end of this session, you will be able to:

- ✅ Analyze existing code to identify testable behaviors
- ✅ Write comprehensive test suites using Claude Code
- ✅ Identify coverage gaps and prioritize what to test
- ✅ Generate meaningful test data for edge cases
- ✅ Use Claude to suggest tests you might have missed

---

## QA Exercise 1: Test an Existing Service (45 min)

**Scenario:** A developer has written `PropertyService` but only has ~60% test coverage. Your job is to bring it to 80%+.

### Setup

```bash
cd courses/ai-101-claude-code/sessions/week-4
cp -r examples/property-manager sandbox/property-manager
cd sandbox/property-manager
```

### The Code You're Testing

Open `Services/PropertyService.cs`. You'll find methods like:

- `GetAllPropertiesAsync()` - Lists properties
- `GetPropertyByIdAsync(int id)` - Fetches single property
- `SearchPropertiesAsync(string searchTerm)` - Has a TODO about edge cases!
- `UpdatePropertyAsync(Property property)` - Has a TODO: "Not tested at all!"

**Your job is NOT to modify this code. Your job is to TEST it.**

### QA Prompts to Use

**1. Analyze the code first:**

```
Read Services/PropertyService.cs and Tests/PropertyServiceTests.cs.
Identify:
1. All public methods that need tests
2. Which methods have TODO comments about missing tests
3. Edge cases for SearchPropertiesAsync (null, empty, no matches)
4. Error conditions in UpdatePropertyAsync
Don't write any tests yet - just analyze.
```

**2. Check existing coverage:**

```
Run the existing tests and show me the coverage report.
Which methods have less than 80% coverage?
```

**3. Write tests for uncovered code:**

```
Write xUnit tests for UpdatePropertyAsync covering:
- Successful update (happy path)
- Property not found (returns false)
- Null property input
- Verify LastModifiedDate gets set
Use FluentAssertions for readable assertions.
```

**4. Generate edge case test data:**

```
Generate test cases for SearchPropertiesAsync covering:
- Null search term
- Empty string search term
- Whitespace-only search term
- Search term with no matches
- Case-insensitive matching (verify it works)
- Partial match in address vs city vs state
```

### Success Criteria

- [ ] Coverage increased from ~60% to 80%+
- [ ] `UpdatePropertyAsync` now has tests
- [ ] `SearchPropertiesAsync` edge cases covered
- [ ] Tests use descriptive names (Given_When_Then or Should_When)
- [ ] No production code was modified

---

## QA Exercise 2: Coverage Gap Analysis (30 min)

**Scenario:** Review an existing test suite and identify what's missing.

### Setup

```bash
cd courses/ai-101-claude-code/sessions/week-4
cp -r examples/property-manager sandbox/property-manager
cd sandbox/property-manager
```

### QA Prompts to Use

**1. Run coverage and analyze gaps:**

```
Run tests with coverage: dotnet test --collect:"XPlat Code Coverage"
Show me which methods have less than 80% branch coverage.
For each gap, explain what scenarios aren't tested.
```

**2. Prioritize what to test:**

```
Looking at the coverage gaps, prioritize them by:
1. Business risk (what breaks if this fails?)
2. Complexity (more branches = more risk)
3. Usage frequency (hot paths matter more)
Give me a prioritized list of tests to write.
```

**3. Write the high-priority tests:**

```
Write tests for the top 3 priority gaps you identified.
Focus on branch coverage, not just line coverage.
```

---

## QA Exercise 3: Test Data Generation (30 min)

**Scenario:** Create realistic test data for HOA scenarios.

### QA Prompts to Use

**1. Generate boundary test data:**

```
Generate test data for ViolationEscalation scenarios:
- Violation at exactly 30 days (warning threshold)
- Violation at exactly 60 days (fine threshold)
- Violation at exactly 90 days (legal threshold)
- Multiple violations on same property
- Violation during board meeting freeze period
```

**2. Use Bogus for realistic data:**

```
Create a test data builder using Bogus that generates:
- Realistic homeowner names and addresses
- Valid HOA account numbers (format: HOA-XXXXX)
- Assessment amounts between $100-$500
- Random but valid violation types
Show me how to use it in a test.
```

---

## Key Differences: QA Track vs Developer Track

| Aspect | Developer Track | QA Track |
| ------ | --------------- | -------- |
| **Goal** | Write code + tests together | Write tests for existing code |
| **Starting point** | Empty project | Existing codebase with gaps |
| **Focus** | TDD cycle (Red-Green-Refactor) | Coverage analysis + gap filling |
| **Success metric** | Working feature with tests | 80%+ coverage on provided code |
| **Claude prompts** | "Build X using TDD" | "Test X, find gaps, write missing tests" |

---

## QA-Specific Tips

### When Analyzing Code

- Look for `if/else` branches - each needs a test
- Check for null handling - test with nulls
- Find magic numbers - test boundaries around them
- Look for exception throws - test that they actually throw

### When Writing Tests

- Name tests by behavior: `CalculateLateFee_WhenBalanceIsZero_ReturnsZero`
- One assertion per test when possible
- Use `[Theory]` with `[InlineData]` for data-driven tests
- Don't test private methods - test through public API

### Coverage Quality > Coverage Quantity

- 80% meaningful coverage beats 95% trivial coverage
- Branch coverage matters more than line coverage
- Test the sad paths, not just happy paths

---

## What to Tell Developers

After this week, you can provide feedback like:

> "I ran coverage on `PaymentService` and found the `ApplyDiscount` method only has 45% branch coverage. The negative discount and zero balance cases aren't tested. Should I write those tests, or do you want to add them?"

> "The `ViolationEscalation` tests don't cover the 90-day legal threshold. Given the business risk, we should add tests before release."

---

## API & Integration Testing with Claude (30 min)

Beyond unit tests, QA engineers often need to write API and integration tests. Claude can help with these too.

### API Testing with RestSharp

```csharp
// Install: dotnet add package RestSharp

using RestSharp;
using FluentAssertions;

public class PaymentApiTests
{
    private readonly RestClient _client;

    public PaymentApiTests()
    {
        _client = new RestClient("https://api.example.com");
    }

    [Fact]
    public async Task GetPayment_WithValidId_ReturnsPayment()
    {
        // Arrange
        var request = new RestRequest("/api/payments/123", Method.Get);

        // Act
        var response = await _client.ExecuteAsync<PaymentResponse>(request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        response.Data.Should().NotBeNull();
        response.Data!.Id.Should().Be(123);
    }

    [Fact]
    public async Task GetPayment_WithInvalidId_Returns404()
    {
        var request = new RestRequest("/api/payments/99999", Method.Get);

        var response = await _client.ExecuteAsync(request);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CreatePayment_WithValidData_Returns201()
    {
        var request = new RestRequest("/api/payments", Method.Post);
        request.AddJsonBody(new { Amount = 100.00m, ResidentId = 456 });

        var response = await _client.ExecuteAsync<PaymentResponse>(request);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Data!.Amount.Should().Be(100.00m);
    }
}
```

### Asking Claude for API Tests

```
Generate API tests for @Controllers/PaymentController.cs
Use RestSharp and FluentAssertions.
Test happy paths, error cases (400, 404, 500), and edge cases.
```

> **Tip:** Use `@path/to/file` to reference files instead of copy-pasting content. Claude will read the file automatically.

### Integration Test Pattern

For tests that hit real databases (in test containers):

```csharp
public class PaymentIntegrationTests : IClassFixture<TestDatabaseFixture>
{
    private readonly TestDatabaseFixture _fixture;

    public PaymentIntegrationTests(TestDatabaseFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task ProcessPayment_UpdatesBalanceInDatabase()
    {
        // Arrange - seed test data
        var resident = await _fixture.SeedResident(balance: 500.00m);

        // Act - call the real service with real DB
        var service = new PaymentService(_fixture.DbContext);
        await service.ProcessPaymentAsync(resident.Id, 100.00m);

        // Assert - check database state
        var updated = await _fixture.DbContext.Residents.FindAsync(resident.Id);
        updated!.Balance.Should().Be(400.00m);
    }
}
```

### E2E Testing Basics

For full end-to-end tests, consider:

| Tool | Use Case | Complexity |
| ---- | -------- | ---------- |
| Playwright | Browser automation | Medium |
| Selenium | Legacy browser tests | High |
| HttpClient | API-only E2E | Low |
| TestContainers | DB integration | Medium |

> **Pro Tip:** Ask Claude to generate E2E test scaffolding:
> "Create a Playwright test that logs in as a resident, views their balance, and makes a payment."

### When to Use Each Test Type

```
Unit Tests (70%)
├── Fast, isolated, mock dependencies
├── Test business logic
└── Run on every commit

Integration Tests (20%)
├── Test component interactions
├── Real database (test container)
└── Run on PR/merge

E2E Tests (10%)
├── Full user flows
├── Real browser/API
└── Run nightly/pre-release
```

This follows the **Test Pyramid** - more unit tests at the base, fewer E2E tests at the top.

---

## 📚 Quick Resources

- [Glossary](../../../resources/glossary.md) - Testing terms explained
- [Troubleshooting](../../../resources/troubleshooting.md#-test-coverage-target-rationale) - Coverage target rationale
- [QA Quick-Start](../../../resources/quick-start-qa.md) - Your full learning path
- [Production Hardening](../../../resources/production-hardening.md) - Production-ready automation patterns
