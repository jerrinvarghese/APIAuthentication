﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace APIAuthentication
{
    public class AuthorizationRequiredAttribute:ActionFilterAttribute
    {
        private const string Token = "Token";
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            var provider = new BusinessServices();
            if(filterContext.Request.Headers.Contains(Token))
            {
                var tokenValue = filterContext.Request.Headers.GetValues(Token).First();
                if(provider!=null&&!provider.ValidateToken(tokenValue))
                {
                    var responseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized) { ReasonPhrase = "Invalid request" };
                    filterContext.Response = responseMessage;

                }
                else
                {
                    filterContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}