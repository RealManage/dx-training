using RealManage.WorkflowAutomation.Models;

namespace RealManage.WorkflowAutomation.Services;

/// <summary>
/// Implementation of cost tracking service.
/// Contains INTENTIONAL BUGS for training purposes.
/// </summary>
public class CostTrackingService : ICostTrackingService
{
    private readonly List<CostMetrics> _costHistory = [];
    private readonly decimal _dailyBudgetLimit;

    // Claude API pricing (example rates)
    private static readonly Dictionary<string, (decimal input, decimal output)> OperationRates = new()
    {
        ["letter_generation"] = (0.003m / 1000, 0.015m / 1000),
        ["report_generation"] = (0.003m / 1000, 0.015m / 1000),
        ["code_review"] = (0.003m / 1000, 0.015m / 1000),
        ["exploration"] = (0.00025m / 1000, 0.00125m / 1000),
        ["default"] = (0.003m / 1000, 0.015m / 1000)
    };

    public CostTrackingService(decimal dailyBudgetLimit = 50.00m)
    {
        _dailyBudgetLimit = dailyBudgetLimit;
    }

    public void RecordCost(CostMetrics metrics)
    {
        // BUG #1: Doesn't validate that tokens are non-negative
        _costHistory.Add(metrics);
    }

    public DailyCostReport GetDailyReport(DateTime date)
    {
        // BUG #2: Date comparison ignores time zone issues
        // Should normalize to UTC before comparing
        var dailyMetrics = _costHistory
            .Where(m => m.RecordedAt.Date == date.Date)
            .ToList();

        var totalCost = dailyMetrics.Sum(m => m.TotalCost);

        // BUG #3: Cost by operation aggregation has floating point precision issues
        // Should use decimal rounding
        var costByOperation = dailyMetrics
            .GroupBy(m => m.OperationType)
            .ToDictionary(g => g.Key, g => g.Sum(m => m.TotalCost));

        return new DailyCostReport
        {
            ReportDate = date,
            TotalOperations = dailyMetrics.Count,
            TotalInputTokens = dailyMetrics.Sum(m => m.InputTokens),
            TotalOutputTokens = dailyMetrics.Sum(m => m.OutputTokens),
            TotalCost = totalCost,
            BudgetLimit = _dailyBudgetLimit,
            BudgetRemaining = _dailyBudgetLimit - totalCost,
            CostByOperation = costByOperation
        };
    }

    public bool IsBudgetExceeded(DateTime date)
    {
        var report = GetDailyReport(date);
        // BUG #4: Uses >= instead of > for budget check
        // Exactly hitting budget should not be considered "exceeded"
        return report.TotalCost >= _dailyBudgetLimit;
    }

    public decimal GetRemainingBudget(DateTime date)
    {
        var report = GetDailyReport(date);
        // BUG #5: Can return negative value, should return 0 minimum
        return report.BudgetRemaining;
    }

    public decimal CalculateOperationCost(string operationType, int inputTokens, int outputTokens)
    {
        // BUG #6: Case-sensitive operation type lookup
        // Should be case-insensitive
        var rates = OperationRates.GetValueOrDefault(operationType, OperationRates["default"]);

        var inputCost = inputTokens * rates.input;
        var outputCost = outputTokens * rates.output;

        return inputCost + outputCost;
    }
}
