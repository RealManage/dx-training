# Claude Code Troubleshooting Guide 🔧

Common issues and solutions for RealManage developers using Claude Code.

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