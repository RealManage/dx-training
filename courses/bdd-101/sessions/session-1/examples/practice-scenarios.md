# Practice Scenarios for Session 1

## Overview

This document contains practice exercises for Session 1 participants. Use these user stories to practice converting requirements into Gherkin scenarios.

## Practice Exercise 1: Movie Ticket Booking

### User Story 1

```text
As a movie theater customer
I want to book tickets online
So that I can guarantee my seats for the show

Acceptance Criteria:
- Customers can select movie, showtime, and seats
- Payment is required to complete booking
- Confirmation email is sent after successful booking
- Booked seats are no longer available to other customers
```

### Your Task 1

Write 2-3 Gherkin scenarios that cover:

1. Successful ticket booking
1. Attempting to book already-taken seats
1. Booking failure due to payment issues

### Space for Your Scenarios 1

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

## Practice Exercise 2: Password Reset

### User Story 2

```text
As a user who forgot their password
I want to reset my password via email
So that I can regain access to my account

Acceptance Criteria:
- User enters their email address
- Reset link is sent to the email (if account exists)
- Reset link expires after 24 hours
- User can set new password using valid reset link
- Old password becomes invalid after reset
```

### Your Task 2

Write scenarios that cover:

1. Successful password reset flow
1. Password reset with non-existent email
1. Using an expired reset link

### Space for Your Scenarios 2

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

## Practice Exercise 3: Online Course Enrollment

### User Story 3

```text
As a student
I want to enroll in online courses
So that I can learn new skills

Business Rules:
- Students must be logged in to enroll
- Some courses have prerequisites
- Course capacity limits enrollment
- Payment is required for paid courses
- Free courses allow immediate enrollment
```

### Your Task 3

Write scenarios covering:

1. Free course enrollment
1. Paid course enrollment
1. Enrollment blocked due to missing prerequisites
1. Enrollment blocked due to course being full

### Space for Your Scenarios

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

Scenario: 
  Given 
  When 
  Then 
```

---

## Sample Solutions

*Note: These are example solutions. Your scenarios might be different and still correct!*

### Exercise 1 Solution: Movie Ticket Booking

```gherkin
Feature: Online Movie Ticket Booking
  As a movie theater customer
  I want to book tickets online
  So that I can guarantee my seats for the show

Scenario: Successfully book available seats
  Given I am on the movie booking page
  And "Avengers: Endgame" is showing at 7 PM tonight
  And seats A1 and A2 are available
  When I select "Avengers: Endgame" at 7 PM
  And I choose seats A1 and A2
  And I complete payment with my credit card
  Then my booking should be confirmed
  And I should receive a confirmation email
  And seats A1 and A2 should no longer be available

Scenario: Cannot book already-taken seats
  Given I am on the movie booking page
  And "Avengers: Endgame" is showing at 7 PM tonight
  And seats A1 and A2 are already booked
  When I try to select seats A1 and A2
  Then I should see "These seats are no longer available"
  And I should be shown alternative available seats

Scenario: Booking fails due to payment failure
  Given I am on the movie booking page
  And I have selected a movie, showtime, and seats
  When I attempt to pay with an expired credit card
  Then I should see "Payment failed - please check your card details"
  And my seats should not be booked
  And the seats should remain available for other customers
```

### Exercise 2 Solution: Password Reset

```gherkin
Feature: Password Reset
  As a user who forgot their password
  I want to reset my password via email
  So that I can regain access to my account

Scenario: Successfully reset password with valid email
  Given I am on the password reset page
  And I have an account with email "user@example.com"
  When I enter "user@example.com" and click "Send Reset Link"
  Then I should see "Reset link sent to your email"
  And I should receive an email with a reset link
  When I click the reset link within 24 hours
  And I enter a new password "NewSecure123!"
  Then my password should be updated
  And I should be able to login with the new password

Scenario: Password reset with non-existent email
  Given I am on the password reset page
  When I enter "nonexistent@example.com" and click "Send Reset Link"
  Then I should see "Reset link sent to your email"
  But no email should be sent
  And no reset link should be created

Scenario: Cannot reset password with expired link
  Given I have a password reset link that expired yesterday
  When I click the expired reset link
  Then I should see "This reset link has expired"
  And I should be directed to request a new reset link
  And I should not be able to change my password
```

### Exercise 3 Solution: Online Course Enrollment

```gherkin
Feature: Online Course Enrollment
  As a student
  I want to enroll in online courses
  So that I can learn new skills

Scenario: Enroll in free course
  Given I am a logged-in student
  And "Introduction to Python" is a free course
  And the course has available spots
  When I click "Enroll" for "Introduction to Python"
  Then I should be immediately enrolled
  And I should see "Enrollment successful"
  And I should have access to course materials

Scenario: Enroll in paid course after payment
  Given I am a logged-in student
  And "Advanced JavaScript" costs $99
  And the course has available spots
  When I click "Enroll" for "Advanced JavaScript"
  And I complete payment with my credit card
  Then I should be enrolled in the course
  And I should receive a payment receipt
  And I should have access to course materials

Scenario: Cannot enroll without prerequisites
  Given I am a logged-in student
  And "Advanced Python" requires "Introduction to Python"
  And I have not completed "Introduction to Python"
  When I try to enroll in "Advanced Python"
  Then I should see "You must complete Introduction to Python first"
  And I should not be enrolled
  And I should see a link to the prerequisite course

Scenario: Cannot enroll when course is full
  Given I am a logged-in student
  And "Web Development Bootcamp" has a capacity of 20 students
  And 20 students are already enrolled
  When I try to enroll in "Web Development Bootcamp"
  Then I should see "This course is currently full"
  And I should not be enrolled
  And I should see an option to join the waiting list
```

## Self-Assessment Questions

After completing these exercises, ask yourself:

1. **Clarity**: Can someone unfamiliar with the system understand what each scenario tests?
1. **Completeness**: Do your scenarios cover the main success path and important error conditions?
1. **Business Focus**: Are your scenarios written from the user's perspective rather than technical implementation?
1. **Testability**: Could a developer automate these scenarios?
1. **Maintainability**: If requirements change slightly, would your scenarios be easy to update?

## Common Issues to Watch For

### Issue 1: Too Much Technical Detail

```gherkin
# Avoid this
When I POST to /api/bookings with JSON payload
Then the response status should be 201

# Better
When I complete my booking
Then my reservation should be confirmed
```

### Issue 2: Vague Outcomes

```gherkin
# Avoid this
Then everything should work

# Better
Then I should receive a confirmation email
And my seat should be reserved
```

### Issue 3: Multiple Behaviors in One Scenario

```gherkin
# Avoid this
Scenario: Book ticket and update profile and check email
  When I book a ticket and update my profile and check my email
  Then everything should be updated

# Better - Create separate scenarios
Scenario: Book movie ticket
Scenario: Update user profile  
Scenario: Check notification email
```

## Tips for Improvement

1. **Read scenarios aloud** - If they sound natural, they're probably good
1. **Show scenarios to non-technical people** - Can they understand what's being tested?
1. **Focus on business value** - Each scenario should test something that matters to users
1. **Use concrete examples** - "3 items" is better than "some items"
1. **Keep scenarios independent** - Each scenario should be able to run on its own

## Next Steps

1. **Review your scenarios** using the self-assessment questions
1. **Get feedback** from a colleague or teammate
1. **Practice with real user stories** from your current project
1. **Prepare for Session 2** where we'll dive deeper into scenario quality and advanced Gherkin features

Remember: The goal isn't to write perfect scenarios immediately. It's to start thinking about requirements from the user's perspective and expressing them in a way that everyone on your team can understand!
