using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentRegistrationWeb.Models
{
    public class UniversityModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
    public class UniversityListModel
    {
        public List<UniversityModel> UniversityList { get; set; }
        public string RespCode { get; set; }
        public string RespDescription { get; set; }
    }


}