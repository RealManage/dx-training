using FluentAssertions;
using Moq;
using RealManage.ViolationAudit.Models;
using RealManage.ViolationAudit.Services;
using Xunit;

namespace RealManage.ViolationAudit.Tests;

public class ViolationServiceTests
{
    private readonly Mock<IAuditLogger> _mockAuditLogger;
    private readonly ViolationService _service;

    public ViolationServiceTests()
    {
        _mockAuditLogger = new Mock<IAuditLogger>();
        _service = new ViolationService(_mockAuditLogger.Object);
    }

    [Fact]
    public void CreateViolation_ShouldCreateWithCorrectProperties()
    {
        // Arrange
        var propertyId = "PROP-001";
        var type = ViolationType.Landscaping;
        var description = "Overgrown lawn";

        // Act
        var violation = _service.CreateViolation(propertyId, type, description);

        // Assert
        violation.PropertyId.Should().Be(propertyId);
        violation.Type.Should().Be(type);
        violation.Description.Should().Be(description);
        violation.EscalationLevel.Should().Be(EscalationLevel.Warning);
        violation.FineAmount.Should().Be(0m);
        violation.IsResolved.Should().BeFalse();
    }

    [Fact]
    public void CreateViolation_ShouldLogAuditEntry()
    {
        // Arrange
        var propertyId = "PROP-001";

        // Act
        _service.CreateViolation(propertyId, ViolationType.Parking, "Blocked driveway");

        // Assert
        _mockAuditLogger.Verify(
            x => x.LogAction("CREATE_VIOLATION", It.IsAny<string>(), null, null),
            Times.Once);
    }

    [Fact]
    public void GetViolation_ShouldReturnViolation_WhenExists()
    {
        // Arrange
        var violation = _service.CreateViolation("PROP-001", ViolationType.Noise, "Loud music");

        // Act
        var result = _service.GetViolation(violation.ViolationId);

        // Assert
        result.Should().NotBeNull();
        result!.ViolationId.Should().Be(violation.ViolationId);
    }

    [Fact]
    public void GetViolation_ShouldReturnNull_WhenNotExists()
    {
        // Act
        var result = _service.GetViolation("non-existent-id");

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public void GetViolationsByProperty_ShouldReturnMatchingViolations()
    {
        // Arrange
        var propertyId = "PROP-001";
        _service.CreateViolation(propertyId, ViolationType.Landscaping, "Issue 1");
        _service.CreateViolation(propertyId, ViolationType.Parking, "Issue 2");
        _service.CreateViolation("PROP-002", ViolationType.Noise, "Other property");

        // Act
        var results = _service.GetViolationsByProperty(propertyId).ToList();

        // Assert
        results.Should().HaveCount(2);
        results.Should().AllSatisfy(v => v.PropertyId.Should().Be(propertyId));
    }

    [Fact]
    public void CalculateFine_Warning_ShouldReturnZero()
    {
        // Arrange
        var violation = _service.CreateViolation("PROP-001", ViolationType.Landscaping, "Test");

        // Act
        var fine = _service.CalculateFine(violation);

        // Assert
        fine.Should().Be(0m);
    }

    [Fact]
    public void CalculateFine_FirstNotice_ShouldReturnFifty()
    {
        // Arrange
        var violation = _service.CreateViolation("PROP-001", ViolationType.Landscaping, "Test");
        violation.EscalationLevel = EscalationLevel.FirstNotice;

        // Act
        var fine = _service.CalculateFine(violation);

        // Assert
        fine.Should().Be(50m);
    }

    [Fact]
    public void ResolveViolation_ShouldSetResolvedDate()
    {
        // Arrange
        var violation = _service.CreateViolation("PROP-001", ViolationType.Landscaping, "Test");

        // Act
        var resolved = _service.ResolveViolation(violation.ViolationId);

        // Assert
        resolved.IsResolved.Should().BeTrue();
        resolved.ResolvedDate.Should().NotBeNull();
    }

    [Fact]
    public void ResolveViolation_ShouldLogAuditEntry()
    {
        // Arrange
        var violation = _service.CreateViolation("PROP-001", ViolationType.Landscaping, "Test");

        // Act
        _service.ResolveViolation(violation.ViolationId);

        // Assert
        _mockAuditLogger.Verify(
            x => x.LogAction("RESOLVE_VIOLATION", It.IsAny<string>(), null, null),
            Times.Once);
    }

    [Fact]
    public void ResolveViolation_ShouldThrow_WhenNotFound()
    {
        // Act & Assert
        var act = () => _service.ResolveViolation("non-existent");
        act.Should().Throw<InvalidOperationException>();
    }
}
