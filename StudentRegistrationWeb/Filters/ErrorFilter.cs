using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace StudentRegistrationWeb.Filters
{
    public class ErrorFilter : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            filterContext.Controller.TempData.Add("Exception", filterContext.Exception);
            filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Home", action = "Error" }));
            filterContext.ExceptionHandled = true;
        }

    }
}