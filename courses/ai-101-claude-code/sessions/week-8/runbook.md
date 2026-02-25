# Week 8: Real-World Automation - Trainer Runbook

## Session Overview

**Duration:** 2 hours
**Format:** In-person or virtual
**Structure:** Shared session (Parts 1-4) + Role-specific track exercises (Part 5)
**Tracks:** Developer, QA, PM, Support

---

## Pre-Session Checklist

### For Instructors

- [ ] Test all example projects build without warnings (`dotnet build` in hoa-workflow-automation)
- [ ] Verify batch-review.sh script runs correctly against example files
- [ ] Test headless CLI flags (`claude -p "hello" --no-session-persistence` works)
- [ ] Have the Week 7 recap ready (plugins, distribution, namespacing)
- [ ] Prepare backup exercises for network issues
- [ ] Monitor `#ai-exchange` Slack channel

---

## Timing Guide

| Time | Segment | What Happens |
| ---- | ------- | ------------ |
| 0:00-0:05 | Welcome & Context | Set the stage: "Last week plugins, this week we automate — and it looks different for every role" |
| 0:05-0:15 | Part 1: What Automation Means for Your Role | Role matrix, automation spectrum, one skill example |
| 0:15-0:35 | Part 2: Headless Mode | Concept, 8 essential flags, live batch reviewer demo |
| 0:35-0:55 | Part 3: Working Smarter with Claude | Model selection, context management (/compact vs /clear), anti-patterns |
| 0:55-1:05 | Part 4: Continuous Improvement | CLAUDE.md as knowledge base, productivity metrics |
| 1:05-1:10 | Break / Track Setup | "Find your track file, get set up" |
| 1:10-1:55 | Part 5: Track Exercises | Role-specific hands-on work (45 min) |
| 1:55-2:00 | Close | Key takeaways, homework, preview Week 9 capstone |

> **Pacing note:** The shared session (Parts 1-4) is designed for ~60 minutes of taught content with 5-minute buffer. If running ahead, use the buffer for Q&A. If running behind, the anti-patterns section (Part 3.3) can be shortened to "reference the README" with a quick verbal summary.

---

## Track Delivery Strategy

### Combined Session (Recommended)

All participants attend Parts 1-4 together. At Part 5, participants split into tracks:

- **Developers** follow [developer.md](./tracks/developer.md) — batch scripts, JSON pipelines, parallel processing
- **QA** follow [qa.md](./tracks/qa.md) — test automation, coverage pipelines
- **PMs** follow [pm.md](./tracks/pm.md) — interactive skill workflows, workflow design, ROI measurement
- **Support** follow [support.md](./tracks/support.md) — ticket triage, response workflows

### Instructor Tips for Mixed Audiences

- **Part 1** is the equalizer — spend time here showing that automation looks different for each role
- **Part 2** headless demo: frame it as "this is what your engineers do" for non-developers. They don't need to memorize the flags.
- **Part 3** anti-patterns: these apply to ALL roles, not just developers. Everyone benefits from understanding retry hammering, verbose prompts, and context-nuking.
- **Part 5** track split: "Non-developers — your track doesn't require bash. PMs, you'll be designing workflows and testing interactive skills."
- For virtual sessions, use breakout rooms by track during Part 5
- Pair non-developers with developers if anyone wants to try the headless exercises

### Key Message for Non-Developers

"You don't need to write bash scripts to automate your work. Your automation is **interactive skills** — the ones you built in Weeks 5 and 7. Today you'll practice using them in real scenarios and learn how to design workflows that engineers can then wrap in scripts."

---

## Live Demo Script

### Part 1 Demo: Role Matrix (3 min)

Walk through the automation spectrum table from the README. Ask participants:

- "What's a task you do repeatedly that could be automated?"
- "Would you automate it with a skill or a script?"

### Part 2 Demo: Headless Mode (15 min)

```bash
# 1. Show the simplest headless example
claude -p "What is 2 + 2?" --no-session-persistence

# 2. Show key automation flags
claude -p "List 3 best practices for C# error handling" \
  --model sonnet \
  --no-session-persistence \
  --max-turns 3

# 3. Show JSON output
claude -p "List 3 C# design patterns as JSON array with name and description" \
  --output-format json \
  --no-session-persistence
```

**Talk through:**

- "`-p` makes Claude non-interactive — it prints the result and exits"
- "`--no-session-persistence` keeps it ephemeral — no session saved"
- "`--max-turns` prevents runaway scripts — critical for automation"
- "`--output-format json` gives structured output for piping into other tools"

### Part 2 Demo: Batch Review Script (10 min)

```bash
# 4. Navigate to sandbox
cd courses/ai-101-claude-code/sessions/week-8/sandbox/hoa-workflow-automation

# 5. Show the batch review script
cat scripts/batch-review.sh

# 6. Run it against one file (for time)
./scripts/batch-review.sh src/Services/BoardReportService.cs

# 7. Show the output
cat code-review-report.md
```

**Talk through:**

- "Each file gets its own Claude call — isolated, no context bleed"
- "`--max-budget-usd` prevents surprise costs in batch jobs"
- "The `|| echo` fallback handles failures gracefully"
- "This is 30 lines of bash that replaces hours of manual review"

### Part 3 Demo: Model Selection (3 min)

```bash
# 8. Show model switching in interactive mode
claude
/model          # Show current model
/model opus     # Switch to Opus
/model sonnet   # Switch back

# 9. Show cost tracking
/stats          # Usage patterns, session history (all users)
/cost           # API cost breakdown (API key users only — subscription users see a notice)

```

**Talk through:**

- "Sonnet handles 90% of daily work — it's the default for a reason"
- "Switch to Opus for architecture reviews, complex debugging, or multi-file refactoring"
- "Haiku is useful for quick subagent tasks and low-cost batch processing"

### Part 3 Demo: Context Management (5 min)

```bash
# 10. Show /compact with instructions
/compact Keep the current task status and drop the investigation details

# 11. Explain the /clear danger
# (Don't actually /clear — just explain verbally)
# "Imagine you just spent 20 minutes debugging. You found the root cause.
#  Then you /clear. Now Claude doesn't know what you found. That's context-nuking."

# 12. Show the safe reset pattern
# "Before clearing, ask Claude to summarize your progress.
#  Copy that summary. THEN /clear. Paste it back to restore context.
#  If you're doing complex planning, /plan enters plan mode — useful
#  but it doesn't auto-capture session state."
```

### Part 3 Demo: Anti-Patterns (3 min)

Quick verbal walkthrough — point participants to the README for the full examples:

1. **Retry hammering:** "If you ask the same question 3 times and get the same wrong answer, the problem is the question, not Claude. Add new info."
2. **Verbose prompts:** "Your CLAUDE.md already tells Claude your tech stack. Don't repeat it every prompt."
3. **Context-nuking:** "Use /compact, not /clear. /clear is the nuclear option."

### Expected Demo Output

**For headless mode:**

- Clean text output to stdout (no interactive UI)
- JSON output parseable with `jq`

**For batch review script:**

- Markdown report with file-by-file findings
- Bullet list format for each reviewed file

**For model switching:**

- `/model` shows current model name
- Switch is immediate — next response uses new model

---

## Instructor Notes

### Key Points to Emphasize

- **Automation looks different per role** — Not everyone writes bash scripts
- **Skills are interactive-only** — They do NOT work with `-p` mode
- **`/compact` is the primary context tool** — `/clear` is a last resort
- **Anti-patterns are universal** — Retry hammering wastes everyone's time, not just developers'
- **Cost awareness** — Use the right model for the job, track with `/stats` (subscription users use `/stats`; API key users also have `/cost`)
- **Safety flags for automation** — Always use `--max-turns` and `--max-budget-usd` in headless scripts

### Common Questions

> **"How do we handle API keys in automation scripts?"**

- Use environment variables (never hardcode)
- Store in secure credential managers
- Reference as `$ANTHROPIC_API_KEY` in scripts
- Never commit keys to repository

> **"How do we ensure consistent output from skills?"**

- Include explicit output format in skill definition
- Test skills with multiple inputs
- Version control skill files
- Review and refine based on results

> **"When should I use Sonnet vs Opus vs Haiku?"**

- **Sonnet:** Daily work, general development (fastest capable model)
- **Opus:** Complex architecture, nuanced analysis (most capable)
- **Haiku:** Quick subagent tasks, simple transformations, low-cost batch (fastest, cheapest)
- Start with Sonnet, switch to Opus for hard problems

> **"Can I use skills in automation scripts?"**

- No — skills (`/skill-name`) are interactive-only and do not work with `-p` mode
- In headless scripts, inline the skill's instructions directly into the prompt
- This is a common mistake — flag it proactively during Part 2

> **"What's the difference between --system-prompt and --append-system-prompt?"**

- `--system-prompt` completely **replaces** the default system prompt
- `--append-system-prompt` **adds to** the default prompt (usually what you want)
- Use `--append-system-prompt` unless you have a specific reason to replace everything

> **"When should I use /compact vs /clear?"**

- `/compact` preserves knowledge and frees context space — use this 90% of the time
- `/clear` wipes everything — only use when starting a truly unrelated task
- Safe pattern: Ask Claude to summarize progress first, copy it, then `/clear` and paste it back

---

## Troubleshooting

**Automation scripts not working:**

- Check script has execute permissions (`chmod +x`)
- Verify Claude CLI is installed and authenticated
- Check environment variables are set
- Test with `--verbose` flag for debugging

**Response quality degrading:**

- Use `/compact` with instructions to clean up context
- Check if using right model for task complexity
- Look for repeated similar prompts (create skills instead)
- As a last resort, use `/plan` then `/clear` for a safe reset

**Skills producing inconsistent results:**

- Make prompts more explicit in the skill definition
- Add example outputs to the skill
- Reduce ambiguity in instructions
- Test with edge cases

**Headless scripts running too long:**

- Add `--max-turns` to limit agentic loop iterations
- Add `--max-budget-usd` to cap spending per invocation
- Use `timeout` command as an outer wrapper: `timeout 60 claude -p "..."`

> **Still having issues?** See the [Troubleshooting Guide](../../resources/troubleshooting.md) for detailed debugging steps.

---

## Session Wrap-Up Checklist

- [ ] All participants understand that automation looks different per role
- [ ] All participants understand headless mode (`-p` flag) and when to use it
- [ ] All participants know skills are interactive-only (not usable in `-p` mode)
- [ ] All participants can explain `/compact` vs `/clear` (and when to use each)
- [ ] All participants can name the three anti-patterns (retry hammering, verbose prompts, context-nuking)
- [ ] All participants know when to use Sonnet vs Opus vs Haiku
- [ ] All participants know how to check usage (`/stats` for everyone; `/cost` for API key users only)
- [ ] Track exercises started (complete as homework if needed)
- [ ] Homework assigned: track exercises + CLAUDE.md update + 3 automation opportunities + share in #ai-exchange
- [ ] Preview of Week 9 delivered: "Week 9 is your capstone — choose a project, apply everything from Weeks 1-8, and present to the team"

---

## Assessment

Quick check at end of session:

1. What CLI flag runs Claude non-interactively?
2. Why can't you use `/release-notes` in a `-p` script?
3. What's the difference between `/compact` and `/clear`? When do you use each?
4. Name the three anti-patterns from today's session.
5. What two safety flags should you always include in production automation scripts?

---

*End of Week 8 Trainer Runbook*
