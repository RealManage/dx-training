# Week 7: Plugins - The Complete Package

**Duration:** 2 hours
**Format:** In-person or virtual
**Audience:** RealManage cross-functional team (engineers, PMs, support staff)

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
- [ ] Created at least one custom command in `.claude/commands/`
- [ ] Created at least one custom subagent in `.claude/agents/`
- [ ] Understanding of hook configuration
- [ ] Ready for 2-hour session

### For Instructors
- [ ] Test all example projects build without warnings
- [ ] Verify plugin examples work correctly
- [ ] Prepare backup exercises for network issues
- [ ] Monitor `#dx-training` Slack channel

---

## Session Plan

### Part 1: Plugin Architecture (20 min)

#### 1.1 Plugins: The Complete Package (10 min)

In Week 6, you learned about foundational components:
- **Commands** (`.claude/commands/`) - Reusable prompts
- **Custom Subagents** (`.claude/agents/`) - Specialized agents
- **Hooks** (`settings.json`) - Lifecycle event handlers

**Plugins package ALL of these** into a distributable unit.

```
Plugin = Commands + Agents + Hooks + Skills (packaged together)
```

**Plugin Architecture:**
```
my-plugin/
├── .claude-plugin/
│   └── plugin.json          # Manifest (required)
├── skills/                   # Skills (enhanced commands)
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
└── README.md                 # Documentation
```

**Why Plugins?**
- **Distribution** - Share automation with your team
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
    "name": "DX Team",
    "email": "dx-team@realmanage.com"
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
/plugin validate ./realmanage-hoa
```

---

### Part 2: Skills in Plugins (30 min)

Skills are **enhanced commands** that live inside plugins. They support features that simple commands don't.

#### 2.1 Skills vs Commands (10 min)

| Feature | Commands | Skills |
|---------|----------|--------|
| Location | `.claude/commands/<name>.md` | `<plugin>/skills/<name>/SKILL.md` |
| Supporting files | No | Yes (templates, scripts) |
| Auto-invocation | No | Yes (`disable-model-invocation: false`) |
| Spawn agents | No | Yes (`context: fork`, `agent:`) |
| Embedded hooks | No | Yes (`hooks:` field) |
| Tool restrictions | No | Yes (`allowed-tools:`) |
| Model override | No | Yes (`model:`) |

**Key insight:** Skills are commands with superpowers.

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
          command: "echo 'Edited: $TOOL_INPUT' >> .claude/audit.log"
---

Process the violation for property $ARGUMENTS following RealManage rules...
```

**Frontmatter Fields:**

| Field | Required | Description |
|-------|----------|-------------|
| `name` | No | Display name (uses directory name if omitted) |
| `description` | Recommended | When to use; enables auto-invocation |
| `argument-hint` | No | Autocomplete hint (e.g., `<property_id>`) |
| `disable-model-invocation` | No | `true` = manual only |
| `user-invocable` | No | `false` = hide from `/` menu |
| `allowed-tools` | No | Restrict available tools |
| `model` | No | `sonnet`, `opus`, `default`, `inherit` |
| `context` | No | `fork` = run in isolated subagent |
| `agent` | No | Custom agent name (from `agents/`) |
| `hooks` | No | Embedded lifecycle hooks |

#### 2.3 Skills with Supporting Files (10 min)

**Real Power: Templates and Scripts**

```
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

Generate a $2 violation notice for property $1.

## Instructions:
1. Look up property owner information
2. Calculate any applicable fines
3. Use the template from `templates/$2-notice.md`
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
```
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
```
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
            "command": "echo \"$(date -Iseconds) | $TOOL_NAME\" >> .claude/plugin-audit.log"
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
          command: "echo \"$(date -Iseconds) | PRE-EDIT | $TOOL_INPUT\" >> .claude/violation-audit.log"
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
/plugin validate ./realmanage-hoa
```

#### 5.2 Marketplace Distribution (10 min)

> **Note:** The Claude Code plugin marketplace is still evolving. The commands below show the expected patterns, but public marketplaces may have limited content. For now, focus on local plugin development and team-internal distribution via Git repositories. Check [Anthropic's documentation](https://docs.anthropic.com/en/docs/claude-code) for the latest marketplace availability.

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
```
/plugin                              # Interactive manager
/plugin marketplace add <source>     # Add marketplace
/plugin marketplace update           # Update all marketplaces
/plugin install <name>@<marketplace> # Install plugin
/plugin validate .                   # Validate current directory
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
            "command": "echo \"$(date -Iseconds) | $TOOL_NAME | $TOOL_INPUT\" >> .claude/plugin-audit.log"
          }
        ]
      }
    ]
  }
}
```

**Test your plugin:**
```bash
# Load and test
claude --plugin-dir ./realmanage-violations

# Try the skill
/realmanage-violations:violation-report PROP-001
```

---

## Key Takeaways

### Plugin Structure
```
my-plugin/
├── .claude-plugin/
│   └── plugin.json      # Manifest (required)
├── skills/              # Enhanced commands
│   └── <name>/
│       ├── SKILL.md
│       └── templates/
├── agents/              # Custom subagents
│   └── <name>.md
├── hooks/               # Hook configurations
│   └── hooks.json
└── README.md
```

### Skills Quick Reference
```
LOCATION: <plugin>/skills/<skill-name>/SKILL.md

FRONTMATTER:
---
name: skill-name
description: When to use this skill
argument-hint: <args>
disable-model-invocation: true|false
allowed-tools: Read, Grep, Edit
context: fork
agent: custom-agent-name
hooks:
  PreToolUse: [...]
---

VARIABLES:
$ARGUMENTS, $1, $2, $3

DYNAMIC CONTEXT:
!`shell command`  -> Executes and injects output
```

### Plugin Commands
```
/plugin                              # Interactive manager
/plugin marketplace add <source>     # Add marketplace
/plugin install <name>@<marketplace> # Install plugin
/plugin validate .                   # Validate plugin
claude --plugin-dir ./path           # Load local plugin

INVOKE:
/plugin-name:skill-name <args>
```

---

## Homework (Before Week 8)

### Required Tasks:
1. Create a complete plugin with at least 2 skills
2. Add a custom agent to your plugin
3. Add audit hooks to your plugin
4. Test your plugin locally with `--plugin-dir`
5. Share your plugin structure in `#dx-training` Slack

### Stretch Goals:
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
- Slack: `#dx-training`

---

## Next Week Preview

**Week 8: Real-World Automation**
- Cross-functional use cases
- Headless Claude automation (CLI scripting)
- Cost optimization strategies
- Batch processing patterns

**The progression:** Week 6 (Agents & Hooks) -> Week 7 (Plugins) -> Week 8 (Production)

---

## Instructor Notes

### Session Timing (2 hours)
- Part 1: Plugin Architecture - 20 min
- Part 2: Skills in Plugins - 30 min
- Part 3: Agents in Plugins - 25 min
- Part 4: Hooks in Plugins - 15 min
- Part 5: Distribution & Marketplace - 20 min
- Part 6: Hands-On Workshop - 30 min
- **Total: 2h 20min** (buffer for Q&A)

### Key Points to Emphasize
- **Plugins are the container** - Everything from Week 6 goes inside
- **Skills = enhanced commands** - Same concept, more capabilities
- **Agents + Hooks in plugins** - Complete packaging
- **Distribution** - Share automation with your team

### Common Questions

**"Do I need a plugin for simple automation?"**
- No, you can use commands/agents/hooks directly
- Plugins are for packaging and distribution
- Use plugins when sharing with team or across projects

**"Can I use Week 6 components without plugins?"**
- Yes! `.claude/commands/`, `.claude/agents/`, `.claude/settings.json` all work
- Plugins just package them for distribution

**"What's the difference between plugin hooks and skill hooks?"**
- Plugin hooks (hooks.json): Apply to all plugin operations
- Skill hooks (embedded): Apply only when that skill runs

### Troubleshooting

**Plugin not loading:**
- Check `.claude-plugin/plugin.json` exists and is valid JSON
- Verify directory structure is correct
- Use `/plugin validate .` to check

**Skill not recognized:**
- Check path: `<plugin>/skills/<name>/SKILL.md`
- Verify YAML frontmatter syntax
- Restart Claude session

**Agent not spawning:**
- Verify agent file exists in `<plugin>/agents/`
- Check `agent:` field matches agent name
- Ensure `context: fork` is set in skill

---

## 📚 Quick Resources

- [Glossary](../../resources/glossary.md) - Key terms and definitions
- [Decision Trees](../../resources/decision-trees.md) - When to use what
- [Troubleshooting](../../resources/troubleshooting.md) - Common issues and fixes
- [CLI Commands](../../resources/cli-commands.md) - Command cheatsheet

---

*End of Week 7 Session Plan*
*Next Session: Week 8 - Real-World Automation*
