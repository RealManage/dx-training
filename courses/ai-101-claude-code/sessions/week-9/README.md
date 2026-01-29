# Week 9: Capstone Hackerspace & Future Roadmap

**Duration:** 2.5 hours
**Format:** Team-based project with individual evaluation
**Prerequisites:** Completed Weeks 1-8

> **Pre-Session Requirement:** Project options should be assigned/selected BEFORE the session to maximize build time. Have students review options and submit their choice by the day before.

## Learning Objectives

By the end of this session, you'll be able to:

- [ ] Synthesize all skills from Weeks 1-8 into a cohesive project
- [ ] Design and implement a production-ready RealManage feature
- [ ] Apply TDD, Plan Mode, hooks, skills, and agents effectively
- [ ] Present technical solutions to stakeholders clearly
- [ ] Evaluate AI-generated code for quality and security
- [ ] Plan continuous improvement using Claude Code
- [ ] Earn your RealManage AI Practitioner certification

## Pre-Session Checklist

### Participant Preparation

```bash
# Verify all tools are ready
dotnet --version        # Should show 10.x.x
node --version          # Should show 22.x.x (for Angular projects)
claude --version        # Latest Claude Code

# Set up Week 9 sandbox
cd courses/ai-101-claude-code/sessions/week-9
cp -r examples sandbox
cd sandbox/capstone-templates
```

### Prerequisites Review

Before starting your capstone:

1. **Weeks 1-4**: Setup, prompting, Plan Mode, TDD
2. **Week 5**: Commands & Basic Skills
3. **Week 6**: Agents & Hooks
4. **Week 7**: Plugins
5. **Week 8**: Real-World Automation

### Materials Needed

- [ ] Laptop with Claude Code and .NET 10 SDK
- [ ] Access to RealManage sandbox environment
- [ ] Team members identified (2-3 per team)
- [ ] Project option selected (see below)

---

## Session Plan

### Part 1: Project Selection & Planning (25 min)

#### Project Options

Choose ONE of the following capstone projects. Each includes starter templates in the `examples/capstone-templates/` folder.

**Role-Specific Recommendations:** See the `tracks/` directory for guidance on which option best fits your role.

---

### Developer Track Options (A, B, C)

##### Option A: Violation Escalation System (Recommended for Backend Focus)

Build an automated violation escalation system that:

- Tracks violations through 30/60/90 day progression
- Sends automated notices at each escalation level
- Calculates compound fines correctly (10% monthly)
- Integrates with board meeting agenda generation
- Provides audit trail for SOC 2 compliance

**Starter Template:** `examples/capstone-templates/option-a-violation-escalation/`

**Key Requirements:**

- State machine for violation lifecycle
- Event-driven architecture for notifications
- 80-90% test coverage on business logic
- Custom skill for `/escalate-violation`
- Hook for audit logging on all state changes

**Success Criteria:**

```
[ ] Violation progresses correctly through stages
[ ] Notices generated with correct fine calculations
[ ] Compound interest applied after 30 days
[ ] Board review triggered at 90+ days
[ ] Audit trail captures all changes
[ ] Test coverage >= 80%
```

##### Option B: Self-Service Knowledge Base (Recommended for Full-Stack)

Build an AI-powered knowledge base for residents:

- Search CCRs using natural language queries
- Auto-generate FAQs from common support tickets
- Resident portal for self-service answers
- Board member dashboard for analytics

**Starter Template:** `examples/capstone-templates/option-b-knowledge-base/`

**Key Requirements:**

- C# Web API for search endpoints
- Angular 17 frontend with search UI
- Custom skill for `/search-ccr`
- Sub-agent pattern for parallel searches
- Usage analytics for search operations

**Success Criteria:**

```
[ ] Natural language search returns relevant CCR sections
[ ] FAQ generation from ticket patterns
[ ] Resident portal functional and responsive
[ ] Analytics dashboard shows usage metrics
[ ] Search performance optimized
[ ] Test coverage >= 90%
```

##### Option C: Financial Forecasting Tool (Recommended for Data Focus)

Build a dues collection and budget forecasting tool:

- Predict dues collection rates based on history
- Identify at-risk accounts for proactive outreach
- Generate variance reports for board meetings
- Integrate with Azure SQL Database

**Starter Template:** `examples/capstone-templates/option-c-financial-forecast/`

**Key Requirements:**

- Historical data analysis service
- Prediction algorithms with confidence intervals
- Automated report generation
- Custom skill for `/forecast-collections`
- Batch automation script for scheduled reports

**Success Criteria:**

```
[ ] Collection predictions within 10% accuracy
[ ] At-risk accounts identified 30 days early
[ ] Variance reports generated automatically
[ ] Board meeting integration working
[ ] Test coverage >= 90%
```

---

### QA Track Option (D)

##### Option D: Test Automation Suite (Recommended for QA Engineers)

Build a comprehensive test automation framework for the HOA module:

- Complete test suite covering violation, dues, and resident workflows
- Test data generation tools for realistic scenarios
- Coverage dashboard with real-time metrics
- Batch test runner script for automation

**Starter Template:** `examples/capstone-templates/option-d-qa-test-automation/`

**Key Requirements:**

- xUnit test framework with FluentAssertions
- Test data generators using Bogus or AutoFixture
- Coverage tracking with Coverlet
- Batch test automation scripts
- Custom skill for `/generate-test-data`

**Success Criteria:**

```
[ ] Test suite covers all critical HOA workflows
[ ] Test data generation creates realistic scenarios
[ ] Coverage dashboard displays real-time metrics
[ ] Batch test script automates test runs
[ ] Tests are maintainable and well-documented
[ ] Coverage >= 80% on critical paths
```

**Deliverables:**

- Comprehensive test suite for HOA module
- Test data generation tools
- Coverage dashboard (HTML report or integration)
- Batch test automation scripts
- Documentation on test patterns and usage

---

### Product Management Track Option (E)

##### Option E: Product Design & Documentation (Recommended for PMs)

Use Claude Code to create complete product specifications and documentation:

- Feature specification for a new HOA capability
- User story mapping with acceptance criteria
- Stakeholder documentation and presentations
- **NON-CODING deliverables focus**

**Starter Template:** `examples/capstone-templates/option-e-pm-product-design/`

**Key Requirements:**

- Complete PRD (Product Requirements Document)
- User story map with epics and stories
- Acceptance criteria using Gherkin syntax
- Stakeholder presentation deck outline
- Custom skill for `/generate-user-stories`

**Success Criteria:**

```
[ ] PRD covers problem, solution, and success metrics
[ ] User stories follow INVEST principles
[ ] Acceptance criteria are testable and clear
[ ] Documentation is stakeholder-ready
[ ] All artifacts generated with Claude assistance
[ ] No coding required for submission
```

**Deliverables:**

- Complete feature specification document (PRD)
- User story mapping document
- Stakeholder documentation package
- Process documentation for using Claude for PM work
- Presentation outline or deck

---

### Support Track Option (F)

##### Option F: Knowledge Base & Response Templates (Recommended for Support)

Build a support toolkit using Claude Code:

- FAQ generation from existing documentation
- Response template library for common issues
- Customer communication tools and scripts
- Self-service content improvements

**Starter Template:** `examples/capstone-templates/option-f-support-knowledge-base/`

**Key Requirements:**

- FAQ document generated from CCRs and policies
- Response template library (20+ templates)
- Escalation decision tree
- Custom skill for `/draft-response`
- Quality metrics for response templates

**Success Criteria:**

```
[ ] FAQ covers top 20 resident questions
[ ] Response templates are professional and accurate
[ ] Templates include personalization placeholders
[ ] Escalation paths are clearly documented
[ ] All content generated with Claude assistance
[ ] Templates reviewed for tone and accuracy
```

**Deliverables:**

- FAQ generation from docs
- Response template library
- Customer communication tools
- Escalation decision tree
- Quality assurance checklist

---

### Custom Project Option

##### Option G: Custom Project (Requires Instructor Approval)

Propose your own project that:

- Solves a real RealManage business problem
- Uses skills from at least 4 different weeks
- Includes measurable success criteria
- Can be completed in 90 minutes

**Approval Process:**

1. Write 1-paragraph proposal
2. List which week's skills you'll use
3. Define 5+ success criteria
4. Get instructor sign-off before starting

---

### Part 2: Architecture & Planning (15 min)

Use Claude Code's Plan Mode to design your solution.

#### Step 1: Enter Plan Mode

```bash
cd sandbox/capstone-templates/<your-project>
claude

# In Claude Code, press Shift+Tab to toggle plan mode
# Or start your prompt with "Use plan mode to..."
```

#### Step 2: Describe Your Architecture

Use this prompt template:

```markdown
I'm building [PROJECT NAME] for the RealManage capstone.

## Requirements
[List 5-7 key requirements from your chosen option]

## Constraints
- C# .NET 10 for backend services
- Angular 17 for frontend (if applicable)
- 80-90% test coverage minimum
- Must use hooks for audit logging
- Must include at least one custom skill

## Please Design
1. High-level architecture diagram (mermaid)
2. Key services and their responsibilities
3. Data models and relationships
4. Testing strategy
5. Implementation order (what to build first)
```

#### Step 3: Review and Approve Plan

Before exiting Plan Mode:

```markdown
[ ] Architecture makes sense for the problem
[ ] All requirements are addressed
[ ] Testing strategy is comprehensive
[ ] Implementation order is logical
[ ] No over-engineering
```

Exit Plan Mode only when ready:

```markdown
# Press Shift+Tab again to exit plan mode
# Or simply start implementing - Claude will transition naturally
```

---

### Part 3: Implementation Sprint (50 min)

#### Implementation Guidelines

##### TDD Approach (Red-Green-Refactor)

```bash
# Start with a failing test
claude "Write a failing test for [specific behavior]"

# Make it pass with minimal code
claude "Make this test pass with the simplest implementation"

# Refactor for quality
claude "Refactor this to be more maintainable while keeping tests green"
```

##### Using Custom Skills

Create a skill for your main workflow:

```bash
mkdir -p .claude/skills/your-workflow
```

Example skill structure (`.claude/skills/your-workflow/SKILL.md`):

```markdown
---
name: your-workflow
description: Execute the main workflow for [your project]
argument-hint: [property_id] [action_type]
disable-model-invocation: true
---

Process $1 with action $2.

## Workflow Steps
1. Validate input parameters
2. Execute business logic
3. Log audit trail
4. Return result with metrics

## Expected Output
- Success/failure status
- Affected records
- Audit entries created
- Cost metrics (if AI operations)
```

##### Using Hooks for Audit Logging

Create an audit hook (`.claude/settings.json`):

```json
{
  "hooks": {
    "PostToolUse": [
      {
        "matcher": "Edit|Write",
        "command": "echo '[AUDIT] File modified: $FILE_PATH' >> .audit.log"
      }
    ]
  }
}
```

##### Parallel Development with Sub-Agents

For complex features, use sub-agents:

```
Use the Task tool to explore the codebase for [pattern] while I work on [feature].
```

#### Time Checkpoints

| Time | Milestone |
| ---- | --------- |
| 0:00-0:10 | Setup and initial test structure |
| 0:10-0:20 | Core domain models and first service |
| 0:20-0:30 | Main business logic with TDD |
| 0:30-0:40 | Integration tests and edge cases |
| 0:40-0:50 | Custom skill, hooks, and final testing |

#### Progress Tracking

Update your TODO list as you work:

```bash
# Check current status
claude "/todo"

# Add items
claude "/todo add Implement fine calculation service"

# Complete items
claude "/todo done 1"
```

---

### Part 4: Testing & Quality Verification (10 min)

#### Run Full Test Suite

```bash
# Build and test
dotnet build --warnaserror
dotnet test --collect:"XPlat Code Coverage"

# Check coverage (should be >= 80%)
# View coverage report in terminal or generate HTML report
```

#### Quality Checklist

```markdown
Code Quality:
[ ] All tests pass
[ ] Coverage >= 80% (or higher for critical paths)
[ ] No build warnings
[ ] No obvious security issues
[ ] Code follows C# conventions

Documentation:
[ ] CLAUDE.md is comprehensive
[ ] Key decisions documented
[ ] Intentional limitations noted
[ ] API endpoints documented (if applicable)

Skills & Hooks:
[ ] Custom skill works correctly
[ ] Hooks trigger as expected
[ ] Audit trail captures changes
```

#### Security Review

Before presenting, verify:

```markdown
[ ] No hardcoded secrets
[ ] Input validation on all endpoints
[ ] SQL injection prevention (parameterized queries)
[ ] XSS prevention (if frontend)
[ ] Proper error handling (no stack traces to users)
```

---

### Part 5: Demo Preparation (10 min)

#### Demo Script Template

Prepare a 3-minute demo that covers:

1. **Problem Statement** (30 sec)
   - What business problem does this solve?
   - Who benefits?

2. **Architecture Overview** (30 sec)
   - Show your Plan Mode diagram
   - Highlight key design decisions

3. **Live Demo** (90 sec)
   - Show the happy path working
   - Demonstrate one edge case handled
   - Show test coverage results

4. **Lessons Learned** (30 sec)
   - What worked well with Claude Code?
   - What would you do differently?

#### Demo Environment

```bash
# Prepare demo environment
cd sandbox/capstone-templates/<project>

# Clear any debug output
dotnet clean
dotnet build

# Have tests ready to run
dotnet test --list-tests

# Open Claude Code for live interaction
claude
```

---

### Part 6: Presentations & Celebration (15 min)

#### Presentation Order

Each team presents for 3-4 minutes:

1. Team A: [Project Option]
2. Team B: [Project Option]
3. Team C: [Project Option]
4. Team D: [Project Option]

#### Peer Evaluation

Rate each presentation (1-5):

| Criteria | Team A | Team B | Team C | Team D |
| -------- | ------ | ------ | ------ | ------ |
| Problem clarity |  |  |  |  |
| Solution elegance |  |  |  |  |
| Demo effectiveness |  |  |  |  |
| Code quality |  |  |  |  |
| Innovation |  |  |  |  |

#### Q&A Guidelines

Ask questions about:

- Interesting design decisions
- How Claude Code helped
- Challenges overcome
- Future enhancements

---

## Evaluation Rubric

### Grading Criteria (100 points total)

| Category | Points | Criteria |
| -------- | ------ | -------- |
| **Functionality** | 25 | Core requirements working, edge cases handled |
| **Code Quality** | 25 | Clean, idiomatic C#, SOLID principles |
| **Test Coverage** | 20 | >= 80-90% for backend, >= 80% for full-stack |
| **Documentation** | 15 | CLAUDE.md, comments, API docs |
| **Innovation** | 15 | Creative use of skills, hooks, agents |

### Role-Specific Adjustments

**For Non-Coding Tracks (E, F):**

| Category | Points | Criteria |
| -------- | ------ | -------- |
| **Completeness** | 25 | All deliverables present and thorough |
| **Quality** | 25 | Professional, clear, actionable content |
| **Claude Usage** | 20 | Effective use of Claude Code features |
| **Documentation** | 15 | Process and methodology documented |
| **Innovation** | 15 | Creative approaches to deliverables |

### Grade Scale

| Score | Grade | Certification |
| ----- | ----- | ------------- |
| 90-100 | A | Certified with Distinction |
| 80-89 | B | Certified |
| 70-79 | C | Certified (Conditional) |
| < 70 | NC | Not Certified - Retry Required |

### Certification Requirements

To earn **RealManage AI Practitioner** certification:

1. Complete all 9 weeks of training
2. Score 70+ on capstone project
3. Submit project code/deliverables for review
4. Receive instructor approval

---

## Certification Process

### Submission Requirements

```bash
# Create submission package
cd sandbox/capstone-templates/<project>

# Ensure all tests pass (for coding tracks)
dotnet test

# Generate coverage report
dotnet test --collect:"XPlat Code Coverage"

# Create submission archive
zip -r capstone-submission.zip . -x "bin/*" -x "obj/*"
```

### Submission Checklist

```
For Coding Tracks (A, B, C, D):
[ ] All source code included
[ ] Tests pass on clean build
[ ] CLAUDE.md complete
[ ] Coverage report included
[ ] Demo script prepared
[ ] No secrets in code

For Non-Coding Tracks (E, F):
[ ] All deliverables included
[ ] Documents are complete and polished
[ ] Process documentation included
[ ] Demo script prepared
[ ] Claude conversation excerpts (optional)
```

### Post-Submission

1. **Review Period**: 3-5 business days
2. **Feedback**: Detailed comments on work
3. **Certificate**: Digital badge and PDF
4. **Recognition**: Announced in Engineering All-Hands

---

## What's Next?

### Continuing Your Journey

#### Advanced Courses (Coming Q3 2025)

- **AI 102: Advanced Agent Development**
  - Building custom MCP servers
  - Multi-agent orchestration
  - Production deployment patterns

- **AI 103: Platform Engineering with AI**
  - Infrastructure as Code with Claude
  - DevOps automation
  - Cost optimization at scale

- **AI 104: Data Engineering with Claude**
  - ETL pipeline development
  - Data quality automation
  - Analytics dashboard creation

#### Self-Directed Learning

```bash
# Join the community
#claude-hackerspace on Slack

# Weekly office hours
Thursdays 2-3 PM CT

# Share discoveries
#ai-exchange channel
```

#### Contributing Back

Ways to contribute to the course:

1. **Share Tips**: Post discoveries in Slack
2. **Improve Docs**: Submit PRs to course repo
3. **Mentor Others**: Help new cohorts
4. **Build Examples**: Create new training exercises

---

## Resources

### Official Documentation

- [Claude Code Documentation](https://docs.anthropic.com/en/docs/claude-code)
- [Claude Code Tutorials](https://docs.anthropic.com/en/docs/agents-and-tools/claude-code/tutorials)
- [Best Practices Guide](https://docs.anthropic.com/en/docs/claude-code/best-practices)

### RealManage Resources

- **Slack**: `#ai-exchange`, `#claude-hackerspace`
- **Office Hours**: Thursdays 2-3 PM CT
- **Wiki**: Internal AI Guidelines (Confluence)

### Community Resources

- [Awesome Claude Code](https://github.com/hesreallyhim/awesome-claude-code)
- [Community Forums](https://htdocs.dev/posts/introducing-claude-your-ultimate-directory-for-claude-code-excellence/)

---

## Quick Reference Card

### Essential Commands

```bash
# Start Claude Code
claude

# Plan Mode
Shift+Tab                # Toggle plan mode on/off
# Or start prompt with "Use plan mode to design..."

# Custom Skills
/<skill-name>            # Run custom skill
/plugin-name:<skill>     # Run plugin skill

# Context Management
/compact                 # Compress context
/clear                   # Start fresh
```

### File Locations

```
.claude/settings.json              # Hooks and settings
.claude/commands/<name>.md         # Legacy commands (still supported)
.claude/skills/<name>/SKILL.md     # Modern skills (recommended)
.claude/agents/<name>.md           # Custom subagents
CLAUDE.md                          # Project context
```

> **Note:** Commands and skills both create `/name` slash commands. Skills are preferred as they support directories with templates and supporting files.

### TDD Cycle

```
1. Write failing test (Red)
2. Minimal code to pass (Green)
3. Refactor for quality (Refactor)
4. Repeat
```

---

## Congratulations

You've completed the **RealManage AI 101: Claude Code** course!

You're now equipped to:

- Use AI as a powerful development partner
- Write better code with comprehensive test coverage
- Automate repetitive tasks with skills and hooks
- Build innovative solutions for HOA management
- Collaborate more effectively with AI assistance

**Remember:** The future of coding isn't replacing developers--it's amplifying their capabilities!

### Your Next Steps

1. Apply these skills to your daily work
2. Share knowledge with your team
3. Continue exploring Claude Code's capabilities
4. Consider advanced courses when available

---

**Questions or Feedback?**

- Slack: `#ai-exchange`
- GitLab: Submit issues to course repo

*Thank you for being part of the RealManage AI journey!*

---

## 📚 Quick Resources

- [Glossary](../../resources/glossary.md) - Key terms and definitions
- [Decision Trees](../../resources/decision-trees.md) - When to use what
- [Troubleshooting](../../resources/troubleshooting.md) - Common issues and fixes
- [CLI Commands](../../resources/cli-commands.md) - Command cheatsheet
