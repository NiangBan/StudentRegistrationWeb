using StudentRegistrationWeb.LocalResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentRegistrationWeb.ViewModel
{
    public class UserViewModel
    {
        [Required(ErrorMessage = "Please enter UserName.")]
        [Display(Name = "UserName", ResourceType = typeof(Resource))]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please enter Full Name.")]
        [Display(Name = "FullName", ResourceType = typeof(Resource))]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Please enter Email.")]
        [Display(Name = "Email", ResourceType = typeof(Resource))]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter Password.")]
        [Display(Name = "Password", ResourceType = typeof(Resource))]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please enter Confirm Password.")]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(Resource))]
        public string ConfirmPassword { get; set; }
    }
}