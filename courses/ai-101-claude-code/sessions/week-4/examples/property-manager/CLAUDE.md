# Property Manager Enhancement - TDD Exercise

## 🚨 TDD ENHANCEMENT RULES
1. Write test for new feature **FIRST**
2. Mock **ALL** external dependencies
3. Use in-memory database for tests
4. Maintain existing test quality
5. Increase coverage from 60% to 95%+

## Current System State

### What's Already Built
- Basic property CRUD operations (60% tested)
- SQLite database with Entity Framework
- Simple CLI interface
- Property search functionality

### Current Test Coverage: ~60%
Check it yourself:
```bash
dotnet test --collect:"XPlat Code Coverage"
```

Missing coverage areas exist - find them through testing!

## TDD Enhancement Workflow

```
For EACH new feature:
1. 🔍 Analyze existing code and tests
2. 🔴 Write failing test for new functionality
3. 🟢 Implement minimal code to pass
4. 🔄 Refactor while keeping tests green
5. 📊 Check coverage increased
6. 🔁 Repeat until 95% coverage
```

## Testing Patterns to Follow

### Existing Code Structure
```
/property-manager
├── Program.cs              # CLI entry point
├── Models/
│   └── Property.cs        # Basic property model
├── Services/
│   ├── PropertyService.cs # Service with gaps
│   ├── INotificationService.cs # Interface to mock
│   └── IDocumentStorage.cs     # Interface to mock
├── Data/
│   └── PropertyContext.cs # EF Core context
└── Tests/
    └── PropertyServiceTests.cs # Existing tests
```

### Mocking Pattern
```csharp
// Mock external services - don't implement them!
var mockNotifier = new Mock<INotificationService>();
mockNotifier.Setup(x => x.SendEmailAsync(
    It.IsAny<string>(), 
    It.IsAny<string>(), 
    It.IsAny<string>()))
    .ReturnsAsync(true);

// Inject into service under test
var service = new EnhancedService(mockNotifier.Object);
```

### In-Memory Database Pattern
```csharp
// Already set up in existing tests
var options = new DbContextOptionsBuilder<PropertyContext>()
    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
    .Options;
```

## Enhancement Strategy

### Step 1: Find Coverage Gaps
- Run coverage report
- Identify untested code paths
- Look for TODO comments in tests

### Step 2: Add Missing Tests
- Test error scenarios
- Test edge cases
- Test validation logic
- Test null handling

### Step 3: Enhance with New Features
Think about what a property management system needs:
- Historical tracking?
- Status management?
- Integration points?
- Business rules?

### Step 4: Mock External Dependencies
The interfaces are there - use them!
- Don't implement INotificationService
- Don't implement IDocumentStorage
- Mock them in tests instead

## C# Best Practices

- Use nullable reference types
- Follow async/await patterns
- Use dependency injection
- Keep services testable
- Separate concerns properly

## Testing Best Practices

### Test Organization
```csharp
public class ServiceTests : IDisposable
{
    private readonly PropertyContext _context;
    private readonly Mock<INotificationService> _mockNotifier;
    
    public ServiceTests()
    {
        // Setup shared test infrastructure
    }
    
    [Fact]
    public async Task Method_Scenario_Result()
    {
        // Individual test
    }
    
    public void Dispose()
    {
        // Cleanup
    }
}
```

### Coverage Goals
- Line coverage: 95%+
- Branch coverage: 90%+
- All public methods tested
- All error paths tested
- All validations tested

## Common Pitfalls to Avoid

❌ Implementing mock interfaces  
❌ Testing private methods directly  
❌ Modifying existing passing tests  
❌ Adding features without tests  
❌ Testing EF Core internals  

## Success Criteria

✅ All new code has tests first  
✅ Coverage increased to 95%+  
✅ All tests pass  
✅ Mocks used for external services  
✅ Code is clean and maintainable  

## Hints (Without Giving Answers)

- Look at the existing test TODOs
- Think about what validations are missing
- Consider async error handling
- Remember null reference checks
- Think about concurrent operations
- Consider business rule enforcement

## Remember

- You're **enhancing**, not rewriting
- Keep existing tests green
- Let missing tests guide what to add
- Mock dependencies, don't implement them
- Coverage should emerge naturally from TDD
- The AI should follow YOUR test specifications!