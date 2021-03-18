using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace StudentRegistrationWeb.Filters
{
    public class AuthorizationFilter : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            bool skipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
            || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true);
            if (!skipAuthorization)
            {
                HttpContextBase httpContext = filterContext.HttpContext;
                var s = httpContext.Session["UserNameForSalted"].ToString();
                if (httpContext.Session["UserNameForSalted"] == null)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Error", errorCode = "sessiontimeout" }));
                }
            }
        }
    }
}