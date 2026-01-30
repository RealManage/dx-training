# Ticket Triage Automation Rules

Define rules for automated ticket categorization and routing.

---

## Category Detection

### Billing Category

**Trigger Keywords:**

- payment, pay, paid, balance, amount, charge
- fee, late fee, interest, dues
- invoice, statement, bill, receipt
- refund, credit, adjustment

**Sub-Categories:**

| Sub-Category | Keywords | Priority |
| ------------ | -------- | -------- |
| Late fee dispute | "unfair", "wrong charge", "didn't know" | Normal |
| Payment confirmation | "did my payment", "confirm receipt" | Low |
| Balance question | "how much", "what do I owe" | Low |
| Payment plan request | "payment plan", "can't afford", "hardship" | Normal |
| Refund request | "refund", "overcharged", "credit" | Normal |

---

### Violation Category

**Trigger Keywords:**

- violation, notice, fine, penalty
- warning, compliance, CC&R, rules
- appeal, dispute, unfair
- landscaping, parking, noise, trash

**Sub-Categories:**

| Sub-Category | Keywords | Priority |
| ------------ | -------- | -------- |
| First notice response | "just received", "first time" | Normal |
| Appeal request | "appeal", "disagree", "unfair" | Normal |
| Compliance question | "what do I need to do", "how to fix" | Low |
| Fine dispute | "shouldn't have to pay", "wrong amount" | Normal |
| 90-day escalation | "legal", "attorney", "final notice" | High |

---

### General Inquiry Category

**Trigger Keywords:**

- question, information, when, where, how
- meeting, board, schedule, event
- amenity, pool, gym, clubhouse
- contact, phone, email, address

**Sub-Categories:**

| Sub-Category | Keywords | Priority |
| ------------ | -------- | -------- |
| Meeting information | "board meeting", "when is", "schedule" | Low |
| Amenity access | "pool", "gym", "hours", "key" | Low |
| New resident | "just moved", "new owner", "welcome" | Normal |
| Document request | "CC&Rs", "rules", "forms" | Low |

---

### Escalation Category (Auto-Flag)

**Immediate Escalation Triggers:**

| Trigger | Route To | Response Time |
| ------- | -------- | ------------- |
| "lawyer", "attorney", "legal action" | Manager | 4 hours |
| "news", "media", "reporter" | Manager | 4 hours |
| "discrimination", "fair housing" | Manager + Legal | 2 hours |
| "ADA", "disability", "accessible" | Manager | 4 hours |
| "BBB", "attorney general", "complaint" | Manager | 4 hours |
| "unsafe", "danger", "emergency" | Emergency | Immediate |

---

## Priority Scoring

### Factors

| Factor | Weight | Scoring |
| ------ | ------ | ------- |
| Emotional intensity | 3x | ALL CAPS +2, multiple !!! +1 |
| Account tenure | 2x | 5+ years = higher priority |
| Issue recurrence | 2x | 3rd contact = escalate |
| Dollar amount | 1x | Over $500 = higher priority |
| Time sensitivity | 2x | Deadline mentioned = +1 |

### Priority Levels

```
Score 0-3: Low (24-48 hour response)
Score 4-6: Normal (same-day response)
Score 7-9: High (4-hour response)
Score 10+: Urgent (immediate escalation)
```

---

## Missing Information Detection

### Required for Response

**Billing Issues:**

- [ ] Account number or unit number
- [ ] Specific charge being questioned
- [ ] Date range (if applicable)

**Violation Issues:**

- [ ] Notice date or violation type
- [ ] Property address (if not on file)
- [ ] What they're disputing specifically

**General Inquiries:**

- [ ] Specific question (not just "call me")
- [ ] Contact preference (if requesting callback)

### Auto-Response for Missing Info

When critical information is missing:

```
Thank you for reaching out. To help you more quickly,
could you please provide:

- [MISSING_ITEM_1]
- [MISSING_ITEM_2]

Once I have this information, I'll be happy to assist.
```

---

## Workflow Automation

### Triage Flow

```
1. RECEIVE ticket

2. EXTRACT:
   - Account/unit (from email or content)
   - Issue type (keyword matching)
   - Sentiment (intensity scoring)
   - Missing info (checklist)

3. CATEGORIZE:
   - Primary category
   - Sub-category
   - Priority score

4. CHECK escalation triggers
   - If triggered → Route to escalation queue
   - Else → Continue

5. CHECK for auto-response eligibility
   - Simple + high confidence → Draft response
   - Missing info → Request info template
   - Complex → Human queue

6. ROUTE to appropriate queue:
   - Billing queue
   - Violation queue
   - General queue
   - Escalation queue (with alert)

7. OUTPUT: Categorized ticket with:
   - Category and sub-category
   - Priority level
   - Suggested response (if applicable)
   - Missing info flags
   - Escalation notes (if any)
```

---

## Quality Gates

### Before Auto-Response

- [ ] Confidence score > 85%
- [ ] No escalation triggers detected
- [ ] All required information present
- [ ] Template match found
- [ ] No previous escalation on this account

### Human Review Required When

- Confidence < 85%
- Any escalation trigger present
- Third contact on same issue
- Account has history flag
- Response involves fee waiver > $50
- Legal/compliance implications
