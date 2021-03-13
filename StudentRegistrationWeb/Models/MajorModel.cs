using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentRegistrationWeb.Models
{
    public class MajorModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string UniversityId { get; set; }
    }
    public class MajorListModel
    {
        public List<MajorModel> MajorList { get; set; }
        public string RespCode { get; set; }
        public string RespDescription { get; set; }
    }
}