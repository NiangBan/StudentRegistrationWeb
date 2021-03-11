using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentRegistrationWeb.Models
{
    public class MajorModel
    {
        public string MajorID { get; set; }
        public string Name { get; set; }
    }
    public class MajorListModel
    {
        public List<MajorModel> lstMajor { get; set; }
        public string RespCode { get; set; }
        public string RespDescription { get; set; }
    }
}