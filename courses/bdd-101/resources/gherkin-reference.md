# Gherkin Reference Guide

## Overview

Gherkin is a business-readable, domain-specific language that describes software behavior. It uses natural language with a simple structure that non-programmers can understand while still being precise enough for automation.

## Core Keywords

### Feature

Describes the high-level capability or business value.

```gherkin
Feature: User Login
  As a registered user
  I want to log into the system
  So that I can access my account
```

### Scenario

Describes a specific example of the feature behavior.

```gherkin
Scenario: Successful login with valid credentials
  Given I am on the login page
  When I enter valid username and password
  Then I should be redirected to the dashboard
```

### Given (Context)

Sets up the initial state or context.

```gherkin
Given I am a registered user
Given the user "john@example.com" exists
Given I am on the login page
```

### When (Action)

Describes the action or event that triggers the behavior.

```gherkin
When I click the "Login" button
When I enter "john@example.com" in the username field
When I submit the login form
```

### Then (Outcome)

Describes the expected outcome or result.

```gherkin
Then I should see "Welcome back!"
Then I should be redirected to the dashboard
Then an error message should be displayed
```

### And / But

Used to continue Given, When, or Then steps.

```gherkin
Given I am on the login page
  And I have entered valid credentials
When I click the "Login" button
  And I wait for the page to load
Then I should see the dashboard
  But I should not see any error messages
```

## Advanced Keywords

### Background

Defines steps that run before each scenario in a feature.

```gherkin
Feature: Account Management

Background:
  Given I am a registered user
  And I am logged into the system

Scenario: View account details
  When I navigate to my account page
  Then I should see my profile information

Scenario: Update account information
  When I update my email address
  Then I should see a confirmation message
```

### Scenario Outline

Creates a template for multiple similar scenarios.

```gherkin
Scenario Outline: Login attempts
  Given I am on the login page
  When I enter "<username>" and "<password>"
  Then I should see "<message>"

Examples:
  | username | password | message |
  | valid@email.com | correct123 | Welcome back! |
  | invalid@email.com | wrong123 | Invalid credentials |
  | valid@email.com | wrong123 | Invalid credentials |
```

### Examples

Provides test data for Scenario Outlines.

```gherkin
Examples:
  | username | password | expected_result |
  | john@example.com | secret123 | success |
  | jane@example.com | wrong | failure |
```

## Data Tables

Use data tables to pass structured data to steps.

```gherkin
Scenario: Create user account
  Given I am on the registration page
  When I fill in the user details:
    | Field | Value |
    | First Name | John |
    | Last Name | Doe |
    | Email | john.doe@example.com |
    | Password | secret123 |
  Then the account should be created successfully
```

## Doc Strings

Use doc strings for multi-line text data.

```gherkin
Scenario: Send email notification
  Given I have composed an email
  When I send the following message:
    """
    Dear Customer,
    
    Thank you for your recent purchase.
    Your order will be shipped within 2 business days.
    
    Best regards,
    Customer Service Team
    """
  Then the email should be delivered successfully
```

## Tags

Use tags to organize and filter scenarios.

```gherkin
@smoke @critical
Feature: User Authentication

@positive
Scenario: Successful login
  Given I have valid credentials
  When I log in
  Then I should be authenticated

@negative @security
Scenario: Failed login with invalid credentials
  Given I have invalid credentials
  When I attempt to log in
  Then I should see an error message
```

## Comments

Use comments for additional context (not executed).

```gherkin
# This feature covers the basic authentication functionality
Feature: User Login

# TODO: Add two-factor authentication scenarios
Scenario: Basic login
  Given I am on the login page
  # The username field should accept email format
  When I enter my credentials
  Then I should be logged in
```

## Best Practices

### Do's ✅

1. **Use business language**

```gherkin
# Good
Given the customer has an active account
When they place an order for $100
Then they should receive a 10% discount

# Avoid technical details
Given the user record exists in the database
When they POST to /api/orders with amount=100
Then the response should return discount=10
```

1. **Be specific and concrete**

```gherkin
# Good
Given I have $100 in my account
When I withdraw $30
Then my balance should be $70

# Too vague
Given I have money in my account
When I withdraw some money
Then my balance should be reduced
```

1. **Write from user perspective**

```gherkin
# Good
When I click the "Submit" button
Then I should see "Order confirmed"

# Implementation-focused
When the form submit event is triggered
Then the success message component should render
```

### Don'ts ❌

1. **Don't write UI-dependent scenarios**

```gherkin
# Fragile
When I click the blue button in the top-right corner
Then the modal dialog with ID "confirmation" should appear

# Better
When I confirm my order
Then I should see an order confirmation
```

1. **Don't be too detailed**

```gherkin
# Too detailed
Given I navigate to www.example.com
And I click on the "Login" link
And I wait 2 seconds for the page to load
And I verify the login form is displayed
When I enter "user@example.com" in the username field
And I enter "password123" in the password field
And I click the "Submit" button
And I wait 3 seconds
Then I should see the text "Welcome" displayed in Arial font

# Just right
Given I am on the login page
When I log in with valid credentials
Then I should see a welcome message
```

1. **Don't repeat context in every scenario**

```gherkin
# Repetitive
Scenario: View products
  Given I am a registered user
  And I am logged in
  When I visit the products page
  Then I should see available products

Scenario: Add to cart
  Given I am a registered user
  And I am logged in
  When I add a product to my cart
  Then it should appear in my cart

# Better - use Background
Background:
  Given I am a registered user
  And I am logged in

Scenario: View products
  When I visit the products page
  Then I should see available products

Scenario: Add to cart
  When I add a product to my cart
  Then it should appear in my cart
```

## Common Anti-Patterns to Avoid

### 1. Conjunctive Steps (Multiple Actions/Assertions)

```gherkin
# Bad
When I log in and navigate to settings and update my profile
Then I should see a success message and be redirected and receive an email

# Good
When I log in
And I navigate to settings
And I update my profile
Then I should see a success message
And I should be redirected to my profile
And I should receive a confirmation email
```

### 2. Implementation Details

```gherkin
# Bad
When I POST to /api/users with JSON payload {"name": "John"}
Then the response status should be 201

# Good
When I create a new user account for "John"
Then the account should be created successfully
```

### 3. Incidental Details

```gherkin
# Bad
Given it is Tuesday at 2:30 PM
And the temperature is 72 degrees
And I am wearing a blue shirt
When I log into the system
Then I should see my dashboard

# Good
Given I am a registered user
When I log into the system
Then I should see my dashboard
```

## Tools and Integration

### SpecFlow (.NET)

```csharp
[Given(@"I am on the login page")]
public void GivenIAmOnTheLoginPage()
{
    // Implementation
}

[When(@"I enter valid credentials")]
public void WhenIEnterValidCredentials()
{
    // Implementation
}

[Then(@"I should be redirected to the dashboard")]
public void ThenIShouldBeRedirectedToTheDashboard()
{
    // Implementation
}
```

### Cucumber (Java)

```java
@Given("I am on the login page")
public void i_am_on_the_login_page() {
    // Implementation
}

@When("I enter valid credentials")
public void i_enter_valid_credentials() {
    // Implementation
}

@Then("I should be redirected to the dashboard")
public void i_should_be_redirected_to_the_dashboard() {
    // Implementation
}
```

## Quick Reference

| Keyword          | Purpose                     | Example |
|---               |---                          |---|
| Feature          | High-level description      | Feature: User Login |
| Scenario         | Specific test case          | Scenario: Successful login |
| Given            | Initial context             | Given I am logged in |
| When             | Action/trigger              | When I click submit |
| Then             | Expected outcome            | Then I see confirmation |
| And/But          | Continue previous step type | And I receive an email |
| Background       | Common setup                | Background: Given I am a user |
| Scenario Outline | Template with examples      | Scenario Outline: Login with &lt;type&gt; |
| Examples         | Data for outlines           | Examples: \| type \| result \| |
| @                | Tags                        | @smoke @regression |

## Resources

- [Official Gherkin Documentation](https://cucumber.io/docs/gherkin/)

- [SpecFlow Documentation](https://docs.specflow.org/)

- [Cucumber Documentation](https://cucumber.io/docs/)

- [BDD Best Practices](https://cucumber.io/blog/bdd/understanding-bdd/)
