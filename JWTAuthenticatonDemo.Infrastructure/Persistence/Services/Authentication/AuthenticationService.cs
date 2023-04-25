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

        public async Task<Response<RegistrationResponse>> RegisterUserAsync(RegistrationRequest request)
        {
            if(!await UserExists(request.Email))
            {
                var passwordHash = await _passwordHasher.HashPasswordAsync(request.Password);
                ApplicationUser user = new()
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    PasswordHash = passwordHash,
                    Email = request.Email,
                };
                await _applicationUserRepo.AddAsync(user);
                await _applicationUserRepo.SaveChangesAsync();
                var result = new RegistrationResponse(user.Email);
                return new Response<RegistrationResponse>(result);
            }
            return new Response<RegistrationResponse>("Email already taken !!");
        }

        public async Task<Response<AuthenticationResponse>> AuthenticateUserAsync(AuthenticationRequest request)
        {
            var user = await _applicationUserRepo.FirstOrDefaultAsync(a => a.Email == request.Email);
            if (user!=null)
            {
                var result = await _passwordHasher.VerifyPasswordAsync(request.Password, user.PasswordHash);
                if (result)
                {
                    var token = await _jwtTokenGenerator.GenerateToken(user);
                    var response = new AuthenticationResponse(user.FirstName, user.Email, token);
                    return new Response<AuthenticationResponse>(response, "Logged in Successfully");
                }
                return new Response<AuthenticationResponse>("Password didnot match");
            }
            return new Response<AuthenticationResponse>("Email not found");
        }

        private async Task<bool> UserExists(string email)
        {
            var user = await _applicationUserRepo.FirstOrDefaultAsync(a => a.Email == email);
            if (user!=null)
            {
                return true;
            }
            return false;
        }
    }
}
