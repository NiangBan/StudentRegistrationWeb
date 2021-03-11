using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentRegistrationWeb.Models
{
    public class AcademicYearModel
    {
        public string AcademicYearID { get; set; }
        public string Name { get; set; }
    }
    public class AcademicYearListModel
    {
        public List<AcademicYearModel> lstAcademicYear { get; set; }
        public string RespCode { get; set; }
        public string RespDescription { get; set; }
    }
}