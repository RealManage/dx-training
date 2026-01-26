using FluentAssertions;
using RealManage.HoaAutomation.Models;
using RealManage.HoaAutomation.Services;
using Xunit;

namespace RealManage.HoaAutomation.Tests;

public class ViolationWorkflowServiceTests
{
    private readonly ViolationWorkflowService _service;

    public ViolationWorkflowServiceTests()
    {
        _service = new ViolationWorkflowService();
    }

    [Fact]
    public void GetAllViolations_ShouldReturnSeededData()
    {
        // Act
        var violations = _service.GetAllViolations().ToList();

        // Assert
        violations.Should().HaveCount(3);
    }

    [Fact]
    public void GetViolationsForBoardReview_ShouldReturnOnly90PlusDays()
    {
        // Act
        var violations = _service.GetViolationsForBoardReview().ToList();

        // Assert
        violations.Should().AllSatisfy(v => v.DaysSinceReported.Should().BeGreaterOrEqualTo(90));
    }

    [Fact]
    public void RunEscalationWorkflow_ShouldReturnCompletedResult()
    {
        // Act
        var result = _service.RunEscalationWorkflow();

        // Assert
        result.Status.Should().Be(WorkflowStatus.Completed);
        result.EndTime.Should().NotBeNull();
        result.Outputs.Should().ContainKey("processed");
    }

    [Fact]
    public void RunEscalationWorkflow_ShouldRecordSteps()
    {
        // Act
        var result = _service.RunEscalationWorkflow();

        // Assert
        result.Steps.Should().NotBeEmpty();
        result.Steps[0].Should().Contain("Gathering");
    }

    [Fact]
    public void GenerateNoticeLetter_ShouldIncludeViolationDetails()
    {
        // Act
        var letter = _service.GenerateNoticeLetter("V001");

        // Assert
        letter.Should().Contain("PROP-001");
        letter.Should().Contain("Landscaping");
        letter.Should().Contain("NOTICE OF VIOLATION");
    }

    [Fact]
    public void GenerateNoticeLetter_ShouldThrow_WhenNotFound()
    {
        // Act & Assert
        var act = () => _service.GenerateNoticeLetter("non-existent");
        act.Should().Throw<InvalidOperationException>();
    }
}
