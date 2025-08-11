# RealManage AI 101: Claude Code (8 Week Training Course)

**Goal:** Over ~8 weeks, progressively immerse RealManage's cross-functional team in Claude Code. Each 2-hour session builds practical skills while leaving time for normal work. The plan assumes participants are mostly beginners in AI but have varying technical backgrounds. Sessions combine short explanations, guided demos, hands-on exercises and reflection. Adjust pacing and topics based on your team's needs.

## Session 1 – Setup & Orientation (Week 1)

### Objectives
- Install and authenticate the `claude-code` CLI. Understand the basics of how Claude Code works and why it's more than a simple code completion tool. [Claude Code Overview](https://docs.anthropic.com/en/docs/claude-code/overview)
- Introduce RealManage-relevant use cases (onboarding to HOA codebases, automating recurring tasks, summarising meeting notes).
- Show how to ask effective questions in plain English; emphasise that quality input yields quality output.

### Agenda (2 hrs)

1. **Installation & Authentication (20 min):** 
   - **Windows users**: First install WSL2 with Ubuntu - [WSL Setup Guide](https://docs.anthropic.com/en/docs/claude-code/setup#windows)
   - System requirements: Node 18+, npm 10+
   - Install via `npm install -g @anthropic-ai/claude-code` - [Official Installation Guide](https://docs.anthropic.com/en/docs/claude-code/setup#installation)
   - Complete OAuth authentication with your Anthropic account - [OAuth Setup](https://docs.anthropic.com/en/docs/claude-code/setup#authentication)
   - Verify installation with `claude doctor` command

2. **Tour of the CLI (20 min):** 
   - Start a session with `claude` command
   - Essential slash commands - [Slash Commands Reference](https://docs.anthropic.com/en/docs/claude-code/slash-commands)
   - Understanding token usage and costs - [Managing Costs and Tokens](https://docs.anthropic.com/en/docs/claude-code/costs)
   - Complete CLI reference - [CLI Reference Guide](https://docs.anthropic.com/en/docs/claude-code/cli-reference)

3. **First Queries (30 min):** 
   - Practice Codebase Q&A in a sample HOA management repo
   - Example questions: "How does payment processing work?", "What edge cases does the violation tracking handle?"
   - Explore common workflows - [Common Workflows Guide](https://docs.anthropic.com/en/docs/claude-code/common-workflows)
   - Experiment with Claude's file reading and code analysis capabilities

4. **CLAUDE.md and Memory (40 min):** 
   - Understand the hierarchical memory system - [Memory Management System](https://docs.anthropic.com/en/docs/claude-code/memory)
   - Project memory (`./CLAUDE.md`) vs user memory (`~/.claude/CLAUDE.md`) - [Memory Hierarchy](https://docs.anthropic.com/en/docs/claude-code/memory#hierarchy)
   - Quick setup: Use `#` shortcut or `/memory` command
   - Best practices: Be specific, use bullet points, organize by topic - [Memory Best Practices](https://docs.anthropic.com/en/docs/claude-code/memory#best-practices)
   - Create a RealManage-specific CLAUDE.md template

5. **Reflection (10 min):** Discuss takeaways: Was installation easy? What questions did Claude answer well? What real tasks could this assist with?

## Session 2 – Prompting Foundations (Week 2)

### Objectives
- Learn how to craft clear prompts that get high-quality responses from Claude.
- Practice using system prompts, examples, and XML tags for structure.
- Build a small prompt library for RealManage use cases (e.g., summarising support tickets, drafting HOA violation letters).

### Agenda (2 hrs)

1. **Prompt Engineering Primer (25 min):** 
   - Learn the fundamentals - [Prompt Engineering Overview](https://docs.anthropic.com/en/docs/build-with-claude/prompt-engineering/overview)
   - Claude 4 specific techniques - [Claude 4 Best Practices](https://docs.anthropic.com/en/docs/build-with-claude/prompt-engineering/claude-4-best-practices)
   - Being explicit and providing context for better results
   - System prompts and role assignment - [System Prompts and Roles](https://docs.anthropic.com/en/docs/build-with-claude/prompt-engineering/system-prompts)

2. **Structured Prompts (15 min):** 
   - Using XML tags for clarity - [Using XML Tags](https://docs.anthropic.com/en/docs/build-with-claude/prompt-engineering/use-xml-tags)
   - Markdown sections to organize prompts
   - Reducing ambiguity through structure

3. **Hands-On Prompt Workshop (60 min):** 
   - Break into pairs for RealManage scenarios:
     - Summarizing board meeting transcripts
     - Drafting homeowner violation notices
     - Generating financial reports
   - Practice with examples - [Multishot Prompting](https://docs.anthropic.com/en/docs/build-with-claude/prompt-engineering/multishot-prompting)
   - Use XML tags to separate instructions and outputs
   - Test and refine prompts in the CLI

4. **Build a Prompt Library (15 min):** Collect the best prompts in a shared repo or CLAUDE.md file. Encourage adding variables and placeholders for dynamic content.

5. **Feedback & Next Steps (10 min):** Discuss what prompt patterns worked. Suggest trying prompts in daily tasks and adding to the library.

## Session 3 – Plan Mode & Exploration (Week 3)

### Objectives
- Understand Claude's Plan Mode and its benefits for breaking down complex tasks.
- Practice the "Explore → Plan → Code → Commit" workflow and time-boxed thinking modes.
- Apply Plan Mode to real tasks (e.g., adding a feature to the HOA platform).

### Agenda (2 hrs)

1. **Plan Mode Overview (20 min):** 
   - Extended thinking for complex tasks - [Extended Thinking](https://docs.anthropic.com/en/docs/build-with-claude/extended-thinking)
   - Different thinking depths: "think," "think hard," "ultra think"
   - Tips for effective thinking - [Extended Thinking Tips](https://docs.anthropic.com/en/docs/build-with-claude/prompt-engineering/extended-thinking-tips)
   - Workflow: Explore → Plan → Code → Commit

2. **Exploration in Practice (25 min):** 
   - Read files, mockups, and URLs without writing code
   - Ask architecture and dependency questions
   - Chain of thought techniques - [Chain of Thought Prompting](https://docs.anthropic.com/en/docs/build-with-claude/prompt-engineering/chain-of-thought)
   - Long context best practices - [Long Context Tips](https://docs.anthropic.com/en/docs/build-with-claude/prompt-engineering/long-context-tips)

3. **Hands-On Plan Mode (45 min):** 
   - Small group exercise: "Add HOA dues dashboard widget"
   - Use Explore to inspect existing code
   - Ask Claude to "think hard" about the implementation
   - Create and review step-by-step plans
   - Practical examples - [Claude Code Beginners Guide](https://www.geeky-gadgets.com/claude-code-beginners-guide-2025/)

4. **Optional Implementation (20 min):** 
   - Begin coding from the plan
   - Use `/permissions` to control file edits
   - Review changes before accepting

5. **Debrief (10 min):** Discuss how Plan Mode improved clarity. Capture the plan and any modifications to the CLAUDE.md file for future reference.

## Session 4 – Test-Driven Development with Claude (Week 4)

### Objectives
- Learn how to pair TDD with Claude to produce reliable code and avoid hallucinations.
- Understand the TDD workflow: write tests first, confirm failure, generate code, iterate until success.
- Apply TDD to RealManage tasks (e.g., verifying HOA rule compliance logic).

### Agenda (2 hrs)

1. **TDD Introduction (20 min):** 
   - Official TDD workflow - [Claude Code Best Practices - TDD](https://www.anthropic.com/engineering/claude-code-best-practices)
   - Why TDD works with AI - [Claude Code and TDD - The New Stack](https://thenewstack.io/claude-code-and-the-art-of-test-driven-development/)
   - Red-Green-Refactor cycle with Claude
   - Benefits: Clear targets, prevents hallucinations, catches regressions

2. **Writing Tests with Claude (20 min):** 
   - Ask Claude to write tests from specifications
   - CLAUDE MD TDD approach - [CLAUDE MD TDD Wiki](https://github.com/ruvnet/claude-flow/wiki/CLAUDE-MD-TDD)
   - Ensure tests cover edge cases
   - Python/TypeScript examples for HOA systems

3. **Iterative Coding (40 min):** 
   - TDD Guard tool - [TDD Guard for Claude Code](https://github.com/nizos/tdd-guard)
   - Run tests to confirm failure (red phase)
   - Write code to pass tests (green phase)
   - Refactor while keeping tests green
   - Never let Claude modify the tests

4. **Hands-On Exercise (30 min):** 
   - RealManage examples: late fee calculation, violation tracking
   - Practical TDD commands - [Awesome Claude Code - TDD](https://github.com/hesreallyhim/awesome-claude-code)
   - E2E testing approach - [E2E Testing with Claude Code](https://shipyard.build/blog/e2e-testing-claude-code/)
   - How Anthropic teams use TDD - [How Teams Use Claude Code](https://www.anthropic.com/news/how-anthropic-teams-use-claude-code)

5. **Reflection (10 min):** 
   - Compare TDD with AI vs traditional TDD - [Test-driven development with AI](https://www.builder.io/blog/test-driven-development-ai)
   - Discuss how TDD improves Claude's accuracy
   - Plan to integrate TDD in daily workflow

## Session 5 – Advanced CLI Features & Multi-Claude Workflows (Week 5)

### Objectives
- Customize the CLI with themes, permissions and slash commands.
- Introduce multi-Claude workflows (running multiple instances to parallelize tasks). Learn about safe YOLO mode and when to use it.

### Agenda (2 hrs)

1. **Customization & Permissions (30 min):** 
   - Settings management - [Claude Code Settings](https://docs.anthropic.com/en/docs/claude-code/settings)
   - Use `/config` for themes and shortcuts
   - Control permissions with `/permissions`
   - Security best practices - [Security Guide](https://docs.anthropic.com/en/docs/claude-code/security)
   - Share settings via `.claude/settings.json`

2. **Custom Slash Commands (20 min):** 
   - Creating custom commands - [Custom Slash Commands](https://docs.anthropic.com/en/docs/claude-code/slash-commands#custom-commands)
   - Place Markdown files in `.claude/commands/`
   - Pass arguments with `$ARGUMENTS`
   - Examples from community - [Awesome Claude Code](https://github.com/hesreallyhim/awesome-claude-code)

3. **Advanced Workflows (40 min):** 
   - Complete CLI reference - [CLI Reference](https://docs.anthropic.com/en/docs/claude-code/cli-reference)
   - Safe YOLO mode for repetitive tasks (use with caution!)
   - Multiple Claude instances with `git worktrees`
   - Collaborative workflows: code writer + reviewer

4. **Hands-On Practice (20 min):** Participants create a custom slash command for a RealManage use case (e.g., generating a summary of HOA violations). They also experiment with a second Claude instance reviewing the first instance's output.

5. **Discussion (10 min):** Share experiences with multi-Claude workflows and brainstorm other automation opportunities.

## Session 6 – MCP Servers & External Integrations (Week 6)

### Objectives
- Understand the concept of MCP servers and how they extend Claude Code with external tools.
- Learn how to build a simple server that provides custom tools (e.g., retrieving real-time HOA dues data) and integrate it with Claude.

### Agenda (2 hrs)

1. **MCP Overview (20 min):** 
   - Official MCP guide - [MCP Integration Guide](https://docs.anthropic.com/en/docs/claude-code/mcp)
   - Understanding the protocol - [Model Context Protocol](https://docs.anthropic.com/en/docs/mcp)
   - Benefits: automation, flexibility, third-party integrations
   - Community overview - [How MCP Servers Enhance Claude Code](https://www.geeky-gadgets.com/mcp-servers-claude-code-integration/)

2. **Examples & Use Cases (15 min):** 
   - Brave Search, Flux image generation
   - HOA-specific: violation visuals, dues tracking
   - Financial analysis and data scraping
   - MCP Connector API - [MCP Connector](https://docs.anthropic.com/en/docs/agents-and-tools/mcp-connector)

3. **Server Quickstart (35 min):** 
   - Build with MCP SDK - [MCP SDK Documentation](https://docs.anthropic.com/en/docs/claude-code/sdk)
   - Quickstart guide - [MCP Server Quickstart](https://modelcontextprotocol.io/quickstart/server)
   - Implement `get_hoa_fees(account_id)` example
   - Test locally with mock API

4. **Connecting to Claude (30 min):** 
   - Configure `.claude/.mcp.json` for server connection
   - Call custom tools from Claude
   - Logging best practices - [MCP Server Logging](https://modelcontextprotocol.io/quickstart/server#logging)

5. **Reflection & Brainstorming (20 min):** 
   - RealManage automation ideas: financial reports, ticket analysis
   - Propose custom MCP server projects
   - Plan future integrations

## Session 7 – Real-World Scenarios & Continuous Improvement (Week 7)

### Objectives
- Apply learned skills to cross-functional RealManage tasks.
- Show how Claude Code can assist product managers, support staff, and engineers.
- Emphasize continuous improvement via prompt iteration, memory updates and test refinement.

### Agenda (2 hrs)

1. **Cross-Functional Use Cases (30 min):** 
   - **Support staff:** Summarize homeowner tickets, draft responses, flag common issues
   - **Project managers:** Generate release notes, plan sprints, track tasks
   - **Product managers:** Synthesize user feedback into feature briefs
   - **Engineers:** Automate refactoring, generate docs, create tests
   - IDE integration - [IDE Integration Guide](https://docs.anthropic.com/en/docs/claude-code/ide-integrations)

2. **Group Exercise (60 min):** 
   - Form cross-functional teams
   - Apply all learned techniques: Plan Mode, TDD, MCP tools
   - Terminal setup tips - [Terminal Configuration](https://docs.anthropic.com/en/docs/claude-code/terminal-config)

3. **Iterate & Improve (20 min):** 
   - Refine prompts, CLAUDE.md, tests
   - Monitor usage and costs - [Usage Monitoring](https://docs.anthropic.com/en/docs/claude-code/monitoring-usage)
   - GitHub Actions for automation - [GitHub Actions Integration](https://docs.anthropic.com/en/docs/claude-code/github-actions)

4. **Debrief (10 min):** Share results across teams; discuss lessons learned and areas where Claude excelled or struggled.

## Session 8 – Capstone Hackerspace & Future Roadmap (Week 8)

### Objectives
- Consolidate all skills by working on a larger project over 2 hours.
- Encourage creativity and identify follow-up projects.

### Agenda (2 hrs)

1. **Project Selection (10 min):** 
   - Teams propose RealManage solutions:
     - Automate HOA violation escalation
     - Create self-service knowledge base
     - Integrate with external CRM systems

2. **Building (90 min):** 
   - Apply all learned skills: Plan Mode, TDD, custom commands, MCP servers
   - Reference tutorials - [Claude Code Tutorials](https://docs.anthropic.com/en/docs/agents-and-tools/claude-code/tutorials)
   - Document everything in CLAUDE.md
   - Write tests throughout development

3. **Demo & Celebration (20 min):** 
   - Team demonstrations
   - Discuss production readiness
   - Future learning paths: RAG, vector databases
   - Community resources - [Introducing .claude Directory](https://htdocs.dev/posts/introducing-claude-your-ultimate-directory-for-claude-code-excellence/)

## Follow-Up and Resources

- **Documentation:** 
  - [Anthropic Claude Code Docs](https://docs.anthropic.com/en/docs/claude-code)
  - [Prompt Engineering Guide](https://docs.anthropic.com/en/docs/build-with-claude/prompt-engineering)
  - [Claude Code Best Practices](https://www.anthropic.com/engineering/claude-code-best-practices)

- **Internal Support:** 
  - Create `#claude-hackerspace` Slack channel
  - Schedule weekly office hours
  - Share prompt libraries and CLAUDE.md templates

- **Continued Learning:** 
  - Advanced topics: RAG, vector databases, agent frameworks
  - Compare with other tools: Copilot, ChatGPT, Cursor
  - Explore automation opportunities

- **Safety & Ethics:** 
  - Maintain human oversight
  - Validate AI outputs before production use
  - Protect sensitive HOA and resident data

---

*This hackerspace syllabus splits the AI 101 content into manageable, two-hour sessions that gradually build proficiency with Claude Code. It emphasises practical application, cross-functional relevance, and continuous iteration—key to embedding AI effectively into RealManage's daily workflows.*

## References

### Official Anthropic Documentation
1. **Claude Code Documentation** - https://docs.anthropic.com/en/docs/claude-code
2. **Prompt Engineering Guide** - https://docs.anthropic.com/en/docs/build-with-claude/prompt-engineering
3. **Model Context Protocol** - https://docs.anthropic.com/en/docs/mcp
4. **Claude Code Best Practices** - https://www.anthropic.com/engineering/claude-code-best-practices

### Test-Driven Development Resources
5. **Claude Code and TDD** - The New Stack - https://thenewstack.io/claude-code-and-the-art-of-test-driven-development/
6. **CLAUDE MD TDD Wiki** - https://github.com/ruvnet/claude-flow/wiki/CLAUDE-MD-TDD
7. **TDD Guard for Claude Code** - https://github.com/nizos/tdd-guard
8. **E2E Testing with Claude Code** - https://shipyard.build/blog/e2e-testing-claude-code/

### Community Resources
9. **Awesome Claude Code** - https://github.com/hesreallyhim/awesome-claude-code
10. **Claude Code Beginners Guide** - Geeky Gadgets - https://www.geeky-gadgets.com/claude-code-beginners-guide-2025/
11. **MCP Servers Guide** - Geeky Gadgets - https://www.geeky-gadgets.com/mcp-servers-claude-code-integration/
12. **MCP Quickstart** - https://modelcontextprotocol.io/quickstart/server

### Additional Learning
13. **Test-driven development with AI** - Builder.io - https://www.builder.io/blog/test-driven-development-ai
14. **How Anthropic Teams Use Claude Code** - https://www.anthropic.com/news/how-anthropic-teams-use-claude-code
15. **Claude Directory Guide** - https://htdocs.dev/posts/introducing-claude-your-ultimate-directory-for-claude-code-excellence/