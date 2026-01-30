# Claude Code Troubleshooting Guide 🔧

Common issues and solutions for RealManage developers using Claude Code.

## Official Documentation

- [CLI Reference](https://code.claude.com/docs/en/cli-reference)
- [Interactive Mode](https://code.claude.com/docs/en/interactive-mode)
- [Troubleshooting](https://docs.anthropic.com/en/docs/claude-code/troubleshooting)

## 📊 Test Coverage Target Rationale

**Why 80-90% coverage instead of 95-100%?**

80-90% coverage is the recommended target because it provides strong confidence in code quality while remaining maintainable. Achieving 100% coverage often leads to brittle tests that test implementation rather than behavior. The final 10-20% of coverage typically requires testing edge cases that provide diminishing returns and can make refactoring harder without meaningful improvement in code quality.

---

## 🚨 Installation Issues

### nvm Not Found

**Windows (nvm-windows):**

```powershell
# Close ALL terminals
# Open NEW terminal as Administrator
nvm version

# If still not found, reinstall from:
# https://github.com/coreybutler/nvm-windows/releases
```

**Mac/Linux/WSL:**

```bash
# Reload shell configuration
source ~/.bashrc  # or ~/.zshrc for Zsh

# If still not found, reinstall:
curl -o- https://raw.githubusercontent.com/nvm-sh/nvm/v0.40.1/install.sh | bash
```

### Node Version Wrong

```bash
# Check current version
node --version

# List installed versions
nvm list     # Windows
nvm ls       # Mac/Linux

# Install Node 22 LTS
nvm install lts      # Windows
nvm install --lts   # Mac/Linux

# Switch to Node 22
nvm use lts          # Windows
nvm use --lts       # Mac/Linux
```

### Claude Command Not Found

**Check npm global path:**

```bash
npm config get prefix

# Windows Git Bash - Add to PATH:
echo 'export PATH=$PATH:/c/Users/'$USER'/AppData/Roaming/npm' >> ~/.bashrc
source ~/.bashrc

# Mac/Linux - Add to PATH:
echo 'export PATH=$PATH:$(npm config get prefix)/bin' >> ~/.bashrc
source ~/.bashrc
```

**Reinstall Claude Code:**

```bash
npm uninstall -g @anthropic-ai/claude-code
npm install -g @anthropic-ai/claude-code
```

### Permission Denied

**DO NOT use sudo!** Instead:

```bash
# Check npm prefix
npm config get prefix

# If it shows /usr/local, change it:
npm config set prefix ~/.npm-global
mkdir -p ~/.npm-global
echo 'export PATH=$PATH:~/.npm-global/bin' >> ~/.bashrc
source ~/.bashrc

# Reinstall Claude Code
npm install -g @anthropic-ai/claude-code
```

## 🔐 Authentication Issues

### OAuth Fails

1. **Disable VPN/Firewall temporarily**
2. **Check browser:**
   - Clear cookies for anthropic.com
   - Try different browser
   - Disable ad blockers

3. **Use API key instead:**

```bash
export ANTHROPIC_API_KEY="your-key-here"
claude
```

### Session Expires Frequently

- Check network stability
- Ensure system time is correct
- Try `claude --no-cache`

## 💻 Git Bash Specific Issues

### Claude Hangs or Doesn't Respond

```bash
# Use winpty wrapper
winpty claude

# Add alias for convenience
echo "alias claude='winpty claude'" >> ~/.bashrc
source ~/.bashrc
```

### Colors Not Displaying

```bash
# Enable colors in Git Bash
export FORCE_COLOR=1
claude
```

### Path Issues with Portable Git

```powershell
# In PowerShell (before opening Git Bash)
$env:CLAUDE_CODE_GIT_BASH_PATH="C:\PortableGit\bin\bash.exe"
```

## 🌐 Corporate Environment

### Behind Proxy

```bash
# Set npm proxy
npm config set proxy http://proxy.company.com:8080
npm config set https-proxy http://proxy.company.com:8080

# Set environment variables
export HTTP_PROXY=http://proxy.company.com:8080
export HTTPS_PROXY=http://proxy.company.com:8080

# For Windows, use set instead of export
set HTTP_PROXY=http://proxy.company.com:8080
set HTTPS_PROXY=http://proxy.company.com:8080
```

### SSL Certificate Issues

```bash
# Temporary workaround (NOT for production!)
npm config set strict-ssl false

# Better solution - add corporate cert:
npm config set cafile /path/to/corporate-cert.pem
```

## 🎯 Claude Code Runtime Issues

### High Token Usage / Costs

**Symptoms:** Costs exceeding $5/hour

**Solutions:**

- Use `/clear` when switching contexts
- Limit context with `/add-dir` specific directories
- Use `/compact` to compress conversation
- Check for large files accidentally included

### Claude Can't See Files

**Check current directory:**

```bash
pwd
ls -la
```

**Start Claude in correct directory:**

```bash
cd /path/to/project
claude
```

**Add directories explicitly:**

```
/add-dir ./src
/add-dir ./tests
```

### Generated Code Has Errors

**Common causes:**

1. **Vague prompts** → Be more specific
2. **Missing context** → Update CLAUDE.md
3. **Wrong model** → Use `/model` to switch
4. **Outdated patterns** → Specify versions explicitly

### Tests Failing After Generation

**Best practices:**

1. Never let Claude modify existing tests
2. Run tests before accepting changes
3. Use TDD approach:

   ```
   > Write tests for <feature>
   > [Run tests - should fail]
   > Now implement the feature
   > [Run tests - should pass]
   ```

## 🔧 Performance Issues

### Claude Code Slow to Start

```bash
# Clear cache
rm -rf ~/.claude/cache

# Check disk space
df -h

# Verify network speed
ping api.anthropic.com
```

### File Operations Slow

- Check antivirus exclusions
- Add project folder to Windows Defender exclusions
- Use SSD for development
- Close other heavy applications

## 📝 CLAUDE.md Issues

### Changes Not Recognized

```bash
# In Claude session
/clear
/memory  # Re-read CLAUDE.md
```

### Memory Hierarchy Confusion

```
Priority Order (highest to lowest):
1. Session context (current conversation)
2. ./CLAUDE.md (project-specific)
3. ~/.claude/CLAUDE.md (user-global)
```

## 🐛 Debugging Tips

### Enable Verbose Logging

```bash
claude --verbose
```

### Check Installation Health

```bash
claude doctor
```

Expected output:

```
✓ Node.js version: 22.x.x
✓ npm version: 10.x.x
✓ Claude Code installed: 1.x.x
✓ Network connectivity: OK
✓ Git installed: 2.x.x
✓ Shell environment: bash
```

### Reset Claude Code

```bash
# Backup settings first
cp -r ~/.claude ~/.claude.backup

# Clear all Claude data
rm -rf ~/.claude

# Reinstall
npm uninstall -g @anthropic-ai/claude-code
npm install -g @anthropic-ai/claude-code
```

## 🛠️ My Skill Doesn't Work

Skills are powerful, but they can be finicky. Here's your debugging checklist:

### Check File Location

Skills must live in `.claude/skills/` directory:

```
project-root/
├── .claude/
│   └── skills/
│       └── my-skill/
│           └── SKILL.md    ← Must be named exactly SKILL.md
```

```bash
# Verify the file exists
ls -la .claude/skills/*/SKILL.md
```

### Verify SKILL.md Frontmatter

Your SKILL.md needs valid YAML frontmatter at the top:

```yaml
---
name: my-skill-name
description: What this skill does
---

# Skill Instructions

Your skill content here...
```

**Common mistakes:**

- Missing the `---` delimiters
- Using tabs instead of spaces for indentation
- Special characters not quoted

### Check Skill Name Matches Invocation

The `name` in frontmatter must match how you invoke it:

```yaml
---
name: generate-report    # ← This name
---
```

```bash
# Invoke with exact same name
/generate-report         # ← Must match exactly
```

### Permission Issues

On Mac/Linux/WSL, check file permissions:

```bash
# Skills themselves don't need execute permission
# But referenced scripts do:
chmod +x .claude/skills/my-skill/script.sh
```

### Test with Explicit Invocation

```bash
# In Claude session, try explicit invocation
/skill-name

# If that fails, check Claude's skill discovery
/help skills
```

### Debugging Steps

1. **Verify YAML syntax:** Use a YAML validator online
2. **Check for typos:** Skill names are case-sensitive
3. **Restart Claude:** `/clear` and restart session
4. **Check logs:** `claude --verbose` for detailed output

## ⚡ Hooks Aren't Triggering

Hooks let you run custom scripts on Claude events. When they don't fire:

### Verify settings.json Configuration

Hooks are configured in `.claude/settings.json`:

```json
{
  "hooks": {
    "PreToolUse": [
      {
        "matcher": "Write|Edit",
        "hooks": [".claude/hooks/pre-edit.sh"]
      }
    ],
    "PostToolUse": [
      {
        "matcher": "Write|Edit",
        "hooks": [".claude/hooks/post-edit.sh"]
      }
    ]
  }
}
```

**Check:**

- JSON is valid (no trailing commas!)
- Paths are relative to project root
- Hook types match: `PreToolUse`, `PostToolUse`, `Notification`, `Stop`

### Check Hook Script Permissions

Scripts must be executable:

```bash
# Make executable
chmod +x .claude/hooks/pre-commit.sh

# Verify permissions
ls -la .claude/hooks/
# Should show: -rwxr-xr-x
```

### Test Hook Script Independently

```bash
# Run the script directly
./.claude/hooks/pre-commit.sh

# Check exit code
echo $?  # 0 = success, non-zero = error
```

### Check for Syntax Errors

```bash
# Bash syntax check
bash -n .claude/hooks/my-hook.sh

# Python syntax check
python -m py_compile .claude/hooks/my-hook.py
```

### Verify Trigger Conditions

Different hooks fire at different times:

| Hook Type | When It Fires |
| --------- | ------------- |
| `PreToolUse` | Before Claude uses a tool (Write, Edit, Bash, etc.) |
| `PostToolUse` | After Claude finishes using a tool |
| `Notification` | For informational hooks that don't block execution |
| `Stop` | To halt Claude's execution on certain conditions |

**Make sure you're testing the right trigger!**

### Debugging Hooks

1. Add debug output to your hook:

   ```bash
   #!/bin/bash
   echo "Hook fired at $(date)" >> /tmp/hook-debug.log
   # ... rest of script
   ```

2. Check the debug log after expected trigger
3. Verify hook isn't silently failing

## 📋 Common YAML Frontmatter Errors

YAML looks simple but bites hard. Here's what breaks:

### Missing `---` Delimiters

**Wrong:**

```markdown
name: my-skill

# Skill content
```

**Right:**

```markdown
---
name: my-skill
---

# Skill content
```

### Indentation Issues

YAML uses spaces, NOT tabs. And indentation matters:

**Wrong:**

```yaml
---
name: my-skill
description: Does stuff
  with multiple lines    # ← Wrong indentation
---
```

**Right:**

```yaml
---
name: my-skill
description: >
  Does stuff
  with multiple lines    # ← Use > for multiline
---
```

### Special Characters Needing Quotes

These characters require quotes around values:

```yaml
---
# These need quotes:
name: "skill: advanced"        # Colons
description: "It's great"      # Apostrophes
tags: "yes, no, maybe"         # Commas at start
special: "#hashtag"            # Hash at start
truth: "true"                  # Boolean-looking strings
---
```

**Characters requiring quotes:** `: { } [ ] , & * # ? | - < > = ! % @ \`

### Required vs Optional Fields

For SKILL.md files:

| Field | Required | Notes |
| ----- | -------- | ----- |
| `name` | Yes | Must match invocation |
| `description` | Yes | Shows in skill listing |
| `version` | No | Semantic versioning |
| `author` | No | For attribution |
| `tags` | No | For categorization |

### Quick YAML Validation

```bash
# Install yq (YAML processor)
# Mac: brew install yq
# Linux: snap install yq

# Validate frontmatter
head -20 .claude/skills/my-skill/SKILL.md | yq '.'
```

Or use online validators: [YAML Lint](https://www.yamllint.com/)

## 📊 Coverage Target Explanation

This course recommends 80-90% test coverage as the target range.

### Why 80-90% is the Recommended Target

80-90% coverage is the recommended target because it provides strong confidence in code quality while remaining maintainable. Achieving 100% coverage often leads to brittle tests that test implementation rather than behavior. The final 10-20% of coverage typically requires testing edge cases that provide diminishing returns and can make refactoring harder without meaningful improvement in code quality.

For most projects, 80-90% coverage is the sweet spot:

- **Diminishing returns:** Going from 80% to 100% often costs 3x the effort
- **Covers critical paths:** Main business logic gets tested
- **Allows pragmatism:** Some code (logging, error handling) is genuinely hard to test
- **Faster development:** Tests should enable, not slow you down

### When Higher Coverage (90%+) Makes Sense

For critical systems, you may want to aim higher:

- Financial calculations and billing
- Security-critical authentication/authorization
- Data integrity and migration code
- Legal compliance features
- Code that rarely changes but must always work

### When Lower Coverage (70-80%) is Acceptable

- Prototypes and proof-of-concepts
- Internal tools with low risk
- UI presentation logic (test behavior, not pixels)
- Third-party integration wrappers
- Code scheduled for deprecation

### Coverage Anti-Patterns to Avoid

❌ **Testing implementation details** - Tests break on refactor
❌ **100% coverage obsession** - Leads to useless tests
❌ **Testing getters/setters** - No business logic to verify
❌ **Ignoring mutation testing** - Coverage != quality

### What Actually Matters

```
Good tests > High coverage

A 75% coverage suite with meaningful tests
beats 100% coverage with shallow assertions.
```

**Focus on:**

- Testing behavior, not implementation
- Edge cases and error conditions
- Integration points between components
- The tests that would catch real bugs

## 🔄 Batch Automation Issues

When Claude Code batch scripts fail:

### Script Timeout

**Symptoms:** Script killed or hangs indefinitely

**Solutions:**

```bash
# Add explicit timeout to Claude calls
timeout 300s claude -p "Your prompt here" --no-session-persistence

# Or use --max-budget-usd to limit cost/time
claude -p "Your prompt here" --max-budget-usd 0.50
```

### API Rate Limits

**Symptoms:** 429 errors, requests failing

**Solutions:**

```bash
#!/bin/bash
# Add retry with exponential backoff
run_claude_with_retry() {
  local prompt="$1"
  for i in 1 2 3; do
    claude -p "$prompt" && return 0
    echo "Attempt $i failed, retrying in $((i * 10))s..."
    sleep $((i * 10))
  done
  return 1
}
```

### Empty or Missing Output

**Symptoms:** Output file is empty or not created

**Solutions:**

```bash
# Validate output before using
output=$(claude -p "Your prompt" --output-format json 2>&1)
if [ -z "$output" ] || echo "$output" | grep -q "error"; then
  echo "Claude automation failed: $output"
  exit 1
fi
```

### JSON Output Parsing

**Symptoms:** Can't parse Claude's JSON output

**Solutions:**

```bash
# Use --output-format json for structured output
claude -p "Analyze this code and return JSON" --output-format json | jq '.result'

# Validate JSON before processing
output=$(claude -p "..." --output-format json)
if echo "$output" | jq -e . >/dev/null 2>&1; then
  echo "$output" | jq '.result'
else
  echo "Invalid JSON output: $output"
fi
```

### Debug Batch Scripts

```bash
#!/bin/bash
# Add verbose debugging
set -x  # Print each command before execution

# Log all output
exec > >(tee -a batch-debug.log) 2>&1

# Use Claude's verbose flag
claude --verbose -p "Your prompt"

# Check exit codes
claude -p "..." || echo "Claude failed with exit code $?"
```

### Silent Failures & Exit Codes

**Symptoms:** Script completes but output is empty or wrong

**Common causes and solutions:**

```bash
# 1. Check if Claude actually ran (exit code 0 = success)
claude -p "Your prompt" --no-session-persistence
echo "Exit code: $?"  # 0 = success, non-zero = failure

# 2. Capture stderr separately to see errors
claude -p "Your prompt" 2>error.log >output.txt
if [ -s error.log ]; then
  echo "Errors occurred:"
  cat error.log
fi

# 3. Handle empty output explicitly
output=$(claude -p "Your prompt" --no-session-persistence)
if [ -z "$output" ]; then
  echo "WARNING: Claude returned empty output"
  exit 1
fi

# 4. Timeout pattern for long-running prompts
timeout 120s claude -p "Complex analysis..." || {
  echo "Claude timed out after 120 seconds"
  exit 1
}
```

**Exit code reference:**

| Exit Code | Meaning | Action |
| --------- | ------- | ------ |
| 0 | Success | Output is valid |
| 1 | General error | Check stderr, retry |
| 124 | Timeout (from `timeout` cmd) | Increase timeout or simplify prompt |
| 137 | Killed (OOM) | Reduce context size |

---

## 📋 Version Compatibility

This course was tested with:

| Component | Version | Notes |
| --------- | ------- | ----- |
| Claude Code CLI | 1.x | `claude --version` to check |
| Node.js | 22 LTS | Required for Claude Code |
| npm | 10.x | Comes with Node.js |
| .NET SDK | 10.0 | For C# exercises |

> **Last verified:** January 2026. Run `claude doctor` to check your setup.

---

## 🆘 Getting Help

### Before Asking for Help

1. Run `claude doctor` and save output
2. Check Node/npm versions
3. Try in a new terminal
4. Test with a simple project
5. Check #ai-exchange for similar issues

### Information to Provide

When asking for help, include:

- OS and version
- Node.js version (`node --version`)
- npm version (`npm --version`)
- Claude Code version (`claude --version`)
- Error messages (full text)
- What you were trying to do
- What you expected to happen

### Support Channels

- **Slack:** `#ai-exchange` for course help
- **Slack:** `#claude-help` for general Claude issues
- **Office Hours:** Thursdays 2-3 PM CT
- **GitLab Issues:** For bug reports

## 🔄 Common Workarounds

### Quick Fixes to Try

1. **Restart terminal**
2. **Run `claude doctor`**
3. **Clear and restart:** `/clear` then restart Claude
4. **Check directory:** Ensure you're in project root
5. **Update Claude:** `npm update -g @anthropic-ai/claude-code`
6. **Check network:** Temporarily disable VPN/firewall
7. **Use different shell:** Try PowerShell vs Git Bash

---

**Still stuck?** Don't hesitate to ask for help in `#ai-exchange`! Include your `claude doctor` output and describe what you've already tried.
