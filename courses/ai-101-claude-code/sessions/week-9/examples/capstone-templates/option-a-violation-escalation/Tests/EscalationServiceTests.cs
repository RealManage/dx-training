using RealManage.ViolationEscalation.Models;
using RealManage.ViolationEscalation.Services;

namespace RealManage.ViolationEscalation.Tests;

/// <summary>
/// Starter test file for EscalationService.
///
/// These tests are intentionally failing (NotImplementedException).
/// Use TDD to make them pass one by one.
///
/// Add more tests as needed for edge cases.
/// </summary>
public class EscalationServiceTests
{
    private readonly EscalationService _service = new();

    #region GetNextStage Tests

    [Fact]
    public void GetNextStage_FromWarning_ReturnsFirstNotice()
    {
        // Act
        var nextStage = _service.GetNextStage(ViolationStage.Warning);

        // Assert
        nextStage.Should().Be(ViolationStage.FirstNotice);
    }

    [Fact]
    public void GetNextStage_FromFirstNotice_ReturnsSecondNotice()
    {
        // Act
        var nextStage = _service.GetNextStage(ViolationStage.FirstNotice);

        // Assert
        nextStage.Should().Be(ViolationStage.SecondNotice);
    }

    [Fact]
    public void GetNextStage_FromLegalAction_StaysAtLegalAction()
    {
        // Act
        var nextStage = _service.GetNextStage(ViolationStage.LegalAction);

        // Assert
        nextStage.Should().Be(ViolationStage.LegalAction);
    }

    #endregion

    #region CalculateFine Tests

    [Fact]
    public void CalculateFine_ForWarning_ReturnsZero()
    {
        // Act
        var fine = _service.CalculateFine(ViolationStage.Warning, 15);

        // Assert
        fine.Should().Be(0m);
    }

    [Fact]
    public void CalculateFine_ForFirstNotice_Returns50()
    {
        // Act
        var fine = _service.CalculateFine(ViolationStage.FirstNotice, 45);

        // Assert
        fine.Should().Be(50m);
    }

    [Fact]
    public void CalculateFine_ForSecondNotice_Returns100Base()
    {
        // At exactly 61 days, no compound interest yet
        // Act
        var fine = _service.CalculateFine(ViolationStage.SecondNotice, 61);

        // Assert
        fine.Should().Be(100m);
    }

    [Fact]
    public void CalculateFine_ForSecondNotice_WithCompoundInterest()
    {
        // At 91 days (1 month over 60), should have 10% compound
        // Act
        var fine = _service.CalculateFine(ViolationStage.SecondNotice, 91);

        // Assert - $100 * 1.10 = $110
        fine.Should().BeApproximately(110m, 0.01m);
    }

    #endregion

    #region ShouldEscalate Tests

    [Fact]
    public void ShouldEscalate_WhenResolved_ReturnsFalse()
    {
        // Arrange
        var violation = new Violation
        {
            Id = "V001",
            PropertyId = "PROP-001",
            ViolationType = "Lawn Maintenance",
            ReportedDate = DateTime.UtcNow.AddDays(-60),
            CurrentStage = ViolationStage.FirstNotice,
            IsResolved = true
        };

        // Act
        var shouldEscalate = _service.ShouldEscalate(violation);

        // Assert
        shouldEscalate.Should().BeFalse();
    }

    [Fact]
    public void ShouldEscalate_WhenUnder30Days_ReturnsFalse()
    {
        // Arrange
        var violation = new Violation
        {
            Id = "V001",
            PropertyId = "PROP-001",
            ViolationType = "Lawn Maintenance",
            ReportedDate = DateTime.UtcNow.AddDays(-15),
            CurrentStage = ViolationStage.Warning
        };

        // Act
        var shouldEscalate = _service.ShouldEscalate(violation);

        // Assert
        shouldEscalate.Should().BeFalse();
    }

    [Fact]
    public void ShouldEscalate_WhenOver30Days_ReturnsTrue()
    {
        // Arrange
        var violation = new Violation
        {
            Id = "V001",
            PropertyId = "PROP-001",
            ViolationType = "Lawn Maintenance",
            ReportedDate = DateTime.UtcNow.AddDays(-35),
            CurrentStage = ViolationStage.Warning
        };

        // Act
        var shouldEscalate = _service.ShouldEscalate(violation);

        // Assert
        shouldEscalate.Should().BeTrue();
    }

    #endregion

    #region EscalateViolation Tests

    [Fact]
    public void EscalateViolation_WhenShouldEscalate_ReturnsUpdatedViolation()
    {
        // Arrange
        var violation = new Violation
        {
            Id = "V001",
            PropertyId = "PROP-001",
            ViolationType = "Lawn Maintenance",
            ReportedDate = DateTime.UtcNow.AddDays(-35),
            CurrentStage = ViolationStage.Warning
        };

        // Act
        var result = _service.EscalateViolation(violation);

        // Assert
        result.Should().NotBeNull();
        result!.Value.UpdatedViolation.CurrentStage.Should().Be(ViolationStage.FirstNotice);
        result.Value.AuditEntry.Should().NotBeNull();
    }

    [Fact]
    public void EscalateViolation_WhenNoEscalationNeeded_ReturnsNull()
    {
        // Arrange
        var violation = new Violation
        {
            Id = "V001",
            PropertyId = "PROP-001",
            ViolationType = "Lawn Maintenance",
            ReportedDate = DateTime.UtcNow.AddDays(-15),
            CurrentStage = ViolationStage.Warning
        };

        // Act
        var result = _service.EscalateViolation(violation);

        // Assert
        result.Should().BeNull();
    }

    #endregion

    // TODO: Add more tests for:
    // - Edge cases at exactly 30 days
    // - Multiple escalation cycles
    // - Audit entry content verification
    // - Compound interest over multiple months
}
