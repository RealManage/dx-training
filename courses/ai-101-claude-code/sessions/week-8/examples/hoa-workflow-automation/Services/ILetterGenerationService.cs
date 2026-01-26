using RealManage.WorkflowAutomation.Models;

namespace RealManage.WorkflowAutomation.Services;

/// <summary>
/// Service interface for generating violation letters.
/// </summary>
public interface ILetterGenerationService
{
    /// <summary>
    /// Generates a violation letter for the specified property and violation.
    /// </summary>
    ViolationLetter GenerateViolationLetter(
        string propertyId,
        string ownerName,
        string ownerEmail,
        string propertyAddress,
        string violationType,
        int daysSinceReport);

    /// <summary>
    /// Generates a payment reminder for the specified property.
    /// </summary>
    PaymentReminder GeneratePaymentReminder(
        string propertyId,
        string ownerName,
        string ownerEmail,
        decimal amountDue,
        DateTime originalDueDate);

    /// <summary>
    /// Gets the CCR reference for a violation type.
    /// </summary>
    string GetCcrReference(string violationType);

    /// <summary>
    /// Calculates the fine amount based on escalation level and days.
    /// </summary>
    decimal CalculateFineAmount(EscalationLevel level, int daysSinceReport);
}
