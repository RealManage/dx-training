using System;
using System.Collections.Generic;
using System.Linq;

namespace RealManage.CodeReviewPro
{
    public class PaymentService
    {
        private List<Payment> payments = new List<Payment>();
        
        // BUG: No validation on amount
        // BUG: No null checks
        // BUG: Not thread-safe
        // PERFORMANCE: Loading all payments into memory
        public void ProcessPayment(string residentId, decimal amount, string paymentMethod)
        {
            var payment = new Payment();
            payment.ResidentId = residentId;
            payment.Amount = amount;
            payment.PaymentMethod = paymentMethod;
            payment.ProcessedDate = DateTime.Now;
            
            // BUG: No error handling
            payments.Add(payment);
            
            // PERFORMANCE: N+1 query problem
            var allPayments = GetAllPayments();
            foreach (var p in allPayments)
            {
                if (p.ResidentId == residentId)
                {
                    // Do something
                }
            }
        }
        
        // BUG: SQL Injection vulnerability
        public List<Payment> GetPaymentsByResident(string residentId)
        {
            string query = "SELECT * FROM Payments WHERE ResidentId = '" + residentId + "'";
            // Imagine this executes the query
            return payments.Where(p => p.ResidentId == residentId).ToList();
        }
        
        // PERFORMANCE: Loading entire table
        public List<Payment> GetAllPayments()
        {
            // Imagine this loads from database
            return payments;
        }
        
        // BUG: Integer overflow potential
        public int CalculateTotalPayments()
        {
            int total = 0;
            foreach (var payment in payments)
            {
                total += (int)payment.Amount;  // BUG: Losing precision
            }
            return total;
        }
        
        // BUG: No authorization check
        public void RefundPayment(int paymentId)
        {
            var payment = payments.FirstOrDefault(p => p.Id == paymentId);
            payment.Status = "Refunded";  // BUG: Possible null reference
        }
    }
    
    public class Payment
    {
        public int Id { get; set; }
        public string ResidentId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime ProcessedDate { get; set; }
        public string Status { get; set; }
    }
}