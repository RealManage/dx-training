using RealManage.HoaAutomation.Models;

namespace RealManage.HoaAutomation.Services;

/// <summary>
/// Simplified violation record for workflow service.
/// </summary>
public record ViolationRecord
{
    public required string ViolationId { get; init; }
    public required string PropertyId { get; init; }
    public required string Type { get; init; }
    public required string Description { get; init; }
    public required int DaysSinceReported { get; init; }
    public required string EscalationLevel { get; init; }
    public decimal FineAmount { get; set; }
}

/// <summary>
/// Service interface for violation workflow automation.
/// </summary>
public interface IViolationWorkflowService
{
    /// <summary>
    /// Gets all violations.
    /// </summary>
    IEnumerable<ViolationRecord> GetAllViolations();

    /// <summary>
    /// Gets violations requiring board review (90+ days).
    /// </summary>
    IEnumerable<ViolationRecord> GetViolationsForBoardReview();

    /// <summary>
    /// Runs the weekly escalation workflow.
    /// </summary>
    WorkflowResult RunEscalationWorkflow();

    /// <summary>
    /// Generates notice letter for a violation.
    /// </summary>
    string GenerateNoticeLetter(string violationId);
}
