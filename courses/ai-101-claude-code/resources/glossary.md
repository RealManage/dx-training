# AI-101 Claude Code Glossary

Quick reference for terminology used throughout this course. Terms are organized by category.

---

## AI & LLM Fundamentals

### Agentic Engineering
A software development approach where AI assistants (like Claude Code) take on more autonomous, agent-like behaviors—executing multi-step tasks, making decisions, and interacting with external systems rather than just responding to single prompts.

### Artificial Intelligence (AI)
Computer systems designed to perform tasks that typically require human intelligence. In this course, we focus on **generative AI** that creates content (code, text, etc.) rather than just analyzing or classifying.

### Context Window
The amount of text (measured in tokens) an AI model can "see" and consider at once. Larger context windows let the model work with more code and conversation history. Claude's context window is ~200K tokens—roughly equivalent to a 400-page book.

### Hallucination
When an AI generates plausible-sounding but incorrect information. Common examples: inventing non-existent APIs, wrong method signatures, or fictional library versions. **Always verify AI-generated code.**

### Large Language Model (LLM)
The type of AI that powers Claude. LLMs are trained on massive text datasets to predict and generate human-like text. They excel at code, conversation, and reasoning tasks.

### Prompt
The text you give to an AI to get a response. In Claude Code, this includes your questions, instructions, and context. Better prompts → better results.

### Token
The basic unit LLMs use to process text. Roughly 1 token = 4 characters or 0.75 words in English. Code uses more tokens per line than prose. Tokens affect context limits—too many tokens can exceed the context window.

---

## Claude Code Concepts

### Agent (Claude Code)
An autonomous persona you define in `.claude/agents/` that specializes in specific tasks. Agents have their own system prompts and can be invoked via `/agent`. Example: a "code reviewer" agent or "test writer" agent.

### Command
A reusable prompt template stored in `.claude/commands/`. Invoked with `/command-name`. Commands accept arguments and execute predefined instructions. Great for repeatable tasks like generating reports.

### Hook
A script that runs automatically before or after Claude Code operations. Hooks enable automation like running tests after code changes, enforcing lint rules, or logging AI interactions for audit purposes.

### CLAUDE.md
A markdown file at your project root that gives Claude Code persistent context about your project—tech stack, coding standards, domain knowledge. Claude reads this automatically at session start.

### Memory Files
The hierarchy of context Claude Code uses:
1. `~/.claude/CLAUDE.md` - Global preferences
2. `./CLAUDE.md` - Project-specific (most common)
3. Session context - Current conversation

### Plugin
A packaged collection of commands, skills, and agents that can be shared across projects or published for others. Plugins are the distribution mechanism for Claude Code customizations.

### Skill
An enhanced command type that can expose functionality to Claude for automatic use. Skills can be invoked by Claude when relevant (auto-discovery) or explicitly by users. Skills are defined in `.claude/skills/`.

### Top-Level Program
A Claude Code session running as your primary development interface (as opposed to background sessions). The session you interact with directly.

---

## Testing & TDD

### Code Coverage
The percentage of your codebase executed by tests. Measured in lines, branches, or statements. **This course targets 80-90% coverage** as a realistic, maintainable goal.

### Mocking
Replacing real dependencies (databases, APIs, external services) with fake implementations during testing. Allows isolated unit testing without external systems. We use **Moq** for C# mocking.

### Red-Green-Refactor
The TDD cycle:
1. **Red:** Write a failing test first
2. **Green:** Write minimal code to pass the test
3. **Refactor:** Improve code quality while keeping tests green

### Test-Driven Development (TDD)
A development approach where you write tests BEFORE writing implementation code. Forces you to think about requirements and design upfront. Claude Code excels at TDD workflows.

### Unit Test
A test that verifies a single unit of code (method, function, class) in isolation. Fast, focused, and independent of external systems.

### Integration Test
A test that verifies multiple components working together, including external systems like databases or APIs. Slower but validates real-world behavior.

### Test Automation
The practice of using software tools to execute tests automatically, compare results, and report outcomes. Reduces manual testing effort and enables continuous testing in CI/CD pipelines.

### Regression Testing
Re-running existing tests after code changes to ensure new changes haven't broken existing functionality. Automated regression suites are essential for confident refactoring.

### Smoke Testing
A quick, high-level test suite that verifies basic functionality works before running full test suites. "Does the application start? Can users log in?" Fails fast if something is fundamentally broken.

### Test Pyramid
A testing strategy that recommends many unit tests (fast, isolated), fewer integration tests (medium speed), and even fewer E2E/UI tests (slow, brittle). Shape: wide base of unit tests, narrow top of E2E tests.

### Exploratory Testing
A testing approach where testers actively explore the application without predefined test cases, using their knowledge and creativity to find bugs. Complements automated testing by finding edge cases automation might miss.

---

## C# & .NET Terminology

### Async/Await
C# pattern for non-blocking asynchronous operations. Essential for I/O (database, API calls). All our services use `async Task<T>` return types.

### Dependency Injection (DI)
Design pattern where dependencies are provided ("injected") rather than created internally. Enables testability and loose coupling. ASP.NET Core has built-in DI.

### Nullable Reference Types
C# feature (enabled by default in .NET 6+) that makes the compiler warn about potential null reference exceptions. We use `?` for nullable and `!` for null-forgiving assertions.

### Record
A C# reference type that provides value-based equality and immutability by default. Perfect for DTOs and domain objects.
```csharp
public record ViolationDto(int Id, string Type, DateTime Date);
```

### Repository Pattern
An abstraction layer between business logic and data access. Provides a collection-like interface for querying and persisting entities. Enables unit testing without a database.

### SOLID Principles
Five design principles for maintainable object-oriented code:
- **S**ingle Responsibility - One reason to change
- **O**pen/Closed - Open for extension, closed for modification
- **L**iskov Substitution - Subtypes must be substitutable
- **I**nterface Segregation - Prefer small, focused interfaces
- **D**ependency Inversion - Depend on abstractions, not concretions

### Top-Level Programs
C# 9+ feature allowing a `Program.cs` without explicit `Main` method, namespace, or class wrapper. Modern .NET projects use this by default.
```csharp
// Old style
namespace MyApp {
    class Program {
        static void Main(string[] args) { }
    }
}

// Top-level (modern)
var builder = WebApplication.CreateBuilder(args);
// ...
```

---

## HOA Domain Terms

### Assessment
A fee charged to homeowners—either regular (dues) or special (one-time for specific projects like roof replacement).

### CCR (Covenants, Conditions & Restrictions)
The legal documents governing an HOA community. Defines rules, restrictions, and homeowner obligations.

### Escalation
The process of increasing violation severity over time:
- **30 days:** First notice/warning
- **60 days:** Second notice + fine
- **90 days:** Third notice + increased fine + potential legal action

### Grace Period
Time allowed before late fees apply. Standard is **30 days** from due date.

### Late Fee
Penalty for overdue payments. RealManage uses **10% monthly compound interest** calculated from the original balance.

### Violation
An infraction of HOA rules requiring tracking, notices, and potential fines. Examples: unapproved landscaping, parking violations, noise complaints.

---

## Development Workflow

### CI/CD (Continuous Integration/Continuous Deployment)
Automated pipelines that build, test, and deploy code. Ensures quality and enables frequent releases. We use Azure DevOps Pipelines.

### Plan Mode
Claude Code feature (activated with `Shift+Tab` or `/plan`) that creates an explicit plan before implementing changes. Essential for complex multi-file modifications.

### Refactoring
Restructuring code without changing external behavior. Improve readability, performance, or maintainability while tests continue passing.

---

## Quick Reference Table

| Term | One-Line Definition |
|------|---------------------|
| Agentic | AI that acts autonomously on multi-step tasks |
| Context Window | How much text Claude can "see" at once (~200K tokens) |
| Hallucination | AI generating incorrect but plausible information |
| Token | Basic unit of text (~4 characters) |
| Command | Reusable prompt template in `.claude/commands/` |
| Skill | Auto-discoverable command in `.claude/skills/` |
| Hook | Script that runs before/after Claude operations |
| Plugin | Packaged collection of commands/skills/agents |
| TDD | Write tests first, then implementation |
| Red-Green-Refactor | Fail → Pass → Improve cycle |
| Coverage | Percentage of code executed by tests (target: 80-90%) |
| Branch Coverage | Percentage of code branches (if/else paths) executed by tests. More thorough than line coverage. |
| Mocking | Faking dependencies for isolated testing |
| Repository Pattern | Abstraction layer for data access |
| SOLID | Five OOP design principles |
| Regression Testing | Re-running tests after changes to ensure existing functionality still works. Critical for refactoring. |
| Smoke Testing | Quick, high-level tests to verify basic functionality works before deeper testing. "Does it turn on?" |
| Test Pyramid | Testing strategy: many unit tests (base), fewer integration tests (middle), few E2E tests (top). Balances speed and coverage. |

---

*See a term missing? Suggest additions in `#dx-training`!*
