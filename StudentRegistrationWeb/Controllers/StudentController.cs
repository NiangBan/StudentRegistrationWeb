using StudentRegistrationWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentRegistrationWeb.Controllers
{
    public class StudentController : Controller
    {
        string test = "";
        // GET: Student
        public ActionResult StudentList()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult GetStudentListSearch()
        {
            return View("StudentList");
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult GetStudentListSearch(StudentModel student)
        {
            return View("StudentList");
        }
    }
}