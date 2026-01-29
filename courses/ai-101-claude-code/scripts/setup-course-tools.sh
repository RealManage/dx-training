#!/bin/bash
#
# Claude Code & VS Code Setup Script for Mac/Linux/WSL
# Fast-track setup for all team members (PMs, Support, Developers)
#
# Installs:
#   - Claude Code CLI (native installer)
#   - VS Code extensions (if VS Code installed)
#
# Official docs: https://code.claude.com/docs/en/setup
#
# Usage: ./setup-course-tools.sh
#

set -e

# Colors for output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
NC='\033[0m' # No Color

# Print colored output
info() { echo -e "${BLUE}[INFO]${NC} $1"; }
success() { echo -e "${GREEN}[OK]${NC} $1"; }
warn() { echo -e "${YELLOW}[WARN]${NC} $1"; }
error() { echo -e "${RED}[ERROR]${NC} $1"; }

echo ""
echo "=============================================="
echo "   Claude Code & VS Code Setup"
echo "   RealManage AI-101 Course"
echo "=============================================="
echo ""

# Detect WSL environment
IS_WSL=false
if grep -qi microsoft /proc/version 2>/dev/null; then
    IS_WSL=true
    info "WSL environment detected"
    echo "  VS Code extensions will install to your Windows VS Code."
    echo "  Make sure VS Code has WSL integration enabled."
    echo ""
fi

# ============================================
# PART 1: Claude Code CLI
# ============================================

info "PART 1: Claude Code CLI"
echo ""

# Check if already installed
if command -v claude &> /dev/null; then
    CLAUDE_VERSION=$(claude --version 2>/dev/null || echo "unknown")
    success "Claude Code already installed: $CLAUDE_VERSION"
    CLAUDE_INSTALLED=true
else
    info "Installing Claude Code using native installer..."
    echo ""
    curl -fsSL https://claude.ai/install.sh | bash
    echo ""

    # Source shell config to pick up new PATH
    if [ -f ~/.bashrc ]; then
        source ~/.bashrc 2>/dev/null || true
    fi
    if [ -f ~/.zshrc ]; then
        source ~/.zshrc 2>/dev/null || true
    fi

    if command -v claude &> /dev/null; then
        CLAUDE_VERSION=$(claude --version 2>/dev/null || echo "installed")
        success "Claude Code installed: $CLAUDE_VERSION"
        CLAUDE_INSTALLED=true
    else
        warn "Claude command not found in current shell."
        echo "Try opening a new terminal window after this script completes."
        CLAUDE_INSTALLED=false
    fi
fi

# Note: `claude doctor` requires an interactive terminal
# Users can run it manually after setup if needed
if [ "$CLAUDE_INSTALLED" = true ]; then
    echo ""
    info "To verify your installation, run: claude doctor"
fi

# ============================================
# PART 2: VS Code & Extensions
# ============================================

echo ""
echo "=============================================="
info "PART 2: VS Code & Extensions"
echo "=============================================="
echo ""

# Recommended extensions for AI-101 course
# Includes C# extensions so anyone can try Dev Track work
EXTENSIONS=(
    "anthropic.claude-code"                # Claude Code
    "bierner.markdown-mermaid"             # Mermaid diagram preview
    "gitlab.gitlab-workflow"               # GitLab integration
    "ms-dotnettools.vscode-dotnet-runtime" # .NET runtime
    "ms-dotnettools.csdevkit"              # C# Dev Kit
    "ms-dotnettools.csharp"                # C# language support
    "ms-vsliveshare.vsliveshare"           # Live Share for collaboration
)

# Check if VS Code is installed
if command -v code &> /dev/null; then
    success "VS Code found"
    echo ""
    info "Installing recommended extensions..."
    echo ""

    for ext in "${EXTENSIONS[@]}"; do
        info "  Installing: $ext"
        code --install-extension "$ext" --force 2>/dev/null || warn "  Failed to install $ext"
    done

    echo ""
    success "VS Code extensions installed!"
else
    warn "VS Code not found"
    echo ""
    echo "VS Code is recommended for the best course experience."
    echo ""
    echo "Install VS Code from:"
    echo "  https://code.visualstudio.com/download"
    echo ""
    echo "After installing, run this script again to install extensions,"
    echo "or manually install these extensions:"
    echo ""
    for ext in "${EXTENSIONS[@]}"; do
        echo "  - $ext"
    done
fi

# ============================================
# SUMMARY
# ============================================

echo ""
echo "=============================================="
echo "   Setup Complete!"
echo "=============================================="
echo ""

if [ "$CLAUDE_INSTALLED" = true ]; then
    success "Claude Code CLI: Installed"
else
    warn "Claude Code CLI: Needs new terminal"
fi

if command -v code &> /dev/null; then
    success "VS Code: Installed with extensions"
else
    warn "VS Code: Not installed (optional but recommended)"
fi

echo ""
echo "NEXT STEPS:"
echo ""
echo "  1. Open VS Code in your project folder"
echo "     code /path/to/your/project"
echo ""
echo "  2. Open the integrated terminal (Ctrl+\` or Cmd+\`)"
echo ""
echo "  3. Run: claude"
echo ""
echo "  4. A browser window will open for authentication"
echo "     - Sign in with your Anthropic/Claude account"
echo "     - Authorize Claude Code"
echo ""
echo "  5. Return to VS Code - you're ready to go!"
echo ""
echo "DOCUMENTATION:"
echo "  https://code.claude.com/docs/en/setup"
echo ""
echo "Need help? Ask in #ai-exchange on Slack"
echo ""
