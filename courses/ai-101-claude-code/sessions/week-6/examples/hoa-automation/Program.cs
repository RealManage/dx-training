// Week 6 Example: HOA Automation
// This project demonstrates agents and hooks patterns.
// Contains intentional bugs for learners to find!

using RealManage.HoaAutomation.Models;
using RealManage.HoaAutomation.Services;

Console.WriteLine("=== RealManage HOA Automation System ===");
Console.WriteLine("Week 6: Agents & Hooks\n");

// Create services
var violationService = new ViolationWorkflowService();
var boardService = new BoardMeetingService(violationService);

// Demo: Create a board meeting
Console.WriteLine("Creating board meeting...\n");
var meeting = boardService.CreateMeeting(
    new DateTime(2025, 2, 4, 19, 0, 0),
    "Community Clubhouse"
);
Console.WriteLine($"Meeting ID: {meeting.MeetingId}");
Console.WriteLine($"Date: {meeting.MeetingDate:f}");
Console.WriteLine($"Location: {meeting.Location}");

// Demo: Add board members
Console.WriteLine("\nAdding board members...");
boardService.AddAttendee(meeting.MeetingId, "President Smith");
boardService.AddAttendee(meeting.MeetingId, "VP Johnson");
var hasQuorum = boardService.AddAttendee(meeting.MeetingId, "Secretary Williams");
Console.WriteLine($"Quorum reached: {hasQuorum}");

// Demo: Generate agenda
Console.WriteLine("\n--- Generated Agenda ---");
var agenda = boardService.GenerateAgenda(meeting.MeetingId);
foreach (var item in agenda)
{
    var voteFlag = item.RequiresVote ? " [VOTE]" : "";
    Console.WriteLine($"  [{item.EstimatedMinutes} min] {item.Title}{voteFlag}");
}

// Demo: Run escalation workflow
Console.WriteLine("\n--- Escalation Workflow ---");
var workflowResult = violationService.RunEscalationWorkflow();
Console.WriteLine($"Status: {workflowResult.Status}");
Console.WriteLine($"Steps executed: {workflowResult.Steps.Count}");
Console.WriteLine("Outputs:");
foreach (var output in workflowResult.Outputs)
{
    Console.WriteLine($"  {output.Key}: {output.Value}");
}

// Note the bugs!
Console.WriteLine("\n=== Training Exercise ===");
Console.WriteLine("This code contains intentional bugs for you to find:");
Console.WriteLine("1. BoardMeetingService.GenerateAgenda - gets ALL violations, not just 90+ days");
Console.WriteLine("2. BoardMeetingService.GenerateAgenda - doesn't save agenda items to meeting");
Console.WriteLine("3. BoardMeetingService.AddAttendee - allows duplicate attendees");
Console.WriteLine("4. BoardMeetingService.StartMeeting - doesn't check quorum");
Console.WriteLine("5. ViolationWorkflowService.GetViolationsForBoardReview - wrong comparison (> vs >=)");
Console.WriteLine("6. ViolationWorkflowService.RunEscalationWorkflow - missing 60-90 day case");
Console.WriteLine("7. ViolationWorkflowService.GenerateNoticeLetter - missing required fields");
Console.WriteLine("\nUse skills and sub-agents to find and fix these bugs!");
Console.WriteLine("Check .claude/skills/ for example skills you can use.");
