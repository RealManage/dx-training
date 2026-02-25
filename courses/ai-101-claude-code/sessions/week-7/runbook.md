# Week 7: Plugins - The Complete Package - Trainer Runbook

## Session Overview

**Duration:** 2 hours
**Format:** In-person or virtual
**Tracks:** Developer, QA, PM, Support

---

## Pre-Session Checklist

### For Instructors

- [ ] Test pm-toolkit plugin loads correctly (`claude --plugin-dir ./examples/pm-toolkit`)
- [ ] Verify all 4 pm-toolkit skills work (write-story, write-epic, write-bdd, acceptance-criteria)
- [ ] Confirm plugin validation works (`/plugin validate` from within pm-toolkit dir)
- [ ] Prepare backup exercises for network issues
- [ ] Monitor `#ai-exchange` Slack channel
- [ ] Have the Week 6 recap ready (agents, hooks, lifecycle events)

---

## Timing Guide

| Time | Segment | What Happens |
| ---- | ------- | ------------ |
| 0:00-0:05 | Welcome & Context | Set the stage: "Last week agents & hooks, this week we package them" |
| 0:05-0:25 | Part 1: Plugin Architecture | Directory structure, manifest (optional), auto-discovery |
| 0:25-0:55 | Part 2: Skills in Plugins | Skills = commands (merged), SKILL.md frontmatter, supporting files |
| 0:55-1:20 | Part 3: Agents in Plugins | Packaging agents, `context: fork`, dynamic context injection |
| 1:20-1:35 | Part 4: Hooks in Plugins | hooks.json, embedded skill hooks, `${CLAUDE_PLUGIN_ROOT}` |
| 1:35-1:55 | Part 5: Distribution | `--plugin-dir`, marketplace, `/plugin` interactive manager |
| 1:55-2:00 | Close | Key takeaways, homework, preview Week 8 |

> **Note:** Part 6 hands-on workshop exercises are self-paced. Encourage participants to complete during the session if time permits, otherwise as homework.

---

## Track Delivery Strategy

### Combined Session (Recommended)

All participants attend Parts 1-5 together. For the hands-on portion, participants work on their track-specific exercises:

- **Developers** follow the main README Part 6 (build a plugin from scratch with skills, agents, hooks)
- **QA** follow `tracks/qa.md` (test pm-toolkit, design QA toolkit plugin, create test generation skill)
- **PMs** follow `tracks/pm.md` (use pm-toolkit, explore plugin structure, extend with new skills)
- **Support** follow `tracks/support.md` (design support plugin concept, document team conventions)

### Instructor Tips for Mixed Audiences

- During Part 1 demo, focus on the pm-toolkit -- all roles can relate to user stories and acceptance criteria
- During Part 2 demo, show a SKILL.md file -- the frontmatter is similar to agents from Week 6
- When splitting for exercises, briefly explain: "Find your track file and work through the exercises. PMs and QA -- start with the pm-toolkit. Devs -- build one from scratch."
- Pair non-developers with developers if anyone struggles with directory structure
- For virtual sessions, use breakout rooms by track

---

## Live Demo Script

### Part 1 Demo: Plugin Structure (10 min)

```bash
# 1. Show the pm-toolkit plugin structure
cd courses/ai-101-claude-code/sessions/week-7/examples
ls -la pm-toolkit/
ls -la pm-toolkit/.claude-plugin/
cat pm-toolkit/.claude-plugin/plugin.json
```

**Talk through:**

- "Plugins are just directories with a specific structure"
- "The manifest (`plugin.json`) is OPTIONAL -- Claude auto-discovers skills, agents, and hooks in default directories"
- "Include a manifest to set name, version, description for distribution"
- "Skills go in `skills/<name>/SKILL.md`, agents in `agents/<name>.md`, hooks in `hooks/hooks.json`"

### Part 2 Demo: Skills (10 min)

```bash
# 2. Show a skill file
cat pm-toolkit/skills/write-story/SKILL.md
```

**Talk through:**

- "Skills and commands are merged -- same functionality, same frontmatter"
- "Skills use a directory structure so you can include templates, scripts alongside SKILL.md"
- "`$ARGUMENTS` is replaced with whatever the user types after the skill name"
- "`$1`, `$2`, `$3` give you indexed arguments"

```bash
# 3. Load and test the plugin
claude --plugin-dir ./pm-toolkit

# In Claude:
> /pm-toolkit:write-story homeowners need to see violation history online
```

**Key point:** "Skills are namespaced with the plugin name -- `/pm-toolkit:write-story` prevents conflicts between plugins."

### Part 4 Demo: Hooks in Plugins (5 min)

```bash
# 4. Show hooks configuration
cat pm-toolkit/hooks/hooks.json
```

**Talk through:**

- "Plugin hooks live in `hooks/hooks.json` -- same format as `settings.json` hooks"
- "Use `${CLAUDE_PLUGIN_ROOT}` to reference scripts inside the plugin directory"
- "Skills can also have embedded hooks in their YAML frontmatter"
- "Plugin hooks apply to all operations; skill hooks apply only when that skill runs"

### Expected Demo Output

**For write-story skill:**

- Formatted user story with title, As a/I want/So that format
- 3-5 acceptance criteria in Given/When/Then format
- Technical notes and story point estimate

**For write-bdd skill:**

- Complete Gherkin feature file
- Scenarios for happy path, edge case, error handling
- Properly tagged with @happy-path, @edge-case, etc.

**For plugin structure:**

- `plugin.json` with name, version, description
- 4 skill directories each with SKILL.md
- agents/ with requirements-analyst.md
- hooks/ with hooks.json

---

## Instructor Notes

### Key Points to Emphasize

- **Plugins are just packaging** -- Everything from Weeks 5-6 goes inside
- **Manifest is optional** -- Claude auto-discovers skills, agents, hooks in default directories
- **Commands and skills are merged** -- Both create `/name`, same frontmatter fields, skills add directory structure
- **`${CLAUDE_PLUGIN_ROOT}`** -- Use this env var in hook scripts for plugin-relative paths
- **Hook handler types** -- `command` (shell), `prompt` (LLM evaluation), `agent` (subagent with tools)
- **Plugin namespacing** -- `/plugin-name:skill-name` prevents conflicts between plugins
- **Distribution** -- `/plugin` interactive manager, `--plugin-dir` for local testing, marketplaces for team sharing

### Common Questions

> **"Do I need a plugin for simple automation?"**

- No, standalone `.claude/skills/`, `.claude/agents/`, `.claude/settings.json` all work fine
- Plugins are for packaging and distribution across projects or teams
- Use plugins when sharing with team or reusing across multiple repos

> **"Can I use Week 6 components without plugins?"**

- Yes! All standalone components work without plugins
- Plugins just package them for distribution and namespacing

> **"What's the difference between plugin hooks and skill hooks?"**

- Plugin hooks (`hooks/hooks.json`): Apply to all operations when plugin is active
- Skill hooks (embedded in SKILL.md frontmatter): Apply only when that specific skill runs

> **"What is `${CLAUDE_PLUGIN_ROOT}`?"**

- An environment variable that resolves to the plugin's root directory
- Use it in hook scripts and MCP configs so paths work regardless of where the plugin is installed
- Example: `"command": "${CLAUDE_PLUGIN_ROOT}/scripts/audit.sh"`

> **"Are commands and skills really the same?"**

- Yes -- the docs say "Custom slash commands have been merged into skills"
- Both support the same YAML frontmatter fields
- Skills are recommended because the directory structure supports templates and scripts

> **"How do I debug plugins?"**

- Use `--plugin-dir ./path` to test locally before publishing
- Use `/plugin validate` to check structure
- Restart Claude session after changing plugin files
- Check `claude --debug` output for plugin loading errors

---

## Troubleshooting

**Plugin not loading:**

- Check directory structure -- `skills/`, `agents/`, `hooks/` at plugin root, NOT inside `.claude-plugin/`
- If using manifest, verify `.claude-plugin/plugin.json` is valid JSON
- Use `/plugin validate` to check
- Restart Claude session

**Skill not recognized:**

- Check path: `<plugin>/skills/<name>/SKILL.md` (not flat `.md` files at skills root)
- Verify YAML frontmatter syntax (no tabs, proper `---` delimiters)
- Restart Claude session after changes

**Agent not spawning from skill:**

- Verify agent file exists in `<plugin>/agents/`
- Check `agent:` field in skill matches agent filename (without `.md`)
- Ensure `context: fork` is set in skill frontmatter
- Agent cannot spawn other agents (hard constraint)

**Hooks not firing:**

- Validate `hooks/hooks.json` is valid JSON: `cat hooks/hooks.json | jq .`
- Check matcher patterns (regex matching tool names)
- Verify script paths use `${CLAUDE_PLUGIN_ROOT}` for plugin-relative paths
- Make scripts executable: `chmod +x scripts/*.sh`

> **Still having issues?** See the [Troubleshooting Guide](../../resources/troubleshooting.md) for detailed debugging steps.

---

## Session Wrap-Up Checklist

- [ ] All participants understand plugins as packaging for skills, agents, and hooks
- [ ] All participants tested the pm-toolkit plugin successfully
- [ ] All participants created at least one skill or designed a plugin concept
- [ ] At least 3 participants shared plugin ideas in `#ai-exchange`
- [ ] Homework assigned: 2 skills + 1 agent + hooks + test locally + share in Slack
- [ ] Preview of Week 8 (Real-World Automation) delivered: "Week 8 puts it all together with headless automation, cost optimization, and batch processing"

---

*End of Week 7 Trainer Runbook*
