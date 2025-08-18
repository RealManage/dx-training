# Scenario Refactoring Exercise

## Overview

This exercise contains poorly written BDD scenarios that need improvement. Practice identifying issues and refactoring them into high-quality scenarios following BDD best practices.

## Instructions

1. **Read each bad scenario carefully**
1. **Identify specific problems** (anti-patterns, unclear language, etc.)
1. **Rewrite the scenario** following BDD best practices
1. **Compare your solution** with the provided improved versions

## Exercise 1: The Technical API Scenario

### Bad Scenario

```gherkin
Scenario: Test user creation endpoint functionality
  Given the user management microservice is running on localhost:8080
  And the MySQL database server is connected with schema version 2.1.3
  And the JWT authentication service is operational
  When I send an HTTP POST request to /api/v1/users endpoint
  And the request Content-Type header is set to "application/json"
  And the request body contains the following JSON payload:
    """
    {
      "username": "testuser123",
      "email": "test@example.com", 
      "password": "securepassword456",
      "firstName": "Test",
      "lastName": "User"
    }
    """
  Then the HTTP response status code should be 201
  And the response Content-Type header should be "application/json"
  And the response body should contain a "userId" field with integer data type
  And the response body should contain a "createdAt" timestamp
  And the user record should be inserted into the users table
  And the password should be hashed using bcrypt algorithm
```

### Problems Identified

- [ ] Too technical (API endpoints, HTTP status codes, database details)
- [ ] Implementation details exposed (bcrypt, MySQL schema)
- [ ] Written from developer perspective, not user perspective
- [ ] Infrastructure details that don't matter to business
- [ ] Testing implementation rather than behavior

### Your Refactored Version

```gherkin
Feature: 

Scenario: 
  Given 
  When 
  Then 
```

---

## Exercise 2: The Everything Scenario

### Bad Scenario

```gherkin
Scenario: Complete e-commerce user journey with all features enabled
  Given I am a new visitor to the website
  And the website has products available in multiple categories
  And the payment gateway is configured for credit cards and PayPal
  And the shipping service is integrated with FedEx and UPS
  And the email service is working for confirmations
  When I browse products and read reviews and compare prices
  And I create a new account with email verification
  And I add multiple items to my cart from different categories
  And I apply a discount coupon and calculate taxes
  And I proceed to checkout and enter billing address
  And I enter shipping address different from billing
  And I select express shipping option
  And I enter credit card payment information
  And I review my order details and terms of service
  And I place the order and receive confirmation number
  And I check my email for order confirmation
  And I track my order status through shipping
  And I receive the package and confirm delivery
  And I write a product review and rate the seller
  And I contact customer service with a question
  And I return one item and get a refund
  Then everything should work perfectly throughout the entire process
  And all systems should integrate seamlessly
  And the customer should have an excellent experience
  And all data should be properly stored and secured
```

### Problems Identified

- [ ] Testing multiple unrelated behaviors
- [ ] Too complex and difficult to understand
- [ ] Vague outcome ("everything should work perfectly")
- [ ] Would be impossible to debug when it fails
- [ ] Mixes different user journeys into one scenario

### Your Refactored Version (Break into multiple focused scenarios)

```gherkin
Feature: 

Scenario: 
  Given 
  When 
  Then 

Scenario: 
  Given 
  When 
  Then 

Scenario: 
  Given 
  When 
  Then 
```

---

## Exercise 3: The Vague and Abstract Scenario

### Bad Scenario

```gherkin
Scenario: User interaction with system
  Given the system is available
  And some data exists
  When the user does something
  And some processing happens
  Then the user should see results
  And the system should behave correctly
  And everything should work as expected
```

### Problems Identified

- [ ] Completely vague and abstract
- [ ] No concrete examples or specific behaviors
- [ ] Impossible to understand what's being tested
- [ ] Cannot be automated or verified
- [ ] Provides no value as documentation

### Your Refactored Version

```gherkin
Feature: 

Scenario: 
  Given 
  When 
  Then 
```

---

## Exercise 4: The UI-Heavy Imperative Scenario

### Bad Scenario

```gherkin
Scenario: User logs into the application using the web interface
  Given I open Chrome browser version 91.0.4472.124
  And I navigate to https://www.example.com/login
  And I wait 3 seconds for the page to completely load
  And I verify that the login form is visible on the screen
  And I can see the username input field with placeholder "Enter your email"
  And I can see the password input field with placeholder "Enter your password"
  And I can see the blue "Sign In" button in the bottom right corner
  When I click on the username input field with my mouse cursor
  And I type "john.doe@example.com" character by character
  And I press the Tab key to move focus to the password field
  And I type "mySecretPassword123!" character by character
  And I move my mouse to the blue "Sign In" button
  And I click the "Sign In" button with a single left-click
  And I wait 2 seconds for the authentication process to complete
  And I wait for the page to redirect to the dashboard URL
  Then I should see the dashboard page with navigation menu
  And I should see my username "John Doe" displayed in the top-right corner
  And I should see the logout button next to my username
  And the browser URL should be "https://www.example.com/dashboard"
  And the page title should be "Dashboard - Example App"
```

### Problems Identified

- [ ] Too much UI detail (browser, specific clicks, waiting times)
- [ ] Imperative style (how to do it vs what to do)
- [ ] Brittle (breaks when UI changes)
- [ ] Too long and hard to read
- [ ] Focuses on implementation rather than business outcome

### Your Refactored Version

```gherkin
Feature: 

Scenario: 
  Given 
  When 
  Then 
```

---

## Exercise 5: The Conjunction Anti-Pattern Scenario

### Bad Scenario

```gherkin
Scenario: Multi-step user workflow with combined actions
  Given I am a registered user and I have products in my wishlist and my account has a valid payment method
  When I log in and navigate to my wishlist and add items to cart and apply a discount code and proceed to checkout and enter shipping information and confirm my order
  Then I should see order confirmation and receive an email and my payment should be processed and my wishlist should be updated and my order should appear in order history and shipping should be scheduled
```

### Problems Identified

- [ ] Multiple unrelated actions combined with "and"
- [ ] Multiple unrelated assertions combined with "and"
- [ ] Difficult to understand what's being tested
- [ ] Hard to debug when something fails
- [ ] Violates single responsibility principle

### Your Refactored Version

```gherkin
Feature: 

Scenario: 
  Given 
  When 
  Then 

# Alternative: Break into multiple scenarios if testing different behaviors
```

---

## Sample Solutions

### Exercise 1 Solution: API Scenario Improved

**Original Focus:** Technical API testing  
**Improved Focus:** Business behavior

```gherkin
Feature: Customer Account Creation
  As a new customer
  I want to create an account
  So that I can make purchases and track orders

Scenario: Create account with valid information
  Given I am on the registration page
  When I provide valid account details:
    | Field | Value |
    | Email | john.doe@example.com |
    | Password | SecurePass123! |
    | First Name | John |
    | Last Name | Doe |
  Then my account should be created successfully
  And I should receive a welcome email
  And I should be automatically logged in
```

### Exercise 2 Solution: Everything Scenario Broken Down

**Original:** One massive scenario testing everything  
**Improved:** Multiple focused scenarios

```gherkin
Feature: Online Shopping Experience

Scenario: Customer browses and adds product to cart
  Given I am viewing the product catalog
  When I select a laptop priced at $999
  And I add it to my cart
  Then my cart should contain the laptop
  And my cart total should be $999

Scenario: Customer applies discount during checkout
  Given I have $100 worth of items in my cart
  When I apply discount code "SAVE10" for 10% off
  Then my cart total should be $90
  And I should see "Discount applied: $10 off"

Scenario: Customer completes order with credit card
  Given I have items in my cart
  When I checkout with my credit card
  Then my order should be confirmed
  And I should receive an order number
  And I should get an email confirmation

Scenario: Customer tracks order after purchase
  Given I have placed an order
  When I check my order status
  Then I should see the current shipping status
  And I should see the estimated delivery date
```

### Exercise 3 Solution: Vague Scenario Made Concrete

**Original:** Completely abstract  
**Improved:** Specific and concrete

```gherkin
Feature: Product Search
  As a customer
  I want to search for products
  So that I can find items I want to buy

Scenario: Customer finds products by keyword search
  Given the catalog contains laptops and phones
  When I search for "laptop"
  Then I should see a list of available laptops
  And each result should show the product name and price
  And I should not see any phones in the results
```

### Exercise 4 Solution: UI-Heavy Scenario Simplified

**Original:** Imperative, UI-focused  
**Improved:** Declarative, business-focused

```gherkin
Feature: User Authentication
  As a registered user
  I want to log into my account
  So that I can access my personal dashboard

Scenario: Successful login with valid credentials
  Given I am a registered user
  When I log in with my email and password
  Then I should see my personal dashboard
  And I should see my username displayed
  And I should have access to my account features
```

### Exercise 5 Solution: Conjunction Anti-Pattern Fixed

**Original:** Multiple actions and assertions chained together  
**Improved:** Clean, separate steps

```gherkin
Feature: Wishlist to Order Conversion
  As a customer
  I want to purchase items from my wishlist
  So that I can easily buy products I've saved

Scenario: Purchase wishlist items with discount
  Given I am logged in
  And I have items in my wishlist
  And I have a valid payment method
  When I add wishlist items to my cart
  And I apply discount code "SAVE15"
  And I complete the checkout process
  Then my order should be confirmed
  And I should receive an order confirmation email
  And my payment should be processed
  And the purchased items should be removed from my wishlist
```

## Key Improvement Patterns

### From Technical to Business Language

- **Before:** "POST to /api/users returns 201"
- **After:** "Customer account is created successfully"

### From Imperative to Declarative

- **Before:** "Click username field, type email, click password field, type password, click submit"
- **After:** "Log in with email and password"

### From Vague to Concrete

- **Before:** "User does something and sees results"
- **After:** "Customer searches for laptop and sees laptop products"

### From Multiple Behaviors to Single Focus

- **Before:** "Login and update profile and place order"
- **After:** Three separate scenarios for login, profile update, and order placement

### From Implementation to Business Outcome

- **Before:** "Database record is created with hashed password"
- **After:** "Customer can log in with their new account"

## Self-Assessment Questions

After completing these exercises, ask yourself:

1. **Business Readability:** Could a non-technical stakeholder understand what each scenario tests?
1. **Single Responsibility:** Does each scenario test exactly one behavior or business rule?
1. **Concrete Examples:** Are you using specific data rather than vague terms like "some" or "something"?
1. **User Perspective:** Are scenarios written from the user's point of view rather than the system's?
1. **Business Value:** Does each scenario test something that matters to the business?
1. **Maintainability:** Will these scenarios be easy to update when requirements change?

## Additional Practice

Try refactoring these common problematic patterns from real projects:

### The Database Scenario

```gherkin
Scenario: Data persistence verification
  Given the user table is empty
  When I insert a user record with valid data
  Then the record should exist in the database
  And the foreign key constraints should be satisfied
```

### The Configuration Scenario

```gherkin
Scenario: System configuration validation
  Given the config file has debug=true and timeout=30
  When the application starts up
  Then it should load with debug logging enabled
  And it should set request timeout to 30 seconds
```

### The Performance Scenario

```gherkin
Scenario: Response time requirements
  Given 1000 concurrent users are active
  When they all search for products simultaneously
  Then 95% of requests should complete within 200ms
  And no requests should timeout
```

**Challenge:** How would you refactor these to focus on business value rather than technical implementation?

## Next Steps

1. **Practice with real scenarios** from your current project
2. **Get feedback** from teammates on your refactored scenarios
3. **Identify patterns** in your team's existing scenarios that could be improved
4. **Create a style guide** for your team based on these principles
5. **Prepare for automation** - which of these improved scenarios would be good candidates for automated testing?

Remember: The goal of refactoring scenarios is to make them more valuable as communication tools and living documentation, not just to make them "technically correct."
