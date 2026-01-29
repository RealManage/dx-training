# RealManage AI 101: Claude Code 🚀

Welcome to the comprehensive Claude Code training course designed specifically for RealManage teams. This self-paced course will transform how you write code, automate tasks, and solve complex problems using AI assistance.

## 🎯 Choose Your Path

| Your Role | Quick Start Guide | Focus Weeks | Skip |
| --------- | ----------------- | ----------- | ---- |
| Developer | [Developer Track](resources/quick-start-developer.md) | All weeks | Week 0 (optional) |
| QA Engineer | [QA Track](resources/quick-start-qa.md) | 1, 2, 4, 8, 9 | 5, 6, 7 (skim) |
| Product Manager | [PM Track](resources/quick-start-pm.md) | 0, 1, 2, 3, 9 | 4-8 (concepts only) |
| Support | See PM Track | 0, 1, 2, 9 | Technical weeks |
| **Experienced Dev** | See below | 3, 4, 8 | 0-2, 5-7 |

### ⚡ Experienced Developer Fast Track (~4 hours)

Already comfortable with AI tools? Skip the basics:

1. **[Decision Trees](resources/decision-trees.md)** (30 min) - When to use what
2. **[Week 3: Code Review](sessions/week-3/README.md)** (1 hr) - Plan Mode + CodeReviewPro exercise
3. **[Week 4: TDD](sessions/week-4/README.md)** (1 hr) - Tests-first workflow with Claude
4. **[Week 8: Automation](sessions/week-8/README.md)** (1 hr) - Headless automation patterns
5. **Capstone** (as needed) - Validate your skills

*Sam-approved for senior devs who've used Copilot and want to see what's different.*

**Quick Resources:** [Glossary](resources/glossary.md) | [Decision Trees](resources/decision-trees.md) | [Troubleshooting](resources/troubleshooting.md)

---

## 📚 Course Overview

- **Duration:** 9 weeks (self-paced, ~2 hours per week)
- **Level:** Beginner to Intermediate
- **Format:** Self-study with optional group sessions
- **Goal:** Progressively immerse RealManage's cross-functional team in Claude Code, building practical skills while leaving time for normal work

## 🎯 What You'll Learn

By completing this course, you'll be able to:

- ✅ Set up and configure Claude Code for your development environment
- ✅ Write effective prompts that get high-quality code responses
- ✅ Use Plan Mode for complex architectural decisions
- ✅ Implement Test-Driven Development with AI assistance (80-90% coverage)
- ✅ Build custom skills and use sub-agents for parallel work
- ✅ Automate repetitive HOA management tasks
- ✅ Create production-ready C# and Angular code with proper testing

## 🗺️ Learning Path

```mermaid
graph LR
    A[Week 0: AI Foundations] -.->|optional| B[Week 1: Setup]
    B --> C[Week 2: Prompting]
    C --> D[Week 3: Plan Mode]
    D --> E[Week 4: TDD]
    E --> F[Week 5: Components]
    F --> G[Week 6: Plugins]
    G --> H[Week 7: Real-World]
    H --> I[Week 8: Advanced]
    I --> J[Week 9: Capstone]
```

## 📁 Course Structure

```
ai-101-claude-code/
├── .vscode/                     # VS Code workspace settings
│   ├── extensions.json         # Recommended extensions
│   └── settings.json           # Project settings
├── resources/                   # Reference materials & templates
│   ├── prompts/
│   │   ├── getting-started.md
│   │   └── hoa-templates.md
│   ├── cli-commands.md         # Command cheatsheet
│   ├── claude-md-minimal.md    # Minimal CLAUDE.md starter
│   ├── claude-md-template.md   # Full CLAUDE.md template
│   ├── common-patterns.md
│   ├── decision-trees.md       # When to use what
│   ├── glossary.md             # Term definitions
│   ├── prompt-library.md
│   ├── quick-reference.md
│   ├── quick-start-developer.md
│   ├── quick-start-pm.md
│   ├── quick-start-qa.md
│   └── troubleshooting.md
├── sessions/                    # Weekly lessons with examples
│   ├── week-0/                 # AI Foundations (optional)
│   │   └── README.md
│   ├── week-1/                 # Setup & Orientation
│   │   ├── example/            # Reference CLI app (in git)
│   │   ├── README.md           # Full lesson plan
│   │   └── setup-verification.md
│   ├── week-2/                 # Prompting Foundations
│   │   └── README.md
│   ├── week-3/                 # Plan Mode
│   │   └── README.md
│   ├── week-4/                 # TDD
│   │   └── README.md
│   ├── week-5/                 # Commands & Basic Skills
│   │   └── README.md
│   ├── week-6/                 # Agents & Hooks
│   │   └── README.md
│   ├── week-7/                 # Plugins
│   │   └── README.md
│   ├── week-8/                 # Advanced Patterns
│   │   └── README.md
│   └── week-9/                 # Capstone
│       └── README.md
├── CLAUDE.md                    # AI context for this course
└── README.md                    # You are here
```

## 🚀 Quick Start

### Option 1: Self-Paced Learning (Recommended)

1. **Complete Prerequisites** → Check the list below
2. **Start Week 1** → [Setup & Orientation](./sessions/week-1/README.md)
3. **Practice in Sandbox** → Copy examples to sandbox for hands-on work
4. **Track Progress** → Use the checklist at the bottom
5. **Get Help** → Join `#ai-exchange` on Slack

### 💡 Sandbox Workflow

Each week with code examples follows this pattern:

```bash
# Navigate to the week's folder
cd sessions/week-1

# Copy the example to create your personal sandbox
cp -r example sandbox

# Enter your sandbox
cd sandbox

# Start Claude Code in your sandbox
claude
```

**Why this approach?**

- ✅ **Safe experimentation** - Break things without fear
- ✅ **Clean git history** - Your work won't be committed
- ✅ **Easy reset** - Just delete sandbox and copy again
- ✅ **Reference available** - Original example stays pristine

### Option 2: Cohort Learning

Join a scheduled cohort for group learning:

- Weekly 2-hour sessions
- Live demonstrations
- Peer programming
- Direct Q&A with instructors

Check `#ai-exchange` for upcoming cohorts.

## 📋 Prerequisites Checklist

Before starting, ensure you have:

- [ ] **Development Environment**
  - [ ] Windows: Git Bash or WSL2 installed
  - [ ] Mac/Linux: Terminal ready
  - [ ] .NET 10 SDK ([Download](https://dotnet.microsoft.com/download/dotnet/9.0))
  - [ ] Node.js 22 LTS via nvm
  - [ ] npm 10+
  - [ ] Git configured with GitLab access
  - [ ] IDE: VS Code, Windsurf, or Cursor with extensions (see `.vscode/extensions.json`)

- [ ] **Accounts & Access**
  - [ ] Anthropic account or API Key (for Claude Code)
  - [ ] GitLab account with RealManage access
  - [ ] Slack access to `#ai-exchange` channel

- [ ] **Basic Knowledge**
  - [ ] Comfortable with command line
  - [ ] Basic Git operations
  - [ ] C# and/or Angular experience
  - [ ] Understanding of TDD principles

### QA Engineer Prerequisites

If following the QA Track, you need less setup:

- [ ] Claude Code installed (see Week 1 QUICKSTART)
- [ ] Basic terminal familiarity (can run commands)
- [ ] .NET SDK (for running tests, not writing production code)
- [ ] Slack access to `#ai-exchange`
- [ ] *Skip:* Deep C# experience not required—focus on test frameworks (xUnit)

### PM / Support Prerequisites

If following the PM or Support Track:

- [ ] Claude Code installed (see Week 1 QUICKSTART)
- [ ] Can open terminal and run basic commands
- [ ] Slack access to `#ai-exchange`
- [ ] *Skip:* .NET SDK, npm, Angular, and testing tools not required

---

## 📖 9-Week Training Curriculum

## Week 1: Setup & Orientation

### Objectives

- Install and authenticate the `claude-code` CLI
- Understand how Claude Code works beyond simple code completion
- Navigate the CLI with essential commands
- Create and manage CLAUDE.md memory files
- Identify 3+ use cases for daily work

### Agenda (2 hrs)

#### 1. Installation & Authentication (20 min)

- **Windows users**: Use Git Bash (recommended) or WSL2
- System requirements: Node 22 LTS, npm 10+
- Install via `npm install -g @anthropic-ai/claude-code`
- Complete OAuth authentication
- Verify with `claude doctor` command

#### 2. Tour of the CLI (20 min)

- Start a session with `claude` command
- Essential slash commands (`/help`, `/clear`, `/context`, `/memory`)
- Understanding context management with `/compact`
- Model selection: Sonnet (fast) vs Opus (most capable)

#### 3. First Contact (30 min)

- Copy the [Week 1 Example](./sessions/week-1/example/) to your sandbox
- Explore a modern C# CLI app with intentional bugs
- Ask questions about top-level programs, records, and patterns
- Fix the compound interest bug in CalculateFine

#### 4. CLAUDE.md and Memory (40 min)

- Understand the hierarchical memory system
- Project memory (`./CLAUDE.md`) vs user memory (`~/.claude/CLAUDE.md`)
- Quick setup: Use `#` shortcut or `/memory` command
- Create RealManage-specific templates with TDD requirements

#### 5. Reflection (10 min)

- Discuss takeaways and real task applications
- Share one "aha moment" in Slack

**[→ Full Week 1 Lesson](./sessions/week-1/README.md)**

---

## Week 2: Prompting Foundations

### Objectives

- Master clear, conversational communication with Claude
- Learn when (and when not) to add structure to prompts
- Build a personal prompt style that works for you
- Generate code with 80-90% test coverage consistently

### Agenda (2 hrs)

#### 1. The Truth About Prompting (25 min)

- **Myth**: You need complex Markdown or XML tags
- **Reality**: Clear communication is what matters
- Most successful interactions use natural language
- Structure is a tool, not a requirement
- [Be Clear and Direct](https://docs.anthropic.com/en/docs/build-with-claude/prompt-engineering/be-clear-and-direct)

#### 2. When Structure Actually Helps (15 min)

- Complex multi-part requests → Bullet points or headers
- API specifications → Tables or structured data
- Nested requirements → Markdown sections
- Everything else → Natural conversation works great!

#### 3. Hands-On Prompt Workshop (60 min)

Practice real RealManage scenarios:

- Start with natural language
- See what Claude asks for clarification
- Add specifics based on feedback
- Compare results with different approaches

#### 4. Build YOUR Prompt Style (15 min)

- Find what works for your typing speed and thinking style
- Create templates only for repetitive tasks
- Focus on clarity over formatting

#### 5. Real Examples & Discussion (10 min)

- Review actual successful prompts (no XML!)
- Share what communication style feels natural

**[→ Full Week 2 Lesson](./sessions/week-2/README.md)**

---

## Week 3: Tactical Planning & Code Review Excellence

### Objectives

- Master Claude's three modes: Auto, Step, and Plan
- Use plan mode as a tactical thinking partner (not upfront documentation)
- Iterate on plans in real-time before executing
- Organize code review fixes systematically
- Switch to Opus model for deep code analysis

### Agenda (2.5 hrs)

#### 1. Mode Controls & Planning (30 min)

- Claude's three modes (Step/Auto/Plan) and when to use each
- Key controls: Shift+Tab to toggle, Esc to stop
- Plan mode for tactical thinking, not documentation
- Iterating on plans through conversation

#### 2. Code Review Mastery (30 min)

- Using Opus for deep analysis with `/model opus`
- Review → Plan → Fix workflow
- Managing Claude when things go wrong

#### 3. Hands-On Workshop (75 min)

- **BugHunter:** Find and fix compound interest bugs with TDD
- **CodeReviewPro:** Review, plan, and fix 15+ issues systematically
- **PhasedBuilder:** Implement 3-phase payment plan feature

#### 4. Q&A and Wrap-Up (15 min)

- Questions and troubleshooting
- Preview Week 4 (TDD with Claude)

### Key Takeaways

- Plan mode is for thinking through problems, not documenting them
- Use Esc and mode switching to keep Claude on track
- Opus for analysis, Sonnet for implementation

**[→ Full Week 3 Lesson](./sessions/week-3/README.md)**

---

## Week 4: Test-Driven Development with Claude

### Objectives

- Learn how to pair TDD with Claude for reliable code
- Master the Red-Green-Refactor cycle with AI
- Apply TDD to RealManage's 80-90% coverage requirement

### Agenda (2 hrs)

#### 1. TDD Introduction (20 min)

- Why TDD works with AI: prevents hallucinations
- [Claude Code Best Practices - TDD](https://www.anthropic.com/engineering/claude-code-best-practices)
- Red-Green-Refactor cycle
- Benefits for HOA management systems

#### 2. Writing Tests with Claude (20 min)

- Ask Claude to write xUnit tests from specifications
- Ensure tests cover edge cases
- C# examples with FluentAssertions and Moq
- Angular testing with Jasmine/Karma

#### 3. Iterative Coding (40 min)

- Run tests to confirm failure (red phase)
- Write code to pass tests (green phase)
- Refactor while keeping tests green
- Never let Claude modify the tests

#### 4. Hands-On Exercise (30 min)

RealManage examples:

- Late fee calculation with compound interest
- Violation escalation workflows
- Payment processing with audit trails
- Board meeting report generation

#### 5. Reflection (10 min)

- Compare TDD with AI vs traditional TDD
- Plan integration into daily workflow

**[→ Full Week 4 Lesson](./sessions/week-4/README.md)**

---

## Week 5: Commands & Basic Skills

### Objectives

- Create custom slash commands for reusable workflows
- Build skills with supporting files and advanced features
- Understand the command vs skill decision tree
- Apply automation to HOA-specific workflows

### Agenda (2 hrs)

#### 1. Slash Commands (30 min)

- Creating commands in `.claude/commands/`
- YAML frontmatter format
- Pass arguments with `$ARGUMENTS`, `$1`, `$2`
- RealManage-specific commands

#### 2. Skills - Commands Evolved (40 min)

- Skills vs Commands comparison
- Supporting files (templates, scripts, data)
- Skill YAML frontmatter fields
- When to use commands vs skills (decision tree)

#### 3. Hands-On Practice (50 min)

- Create custom command for HOA violations
- Build a skill with supporting files
- Convert a command to a skill
- Test your automation with real scenarios

**[→ Full Week 5 Lesson](./sessions/week-5/README.md)**

---

## Week 6: Agents & Hooks

### Objectives

- Define custom subagents with specific tools and permissions
- Configure hooks for compliance and automation
- Build security patterns with PreToolUse hooks
- Create audit trails for SOC 2 compliance

### Agenda (2 hrs)

#### 1. Custom Subagents (40 min)

- Define agents in `.claude/agents/`
- Tool restrictions and permission modes
- Model selection (sonnet, opus, default)
- Using `/agents` command to manage agents

#### 2. Hooks (40 min)

- PreToolUse, PostToolUse, Notification, Stop
- Audit logging for SOC 2 compliance
- Blocking dangerous operations
- Auto-run tests after edits

#### 3. Hands-On Practice (40 min)

- Define a code-reviewer subagent
- Configure audit hooks
- Build security guardrails
- Test hook triggers

**[→ Full Week 6 Lesson](./sessions/week-6/README.md)**

---

## Week 7: Plugins - The Complete Package

### Objectives

- Understand Plugins as the packaging system for all components
- Create plugins containing skills, agents, and hooks
- Package your Week 5-6 work for distribution
- Distribute plugins via marketplace or local installation

### Agenda (2 hrs)

#### 1. Plugin Architecture (30 min)

- Plugins package everything (skills + agents + hooks)
- Plugin manifest and structure
- Creating plugin from scratch

#### 2. Skills in Plugins (30 min)

- Skills vs Commands comparison
- Skill YAML frontmatter
- Supporting files (templates, scripts)

#### 3. Agents in Plugins (25 min)

- Packaging Week 5 agents in plugins
- Skills spawning agents (`context: fork`, `agent:`)
- Dynamic context injection (`` !`command` ``)

#### 4. Hooks in Plugins (15 min)

- Plugin-level hooks (hooks.json)
- Skill-embedded hooks
- When to use each

#### 5. Distribution & Marketplace (20 min)

- Local plugin development
- Marketplace installation
- Plugin commands

#### 6. Hands-On Workshop (30 min)

- Build a complete RealManage plugin
- Add skills, agents, and hooks
- Test with `--plugin-dir`

**[→ Full Week 7 Lesson](./sessions/week-7/README.md)**

---

## Week 8: Real-World Automation

### Objectives

- Build cross-functional skills for different team workflows
- Create headless automation scripts using Claude Code CLI
- Apply efficiency strategies for optimal Claude Code usage
- Create continuous improvement workflows

### Agenda (2 hrs)

#### 1. Cross-Functional Use Cases (25 min)

- **Support:** Ticket summarization, response drafting
- **PM:** Release notes, sprint planning
- **Engineering:** API docs, refactoring automation

#### 2. Headless Claude Automation (30 min)

- CLI flags for batch processing (`-p`, `--output-format`, etc.)
- Building automation scripts
- Multi-stage processing pipelines

#### 3. Efficiency & Context Management (20 min)

- Model selection (Sonnet vs Opus)
- Context management strategies
- Efficient prompting patterns

#### 4. Hands-On Workshop (45 min)

- Build cross-functional skills
- Create batch automation scripts
- Optimize your workflows

**[→ Full Week 8 Lesson](./sessions/week-8/README.md)**

---

## Week 9: Capstone Hackerspace & Future Roadmap

### Objectives

- Consolidate all skills in a larger project
- Encourage creativity and innovation
- Identify follow-up projects

### Agenda (2 hrs)

#### 1. Project Selection (10 min)

Teams propose RealManage solutions:

- Automate HOA violation escalation
- Create self-service knowledge base
- Integrate with external CRM systems
- Build financial forecasting tools

#### 2. Building (90 min)

- Apply all learned skills
- Document in CLAUDE.md
- Write tests throughout (80-90% coverage)
- Use C#/.NET 10 and Angular 17

#### 3. Demo & Celebration (20 min)

- Team demonstrations
- Discuss production readiness
- Future learning paths

**[→ Full Week 9 Lesson](./sessions/week-9/README.md)**

---

## 🎓 Certification Path

Complete all 9 weeks and submit a capstone project to earn:

- **RealManage AI Practitioner** certificate
- Recognition in Engineering All-Hands
- Badge for your GitLab profile
- Priority access to advanced courses

## 🤝 Getting Help

### Immediate Help

- **Quick Questions**: `#ai-exchange` on Slack
- **Bugs/Issues**: [GitLab Issues](https://gitlab.com/therealmanage/tools/dx/dx-training/-/issues)
- **Office Hours**: Thursdays 2-3 PM CT

### Self-Help Resources

- [Glossary](./resources/glossary.md) - Key terms and definitions
- [Quick Reference](./resources/quick-reference.md) - CLAUDE.md templates and shortcuts
- [Decision Trees](./resources/decision-trees.md) - When to use what
- [Common Patterns](./resources/common-patterns.md) - Code patterns and examples
- [CLI Commands Cheatsheet](./resources/cli-commands.md) - Essential commands at a glance
- [Troubleshooting Guide](./resources/troubleshooting.md) - Common issues and fixes
- [Prompt Library](./resources/prompt-library.md) - Reusable prompts for HOA tasks
- [Getting Help](./resources/getting-help.md) - Support channels and how to get assistance

## 📚 Follow-Up and Resources

### Official Documentation

- [Anthropic Claude Code Docs](https://docs.anthropic.com/en/docs/claude-code)
- [Prompt Engineering Guide](https://docs.anthropic.com/en/docs/build-with-claude/prompt-engineering)
- [Claude Code Best Practices](https://www.anthropic.com/engineering/claude-code-best-practices)

### Internal Support

- Create `#claude-hackerspace` Slack channel
- Schedule weekly office hours
- Share prompt libraries and CLAUDE.md templates

### Continued Learning

- Advanced topics: RAG, vector databases, agent frameworks
- Compare with other tools: Copilot, ChatGPT, Cursor
- Explore automation opportunities

### Safety & Ethics

- Maintain human oversight
- Validate AI outputs before production use
- Protect sensitive HOA and resident data
- Follow SOC 2 Type II requirements

## 🌟 Success Stories

> "Claude Code reduced our code review time by 40% and caught edge cases we typically miss." - *Engineering Team Lead*

> "I automated our monthly HOA violation reports in Week 6. What used to take 4 hours now takes 15 minutes." - *Product Manager*

> "The TDD module changed how I approach all development, not just with AI." - *Senior Developer*

## 📊 Success Metrics

### Developer Track

You're ready for the next week when you can:

- Start Claude Code in any project directory
- Use basic slash commands without looking them up
- Write a CLAUDE.md that provides context
- Generate working C# code with 80-90% test coverage
- Manage context efficiently for optimal results

### QA Track

You've completed the QA track when you can:

- Generate comprehensive test suites for existing code
- Identify coverage gaps and write tests to fill them
- Use Claude to create test data and edge case scenarios
- Review and improve existing test quality
- Complete Option D capstone (Test Automation Suite)

### PM Track

You've completed the PM track when you can:

- Explain AI capabilities and limitations to stakeholders
- Write AI-ready feature specifications using the CLEAR framework
- Use Claude to refine requirements and generate user stories
- Understand developer conversations about Claude Code workflows
- Complete Option E capstone (PRD + User Stories)

## 🚦 Red Flags

Seek help if:

- Claude Code won't start after installation
- Authentication keeps failing
- Generated code has obvious errors
- Costs seem unusually high (>$5/hour)
- Test coverage drops below 80% *(Developer/QA tracks)*

---

## References

### Official Anthropic Documentation

1. **Claude Code Documentation** - <https://docs.anthropic.com/en/docs/claude-code>
2. **Prompt Engineering Guide** - <https://docs.anthropic.com/en/docs/build-with-claude/prompt-engineering>
3. **Model Context Protocol** - <https://docs.anthropic.com/en/docs/mcp>
4. **Claude Code Best Practices** - <https://www.anthropic.com/engineering/claude-code-best-practices>

### Test-Driven Development Resources

1. **Claude Code and TDD** - The New Stack - <https://thenewstack.io/claude-code-and-the-art-of-test-driven-development/>
2. **CLAUDE MD TDD Wiki** - <https://github.com/ruvnet/claude-flow/wiki/CLAUDE-MD-TDD>
3. **TDD Guard for Claude Code** - <https://github.com/nizos/tdd-guard>
4. **E2E Testing with Claude Code** - <https://shipyard.build/blog/e2e-testing-claude-code/>

### Community Resources

1. **Awesome Claude Code** - <https://github.com/hesreallyhim/awesome-claude-code>
2. **Claude Code Beginners Guide** - Geeky Gadgets - <https://www.geeky-gadgets.com/claude-code-beginners-guide-2025/>
3. **MCP Servers Guide** - Geeky Gadgets - <https://www.geeky-gadgets.com/mcp-servers-claude-code-integration/>
4. **MCP Quickstart** - <https://modelcontextprotocol.io/quickstart/server>

### Additional Learning

1. **Test-driven development with AI** - Builder.io - <https://www.builder.io/blog/test-driven-development-ai>
2. **How Anthropic Teams Use Claude Code** - <https://www.anthropic.com/news/how-anthropic-teams-use-claude-code>
3. **Claude Directory Guide** - <https://htdocs.dev/posts/introducing-claude-your-ultimate-directory-for-claude-code-excellence/>

---

**Questions?** Reach out in `#ai-exchange`

**Course Version:** 1.0.0 | **Last Updated:** January 2025

*"The future of coding isn't replacing developers—it's amplifying their capabilities with 80-90% test coverage."* - DX Team
