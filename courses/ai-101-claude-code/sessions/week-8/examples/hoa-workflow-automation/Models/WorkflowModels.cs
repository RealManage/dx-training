namespace RealManage.WorkflowAutomation.Models;

/// <summary>
/// Represents a violation letter to be generated and sent to a homeowner.
/// </summary>
public record ViolationLetter
{
    public required string PropertyId { get; init; }
    public required string OwnerName { get; init; }
    public required string OwnerEmail { get; init; }
    public required string PropertyAddress { get; init; }
    public required string ViolationType { get; init; }
    public required string CcrReference { get; init; }
    public required EscalationLevel EscalationLevel { get; init; }
    public required decimal FineAmount { get; init; }
    public required DateTime ComplianceDeadline { get; init; }
    public required string LetterBody { get; init; }
    public DateTime GeneratedAt { get; init; } = DateTime.UtcNow;
}

/// <summary>
/// Represents escalation levels for HOA violations.
/// </summary>
public enum EscalationLevel
{
    Warning = 0,
    FirstNotice = 1,
    SecondNotice = 2,
    BoardReview = 3,
    LegalAction = 4
}

/// <summary>
/// Payment reminder sent to homeowners with outstanding dues.
/// </summary>
public record PaymentReminder
{
    public required string PropertyId { get; init; }
    public required string OwnerName { get; init; }
    public required string OwnerEmail { get; init; }
    public required decimal AmountDue { get; init; }
    public required decimal LateFee { get; init; }
    public required DateTime DueDate { get; init; }
    public required DateTime OriginalDueDate { get; init; }
    public required int DaysPastDue { get; init; }
    public required string ReminderType { get; init; } // "Friendly", "Urgent", "Final"
    public DateTime GeneratedAt { get; init; } = DateTime.UtcNow;
}

/// <summary>
/// Board meeting report summarizing violations, financials, and action items.
/// </summary>
public record BoardMeetingReport
{
    public required DateTime MeetingDate { get; init; }
    public required string ReportTitle { get; init; }
    public required List<ViolationSummary> ViolationSummaries { get; init; }
    public required FinancialSummary Financials { get; init; }
    public required List<ActionItem> ActionItems { get; init; }
    public required List<string> Attendees { get; init; }
    public DateTime GeneratedAt { get; init; } = DateTime.UtcNow;
}

/// <summary>
/// Summary of a violation for board review.
/// </summary>
public record ViolationSummary
{
    public required string PropertyId { get; init; }
    public required string PropertyAddress { get; init; }
    public required string ViolationType { get; init; }
    public required int DaysSinceReport { get; init; }
    public required EscalationLevel CurrentLevel { get; init; }
    public required decimal TotalFines { get; init; }
    public required string StaffRecommendation { get; init; }
}

/// <summary>
/// Financial summary for board meeting report.
/// </summary>
public record FinancialSummary
{
    public required decimal TotalDuesCollected { get; init; }
    public required decimal TotalDuesOutstanding { get; init; }
    public required decimal LateFeeCollections { get; init; }
    public required decimal ViolationFineCollections { get; init; }
    public required decimal ReserveFundBalance { get; init; }
    public required decimal BudgetVariance { get; init; }
}

/// <summary>
/// Action item from board meeting.
/// </summary>
public record ActionItem
{
    public required string Description { get; init; }
    public required string AssignedTo { get; init; }
    public required DateTime DueDate { get; init; }
    public required string Priority { get; init; } // "High", "Medium", "Low"
    public bool IsCompleted { get; init; } = false;
}

/// <summary>
/// Workflow execution result with timing and cost metrics.
/// </summary>
public record WorkflowResult<T>
{
    public required bool Success { get; init; }
    public T? Data { get; init; }
    public string? ErrorMessage { get; init; }
    public required TimeSpan ExecutionTime { get; init; }
    public required int TokensUsed { get; init; }
    public required decimal EstimatedCost { get; init; }
}

/// <summary>
/// Cost tracking for AI operations.
/// </summary>
public record CostMetrics
{
    public required string OperationType { get; init; }
    public required int InputTokens { get; init; }
    public required int OutputTokens { get; init; }
    public required decimal CostPerInputToken { get; init; }
    public required decimal CostPerOutputToken { get; init; }
    public decimal TotalCost => (InputTokens * CostPerInputToken) + (OutputTokens * CostPerOutputToken);
    public DateTime RecordedAt { get; init; } = DateTime.UtcNow;
}

/// <summary>
/// Daily cost report for monitoring AI usage.
/// </summary>
public record DailyCostReport
{
    public required DateTime ReportDate { get; init; }
    public required int TotalOperations { get; init; }
    public required int TotalInputTokens { get; init; }
    public required int TotalOutputTokens { get; init; }
    public required decimal TotalCost { get; init; }
    public required decimal BudgetLimit { get; init; }
    public required decimal BudgetRemaining { get; init; }
    public required Dictionary<string, decimal> CostByOperation { get; init; }
    public bool IsOverBudget => TotalCost > BudgetLimit;
}
