# Bug Hunter Project Context

## Tech Stack
- C# .NET 9
- xUnit for testing
- FluentAssertions

## Testing Requirements
- Write tests FIRST to reproduce the bug
- Use TDD Red-Green-Refactor cycle
- Achieve 95% test coverage
- Test edge cases around 90-day boundary

## Business Rules
- 30-day grace period before interest applies
- 10% monthly compound interest after grace period
- Interest compounds monthly, not daily
- Partial months should round up (business requirement)

## Code Standards
- Use decimal for all financial calculations
- Avoid magic numbers - use constants
- Clear method names that express intent
- Add XML documentation for public methods