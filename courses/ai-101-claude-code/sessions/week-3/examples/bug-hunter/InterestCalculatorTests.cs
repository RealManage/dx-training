using System;
using Xunit;
namespace RealManage.BugHunter
{
    public class InterestCalculatorTests
    {
        private readonly InterestCalculator _calculator = new InterestCalculator();
        
        [Fact]
        public void Calculate_WithinGracePeriod_ReturnsOriginalAmount()
        {
            // Arrange
            var principal = 1000m;
            var dueDate = DateTime.Now.AddDays(-15);
            
            // Act
            var result = _calculator.CalculateCompoundInterest(principal, dueDate, DateTime.Now);
            
            // Assert
            Assert.Equal(principal, result);
        }
        
        [Fact]
        public void Calculate_60DaysOverdue_ReturnsCorrectAmount()
        {
            // Arrange
            var principal = 1000m;
            var dueDate = DateTime.Now.AddDays(-60);
            
            // Act
            var result = _calculator.CalculateCompoundInterest(principal, dueDate, DateTime.Now);
            
            // Assert
            // 60 days = 30 grace + 30 days = 1 month overdue
            // $1000 * 1.10 = $1100
            Assert.Equal(1100m, result);
        }
        
        // TODO: Add test for 90 days (this might fail!)
        // TODO: Add test for 95 days (this might fail!)  
        // TODO: Add test for 120 days (this might fail!)
        // TODO: Add test for 150 days (this might fail!)
        
        // Uncomment these to reproduce the bug:
        
        /*
        [Fact]
        public void Calculate_120DaysOverdue_ReturnsCorrectAmount()
        {
            // This test will likely FAIL and show the bug
            var principal = 1000m;
            var dueDate = DateTime.Now.AddDays(-120);
            
            var result = _calculator.CalculateCompoundInterest(principal, dueDate, DateTime.Now);
            
            // 120 days = 30 grace + 90 days = 3 months
            // $1000 * (1.10)^3 = $1331
            Assert.Equal(1331m, result);
        }
        */
    }
}