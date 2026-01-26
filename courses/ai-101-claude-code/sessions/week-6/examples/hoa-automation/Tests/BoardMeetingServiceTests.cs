using FluentAssertions;
using Moq;
using RealManage.HoaAutomation.Models;
using RealManage.HoaAutomation.Services;
using Xunit;

namespace RealManage.HoaAutomation.Tests;

public class BoardMeetingServiceTests
{
    private readonly Mock<IViolationWorkflowService> _mockViolationService;
    private readonly BoardMeetingService _service;

    public BoardMeetingServiceTests()
    {
        _mockViolationService = new Mock<IViolationWorkflowService>();
        _mockViolationService.Setup(x => x.GetAllViolations()).Returns([]);
        _service = new BoardMeetingService(_mockViolationService.Object);
    }

    [Fact]
    public void CreateMeeting_ShouldCreateWithCorrectProperties()
    {
        // Arrange
        var meetingDate = new DateTime(2025, 2, 4, 19, 0, 0);
        var location = "Community Clubhouse";

        // Act
        var meeting = _service.CreateMeeting(meetingDate, location);

        // Assert
        meeting.MeetingDate.Should().Be(meetingDate);
        meeting.Location.Should().Be(location);
        meeting.Status.Should().Be(MeetingStatus.Scheduled);
        meeting.HasQuorum.Should().BeFalse();
    }

    [Fact]
    public void GetMeeting_ShouldReturnMeeting_WhenExists()
    {
        // Arrange
        var meeting = _service.CreateMeeting(DateTime.Now, "Test Location");

        // Act
        var result = _service.GetMeeting(meeting.MeetingId);

        // Assert
        result.Should().NotBeNull();
        result!.MeetingId.Should().Be(meeting.MeetingId);
    }

    [Fact]
    public void GetMeeting_ShouldReturnNull_WhenNotExists()
    {
        // Act
        var result = _service.GetMeeting("non-existent");

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public void GenerateAgenda_ShouldIncludeStandardItems()
    {
        // Arrange
        var meeting = _service.CreateMeeting(DateTime.Now, "Test Location");

        // Act
        var agenda = _service.GenerateAgenda(meeting.MeetingId);

        // Assert
        agenda.Should().Contain(x => x.Type == AgendaItemType.CallToOrder);
        agenda.Should().Contain(x => x.Type == AgendaItemType.RollCall);
        agenda.Should().Contain(x => x.Type == AgendaItemType.MinutesApproval);
        agenda.Should().Contain(x => x.Type == AgendaItemType.Adjournment);
    }

    [Fact]
    public void AddAttendee_ShouldAddToList()
    {
        // Arrange
        var meeting = _service.CreateMeeting(DateTime.Now, "Test Location");

        // Act
        _service.AddAttendee(meeting.MeetingId, "John Smith");

        // Assert
        var result = _service.GetMeeting(meeting.MeetingId);
        result!.Attendees.Should().Contain("John Smith");
    }

    [Fact]
    public void AddAttendee_ShouldReturnTrue_WhenQuorumReached()
    {
        // Arrange
        var meeting = _service.CreateMeeting(DateTime.Now, "Test Location");
        _service.AddAttendee(meeting.MeetingId, "Member 1");
        _service.AddAttendee(meeting.MeetingId, "Member 2");

        // Act
        var hasQuorum = _service.AddAttendee(meeting.MeetingId, "Member 3");

        // Assert
        hasQuorum.Should().BeTrue();
    }

    [Fact]
    public void StartMeeting_ShouldChangeStatusToInProgress()
    {
        // Arrange
        var meeting = _service.CreateMeeting(DateTime.Now, "Test Location");

        // Act
        var started = _service.StartMeeting(meeting.MeetingId);

        // Assert
        started.Status.Should().Be(MeetingStatus.InProgress);
    }

    [Fact]
    public void CompleteMeeting_ShouldChangeStatusAndSetMinutes()
    {
        // Arrange
        var meeting = _service.CreateMeeting(DateTime.Now, "Test Location");
        _service.StartMeeting(meeting.MeetingId);
        var minutes = "Meeting minutes content...";

        // Act
        var completed = _service.CompleteMeeting(meeting.MeetingId, minutes);

        // Assert
        completed.Status.Should().Be(MeetingStatus.Completed);
        completed.Minutes.Should().Be(minutes);
    }
}
