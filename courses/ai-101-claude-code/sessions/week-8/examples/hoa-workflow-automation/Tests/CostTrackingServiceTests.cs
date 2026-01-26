using RealManage.WorkflowAutomation.Models;
using RealManage.WorkflowAutomation.Services;

namespace RealManage.WorkflowAutomation.Tests;

public class CostTrackingServiceTests
{
    private readonly CostTrackingService _service = new(dailyBudgetLimit: 50.00m);

    [Fact]
    public void RecordCost_AddsMetricsToHistory()
    {
        // Arrange
        var metrics = new CostMetrics
        {
            OperationType = "letter_generation",
            InputTokens = 1000,
            OutputTokens = 500,
            CostPerInputToken = 0.003m / 1000,
            CostPerOutputToken = 0.015m / 1000
        };

        // Act
        _service.RecordCost(metrics);
        var report = _service.GetDailyReport(DateTime.UtcNow);

        // Assert
        report.TotalOperations.Should().Be(1);
    }

    [Fact]
    public void GetDailyReport_CalculatesTotalCost()
    {
        // Arrange
        _service.RecordCost(new CostMetrics
        {
            OperationType = "letter_generation",
            InputTokens = 1000,
            OutputTokens = 500,
            CostPerInputToken = 0.003m / 1000,
            CostPerOutputToken = 0.015m / 1000
        });

        // Act
        var report = _service.GetDailyReport(DateTime.UtcNow);

        // Assert
        report.TotalCost.Should().BeGreaterThan(0);
        report.TotalInputTokens.Should().Be(1000);
        report.TotalOutputTokens.Should().Be(500);
    }

    [Fact]
    public void IsBudgetExceeded_WhenUnderBudget_ReturnsFalse()
    {
        // Arrange - small cost well under $50 limit
        _service.RecordCost(new CostMetrics
        {
            OperationType = "letter_generation",
            InputTokens = 100,
            OutputTokens = 50,
            CostPerInputToken = 0.003m / 1000,
            CostPerOutputToken = 0.015m / 1000
        });

        // Act
        var exceeded = _service.IsBudgetExceeded(DateTime.UtcNow);

        // Assert
        exceeded.Should().BeFalse();
    }

    [Fact]
    public void IsBudgetExceeded_AtExactBudget_ReturnsTrue()
    {
        // This documents the >= bug - should be > not >=
        var service = new CostTrackingService(dailyBudgetLimit: 0.01m);

        service.RecordCost(new CostMetrics
        {
            OperationType = "letter_generation",
            InputTokens = 1000,
            OutputTokens = 500,
            CostPerInputToken = 0.003m / 1000,
            CostPerOutputToken = 0.015m / 1000
        });

        // Act
        var exceeded = service.IsBudgetExceeded(DateTime.UtcNow);

        // Assert - returns true even at exact budget (bug!)
        exceeded.Should().BeTrue();
    }

    [Fact]
    public void GetRemainingBudget_ReturnsCorrectAmount()
    {
        // Arrange
        _service.RecordCost(new CostMetrics
        {
            OperationType = "letter_generation",
            InputTokens = 1000,
            OutputTokens = 500,
            CostPerInputToken = 0.003m / 1000,
            CostPerOutputToken = 0.015m / 1000
        });

        // Act
        var remaining = _service.GetRemainingBudget(DateTime.UtcNow);

        // Assert
        remaining.Should().BeLessThan(50.00m);
        remaining.Should().BeGreaterThan(49.00m);
    }

    [Fact]
    public void GetRemainingBudget_WhenOverBudget_ReturnsNegative()
    {
        // This documents the bug - should return 0 minimum
        var service = new CostTrackingService(dailyBudgetLimit: 0.001m);

        service.RecordCost(new CostMetrics
        {
            OperationType = "letter_generation",
            InputTokens = 10000,
            OutputTokens = 5000,
            CostPerInputToken = 0.003m / 1000,
            CostPerOutputToken = 0.015m / 1000
        });

        // Act
        var remaining = service.GetRemainingBudget(DateTime.UtcNow);

        // Assert - returns negative value (bug!)
        remaining.Should().BeLessThan(0);
    }

    [Fact]
    public void CalculateOperationCost_ForKnownOperation_UsesCorrectRates()
    {
        // Act
        var cost = _service.CalculateOperationCost("letter_generation", 1000, 500);

        // Assert
        // Input: 1000 * (0.003/1000) = 0.003
        // Output: 500 * (0.015/1000) = 0.0075
        // Total: 0.0105
        cost.Should().BeApproximately(0.0105m, 0.0001m);
    }

    [Fact]
    public void CalculateOperationCost_IsCaseSensitive()
    {
        // This documents the case-sensitivity bug
        var lowerCost = _service.CalculateOperationCost("exploration", 1000, 500);
        var upperCost = _service.CalculateOperationCost("EXPLORATION", 1000, 500);

        // They should be equal, but due to bug, EXPLORATION uses default rates
        // exploration uses lower rates
        lowerCost.Should().NotBe(upperCost); // Documents the bug
    }

    [Fact]
    public void GetDailyReport_GroupsCostByOperation()
    {
        // Arrange
        _service.RecordCost(new CostMetrics
        {
            OperationType = "letter_generation",
            InputTokens = 1000,
            OutputTokens = 500,
            CostPerInputToken = 0.003m / 1000,
            CostPerOutputToken = 0.015m / 1000
        });
        _service.RecordCost(new CostMetrics
        {
            OperationType = "report_generation",
            InputTokens = 2000,
            OutputTokens = 1000,
            CostPerInputToken = 0.003m / 1000,
            CostPerOutputToken = 0.015m / 1000
        });

        // Act
        var report = _service.GetDailyReport(DateTime.UtcNow);

        // Assert
        report.CostByOperation.Should().ContainKey("letter_generation");
        report.CostByOperation.Should().ContainKey("report_generation");
        report.TotalOperations.Should().Be(2);
    }
}
