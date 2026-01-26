using RealManage.WorkflowAutomation.Models;
using RealManage.WorkflowAutomation.Services;

namespace RealManage.WorkflowAutomation.Tests;

public class LetterGenerationServiceTests
{
    private readonly LetterGenerationService _service = new();

    [Fact]
    public void GenerateViolationLetter_WithValidInput_ReturnsLetter()
    {
        // Arrange
        var propertyId = "PROP-001";
        var ownerName = "John Smith";
        var ownerEmail = "john@example.com";
        var propertyAddress = "123 Oak Street";
        var violationType = "Lawn Maintenance";
        var daysSinceReport = 15;

        // Act
        var letter = _service.GenerateViolationLetter(
            propertyId, ownerName, ownerEmail, propertyAddress, violationType, daysSinceReport);

        // Assert
        letter.PropertyId.Should().Be(propertyId);
        letter.OwnerName.Should().Be(ownerName);
        letter.EscalationLevel.Should().Be(EscalationLevel.Warning);
        letter.FineAmount.Should().Be(0m); // No fine for warning
    }

    [Fact]
    public void GenerateViolationLetter_At45Days_ReturnsFirstNotice()
    {
        // Act
        var letter = _service.GenerateViolationLetter(
            "PROP-001", "Owner", "email@test.com", "123 Main St", "Lawn Maintenance", 45);

        // Assert
        letter.EscalationLevel.Should().Be(EscalationLevel.FirstNotice);
        letter.FineAmount.Should().Be(50m);
    }

    [Fact]
    public void GenerateViolationLetter_At75Days_ReturnsSecondNotice()
    {
        // Act
        var letter = _service.GenerateViolationLetter(
            "PROP-001", "Owner", "email@test.com", "123 Main St", "Lawn Maintenance", 75);

        // Assert
        letter.EscalationLevel.Should().Be(EscalationLevel.SecondNotice);
    }

    [Fact]
    public void GetCcrReference_WithKnownType_ReturnsReference()
    {
        // Act
        var reference = _service.GetCcrReference("Lawn Maintenance");

        // Assert
        reference.Should().Be("Section 4.2.1 - Landscaping Standards");
    }

    [Fact]
    public void GetCcrReference_WithUnknownType_ReturnsEmptyString()
    {
        // This test documents the BUG - should return "General Rules"
        // Act
        var reference = _service.GetCcrReference("Unknown Violation");

        // Assert - documenting current (buggy) behavior
        reference.Should().BeEmpty();
    }

    [Fact]
    public void GeneratePaymentReminder_CalculatesLateFee()
    {
        // Arrange
        var originalDueDate = DateTime.UtcNow.AddDays(-45);

        // Act
        var reminder = _service.GeneratePaymentReminder(
            "PROP-001", "Owner", "email@test.com", 500m, originalDueDate);

        // Assert
        reminder.LateFee.Should().Be(50m); // 10% of 500
        reminder.DaysPastDue.Should().BeGreaterThanOrEqualTo(44);
    }

    [Fact]
    public void GeneratePaymentReminder_SetsReminderType()
    {
        // Arrange - 5 days past due
        var originalDueDate = DateTime.UtcNow.AddDays(-5);

        // Act
        var reminder = _service.GeneratePaymentReminder(
            "PROP-001", "Owner", "email@test.com", 100m, originalDueDate);

        // Assert - documenting current behavior (uses 10 day threshold, not 15)
        reminder.ReminderType.Should().Be("Friendly");
    }

    [Fact]
    public void CalculateFineAmount_ForWarning_ReturnsZero()
    {
        // Act
        var fine = _service.CalculateFineAmount(EscalationLevel.Warning, 15);

        // Assert
        fine.Should().Be(0m);
    }

    [Fact]
    public void CalculateFineAmount_ForFirstNotice_Returns50()
    {
        // Act
        var fine = _service.CalculateFineAmount(EscalationLevel.FirstNotice, 45);

        // Assert
        fine.Should().Be(50m);
    }

    [Fact]
    public void CalculateFineAmount_ForSecondNoticeOver60Days_AppliesCompound()
    {
        // This test documents the compound interest bug
        // Act
        var fine = _service.CalculateFineAmount(EscalationLevel.SecondNotice, 75);

        // Assert - documenting buggy behavior (uses days instead of months)
        fine.Should().BeGreaterThan(100m);
    }
}
