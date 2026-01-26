using RealManage.WorkflowAutomation.Models;

namespace RealManage.WorkflowAutomation.Services;

/// <summary>
/// Service interface for tracking AI operation costs.
/// </summary>
public interface ICostTrackingService
{
    /// <summary>
    /// Records a cost metric for an AI operation.
    /// </summary>
    void RecordCost(CostMetrics metrics);

    /// <summary>
    /// Gets the daily cost report.
    /// </summary>
    DailyCostReport GetDailyReport(DateTime date);

    /// <summary>
    /// Checks if the budget limit has been exceeded.
    /// </summary>
    bool IsBudgetExceeded(DateTime date);

    /// <summary>
    /// Gets the remaining budget for the day.
    /// </summary>
    decimal GetRemainingBudget(DateTime date);

    /// <summary>
    /// Calculates cost for a specific operation.
    /// </summary>
    decimal CalculateOperationCost(string operationType, int inputTokens, int outputTokens);
}
