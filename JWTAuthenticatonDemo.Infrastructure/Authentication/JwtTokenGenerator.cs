using JWTAuthenticatonDemo.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using JWTAuthenticatonDemo.Application.Settings;
using Microsoft.Extensions.Options;
using JWTAuthenticatonDemo.Application.Models.Authentication;
using JWTAuthenticatonDemo.Domain.Entities;
using System.Security.Cryptography;
using JWTAuthenticatonDemo.Application.Contracts.Repositories;

namespace JWTAuthenticatonDemo.Infrastructure.Authentication
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JWTSettings _jwtSettings;
        private readonly ILoginTokenRepository _loginTokenRepo;
        private readonly IApplicationUserRepository _applicationUserRepo;
        public JwtTokenGenerator(IOptions<JWTSettings> jwtSettings, ILoginTokenRepository loginTokenRepo, IApplicationUserRepository applicationUserRepo)
        {
            _jwtSettings = jwtSettings.Value;
            _loginTokenRepo = loginTokenRepo;
            _applicationUserRepo = applicationUserRepo;
        }
        public async Task<string> GenerateAccessToken(ApplicationUser user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = await Task.Run(() =>
            new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: signingCredentials)
            );
            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }

        public async  Task<string> GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            await Task.Run(() => rng.GetBytes(randomNumber));            
            return Convert.ToBase64String(randomNumber);
        }

        //public async Task<ApplicationUser> GetUserFromRefreshToken(string refreshToken)
        //{
        //    var loginToken = await _loginTokenRepo.FirstOrDefaultAsync(t => t.RefreshToken == refreshToken);
        //    var user = await _applicationUserRepo.FirstOrDefaultAsync(u => u.Id == loginToken.UserId);
        //    return user;
        //}

        public async Task<bool> ValidateRefreshToken(string refreshToken)
        {
            return await _loginTokenRepo.AnyAsync(x => x.RefreshToken == refreshToken);
        }
    }
}
