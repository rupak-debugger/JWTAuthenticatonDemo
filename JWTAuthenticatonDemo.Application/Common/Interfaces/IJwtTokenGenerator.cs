using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuthenticatonDemo.Application.Common.Interfaces
{
    public interface IJwtTokenGenerator
    {
        Task<string> GenerateToken(string userId,string firstName, string lastName);
    }
}
