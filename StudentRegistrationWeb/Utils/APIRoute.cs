using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentRegistrationWeb.Utils
{
    public class APIRoute
    {
        public static string API_User_Account_Register = "API/Account/Register";
        public static string API_Student_List = "API/Student/GetStudentList";
        public static string API_Student_Register = "API/Student/Create";
        public static string API_Student_Update = "API/Student/Update";
        public static string API_Student_Delete = "API/Student/Delete";
        public static string API_Get_StudentById = "API/Student/GetStudentById";
        public static string API_Get_UniversityList = "API/University/GetUniversityList";
        public static string API_Get_MajorList_UniId = "API/Major/GetMajorList";
        public static string API_Get_AcademicList_MajorId = "API/Academicyear/GetAcademicyearList";
    }
}