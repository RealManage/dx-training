# Capstone Option C: Financial Forecasting Tool

## Project Purpose
Build a dues collection forecasting tool that predicts collection rates,
identifies at-risk accounts, and generates variance reports for board meetings.

## Tech Stack
- C# .NET 10
- xUnit, FluentAssertions, Moq
- Statistical analysis patterns

## Requirements

### Core Features (Must Have)
1. Historical data analysis for payment patterns
2. Collection rate predictions with confidence intervals
3. At-risk account identification (30 days early warning)
4. Variance report generation for board meetings
5. Trend analysis over time

### Domain Context
- Dues are collected monthly, due on the 1st
- Grace period: 30 days (late fee applied after)
- Late fee: 10% of dues amount
- Fiscal year: July 1 - June 30
- Board meetings: First Tuesday monthly

### Custom Skill Required
Create `/forecast-collections <month> <year>` skill that:
1. Analyzes historical payment data
2. Predicts collection rate for specified month
3. Identifies at-risk accounts
4. Returns forecast with confidence interval

### Report Generation Required
Generate board-ready variance report in markdown format

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

1. Define payment and account models
2. Write tests for payment history analysis
3. Implement historical analysis
4. Write tests for prediction algorithm
5. Implement prediction with confidence
6. Write tests for at-risk identification
7. Implement at-risk scoring
8. Write tests for report generation
9. Implement variance report
10. Create forecast skill

## Success Criteria

```
[ ] Historical data analyzed correctly
[ ] Predictions within 10% accuracy
[ ] Confidence intervals calculated
[ ] At-risk accounts identified 30 days early
[ ] Variance reports generated in markdown
[ ] Test coverage >= 90%
```

## Sample Data Structure

Use this sample data for testing:

```csharp
var paymentHistory = new List<Payment>
{
    new Payment { AccountId = "A001", Amount = 500, DueDate = "2024-01-01", PaidDate = "2024-01-05" },
    new Payment { AccountId = "A001", Amount = 500, DueDate = "2024-02-01", PaidDate = "2024-02-15" },
    new Payment { AccountId = "A002", Amount = 500, DueDate = "2024-01-01", PaidDate = null }, // Never paid
    // ... more history
};
```

## Prediction Algorithm Hints

Consider these factors for collection prediction:
- Historical payment rate for the account
- Average days to payment
- Seasonal patterns (tax season, holidays)
- Economic indicators (if available)
- Payment method (ACH typically more reliable)
