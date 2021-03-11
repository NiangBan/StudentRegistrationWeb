using StudentRegistrationWeb.LocalResources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentRegistrationWeb.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter UserName.")]
        [Display(Name = "UserName", ResourceType = typeof(Resource))]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please enter Password.")]
        [Display(Name = "Password", ResourceType = typeof(Resource))]
        public string Password { get; set; }
    }
}