# Week 2: Prompting Foundations 🎯

**Duration:** 2 hours  
**Format:** In-person or virtual  
**Audience:** RealManage cross-functional team  
**Prerequisites:** Completed Week 1 Setup & Orientation

## 🎯 Learning Objectives

By the end of this session, participants will be able to:
- ✅ Craft clear, specific prompts that generate high-quality code
- ✅ Use XML tags and markdown for structured inputs
- ✅ Build and maintain a RealManage prompt library
- ✅ Master few-shot prompting for consistency
- ✅ Generate code with 95% test coverage requirements
- ✅ Iterate and refine prompts based on output quality

## 📋 Pre-Session Checklist

### For Participants
- [ ] Claude Code installed and authenticated
- [ ] Completed Week 1 homework
- [ ] Can run `claude` command successfully
- [ ] Have a project ready to practice with
- [ ] Reviewed [Prompt Engineering Guide](https://docs.anthropic.com/en/docs/build-with-claude/prompt-engineering)

### For Instructors
- [ ] Test Prompt Lab example app
- [ ] Prepare before/after prompt examples
- [ ] Have real RealManage code snippets ready
- [ ] Test all interactive exercises
- [ ] Monitor `#dx-training` Slack channel

## 📚 Session Plan

### Part 1: Prompt Engineering Primer (25 min)

#### 1.1 Welcome & Context (5 min)
```markdown
"Last week we learned how to use Claude Code. This week, we're learning HOW to talk 
to Claude effectively. The difference between a vague prompt and a specific one can be 
the difference between debugging for hours and getting working code instantly. By the 
end of today, you'll write prompts that consistently generate production-ready code 
with 95% test coverage."
```

#### 1.2 The Anatomy of an Effective Prompt (10 min)

**The Five Elements:**
1. **Action** - What you want Claude to do (create, fix, refactor, explain)
2. **Context** - System, domain, and technical environment
3. **Specifics** - Exact requirements, constraints, patterns
4. **Structure** - How to organize complex requests
5. **Validation** - Testing and quality requirements

**Live Demo - Transform a Vague Prompt:**
```
# Start with vague prompt:
> Write a payment service

# Gather requirements:
- What language? C# .NET 9
- What pattern? Repository
- What operations? Process payments, calculate fees
- Testing? Yes, 95% coverage
- Domain rules? 30-day grace, 10% compound interest

# Final specific prompt:
> Create a C# PaymentService class for processing HOA dues with:
  - .NET 9, nullable reference types enabled
  - Repository pattern with IPaymentRepository interface
  - ProcessPayment method (async) for monthly/quarterly/annual dues
  - CalculateLateFee method using 10% monthly compound interest after 30-day grace period
  - xUnit tests with FluentAssertions and Moq, 95% coverage minimum
  - PCI compliance comments for sensitive data handling
  - XML documentation for all public methods
```

#### 1.3 Understanding Claude's Strengths (10 min)

**What Claude Code Excels At:**
- Reading entire codebases for context
- Following specific patterns and conventions
- Generating comprehensive tests
- Explaining complex code
- Refactoring with specific goals

**Current Limitations:**
- Can't see UI/visual output directly (NOTE: can see image files)
- Limited to text-based interactions
- May need iteration for complex tasks
- Token limits for very large codebases

### Part 2: Structured Prompts (15 min)

#### 2.1 Using XML Tags for Clarity (7 min)

**Why XML Tags Help:**
- Clear section boundaries
- Easy to parse visually
- Prevents ambiguity
- Supports nested structures

**Example with XML:**
```xml
Create a violation notification system with these specifications:

<requirements>
  <functional>
    - Send email notifications for new violations
    - Include violation details and fine amount
    - Support bulk sending for multiple violations
  </functional>
  
  <technical>
    - C# .NET 9 with async/await
    - Use IEmailService interface
    - Queue messages with Azure Service Bus
    - Retry failed sends with Polly
  </technical>
  
  <testing>
    - xUnit tests with 95% coverage
    - Mock external dependencies
    - Test retry logic and failures
  </testing>
</requirements>

<constraints>
  - No PII in logs
  - Rate limit: 100 emails/minute
  - Template must be configurable
</constraints>
```

#### 2.2 Markdown for Readability (8 min)

**When to Use Markdown:**
```markdown
## Refactor the PaymentProcessor class

### Goals
1. Extract payment validation to separate validator
2. Add async/await to all I/O operations
3. Implement retry logic for transient failures

### Requirements
- **Maintain backward compatibility**
- **Add comprehensive logging**
- **Preserve all existing tests**

### Definition of Done
- [ ] All existing tests pass
- [ ] New tests for retry logic
- [ ] 95% code coverage maintained
- [ ] No breaking changes to public API
```

**Live Exercise:**
Have participants convert a paragraph prompt into structured format.

### Part 3: Hands-On Prompt Workshop (60 min)

#### 3.1 Set Up Your Prompt Lab (5 min)
```bash
# Copy example to sandbox
cd courses/ai-101-claude-code/sessions/week-2
cp -r example sandbox
cd sandbox

# Build and run the Prompt Lab
dotnet build
dotnet run

# Explore the menu options
```

#### 3.2 Individual Exercises (20 min)

**Exercise 1: Analyze Your Prompts**
```
# In Prompt Lab, select option 1
# Try these prompts and compare scores:

Vague: "Make a report"

Better: "Generate a monthly financial report"

Best: "Create a C# FinancialReportService that generates monthly P&L statements 
for HOAs including income from dues, expenses by category, and reserve fund 
status. Use Entity Framework Core 9 to query SQL Server, return data as 
ReportDto, include PDF export via QuestPDF, and write xUnit tests with 95% coverage."
```

**Exercise 2: Template Practice**
```
# Select option 3: Use a template
# Choose "HOA Violation Handler"
# Fill in:
- ACTION: "escalation"
- VIOLATION_TYPE: "landscaping"
- RECIPIENTS: "resident, board president"
```

**Exercise 3: Learn from Examples**
```
# Select option 4: View HOA prompt examples
# Review each category
# Note the patterns in "good" examples
```

#### 3.3 Pair Programming Exercise (25 min)

**Scenario: Board Meeting Report Generator**

Work in pairs. One person is the "prompt writer", the other is "Claude" (reviewer).

**Round 1 (10 min):**
- Prompt writer: Create initial prompt for board meeting report generator
- "Claude": Point out what's missing or unclear
- Refine together

**Round 2 (10 min):**
- Switch roles
- New scenario: Resident payment portal
- Apply lessons from Round 1

**Share Out (5 min):**
- Each pair shares their best prompt
- Discuss what made it effective

#### 3.4 Real RealManage Task (10 min)

**Live Coding with the Class:**
```
# Start Claude in your sandbox
claude

# Use this refined prompt:
> <task>
  Create a ResidentPortalController for our HOA management system
  </task>
  
  <requirements>
  - ASP.NET Core Web API (.NET 9)
  - Endpoints: GET account balance, GET violations, GET payment history, POST payment
  - Use existing ResidentService and PaymentService
  - Authorize with Azure AD B2C, resident role only
  - </requirements>
  
  <validation>
  - FluentValidation for request models
  - Return appropriate HTTP status codes
  - Handle all error cases gracefully
  </validation>
  
  <testing>
  - xUnit integration tests using WebApplicationFactory
  - Test authorization, validation, and error cases
  - 95% code coverage minimum
  - Use FluentAssertions for all assertions
  </testing>
```

**Observe:**
- How Claude follows the structure
- The completeness of the generated code
- Test coverage included
- Proper error handling

### Part 4: Build Your Prompt Library (15 min)

#### 4.1 Creating a Team Library (7 min)

**In CLAUDE.md or separate prompt file:**
```markdown
# RealManage Prompt Library

## Frequently Used Prompts

### 1. New Service Class
Create a C# service class for {FEATURE} that:
- Uses .NET 9 with nullable reference types
- Follows repository pattern with I{NAME}Repository
- Implements async/await for all I/O operations
- Includes xUnit tests with 95% coverage
- Uses FluentAssertions and Moq for testing
- Follows RealManage naming conventions
- Includes XML documentation

### 2. Fix With TDD
There's a bug in {CLASS}.{METHOD} where {DESCRIPTION}.
- First, write a failing test that reproduces the bug
- Fix the implementation to make the test pass
- Ensure all existing tests still pass
- Add edge case tests to prevent regression
- Document the fix with a comment

### 3. Angular Component
Create an Angular 17 standalone component named {NAME}Component that:
- Uses OnPush change detection
- Implements {FEATURE}
- Uses RxJS for state management
- Includes Jasmine tests with 95% coverage
- Follows RealManage UI patterns
- Is mobile-responsive
```

#### 4.2 Personal Customization (8 min)

**Activity: Create Your Top 3 Prompts**

Have each participant create prompts for their most common tasks:

**Support Team Example:**
```
Analyze these support tickets and:
1. Identify common issues
2. Suggest root causes
3. Draft a knowledge base article for the top issue
4. Include step-by-step resolution
```

**Product Manager Example:**
```
Generate user stories from this feature request:
<paste requirements>

Format as:
- Title
- As a... I want... So that...
- Acceptance criteria
- Test scenarios
- Effort estimate (S/M/L)
```

**QA Engineer Example:**
```
Create comprehensive test cases for {FEATURE} including:
- Happy path scenarios
- Edge cases and boundary conditions
- Error scenarios
- Performance considerations
- Test data requirements
- Expected results for each case
```

### Part 5: Reflection & Practice (10 min)

#### 5.1 Key Takeaways (5 min)

**The Prompt Quality Spectrum:**
```
Vague ←────────────────────→ Specific
"Fix bug"                    "Fix the scoring bug in PromptAnalyzer.
                            Analyze method (line 25) where all 
                            factors get equal weight. Prioritize 
                            specificity and context higher than 
                            other factors. Include xUnit test."
```

**The Three Rules of Effective Prompting:**
1. **Be Specific** - Include all technical requirements
2. **Provide Context** - Domain rules and constraints
3. **Request Validation** - Always ask for tests

**Structure Decision Tree:**
```
Is your prompt complex?
├─ No → Simple text is fine
└─ Yes → Has multiple parts?
    ├─ No → Use bullet points
    └─ Yes → Use XML tags or markdown sections
```

#### 5.2 Quick Practice (5 min)

**Lightning Round - Fix These Prompts:**

Participants fix these in chat/verbally:

1. "Make login work" → ?
2. "Add tests" → ?
3. "Fix the error" → ?
4. "Make it faster" → ?

**Improved versions:**
1. "Implement JWT authentication for the resident portal API using Azure AD B2C with role-based authorization"
2. "Add xUnit tests for PromptAnalyzer.Analyze achieving 95% coverage, testing all scoring scenarios and edge cases"
3. "Fix the NullReferenceException in TemplateManager.FillTemplate when template name not found. Return empty template instead"
4. "Optimize the PromptAnalyzer by caching common action verbs and using compiled regex patterns for better performance"

## 🎯 Key Takeaways

### Prompt Engineering Principles
1. **Specificity beats brevity** - Longer, detailed prompts get better results
2. **Context is crucial** - Include domain knowledge and technical environment
3. **Structure aids comprehension** - Use XML tags or markdown for complex prompts
4. **Testing is non-negotiable** - Always request 95% coverage explicitly
5. **Iteration improves quality** - Refine prompts based on output

### Quick Reference Card
```
PROMPT FORMULA:
[Action Verb] + [What] + [Specific Requirements] + [Constraints] + [Testing]

EXAMPLE:
Create + PaymentService + using .NET 9, async, repository pattern + 
PCI compliance + with xUnit tests, 95% coverage

XML TEMPLATE:
<task>What you want</task>
<requirements>Technical specs</requirements>
<constraints>Limitations</constraints>
<testing>Test requirements</testing>
```

### Common Pitfalls to Avoid
- ❌ Assuming Claude knows your context
- ❌ Using pronouns without clear antecedents
- ❌ Forgetting to specify test requirements
- ❌ Being vague about technologies or versions
- ❌ Not including domain-specific rules

## 📝 Homework (Before Week 3)

### Required Tasks:
1. ✅ Create a CLAUDE.md with 5+ prompt templates for your role
2. ✅ Use the Prompt Lab to score and improve 3 of your common prompts
3. ✅ Generate one complete feature with tests using a structured prompt
4. ✅ Share your best prompt in `#dx-training` with before/after comparison
5. ✅ Fix the scoring bug in PromptAnalyzer.cs

### Stretch Goals:
1. 🎯 Build a prompt for a complex multi-step task
2. 🎯 Create a prompt that generates an entire microservice
3. 🎯 Add a new category to HoaPromptExamples.cs

### Skill Check:
Without looking at examples, write a prompt that:
```
Generates a complete audit logging system for HOA transactions including:
- Service class with async methods
- Database models with EF Core
- API endpoints with authorization
- 95% test coverage
- Compliance with SOC 2 requirements
```

## 🔗 Resources

### Official Documentation
- [Prompt Engineering Overview](https://docs.anthropic.com/en/docs/build-with-claude/prompt-engineering/overview)
- [Use XML Tags](https://docs.anthropic.com/en/docs/build-with-claude/prompt-engineering/use-xml-tags)
- [Be Clear and Direct](https://docs.anthropic.com/en/docs/build-with-claude/prompt-engineering/be-clear-and-direct)
- [Use Examples (Multishot)](https://docs.anthropic.com/en/docs/build-with-claude/prompt-engineering/multishot-prompting)
- [Let Claude Think](https://docs.anthropic.com/en/docs/build-with-claude/prompt-engineering/chain-of-thought)

### RealManage Resources
- [Week 2 Prompt Lab](./example/) - Interactive prompt training tool
- [Prompt Library](../../resources/prompt-library.md) - Curated RealManage prompts
- [Getting Started Prompts](../../resources/prompts/getting-started.md) - Beginner-friendly examples
- Slack: `#dx-training` for prompt help

### Additional Reading
- [Anthropic's Prompt Engineering Tutorial](https://docs.anthropic.com/en/docs/build-with-claude/prompt-engineering)
- [XML vs JSON for Structured Prompts](https://docs.anthropic.com/en/docs/build-with-claude/prompt-engineering/use-xml-tags)
- [Few-shot Prompting Techniques](https://docs.anthropic.com/en/docs/build-with-claude/prompt-engineering/multishot-prompting)

## 📊 Success Metrics

### You're ready for Week 3 when you can:
- [ ] Write prompts that consistently generate working code
- [ ] Use XML tags or markdown to structure complex requests
- [ ] Include all necessary context without Claude asking for clarification
- [ ] Get 95% test coverage in generated code without reminding
- [ ] Score 75+ on the Prompt Analyzer for your prompts

### Red Flags (seek help if):
- [ ] Claude frequently asks for clarification
- [ ] Generated code has syntax errors
- [ ] Tests are missing or have low coverage
- [ ] You're using more than 3 iterations to get desired output

## 🚀 Next Week Preview

**Week 3: Plan Mode & Exploration**
- Learn to break down complex tasks
- Master the "think deeply" approach
- Explore codebases before making changes
- Make architectural decisions with AI assistance

**Pre-work:** Think of a complex feature that would benefit from planning

---

## Instructor Notes

### Common Issues & Solutions

**"My prompts are too long"**
- Reassure that comprehensive > concise
- Show token usage with `/cost`
- Demonstrate that one good prompt beats multiple iterations

**"Claude isn't following my structure"**
- Check for XML tag typos
- Ensure closing tags match opening tags
- Try markdown if XML isn't working

**"Generated tests are too simple"**
- Explicitly request edge cases
- Specify coverage percentage
- Provide example test scenarios

### Time Management Tips
- Part 1: Critical foundation - don't rush
- Part 3: Heart of the session - full 60 min
- If running late: Skip Part 4.2 (personal customization)
- Always leave 5 min for Q&A

### Engagement Strategies
- Use real code from RealManage projects
- Have participants share prompts in chat
- Celebrate "aha moments" when prompts improve
- Do live demonstrations with participant suggestions

### Assessment
Quick check at end:
1. Show a vague prompt - have them improve it
2. What are the 5 elements of an effective prompt?
3. When would you use XML tags vs markdown?
4. What's the minimum test coverage requirement?

---

*End of Week 2 Session Plan*  
*Next Session: Week 3 - Plan Mode & Exploration*