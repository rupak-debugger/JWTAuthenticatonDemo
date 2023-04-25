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

namespace JWTAuthenticatonDemo.Infrastructure.Authentication
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JWTSettings _jwtSettings;
        public JwtTokenGenerator(IOptions<JWTSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }
        public async Task<string> GenerateToken(AuthenticationRequest request)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, request.Email),
                //new Claim(JwtRegisteredClaimNames.GivenName, firstName),
                //new Claim(JwtRegisteredClaimNames.FamilyName, lastName),
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
    }
}
