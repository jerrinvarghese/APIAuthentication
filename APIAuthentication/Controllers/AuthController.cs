using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APIAuthentication.Controllers
{
    public class AuthController : ApiController
    {
        [HttpGet]
        [Route("AuthToken")]
         public HttpResponseMessage Authenticate(string username,string password)
        {
            return GetAuthToken(username, password);
        }

        public HttpResponseMessage GetAuthToken(string Username, string Password)
        {
            var _tokenservices = new BusinessServices();
            var token = _tokenservices.GenerateToken(Username, Password);
            var response = Request.CreateResponse(HttpStatusCode.OK, "Authorized");
            response.Headers.Add("Token", token.Token);
            response.Headers.Add("TokenExpiry", DateTime.Now.AddHours(2).ToString());
            response.Headers.Add("Access-Control-Expose-Headers", "Token,TokenExpiry");
            return response;
        }
        [Route("GetUserDetails")]
        [AuthorizationRequired]
        public string GetUserDetails(string UserName)
        {
            return "UnAuthorized";
        }
    }
}
