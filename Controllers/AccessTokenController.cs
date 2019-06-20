using Microsoft.AspNetCore.Mvc;
using crm.Models;
using System.Management;
using System;
using System.Linq;
using System.Collections.Generic;

namespace crm.Controllers
{
    [Route("/api/[Controller]")]
    [ApiController]
    public class AccessTokenController : ControllerBase
    {
        private readonly DatabaseContext context;
        public AccessTokenController(DatabaseContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public ActionResult<TokenAccess> CreateTokenAccess([FromForm] string key)
        {
            var item = context.payments.Where(w => w.Key == key.ToString()).FirstOrDefault();
            var access = context.TokenAccesss.Where(w => w.Key == key.ToString());
            int count = access.Count();
            bool haveSerial = false;

            if(item.MaxUsed <= count)
            {
                return BadRequest("Maximum used key are "+item.MaxUsed);
            }
            else
            {
                // Get Serial Number
                ManagementClass mangnmt = new ManagementClass("Win32_LogicalDisk");
                ManagementObjectCollection mcol = mangnmt.GetInstances();
                string result = "";
                foreach (ManagementObject strt in mcol)
                {
                    result += Convert.ToString(strt["VolumeSerialNumber"]);
                }

                foreach (var a in access.ToList())
                {
                    if(a.SerialNumber == result){
                        haveSerial = true;
                        break;
                    }
                }
                
                if(haveSerial == true){
                    access.FirstOrDefault().Created_At = DateTime.Now;
                }
                else
                {
                    TokenAccess tok = new TokenAccess();
                    tok.PaymentId = item.Id;
                    tok.Key = key;
                    tok.SerialNumber = result;
                    tok.Created_At = DateTime.Now;
                    context.Add(tok);
                }

                context.SaveChanges();

                return Ok("Success TokenAccess" + result);
            }
        }

        [HttpGet]
        public ActionResult<ICollection<TokenAccess>> TokenAccessList()
        {
            return context.TokenAccesss.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<TokenAccess> Token(long id)
        {
            return context.TokenAccesss.Find(id);
        }
    }
}