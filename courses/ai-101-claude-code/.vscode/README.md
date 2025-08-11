# VS Code / IDE Configuration

This folder contains workspace settings for VS Code and compatible IDEs (Windsurf, Cursor, etc.).

## Extensions (extensions.json)

The recommended extensions provide:

- **Anthropic.claude-code** - Claude Code integration for AI assistance
- **bierner.markdown-mermaid** - Renders Mermaid diagrams in markdown preview
- **GitLab.gitlab-workflow** - GitLab integration for MRs and CI/CD
- **ms-dotnettools.vscode-dotnet-runtime** - .NET runtime for C# development
- **ms-python.python** - Python support (optional, for any Python scripts)
- **ms-toolsai.jupyter** - Jupyter notebook support (optional)

**VS Code Only:**
- **ms-dotnettools.csharp** - Full C# IntelliSense and debugging (add manually in VS Code)

**Note:** Extension IDs vary between IDEs. If an extension isn't available in your IDE, look for equivalent functionality:
- C# language support with IntelliSense
- GitLab/Git integration
- Markdown preview with diagram support
- .NET debugging capabilities

## Settings (settings.json)

Workspace settings include:
- Auto-format on save
- Exclude bin/obj folders from explorer
- Markdown word wrap enabled
- Custom dictionary for spell check (HOA terms)

## IDE Compatibility

- **VS Code**: Full support
- **Windsurf**: Most extensions work
- **Cursor**: Install equivalent extensions
- **Other IDEs**: Use as reference for setup