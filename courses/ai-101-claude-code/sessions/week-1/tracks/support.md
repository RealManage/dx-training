# Week 1: Setup & Orientation - Support Track

**Duration:** 45-60 minutes
**Technical Level:** Beginner-friendly (no coding required)
**Prerequisites:** Just your computer and an internet connection

---

## Welcome to the Support Track

This 6-week course teaches you to use Claude Code as your AI assistant for customer support work. By the end, you'll draft responses faster, create better templates, and build a comprehensive knowledge base.

### Your 6-Week Journey

| Week | Topic | What You'll Do |
| ---- | ----- | -------------- |
| 1 | Setup & Orientation | Install Claude Code, try your first prompts ✓ |
| 2 | Customer Communication | Draft responses, create templates |
| 3 | Template Organization | Structure your template library |
| 4 | Quality Assurance | Test and improve response quality |
| 5 | Using Skills | Leverage pre-built automations |
| 6 | Capstone Project | Build a Support Knowledge Base |

**Total time:** 4-6 hours over 6 weeks

---

## What You'll Learn Today

By the end of this session, you'll be able to:

- ✅ Install Claude Code on your computer
- ✅ Log in and authenticate
- ✅ Ask Claude questions and get useful responses
- ✅ Refine responses through iteration
- ✅ Understand how CLAUDE.md context files work
- ✅ Know what makes a good vs. bad prompt

---

## Why This Matters for Support

You already know HOA policies better than anyone. Claude Code helps you:

| Your Expertise | + Claude = | Result |
| -------------- | ---------- | ------ |
| Resident issues | Quick response drafts | Faster ticket resolution |
| Policy knowledge | FAQ generation | Consistent answers |
| Escalation paths | Decision trees | Clear routing |
| Common questions | Template library | Less repetitive typing |

**You're not learning to code.** You're learning to work *with* an AI assistant that amplifies your domain expertise.

### What Claude Code Can Do For You

Here are real examples of how Support team members use Claude Code daily:

**Draft Professional Responses**
> "A resident is upset about a $200 fine. Draft an empathetic response that explains compound interest and offers a payment plan."

**Generate FAQ Articles**
> "Create a FAQ about the violation appeal process with 5 questions and answers."

**Explain Complex Policies**
> "Explain late fee calculations in simple terms for a resident who doesn't understand why their $100 balance became $133."

**Create Response Templates**
> "Create a template for responding to first-time violation notices. Include placeholders for [RESIDENT_NAME], [VIOLATION_TYPE], and [CURE_DATE]."

**Summarize Long Documents**
> "Summarize the key points from this 20-page CC&R document that affect landscaping violations."

---

## Setup (15 minutes)

### Option A: Use the Install Script (Recommended)

We've created scripts to make installation easy:

**Mac/Linux:**

```bash
# From the dx-training repository root, run:
cd courses/ai-101-claude-code/scripts
./setup-course-tools.sh
```

**Windows:**

```powershell
# From the dx-training repository root, run:
cd courses\ai-101-claude-code\scripts
.\SetupCourseTools.ps1
```

The script will:

1. Check if you have the required software
2. Install Claude Code
3. Tell you what to do next

### Option B: Manual Installation

If the script doesn't work, use the official native installer:

**Mac/Linux/WSL:**

```bash
curl -fsSL https://claude.ai/install.sh | bash
```

**Windows PowerShell:**

```powershell
irm https://claude.ai/install.ps1 | iex
```

**Then authenticate:**

- Run: `claude`
- A browser window opens
- Sign in with your Anthropic/Claude account
- Return to your terminal

See [official setup docs](https://code.claude.com/docs/en/setup) for more options.

> **Stuck?** Ask for help in **#ai-exchange** on Slack. No question is too basic!

---

## Your First Interaction (30 minutes)

### 1. Open Claude Code

From the repository root, run these commands:

```bash
# Navigate to the support example folder
cd courses/ai-101-claude-code/sessions/week-1/examples/support

# Start Claude Code
claude
```

Claude Code reads the CLAUDE.md file in this folder automatically. This file contains HOA policies, escalation timelines, and response guidelines that Claude will use when answering your questions.

### 2. Practice Prompts

Try each of these prompts. Notice how Claude uses the context from CLAUDE.md to give accurate, policy-compliant answers.

---

**Prompt 1: Violation Notice Response**

```text
A resident just received their first violation notice for leaving trash cans out.
They're upset and say they didn't know about the rule.
Draft a professional, empathetic response.
```

**What to notice:** Claude acknowledges their frustration while clearly explaining the policy and next steps.

---

**Prompt 2: Late Fee Explanation**

```text
A resident is confused about their account balance. They owed $100 and now it shows $133.
Explain how the 10% monthly compound interest works and what their options are.
```

**What to notice:** Claude does the math correctly and offers solutions (payment plan, etc.).

---

**Prompt 3: Appeal Process**

```text
A homeowner wants to appeal a $150 fine for a second violation.
What's the process, and what should I tell them?
```

**What to notice:** Claude references the correct escalation timeline (30/60/90 days) from the context file.

---

**Prompt 4: New Resident Welcome**

```text
Draft a welcome email for a new resident explaining:
- How to access the resident portal
- When dues are due
- Who to contact for questions
Keep it friendly and under 200 words.
```

**What to notice:** Claude follows the word limit and maintains a welcoming tone.

---

**Prompt 5: Escalation Message**

```text
A resident has ignored two violation notices over 60 days.
Draft the escalation message for the third notice that mentions potential legal action.
```

**What to notice:** Claude maintains professionalism while conveying seriousness.

---

**Prompt 6: FAQ Generation**

```text
Generate 5 FAQs about the violation appeal process.
Write clear, friendly answers that a resident can easily understand.
```

**What to notice:** Claude creates structured, helpful content you can use directly.

---

### 3. Iterative Prompting Exercise

Good prompts often require refinement. Let's practice improving a response through iteration.

**Step 1: Initial prompt**

```text
A resident is asking why they have to pay $50 for a parking violation.
Draft a response.
```

**Step 2: Review the response.** What's missing or could be better?

**Step 3: Refine with follow-up prompts:**

```text
Make the response more empathetic - they're a first-time offender.
```

```text
Add information about how to avoid this in the future.
```

```text
Include the phone number for questions: (555) 123-4567
```

**Step 4: Final polish**

```text
Review this response for tone. Is it professional but friendly?
Suggest any final improvements.
```

**Key lesson:** You don't need to get it perfect on the first try. Claude can refine responses based on your feedback.

---

### 4. Template Creation Exercise

Let's create a reusable template you can use for real tickets.

**Prompt:**

```text
Create a response template for first-time violation notices.

Include these placeholders:
- [RESIDENT_NAME]
- [PROPERTY_ADDRESS]
- [VIOLATION_TYPE]
- [VIOLATION_DATE]
- [CURE_DEADLINE]
- [YOUR_NAME]

The template should:
1. Be empathetic (first offense)
2. Clearly explain what needs to be fixed
3. Provide the cure deadline
4. Offer help if they have questions
```

**Save this template!** Copy it to a document for future use. In Week 3, you'll learn to organize a whole template library.

---

## Understanding CLAUDE.md

When you started Claude Code in the examples/support folder, it automatically read the CLAUDE.md file. This file tells Claude:

- **Your role** - Support team member at RealManage
- **Communication guidelines** - Professional, empathetic, policy-accurate
- **Business context** - Violation timelines, late fee rules, common issues
- **Response templates** - Examples of good responses

**This is why Claude gives policy-accurate answers** - it has the context it needs.

### How Context Files Work

```
Your Question + CLAUDE.md Context = Better Answer
```

Without CLAUDE.md:
> "Explain late fees" → Generic answer about late fees

With CLAUDE.md:
> "Explain late fees" → Specific answer about 10% monthly compound interest after 30-day grace period

**In Week 3**, you'll learn to customize CLAUDE.md files for your specific needs.

---

## Tips for Effective Prompting

### What Works

| Do This | Example |
| ------- | ------- |
| Be specific | "Draft a response for a first-time parking violation" |
| Provide context | "The resident is elderly and on a fixed income" |
| Set constraints | "Keep it under 150 words" |
| Ask for revisions | "Make it more empathetic" |
| Request formats | "Use bullet points for the action items" |

### What Doesn't Work

| Avoid This | Why |
| ---------- | --- |
| Vague prompts | "Write something about violations" gives generic results |
| No context | Claude can't read your mind about the situation |
| Expecting perfection | First drafts are starting points, not final answers |
| Not iterating | One prompt rarely gets the perfect result |

### The 3-Step Prompting Process

1. **Draft** - Get an initial response
2. **Review** - What's good? What's missing?
3. **Refine** - Ask Claude to improve specific parts

---

## What You DON'T Need

Unlike developers, you can skip:

| Skip This | Why |
| --------- | --- |
| .NET SDK | You're not writing C# code |
| VS Code extensions | You'll use the terminal or simple text files |
| Sandbox project setup | We provide a pre-configured example |
| Git commands | You're creating content, not code |

---

## Homework

Before next week, practice these skills:

### Required (15-20 minutes)

1. **Ask 5 work-related questions** - Use Claude Code to help with actual tickets or tasks
2. **Save 2 good responses** - Copy responses you'd actually use to a document
3. **Practice iteration** - Take one response through 3 refinement steps
4. **Note what didn't work** - Write down prompts that gave poor results (we'll fix them in Week 2)

### Stretch Goals

1. **Create a template** - Build one reusable template for a common ticket type
2. **Try different tones** - Ask Claude to make the same response "more formal" or "more friendly"
3. **Share a win** - Post something useful Claude helped you create in **#ai-exchange**

---

## Getting Help

- **#ai-exchange** - Ask questions, share wins, get help
- **DM a trainer** - For private questions or scheduling help
- **Office Hours** - Drop-in live help sessions

---

## Next Week: Prompting for Customer Communication

In Week 2, you'll learn to:

- Write prompts that generate professional responses every time
- Create templates for common ticket types
- Draft KB articles from scratch
- Handle sensitive escalation messages
- Use iterative prompting to get exactly what you need

---

*Return to [main README](../README.md) | See [Support Quick-Start Guide](../../../resources/quick-start-support.md)*
