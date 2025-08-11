# Claude Code Hackerspace – 2-hour Weekly Sessions (RealManage AI 101)

**Goal:** Over ~8 weeks, progressively immerse RealManage's cross-functional team in Claude Code. Each 2-hour session builds practical skills while leaving time for normal work. The plan assumes participants are mostly beginners in AI but have varying technical backgrounds. Sessions combine short explanations, guided demos, hands-on exercises and reflection. Adjust pacing and topics based on your team's needs.

## Session 1 – Setup & Orientation (Week 1)

### Objectives
- Install and authenticate the `claude-code` CLI. Understand the basics of how Claude Code works and why it's more than a simple code completion tool [**<sup>1</sup>**](https://apidog.com/blog/claude-code-beginners-guide-best-practices/#:~:text=The%20%60claude.md).
- Introduce RealManage-relevant use cases (onboarding to HOA codebases, automating recurring tasks, summarising meeting notes).
- Show how to ask effective questions in plain English; emphasise that quality input yields quality output [**<sup>2</sup>**](https://apidog.com/blog/claude-code-beginners-guide-best-practices/#:~:text=fundamental%20principles%20of%20communicating%20with,reading%20junior%20developer).

### Agenda (2 hrs)

1. **Installation & Authentication (20 min):** Walk participants through system requirements (Node 18+, macOS/Ubuntu/WSL) and installation via `npm install -g @anthropic-ai/claude-code` [**<sup>3</sup>**](https://apidog.com/blog/claude-code-beginners-guide-best-practices/#:~:text=npm%20install%20). Explain how authentication works via Anthropic, Bedrock or Vertex AI [**<sup>4</sup>**](https://apidog.com/blog/claude-code-beginners-guide-best-practices/#:~:text=Authentication%3A%20The%20first%20time%20you,Bedrock%20and%20Google%20Vertex%20AI).

2. **Tour of the CLI (20 min):** Demonstrate starting a session, basic commands (`/help`, `/clear`, `/permissions`, `/memory`, etc.). Highlight cost and token usage; show how to compact history to stay within context limits [**<sup>5</sup>**](https://www.geeky-gadgets.com/claude-code-beginners-guide-2025/#:~:text=developers.%20,volume%20or).

3. **First Queries (30 min):** Practice using Claude for Codebase Q&A: ask questions about a sample repo such as "How does logging work?" or "What edge cases does this class handle?" [**<sup>6</sup>**](https://apidog.com/blog/claude-code-beginners-guide-best-practices/#:~:text=Workflow%201%3A%20Codebase%20Q%26A%20). Encourage participants to experiment and note Claude's strengths and limitations.

4. **CLAUDE.md and Memory (40 min):** Explain the file-based memory system: project memory (`./CLAUDE.md`), user memory (`~/.claude/CLAUDE.md`), parent/child directories [**<sup>7</sup>**](https://apidog.com/blog/claude-code-beginners-guide-best-practices/#:~:text=Part%202%3A%20Managing%20Context%20and,Memory). Show how to initialize a file using `/init` and modify it via `/memory`. Discuss best practices: concise, structured sections; document common commands; identify core files; define code style and workflows [**<sup>8</sup>**](https://apidog.com/blog/claude-code-beginners-guide-best-practices/#:~:text=,and%20allows%20for%20modular%20context). Create a starter CLAUDE.md for the sample project.

5. **Reflection (10 min):** Discuss takeaways: Was installation easy? What questions did Claude answer well? What real tasks could this assist with?

## Session 2 – Prompting Foundations (Week 2)

### Objectives
- Learn how to craft clear prompts that get high-quality responses from Claude.
- Practice using system prompts, examples, and XML tags for structure.
- Build a small prompt library for RealManage use cases (e.g., summarising support tickets, drafting HOA violation letters).

### Agenda (2 hrs)

1. **Prompt Engineering Primer (25 min):** Explain why being explicit and direct matters [**<sup>9</sup>**](https://apidog.com/blog/claude-code-beginners-guide-best-practices/#:~:text=Be%20Explicit%20and%20Direct). Show how providing context and motivation leads to better results [**<sup>10</sup>**](https://apidog.com/blog/claude-code-beginners-guide-best-practices/#:~:text=Explaining%20why%20you%27re%20asking%20for,a%20problem%20to%20be%20solved). Introduce few-shot prompting: supply high-quality examples to guide the model [**<sup>11</sup>**](https://apidog.com/blog/claude-code-beginners-guide-best-practices/#:~:text=Use%20High). Demonstrate assigning roles via system prompts to shape Claude's persona [**<sup>12</sup>**](https://apidog.com/blog/claude-code-beginners-guide-best-practices/#:~:text=Give%20Claude%20a%20Role%20,Prompts).

2. **Structured Prompts (15 min):** Teach using XML tags or Markdown sections to delineate instructions, input and desired output [**<sup>13</sup>**](https://apidog.com/blog/claude-code-beginners-guide-best-practices/#:~:text=Use%20XML%20Tags%20for%20Structure,and%20Clarity). Explain why this reduces ambiguity.

3. **Hands-On Prompt Workshop (60 min):** Break into pairs; each pair chooses a RealManage scenario (e.g., summarising a board meeting transcript, drafting responses to homeowner emails). Write prompts that include clear instructions, context, examples and role assignments. Use XML tags to separate instructions and outputs. Evaluate and refine prompts by running them in the CLI.

4. **Build a Prompt Library (15 min):** Collect the best prompts in a shared repo or CLAUDE.md file. Encourage adding variables and placeholders for dynamic content.

5. **Feedback & Next Steps (10 min):** Discuss what prompt patterns worked. Suggest trying prompts in daily tasks and adding to the library.

## Session 3 – Plan Mode & Exploration (Week 3)

### Objectives
- Understand Claude's Plan Mode and its benefits for breaking down complex tasks.
- Practice the "Explore → Plan → Code → Commit" workflow and time-boxed thinking modes.
- Apply Plan Mode to real tasks (e.g., adding a feature to the HOA platform).

### Agenda (2 hrs)

1. **Plan Mode Overview (20 min):** Explain that Plan Mode lets Claude "think" at different depths ("think," "think hard," "ultra think") to strategize tasks [**<sup>14</sup>**](https://www.geeky-gadgets.com/claude-code-beginners-guide-2025/#:~:text=Plan%20Mode%20is%20one%20of,of%20the%20task%20at%20hand). Review Anthropic's recommended workflow: Explore (gather information), Plan (detail steps), Code, and Commit [**<sup>15</sup>**](https://apidog.com/blog/claude-code-beginners-guide-best-practices/#:~:text=Workflow%202%3A%20Explore%2C%20Plan%2C%20Code%2C,Commit). Highlight the importance of not skipping the planning step [**<sup>16</sup>**](https://apidog.com/blog/claude-code-beginners-guide-best-practices/#:~:text=Workflow%202%3A%20Explore%2C%20Plan%2C%20Code%2C,Commit).

2. **Exploration in Practice (25 min):** Use the CLI to ask Claude to read relevant files, UI mockups or URLs without writing code. Ask questions about architecture or dependencies. Show how to incorporate long context recall by instructing Claude to extract supporting quotes before answering [**<sup>17</sup>**](https://apidog.com/blog/claude-code-beginners-guide-best-practices/#:~:text=Ask%20for%20Reference%20Quotes%3A%20Before,XML%20tag%20for%20this).

3. **Hands-On Plan Mode (45 min):** In small groups, pick a feature idea (e.g., "Add a dashboard widget for homeowner dues status"). Use Explore to inspect the existing code; then instruct Claude to enter Plan Mode and create a step-by-step plan to implement the feature. Review the plan and adjust as needed.

4. **Optional Implementation (20 min):** If time allows, let Claude begin coding part of the plan. Emphasize how to use `/permissions` to allow or deny file edits [**<sup>18</sup>**](https://apidog.com/blog/claude-code-beginners-guide-best-practices/#:~:text=,These%20settings).

5. **Debrief (10 min):** Discuss how Plan Mode improved clarity. Capture the plan and any modifications to the CLAUDE.md file for future reference.

## Session 4 – Test-Driven Development with Claude (Week 4)

### Objectives
- Learn how to pair TDD with Claude to produce reliable code and avoid hallucinations.
- Understand the TDD workflow: write tests first, confirm failure, generate code, iterate until success [**<sup>19</sup>**](https://apidog.com/blog/claude-code-beginners-guide-best-practices/#:~:text=Workflow%203%3A%20Test,with%20Claude).
- Apply TDD to RealManage tasks (e.g., verifying HOA rule compliance logic).

### Agenda (2 hrs)

1. **TDD Introduction (20 min):** Review the benefits of TDD for AI-assisted development: it sets clear targets, prevents overfitting to one prompt, and catches regressions [**<sup>19</sup>**](https://apidog.com/blog/claude-code-beginners-guide-best-practices/#:~:text=Workflow%203%3A%20Test,with%20Claude). Explain the four phases of Experimentation, Evaluation, Deployment and Monitoring from the AI TDD lifecycle [**<sup>20</sup>**](https://sdtimes.com/softwaredev/closing-the-loop-on-agents-with-test-driven-development/#:~:text=Traditionally,%2C%20developers%20have%20used%20test,and%20continuing%20these%20steps%20iteratively).

2. **Writing Tests with Claude (20 min):** Demonstrate asking Claude to write unit tests based on a high-level specification. Ensure tests are explicit about expected behavior and edge cases. Use a simple Python or TypeScript example.

3. **Iterative Coding (40 min):** Ask Claude to run the tests (within its controlled environment) and confirm they fail. Then instruct it to write code to make the tests pass; iterate until success. Emphasize not letting Claude modify the tests, and show how to review diffs before committing [**<sup>21</sup>**](https://apidog.com/blog/claude-code-beginners-guide-best-practices/#:~:text=4,Claude%20commit%20the%20final%20implementation).

4. **Hands-On Exercise (30 min):** Teams choose a small function relevant to RealManage (e.g., computing late fees, parsing homeowner violation logs). They write tests, run them, and use Claude to generate the implementation. Optionally integrate Plan Mode for larger tasks.

5. **Reflection (10 min):** Discuss how TDD changed the interaction with Claude. Encourage participants to adopt this workflow in everyday coding.

## Session 5 – Advanced CLI Features & Multi-Claude Workflows (Week 5)

### Objectives
- Customize the CLI with themes, permissions and slash commands.
- Introduce multi-Claude workflows (running multiple instances to parallelize tasks). Learn about safe YOLO mode and when to use it.

### Agenda (2 hrs)

1. **Customization & Permissions (30 min):** Use `/config` to adjust the theme and keyboard shortcuts. Use `/permissions` to allow or restrict actions like file edits and git commits [**<sup>22</sup>**](https://apidog.com/blog/claude-code-beginners-guide-best-practices/#:~:text=,can%20share%20with%20your%20team). Discuss how to share these settings via `.claude/settings.json`.

2. **Custom Slash Commands (20 min):** Show how to create reusable prompt templates by placing Markdown files in `.claude/commands/` [**<sup>23</sup>**](https://apidog.com/blog/claude-code-beginners-guide-best-practices/#:~:text=Markdown%20files%20to%20the%20,keyword%20to%20pass%20parameters). Demonstrate passing arguments with `$ARGUMENTS` and show examples like `/project:refactor` for consistent refactoring instructions.

3. **Advanced Workflows (40 min):** Explain safe YOLO mode for repetitive tasks and its risks—only run in isolated environments [**<sup>24</sup>**](https://apidog.com/blog/claude-code-beginners-guide-best-practices/#:~:text=%2A%20,to%20prevent%20accidental%20system%20damage). Show how to run multiple Claude instances using `git worktrees` and have them collaborate (e.g., one writes code, another reviews it) [**<sup>25</sup>**](https://apidog.com/blog/claude-code-beginners-guide-best-practices/#:~:text=,a%20human%20code%20review%20process).

4. **Hands-On Practice (20 min):** Participants create a custom slash command for a RealManage use case (e.g., generating a summary of HOA violations). They also experiment with a second Claude instance reviewing the first instance's output.

5. **Discussion (10 min):** Share experiences with multi-Claude workflows and brainstorm other automation opportunities.

## Session 6 – MCP Servers & External Integrations (Week 6)

### Objectives
- Understand the concept of MCP servers and how they extend Claude Code with external tools [**<sup>26</sup>**](https://www.geeky-gadgets.com/mcp-servers-claude-code-integration/#:~:text=Understanding%20mCP%20Servers).
- Learn how to build a simple server that provides custom tools (e.g., retrieving real-time HOA dues data) and integrate it with Claude.

### Agenda (2 hrs)

1. **MCP Overview (20 min):** Explain how MCP servers act as intermediaries between Claude and external APIs [**<sup>27</sup>**](https://www.geeky-gadgets.com/mcp-servers-claude-code-integration/#:~:text=,party%20tools%20to%20boost%20productivity). Discuss practical benefits: automation, flexibility, integration with third-party tools [**<sup>28</sup>**](https://www.geeky-gadgets.com/mcp-servers-claude-code-integration/#:~:text=,party%20tools%20to%20boost%20productivity).

2. **Examples & Use Cases (15 min):** Describe scenarios like retrieving Brave Search results, generating images with Flux, or combining APIs to create HOA violation visuals [**<sup>29</sup>**](https://www.geeky-gadgets.com/mcp-servers-claude-code-integration/#:~:text=How%20APIs%20Enhance%20mCP%20Server,Functionality). Highlight that you can build custom servers for financial analysis or data scraping [**<sup>30</sup>**](https://www.geeky-gadgets.com/mcp-servers-claude-code-integration/#:~:text=,to%20provide%20actionable%20market%20insights).

3. **Server Quickstart (35 min):** Provide a starter template using Python's `mcp` package. Guide participants to implement a simple function (e.g., `get_hoa_fees(account_id)` retrieving data from a mock API). Register the tool with the server and test it locally.

4. **Connecting to Claude (30 min):** Show how to configure `.claude/.mcp.json` to connect to the server. Use Claude to call the new tool and integrate results into a conversation. Discuss the importance of logging to stderr rather than stdout when using MCP servers [**<sup>31</sup>**](https://modelcontextprotocol.io/quickstart/server#:~:text=Logging%20in%20MCP%20Servers).

5. **Reflection & Brainstorming (20 min):** Ask participants to think of other tools that could automate RealManage tasks (e.g., generating financial reports, analyzing support ticket sentiment). Encourage them to propose MCP server projects for future sessions.

## Session 7 – Real-World Scenarios & Continuous Improvement (Week 7)

### Objectives
- Apply learned skills to cross-functional RealManage tasks.
- Show how Claude Code can assist product managers, support staff, and engineers.
- Emphasize continuous improvement via prompt iteration, memory updates and test refinement.

### Agenda (2 hrs)

1. **Cross-Functional Use Cases (30 min):** Present scenarios:
   - **Support staff:** Summarize homeowner tickets, draft responses, flag common issues.
   - **Project managers:** Generate release notes from commit history, plan sprints, track tasks.
   - **Product managers:** Prepare feature briefs by synthesizing user feedback.
   - **Engineers:** Automate code refactoring, generate documentation, create test cases.

2. **Group Exercise (60 min):** Form cross-role teams. Each team chooses a scenario and uses Claude to complete it. Encourage using Plan Mode, TDD and MCP tools as appropriate.

3. **Iterate & Improve (20 min):** After initial completion, revisit prompts, CLAUDE.md, tests, and server code. Improve clarity, adjust memory, refine tests. Discuss the importance of continuous evaluation and monitoring [**<sup>20</sup>**](https://sdtimes.com/softwaredev/closing-the-loop-on-agents-with-test-driven-development/#:~:text=Traditionally,%2C%20developers%20have%20used%20test,and%20continuing%20these%20steps%20iteratively).

4. **Debrief (10 min):** Share results across teams; discuss lessons learned and areas where Claude excelled or struggled.

## Session 8 – Capstone Hackerspace & Future Roadmap (Week 8)

### Objectives
- Consolidate all skills by working on a larger project over 2 hours.
- Encourage creativity and identify follow-up projects.

### Agenda (2 hrs)

1. **Project Selection (10 min):** Teams propose a RealManage problem to solve or workflow to improve (e.g., automate HOA violation escalation, create a self-service knowledge base, or integrate with external CRM).

2. **Building (90 min):** Teams build a prototype using Claude Code, Plan Mode, TDD, custom slash commands, and an MCP server if needed. Mentors circulate to assist and pair with less-experienced members. Emphasize documenting CLAUDE.md and writing tests along the way.

3. **Demo & Celebration (20 min):** Each team demos their solution. Discuss what was learned and potential next steps for productionizing the prototype. Highlight that this is a starting point; encourage participants to continue exploring advanced features, other models, retrieval-augmented generation and vector databases.

## Follow-Up and Resources

- **Documentation:** Encourage reading Anthropic's best practices guides [**<sup>32</sup>**](https://apidog.com/blog/claude-code-beginners-guide-best-practices/#:~:text=Be%20Explicit%20and%20Direct) and beginner's articles for deeper dives [**<sup>33</sup>**](https://apidog.com/blog/claude-code-beginners-guide-best-practices/#:~:text=Part%202%3A%20Managing%20Context%20and,Memory).
- **Internal Support:** Create a dedicated `#claude-hackerspace` channel for ongoing questions, prompt sharing and success stories. Schedule office hours where experts can pair with colleagues.
- **Continued Learning:** Suggest optional sessions on retrieval-augmented generation (RAG), vector databases, agent frameworks (AutoGen, LangChain), or deploying chatbots. Encourage experimenting with other AI tools like Copilot, ChatGPT or Flow to compare features.
- **Safety & Ethics:** Remind the team to maintain human oversight, avoid over-trusting AI outputs, and respect sensitive data when using external services.

---

*This hackerspace syllabus splits the AI 101 content into manageable, two-hour sessions that gradually build proficiency with Claude Code. It emphasises practical application, cross-functional relevance, and continuous iteration—key to embedding AI effectively into RealManage's daily workflows.*

## References

### Primary Sources

1. **Claude Code Beginners' Guide: Best Practices**  
   https://apidog.com/blog/claude-code-beginners-guide-best-practices/

2. **Claude Code Beginners Guide 2025 - Geeky Gadgets**  
   https://www.geeky-gadgets.com/claude-code-beginners-guide-2025/

3. **How MCP Servers Enhance Claude Code for Workflow Automations - Geeky Gadgets**  
   https://www.geeky-gadgets.com/mcp-servers-claude-code-integration/

4. **Build an MCP Server - Model Context Protocol**  
   https://modelcontextprotocol.io/quickstart/server

5. **Closing the loop on agents with test-driven development - SD Times**  
   https://sdtimes.com/softwaredev/closing-the-loop-on-agents-with-test-driven-development/