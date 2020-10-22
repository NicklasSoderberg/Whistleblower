using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiJWT.Controllers
{
    public class AccountController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage ValidLogin(string userName, string userPassword)
        {
            if (userName == "admin" && userPassword == "admin")
            {
                return Request.CreateResponse(HttpStatusCode.OK, TokenManager.GenerateToken(userName));
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadGateway, "Username and password is invalid");
            }
        }
    }
}
