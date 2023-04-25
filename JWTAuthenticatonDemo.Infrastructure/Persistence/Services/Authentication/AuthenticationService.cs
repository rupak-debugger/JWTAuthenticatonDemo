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
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IApplicationUserRepository _applicationUserRepo;

        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IApplicationUserRepository applicationUserRepo, IPasswordHasher passwordHasher)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _applicationUserRepo = applicationUserRepo;
            _passwordHasher = passwordHasher;
        }

        //public async Task<Response<AuthenticationResponse>> AuthenticateUserAsync(AuthenticationRequest request)
        //{
        //    var token = await _jwtTokenGenerator.GenerateToken(request);
        //    var response = new AuthenticationResponse(Guid.NewGuid().ToString(), "firstName", "lastName", token);
        //    return new Response<AuthenticationResponse>(response, "");
        //}

        public async Task<Response<RegistrationResponse>> RegisterUserAsync(RegistrationRequest request)
        {
            var passwordHash = await _passwordHasher.HashPasswordAsync(request.Password);
            ApplicationUser user = new ()
            { 
                FirstName = request.FirstName,
                LastName = request.LastName,
                PasswordHash = passwordHash,
                Email = request.Email,
            };
            await _applicationUserRepo.AddAsync(user);
            await _applicationUserRepo.SaveChangesAsync();
            var result = new RegistrationResponse(user.Email);
            return new Response<RegistrationResponse>(result,"User registered successfully !");
        }

        public async Task AuthenticateUserAsync(AuthenticationRequest request)
        {
            var user = await _applicationUserRepo.FindByEmailAsync(e => e.Email == request.Email);
            await _passwordHasher.VerifyPasswordAsync(request.Password, user.PasswordHash);
        }
    }
}
