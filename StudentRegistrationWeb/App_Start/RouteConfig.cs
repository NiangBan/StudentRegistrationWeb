using StudentRegistrationWeb.Extension;
using StudentRegistrationWeb.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace StudentRegistrationWeb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                  name: CommonUtils.Secure_Url_Prefix,
                  url: "{urlValues}",
                   //defaults: new { controller = "Login", action = "Login", id = UrlParameter.Optional })
                    defaults: new { controller = "Login", action = "Login", id = UrlParameter.Optional })
                  .RouteHandler = new MyCustomRouteHandler();
            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Login", action = "Login", id = UrlParameter.Optional }
            //);



            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
