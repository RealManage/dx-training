# Common Patterns for RealManage Development 🏗️

Proven patterns and solutions for recurring RealManage development scenarios using Claude Code.

## 🎯 TDD Workflow Pattern

### The Sacred Cycle
```
1. RED: Write failing test
2. GREEN: Write minimal code to pass
3. REFACTOR: Improve code quality
4. REPEAT: Never skip steps
```

### Example Session
```
> Write xUnit tests for a ViolationService.CalculateFine method that:
  - Takes base fine amount and days overdue
  - Adds 10% compound interest monthly
  - Caps at $1000 maximum
  - Returns 0 for negative days

[Claude writes tests]

> Run: dotnet test
[Tests fail - RED phase]

> Now implement CalculateFine to pass these tests

[Claude implements]

> Run: dotnet test
[Tests pass - GREEN phase]

> Refactor for better readability while keeping tests green
```

## 🏘️ HOA Domain Patterns

### Violation Lifecycle State Machine
```csharp
public enum ViolationStatus
{
    Reported,
    UnderReview,
    Verified,
    NotificationSent,
    Appealed,
    Resolved,
    Escalated
}

// Pattern: State transitions with audit trail
> Implement a ViolationStateMachine that:
  - Enforces valid transitions only
  - Records who/when for each transition
  - Sends notifications on status change
  - Integrates with payment system
  - Has 80-90% test coverage
```

### Payment Processing with Audit
```
> Create a payment processor that:
  - Handles ACH and credit cards
  - Calculates late fees correctly
  - Creates audit records for all transactions
  - Implements idempotency for retries
  - Follows PCI DSS guidelines
  - Uses Entity Framework transactions
  - Tests all edge cases
```

### Board Meeting Workflow
```
Pattern: Document Generation → Review → Approval → Archive

> Build a board meeting system with:
  - Template-based agenda generation
  - Draft → Review → Final states
  - Version control for documents
  - Member voting tracking
  - Public/Executive session separation
  - Compliance with state laws
```

## 🔧 C# Service Patterns

### Repository with Unit of Work
```
> Implement repository pattern for <Entity> with:
  - Generic repository base class
  - Unit of Work for transactions
  - Async methods throughout
  - Include/ThenInclude for eager loading
  - Specification pattern for queries
  - Full test coverage with in-memory database
```

### CQRS Implementation
```
> Create CQRS pattern for <Feature>:
  - Separate command and query models
  - MediatR for handler dispatch
  - FluentValidation for commands
  - Async handlers
  - Logging and error handling
  - Integration tests for full pipeline
```

### Background Service Pattern
```
> Build a background service for <Task> using:
  - IHostedService implementation
  - Graceful shutdown handling
  - Error recovery with exponential backoff
  - Health checks
  - Structured logging
  - Configuration via appsettings
```

## 🎨 Angular Component Patterns

### Smart/Dumb Component Pattern
```
Smart Component (Container):
> Create a smart component for <Feature>Container that:
  - Manages state with services
  - Handles business logic
  - Uses OnPush change detection
  - Passes data to dumb components
  - Handles events from children

Dumb Component (Presentational):
> Create a dumb component for <Feature>Display that:
  - Receives data via @Input()
  - Emits events via @Output()
  - No service dependencies
  - Pure presentation logic
  - Easy to test
```

### Form Handling Pattern
```
> Build a reactive form for <Entity> with:
  - FormBuilder configuration
  - Custom validators
  - Async validators for uniqueness
  - Error message strategy
  - Dirty checking for unsaved changes
  - Submit/Reset/Cancel actions
  - Loading states
  - 80-90% test coverage
```

### Data Table Pattern
```
> Create a data table component with:
  - Server-side pagination
  - Multi-column sorting
  - Column filtering
  - Row selection
  - Export to Excel/CSV
  - Responsive design
  - Virtual scrolling for performance
  - Accessibility compliance
```

## 📊 Reporting Patterns

### Excel Export Pattern
```
> Generate Excel export for <Report> using:
  - EPPlus or ClosedXML
  - Multiple worksheets
  - Formatting and formulas
  - Charts if needed
  - Streaming for large datasets
  - Memory-efficient processing
  - Unit tests for calculations
```

### PDF Generation Pattern
```
> Create PDF report for <Document> with:
  - Header/Footer templates
  - Page numbers
  - Table of contents
  - Images and charts
  - Digital signatures if required
  - Accessibility tags
  - Tests for layout
```

## 🔒 Security Patterns

### Authentication/Authorization
```
> Implement auth for <Resource> with:
  - Azure AD B2C integration
  - Role-based access control
  - Claims-based authorization
  - JWT token validation
  - Refresh token handling
  - Audit logging
  - Tests for all scenarios
```

### Data Protection Pattern
```
> Add data protection for <SensitiveData>:
  - Encryption at rest
  - Encryption in transit
  - Key rotation strategy
  - PII masking in logs
  - Secure configuration storage
  - Compliance with GDPR/CCPA
```

## 🚀 Performance Patterns

### Caching Strategy
```
> Implement caching for <Operation> with:
  - Memory cache for hot data
  - Redis for distributed cache
  - Cache invalidation strategy
  - Cache-aside pattern
  - Monitoring cache hit rates
  - Tests for cache scenarios
```

### Database Optimization
```
> Optimize database queries for <Feature>:
  - Add appropriate indexes
  - Use projection for read models
  - Implement pagination
  - Batch operations
  - Async streaming for large results
  - Query performance tests
```

## 🧪 Testing Patterns

### Integration Test Pattern
```
> Write integration tests for <API> using:
  - WebApplicationFactory
  - Test database with known data
  - Authentication mocking
  - Full request/response testing
  - Cleanup after each test
  - Parallel test execution
  - 80-90% coverage requirement
```

### Mock Data Pattern
```
> Create test data builders for <Entity>:
  - Builder pattern for test objects
  - Bogus/Faker for realistic data
  - Consistent seed for reproducibility
  - Edge case generators
  - Performance test datasets
```

## 📝 Documentation Patterns

### API Documentation
```
> Generate API docs for <Controller>:
  - XML comments on all public methods
  - Swagger/OpenAPI attributes
  - Request/Response examples
  - Error code documentation
  - Authentication requirements
  - Versioning information
```

### Code Documentation
```
> Add documentation to <Class>:
  - Class-level summary
  - Method descriptions with parameters
  - Return value explanations
  - Exception documentation
  - Example usage
  - See also references
```

## 🔄 Refactoring Patterns

### Legacy Modernization
```
Step 1: Add tests to legacy code
> Write characterization tests for <LegacyClass>

Step 2: Refactor safely
> Refactor to modern patterns while keeping tests green

Step 3: Improve design
> Apply SOLID principles and remove technical debt
```

### Performance Improvement
```
> Profile and optimize <SlowOperation>:
  1. Add performance benchmarks
  2. Identify bottlenecks
  3. Apply optimizations
  4. Verify improvements
  5. Document changes
```

## 💡 Claude Code Best Practices

### Incremental Development
```
Instead of: "Build entire feature"
Better: Break into smaller tasks:
  1. Create data model
  2. Add repository
  3. Build service layer
  4. Create API endpoint
  5. Add UI component
  6. Write integration tests
```

### Context Management
```
# Add to CLAUDE.md for consistency:
- Preferred patterns
- Naming conventions
- Test requirements
- Common pitfalls
- Team decisions
```

### Review Pattern
```
# Two-instance review:
Terminal 1: claude write the code
Terminal 2: claude review for issues
```

---

**Pro Tip:** Save successful patterns to your project's CLAUDE.md for consistent results across the team!