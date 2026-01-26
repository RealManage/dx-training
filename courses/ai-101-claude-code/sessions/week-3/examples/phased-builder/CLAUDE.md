# Phased Builder Project Context

## Tech Stack
- C# .NET 10
- ASP.NET Core Web API
- Entity Framework Core
- xUnit for testing

## Phase Approach
- Build each phase incrementally
- Don't over-engineer early phases
- Let implementation guide next phase design
- Keep phases focused and small

## Domain Model
- PaymentPlan: Represents agreement to pay balance over time
- PaymentPlanInstallment: Individual scheduled payments
- Relationships: One plan has many installments

## Business Rules
- Payment plans split balance into equal installments
- First installment may include remainder from division
- Overdue installments accrue 10% monthly interest
- Email notifications for new plans and overdue payments

## Testing Requirements
- TDD for all new code
- 80-90% coverage minimum
- Test business logic thoroughly
- Mock external dependencies (email service)

## Code Standards
- Domain models in /Models
- Business logic in /Services
- API endpoints in /Controllers
- Clear separation of concerns