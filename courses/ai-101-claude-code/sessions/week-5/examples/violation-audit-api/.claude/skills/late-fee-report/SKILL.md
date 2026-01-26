---
name: late-fee-report
description: Generate late fee report with compound interest calculations
argument-hint: <property_id>
context: fork
allowed-tools: Read, Grep
---

Generate a late fee report for property $ARGUMENTS.

Use ./fee-schedule.txt for the fee structure.

Include:
1. Original balance
2. Months overdue
3. Interest calculation (10% monthly compound)
4. Total amount due
5. Payment plan options (3, 6, 12 months)

Format as markdown suitable for homeowner communication.
