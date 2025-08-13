using System;
using Microsoft.AspNetCore.Mvc;

namespace RealManage.CodeReviewPro
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private PaymentService paymentService = new PaymentService();
        
        // BUG: No input validation
        // SECURITY: No authentication/authorization
        // BUG: No error handling
        [HttpPost("process")]
        public IActionResult ProcessPayment([FromBody] PaymentRequest request)
        {
            // BUG: No null check on request
            paymentService.ProcessPayment(
                request.ResidentId,
                request.Amount,
                request.PaymentMethod
            );
            
            // BUG: Always returns success, even if processing failed
            return Ok("Payment processed");
        }
        
        // SECURITY: SQL Injection via residentId parameter
        // BUG: No authorization - anyone can see anyone's payments
        [HttpGet("resident/{residentId}")]
        public IActionResult GetResidentPayments(string residentId)
        {
            // BUG: No input sanitization
            var payments = paymentService.GetPaymentsByResident(residentId);
            
            // BUG: Returning sensitive data without filtering
            return Ok(payments);
        }
        
        // SECURITY: No admin check for refunds
        // BUG: No validation on paymentId
        [HttpPost("refund/{paymentId}")]
        public IActionResult RefundPayment(int paymentId)
        {
            try
            {
                paymentService.RefundPayment(paymentId);
                return Ok();
            }
            catch
            {
                // BUG: Swallowing all exceptions
                return Ok("Refund processed");
            }
        }
        
        // PERFORMANCE: Loading all payments for summary
        [HttpGet("summary")]
        public IActionResult GetPaymentSummary()
        {
            var allPayments = paymentService.GetAllPayments();
            
            // BUG: Doing calculations in controller instead of service
            decimal total = 0;
            foreach (var payment in allPayments)
            {
                total = total + payment.Amount;  // PERFORMANCE: Not using LINQ Sum()
            }
            
            // BUG: Returning unstructured data
            return Ok(new { total = total });
        }
        
        // BUG: GET request modifying state
        [HttpGet("apply-late-fees")]
        public IActionResult ApplyLateFees()
        {
            // This should be a POST!
            // Imagine this modifies database
            return Ok("Late fees applied");
        }
    }
    
    public class PaymentRequest
    {
        public string ResidentId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
    }
}