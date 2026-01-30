# Week 1: Setup & Orientation 🚀

**Duration:** 2 hours  
**Format:** In-person or virtual  
**Audience:** RealManage cross-functional team (engineers, PMs, support staff)

## 🎯 Learning Objectives

By the end of this session, participants will be able to:

- ✅ Install and authenticate Claude Code CLI
- ✅ Navigate the CLI with essential commands
- ✅ Ask effective questions about HOA codebases
- ✅ Create and manage CLAUDE.md memory files
- ✅ Identify 3+ use cases for their daily work

> **PM/Support Track Note:** Focus on observing and understanding the developer experience. You don't need to complete the coding exercises—watch a developer work through them instead. See [PM Quick-Start Guide](../../resources/quick-start-pm.md) for your customized path.

## 📋 Pre-Session Checklist

### System Requirements

- **OS:** Windows 10+, macOS 10.15+, or Ubuntu 20.04+
- **RAM:** 4GB minimum
- **.NET:** .NET 10 SDK required for C# exercises
- **Shell:** Bash, Zsh, PowerShell, or Fish
- **IDE:** VS Code with recommended extensions

### Step 1: Install Claude Code

Claude Code now uses native installers (npm method is deprecated).

#### Windows (Native)

```powershell
# In PowerShell (Run as Administrator recommended)
winget install Anthropic.ClaudeCode

# Or
# irm https://claude.ai/install.ps1 | iex

# Or From CMD terminal only
curl -fsSL https://claude.ai/install.cmd -o install.cmd && install.cmd && del install.cmd

# Verify installation
claude --version
```

#### Windows (WSL/Ubuntu)

```bash
# In WSL terminal
curl -fsSL https://claude.ai/install.sh | bash -s latest

# Add to PATH if needed
echo 'export PATH="$HOME/.local/bin:$PATH"' >> ~/.bashrc
source ~/.bashrc

# Verify installation
claude --version
```

#### macOS

```bash
# Option A: Homebrew (recommended)
brew install claude-code

# Option B: Direct install
curl -fsSL https://claude.ai/install.sh | bash -s latest

# Verify installation
claude --version
```

#### Linux

```bash
curl -fsSL https://claude.ai/install.sh | bash -s latest

# Add to PATH if needed
echo 'export PATH="$HOME/.local/bin:$PATH"' >> ~/.bashrc
source ~/.bashrc

# Verify installation
claude --version
```

### Step 2: Install .NET 10 SDK (for C# exercises)

```bash
# Verify .NET installation
dotnet --version  # Should show 10.x.x

# If not installed:
# Windows: Download from https://dotnet.microsoft.com/download/dotnet/10.0
# macOS: brew install dotnet-sdk
# Ubuntu/Debian: sudo apt-get update && sudo apt-get install -y dotnet-sdk-10.0
```

### Step 3: Install IDE Extensions

**For VS Code/Windsurf:**

1. Open Command Palette (Ctrl+Shift+P / Cmd+Shift+P)
2. Type "Extensions: Show Recommended Extensions"
3. Install available extensions from `.vscode/extensions.json`
4. Restart IDE

**For Cursor/Other IDEs:**
Install equivalent extensions:

- Claude/AI integration extension
- C# language support
- GitLab integration
- Markdown with Mermaid support
- .NET runtime support

Key extensions needed:

- **Claude Code/AI Assistant** - For AI integration
- **C# Language Support** - For IntelliSense and debugging
- **GitLab Workflow** - For merge requests and CI/CD
- **Markdown Preview** - For viewing course materials
- **.NET Runtime** - For running C# code

### Step 4: Pre-Session Verification

- [ ] Claude Code installed (`claude --version`)
- [ ] .NET 10 SDK installed (`dotnet --version`)
- [ ] VS Code with extensions installed
- [ ] Anthropic account for authentication
- [ ] Internet connection (no VPN initially)

## 📂 WHERE to Work

**Each week has its own `sandbox/` folder for your practice work.**

Understanding the folder structure is essential before diving in:

```
courses/ai-101-claude-code/
├── sessions/
│   ├── week-1/
│   │   ├── README.md      ← You're reading this!
│   │   ├── examples/      ← Reference code (READ-ONLY)
│   │   │   ├── hoa-cli/   ← Developer track example
│   │   │   └── support/   ← Support track example
│   │   └── sandbox/       ← YOUR WORK for Week 1
│   ├── week-2/
│   │   ├── example/       ← Reference code (READ-ONLY)
│   │   └── sandbox/       ← YOUR WORK for Week 2
│   └── ...
└── resources/             ← Reference materials (templates, prompts)
```

### Creating Your Sandbox

```bash
# Navigate to Week 1
cd courses/ai-101-claude-code/sessions/week-1

# Copy the example to your sandbox folder
mkdir -p sandbox
cp -r examples/hoa-cli sandbox/
cd sandbox/hoa-cli

# Start Claude Code and begin practicing!
claude
```

### Important Notes

- **`examples/` folders are READ-ONLY** - Don't modify them directly. Copy to `sandbox/` first.
- **Each week has its own `sandbox/`** - Your Week 1 work stays in `week-1/sandbox/`, Week 2 in `week-2/sandbox/`, etc.
- **Git ignores `sandbox/` folders** - Your practice code won't clutter the course repo history.
- **For weeks with multiple examples** - Create subdirectories: `sandbox/example-a/`, `sandbox/example-b/`

## 📚 Session Plan

### Part 1: Installation & Authentication (20 min)

#### 1.1 Welcome & Context (5 min)

```markdown
"Today we're learning Claude Code - An Agentic CLI tool that can act as an AI pair programmer,
a tech writer, a QA analyst, code reviewer, and much more. Unlike Copilot, Claude Code can read
entire codebases, write tests, and even fix its own bugs. By the end of today, you'll be using
Claude Code to navigate our C# APIs and Angular components."
```

> **Key Mindset:** Don't know something? Ask Claude. Whether it's writing user stories, understanding code, drafting documentation, analyzing test scenarios, or learning a new tool - Claude is your knowledge partner. You don't need to be an expert in everything; you need to know how to ask good questions. This course teaches you how to work *with* an AI to amplify what you already do well.

#### 1.2 Installation (10 min)

**Prerequisites Check:**

```bash
# Verify .NET is installed (for C# exercises)
dotnet --version  # Should be 10.x.x
```

**For Windows Users (Native):**

```powershell
# In PowerShell
winget install Anthropic.ClaudeCode

# Verify installation
claude --version
```

**For Windows Users (WSL/Ubuntu):**

```bash
# In WSL terminal
curl -fsSL https://claude.ai/install.sh | bash -s latest

# Verify installation
claude --version
```

**For Mac Users:**

```bash
# Homebrew (recommended)
brew install claude-code

# Verify installation
claude --version
```

**For Linux Users:**

```bash
curl -fsSL https://claude.ai/install.sh | bash -s latest

# Verify installation
claude --version
```

**If `claude` command not found after install:**

```bash
# Add to PATH
echo 'export PATH="$HOME/.local/bin:$PATH"' >> ~/.bashrc
source ~/.bashrc
```

#### 1.3 Verification & Authentication (5 min)

**Verify Installation:**

```bash
# Run the diagnostic tool
claude doctor

# Should show all green checks for:
# - Claude Code CLI version
# - Authentication status
# - Network connectivity
```

**Authenticate:**

```bash
# Start Claude - will open browser for OAuth
claude

# Authentication options:
# 1. Anthropic Console (Free)
# 2. Claude App with Pro/Max subscription
# 3. Enterprise platforms (Bedrock, Vertex)
```

**Troubleshooting Common Issues:**

**Claude not found after install:**

- Windows: Restart your terminal/PowerShell
- macOS/Linux: Run `source ~/.bashrc` or restart terminal
- Check PATH includes `~/.local/bin` or Claude install location

**Authentication Issues:**

- Temporarily disable VPN/firewall for initial setup
- Try `claude logout` then `claude` to re-authenticate
- Check your Anthropic account is active

**WSL Specific:**

- Ensure you're running commands inside WSL, not Windows
- If PATH issues persist, add to `~/.bashrc`: `export PATH="$HOME/.local/bin:$PATH"`

### Part 2: Tour of the CLI (20 min)

#### 2.1 Starting a Session (5 min)

```bash
# Navigate to week 1 directory
cd courses/ai-101-claude-code/sessions/week-1

# Start Claude in current directory
claude

# Claude now has context of your entire codebase!
```

#### 2.2 Essential Slash Commands (10 min)

**Live Demo - Type these with the class:**

```bash
# In Claude session
/help              # Show all available commands
/clear             # Clear conversation history (reset context)
/init              # Initialize a CLAUDE.md file for your project
/context           # View current context window size
/memory            # Edit CLAUDE.md file
/model             # Switch AI models
/permissions       # Control what Claude can modify
/doctor            # Check installation health
/exit              # Exit Claude session
```

**Additional Useful Commands:**

```bash
/compact           # Compress conversation to save tokens
/add-dir           # Add more directories to context
/mcp               # Add and manage MCP Servers
/status            # View account and system status
```

**Customize Your Learning Experience:**

```bash
/output-style      # Change how Claude communicates with you
```

| Style | Use When |
| ----- | -------- |
| **Default** | You need to get work done efficiently |
| **Explanatory** | You want to understand *why* Claude made certain choices |
| **Learning** | You want hands-on practice - Claude will ask YOU to write code |

> **Tip:** New to Claude or coding? Try **Learning** mode for the first few weeks to build muscle memory. You can switch styles anytime.

**Reference:** [Complete Slash Commands](https://docs.anthropic.com/en/docs/claude-code/slash-commands)

#### 2.3 Powerful CLI Arguments (5 min)

**Essential Arguments for Daily Use:**

```bash
# Full CLI arguments list
claude --help

# Continue your last conversation where you left off
claude -c
claude --continue

# Resume any previous conversation
claude -r          # Resume with selection
claude --resume    # Same as -r

# Switch models for different tasks
claude --model sonnet    # Default - excellent for daily development
claude --model opus      # Most capable - use for complex analysis & architecture

# Add directories Claude can access
claude --add-dir ../shared-lib ../api

# Debug mode to see what's happening
claude -d
claude --debug
```

**Permission Control:**

```bash
# Bypass all permission checks (sandboxes only!)
claude --dangerously-skip-permissions

# Start in plan mode (no auto-edits)
claude --permission-mode plan

# Allow specific tools only
claude --allowedTools "Read Edit"

# Deny dangerous operations
claude --disallowedTools "Bash(git:*)"
```

**MCP Server Management:**

```bash
# Configure MCP servers
claude mcp

# Load custom MCP config
claude --mcp-config ./my-mcp-config.json

# Check for updates and installs if available
claude update
```

**Pro Tips:**

- Use `-c` to quickly continue working
- Use `-r` to find old conversations
- Use `--add-dir` for multi-project work
- Use `--model opus` for architecture decisions

### Part 3: First Contact (30 min)

#### 3.1 Set Up Your Sandbox (5 min)

```bash
# Navigate to Week 1
cd courses/ai-101-claude-code/sessions/week-1

# Copy the developer example (hoa-cli) to your sandbox
mkdir -p sandbox
cp -r examples/hoa-cli sandbox/
cd sandbox/hoa-cli

# See what's here - modern C# with no Main() method!
ls -la

# Build and run the CLI app
dotnet build
dotnet run

# Try option 1: Calculate a fine
# Try option 2: View violations
# Notice option 3-5 aren't implemented yet!
# Exit with option 6

# Now start Claude to help improve it
claude
```

> **Note:** Week 1 has two example projects: `hoa-cli` (for developers) and `support` (for support staff). Choose the one that matches your role, or see [Hands-On Exercises](#part-5-hands-on-exercises-30-45-min) for role-specific tracks.

#### 3.2 Codebase Q&A Practice (15 min)

**Exercise - Ask these questions in Claude:**

```
> What does this example application do?

> Explain how top-level programs work in modern C#

> What features are marked as TODO in the code?

> How would you improve the code structure?

> What's a C# record and why is it used for Violation?
```

**What to Notice:**

- Claude reads all files in your workspace automatically
- Identifies bugs and TODOs
- Explains modern C# features
- Suggests improvements
- Shows file paths and line numbers

#### 3.3 Code Generation Demo (10 min)

**Live coding with the class - Fix and Extend the Example:**

```
> Find and fix the bug in CalculateFine then write a xUnit unit test to cover all cases

> Implement the GetOverdueViolations method that's marked TODO and add option "3" to the menu to show overdue violations

> Create a new method to save violations to a JSON file

> Add input validation to prevent the app from crashing on bad input

> Convert ViolationService methods to async/await pattern
```

**Watch Claude:**

- Fix the compounding calculation
- Implement missing features
- Add modern async patterns
- Suggest better architecture

### Part 4: CLAUDE.md and Memory (40 min)

#### 4.1 Memory Hierarchy (10 min)

```
~/.claude/CLAUDE.md           # User-level (all projects)
    ↓
./CLAUDE.md                   # Project-level (this project)
    ↓
./<nested-path(s)>/CLAUDE.md  # Nested project-level (this folder)
    ↓
Session Context               # Current conversation
```

**Key Points:**

- More specific memory overrides general
- Project memory persists across sessions
- User memory applies to all projects
- Session context resets on `/clear`

#### 4.2 Creating Project Memory (15 min)

**Method 1 - Quick Edit:**

```bash
# In Claude session, type just a hashtag:
#

# This opens your default editor for CLAUDE.md
```

**Method 2 - Command:**

```bash
/memory

# Opens CLAUDE.md in editor
```

**Method 3 - Direct Creation:**

```bash
# Outside Claude, create in project root:
echo "# Project Context" > CLAUDE.md
```

#### 4.3 RealManage CLAUDE.md Template (15 min)

**Create together as a class:**

```markdown
# RealManage HOA Management System

## Tech Stack
- **Backend:** C# (.NET 10), ASP.NET Core Web API
- **Frontend:** Angular 17, TypeScript, RxJS
- **Database:** SQL Server 2019, Entity Framework Core 10
- **Cloud:** Azure (App Service, SQL Database, Service Bus)
- **Auth:** Azure AD B2C

## Architecture Patterns
- Microservices: Violations, Payments, Residents, Communications
- Repository Pattern with Unit of Work
- CQRS for complex operations
- API Gateway via Azure API Management
- Event-driven with Azure Service Bus

## Coding Standards
- **Testing:** TDD with 80-90% coverage REQUIRED
- **C# Standards:**
  - xUnit for unit tests
  - FluentAssertions for assertions
  - Moq for mocking
  - Async/await for all I/O operations
  - Nullable reference types enabled
- **Angular Standards:**
  - Jasmine/Karma for tests
  - OnPush change detection
  - Strict TypeScript settings

## Domain Terminology
- **HOA:** Homeowners Association
- **Violation:** Rule infraction requiring tracking/fines
- **Dues:** Monthly/annual payments from residents
- **Board:** Elected officials making community decisions
- **CCR:** Covenants, Conditions & Restrictions
- **Architectural Review:** Approval process for modifications

## Common Tasks
- Calculate late fees with compound interest
- Track violation escalation workflows
- Generate board meeting reports
- Process bulk payment imports
- Send automated violation notices

## Security & Compliance
- Never log resident PII (SSN, bank accounts)
- All financial calculations need audit trails
- Follow SOC 2 Type II requirements
- GDPR compliance for EU residents
- PCI DSS for payment processing

## Performance Requirements
- API response time < 200ms (p95)
- Angular initial load < 3 seconds
- Database queries < 100ms
- Support 10,000 concurrent users

## Helpful Context
- Fiscal year starts July 1
- Late fees accrue monthly after 30-day grace period
- Violations escalate after 30, 60, 90 days
- Board meetings are monthly (first Tuesday)
```

**Test it works:**

```
> What's our testing coverage requirement?
# Should respond: 80-90% based on CLAUDE.md

> How do late fees work in our system?
# Should reference the 30-day grace period
```

### Part 5: Hands-On Exercises (30-45 min)

Now it's time to practice! Choose the exercise track that matches your role:

| Track | Best For | Focus Area |
| ----- | -------- | ---------- |
| [Developer](tracks/developer.md) | Engineers writing C# code | CLI debugging, code generation, TDD |
| [QA](tracks/qa.md) | QA engineers, testers | Test scenario design, coverage analysis |
| [PM](tracks/pm.md) | Product managers, analysts | Requirements documentation, user stories |
| [Support](tracks/support.md) | Support staff, customer success | Response drafting, template creation |

**Instructions:**

1. Click your track link above
2. Complete the 30-45 minute exercise
3. Return here for the wrap-up discussion

> **Note:** All tracks work toward the same learning objectives. You'll learn Claude Code fundamentals through the lens of your daily work.

---

### Part 6: Reflection & Practice (15-20 min)

#### 6.1 Group Discussion (5 min)

**Questions for the group:**

- What surprised you about Claude Code?
- Which feature would help most with your current work?
- Any concerns about using AI for production code?
- How does this compare to Copilot or ChatGPT?

#### 6.2 Quick Wins by Role (5 min)

**Support Team:**

```
> Summarize all error messages from the last 24 hours
> Draft a response to this homeowner complaint
```

**Product Managers:**

```
> Generate release notes from the last 10 commits
> Create user stories from this feature request
```

**Engineers:**

```
> Write unit tests for the PaymentService class
> Find all TODO comments in the codebase
```

**QA/Testing:**

```
> Generate test cases for the violation workflow
> Find all hardcoded values that should be configurable
```

## 🎯 Key Takeaways

### The Three Rules of Claude Code

1. **Context is King** - CLAUDE.md makes Claude smarter
2. **Be Specific** - Vague prompts = vague code
3. **Trust but Verify** - Always review generated code

### Context Management

- Use `/compact` to compress long conversations; supply optional instructions if you want to keep something specific
- Use `/clear` to start with a fresh conversation
- Check `/usage` to see your plan limits

### Best Practices

- Start sessions in project root
- Create project-specific CLAUDE.md files
- Use TDD approach (tests first!)
- Review changes with git diff before accepting changes

## 📝 Homework (Before Week 2)

### Required Tasks

1. ✅ Install Claude Code on your primary development machine
2. ✅ Run `claude doctor` and resolve any issues
3. ✅ Create a CLAUDE.md for one of your projects
4. ✅ Ask Claude 5 questions about your codebase
5. ✅ Share one "aha moment" in `#ai-exchange` Slack channel

### Stretch Goals

1. 🎯 Generate a complete CRUD service with tests
2. 🎯 Use Claude to review one of your recent PRs
3. 🎯 Create a custom slash command for your workflow

### Skill Check

Try this in your sandbox without help:

```
> Add a new ViolationAppeal class with these rules:
  - Appeals must be within 30 days of violation
  - Must include justification text
  - Can only appeal once per violation
  - Create a method to process appeals
  - Include unit tests
```

## 🔗 Resources

### Official Documentation

- [Setup Guide](https://docs.anthropic.com/en/docs/claude-code/setup)
- [System Requirements](https://docs.anthropic.com/en/docs/claude-code/setup#system-requirements)
- [CLI Reference](https://docs.anthropic.com/en/docs/claude-code/cli-reference)
- [Memory Management](https://docs.anthropic.com/en/docs/claude-code/memory)
- [Troubleshooting](https://docs.anthropic.com/en/docs/claude-code/troubleshooting)

### RealManage Resources

- [Week 1 Example CLI App](./examples/hoa-cli/) - Reference implementation
- [C# Coding Standards](https://wiki.realmanage.com/coding-standards)
- [Architecture Guidelines](https://wiki.realmanage.com/architecture)
- Slack: `#ai-exchange` for help

### Quick References

- [Slash Commands Cheatsheet](../../resources/cli-commands.md)
- [CLAUDE.md Template](../../resources/claude-md-template.md)
- [Common Prompts](../../resources/prompts/getting-started.md)

## 📊 Success Metrics

### You're ready for Week 2 when you can

- [ ] Start Claude Code in any project directory
- [ ] Use basic slash commands without looking them up
- [ ] Write a CLAUDE.md that provides context
- [ ] Generate working C# code with tests
- [ ] Manage context efficiently with /clear and /compact

### Red Flags (seek help if)

- [ ] Claude Code won't start after installation
- [ ] Authentication keeps failing
- [ ] Generated code has obvious errors
- [ ] Response quality degrading significantly

## 🚀 Next Week Preview

**Week 2: Prompting Foundations**

- Craft prompts that generate production-ready code
- Use XML tags for structured inputs
- Build a RealManage prompt library
- Master few-shot prompting for consistency

**Pre-work:** Think of 3 repetitive coding tasks you do weekly

---

*End of Week 1 Session Plan*
*Next Session: Week 2 - Prompting Foundations*
