using RealManage.WorkflowAutomation.Models;

namespace RealManage.WorkflowAutomation.Services;

/// <summary>
/// Implementation of letter generation service.
/// Contains INTENTIONAL BUGS for training purposes.
/// </summary>
public class LetterGenerationService : ILetterGenerationService
{
    private static readonly Dictionary<string, string> CcrReferences = new()
    {
        ["Lawn Maintenance"] = "Section 4.2.1 - Landscaping Standards",
        ["Architectural"] = "Section 3.1.0 - Architectural Control",
        ["Noise"] = "Section 5.3.2 - Noise and Nuisance",
        ["Parking"] = "Section 6.1.1 - Vehicle and Parking",
        ["Pet"] = "Section 5.4.0 - Pet Regulations",
        ["Trash"] = "Section 4.4.3 - Waste Disposal"
    };

    public ViolationLetter GenerateViolationLetter(
        string propertyId,
        string ownerName,
        string ownerEmail,
        string propertyAddress,
        string violationType,
        int daysSinceReport)
    {
        var escalationLevel = DetermineEscalationLevel(daysSinceReport);
        var fineAmount = CalculateFineAmount(escalationLevel, daysSinceReport);
        var ccrReference = GetCcrReference(violationType);

        // BUG #1: Compliance deadline is calculated from today, but should account for weekends
        // This could result in a deadline falling on a weekend
        var complianceDeadline = DateTime.UtcNow.AddDays(30);

        var letterBody = GenerateLetterBody(
            ownerName,
            propertyAddress,
            violationType,
            ccrReference,
            escalationLevel,
            fineAmount,
            complianceDeadline);

        return new ViolationLetter
        {
            PropertyId = propertyId,
            OwnerName = ownerName,
            OwnerEmail = ownerEmail,
            PropertyAddress = propertyAddress,
            ViolationType = violationType,
            CcrReference = ccrReference,
            EscalationLevel = escalationLevel,
            FineAmount = fineAmount,
            ComplianceDeadline = complianceDeadline,
            LetterBody = letterBody
        };
    }

    public PaymentReminder GeneratePaymentReminder(
        string propertyId,
        string ownerName,
        string ownerEmail,
        decimal amountDue,
        DateTime originalDueDate)
    {
        var daysPastDue = (DateTime.UtcNow - originalDueDate).Days;

        // BUG #2: Late fee calculation should use compound interest after 30 days
        // Currently uses simple percentage regardless of time
        var lateFee = amountDue * 0.10m;

        // BUG #3: Reminder type determination uses wrong thresholds
        // Should be 15/30/45 days, not 10/20/30
        var reminderType = daysPastDue switch
        {
            < 10 => "Friendly",
            < 20 => "Urgent",
            _ => "Final"
        };

        // BUG #4: Due date should be next business day, not just +7 days
        var dueDate = DateTime.UtcNow.AddDays(7);

        return new PaymentReminder
        {
            PropertyId = propertyId,
            OwnerName = ownerName,
            OwnerEmail = ownerEmail,
            AmountDue = amountDue,
            LateFee = lateFee,
            DueDate = dueDate,
            OriginalDueDate = originalDueDate,
            DaysPastDue = daysPastDue,
            ReminderType = reminderType
        };
    }

    public string GetCcrReference(string violationType)
    {
        // BUG #5: Returns empty string instead of "General Rules" for unknown types
        return CcrReferences.GetValueOrDefault(violationType, "");
    }

    public decimal CalculateFineAmount(EscalationLevel level, int daysSinceReport)
    {
        // BUG #6: Compound interest calculation is wrong
        // Should be: base * (1.10 ^ months), but uses days incorrectly
        var baseFine = level switch
        {
            EscalationLevel.Warning => 0m,
            EscalationLevel.FirstNotice => 50m,
            EscalationLevel.SecondNotice => 100m,
            EscalationLevel.BoardReview => 200m,
            EscalationLevel.LegalAction => 500m,
            _ => 0m
        };

        if (level >= EscalationLevel.SecondNotice && daysSinceReport > 60)
        {
            // BUG: Using days instead of months for compound calculation
            var compoundPeriods = daysSinceReport - 60;
            baseFine *= (decimal)Math.Pow(1.10, compoundPeriods);
        }

        return baseFine;
    }

    private static EscalationLevel DetermineEscalationLevel(int daysSinceReport)
    {
        return daysSinceReport switch
        {
            <= 30 => EscalationLevel.Warning,
            <= 60 => EscalationLevel.FirstNotice,
            <= 90 => EscalationLevel.SecondNotice,
            <= 120 => EscalationLevel.BoardReview,
            _ => EscalationLevel.LegalAction
        };
    }

    private static string GenerateLetterBody(
        string ownerName,
        string propertyAddress,
        string violationType,
        string ccrReference,
        EscalationLevel level,
        decimal fineAmount,
        DateTime complianceDeadline)
    {
        var levelText = level switch
        {
            EscalationLevel.Warning => "Initial Warning",
            EscalationLevel.FirstNotice => "First Notice",
            EscalationLevel.SecondNotice => "Second Notice",
            EscalationLevel.BoardReview => "Board Review Notice",
            EscalationLevel.LegalAction => "Legal Action Notice",
            _ => "Notice"
        };

        // BUG #7: Missing appeal instructions in letter body
        return $"""
            Dear {ownerName},

            RE: {levelText} - {violationType} Violation
            Property: {propertyAddress}

            This letter serves as a {levelText.ToLower()} regarding a violation of
            the Community Covenants, Conditions, and Restrictions (CC&Rs).

            Violation Type: {violationType}
            CCR Reference: {ccrReference}

            {(fineAmount > 0 ? $"Current Fine Amount: ${fineAmount:F2}" : "No fine has been assessed at this time.")}

            Please resolve this matter by {complianceDeadline:MMMM dd, yyyy}.

            Failure to comply may result in additional fines and/or escalation to
            the Board of Directors.

            Sincerely,
            RealManage HOA Management
            """;
    }
}
