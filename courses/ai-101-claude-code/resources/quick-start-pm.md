# Quick Start: Product Manager Track

You define what gets built. You want Claude Code to help you communicate with developers and understand AI-assisted development. Here's your focused path.

**Estimated Time:** 6-8 hours total

---

## Your Learning Path

```mermaid
graph LR
    subgraph "Core PM Weeks (6 hrs)"
        W0[Week 0<br>AI Foundations<br>45 min] -.->|optional| W1[Week 1<br>Setup<br>45 min]
        W1 --> W2[Week 2<br>Prompting<br>1.5 hrs]
        W2 --> W3[Week 3<br>Plan Mode<br>1 hr]
    end

    subgraph "Skip (concepts only)"
        W4[Weeks 4-8<br>Technical<br>skim]
    end

    subgraph "Capstone"
        W9[Week 9<br>Option E<br>2 hrs]
    end

    W3 --> W4
    W4 --> W9

    style W2 fill:#4CAF50,color:#fff
    style W3 fill:#4CAF50,color:#fff
    style W9 fill:#4CAF50,color:#fff
    style W4 fill:#9E9E9E,color:#fff
```

**Your Focus Weeks (green):** Week 2 (Prompting) and Week 3 (Plan Mode) are your power weeks. Week 9 has a PM-specific capstone option.

### What You'll Cover vs Skip

| Week | Topic | PM Priority | Notes |
|------|-------|-------------|-------|
| 0 | AI Foundations | Recommended | Understand the technology |
| 1 | Setup & Orientation | Must Do | Observe, don't need to master |
| 2 | Prompting Foundations | Must Do | Learn to write good specs |
| 3 | Plan Mode | Must Do | Understand dev planning |
| 4 | Test-Driven Development | Skim | Concepts only, skip coding |
| 5 | Commands & Skills | Skip | Developer-focused |
| 6 | Agents & Hooks | Skip | Developer-focused |
| 7 | Plugins | Skip | Developer-focused |
| 8 | Real-World Workflows | Skim | Concepts for roadmap planning |
| 9 | Capstone | Must Do | PM-specific option available |

---

## Week-by-Week Focus

### Week 0: AI Foundations (45 min) - Recommended
**Goal:** Speak the language of AI development.

Key concepts you need:
- **LLM (Large Language Model):** The AI type Claude uses
- **Tokens:** Basic units of text that affect context limits
- **Context window:** How much the AI can "remember"
- **Hallucination:** Why AI output must be verified (critical for requirements)
- **Agentic:** AI that can take actions, not just answer questions

**Why this matters for PMs:** You'll make better decisions about when to use AI and set realistic expectations with stakeholders.

---

### Week 1: Setup & Orientation (45 min)
**Goal:** Understand the developer experience, not master the tool.

**What to do:**
- [ ] Install Claude Code (follow the guide)
- [ ] Have one conversation with Claude
- [ ] Observe a developer using it (pair session recommended)

**What to skip:** Deep configuration, CLAUDE.md customization.

**PM Insight:** Notice how developers interact with Claude. This helps you:
- Estimate AI-assisted development more accurately
- Write requirements that work well with AI workflows
- Understand what makes a "good" vs "bad" spec for AI consumption

---

### Week 2: Prompting Foundations (1.5 hours)
**Goal:** Learn to write specifications that translate well to AI-assisted development.

**This is your power week.** Good prompts = good specs = good products.

**The CLEAR Framework for PM Specs:**

| Element | What It Means | Example |
|---------|---------------|---------|
| **C**ontext | Business background | "Our HOA clients need to track violations..." |
| **L**ogic | Business rules | "Late fees compound monthly at 10%..." |
| **E**dge cases | What happens when... | "If payment is partial, apply to oldest first..." |
| **A**cceptance | How to verify | "User can see payment history sorted by date..." |
| **R**estrictions | What NOT to do | "Never auto-delete violations, only soft-delete..." |

**Spec Template for AI-Ready Requirements:**
```markdown
## Feature: [Name]

### Context
[Why this feature exists. Business driver.]

### User Story
As a [role], I want to [action], so that [benefit].

### Business Rules
1. [Rule 1 - be specific]
2. [Rule 2 - include numbers/thresholds]
3. [Rule 3 - describe edge cases]

### Acceptance Criteria
- [ ] [Verifiable criterion 1]
- [ ] [Verifiable criterion 2]
- [ ] [Verifiable criterion 3]

### Out of Scope
- [What this feature does NOT include]
- [Explicit non-goals]

### Questions for Dev
- [Ambiguity 1]
- [Technical decision needed]
```

**Exercise:** Take an existing spec and rewrite it using this template.

---

### Week 3: Plan Mode & Code Review (1 hour)
**Goal:** Understand how developers plan AI-assisted implementations.

**Key Concept:** When developers use "plan mode" (`Shift+Tab`), Claude creates an implementation plan before writing code. As a PM, you can:

1. **Review plans** - Ask to see Claude's implementation plan before coding starts
2. **Validate approach** - Check if the plan addresses your requirements
3. **Catch issues early** - Identify missing requirements before code is written

**PM Use Case - Requirement Validation:**
```
Given this feature spec:
[PASTE YOUR SPEC]

Create an implementation plan that covers:
1. What components need to change
2. What tests will verify the requirements
3. What edge cases are handled

Don't write code yet, just the plan.
```

Review the plan with your developer. Does it cover your requirements?

---

### Week 4: Test-Driven Development (30 min - Concepts Only)
**Goal:** Understand TDD, not practice it.

**What TDD Means for PMs:**
- Developers write tests BEFORE code
- Tests are essentially executable specifications
- 80-90% coverage means most requirements are verified automatically

**Why You Care:**
- TDD produces more reliable estimates (tests surface complexity early)
- You can ask "what tests will verify this requirement?"
- Coverage reports show what's actually being tested

**Skip:** All coding exercises. Just read the concepts.

---

### Weeks 5-7: Skip
These weeks are developer-focused (commands, skills, agents, plugins). Skip entirely.

**If curious:** Ask a developer to demo their custom commands. It's interesting but not essential for PMs.

---

### Week 8: Real-World Workflows (30 min - Skim)
**Goal:** Understand CI/CD for roadmap planning.

**Key Concepts:**
- **CI (Continuous Integration):** Automated testing on every code change
- **CD (Continuous Deployment):** Automated releases
- **AI in CI/CD:** Claude can help with test generation, code review in pipelines

**Why You Care:**
- Understand deployment frequency possibilities
- Know what "automation" means when devs mention it
- Better release planning

---

### Week 9: Capstone (3-4 hours)
**Goal:** Create PM-focused deliverables using Claude.

**PM Capstone Option: Product Design & Documentation**

Build a complete product specification package:

1. **Feature Specification Document**
   - Use Claude to help refine requirements
   - Generate edge case analysis
   - Create acceptance criteria

2. **User Story Mapping**
   - Use Claude to break epics into stories
   - Generate story dependencies
   - Identify MVP vs future scope

3. **Stakeholder Documentation**
   - Executive summary
   - Technical overview (for engineering)
   - User-facing documentation

**Deliverables:**
- Complete spec document for an HOA feature
- User story map with priorities
- Presentation to stakeholders

**Non-Coding Exercises:**

Instead of building code, you'll:
```
Prompt: "Given this high-level requirement: [PASTE]
Break it down into:
1. User stories (epic → stories)
2. Acceptance criteria for each story
3. Edge cases and error scenarios
4. Questions for stakeholders
5. MVP vs Phase 2 recommendations"
```

---

## PM-Specific Prompt Library

### Requirement Refinement
```
Here's a rough feature idea:
[PASTE IDEA]

Help me turn this into a complete specification:
1. Identify ambiguities and ask clarifying questions
2. Suggest acceptance criteria
3. List edge cases to consider
4. Recommend MVP scope
```

### User Story Generation
```
Break this epic into user stories:
[PASTE EPIC]

For each story include:
- User story format (As a... I want... So that...)
- Acceptance criteria
- Story points estimate rationale
```

### Edge Case Discovery
```
For this feature:
[PASTE FEATURE]

What edge cases should we consider?
Think about: null inputs, boundaries, concurrent access,
error states, user permissions, timing issues.
```

### Spec Review
```
Review this specification for completeness:
[PASTE SPEC]

Check for:
- Missing acceptance criteria
- Ambiguous requirements
- Untestable statements
- Scope creep risks
```

### Stakeholder Communication
```
Translate this technical description into stakeholder-friendly language:
[PASTE TECHNICAL TEXT]

Audience: [executive / end user / sales team]
Tone: [formal / casual / marketing]
```

---

## What PMs Should Know About AI-Assisted Dev

### Setting Expectations

| Topic | Reality |
|-------|---------|
| Speed | AI speeds up coding 2-5x, NOT 10x |
| Quality | Still requires human review |
| Estimates | May be 20-30% faster, not revolutionary |
| New features | AI helps most with well-defined requirements |
| Complex logic | Human judgment still essential |

### Good Specs for AI

**AI-Friendly Spec:**
```
Calculate late fees using 10% monthly compound interest.
Grace period: 30 days from due date.
Maximum fee cap: $1,000.
Round to 2 decimal places.
```

**AI-Unfriendly Spec:**
```
Add late fees that seem fair and reasonable.
Use industry standards.
Make sure it works correctly.
```

### Questions to Ask Developers

1. "Can you show me Claude's implementation plan before coding?"
2. "What tests will verify this requirement?"
3. "What edge cases is Claude handling?"
4. "Is this feature well-defined enough for AI assistance?"

---

## Resources

- [Glossary](/resources/glossary.md) - Term definitions (start here!)
- [Week 0 README](/sessions/week-0/README.md) - AI Foundations
- [Week 2 README](/sessions/week-2/README.md) - Prompting (most relevant)

---

*Questions? Hit up `#dx-training` on Slack. Ask for PM-specific guidance!*
