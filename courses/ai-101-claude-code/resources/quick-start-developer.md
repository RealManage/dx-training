# Quick Start: Developer Track

You write code. You want Claude Code to make you faster. Here's your optimized path.

**Estimated Time:** 12-16 hours total (1.5-2 hours per week)

---

## Your Learning Path

```
Week 0 ──▶ Week 1 ──▶ Week 2 ──▶ Week 3 ──▶ Week 4 ──▶ Week 5 ──▶ Week 6 ──▶ Week 7 ──▶ Week 8 ──▶ Week 9
(opt)      Setup       Prompts    Planning   ★ TDD ★    Commands    Agents      Plugins   Workflows   Capstone
                                                         & Skills    & Hooks
```

### Week Breakdown

| Week | Topic | Priority | Why It Matters |
| ---- | ----- | -------- | -------------- |
| 0 | AI Foundations | Optional | Skip if you know LLMs |
| 1 | Setup & Orientation | Must Do | Get Claude working |
| 2 | Prompting Foundations | Must Do | 10x your prompt quality |
| 3 | Plan Mode & Code Review | Must Do | Handle complex changes |
| 4 | Test-Driven Development | **Critical** | The killer feature |
| 5 | Commands & Skills | Must Do | Build your toolkit |
| 6 | Agents & Hooks | Must Do | Automation & personas |
| 7 | Plugins | Good to Know | Packaging & sharing |
| 8 | Real-World Workflows | Must Do | Headless automation, batch processing |
| 9 | Capstone | Must Do | Prove your skills |

---

## Week-by-Week Focus

### Week 0: AI Foundations (Optional - 30 min)

**Skip if:** You already understand LLMs, tokens, and context windows.

**Do if:** You're new to AI coding tools or want vocabulary definitions.

Key concepts: LLMs, tokens, context windows, hallucinations, agentic engineering.

---

### Week 1: Setup & Orientation (1 hour)

**Goal:** Claude Code installed and working.

Focus on:

- [ ] Installation verification (`claude doctor`)
- [ ] First conversation
- [ ] Creating your project `CLAUDE.md`
- [ ] Understanding where to work (`sandbox/` folder)

**Checkpoint:** Can you ask Claude to generate a simple C# class with tests?

---

### Week 2: Prompting Foundations (1.5 hours)

**Goal:** Write prompts that get great results first try.

Focus on:

- [ ] Context + Instruction pattern
- [ ] Specificity (tech stack, patterns, coverage)
- [ ] The 4C structure: Context, Constraints, Criteria, Clarifications

**Checkpoint:** Can you prompt for a complete service with tests without back-and-forth?

---

### Week 3: Plan Mode & Code Review (1.5 hours)

**Goal:** Handle multi-file changes confidently.

Focus on:

- [ ] When to use plan mode (`Shift+Tab`)
- [ ] Reviewing AI-generated plans
- [ ] Code review with Claude

**Checkpoint:** Can you use plan mode to implement a feature across 3+ files?

---

### Week 4: Test-Driven Development (2 hours) ⭐

**Goal:** Master TDD with AI assistance. This is the most valuable week.

Focus on:

- [ ] Red-Green-Refactor cycle
- [ ] Writing test-first prompts
- [ ] Achieving 80-90% coverage naturally
- [ ] The "tests reveal design" philosophy

**Checkpoint:** Can you build a feature entirely test-first with Claude?

---

### Week 5: Commands & Skills (1.5 hours)

**Goal:** Create reusable prompts.

Focus on:

- [ ] Creating commands in `.claude/commands/`
- [ ] Creating skills in `.claude/skills/`
- [ ] Difference: commands (manual) vs skills (auto-discoverable)

**Checkpoint:** Can you create a custom `/generate-service` command?

---

### Week 6: Agents & Hooks (1.5 hours)

**Goal:** Specialized personas and automation.

Focus on:

- [ ] Creating agents in `.claude/agents/`
- [ ] Setting up hooks for automation
- [ ] Security and audit logging patterns

**Checkpoint:** Can you create a "code reviewer" agent and a post-change hook?

---

### Week 7: Plugins (1 hour)

**Goal:** Package and share your tools.

Focus on:

- [ ] Plugin structure and manifest
- [ ] Packaging commands, skills, agents
- [ ] Installation and distribution

**Checkpoint:** Can you package a plugin and install it in another project?

---

### Week 8: Real-World Workflows (1.5 hours)

**Goal:** Production-ready Claude Code usage.

Focus on:

- [ ] Headless automation scripts
- [ ] Cost optimization strategies
- [ ] Multi-session workflows

**Checkpoint:** Can you build batch automation scripts with Claude CLI?

---

### Week 9: Capstone (3-4 hours)

**Goal:** Build something real.

Options for developers:

- **Option A:** Violation Escalation Service (backend-focused)
- **Option B:** Knowledge Base Chatbot (full-stack)
- **Option C:** Financial Forecasting Tool (data-focused)

**Checkpoint:** Present a working project to your team.

---

## Pro Tips for Developers

### Prompt Pattern That Works

```
Create a [SERVICE/COMPONENT] that [DOES WHAT]
- Using [TECH STACK]
- Following [PATTERN]
- With [TEST FRAMEWORK] tests achieving 80-90% coverage
```

### Commands to Know

| Command | When to Use |
| ------- | ----------- |
| `Shift+Tab` | Toggle plan mode |
| `/model` | Switch models (sonnet/opus) |
| `/clear` | Reset context |
| `/add-dir` | Add folder to context |
| `/compact` | Compress conversation |

### What to Skip

- Week 0 if you know AI basics
- Detailed prompt structure exercises if you're getting good results
- Plugin publishing if you're just using locally

### What to Go Deep On

- **Week 4 (TDD)** - This is the money. Spend extra time here.
- **Week 5-6 (Commands/Skills/Agents)** - Your productivity multipliers.

### After Certification

Own testing? Consider the **[QA Track](../docs/certification/tracks/qa-track.md)** for test case generation and automation skills.

---

## Resources

- [Production Hardening](./production-hardening.md) - Production-ready bash patterns for automation
- [Glossary](./glossary.md) - Term definitions
- [Decision Trees](./decision-trees.md) - When to use what
- [Troubleshooting](./troubleshooting.md) - Common issues
- [Prompt Library](./prompt-library.md) - Ready-to-use prompts
- [Getting Help](./getting-help.md) - Support channels and how to get assistance

---

**Need help?** See [Getting Help](getting-help.md) for support channels.

*Questions? Hit up `#ai-exchange` on Slack.*
