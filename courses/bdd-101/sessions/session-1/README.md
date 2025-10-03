# Session 1: BDD Foundations & Gherkin Syntax

**Duration:** 2 hours  
**Format:** Interactive workshop  
**Prerequisites:** Basic understanding of software testing and user stories

## 🎯 Learning Objectives

By the end of this session, participants will be able to:

- ✅ Explain what BDD is and how it differs from other testing approaches
- ✅ Understand the Three Amigos collaboration model
- ✅ Write basic scenarios using Gherkin syntax
- ✅ Convert simple user stories into Gherkin scenarios
- ✅ Identify the business value of BDD practices

## 📚 Session Agenda

### 1. Welcome & Introductions (10 minutes)

#### Session Overview

- What we'll cover today
- How this fits into the 3-session BDD course
- Expectations and ground rules

---

### 2. What is BDD? (30 minutes)

#### 2.1 BDD Definition and Benefits (15 minutes)

**Behavior-Driven Development (BDD)** is a collaborative approach to software development that:

- Focuses on the behavior of software from the user's perspective
- Uses natural language to describe expected behavior
- Encourages collaboration between business and technical team members
- Creates living documentation that stays current with the software

### Key Principles

1. **Collaboration First** - BDD is about people working together
1. **Business Value Focus** - Every feature should deliver business value
1. **Outside-In Thinking** - Start with user needs, not technical implementation
1. **Living Documentation** - Scenarios serve as always up-to-date documentation

#### 2.2 BDD vs. Other Approaches (10 minutes)

| Approach | Focus | Language | Primary Benefit |
|----------|-------|----------|-----------------|
| **Unit Testing** | Code functions | Technical | Fast feedback on code correctness (Can be implemented with BDD) |
| **TDD** | Test-first development | Technical | Better code design |
| **BDD** | Business behavior | Natural language | Better communication & collaboration |
| **Manual Testing** | User experience | Natural language | Real user perspective |

### BDD complements, doesn't replace other testing approaches

#### 2.3 Common BDD Misconceptions (5 minutes)

❌ **Myth:** "BDD is just testing with different keywords"  
✅ **Reality:** BDD is about collaboration and shared understanding

❌ **Myth:** "BDD scenarios must all be automated"  
✅ **Reality:** The conversation and shared understanding are more valuable than automation

❌ **Myth:** "BDD is only for complex applications"  
✅ **Reality:** BDD principles benefit any software project

---

### 3. The Three Amigos Collaboration Model (20 minutes)

#### 3.1 Who Are the Three Amigos? (10 minutes)

### The Business Perspective

- Represents user needs and business value
- Understands market requirements and constraints
- Focuses on "What should the system do?"
- **Typical roles:** Product Manager, Business Analyst, Domain Expert

### The Development Perspective

- Understands technical constraints and possibilities
- Knows the existing system architecture
- Focuses on "How can we build this?"
- **Typical roles:** Developer, Architect, Technical Lead

### The Testing Perspective

- Thinks about edge cases and error conditions
- Considers user experience and quality
- Focuses on "What could go wrong?"
- **Typical roles:** QA Engineer, Tester, UX Designer

#### 3.2 Three Amigos in Practice (10 minutes)

### When do Three Amigos meet?

- During feature planning (before development starts)
- When requirements are unclear or complex
- When there are disagreements about functionality
- Regularly throughout development for complex features

### What do they do together?

1. **Explore** the feature requirements
1. **Clarify** ambiguous or missing requirements  
1. **Create examples** of expected behavior
1. **Document** agreed understanding as scenarios

### Example Three Amigos Conversation

*Business:* "Users need to search for products on our e-commerce site."

*Developer:* "Should the search work on product names only, or also descriptions and categories?"

*Tester:* "What should happen if someone searches for something we don't sell? And what about typos in search terms?"

*Business:* "Good questions! Search should cover names and descriptions. For items we don't sell, show 'No results found' and maybe suggest popular items. For typos... let's start simple and just show exactly what they typed."

*Developer:* "That's feasible. Should results be sorted by relevance, price, or popularity?"

*Tester:* "And how many results should we show per page? What if someone searches for a single letter?"

---

### 4. Introduction to Gherkin Syntax (45 minutes)

#### 4.1 Basic Gherkin Structure (15 minutes)

**Gherkin** is the language used to write BDD scenarios. It uses simple keywords that anyone can understand.

### Core Keywords

**Feature:** High-level description of functionality

```gherkin
Feature: Product Search
  As a customer
  I want to search for products
  So that I can find items I want to buy
```

**Scenario:** Specific example of the feature behavior

```gherkin
Scenario: Customer finds products by keyword
```

**Given:** Sets up the initial context (preconditions)

```gherkin
Given I am on the product catalog page
Given there are laptops available in the store
```

**When:** Describes the action or event (trigger)

```gherkin
When I search for "laptop"
```

**Then:** Describes the expected outcome (verification)

```gherkin
Then I should see a list of laptops
Then the results should be sorted by popularity
```

**And/But:** Continues the previous step type

```gherkin
Given I am on the product catalog page
  And there are laptops available in the store
When I search for "laptop"
Then I should see a list of laptops
  And the results should be sorted by popularity
  But I should not see any phones
```

#### 4.2 Complete Example (10 minutes)

```gherkin
Feature: Online Product Search
  As a customer
  I want to search for products by keyword
  So that I can quickly find items I'm interested in buying

Scenario: Successful search with results
  Given I am on the product catalog page
  And there are products available
  When I search for "laptop"
  Then I should see a list of laptops
  And each result should show the product name and price
  And the results should be sorted by relevance

Scenario: Search with no results
  Given I am on the product catalog page
  When I search for "unicorn"
  Then I should see "No products found"
  And I should see suggested popular products
```

#### 4.3 Writing Good Scenarios (20 minutes)

### Best Practices

1. **Use Business Language**

   ```gherkin
   # Good - Business perspective
   Given I am a premium customer
   When I place an order over $100
   Then I should receive free shipping
   
   # Avoid - Technical perspective  
   Given the user record has premium_flag = true
   When the order total > 100
   Then the shipping_cost should = 0
   ```

1. **Be Specific but Not Overly Detailed**

   ```gherkin
   # Good - Right level of detail
   Given I have 3 items in my shopping cart
   When I proceed to checkout
   Then I should see the order total

   # Too vague
   Given I have some items
   When I do something with my cart
   Then something should happen

   # Too detailed
   Given I click on product A and add it to cart
   And I click on product B and add it to cart  
   And I click on product C and add it to cart
   And I navigate to the cart page
   And I click the checkout button
   And I wait for the page to load
   When I view the checkout page
   Then I should see $47.99 in the order total field
   ```

1. **Focus on Business Outcomes**

   ```gherkin
   # Good - Business outcome
   When I apply the discount code "SAVE10"
   Then my order total should be reduced by 10%

   # Avoid - Implementation details
   When I POST to /api/discounts with code "SAVE10"
   Then the response should contain discount_amount = order_total * 0.1
   ```

---

### 5. Hands-on Workshop: User Story to Scenarios (30 minutes)

#### 5.1 Exercise Setup (5 minutes)

### User Story

```text
As a library member
I want to renew my borrowed books online
So that I can keep books longer without visiting the library

Acceptance Criteria:
- Members can renew books that are not overdue
- Books can be renewed up to 2 times
- Renewal extends the due date by 2 weeks
- Members receive confirmation of successful renewal
- Overdue books cannot be renewed
```

#### 5.2 Individual Work (10 minutes)

**Task:** Convert this user story into 2-3 Gherkin scenarios

- Write scenarios that cover the main success path and important edge cases
- Use proper Given-When-Then structure
- Focus on business language and outcomes

#### 5.3 Small Group Discussion (10 minutes)

### Groups of 3-4 people

- Share your scenarios
- Discuss different approaches
- Agree on the best scenarios
- Prepare to share one scenario with the full group

#### 5.4 Group Sharing and Feedback (5 minutes)

- Each group shares one scenario
- Facilitator provides feedback
- Identify common patterns and good practices

### Expected Results

```gherkin
Feature: Online Book Renewal
  As a library member
  I want to renew my borrowed books online
  So that I can keep books longer without visiting the library

Scenario: Successfully renew book within renewal limit
  Given I am a library member
  And I have borrowed "The Great Gatsby"
  And the book is not overdue
  And I have not renewed this book before
  When I renew the book online
  Then the due date should be extended by 2 weeks
  And I should receive a renewal confirmation
  And the book should show 1 renewal used

Scenario: Cannot renew overdue book
  Given I am a library member
  And I have borrowed a book that is overdue
  When I attempt to renew the book online
  Then I should see "Cannot renew overdue books"
  And the due date should remain unchanged

Scenario: Cannot renew book at renewal limit
  Given I am a library member
  And I have borrowed a book
  And I have already renewed it 2 times
  When I attempt to renew the book again
  Then I should see "Maximum renewals reached"
  And the due date should remain unchanged
```

---

### 6. Common Pitfalls and How to Avoid Them (10 minutes)

#### 6.1 Anti-Patterns to Avoid

### 1. The Conjunction Anti-Pattern

```gherkin
# Bad - Multiple actions in one step
When I log in and navigate to settings and update my profile

# Good - Separate steps
When I log in
And I navigate to settings  
And I update my profile
```

### 2. The Implementation Details Anti-Pattern

```gherkin
# Bad - Shows how it's implemented
Given the database contains user record with id=123
When I call the updateUser API
Then the user table should be updated

# Good - Shows what the user experiences
Given I am user "John Smith"
When I update my profile information
Then my changes should be saved
```

### 3. The Everything and the Kitchen Sink Anti-Pattern

```gherkin
# Bad - Testing too much in one scenario
Scenario: Complete e-commerce workflow
  Given I visit the homepage
  When I search for products
  And I view product details
  And I add items to cart
  And I update quantities
  And I apply coupon codes
  And I proceed to checkout
  And I enter shipping information
  And I enter payment details
  And I place the order
  And I receive confirmation
  Then everything should work perfectly

# Good - Multiple focused scenarios
Scenario: Search for products
Scenario: Add product to cart  
Scenario: Apply discount coupon
Scenario: Complete checkout process
```

#### 6.2 Quality Checklist

Before finalizing scenarios, ask:

- [ ] Can a business stakeholder understand this scenario?
- [ ] Does it test one specific behavior?
- [ ] Is it written from the user's perspective?
- [ ] Does it focus on business outcomes, not implementation?
- [ ] Could this scenario be automated by a developer?
- [ ] Does it provide clear acceptance criteria?

---

### 7. Wrap-up and Next Steps (5 minutes)

#### 7.1 Key Takeaways

- BDD is about collaboration and shared understanding first
- Three Amigos bring different valuable perspectives  
- Gherkin scenarios should be readable by business stakeholders
- Focus on business behavior, not technical implementation
- Quality scenarios require practice and iteration

#### 7.2 Homework Assignment

### Choose a user story from your current project and

1. Write 2-3 Gherkin scenarios for it
1. Use the quality checklist to review your scenarios
1. Share with a teammate and get feedback
1. Be prepared to discuss in Session 2

#### 7.3 Preview of Session 2

Next session we'll cover:

- Advanced Gherkin features (Background, Scenario Outlines)
- Scenario quality patterns and anti-patterns
- Maintaining scenarios as living documentation
- Hands-on scenario improvement workshop

## 📚 Resources for Further Learning

### Essential Reading

- [Reqnroll Gherkin Documentation](https://docs.reqnroll.net/latest/gherkin/index.html)

- [BDD Introduction - Dan North](https://dannorth.net/blog/introducing-bdd/)

### Tools to Explore

- **Text Editors with Gherkin support:** VS Code with Cucumber extension
- **BDD Tools:** Reqnroll (.NET), Cucumber (Java/Ruby/JavaScript)

### Practice Exercises

- Try converting user stories from your project backlog
- Review scenarios in the [examples folder](./examples/)
- Practice the Three Amigos approach with your team

## 🤝 Getting Help

- **During Session:** Ask questions anytime
- **Between Sessions:** Contact instructor or use team chat
- **Resources:** Check the [troubleshooting guide](../../resources/troubleshooting.md)

---

**Next Session:** [Session 2: Writing Effective Scenarios](../session-2/README.md)
