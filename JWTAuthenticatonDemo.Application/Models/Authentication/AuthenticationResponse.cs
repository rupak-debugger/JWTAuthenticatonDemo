using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuthenticatonDemo.Application.Models.Authentication
{
    public class AuthenticationResponse
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public AuthenticationResponse(string userId, string userName, string email, string token)
        {
            this.UserId = userId;
            this.UserName = userName;
            this.Email = email;
            this.Token = token;
        }
    }
}
