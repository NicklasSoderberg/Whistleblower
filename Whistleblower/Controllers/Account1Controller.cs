using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Whistleblower.Encryption;

namespace Whistleblower.Controllers
{
    public class Account1Controller : Controller
    {
        [HttpGet]
        public ActionResult ValidLogin(string userName, string Password)
        {
            if (userName == "admin" && Password == "admin")
            {
                //return Request.CreateResponse(HttpStatusCode.OK, value: TokenManager.GenerateToken(userName));
                return new HttpStatusCodeResult(HttpStatusCode.OK, TokenManager.GenerateToken(userName));
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Wrong username or password");
            }
        }    

            [HttpGet]
            [CustomAuthenticationFilter]
            public ActionResult GetEmployee()
            {
                //return Request.CreateResponse(HttpStatusCode.OK, "Succefully Valid");
                return new HttpStatusCodeResult(HttpStatusCode.OK, "Succefully Valid");
            }
        }
}