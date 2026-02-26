# Claude Code Slash Commands Reference

> **Source:** Captured from raw terminal output, February 2026. This is the definitive list of built-in slash commands available in the current version of Claude Code.

## Session Management

| Command | Description |
| ------- | ----------- |
| `/clear` | Clear conversation history and free up context |
| `/compact` | Clear conversation history but keep a summary in context. Optional: `/compact [instructions for summarization]` |
| `/context` | Visualize current context usage as a colored grid |
| `/exit` | Exit the REPL |
| `/fork` | Create a fork of the current conversation at this point |
| `/rename` | Rename the current conversation |
| `/resume` | Resume a previous conversation |
| `/rewind` | Restore the code and/or conversation to a previous point |

## Model & Output

| Command | Description |
| ------- | ----------- |
| `/model` | Set the AI model for Claude Code |
| `/output-style` | Set the output style directly or from a selection menu |
| `/fast` | Toggle fast mode (Opus 4.6 only) |
| `/theme` | Change the theme |

## Planning & Review

| Command | Description |
| ------- | ----------- |
| `/plan` | Enable plan mode or view the current session plan |
| `/review` | Review a pull request |
| `/pr-comments` | Get comments from a GitHub pull request |
| `/security-review` | Complete a security review of the pending changes on the current branch |
| `/diff` | View uncommitted changes and per-turn diffs |

## Usage & Status

| Command | Description |
| ------- | ----------- |
| `/stats` | Show your Claude Code usage statistics and activity |
| `/usage` | Show plan usage limits |
| `/status` | Show Claude Code status including version, model, account, API connectivity, and tool statuses |
| `/doctor` | Diagnose and verify your Claude Code installation and settings |

## Configuration

| Command | Description |
| ------- | ----------- |
| `/config` | Open config panel |
| `/permissions` | Manage allow & deny tool permission rules |
| `/add-dir` | Add a new working directory |
| `/keybindings` | Open or create your keybindings configuration file |
| `/terminal-setup` | Install Shift+Enter key binding for newlines |
| `/statusline` | Set up Claude Code's status line UI |
| `/vim` | Toggle between Vim and Normal editing modes |
| `/extra-usage` | Configure extra usage to keep working when limits are hit |
| `/remote-env` | Configure the default remote environment for teleport sessions |
| `/sandbox` | Configure sandbox settings |

## Skills, Plugins & Agents

| Command | Description |
| ------- | ----------- |
| `/skills` | List available skills |
| `/plugin` | Manage Claude Code plugins |
| `/agents` | Manage agent configurations |
| `/hooks` | Manage hook configurations for tool events |
| `/mcp` | Manage MCP servers |

## Project Setup

| Command | Description |
| ------- | ----------- |
| `/init` | Initialize a new CLAUDE.md file with codebase documentation |
| `/memory` | Edit Claude memory files |

## Utilities

| Command | Description |
| ------- | ----------- |
| `/help` | Show help and available commands |
| `/copy` | Copy Claude's last response or a code block to clipboard |
| `/export` | Export the current conversation to a file or clipboard |
| `/debug` | Debug your current Claude Code session by reading the session debug log |
| `/insights` | Generate a report analyzing your Claude Code sessions |
| `/todos` | List current todo items |
| `/tasks` | List and manage background tasks |
| `/feedback` | Submit feedback about Claude Code |
| `/release-notes` | View release notes |

## Account & Integrations

| Command | Description |
| ------- | ----------- |
| `/login` | Sign in with your Anthropic account |
| `/logout` | Sign out from your Anthropic account |
| `/upgrade` | Upgrade to Max for higher rate limits and more Opus |
| `/ide` | Manage IDE integrations and show status |
| `/chrome` | Claude in Chrome (Beta) settings |
| `/install-github-app` | Set up Claude GitHub Actions for a repository |
| `/install-slack-app` | Install the Claude Slack app |
| `/mobile` | Show QR code to download the Claude mobile app |
| `/stickers` | Order Claude Code stickers |

---

*57 commands total. Last verified: February 2026.*
