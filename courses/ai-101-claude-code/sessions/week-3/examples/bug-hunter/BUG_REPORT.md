# BUG-1847: Interest Calculations Wrong After 90 Days

## Reporter
Susan Thompson (Willow Creek HOA Board President)

## Date Reported
Today

## Severity
HIGH - Affecting resident billing

## Description
Multiple residents are reporting that their late fee interest calculations appear to be incorrect when their balance is overdue by more than 90 days. The amounts charged seem to be less than expected.

## Steps to Reproduce
1. Take a resident with $1000 overdue balance
2. Set due date to 120 days ago
3. Calculate interest
4. The amount shown is less than manual calculation

## Expected Behavior
- 10% monthly compound interest after 30-day grace period
- After 120 days (90 days past grace), should have 3 months of compound interest
- $1000 * (1.10)^3 = $1,331.00

## Actual Behavior
- System shows $1,210.00 (appears to be simple interest?)
- Some residents show even less
- Inconsistent results

## Additional Information
- Problem only occurs after 90 days past due
- Calculations seem correct for 60 days or less
- Board member did manual calculation and confirmed discrepancy
- Affects approximately 47 residents currently

## Business Impact
- Under-collecting late fees
- Board questioning system accuracy
- Potential legal issues if we have to correct bills

## Test Data
Example residents with issues:
- RES-0042: $1000 due 91 days ago, showing $1,210 (should be $1,331)
- RES-0156: $500 due 95 days ago, showing $605 (should be $665.50)  
- RES-0289: $2000 due 125 days ago, showing $2,662 (should be $2,928.20)

## Manual Calculation Example
For RES-0156 ($500 due 95 days ago):
- Days overdue: 95
- Grace period: 30 days  
- Days past grace: 65 days = 2.17 months (should round UP to 3)
- System calculates: 65/30 = 2 months (integer division bug!)
- Correct: 3 months of interest
  - Month 1: $500 × 1.10 = $550
  - Month 2: $550 × 1.10 = $605
  - Month 3: $605 × 1.10 = $665.50
- **Expected:** $665.50
- **System shows:** $605 ❌ (only 2 months applied)