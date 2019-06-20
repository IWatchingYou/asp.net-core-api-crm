using System;
using System.ComponentModel.DataAnnotations;

namespace crm.Models
{
    public class TokenAccess
    {
        [Key]
        public long Id {get; set;}
        public long PaymentId {get; set;}
        public string SerialNumber {get; set;}
        public string Key {get; set;}
        public DateTime Created_At {get; set;}
    }
}