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
    public class LoginController : ControllerBase
    {
        private readonly DatabaseContext context;
        private IDataProtector protector;
        
        public LoginController(DatabaseContext _context, IDataProtectionProvider provider)
        {
            context = _context;
            protector = provider.CreateProtector("crm.protect");
        }

        [HttpPost("auth")]
        public ActionResult<Customer> Login([FromForm] LoginRequest req)
        {
            var item = context.customers.Where(w => w.Username == req.Username ).FirstOrDefault();

            if(item == null) return BadRequest();

            string password = protector.Unprotect(item.Password);

            if(req.Password == password) {
                string token = Guid.NewGuid().ToString();
                
                item.Token = token;

                context.SaveChanges();

                return Ok(item);
            }
            else return BadRequest();
        }

        [HttpPost("change_password")]
        public ActionResult<Customer> ChangePassword([FromForm] string username, [FromForm] string email, [FromForm] string newPassword, [FromForm] string token )
        {
            var item = context.customers.Where( w => w.Username == username && w.Email == email && w.Token == token ).FirstOrDefault();

            if( item == null) return BadRequest();
            else {
                item.Password = protector.Protect(newPassword);

                context.SaveChanges();

                return Ok(item);
            }
        }
    }
}