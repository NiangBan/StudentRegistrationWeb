using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentRegistrationWeb.Models
{
    public class AcademicYearModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string MajorId { get; set; }
    }
    public class AcademicYearListModel
    {
        public List<AcademicYearModel> YearList { get; set; }
        public string RespCode { get; set; }
        public string RespDescription { get; set; }
    }
}