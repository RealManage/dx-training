# Week 8: Real-World Automation

**Duration:** 2 hours (60 min taught + 45 min exercises + 10 min transitions/buffer + 5 min Q&A)
**Format:** In-person or virtual
**Audience:** RealManage cross-functional team (engineers, PMs, QA, support staff)

> **Working solo?** Everything in this week applies whether you're on a team of twenty or a team of one. A solo developer wears every hat — engineer, PM, QA, support — so these cross-functional patterns are your daily reality. Look for solo-specific tips throughout.

## Learning Tracks

This week uses a **shared session + role-specific tracks** model. Everyone attends Parts 1-4 together, then splits into tracks for hands-on exercises.

- **[Developer Track](./tracks/developer.md)** - Headless scripts, batch processing, JSON pipelines, parallel processing
- **[QA Track](./tracks/qa.md)** - Automate test runs, build coverage pipelines, and batch quality checks
- **[PM Track](./tracks/pm.md)** - Interactive skill workflows, workflow design, automation ROI
- **[Support Track](./tracks/support.md)** - Automate ticket triage, draft responses, and build escalation workflows

---

## Learning Objectives

By the end of this session, participants will be able to:

- Understand what automation means for their specific role
- Recognize when headless mode is appropriate (and when it isn't)
- Apply context management best practices (`/compact`, `/clear`, plan mode)
- Identify and avoid common anti-patterns
- Build continuous improvement into their workflow
- Measure and track productivity gains

## Pre-Session Checklist

### For Participants

- [ ] Completed Weeks 1-7 (especially skills from Week 5 and hooks from Week 6)
- [ ] Access to a test repository for exercises
- [ ] Ready for 2-hour session

> **Note:** Not familiar with bash scripting? That's okay — the shared session focuses on concepts and patterns. Bash scripting details are in the Developer Track for engineers.

## Session Plan

### Part 1: What Automation Means for Your Role (10 min)

Automation doesn't mean the same thing for every role. Before we dive into tools, let's align on what each role actually automates.

#### The Automation Spectrum

| Role | What They Automate | How They Do It |
| ---- | ------------------ | -------------- |
| **Developer** | Batch code reviews, test generation, report pipelines | Headless CLI scripts (`-p` mode), bash/PowerShell |
| **PM** | Release notes, sprint summaries, status reports | Interactive skills, workflow design, engineer handoff |
| **QA** | Test coverage analysis, regression checks, bug triage | Skills + focused CLI automation |
| **Support** | Ticket summarization, response drafts, escalation workflows | Interactive skills with structured output |

**Key insight:** Automation for non-developers is primarily about **interactive skills** — the ones you built in Weeks 5 and 7. You don't need to write bash scripts to automate your work. You need well-designed skills that produce consistent output.

#### One Skill, Four Roles

Here's a single skill that demonstrates cross-functional value — a weekly status report generator:

```markdown
---
name: weekly-status
description: Generate weekly status report for team
argument-hint: [sprint_number]
disable-model-invocation: true
---

Generate a weekly status report for Sprint $ARGUMENTS.

## Include:
1. Completed work (from recent git commits)
2. Open items requiring attention
3. Blockers and risks
4. Key metrics (coverage, velocity)
5. Next week priorities

Format for stakeholder communication — concise, no jargon.
```

Every role uses this differently:

- **Developer** runs it interactively or wraps it in a `-p` script for scheduled generation
- **PM** runs it interactively before standups, edits the output for stakeholder emails
- **QA** uses it to track coverage trends and regression status
- **Support** uses it to identify recurring issues for the week

### Part 2: Headless Mode — The Developer Superpower (20 min)

#### 2.1 What Headless Mode Does (5 min)

Claude Code can run without interactive prompts using the `-p` (or `--print`) flag. This enables:

- Batch processing multiple files
- Scripted workflows and pipelines
- Automated report generation
- Integration with CI/CD and other tools

> **Important:** Skills (`/skill-name`) are interactive-only — they do **not** work with the `-p` flag. In headless mode, describe the task directly in your prompt instead.

#### 2.2 Essential CLI Flags (5 min)

These are the flags you'll use in 90% of automation scripts:

| Flag | What It Does | Example |
| ---- | ------------ | ------- |
| `-p, --print` | Non-interactive mode — prints result and exits | `claude -p "Review this code"` |
| `--model` | Select model: `sonnet`, `opus`, `haiku` | `--model sonnet` |
| `--no-session-persistence` | Ephemeral run — no session saved | `--no-session-persistence` |
| `--max-turns` | Limit agentic turns (prevents runaway scripts) | `--max-turns 10` |
| `--max-budget-usd` | Cap spending per invocation | `--max-budget-usd 1.00` |
| `--output-format` | Output format: `text`, `json`, `stream-json` | `--output-format json` |
| `--allowedTools` | Tools that run without prompting (critical for headless) | `--allowedTools "Bash,Read,Write"` |
| `--append-system-prompt` | Add instructions to default system prompt | `--append-system-prompt "Focus on security"` |

> **Full reference:** The complete flags table (16+ flags including `--json-schema`, `--permission-mode`, `--fallback-model`, `--resume`, `--continue`, `--system-prompt`, `--verbose`, `--add-dir`) is in the [Developer Track](./tracks/developer.md#full-cli-flags-reference).

#### 2.3 Live Demo: Batch Code Reviewer (10 min)

This is the pattern that makes headless mode click. One script that reviews every service file and generates a consolidated report:

```bash
#!/bin/bash
# batch-review.sh - Review multiple files and generate a report
# Usage: ./batch-review.sh src/Services/*.cs

set -e

OUTPUT_FILE="code-review-report.md"
TIMESTAMP=$(date '+%Y-%m-%d %H:%M:%S')

cat > "$OUTPUT_FILE" << EOF
# Code Review Report

**Generated:** $TIMESTAMP
**Reviewed by:** Claude Code
**Files:** $#

---

EOF

echo "Starting batch review of $# files..."

for file in "$@"; do
    if [[ ! -f "$file" ]]; then
        echo "Warning: Skipping (not found): $file"
        continue
    fi

    echo "Reviewing: $file"

    echo "## $(basename "$file")" >> "$OUTPUT_FILE"
    echo "" >> "$OUTPUT_FILE"
    echo "\`$file\`" >> "$OUTPUT_FILE"
    echo "" >> "$OUTPUT_FILE"

    claude -p "Review this file for:
1. Bugs or logic errors
2. Security issues
3. Code style problems
4. Missing error handling

Be concise. Format findings as a bullet list.

File: $file" \
      --model sonnet \
      --no-session-persistence \
      --max-turns 5 \
      --max-budget-usd 0.50 \
      >> "$OUTPUT_FILE" 2>/dev/null || echo "_Review failed_" >> "$OUTPUT_FILE"

    echo "" >> "$OUTPUT_FILE"
    echo "---" >> "$OUTPUT_FILE"
    echo "" >> "$OUTPUT_FILE"
done

echo ""
echo "Report saved to: $OUTPUT_FILE"
echo "Files reviewed: $#"
```

> **Note:** This is simplified for learning. Production scripts should include retry logic, timeouts, and structured error handling. See the [Production Hardening Guide](../../resources/production-hardening.md) for production-ready patterns.

**Run it:**

```bash
cd courses/ai-101-claude-code/sessions/week-8/sandbox/hoa-workflow-automation
chmod +x scripts/batch-review.sh
./scripts/batch-review.sh src/Services/*.cs
```

### Part 3: Working Smarter with Claude (20 min)

#### 3.1 Model Selection (5 min)

| Model | Capability | Speed | Best For |
| ----- | ---------- | ----- | -------- |
| **Sonnet** | High | Fast | Daily driver, general development (default) |
| **Opus** | Highest | Moderate | Complex analysis, architecture, nuanced tasks |
| **Haiku** | Good | Fastest | Quick subagent tasks, simple transformations, low-cost batch jobs |

```bash
# Check and switch models mid-session
/model          # Show current model
/model opus     # Switch for complex work
/model sonnet   # Switch back for implementation

# Track your usage
/stats   # Usage patterns, session history, streaks (all users)
/cost    # API cost breakdown (API key users only — subscription users see a notice instead)
```

**Rule of thumb:** Start with Sonnet. Switch to Opus when you're stuck, when the task requires reasoning across many files, or when you need architectural-level analysis. Use Haiku for quick subagent tasks and low-cost batch jobs.

#### 3.2 Context Management (10 min)

This is one of the most important skills for productive Claude Code usage. Getting this wrong wastes tokens, degrades response quality, and loses valuable session knowledge.

**`/compact` — Your Primary Tool**

`/compact` summarizes your conversation history to free up context space while preserving key information. **Use it with instructions** to tell Claude what to keep:

```bash
# Basic compact
/compact

# Directed compact (much better) — tell Claude what matters
/compact Keep details of the current status and only a short summary of the work leading up to it

# After a long debugging session
/compact Keep the root cause we found and the fix. Summarize the investigation steps briefly.

# During a multi-file refactor
/compact Keep the list of files changed and the pattern we're applying. Drop the file contents.
```

**`/clear` — The Nuclear Option**

`/clear` wipes your entire conversation. This is a **last resort**, not a habit.

Why? Because `/clear` destroys:

- Discoveries you made during debugging
- Failed approaches (so you don't repeat them)
- Context about what you've already tried
- Session knowledge Claude has built up

**The Safe Reset Pattern:**

When you genuinely need a fresh start, protect your progress first:

```text
# Step 1: Ask Claude to capture your current state BEFORE clearing
> "Summarize our current progress, findings, failed approaches, and next steps
   as a detailed plan I can paste back after clearing context."

# Step 2: Copy that summary somewhere (clipboard, file, etc.)

# Step 3: Now clear is safe — you have a written record of progress
/clear

# Step 4: Start fresh with context restored
> "Here's where I left off: [paste summary]. Continue from here."
```

> **Tip:** If you're in plan mode (`/plan`), Claude focuses on planning without executing — useful for complex tasks. But plan mode itself doesn't auto-capture your session state. Ask Claude explicitly for a summary before clearing.

**When to use which:**

| Situation | Tool | Why |
| --------- | ---- | --- |
| Context getting large, responses slowing | `/compact` | Preserves knowledge, frees space |
| Long debugging session, lots of noise | `/compact` with instructions | Keep findings, drop dead ends |
| Completely unrelated new task | `/clear` (or new session) | Clean slate is appropriate |
| Complex task hitting a wall | Summarize progress, then `/clear` | Written summary preserves progress |
| Routine task switch (same project) | `/compact` | Don't lose project context |

#### 3.3 Anti-Patterns to Avoid (5 min)

These are the most common mistakes that waste time and degrade quality. Learn them, avoid them.

**1. Retry Hammering (The #1 Anti-Pattern)**

```text
# BAD CONVERSATION:
User: "Fix the null reference in PropertyService"
Claude: [attempts fix]
User: "That didn't work, fix the null reference in PropertyService"
Claude: [same approach again]
User: "Still broken, fix the null reference in PropertyService"
# ...repeat 7 more times with identical prompt

# GOOD CONVERSATION:
User: "Fix the null reference in PropertyService"
Claude: [attempts fix]
User: "Still failing. Here's the stack trace: [paste trace].
       The error happens when property.Owner is null"
Claude: [now has new information, tries different approach]
User: "Getting closer but now failing on line 47.
       Let me show you the test output: [paste output]"
# Each retry adds NEW information
```

**The rule:** After 2-3 identical attempts, **stop and change your approach**. Add new information (stack traces, test output, error logs), rephrase the problem, or break it into smaller pieces. See the [Decision Trees](../../resources/decision-trees.md#5-when-to-bail-on-claude) for the 3-strike escalation protocol.

**2. Verbose Prompts (Trust Your CLAUDE.md)**

```markdown
# BAD: Wall of text repeating known context
"I'm working on the RealManage HOA system which manages properties and
violations. We use C# .NET 10 with Entity Framework Core. We follow TDD
practices with xUnit and FluentAssertions..."

# GOOD: Concise, trust CLAUDE.md for context
"Add CalculateLateFee(decimal principal, int daysLate) to ViolationService.
Grace: 30d = $0, then 10% monthly compound. Tests first."
```

Your CLAUDE.md already tells Claude about your tech stack, TDD requirements, and project conventions. Don't repeat it.

**3. Context-Nuking (Using /clear When /compact Would Do)**

```markdown
# BAD: Clearing after every task
> [20 minutes debugging a tricky null reference...]
> [Finally found the root cause in the DI configuration...]
> /clear
> "Now fix the related issue in PropertyService"
# You just wiped the knowledge of the root cause!

# GOOD: Compact to preserve knowledge
> [20 minutes debugging a tricky null reference...]
> [Finally found the root cause in the DI configuration...]
> /compact Keep the DI configuration root cause and the fix we applied
> "Now fix the related issue in PropertyService"
# Claude still knows what you found
```

> **Note:** Claude Code handles large repos well — don't overthink context management. Focus on clear communication, not micromanaging what Claude sees.

### Part 4: Continuous Improvement (10 min)

#### 4.1 CLAUDE.md as Living Knowledge Base (5 min)

Whether you're building institutional knowledge for a team or a personal reference, CLAUDE.md is where lessons accumulate:

```markdown
# Project: HOA Management System
# Last Updated: 2025-01-22

## Quick Reference
- API: ASP.NET Core 10 Web API
- Tests: xUnit, FluentAssertions, Moq

## Lessons Learned

### Common Pitfalls
1. Forgetting async/await on SaveChangesAsync
2. Not disposing DbContext in tests
3. Missing CORS configuration for Angular dev

## Effective Prompts (Tested)

### For Bug Fixes
"Find the bug causing [symptom]. Check:
1. Input validation
2. Null handling
3. Async issues
Show fix with test."
```

**Key practice:** Update CLAUDE.md after every significant debugging session or discovery. Future you (and future Claude) will thank you.

#### 4.2 Measuring Productivity Gains (5 min)

Track these metrics to justify and improve your automation:

| Metric | How to Measure | Target |
| ------ | -------------- | ------ |
| Time to PR | Commit to PR creation | -30% |
| Test Coverage | Coverlet reports | 80-90% |
| Bug Escape Rate | Production issues/sprint | -40% |
| Documentation | Pages auto-generated | +200% |
| Code Review Time | PR open to approve | -50% |

**Productivity Report Skill (.claude/skills/productivity-report/SKILL.md):**

```markdown
---
name: productivity-report
description: Generate productivity metrics
argument-hint: [sprint_number]
disable-model-invocation: true
---

Analyze productivity for Sprint $ARGUMENTS.

## Metrics to Calculate:
1. PRs created with Claude assistance
2. Lines of code + test coverage
3. Bugs found during review vs production
4. Documentation pages generated
5. Time saved estimates

## Data Sources:
- Git history for code metrics
- Merge request data
- Jira tickets for bug tracking
- Coverage reports for trends

## Output Format:
### Sprint $ARGUMENTS Productivity Report

**Code Output:**
- Features delivered: X
- Tests written: X (Y% coverage)
- Documentation: X pages

**Quality:**
- Bugs caught in review: X
- Bugs in production: X

**Efficiency:**
- Estimated hours saved: X
- Claude Code cost: $X
- ROI: X hours/$1 spent
```

### Part 5: Track Exercises (45 min)

Split into your role-specific track for hands-on exercises:

| Track | File | Focus |
| ----- | ---- | ----- |
| **Developer** | [developer.md](./tracks/developer.md) | Headless scripts, batch processing, JSON pipelines, parallel processing |
| **PM** | [pm.md](./tracks/pm.md) | Interactive skill workflows, workflow design, automation ROI |
| **QA** | [qa.md](./tracks/qa.md) | Test automation, coverage analysis |
| **Support** | [support.md](./tracks/support.md) | Workflow design exercises |

> **Not sure which track?** Developers and QA engineers should take their respective tracks. PMs and Support staff take theirs. Solo developers — start with the Developer track, then skim PM for workflow design ideas.

### Part 6: Q&A and Wrap-Up (5 min)

**Discussion Questions:**

- Which workflow will have the biggest impact for your team (or your personal productivity)?
- What metrics should we track for ROI?
- How can we share learnings across teams (or capture them for your future self)?

## Key Takeaways

### Context Management Quick Reference

```text
/compact                    Summarize conversation, keep key info
/compact Keep [details]     Directed compact — tell Claude what matters
/clear                      Nuclear option — wipes everything (use sparingly)
Summarize + /clear          Safe reset — ask for summary first, then clear
/model opus                 Switch to Opus for complex work
/stats                      Usage patterns, session history, streaks (all users)
/cost                       API cost breakdown (API key users only)
```

### Anti-Pattern Cheat Sheet

```text
Retry Hammering     Repeating same prompt hoping for different result
                    Fix: Add NEW information with each retry

Verbose Prompts     Repeating context Claude already has from CLAUDE.md
                    Fix: Be concise, trust your project config

Context-Nuking      Using /clear when /compact would preserve knowledge
                    Fix: /compact with instructions, /clear only as last resort
```

### Efficiency Checklist

```text
[ ] Use /compact with instructions for context management
[ ] Create skills for repeated tasks (Week 5)
[ ] Match model to task complexity (Sonnet default, Opus for hard problems)
[ ] Update CLAUDE.md after significant discoveries
[ ] Track productivity metrics monthly
```

## Homework (Before Week 9)

### Required Tasks

1. Complete your track exercises (developer.md, pm.md, qa.md, or support.md)
2. Update your project CLAUDE.md with lessons learned from Weeks 1-8
3. Identify three tasks in your daily work that could benefit from automation
4. Share your best workflow tip in `#ai-exchange`

### Stretch Goals

1. Build a multi-stage processing pipeline (Developer track)
2. Design an automation workflow spec for your team (PM track)
3. Document team efficiency gains with metrics

### Skill Check

Design a complete automation workflow that:

```text
1. Triggers on new violation created
2. Auto-generates first notice letter
3. Schedules follow-up for 30 days
4. Logs all actions for SOC 2 compliance
5. Notifies property manager via Slack
```

## Resources

### Official Documentation

- [Claude Code CLI Reference](https://code.claude.com/docs/en/cli-reference)
- [Claude Code Best Practices](https://code.claude.com/docs/en/best-practices)
- [Context Management](https://code.claude.com/docs/en/context)

### RealManage Resources

- [Week 8 Examples](./examples/) - HOA Workflows project
- Slack: `#ai-exchange` for workflow help

### Additional Reading

- [Bash Scripting Guide](https://www.gnu.org/software/bash/manual/)
- [Measuring Developer Productivity](https://www.microsoft.com/research/group/dev-analytics/)

## Success Metrics

### You're ready for Week 9 when you can

- [ ] Explain what headless mode does and when to use it
- [ ] Use `/compact` with instructions for context management
- [ ] Identify the three main anti-patterns and their fixes
- [ ] Build or design automation appropriate to your role
- [ ] Measure productivity gains objectively

### Red Flags (seek help if)

- [ ] Automation scripts failing consistently
- [ ] Claude responses degrading in quality (check context management)
- [ ] Skills not providing consistent results
- [ ] Workflows not sticking (team not adopting, or you keep reverting to old habits)
- [ ] Metrics showing no improvement

## Next Week Preview

**Week 9: Capstone Hackerspace**

- Choose from capstone project options
- Apply all skills learned in Weeks 1-8
- Build production-ready automation
- Present to team and get certified!

**Pre-work (Required):** Select your capstone project option (A-G) BEFORE the Week 9 session. Review the options in the [Week 9 README](../week-9/README.md#-project-options) and notify your instructor of your choice.

---

## Quick Resources

- [Glossary](../../resources/glossary.md) - Key terms and definitions
- [Decision Trees](../../resources/decision-trees.md) - When to use what
- [Troubleshooting](../../resources/troubleshooting.md) - Common issues and fixes
- [CLI Commands](../../resources/cli-commands.md) - Command cheatsheet
- [Production Hardening](../../resources/production-hardening.md) - Production-ready bash patterns for automation

---

*End of Week 8 Session Plan*
*Next Session: Week 9 - Capstone Hackerspace*

---

**Next:** [Week 9: Capstone & Certification](../week-9/README.md)
