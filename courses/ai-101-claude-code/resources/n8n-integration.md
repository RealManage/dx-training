# n8n Integration Guide for Claude Automation

This guide covers conceptual patterns for integrating Claude with n8n workflow automation. Focus is on design principles and human-in-the-loop patterns, not code implementation.

---

## Why n8n + Claude for Support

n8n is a workflow automation platform that connects different systems. Combined with Claude, it enables:

| Capability | Example |
| ---------- | ------- |
| Automated triage | Classify incoming tickets by type and urgency |
| Draft generation | Pre-draft responses for human review |
| Quality validation | Check responses before sending |
| Escalation routing | Flag tickets needing manager attention |
| Reporting | Summarize ticket trends |

**Important:** These workflows assist humans, they don't replace them.

---

## Core Workflow Patterns

### Pattern 1: Ticket Triage Pipeline

**Purpose:** Automatically categorize and prioritize incoming tickets.

```text
┌──────────────┐    ┌──────────────┐    ┌──────────────┐    ┌──────────────┐
│   Webhook    │--->│    Claude    │--->│   Route by   │--->│    Queue     │
│ (new ticket) │    │   Classify   │    │   Category   │    │  Assignment  │
└──────────────┘    └──────────────┘    └──────────────┘    └──────────────┘
```

**Workflow Steps:**

1. **Trigger:** Webhook receives new ticket from ticketing system
2. **Extract:** Parse ticket text, sender info, account data
3. **Classify with Claude:**
   - Category: billing, violation, maintenance, general, legal
   - Priority: urgent, normal, low
   - Sentiment: frustrated, confused, neutral
   - Missing info: what we need to respond
4. **Route:** Send to appropriate queue based on classification
5. **Enrich:** Add classification metadata to ticket

**Claude Prompt for Classification:**

```text
Analyze this support ticket and classify it:

---
[TICKET TEXT]
---

Respond in JSON format:
{
  "category": "billing|violation|maintenance|general|legal",
  "priority": "urgent|normal|low",
  "sentiment": "frustrated|confused|neutral|positive",
  "confidence": 0.0-1.0,
  "missing_info": ["list of info needed to respond"],
  "suggested_template": "template name if applicable",
  "escalation_flag": true|false,
  "escalation_reason": "reason if flagged"
}
```

**Human Checkpoint:**

- Review any classification with confidence < 0.8
- Review all escalation flags
- Spot-check 10% of normal priority tickets

---

### Pattern 2: Response Draft Pipeline

**Purpose:** Pre-draft responses for human review and approval.

```text
┌──────────────┐    ┌──────────────┐    ┌──────────────┐    ┌──────────────┐
│  Classified  │--->│    Claude    │--->│   Quality    │--->│    Human     │
│    Ticket    │    │    Draft     │    │    Check     │    │    Review    │
└──────────────┘    └──────────────┘    └──────────────┘    └──────────────┘
                                               │
                                               ▼
                                        ┌──────────────┐
                                        │    Reject    │
                                        │  (re-draft)  │
                                        └──────────────┘
```

**Workflow Steps:**

1. **Input:** Ticket with classification metadata
2. **Draft with Claude:**
   - Use appropriate template for category
   - Include ticket-specific details
   - Apply tone guidelines
3. **Quality Check:**
   - Verify required elements present
   - Check tone alignment
   - Flag policy references for accuracy
4. **Human Review:**
   - Present draft with quality score
   - Allow edit, approve, or reject
5. **Output:** Approved response ready to send

**Claude Prompt for Drafting:**

```text
Draft a customer response for this classified ticket:

Ticket:
---
[TICKET TEXT]
---

Classification:
- Category: [CATEGORY]
- Sentiment: [SENTIMENT]
- Priority: [PRIORITY]

Requirements:
- Acknowledge the customer's specific concern
- Match tone to their sentiment level
- Reference policy [X.X] if applicable
- Provide clear next steps
- Keep under 150 words

Use our standard greeting and closing.
```

**Quality Gate Criteria:**

| Check | Pass Criteria |
| ----- | ------------- |
| Greeting | Present and appropriate |
| Acknowledgment | Addresses specific concern |
| Explanation | Clear and accurate |
| Next steps | Specific and actionable |
| Tone | Matches sentiment |
| Length | Within guidelines |
| Forbidden phrases | None present |

---

### Pattern 3: Escalation Alert Pipeline

**Purpose:** Detect and route tickets requiring manager attention.

```text
┌──────────────┐    ┌──────────────┐    ┌──────────────┐    ┌──────────────┐
│    Ticket    │--->│    Claude    │--->│  Threshold   │--->│    Alert     │
│    Stream    │    │   Evaluate   │    │    Check     │    │ Slack/Email  │
└──────────────┘    └──────────────┘    └──────────────┘    └──────────────┘
```

**Escalation Triggers:**

| Condition | Threshold |
| --------- | --------- |
| Repeat contact | 3+ contacts on same issue |
| Legal language | Mentions attorney, lawsuit, legal action |
| Social media | Threatens public complaint |
| Account value | High-value account with complaint |
| Time sensitivity | Deadline within 24 hours |
| Sentiment | Extremely negative tone |

**Claude Prompt for Escalation Detection:**

```text
Evaluate this ticket for escalation:

Ticket:
---
[TICKET TEXT]
---

Account context:
- Contact count for this issue: [N]
- Account value: [VALUE]
- Account standing: [STATUS]
- Previous escalations: [HISTORY]

Check for:
1. Legal language or threats
2. Social media/review threats
3. Repeat contact frustration
4. Safety or emergency concerns
5. Board member or VIP status
6. Deadline pressure

Respond:
{
  "escalate": true|false,
  "reason": "primary reason for escalation",
  "urgency": "immediate|today|this_week",
  "recommended_handler": "tier2|manager|legal",
  "summary": "2-sentence summary for manager"
}
```

---

## CLAUDE.md Setup for n8n Environments

When running Claude in automated workflows, configure context appropriately.

**Automation-Focused CLAUDE.md:**

```markdown
# Support Automation Context

## Role
You are processing support tickets for automated triage and drafting.

## Response Format
Always respond in valid JSON format as specified in the prompt.
Do not include explanations outside the JSON structure.

## Policies
[Include relevant policy snippets]

## Tone Guidelines
- Empathetic but professional
- Clear and concise
- Action-oriented

## Forbidden Phrases
- "Unfortunately"
- "As I said before"
- "Per our policy"
- "You should have"

## Templates
[Include standard response templates]

## Confidence Thresholds
- Classification: Flag for review if confidence < 0.8
- Escalation: Always flag legal, safety, or VIP
- Drafts: Human review required for all outputs
```

---

## Human-in-the-Loop Patterns

**Critical Principle:** Automated Claude workflows should assist humans, never replace human judgment entirely.

### Approval Gates

```text
┌──────────────┐    ┌──────────────┐    ┌──────────────┐
│   AI Draft   │--->│    Human     │--->│    Action    │
│              │    │    Review    │    │    (send)    │
└──────────────┘    └──────────────┘    └──────────────┘
                          │
                          ▼
                    ┌──────────────┐
                    │    Reject    │
                    │ (edit/redo)  │
                    └──────────────┘
```

**When to require human approval:**

| Scenario | Approval Level |
| -------- | -------------- |
| All customer-facing responses | Required |
| Internal routing decisions | Optional (spot-check) |
| Escalation flags | Manager review |
| Account changes | Required |
| Financial decisions | Manager approval |

### Confidence-Based Routing

Route to human review based on AI confidence:

| Confidence | Action |
| ---------- | ------ |
| > 0.9 | Auto-route, spot-check 5% |
| 0.8 - 0.9 | Human verification before action |
| < 0.8 | Full human review required |

### Audit Trail

Every automated action should log:

- Timestamp
- Input (ticket ID, text hash)
- Claude's output
- Confidence scores
- Human reviewer (if any)
- Final action taken
- Any edits made

---

## Example: Slack Integration

**Scenario:** New ticket triggers Slack notification with pre-classification.

```text
┌──────────────┐    ┌──────────────┐    ┌──────────────┐
│    Ticket    │--->│    Claude    │--->│    Slack     │
│   Webhook    │    │   Classify   │    │   Message    │
└──────────────┘    └──────────────┘    └──────────────┘
```

**Slack Message Format:**

```text
🎫 New Ticket #12345

Category: Billing (0.95 confidence)
Priority: Normal
Sentiment: Frustrated

Summary: Customer questioning $150 late fee, claims payment was made

Suggested Template: late-fee-explanation

[ View Ticket ] [ Draft Response ] [ Escalate ]
```

**Button Actions:**

- **View Ticket:** Opens ticketing system
- **Draft Response:** Triggers draft workflow, posts draft to thread
- **Escalate:** Routes to manager with auto-generated escalation note

---

## Implementation Considerations

### Rate Limiting

Don't overwhelm Claude with requests:

| Guideline | Value |
| --------- | ----- |
| Max concurrent requests | 5-10 |
| Delay between requests | 1-2 seconds |
| Daily budget cap | Set per workflow |

### Error Handling

What happens when Claude fails:

| Failure | Action |
| ------- | ------ |
| Timeout | Retry with exponential backoff |
| Invalid JSON | Log and route to human |
| Low confidence | Route to human |
| API error | Queue for later, alert if persistent |

### Cost Management

Monitor and cap costs:

- Set `--max-budget-usd` per request
- Track daily/weekly spend
- Alert on unusual patterns
- Use lower-cost models for simple classification

---

## Workflow Checklist

Before deploying any Claude + n8n workflow:

- [ ] Human approval gate for all customer-facing outputs
- [ ] Confidence thresholds defined
- [ ] Error handling configured
- [ ] Rate limiting in place
- [ ] Cost caps set
- [ ] Audit logging enabled
- [ ] Escalation paths tested
- [ ] Fallback to human for failures
- [ ] Spot-check process defined
- [ ] Metrics/monitoring configured

---

## What This Guide Doesn't Cover

This is a conceptual guide. For implementation:

- **n8n documentation:** [docs.n8n.io](https://docs.n8n.io)
- **Claude API:** [docs.anthropic.com](https://docs.anthropic.com)
- **Production hardening:** See [Production Hardening Guide](./production-hardening.md)

---

*Questions about Claude automation? Ask in `#ai-exchange` on Slack.*
