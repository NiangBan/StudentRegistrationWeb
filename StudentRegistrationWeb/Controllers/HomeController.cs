using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentRegistrationWeb.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [AllowAnonymous]
        public ActionResult Error(string errorCode)
        {
            if (errorCode == null)
                errorCode = "";

            FormsAutheticationSignOutAndSessionAbandon();

            switch (errorCode.ToLower())
            {
                case "invalidkey":
                    ViewBag.ErrorMessage1 = "Your had logon from another device and this session become invalid";
                    ViewBag.ErrorMessage2 = "Please login again by clicking following button.";
                    break;

                case "sessiontimeout":
                    ViewBag.ErrorMessage1 = "Your session has been timeout due to inactivity.";
                    ViewBag.ErrorMessage2 = "Please login again by clicking following button.";
                    break;

                case "accountlocked":
                    ViewBag.ErrorMessage1 = "Your account is locked.";
                    ViewBag.ErrorMessage2 = "Please login again by clicking following button.";
                    break;

                case "accountdeleted":
                    ViewBag.ErrorMessage1 = "Your account is deleted.";
                    ViewBag.ErrorMessage2 = "Please login again by clicking following button.";
                    break;
                case "unauthorized":
                    ViewBag.ErrorMessage1 = "You don't have Permission for this menu.";
                    ViewBag.ErrorMessage2 = "Please login again by clicking following button.";
                    break;
                case "pagenotfound":
                    ViewBag.ErrorMessage1 = "Can't find your requested Page.";
                    ViewBag.ErrorMessage2 = "Please login again by clicking following button.";
                    break;

                default:
                    ViewBag.ErrorMessage1 = "System has encountered an issue.";
                    ViewBag.ErrorMessage2 = "The detail of the issues has been logged. Please login again by clicking following button.";
                    break;
            }

            return View();
        }
    }
}