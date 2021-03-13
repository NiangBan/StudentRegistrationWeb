using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentRegistrationWeb.Models
{
    public class LoginResposeModel : BaseResponseModel
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string DynamicKey { get; set; }
        public string SessionId { get; set; }
        public string UserName { get; set; }
    }
}