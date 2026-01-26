# Week 6 Example: HOA Automation

## Project Purpose
Training exercise for Week 6: Agents & Hooks.
This project demonstrates custom agents and hooks for automation and compliance.
The same project is also used in Week 7 (Plugins) to demonstrate packaging.

## Tech Stack
- C# .NET 10
- xUnit, FluentAssertions, Moq
- Top-level program pattern

## Intentional Bugs (Training Exercise!)
This code contains INTENTIONAL bugs for learners to find using skills and agents:

### BoardMeetingService.cs
1. **Line ~82** - GenerateAgenda gets ALL violations instead of only 90+ day BoardReview
2. **Line ~115** - GenerateAgenda doesn't save agenda items to the meeting object
3. **Line ~125** - AddAttendee allows duplicate attendees
4. **Line ~136** - StartMeeting doesn't verify quorum before starting

### ViolationWorkflowService.cs
5. **Line ~57** - GetViolationsForBoardReview uses `> 90` instead of `>= 90`
6. **Line ~70-85** - RunEscalationWorkflow missing 60-90 day SecondNotice case
7. **Line ~100** - GenerateNoticeLetter missing required fields (owner name, CCR ref, appeal instructions)

## Available Skills
- `/board-agenda <date>` - Generate board meeting agenda
- `/violation-workflow <property> <type> <days>` - Process violation

## HOA Domain Rules
- Quorum: Minimum 3 board members for valid meeting
- Violation escalation: 30/60/90 day progression
- Fine schedule: Warning (free), FirstNotice ($50), SecondNotice ($100 + 10% compound)
- Board review required for violations 90+ days old

## Testing
```bash
dotnet build
dotnet test
```

## Workflow Patterns Demonstrated
1. Multi-step automation (escalation workflow)
2. Agenda generation from violations
3. Notice letter generation
4. Audit trail tracking
