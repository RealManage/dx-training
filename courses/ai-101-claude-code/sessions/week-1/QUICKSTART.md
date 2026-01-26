# Week 1 Quick Start

**Time:** 15-20 minutes | **Audience:** Developers who just need the essentials

---

## 1. Install Prerequisites

```bash
# Install Node.js 22 via nvm
nvm install --lts && nvm use --lts

# Verify
node --version   # Should be 22.x.x
```

## 2. Install Claude Code

```bash
npm install -g @anthropic-ai/claude-code
claude --version
```

## 3. Verify & Authenticate

```bash
claude doctor    # Check installation health
claude           # Opens browser for OAuth login
```

## 4. Know Where to Work

```
courses/ai-101-claude-code/sessions/week-1/
├── example/    ← Reference (READ-ONLY)
├── sandbox/    ← YOUR WORK GOES HERE
└── README.md   ← Full lesson
```

**Create your workspace:**
```bash
cd courses/ai-101-claude-code/sessions/week-1
cp -r example sandbox
cd sandbox
```

## 5. Start Claude & Practice

```bash
claude                    # Start a session
```

**Try these commands:**
```
/help       # See all commands
/memory     # Edit CLAUDE.md
/clear      # Reset conversation
/compact    # Compress context
```

**Ask Claude:**
```
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
|---------|---------|
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
|---------|----------|
| `command not found` | Restart terminal after install |
| Permission denied | Don't use `sudo`; check `npm config get prefix` |
| Auth fails | Disable VPN temporarily |
| Windows issues | Use Git Bash, not PowerShell |

---

## Next Steps

- Read full [README.md](./README.md) for detailed exercises
- Create CLAUDE.md for your own projects
- Join `#dx-training` on Slack

---

*Ready for more? See the full Week 1 README for hands-on exercises and CLAUDE.md templates.*
