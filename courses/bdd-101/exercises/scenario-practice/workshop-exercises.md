# BDD Workshop Exercises

## Overview

These hands-on exercises are designed for use during BDD training sessions. Each exercise builds on the previous ones and includes facilitator notes, timing guidance, and expected outcomes.

## Exercise 1: User Story to Scenario Conversion (45 minutes)

### Objective

Learn to convert user stories into basic Gherkin scenarios.

### Materials Needed

- Whiteboard or flip chart
- Sticky notes
- User story examples (provided below)

### Instructions

#### Part A: Individual Work (15 minutes)

Each participant receives this user story:

```text
As a library member
I want to borrow books online
So that I can reserve books before visiting the library

Acceptance Criteria:
- Members can search available books
- Members can reserve up to 3 books at a time
- Reserved books are held for 48 hours
- Members receive confirmation email
```

**Task:** Write 2-3 scenarios in Gherkin format

#### Part B: Small Group Review (15 minutes)

- Form groups of 3-4 people
- Share individual scenarios
- Discuss differences in approach
- Agree on best version for each scenario

#### Part C: Group Presentation (15 minutes)

- Each group presents one scenario
- Facilitator provides feedback
- Class discusses common patterns and issues

### Expected Outcomes

### Good Example

```gherkin
Feature: Online Book Reservation
  As a library member
  I want to borrow books online
  So that I can reserve books before visiting the library

Scenario: Member successfully reserves available book
  Given I am a registered library member
  And there are books available for reservation
  When I search for "The Great Gatsby"
  And I reserve the book
  Then the book should be reserved for me
  And I should receive a confirmation email
  And the book should be held for 48 hours

Scenario: Member cannot reserve when limit reached
  Given I am a library member
  And I already have 3 books reserved
  When I attempt to reserve another book
  Then I should see an error message
  And the reservation should not be processed
```

### Facilitator Notes

- Watch for overly technical language
- Encourage business-readable scenarios
- Point out when scenarios are too detailed or too vague
- Emphasize the Given-When-Then structure

---

## Exercise 2: Three Amigos Collaboration (60 minutes)

### Exercise 2 Objective

Practice collaborative scenario writing using the Three Amigos approach.

### Setup

- Groups of 3 people
- Assign roles: Business Analyst, Developer, Tester
- Provide role cards with perspective guidelines

### Role Cards

#### Business Analyst

- Focus on business value and user needs
- Ask "Why is this important to users?"
- Ensure scenarios reflect real business rules
- Challenge technical assumptions

#### Developer

- Consider technical feasibility
- Ask "How will this be implemented?"
- Identify potential technical constraints
- Suggest simpler alternatives

#### Tester

- Think about edge cases and error conditions
- Ask "What could go wrong?"
- Consider different user types and scenarios
- Focus on testability

### User Story for Exercise

```text
As an online shopper
I want to apply discount coupons to my order
So that I can save money on my purchases

Business Rules:
- Coupons have expiration dates
- Some coupons are for specific product categories
- Maximum one coupon per order
- Coupons cannot be combined with sale prices
- Minimum order value may be required
```

### Exercise 2 Instructions

#### Phase 1: Exploration (20 minutes)

- Each role asks questions from their perspective
- Document questions and assumptions
- Identify unclear requirements
- Note potential edge cases

#### Phase 2: Example Generation (25 minutes)

- Collaboratively create specific examples
- Use Example Mapping technique:
  - Story (center)
  - Rules (yellow sticky notes)
  - Examples (green sticky notes)
  - Questions (red sticky notes)

#### Phase 3: Scenario Writing (15 minutes)

- Convert examples into Gherkin scenarios
- Ensure all three perspectives are represented
- Focus on most important scenarios first

### Exercise 2 Expected Outcomes

#### Example Mapping Result

```text
Story: Apply Discount Coupons

Rules:
- Expiration dates must be checked
- Category restrictions apply
- One coupon per order
- No combination with sale prices
- Minimum order requirements

Examples:
- Valid coupon, order meets requirements → discount applied
- Expired coupon → error message
- Wrong category coupon → error message  
- Order below minimum → error message
- Sale item + coupon → coupon not applied

Questions:
- What happens if coupon value > order total?
- Can customers save coupons for later?
- How are percentage vs. fixed amount coupons handled?
```

### Resulting Scenarios

```gherkin
Scenario: Valid coupon applied successfully
  Given I have items worth $100 in my cart
  And I have a valid 10% discount coupon
  When I apply the coupon code "SAVE10"
  Then my order total should be $90
  And I should see "Coupon applied successfully"

Scenario: Expired coupon rejected
  Given I have items in my cart
  And I have a coupon that expired yesterday
  When I apply the expired coupon code
  Then I should see "This coupon has expired"
  And my order total should remain unchanged
```

### Exercise 2 Facilitator Notes

- Encourage active participation from all roles
- Intervene if one role dominates discussion
- Help resolve conflicts between perspectives
- Emphasize that questions are valuable outputs

---

## Exercise 3: Scenario Quality Assessment (30 minutes)

### Exercise 3 Objective

Learn to identify and fix common scenario quality issues.

### Exercise 3 Materials

- Handout with poor-quality scenarios
- Quality checklist

### Poor-Quality Scenarios for Review

#### Scenario A

```gherkin
Scenario: User login
  Given the user goes to www.example.com
  And clicks on the login button
  And waits for the page to load
  And sees the login form
  When they enter username "john@example.com" in the username field
  And they enter password "password123" in the password field
  And they click the Submit button
  And the page loads for 2 seconds
  Then they should see the dashboard page
  And their username should appear in the top right corner
  And the navigation menu should be visible
  And there should be no error messages
```

#### Scenario B

```gherkin
Scenario: API test
  Given the system is running
  When I send a POST request
  Then I should get a response
```

#### Scenario C

```gherkin
Scenario: Customer places order and receives confirmation and gets charged
  Given I am logged in
  When I buy something and pay for it and get an email
  Then everything should work
```

### Exercise 3 Instructions

#### Part A: Individual Assessment (10 minutes)

- Review each scenario
- Identify specific problems
- Note which BDD principles are violated

#### Part B: Group Discussion (10 minutes)

- Share findings with the group
- Discuss why each issue is problematic
- Reference BDD best practices

#### Part C: Scenario Improvement (10 minutes)

- Rewrite one scenario following best practices
- Compare improved version with original

### Quality Checklist

- [ ] Written in business language?
- [ ] Single responsibility per scenario?
- [ ] Clear Given-When-Then structure?
- [ ] Declarative rather than imperative?
- [ ] Focuses on business outcome, not UI details?
- [ ] Uses concrete examples?
- [ ] Could be understood by business stakeholders?

### Expected Improvements

#### Scenario A Improved

```gherkin
Scenario: Successful user login
  Given I am a registered user
  When I log in with valid credentials
  Then I should see my personal dashboard
```

#### Scenario B Improved

```gherkin
Scenario: Create new customer account
  Given I am on the registration page
  When I provide valid customer information
  Then my account should be created successfully
  And I should receive a welcome email
```

#### Scenario C Improved

```gherkin
Scenario: Customer completes online purchase
  Given I have items in my shopping cart
  When I complete the checkout process
  Then my order should be confirmed
  And I should be charged the correct amount
  And I should receive an order confirmation email
```

---

## Exercise 4: Scenario Outline Practice (40 minutes)

### Exercise 4 Objective

Learn when and how to use Scenario Outlines effectively.

### User Story

```text
As a password security system
I want to validate user passwords
So that user accounts remain secure

Password Rules:
- Minimum 8 characters
- Must contain at least one uppercase letter
- Must contain at least one lowercase letter  
- Must contain at least one number
- Must contain at least one special character (!@#$%^&*)
- Cannot contain common words (password, 123456, qwerty)
```

### Exercise 4 Instructions

#### Part A: Individual Work (20 minutes)

Write individual scenarios for different password validation cases:

1. Valid password that meets all criteria
1. Password too short
1. Missing uppercase letter
1. Missing number
1. Contains common word

#### Part B: Group Conversion (20 minutes)

- Compare individual scenarios
- Identify common patterns
- Convert to a Scenario Outline with Examples table
- Discuss when Scenario Outlines are appropriate

### Expected Outcome

#### Individual Scenarios

```gherkin
Scenario: Valid password is accepted
  Given I am creating a new account
  When I enter the password "MySecure123!"
  Then the password should be accepted

Scenario: Short password is rejected
  Given I am creating a new account
  When I enter the password "Short1!"
  Then I should see "Password must be at least 8 characters"
```

#### Scenario Outline

```gherkin
Scenario Outline: Password validation
  Given I am creating a new account
  When I enter the password "<password>"
  Then I should see "<result>"

Examples:
  | password | result |
  | MySecure123! | Password accepted |
  | Short1! | Password must be at least 8 characters |
  | mysecure123! | Password must contain uppercase letter |
  | MYSECURE123! | Password must contain lowercase letter |
  | MySecurePass! | Password must contain a number |
  | MySecure123 | Password must contain special character |
  | password123! | Password cannot contain common words |
```

### Discussion Points

- When are Scenario Outlines useful vs. individual scenarios?
- How do you balance readability with data coverage?
- What are the maintenance implications?

---

## Exercise 5: Real Project Application (45 minutes)

### Exercise 5 Objective

Apply BDD skills to participants' actual work projects.

### Prerequisites

- Participants bring real user stories from their projects
- Stories should be in progress or upcoming

### Exercise 5 Instructions

#### Part A: Story Selection (5 minutes)

- Each participant chooses one user story from their project
- Story should be complex enough for interesting scenarios
- Stories can be shared if participants work on same project

#### Part B: Individual Scenario Writing (20 minutes)

- Apply BDD techniques to real user story
- Write 2-4 scenarios covering main cases
- Focus on business value and acceptance criteria

#### Part C: Peer Review (15 minutes)

- Pair up with someone from different project
- Review each other's scenarios
- Provide feedback using quality checklist
- Suggest improvements

#### Part D: Group Sharing (5 minutes)

- Volunteers share one scenario with full group
- Class provides feedback
- Discuss challenges with real-world application

### Facilitator Guidelines

- Circulate during individual work to provide coaching
- Help participants avoid over-engineering
- Encourage focus on business value
- Address project-specific challenges

### Success Criteria

- Scenarios are written in business language
- Business value is clear
- Scenarios could be understood by project stakeholders
- Participants feel confident applying BDD to their work

---

## Exercise 6: Automation Planning (30 minutes)

### Exercise 6 Objective

Understand how scenarios translate into automated tests.

### Exercise 6 Materials

- Sample scenario
- Step definition examples
- Automation architecture diagram

### Sample Scenario

```gherkin
Scenario: Customer views order history
  Given I am a logged-in customer
  And I have placed 3 orders in the past month
  When I navigate to my order history page
  Then I should see my 3 recent orders
  And each order should show the order date
  And each order should show the total amount
```

### Exercise 6 Instructions

#### Part A: Step Analysis (10 minutes)

- Break down scenario into individual steps
- Identify what each step needs to accomplish
- Consider different implementation approaches

#### Part B: Implementation Discussion (15 minutes)

- Discuss how each step might be automated
- Consider different layers of testing (UI, API, database)
- Identify test data requirements

#### Part C: Architecture Planning (5 minutes)

- Discuss overall test automation strategy
- Consider Page Object pattern
- Plan test data management approach

### Expected Discussions

#### Step Implementation Options

```csharp
[Given(@"I am a logged-in customer")]
public void GivenIAmALoggedInCustomer()
{
    // Option 1: Use test user account
    // Option 2: Create user via API
    // Option 3: Mock authentication
}

[Given(@"I have placed (.*) orders in the past month")]
public void GivenIHavePlacedOrdersInThePastMonth(int orderCount)
{
    // Option 1: Create via UI
    // Option 2: Create via API  
    // Option 3: Use test database setup
}
```

#### Architecture Considerations

- Page Object pattern for UI interactions
- API helpers for data setup
- Database utilities for verification
- Test data management strategy

---

## Workshop Wrap-up Exercise (20 minutes)

### Wrap-up Objective

Consolidate learning and plan next steps.

### Reflection Questions

#### Individual Reflection (10 minutes)

Write brief answers to:

1. What was the most valuable thing you learned today?
2. What BDD concept do you still find confusing?
3. How will you apply BDD in your current project?
4. What obstacles do you anticipate?

#### Group Discussion (10 minutes)

- Share key insights
- Discuss common challenges
- Plan implementation approach
- Schedule follow-up sessions

### Action Planning Template

```text
Next Steps for BDD Implementation:

Week 1:

- [ ] Choose one feature for BDD pilot
- [ ] Schedule Three Amigos session
- [ ] Write first scenarios

Week 2:

- [ ] Set up automation framework
- [ ] Implement step definitions
- [ ] Run first automated scenarios

Month 1:

- [ ] Expand to 2-3 features
- [ ] Train additional team members
- [ ] Establish BDD process in workflow

Obstacles to Address:

- [ ] Tool setup and training
- [ ] Business stakeholder engagement
- [ ] Team adoption resistance
- [ ] Integration with existing processes
```

## Facilitator Resources

### Timing Guidelines

- Total workshop time: 4-6 hours
- Include 15-minute breaks every 90 minutes
- Adjust exercise times based on group size
- Allow extra time for complex discussions

### Common Issues and Solutions

- **Participants write technical scenarios**: Emphasize business language
- **Groups finish exercises too quickly**: Provide extension challenges
- **Resistance to BDD approach**: Focus on collaboration benefits
- **Complex project contexts**: Help simplify for learning purposes

### Materials Checklist

- [ ] Flip chart paper and markers
- [ ] Sticky notes (multiple colors)
- [ ] Handouts with examples
- [ ] Quality checklists
- [ ] Role cards for Three Amigos
- [ ] Real user stories from participants

### Follow-up Resources

- Provide contact information for ongoing support
- Share additional reading materials
- Schedule optional follow-up sessions
- Create internal community of practice
