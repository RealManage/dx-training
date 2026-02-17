# Week 1: Setup & Orientation - Trainer Runbook

## For Instructors

- [ ] Test installation on Windows (PowerShell native), macOS (curl), and Linux/WSL (curl)
- [ ] Prepare sample HOA codebase repository
- [ ] Test all demo commands
- [ ] Have `claude doctor` output examples ready
- [ ] Slack channel `#ai-exchange` monitored

## Instructor Notes

### Common Installation Issues & Solutions

**Windows:**

- Issue: "command not found" → Restart terminal; ensure [Git for Windows](https://git-scm.com/downloads/win) is installed
- Issue: WSL path issues → Add `~/.local/bin` to PATH in `~/.bashrc`
- Issue: `claude update` fails with WinGet → Use `winget upgrade Anthropic.ClaudeCode` instead

**macOS:**

- Issue: "command not found" → Restart terminal; ensure `~/.local/bin` is in PATH
- Issue: `claude update` fails with Homebrew → Use `brew upgrade claude-code` instead

**All Platforms:**

- Issue: Behind proxy -> See [Corporate Proxy Guide](https://code.claude.com/docs/en/corporate-proxy)
- Issue: VPN blocking -> Whitelist *.anthropic.com

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
