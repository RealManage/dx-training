using RealManage.HoaAutomation.Models;

namespace RealManage.HoaAutomation.Services;

/// <summary>
/// Service interface for board meeting management.
/// </summary>
public interface IBoardMeetingService
{
    /// <summary>
    /// Creates a new board meeting.
    /// </summary>
    BoardMeeting CreateMeeting(DateTime meetingDate, string location);

    /// <summary>
    /// Gets a meeting by ID.
    /// </summary>
    BoardMeeting? GetMeeting(string meetingId);

    /// <summary>
    /// Generates agenda from open violations and pending items.
    /// </summary>
    List<AgendaItem> GenerateAgenda(string meetingId);

    /// <summary>
    /// Adds attendee to meeting and checks quorum.
    /// </summary>
    bool AddAttendee(string meetingId, string attendeeName);

    /// <summary>
    /// Starts the meeting if quorum is met.
    /// </summary>
    BoardMeeting StartMeeting(string meetingId);

    /// <summary>
    /// Completes the meeting and records minutes.
    /// </summary>
    BoardMeeting CompleteMeeting(string meetingId, string minutes);
}
