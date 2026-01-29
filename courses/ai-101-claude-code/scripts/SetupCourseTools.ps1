#Requires -Version 5.1
<#
.SYNOPSIS
    Claude Code & VS Code Setup Script for Windows
    Fast-track setup for all team members (PMs, Support, Developers)

.DESCRIPTION
    Installs:
    - Claude Code CLI (native installer)
    - VS Code extensions (if VS Code installed)

    Official docs: https://code.claude.com/docs/en/setup

.EXAMPLE
    .\SetupCourseTools.ps1

.NOTES
    RealManage AI-101 Course Setup
#>

# Stop on errors
$ErrorActionPreference = "Stop"

# Colors for output
function Write-Info { param($Message) Write-Host "[INFO] " -ForegroundColor Blue -NoNewline; Write-Host $Message }
function Write-Success { param($Message) Write-Host "[OK] " -ForegroundColor Green -NoNewline; Write-Host $Message }
function Write-Warn { param($Message) Write-Host "[WARN] " -ForegroundColor Yellow -NoNewline; Write-Host $Message }
function Write-Err { param($Message) Write-Host "[ERROR] " -ForegroundColor Red -NoNewline; Write-Host $Message }

Write-Host ""
Write-Host "==============================================" -ForegroundColor Cyan
Write-Host "   Claude Code & VS Code Setup" -ForegroundColor Cyan
Write-Host "   RealManage AI-101 Course" -ForegroundColor Cyan
Write-Host "==============================================" -ForegroundColor Cyan
Write-Host ""

# ============================================
# PART 1: Claude Code CLI
# ============================================

Write-Info "PART 1: Claude Code CLI"
Write-Host ""

$claudeInstalled = $false

# Check if already installed
$claudeExists = $null
try {
    $claudeExists = Get-Command claude -ErrorAction SilentlyContinue
} catch {
    $claudeExists = $null
}

if ($claudeExists) {
    try {
        $claudeVersion = & claude --version 2>$null
    } catch {
        $claudeVersion = "unknown"
    }
    Write-Success "Claude Code already installed: $claudeVersion"
    $claudeInstalled = $true
} else {
    Write-Info "Installing Claude Code using native installer..."
    Write-Host ""

    try {
        Invoke-Expression (Invoke-RestMethod -Uri "https://claude.ai/install.ps1")
        Write-Host ""

        # Refresh PATH
        $env:Path = [System.Environment]::GetEnvironmentVariable("Path", "Machine") + ";" + [System.Environment]::GetEnvironmentVariable("Path", "User")

        $claudeVerify = $null
        try {
            $claudeVerify = Get-Command claude -ErrorAction SilentlyContinue
        } catch {
            $claudeVerify = $null
        }

        if ($claudeVerify) {
            try {
                $claudeVersion = & claude --version 2>$null
            } catch {
                $claudeVersion = "installed"
            }
            Write-Success "Claude Code installed: $claudeVersion"
            $claudeInstalled = $true
        } else {
            Write-Warn "Claude command not found in current shell."
            Write-Host "Try closing and reopening PowerShell after this script completes."
        }
    } catch {
        Write-Err "Installation failed: $_"
        Write-Host ""
        Write-Host "Alternative installation methods:" -ForegroundColor Yellow
        Write-Host "  1. WinGet: winget install Anthropic.ClaudeCode" -ForegroundColor Gray
        Write-Host "  2. Manual: Visit https://code.claude.com/docs/en/setup" -ForegroundColor Gray
    }
}

# Note: `claude doctor` requires an interactive terminal
# Users can run it manually after setup if needed
if ($claudeInstalled) {
    Write-Host ""
    Write-Info "To verify your installation, run: claude doctor"
}

# ============================================
# PART 2: VS Code & Extensions
# ============================================

Write-Host ""
Write-Host "==============================================" -ForegroundColor Cyan
Write-Info "PART 2: VS Code & Extensions"
Write-Host "==============================================" -ForegroundColor Cyan
Write-Host ""

# Recommended extensions for AI-101 course
# Includes C# extensions so anyone can try Dev Track work
$extensions = @(
    "anthropic.claude-code"                # Claude Code
    "bierner.markdown-mermaid"             # Mermaid diagram preview
    "gitlab.gitlab-workflow"               # GitLab integration
    "ms-dotnettools.vscode-dotnet-runtime" # .NET runtime
    "ms-dotnettools.csdevkit"              # C# Dev Kit
    "ms-dotnettools.csharp"                # C# language support
    "ms-vsliveshare.vsliveshare"           # Live Share for collaboration
)

# Check if VS Code is installed
$codeExists = $null
try {
    $codeExists = Get-Command code -ErrorAction SilentlyContinue
} catch {
    $codeExists = $null
}

if ($codeExists) {
    Write-Success "VS Code found"
    Write-Host ""
    Write-Info "Installing recommended extensions..."
    Write-Host ""

    foreach ($ext in $extensions) {
        Write-Info "  Installing: $ext"
        try {
            & code --install-extension $ext --force 2>$null
        } catch {
            Write-Warn "  Failed to install $ext"
        }
    }

    Write-Host ""
    Write-Success "VS Code extensions installed!"
    $vscodeInstalled = $true
} else {
    Write-Warn "VS Code not found"
    Write-Host ""
    Write-Host "VS Code is recommended for the best course experience." -ForegroundColor Yellow
    Write-Host ""
    Write-Host "Install VS Code from:" -ForegroundColor White
    Write-Host "  https://code.visualstudio.com/download" -ForegroundColor Gray
    Write-Host ""
    Write-Host "Or use winget:" -ForegroundColor White
    Write-Host "  winget install Microsoft.VisualStudioCode" -ForegroundColor Gray
    Write-Host ""
    Write-Host "After installing, run this script again to install extensions," -ForegroundColor Yellow
    Write-Host "or manually install these extensions:" -ForegroundColor Yellow
    Write-Host ""
    foreach ($ext in $extensions) {
        Write-Host "  - $ext" -ForegroundColor Gray
    }
    $vscodeInstalled = $false
}

# ============================================
# SUMMARY
# ============================================

Write-Host ""
Write-Host "==============================================" -ForegroundColor Green
Write-Host "   Setup Complete!" -ForegroundColor Green
Write-Host "==============================================" -ForegroundColor Green
Write-Host ""

if ($claudeInstalled) {
    Write-Success "Claude Code CLI: Installed"
} else {
    Write-Warn "Claude Code CLI: Needs new terminal"
}

if ($vscodeInstalled) {
    Write-Success "VS Code: Installed with extensions"
} else {
    Write-Warn "VS Code: Not installed (optional but recommended)"
}

Write-Host ""
Write-Host "NEXT STEPS:" -ForegroundColor Cyan
Write-Host ""
Write-Host "  1. Open VS Code in your project folder" -ForegroundColor White
Write-Host "     code C:\path\to\your\project" -ForegroundColor Gray
Write-Host ""
Write-Host "  2. Open the integrated terminal (Ctrl+``)" -ForegroundColor White
Write-Host ""
Write-Host "  3. Run: " -ForegroundColor White -NoNewline
Write-Host "claude" -ForegroundColor Yellow
Write-Host ""
Write-Host "  4. A browser window will open for authentication" -ForegroundColor White
Write-Host "     - Sign in with your Anthropic/Claude account" -ForegroundColor Gray
Write-Host "     - Authorize Claude Code" -ForegroundColor Gray
Write-Host ""
Write-Host "  5. Return to VS Code - you're ready to go!" -ForegroundColor White
Write-Host ""
Write-Host "DOCUMENTATION:" -ForegroundColor Cyan
Write-Host "  https://code.claude.com/docs/en/setup" -ForegroundColor Gray
Write-Host ""
Write-Host "Need help? Ask in #ai-exchange on Slack" -ForegroundColor Cyan
Write-Host ""
