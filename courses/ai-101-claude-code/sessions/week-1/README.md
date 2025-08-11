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

## 📋 Pre-Session Checklist

### System Requirements
- **OS:** Windows 10+, macOS 10.15+, or Ubuntu 20.04+
- **RAM:** 4GB minimum
- **.NET:** .NET 9 SDK required for C# development (supports top-level programs)
- **Node.js:** Version 18+ required, 22 LTS recommended (via nvm)
- **Shell:** Bash, Zsh, or Fish preferred
- **IDE:** VS Code with recommended extensions

### Step 1: Choose Your Windows Environment
Windows users must choose ONE option:

#### Option A: Git Bash (Recommended - Simplest)
- Install [Git for Windows](https://git-scm.com/download/win)
- Use Git Bash terminal for all commands
- Works out of the box for most users

#### Option B: WSL (Most Linux-like)
- Install WSL 1 or WSL 2 (both supported)
- Full Linux environment
- Best for Linux-familiar developers

#### Option C: PowerShell (Fallback only)
- Native Windows terminal
- May have compatibility issues
- Use only if Git Bash and WSL unavailable

### Step 2: Install Node Version Manager (nvm)

#### Windows - nvm-windows
```powershell
# Download nvm-windows from:
# https://github.com/coreybutler/nvm-windows/releases
# Run the installer (nvm-setup.exe)

# After installation, in NEW terminal:
# NOTE: the `--` prefix is not needed on Windows
nvm install lts
nvm use lts
node version  # Should show v22.x.x
```

#### WSL/macOS/Linux - Official nvm
```bash
# Install nvm
curl -o- https://raw.githubusercontent.com/nvm-sh/nvm/v0.40.1/install.sh | bash

# Reload shell configuration
source ~/.bashrc  # or ~/.zshrc for Zsh

# Install Node LTS (version 22)
nvm install --lts
nvm use --lts
node --version  # Should show v22.x.x
```

### Step 3: Install .NET 9 SDK
```bash
# Verify .NET installation
dotnet --version  # Should show 9.0.x

# If not installed, download from:
# https://dotnet.microsoft.com/download/dotnet/9.0
```

### Step 4: Install IDE Extensions

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

### Step 5: Pre-Session Verification
- [ ] .NET 9 SDK installed (`dotnet --version`)
- [ ] nvm installed and working (`nvm --version`)
- [ ] Node.js 22 LTS installed via nvm (`node --version`)
- [ ] npm 10+ available (`npm --version`)
- [ ] VS Code with extensions installed
- [ ] Terminal ready (Git Bash, WSL, or Terminal)
- [ ] Anthropic account for authentication
- [ ] Internet connection (no VPN initially)

### For Instructors
- [ ] Test installation on all platforms
- [ ] Have nvm troubleshooting guide ready
- [ ] Prepare sample HOA codebase repository
- [ ] Test all demo commands
- [ ] Have `claude doctor` output examples
- [ ] Slack channel `#dx-training` monitored

## 📚 Session Plan

### Part 1: Installation & Authentication (20 min)

#### 1.1 Welcome & Context (5 min)
```markdown
"Today we're learning Claude Code - An Agentic CLI tool that can act as an AI pair programmer,
a tech writer, a QA analyst, code reviewer, and much more. Unlike Copilot, Claude Code can read
entire codebases, write tests, and even fix its own bugs. By the end of today, you'll be using
Claude Code to navigate our C# APIs and Angular components."
```

#### 1.2 Installation (10 min)

**Prerequisites Check:**
```bash
# Verify nvm is working
nvm --version

# Verify Node.js 22 is active
node --version  # Should be 22.x.x

# Verify npm 10+
npm --version   # Should be 10.x.x or higher
```

**For Windows Users (Git Bash):**
```bash
# Open Git Bash (not PowerShell or CMD)
npm install -g @anthropic-ai/claude-code

# Verify installation
claude --version
```

**⚠️ Note on Portable Git:** If using a portable Git installation (not the standard installer), you may need to set this in PowerShell BEFORE opening Git Bash:
```powershell
# Only for PORTABLE Git - skip if you used the standard installer
$env:CLAUDE_CODE_GIT_BASH_PATH="C:\PortableGit\bin\bash.exe"
```

**For Windows Users (WSL):**
```bash
# In WSL terminal (Ubuntu/Debian)
npm install -g @anthropic-ai/claude-code

# Verify installation
claude --version
```

**For Mac/Linux Users:**
```bash
# In terminal
npm install -g @anthropic-ai/claude-code

# Verify installation
claude --version
```

⚠️ **Important:** 
- Do NOT use `sudo npm install -g` - causes permission issues
- If permission denied, check npm prefix: `npm config get prefix`
- Should be in your home directory, not system directories

#### 1.3 Verify Installation (2 min)
```bash
# Run the diagnostic tool
claude doctor

# Should show all green checks for:
# - Node.js version
# - npm installation
# - Claude Code CLI
# - Network connectivity
```

#### 1.4 Authentication (3 min)
```bash
# Start Claude - will open browser for OAuth
claude

# Authentication options:
# 1. Anthropic Console (Free)
# 2. Claude App with Pro/Max subscription
# 3. Enterprise platforms (Bedrock, Vertex)
```

**Troubleshooting Common Issues:**

**nvm Issues:**
- `nvm: command not found` (Windows): Close and reopen terminal after nvm-windows install
- `nvm: command not found` (Mac/Linux): Run `source ~/.bashrc` or restart terminal
- `nvm use` fails: May need to run terminal as Administrator (Windows only)

**npm/Claude Issues:**
- `claude: command not found`: Check PATH includes npm global bin
  - Windows Git Bash: `echo $PATH` should include `/c/Users/YOUR_NAME/AppData/Roaming/npm`
  - Mac/Linux: Should include `~/.npm-global/bin` or similar
- Permission denied: Never use sudo; check `npm config get prefix`
- Auth fails: Temporarily disable VPN/firewall for initial setup

**Git Bash Specific:**
- Use Git Bash, not git-bash.exe directly
- If commands hang, try winpty: `winpty claude`
- Portable Git users only: Set `$env:CLAUDE_CODE_GIT_BASH_PATH` in PowerShell first

### Part 2: Tour of the CLI (20 min)

#### 2.1 Starting a Session (5 min)
```bash
# Navigate to a project directory
cd ~/projects/realmanage-api

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
/cost              # Check token usage and costs
/memory            # Edit CLAUDE.md file
/permissions       # Control what Claude can modify
/doctor            # Check installation health
# Exit with Ctrl+C or Ctrl+D (no /exit command)
```

**Additional Useful Commands:**
```bash
/review            # Request code review
/compact           # Compress conversation to save tokens
/add-dir           # Add more directories to context
/model             # Switch AI models
/status            # View account and system status
```

**Reference:** [Complete Slash Commands](https://docs.anthropic.com/en/docs/claude-code/slash-commands)

#### 2.3 Understanding Costs (5 min)

**Token Pricing (Claude Sonnet 4 - as of 2025):**
- Input: $0.003 per 1K tokens
- Output: $0.015 per 1K tokens  
- Cache writes (5min): $0.00375 per 1K tokens
- Cache hits: $0.0003 per 1K tokens

**Actual Usage Costs:**
- **Average daily cost:** $6 per developer
- **90% of users:** Under $12 per day
- **Monthly team usage:** $100-200 per developer
- **Background processes:** ~$0.04 per session

```bash
# Monitor usage during session
/cost

# For Claude Max subscribers:
# "No need to monitor cost — your subscription includes Claude Code usage"

# For API users shows:
# - Tokens used this session
# - Estimated cost  
# - Cache hit rate
```

**Note:** Claude Max subscription includes works on 5 hours sessions and the number of messages sent. It does not report costs metrics!

[Detailed Cost Guide](https://docs.anthropic.com/en/docs/claude-code/costs)

### Part 3: First Queries (30 min)

#### 3.1 Explore the Sandbox CLI App (5 min)
```bash
# Navigate to sandbox (already in week-1 directory)
cd sandbox

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

#### 3.2 Codebase Q&A Practice (15 min)

**Exercise - Ask these questions in Claude:**
```
> What does this sandbox application do?

> Explain how top-level programs work in modern C#

> What's the bug in the CalculateFine method?

> What features are marked as TODO in the code?

> How would you improve the code structure?

> What's a C# record and why is it used for Violation?
```

**What to Notice:**
- Claude reads all files in the sandbox automatically
- Identifies bugs and TODOs
- Explains modern C# features
- Suggests improvements
- Shows file paths and line numbers

#### 3.3 Code Generation Demo (10 min)

**Live coding with the class - Fix and Extend the Sandbox:**
```
> Fix the bug in CalculateFine so it compounds 10% monthly

> Implement the GetOverdueViolations method that's marked TODO

> Add option 3 to the menu to show overdue violations

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
~/.claude/CLAUDE.md         # User-level (all projects)
    ↓
./CLAUDE.md                 # Project-level (this project)
    ↓
Session Context             # Current conversation
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
- **Backend:** C# (.NET 8), ASP.NET Core Web API
- **Frontend:** Angular 17, TypeScript, RxJS
- **Database:** SQL Server 2019, Entity Framework Core 8
- **Cloud:** Azure (App Service, SQL Database, Service Bus)
- **Auth:** Azure AD B2C

## Architecture Patterns
- Microservices: Violations, Payments, Residents, Communications
- Repository Pattern with Unit of Work
- CQRS for complex operations
- API Gateway via Azure API Management
- Event-driven with Azure Service Bus

## Coding Standards
- **Testing:** TDD with 95-100% coverage REQUIRED
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
# Should respond: 95-100% based on CLAUDE.md

> How do late fees work in our system?
# Should reference the 30-day grace period
```

### Part 5: Reflection & Practice (10 min)

#### 5.1 Group Discussion (5 min)

**Questions for the group:**
- What surprised you about Claude Code?
- Which feature would help most with your current work?
- Any concerns about using AI for production code?
- How does this compare to Copilot or ChatGPT?

#### 5.2 Quick Wins by Role (5 min)

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

### Cost Management
- Use `/clear` when switching contexts  
- Monitor with `/cost` regularly
- Cache helps reduce costs significantly

### Best Practices
- Start sessions in project root
- Create project-specific CLAUDE.md files
- Use TDD approach (tests first!)
- Review changes with `/diff` before accepting

## 📝 Homework (Before Week 2)

### Required Tasks:
1. ✅ Install Claude Code on your primary development machine
2. ✅ Run `claude doctor` and resolve any issues
3. ✅ Create a CLAUDE.md for one of your projects
4. ✅ Ask Claude 5 questions about your codebase
5. ✅ Share one "aha moment" in `#dx-training` Slack channel

### Stretch Goals:
1. 🎯 Generate a complete CRUD service with tests
2. 🎯 Use Claude to review one of your recent PRs
3. 🎯 Create a custom slash command for your workflow

### Skill Check:
Try this in the sandbox without help:
```
> Add a new ViolationAppeal class to the sandbox with these rules:
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
- [Week 1 Sandbox CLI App](./sandbox/) - Your practice space
- [C# Coding Standards](https://wiki.realmanage.com/coding-standards)
- [Architecture Guidelines](https://wiki.realmanage.com/architecture)
- Slack: `#dx-training` for help

### Quick References
- [Slash Commands Cheatsheet](../../resources/cli-commands.md)
- [CLAUDE.md Template](../../exercises/week-1/claude-md-template.md)
- [Common Prompts](../../resources/prompts/getting-started.md)

## 📊 Success Metrics

### You're ready for Week 2 when you can:
- [ ] Start Claude Code in any project directory
- [ ] Use basic slash commands without looking them up
- [ ] Write a CLAUDE.md that provides context
- [ ] Generate working C# code with tests
- [ ] Estimate token costs for your queries

### Red Flags (seek help if):
- [ ] Claude Code won't start after installation
- [ ] Authentication keeps failing
- [ ] Generated code has obvious errors
- [ ] Costs seem unusually high (>$5/hour)

## 🚀 Next Week Preview

**Week 2: Prompting Foundations**
- Craft prompts that generate production-ready code
- Use XML tags for structured inputs
- Build a RealManage prompt library
- Master few-shot prompting for consistency

**Pre-work:** Think of 3 repetitive coding tasks you do weekly

---

## Instructor Notes

### Common Installation Issues & Solutions

**Windows Git Bash:**
- Issue: "command not found" → Solution: Restart Git Bash after npm install
- Issue: Path errors → Solution: Set `CLAUDE_CODE_GIT_BASH_PATH` environment variable

**macOS:**
- Issue: Permission denied → Solution: Check npm prefix with `npm config get prefix`
- Issue: Node too old → Solution: Use nvm to manage versions

**All Platforms:**
- Issue: Behind proxy → See [Corporate Proxy Guide](https://docs.anthropic.com/en/docs/claude-code/corporate-proxy)
- Issue: VPN blocking → Whitelist *.anthropic.com

### Time Management Tips
- Installation: 20 min (have helpers for Windows users)
- Memory section: Critical - full 40 min needed
- If running late: Skip code generation demo
- Always leave 10 min for Q&A

### Engagement Strategies
- Let participants drive queries
- Share a "failed" prompt and fix it together
- Celebrate first successful code generation
- Use real RealManage code examples

### Assessment
Quick poll at end:
1. Confidence level (1-5) with Claude Code
2. Most useful feature learned
3. Biggest concern/question
4. Will you use it this week? Y/N

---

*End of Week 1 Session Plan*
*Next Session: Week 2 - Prompting Foundations*