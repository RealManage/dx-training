# Support Prompt Library

Ready-to-use prompt templates for support workflows. Copy, customize, and add to your CLAUDE.md for consistent results.

---

## Response Drafting Templates

### Standard Response Draft

```
Draft a response for this customer issue:
[PASTE TICKET/EMAIL]

Context:
- Customer type: [homeowner/board member/vendor]
- Issue type: [billing/violation/maintenance/general]
- Emotional state: [frustrated/confused/neutral/upset]
- Account status: [good standing/past due/new customer]

Requirements:
- Tone: empathetic and professional
- Acknowledge their specific concern first
- Reference policy [X.X] if applicable
- Provide 2-3 specific next steps
- Keep under 150 words
```

### Urgent/Escalated Response

```
Draft a priority response for this escalated issue:
[PASTE TICKET/EMAIL]

Escalation context:
- This is the customer's [Nth] contact about this issue
- Previous resolution attempts: [list what's been tried]
- Customer sentiment: [frustrated/demanding/threatening legal action]
- Deadline or urgency: [describe timeline pressure]

Requirements:
- Lead with sincere acknowledgment of the ongoing issue
- Summarize what's happened (show we understand)
- State exactly what we're doing NOW
- Provide specific timeline for resolution
- Include direct contact for follow-up
- Tone: serious, accountable, empathetic
- Length: under 200 words
```

### Welcome/Onboarding Response

```
Draft a welcome message for a new resident:

New resident info:
- Property address: [ADDRESS]
- Move-in date: [DATE]
- Community name: [COMMUNITY]
- Account number: [NUMBER]

Include:
- Warm welcome to the community
- Key dates (dues, board meetings)
- How to access the portal
- Contact information for questions
- One piece of helpful local info
- Tone: friendly, welcoming, helpful
```

### Tone Variations

**Empathetic opening for frustrated customers:**
```
Rewrite this response opening to be more empathetic:
[PASTE CURRENT OPENING]

The customer is frustrated about [ISSUE]. They've contacted us [N] times.
Show genuine understanding without being overly apologetic or patronizing.
```

**Firm but fair for policy enforcement:**
```
Draft a response that maintains policy while showing understanding:

Situation: [DESCRIBE]
Policy being enforced: [POLICY REFERENCE]
Customer's objection: [THEIR ARGUMENT]

Requirements:
- Acknowledge their perspective
- Explain the policy clearly (not defensively)
- State what we can and cannot do
- Offer any available alternatives
- Professional, not cold
```

---

## Fee Explanation Templates

### Late Fee Breakdown

```
Explain this late fee situation with a clear calculation breakdown:

Account details:
- Original assessment amount: $[AMOUNT]
- Original due date: [DATE]
- Current date: [DATE]
- Current balance: $[AMOUNT]
- Late fee rate: 10% monthly compound interest

Create:
1. Month-by-month calculation showing how we arrived at the current balance
2. Plain English explanation (no financial jargon)
3. What happens if payment isn't made this month
4. Payment options available

Format for customer readability (short paragraphs, clear numbers).
```

### Special Assessment Explanation

```
Explain this special assessment to a homeowner:

Assessment details:
- Amount: $[AMOUNT]
- Purpose: [PROJECT - e.g., roof replacement, reserve funding]
- Due date: [DATE]
- Payment plan available: [YES/NO]

Context:
- Board approval date: [DATE]
- Total project cost: $[AMOUNT]
- Number of units sharing cost: [NUMBER]

Include:
- Why this assessment was necessary
- How the amount was calculated
- Payment options
- Consequences of non-payment
- Who to contact with questions
```

### Fee Waiver Decision

```
Help me evaluate this fee waiver request:

Request details:
- Fee amount: $[AMOUNT]
- Fee type: [late fee/processing fee/legal fee]
- Customer's reason: [THEIR EXPLANATION]
- Account history: [good/poor/first issue]
- Previous waivers: [YES/NO - if yes, when]

Consider:
- Is this a reasonable first-time exception?
- What precedent does this set?
- What's the customer's overall payment history?

Provide:
- Recommendation (approve/deny/partial)
- Suggested response language either way
- Any conditions to attach if approving
```

---

## Escalation Documentation

### Internal Escalation Note

```
Create an internal escalation note for ticket #[NUMBER]:

Ticket summary:
[PASTE TICKET AND FULL HISTORY]

Create an escalation note including:
- Issue summary (2-3 sentences max)
- Timeline of customer contacts
- Resolution attempts made
- Why this needs escalation
- Customer's desired outcome
- Recommended resolution
- Urgency level (low/medium/high/critical)
- Financial impact (if any)
```

### Manager Handoff

```
Create a manager handoff document for this escalated case:

Case details:
[PASTE ALL RELEVANT INFORMATION]

Format:
## Executive Summary
[1-2 sentences: what's the issue and why does it need manager attention]

## Customer Background
[Account standing, tenure, previous issues]

## Issue Timeline
[Chronological list of events]

## Actions Taken
[What support has already done]

## Customer Position
[What they want and why]

## Our Position
[Policy considerations, constraints]

## Recommendation
[Suggested resolution with rationale]

## Decision Needed
[Specific question for manager]
```

### Legal/Collections Referral

```
Prepare documentation for collections/legal referral:

Account: [NUMBER]
Property: [ADDRESS]
Balance owed: $[AMOUNT]
Oldest delinquency: [DATE]

Document the following:
1. Complete payment history (last 12 months)
2. All communication attempts (dates, methods, outcomes)
3. Any payment plans offered and their status
4. Customer responses or objections
5. Reason standard collection has failed
6. Recommendation for next steps

Note: Include only factual information. No opinions or emotional language.
```

---

## Knowledge Base Generation

### FAQ from Common Questions

```
Create a FAQ entry for this common question:

Question we receive: "[EXACT QUESTION AS CUSTOMERS ASK IT]"

Background:
- Why customers ask this: [CONTEXT]
- Relevant policy: [REFERENCE]
- Typical resolution: [WHAT WE TELL THEM]

Format the FAQ entry as:
**Q: [Customer-friendly version of the question]**

A: [Clear answer in plain language, under 100 words]

**Policy Reference:** [Section X.X]

**Related Questions:**
- [Link to related FAQ 1]
- [Link to related FAQ 2]

**Still need help?** [Contact information]
```

### KB Article from Ticket Pattern

```
I've noticed we get many tickets about [TOPIC]. Help me create a knowledge base article.

Common ticket examples:
[PASTE 2-3 EXAMPLE TICKETS]

Create a self-service KB article with:
1. Clear title (what customers search for)
2. Brief overview (1-2 sentences)
3. Step-by-step instructions (if applicable)
4. Common variations/scenarios
5. Troubleshooting section
6. When to contact support instead
7. Related articles

Write for customers, not staff. Use simple language.
```

### Process Documentation

```
Document this support process for training purposes:

Process name: [NAME]
When to use: [TRIGGER CONDITIONS]

Current process:
[DESCRIBE OR PASTE CURRENT STEPS]

Create documentation that includes:
1. Overview (what this process accomplishes)
2. Prerequisites (what you need before starting)
3. Step-by-step instructions (numbered)
4. Decision points (if X, do Y)
5. Common mistakes to avoid
6. Quality checks
7. Handoff points (when to escalate)
8. Examples of correct execution
```

---

## Tone Checking Prompts

### Pre-Send Quality Check

```
Review this draft response before I send it:

[PASTE DRAFT RESPONSE]

Customer context:
- Emotional state: [frustrated/confused/neutral]
- Issue severity: [minor/moderate/serious]
- Previous contacts: [number]

Check for:
- [ ] Empathy: Does it acknowledge their concern?
- [ ] Clarity: Is it easy to understand?
- [ ] Completeness: Are all questions answered?
- [ ] Accuracy: Are policy references correct?
- [ ] Tone: Appropriate for the situation?
- [ ] Next steps: Clear actions for the customer?
- [ ] Length: Appropriate (not too long/short)?

Provide specific suggestions for improvement.
```

### Forbidden Phrase Check

```
Check this response for phrases we should avoid:

[PASTE DRAFT]

Flag any instances of:
- "Unfortunately" at the start (sounds negative)
- "Per our policy" (sounds bureaucratic)
- "You should have" (blaming language)
- "I'm just..." (diminishing language)
- "To be honest" (implies previous dishonesty)
- "As I said before" (sounds impatient)
- ALL CAPS (sounds like yelling)
- Excessive exclamation points

Suggest professional alternatives for any flagged phrases.
```

### Empathy Enhancement

```
Make this response more empathetic without being over-the-top:

Original response:
[PASTE RESPONSE]

The customer is [frustrated/confused/upset] because [REASON].

Improve the empathy by:
- Adding acknowledgment of their specific concern
- Showing we understand the impact on them
- Using warmer language where appropriate
- Avoiding corporate-speak

Don't make it longer. Don't add false promises.
Keep it genuine and professional.
```

### Consistency Check

```
Compare these responses for consistency:

Response 1 (to Customer A):
[PASTE]

Response 2 (to Customer B):
[PASTE]

Both customers have the same issue: [DESCRIBE]

Check:
- Are we giving the same information?
- Is the tone consistent?
- Are any policies stated differently?
- Would either customer feel treated unfairly?

Identify any inconsistencies and recommend the correct standard response.
```

---

## Quick Reference Prompts

### One-Line Drafts

```
Quick draft: [late fee explanation for $X balance, Y months overdue]
```

```
Quick draft: [payment plan offer, $X over N months]
```

```
Quick draft: [violation first notice, landscaping issue]
```

```
Quick draft: [board meeting info request response]
```

### Quality Shortcuts

```
Tone check: [PASTE] - is this empathetic enough for an upset customer?
```

```
Clarity check: [PASTE] - can a non-expert understand this?
```

```
Policy check: [PASTE] - does this accurately reflect our late fee policy?
```

---

## Tips for Using These Prompts

1. **Customize the brackets:** Replace [BRACKETED TEXT] with actual information
2. **Add your context:** Include CLAUDE.md with your policies and tone guidelines
3. **Iterate:** If the first output isn't right, refine the prompt
4. **Verify accuracy:** Always check policy references before sending
5. **Human review:** Never send AI-generated responses without reading them

---

**Remember:** AI drafts, humans verify and send. These prompts save time; they don't replace judgment.

*See [Quick Start: Support](./quick-start-support.md) for the full track overview.*
