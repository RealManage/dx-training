# Support Toolkit Plugin Specification

Design document for a team-wide support plugin.

---

## Overview

**Plugin Name:** RealManage Support Toolkit
**Version:** 1.0.0
**Audience:** Customer Support Team
**Purpose:** Standardize response quality and automate common tasks

---

## Installation

### Using the Plugin Manager (Recommended)

```bash
# Interactive: Open plugin manager
/plugin
# Navigate to Marketplaces tab → Add your team's marketplace
# Then go to Discover tab → Install the plugin

# Or CLI: Install from marketplace
claude plugin install support-toolkit@realmanage-plugins --scope project
```

### Installation Scopes

| Scope | Use Case | Shared via Git? |
| ----- | -------- | --------------- |
| `user` | Personal use across all projects | No |
| `project` | Team-shared (in `.claude/settings.json`) | Yes |
| `local` | Project-only, personal (gitignored) | No |

### For Local Development

```bash
# Test plugin locally before publishing
claude --plugin-dir ./support-toolkit

# Validate plugin structure
/plugin validate ./support-toolkit
```

### What Gets Installed

- 5 custom skills (namespaced as `/support-toolkit:skill-name`)
- 2 hook configurations
- Team CLAUDE.md context
- Response templates library

> **Note:** Skills are available immediately after installation - no restart required.

---

## Component Specifications

### Skill: /support-toolkit:draft-response

**Purpose:** Generate customer responses quickly

**Arguments:**

- `type` - Issue category (late-fee, violation, general, welcome)
- `description` - Situation summary

**Output:** Complete response ready for review

**Example:**

```bash
/support-toolkit:draft-response late-fee "Resident confused about $542 balance"
```

---

### Skill: /support-toolkit:explain-fee

**Purpose:** Generate accurate fee explanations

**Arguments:**

- `fee-type` - Type of fee (late, processing, legal)
- `amount` - Optional: specific amount
- `months` - Optional: months overdue

**Output:** Clear fee calculation with policy reference

**Example:**

```bash
/support-toolkit:explain-fee late 450 2
```

---

### Skill: /support-toolkit:tone-check

**Purpose:** Review response tone before sending

**Arguments:** None (reads from clipboard or input)

**Output:** Analysis with improvement suggestions

**Example:**

```bash
/support-toolkit:tone-check
[paste your draft response]
```

---

### Skill: /support-toolkit:escalation-note

**Purpose:** Create internal escalation documentation

**Arguments:**

- `ticket-id` - Reference number
- `summary` - Brief issue description

**Output:** Formatted escalation document

---

### Hook: Pre-Send Quality Gate

**Trigger:** Before sending any response

**Checks:**

1. Greeting present
2. Acknowledgment of concern
3. Clear next steps
4. Professional closing
5. No forbidden phrases
6. Accurate policy references

**On Fail:** Block send, show issues

**On Pass:** Allow send

---

### Hook: Escalation Detector

**Trigger:** During ticket review

**Flags:**

- Legal threats → Manager
- Third contact on same issue → Team Lead
- ADA/accessibility → Manager + Legal
- Media threats → Manager

**Output:** Escalation recommendation with routing

---

## Template Library

### Template: Late Fee Response

```markdown
Hi [NAME],

I understand you have questions about your account balance.

[ACKNOWLEDGMENT of their specific concern]

Here's how the charges accumulated:
- Original amount: $[AMOUNT]
- [FEE BREAKDOWN with dates]
- Current balance: $[TOTAL]

Your payment options:
- Online: [portal link]
- Phone: [number]
- Mail: [address]

[PAYMENT PLAN OFFER if balance > $500]

Please let me know if you have questions.

Best regards,
[SIGNATURE]
```

### Template: Violation Response

```markdown
Hi [NAME],

Thank you for reaching out about the violation notice dated [DATE].

[ACKNOWLEDGMENT of their concern]

The notice was issued for [VIOLATION TYPE] per CC&R Section [X.X].
[EXPLAIN what compliance looks like]

To resolve this:
1. [SPECIFIC ACTION]
2. [DEADLINE]
3. [HOW TO CONFIRM COMPLIANCE]

If you believe this was issued in error, you may appeal within 30 days
by [APPEAL PROCESS].

Best regards,
[SIGNATURE]
```

---

## Maintenance

### Updates

- Plugin owner: Support Team Lead
- Update frequency: Monthly
- Change process: PR to shared repository

### Metrics

Track plugin effectiveness:

- Response time (should decrease)
- Quality scores (should increase)
- Escalation rate (should stabilize)
- Team adoption (target: 100%)

---

## Roadmap

### v1.0 (MVP)

- [x] draft-response skill
- [x] explain-fee skill
- [x] Pre-send quality hook
- [x] Core templates (3)

### v1.1

- [ ] tone-check skill
- [ ] escalation-note skill
- [ ] Escalation detector hook
- [ ] Additional templates (5)

### v2.0

- [ ] Integration with ticketing system
- [ ] Analytics dashboard
- [ ] Multi-language support
