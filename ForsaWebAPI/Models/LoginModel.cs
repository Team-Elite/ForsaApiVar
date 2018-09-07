using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForsaWebAPI.Models
{
    public class LoginModel
    {
        public string UserName { get; set; }

        public string UserPassword { get; set; }
        public string UserEmailId { get; set; }
        public string ForgotPasswordEmailId { get; set; }
    }
}