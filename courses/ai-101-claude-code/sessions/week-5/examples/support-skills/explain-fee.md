# /explain-fee

Generate a clear explanation of HOA fee calculations.

## Arguments

- `$0` - Type of fee (late, processing, legal, assessment)
- `$1` - Original amount or current balance (optional)
- `$2` - Months overdue (optional, for late fees)

## Instructions

You are explaining HOA fees to a resident. Be clear, accurate, and non-defensive.

### Fee Calculations

**Late Fees:**

- 10% of outstanding balance, compounded monthly
- Applied after 30-day grace period
- Example: $100 dues -> $110 after month 1 -> $121 after month 2

**Processing Fees:**

- Returned check fee: $35
- Payment plan setup: $25
- Account research: $15/hour

**Legal Fees:**

- Applied at 90-day escalation
- Varies by situation
- Typically starts at $200+
- Resident responsible per CC&Rs

**Special Assessments:**

- One-time fees for capital improvements
- Approved by board vote
- Due date specified in notice
- Subject to same late fee policy

### Response Format

1. State the fee type clearly
2. Show the calculation (with math if applicable)
3. Explain WHY this fee exists (policy rationale)
4. Offer options (payment plan, dispute process)

### Tone

- Matter-of-fact, not apologetic
- Accurate and specific
- Helpful, not defensive
- Offer next steps

## Output

A clear, accurate fee explanation suitable for customer communication.

## Example Usage

```text
/explain-fee late 450 2
```

> Explains how $450 became ~$544 over 2 months

```text
/explain-fee processing
```

> Lists all processing fees and when they apply

```text
/explain-fee legal
```

> Explains legal fee policy and when it applies
