using RealManage.HoaAutomation.Models;

namespace RealManage.HoaAutomation.Services;

/// <summary>
/// Implementation of violation workflow automation.
/// Contains intentional bugs for training!
/// </summary>
public class ViolationWorkflowService : IViolationWorkflowService
{
    private readonly List<ViolationRecord> _violations = [];

    public ViolationWorkflowService()
    {
        // Seed with sample data for demo
        _violations.AddRange([
            new ViolationRecord
            {
                ViolationId = "V001",
                PropertyId = "PROP-001",
                Type = "Landscaping",
                Description = "Overgrown lawn exceeds 6 inches",
                DaysSinceReported = 95,
                EscalationLevel = "BoardReview",
                FineAmount = 121m
            },
            new ViolationRecord
            {
                ViolationId = "V002",
                PropertyId = "PROP-002",
                Type = "Parking",
                Description = "Vehicle on lawn",
                DaysSinceReported = 45,
                EscalationLevel = "FirstNotice",
                FineAmount = 50m
            },
            new ViolationRecord
            {
                ViolationId = "V003",
                PropertyId = "PROP-003",
                Type = "Architectural",
                Description = "Unapproved fence color",
                DaysSinceReported = 15,
                EscalationLevel = "Warning",
                FineAmount = 0m
            }
        ]);
    }

    public IEnumerable<ViolationRecord> GetAllViolations()
    {
        return _violations.AsReadOnly();
    }

    public IEnumerable<ViolationRecord> GetViolationsForBoardReview()
    {
        // BUG: Uses wrong comparison - should be >= 90, not > 90
        return _violations.Where(v => v.DaysSinceReported > 90);
    }

    public WorkflowResult RunEscalationWorkflow()
    {
        var result = new WorkflowResult
        {
            WorkflowId = Guid.NewGuid().ToString(),
            WorkflowName = "Weekly Escalation",
            StartTime = DateTime.UtcNow
        };

        try
        {
            result.Steps.Add("Gathering open violations...");

            var escalated = 0;
            var noticesGenerated = 0;
            var totalNewFines = 0m;

            foreach (var violation in _violations)
            {
                // BUG: Escalation logic is incomplete
                // Missing 60-90 day case entirely!
                if (violation.DaysSinceReported > 30 && violation.EscalationLevel == "Warning")
                {
                    violation.FineAmount = 50m;
                    escalated++;
                    noticesGenerated++;
                    totalNewFines += 50m;
                    result.Steps.Add($"Escalated {violation.ViolationId} to FirstNotice");
                }
                else if (violation.DaysSinceReported > 90)
                {
                    // BUG: Should calculate compound interest, not flat rate
                    violation.FineAmount = 100m;
                    escalated++;
                    result.Steps.Add($"Escalated {violation.ViolationId} to BoardReview");
                }
            }

            result.Outputs["processed"] = _violations.Count;
            result.Outputs["escalated"] = escalated;
            result.Outputs["notices_generated"] = noticesGenerated;
            result.Outputs["total_new_fines"] = totalNewFines;
            result.Outputs["board_review_required"] = _violations.Count(v => v.DaysSinceReported > 90);

            result.Status = WorkflowStatus.Completed;
            result.EndTime = DateTime.UtcNow;
        }
        catch (Exception ex)
        {
            result.Status = WorkflowStatus.Failed;
            result.ErrorMessage = ex.Message;
            result.EndTime = DateTime.UtcNow;
        }

        return result;
    }

    public string GenerateNoticeLetter(string violationId)
    {
        var violation = _violations.FirstOrDefault(v => v.ViolationId == violationId)
            ?? throw new InvalidOperationException($"Violation {violationId} not found");

        // BUG: Doesn't include all required fields per RealManage template
        // Missing: Property owner name, CCR reference, appeal instructions
        return $"""
            NOTICE OF VIOLATION

            Property: {violation.PropertyId}
            Violation Type: {violation.Type}
            Description: {violation.Description}

            Days Since Report: {violation.DaysSinceReported}
            Current Fine: ${violation.FineAmount}

            Please resolve this violation within 30 days.
            """;
    }
}
