# BDD Patterns and Best Practices

## Overview

This guide outlines common patterns, practices, and anti-patterns in Behavior-Driven Development to help teams write effective, maintainable scenarios.

## Core BDD Patterns

### 1. The Single Responsibility Principle

Each scenario should test one specific behavior or business rule.

```gherkin
# Good - Single responsibility
Scenario: Customer receives discount for large orders
  Given I am a premium customer
  When I place an order worth $500
  Then I should receive a 10% discount

# Bad - Multiple responsibilities
Scenario: Order processing workflow
  Given I am a customer
  When I place an order worth $500
  And I apply a coupon code
  And I select express shipping
  Then I should receive a discount
  And I should be charged for shipping
  And I should receive an email confirmation
  And the order should be sent to fulfillment
```

### 2. Declarative vs. Imperative Style

Focus on what, not how.

```gherkin
# Good - Declarative (what)
Scenario: User updates profile information
  Given I am logged in
  When I update my email address to "new@example.com"
  Then my profile should show the new email address

# Bad - Imperative (how)
Scenario: User updates profile information
  Given I am on the dashboard
  When I click the "Profile" menu item
  And I click the "Edit" button
  And I clear the email field
  And I type "new@example.com"
  And I click "Save"
  And I wait for the page to reload
  Then the email field should contain "new@example.com"
```

### 3. Domain Language

Use the language of the business domain, not technical implementation.

```gherkin
# Good - Domain language
Scenario: Loan application approval
  Given I am a customer with excellent credit rating
  When I apply for a $10,000 personal loan
  Then my application should be automatically approved

# Bad - Technical language
Scenario: Loan application approval
  Given the credit_score field in user table is > 750
  When I POST to /api/loans with amount=10000
  Then the response should contain approved=true
```

### 4. The BRIEF Pattern

**B**usiness language  
**R**eal data  
**I**ntention revealing  
**E**ssential  
**F**ocused  

```gherkin
# Follows BRIEF pattern
Scenario: VIP customer priority support
  Given I am a VIP customer with an urgent issue
  When I submit a support ticket
  Then I should be assigned to the priority queue
  And I should receive a response within 1 hour
```

## Scenario Organization Patterns

### 1. Background for Common Setup

Use Background for steps that apply to all scenarios in a feature.

```gherkin
Feature: Online Shopping Cart

Background:
  Given I am a registered customer
  And I am logged into the website
  And I have items in my shopping cart

Scenario: Apply discount code
  When I enter the discount code "SAVE10"
  Then my total should be reduced by 10%

Scenario: Remove item from cart
  When I remove an item from my cart
  Then my total should be recalculated
```

### 2. Scenario Outlines for Data Variations

Use Scenario Outlines when the behavior is the same but data varies.

```gherkin
Scenario Outline: Password validation
  Given I am creating a new account
  When I enter "<password>" as my password
  Then I should see "<result>"

Examples:
  | password | result |
  | abc123 | Password too short |
  | abcdefgh | Password must contain numbers |
  | Abc123!@ | Password accepted |
  | PASSWORD123 | Password must contain lowercase |
```

### 3. Feature-Level Organization

Group related scenarios under logical features.

```gherkin
# One feature file per logical business capability
Feature: User Account Management
  # All scenarios related to user accounts

Feature: Product Search
  # All scenarios related to searching products

Feature: Order Processing
  # All scenarios related to order handling
```

## Data Handling Patterns

### 1. Meaningful Test Data

Use realistic, meaningful data that represents actual business scenarios.

```gherkin
# Good - Meaningful data
Scenario: International shipping cost calculation
  Given I am shipping to "Germany"
  When I calculate shipping for a 2kg package
  Then the shipping cost should be $25.50

# Bad - Meaningless data
Scenario: International shipping cost calculation
  Given I am shipping to "XX"
  When I calculate shipping for a 999kg package
  Then the shipping cost should be $999999.99
```

### 2. Data Tables for Complex Input

Use data tables when multiple related data points are needed.

```gherkin
Scenario: Create customer profile
  Given I am registering a new customer
  When I provide the following information:
    | Field | Value |
    | First Name | John |
    | Last Name | Smith |
    | Email | john.smith@example.com |
    | Phone | +1-555-123-4567 |
    | Country | United States |
  Then the customer profile should be created successfully
```

### 3. Parameterized Data

Use parameters to make scenarios reusable with different data sets.

```gherkin
Scenario: Calculate shipping cost by weight
  Given I have a package weighing <weight> kg
  When I calculate domestic shipping cost
  Then the cost should be $<expected_cost>

Examples:
  | weight | expected_cost |
  | 1 | 5.00 |
  | 5 | 15.00 |
  | 10 | 25.00 |
```

## Advanced Patterns

### 1. State Transition Testing

Test how the system behaves when moving between different states.

```gherkin
Feature: Order Status Management

Scenario: Order progresses through fulfillment states
  Given I have placed an order
  When the order is processed by the warehouse
  Then the order status should change to "In Transit"
  When the order is delivered
  Then the order status should change to "Delivered"
```

### 2. Error Condition Testing

Test how the system handles error conditions and edge cases.

```gherkin
Scenario Outline: Invalid payment handling
  Given I am checking out with items worth $100
  When I attempt to pay with "<payment_method>"
  Then I should see the error "<error_message>"
  And my order should not be processed

Examples:
  | payment_method | error_message |
  | expired credit card | Your card has expired |
  | insufficient funds | Insufficient funds available |
  | invalid card number | Please check your card number |
```

### 3. Time-Based Scenarios

Handle scenarios that depend on time or scheduling.

```gherkin
Scenario: Automatic subscription renewal
  Given I have a monthly subscription that expires tomorrow
  When the renewal date arrives
  Then my subscription should be automatically renewed
  And I should be charged the monthly fee
  And I should receive a renewal confirmation email
```

## Anti-Patterns to Avoid

### 1. The Conjunction Anti-Pattern

Don't use "and" to combine unrelated actions or assertions.

```gherkin
# Bad - Multiple unrelated actions
When I log in and update my profile and place an order and contact support

# Good - Separate, focused steps
When I log in
And I update my profile
And I place an order
And I contact support
```

### 2. The Everything Anti-Pattern

Don't try to test everything in one scenario.

```gherkin
# Bad - Testing entire application flow
Scenario: Complete e-commerce workflow
  Given I visit the homepage
  When I search for products
  And I view product details
  And I add items to cart
  And I update quantities
  And I apply coupons
  And I proceed to checkout
  And I enter shipping information
  And I select payment method
  And I confirm the order
  And I track the shipment
  Then everything should work perfectly

# Good - Multiple focused scenarios
Scenario: Add product to cart
Scenario: Apply discount coupon
Scenario: Complete checkout process
Scenario: Track order shipment
```

### 3. The Implementation Details Anti-Pattern

Don't expose implementation details in scenarios.

```gherkin
# Bad - Implementation details
Given the user table has a record with id=123
When I call the updateUser API with JSON payload
Then the database should be updated
And the cache should be invalidated
And the audit log should contain an entry

# Good - Business perspective
Given I am user "John Smith"
When I update my phone number
Then my profile should show the new phone number
```

### 4. The Chatty Scenario Anti-Pattern

Don't make scenarios overly verbose.

```gherkin
# Bad - Too chatty
Scenario: User login process with detailed steps
  Given I am a user who wants to access the system
  And I have valid credentials that were previously registered
  And the system is available and running
  When I navigate to the login page by clicking the login link
  And I carefully enter my username in the username field
  And I carefully enter my password in the password field
  And I double-check that my credentials are correct
  And I click the login button to submit my credentials
  Then the system should validate my credentials
  And the system should log me in successfully
  And I should be redirected to the main dashboard page
  And I should see my username displayed in the header

# Good - Concise and clear
Scenario: Successful user login
  Given I have valid credentials
  When I log in
  Then I should see my dashboard
```

## Collaboration Patterns

### 1. The Three Amigos

Include Business Analyst, Developer, and Tester perspectives.

#### Business Analyst Focus

- What value does this provide?
- What are the business rules?
- What are the acceptance criteria?

#### Developer Focus

- Is this technically feasible?
- What are the technical constraints?
- How does this fit with the architecture?

#### Tester Focus

- What could go wrong?
- What edge cases should we consider?
- How will we verify this works?

### 2. Example Mapping

Use example mapping sessions to explore scenarios before writing them.

1. **Start with a User Story**

2. **Identify Rules** (business rules that govern the story)

3. **Create Examples** (specific scenarios that illustrate the rules)

4. **Note Questions** (things that need clarification)

### 3. Progressive Refinement

Start with rough scenarios and refine them over time.

```gherkin
# Initial draft
Scenario: User can search for products
  When I search for something
  Then I should see results

# After discussion
Scenario: Search returns relevant products
  Given there are products available
  When I search for "laptop"
  Then I should see laptop products in the results
  And the results should be sorted by relevance

# Final version
Scenario: Product search shows relevant results
  Given the catalog contains laptops and phones
  When I search for "laptop"
  Then I should see only laptop products
  And they should be sorted by customer rating
```

## Maintenance Patterns

### 1. Living Documentation

Keep scenarios up-to-date with the current system behavior.

- Review scenarios during sprint planning
- Update scenarios when requirements change
- Remove obsolete scenarios
- Ensure scenarios reflect current business rules

### 2. Scenario Versioning

Handle scenario changes over time.

```gherkin
# Version 1.0 - Original requirement
Scenario: Standard shipping cost
  Given I have items worth $50
  When I select standard shipping
  Then I should be charged $5 for shipping

# Version 2.0 - Updated business rule
Scenario: Free shipping for orders over $75
  Given I have items worth $100
  When I select standard shipping
  Then I should not be charged for shipping

# Keep both scenarios if both rules are still valid for different conditions
```

### 3. Scenario Organization

Organize scenarios for maintainability.

```text
features/
├── user-management/
│   ├── registration.feature
│   ├── authentication.feature
│   └── profile-management.feature
├── product-catalog/
│   ├── product-search.feature
│   ├── product-details.feature
│   └── product-reviews.feature
└── order-processing/
    ├── shopping-cart.feature
    ├── checkout.feature
    └── order-tracking.feature
```

## Performance Patterns

### 1. Fast Feedback Scenarios

Create quick-running scenarios for immediate feedback.

```gherkin
@smoke
Scenario: Homepage loads successfully
  When I visit the homepage
  Then I should see the main navigation
  And the page should load within 2 seconds

@integration
Scenario: Complete order processing workflow
  Given I have items in my cart
  When I complete the checkout process
  Then my order should be submitted to fulfillment
  And I should receive order confirmation
```

### 2. Test Pyramid Alignment

Align BDD scenarios with the test pyramid principles.

- **Few scenarios** for end-to-end workflows
- **More scenarios** for business logic
- **Many unit tests** for technical details

### 3. Selective Execution

Use tags to run different scenario sets.

```gherkin
@critical @smoke
Scenario: User can log in

@regression @slow
Scenario: Complex reporting workflow

@wip
Scenario: New feature under development
```

## Integration Patterns

### 1. CI/CD Integration

Integrate BDD scenarios into the build pipeline.

- Run critical scenarios on every commit
- Run full suite before every deploy (with every commit if it doesn't take too long)
- Fail builds on scenario failures
- Generate living documentation

### 2. Tool Integration

Connect scenarios with development tools.

- Link scenarios to user stories in JIRA
- Generate test reports for stakeholders
- Track scenario execution metrics
- Integrate with test management tools

### 3. Documentation Generation

Use scenarios to generate living documentation.

- Automatically generate feature documentation
- Create business-readable reports
- Maintain traceability from requirements to tests
- Share results with stakeholders

## Resources and Further Reading

- [Specification by Example](https://gojko.net/books/specification-by-example/) by Gojko Adzic
- [The Cucumber Book](https://pragprog.com/titles/hwcuc2/) by Matt Wynne
- [BDD in Action](https://www.manning.com/books/bdd-in-action) by John Ferguson Smart
- [Example Mapping](https://cucumber.io/blog/bdd/example-mapping-introduction/) by Matt Wynne
- [Writing Better Gherkin](https://cucumber.io/blog/bdd/writing-better-gherkin/) by various authors
