# User Story Templates for BDD Practice

## Overview

This document provides user story templates and examples that teams can use to practice converting user stories into BDD scenarios. These examples cover common software development scenarios and business domains.

## Template Structure

### Basic User Story Template

```text
As a [role/persona]
I want [functionality/capability]
So that [business value/benefit]
```

### Extended Template with Acceptance Criteria

```text
As a [role/persona]
I want [functionality/capability]
So that [business value/benefit]

Acceptance Criteria:

- [Specific condition and expected outcome]
- [Additional conditions and outcomes]
- [Edge cases and error conditions]
```

## E-Commerce Examples

### User Story 1: Product Search

```text
As a customer
I want to search for products by keyword
So that I can quickly find items I'm interested in buying

Acceptance Criteria:
- Search results should show relevant products
- Results should be sorted by relevance
- No results message should appear when no matches found
- Search should work with partial keywords
```

**Practice Exercise:** Convert this to 2-3 Gherkin scenarios covering the main success path and edge cases.

### User Story 2: Shopping Cart Management

```text
As a customer
I want to add items to my shopping cart
So that I can collect multiple items before purchasing

Acceptance Criteria:
- Items should be added with correct quantity
- Cart total should update automatically
- Customer should see confirmation when item is added
- Cart should persist between sessions
```

**Practice Exercise:** Write scenarios for adding items, updating quantities, and removing items.

### User Story 3: Order Processing

```text
As a customer
I want to place an order for items in my cart
So that I can purchase the products I've selected

Acceptance Criteria:
- Order should be created with correct items and total
- Customer should receive order confirmation
- Inventory should be updated
- Payment should be processed
- Customer should receive email confirmation
```

**Practice Exercise:** Create scenarios for successful orders and payment failures.

## Banking/Financial Examples

### User Story 4: Account Balance Inquiry

```text
As a bank customer
I want to check my account balance
So that I know how much money I have available

Acceptance Criteria:
- Balance should show current available amount
- Recent transactions should be visible
- Pending transactions should be clearly marked
- Balance should be real-time accurate
```

**Practice Exercise:** Write scenarios for different account types and transaction states.

### User Story 5: Money Transfer

```text
As a bank customer
I want to transfer money between my accounts
So that I can manage my finances effectively

Acceptance Criteria:
- Transfer should only work with sufficient funds
- Both accounts should be updated immediately
- Customer should receive confirmation
- Transfer should be logged for audit
- Daily transfer limits should be enforced
```

**Practice Exercise:** Create scenarios covering successful transfers, insufficient funds, and limit violations.

### User Story 6: Loan Application

```text
As a potential borrower
I want to apply for a personal loan
So that I can finance a major purchase

Acceptance Criteria:
- Application should collect required information
- Credit check should be performed
- Approval decision should be made automatically for simple cases
- Customer should be notified of decision
- Approved loans should be funded within 24 hours
```

**Practice Exercise:** Write scenarios for automatic approval, manual review, and rejection cases.

## Healthcare Examples

### User Story 7: Appointment Scheduling

```text
As a patient
I want to schedule a medical appointment
So that I can receive healthcare services

Acceptance Criteria:
- Available time slots should be shown
- Patient information should be captured
- Appointment confirmation should be sent
- Calendar should be updated
- Reminder notifications should be scheduled
```

**Practice Exercise:** Create scenarios for different appointment types and scheduling conflicts.

### User Story 8: Prescription Management

```text
As a patient
I want to request prescription refills online
So that I don't have to call or visit the pharmacy

Acceptance Criteria:
- Valid prescriptions should be available for refill
- Refill requests should be sent to pharmacy
- Patient should receive status updates
- Insurance coverage should be verified
- Pickup notifications should be sent when ready
```

**Practice Exercise:** Write scenarios covering refill eligibility, insurance issues, and pharmacy communication.

## Project Management Examples

### User Story 9: Task Assignment

```text
As a project manager
I want to assign tasks to team members
So that work can be distributed and tracked effectively

Acceptance Criteria:
- Tasks should be assigned to available team members
- Assignees should receive notifications
- Task status should be trackable
- Workload should be visible across team
- Dependencies should be respected
```

**Practice Exercise:** Create scenarios for assignment, reassignment, and workload management.

### User Story 10: Project Reporting

```text
As a stakeholder
I want to view project progress reports
So that I can understand current status and make informed decisions

Acceptance Criteria:
- Reports should show completed vs. remaining work
- Progress should be visually represented
- Key milestones should be highlighted
- Risk indicators should be visible
- Reports should be updated in real-time
```

**Practice Exercise:** Write scenarios for different report types and stakeholder roles.

## Content Management Examples

### User Story 11: Article Publishing

```text
As a content editor
I want to publish articles to the website
So that readers can access new content

Acceptance Criteria:
- Articles should go through approval workflow
- Published content should be immediately visible
- SEO metadata should be included
- Social media links should be generated
- Analytics tracking should be enabled
```

**Practice Exercise:** Create scenarios for the approval process, publishing, and content visibility.

### User Story 12: User Comments

```text
As a website visitor
I want to comment on articles
So that I can engage with the content and community

Acceptance Criteria:
- Comments should require moderation before appearing
- Users should be able to reply to comments
- Inappropriate content should be filtered
- Comment authors should receive reply notifications
- Comments should be threaded for readability
```

**Practice Exercise:** Write scenarios for comment submission, moderation, and notification.

## Complex Business Rule Examples

### User Story 13: Insurance Premium Calculation

```text
As an insurance agent
I want to calculate premium quotes for customers
So that I can provide accurate pricing information

Business Rules:
- Base premium varies by age group (18-25: high, 26-65: standard, 65+: reduced)
- Location affects premium (urban: +20%, suburban: standard, rural: -10%)
- Claims history affects rate (0 claims: -5%, 1-2 claims: standard, 3+ claims: +25%)
- Multi-policy discount: 10% for 2+ policies
- Good driver discount: 15% for 5+ years no accidents
```

**Practice Exercise:** Create scenarios covering different combinations of these business rules.

### User Story 14: Shipping Cost Calculation

```text
As a customer
I want to see shipping costs for my order
So that I can make informed purchasing decisions

Business Rules:
- Free shipping for orders over $75
- Standard shipping: $5 for items under 1kg, $2 per additional kg
- Express shipping: $15 for items under 1kg, $5 per additional kg
- International shipping: $25 base + $10 per kg
- Oversized items (>10kg) require freight shipping
- Some items cannot be shipped internationally
```

**Practice Exercise:** Write scenarios testing various weight and destination combinations.

## Practice Templates

### Template 1: Simple Success Path

```gherkin
Feature: [Feature name from user story]
  As a [role]
  I want [capability]
  So that [benefit]

Scenario: [Happy path description]
  Given [initial context]
  When [action taken]
  Then [expected outcome]
```

### Template 2: Multiple Scenarios

```gherkin
Feature: [Feature name]

Scenario: [Main success scenario]
  Given [context]
  When [action]
  Then [outcome]

Scenario: [Alternative scenario]
  Given [different context]
  When [same or different action]
  Then [different outcome]

Scenario: [Error scenario]
  Given [context]
  When [action that causes error]
  Then [error handling outcome]
```

### Template 3: Scenario Outline

```gherkin
Feature: [Feature name]

Scenario Outline: [Template description]
  Given [context with <parameter>]
  When [action with <parameter>]
  Then [outcome with <parameter>]

Examples:
  | parameter1 | parameter2 | expected_result |
  | value1     | value2     | result1         |
  | value3     | value4     | result2         |
```

## Practice Exercises

### Exercise 1: User Story Analysis

1. Choose a user story from the examples above
1. Identify the main business rules
1. List possible edge cases and error conditions
1. Convert to 3-5 Gherkin scenarios
1. Review scenarios for clarity and completeness

### Exercise 2: Three Amigos Session

1. Form groups of 3 people (Business Analyst, Developer, Tester roles)
1. Take a complex user story (like insurance premium calculation)
1. Use Example Mapping to explore the requirements
1. Collaboratively write scenarios
1. Compare results with other groups

### Exercise 3: Scenario Improvement

1. Take a poorly written scenario (examples in troubleshooting guide)
1. Identify what makes it poor quality
1. Rewrite it following BDD best practices
1. Compare before and after versions

### Exercise 4: Business Rule Testing

1. Choose a user story with complex business rules
1. Create a scenario outline that tests all rule combinations
1. Use examples table to cover edge cases
1. Verify all business rules are tested

### Exercise 5: End-to-End Workflow

1. Take multiple related user stories (e.g., e-commerce flow)
1. Write scenarios for each individual story
1. Create integration scenarios that span multiple stories
1. Organize scenarios into logical features

## Guidelines for Practice

### Do's ✅

- Start with simple user stories before attempting complex ones
- Focus on business value, not technical implementation
- Write scenarios from the user's perspective
- Use concrete examples rather than abstract descriptions
- Collaborate with others when possible
- Review and refine scenarios iteratively

### Don'ts ❌

- Don't try to cover every possible case in one scenario
- Don't include UI implementation details
- Don't write scenarios that are too vague or too detailed
- Don't skip the business context (Given statements)
- Don't ignore error conditions and edge cases
- Don't write scenarios in isolation without business input

## Self-Assessment Questions

After completing practice exercises, ask yourself:

1. **Clarity**: Can a business stakeholder understand what each scenario is testing?
1. **Coverage**: Do the scenarios cover the main business rules and edge cases?
1. **Maintainability**: Will these scenarios be easy to update when requirements change?
1. **Automation**: Could these scenarios be automated by a developer?
1. **Value**: Do these scenarios provide clear acceptance criteria for the feature?

## Next Steps

Once comfortable with these practice templates:

1. Apply BDD to real user stories from your current project
1. Facilitate Three Amigos sessions with your team
1. Start automating scenarios using tools like Reqnroll or Cucumber
1. Establish BDD practices in your development workflow
1. Continuously refine and improve your scenario writing skills

Remember: The goal is effective communication and collaboration, not perfect scenarios. Start simple and improve over time.
