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
        public ActionResult StudentRegister()
        {
            TestModel t = new TestModel();
            t.Name = "CU";
            TestModel t1 = new TestModel();
            t1.Name = "TU";
            List<TestModel> l = new List<TestModel>();
            l.Add(t);
            l.Add(t1);
            ViewBag.UniversityList = l;
            return View();
        }

    }
}