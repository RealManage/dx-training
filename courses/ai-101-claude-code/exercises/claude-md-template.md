# RealManage Project Context Template

Copy this template to your project root as `CLAUDE.md` and customize for your specific project.

---

# [PROJECT NAME] - Claude Code Context

## Project Overview
Brief description of what this project does and its purpose within RealManage.

## Tech Stack
- **Backend:** C# .NET 9, ASP.NET Core Web API
- **Frontend:** Angular 17, TypeScript 5.x, RxJS
- **Database:** SQL Server 2019, Entity Framework Core 9
- **Cloud:** Azure App Service, Azure SQL Database, Azure Service Bus
- **Authentication:** Azure AD B2C
- **Testing:** xUnit, FluentAssertions, Moq (backend), Jasmine/Karma (frontend)

## Architecture Patterns
- Microservices: [List relevant services]
- Repository Pattern with Unit of Work
- CQRS for [specific operations]
- Event-driven with Azure Service Bus
- API Gateway via Azure API Management

## Coding Standards

### General Requirements
- **TDD Required:** Red-Green-Refactor cycle
- **Test Coverage:** 95% minimum (non-negotiable)
- **Code Reviews:** Required for all PRs
- **Documentation:** XML comments for public APIs

### C# Standards
- Nullable reference types enabled
- Async/await for all I/O operations
- Use records for DTOs
- Pattern matching where appropriate
- LINQ for collections (no raw loops)
- Dependency injection throughout

### Angular Standards
- Standalone components (no modules)
- OnPush change detection
- Signals for state management where appropriate
- RxJS for async operations
- Strict TypeScript settings
- WCAG 2.1 AA accessibility

### Database Standards
- All tables have audit columns (CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
- Use migrations for schema changes
- Indexes on all foreign keys
- Stored procedures for complex queries
- No direct SQL in code (use EF Core)

## Domain Terminology
- **HOA:** Homeowners Association
- **Violation:** Rule infraction requiring tracking and potential fines
- **Dues:** Monthly/quarterly/annual payments from residents
- **Board:** Elected officials making community decisions
- **CCR:** Covenants, Conditions & Restrictions
- **Architectural Review:** Approval process for property modifications
- **Assessment:** Special or regular fees charged to homeowners
- **Quorum:** Minimum members required for board decisions

## Business Rules
- Late fees: 10% compound interest applied monthly after 30-day grace period
- Violations escalate at 30, 60, and 90 days
- Maximum fine cap: $1,000 per violation
- Board meetings: First Tuesday of each month
- Fiscal year: July 1 - June 30
- Payment methods: ACH preferred, credit cards accepted with 2.9% fee
- Notice requirements: 15 days for violations, 30 days for assessments

## Current Sprint/Tasks
- Feature: [Current feature being developed]
- Sprint: [Sprint number and dates]
- Priority bugs: [List any critical bugs]
- Technical debt: [Items to address]

## API Endpoints
Base URL: `https://api.realmanage.com/v1`

Key endpoints:
- `/violations` - Violation management
- `/payments` - Payment processing
- `/residents` - Resident information
- `/properties` - Property management
- `/boards` - Board operations

## Database Schema Notes
Main tables:
- `Properties` - Physical units/lots
- `Residents` - Homeowner information
- `Violations` - Tracking infractions
- `Payments` - Financial transactions
- `BoardMembers` - Current board
- `Documents` - Stored documents

## Security & Compliance
- PCI DSS compliance for payment processing
- SOC 2 Type II certified
- GDPR compliance for EU residents
- Never log: SSN, credit card numbers, bank accounts
- Encrypt PII at rest and in transit
- Audit trail for all financial operations

## Performance Requirements
- API response time: < 200ms (p95)
- Angular initial load: < 3 seconds
- Database queries: < 100ms
- Support 10,000 concurrent users
- 99.9% uptime SLA

## Common Tasks & Solutions

### Calculate Late Fees
```csharp
// Use compound interest formula
// Principal * (1 + rate)^periods
```

### Generate Violation Notice
```csharp
// Use template engine
// Include violation details, fine amount, appeal process
```

### Process Bulk Payments
```csharp
// Use batch processing
// Transaction per batch, not per payment
```

## Known Issues/Gotchas
- [List any known quirks or issues]
- [Common mistakes to avoid]
- [Performance bottlenecks]

## Useful Commands
```bash
# Run tests with coverage
dotnet test --collect:"XPlat Code Coverage"

# Run Angular tests
ng test --code-coverage

# Generate EF migration
dotnet ef migrations add MigrationName

# Check for outdated packages
npm outdated
dotnet list package --outdated
```

## Team Conventions
- PR naming: `[JIRA-123] Brief description`
- Branch naming: `feature/JIRA-123-brief-description`
- Commit messages: Conventional commits format
- Code review SLA: 24 hours
- Merge strategy: Squash and merge

## External Integrations
- Payment Gateway: [Provider name]
- Email Service: SendGrid
- SMS: Twilio
- Document Storage: Azure Blob Storage
- Search: Azure Cognitive Search

## Monitoring & Logging
- Application Insights for monitoring
- Structured logging with Serilog
- Log levels: Error, Warning, Information
- Correlation IDs for request tracking
- Health checks at `/health`

## Development Environment
- IDE: Visual Studio 2022 / VS Code / Windsurf
- Required extensions: [List key extensions]
- Local database: SQL Server Developer Edition
- Node.js: 22 LTS (via nvm)
- Angular CLI: Latest stable

## Deployment
- CI/CD: Azure DevOps Pipelines
- Environments: Dev → QA → Staging → Production
- Deployment schedule: Tuesday/Thursday releases
- Rollback procedure: Blue-green deployment

## Contact & Resources
- Tech Lead: [Name]
- Product Owner: [Name]
- Slack Channel: #[project-channel]
- Wiki: [Link to documentation]
- JIRA Board: [Link to board]

---

*Last Updated: [Date]*
*Version: 1.0.0*