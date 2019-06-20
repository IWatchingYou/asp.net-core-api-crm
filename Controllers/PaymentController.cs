using Microsoft.AspNetCore.Mvc;
using crm.Models;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace crm.Controllers
{
    [Route("/api/[Controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly DatabaseContext context;

        public PaymentController(DatabaseContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public ActionResult<Payment> CreatePayment(PaymentRequest req)
        {
            Payment pay = new Payment();
            pay.CustomerId = req.CustomerId;
            pay.Key = Guid.NewGuid().ToString();
            pay.MaxUsed = req.MaxUsed;
            pay.Expired_At = req.Expired_At;
            pay.Created_At = DateTime.Now;
            pay.price = req.price;

            context.Add(pay);
            context.SaveChanges();

            return Ok(pay);
        } 

        [HttpGet]
        public ActionResult<ICollection<Payment>> PaymentList()
        {
            return context.payments.Include(p => p.TokenAccess).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Payment> PaymentSolo(long id)
        {
            return context.payments.Include(p => p.TokenAccess).Where(w => w.Id == id).FirstOrDefault();
        }

        [HttpPut("update/{id}")]
        public ActionResult<Payment> UpdatePayment(long id, PaymentRequest req)
        {
            var item = context.payments.Find(id);

            item.CustomerId = req.CustomerId;
            item.MaxUsed = req.MaxUsed;
            item.Expired_At = req.Expired_At;
            item.Created_At = DateTime.Now;
            item.price = req.price;

            context.SaveChanges();

            return Ok(item);
        }

        [HttpPut("refreshKey/{id}")]
        public ActionResult<Payment> RefreshKey(long id, [FromForm] string oldKey, [FromForm] DateTime delayTo)
        {
            var item = context.payments.Where(w => w.Id == id && w.Key == oldKey).FirstOrDefault();
            
            item.Expired_At = delayTo;

            context.SaveChanges();

            return Ok(item);
        }
    }
}