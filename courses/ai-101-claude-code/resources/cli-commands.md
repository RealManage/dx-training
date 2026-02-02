# Claude Code CLI Commands Quick Reference 🎯

> **Official Documentation:**
>
> - [CLI Reference](https://code.claude.com/docs/en/cli-reference) - All CLI flags
> - [Interactive Mode](https://code.claude.com/docs/en/interactive-mode) - Slash commands
>
> **Note:** The official docs are incomplete. This list is from Claude Code v2.1.17 `/help` output.

---

## Starting & Stopping

| Action | Command | Notes |
| ------ | ------- | ----- |
| Start Claude | `claude` | Opens interactive session |
| Start with prompt | `claude "your prompt"` | Start with initial query |
| Exit Claude | `Ctrl+C` or `/exit` | End session |
| Check version | `claude --version` | From terminal |
| Health check | `claude doctor` | From terminal |

---

## Complete Slash Commands (47 total)

### Session Management

| Command | Description |
| ------- | ----------- |
| `/help` | Show help and available commands |
| `/clear` | Clear conversation history and free up context |
| `/compact` | Clear history but keep summary. Optional: `/compact [focus instructions]` |
| `/context` | Visualize current context usage as colored grid |
| `/exit` | Exit the REPL |
| `/fork` | Create a fork of conversation at this point |
| `/rename` | Rename current conversation |
| `/resume` | Resume a previous conversation |
| `/rewind` | Restore code and/or conversation to previous point |
| `/export` | Export conversation to file or clipboard |

### Usage & Status

| Command | Description |
| ------- | ----------- |
| `/usage` | Show plan usage limits |
| `/stats` | Show usage statistics and activity |
| `/status` | Show version, model, account, API connectivity |

### Model & Output

| Command | Description |
| ------- | ----------- |
| `/model` | Set the AI model for Claude Code |
| `/output-style` | Set output style directly or from menu |
| `/plan` | Enable plan mode or view current session plan |

### File & Directory Management

| Command | Description |
| ------- | ----------- |
| `/add-dir` | Add a new working directory |
| `/memory` | Edit Claude memory files (CLAUDE.md) |
| `/init` | Initialize CLAUDE.md with codebase documentation |
| `/permissions` | Manage allow & deny tool permission rules |

### Code Review & Security

| Command | Description |
| ------- | ----------- |
| `/review` | Review a pull request |
| `/pr-comments` | Get comments from a GitHub pull request |
| `/security-review` | Security review of pending changes on current branch |

### Agents, Skills & Automation

| Command | Description |
| ------- | ----------- |
| `/agents` | Manage agent configurations |
| `/skills` | List available skills |
| `/hooks` | Manage hook configurations for tool events |
| `/tasks` | List and manage background tasks |
| `/todos` | List current todo items |

### Configuration & Settings

| Command | Description |
| ------- | ----------- |
| `/config` | Open config panel |
| `/doctor` | Diagnose and verify installation and settings |
| `/theme` | Change the color theme |
| `/statusline` | Set up Claude Code's status line UI |
| `/terminal-setup` | Install Shift+Enter key binding for newlines |
| `/sandbox` | Configure sandbox settings |

### Integrations

| Command | Description |
| ------- | ----------- |
| `/mcp` | Manage MCP servers |
| `/plugin` | Manage Claude Code plugins |
| `/ide` | Manage IDE integrations and show status |
| `/chrome` | Claude in Chrome (Beta) settings |
| `/install-github-app` | Set up Claude GitHub Actions for a repository |
| `/install-slack-app` | Install the Claude Slack app |

### Account & Support

| Command | Description |
| ------- | ----------- |
| `/login` | Sign in with your Anthropic account |
| `/logout` | Sign out from your Anthropic account |
| `/feedback` | Submit feedback about Claude Code |
| `/upgrade` | Upgrade to Max for higher rate limits |
| `/release-notes` | View release notes |
| `/mobile` | Show QR code to download Claude mobile app |
| `/stickers` | Order Claude Code stickers 🎉 |

### Remote Sessions

| Command | Description |
| ------- | ----------- |
| `/teleport` | Resume a remote session from claude.ai |
| `/remote-env` | Configure default remote environment for teleport |

---

## Quick Input Modes

| Prefix | Mode | Example |
| ------ | ---- | ------- |
| `/` | Commands/Skills | `/help`, `/model sonnet` |
| `!` | Bash mode | `!git status` (runs directly without Claude) |
| `@` | File autocomplete | `@src/` triggers path completion |
| `#` | Edit CLAUDE.md | Opens memory file |

---

## Keyboard Shortcuts

| Shortcut | Action |
| -------- | ------ |
| `Ctrl+C` | Exit Claude |
| `Ctrl+D` | Exit Claude (alternative) |
| `Ctrl+R` | Reverse history search |
| `Ctrl+T` | Toggle task list view |
| `?` | Show available shortcuts |
| `Tab` | Autocomplete |
| `Shift+Enter` | Newline (after `/terminal-setup`) |

---

## CLI Flags (from `claude --help`)

### Session & Model

| Flag | Short | Description |
| ---- | ----- | ----------- |
| `--model <model>` | | Set model (`sonnet`, `opus`, or full name) |
| `--agent <name>` | | Use specific agent |
| `--agents <json>` | | Define custom agents via JSON |
| `--continue` | `-c` | Continue most recent conversation |
| `--resume [id]` | `-r` | Resume by session ID or open picker |
| `--fork-session` | | Create new session ID when resuming |
| `--session-id <uuid>` | | Use specific session UUID |

### Output Modes

| Flag | Short | Description |
| ---- | ----- | ----------- |
| `--print` | `-p` | Print response and exit (for pipes/scripts) |
| `--output-format <fmt>` | | `text`, `json`, or `stream-json` |
| `--input-format <fmt>` | | `text` or `stream-json` |
| `--json-schema <schema>` | | JSON Schema for structured output |
| `--verbose` | | Verbose logging |
| `--debug [filter]` | `-d` | Debug mode with optional filtering |

### Permissions & Tools

| Flag | Description |
| ---- | ----------- |
| `--permission-mode <mode>` | `default`, `acceptEdits`, `dontAsk`, `plan`, `bypassPermissions` |
| `--dangerously-skip-permissions` | Bypass all permission checks |
| `--allowedTools <tools>` | Tools to allow (e.g., `"Bash(git:*) Edit"`) |
| `--disallowedTools <tools>` | Tools to deny |
| `--tools <tools>` | Specify available tools (`""` for none, `"default"` for all) |

### Files & Directories

| Flag | Description |
| ---- | ----------- |
| `--add-dir <dirs>` | Additional directories to allow access |
| `--file <specs>` | Files to download at startup (`file_id:path`) |

### Configuration

| Flag | Description |
| ---- | ----------- |
| `--settings <file\|json>` | Load settings from file or JSON string |
| `--setting-sources <sources>` | Sources to load: `user`, `project`, `local` |
| `--system-prompt <prompt>` | Custom system prompt |
| `--append-system-prompt <text>` | Append to default system prompt |
| `--disable-slash-commands` | Disable all skills |

### Integrations

| Flag | Description |
| ---- | ----------- |
| `--plugin-dir <paths>` | Load plugins from directories |
| `--mcp-config <configs>` | Load MCP servers from JSON |
| `--strict-mcp-config` | Only use MCP from --mcp-config |
| `--chrome` / `--no-chrome` | Enable/disable Chrome integration |
| `--ide` | Auto-connect to IDE on startup |

### Budget & Limits

| Flag | Description |
| ---- | ----------- |
| `--max-budget-usd <amount>` | Max spend on API calls (with `--print`) |
| `--fallback-model <model>` | Fallback when default overloaded |

### Info

| Flag | Short | Description |
| ---- | ----- | ----------- |
| `--version` | `-v` | Show version |
| `--help` | `-h` | Show help |

### CLI Subcommands

```bash
claude doctor        # Check installation health
claude update        # Check for and install updates
claude mcp           # Configure MCP servers
claude plugin        # Manage plugins
claude install       # Install native build
claude setup-token   # Set up auth token (requires subscription)
```

---

## Common Workflows

### Starting a New Project

```bash
cd my-project
claude
/init                 # Create CLAUDE.md
/permissions          # Review file access
# Start coding...
```

### Long Coding Session

```bash
claude
# Work for a while...
/compact              # Compress context if responses slow
/clear                # Reset when switching tasks
```

### Code Review Workflow

```bash
claude
/review               # Review current PR
/pr-comments          # Get existing PR comments
/security-review      # Security audit
```

### Multi-Session Work

```bash
claude
/fork                 # Branch conversation
/rename "feature-x"   # Name it
# Later...
/resume               # Pick up where you left off
```

---

## Memory Hierarchy

```
~/.claude/CLAUDE.md    # User level (all projects)
./CLAUDE.md            # Project level (current project)
Session context        # Current conversation
```

---

## Pro Tips 💡

1. **Use `/compact` liberally** - Keeps context summary while freeing tokens
2. **`/context` is your friend** - Visualize what's eating your context
3. **`/fork` before experiments** - Easy rollback point
4. **`!` for quick bash** - `!git status` skips Claude interpretation
5. **`/feedback` not `/bug`** - Report issues the right way

---

## Troubleshooting

| Issue | Command |
| ----- | ------- |
| Slow responses | `/compact` or `/clear` |
| Quality degrading | `/clear` and start fresh |
| Installation issues | `/doctor` or `claude doctor` |
| Wrong model | `/model` to switch |
| Permission errors | `/permissions` to review |
| Check connectivity | `/status` |

---

*Complete command list from Claude Code v2.1.17 | Last verified: January 2026*
