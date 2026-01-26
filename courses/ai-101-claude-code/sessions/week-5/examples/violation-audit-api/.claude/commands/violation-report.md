# Description: Generate HOA violation report for a property
# Arguments: property_id

Generate a comprehensive violation report for property $ARGUMENTS.

Include:
1. All open violations with dates and current status
2. Escalation status (30/60/90 day progression)
3. Fine calculations with compound interest (10% monthly after 60 days)
4. Recommended next actions based on escalation level
5. Communication history from audit trail

Format the output as markdown suitable for board review.

Use RealManage standard escalation policy:
- 0-30 days: Warning (no fine)
- 31-60 days: First Notice ($50 fine)
- 61-90 days: Second Notice ($100 + 10% monthly compound interest)
- 90+ days: Board Review required

Include SOC 2 compliant audit trail for all operations.
