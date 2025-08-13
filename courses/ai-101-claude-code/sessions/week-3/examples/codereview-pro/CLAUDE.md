# Code Review Pro Project Context

## Tech Stack
- C# .NET 9
- ASP.NET Core Web API
- xUnit for testing
- Entity Framework Core (implied)

## Review Focus Areas
- Security vulnerabilities (SQL injection, auth)
- Performance bottlenecks (N+1 queries, memory)
- Input validation and null checks
- Error handling and logging
- Thread safety concerns
- Magic numbers and code smells

## Testing Requirements
- 95% test coverage minimum
- Test edge cases (negative amounts, nulls)
- Test security scenarios
- Performance tests for large datasets

## Code Standards
- Repository pattern for data access
- Service layer for business logic
- Controllers should be thin
- Use async/await properly
- Follow SOLID principles
- Use LINQ efficiently