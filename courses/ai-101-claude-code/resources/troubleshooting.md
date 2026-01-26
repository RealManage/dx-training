# Claude Code Troubleshooting Guide 🔧

Common issues and solutions for RealManage developers using Claude Code.

> **Official Documentation:**
> - [CLI Reference](https://code.claude.com/docs/en/cli-reference)
> - [Interactive Mode](https://code.claude.com/docs/en/interactive-mode)
> - [Troubleshooting](https://docs.anthropic.com/en/docs/claude-code/troubleshooting)

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
    "pre-commit": ".claude/hooks/pre-commit.sh",
    "post-response": ".claude/hooks/post-response.sh"
  }
}
```

**Check:**
- JSON is valid (no trailing commas!)
- Paths are relative to project root
- Hook names match expected triggers

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

| Hook | When It Fires |
|------|---------------|
| `pre-commit` | Before Claude commits code |
| `post-response` | After every Claude response |
| `pre-tool` | Before Claude runs a tool |
| `post-tool` | After Claude runs a tool |

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
|-------|----------|-------|
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

## 🔄 CI/CD Pipeline Issues

When Claude Code in CI/CD pipelines fails:

### Pipeline Timeout

**Symptoms:** Job killed after 10+ minutes

**Solutions:**
```yaml
# Add explicit timeout
claude-review:
  timeout: 10 minutes
  script:
    - timeout 300s claude --print "..."  # 5 min max for Claude
```

### API Rate Limits

**Symptoms:** 429 errors, requests failing

**Solutions:**
```yaml
# Add retry with backoff
retry:
  max: 3
  when:
    - api_failure
script:
  - for i in 1 2 3; do claude --print "..." && break || sleep $((i * 10)); done
```

### Empty or Missing Output

**Symptoms:** review.md is empty, no comment posted

**Solutions:**
```bash
# Validate output before posting
if [ ! -s review.md ]; then
  echo "**Automated review unavailable**" > review.md
fi
```

### GitLab Token Permission Issues

**Symptoms:** 401/403 when posting comments

**Checklist:**
- [ ] `GITLAB_TOKEN` has `api` scope
- [ ] Token not expired
- [ ] Variable is masked but not protected (if running on non-protected branches)
- [ ] Token owner has access to the project

### Debug CI/CD Jobs

```yaml
# Add debug output
script:
  - echo "Changed files: $(git diff --name-only $CI_MERGE_REQUEST_DIFF_BASE_SHA...HEAD)"
  - claude --verbose --print "..." 2>&1 | tee debug.log
  - cat debug.log
artifacts:
  when: always
  paths:
    - debug.log
```

---

## 🆘 Getting Help

### Before Asking for Help

1. Run `claude doctor` and save output
2. Check Node/npm versions
3. Try in a new terminal
4. Test with a simple project
5. Check #dx-training for similar issues

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

- **Slack:** `#dx-training` for course help
- **Slack:** `#claude-help` for general Claude issues
- **Office Hours:** Thursdays 2-3 PM CT
- **GitLab Issues:** For bug reports
- **Email:** dx-team@realmanage.com

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

**Still stuck?** Don't hesitate to ask for help in `#dx-training`! Include your `claude doctor` output and describe what you've already tried.