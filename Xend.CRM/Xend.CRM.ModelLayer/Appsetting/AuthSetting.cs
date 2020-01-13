using System;
using System.Collections.Generic;
using System.Text;

namespace Xend.CRM.ModelLayer.Appsetting
{
    public class AuthSetting
    {
        public string IdentityUrl { get; set; }
        public string Audience { get; set; }
        public string AuthSecret { get; set; }
        public bool RequireHttps { get; set; }
    }
}
