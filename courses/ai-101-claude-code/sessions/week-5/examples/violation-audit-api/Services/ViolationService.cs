using RealManage.ViolationAudit.Models;

namespace RealManage.ViolationAudit.Services;

/// <summary>
/// Implementation of violation management service.
/// Contains intentional bugs for training purposes - can you find them?
/// </summary>
public class ViolationService : IViolationService
{
    private readonly Dictionary<string, Violation> _violations = new();
    private readonly IAuditLogger _auditLogger;

    public ViolationService(IAuditLogger auditLogger)
    {
        _auditLogger = auditLogger;
    }

    public Violation CreateViolation(string propertyId, ViolationType type, string description)
    {
        var violation = new Violation
        {
            ViolationId = Guid.NewGuid().ToString(),
            PropertyId = propertyId,
            Type = type,
            Description = description,
            ReportedDate = DateTime.UtcNow,
            EscalationLevel = EscalationLevel.Warning,
            FineAmount = 0m
        };

        _violations[violation.ViolationId] = violation;

        _auditLogger.LogAction("CREATE_VIOLATION", $"Created violation {violation.ViolationId} for property {propertyId}");

        return violation;
    }

    public Violation? GetViolation(string violationId)
    {
        // BUG: No audit logging for read operations - SOC 2 compliance issue!
        return _violations.GetValueOrDefault(violationId);
    }

    public IEnumerable<Violation> GetViolationsByProperty(string propertyId)
    {
        _auditLogger.LogAction("LIST_VIOLATIONS", $"Listed violations for property {propertyId}");
        return _violations.Values.Where(v => v.PropertyId == propertyId);
    }

    public Violation UpdateEscalation(string violationId)
    {
        var violation = GetViolation(violationId)
            ?? throw new InvalidOperationException($"Violation {violationId} not found");

        if (violation.IsResolved)
            return violation;

        var days = violation.DaysSinceReported;

        // BUG: Wrong boundary conditions - should be > not >=
        // This causes escalation to happen one day early!
        var newLevel = days switch
        {
            >= 90 => EscalationLevel.BoardReview,
            >= 60 => EscalationLevel.SecondNotice,
            >= 30 => EscalationLevel.FirstNotice,
            _ => EscalationLevel.Warning
        };

        if (newLevel != violation.EscalationLevel)
        {
            violation.EscalationLevel = newLevel;
            violation.FineAmount = CalculateFine(violation);
            _auditLogger.LogAction("ESCALATE_VIOLATION", $"Escalated {violationId} to {newLevel}");
        }

        return violation;
    }

    public decimal CalculateFine(Violation violation)
    {
        return violation.EscalationLevel switch
        {
            EscalationLevel.Warning => 0m,
            EscalationLevel.FirstNotice => 50m,
            EscalationLevel.SecondNotice => CalculateCompoundFine(violation),
            EscalationLevel.BoardReview => CalculateCompoundFine(violation),
            _ => 0m
        };
    }

    private decimal CalculateCompoundFine(Violation violation)
    {
        // BUG: Compound interest calculation is wrong!
        // Should use Math.Pow for proper compound interest
        // Current implementation just does simple interest
        var baseFine = 100m;
        var monthsOverdue = (violation.DaysSinceReported - 60) / 30;
        var interestRate = 0.10m; // 10% monthly

        // Wrong: This is simple interest, not compound!
        var totalFine = baseFine + (baseFine * interestRate * monthsOverdue);

        return totalFine;
    }

    public Violation ResolveViolation(string violationId)
    {
        var violation = GetViolation(violationId)
            ?? throw new InvalidOperationException($"Violation {violationId} not found");

        violation.ResolvedDate = DateTime.UtcNow;

        _auditLogger.LogAction("RESOLVE_VIOLATION", $"Resolved violation {violationId}");

        return violation;
    }
}
