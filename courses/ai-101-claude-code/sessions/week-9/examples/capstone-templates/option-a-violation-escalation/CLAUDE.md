# Capstone Option A: Violation Escalation System

## Project Purpose
Build an automated HOA violation escalation system that tracks violations through
the 30/60/90 day progression, sends automated notices, and integrates with board
meeting agenda generation.

## Tech Stack
- C# .NET 10
- xUnit, FluentAssertions, Moq
- State machine pattern for violation lifecycle

## Requirements

### Core Features (Must Have)
1. Violation state machine with stages: Warning -> FirstNotice -> SecondNotice -> BoardReview -> LegalAction
2. Automatic escalation triggers at 30/60/90 day marks
3. Fine calculation with 10% monthly compound interest
4. Notice generation with all required fields
5. Audit trail for SOC 2 compliance

### Domain Rules
- Violations escalate every 30 days if unresolved
- Warning phase: No fine
- FirstNotice (31-60 days): $50 base fine
- SecondNotice (61-90 days): $100 base + compound interest
- BoardReview (90+ days): Requires board action
- Compound interest: 10% monthly, compounding after 30 days

### Custom Skill Required
Create `/escalate-violation <property_id>` skill that:
1. Finds the violation record
2. Determines current stage
3. Escalates to next stage if conditions met
4. Generates appropriate notice
5. Returns status and audit entry

### Hook Required
Create PostToolUse hook that logs all file changes to `.audit.log`

## Getting Started

```bash
# Build the starter
dotnet build

# Run tests (should see failures - TDD!)
dotnet test

# Start Claude Code
claude
```

## Suggested Implementation Order

1. Define violation state enum and domain model
2. Write tests for state transitions
3. Implement state machine
4. Write tests for fine calculation
5. Implement fine calculation with compound interest
6. Write tests for notice generation
7. Implement notice generation
8. Add audit logging hook
9. Create escalation skill
10. Integration tests

## Success Criteria

```
[ ] State machine correctly handles all transitions
[ ] Fines calculated correctly with compound interest
[ ] Notices contain all required fields
[ ] Board review triggered at 90+ days
[ ] Audit trail captures all state changes
[ ] Custom skill works end-to-end
[ ] Test coverage >= 80%
```

## Tips for Using Claude Code

- Use Plan Mode first to design your architecture
- TDD: Write failing tests before implementation
- Use sub-agents to explore patterns in earlier weeks
- Check your coverage frequently with `dotnet test --collect:"XPlat Code Coverage"`
