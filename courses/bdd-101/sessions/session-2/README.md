# Session 2: Writing Effective Scenarios

**Duration:** 2 hours  
**Format:** Interactive workshop with hands-on exercises  
**Prerequisites:** Completed Session 1 or equivalent BDD/Gherkin knowledge

## 🎯 Learning Objectives

By the end of this session, participants will be able to:

- ✅ Identify and avoid common BDD anti-patterns
- ✅ Write maintainable, focused scenarios using advanced Gherkin features
- ✅ Use Background, Scenario Outlines, and Data Tables effectively
- ✅ Organize scenarios as living documentation
- ✅ Refactor poor-quality scenarios into excellent ones
- ✅ Apply scenario quality principles to real-world requirements

## 📚 Session Agenda

### 1. Welcome & Homework Review (15 minutes)

#### Check-in Questions

- How did the homework assignment go?
- What challenges did you face converting user stories to scenarios?
- What questions came up when sharing scenarios with teammates?

#### Quick Homework Sharing

- Volunteers share one scenario from homework
- Group provides constructive feedback
- Identify common patterns and issues

---

### 2. Scenario Quality Principles (30 minutes)

#### 2.1 The BRIEF Pattern (10 minutes)

High-quality BDD scenarios follow the **BRIEF** pattern:

**B**usiness language - Uses terms stakeholders understand  
**R**eal data - Concrete examples, not abstract placeholders  
**I**ntention revealing - Clear about what's being tested  
**E**ssential - Focuses on what matters, omits implementation details  
**F**ocused - Tests one behavior per scenario  

#### 2.2 Good vs. Bad Examples (20 minutes)

### Example 1: Business Language

❌ **Bad - Technical jargon:**

```gherkin
Scenario: API endpoint returns user data
  Given the user service is running
  When I GET /api/users/123
  Then the response status should be 200
  And the JSON should contain user attributes
```

✅ **Good - Business language:**

```gherkin
Scenario: Customer views their profile information
  Given I am logged in as "John Smith"
  When I view my profile page
  Then I should see my personal information
  And I should see my account preferences
```

### Example 2: Real Data

❌ **Bad - Abstract data:**

```gherkin
Scenario: Calculate discount
  Given I have some items
  When I apply a discount
  Then I should save some money
```

✅ **Good - Concrete examples:**

```gherkin
Scenario: VIP customer receives 15% discount on large orders
  Given I am a VIP customer
  And I have $200 worth of items in my cart
  When I proceed to checkout
  Then I should see a $30 discount applied
  And my total should be $170
```

### Example 3: Focused Behavior

❌ **Bad - Multiple behaviors:**

```gherkin
Scenario: Complete user journey
  Given I create an account
  When I log in and update my profile and place an order and track shipment
  Then everything should work correctly
```

✅ **Good - Single focused behavior:**

```gherkin
Scenario: Track order after shipment
  Given I have placed an order that has shipped
  When I check my order status
  Then I should see the tracking number
  And I should see the estimated delivery date
```

---

### 3. Advanced Gherkin Features (45 minutes)

#### 3.1 Background - Common Setup (15 minutes)

Use **Background** when multiple scenarios share the same setup steps.

##### Without Background (repetitive)

```gherkin
Scenario: VIP customer gets priority support
  Given I am logged into the customer portal
  And I am a VIP customer
  And I have an active support plan
  When I submit a support ticket
  Then I should be assigned to the priority queue

Scenario: VIP customer gets extended warranty
  Given I am logged into the customer portal
  And I am a VIP customer
  And I have an active support plan
  When I purchase a product
  Then I should automatically receive extended warranty
```

##### With Background (clean and maintainable)

```gherkin
Feature: VIP Customer Benefits

Background:
  Given I am logged into the customer portal
  And I am a VIP customer
  And I have an active support plan

Scenario: VIP customer gets priority support
  When I submit a support ticket
  Then I should be assigned to the priority queue

Scenario: VIP customer gets extended warranty
  When I purchase a product
  Then I should automatically receive extended warranty
```

#### Best Practices for Background

- Keep Background short (3-5 steps maximum)
- Only include steps that apply to ALL scenarios in the feature
- Don't put When or Then steps in Background
- Use Given steps only

#### 3.2 Scenario Outlines - Data-Driven Testing (20 minutes)

Use **Scenario Outline** when testing the same behavior with different data.

### Example: Password Validation

```gherkin
Scenario Outline: Password validation rules
  Given I am creating a new account
  When I enter "<password>" as my password
  Then I should see "<result>"

Examples:
  | password | result |
  | MySecure123! | Password accepted |
  | short | Password must be at least 8 characters |
  | lowercase123! | Password must contain uppercase letter |
  | UPPERCASE123! | Password must contain lowercase letter |
  | NoNumbers! | Password must contain at least one number |
  | NoSpecials123 | Password must contain special character |
```

#### When to Use Scenario Outlines

- ✅ Testing business rules with multiple data combinations
- ✅ Boundary value testing (age limits, price thresholds)
- ✅ Input validation scenarios
- ❌ Don't use for unrelated scenarios that happen to share structure

### Example: Shipping Cost Calculation

```gherkin
Scenario Outline: Shipping cost by weight and destination
  Given I have a package weighing <weight> kg
  When I calculate shipping to <destination>
  Then the shipping cost should be $<cost>

Examples:
  | weight | destination | cost |
  | 1 | domestic | 5.00 |
  | 1 | international | 15.00 |
  | 5 | domestic | 12.00 |
  | 5 | international | 35.00 |
  | 10 | domestic | 20.00 |
  | 10 | international | 60.00 |
```

#### 3.3 Data Tables - Complex Input Data (10 minutes)

Use **Data Tables** when you need to pass structured data to a step.

### Example: User Registration

```gherkin
Scenario: Register new customer account
  Given I am on the registration page
  When I fill out the registration form:
    | Field | Value |
    | First Name | John |
    | Last Name | Smith |
    | Email | john.smith@example.com |
    | Phone | +1-555-123-4567 |
    | Country | United States |
    | Age | 28 |
  Then my account should be created successfully
  And I should receive a welcome email
```

### Example: Bulk Order Processing

```gherkin
Scenario: Process multiple items in order
  Given I am placing a bulk order
  When I add the following items to my cart:
    | Product | Quantity | Unit Price |
    | Laptop | 5 | $999.00 |
    | Mouse | 5 | $25.00 |
    | Keyboard | 5 | $75.00 |
  Then my order total should be $5,495.00
  And I should qualify for bulk discount pricing
```

---

### 4. Common Anti-Patterns and How to Fix Them (25 minutes)

#### 4.1 The Conjunction Anti-Pattern

**Problem:** Using "and" to combine unrelated actions or assertions.

❌ **Bad:**

```gherkin
When I log in and place an order and update my profile and contact support
Then I should see my dashboard and my order confirmation and my updated profile and support ticket number
```

✅ **Good:**

```gherkin
When I log in
And I place an order
And I update my profile  
And I contact support
Then I should see my dashboard
And I should see my order confirmation
And I should see my updated profile
And I should see my support ticket number
```

#### 4.2 The Imperative Anti-Pattern

**Problem:** Describing how to do something instead of what to do.

❌ **Bad - Imperative (how):**

```gherkin
Scenario: User updates email address
  Given I am on the dashboard page
  When I click the "Profile" menu item
  And I click the "Edit Profile" button
  And I clear the email field
  And I type "new@example.com"
  And I click the "Save" button
  And I wait for the confirmation message
  Then I should see "Profile updated successfully"
```

✅ **Good - Declarative (what):**

```gherkin
Scenario: User updates email address  
  Given I am logged into my account
  When I change my email address to "new@example.com"
  Then my profile should show the new email address
  And I should receive a confirmation message
```

#### 4.3 The Incidental Details Anti-Pattern

**Problem:** Including details that aren't relevant to the behavior being tested.

❌ **Bad - Irrelevant details:**

```gherkin
Scenario: Process order on Tuesday afternoon
  Given it is Tuesday at 2:30 PM
  And the weather is sunny
  And I am using Chrome browser version 96.0.4664.110
  And I am wearing a blue shirt
  When I place an order for a red bicycle
  Then my order should be processed
```

✅ **Good - Essential details only:**

```gherkin
Scenario: Process order during business hours
  Given I am a registered customer
  When I place an order for a bicycle
  Then my order should be processed immediately
  And I should receive an order confirmation
```

#### 4.4 The Everything and the Kitchen Sink Anti-Pattern

**Problem:** Trying to test too much in a single scenario.

❌ **Bad - Testing everything:**

```gherkin
Scenario: Complete e-commerce workflow with all features
  Given I visit the homepage
  When I search for products
  And I view product details
  And I read customer reviews
  And I compare prices
  And I add items to cart
  And I update quantities
  And I apply coupon codes
  And I calculate shipping
  And I proceed to checkout
  And I enter billing information
  And I enter shipping information
  And I select payment method
  And I review order details
  And I place the order
  And I receive confirmation
  And I track the order
  And I contact customer service
  And I write a product review
  Then everything should work perfectly
```

✅ **Good - Multiple focused scenarios:**

```gherkin
Scenario: Search for products by keyword
Scenario: View detailed product information  
Scenario: Add product to shopping cart
Scenario: Apply discount coupon at checkout
Scenario: Complete order with credit card payment
Scenario: Track order status after purchase
```

---

### 5. Hands-on Refactoring Workshop (20 minutes)

#### 5.1 Exercise: Fix the Bad Scenarios (15 minutes)

**Your task:** Refactor these poor-quality scenarios following BDD best practices.

##### Bad Scenario 1

```gherkin
Scenario: User interaction with system components  
  Given the system is operational
  When user performs actions
  Then stuff happens and things work
```

#### Bad Scenario 2

```gherkin
Scenario: Login and profile and order management workflow
  Given I navigate to www.example.com/login
  And I wait 3 seconds for the page to load
  And I see the login form with username and password fields
  When I type "user@example.com" in the username input field with id="username"
  And I type "password123" in the password input field with id="password"  
  And I click the submit button with class="btn-primary"
  And I wait 2 seconds for authentication
  And I click on "My Profile" in the navigation menu
  And I click "Edit Profile" button
  And I update my phone number to "+1-555-987-6543"
  And I click "Save Profile" button
  And I navigate to the orders page by clicking "My Orders"
  And I click "Place New Order" button
  And I select "Express Shipping" option
  And I enter my credit card details
  And I click "Submit Order" button
  Then I should see login success message
  And I should see profile update confirmation
  And I should see order confirmation number
  And my profile should show updated phone number
  And my order should be created in the database
  And my credit card should be charged
  And shipping notification should be sent
```

#### Bad Scenario 3

```gherkin
Scenario: API testing scenario
  Given I send GET request to endpoint
  When I receive response
  Then response should be good
```

#### 5.2 Group Review and Discussion (5 minutes)

- Share your refactored scenarios
- Discuss different approaches to improvement
- Identify the most effective changes

#### Expected Improvements

##### Scenario 1 Improved

```gherkin
Scenario: Customer places online order successfully
  Given I am a registered customer
  When I place an order for a laptop
  Then my order should be confirmed
  And I should receive an order number
```

##### Scenario 2 Improved (broken into multiple scenarios)

```gherkin
Scenario: Customer logs in successfully
  Given I am a registered customer  
  When I log in with valid credentials
  Then I should see my account dashboard

Scenario: Customer updates profile information
  Given I am logged into my account
  When I update my phone number to "+1-555-987-6543"
  Then my profile should show the new phone number
  And I should see a confirmation message

Scenario: Customer places express order
  Given I am logged into my account
  When I place an order with express shipping
  Then my order should be confirmed
  And I should be charged for express shipping
```

##### Scenario 3 Improved

```gherkin
Scenario: Retrieve customer account information
  Given I am a registered customer with account number "12345"
  When I request my account details
  Then I should see my account balance
  And I should see my recent transaction history
```

---

### 6. Organizing Scenarios as Living Documentation (15 minutes)

#### 6.1 Feature Organization Patterns

##### By User Journey

```gherkin
Feature: User Registration and Onboarding
Feature: Product Search and Discovery  
Feature: Shopping Cart Management
Feature: Checkout and Payment Processing
Feature: Order Fulfillment and Tracking
```

##### By Business Capability

```gherkin
Feature: Account Management
Feature: Inventory Management
Feature: Payment Processing
Feature: Customer Support
Feature: Reporting and Analytics
```

##### By User Role

```gherkin
Feature: Customer Self-Service Portal
Feature: Administrator Management Console
Feature: Support Agent Tools
Feature: Manager Reporting Dashboard
```

#### 6.2 Using Tags for Organization

```gherkin
@smoke @critical
Scenario: User can log in

@regression @integration  
Scenario: Complete order processing workflow

@wip @experimental
Scenario: New recommendation algorithm

@bug @high-priority
Scenario: Fix duplicate order issue
```

##### Common Tag Categories

- **Execution:** @smoke, @regression, @integration
- **Priority:** @critical, @high, @medium, @low
- **Status:** @wip, @ready, @blocked
- **Type:** @bug, @feature, @improvement
- **Team:** @frontend, @backend, @mobile

#### 6.3 Living Documentation Best Practices

1. **Keep scenarios current** - Update when requirements change
1. **Remove obsolete scenarios** - Don't let dead scenarios accumulate
1. **Use business-friendly language** - Stakeholders should understand them
1. **Link to user stories** - Maintain traceability
1. **Regular reviews** - Schedule scenario maintenance sessions

---

### 7. Real-World Application Exercise (20 minutes)

#### 7.1 Team Exercise: Improve Your Project Scenarios (15 minutes)

**Instructions:**

1. Form small groups (3-4 people)
1. Each person brings a user story from their current project
1. Together, write high-quality scenarios for one user story
1. Apply all the techniques learned today
1. Prepare to share with the larger group

**Focus Areas:**

- Use BRIEF pattern principles
- Consider if Background or Scenario Outline would help
- Avoid the anti-patterns discussed
- Write scenarios that could serve as living documentation

#### 7.2 Group Presentations (5 minutes)

- Each group shares one scenario they created
- Group explains what techniques they applied
- Class provides feedback and suggestions

---

### 8. Wrap-up and Session 3 Preview (5 minutes)

#### 8.1 Key Takeaways

- Quality scenarios follow the BRIEF pattern
- Advanced Gherkin features (Background, Scenario Outline) improve maintainability
- Common anti-patterns can make scenarios brittle and hard to understand
- Scenarios should serve as living documentation for the team
- Regular refactoring improves scenario quality over time

#### 8.2 Homework Assignment

**Before Session 3:**

1. **Refactor one poorly written scenario** from your project using today's techniques
1. **Identify automation candidates** - Which scenarios would be good for automated testing?
1. **Research BDD tools** - Look into Reqnroll (.NET) or Cucumber (Java/JavaScript/Ruby) depending on your stack
1. **Prepare questions** about automation setup for Session 3

#### 8.3 Preview of Session 3: From Scenarios to Automation

- How scenarios become automated tests
- Writing step definitions and test code
- Setting up a BDD automation framework
- Integration with CI/CD pipelines
- Maintaining automated BDD tests

## 📚 Resources for Further Learning

### Quality Guidelines Reference

- [BDD Patterns Guide](../../resources/bdd-patterns.md)

- [Gherkin Reference](../../resources/gherkin-reference.md)

- [Troubleshooting Common Issues](../../resources/troubleshooting.md)

### Examples to Study

- [Good vs Bad Scenarios](./examples/good-vs-bad.feature)

- [Refactoring Exercises](./examples/refactoring-exercise.md)

### Tools for Scenario Writing

- **VS Code Extensions:** Cucumber (Gherkin) Full Support
- **IntelliJ Plugins:** Gherkin, Cucumber for Java
- **Online Editors:** Cucumber Studio, Relish

## 🤝 Getting Help

- **Questions about scenario quality:** Review the BRIEF pattern checklist
- **Struggling with refactoring:** Practice with the provided examples
- **Need feedback:** Share scenarios with teammates or the instructor
- **Tool questions:** Check the troubleshooting guide or ask in Session 3

---

**Next Session:** [Session 3: From Scenarios to Automation](../session-3/README.md)
