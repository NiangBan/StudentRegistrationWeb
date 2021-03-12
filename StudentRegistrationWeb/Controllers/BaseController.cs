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

        protected string EncrypRequestObject(ApiRequestModel requestModel)
        {
            return this.Crypto.Encrypt(JsonConvert.SerializeObject(requestModel), CryptoUtils.EncryptionKey, CryptoUtils.EncryptionIV);
        }
        protected string EncryptUserRegisterRequestObject(AccountCreateRequestModel requestModel)
        {
            return this.Crypto.Encrypt(JsonConvert.SerializeObject(requestModel), CryptoUtils.EncryptionKey, CryptoUtils.EncryptionIV);
        }
        protected AccountCreateResponseModel DecryptUserRegisterResponseObject(string encryptString)
        {
            return JsonConvert.DeserializeObject<AccountCreateResponseModel>(this.Crypto.Decrypt(encryptString, CryptoUtils.EncryptionKey, CryptoUtils.EncryptionIV));
        }

        protected string EncryptOtherRequestObject(object obj, string dynamicKey, string dynamicIV)
        {
            return this.Crypto.Encrypt(JsonConvert.SerializeObject(obj), dynamicKey, dynamicIV);
        }

        public ApiRequestModel APIRequest(string jsonString, object SessionID, object UserID,bool status)
        {
            ApiRequestModel model = new ApiRequestModel();
            model.JsonStringRequest = jsonString;
            model.SessionID = status? SessionID.ToString(): string.Empty;
            model.UserId = status ? UserID.ToString(): string.Empty;
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
                requestModel.UserId = RijndaelCrypt.EncryptAES(requestModel.UserId, hardCodeKey, hardCodeIV);
                // path = "API/IB/IB_Profile";
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["domainapi"]);
                    // client.BaseAddress = new Uri("http://localhost:8312/");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = new TimeSpan(1, 0, 0);

                    var postResponse = client.PostAsJsonAsync(path, requestModel).Result;

                    if (postResponse.IsSuccessStatusCode)
                    {

                        var data = await postResponse.Content.ReadAsStringAsync();
                        var responseModel = JsonConvert.DeserializeObject<ApiResponseModel>(data);


                        //Log.Info("Request:" + JsonConvert.SerializeObject(responseModel));

                        if (responseModel.RespCode == "000")
                        {
                            //Log.Info(path + " API call successful.");
                            return responseModel;
                        }

                        //to check response code for custom error message
                        //switch (responseModel.RespCode)
                        //{
                        //    // if invalid key
                        //    case "031":
                        //        responseModel.IsSystemError = true;
                        //        responseModel.SystemErrorURL = "~/Home/Error?errorCode=invalidkey";
                        //        //HttpContext.Current.Response.Redirect("/Home/Error?errorCode=invalidkey", true);
                        //        break;

                        //    // if session timeout
                        //    case "005":
                        //        responseModel.IsSystemError = true;
                        //        responseModel.SystemErrorURL = "~/Home/Error?errorCode=sessiontimeout";
                        //        //HttpContext.Current.Response.Redirect("/Home/Error?errorCode=sessiontimeout", true);
                        //        break;

                        //    // if account is locked
                        //    case "M113":
                        //        responseModel.IsSystemError = true;
                        //        responseModel.SystemErrorURL = "~/Home/Error?errorCode=accountlocked";
                        //        break;

                        //    // if account is locked
                        //    case "M114":
                        //        responseModel.IsSystemError = true;
                        //        responseModel.SystemErrorURL = "~/Home/Error?errorCode=accountdeleted";
                        //        break;

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

    }
}