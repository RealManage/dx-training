# RealManage Developer Experience Training Hub

Welcome to the RealManage DX Training repository — technical training courses designed to elevate engineering capabilities through hands-on, AI-assisted development practices.

## Available Courses

### [AI 101: Claude Code](./courses/ai-101-claude-code/)

**Duration:** 9 weeks + optional Week 0 primer (2 hours/week)
**Level:** Beginner to Intermediate
**Status:** Active

Transform how you write code with Anthropic's Claude Code. This comprehensive course takes you from setup to advanced workflows, including Test-Driven Development, custom skills and plugins, automation patterns, and real-world scenarios across developer, QA, PM, and support tracks.

[**Start Course →**](./courses/ai-101-claude-code/README.md)

### [BDD 101: Behavior-Driven Development Foundations](./courses/bdd-101/)

**Duration:** 3 sessions (2 hours each)
**Level:** Beginner
**Status:** Active

Learn to bridge the gap between business requirements and technical implementation through Behavior-Driven Development. Covers Gherkin syntax, Three Amigos conversations, test automation, and collaborative specification practices.

[**Start Course →**](./courses/bdd-101/README.md)

### Coming Soon

- **AI 102: Advanced Claude Code** — Agent Teams, spec-driven development, multi-agent orchestration, and enterprise trust patterns

## Training Philosophy

1. **Practical First** — Every course includes hands-on exercises with real-world use cases
2. **Progressive Learning** — Start with fundamentals, build to advanced techniques
3. **Cross-Functional** — Designed for engineers, product managers, QA, and support staff
4. **Continuous Improvement** — Courses evolve based on team feedback and industry best practices

## About RealManage

RealManage is a leader in HOA and community association management. Our technology platform serves thousands of communities nationwide, and our engineering team is at the forefront of transforming how communities operate in the digital age.

## Getting Started

### 1. Open a Terminal

**Windows:** Press `Win + R`, type `cmd`, press Enter (or search for "Terminal" or "PowerShell" in the Start menu)

**Mac:** Press `Cmd + Space`, type "Terminal", press Enter

**Linux:** Press `Ctrl + Alt + T` or open your terminal emulator

### 2. Create a Workspace and Clone the Repository

Navigate to (or create) a folder where you'd like to keep the training material, then clone:

**Windows (Command Prompt or PowerShell):**

```cmd
mkdir %USERPROFILE%\repos
cd %USERPROFILE%\repos
git clone https://github.com/RealManage/dx-training.git
cd dx-training
```

**Mac / Linux:**

```bash
mkdir -p ~/repos
cd ~/repos
git clone https://github.com/RealManage/dx-training.git
cd dx-training
```

### 3. Choose a Course and Start Learning

- [AI 101: Claude Code](./courses/ai-101-claude-code/README.md) — Start with Week 1 setup
- [BDD 101: Behavior-Driven Development](./courses/bdd-101/README.md) — Start with Session 1

Each course README has prerequisites, setup instructions, and a weekly curriculum to follow.

### For Contributors

See [CONTRIBUTING.md](./CONTRIBUTING.md) for details on how to contribute to this project.

## Course Structure

Each course follows a consistent structure:

```txt
courses/
├── course-name/
│   ├── README.md           # Course overview and prerequisites
│   ├── CLAUDE.md           # AI assistant context (if applicable)
│   ├── sessions/           # Weekly lessons with examples
│   │   └── week-N/
│   │       ├── README.md   # Lesson content
│   │       └── tracks/     # Role-specific exercises
│   ├── resources/          # Reference materials and tools
│   └── exercises/          # Hands-on practice materials
```

## Prerequisites

### General Requirements

- **Development Environment:** WSL2 (Windows) or macOS/Linux
- **Node.js:** Version 22 LTS with npm 10+
- **Git:** Configured with repository access
- **IDE:** VS Code, Cursor, or similar

### Course-Specific Requirements

Check each course's README for specific prerequisites and setup instructions.

## Support

- **Slack:** `#ai-exchange` for questions and discussion
- **Issues:** Open an issue in this repository
- **Course feedback:** Submit via course feedback forms

## Version History

| Version | Date | Changes |
| - | - | - |
| 1.0.0 | 2025-01-11 | Initial release with AI 101: Claude Code |
| 1.0.1 | 2025-10-03 | Added BDD 101: Behavior-Driven Development Foundations |
| 2.0.0 | 2026-02-09 | Open source release, AI 102 course planned |

## License

This work is licensed under a [Creative Commons Attribution-ShareAlike 4.0 International License](./LICENSE).

You are free to share and adapt this material with appropriate credit to RealManage. Derivative works must use the same license.

## Acknowledgments

- The RealManage DX Team for curriculum development
- Engineering Leadership for supporting continuous learning
- Anthropic for Claude Code and excellent documentation
- The broader developer community for inspiration

---

**Ready to start?** [AI 101: Claude Code →](./courses/ai-101-claude-code/README.md) | [BDD 101 →](./courses/bdd-101/README.md)

*"Investing in our team's growth is investing in RealManage's future."* — DX Team
