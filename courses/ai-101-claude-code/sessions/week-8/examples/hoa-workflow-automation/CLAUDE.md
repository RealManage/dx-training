# Week 8 Example: HOA Workflow Automation

## Project Purpose
Training exercise for Week 8: Real-World Automation.
This project demonstrates cross-functional workflows, headless automation patterns, and efficiency strategies.

## Tech Stack
- C# .NET 10
- xUnit, FluentAssertions, Moq
- Top-level program pattern
- Bash scripting for automation

## Intentional Bugs (Training Exercise!)
This code contains INTENTIONAL bugs for learners to find using Claude Code:

### LetterGenerationService.cs
1. **Line ~42** - Compliance deadline doesn't account for weekends
2. **Line ~67** - Late fee uses simple percentage, not compound interest
3. **Line ~71** - Reminder type thresholds wrong (10/20/30 vs 15/30/45 days)
4. **Line ~75** - Due date is +7 days, should be next business day
5. **Line ~81** - Returns empty string for unknown CCR types instead of "General Rules"
6. **Line ~97** - Compound interest uses days instead of months
7. **Line ~138** - Missing appeal instructions in letter body

### BoardReportService.cs
1. **Line ~22** - Doesn't validate minimum quorum (3 board members)
2. **Line ~37** - Legal action threshold is 150+ days, should be 120+
3. **Line ~41** - Missing case for BoardReview level
4. **Line ~59** - Due date doesn't account for priority (all set to 14 days)
5. **Line ~62** - Priority always set to "Medium"
6. **Line ~77** - Creates action items for ALL violations including warnings

### CostTrackingService.cs
1. **Line ~36** - Doesn't validate non-negative tokens
2. **Line ~43** - Date comparison ignores time zones
3. **Line ~49** - Floating point precision issues in cost aggregation
4. **Line ~63** - Uses >= for budget check instead of >
5. **Line ~70** - Can return negative remaining budget
6. **Line ~77** - Case-sensitive operation type lookup

## HOA Domain Rules
- Quorum: Minimum 3 board members for valid meeting
- Late fee: 10% monthly compound interest after 30 days grace
- Reminder thresholds: Friendly (0-14), Urgent (15-29), Final (30+)
- Compliance deadline: 30 days, must be a business day
- Unknown CCR types should reference "General Rules - Section 1.0"

## Headless Automation Examples
Use Claude Code CLI for batch processing:

```bash
# Batch code review
claude -p "Review src/Services/*.cs for bugs" --model sonnet --no-session-persistence

# Generate coverage analysis
claude -p "Analyze coverage gaps" --output-format json --no-session-persistence

# Multi-file processing
for file in src/Services/*.cs; do
  claude -p "Review $file briefly" --no-session-persistence > "reviews/$(basename $file).md"
done
```

## Testing
```bash
dotnet build
dotnet test
dotnet test --collect:"XPlat Code Coverage"
```

## Context Management Tips
1. Use /compact for long sessions
2. Clear context between unrelated tasks
3. Use skills for repeated operations
4. Batch similar operations when possible
5. Keep CLAUDE.md focused and concise

## Workflow Patterns Demonstrated
1. Violation letter generation
2. Payment reminder automation
3. Board meeting report generation
4. Cost tracking and budget monitoring
5. Headless automation patterns
