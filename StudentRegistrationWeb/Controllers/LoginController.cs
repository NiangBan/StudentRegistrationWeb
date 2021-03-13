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
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel viewModel)
        {
            LoginResposeModel res = new LoginResposeModel();
            LoginRequestModel req = new LoginRequestModel();

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
                /*errormsg = ex.Message;
                TempData["Message"] = errormsg;
                Log.Error($"Exception => {ex.Message} ");*/
                return View(viewModel);
            }

            finally
            {
            }

            return View();
        }

        public ActionResult UserRegister()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UserRegister(UserViewModel userViewModel)
        {
            AccountCreateResponseModel res = new AccountCreateResponseModel();
            AccountCreateRequestModel req = new AccountCreateRequestModel();

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