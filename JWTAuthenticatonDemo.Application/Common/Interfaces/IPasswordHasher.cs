using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuthenticatonDemo.Application.Common.Interfaces
{
    public interface IPasswordHasher
    {
        Task<string> HashPasswordAsync(string password, byte[] salt);
        Task<byte[]> GenerateSaltAsync();
    }
}
