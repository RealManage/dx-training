# Quality Hooks - Automated Response Checks

## Context

This environment is for designing **hooks** that automatically check response quality. Hooks run when certain events happen, catching issues before they reach customers.

## What Are Hooks for Support?

Hooks are automated checks that can:

- Review responses before sending
- Validate policy accuracy
- Check tone and empathy
- Flag missing information
- Prevent common mistakes

## Hook Trigger Points

> **Note:** The trigger names below are conceptual workflow designs for discussion purposes. They are **not** literal Claude Code hook events. Claude Code's actual hook events are `PreToolUse`, `PostToolUse`, `Notification`, and `Stop`. See the [main README](../../README.md) for real hook syntax.

| Conceptual Trigger | When It Would Run | Use Case |
| ------------------ | ----------------- | -------- |
| Pre-send | Before response goes out | Quality check |
| Post-draft | After generating response | Improvement suggestions |
| On-escalation | When ticket is escalated | Documentation check |
| Daily-review | End of shift | Batch quality audit |

## Quality Check Categories

### 1. Completeness Hooks

- All customer questions answered?
- Next steps included?
- Contact info provided?
- Missing required elements?

### 2. Tone Hooks

- Empathy statement present?
- Defensive language detected?
- Professional throughout?
- Appropriate for situation?

### 3. Accuracy Hooks

- Policy references correct?
- Numbers/dates accurate?
- No forbidden phrases?
- Legal risk language?

### 4. Format Hooks

- Length within limits?
- Proper greeting/closing?
- Well-structured paragraphs?
- Signature present?

## Hook Design Pattern

```
HOOK: [Name]
TRIGGER: [When it runs]
CHECKS:
  - [What it validates]
  - [What it validates]
PASS: [What happens if OK]
FAIL: [What happens if not OK]
```

## Practice Tasks

1. Review hook designs in `validation-rules.md`
2. Design a pre-send quality hook
3. Define what triggers escalation
4. Create a feedback mechanism for failures
