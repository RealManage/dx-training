# Prime Directives

- **YOU MUST** follow Red/Green/Refactor TDD Best Practics using xUnit
- **YOU MUST** implement C# code in a Best Practices language idiomatic fashion
- **YOU MUST** achieve 80-90% test coverage for all code
- **YOU MUST** use async/await for all I/O operations

## About This Example

This is a simple HOA Violation Tracker CLI application for Week 1 training. It demonstrates:

- Modern C# with top-level programs (no Main method)
- Record types for immutable data models
- Service pattern for business logic
- Basic console interaction

## Known Issues (For Students to Discover)

There is an intentional bug in the CalculateFine method:

- Currently multiplies by 1.1 once for late fees
- Should compound 10% monthly (e.g., 2 months = baseFine *1.1* 1.1)

## Tech Stack

- **Language:** C# with .NET 10
- **Testing:** xUnit, FluentAssertions, Moq
- **Patterns:** Service pattern, repository ready
- **Style:** Top-level programs, nullable reference types enabled

## HOA Business Rules

- **Grace Period:** 30 days (no late fees)
- **Late Fees:** 10% monthly compound interest after grace period
- **Escalation:** 30/60/90 day intervals
- **Violation Types:** Landscaping, Parking, Noise, Architectural

## Testing Requirements

- Write tests FIRST (Red phase)
- Make tests pass (Green phase)
- Refactor while keeping tests green
- Minimum 80-90% code coverage
- Test edge cases and error conditions

## Common Tasks

Students will practice:

1. Finding and fixing the compound interest bug
2. Implementing missing menu options (3-5)
3. Adding persistence with JSON files
4. Creating comprehensive unit tests
5. Refactoring to async/await pattern

## Code Conventions

- Use `var` for obvious types
- Prefer expression-bodied members for simple logic
- Use pattern matching where appropriate
- Follow C# naming conventions (PascalCase for public, camelCase for private)
- Add XML documentation for public members
