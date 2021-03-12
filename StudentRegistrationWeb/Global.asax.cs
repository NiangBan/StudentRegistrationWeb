using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace StudentRegistrationWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Application_BeginRequest()
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
        }
        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            try
            {
                var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (authCookie != null)
                {
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                    if (authTicket != null && !authTicket.Expired)
                    {
                        var roles = authTicket.UserData.Split(',');
                        HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(new FormsIdentity(authTicket), roles);
                    }
                }
            }
            catch (Exception ex)
            {
                if (HttpContext.Current != null)
                {
                    string[] myCookies = HttpContext.Current.Request.Cookies.AllKeys;
                    foreach (string cookie in myCookies)
                    {
                        var httpCookie = HttpContext.Current.Response.Cookies[cookie];
                        if (httpCookie != null) httpCookie.Expires = DateTime.Now.AddDays(-1);
                    }
                }

                //Log.Error("PostAuthenticateRequest Error Handler :" + ex.Message);
                //Log.Error(ex);
                HttpContext.Current.Response.Redirect("~/Home/Error", true);
            }
        }
    }
}
