using JWTAuthenticatonDemo.Application.Common.Interfaces;
using JWTAuthenticatonDemo.Application.Contracts.Repositories;
using JWTAuthenticatonDemo.Application.Contracts.Services;
using JWTAuthenticatonDemo.Application.Models.Authentication;
using JWTAuthenticatonDemo.Application.Wrappers;
using JWTAuthenticatonDemo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuthenticatonDemo.Infrastructure.Persistence.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IApplicationUserRepository _applicationUserRepo;

        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IApplicationUserRepository applicationUserRepo)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _applicationUserRepo = applicationUserRepo;
        }

        public async Task<Response<AuthenticationResponse>> AuthenticateUserAsync(AuthenticationRequest request)
        {
            var token = await _jwtTokenGenerator.GenerateToken(request);
            var response = new AuthenticationResponse(Guid.NewGuid().ToString(), "firstName", "lastName", token);
            //return new AuthenticationResponse(guid,firstName,lastName,token);
            return new Response<AuthenticationResponse>(response, "");
        }

        public async Task<ApplicationUser> RegisterUserAsync(ApplicationUser user)
        {
            var result = await _applicationUserRepo.AddAsync(user);
            await _applicationUserRepo.SaveChangesAsync();
            return result;
        }
    }
}
