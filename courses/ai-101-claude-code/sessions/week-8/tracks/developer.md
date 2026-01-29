# Week 8: Real-World Automation - Developer Track

**Track:** Developer (Full Content - Default)
**Duration:** 2 hours
**Prerequisites:** Weeks 1-7 completed

---

This is the complete developer track covering all aspects of real-world automation with Claude Code, including headless automation scripts and batch processing.

## Learning Objectives

By the end of this session, you will be able to:

- Build cross-functional skills for different team workflows
- Create headless automation scripts using Claude Code CLI
- Apply efficiency strategies for optimal Claude Code usage
- Create continuous improvement workflows
- Design batch processing and report generation tools
- Measure and track productivity gains with metrics

## Part 1: Cross-Functional Use Cases (25 min)

### 1.1 Support Team Workflows

As a developer, you may need to create skills that support other teams. Here are patterns for support workflows:

**Ticket Summarization Skill (.claude/skills/ticket-summary/SKILL.md):**

```markdown
---
name: ticket-summary
description: Summarize support ticket for escalation
argument-hint: [ticket_id]
disable-model-invocation: true
---

Analyze support ticket $ARGUMENTS and create an executive summary.

## Summary Format:
1. **Issue Type:** [Category]
2. **Severity:** [High/Medium/Low]
3. **Affected Property:** [Property ID]
4. **Summary:** [2-3 sentence description]
5. **Root Cause:** [If identifiable]
6. **Recommended Action:** [Next steps]
7. **Estimated Resolution:** [Timeline]

## Guidelines:
- Extract key facts from ticket history
- Identify if related to existing violations
- Note any recurring issues for this property
- Flag if board escalation needed
```

**Response Draft Skill (.claude/skills/draft-response/SKILL.md):**

```markdown
---
name: draft-response
description: Draft professional homeowner response
argument-hint: [ticket_id] [response_type]
disable-model-invocation: true
---

Draft a $2 response for ticket $1.

## Response Types:
- acknowledgment: Initial receipt of inquiry
- status_update: Progress update on issue
- resolution: Issue resolved notification
- escalation: Escalating to board/management

## Tone Guidelines:
- Professional but warm
- Empathetic to homeowner concerns
- Clear on next steps
- Include relevant policy references
- Provide contact for follow-up
```

### 1.2 Product Manager Workflows

Skills you can build to help PMs:

**Release Notes Generator (.claude/skills/release-notes/SKILL.md):**

```markdown
---
name: release-notes
description: Generate release notes from git history
argument-hint: [from_tag] [to_tag]
disable-model-invocation: true
---

Generate release notes for changes between $1 and $2.

## Steps:
1. Get commits between tags: git log $1..$2
2. Categorize by type (feat, fix, refactor, docs)
3. Extract ticket references (DX-###)
4. Group by area (API, UI, Database, etc.)

## Output Format:
### Release Notes: [Version]
**Date:** [Today's date]

#### New Features
- [Feature description] (DX-###)

#### Bug Fixes
- [Bug description] (DX-###)

#### Improvements
- [Improvement description]
```

**Sprint Planning Assistant (.claude/skills/sprint-plan/SKILL.md):**

```markdown
---
name: sprint-plan
description: Assist with sprint planning from backlog
argument-hint: [sprint_number] [capacity_points]
disable-model-invocation: true
---

Plan sprint $1 with team capacity of $2 story points.

## Process:
1. Review prioritized backlog items
2. Estimate complexity for unpointed items
3. Select items fitting capacity ($2 points)
4. Identify dependencies between items
5. Flag risks or blockers
```

### 1.3 Engineering Workflows

Core developer skills:

**Code Documentation Generator (.claude/skills/api-docs/SKILL.md):**

```markdown
---
name: api-docs
description: Generate API documentation from code
argument-hint: [controller_path]
disable-model-invocation: true
---

Generate OpenAPI-compatible documentation for $ARGUMENTS.

## Process:
1. Parse controller/endpoint files
2. Extract route definitions
3. Identify request/response models
4. Document authentication requirements
5. Include example payloads
```

**Refactoring Assistant (.claude/skills/refactor/SKILL.md):**

```markdown
---
name: refactor
description: Safely refactor code with test preservation
argument-hint: [file_path] [refactor_type]
disable-model-invocation: true
---

Refactor $1 applying $2 pattern.

## Supported Refactor Types:
- extract_method: Extract repeated code into method
- extract_interface: Create interface from class
- simplify_conditionals: Reduce nesting and complexity
- async_conversion: Convert sync to async/await

## Safety Rules:
- Never break existing tests
- Preserve public API surface
- Add tests for new extracted methods
- Create rollback commit point first
```

## Part 2: Headless Claude Automation (30 min)

### 2.1 Understanding Headless Mode

**What is Headless Automation?**

Claude Code can run without interactive prompts using the `-p` (or `--print`) flag. This enables:

- Batch processing multiple files
- Scripted workflows and pipelines
- Automated report generation
- Integration with other tools

**Key CLI Flags for Automation:**

| Flag | Description | Example |
| ---- | ----------- | ------- |
| `-p, --print` | Non-interactive mode, prints result and exits | `claude -p "Review this code"` |
| `--output-format` | Output format: `text`, `json`, `stream-json` | `--output-format json` |
| `--model` | Select model: `sonnet`, `opus` | `--model opus` |
| `--fallback-model` | Fallback if primary overloaded | `--fallback-model sonnet` |
| `--add-dir` | Add directories to context | `--add-dir ./src` |
| `--max-budget-usd` | Set spending limit for the run | `--max-budget-usd 1.00` |
| `--json-schema` | Validate output against JSON schema | `--json-schema '{"type":"object"}'` |
| `--system-prompt` | Override the system prompt | `--system-prompt "You are a reviewer"` |
| `--verbose` | Show detailed output | `--verbose` |
| `-r, --resume` | Resume a previous session by ID | `--resume abc123` |
| `-c, --continue` | Continue the most recent session | `--continue` |
| `--no-session-persistence` | Don't save the session (ephemeral) | `--no-session-persistence` |

### 2.2 Build a Batch Code Reviewer

Let's build a script that reviews multiple files and generates a consolidated report.

**Create `scripts/batch-review.sh`:**

```bash
#!/bin/bash
# batch-review.sh - Review multiple files and generate a report
# Usage: ./batch-review.sh file1.cs file2.cs file3.cs
#    or: ./batch-review.sh src/Services/*.cs

set -e

OUTPUT_FILE="code-review-report.md"
TIMESTAMP=$(date '+%Y-%m-%d %H:%M:%S')

# Start the report
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
        echo "⚠️  Skipping (not found): $file"
        continue
    fi

    echo "📝 Reviewing: $file"

    echo "## $(basename "$file")" >> "$OUTPUT_FILE"
    echo "\`$file\`" >> "$OUTPUT_FILE"
    echo "" >> "$OUTPUT_FILE"

    # Run Claude review with automation flags
    claude -p "Review this file for bugs, security issues, and style problems. Be concise. Format as bullet list. File: $file" \
      --model sonnet \
      --no-session-persistence \
      >> "$OUTPUT_FILE" 2>/dev/null || echo "_Review failed_" >> "$OUTPUT_FILE"

    echo -e "\n---\n" >> "$OUTPUT_FILE"
done

echo "✅ Report saved to: $OUTPUT_FILE"
```

### 2.3 Advanced Automation Patterns

**JSON Output for Programmatic Use:**

```bash
# Get structured output for parsing
claude -p "Analyze this code. Output JSON: {file, issues[], severity}" \
  --output-format json \
  --model sonnet
```

**Structured Output with JSON Schema:**

```bash
# Enforce specific output structure
claude -p "Analyze this code for security issues" \
  --output-format json \
  --json-schema '{"type":"object","properties":{"issues":{"type":"array"},"risk_level":{"type":"string"}},"required":["issues","risk_level"]}'
```

**Pipeline Pattern (Multi-Stage Processing):**

```bash
#!/bin/bash
# Process through multiple stages
INPUT=$1
mkdir -p ./pipeline-output

echo "Stage 1: Analysis..."
claude -p "Analyze architecture in $INPUT" --model opus --no-session-persistence > ./pipeline-output/analysis.md

echo "Stage 2: Recommendations..."
claude -p "Based on this analysis, suggest improvements: $(cat ./pipeline-output/analysis.md)" \
  --model sonnet --no-session-persistence > ./pipeline-output/recommendations.md

echo "✅ Pipeline complete!"
```

**Using --continue for Multi-Turn Scripts:**

```bash
# First call - analyze
claude -p "Analyze the architecture in src/Services/" --model opus

# Follow-up call - continues the conversation with full context
claude --continue -p "Now suggest improvements to the patterns you identified"
```

**Parallel Processing:**

```bash
# Review files in parallel for faster processing
for file in src/Services/*.cs; do
    claude -p "Brief review of $file" \
      --model sonnet \
      --no-session-persistence \
      > "reviews/$(basename "$file" .cs)-review.md" &
done
wait
echo "All reviews complete"
```

## Part 3: Efficiency & Context Management (20 min)

### 3.1 Model Selection

Choose the right model for the task:

| Model | Capability | Speed | Best For |
| ----- | ---------- | ----- | -------- |
| Sonnet | High | Fast | General development (default) |
| Opus | Highest | Moderate | Complex analysis, architecture |

```bash
# Use Sonnet for most tasks (default)
claude --model sonnet
> "Implement the payment processing service"

# Use Opus for complex architecture decisions
claude --model opus
> "Review the system architecture for scalability issues"
/model sonnet  # Switch back for implementation
```

**When to use Opus:**

- Designing system architecture
- Complex algorithmic problems
- Multi-file refactoring with dependencies
- Security and performance analysis

### 3.2 Context Management

Keep Claude focused for better, faster responses:

```bash
# Compact conversation when context grows large
/compact

# Clear and start fresh for unrelated tasks
/clear

# Use focused context - only relevant directories
claude --add-dir src/Services  # Don't add entire repo

# Check what's in context
/context
```

**Why this matters:** Focused context = better responses. When Claude has 50 irrelevant files in context, response quality degrades.

### 3.3 Efficient Prompting

Write prompts that get to the point:

```markdown
# INEFFICIENT (verbose):
"I'm working on the RealManage HOA system which manages properties and
violations. We have a ViolationService that handles violation tracking.
The system uses C# .NET 10 with Entity Framework Core for data access.
We follow TDD practices and use xUnit with FluentAssertions for testing.
The team uses a repository pattern for data access. I need you to help me
write a method that calculates late fees. Late fees are 10% per month
compounded. There's a 30-day grace period. Can you write this method with
tests?"

# EFFICIENT (concise):
"Add to ViolationService:
CalculateLateFee(decimal principal, int daysLate) -> decimal
- Grace: 30d = $0
- After: 10% monthly compound
- Write tests first"
```

**Why efficient prompts work better:**

- CLAUDE.md already has your tech stack and TDD requirements
- Claude remembers session context—don't repeat it
- Concise prompts = faster responses
- Less noise = more accurate results

### 3.4 Anti-Patterns to Avoid

Common mistakes that reduce quality or waste your time:

| Anti-Pattern | Problem | Solution |
| ------------ | ------- | -------- |
| **No `/clear` Discipline** | Context grows stale between unrelated tasks | Clear when switching contexts |
| **Verbose Prompts** | Repeating context Claude already has | Reference CLAUDE.md, be concise |
| **Retry Hammering** | Same prompt 10x hoping for different result | After 3 tries, change approach |
| **Never Using /compact** | Very long sessions can slow down | Compact periodically on big tasks |
| **Ignoring the 3-Strike Rule** | Circular conversations waste time | See [Decision Trees](../../../resources/decision-trees.md#5-when-to-bail-on-claude) |

> **Note:** Claude Code handles large repos well—don't overthink context management. It's smart enough to search and read what it needs. Focus on clear communication, not micromanaging files.

#### Before/After Examples

**Retry Hammering (The #1 Anti-Pattern):**

```markdown
# ❌ BAD CONVERSATION:
User: "Fix the null reference in PropertyService"
Claude: [attempts fix]
User: "That didn't work, fix the null reference in PropertyService"
Claude: [same approach again]
User: "Still broken, fix the null reference in PropertyService"
# ...repeat 7 more times with identical prompt

# ✅ GOOD CONVERSATION:
User: "Fix the null reference in PropertyService"
Claude: [attempts fix]
User: "Still failing. Here's the stack trace: [paste trace]. The error happens when property.Owner is null"
Claude: [now has new information, tries different approach]
User: "Getting closer but now failing on line 47. Let me show you the test output: [paste output]"
# Each retry adds NEW information
```

**Verbose Prompts:**

```markdown
# ❌ BAD: Wall of text repeating known context
"I'm working on the RealManage HOA system which manages properties and
violations. We use C# .NET 10 with Entity Framework Core. We follow TDD
practices with xUnit and FluentAssertions. The team uses repository pattern.
I need you to write a method that calculates late fees with 10% monthly
compound interest and a 30-day grace period. Can you write this with tests?"

# ✅ GOOD: Concise, trust CLAUDE.md for context
"Add CalculateLateFee(decimal principal, int daysLate) to ViolationService.
Grace: 30d = $0, then 10% monthly compound. Tests first."
```

**No /clear Discipline:**

```markdown
# ❌ BAD: One endless session for everything
> [Deep in ViolationService refactor with 50 files in context...]
> [20 minutes later...]
> "Now help me write CSS for the dashboard"
# Context is polluted with irrelevant C# history

# ✅ GOOD: Fresh start for unrelated task
> [Deep in ViolationService refactor...]
> /clear
> "Help me style the dashboard header"
# Clean slate, focused response
```

### 3.5 Reusable Patterns

Don't repeat yourself—create skills for common tasks:

```markdown
# Instead of typing the same prompt repeatedly...
# Save to: .claude/skills/fine-calc/SKILL.md
```

See Week 5 for building commands and skills.

## Part 4: Continuous Improvement Workflows (20 min)

### 4.1 Building a Team Knowledge Base

**Evolving Your CLAUDE.md:**

```markdown
# Project: HOA Management System
# Last Updated: 2025-01-22

## Lessons Learned

### 2025-01 Discoveries
- Use `[FromQuery]` not `[FromBody]` for GET params
- FluentAssertions `.Should().BeEquivalentTo()` ignores order
- EF Core 9 requires explicit `Include()` for navigation

### Common Pitfalls
1. Forgetting async/await on SaveChangesAsync
2. Not disposing DbContext in tests
3. Missing CORS configuration for Angular dev
```

### 4.2 Measuring Productivity Gains

**Metrics to Track:**

| Metric | How to Measure | Target |
| ------ | -------------- | ------ |
| Time to PR | Commit to PR creation | -30% |
| Test Coverage | Coverlet reports | 80-90% |
| Bug Escape Rate | Production issues/sprint | -40% |
| Documentation | Pages auto-generated | +200% |
| Code Review Time | PR open to approve | -50% |

## Part 5: Hands-On Workshop (20 min)

### 5.1 Set Up Your Sandbox

```bash
# Copy example to sandbox
cd courses/ai-101-claude-code/sessions/week-8
cp -r examples sandbox
cd sandbox/hoa-workflow-automation

# Build and verify
dotnet build
dotnet test

# Start Claude
claude
```

### 5.2 Workshop Exercises

**Exercise 1: Create Cross-Functional Skill (10 min)**

```
Create a skill that generates a weekly status report:
1. Takes sprint_number as argument
2. Summarizes completed work from git commits
3. Lists open violations requiring attention
4. Calculates late fee revenue this week
5. Notes any escalations to board

Save to: .claude/skills/weekly-status/SKILL.md
```

**Exercise 2: Build Batch Review Script (5 min)**

```
Create an automation script that:
1. Takes a list of files as arguments
2. Runs Claude review on each file
3. Generates a consolidated report
4. Saves to code-review-report.md

Add to: scripts/batch-review.sh

```

**Exercise 3: Efficiency Audit (5 min)**

```

Analyze the example project for efficiency improvements:

1. Identify repeated prompts that could be skills
2. Find tasks that could use more focused context
3. Create a streamlined version of the project CLAUDE.md

```

---

## 📚 Quick Resources

- [Troubleshooting](../../../resources/troubleshooting.md) - Common issues and solutions
- [CLI Commands](../../../resources/cli-commands.md) - Full CLI reference
- [Production Hardening Guide](../../../resources/production-hardening.md) - Production-ready bash patterns for Claude automation

---

## Homework (Before Week 9)

### Required Tasks

1. Create three cross-functional skills for your team
2. Build a batch automation script using Claude Code CLI
3. Update your project CLAUDE.md with lessons learned
4. Share your best workflow in `#ai-exchange`

### Stretch Goals

1. Build a multi-stage processing pipeline
2. Create team productivity dashboard
3. Implement auto-run-tests hook for your projects

---

*Developer Track - Week 8*
*Full content for engineers implementing headless automation*
