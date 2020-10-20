using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Whistleblower.Encryption;

namespace Whistleblower.Controllers
{
    public class AccountController : Controller
    {
        //[HttpGet]
        //public HttpResponseMessage ValidLogin(string userName, string Password)
        //{
        //    if (userName == "admin" && Password == "admin")
        //    {
        //        //return Request.CreateResponse(HttpStatusCode.OK, value: TokenManager.GenerateToken(userName));
        //        return TokenManager.GenerateToken(userName);
        //    else
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadGateway, message: "User name and password is invalid");
        //    }
        //}

        //[HttpGet]
        //[CustomAuthenticationFilter]
        //public HttpResponseMessage GetEmployee()
        //{
        //    return Request.CreateResponse(HttpStatusCode.OK, "Succefully Valid");
        //}
    }
}
