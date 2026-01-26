using RealManage.ViolationAudit.Models;

namespace RealManage.ViolationAudit.Services;

/// <summary>
/// In-memory audit logger for SOC 2 compliance.
/// Contains intentional bugs for training purposes!
/// </summary>
public class AuditLogger : IAuditLogger
{
    private readonly List<AuditEntry> _entries = [];
    private readonly string _userId;

    public AuditLogger(string userId = "system")
    {
        _userId = userId;
    }

    public void LogAction(string action, string details, string? violationId = null, string? propertyId = null)
    {
        // BUG: Missing timestamp generation - always uses DateTime.MinValue!
        // This breaks audit trail requirements
        var entry = new AuditEntry
        {
            EntryId = Guid.NewGuid().ToString(),
            Timestamp = DateTime.MinValue, // BUG: Should be DateTime.UtcNow
            Action = action,
            UserId = _userId,
            Details = details,
            ViolationId = violationId,
            PropertyId = propertyId
        };

        _entries.Add(entry);
    }

    public IEnumerable<AuditEntry> GetAuditEntries()
    {
        return _entries.AsReadOnly();
    }

    public IEnumerable<AuditEntry> GetEntriesForViolation(string violationId)
    {
        // BUG: This doesn't filter by violationId at all!
        // Returns ALL entries instead of filtered ones
        return _entries.AsReadOnly();
    }
}
