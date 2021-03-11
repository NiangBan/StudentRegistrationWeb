using System;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace StudentRegistrationWeb.Extension
{
    public static class HtmlExtension
    {
        public static String GetEncryptLinkForRedirect(string actionName, string ControllerName)
        {
           return $@"~/{CommonUtils.Secure_Url_Prefix}/{HttpUtility.UrlEncode(new CryptoUtils().EncryptForExtension($"/{ControllerName}/{actionName}"), Encoding.UTF8)}";
        }

        public static String GetEncryptLink(string actionName, string ControllerName)
        {
            return $@"/{HttpUtility.UrlEncode(new CryptoUtils().EncryptForExtension($"/{ControllerName}/{actionName}"), Encoding.UTF8)}";
        }
        public static String GetEncryptLink(string actionName, string ControllerName, object routeValues)
        {
            string pathValues = string.Empty;
            if (routeValues != null)
            {
                RouteValueDictionary d = new RouteValueDictionary(routeValues);
                for (int i = 0; i < d.Keys.Count; i++)
                {
                    pathValues += "/";
                    pathValues += d.Keys.ElementAt(i) + "=" + d.Values.ElementAt(i);
                }
            }
            return $@"/{HttpUtility.UrlEncode(new CryptoUtils().EncryptForExtension($"/{ControllerName}/{actionName + pathValues}"), Encoding.UTF8)}";
        }

        public static String GetEncryptLink_withTab(string actionName, string ControllerName, string Tabparameter)
        {
            var tetlink = $"/{ControllerName}/{actionName}?tabName={Tabparameter}";
            var link = $@"{CommonUtils.Root_Url_Prefix}/{CommonUtils.Secure_Url_Prefix}/{HttpUtility.UrlEncode(new CryptoUtils().EncryptForExtension(tetlink), Encoding.UTF8)}";
            return link;
        }

        public static String GetEncryptLinkForDetail(string actionName, string ControllerName, object routeValues)
        {
            string pathValues = string.Empty;
            if (routeValues != null)
            {
                RouteValueDictionary d = new RouteValueDictionary(routeValues);
                for (int i = 0; i < d.Keys.Count; i++)
                {
                    pathValues += "/";
                    pathValues += "=" + d.Values.ElementAt(i);
                }
            }
            return $@"{CommonUtils.Root_Url_Prefix}/{CommonUtils.Secure_Url_Prefix}/{HttpUtility.UrlEncode(new CryptoUtils().EncryptForExtension($"/{ControllerName}/{actionName + pathValues}"), Encoding.UTF8)}";
        }
        public static MvcHtmlString EncodedActionLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues, object htmlAttributes, string iconClass)
        {
            string pathValues = string.Empty;
            string htmlAttributesString = string.Empty;
            if (routeValues != null)
            {
                RouteValueDictionary d = new RouteValueDictionary(routeValues);
                for (int i = 0; i < d.Keys.Count; i++)
                {
                    pathValues += "/";
                    pathValues += d.Keys.ElementAt(i) + "=" + d.Values.ElementAt(i);
                }
            }

            if (htmlAttributes != null)
            {
                RouteValueDictionary d = new RouteValueDictionary(htmlAttributes);
                for (int i = 0; i < d.Keys.Count; i++)
                {
                    htmlAttributesString += " " + d.Keys.ElementAt(i) + "=" + d.Values.ElementAt(i);
                }
            }

            //What is Entity Framework??
            String url = "/" + controllerName + "/" + actionName + pathValues;
            string ancor = $@"
            <a {(htmlAttributesString != string.Empty ? htmlAttributesString : string.Empty) }
            href='{CommonUtils.Root_Url_Prefix}/{CommonUtils.Secure_Url_Prefix}/{HttpUtility.UrlEncode(new CryptoUtils().Encrypt(url), Encoding.UTF8)}'>
            {(iconClass == null ? string.Empty : iconClass)}{linkText}</a>
            ";

            
            return new MvcHtmlString(ancor.ToString());
        }
    }
}