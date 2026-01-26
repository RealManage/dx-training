// Week 5 Example: Violation Audit API
// This project demonstrates custom commands and skills.
// Contains intentional bugs for learners to find using Claude Code!

using RealManage.ViolationAudit.Models;
using RealManage.ViolationAudit.Services;

Console.WriteLine("=== RealManage Violation Audit System ===");
Console.WriteLine("Week 5: Commands & Basic Skills\n");

// Create services
var auditLogger = new AuditLogger("demo-user");
var violationService = new ViolationService(auditLogger);

// Demo: Create some violations
Console.WriteLine("Creating sample violations...\n");

var v1 = violationService.CreateViolation("PROP-001", ViolationType.Landscaping, "Overgrown lawn exceeds 6 inches");
var v2 = violationService.CreateViolation("PROP-001", ViolationType.Parking, "Vehicle parked on lawn");
var v3 = violationService.CreateViolation("PROP-002", ViolationType.Noise, "Loud music after 10 PM");

Console.WriteLine($"Created violation: {v1.ViolationId} - {v1.Type}");
Console.WriteLine($"Created violation: {v2.ViolationId} - {v2.Type}");
Console.WriteLine($"Created violation: {v3.ViolationId} - {v3.Type}");

// Demo: List violations for a property
Console.WriteLine("\n--- Violations for PROP-001 ---");
foreach (var v in violationService.GetViolationsByProperty("PROP-001"))
{
    Console.WriteLine($"  [{v.EscalationLevel}] {v.Type}: {v.Description}");
}

// Demo: Show audit trail
Console.WriteLine("\n--- Audit Trail ---");
foreach (var entry in auditLogger.GetAuditEntries())
{
    Console.WriteLine($"  {entry.Timestamp:yyyy-MM-dd HH:mm:ss} | {entry.Action} | {entry.Details}");
}

// Note the bugs!
Console.WriteLine("\n=== Training Exercise ===");
Console.WriteLine("This code contains intentional bugs for you to find:");
Console.WriteLine("1. Audit logger has a timestamp bug");
Console.WriteLine("2. Compound interest calculation is wrong");
Console.WriteLine("3. GetViolation doesn't log audit entries (SOC 2 issue!)");
Console.WriteLine("4. Escalation boundaries are off by one day");
Console.WriteLine("5. GetEntriesForViolation doesn't filter properly");
Console.WriteLine("\nUse Claude Code with hooks to find and fix these bugs!");
Console.WriteLine("Check .claude/settings.json for hook configuration examples.");
