# Production Hardening Guide for Claude Automation

This appendix provides production-ready patterns for Claude CLI automation scripts. The course examples are simplified for learning - use these patterns when building real automation.

---

## Why This Matters

Course examples focus on teaching concepts. Production scripts need:

- **Reliability** - Handle failures gracefully
- **Observability** - Know what happened and why
- **Cost control** - Prevent runaway spending
- **Rate limiting** - Respect API limits

---

## Production-Ready Script Template

```bash
#!/bin/bash
# production-claude-script.sh
# A template for production-ready Claude automation

set -euo pipefail  # Exit on error, undefined vars, pipe failures

# ============================================
# CONFIGURATION
# ============================================
readonly SCRIPT_NAME=$(basename "$0")
readonly LOG_FILE="/var/log/claude-automation/${SCRIPT_NAME}.log"
readonly MAX_RETRIES=3
readonly RETRY_DELAY_BASE=10  # seconds, exponential backoff
readonly TIMEOUT_SECONDS=300  # 5 minutes max per call
readonly MAX_BUDGET_USD=1.00  # Cost cap per invocation

# ============================================
# LOGGING
# ============================================
log() {
    local level="$1"
    shift
    echo "[$(date '+%Y-%m-%d %H:%M:%S')] [$level] $*" | tee -a "$LOG_FILE"
}

log_info()  { log "INFO" "$@"; }
log_warn()  { log "WARN" "$@"; }
log_error() { log "ERROR" "$@"; }

# ============================================
# ERROR HANDLING
# ============================================
cleanup() {
    local exit_code=$?
    if [ $exit_code -ne 0 ]; then
        log_error "Script failed with exit code $exit_code"
        # Add alerting here (Slack, PagerDuty, etc.)
    fi
    exit $exit_code
}
trap cleanup EXIT

# ============================================
# RETRY WITH EXPONENTIAL BACKOFF
# ============================================
run_with_retry() {
    local attempt=1
    local output=""

    while [ $attempt -le $MAX_RETRIES ]; do
        log_info "Attempt $attempt of $MAX_RETRIES"

        if output=$(timeout "${TIMEOUT_SECONDS}s" claude -p "$1" \
            --no-session-persistence \
            --max-budget-usd "$MAX_BUDGET_USD" \
            2>&1); then

            # Validate output isn't empty
            if [ -n "$output" ]; then
                echo "$output"
                return 0
            else
                log_warn "Empty output received"
            fi
        fi

        local exit_code=$?

        # Handle specific exit codes
        case $exit_code in
            124) log_warn "Timeout after ${TIMEOUT_SECONDS}s" ;;
            137) log_error "Process killed (OOM?)" ;;
            *)   log_warn "Failed with exit code $exit_code" ;;
        esac

        # Exponential backoff
        local delay=$((RETRY_DELAY_BASE * attempt))
        log_info "Retrying in ${delay}s..."
        sleep $delay

        ((attempt++))
    done

    log_error "All $MAX_RETRIES attempts failed"
    return 1
}

# ============================================
# RATE LIMITING
# ============================================
# Simple file-based rate limiter
RATE_LIMIT_FILE="/tmp/claude-rate-limit-$$"
RATE_LIMIT_CALLS=10  # Max calls per minute

check_rate_limit() {
    local now=$(date +%s)
    local minute_ago=$((now - 60))

    # Clean old entries
    if [ -f "$RATE_LIMIT_FILE" ]; then
        awk -v cutoff="$minute_ago" '$1 > cutoff' "$RATE_LIMIT_FILE" > "${RATE_LIMIT_FILE}.tmp"
        mv "${RATE_LIMIT_FILE}.tmp" "$RATE_LIMIT_FILE"
    fi

    # Count recent calls
    local count=$(wc -l < "$RATE_LIMIT_FILE" 2>/dev/null || echo 0)

    if [ "$count" -ge "$RATE_LIMIT_CALLS" ]; then
        log_warn "Rate limit reached ($count calls in last minute)"
        sleep 60
    fi

    # Record this call
    echo "$now" >> "$RATE_LIMIT_FILE"
}

# ============================================
# MAIN FUNCTION
# ============================================
main() {
    local input_file="$1"

    log_info "Starting processing of $input_file"

    # Validate input
    if [ ! -f "$input_file" ]; then
        log_error "Input file not found: $input_file"
        exit 1
    fi

    # Check rate limit
    check_rate_limit

    # Run with retry and capture result
    local result
    if result=$(run_with_retry "Analyze this file: $(cat "$input_file")"); then
        log_info "Success!"
        echo "$result"
    else
        log_error "Processing failed for $input_file"
        exit 1
    fi
}

# ============================================
# ENTRY POINT
# ============================================
if [ $# -lt 1 ]; then
    echo "Usage: $SCRIPT_NAME <input_file>"
    exit 1
fi

main "$@"
```

---

## Key Patterns Explained

### 1. Strict Mode

```bash
set -euo pipefail
```

| Flag | Meaning | Why |
| ---- | ------- | --- |
| `-e` | Exit on error | Don't continue after failures |
| `-u` | Error on undefined vars | Catch typos early |
| `-o pipefail` | Pipe failures propagate | `cmd1 | cmd2` fails if cmd1 fails |

### 2. Timeout Handling

```bash
timeout 300s claude -p "..." || {
    echo "Timed out"
    exit 124
}
```

Always wrap Claude calls in `timeout`. Complex prompts can hang.

### 3. Cost Controls

```bash
claude -p "..." --max-budget-usd 1.00
```

Set a budget cap per invocation. For batch jobs processing many files, use lower per-file caps.

### 4. Exponential Backoff

```bash
delay=$((base_delay * attempt))  # 10s, 20s, 30s...
```

API errors often resolve with time. Don't hammer the API.

### 5. Output Validation

```bash
output=$(claude -p "...")
if [ -z "$output" ]; then
    echo "Empty output - treating as failure"
    exit 1
fi
```

Empty output usually means something went wrong.

---

## Batch Processing Pattern

For processing multiple files:

```bash
#!/bin/bash
set -euo pipefail

process_file() {
    local file="$1"
    local output_dir="$2"

    local basename=$(basename "$file" .cs)
    local output_file="$output_dir/${basename}-review.md"

    # Skip if already processed
    if [ -f "$output_file" ]; then
        echo "Skipping (already done): $file"
        return 0
    fi

    echo "Processing: $file"

    if result=$(timeout 120s claude -p "Review: $(cat "$file")" \
        --no-session-persistence \
        --max-budget-usd 0.25 2>&1); then
        echo "$result" > "$output_file"
        echo "Done: $output_file"
    else
        echo "FAILED: $file" >> "$output_dir/failures.log"
    fi

    # Rate limit: 1 second between calls
    sleep 1
}

main() {
    local input_dir="$1"
    local output_dir="$2"

    mkdir -p "$output_dir"

    for file in "$input_dir"/*.cs; do
        process_file "$file" "$output_dir"
    done

    # Report failures
    if [ -f "$output_dir/failures.log" ]; then
        echo "=== FAILURES ==="
        cat "$output_dir/failures.log"
        exit 1
    fi
}

main "$@"
```

---

## Monitoring & Alerting

### Log Aggregation

```bash
# Structured logging for log aggregation (JSON)
log_json() {
    local level="$1"
    local message="$2"
    echo "{\"timestamp\":\"$(date -Iseconds)\",\"level\":\"$level\",\"message\":\"$message\",\"script\":\"$SCRIPT_NAME\"}"
}
```

### Metrics

Track these metrics for your Claude automation:

| Metric | Why |
| ------ | --- |
| Success rate | Are prompts working? |
| Latency (p50, p99) | Performance baseline |
| Token usage | Cost tracking |
| Retry rate | API health indicator |
| Empty output rate | Prompt quality indicator |

### Alerting Thresholds

| Condition | Action |
| --------- | ------ |
| Success rate < 90% | Warning |
| Success rate < 70% | Page on-call |
| p99 latency > 60s | Investigate |
| Daily cost > $50 | Review scripts |

---

## Common Pitfalls

### ❌ Swallowing Errors

```bash
# BAD - hides failures
claude -p "..." 2>/dev/null || true
```

```bash
# GOOD - handle explicitly
if ! result=$(claude -p "..." 2>&1); then
    log_error "Claude failed: $result"
    exit 1
fi
```

### ❌ No Timeout

```bash
# BAD - can hang forever
claude -p "Analyze this huge codebase..."
```

```bash
# GOOD - always timeout
timeout 300s claude -p "..."
```

### ❌ Unbounded Costs

```bash
# BAD - no cost limit
for file in *.cs; do
    claude -p "Review $file"
done
```

```bash
# GOOD - cap per file and total
total_cost=0
max_total=10.00
per_file=0.25

for file in *.cs; do
    if (( $(echo "$total_cost >= $max_total" | bc -l) )); then
        echo "Budget exhausted"
        break
    fi
    claude -p "Review $file" --max-budget-usd $per_file
    total_cost=$(echo "$total_cost + $per_file" | bc)
done
```

---

## Checklist

Before deploying Claude automation to production:

- [ ] `set -euo pipefail` at top of script
- [ ] All Claude calls wrapped in `timeout`
- [ ] `--max-budget-usd` set appropriately
- [ ] Retry logic with exponential backoff
- [ ] Empty output handled as failure
- [ ] Logging to file (not just stdout)
- [ ] Rate limiting between calls
- [ ] Error alerting configured
- [ ] Tested with intentional failures

---

*This guide covers bash scripting. For Python/Node.js automation, see the [Anthropic SDK documentation](https://docs.anthropic.com/en/docs/sdks).*
