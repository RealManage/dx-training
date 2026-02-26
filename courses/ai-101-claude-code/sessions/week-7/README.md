# Week 7: Plugins - The Complete Package

**Duration:** 2 hours
**Format:** In-person or virtual
**Audience:** RealManage cross-functional team (engineers, PMs, QA, support staff)

## Learning Tracks

This week has role-specific tracks:

- **Developer Track** - Build and distribute plugins with skills, agents, and hooks (this README)
- **[QA Track](./tracks/qa.md)** - Test plugins and build quality automation packages
- **[PM Track](./tracks/pm.md)** - Design plugin workflows and plan automation strategy
- **[Support Track](./tracks/support.md)** - Design and build a support automation plugin

---

## Learning Objectives

By the end of this session, participants will be able to:

- Understand Plugins as the packaging system for all Claude Code components
- Create plugins that contain skills, agents, and hooks
- Build skills that leverage custom agents and embedded hooks
- Use dynamic context injection in skills
- Distribute plugins via marketplace or local installation
- Design a complete RealManage automation plugin

## Pre-Session Checklist

### For Participants

- [ ] Completed Week 6 (agents, hooks, lifecycle events)
- [ ] Created at least one custom skill or command (`.claude/skills/` or `.claude/commands/`)
- [ ] Created at least one custom subagent in `.claude/agents/`
- [ ] Understanding of hook configuration
- [ ] Ready for 2-hour session

---

## Session Plan

### Part 1: Plugin Architecture (20 min)

#### 1.1 Plugins: The Complete Package (10 min)

In previous weeks, you learned about standalone components:

- **Skills** (`.claude/skills/<name>/SKILL.md`) - Reusable prompt templates with frontmatter
- **Custom Subagents** (`.claude/agents/`) - Specialized agents
- **Hooks** (`settings.json`) - Lifecycle event handlers

> **Note:** Custom slash commands (`.claude/commands/<name>.md`) have been **merged into skills**. Both create `/name` and work the same way. Skills are the recommended approach because they support a directory structure with templates, scripts, and other supporting files.

**Plugins package ALL of these** into a distributable unit.

```text
Plugin = Skills + Agents + Hooks (packaged together)
```

**Plugin Architecture:**

```text
my-plugin/
├── .claude-plugin/
│   └── plugin.json          # Manifest (optional — auto-discovers without it)
├── skills/                   # Skills (slash commands with supporting files)
│   ├── violation-workflow/
│   │   ├── SKILL.md
│   │   └── templates/
│   └── board-report/
│       └── SKILL.md
├── agents/                   # Custom subagents
│   ├── security-auditor.md
│   └── code-reviewer.md
├── hooks/                    # Hook configurations
│   └── hooks.json
├── scripts/                  # Hook and utility scripts
│   └── plugin-audit.sh
└── README.md                 # Documentation
```

> **Manifest is optional.** Claude Code auto-discovers `skills/`, `agents/`, and `hooks/` in default locations. Include `plugin.json` to set name, version, description, and customize paths.

**Why Plugins?**

- **Distribution** - Share automation with your team or across your own projects
- **Organization** - Group related functionality
- **Versioning** - Track changes over time
- **Reusability** - Use across multiple projects

#### 1.2 Plugin Manifest (10 min)

**The plugin.json File (.claude-plugin/plugin.json):**

```json
{
  "name": "realmanage-hoa",
  "version": "1.0.0",
  "description": "RealManage HOA automation - violations, fines, board reports",
  "author": {
    "name": "DX Team"
  }
}
```

**Creating a Plugin:**

```bash
# Create plugin structure
mkdir -p realmanage-hoa/.claude-plugin
mkdir -p realmanage-hoa/skills
mkdir -p realmanage-hoa/agents
mkdir -p realmanage-hoa/hooks

# Create manifest
cat > realmanage-hoa/.claude-plugin/plugin.json << 'EOF'
{
  "name": "realmanage-hoa",
  "version": "1.0.0",
  "description": "RealManage HOA automation tools",
  "author": { "name": "DX Team" }
}
EOF

# Validate structure
/plugin validate
```

> **Not familiar with these commands?** No worries - Claude can walk you through them. Just ask!

---

### Part 2: Skills in Plugins (30 min)

Skills are the standard way to create slash commands in Claude Code. They live in a directory structure that supports templates, scripts, and other supporting files alongside the main SKILL.md.

#### 2.1 Skills and Commands (10 min)

Commands (`.claude/commands/<name>.md`) and skills (`.claude/skills/<name>/SKILL.md`) have been **merged** — both create the same `/name` slash command and support the same YAML frontmatter fields. Skills are the recommended approach:

| Feature | Commands (legacy) | Skills (recommended) |
| ------- | -------- | ------ |
| Location | `.claude/commands/<name>.md` | `.claude/skills/<name>/SKILL.md` |
| YAML frontmatter | Same fields | Same fields |
| Supporting files | No (single .md file) | Yes (templates, scripts in skill directory) |
| In plugins | `.claude/commands/` or `commands/` | `skills/<name>/SKILL.md` |

Both support: `allowed-tools`, `model`, `context: fork`, `agent`, `hooks`, `disable-model-invocation`, `user-invocable`, and all other frontmatter fields.

**Key insight:** Skills and commands are functionally identical. Skills add directory structure for supporting files.

#### 2.2 Skill Structure (10 min)

**SKILL.md Anatomy:**

```markdown
---
name: violation-workflow
description: Process HOA violations through escalation workflow
argument-hint: <property_id>
disable-model-invocation: true
user-invocable: true
allowed-tools: Read, Grep, Edit
model: sonnet
context: fork
agent: violation-processor
hooks:
  PostToolUse:
    - matcher: "Edit"
      hooks:
        - type: command
          command: "echo \"$(date -Iseconds) | POST-EDIT\" >> .claude/audit.log"
---

Process the violation for property $ARGUMENTS following RealManage rules...
```

**Frontmatter Fields:**

| Field | Required | Description |
| ----- | -------- | ----------- |
| `name` | No | Display name (uses directory name if omitted) |
| `description` | Recommended | When to use; enables auto-invocation |
| `argument-hint` | No | Autocomplete hint (e.g., `<property_id>`) |
| `disable-model-invocation` | No | `true` = manual only |
| `user-invocable` | No | `false` = hide from `/` menu |
| `allowed-tools` | No | Restrict available tools |
| `model` | No | `sonnet`, `opus`, `haiku`, `default`, `inherit` |
| `context` | No | `fork` = run in isolated subagent |
| `agent` | No | Custom agent name (from `agents/`) |
| `hooks` | No | Embedded lifecycle hooks |

#### 2.3 Skills with Supporting Files (10 min)

**Real Power: Templates and Scripts**

```text
realmanage-hoa/
└── skills/
    └── violation-notice/
        ├── SKILL.md
        └── templates/
            ├── first-notice.md
            ├── second-notice.md
            └── final-notice.md
```

**SKILL.md:**

```markdown
---
name: violation-notice
description: Generate violation notice using appropriate template
argument-hint: <property_id> <notice_level>
disable-model-invocation: true
---

Generate a $1 violation notice for property $0.

## Instructions:
1. Look up property owner information
2. Calculate any applicable fines
3. Use the template from `templates/$1-notice.md`
4. Fill in all placeholders:
   - {{OWNER_NAME}}
   - {{PROPERTY_ADDRESS}}
   - {{VIOLATION_TYPE}}
   - {{FINE_AMOUNT}}
   - {{DUE_DATE}}
   - {{APPEAL_DEADLINE}}

## Compliance Requirements:
- Include CCR reference section
- Add appeal instructions
- Log generation to audit trail
```

**templates/first-notice.md:**

```markdown
# First Notice - HOA Violation

**Date:** {{DATE}}
**To:** {{OWNER_NAME}}
**Property:** {{PROPERTY_ADDRESS}}

Dear {{OWNER_NAME}},

This notice is to inform you that a violation has been observed...

**Violation Type:** {{VIOLATION_TYPE}}
**Date Observed:** {{OBSERVED_DATE}}

Please remedy this violation within 30 days to avoid fines.

**CCR Reference:** {{CCR_SECTION}}

Sincerely,
RealManage HOA Management
```

---

### Part 3: Agents in Plugins (25 min)

Custom agents from Week 6 can be packaged in plugins and referenced by skills.

#### 3.1 Plugin Agents (10 min)

**Agent Location in Plugins:**

```text
realmanage-hoa/
└── agents/
    ├── security-auditor.md
    ├── code-reviewer.md
    └── violation-processor.md
```

**Agent File (agents/security-auditor.md):**

```markdown
---
name: security-auditor
description: Audit code for security vulnerabilities and compliance
tools: Read, Grep, Glob
model: opus
permissionMode: dontAsk
---

You are a security auditor checking for vulnerabilities.

Review for:
- OWASP Top 10 vulnerabilities
- SQL injection risks
- XSS attack vectors
- Hardcoded secrets or credentials
- PII exposure
- SOC 2 compliance violations

Output a security report with:
- Severity: CRITICAL / HIGH / MEDIUM / LOW
- Location: File and line number
- Issue: Description of vulnerability
- Remediation: How to fix it
```

#### 3.2 Skills Spawning Agents (10 min)

**The `context: fork` and `agent:` Fields**

Skills can spawn agents defined in the same plugin:

```markdown
---
name: security-review
description: Run comprehensive security review on codebase
context: fork
agent: security-auditor
---

Perform a security audit of the current codebase.

Focus on:
1. OWASP Top 10 vulnerabilities
2. Hardcoded secrets
3. SQL injection risks
4. PII exposure

Generate a security report with severity ratings.
```

**What Happens:**

1. User invokes `/realmanage-hoa:security-review`
2. Claude loads the `security-auditor` agent from `agents/`
3. Skill runs in isolated context with agent's tools/permissions
4. Results return to main conversation

#### 3.3 Dynamic Context Injection (5 min)

**The `` !`command` `` Syntax**

Inject live data into skills before Claude processes them:

```markdown
---
name: pr-summary
description: Summarize current pull request
context: fork
agent: code-reviewer
---

## Pull Request Context

**Changed Files:**
!`git diff --name-only main...HEAD`

**Diff Summary:**
!`git diff --stat main...HEAD`

**Recent Commits:**
!`git log main..HEAD --oneline`

## Your Task
Summarize this PR with risks and recommendations.
```

**How It Works:**

1. `` !`command` `` executes before Claude processes the skill
2. Output replaces the placeholder
3. Claude sees actual data, not the command

---

### Part 4: Hooks in Plugins (15 min)

#### 4.1 Plugin Hook Configuration (5 min)

**Hook Location in Plugins:**

```text
realmanage-hoa/
└── hooks/
    └── hooks.json
```

**hooks/hooks.json:**

```json
{
  "hooks": {
    "PreToolUse": [
      {
        "matcher": "*",
        "hooks": [
          {
            "type": "command",
            "command": "${CLAUDE_PLUGIN_ROOT}/scripts/plugin-audit.sh"
          }
        ]
      }
    ],
    "PostToolUse": [
      {
        "matcher": "Edit",
        "hooks": [
          {
            "type": "command",
            "command": "echo \"$(date -Iseconds) | EDIT COMPLETE\" >> .claude/plugin-audit.log"
          }
        ]
      }
    ]
  }
}
```

> **`${CLAUDE_PLUGIN_ROOT}`** is an environment variable that resolves to the plugin's root directory. Always use it in plugin hooks and scripts so paths work regardless of where the plugin is installed.
>
> **Note:** The wildcard matcher (`*`) requires a script to read the tool name from stdin JSON. See Week 6 for Bash (`jq`) and PowerShell (`ConvertFrom-Json`) examples. For matchers targeting a specific tool (like `"Edit"` above), simple inline commands work fine.

**Hook Handler Types:**

Hooks support three handler types:

| Type | Description | Use Case |
| ---- | ----------- | -------- |
| `command` | Runs a shell command; receives JSON on stdin | Script execution, logging, blocking |
| `prompt` | Single-turn LLM evaluation | Quick validation, format checking |
| `agent` | Subagent with tools for verification | Complex multi-step validation |

All examples in this course use `command` hooks. See the [official hooks docs](https://code.claude.com/docs/en/hooks) for `prompt` and `agent` handler examples.

#### 4.2 Embedded Hooks in Skills (10 min)

**Hooks can also be embedded directly in skills:**

```markdown
---
name: edit-violation-record
description: Edit a violation record with full audit trail
argument-hint: <violation_id>
disable-model-invocation: true
hooks:
  PreToolUse:
    - matcher: "Edit"
      hooks:
        - type: command
          command: "echo \"$(date -Iseconds) | PRE-EDIT\" >> .claude/violation-audit.log"
  PostToolUse:
    - matcher: "Edit"
      hooks:
        - type: command
          command: "echo \"$(date -Iseconds) | POST-EDIT | SUCCESS\" >> .claude/violation-audit.log"
---

Edit violation record $ARGUMENTS.

IMPORTANT: All edits are logged for SOC 2 compliance.
```

**When to Use Each:**

- **Plugin hooks (hooks.json)** - Apply to all plugin operations
- **Skill hooks (embedded)** - Apply only when that skill is invoked

---

### Part 5: Distribution & Marketplace (20 min)

#### 5.1 Local Plugin Development (10 min)

**Loading Local Plugins:**

```bash
# Load plugin for testing
claude --plugin-dir ./realmanage-hoa

# Load multiple plugins
claude --plugin-dir ./plugin-one --plugin-dir ./plugin-two
```

**Invoking Plugin Skills:**

```bash
# Plugin skills are namespaced
/realmanage-hoa:violation-workflow PROP-001
/realmanage-hoa:board-report 2026-01-15
/realmanage-hoa:security-review
```

**Validating Plugins:**

```bash
# Check plugin structure
/plugin validate
```

#### 5.2 Marketplace Distribution (10 min)

> **Note:** Claude Code ships with a default marketplace containing 50+ plugins out of the box. For team-specific automation, you can also create local plugins or add custom marketplaces from Git repositories.
>
> **Solo developer?** Plugins are just as valuable for individual workflows. Package your personal skills, agents, and hooks into a plugin you can reuse across projects or share on the marketplace.

**Exploring Available Plugins:**

Run `/plugin` to open the interactive plugin manager:

| Tab | Purpose |
| --- | ------- |
| **Discover** | Browse 50+ available plugins with install counts |
| **Installed** | View and manage your installed plugins |
| **Marketplaces** | See configured plugin sources |

**Navigation:** Type to search · Space to toggle · Enter for details · Esc to go back

**Popular plugins** from the [official repository](https://github.com/anthropics/claude-plugins-official):

- `frontend-design` - Production-grade frontend interfaces
- `code-review` - Automated code review for PRs
- `feature-dev` - Comprehensive feature development workflow
- `ralph-loop` - Ralph Wiggum technique for iterative, self-referential AI development loops

**Adding a Marketplace:**

```bash
# Add marketplace from various sources
/plugin marketplace add ./local-marketplace
/plugin marketplace add owner/repo                    # GitHub
/plugin marketplace add https://gitlab.com/team/plugins.git

# Update marketplace copies
/plugin marketplace update
```

**Installing from Marketplace:**

```bash
# Install a plugin
/plugin install realmanage-hoa@company-tools

# Interactive plugin manager
/plugin
```

**Plugin Commands Summary:**

```bash
/plugin                              # Interactive manager
/plugin marketplace add <source>     # Add marketplace
/plugin marketplace update           # Update all marketplaces
/plugin install <name>@<marketplace> # Install plugin
/plugin validate                     # Validate plugin structure
claude --plugin-dir ./path           # Load local plugin
```

---

### Part 6: Hands-On Workshop - Build a Plugin (30 min)

#### 6.1 Setup Your Sandbox

```bash
# Copy example to sandbox
cd courses/ai-101-claude-code/sessions/week-7
cp -r examples sandbox
cd sandbox/pm-toolkit

# Start Claude
claude

# Try the included skills
/write-story homeowners need to see their violation history
/write-bdd when a violation reaches 90 days it should escalate to board review
```

#### 6.2 Exercise 1: Create Plugin Structure (10 min)

Create a complete plugin from scratch:

```bash
# Create plugin structure
mkdir -p realmanage-violations/.claude-plugin
mkdir -p realmanage-violations/skills/violation-report
mkdir -p realmanage-violations/agents
mkdir -p realmanage-violations/hooks
mkdir -p realmanage-violations/scripts

# Create manifest
cat > realmanage-violations/.claude-plugin/plugin.json << 'EOF'
{
  "name": "realmanage-violations",
  "version": "1.0.0",
  "description": "RealManage violation management tools",
  "author": { "name": "Your Name" }
}
EOF
```

#### 6.3 Exercise 2: Add a Skill (10 min)

Create `realmanage-violations/skills/violation-report/SKILL.md`:

```markdown
---
name: violation-report
description: Generate comprehensive violation report for a property
argument-hint: <property_id>
disable-model-invocation: true
allowed-tools: Read, Grep, Glob
---

Generate a violation report for property $ARGUMENTS.

Include:
1. All open violations with dates
2. Escalation status (30/60/90 days)
3. Fine calculations with 10% monthly compound interest
4. Recommended next actions
5. Compliance status

Format as markdown suitable for board review.
```

#### 6.4 Exercise 3: Add an Agent (5 min)

Create `realmanage-violations/agents/violation-auditor.md`:

```markdown
---
name: violation-auditor
description: Audit violation records for compliance issues
tools: Read, Grep, Glob
model: default
permissionMode: plan
---

You are a violation compliance auditor.

When auditing:
1. Check escalation timelines are correct
2. Verify fine calculations use compound interest
3. Ensure all required notice fields are present
4. Flag any SOC 2 compliance issues

Report findings with severity levels.
```

#### 6.5 Exercise 4: Add Audit Hooks (5 min)

Create `realmanage-violations/hooks/hooks.json`:

```json
{
  "hooks": {
    "PreToolUse": [
      {
        "matcher": "*",
        "hooks": [
          {
            "type": "command",
            "command": "${CLAUDE_PLUGIN_ROOT}/scripts/plugin-audit.sh"
          }
        ]
      }
    ]
  }
}
```

Create `realmanage-violations/scripts/plugin-audit.sh`:

**Bash (Mac/Linux/WSL):**

```bash
#!/bin/bash
mkdir -p .claude
TOOL=$(jq -r '.tool_name')
INPUT=$(jq -r '.tool_input | tostring')
echo "$(date -Iseconds) | $TOOL | $INPUT" >> .claude/plugin-audit.log
```

**PowerShell (Windows):** `realmanage-violations/scripts/plugin-audit.ps1`

```powershell
$hookData = [Console]::In.ReadToEnd() | ConvertFrom-Json
$tool = $hookData.tool_name
$input = $hookData.tool_input | ConvertTo-Json -Compress
$timestamp = Get-Date -Format "o"
if (-not (Test-Path ".claude")) { New-Item -ItemType Directory -Path ".claude" | Out-Null }
Add-Content -Path ".claude/plugin-audit.log" -Value "$timestamp | $tool | $input"
```

> **Windows:** Use `"command": "powershell -NoProfile -File ${CLAUDE_PLUGIN_ROOT}/scripts/plugin-audit.ps1"` in hooks.json.

**Test your plugin:**

```bash
# Load and test
claude --plugin-dir ./realmanage-violations

# Try the skill
/realmanage-violations:violation-report PROP-001
```

---

## 🎯 Key Takeaways

### Plugin Structure

```text
my-plugin/
├── .claude-plugin/
│   └── plugin.json      # Manifest (optional — auto-discovers without it)
├── skills/              # Slash commands with supporting files
│   └── <name>/
│       ├── SKILL.md
│       └── templates/
├── agents/              # Custom subagents
│   └── <name>.md
├── hooks/               # Hook configurations
│   └── hooks.json
├── scripts/             # Hook and utility scripts
│   └── audit.sh
└── README.md
```

**Key Variable:** `${CLAUDE_PLUGIN_ROOT}` — resolves to plugin root. Use in hooks/scripts for portable paths.

### Skills Quick Reference

```text
LOCATION: <plugin>/skills/<skill-name>/SKILL.md
   (or)   .claude/skills/<skill-name>/SKILL.md
   (or)   .claude/commands/<skill-name>.md  (legacy, same functionality)

FRONTMATTER (all optional):
---
name: skill-name
description: When to use this skill
argument-hint: <args>
disable-model-invocation: true|false
user-invocable: true|false
allowed-tools: Read, Grep, Edit
model: sonnet|opus|haiku|default|inherit
context: fork
agent: custom-agent-name
hooks:
  PreToolUse: [...]
---

VARIABLES:
$ARGUMENTS        — All arguments as string
$ARGUMENTS[0]     — First argument (0-indexed)
$0, $1, $2        — Shorthand for $ARGUMENTS[N] (0-based)
${CLAUDE_SESSION_ID} — Current session ID

DYNAMIC CONTEXT:
!`shell command`  — Executes before skill loads, injects output
```

### Agent Frontmatter Quick Reference

```text
LOCATION: <plugin>/agents/<name>.md
   (or)   .claude/agents/<name>.md

FRONTMATTER:
---
name: agent-name                       # Required
description: When to delegate here     # Required
tools: Read, Glob, Grep, Bash          # Optional (inherits all if omitted)
disallowedTools: Write, Edit           # Optional (denylist)
model: sonnet|opus|haiku|inherit       # Optional (default: inherit)
permissionMode: default|plan|acceptEdits|dontAsk|bypassPermissions
maxTurns: 10                           # Optional
skills:                                # Optional — preload skills
  - api-conventions
mcpServers:                            # Optional — MCP servers
  - slack
memory: user|project|local             # Optional — persistent memory
background: true                       # Optional — always run in background
isolation: worktree                    # Optional — git worktree isolation
---
```

### Hook Events Reference

| Event | When It Fires | Matcher Input |
| ----- | ------------- | ------------- |
| `PreToolUse` | Before tool executes | Tool name |
| `PostToolUse` | After tool succeeds | Tool name |
| `PostToolUseFailure` | After tool fails | Tool name |
| `SessionStart` | Session begins/resumes | `startup`, `resume`, `clear`, `compact` |
| `Stop` | Claude finishes responding | No matcher |
| `Notification` | Notification sent | Notification type |
| `SubagentStart` | Subagent spawned | Agent type |
| `SubagentStop` | Subagent finishes | Agent type |
| `UserPromptSubmit` | User submits prompt | No matcher |

> See [official hooks docs](https://code.claude.com/docs/en/hooks) for the complete list including `PermissionRequest`, `ConfigChange`, `PreCompact`, `SessionEnd`, and others.

### Plugin Commands

```bash
/plugin                              # Interactive manager
/plugin marketplace add <source>     # Add marketplace
/plugin marketplace update           # Update all marketplaces
/plugin install <name>@<marketplace> # Install plugin
/plugin validate                     # Validate plugin
claude --plugin-dir ./path           # Load local plugin (session only)
claude plugin install <name> -s project  # Install to project scope

INVOKE:
/plugin-name:skill-name <args>
```

---

## Homework (Before Week 8)

### Required Tasks

1. Create a complete plugin with at least 2 skills
2. Add a custom agent to your plugin
3. Add audit hooks to your plugin
4. Test your plugin locally with `--plugin-dir`
5. Share your plugin structure in `#ai-exchange` Slack (or document it in your project README for future reference)

### Stretch Goals

1. Create a skill with supporting template files
2. Use dynamic context injection (`` !`command` ``)
3. Create a skill that spawns your custom agent

---

## Resources

### Official Documentation

- [Plugins](https://code.claude.com/docs/en/plugins)
- [Skills](https://code.claude.com/docs/en/skills)
- [Sub-agents](https://code.claude.com/docs/en/sub-agents)
- [Hooks](https://code.claude.com/docs/en/hooks)

### RealManage Resources

- [Week 7 Examples](./examples/) - PM Toolkit plugin
- Slack: `#ai-exchange`

---

## Next Week Preview

**Week 8: Real-World Automation**

- What automation means for each role (shared session)
- Headless mode for developers, interactive skills for everyone else
- Context management: `/compact` vs `/clear` vs plan mode
- Anti-patterns: retry hammering, verbose prompts, context-nuking
- Role-specific track exercises (45 min hands-on)

**The progression:** Week 6 (Agents & Hooks) -> Week 7 (Plugins) -> Week 8 (Putting it all together)

---

## 📚 Quick Resources

- [Glossary](../../resources/glossary.md) - Key terms and definitions
- [Decision Trees](../../resources/decision-trees.md) - When to use what
- [Troubleshooting](../../resources/troubleshooting.md) - Common issues and fixes
- [CLI Commands](../../resources/cli-commands.md) - Command cheatsheet

---

*End of Week 7 Session Plan*
*Next Session: Week 8 - Real-World Automation*

---

**Next:** [Week 8: Real-World Automation](../week-8/README.md)
