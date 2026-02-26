# Description: Process violation through escalation workflow

## Arguments: property_id violation_type days_since_report

Process violation for property $0, type $1, reported $2 days ago.

## Escalation Decision Tree

### Days 0-30: Warning Phase

- Issue Warning Notice (no fine)
- Include CCR reference for $1
- Allow 30 days for compliance
- Next check: Day 31

### Days 31-60: First Notice Phase

- Issue First Notice
- Apply $50 base fine
- Reference previous warning
- Allow 30 days for compliance
- Next check: Day 61

### Days 61-90: Second Notice Phase

- Issue Second Notice
- Apply $100 base fine
- Calculate compound interest: $100 * (1.10 ^ months_over_60)
- Final warning before board escalation
- Next check: Day 91

### Days 90+: Board Review Phase

- Escalate to Board of Directors
- Require agenda item at next meeting
- Continue compound interest accrual
- Present options: legal action, lien, dismissal

## Required Outputs

1. **Notice Letter**
   - Property address
   - Owner name (lookup required)
   - Violation type: $1
   - CCR section reference
   - Current status and fine
   - Compliance deadline
   - Appeal instructions

2. **Fine Calculation**
   - Days since report: $2
   - Base fine amount
   - Interest calculation (if applicable)
   - Total amount due
   - Payment instructions

3. **Audit Trail Entry**
   - Timestamp
   - Violation ID
   - Property: $0
   - Action taken
   - User ID
   - SOC 2 compliance verified

4. **Next Actions**
   - Next review date
   - Escalation path if unresolved
   - Board meeting date (if 90+ days)
