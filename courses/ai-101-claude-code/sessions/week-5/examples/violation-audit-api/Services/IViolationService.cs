using RealManage.ViolationAudit.Models;

namespace RealManage.ViolationAudit.Services;

/// <summary>
/// Service interface for managing HOA violations.
/// Implementations must ensure SOC 2 compliance with audit logging.
/// </summary>
public interface IViolationService
{
    /// <summary>
    /// Creates a new violation record.
    /// </summary>
    Violation CreateViolation(string propertyId, ViolationType type, string description);

    /// <summary>
    /// Gets a violation by its ID.
    /// </summary>
    Violation? GetViolation(string violationId);

    /// <summary>
    /// Gets all violations for a property.
    /// </summary>
    IEnumerable<Violation> GetViolationsByProperty(string propertyId);

    /// <summary>
    /// Updates the escalation level based on days elapsed.
    /// Applies fines according to RealManage 30/60/90 policy.
    /// </summary>
    Violation UpdateEscalation(string violationId);

    /// <summary>
    /// Calculates the fine amount with compound interest.
    /// RealManage policy: 10% monthly compound interest after 60 days.
    /// </summary>
    decimal CalculateFine(Violation violation);

    /// <summary>
    /// Marks a violation as resolved.
    /// </summary>
    Violation ResolveViolation(string violationId);
}
