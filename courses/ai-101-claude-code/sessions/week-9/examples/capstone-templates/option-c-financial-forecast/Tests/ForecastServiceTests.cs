using RealManage.FinancialForecast.Models;

namespace RealManage.FinancialForecast.Tests;

/// <summary>
/// Starter tests for financial forecast service.
/// Implement IForecastService to make these pass.
/// Remove the Skip attribute as you implement each feature.
/// </summary>
public class ForecastServiceTests
{
    [Fact(Skip = "Implement: ForecastService.CalculateHistoricalRate() for payment collection analysis")]
    public void CalculateHistoricalRate_ReturnsCorrectPercentage()
    {
        // Use GetSamplePayments() for test data - expects ~80% collection rate for 2024
        throw new NotImplementedException("Implement CalculateHistoricalRate");
    }

    [Fact(Skip = "Implement: ForecastService.IdentifyAtRiskAccounts() to flag delinquent accounts")]
    public void IdentifyAtRiskAccounts_FlagsDelinquentAccounts()
    {
        // Use GetSampleAccounts() and GetSamplePayments() - A005 should be flagged as at-risk
        throw new NotImplementedException("Implement IdentifyAtRiskAccounts with RiskScore > 0.5");
    }

    [Fact(Skip = "Implement: ForecastService.GenerateForecast() with confidence intervals")]
    public void GenerateForecast_IncludesConfidenceInterval()
    {
        // Forecast should have ConfidenceLow < PredictedRate < ConfidenceHigh
        throw new NotImplementedException("Implement GenerateForecast with confidence intervals");
    }

    [Fact(Skip = "Implement: ForecastService.GenerateVarianceReport() for board meetings")]
    public void GenerateVarianceReport_CalculatesCorrectVariance()
    {
        // Budget $50k, Actual $45k should show -$5k / -10% variance
        throw new NotImplementedException("Implement GenerateVarianceReport");
    }

    [Fact(Skip = "Implement: Predictions should be within 10% of actual collection rates")]
    public void PredictionAccuracy_WithinTenPercent()
    {
        // Validate prediction accuracy against known historical data
        throw new NotImplementedException("Implement prediction accuracy validation");
    }

    /// <summary>
    /// Sample payment data for testing. January 2024: 4/5 paid (80%), February 2024: 3/5 paid (60%)
    /// </summary>
    public static List<Payment> GetSamplePayments() =>
    [
        // January 2024 - 4 out of 5 paid
        new Payment { Id = "P001", AccountId = "A001", Amount = 500, DueDate = new DateTime(2024, 1, 1), PaidDate = new DateTime(2024, 1, 5) },
        new Payment { Id = "P002", AccountId = "A002", Amount = 500, DueDate = new DateTime(2024, 1, 1), PaidDate = new DateTime(2024, 1, 15) },
        new Payment { Id = "P003", AccountId = "A003", Amount = 500, DueDate = new DateTime(2024, 1, 1), PaidDate = new DateTime(2024, 1, 3) },
        new Payment { Id = "P004", AccountId = "A004", Amount = 500, DueDate = new DateTime(2024, 1, 1), PaidDate = new DateTime(2024, 1, 28) },
        new Payment { Id = "P005", AccountId = "A005", Amount = 500, DueDate = new DateTime(2024, 1, 1), PaidDate = null },

        // February 2024 - 3 out of 5 paid
        new Payment { Id = "P006", AccountId = "A001", Amount = 500, DueDate = new DateTime(2024, 2, 1), PaidDate = new DateTime(2024, 2, 5) },
        new Payment { Id = "P007", AccountId = "A002", Amount = 500, DueDate = new DateTime(2024, 2, 1), PaidDate = null },
        new Payment { Id = "P008", AccountId = "A003", Amount = 500, DueDate = new DateTime(2024, 2, 1), PaidDate = new DateTime(2024, 2, 10) },
        new Payment { Id = "P009", AccountId = "A004", Amount = 500, DueDate = new DateTime(2024, 2, 1), PaidDate = new DateTime(2024, 2, 14) },
        new Payment { Id = "P010", AccountId = "A005", Amount = 500, DueDate = new DateTime(2024, 2, 1), PaidDate = null }
    ];

    /// <summary>
    /// Sample account data for testing. A005 is delinquent (2 months).
    /// </summary>
    public static List<Account> GetSampleAccounts() =>
    [
        new Account { Id = "A001", PropertyAddress = "123 Oak St", OwnerName = "John Smith", MonthlyDues = 500, IsAchEnrolled = true },
        new Account { Id = "A002", PropertyAddress = "456 Elm St", OwnerName = "Jane Doe", MonthlyDues = 500, IsAchEnrolled = false },
        new Account { Id = "A003", PropertyAddress = "789 Pine St", OwnerName = "Bob Wilson", MonthlyDues = 500, IsAchEnrolled = true },
        new Account { Id = "A004", PropertyAddress = "321 Maple St", OwnerName = "Alice Brown", MonthlyDues = 500, IsAchEnrolled = false },
        new Account { Id = "A005", PropertyAddress = "654 Cedar St", OwnerName = "Charlie Davis", MonthlyDues = 500, IsAchEnrolled = false, MonthsDelinquent = 2 }
    ];
}
