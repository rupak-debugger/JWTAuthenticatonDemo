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
        private readonly ILoginTokenRepository _loginTokenRepo;

        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IApplicationUserRepository applicationUserRepo, IPasswordHasher passwordHasher, ILoginTokenRepository loginTokenRepo)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _applicationUserRepo = applicationUserRepo;
            _passwordHasher = passwordHasher;
            _loginTokenRepo = loginTokenRepo;
        }

        public async Task<Response<RegistrationResponse>> RegisterUserAsync(RegistrationRequest request)
        {
            try
            {
                if (!await _applicationUserRepo.AnyAsync(u => u.Email==request.Email))
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
                //EmailAlreadyTakenException
                throw new Exception("Email Already Taken");
            }
            catch (Exception ex)
            {
                List<string> errors = new();
                errors.Add(ex.Message);
                return new Response<RegistrationResponse>(errors);
            }
        }

        public async Task<Response<AuthenticationResponse>> AuthenticateUserAsync(AuthenticationRequest request)
        {
            try
            {
                var user = await _applicationUserRepo.FirstOrDefaultAsync(a => a.Email == request.Email);
                if (user!=null)
                {
                    var result = await _passwordHasher.VerifyPasswordAsync(request.Password, user.PasswordHash);
                    if (result)
                    {
                        var token = await _jwtTokenGenerator.GenerateAccessToken(user);
                        var refreshToken = await _jwtTokenGenerator.GenerateRefreshToken();
                        var loginEntity = new LoginToken()
                        {
                            UserId = user.Id,
                            RefreshToken = refreshToken,
                        };
                        await _loginTokenRepo.AddAsync(loginEntity);
                        await _loginTokenRepo.SaveChangesAsync();
                        var response = new AuthenticationResponse(user.FirstName, user.Email, token, refreshToken);
                        return new Response<AuthenticationResponse>(response, "Logged in Successfully");
                    }
                    //PasswordsDonotMatchException
                    throw new Exception("Passwords Don't match");
                }
                //EmailNotFoundException
                throw new Exception("Email not found");
            }
            catch(Exception ex)
            {
                List<string> errors = new();
                errors.Add(ex.Message);
                return new Response<AuthenticationResponse>(errors);
            }
        }

        public async Task<Response<AuthenticationResponse>> RefreshTokenAsync(string refreshToken)
        {
            try
            {
                var result = await _jwtTokenGenerator.ValidateRefreshToken(refreshToken);
                if (result)
                {
                    var loginTokenEntity = await _loginTokenRepo.FirstOrDefaultAsync(l => l.RefreshToken == refreshToken);
                    //var user = await _jwtTokenGenerator.GetUserFromRefreshToken(refreshToken);
                    var user = await _applicationUserRepo.FirstOrDefaultAsync(a => a.Id == loginTokenEntity.UserId);
                    var token = await _jwtTokenGenerator.GenerateAccessToken(user);
                    var newRefreshToken = await _jwtTokenGenerator.GenerateRefreshToken();                    
                    loginTokenEntity.RefreshToken = newRefreshToken;
                    await _loginTokenRepo.UpdateAsync(loginTokenEntity);
                    await _loginTokenRepo.SaveChangesAsync();
                    var response = new AuthenticationResponse(user.FirstName, user.Email, token, newRefreshToken);
                    return new Response<AuthenticationResponse>(response, "Access token updated");
                }
                throw new Exception("Refresh token not valid");
            }
            catch (Exception ex)
            {
                List<string> errors = new();
                errors.Add(ex.Message);
                return new Response<AuthenticationResponse>(errors);
            }
        }

    }
}
