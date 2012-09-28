using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace PersonaMVC4Example
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            /* added for the the WebAPI for Persona. 
               Note the added {action} url parameter 
               that makes this RPC rather than REST */
            config.Routes.MapHttpRoute(
               name: "DefaultApiWithAction",
               routeTemplate: "api/{controller}/{action}/{id}",
               defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
