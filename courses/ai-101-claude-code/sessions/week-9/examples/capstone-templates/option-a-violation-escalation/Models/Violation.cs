namespace RealManage.ViolationEscalation.Models;

/// <summary>
/// Violation escalation stages.
/// TODO: Implement state machine transitions.
/// </summary>
public enum ViolationStage
{
    Warning = 0,
    FirstNotice = 1,
    SecondNotice = 2,
    BoardReview = 3,
    LegalAction = 4,
    Resolved = 5
}

/// <summary>
/// Represents an HOA violation requiring escalation tracking.
/// TODO: Add required properties and validation.
/// </summary>
public record Violation
{
    public required string Id { get; init; }
    public required string PropertyId { get; init; }
    public required string ViolationType { get; init; }
    public required DateTime ReportedDate { get; init; }
    public ViolationStage CurrentStage { get; init; } = ViolationStage.Warning;
    public DateTime? LastEscalationDate { get; init; }
    public decimal TotalFines { get; init; } = 0m;
    public bool IsResolved { get; init; } = false;

    // TODO: Add computed properties for:
    // - DaysSinceReport
    // - DaysAtCurrentStage
    // - NextEscalationDate
    // - ShouldEscalate
}

/// <summary>
/// Audit entry for SOC 2 compliance.
/// TODO: Add all required audit fields.
/// </summary>
public record AuditEntry
{
    public required string Id { get; init; }
    public required string ViolationId { get; init; }
    public required string Action { get; init; }
    public required ViolationStage FromStage { get; init; }
    public required ViolationStage ToStage { get; init; }
    public required decimal FineAmount { get; init; }
    public required DateTime Timestamp { get; init; }
    public required string UserId { get; init; }

    // TODO: Add fields for:
    // - IP address (for SOC 2)
    // - Session ID
    // - Change details JSON
}
