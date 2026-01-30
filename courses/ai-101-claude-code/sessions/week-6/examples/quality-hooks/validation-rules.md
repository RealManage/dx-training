# Response Validation Rules

These rules power automated quality hooks. Design hooks that check for these criteria.

---

## Rule Set 1: Pre-Send Validation

**Hook Name:** `response-quality-gate`

**Trigger:** Before any customer response is sent

### Required Checks

**Greeting Check:**

- [ ] Response starts with greeting (Hi, Hello, Dear, Good morning/afternoon)
- [ ] Customer name used if known
- **FAIL:** "Missing greeting - add appropriate opening"

**Closing Check:**

- [ ] Response ends with professional closing
- [ ] Signature or contact info included
- **FAIL:** "Missing closing - add sign-off and contact info"

**Acknowledgment Check:**

- [ ] Response acknowledges customer's concern/question
- [ ] Not jumping straight to policy/facts
- **FAIL:** "Add acknowledgment of customer's situation first"

**Next Steps Check:**

- [ ] Clear action items for customer
- [ ] Or explicit statement that no action needed
- **FAIL:** "Customer won't know what to do - add next steps"

---

## Rule Set 2: Tone Validation

**Hook Name:** `tone-checker`

**Trigger:** After draft, before send

### Forbidden Phrases

Flag these for review:

- "As I already explained..."
- "You should have..."
- "It's not our fault..."
- "You need to understand..."
- "Policy clearly states..." (soften this)
- "Unfortunately, there's nothing we can do"

**Replacement Suggestions:**

- "As I already explained" → "To clarify..."
- "You should have" → "Going forward..."
- "Policy clearly states" → "Per community guidelines..."

### Required Elements for Negative News

When delivering bad news (fee won't be waived, violation stands, etc.):

- [ ] Empathy statement present
- [ ] Explanation of WHY (not just WHAT)
- [ ] Alternative options if any exist
- [ ] Escalation path mentioned

---

## Rule Set 3: Accuracy Validation

**Hook Name:** `fact-checker`

**Trigger:** Before send

### Policy Accuracy

**Late Fee References:**

- [ ] Percentage stated as 10% (not 8%, 12%, etc.)
- [ ] Grace period stated as 30 days
- [ ] "Compound" interest mentioned correctly

**Violation Timeline:**

- [ ] Stages are 30/60/90 days
- [ ] Fines match: $0 → $50 → $100 → $200
- [ ] Appeal window stated as 30 days

**Payment Info:**

- [ ] Portal URL is correct
- [ ] Phone number is current
- [ ] Mailing address is accurate

### Risky Statements

Flag for manager review:

- Any promise of fee waiver over $50
- Any timeline commitment
- Any reference to legal matters
- Any statement about what board will/won't do

---

## Rule Set 4: Escalation Triggers

**Hook Name:** `escalation-detector`

**Trigger:** During response drafting

### Auto-Escalate to Team Lead

- Customer mentions attorney/lawyer
- Third contact about same issue
- Customer threatens media/BBB
- Balance dispute over $500

### Auto-Escalate to Manager

- ADA/accessibility requests
- Fair housing concerns mentioned
- Written complaints about staff
- Legal documents received

### Auto-Escalate to Legal

- Actual legal papers served
- Government agency inquiry
- Discrimination allegations
- Safety/liability concerns

---

## Implementing Hooks

### Conceptual Hook Definition

```yaml
hook:
  name: response-quality-gate
  trigger: pre-send
  checks:
    - type: contains
      element: greeting
      fail_message: "Add greeting"
    - type: contains
      element: closing
      fail_message: "Add closing"
    - type: not_contains
      patterns: ["as I already explained", "you should have"]
      fail_message: "Soften language - see suggestions"
  on_fail: block_and_review
  on_pass: allow_send
```

### Using Claude as Quality Gate

```
Review this response before I send it.

Check for:
1. Greeting and closing present
2. Acknowledgment of customer's concern
3. Clear next steps
4. No forbidden phrases (defensive language)
5. Accurate policy references

Response:
[PASTE YOUR DRAFT]

Give me PASS or FAIL with specific issues to fix.
```
