using StudentRegistrationWeb.Extension;
using StudentRegistrationWeb.Models;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace StudentRegistrationWeb.Handler
{
    public class MyCustomRouteHandler : MvcRouteHandler
    {
        protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            string decryptedUrl = ExtractEncryptUrl(); // /Home/List/q=stp/a=wth
            if (string.IsNullOrEmpty(decryptedUrl))
            {
                // redirect to error page
            }

            string[] urlValues = decryptedUrl.Split(new string[] {"/"}, StringSplitOptions.RemoveEmptyEntries);

            requestContext.RouteData.Values["controller"] = urlValues[0];
            requestContext.RouteData.Values["action"] = urlValues[1];


            if (urlValues.Length > 2)
            {
                for (int i = 2; i < urlValues.Length; i++)
                {
                    string[] queryStringValues = urlValues[i].Split('=');
                    var Model = new QueryStringModel();
                    Model.key = queryStringValues[0];
                    Model.Value = queryStringValues[1];
                    requestContext.RouteData.Values.Add(Model.key, Model.Value);
                }
            }

            return base.GetHttpHandler(requestContext);
        }

        private string ExtractEncryptUrl()
        {
            Dictionary<string, object> decryptedParameters = new Dictionary<string, object>();
            string rawUrl = HttpContext.Current.Request.Url.AbsolutePath;
            if (!rawUrl.Contains(CommonUtils.Secure_Url_Prefix))
            {
                string encryptedUrl = rawUrl.Substring(1);
                return new CryptoUtils().DecryptForExtension(encryptedUrl);
            }

            return null;
        }
    }
}