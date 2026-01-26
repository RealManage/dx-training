---
name: write-bdd
description: Convert plain English requirements into Gherkin BDD scenarios
argument-hint: <requirement>
---

Convert this requirement into well-structured Gherkin BDD scenarios:

$ARGUMENTS

## Output Format

Generate a complete Gherkin feature file:

```gherkin
Feature: [Feature Name]
  As a [role]
  I want [capability]
  So that [benefit]

  Background:
    Given [common preconditions shared by all scenarios]

  @happy-path
  Scenario: [Descriptive scenario name - success case]
    Given [initial context]
    And [additional context if needed]
    When [action taken]
    And [additional action if needed]
    Then [expected outcome]
    And [additional verification]

  @edge-case
  Scenario: [Edge case description]
    Given [context]
    When [action]
    Then [outcome]

  @error-handling
  Scenario: [Error scenario description]
    Given [context that will cause error]
    When [action attempted]
    Then [error handling behavior]
    And [user feedback]

  @validation
  Scenario Outline: [Parameterized test description]
    Given [context with <parameter>]
    When [action with <input>]
    Then [outcome with <expected>]

    Examples:
      | parameter | input | expected |
      | value1    | val1  | result1  |
      | value2    | val2  | result2  |
```

## Guidelines

### Scenario Coverage
Include scenarios for:
1. **Happy path** - Normal successful flow
2. **Edge cases** - Boundary conditions
3. **Error handling** - Invalid inputs, failures
4. **Permissions** - Unauthorized access attempts
5. **Data variations** - Use Scenario Outline for multiple inputs

### Writing Style
- Use domain language, not technical jargon
- Keep steps atomic and focused
- Avoid implementation details in scenarios
- Use present tense
- Be specific with test data

### Tags
Use tags to categorize:
- `@happy-path` - Success scenarios
- `@edge-case` - Boundary conditions
- `@error-handling` - Failure scenarios
- `@smoke` - Critical path tests
- `@regression` - Full test suite

### Anti-Patterns to Avoid
- Don't use "click" or UI-specific language
- Don't chain multiple actions in one step
- Don't include technical implementation details
- Don't make scenarios dependent on each other
