# Week 1: Setup Verification Checklist

Use this checklist to verify your Claude Code installation is working correctly.

## Installation Verification

### Step 1: Verify .NET 10 SDK Installation

> **Local Admin required:** .NET SDK installation requires administrator permissions. Contact Desktop Support if you don't have local admin access.

```bash
# Check .NET version
dotnet --version
# Should show: 10.x.x

# Check available SDKs
dotnet --list-sdks
# Should include 10.x.x

# Verify you can build C# projects
dotnet new console -n TestApp
cd TestApp
dotnet build
dotnet run
# Should print "Hello, World!"
cd ..
rm -rf TestApp
```

### Step 2: Install Claude Code

Claude Code uses a **native installer** that auto-updates in the background.

**macOS / Linux / WSL:**

```bash
curl -fsSL https://claude.ai/install.sh | bash
```

**Windows (PowerShell — recommended):**

```powershell
irm https://claude.ai/install.ps1 | iex
```

> **Windows native** requires [Git for Windows](https://git-scm.com/downloads/win) (Git Bash). Install Git for Windows first if you don't have it.

**Alternative install methods (not recommended):**

| Method | Command | Auto-updates? |
| - | - | - |
| WinGet | `winget install Anthropic.ClaudeCode` | No — use `winget upgrade Anthropic.ClaudeCode` |
| Homebrew | `brew install --cask claude-code` | No — use `brew upgrade claude-code` |

> **Important:** `claude update` only works with the native installer. If you installed via WinGet or Homebrew, you must update using the package manager command shown above.

### Step 3: Verify Claude Installation

```bash
claude --version
# Should show: Claude Code version number
```

### Step 4: Run Diagnostic

```bash
claude doctor
```

**Expected output:**

```text
✓ Claude Code installed: 2.x.x
✓ Network connectivity: OK
✓ Git installed: 2.x.x
✓ Shell environment: bash

All checks passed! Claude Code is ready to use.
```

### Step 5: Verify Node.js (for Angular exercises only)

> Node.js is **not required** for Claude Code itself, but is needed for Angular exercises in later weeks. You can skip this step for Week 1.

**Windows (nvm-windows):**

```powershell
nvm version
# Should show: nvm version number (e.g., 1.2.2)

nvm list
# Should show your installed Node versions
```

**Mac/Linux/WSL:**

```bash
nvm --version
# Should show: 0.40.x

nvm ls
# Should show your installed Node versions with arrow pointing to current
```

```bash
# Check current version
node --version
# Should show: v22.x.x (LTS)

# If not 22, switch versions:
# Windows: nvm use 22
# Mac/Linux: nvm use --lts
```

## Common Issues & Fixes

### Claude: "command not found"

**Native install (macOS/Linux/WSL):**

```bash
# Restart your terminal first — the installer adds claude to your PATH
# If still not found, ensure ~/.local/bin is in your PATH:
echo 'export PATH="$HOME/.local/bin:$PATH"' >> ~/.bashrc
source ~/.bashrc
```

**Windows native install:**

```powershell
# Close and reopen PowerShell/Git Bash
# If still not found, verify Git for Windows is installed:
git --version
```

### Authentication Issues

```bash
# If OAuth fails, try disabling VPN temporarily
# Corporate VPN can block the OAuth callback
```

### WinGet: `claude update` Fails

If you installed via WinGet, `claude update` is not supported. Use:

```powershell
winget upgrade Anthropic.ClaudeCode
```

Or switch to the native installer:

```powershell
# Uninstall WinGet version first
winget uninstall Anthropic.ClaudeCode

# Install native version (auto-updates)
irm https://claude.ai/install.ps1 | iex
```

### Homebrew: `claude update` Fails

If you installed via Homebrew, `claude update` is not supported. Use:

```bash
brew upgrade claude-code
```

### Git Bash Specific Issues

```bash
# If claude hangs or doesn't respond:
winpty claude

# If still issues, check Git Bash version:
git --version
# Should be 2.x or higher
```

### .NET SDK: Permission Denied

```text
# .NET SDK requires local admin to install
# Contact Desktop Support for admin access
# Or ask a team member with admin to assist
```

### nvm Not Found

**Windows:**

```powershell
# Close ALL terminals
# Open NEW terminal as Administrator
# Try again: nvm version
```

**Mac/Linux:**

```bash
# Reload shell configuration
source ~/.bashrc  # or ~/.zshrc for Zsh users

# If still not found, check if install script completed:
curl -o- https://raw.githubusercontent.com/nvm-sh/nvm/v0.40.1/install.sh | bash
```

### Corporate Proxy

```bash
# Set proxy environment variables
export HTTP_PROXY=http://proxy.company.com:8080
export HTTPS_PROXY=http://proxy.company.com:8080
```

## Authentication Test

### Step 1: Start Claude

```bash
claude
```

### Step 2: Complete OAuth

- Browser should open automatically
- Log in with Anthropic account
- Authorize Claude Code

### Step 3: Test Basic Query

```text
> Hello Claude, what version are you?

> Can you see any files in this directory?
```

## Final Verification

Create a test file and interact with it:

```bash
# Create a simple C# file
cat > Test.cs << 'EOF'
public class Calculator
{
    public int Add(int a, int b)
    {
        return a + b;
    }
}
EOF

# Start Claude
claude

# Ask Claude about the file
> What does the Test.cs file do?

> Add a Multiply method to the Calculator class

> Write xUnit tests for the Calculator class
```

## Success Criteria

- [ ] .NET 10 SDK installed and working
- [ ] VS Code with recommended extensions
- [ ] Claude Code installed and version shows
- [ ] `claude doctor` shows all green checks
- [ ] Authentication completed successfully
- [ ] Claude can read files in your directory
- [ ] Claude can generate C# code
- [ ] Claude can write files when asked
- [ ] Can build and run the example C# app

## Troubleshooting Resources

- [Official Setup Guide](https://code.claude.com/docs/en/setup)
- [Official Troubleshooting Guide](https://code.claude.com/docs/en/troubleshooting)
- Slack: `#ai-exchange`

---

**Next:** Once setup is verified, copy the [Week 1 Example](./examples/hoa-cli/) to create your sandbox
