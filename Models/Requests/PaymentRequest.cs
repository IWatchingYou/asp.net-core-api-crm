using System;

namespace crm.Models
{
    public class PaymentRequest
    {
        public long CustomerId { get; set;}
        public int MaxUsed {get; set;}
        public double price {get; set;}
        public DateTime Expired_At {get; set;}
    }
}