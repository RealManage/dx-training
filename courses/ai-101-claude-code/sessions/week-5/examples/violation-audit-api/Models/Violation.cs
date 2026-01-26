namespace RealManage.ViolationAudit.Models;

/// <summary>
/// Represents an HOA violation record with escalation tracking.
/// Used for tracking rule infractions and calculating fines.
/// </summary>
public record Violation
{
    public required string ViolationId { get; init; }
    public required string PropertyId { get; init; }
    public required ViolationType Type { get; init; }
    public required string Description { get; init; }
    public required DateTime ReportedDate { get; init; }
    public EscalationLevel EscalationLevel { get; set; } = EscalationLevel.Warning;
    public decimal FineAmount { get; set; } = 0m;
    public DateTime? ResolvedDate { get; set; }
    public bool IsResolved => ResolvedDate.HasValue;

    /// <summary>
    /// Calculate days since violation was reported.
    /// </summary>
    public int DaysSinceReported => (DateTime.UtcNow - ReportedDate).Days;
}

/// <summary>
/// Types of HOA violations that can be reported.
/// </summary>
public enum ViolationType
{
    Landscaping,
    Parking,
    Noise,
    Architectural,
    Pet,
    Trash,
    Other
}

/// <summary>
/// Escalation levels for violations based on time elapsed.
/// RealManage policy: 30/60/90 day escalation.
/// </summary>
public enum EscalationLevel
{
    Warning,      // 0-30 days: First notice, no fine
    FirstNotice,  // 31-60 days: $50 fine
    SecondNotice, // 61-90 days: $100 fine + compound interest
    BoardReview   // 90+ days: Board review required
}

/// <summary>
/// Audit entry for SOC 2 compliance tracking.
/// All violation operations must be logged.
/// </summary>
public record AuditEntry
{
    public required string EntryId { get; init; }
    public required DateTime Timestamp { get; init; }
    public required string Action { get; init; }
    public required string UserId { get; init; }
    public required string Details { get; init; }
    public string? ViolationId { get; init; }
    public string? PropertyId { get; init; }
}
