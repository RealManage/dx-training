using RealManage.ViolationEscalation.Models;

namespace RealManage.ViolationEscalation.Services;

/// <summary>
/// Service interface for violation escalation.
/// TODO: Implement this interface in EscalationService.cs
/// </summary>
public interface IEscalationService
{
    /// <summary>
    /// Escalates a violation to the next stage if conditions are met.
    /// </summary>
    /// <param name="violation">The violation to escalate</param>
    /// <returns>Updated violation and audit entry, or null if no escalation needed</returns>
    (Violation UpdatedViolation, AuditEntry AuditEntry)? EscalateViolation(Violation violation);

    /// <summary>
    /// Calculates the fine for a violation based on current stage and duration.
    /// </summary>
    /// <param name="stage">Current violation stage</param>
    /// <param name="daysSinceReport">Days since violation was reported</param>
    /// <returns>Calculated fine amount</returns>
    decimal CalculateFine(ViolationStage stage, int daysSinceReport);

    /// <summary>
    /// Determines if a violation should be escalated.
    /// </summary>
    /// <param name="violation">The violation to check</param>
    /// <returns>True if escalation conditions are met</returns>
    bool ShouldEscalate(Violation violation);

    /// <summary>
    /// Gets the next stage for a violation.
    /// </summary>
    /// <param name="currentStage">Current stage</param>
    /// <returns>Next stage, or same stage if already at max</returns>
    ViolationStage GetNextStage(ViolationStage currentStage);
}
