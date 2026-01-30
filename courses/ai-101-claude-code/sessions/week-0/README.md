# Week 0: AI Foundations (Optional Pre-Course Primer)

**Duration:** 1-1.5 hours async (self-paced, includes reading time)
**Format:** Reading, reflection, discussion
**Audience:** All participants, especially non-developers (PMs, QA, Support Staff)

## Purpose

This optional "on-ramp" week provides foundational context before diving into Claude Code. If you already understand AI basics and have realistic expectations about AI coding tools, feel free to skip ahead to [Week 1: Setup & Orientation](../week-1/README.md).

---

## Part 1: What is AI/LLM/Agentic Engineering?

### A Brief History of AI in Software Development

The journey to AI-assisted coding has been decades in the making:

| Era | Development | Impact |
| --- | ----------- | ------ |
| **1950s-1990s** | Expert systems, rule-based AI | Limited domain-specific applications |
| **2000s-2015** | Machine learning, autocomplete | IDE suggestions, syntax highlighting |
| **2015-2020** | Deep learning, neural networks | Code completion (IntelliSense, TabNine) |
| **2020-2022** | Large Language Models | Copilot, ChatGPT - line-level suggestions |
| **2023-Present** | Agentic AI | Claude Code - full codebase understanding, multi-step execution |

The key evolution is from **reactive** (autocomplete what you're typing) to **agentic** (understand context, make decisions, execute multi-step tasks).

### What LLMs Are (High Level)

**Large Language Models (LLMs)** like Claude are AI systems trained on massive text datasets to understand and generate human-like text. Think of them as extremely sophisticated pattern-matchers that have absorbed patterns from billions of lines of code and documentation.

**Key characteristics:**

- **Context-aware:** Can understand surrounding code and conversation history
- **Generative:** Creates new content rather than just retrieving existing answers
- **Probabilistic:** Outputs are predictions, not deterministic lookups
- **Multi-modal:** Can understand code, prose, structured data, and increasingly, images

**Practical implication:** LLMs don't "know" things the way a database does. They predict likely responses based on patterns. This is why they can be remarkably helpful AND occasionally wrong.

### What "Agentic" Means in Claude Code

Traditional AI assistants respond to single prompts. **Agentic** AI can:

- **Read entire codebases** - not just the file you're in
- **Execute multi-step plans** - break down complex tasks autonomously
- **Use tools** - run tests, search files, execute commands
- **Learn context** - maintain project-specific knowledge via CLAUDE.md
- **Iterate independently** - fix its own mistakes without constant guidance

**Example comparison:**

| Traditional AI | Agentic AI (Claude Code) |
| -------------- | ------------------------ |
| "Here's a function that might work" | "I'll read the related files, understand the patterns, write the code, generate tests, run them, and fix any failures" |
| One response, then waits | Can execute 20+ steps autonomously |
| Limited to current file | Understands full project architecture |

---

## Part 2: Realistic Expectations

### What AI Coding Tools CAN Do Well

Claude Code excels at tasks that benefit from pattern recognition, broad knowledge, and tireless consistency:

**High-Value Use Cases:**

- Explain unfamiliar codebases quickly
- Generate boilerplate and CRUD operations
- Write comprehensive test suites
- Refactor with specific patterns
- Draft documentation from code
- Identify bugs through code analysis
- Convert between languages/frameworks
- Apply consistent coding standards

**Why These Work:** These tasks benefit from Claude having seen millions of similar examples and never getting bored with repetitive work.

### What AI Tools CANNOT Do (Yet)

Understanding limitations prevents frustration and dangerous over-reliance:

**Hallucinations are real:**

- May invent APIs that don't exist
- Can suggest deprecated methods with confidence
- Might reference library versions incorrectly
- Could generate plausible but wrong business logic

**Verification is always required:**

- Claude doesn't actually run your code (unless explicitly told to)
- It doesn't know your production data or edge cases
- It can't access private systems without MCP configuration
- Domain expertise (like HOA regulations) may be incomplete

**Current limitations:**

- Cannot see UI/visual output directly (but can read image files)
- No access to real-time information without web search
- Token limits for extremely large codebases
- May need iteration for complex, novel tasks

### The "10x Developer" Myth vs. Reality

You may have heard AI makes developers "10x more productive." Here's a nuanced reality:

**What 10x means in practice:**

| Task Type | Realistic Speedup | Why |
| --------- | ----------------- | --- |
| Boilerplate/CRUD | 5-10x | Highly repetitive, well-known patterns |
| Documentation | 3-5x | Claude excels at explanation |
| Test writing | 3-5x | Given good examples, very consistent |
| Complex architecture | 1.2-2x | Still requires deep human judgment |
| Novel algorithms | 1-1.5x | Less training data, needs more iteration |
| Debugging production | Variable | Depends heavily on context available |

**The honest truth:**

- AI amplifies your existing skills - it doesn't replace expertise
- Senior developers benefit MORE (know what to ask, can verify quality)
- Juniors can move faster but need to develop judgment alongside
- "10x" on some tasks, "0.5x" on others when fighting the AI

**Bottom line:** Expect productivity gains, but treat AI as a capable junior developer who needs review, not an infallible expert.

---

## Part 3: Key Vocabulary

Before starting the course, familiarize yourself with these essential terms. Full definitions are available in the [Course Glossary](../../resources/glossary.md).

### Top 10 Terms You MUST Know

| Term | Quick Definition | Why It Matters |
| ---- | ---------------- | -------------- |
| **LLM (Large Language Model)** | The AI technology powering Claude | Understanding what you're working with |
| **Token** | Basic unit of text (~4 characters) | Affects context window limits |
| **Context Window** | How much Claude can "see" at once (~200K tokens) | Determines project size limitations |
| **Prompt** | Your instruction/question to Claude | Better prompts = better results |
| **Hallucination** | AI generating incorrect information confidently | Why you ALWAYS verify AI output |
| **Agentic** | AI that acts autonomously on multi-step tasks | What makes Claude Code different |
| **CLAUDE.md** | Project context file Claude reads automatically | Your project's "memory" |
| **TDD (Test-Driven Development)** | Write tests before implementation | Core practice in this course |
| **Code Coverage** | Percentage of code tested (target: 80-90%) | Our recommended quality bar |
| **Plan Mode** | Claude thinks before acting | Essential for complex tasks |

### Quick Self-Test

Can you explain these to a colleague?

1. Why might an LLM "hallucinate" an API that doesn't exist?
2. What's the difference between a traditional AI assistant and an agentic one?
3. Why do we care about tokens?

---

## Part 4: How This Course Works

### The 9-Week Structure

| Week | Topic | Focus |
| ---- | ----- | ----- |
| **Week 0** | AI Foundations | *(You are here)* Optional primer |
| **Week 1** | Setup & Orientation | Install, configure, first contact |
| **Week 2** | Prompting Foundations | Clear communication with Claude |
| **Week 3** | Tactical Planning & Code Review | Breaking down complex tasks |
| **Week 4** | Test-Driven Development | Red-Green-Refactor with AI |
| **Week 5** | Commands & Basic Skills | Custom slash commands, reusable prompts |
| **Week 6** | Agents & Hooks | Custom agents, lifecycle automation |
| **Week 7** | Plugins | Packaging and distribution |
| **Week 8** | Real-World Automation | Headless automation, efficiency strategies |
| **Week 9** | Capstone & Certification | Apply everything, get certified |

### Role-Specific Tracks

While everyone follows the same core curriculum, each week includes role-specific exercises:

**Developers:**

- Generate production code with tests
- Refactor legacy systems
- Build microservices
- [Quick Start Guide for Developers](../../resources/quick-start-developer.md)

**Product Managers:**

- Generate user stories from requirements
- Create release notes from commits
- Analyze technical documentation
- [Quick Start Guide for PMs](../../resources/quick-start-pm.md)

**QA Engineers:**

- Generate test cases automatically
- Create comprehensive test suites
- Identify edge cases and coverage gaps
- [Quick Start Guide for QA](../../resources/quick-start-qa.md)

**Support Staff:**

- Search codebases for relevant info
- Draft customer responses
- Generate knowledge base articles
- *(Uses PM quick-start as foundation)*

### What You'll Need

**Technical Requirements:**

- Windows 10+, macOS 10.15+, or Ubuntu 20.04+
- 4GB RAM minimum
- Node.js 18+ (22 LTS recommended)
- VS Code or similar IDE
- Internet connection

**Time Commitment:**

- 2 hours per week (session time)
- 1-2 hours per week (homework/practice)
- Optional: Office hours Thursdays 2-3 PM CT

**Support Channels:**

- Slack: `#ai-exchange`

---

## Exercises (Non-Coding)

These reflection exercises prepare your mindset for AI-assisted work.

### Exercise 1: Reflection - Your AI History (15 min)

**Journal or discuss with a colleague:**

1. What AI tools have you used before (Copilot, ChatGPT, Siri, etc.)?
2. What worked well? What frustrated you?
3. What concerns do you have about AI-assisted coding?
4. What tasks in your current role would benefit most from AI assistance?

**Share your thoughts:** Post one insight in `#ai-exchange` Slack channel.

### Exercise 2: Hallucination Awareness (15 min)

**Read this scenario:**

> A developer uses AI to generate a function that calculates compound interest. The AI produces confident-looking code that uses `Math.CompoundInterest(principal, rate, periods)`. The developer copies it directly into production.
>
> Problem: `Math.CompoundInterest()` doesn't exist in C#. The code compiles (syntax is valid) but throws a runtime error.

**Questions to consider:**

1. How could this have been caught before production?
2. What verification steps should become habit?
3. How does this change your mental model of AI assistance?

**Write down 3 verification habits** you'll adopt when using AI-generated code.

### Exercise 3: Expectation Calibration (15 min)

**Rate your current expectations (1-5):**

| Statement | Rating |
| --------- | ------ |
| AI will write most of my code for me | |
| AI-generated code needs careful review | |
| AI understands my specific business domain | |
| AI can replace junior developers | |
| AI will make me faster at my job | |

**Revisit after Week 2** - see how your expectations shift with hands-on experience.

### Exercise 4: Pre-Course Discussion (15 min)

**Discussion prompts for team meetings or Slack:**

1. "If AI can write code, what becomes MORE valuable about human developers?"
2. "What's one task you spend too much time on that might benefit from AI?"
3. "How do we maintain code quality when AI generates much of our code?"
4. "What guardrails should we have for AI-generated code in production?"

**Post your best insight** from the discussion to `#ai-exchange`.

---

## Success Criteria

You're ready for Week 1 when you can:

- [ ] Explain what an LLM is at a high level
- [ ] Describe what "agentic" means and why it matters
- [ ] List 3 things AI coding tools do well
- [ ] List 3 limitations or risks of AI coding tools
- [ ] Define the 10 key vocabulary terms
- [ ] Understand the 9-week course structure
- [ ] Know where to find your role-specific quick-start guide

---

## Resources

### Course Materials

- [Full Course Glossary](../../resources/glossary.md)
- [Quick Start: Developers](../../resources/quick-start-developer.md)
- [Quick Start: PMs](../../resources/quick-start-pm.md)
- [Quick Start: QA](../../resources/quick-start-qa.md)

### External Reading (Optional)

- [Anthropic's Claude Documentation](https://docs.anthropic.com/en/docs/claude-code)
- [What are LLMs? (MIT Technology Review)](https://www.technologyreview.com/2023/04/10/1071177/what-are-large-language-models/)
- [The Agentic Era (Anthropic Blog)](https://www.anthropic.com/news)

### Support

- Slack: `#ai-exchange`

---

## Next Steps

When you're ready, proceed to [Week 1: Setup & Orientation](../week-1/README.md) where you'll install Claude Code and have your first AI-assisted coding experience.

**Pre-work for Week 1:**

- Ensure you have an Anthropic account (free tier works)
- Have VS Code installed with recommended extensions
- Clear 2 hours for the hands-on session

---

*End of Week 0: AI Foundations*
*Next Session: Week 1 - Setup & Orientation*
