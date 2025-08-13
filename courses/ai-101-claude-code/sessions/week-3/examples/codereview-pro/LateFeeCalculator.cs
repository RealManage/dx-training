using System;

namespace RealManage.CodeReviewPro
{
    // TYPO: Should be LateFeeCalculator (missing 'e')
    public class LateFeecalculator
    {
        // BUG: Interest rate should be 10% (0.10) not 0.01
        private const decimal InterestRate = 0.01m;
        
        // BUG: Wrong calculation formula
        // Should be: principal * (1 + rate)^months
        // Currently: principal + (rate * days)
        public decimal CalculateLateFee(decimal principal, int daysLate)
        {
            if (daysLate <= 30)
            {
                return 0; // Grace period
            }
            
            // BUG: Should use compound interest, not simple
            decimal fee = principal + (InterestRate * daysLate);
            
            // BUG: No maximum fee cap (legal requirement in many states)
            return fee;
        }
        
        // TYPO: Method name misspelled
        public decimal CaluculateMonthlyInterest(decimal balance)
        {
            // BUG: Dividing by 12 then multiplying by 12?
            return balance * (InterestRate / 12) * 12;
        }
        
        // BUG: No input validation
        public bool IsOverdue(DateTime dueDate)
        {
            // BUG: Should be >= 30, not > 30
            return (DateTime.Now - dueDate).Days > 30;
        }
        
        // CODE SMELL: Magic numbers everywhere
        public decimal GetPenaltyAmount(int monthsLate)
        {
            if (monthsLate == 1)
                return 25;  // Magic number
            else if (monthsLate == 2)
                return 50;  // Magic number
            else if (monthsLate >= 3)
                return 100; // Magic number
                
            return 0;
        }
        
        // BUG: Infinite loop potential
        public decimal CalculateCompoundInterest(decimal principal, int months)
        {
            decimal total = principal;
            int i = 0;
            while (i <= months)  // BUG: Should be < not <=
            {
                total = total * (1 + InterestRate);
                // BUG: Forgot to increment i - infinite loop!
            }
            return total;
        }
    }
}