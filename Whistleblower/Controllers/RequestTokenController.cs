using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Whistleblower.Auth;

namespace Whistleblower.Controllers
{
    public class RequestTokenController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get(string username, string password)
        {
            if (CheckUser(username, password))
            {
                return Request.CreateResponse(HttpStatusCode.OK,
             JwtAuthManager.GenerateJWTToken(username));
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized,
             "Invalid Request");
            }
        }
        public bool CheckUser(string username, string password)
        {
            if (username == "codeadda" && password == "abc123")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
