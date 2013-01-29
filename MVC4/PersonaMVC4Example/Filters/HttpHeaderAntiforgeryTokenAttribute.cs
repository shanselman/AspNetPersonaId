using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http; //HttpRequestMessageExtensions
using System.Net.Http.Headers;
using System.Web.Helpers;
using System.Web.Http;
using System.Net;
using System.Web.Mvc;

namespace PersonaMVC4Example {
    public class HttpHeaderAntiForgeryTokenAttribute : System.Web.Http.Filters.AuthorizationFilterAttribute {
        static string _tokenKey = AntiForgeryConfig.CookieName;

        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext) {
            if (actionContext == null) {
                throw new ArgumentNullException("actionContext");
            }

            try {

                CookieHeaderValue cookie = actionContext.Request.Headers.GetCookies(_tokenKey).FirstOrDefault();
                var cookieToken = cookie != null ? cookie[_tokenKey].Value : null;

                string headerToken = actionContext.Request.Headers.GetValues(_tokenKey).First();
                System.Web.Helpers.AntiForgery.Validate(cookieToken, headerToken); // Will throw HttpAntiForgeryException
            }
            catch {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest) {
                    Content = new StringContent("Invalid or Missing AntiForgery Token")
                };
            }
            return;
        }
    }
}