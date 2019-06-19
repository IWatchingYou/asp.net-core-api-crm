using Microsoft.AspNetCore.Mvc;
using crm.Models;
using Microsoft.AspNetCore.DataProtection;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace crm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly DatabaseContext context;
        private IDataProtector _protector;

        public CustomerController(DatabaseContext context, IDataProtectionProvider provider)
        {
            this.context = context;
            _protector = provider.CreateProtector("crm.protect");
        }

        [HttpPost]
        public ActionResult<Customer> CreateCustomer(CustomerRequest req)
        {
            var item = context.customers.Where(w => w.Username == req.Username).FirstOrDefault();

            if (item == null)
            {
                Customer cus = new Customer();
                cus.First_Name = req.First_Name;
                cus.Last_Name = req.Last_Name;
                cus.Email = req.Email;
                cus.Username = req.Username;
                cus.Password = _protector.Protect(req.Password);
                cus.Created_At = DateTime.Now;
                cus.Active = true;

                context.customers.Add(cus);
                context.SaveChanges();

                return Ok(cus);
            }
            else
            {
                return BadRequest("user are already exists.");
            }
        }

        [HttpGet]
        public ActionResult<ICollection<Customer>> CustomerList()
        {
            return context.customers.Include(c => c.payments).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Customer> Customer(long id)
        {
            return context.customers.Include(c => c.payments).Where(w => w.Id == id).FirstOrDefault();
        }

        [HttpPut("update/{id}")]
        public ActionResult<Customer> UpdateCustomer(long id, CustomerRequest req)
        {
            var item = context.customers.Find(id);

            item.First_Name = req.First_Name;
            item.Last_Name = req.Last_Name;
            item.Email = req.Email;
            item.Username = req.Username;

            context.SaveChanges();

            return Ok(item);
        }

        [HttpPut("active/{id}")]
        public ActionResult<Customer> ActiveCustomer(long id, bool active)
        {
            var item = context.customers.Find(id);

            item.Active = active;

            context.SaveChanges();

            return Ok(item);
        }
    }
}