using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentRegistrationWeb.Models
{
    public class StudentModel
    {
        [Display(Name = "Student Name")]
        public string StudentName { get; set; }
        [Display(Name = "Student ID")]
        public string StudentNumber { get; set; }
        [Display(Name = "Father Name")]
        public string FatherName { get; set; }
        [Display(Name = "Phone")]
        public string Phone { get; set; }
        public string idString { get; set; }
    }
}