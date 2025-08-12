namespace RealManage.PromptLab;

public class HoaPromptExamples
{
    private readonly Dictionary<string, List<PromptExample>> _examples;
    
    public HoaPromptExamples()
    {
        _examples = InitializeExamples();
    }
    
    public List<string> GetCategories()
    {
        return _examples.Keys.ToList();
    }
    
    public List<PromptExample> GetExamples(string category)
    {
        return _examples.TryGetValue(category, out var examples) 
            ? examples 
            : new List<PromptExample>();
    }
    
    private Dictionary<string, List<PromptExample>> InitializeExamples()
    {
        return new Dictionary<string, List<PromptExample>>
        {
            ["Code Generation"] = new List<PromptExample>
            {
                new PromptExample
                {
                    Title = "Requesting a Service",
                    Bad = "Write a payment service",
                    Good = @"Create a C# PaymentService class that:
- Processes HOA dues payments (monthly/quarterly/annual)
- Calculates late fees with 10% monthly compound interest after 30-day grace period
- Uses async/await for all database operations
- Implements repository pattern with IPaymentRepository
- Includes xUnit tests with 95% coverage using FluentAssertions and Moq
- Handles PCI compliance requirements
- Returns PaymentResult with success/failure status and transaction ID",
                    Explanation = "The good prompt specifies exact requirements, patterns, testing needs, and domain rules"
                },
                new PromptExample
                {
                    Title = "Creating a Component",
                    Bad = "Make a violations list",
                    Good = @"Create an Angular 17 standalone component called ViolationListComponent that:
- Displays a paginated table of HOA violations
- Uses OnPush change detection
- Implements sorting by date, status, and resident name
- Shows violation status with color coding (new=yellow, escalated=orange, resolved=green)
- Includes a search filter for violation type and date range
- Uses Angular Material table component
- Fetches data from ViolationService using RxJS
- Includes Jasmine tests with 95% coverage
- Is mobile-responsive using CSS Grid",
                    Explanation = "Specific component requirements, UI details, and technical implementation details"
                }
            },
            
            ["Testing"] = new List<PromptExample>
            {
                new PromptExample
                {
                    Title = "Unit Test Request",
                    Bad = "Add tests for the fine calculation",
                    Good = @"Write xUnit tests for ViolationService.CalculateFine method that:
- Test normal fine calculation (base amount)
- Test 30-day grace period (no late fee)
- Test compound interest after 30 days (10% monthly)
- Test edge cases: negative amounts, null violations, max date values
- Use FluentAssertions for all assertions
- Mock IViolationRepository with Moq
- Use [Theory] with [InlineData] for parameterized tests
- Achieve 100% code coverage for this method
- Include test names that describe the scenario being tested",
                    Explanation = "Specifies test scenarios, tools, patterns, and coverage expectations"
                }
            },
            
            ["Refactoring"] = new List<PromptExample>
            {
                new PromptExample
                {
                    Title = "Code Improvement",
                    Bad = "Make this code better",
                    Good = @"Refactor the ResidentNotificationService to:
- Extract email sending logic to IEmailService interface
- Convert synchronous database calls to async/await
- Replace string concatenation with StringBuilder for templates
- Add proper exception handling with custom exceptions
- Implement retry logic with Polly for transient failures
- Add logging using ILogger<T>
- Ensure backward compatibility with existing callers
- Write tests before refactoring (if missing)
- Maintain 95% test coverage throughout",
                    Explanation = "Clear refactoring goals with specific patterns and requirements"
                }
            },
            
            ["Documentation"] = new List<PromptExample>
            {
                new PromptExample
                {
                    Title = "API Documentation",
                    Bad = "Document this API",
                    Good = @"Generate OpenAPI/Swagger documentation for the ViolationsController that includes:
<endpoints>
- GET /api/violations - List all violations with pagination
- GET /api/violations/{id} - Get specific violation details
- POST /api/violations - Create new violation
- PUT /api/violations/{id} - Update violation status
- DELETE /api/violations/{id} - Soft delete violation
</endpoints>

For each endpoint document:
- Summary and description
- Request parameters with validation rules
- Request/response body examples in JSON
- All possible HTTP status codes with meanings
- Authorization requirements (roles/scopes)
- Rate limiting (100 requests per minute)
- Example cURL commands
- C# and TypeScript code samples",
                    Explanation = "Structured request with XML tags and comprehensive documentation requirements"
                }
            },
            
            ["Bug Fixing"] = new List<PromptExample>
            {
                new PromptExample
                {
                    Title = "Bug Report",
                    Bad = "The late fee calculation is wrong",
                    Good = @"Fix the bug in ViolationService.CalculateFine (line 42) where:

Current behavior: Late fees are calculated as baseFine * 1.1 regardless of months overdue
Expected behavior: Should compound 10% monthly (e.g., 2 months = baseFine * 1.1 * 1.1)

Steps to reproduce:
1. Create violation with $100 base fine
2. Set due date to 60 days ago
3. Calculate fine
4. Expected: $121 (100 * 1.1 * 1.1)
5. Actual: $110 (100 * 1.1)

Requirements:
- Write failing test that demonstrates the bug
- Fix the calculation to properly compound
- Ensure all existing tests still pass
- Add edge case tests for 0-30 days (no fee) and 365+ days
- Add comment explaining the compound interest formula",
                    Explanation = "Provides clear bug description, reproduction steps, and fix requirements"
                }
            }
        };
    }
}

public class PromptExample
{
    public string Title { get; set; } = "";
    public string Bad { get; set; } = "";
    public string Good { get; set; } = "";
    public string Explanation { get; set; } = "";
}