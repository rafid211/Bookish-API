using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Filters;

namespace Project_API.Attributes
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            base.OnAuthorization(actionContext);
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            else
            {
                string encodedStr = actionContext.Request.Headers.Authorization.Parameter;
                string decodedStr = Encoding.UTF8.GetString(Convert.FromBase64String(encodedStr));
                string[] splitedData = decodedStr.Split(new Char[] { ':' });
                //string[] splitedData = actionContext.Request.Headers.Authorization.Parameter.Split(new Char[] { ':' });
                string username = splitedData[0];
                string password = splitedData[1];
                if (username == "admin" && password == "123")
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username), null);
                    //actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }

            }

        }
    }
}