using RealManage.HoaAutomation.Models;

namespace RealManage.HoaAutomation.Services;

/// <summary>
/// Implementation of board meeting management.
/// Contains intentional bugs for training purposes!
/// </summary>
public class BoardMeetingService : IBoardMeetingService
{
    private readonly Dictionary<string, BoardMeeting> _meetings = new();
    private readonly IViolationWorkflowService _violationService;

    public BoardMeetingService(IViolationWorkflowService violationService)
    {
        _violationService = violationService;
    }

    public BoardMeeting CreateMeeting(DateTime meetingDate, string location)
    {
        var meeting = new BoardMeeting
        {
            MeetingId = Guid.NewGuid().ToString(),
            MeetingDate = meetingDate,
            Location = location,
            Status = MeetingStatus.Scheduled
        };

        _meetings[meeting.MeetingId] = meeting;
        return meeting;
    }

    public BoardMeeting? GetMeeting(string meetingId)
    {
        return _meetings.GetValueOrDefault(meetingId);
    }

    public List<AgendaItem> GenerateAgenda(string meetingId)
    {
        var meeting = GetMeeting(meetingId)
            ?? throw new InvalidOperationException($"Meeting {meetingId} not found");

        var agenda = new List<AgendaItem>();
        var itemNumber = 1;

        // Standard opening items
        agenda.Add(new AgendaItem
        {
            ItemId = $"AGENDA-{itemNumber++}",
            Title = "Call to Order",
            Description = "President calls meeting to order",
            Type = AgendaItemType.CallToOrder,
            EstimatedMinutes = 2
        });

        agenda.Add(new AgendaItem
        {
            ItemId = $"AGENDA-{itemNumber++}",
            Title = "Roll Call & Quorum",
            Description = "Secretary takes roll call and confirms quorum",
            Type = AgendaItemType.RollCall,
            EstimatedMinutes = 3
        });

        agenda.Add(new AgendaItem
        {
            ItemId = $"AGENDA-{itemNumber++}",
            Title = "Approve Previous Minutes",
            Description = "Motion to approve minutes from last meeting",
            Type = AgendaItemType.MinutesApproval,
            EstimatedMinutes = 5,
            RequiresVote = true
        });

        // Financial report
        agenda.Add(new AgendaItem
        {
            ItemId = $"AGENDA-{itemNumber++}",
            Title = "Financial Report",
            Description = "Treasurer presents financial summary",
            Type = AgendaItemType.FinancialReport,
            EstimatedMinutes = 10
        });

        // BUG: Should get violations requiring board review (90+ days)
        // but incorrectly gets ALL violations instead!
        var violations = _violationService.GetAllViolations();
        foreach (var violation in violations)
        {
            agenda.Add(new AgendaItem
            {
                ItemId = $"AGENDA-{itemNumber++}",
                Title = $"Violation Review: {violation.PropertyId}",
                Description = $"{violation.Type} - {violation.Description}",
                Type = AgendaItemType.ViolationReview,
                EstimatedMinutes = 5,
                RelatedViolationId = violation.ViolationId,
                RequiresVote = true
            });
        }

        // Standard closing items
        agenda.Add(new AgendaItem
        {
            ItemId = $"AGENDA-{itemNumber++}",
            Title = "New Business",
            Description = "Discussion of new items",
            Type = AgendaItemType.NewBusiness,
            EstimatedMinutes = 15
        });

        agenda.Add(new AgendaItem
        {
            ItemId = $"AGENDA-{itemNumber++}",
            Title = "Homeowner Forum",
            Description = "Open floor for homeowner comments",
            Type = AgendaItemType.HomeownerForum,
            EstimatedMinutes = 10
        });

        agenda.Add(new AgendaItem
        {
            ItemId = $"AGENDA-{itemNumber++}",
            Title = "Adjournment",
            Description = "Motion to adjourn",
            Type = AgendaItemType.Adjournment,
            EstimatedMinutes = 2,
            RequiresVote = true
        });

        // BUG: Doesn't save agenda items to the meeting!
        // Should be: meeting.AgendaItems.AddRange(agenda);

        return agenda;
    }

    public bool AddAttendee(string meetingId, string attendeeName)
    {
        var meeting = GetMeeting(meetingId)
            ?? throw new InvalidOperationException($"Meeting {meetingId} not found");

        // BUG: Allows duplicate attendees
        // Should check if already in list
        meeting.Attendees.Add(attendeeName);

        return meeting.HasQuorum;
    }

    public BoardMeeting StartMeeting(string meetingId)
    {
        var meeting = GetMeeting(meetingId)
            ?? throw new InvalidOperationException($"Meeting {meetingId} not found");

        // BUG: Doesn't check quorum before starting!
        // Should verify HasQuorum is true

        meeting.Status = MeetingStatus.InProgress;
        return meeting;
    }

    public BoardMeeting CompleteMeeting(string meetingId, string minutes)
    {
        var meeting = GetMeeting(meetingId)
            ?? throw new InvalidOperationException($"Meeting {meetingId} not found");

        meeting.Minutes = minutes;
        meeting.Status = MeetingStatus.Completed;

        return meeting;
    }
}
