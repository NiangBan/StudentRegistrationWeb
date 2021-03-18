using Newtonsoft.Json;
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
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel viewModel)
        {
            LoginResposeModel res = new LoginResposeModel();
            LoginRequestModel req = new LoginRequestModel();
            Session[CommonDynamicKey] = String.Empty;
            Session[CommonUserID] = String.Empty;
            Session[CommonSessionID] = String.Empty;
            try
            {
                if (ModelState.IsValid)
                {
                    req.UserName = viewModel.UserName;
                    Session["UserNameForSalted"] = viewModel.UserName;
                    req.Password = CommonUtils.SHA256HexHashString(viewModel.Password);
                    string jsonString = JsonConvert.SerializeObject(req);
                    var apiRequestModel = this.APIRequest(jsonString, null, null, false);

                    var dataReturn = this.PostAPI(apiRequestModel, APIRoute.API_User_Login).Result;
                    var redirectLink = string.Empty;
                    if (dataReturn != null)
                    {
                        res = JsonConvert.DeserializeObject<LoginResposeModel>(dataReturn.JsonStringResponse);
                        if (res.RespCode == "000")
                        {
                            Session[CommonDynamicKey] = res.DynamicKey;
                            Session[CommonUserID] = res.UserId;
                            Session[CommonSessionID] = res.SessionId;
                            Session[UserName] = res.UserName;
                            redirectLink = HtmlExtension.GetEncryptLinkForRedirect("StudentList", "Student");
                            Response.Redirect(redirectLink, false);
                        }
                        else
                        {
                            ViewBag.IsSuccess = "fail";
                            ViewBag.Message = res.RespDescription;
                        }
                    }
                }
                return View(viewModel);
            }
            catch (Exception ex)
            {
                return View(viewModel);
            }

            finally
            {
            }

            return View();
        }

        [AllowAnonymous]
        public ActionResult UserRegister()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult UserRegister(UserViewModel userViewModel)
        {
            AccountCreateResponseModel res = new AccountCreateResponseModel();
            AccountCreateRequestModel req = new AccountCreateRequestModel();
            Session[CommonDynamicKey] = String.Empty;
            Session[CommonUserID] = String.Empty;
            Session[CommonSessionID] = String.Empty;
            try
            {
                if (ModelState.IsValid)
                {
                    if (!userViewModel.Password.Equals(userViewModel.ConfirmPassword))
                    {
                        ViewBag.IsSuccess = "fail";
                        ViewBag.Message = "Password and Confirm Password are not same.";
                        return View(userViewModel);
                    }
                    req.UserName = userViewModel.UserName;
                    req.Email = userViewModel.Email;
                    req.FullName = userViewModel.FullName;
                    Session["UserNameForSalted"] = userViewModel.UserName;
                    req.Password = CommonUtils.SHA256HexHashString(userViewModel.Password);
                    string jsonString = JsonConvert.SerializeObject(req);
                    var apiRequestModel = this.APIRequest(jsonString, null, null, false);

                    var dataReturn = this.PostAPI(apiRequestModel, APIRoute.API_User_Account_Register).Result;
                    if (dataReturn != null)
                    {
                        res = JsonConvert.DeserializeObject<AccountCreateResponseModel>(dataReturn.JsonStringResponse);
                        if (res.RespCode == "000")
                        {
                            ViewBag.IsSuccess = "success";
                            ViewBag.Message = "User account creation is successful!";
                        }
                        else
                        {
                            ViewBag.IsSuccess = "fail";
                            ViewBag.Message = res.RespDescription;
                        }
                    }
                }
                return View(userViewModel);
            }
            catch (Exception ex)
            {
                return View(userViewModel);
            }

            finally
            {
            }
        }

        [AllowAnonymous]
        public ActionResult Logout()
        {
            this.FormsAutheticationSignOutAndSessionAbandon();
            return View("Login");
        }
    }
}