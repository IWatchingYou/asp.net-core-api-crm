using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace crm.Models
{
    public class Payment
    {
        [Key]
        public long Id {get; set;}
        public long CustomerId { get; set;}
        public string Key {get; set;}
        public int MaxUsed {get; set;}
        public double price {get; set;}
        public DateTime Expired_At {get; set;}
        public DateTime Created_At {get; set;}

        public ICollection<TokenAccess> TokenAccess {get; set;}
    }
}