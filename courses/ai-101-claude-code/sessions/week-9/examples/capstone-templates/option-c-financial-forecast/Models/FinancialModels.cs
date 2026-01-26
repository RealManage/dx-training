namespace RealManage.FinancialForecast.Models;

/// <summary>
/// Represents a payment record.
/// </summary>
public record Payment
{
    public required string Id { get; init; }
    public required string AccountId { get; init; }
    public required decimal Amount { get; init; }
    public required DateTime DueDate { get; init; }
    public DateTime? PaidDate { get; init; }
    public decimal LateFee { get; init; } = 0m;
    public string PaymentMethod { get; init; } = "Check";
}

/// <summary>
/// Represents an HOA account.
/// </summary>
public record Account
{
    public required string Id { get; init; }
    public required string PropertyAddress { get; init; }
    public required string OwnerName { get; init; }
    public required decimal MonthlyDues { get; init; }
    public bool IsAchEnrolled { get; init; } = false;
    public int MonthsDelinquent { get; init; } = 0;
}

/// <summary>
/// Forecast result with confidence interval.
/// </summary>
public record CollectionForecast
{
    public required DateTime ForecastMonth { get; init; }
    public required decimal PredictedCollectionRate { get; init; }
    public required decimal ConfidenceLow { get; init; }
    public required decimal ConfidenceHigh { get; init; }
    public required decimal PredictedAmount { get; init; }
    public required int TotalAccounts { get; init; }
    public required List<AtRiskAccount> AtRiskAccounts { get; init; }
}

/// <summary>
/// Account flagged as at-risk for non-payment.
/// </summary>
public record AtRiskAccount
{
    public required string AccountId { get; init; }
    public required string PropertyAddress { get; init; }
    public required string OwnerName { get; init; }
    public required decimal RiskScore { get; init; } // 0-1, higher = more risk
    public required string RiskReason { get; init; }
    public required decimal AmountAtRisk { get; init; }
}

/// <summary>
/// Variance report for board meetings.
/// </summary>
public record VarianceReport
{
    public required DateTime ReportDate { get; init; }
    public required string FiscalYear { get; init; }
    public required decimal BudgetedCollections { get; init; }
    public required decimal ActualCollections { get; init; }
    public required decimal VarianceAmount { get; init; }
    public required decimal VariancePercent { get; init; }
    public required string VarianceExplanation { get; init; }
    public required List<MonthlyComparison> MonthlyBreakdown { get; init; }
}

/// <summary>
/// Monthly comparison for variance report.
/// </summary>
public record MonthlyComparison
{
    public required string Month { get; init; }
    public required decimal Budgeted { get; init; }
    public required decimal Actual { get; init; }
    public decimal Variance => Actual - Budgeted;
    public decimal VariancePercent => Budgeted != 0 ? (Variance / Budgeted) * 100 : 0;
}
