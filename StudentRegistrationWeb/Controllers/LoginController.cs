using StudentRegistrationWeb.Extension;
using StudentRegistrationWeb.Models;
using StudentRegistrationWeb.Utils;
using StudentRegistrationWeb.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentRegistrationWeb.Controllers
{
    public class LoginController : BaseController
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult UserRegister()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveUser(UserViewModel userViewModel)
        {
            AccountCreateResponseModel res = new AccountCreateResponseModel();
            AccountCreateRequestModel req = new AccountCreateRequestModel();

            try
            {
                if (ModelState.IsValid)
                {
                    var dynamicKey = CommonUtils.HardCodeKeyForAES();

                    req.UserName = userViewModel.UserName;
                    req.Email = userViewModel.Email;
                    req.FullName = userViewModel.FullName;
                    Session["UserNameForSalted"] = userViewModel.UserName;
                    req.Password = CommonUtils.SHA256HexHashString(userViewModel.Password);
                    string jsonString = EncryptUserRegisterRequestObject(req);
                    var apiRequestModel = this.APIRequest(jsonString, null, null,false);

                    var dataReturn = this.PostAPI(apiRequestModel, APIRoute.API_User_Account_Register).Result;

                    var ticketKey = CommonUtils.AESKeyForTicket();
                    var ticketIV = CommonUtils.AESIVForTicket();

                    /*var respomseStr = CryptoUtils.DecryptAES(dataReturn.JsonStringResponse, dynamicKey, dynamicIv);
                    if (string.IsNullOrEmpty(respomseStr))
                    {
                        ModelState.AddModelError("", "System Error");
                        return View(viewModel);
                    }
                    loginResponse = JsonConvert.DeserializeObject<LoginResponseModel>(respomseStr);*/

                }
                return View(userViewModel);
            }
            catch (Exception ex)
            {
                /*errormsg = ex.Message;
                TempData["Message"] = errormsg;
                Log.Error($"Exception => {ex.Message} ");*/
                return View(userViewModel);
            }

            finally
            {
            }
        }
    }
}