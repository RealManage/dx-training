namespace RealManage.HoaAutomation.Models;

/// <summary>
/// Represents a board meeting with agenda items.
/// </summary>
public record BoardMeeting
{
    public required string MeetingId { get; init; }
    public required DateTime MeetingDate { get; init; }
    public required string Location { get; init; }
    public MeetingStatus Status { get; set; } = MeetingStatus.Scheduled;
    public List<AgendaItem> AgendaItems { get; init; } = [];
    public List<string> Attendees { get; init; } = [];
    public string? Minutes { get; set; }

    /// <summary>
    /// Check if quorum is met (minimum 3 board members).
    /// </summary>
    public bool HasQuorum => Attendees.Count >= 3;
}

/// <summary>
/// Individual agenda item for a board meeting.
/// </summary>
public record AgendaItem
{
    public required string ItemId { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required AgendaItemType Type { get; init; }
    public int EstimatedMinutes { get; init; } = 5;
    public string? RelatedViolationId { get; init; }
    public string? Resolution { get; set; }
    public bool RequiresVote { get; init; } = false;
}

public enum MeetingStatus
{
    Scheduled,
    InProgress,
    Completed,
    Cancelled
}

public enum AgendaItemType
{
    CallToOrder,
    RollCall,
    MinutesApproval,
    FinancialReport,
    ViolationReview,
    NewBusiness,
    HomeownerForum,
    Adjournment
}

/// <summary>
/// Workflow execution result for automation tracking.
/// </summary>
public record WorkflowResult
{
    public required string WorkflowId { get; init; }
    public required string WorkflowName { get; init; }
    public required DateTime StartTime { get; init; }
    public DateTime? EndTime { get; set; }
    public WorkflowStatus Status { get; set; } = WorkflowStatus.Running;
    public List<string> Steps { get; init; } = [];
    public Dictionary<string, object> Outputs { get; init; } = [];
    public string? ErrorMessage { get; set; }
}

public enum WorkflowStatus
{
    Running,
    Completed,
    Failed,
    Cancelled
}
