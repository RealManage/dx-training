# Claude Code CLI Commands Quick Reference 🎯

## Starting & Stopping

| Action | Command | Notes |
|--------|---------|-------|
| Start Claude | `claude` | Opens interactive session |
| Exit Claude | `Ctrl+C` or `Ctrl+D` | `/exit` command |
| Check version | `claude --version` | From terminal (not in session) |
| Health check | `claude doctor` | From terminal |

## Essential Session Commands

| Command | Description | Example |
|---------|-------------|---------|
| `/help` | Show all available commands | Just type `/help` |
| `/clear` | Clear conversation history | Resets context, saves tokens |
| `/cost` | Show token usage and costs | Monitor spending |
| `/memory` | Edit CLAUDE.md file | Opens in default editor |
| `/permissions` | View/update file permissions | Control what Claude can modify |
| `/doctor` | Check installation health | Diagnose issues |

## Working with Code

| Command | Description | When to Use |
|---------|-------------|-------------|
| `/review` | Request code review | After generating code |
| `/compact` | Compress conversation | When context gets too large |
| `/add-dir` | Add more directories | Need files from other folders |
| `/init` | Initialize project | Creates CLAUDE.md template |

## Advanced Commands

| Command | Description | Usage |
|---------|-------------|-------|
| `/model` | Switch AI models | Choose different Claude version |
| `/status` | View account status | Check limits and availability |
| `/config` | View/modify configuration | Adjust settings |
| `/mcp` | Manage MCP servers | External tool integration |
| `/agents` | Manage AI subagents | Specialized task automation |

## Account Management

| Command | Description |
|---------|-------------|
| `/login` | Switch Anthropic accounts |
| `/logout` | Sign out from account |
| `/bug` | Report bugs to Anthropic |

## Special Modes

| Command | Description | How to Exit |
|---------|-------------|-------------|
| `/vim` | Enter vim mode | Type `/vim` again to toggle |
| `/terminal-setup` | Install Shift+Enter binding | One-time setup |


## Cost Management Tips

### Monitor Usage
```bash
# Check current session cost
/cost

# Clear context to reset tokens
/clear

# Compress long conversations
/compact {optional instructions for what to keep during compaction}
```

### Token Saving Strategies
1. Use `/clear` when switching tasks
2. Use `/compact` for long sessions
3. Keep CLAUDE.md concise
4. Be specific in prompts to avoid iterations

## Memory Management

### Create/Edit CLAUDE.md
```bash
# Quick edit (in session)
/memory

# Or use hashtag shortcut
#
```

### Memory Hierarchy
```
~/.claude/CLAUDE.md    # User level (all projects)
./CLAUDE.md           # Project level (current project)
Session context       # Current conversation
```

## Common Workflows

### Starting a New Project
```bash
cd my-project
claude
/init                 # Create CLAUDE.md template
/permissions          # Review file access
# Start coding...
```

### Long Coding Session
```bash
claude
# Work for a while...
/cost                 # Check token usage
/compact              # Compress if needed
# Continue working...
/clear                # Reset when switching tasks
```

### Code Review
```bash
# After generating code
/review               # Request review
/cost                 # Check cost impact
```

## Keyboard Shortcuts

| Shortcut | Action | Context |
|----------|--------|---------|
| `Ctrl+C` | Exit Claude | Any time |
| `Ctrl+D` | Exit Claude | Any time |
| `#` | Edit CLAUDE.md | Start of line |
| `Tab` | Autocomplete | While typing |

## Pro Tips 💡

1. **No need to save** - All changes are automatic
2. **Use `/clear` liberally** - Resets context and saves money
3. **`/compact` for long sessions** - Preserves context while reducing tokens
4. **Check `/cost` regularly** - Avoid surprises
5. **Keep CLAUDE.md updated** - Better context = better code

## Troubleshooting

| Issue | Command to Use |
|-------|---------------|
| High costs | `/cost` then `/clear` |
| Slow responses | `/compact` or `/clear` |
| Installation issues | Exit and run `claude doctor` |
| Wrong model | `/model` to switch |
| Permission errors | `/permissions` to review |

---

*Last updated for Claude Code v1.x | Valid as of January 2025*