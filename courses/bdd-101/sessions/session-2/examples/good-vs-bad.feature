# Good vs Bad BDD Scenarios

# This file demonstrates the difference between well-written and poorly-written
# BDD scenarios. Use these examples for Session 2 discussions and exercises.

# =============================================================================
# BAD EXAMPLES - What NOT to do
# =============================================================================

Feature: Bad Examples - Do Not Copy These!

  # BAD: Too technical, implementation details exposed
  Scenario: API endpoint validation
    Given the user service microservice is running on port 8080
    And the database has table user_accounts with valid schema
    When I send HTTP POST request to /api/v1/users endpoint
    And the request payload contains valid JSON with required fields
    Then the response should have status code 201
    And the response header should contain Content-Type application/json
    And the response body should contain user_id field with integer value

  # BAD: Too vague, no concrete details  
  Scenario: User does something
    Given user exists
    When user performs action
    Then something happens

  # BAD: Multiple unrelated behaviors in one scenario
  Scenario: Complete application workflow
    Given I create an account and verify email and log in and update profile
    When I search for products and add to cart and checkout and pay and track order
    Then everything should work and I should be happy

  # BAD: Imperative style, too much UI detail
  Scenario: User login process with detailed UI steps
    Given I navigate to www.example.com in Chrome browser
    And I wait 3 seconds for the page to fully load
    And I see the login form with blue Submit button
    When I click on the username input field with id="username"
    And I type "john@example.com" using keyboard
    And I press Tab key to move to password field
    And I type "secretpassword123" using keyboard
    And I click the blue Submit button with CSS class "btn-primary"
    And I wait 2 seconds for form submission
    Then I should be redirected to URL "/dashboard"
    And I should see the text "Welcome back!" in Arial font
    And the browser title should change to "Dashboard - My App"

  # BAD: Incidental details that don't matter
  Scenario: Order processing on Tuesday
    Given it is Tuesday, March 15th, 2024 at 2:30 PM EST
    And the temperature outside is 72 degrees Fahrenheit
    And I am using a Dell laptop with Windows 11
    And I am sitting in my office chair
    And I had coffee for breakfast
    When I place an order for a red bicycle
    Then my order should be processed

  # BAD: Unclear pronouns and references
  Scenario: Thing happens to stuff
    Given it exists
    When they do it
    Then that should be there

  # BAD: Conjunction anti-pattern
  Scenario: User registration and login and shopping
    When I register and login and shop and pay
    Then I should see confirmation and receipt and tracking

# =============================================================================
# GOOD EXAMPLES - Follow These Patterns
# =============================================================================

Feature: User Account Management
  As a website user
  I want to manage my account
  So that I can access personalized features

  # GOOD: Business language, clear behavior, concrete details
  Scenario: Customer creates new account with valid information
    Given I am on the registration page
    When I provide valid account information:
      | Field | Value |
      | Email | john.smith@example.com |
      | Password | SecurePass123! |
      | First Name | John |
      | Last Name | Smith |
    Then my account should be created successfully
    And I should receive a welcome email
    And I should be automatically logged in

  # GOOD: Focused on one specific behavior
  Scenario: Customer cannot register with existing email address
    Given an account already exists for "existing@example.com"
    When I try to register with email "existing@example.com"
    Then I should see "An account with this email already exists"
    And I should not be logged in
    And no new account should be created

  # GOOD: Declarative style, business outcome focused
  Scenario: Customer updates profile information
    Given I am logged into my account
    When I change my email address to "newemail@example.com"
    Then my profile should show the updated email address
    And I should receive a confirmation email at the new address

Feature: Online Shopping Cart
  As a customer
  I want to manage items in my shopping cart
  So that I can control my purchases before checkout

  Background:
    Given I am a registered customer
    And I am logged into the website

  # GOOD: Concrete data, clear business rule
  Scenario: Add product to empty cart
    Given my shopping cart is empty
    When I add a "Wireless Bluetooth Headphones" priced at $79.99 to my cart
    Then my cart should contain 1 item
    And my cart total should be $79.99
    And I should see "Item added to cart" confirmation

  # GOOD: Tests business rule with specific data
  Scenario: Apply valid discount code
    Given I have $150 worth of items in my cart
    When I apply discount code "SAVE20" for 20% off
    Then my cart total should be $120
    And I should see "Discount applied: Save $30"

  # GOOD: Error condition clearly specified
  Scenario: Cannot apply expired discount code
    Given I have items in my cart
    When I apply discount code "EXPIRED2023" that expired last month
    Then I should see "This discount code has expired"
    And my cart total should remain unchanged
    And no discount should be applied

Feature: Order Processing
  As a customer
  I want to place orders for my selected items
  So that I can purchase products I need

  # GOOD: Uses Scenario Outline for testing business rules
  Scenario Outline: Calculate shipping cost based on order value
    Given I have $<order_value> worth of items in my cart
    When I proceed to checkout
    Then my shipping cost should be $<shipping_cost>

    Examples:
      | order_value | shipping_cost |
      | 25.00 | 5.99 |
      | 75.00 | 5.99 |
      | 100.00 | 0.00 |
      | 250.00 | 0.00 |

  # GOOD: Clear business scenario with specific outcome
  Scenario: VIP customer receives priority processing
    Given I am a VIP customer
    And I place an order worth $500
    When I complete my purchase
    Then my order should be marked for priority processing
    And I should receive expedited shipping at no extra cost
    And I should get a dedicated customer service contact

Feature: Customer Support
  As a customer
  I want to get help with my orders
  So that I can resolve any issues quickly

  # GOOD: Tests business rule about response times
  Scenario: Premium customer gets priority support response
    Given I am a premium customer with active support plan
    When I submit a support ticket with "High" priority
    Then I should receive an automated confirmation
    And my ticket should be assigned to the priority queue
    And I should receive a response within 2 hours

  # GOOD: Covers edge case with clear business rule
  Scenario: Cannot submit support ticket without order number for order-related issues
    Given I want to report an issue with my order
    When I submit a support ticket without providing an order number
    Then I should see "Order number is required for order-related issues"
    And my ticket should not be submitted
    And I should be prompted to provide my order number

Feature: Product Search
  As a customer
  I want to search for products
  So that I can find items I want to purchase

  # GOOD: Tests main functionality with realistic data
  Scenario: Search returns relevant products sorted by popularity
    Given the catalog contains laptops, phones, and accessories
    When I search for "laptop"
    Then I should see only laptop products in the results
    And the results should be sorted by customer rating
    And each result should show product name, price, and rating

  # GOOD: Tests boundary condition
  Scenario: Search with very long query is handled gracefully
    Given I am on the search page
    When I search for a 500-character query string
    Then I should see "Search query too long. Please use fewer words."
    And no search results should be displayed

  # GOOD: Tests empty state
  Scenario: Search for unavailable product shows helpful message
    Given there are no unicorn-related products in the catalog
    When I search for "unicorn"
    Then I should see "No products found for 'unicorn'"
    And I should see suggestions for popular products
    And I should see an option to request this product

# =============================================================================
# ANALYSIS NOTES
# =============================================================================

# What makes the GOOD examples good:
# 1. Business language - Anyone can understand what's being tested
# 2. Concrete data - Specific amounts, names, timeframes
# 3. Single responsibility - Each scenario tests one behavior
# 4. User perspective - Written from customer/user point of view  
# 5. Clear outcomes - Specific, measurable results
# 6. Business rules focus - Tests important business logic
# 7. Realistic examples - Scenarios that could actually happen

# What makes the BAD examples bad:
# 1. Technical jargon - Only developers can understand
# 2. Vague language - Abstract terms like "something" and "stuff"
# 3. Multiple behaviors - Testing too many things at once
# 4. Implementation details - Exposing how rather than what
# 5. Irrelevant details - Information that doesn't affect the behavior
# 6. UI-dependent - Tightly coupled to user interface specifics
# 7. Unmaintainable - Will break when implementation changes

# Key principles for writing good scenarios:
# - Write for your audience (business stakeholders should understand)
# - Focus on business value and user outcomes
# - Use concrete, realistic examples
# - Keep scenarios independent and focused
# - Avoid implementation details
# - Make scenarios readable as documentation