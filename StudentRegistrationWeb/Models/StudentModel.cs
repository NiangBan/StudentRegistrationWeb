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
    
    public class StudentDTO
    {
        public string Id { get; set; }
        public string StudentNo { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string NRC { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string DateOfBirth { get; set; }
        public string UniversityId { get; set; }
        public string UniversityName { get; set; }
        public string MajorId { get; set; }
        public string MajorName { get; set; }
        public string AcademicyearId { get; set; }
        public string AcademicyearName { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
        public string CreatedUserId { get; set; }
        public string UpdatedUserId { get; set; }
    }

    public class StudentListModel
    {
        public List<StudentDTO> studentList { get; set; }
    }
}