using JWTAuthenticatonDemo.Application.Common.Interfaces;
using JWTAuthenticatonDemo.Application.Contracts.Services;
using JWTAuthenticatonDemo.Application.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuthenticatonDemo.Infrastructure.Authentication.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<AuthenticationResponse> AuthenticateUserAsync()
        {
            string firstName = "firstName";
            string lastName = "lastName";
            string guid = Guid.NewGuid().ToString();
            var token = await _jwtTokenGenerator.GenerateToken(guid, firstName, lastName);
            return new AuthenticationResponse(guid,firstName,lastName,token);
        }
    }
}
