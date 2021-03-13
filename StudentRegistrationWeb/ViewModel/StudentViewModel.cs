using StudentRegistrationWeb.LocalResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentRegistrationWeb.ViewModel
{
    public class StudentViewModel
    {
        [Required(ErrorMessage = "Please enter Student No.")]
        [Display(Name = "StudentNo", ResourceType = typeof(Resource))]
        public string StudentNo { get; set; }
        [Required(ErrorMessage = "Please enter Name.")]
        [Display(Name = "Name", ResourceType = typeof(Resource))]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter NRC.")]
        [Display(Name = "NRC", ResourceType = typeof(Resource))]
        public string NRC { get; set; }
        [Required(ErrorMessage = "Please enter Address.")]
        [Display(Name = "Address", ResourceType = typeof(Resource))]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please enter Phone.")]
        [Display(Name = "Phone", ResourceType = typeof(Resource))]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Please enter Email.")]
        [Display(Name = "Email", ResourceType = typeof(Resource))]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter FatherName.")]
        [Display(Name = "FatherName", ResourceType = typeof(Resource))]
        public string FatherName { get; set; }
        [Required(ErrorMessage = "Please enter University.")]
        [Display(Name = "University", ResourceType = typeof(Resource))]
        public string University { get; set; }
        [Required(ErrorMessage = "Please enter Major.")]
        [Display(Name = "Major", ResourceType = typeof(Resource))]
        public string Major { get; set; }
        [Required(ErrorMessage = "Please enter AcademicYear.")]
        [Display(Name = "AcademicYear", ResourceType = typeof(Resource))]
        public string AcademicYear { get; set; }
        //[Required(ErrorMessage = "Please enter Gender.")]
        //[Display(Name = "Gender", ResourceType = typeof(Resource))]
        //public string Gender { get; set; }
        [Required(ErrorMessage = "Please enter DateOfBirth.")]
        [Display(Name = "DateOfBirth", ResourceType = typeof(Resource))]
        public string DateOfBirth { get; set; }
        public int UniversityID { get; set; }
        public int MajorID { get; set; }
        public int AcademicYearID { get; set; }
        public int Id { get; set; }

        [Required(ErrorMessage = "Gender Required")]
        [Display(Name = "Gender", ResourceType = typeof(Resource))]
        public string Gender { get; set; }

        public List<GenderModel> GenderList { get; set; }
    }
    public class GenderModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}