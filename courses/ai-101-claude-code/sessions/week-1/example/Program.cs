// Modern C# Top-Level Program - No Main() needed! 🚀
using RealManage.HOA;

Console.WriteLine("🏘️  HOA Violation Tracker CLI");
Console.WriteLine("==============================\n");

var service = new ViolationService();

// Seed some sample data
service.AddViolation("Landscaping", "Overgrown lawn at 123 Main St", DateTime.Now.AddDays(-45));
service.AddViolation("Parking", "RV in driveway at 456 Oak Ave", DateTime.Now.AddDays(-15));
service.AddViolation("Noise", "Loud music at 789 Elm St", DateTime.Now.AddDays(-60));

// Interactive CLI menu
while (true)
{
    Console.WriteLine("\n📋 Main Menu:");
    Console.WriteLine("1. Calculate fine for violation");
    Console.WriteLine("2. View all violations");
    Console.WriteLine("3. Check overdue violations");
    Console.WriteLine("4. Add new violation");
    Console.WriteLine("5. Generate report");
    Console.WriteLine("6. Exit");
    Console.Write("\nYour choice (1-6): ");
    
    var choice = Console.ReadLine();
    Console.WriteLine();
    
    switch (choice)
    {
        case "1":
            CalculateFineMenu();
            break;
            
        case "2":
            ViewAllViolations();
            break;
            
        case "3":
            // TODO: Implement with Claude's help!
            Console.WriteLine("❌ Not implemented yet - ask Claude to help!");
            Console.WriteLine("Hint: Use 'Implement option 3 to show overdue violations'");
            break;
            
        case "4":
            // TODO: Add new violation
            Console.WriteLine("❌ Feature coming soon...");
            Console.WriteLine("Challenge: Ask Claude to implement this!");
            break;
            
        case "5":
            // TODO: Generate report
            Console.WriteLine("❌ Report generation not available");
            Console.WriteLine("Advanced: Ask Claude to create a PDF report!");
            break;
            
        case "6":
            Console.WriteLine("👋 Goodbye! Thanks for using HOA Tracker!");
            return;
            
        default:
            Console.WriteLine("⚠️ Invalid option. Please choose 1-6.");
            break;
    }
}

void CalculateFineMenu()
{
    Console.WriteLine("💰 Fine Calculator");
    Console.WriteLine("-----------------");
    Console.Write("Violation type (Landscaping/Parking/Noise): ");
    var type = Console.ReadLine() ?? "Landscaping";
    
    Console.Write("Days overdue: ");
    if (!int.TryParse(Console.ReadLine(), out var days))
    {
        Console.WriteLine("⚠️ Invalid number of days!");
        return;
    }
    
    var fine = service.CalculateFine(type, days);
    
    Console.WriteLine($"\n📊 Fine Calculation:");
    Console.WriteLine($"   Type: {type}");
    Console.WriteLine($"   Days Overdue: {days}");
    Console.WriteLine($"   Fine Amount: ${fine:F2}");
    
    // Show the bug hint
    if (days > 60)
    {
        Console.WriteLine("\n⚠️ Note: Does this look right for {0} months overdue?", days / 30);
    }
}

void ViewAllViolations()
{
    Console.WriteLine("📋 All Violations");
    Console.WriteLine("-----------------");
    
    var violations = service.GetAllViolations();
    
    if (!violations.Any())
    {
        Console.WriteLine("No violations recorded.");
        return;
    }
    
    foreach (var v in violations)
    {
        var age = (DateTime.Now - v.ReportedDate).Days;
        var status = age > 30 ? "⚠️ OVERDUE" : "✅ Current";
        
        Console.WriteLine($"\n{status} {v.Type} Violation");
        Console.WriteLine($"   Description: {v.Description}");
        Console.WriteLine($"   Reported: {v.ReportedDate:MM/dd/yyyy} ({age} days ago)");
        Console.WriteLine($"   Fine: ${v.FineAmount:F2}");
    }
    
    Console.WriteLine($"\nTotal Violations: {violations.Count()}");
}

// Helper function ideas for Claude to implement:
// - ColorConsole.WriteLine() for colored output
// - SaveToFile() for persistence  
// - GenerateReport() for PDF creation
// - SendNotifications() for email alerts