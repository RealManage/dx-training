# Week 1: Setup Verification Checklist ✅

Use this checklist to verify your Claude Code installation is working correctly.

## Installation Verification

### Step 1: Verify .NET SDK Installation

```bash
# Check .NET version
dotnet --version
# Should show: 9.0.x

# Check available SDKs
dotnet --list-sdks
# Should include 9.0.x

# Verify you can build C# projects
dotnet new console -n TestApp
cd TestApp
dotnet build
dotnet run
# Should print "Hello, World!"
cd ..
rm -rf TestApp
```

### Step 2: Verify nvm Installation

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

### Step 3: Ensure Correct Node.js Version

```bash
# Check current version
node --version
# Should show: v22.x.x (LTS)

# If not 22, switch versions:
# Windows: nvm use 22
# Mac/Linux: nvm use --lts
```

### Step 4: Check npm Version

```bash
npm --version
# Should show: 10.0.0 or higher

# Check global package location
npm config get prefix
# Windows Git Bash: /c/Users/YOUR_NAME/AppData/Roaming/npm
# Mac/Linux: Should be in home directory, NOT /usr/local
```

### Step 5: Install Claude Code

**For Windows (Git Bash):**

```bash
# Make sure you're in Git Bash, not PowerShell
npm install -g @anthropic-ai/claude-code
```

**For Mac/Linux/WSL:**

```bash
npm install -g @anthropic-ai/claude-code
```

### Step 6: Verify Claude Installation

```bash
claude --version
# Should show: Claude Code version number
```

### Step 7: Run Diagnostic

```bash
claude doctor
```

**Expected output:**

```
✓ Node.js version: 20.11.0
✓ npm version: 10.2.4
✓ Claude Code installed: 1.x.x
✓ Network connectivity: OK
✓ Git installed: 2.x.x
✓ Shell environment: bash

All checks passed! Claude Code is ready to use.
```

## Common Issues & Fixes

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

### Node Version Issues

```bash
# List installed versions
nvm list  # Windows
nvm ls    # Mac/Linux

# Install if missing
nvm install 22    # Windows
nvm install --lts # Mac/Linux

# Switch to correct version
nvm use 22        # Windows
nvm use --lts     # Mac/Linux
```

### Claude: "command not found"

```bash
# Check npm global bin path
npm config get prefix

# Windows Git Bash - Add to PATH in ~/.bashrc:
echo 'export PATH=$PATH:/c/Users/'$USER'/AppData/Roaming/npm' >> ~/.bashrc
source ~/.bashrc

# Mac/Linux - Add to PATH:
echo 'export PATH=$PATH:$(npm config get prefix)/bin' >> ~/.bashrc
source ~/.bashrc
```

### Permission Denied on npm install

```bash
# DO NOT use sudo!
# Check npm prefix:
npm config get prefix

# If it shows /usr/local, change it:
npm config set prefix ~/.npm-global
mkdir -p ~/.npm-global
echo 'export PATH=$PATH:~/.npm-global/bin' >> ~/.bashrc
source ~/.bashrc

# Reinstall Claude Code
npm install -g @anthropic-ai/claude-code
```

### Git Bash Specific Issues

```bash
# If claude hangs or doesn't respond:
winpty claude

# If still issues, check Git Bash version:
git --version
# Should be 2.x or higher
```

### Corporate Proxy

```bash
# Set npm proxy
npm config set proxy http://proxy.company.com:8080
npm config set https-proxy http://proxy.company.com:8080

# May also need:
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

```
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

- [Official Troubleshooting Guide](https://code.claude.com/docs/en/troubleshooting)
- Slack: `#ai-exchange`

---

**Next:** Once setup is verified, copy the [Week 1 Example](./examples/hoa-cli/) to create your sandbox
