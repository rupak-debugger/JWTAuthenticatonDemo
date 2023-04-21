using JWTAuthenticatonDemo.Application.Common.Interfaces;
using JWTAuthenticatonDemo.Application.Contracts.Services;
using JWTAuthenticatonDemo.Application.Models.Authentication;
using JWTAuthenticatonDemo.Application.Wrappers;
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

        public async Task<Response<AuthenticationResponse>> AuthenticateUserAsync(AuthenticationRequest request)
        {
            var token = await _jwtTokenGenerator.GenerateToken(request);
            var response = new AuthenticationResponse(Guid.NewGuid().ToString(), "firstName", "lastName", token);
            //return new AuthenticationResponse(guid,firstName,lastName,token);
            return new Response<AuthenticationResponse>(response, "");
        }
    }
}
