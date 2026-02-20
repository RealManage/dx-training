# Description: Calculate late fees with compound interest
# Arguments: principal_amount months_overdue

Calculate late fees for $0 principal over $1 months overdue.

RealManage Late Fee Policy:
- 30-day grace period (first month has no interest)
- 10% monthly compound interest after grace period
- Formula: principal * (1.10 ^ (months - 1)) - principal
- Cap at 3x original amount per state regulations

Output a detailed breakdown showing:
1. Original principal amount
2. Number of months overdue
3. Grace period status
4. Month-by-month interest accrual
5. Total amount due
6. Cap check (if applicable)
7. Payment plan options (3, 6, 12 month installments)

Example calculation for $500 over 4 months:
- Month 1: $500 (grace period)
- Month 2: $500 * 1.10 = $550
- Month 3: $550 * 1.10 = $605
- Month 4: $605 * 1.10 = $665.50

Generate audit trail entry for this calculation.
