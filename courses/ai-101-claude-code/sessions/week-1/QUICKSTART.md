# Week 1 Quick Start

**Time:** 15-20 minutes | **Audience:** Developers who just need the essentials

---

## 1. Install Claude Code

**macOS / Linux / WSL:**

```bash
curl -fsSL https://claude.ai/install.sh | bash
claude --version
```

**Windows (PowerShell):**

```powershell
irm https://claude.ai/install.ps1 | iex
claude --version
```

> **Windows native** requires [Git for Windows](https://git-scm.com/downloads/win). Alternatively, use WSL with the macOS/Linux command above.

## 2. Verify & Authenticate

```bash
claude doctor    # Check installation health
claude           # Opens browser for OAuth login
```

## 3. Install .NET 10 SDK (for C# exercises)

> **Local Admin required:** .NET SDK installation requires administrator permissions. Contact Desktop Support if you don't have local admin access.

```bash
dotnet --version   # Should show 10.x.x
# If not installed: https://dotnet.microsoft.com/download/dotnet/10.0
```

## 4. Know Where to Work

```text
courses/ai-101-claude-code/sessions/week-1/
├── examples/   <- Reference (READ-ONLY)
│   ├── hoa-cli/    <- Developer track
│   └── support/    <- Support track
├── sandbox/    <- YOUR WORK GOES HERE
└── README.md   <- Full lesson
```

**Create your workspace:**

```bash
cd courses/ai-101-claude-code/sessions/week-1
cp -r examples/hoa-cli sandbox/
cd sandbox
```

## 5. Start Claude & Practice

```bash
claude                    # Start a session
```

**Try these commands:**

```text
/help       # See all commands
/memory     # Edit CLAUDE.md
/clear      # Reset conversation
/compact    # Compress context
```

**Ask Claude:**

```text
> What does this code do?
> Find the bug in CalculateFine
> Write a unit test for ViolationService
```

## 6. Create Project Memory

```bash
# In Claude session, type:
/init

# Or create manually:
echo "# My HOA Project" > CLAUDE.md
```

---

## Essential Commands Reference

| Command | Purpose |
| ------- | ------- |
| `claude` | Start session |
| `claude -c` | Continue last session |
| `claude -r` | Resume any past session |
| `claude doctor` | Health check |
| `/help` | Show all slash commands |
| `/clear` | Reset conversation |
| `/memory` | Edit CLAUDE.md |

---

## Troubleshooting

| Problem | Solution |
| ------- | -------- |
| `command not found` | Restart terminal; ensure `~/.local/bin` is in PATH |
| Auth fails | Disable VPN temporarily |
| Windows: Claude won't start | Install [Git for Windows](https://git-scm.com/downloads/win) |
| WinGet: `claude update` fails | Use `winget upgrade Anthropic.ClaudeCode` instead |
| Homebrew: `claude update` fails | Use `brew upgrade claude-code` instead |

---

## Next Steps

- Read full [README.md](./README.md) for detailed exercises
- Create CLAUDE.md for your own projects
- Join `#ai-exchange` on Slack

---

*Ready for more? See the full Week 1 README for hands-on exercises and CLAUDE.md templates.*
