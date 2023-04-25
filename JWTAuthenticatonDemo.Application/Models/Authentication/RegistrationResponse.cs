using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuthenticatonDemo.Application.Models.Authentication
{
    public class RegistrationResponse
    {
        public string Email { get; set; }
        public RegistrationResponse()
        {

        }
        public RegistrationResponse(string email)
        {
            this.Email = email;
        }
    }
}
