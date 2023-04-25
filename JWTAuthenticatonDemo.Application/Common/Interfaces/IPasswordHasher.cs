using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuthenticatonDemo.Application.Common.Interfaces
{
    public interface IPasswordHasher
    {
        Task<string> HashPasswordAsync(string password);
        Task<bool> VerifyPasswordAsync(string password, string hashedPasswordWithSalt);
        //Task<byte[]> GenerateSaltAsync();
    }
}
