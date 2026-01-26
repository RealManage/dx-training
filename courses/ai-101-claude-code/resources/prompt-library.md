# RealManage Prompt Library 📝

A collection of proven prompts for common RealManage development tasks. Copy, customize, and add to your CLAUDE.md for consistent results.

## 🏗️ C# Development Prompts

### Generate Service with TDD
```
Create a C# service class for <FUNCTIONALITY> following these requirements:
- Use .NET 10 with nullable reference types enabled
- Follow Red/Green/Refactor TDD pattern
- Write xUnit tests FIRST with 80-90% coverage minimum
- Use FluentAssertions for test assertions
- Mock dependencies with Moq
- Follow repository pattern
- Include async/await for all I/O operations
- Add comprehensive XML documentation
- Follow RealManage naming conventions
```

### Entity Framework Migration
```
Create an Entity Framework Core 10 migration for:
<DESCRIBE SCHEMA CHANGES>

Requirements:
- Include both Up and Down methods
- Add data annotations for validation
- Configure relationships in OnModelCreating
- Include seed data if applicable
- Add indexes for foreign keys
- Consider SQL Server 2019 compatibility
```

### API Controller with Validation
```
Create an ASP.NET Core Web API controller for <RESOURCE> with:
- Full CRUD operations
- Model validation with FluentValidation
- Async/await pattern
- Proper HTTP status codes
- Error handling middleware integration
- Swagger documentation attributes
- Authorization with Azure AD B2C
- 80-90% test coverage with integration tests
```

## 🎨 Angular Development Prompts

### Component with OnPush
```
Create an Angular 17 component for <FEATURE> with:
- OnPush change detection strategy
- Standalone component (no modules)
- RxJS for state management
- Signals where appropriate
- Jasmine/Karma tests with 80-90% coverage
- Responsive design with CSS Grid
- WCAG 2.1 AA accessibility
- Error handling with user feedback
```

### Service with Caching
```
Create an Angular service for <FUNCTIONALITY> that:
- Uses HttpClient with interceptors
- Implements caching with RxJS operators
- Handles errors gracefully
- Includes retry logic
- Has comprehensive unit tests
- Uses TypeScript strict mode
- Follows RealManage service patterns
```

### Form with Validation
```
Build a reactive Angular form for <PURPOSE> with:
- FormBuilder and validators
- Custom async validators
- Real-time validation feedback
- Accessibility labels and ARIA
- Submit/reset functionality
- Loading states
- Error display strategy
- 80-90% test coverage
```

## 🏘️ HOA Domain Prompts

### Violation Tracking
```
Implement a violation tracking system that:
- Tracks violation lifecycle (reported → verified → notified → resolved)
- Calculates escalation based on 30/60/90 day intervals
- Generates notification letters
- Integrates with payment system for fines
- Maintains audit trail
- Includes tests for all edge cases
- Handles bulk operations
```

### Payment Processing
```
Create a payment processing module for HOA dues that:
- Handles monthly/quarterly/annual payments
- Calculates late fees with compound interest
- Processes ACH and credit card payments
- Generates receipts and statements
- Integrates with accounting system
- Includes PCI DSS compliance measures
- Has 80-90% test coverage
```

### Board Meeting Automation
```
Build a board meeting management system with:
- Agenda generation from templates
- Minutes recording and approval workflow
- Action item tracking
- Document attachment handling
- Member voting records
- Public/private session separation
- Email notifications
- Compliance with state HOA laws
```

## 📊 Reporting Prompts

### Financial Report Generation
```
Generate a financial reporting service that:
- Creates monthly P&L statements
- Tracks budget vs actual
- Calculates reserve fund requirements
- Generates collection reports
- Exports to Excel/PDF
- Includes charts with Chart.js
- Handles fiscal year boundaries
- Tests all calculation logic
```

### Resident Dashboard
```
Create a resident portal dashboard showing:
- Account balance and payment history
- Violation status
- Community announcements
- Maintenance requests
- Document library access
- Contact information
- Uses Angular Material components
- Mobile-responsive design
```

## 🧪 Testing Prompts

### Unit Test Generation
```
Write comprehensive xUnit tests for <CLASS/METHOD> that:
- Follow AAA pattern (Arrange, Act, Assert)
- Test happy path and edge cases
- Mock all dependencies
- Use FluentAssertions
- Include parameterized tests where applicable
- Achieve 80-90% code coverage
- Test exception scenarios
- Verify async behavior
```

### Integration Test Suite
```
Create integration tests for <FEATURE> that:
- Use TestServer and WebApplicationFactory
- Test full request/response cycle
- Verify database state changes
- Include authentication/authorization
- Test error scenarios
- Clean up test data
- Run in CI/CD pipeline
```

## 🔧 Refactoring Prompts

### Legacy Code Modernization
```
Refactor this legacy code to modern C# standards:
<PASTE CODE>

Requirements:
- Convert to async/await
- Add null safety
- Use pattern matching
- Apply SOLID principles
- Extract interfaces
- Add unit tests before refactoring
- Maintain backward compatibility
- Document breaking changes
```

### Performance Optimization
```
Optimize this code for performance:
<PASTE CODE>

Focus on:
- Database query optimization
- Caching opportunities
- Async operations
- Memory allocation
- Collection performance
- Lazy loading where appropriate
- Measure improvements with benchmarks
```

## 📝 Documentation Prompts

### API Documentation
```
Generate comprehensive API documentation for <SERVICE> including:
- OpenAPI/Swagger specification
- Request/response examples
- Error codes and meanings
- Authentication requirements
- Rate limiting details
- Versioning strategy
- Code samples in C# and TypeScript
```

### README Generation
```
Create a README.md for <PROJECT> that includes:
- Project description and purpose
- Prerequisites and setup instructions
- Configuration options
- Usage examples
- API reference
- Testing instructions
- Deployment guide
- Contributing guidelines
- License information
```

## 🎯 CLAUDE.md Templates

### Project-Specific Context
```markdown
# <PROJECT NAME> Context

## Tech Stack
- Backend: C# .NET 10, ASP.NET Core Web API
- Frontend: Angular 17, TypeScript 5.x
- Database: SQL Server 2019, Entity Framework Core 10
- Cloud: Azure (specify services)

## Coding Standards
- TDD with 80-90% minimum coverage
- xUnit, FluentAssertions, Moq for C#
- Jasmine/Karma for Angular
- Async/await for all I/O
- OnPush change detection in Angular

## Domain Knowledge
- <List domain-specific terms>
- <Business rules>
- <Compliance requirements>

## Current Sprint Goals
- <Active features>
- <Tech debt items>
- <Bug fixes>
```

## 💡 Tips for Effective Prompts

1. **Be Specific**: Include exact version numbers, frameworks, and requirements
2. **Set Constraints**: Specify test coverage, performance needs, compliance
3. **Provide Context**: Include domain knowledge and business rules
4. **Request Structure**: Ask for specific patterns and architectures
5. **Iterate**: Refine prompts based on output quality

## 📌 Contributing

Have a great prompt that works well? Add it to this library:
1. Test the prompt thoroughly
2. Include all necessary context
3. Add examples of successful output
4. Submit PR with your addition

---

**Remember:** These prompts are starting points. Customize them for your specific needs and add project context for best results!