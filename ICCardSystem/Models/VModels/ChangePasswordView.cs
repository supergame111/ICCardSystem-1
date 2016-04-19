using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICCardSystem.Models.VModels
{
    public class ChangePasswordView
    {
        public string bh { get; set; }
        public string oldpassword { get; set; }
        public string newpassword { get; set; }
    }
}