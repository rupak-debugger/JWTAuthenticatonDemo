using JWTAuthenticatonDemo.Application.Models.Authentication;
using JWTAuthenticatonDemo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuthenticatonDemo.Application.Common.Interfaces
{
    public interface IJwtTokenGenerator
    {
        Task<string> GenerateAccessToken(ApplicationUser user);
        Task<string> GenerateRefreshToken();
        Task<bool> ValidateRefreshToken(string refreshToken);
        //Task<ApplicationUser> GetUserFromRefreshToken(string refreshToken);
    }
}
