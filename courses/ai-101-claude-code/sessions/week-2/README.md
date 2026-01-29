# Week 2: Prompting Foundations 🎯

**Duration:** 2 hours  
**Format:** In-person or virtual  
**Audience:** RealManage cross-functional team  
**Prerequisites:** Completed Week 1 Setup & Orientation

## 🎯 Learning Objectives

By the end of this session, participants will be able to:

- ✅ Communicate clearly with Claude using natural language
- ✅ Know when structure helps (and when it doesn't)
- ✅ Build a personal prompting style that works for them
- ✅ Generate code with 80-90% test coverage consistently
- ✅ Iterate and refine prompts based on Claude's feedback
- ✅ Recognize that clarity beats formatting every time

## 📋 Pre-Session Checklist

### For Participants

- [ ] Claude Code installed and authenticated
- [ ] Completed Week 1 homework
- [ ] Can run `claude` command successfully
- [ ] Have a project ready to practice with
- [ ] Reviewed [Prompt Engineering Guide](https://docs.anthropic.com/en/docs/build-with-claude/prompt-engineering)

## 📚 Session Plan

### Part 1: Prompt Engineering Primer (25 min)

#### 1.1 Welcome & Reality Check (5 min)

```markdown
"Let me share a secret: I've been using Claude successfully for months, and I rarely 
use Markdown or XML tags. Most of my prompts are just clear, conversational 
requests. Today, we're learning what ACTUALLY works - clear communication, not 
complex formatting. By the end, you'll write prompts in YOUR style that consistently 
get great results with 80-90% test coverage."
```

#### 1.2 The Anatomy of an Effective Prompt (10 min)

**What Actually Matters:**

1. **Clear Intent** - Say what you want directly
2. **Specific Context** - Include relevant details (file paths, tech stack)
3. **Domain Knowledge** - Mention business rules when relevant  
4. **Test Requirements** - Always specify 80-90% coverage
5. **Natural Flow** - Write like you're explaining to a colleague

**Real Examples - How Successful Users Actually Prompt:**

```
# Vague (doesn't work):
> Write a payment service

# Natural conversation (works great!):
> I need a payment service for our HOA system. It should handle monthly, quarterly, 
  and annual dues payments. We're using C# with .NET 10 and the repository pattern.
  Important: after a 30-day grace period, we charge 10% monthly compound interest 
  on late payments. Please include async methods and comprehensive xUnit tests 
  with 80-90% coverage.

# Notice: No Markdown headers, no XML tags, just clear communication!

# Claude might ask: "Should I include refund functionality?"
# You respond: "Yes, good catch! Add a refund method with audit logging."

# This iterative, conversational approach often yields better results than 
# trying to specify everything upfront in a complex structure.
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

### Part 2: When Structure Actually Helps (And When It Doesn't) (15 min)

#### 2.1 The Structure Spectrum (10 min)

**Most prompts work fine with natural language:**

```
Natural conversation → Bullet points → Markdown → XML
        ↑
   90% of your prompts should be here!
```

**When structure genuinely helps:**

**Simple Lists (for multiple related items):**

```
Can you help me with these HOA reports:
- Monthly financial summary with income/expenses
- Delinquency report showing accounts 30+ days overdue  
- Violation status report grouped by type
All should export to PDF and include 80-90% test coverage.
```

**Markdown Headers (for complex multi-part requests):**

```markdown
## Refactor our payment system

### Current Issues
The PaymentProcessor is doing too much - it handles payments, refunds, 
and reporting all in one class.

### Goals
Split into separate services with single responsibilities
Keep all existing tests passing
Add new tests for the refactored services (80-90% coverage)

### Constraints
Can't break existing API contracts
Need to deploy by Friday
```

#### 2.2 Examples of What Doesn't Need Structure (5 min)

**Most requests work great as natural conversation:**

```
# Bug fix:
> The late fee calculation in ViolationService is wrong. It's only multiplying 
  by 1.1 once instead of compounding monthly. Can you fix it and add tests?

# New feature:
> Add a method to ResidentService that returns all residents with outstanding 
  balances over $500. Make it async and include unit tests with 80-90% coverage.

# Code review:
> Can you review the PaymentController class and suggest improvements? 
  I'm particularly concerned about error handling and logging.

# Explanation:
> How does the violation escalation workflow work in our system? I see it 
  mentioned 30/60/90 day intervals but I'm not clear on the implementation.
```

**The XML Myth (Rarely Needed):**

```
I've been using Claude for months and have never used XML. 
Markdown, YAML, JSON or tables work just as well for structured data.
Even complex API specs can be described conversationally!
```

**Live Exercise:**
Try the same request three ways: natural language, bullet points, and structured.
You'll likely find natural language or simple bullets work best!

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
status. Use Entity Framework Core 10 to query SQL Server, return data as 
ReportDto, include PDF export via QuestPDF, and write xUnit tests with 80-90% coverage."
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

#### 3.3 Claude as Your Prompt Engineering Coach (25 min)

**Learn how Claude can help you write better prompts!**

**Exercise 1: Prompt Improvement Workshop (10 min)**

```bash
# Start Claude in your sandbox
claude

# Use Claude to improve your prompts
> I need to create a board meeting report generator. Help me write a better prompt for this task.

# Claude will suggest elements to include. Then try:
> Review this prompt and suggest improvements:
  "Create a board meeting report generator for HOA management"

# Ask for structured format help
> Convert this prompt to use XML tags for clarity:
  [paste your current prompt]

# Get Claude to teach you patterns
> Show me the difference between a vague and specific prompt for creating a payment processing service
```

**Exercise 2: Learning Prompt Patterns (10 min)**

```bash
# Have Claude analyze prompt quality
> Here's my prompt for a resident portal. Score it 1-10 and explain why:
  "Build a web portal where residents can view their HOA account"

# Learn what makes prompts effective
> What are the 5 most important elements to include when prompting for C# service creation?

# Practice with templates
> Create a reusable prompt template for generating Angular components with these variables: [COMPONENT_NAME], [FEATURE], [STATE_MANAGEMENT]

# Compare approaches
> Show me the same prompt written three ways: plain text, with Markdown structure, and with XML tags
```

**Key Discovery Questions to Ask Claude:**

- "What context would help you generate better code?"
- "What technical specifications am I missing?"
- "How could I structure this prompt more clearly?"
- "What test requirements should I include?"

**Share Out (5 min):**

- Share the most surprising thing Claude taught you about prompting
- Post your before/after prompt improvement
- Discuss which Claude suggestions were most helpful

#### 3.4 Real RealManage Task (10 min)

**Live Coding with the Class:**

```
# Start Claude in your sandbox
claude

# Use this refined prompt with Markdown structure:
> ## Create a ResidentPortalController for our HOA management system
  
  ### Requirements
  - ASP.NET Core Web API (.NET 10)
  - Endpoints: GET account balance, GET violations, GET payment history, POST payment
  - Use existing ResidentService and PaymentService
  - Authorize with Azure AD B2C, resident role only
  
  ### Validation
  - FluentValidation for request models
  - Return appropriate HTTP status codes
  - Handle all error cases gracefully
  
  ### Testing
  - xUnit integration tests using WebApplicationFactory
  - Test authorization, validation, and error cases
  - 80-90% code coverage minimum
  - Use FluentAssertions for all assertions
```

**Observe:**

- How Claude follows the structure
- The completeness of the generated code
- Test coverage included
- Proper error handling

### Part 4: Build Your Prompt Library (10 min)

#### 4.1 Creating a Team Library (5 min)

**In CLAUDE.md or separate prompt file:**

```markdown
# RealManage Prompt Library

## Frequently Used Prompts

### 1. New Service Class
Create a C# service class for {FEATURE} that:
- Uses .NET 10 with nullable reference types
- Follows repository pattern with I{NAME}Repository
- Implements async/await for all I/O operations
- Includes xUnit tests with 80-90% coverage
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
- Includes Jasmine tests with 80-90% coverage
- Follows RealManage UI patterns
- Is mobile-responsive
```

#### 4.2 Personal Customization (5 min)

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

**Product Manager Examples:**

*Requirement Refinement (CLEAR Framework):*

```
Help me refine this rough feature idea into a clear spec:
"We need a way for residents to pay their HOA dues online"

Identify:
1. Ambiguities I need to clarify with stakeholders
2. Acceptance criteria (Gherkin format)
3. Edge cases and error scenarios
4. MVP scope recommendation
```

*User Story Generation:*

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

*Implementation Plan Validation:*

```
Review my PRD for this feature and identify:
1. Missing requirements or ambiguities
2. Technical questions I should ask engineering
3. Edge cases not covered
4. Suggested phasing (MVP vs future)
```

> **PM Tip:** See [PM Quick-Start Guide](../../resources/quick-start-pm.md) for the full CLEAR framework and more PM-specific prompts.

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
Vague ←───────────→ Specific
"Fix bug"          "Fix the scoring bug in PromptAnalyzer.
                    Analyze method (line 25) where all 
                    factors get equal weight. Prioritize 
                    specificity and context higher than 
                    other factors. Include xUnit test."
```

**The Real Rules of Effective Prompting:**

1. **Be Clear** - Say what you want in plain language
2. **Be Specific** - Include versions, patterns, coverage requirements
3. **Be Conversational** - Claude responds well to natural dialogue

**When to Add Structure:**

```
Writing your first attempt?
├─ Yes → Just use natural language
└─ No → Did Claude ask for clarification?
    ├─ Yes → Add the missing details conversationally
    └─ No → Is the output wrong?
        ├─ Yes → Try bullets or headers for clarity
        └─ No → You're done! No structure needed.
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
2. "Add xUnit tests for PromptAnalyzer.Analyze achieving 80-90% coverage, testing all scoring scenarios and edge cases"
3. "Fix the NullReferenceException in TemplateManager.FillTemplate when template name not found. Return empty template instead"
4. "Optimize the PromptAnalyzer by caching common action verbs and using compiled regex patterns for better performance"

## 🎯 Key Takeaways

### Prompt Engineering Reality Check

1. **Clarity beats structure** - Natural language often works better than formatted prompts
2. **Conversation beats specification** - Iterative dialogue > trying to specify everything upfront
3. **Context is crucial** - Include domain knowledge and technical requirements
4. **Testing is non-negotiable** - Always mention 80-90% coverage requirement
5. **Your style is valid** - Find what works for your thinking and typing speed

### Quick Reference Card

```
NATURAL LANGUAGE APPROACH:
"I need a [what] that [does what]. We're using [tech stack]. 
Important: [domain rules]. Please include [test requirements]."

REAL EXAMPLE:
"I need a payment service that processes HOA dues. We're using C# .NET 10 
with the repository pattern. Important: 30-day grace period, then 10% 
monthly compound interest. Please include xUnit tests with 80-90% coverage."

ONLY ADD STRUCTURE WHEN:
- You have 5+ distinct requirements → Use bullet points
- You're explaining a complex refactor → Use Markdown sections
- You're specifying an API → Use a table or JSON
- Otherwise → Just talk naturally!
```

### Common Pitfalls to Avoid

- ❌ Assuming Claude knows your context
- ❌ Using pronouns without clear antecedents
- ❌ Forgetting to specify test requirements
- ❌ Being vague about technologies or versions
- ❌ Not including domain-specific rules

## 📝 Homework (Before Week 3)

### Required Tasks

1. ✅ Create a CLAUDE.md with 5+ prompt templates for your role
2. ✅ Use the Prompt Lab to score and improve 3 of your common prompts
3. ✅ Generate one complete feature with tests using a structured prompt
4. ✅ Share your best prompt in `#ai-exchange` with before/after comparison
5. ✅ Fix the scoring bug in PromptAnalyzer.cs

### Stretch Goals

1. 🎯 Build a prompt for a complex multi-step task
2. 🎯 Create a prompt that generates an entire microservice
3. 🎯 Add a new category to HoaPromptExamples.cs

### Skill Check

Without looking at examples, write a prompt that:

```
Generates a complete audit logging system for HOA transactions including:
- Service class with async methods
- Database models with EF Core
- API endpoints with authorization
- 80-90% test coverage
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
- Slack: `#ai-exchange` for prompt help

### Additional Reading

- [Anthropic's Prompt Engineering Tutorial](https://docs.anthropic.com/en/docs/build-with-claude/prompt-engineering)
- [XML vs JSON for Structured Prompts](https://docs.anthropic.com/en/docs/build-with-claude/prompt-engineering/use-xml-tags)
- [Few-shot Prompting Techniques](https://docs.anthropic.com/en/docs/build-with-claude/prompt-engineering/multishot-prompting)

## 📊 Success Metrics

### You're ready for Week 3 when you can

- [ ] Write prompts that consistently generate working code
- [ ] Use Markdown or bullet points to structure complex requests
- [ ] Include all necessary context without Claude asking for clarification
- [ ] Get 80-90% test coverage in generated code without reminding
- [ ] Score 75+ on the Prompt Analyzer for your prompts

### Red Flags (seek help if)

- [ ] Claude frequently asks for clarification
- [ ] Generated code has syntax errors
- [ ] Tests are missing or have low coverage
- [ ] You're using more than 3 iterations to get desired output

## 🚀 Next Week Preview

**Week 3: Tactical Planning & Code Review**

- Use plan mode as a tactical thinking partner
- Iterate on plans in real-time before executing
- Organize code review fixes systematically
- Switch to Opus model for deep code analysis

**Pre-work:** Think of a complex feature that would benefit from planning

---

*End of Week 2 Session Plan*
*Next Session: Week 3 - Plan Mode & Exploration*
