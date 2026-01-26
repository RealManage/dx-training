# Week 5 Example: Violation Audit API

## Project Purpose
Training exercise for Week 5: Commands & Basic Skills.
This project demonstrates custom commands and skills for automation.

## Tech Stack
- C# .NET 10
- xUnit, FluentAssertions, Moq
- Top-level program pattern

## Intentional Bugs (Training Exercise!)
This code contains INTENTIONAL bugs for learners to find using Claude Code:

1. **AuditLogger.cs:25** - Timestamp bug: Uses DateTime.MinValue instead of DateTime.UtcNow
2. **ViolationService.cs:75** - Wrong compound interest: Uses simple interest instead of Math.Pow
3. **ViolationService.cs:38** - Missing audit: GetViolation doesn't log (SOC 2 violation!)
4. **ViolationService.cs:52** - Off-by-one: Escalation uses >= instead of > for boundaries
5. **AuditLogger.cs:42** - No filter: GetEntriesForViolation returns ALL entries

## HOA Domain Rules
- Violation escalation: 30/60/90 day progression
- Late fees: 10% monthly COMPOUND interest (not simple!)
- Grace period: 30 days
- Fine caps: 3x original per state law

## Testing
```bash
dotnet build
dotnet test
```

## Custom Commands
- `/violation-report <property_id>` - Generate violation report
- `/late-fee <principal> <months>` - Calculate compound interest

## Skills
- `/late-fee-report <property_id>` - Generate late fee report with supporting fee schedule file

## Custom Subagents
- `violation-analyst` - Analyze violation patterns (read-only)

## Hooks Configuration
See `.claude/settings.json` for:
- Audit logging hooks
- rm command blocking
- Auto-build after edits
