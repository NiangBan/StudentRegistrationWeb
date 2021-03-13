using StudentRegistrationWeb.Extension;
using System;
using System.Web.Mvc;

using StudentRegistrationWeb.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;
using StudentRegistrationWeb.Helper;
using System.Configuration;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Web.Security;
using System.Net.Http;

namespace StudentRegistrationWeb.Controllers
{
    public class SessionValidationAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.Controller.ControllerContext.HttpContext != null && filterContext.Controller.ControllerContext.HttpContext.Session["UserName"] == null)
            {
                string redirectURL = "~/Home/error?errorCode=sessiontimeout";
                filterContext.Result = new RedirectResult(redirectURL);
            }
        }

    }
    public class BaseController : Controller
    {
        
        protected CryptoUtils Crypto { get; set; }

        public string CommonDynamicKey = "DynemicKey";
        public string CommonSessionID = "SessionID";
        public string CommonUserID = "UserID";


        public BaseController()
        {
            this.Crypto = new CryptoUtils();
        }

        protected override bool DisableAsyncSupport
        {
            get { return true; }
        }

        //protected LoginResponseModel CurrentUserSession
        //{
        //    get
        //    {
        //        if (Session["CurrentUserSession"] == null)
        //        {
        //            return null;
        //        }

        //        return (LoginResponseModel)Session["CurrentUserSession"];
        //    }
        //    set
        //    {
        //        Session["CurrentUserSession"] = value;
        //    }
        //}

        protected void FormsAutheticationSignOutAndSessionAbandon()
        {
            if (HttpContext.CurrentHandler != null)
            {
                string[] myCookies = HttpContext.Request.Cookies.AllKeys;
                foreach (string cookie in myCookies)
                {
                    var httpCookie = HttpContext.Response.Cookies[cookie];
                    if (httpCookie != null) httpCookie.Expires = DateTime.Now.AddDays(-1);
                }

                FormsAuthentication.SignOut();
                System.Web.HttpContext.Current.Session.Abandon();
            }
        }

        public ApiRequestModel APIRequest(string jsonString, object SessionID, object UserID,bool status)
        {
            ApiRequestModel model = new ApiRequestModel();
            string hardCodeKey = CommonUtils.HardCodeKeyForAES();
            string hardCodeIV = CommonUtils.HardCodeIVForAES();
            
            model.JsonStringRequest = jsonString;
            model.SessionID = status? RijndaelCrypt.DecryptAES(SessionID.ToString(), hardCodeKey, hardCodeIV) : string.Empty;
            model.UserId = status ? RijndaelCrypt.DecryptAES(UserID.ToString(), hardCodeKey, hardCodeIV) : string.Empty;
            return model;
        }

        protected async Task<ApiResponseModel> PostAPI(ApiRequestModel requestModel, string path)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            var resModel = new ApiResponseModel();

            try
            {
                var hardCodeKey = CommonUtils.HardCodeKeyForAES();
                var hardCodeIV = CommonUtils.HardCodeIVForAES();
                string dynamicKey = Session[CommonDynamicKey].ToString();

                if (string.IsNullOrEmpty(requestModel.UserId)  && string.IsNullOrEmpty(requestModel.SessionID) && string.IsNullOrEmpty(dynamicKey))
                {
                    requestModel.JsonStringRequest = this.Crypto.Encrypt(requestModel.JsonStringRequest, CryptoUtils.EncryptionKey, CryptoUtils.EncryptionIV);
                }
                else
                {
                    requestModel.JsonStringRequest = RijndaelCrypt.EncryptAES(requestModel.JsonStringRequest, dynamicKey, hardCodeIV);
                    requestModel.SessionID = RijndaelCrypt.EncryptAES(requestModel.SessionID, hardCodeKey, hardCodeIV);
                    requestModel.UserId = RijndaelCrypt.EncryptAES(requestModel.UserId, hardCodeKey, hardCodeIV);
                }
                
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["domainapi"]);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = new TimeSpan(1, 0, 0);
                    var postResponse = client.PostAsJsonAsync(path, requestModel).Result;

                    if (postResponse.IsSuccessStatusCode)
                    {

                        var data = await postResponse.Content.ReadAsStringAsync();
                        var responseModel = JsonConvert.DeserializeObject<ApiResponseModel>(data);
                        if (string.IsNullOrEmpty(requestModel.UserId) && string.IsNullOrEmpty(requestModel.SessionID))
                        {
                            responseModel.JsonStringResponse = this.Crypto.Decrypt(responseModel.JsonStringResponse, CryptoUtils.EncryptionKey, CryptoUtils.EncryptionIV);
                        }
                        else
                        {
                            responseModel.JsonStringResponse = RijndaelCrypt.DecryptAES(responseModel.JsonStringResponse, dynamicKey, hardCodeIV);
                        }
                        //if (responseModel.RespCode == "000")
                        //{
                        //    //Log.Info(path + " API call successful.");

                            
                        //    return responseModel;
                        //}
                        return responseModel;
                    }
                    else
                    {
                        
                        //resModel.IsSystemError = true;
                        //resModel.SystemErrorURL = "~/Home/Error";
                        //resModel.RespCode = "014";
                        //resModel.RespDescription = "System Error";
                        return resModel;
                    }
                }
            }
            catch (Exception ex)
            {/*
                resModel.IsSystemError = true;
                resModel.SystemErrorURL = "~/Home/Error";*/
                resModel.RespCode = "014";
                resModel.RespDescription = ex.Message;
                return resModel;
            }
        }

        protected async Task<ApiResponseModel> PostDataAPI(ApiRequestModel requestModel, string path)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            var resModel = new ApiResponseModel();

            try
            {
                #region TestData
                Session[CommonDynamicKey] = "aaaaa7";
                var dynamicKey = Session[CommonDynamicKey].ToString();
                #endregion
                var hardCodeKey = CommonUtils.HardCodeKeyForAES();
                var hardCodeIV = CommonUtils.HardCodeIVForAES();
                //var dynamicKey = Session[CommonDynamicKey].ToString();
                var commonKey = String.Empty;
                if(!String.IsNullOrEmpty(dynamicKey))
                {
                    commonKey = dynamicKey;
                }
                else
                {
                    commonKey = hardCodeKey;
                }
                requestModel.JsonStringRequest = RijndaelCrypt.EncryptAES(requestModel.JsonStringRequest, commonKey, hardCodeIV);
                requestModel.UserId = RijndaelCrypt.EncryptAES(requestModel.UserId, hardCodeKey, hardCodeIV);
                requestModel.SessionID = RijndaelCrypt.EncryptAES(requestModel.SessionID, hardCodeKey, hardCodeIV);
                
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["domainapi"]);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = new TimeSpan(1, 0, 0);

                    var postResponse = client.PostAsJsonAsync(path, requestModel).Result;

                    if (postResponse.IsSuccessStatusCode)
                    {

                        var data = await postResponse.Content.ReadAsStringAsync();
                        var responseModel = JsonConvert.DeserializeObject<ApiResponseModel>(data);

                        if (responseModel.RespCode == "000")
                        {
                            responseModel.JsonStringResponse = RijndaelCrypt.DecryptAES(responseModel.JsonStringResponse, commonKey, hardCodeIV);
                            return responseModel;
                        }
                        return responseModel;
                    }
                    else
                    {
                        return resModel;
                    }
                }
            }
            catch (Exception ex)
            {
                resModel.RespCode = "014";
                resModel.RespDescription = ex.Message;
                return resModel;
            }
        }


    }
}