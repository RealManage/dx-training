using RealManage.ViolationAudit.Models;

namespace RealManage.ViolationAudit.Services;

/// <summary>
/// Interface for SOC 2 compliant audit logging.
/// All operations that modify or access violation data must be logged.
/// </summary>
public interface IAuditLogger
{
    /// <summary>
    /// Log an action for audit trail.
    /// </summary>
    void LogAction(string action, string details, string? violationId = null, string? propertyId = null);

    /// <summary>
    /// Get all audit entries.
    /// </summary>
    IEnumerable<AuditEntry> GetAuditEntries();

    /// <summary>
    /// Get audit entries for a specific violation.
    /// </summary>
    IEnumerable<AuditEntry> GetEntriesForViolation(string violationId);
}
