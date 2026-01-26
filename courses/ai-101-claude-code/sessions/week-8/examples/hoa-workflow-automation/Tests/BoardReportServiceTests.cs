using RealManage.WorkflowAutomation.Models;
using RealManage.WorkflowAutomation.Services;

namespace RealManage.WorkflowAutomation.Tests;

public class BoardReportServiceTests
{
    private readonly BoardReportService _service = new();

    [Fact]
    public void GenerateMeetingReport_WithValidInput_ReturnsReport()
    {
        // Arrange
        var meetingDate = new DateTime(2025, 2, 4);
        var violations = new List<ViolationSummary>
        {
            new()
            {
                PropertyId = "PROP-001",
                PropertyAddress = "123 Oak St",
                ViolationType = "Lawn Maintenance",
                DaysSinceReport = 95,
                CurrentLevel = EscalationLevel.BoardReview,
                TotalFines = 150m,
                StaffRecommendation = "Escalate"
            }
        };
        var financials = new FinancialSummary
        {
            TotalDuesCollected = 50000m,
            TotalDuesOutstanding = 5000m,
            LateFeeCollections = 500m,
            ViolationFineCollections = 300m,
            ReserveFundBalance = 100000m,
            BudgetVariance = -2000m
        };
        var attendees = new List<string> { "President", "Treasurer", "Secretary" };

        // Act
        var report = _service.GenerateMeetingReport(meetingDate, violations, financials, attendees);

        // Assert
        report.MeetingDate.Should().Be(meetingDate);
        report.ViolationSummaries.Should().HaveCount(1);
        report.Attendees.Should().HaveCount(3);
        report.ReportTitle.Should().Contain("February 2025");
    }

    [Fact]
    public void GenerateMeetingReport_WithLessThanThreeAttendees_StillGenerates()
    {
        // This test documents the quorum bug - should require 3 attendees
        var violations = new List<ViolationSummary>();
        var financials = new FinancialSummary
        {
            TotalDuesCollected = 0, TotalDuesOutstanding = 0, LateFeeCollections = 0,
            ViolationFineCollections = 0, ReserveFundBalance = 0, BudgetVariance = 0
        };

        // Act - only 2 attendees (no quorum)
        var report = _service.GenerateMeetingReport(
            DateTime.Today,
            violations,
            financials,
            ["President", "Treasurer"]);

        // Assert - report is generated anyway (bug!)
        report.Should().NotBeNull();
        report.Attendees.Should().HaveCount(2);
    }

    [Fact]
    public void GenerateStaffRecommendation_ForWarning_ReturnsMonitoringAdvice()
    {
        // Arrange
        var violation = new ViolationSummary
        {
            PropertyId = "PROP-001",
            PropertyAddress = "123 Oak St",
            ViolationType = "Lawn Maintenance",
            DaysSinceReport = 15,
            CurrentLevel = EscalationLevel.Warning,
            TotalFines = 0m,
            StaffRecommendation = ""
        };

        // Act
        var recommendation = _service.GenerateStaffRecommendation(violation);

        // Assert
        recommendation.Should().Contain("monitoring");
    }

    [Fact]
    public void GenerateStaffRecommendation_ForBoardReview_ReturnsFallback()
    {
        // This test documents the missing BoardReview case
        var violation = new ViolationSummary
        {
            PropertyId = "PROP-001",
            PropertyAddress = "123 Oak St",
            ViolationType = "Lawn Maintenance",
            DaysSinceReport = 100,
            CurrentLevel = EscalationLevel.BoardReview,
            TotalFines = 200m,
            StaffRecommendation = ""
        };

        // Act
        var recommendation = _service.GenerateStaffRecommendation(violation);

        // Assert - falls through to default case (bug!)
        recommendation.Should().Contain("Review and determine");
    }

    [Fact]
    public void CreateActionItems_WithDecisions_CreatesItems()
    {
        // Arrange
        var decisions = new List<string>
        {
            "Approve fine increase for 123 Oak St",
            "Grant 30-day extension for 456 Elm St"
        };
        var meetingDate = DateTime.Today;

        // Act
        var items = _service.CreateActionItems(decisions, "Property Manager", meetingDate);

        // Assert
        items.Should().HaveCount(2);
        items.All(i => i.Priority == "Medium").Should().BeTrue(); // Documents priority bug
        items.All(i => i.DueDate == meetingDate.AddDays(14)).Should().BeTrue();
    }

    [Fact]
    public void GenerateMeetingReport_CreatesActionItemsForAllViolations()
    {
        // This documents the bug where action items are created for warnings too
        var violations = new List<ViolationSummary>
        {
            new()
            {
                PropertyId = "PROP-001",
                PropertyAddress = "123 Oak St",
                ViolationType = "Lawn Maintenance",
                DaysSinceReport = 15, // Warning level
                CurrentLevel = EscalationLevel.Warning,
                TotalFines = 0m,
                StaffRecommendation = ""
            }
        };
        var financials = new FinancialSummary
        {
            TotalDuesCollected = 0, TotalDuesOutstanding = 0, LateFeeCollections = 0,
            ViolationFineCollections = 0, ReserveFundBalance = 0, BudgetVariance = 0
        };

        // Act
        var report = _service.GenerateMeetingReport(
            DateTime.Today, violations, financials, ["P", "T", "S"]);

        // Assert - action item created even for warning (bug!)
        report.ActionItems.Should().HaveCount(1);
    }
}
