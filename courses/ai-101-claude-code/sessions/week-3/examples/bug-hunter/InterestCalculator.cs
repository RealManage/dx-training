using System;

namespace RealManage.BugHunter
{
    /// <summary>
    /// Calculates compound interest for late HOA fees.
    /// Bug Report: "Interest calculations are wrong after 90 days"
    /// </summary>
    public class InterestCalculator
    {
        private const decimal MonthlyRate = 0.10m; // 10% per month
        
        /// <summary>
        /// Calculates compound interest on overdue balance.
        /// Formula: A = P(1 + r)^t
        /// </summary>
        public decimal CalculateCompoundInterest(decimal principal, DateTime dueDate, DateTime currentDate)
        {
            if (principal <= 0)
                return 0;
                
            var daysPastDue = (currentDate - dueDate).Days;
            
            // 30-day grace period
            if (daysPastDue <= 30)
                return principal;
            
            // Calculate months past due (after grace period)
            var monthsPastDue = (daysPastDue - 30) / 30;
            
            // The bug is hidden here - can you find it?
            // Hint: Users report wrong calculations after 90 days
            decimal total = principal;
            for (int i = 0; i < monthsPastDue; i++)
            {
                total = total * (1 + MonthlyRate);
            }
            
            return Math.Round(total, 2);
        }
        
        /// <summary>
        /// Calculates the interest portion only (total - principal)
        /// </summary>
        public decimal CalculateInterestAmount(decimal principal, DateTime dueDate, DateTime currentDate)
        {
            var total = CalculateCompoundInterest(principal, dueDate, currentDate);
            return total - principal;
        }
        
        /// <summary>
        /// Gets a breakdown of interest by month
        /// </summary>
        public InterestBreakdown GetInterestBreakdown(decimal principal, DateTime dueDate, DateTime currentDate)
        {
            var breakdown = new InterestBreakdown
            {
                Principal = principal,
                DueDate = dueDate,
                CalculationDate = currentDate
            };
            
            var daysPastDue = (currentDate - dueDate).Days;
            if (daysPastDue <= 30)
            {
                breakdown.TotalWithInterest = principal;
                breakdown.InterestAmount = 0;
                breakdown.MonthsOverdue = 0;
                return breakdown;
            }
            
            // Something subtle is wrong here too
            var monthsPastDue = daysPastDue / 30;  
            breakdown.MonthsOverdue = monthsPastDue;
            
            decimal runningTotal = principal;
            for (int month = 1; month <= monthsPastDue; month++)
            {
                var monthlyInterest = runningTotal * MonthlyRate;
                breakdown.MonthlyCharges.Add(new MonthlyCharge
                {
                    Month = month,
                    InterestCharge = Math.Round(monthlyInterest, 2),
                    RunningTotal = Math.Round(runningTotal + monthlyInterest, 2)
                });
                runningTotal = runningTotal * (1 + MonthlyRate);
            }
            
            breakdown.TotalWithInterest = Math.Round(runningTotal, 2);
            breakdown.InterestAmount = breakdown.TotalWithInterest - principal;
            
            return breakdown;
        }
    }
    
    public class InterestBreakdown
    {
        public decimal Principal { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CalculationDate { get; set; }
        public int MonthsOverdue { get; set; }
        public decimal InterestAmount { get; set; }
        public decimal TotalWithInterest { get; set; }
        public List<MonthlyCharge> MonthlyCharges { get; set; } = new List<MonthlyCharge>();
    }
    
    public class MonthlyCharge
    {
        public int Month { get; set; }
        public decimal InterestCharge { get; set; }
        public decimal RunningTotal { get; set; }
    }
}