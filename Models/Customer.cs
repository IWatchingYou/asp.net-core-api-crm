using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace crm.Models
{
    public class Customer
    {
        [Key]
        public long Id {get; set;}
        public string First_Name {get; set;}
        public string Last_Name {get; set;}
        public string Email {get; set;}
        public string Username {get; set;}
        public string Password {get; set;}
        public string Token {get; set;}
        public DateTime Created_At {get; set;}

        public bool Active {get; set;}

        public ICollection<Payment> payments {get; set;}
    }
}