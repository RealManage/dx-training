using RealManage.WorkflowAutomation.Models;

namespace RealManage.WorkflowAutomation.Services;

/// <summary>
/// Implementation of board report generation service.
/// Contains INTENTIONAL BUGS for training purposes.
/// </summary>
public class BoardReportService : IBoardReportService
{
    public BoardMeetingReport GenerateMeetingReport(
        DateTime meetingDate,
        List<ViolationSummary> violations,
        FinancialSummary financials,
        List<string> attendees)
    {
        // BUG #1: Doesn't validate minimum quorum (3 board members)
        // Meeting report should not be generated without quorum

        var actionItems = GenerateDefaultActionItems(violations, meetingDate);

        return new BoardMeetingReport
        {
            MeetingDate = meetingDate,
            ReportTitle = $"HOA Board Meeting Report - {meetingDate:MMMM yyyy}",
            ViolationSummaries = violations,
            Financials = financials,
            ActionItems = actionItems,
            Attendees = attendees
        };
    }

    public string GenerateStaffRecommendation(ViolationSummary violation)
    {
        // BUG #2: Wrong threshold for legal action recommendation
        // Should recommend legal action at 120+ days, not 150+
        if (violation.DaysSinceReport > 150)
        {
            return "Recommend initiating legal proceedings";
        }

        // BUG #3: Missing recommendation for BoardReview level
        // Should have specific recommendation for 90-120 day violations
        return violation.CurrentLevel switch
        {
            EscalationLevel.Warning => "Continue monitoring; no board action required",
            EscalationLevel.FirstNotice => "Issue follow-up notice; consider site inspection",
            EscalationLevel.SecondNotice => "Escalate to board review at next meeting",
            EscalationLevel.LegalAction => "Proceed with legal action per board approval",
            _ => "Review and determine appropriate action"
        };
    }

    public List<ActionItem> CreateActionItems(
        List<string> decisions,
        string defaultAssignee,
        DateTime meetingDate)
    {
        var actionItems = new List<ActionItem>();

        foreach (var decision in decisions)
        {
            // BUG #4: Due date calculation doesn't account for priority
            // High priority should be 7 days, Medium 14, Low 30
            var dueDate = meetingDate.AddDays(14);

            // BUG #5: Priority is always set to "Medium" regardless of content
            var priority = "Medium";

            actionItems.Add(new ActionItem
            {
                Description = decision,
                AssignedTo = defaultAssignee,
                DueDate = dueDate,
                Priority = priority
            });
        }

        return actionItems;
    }

    private List<ActionItem> GenerateDefaultActionItems(
        List<ViolationSummary> violations,
        DateTime meetingDate)
    {
        var items = new List<ActionItem>();

        // BUG #6: Should only create action items for violations requiring board action
        // Currently creates items for ALL violations, including warnings
        foreach (var violation in violations)
        {
            items.Add(new ActionItem
            {
                Description = $"Follow up on {violation.ViolationType} violation at {violation.PropertyAddress}",
                AssignedTo = "Property Manager",
                DueDate = meetingDate.AddDays(14),
                Priority = violation.CurrentLevel >= EscalationLevel.BoardReview ? "High" : "Medium"
            });
        }

        return items;
    }
}
