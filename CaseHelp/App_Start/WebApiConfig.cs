using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CaseHelp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{area}/{controller}/{action}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
