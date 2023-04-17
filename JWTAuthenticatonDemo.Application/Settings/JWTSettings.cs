using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuthenticatonDemo.Application.Settings
{
    public class JWTSettings
    {
        public const string SectionName = "JWTSettings";
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
        public double DurationInMinutes { get; set; }
    }
}
