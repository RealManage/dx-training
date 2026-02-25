# Week 8: Real-World Automation - Developer Track

**Track:** Developer
**Duration:** 45 minutes (exercises portion of shared session)
**Prerequisites:** Weeks 1-7 completed, comfortable with bash scripting basics

---

This track is the deep-dive into headless automation. The shared session (README) covered concepts, key flags, and a batch reviewer demo. Here you'll build on that with JSON output, pipelines, parallel processing, `--continue` for multi-turn scripts, and the full CLI flags reference.

> **Coming from the shared session?** You've already seen the batch reviewer script and the 8 essential flags. This track covers the remaining flags, advanced patterns, and hands-on exercises.

## Learning Objectives

By the end of this track, you will be able to:

- Use the full set of CLI flags for headless automation
- Build multi-stage processing pipelines
- Use JSON output and schema validation for structured results
- Process files in parallel for faster batch jobs
- Use `--continue` for multi-turn headless scripts
- Apply production hardening patterns to automation scripts

## Full CLI Flags Reference

The shared session covered 8 essential flags. Here's the expanded reference table (for all 40+ flags, see the [official CLI reference](https://code.claude.com/docs/en/cli-reference)):

| Flag | Description | Example |
| ---- | ----------- | ------- |
| `-p, --print` | Non-interactive mode, prints result and exits | `claude -p "Review this code"` |
| `--output-format` | Output format: `text`, `json`, `stream-json` | `--output-format json` |
| `--model` | Select model: `sonnet`, `opus`, `haiku` | `--model opus` |
| `--fallback-model` | Fallback if primary overloaded | `--fallback-model sonnet` |
| `--add-dir` | Add directories to context | `--add-dir ./src` |
| `--max-budget-usd` | Set spending limit for the run | `--max-budget-usd 1.00` |
| `--json-schema` | Validate output against JSON schema | `--json-schema '{"type":"object"}'` |
| `--system-prompt` | Replace the entire default system prompt (use `--append-system-prompt` to add to it instead) | `--system-prompt "You are a reviewer"` |
| `--append-system-prompt` | Append to the default system prompt (safer than replacing) | `--append-system-prompt "Focus on security"` |
| `--max-turns` | Limit agentic turns in print mode (prevents runaway scripts) | `--max-turns 10` |
| `--allowedTools` | Tools that execute without prompting (critical for headless) | `--allowedTools "Bash,Read,Write"` |
| `--permission-mode` | Set permission mode (`plan`, `bypassPermissions`, etc.) | `--permission-mode plan` |
| `--verbose` | Show detailed output | `--verbose` |
| `-r, --resume` | Resume a previous session by ID | `--resume <session-id>` |
| `-c, --continue` | Continue the most recent session | `--continue` |
| `--no-session-persistence` | Don't save the session (ephemeral) | `--no-session-persistence` |

### When to Use Headless Mode

| Use Case | Interactive | Headless |
| -------- | ----------- | -------- |
| Exploratory coding | Yes | |
| Single file review | Yes | |
| Batch file processing | | Yes |
| Generating reports | | Yes |
| Scripted workflows | | Yes |
| Scheduled tasks | | Yes |
| CI/CD integration | | Yes |

> **Important reminder:** Skills (`/skill-name`) are interactive-only — they do **not** work with the `-p` flag. In headless mode, inline the skill's instructions directly into the prompt.

## Exercise 1: Batch Code Reviewer (10 min)

You saw the batch reviewer in the shared session. Now build and run it yourself.

### Set Up Your Sandbox

```bash
# Copy example to sandbox
cd courses/ai-101-claude-code/sessions/week-8
cp -r examples sandbox
cd sandbox/hoa-workflow-automation

# Build and verify
dotnet build
dotnet test

# Create scripts directory
mkdir -p scripts
```

### Create the Script

Create `scripts/batch-review.sh` with the batch reviewer from the shared session README, then:

```bash
chmod +x scripts/batch-review.sh

# Run against the service files
./scripts/batch-review.sh src/Services/*.cs

# View the report
cat code-review-report.md
```

**Expected output:**

```text
Starting batch review of 3 files...

Reviewing: src/Services/BoardReportService.cs
Reviewing: src/Services/CostTrackingService.cs
Reviewing: src/Services/LetterGenerationService.cs

Report saved to: code-review-report.md
Files reviewed: 3
```

### Extend It

Try adding these improvements:

- Add a `--model` flag to accept different models as an argument
- Add a summary section at the end that counts total issues found
- Add timing information (how long each file took to review)

## Exercise 2: JSON Output and Schema Validation (10 min)

### Structured Output for Programmatic Use

```bash
# Get JSON output for parsing
claude -p "Review src/Services/BoardReportService.cs for security issues.
Output JSON with: file, issues array (each with description, severity, line), overall_risk" \
  --output-format json \
  --model sonnet \
  --no-session-persistence
```

### Enforced Schema Validation

```bash
# Enforce specific output structure
claude -p "Analyze src/Services/CostTrackingService.cs for code quality issues" \
  --output-format json \
  --json-schema '{"type":"object","properties":{"file":{"type":"string"},"issues":{"type":"array","items":{"type":"object","properties":{"description":{"type":"string"},"severity":{"type":"string","enum":["low","medium","high"]},"line":{"type":"integer"}}}},"risk_level":{"type":"string"}},"required":["file","issues","risk_level"]}' \
  --model sonnet \
  --no-session-persistence
```

### Parse JSON with jq

```bash
# Pipe JSON output through jq for filtering
# --output-format json wraps the response in a JSON object with a "result" field (string)
# If you asked Claude to output JSON, you may need to parse the result string separately
claude -p "Review src/Services/BoardReportService.cs. Output JSON: {file, issues: [{description, severity, line}]}" \
  --output-format json \
  --model sonnet \
  --no-session-persistence \
  | jq -r '.result' | jq '.issues[] | select(.severity == "high")'
```

> **Note:** With `--output-format json`, Claude wraps its response in a JSON envelope with a `result` field containing the response as a string. If you asked Claude to generate JSON, pipe through `jq -r '.result'` first to extract the string, then parse it as JSON with a second `jq` call.

## Exercise 3: Multi-Stage Pipeline (10 min)

Build a pipeline that processes meeting notes through multiple stages:

```bash
#!/bin/bash
# meeting-pipeline.sh - Process meeting notes through stages
# Usage: ./meeting-pipeline.sh meeting-notes.txt

set -e

INPUT=$1
WORK_DIR="./pipeline-output"
mkdir -p "$WORK_DIR"

if [[ ! -f "$INPUT" ]]; then
    echo "Error: File not found: $INPUT"
    exit 1
fi

echo "Stage 1: Summarizing..."
claude -p "Summarize this meeting in 5 bullet points: $(cat "$INPUT")" \
  --model sonnet \
  --no-session-persistence \
  --max-turns 3 \
  --max-budget-usd 0.25 \
  > "$WORK_DIR/summary.md"

echo "Stage 2: Extracting action items..."
claude -p "Extract action items from this summary.
Format: - [ ] [OWNER] - [TASK] - [DUE DATE]
Group by assignee.

$(cat "$WORK_DIR/summary.md")" \
  --model sonnet \
  --no-session-persistence \
  --max-turns 3 \
  --max-budget-usd 0.25 \
  > "$WORK_DIR/actions.md"

echo "Stage 3: Generating tickets..."
claude -p "Convert these action items to a JSON array.
Each item: {\"summary\": \"...\", \"assignee\": \"...\", \"priority\": \"high|medium|low\"}

$(cat "$WORK_DIR/actions.md")" \
  --output-format json \
  --no-session-persistence \
  --max-turns 3 \
  --max-budget-usd 0.25 \
  > "$WORK_DIR/tickets.json"

echo ""
echo "Pipeline complete! Output in $WORK_DIR/"
echo "  summary.md  - Meeting summary"
echo "  actions.md  - Action items by owner"
echo "  tickets.json - Structured ticket data"
```

**Create a test input file and run it:**

```bash
cat > test-meeting.txt << 'EOF'
Sprint 15 planning meeting. Sarah will complete the violation workflow by Thursday.
Mike needs to review the payment service architecture before billing can start — target Monday.
PM will update stakeholders on timeline change by Friday. QA starts regression testing
on letter generation after Sarah's PR merges. Team agreed to bump coverage target to 85%.
EOF

chmod +x scripts/meeting-pipeline.sh
./scripts/meeting-pipeline.sh test-meeting.txt
```

## Exercise 4: Parallel Processing (5 min)

Process multiple files simultaneously for faster batch jobs:

```bash
#!/bin/bash
# parallel-review.sh - Review files in parallel
# Usage: ./parallel-review.sh src/Services/*.cs

set -e
mkdir -p reviews

echo "Starting parallel review of $# files..."

for file in "$@"; do
    if [[ ! -f "$file" ]]; then
        echo "Skipping (not found): $file"
        continue
    fi

    echo "Spawning review: $file"
    claude -p "Brief code review of $file. Focus on bugs and security. Be concise." \
      --model sonnet \
      --no-session-persistence \
      --max-turns 5 \
      --max-budget-usd 0.50 \
      > "reviews/$(basename "$file" .cs)-review.md" 2>/dev/null &
done

echo "Waiting for all reviews to complete..."
wait
echo "All reviews complete! Check ./reviews/ directory."
```

> **Cost note:** Parallel processing runs multiple Claude instances simultaneously. Use `--max-budget-usd` on each to prevent cost surprises.

## Exercise 5: Multi-Turn Scripts with --continue (5 min)

Use `--continue` to maintain conversation context across multiple headless calls:

```bash
# First call - analyze the architecture
claude -p "Analyze the architecture in src/Services/. Identify patterns and potential issues." \
  --model opus

# Follow-up call - continues with full context from the first call
claude --continue -p "Now suggest specific improvements to the patterns you identified. Prioritize by impact."

# Another follow-up - still has full context
claude --continue -p "Generate a refactoring plan as a numbered checklist. Include estimated effort for each item."
```

This is powerful for complex analysis that builds on previous findings — each call has the full context of previous responses.

## PowerShell Alternative (Windows Reference)

For Windows users who prefer PowerShell, here's the batch reviewer pattern adapted:

```powershell
# batch-review.ps1 - Review multiple files and generate a report
# Usage: .\batch-review.ps1 -Files "src\Services\BoardReportService.cs", "src\Services\CostTrackingService.cs"
#    or: .\batch-review.ps1 -Files (Get-ChildItem src\Services\*.cs | Select-Object -ExpandProperty FullName)

param(
    [Parameter(Mandatory=$true)]
    [string[]]$Files
)

$OutputFile = "code-review-report.md"
$Timestamp = Get-Date -Format "yyyy-MM-dd HH:mm:ss"

@"
# Code Review Report

**Generated:** $Timestamp
**Reviewed by:** Claude Code
**Files:** $($Files.Count)

---

"@ | Out-File -FilePath $OutputFile -Encoding utf8

Write-Host "Starting batch review of $($Files.Count) files..."

foreach ($file in $Files) {
    if (-not (Test-Path $file)) {
        Write-Host "Skipping (not found): $file" -ForegroundColor Yellow
        continue
    }

    Write-Host "Reviewing: $file"

    @"

## $(Split-Path $file -Leaf)

``$file``

"@ | Add-Content -Path $OutputFile -Encoding utf8

    try {
        $review = claude -p "Review this file for bugs, security issues, and style problems. Be concise. Format as bullet list. File: $file" `
          --model sonnet `
          --no-session-persistence `
          --max-turns 5 `
          --max-budget-usd 0.50 2>$null

        $review | Add-Content -Path $OutputFile -Encoding utf8
    }
    catch {
        "_Review failed_" | Add-Content -Path $OutputFile -Encoding utf8
    }

    @"

---

"@ | Add-Content -Path $OutputFile -Encoding utf8
}

Write-Host "Report saved to: $OutputFile" -ForegroundColor Green
Write-Host "Files reviewed: $($Files.Count)"
```

## Anti-Patterns Reference

The three main anti-patterns were covered in the shared session (README Part 3.3). Quick reference:

- **Retry Hammering** — Repeating the same prompt; fix by adding new information with each retry
- **Verbose Prompts** — Repeating context from CLAUDE.md; fix by being concise
- **Context-Nuking** — Using `/clear` when `/compact` would preserve knowledge; fix by using `/compact` with instructions

For the full examples and explanations, see the [shared session README](../README.md#33-anti-patterns-to-avoid-5-min).

---

## Quick Resources

- [Troubleshooting](../../../resources/troubleshooting.md) - Common issues and solutions
- [CLI Commands](../../../resources/cli-commands.md) - Full CLI reference
- [Production Hardening Guide](../../../resources/production-hardening.md) - Production-ready bash patterns for Claude automation
- [Decision Trees](../../../resources/decision-trees.md) - When to bail, when to escalate

---

## Homework (Before Week 9)

### Required Tasks

1. Build and run the batch code reviewer against your own project
2. Create a multi-stage pipeline for a real workflow (meeting notes, code analysis, etc.)
3. Update your project CLAUDE.md with lessons learned
4. Share your best automation script in `#ai-exchange`

### Stretch Goals

1. Build a parallel processing script for your test suite
2. Create a JSON-output pipeline that feeds into another tool (Jira, Slack, etc.)
3. Implement auto-run-tests hook for your projects (from Week 6)
4. Write the PowerShell equivalent of one of your bash scripts

---

*Developer Track - Week 8*
*Headless automation, batch processing, and pipeline patterns for engineers*
