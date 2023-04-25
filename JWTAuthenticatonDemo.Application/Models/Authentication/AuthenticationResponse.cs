using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuthenticatonDemo.Application.Models.Authentication
{
    public class AuthenticationResponse
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

        public AuthenticationResponse()
        {
            
        }
        public AuthenticationResponse(string userName, string email, string token)
        {
            this.UserName = userName;
            this.Email = email;
            this.Token = token;
        }
    }
}
