# Week 3: Plan Mode & Breaking Down Complex Tasks 🗺️

**Duration:** 2 hours  
**Format:** In-person or virtual  
**Audience:** RealManage cross-functional team  
**Prerequisites:** Completed Weeks 1-2

## 🎯 Learning Objectives

By the end of this session, participants will be able to:
- ✅ Know when to ask Claude to plan vs dive straight into coding
- ✅ Use natural conversation to explore codebases effectively
- ✅ Break down complex features into manageable chunks
- ✅ Apply the "Explore → Plan → Implement → Verify" workflow
- ✅ Recognize when overthinking slows you down
- ✅ Balance planning with action

## 📋 Pre-Session Checklist

### For Participants
- [ ] Claude Code working smoothly from Weeks 1-2
- [ ] A complex feature idea you'd like to plan
- [ ] Access to a RealManage codebase (or use our example)
- [ ] Comfortable with natural language prompting

### For Instructors
- [ ] Test Plan Explorer example app
- [ ] Have a real complex feature to demonstrate
- [ ] Prepare examples of good vs over-planning
- [ ] Ready to show actual planning conversations

## 📚 Session Plan

### Part 1: When Planning Actually Helps (20 min)

#### 1.1 The Reality of Plan Mode (5 min)
```markdown
"Here's the truth: Most tasks don't need elaborate planning. Claude can help you 
think through complex problems, but often the best approach is to start coding 
and iterate. Today we'll learn when planning truly adds value and when it's just 
procrastination disguised as preparation."
```

#### 1.2 When to Plan vs When to Code (10 min)

**Just Start Coding:**
- Single file changes
- Bug fixes with clear solutions
- Adding a simple method or endpoint
- Refactoring with obvious goals
- Features you've built before

**Worth Planning First:**
- Multi-file architectural changes
- Features touching 5+ components
- Database schema migrations
- Breaking changes to APIs
- Unfamiliar technology decisions

**Real Example:**
```
# Over-planning (don't do this):
> I need to add a button to export a report to PDF. Let's create a comprehensive 
  plan with all edge cases, error handling, and architectural considerations.

# Just right:
> I need to add PDF export to our financial reports. The report data is already 
  in FinancialService. Can you add a PDF export method using QuestPDF?

# Needs planning:
> We need to refactor our entire payment system from synchronous to event-driven 
  using Azure Service Bus. This affects 12 services and our database schema.
```

#### 1.3 Claude's Thinking Modes - Myth vs Reality (5 min)

**What you might have heard:**
- "Use 'think hard' for complex problems"
- "Ultra think for architecture decisions"
- "Extended thinking prevents mistakes"

**The reality:**
- Most problems don't need special thinking modes
- Conversation and iteration work better than upfront thinking
- Extended thinking can lead to overthinking
- Real planning happens through dialogue, not monologue

### Part 2: Effective Exploration (25 min)

#### 2.1 Exploring a Codebase Naturally (10 min)

**Don't do this:**
```
> Analyze the entire codebase and create a comprehensive architectural document 
  with all patterns, dependencies, and potential improvements.
```

**Do this instead:**
```
> I'm new to this HOA management system. Can you help me understand how 
  violation tracking works? I see ViolationService.cs - what does it do?

> Where do we calculate late fees for HOA dues?

> How does the payment processing connect to our accounting system?
```

**Key insight:** Ask specific questions as you need answers, not everything upfront.

#### 2.2 The Explore → Plan → Implement → Verify Workflow (15 min)

**Explore (2-5 min):**
```
> Show me how the current notification system works
> What files handle email sending?
> Are we using any queuing system?
```

**Plan (5-10 min):**
```
> I need to add SMS notifications alongside emails. Based on what you've shown me,
  what's the best approach? Should we:
  1. Add SMS to the existing EmailService?
  2. Create a new SmsService?
  3. Make a generic NotificationService?
```

**Implement (rest of time):**
```
> Let's go with option 3. Create a NotificationService that can handle both 
  email and SMS. Include tests with 95% coverage.
```

**Verify (ongoing):**
```
> Run the tests
> Does this maintain backward compatibility?
> Show me what changed
```

### Part 3: Hands-On Planning Workshop (60 min)

#### 3.1 Set Up Your Planning Lab (5 min)
```bash
# Copy example to sandbox
cd courses/ai-101-claude-code/sessions/week-3
cp -r example sandbox
cd sandbox

# Start Claude
claude
```

#### 3.2 Exercise 1: Feature Exploration (15 min)

**Scenario:** You need to add a "payment plan" feature for residents with overdue HOA fees.

**Practice natural exploration:**
```
# Start with understanding
> How do we currently handle overdue payments?
> Show me the payment processing logic
> What happens when someone can't pay their full balance?

# Ask about constraints
> Are there any legal requirements for payment plans in HOAs?
> What's our current database schema for payments?

# Don't over-explore
Stop when you have enough info to start planning!
```

#### 3.3 Exercise 2: Right-Sized Planning (20 min)

**Compare these approaches:**

**Over-planned approach:**
```
> Create a comprehensive 20-step plan for implementing payment plans including:
  - All database migrations
  - Every edge case
  - Full error handling strategy
  - Complete testing strategy
  - Deployment plan
  - Rollback procedures
  [This takes 30 minutes and you haven't written any code]
```

**Right-sized approach:**
```
> Based on what we discussed, here's what I need:
  - A PaymentPlan model with installments
  - Method to create plans with configurable terms
  - Calculate installment amounts
  - Track payments against installments
  Let's start with the model and basic creation logic, then iterate.
```

#### 3.4 Exercise 3: Real Feature Implementation (20 min)

**Pick one of these RealManage features:**

1. **Board Election System**
   - Explore: How are board members currently tracked?
   - Plan: Online voting with eligibility rules
   - Implement: Start with the data model

2. **Maintenance Request Portal**
   - Explore: Current request handling process
   - Plan: Resident submission and tracking
   - Implement: Basic submission endpoint

3. **Reserve Fund Calculator**
   - Explore: How do we track reserve funds?
   - Plan: Projection and contribution calculator
   - Implement: Core calculation logic

**Use this pattern:**
```
> [Explore] How does the current X work?
> [Plan] I want to add Y. What's the simplest approach?
> [Implement] Let's start with Z. Include tests.
> [Verify] Does this work with our existing system?
```

### Part 4: Common Anti-Patterns to Avoid (15 min)

#### 4.1 Analysis Paralysis (5 min)

**Signs you're overthinking:**
- Spending 30+ minutes planning a 1-hour task
- Creating diagrams for simple features
- Listing every possible edge case upfront
- Asking for multiple architectural options repeatedly

**The cure:**
```
> Let's just start with a basic implementation and refine it
```

#### 4.2 The "Boil the Ocean" Trap (5 min)

**Bad:**
```
> Let's redesign the entire payment system to be more scalable, implement CQRS,
  add event sourcing, migrate to microservices, and integrate with 5 payment gateways.
```

**Good:**
```
> Our payment processing is slow. Let's add async processing for the 
  payment confirmation step and see if that helps.
```

#### 4.3 Premature Optimization (5 min)

**Don't start with:**
```
> Plan a highly optimized, distributed, cached, horizontally scalable solution
```

**Start with:**
```
> Make it work, make it right, then make it fast (if needed)
```

### Part 5: Reflection & Best Practices (10 min)

#### 5.1 Key Takeaways (5 min)

**The Planning Sweet Spot:**
```
Too Little ←────────→ Too Much
Jump in blind        Analysis paralysis
     ↑
 Most tasks should be here
 (5-10 min exploration)
```

**Remember:**
1. Conversation beats monologue planning
2. Start simple, iterate quickly
3. Most features don't need elaborate plans
4. Explore only what you need to know now
5. Implementation teaches you more than planning

#### 5.2 Quick Practice (5 min)

**Which of these needs planning?**
1. Add a new field to the Resident model → _Just do it_
2. Replace our database from SQL to NoSQL → _Definitely plan_
3. Add email validation to a form → _Just do it_
4. Implement SSO across all our apps → _Plan it_
5. Fix a calculation bug → _Just do it_
6. Add real-time notifications → _Light planning_

## 🎯 Key Takeaways

### Planning Best Practices
1. **Time-box planning** - 10 minutes max for most features
2. **Start coding sooner** - Implementation reveals unknowns
3. **Iterate over specify** - Rough plan + quick implementation > perfect plan
4. **Ask as you go** - Don't front-load all questions
5. **Trust Claude's suggestions** - But verify with implementation

### Quick Reference Card
```
EXPLORATION PATTERN:
"How does [current feature] work?"
"Where is [specific logic] implemented?"
"What happens when [scenario]?"

PLANNING PATTERN:
"I need to [goal]. Based on what we've seen, what's the simplest approach?"
"Should I [option A] or [option B]?"
"Let's start with [specific piece] and iterate"

ANTI-PATTERNS TO AVOID:
❌ "Create a comprehensive plan for..."
❌ "Analyze all possible approaches..."
❌ "Design the perfect architecture..."
✅ "Let's start simple and improve"
```

## 📝 Homework (Before Week 4)

### Required Tasks:
1. ✅ Take a complex feature from your backlog
2. ✅ Practice the Explore → Plan → Implement workflow
3. ✅ Time how long you spend planning vs coding
4. ✅ Share your experience in `#dx-training`
5. ✅ Identify one time you over-planned this week

### Stretch Goals:
1. 🎯 Refactor something without any planning - see what happens
2. 🎯 Try planning a feature in under 5 minutes
3. 🎯 Use conversation instead of "think hard" mode

### Reflection Questions:
- When did planning help you this week?
- When did it slow you down?
- What's your personal planning sweet spot?

## 🔗 Resources

### Official Documentation
- [Claude Code Best Practices](https://www.anthropic.com/engineering/claude-code-best-practices)
- [Chain of Thought Prompting](https://docs.anthropic.com/en/docs/build-with-claude/prompt-engineering/chain-of-thought)
- [Let Claude Think](https://docs.anthropic.com/en/docs/build-with-claude/prompt-engineering/let-claude-think)

### RealManage Resources
- [Week 3 Plan Explorer](./example/) - Practice planning tool
- [Architecture Decisions](../../resources/architecture-guide.md)
- Slack: `#dx-training` for planning help

### Additional Reading
- [Agile Planning Principles](https://agilemanifesto.org/principles.html)
- [YAGNI - You Aren't Gonna Need It](https://martinfowler.com/bliki/Yagni.html)
- [Avoiding Analysis Paralysis](https://en.wikipedia.org/wiki/Analysis_paralysis)

## 📊 Success Metrics

### You're ready for Week 4 when you can:
- [ ] Identify tasks that need planning vs immediate coding
- [ ] Explore a codebase with targeted questions
- [ ] Create a simple plan in under 10 minutes
- [ ] Start implementing without over-specifying
- [ ] Recognize when you're overthinking

### Red Flags (seek help if):
- [ ] Spending 30+ minutes planning simple features
- [ ] Creating detailed diagrams for every change
- [ ] Never starting implementation
- [ ] Feeling paralyzed by options

## 🚀 Next Week Preview

**Week 4: Test-Driven Development with Claude**
- Write tests FIRST (Red phase)
- Let Claude implement to pass tests (Green phase)
- Refactor while keeping tests green
- Achieve 95% coverage naturally

**Pre-work:** Think about how you currently write tests

---

## Instructor Notes

### Common Issues & Solutions

**"How detailed should my plan be?"**
- If you can't implement from it in 5 minutes, it's too vague
- If it takes 20 minutes to write, it's too detailed
- Aim for bullet points, not paragraphs

**"Claude keeps suggesting complex architectures"**
- Be explicit: "What's the SIMPLEST approach?"
- Add constraints: "Using our existing patterns"
- Time-box: "We have 2 hours to implement this"

**"I don't know what questions to ask"**
- Start with: "How does this currently work?"
- Then: "What files are involved?"
- Finally: "What's the simplest way to add X?"

### Time Management Tips
- Part 1 & 2: Foundation - keep conceptual
- Part 3: Heart of the session - full hands-on
- Part 4: Critical - address bad habits early
- Always leave time for Q&A

### Engagement Strategies
- Share your own over-planning disasters
- Celebrate quick wins over perfect plans
- Show real planning conversations
- Time-box exercises strictly

### Assessment
Quick check at end:
1. Feature X needs 5 files changed - plan or code?
2. How long should planning take for a 2-hour task?
3. What's better: perfect plan or quick iteration?
4. When is "think hard" actually useful?

---

*End of Week 3 Session Plan*  
*Next Session: Week 4 - Test-Driven Development with Claude*