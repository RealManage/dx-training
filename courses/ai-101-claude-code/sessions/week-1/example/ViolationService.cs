namespace RealManage.HOA;

/// <summary>
/// Service for managing HOA violations and calculating fines.
/// Note: This implementation has several intentional issues for training purposes!
/// </summary>
public class ViolationService
{
    private readonly List<Violation> _violations = new();
    
    /// <summary>
    /// Calculates fine for a violation based on type and days overdue.
    /// TODO: Fix the calculation bug - fines aren't compounding correctly!
    /// </summary>
    public decimal CalculateFine(string violationType, int daysOverdue)
    {
        // Base fines by violation type
        var baseFine = violationType.ToLower() switch
        {
            "landscaping" => 50m,
            "parking" => 75m,
            "noise" => 100m,
            "architectural" => 150m,
            _ => 50m
        };
        
        // BUG: This calculation is wrong! 
        // Should compound 10% per month, not just once
        if (daysOverdue > 30)
        {
            return baseFine * 1.1m;  // This only adds 10% once, regardless of how overdue!
        }
        
        return baseFine;
    }
    
    /// <summary>
    /// Adds a new violation to the system.
    /// TODO: Add validation
    /// TODO: Generate unique IDs properly
    /// </summary>
    public void AddViolation(string type, string description, DateTime reportedDate)
    {
        var violation = new Violation(
            Id: Guid.NewGuid(),
            Type: type,
            Description: description,
            ReportedDate: reportedDate,
            FineAmount: CalculateFine(type, (DateTime.Now - reportedDate).Days)
        );
        
        _violations.Add(violation);
    }
    
    /// <summary>
    /// Gets all violations in the system.
    /// TODO: Add filtering and sorting options
    /// </summary>
    public IEnumerable<Violation> GetAllViolations()
    {
        return _violations;
    }
    
    // TODO: Implement these methods with Claude's help!
    
    /// <summary>
    /// Gets violations that are overdue (>30 days old).
    /// </summary>
    public IEnumerable<Violation> GetOverdueViolations()
    {
        // TODO: Implement this!
        throw new NotImplementedException("Ask Claude to help implement this!");
    }
    
    /// <summary>
    /// Calculates total fines owed across all violations.
    /// </summary>
    public decimal GetTotalFinesOwed()
    {
        // TODO: Sum up all fines
        throw new NotImplementedException();
    }
    
    /// <summary>
    /// Marks a violation as resolved.
    /// </summary>
    public void ResolveViolation(Guid violationId)
    {
        // TODO: Find and update violation status
        throw new NotImplementedException();
    }
    
    // TODO: Make these methods async!
    // TODO: Add persistence to JSON file
    // TODO: Add email notifications
    // TODO: Add data validation
    // TODO: Implement repository pattern
}

/// <summary>
/// Represents an HOA violation.
/// Using C# record for immutable data structure.
/// </summary>
public record Violation(
    Guid Id,
    string Type,
    string Description,
    DateTime ReportedDate,
    decimal FineAmount
)
{
    // TODO: Add Status property (Open, InProgress, Resolved)
    // TODO: Add ResolutionDate property
    // TODO: Add PropertyAddress property
    // TODO: Add validation in init accessor
}

// TODO: Create these additional classes with Claude:
// - public class ViolationRepository : IViolationRepository
// - public class NotificationService : INotificationService  
// - public class ReportGenerator : IReportGenerator
// - public enum ViolationStatus { Open, InProgress, Resolved }
// - public record Address(string Street, string City, string State, string Zip)