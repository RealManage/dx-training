# Week 8: Real-World Automation

**Duration:** 2 hours
**Format:** In-person or virtual
**Audience:** RealManage cross-functional team (engineers, PMs, support staff)

## Learning Tracks

This week has role-specific tracks:

- **Developer Track** - Full automation content (this README)
- **[QA Track](./tracks/qa.md)** - Test automation patterns
- **[PM Track](./tracks/pm.md)** - Workflow automation design
- **[Support Track](./tracks/support.md)** - Ticket triage and response automation

---

## Learning Objectives

By the end of this session, participants will be able to:

- Apply Claude Code to cross-functional RealManage workflows
- Build headless automation scripts using Claude Code CLI
- Implement efficiency and context management strategies
- Build continuous improvement workflows
- Create batch processing and report generation tools
- Measure and track productivity gains

## Pre-Session Checklist

### For Participants

- [ ] Completed Weeks 1-7 (especially skills from Week 7)
- [ ] Comfortable with bash scripting basics
- [ ] Access to a test repository for exercises
- [ ] Ready for 2-hour session

> **Note:** Not familiar with bash scripting? That's okay - ask Claude to explain any command or write scripts for you. This applies to anything in the course: if you're stuck, Claude is your pair programming partner.

## Session Plan

### Part 1: Cross-Functional Use Cases (25 min)

#### 1.1 Support Team Workflows (10 min)

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

## Template Elements:
1. Greeting with owner name
2. Reference to original inquiry
3. Status/information update
4. Next steps (if any)
5. Closing with contact info
```

#### 1.2 Product Manager Workflows (10 min)

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

#### Breaking Changes
- [If any, with migration notes]

#### Known Issues
- [Ongoing issues if any]
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

## Output:
### Sprint $1 Plan

**Capacity:** $2 points
**Selected Items:** [List with points]
**Total Points:** [Sum]
**Buffer:** [Remaining capacity]

**Dependencies:**
- [Item A] blocks [Item B]

**Risks:**
- [Identified risks]

**Recommended Assignments:**
- [Item] -> [Team member rationale]
```

#### 1.3 Engineering Workflows (5 min)

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

## Output Format:
### [Controller Name] API

#### Endpoints

##### [METHOD] /api/[route]
**Description:** [Purpose]
**Authentication:** [Required/Optional]

**Request:**
\`\`\`json
{ "example": "payload" }
\`\`\`

**Response (200):**
\`\`\`json
{ "example": "response" }
\`\`\`

**Errors:**
- 400: [When/why]
- 401: [When/why]
- 404: [When/why]
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

## Process:
1. Read existing code and tests
2. Identify refactoring opportunities
3. Apply refactoring incrementally
4. Run tests after each change
5. Document changes made

## Safety Rules:
- Never break existing tests
- Preserve public API surface
- Add tests for new extracted methods
- Create rollback commit point first
```

### Part 2: Headless Claude Automation (30 min)

#### 2.1 Understanding Headless Mode (5 min)

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
| `-r, --resume` | Resume a previous session by ID | `--resume <session-id>` |
| `-c, --continue` | Continue the most recent session | `--continue` |
| `--no-session-persistence` | Don't save the session (ephemeral) | `--no-session-persistence` |

**When to Use Headless Mode:**

| Use Case | Interactive | Headless |
| -------- | ----------- | -------- |
| Exploratory coding | ✅ | |
| Single file review | ✅ | |
| Batch file processing | | ✅ |
| Generating reports | | ✅ |
| Scripted workflows | | ✅ |
| Scheduled tasks | | ✅ |

#### 2.2 Build a Batch Code Reviewer (15 min)

Let's build a script that reviews multiple files and generates a consolidated report.

**Create the Script:**

Create `scripts/batch-review.sh` in your sandbox:

```bash
#!/bin/bash
# batch-review.sh - Review multiple files and generate a report
# Usage: ./batch-review.sh file1.cs file2.cs file3.cs
#    or: ./batch-review.sh src/Services/*.cs

set -e

# Configuration
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
echo ""

# Review each file
for file in "$@"; do
    if [[ ! -f "$file" ]]; then
        echo "⚠️  Skipping (not found): $file"
        continue
    fi

    echo "📝 Reviewing: $file"

    # Add file header to report
    echo "## $(basename "$file")" >> "$OUTPUT_FILE"
    echo "" >> "$OUTPUT_FILE"
    echo "\`$file\`" >> "$OUTPUT_FILE"
    echo "" >> "$OUTPUT_FILE"

    # Run Claude review with automation flags
    claude -p "Review this file for:
1. Bugs or logic errors
2. Security issues
3. Code style problems
4. Missing error handling

Be concise. Format findings as a bullet list.

File: $file" \
      --model sonnet \
      --no-session-persistence \
      >> "$OUTPUT_FILE" 2>/dev/null || echo "_Review failed_" >> "$OUTPUT_FILE"

    echo "" >> "$OUTPUT_FILE"
    echo "---" >> "$OUTPUT_FILE"
    echo "" >> "$OUTPUT_FILE"
done

echo ""
echo "✅ Report saved to: $OUTPUT_FILE"
echo "   Files reviewed: $#"
```

> **📝 Note:** These examples are simplified for learning. Production automation scripts should include retry logic, error handling, cost controls (`--max-budget-usd`), and timeouts. The goal here is to understand the patterns, not create production-ready code.

**Make it executable:**

```bash
chmod +x scripts/batch-review.sh
```

**PowerShell Alternative (Windows):**

For Windows users who prefer PowerShell, create `scripts/batch-review.ps1`:

```powershell
# batch-review.ps1 - Review multiple files and generate a report
# Usage: .\batch-review.ps1 file1.cs file2.cs file3.cs
#    or: .\batch-review.ps1 (Get-ChildItem src\Services\*.cs)

param(
    [Parameter(Mandatory=$true, ValueFromPipeline=$true)]
    [string[]]$Files
)

# Configuration
$OutputFile = "code-review-report.md"
$Timestamp = Get-Date -Format "yyyy-MM-dd HH:mm:ss"

# Start the report
@"
# Code Review Report

**Generated:** $Timestamp
**Reviewed by:** Claude Code
**Files:** $($Files.Count)

---

"@ | Out-File -FilePath $OutputFile -Encoding utf8

Write-Host "Starting batch review of $($Files.Count) files..."
Write-Host ""

# Review each file
foreach ($file in $Files) {
    if (-not (Test-Path $file)) {
        Write-Host "⚠️  Skipping (not found): $file" -ForegroundColor Yellow
        continue
    }

    Write-Host "📝 Reviewing: $file"

    # Add file header to report
    @"

## $(Split-Path $file -Leaf)

``$file``

"@ | Add-Content -Path $OutputFile -Encoding utf8

    # Run Claude review with automation flags
    try {
        $review = claude -p @"
Review this file for:
1. Bugs or logic errors
2. Security issues
3. Code style problems
4. Missing error handling

Be concise. Format findings as a bullet list.

File: $file
"@ --model sonnet --no-session-persistence 2>$null

        $review | Add-Content -Path $OutputFile -Encoding utf8
    }
    catch {
        "_Review failed_" | Add-Content -Path $OutputFile -Encoding utf8
    }

    @"

---

"@ | Add-Content -Path $OutputFile -Encoding utf8
}

Write-Host ""
Write-Host "✅ Report saved to: $OutputFile" -ForegroundColor Green
Write-Host "   Files reviewed: $($Files.Count)"
```

**Run in PowerShell:**

```powershell
# Single files
.\scripts\batch-review.ps1 -Files "src\Services\BoardReportService.cs", "src\Services\CostTrackingService.cs"

# Wildcard pattern
.\scripts\batch-review.ps1 -Files (Get-ChildItem src\Services\*.cs | Select-Object -ExpandProperty FullName)
```

#### 2.3 Run the Batch Reviewer (10 min)

**Exercise: Review the Example Services**

```bash
# Navigate to your sandbox
cd courses/ai-101-claude-code/sessions/week-8/sandbox/hoa-workflow-automation

# Create scripts directory
mkdir -p scripts

# Create the batch-review.sh script (copy from above)
# Then make it executable
chmod +x scripts/batch-review.sh

# Run against the service files
./scripts/batch-review.sh src/Services/*.cs
```

**Expected Output:**

```
Starting batch review of 3 files...

📝 Reviewing: src/Services/BoardReportService.cs
📝 Reviewing: src/Services/CostTrackingService.cs
📝 Reviewing: src/Services/LetterGenerationService.cs

✅ Report saved to: code-review-report.md
   Files reviewed: 3
```

**View the Report:**

```bash
cat code-review-report.md
```

#### 2.4 Advanced Automation Patterns *(Stretch Goal)*

> **Note:** These patterns are valuable but optional during the session. Focus on Parts 2.1-2.3 first. Review these as homework if time runs short.

**JSON Output for Programmatic Use:**

```bash
# Get structured output for parsing
claude -p "Review $file. Output JSON with: file, issues array, severity (low/medium/high)" \
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
# meeting-pipeline.sh - Process meeting notes through stages

INPUT=$1
WORK_DIR="./pipeline-output"
mkdir -p "$WORK_DIR"

echo "Stage 1: Summarizing..."
claude -p "Summarize this meeting in 5 bullet points: $(cat "$INPUT")" \
  --model sonnet --no-session-persistence > "$WORK_DIR/summary.md"

echo "Stage 2: Extracting action items..."
claude -p "Extract action items. Format: - [ ] [OWNER] - [TASK]
$(cat "$WORK_DIR/summary.md")" \
  --model sonnet --no-session-persistence > "$WORK_DIR/actions.md"

echo "Stage 3: Generating tickets..."
claude -p "Convert to JSON array with summary, assignee, priority:
$(cat "$WORK_DIR/actions.md")" \
  --output-format json --no-session-persistence > "$WORK_DIR/tickets.json"

echo "✅ Pipeline complete! Output in $WORK_DIR/"
```

**Parallel Processing (Advanced):**

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

**Using --continue for Multi-Turn Scripts:**

```bash
# First call - analyze
claude -p "Analyze the architecture in src/Services/" --model opus

# Follow-up call - continues the conversation
claude --continue -p "Now suggest improvements to the patterns you identified"
```

### Part 3: Model Selection & Context Management (20 min)

#### 3.1 Choosing the Right Model (10 min)

**Available Models:**

| Model | Capability | Speed | Best For |
| ----- | ---------- | ----- | -------- |
| **Sonnet** | High | Fast | Daily driver, general development (default) |
| **Opus** | Highest | Moderate | Complex analysis, architecture, nuanced tasks |

**Model Selection Commands:**

```bash
# Check current model
/model

# Switch models mid-session
/model opus    # For complex architectural decisions
/model sonnet  # Switch back for implementation
```

**When to Use Opus:**

- Complex architectural decisions
- Multi-file refactoring with many dependencies
- Nuanced code review requiring deep understanding
- Debugging subtle, hard-to-find issues
- Tasks requiring strong reasoning across large contexts

**Sonnet as Your Default:**
Sonnet handles 90%+ of development tasks excellently. Use it for:

- Feature implementation
- Writing tests
- Code generation
- Standard refactoring
- Documentation

#### 3.2 Context Management Strategies (10 min)

**Strategy 1: Model Selection for Task Type**

```bash
# Sonnet for implementation (default)
claude --model sonnet
> "Implement the payment processing service with TDD"

# Opus for complex analysis
claude --model opus
> "Review the system architecture for scalability issues"
/model sonnet  # Switch back for implementation
```

**Strategy 2: Context Management**

```bash
# Compact conversation to save tokens
/compact

# Clear and start fresh for new task
/clear

# Use focused context
claude --add-dir src/Services  # Only relevant code
```

**Strategy 3: Efficient Prompting**

```markdown
# INEFFICIENT (verbose):
"I have a C# service that handles HOA violations. The service needs to
calculate fines based on how long the violation has been open. We use
30/60/90 day escalation. Can you help me write this method? I want it
to use compound interest at 10% monthly after 60 days."

# EFFICIENT (concise):
"Write C# method: CalculateFine(Violation v)
- 0-30d: $0
- 31-60d: $50
- 61-90d: $100 + 10% monthly compound
- 90+d: Same formula, flag for board"
```

**Strategy 4: Caching and Reuse**

```markdown
# Create reusable skills instead of repeating prompts
# Save to: .claude/skills/fine-calc/SKILL.md

---
name: fine-calc
description: Calculate violation fine
argument-hint: [days_since_report]
disable-model-invocation: true
---

Calculate fine for violation reported $ARGUMENTS days ago:
- 0-30d: $0 (warning)
- 31-60d: $50 (first notice)
- 61-90d: $100 + 10% compound (second notice)
- 90+d: Continue compound, flag board review

Show calculation breakdown.
```

**Productivity Dashboard Skill (.claude/skills/productivity-report/SKILL.md):**

```markdown
---
name: productivity-report
description: Generate productivity analysis report
argument-hint: [time_period]
disable-model-invocation: true
---

Analyze development productivity for $ARGUMENTS.

## Metrics to Include:
1. PRs merged and review turnaround
2. Test coverage trends
3. Bug escape rate
4. Common patterns in commits
5. Skill usage frequency

## Recommendations:
- Repeated tasks that should become skills
- Patterns that could be automated
- Context management improvements
```

### Part 4: Continuous Improvement Workflows (20 min)

#### 4.1 Building a Team Knowledge Base (10 min)

**Evolving Your CLAUDE.md:**

```markdown
# Project: HOA Management System
# Last Updated: 2025-01-22

## Quick Reference
- API: ASP.NET Core 10 Web API
- UI: Angular 17 standalone
- Tests: xUnit, FluentAssertions, Moq

## Lessons Learned

### 2025-01 Discoveries
- Use `[FromQuery]` not `[FromBody]` for GET params
- FluentAssertions `.Should().BeEquivalentTo()` ignores order
- EF Core 9 requires explicit `Include()` for navigation

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

### For New Features
"Implement [feature] following:
1. Existing patterns in [similar file]
2. TDD approach
3. 80-90% coverage target"

## Team Conventions
- PR titles: type(scope): description
- Commits: conventional commits format
- Reviews: Always run /code-review skill first
```

**Knowledge Capture Skill (.claude/skills/capture-learning/SKILL.md):**

```markdown
---
name: capture-learning
description: Capture learning from current session
argument-hint: [topic]
disable-model-invocation: true
---

Document what we learned about $ARGUMENTS in this session.

## Format:
### Learning: $ARGUMENTS
**Date:** [Today]
**Context:** [What we were trying to do]

**Discovery:**
[Key insight or pattern discovered]

**Example:**
[Code snippet or command]

**When to Apply:**
[Future scenarios where this helps]

**Gotchas:**
[What to watch out for]

Append to CLAUDE.md under "## Lessons Learned"
```

#### 4.2 Measuring Productivity Gains (10 min)

**Metrics to Track:**

| Metric | How to Measure | Target |
| ------ | -------------- | ------ |
| Time to PR | Commit to PR creation | -30% |
| Test Coverage | Coverlet reports | 80-90% |
| Bug Escape Rate | Production issues/sprint | -40% |
| Documentation | Pages auto-generated | +200% |
| Code Review Time | PR open to approve | -50% |

**Productivity Dashboard Skill (.claude/skills/productivity-report/SKILL.md):**

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
- Review feedback addressed: X%

**Efficiency:**
- Estimated hours saved: X
- Claude Code cost: $X
- ROI: X hours/$1 spent
```

### Part 5: Hands-On Workshop (20 min)

#### 5.1 Set Up Your Sandbox

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

#### 5.2 Workshop Exercises

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

### Part 6: Q&A and Reflection (5 min)

**Discussion Questions:**

- Which workflow will have the biggest impact for your team?
- What metrics should we track for ROI?
- How can we share learnings across teams?

**Quick Poll:**

1. Most valuable topic from today (1-5)?
2. Will you build batch automation scripts?
3. What will you automate first?

## 🎯 Key Takeaways

### Cross-Functional Skills Quick Reference

```
SUPPORT:
/ticket-summary <id>           -> Summarize for escalation
/draft-response <id> <type>    -> Write professional reply

PM:
/release-notes <from> <to>     -> Generate changelog
/sprint-plan <num> <pts>       -> Plan sprint items

ENGINEERING:
/api-docs <path>               -> Generate API documentation
/refactor <file> <type>        -> Safe refactoring
```

### Headless Automation Patterns

```
BATCH CODE REVIEW:
- Input: List of files to review
- Action: Run Claude review on each
- Output: Consolidated report

MULTI-STAGE PIPELINE:
- Stage 1: Summarize/analyze
- Stage 2: Extract action items
- Stage 3: Generate output (JSON, tickets, etc.)

PARALLEL PROCESSING:
- Input: Multiple files
- Action: Review in parallel (background jobs)
- Output: Individual review files
```

### Efficiency Checklist

```
[ ] Use /compact for context management
[ ] Create skills for repeated tasks
[ ] Clear context between unrelated tasks
[ ] Match model to task complexity (Sonnet vs Opus)
[ ] Track productivity metrics monthly
```

## Homework (Before Week 9)

### Required Tasks

1. Create three cross-functional skills for your team
2. Build a batch automation script (code review, test analysis, etc.)
3. Implement auto-run-tests hook for your projects
4. Update your project CLAUDE.md with lessons learned
5. Share your best workflow in `#ai-exchange`

### Stretch Goals

1. Build a multi-stage processing pipeline
2. Create team productivity dashboard
3. Document team efficiency gains with metrics

### Skill Check

Design a complete automation workflow that:

```
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

- [ ] Create cross-functional skills for different team roles
- [ ] Build headless automation scripts with Claude CLI
- [ ] Manage context effectively for quality responses
- [ ] Build continuous improvement into your workflow
- [ ] Measure productivity gains objectively
- [ ] Share learnings through CLAUDE.md updates

### Red Flags (seek help if)

- [ ] Automation scripts failing consistently
- [ ] Claude responses degrading in quality
- [ ] Skills not providing consistent results
- [ ] Team not adopting new workflows
- [ ] Metrics showing no improvement

## Next Week Preview

**Week 9: Capstone Hackerspace**

- Choose from three capstone options
- Apply all skills learned in Weeks 1-8
- Build production-ready automation
- Present to team and get certified!

**Pre-work (Required):** Select your capstone project option (A-G) BEFORE the Week 9 session. Review the options in the [Week 9 README](../week-9/README.md#-project-options) and notify your instructor of your choice.

---

## 📚 Quick Resources

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
