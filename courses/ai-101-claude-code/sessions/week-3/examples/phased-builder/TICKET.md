# JIRA-4521: Implement Payment Plan Feature

## Overview
Residents with overdue HOA fees need the ability to set up payment plans to pay off their balance over time.

## Requirements

### Phase 1: Data Model & Basic CRUD
- Create PaymentPlan entity with fields:
  - ResidentId
  - OriginalAmount  
  - PlanType (3, 6, or 12 months)
  - SetupFee (5% of original)
  - MonthlyPayment
  - StartDate
  - Status (Active, Completed, Defaulted)
- Create PaymentPlanInstallment entity:
  - PaymentPlanId
  - InstallmentNumber
  - DueDate
  - Amount
  - Status (Pending, Paid, Overdue)
- Basic CRUD operations for plans

### Phase 2: Business Logic
- Calculate monthly payment amount (original + fee / months)
- Generate installment schedule on plan creation
- Auto-apply 5% setup fee
- Handle partial payments
- Mark installments as overdue after 30 days
- Cancel plan after 2 missed payments
- Validate resident eligibility (no active plans)

### Phase 3: API & Integration
- POST /api/payment-plans - Create new plan
- GET /api/payment-plans/{id} - Get plan details
- GET /api/residents/{id}/payment-plans - Get resident's plans
- POST /api/payment-plans/{id}/payments - Record payment
- Integration with existing payment system
- Email notifications for:
  - Plan created
  - Payment due (7 days before)
  - Payment overdue
  - Plan cancelled

## Acceptance Criteria
- [ ] Residents can create 3, 6, or 12 month plans
- [ ] 5% setup fee automatically added
- [ ] Installments generated with correct dates
- [ ] Overdue logic working (30 days)
- [ ] Plan cancellation after 2 missed payments
- [ ] Email notifications sent
- [ ] 80-90% test coverage
- [ ] API documented

## Technical Notes
- Use existing PaymentService for processing
- Follow RealManage coding standards
- TDD approach required
- Consider performance for bulk operations

## Definition of Done
- Code complete with tests
- Code review passed
- Documentation updated
- Deployed to staging
- QA sign-off