using System;
using System.ComponentModel.DataAnnotations;

namespace crm.Models
{
    public class CustomerRequest
    {
        [Key]
        public string First_Name {get; set;}
        public string Last_Name {get; set;}
        public string Email {get; set;}
        public string Username {get; set;}
        public string Password {get; set;}
    }
}