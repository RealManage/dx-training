# Basic Gherkin Scenarios for Learning

# This file contains example scenarios for Session 1 of the BDD course.
# These scenarios demonstrate proper Gherkin syntax and good BDD practices.

Feature: ATM Cash Withdrawal
  As a bank customer
  I want to withdraw cash from an ATM
  So that I can access my money when needed

  Scenario: Successful cash withdrawal with sufficient funds
    Given I am a bank customer with a valid account
    And my account balance is $500
    And I have inserted my debit card into the ATM
    When I enter my correct PIN
    And I select "Withdraw Cash"
    And I choose to withdraw $100
    Then I should receive $100 in cash
    And my account balance should be $400
    And I should receive a transaction receipt

  Scenario: Cannot withdraw more than account balance
    Given I am a bank customer with a valid account
    And my account balance is $50
    And I have inserted my debit card into the ATM
    When I enter my correct PIN
    And I select "Withdraw Cash"
    And I choose to withdraw $100
    Then I should see "Insufficient funds"
    And I should not receive any cash
    And my account balance should remain $50

  Scenario: ATM blocks card after multiple incorrect PIN attempts
    Given I am at an ATM with my debit card
    And I have already entered an incorrect PIN 2 times
    When I enter an incorrect PIN for the third time
    Then my card should be retained by the ATM
    And I should see "Card blocked for security reasons"
    And I should be instructed to contact my bank

Feature: Online Shopping Cart
  As an online shopper
  I want to manage items in my shopping cart
  So that I can control what I purchase

  Scenario: Add item to empty cart
    Given I am on the product catalog page
    And my shopping cart is empty
    When I click "Add to Cart" for a laptop priced at $999
    Then the laptop should appear in my cart
    And my cart total should be $999
    And the cart icon should show "1 item"

  Scenario: Remove item from cart
    Given I have a laptop in my cart worth $999
    When I click "Remove" next to the laptop
    Then my cart should be empty
    And my cart total should be $0
    And the cart icon should show "0 items"

  Scenario: Update item quantity in cart
    Given I have 2 books in my cart, each priced at $15
    And my cart total is $30
    When I change the quantity of books to 3
    Then my cart should show 3 books
    And my cart total should be $45
    And the cart icon should show "3 items"

Feature: Email Newsletter Subscription
  As a website visitor
  I want to subscribe to the email newsletter
  So that I can receive updates about new content

  Scenario: Subscribe with valid email address
    Given I am on the newsletter signup page
    When I enter "user@example.com" in the email field
    And I click "Subscribe"
    Then I should see "Thank you for subscribing!"
    And I should receive a confirmation email
    And my email should be added to the newsletter list

  Scenario: Cannot subscribe with invalid email format
    Given I am on the newsletter signup page
    When I enter "invalid-email" in the email field
    And I click "Subscribe"
    Then I should see "Please enter a valid email address"
    And I should not receive a confirmation email
    And my email should not be added to the newsletter list

  Scenario: Cannot subscribe with email already on list
    Given "existing@example.com" is already subscribed to the newsletter
    And I am on the newsletter signup page
    When I enter "existing@example.com" in the email field
    And I click "Subscribe"
    Then I should see "This email is already subscribed"
    And I should not receive a confirmation email

Feature: User Account Login
  As a registered user
  I want to log into my account
  So that I can access my personal information

  Scenario: Successful login with valid credentials
    Given I am a registered user with email "john@example.com"
    And my password is "SecurePass123"
    And I am on the login page
    When I enter "john@example.com" in the email field
    And I enter "SecurePass123" in the password field
    And I click "Log In"
    Then I should be redirected to my dashboard
    And I should see "Welcome back, John!"
    And I should see my account navigation menu

  Scenario: Login fails with incorrect password
    Given I am a registered user with email "john@example.com"
    And I am on the login page
    When I enter "john@example.com" in the email field
    And I enter "WrongPassword" in the password field
    And I click "Log In"
    Then I should remain on the login page
    And I should see "Invalid email or password"
    And I should not be logged in

  Scenario: Login fails with unregistered email
    Given I am on the login page
    When I enter "unknown@example.com" in the email field
    And I enter "AnyPassword123" in the password field
    And I click "Log In"
    Then I should remain on the login page
    And I should see "Invalid email or password"
    And I should not be logged in

Feature: Library Book Search
  As a library patron
  I want to search for books in the catalog
  So that I can find books I want to borrow

  Scenario: Search finds matching books
    Given the library catalog contains books about cooking
    And I am on the library search page
    When I search for "cooking"
    Then I should see a list of cooking books
    And each result should show the title and author
    And each result should show availability status

  Scenario: Search returns no results
    Given I am on the library search page
    When I search for "unicorn training manual"
    Then I should see "No books found matching your search"
    And I should see suggestions for popular books
    And I should be able to request the library purchase the book

  Scenario: Search with empty query
    Given I am on the library search page
    When I click "Search" without entering any text
    Then I should see "Please enter a search term"
    And I should remain on the search page
    And no search results should be displayed

Feature: Restaurant Table Reservation
  As a restaurant customer
  I want to make a table reservation
  So that I can guarantee seating for my planned visit

  Scenario: Successfully reserve available table
    Given I am on the restaurant reservation page
    And there are tables available for 4 people on Friday at 7 PM
    When I select Friday at 7 PM for 4 people
    And I provide my contact information
    And I click "Reserve Table"
    Then I should see "Reservation confirmed"
    And I should receive a confirmation email
    And the table should be marked as reserved

  Scenario: Cannot reserve when restaurant is fully booked
    Given I am on the restaurant reservation page
    And there are no tables available for 6 people on Saturday at 8 PM
    When I select Saturday at 8 PM for 6 people
    And I click "Check Availability"
    Then I should see "No tables available for your requested time"
    And I should see alternative time suggestions
    And I should be able to join a waiting list

  Scenario: Reservation requires advance notice
    Given I am on the restaurant reservation page
    And today is Friday
    When I try to make a reservation for Friday at 8 PM (same day)
    Then I should see "Reservations require 24 hours advance notice"
    And I should not be able to complete the reservation
    And I should see available times for tomorrow and later