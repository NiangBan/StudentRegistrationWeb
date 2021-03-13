using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentRegistrationWeb.Models
{
    public class LoginRequestModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}