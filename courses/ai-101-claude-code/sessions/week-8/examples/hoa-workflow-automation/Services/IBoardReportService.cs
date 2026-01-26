using RealManage.WorkflowAutomation.Models;

namespace RealManage.WorkflowAutomation.Services;

/// <summary>
/// Service interface for generating board meeting reports.
/// </summary>
public interface IBoardReportService
{
    /// <summary>
    /// Generates a comprehensive board meeting report.
    /// </summary>
    BoardMeetingReport GenerateMeetingReport(
        DateTime meetingDate,
        List<ViolationSummary> violations,
        FinancialSummary financials,
        List<string> attendees);

    /// <summary>
    /// Generates staff recommendations for violations.
    /// </summary>
    string GenerateStaffRecommendation(ViolationSummary violation);

    /// <summary>
    /// Creates action items from meeting decisions.
    /// </summary>
    List<ActionItem> CreateActionItems(
        List<string> decisions,
        string defaultAssignee,
        DateTime meetingDate);
}
