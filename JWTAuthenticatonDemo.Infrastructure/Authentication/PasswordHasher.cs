using JWTAuthenticatonDemo.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuthenticatonDemo.Infrastructure.Authentication
{
    public class PasswordHasher : IPasswordHasher
    {
        public async Task<string> HashPasswordAsync(string password, byte[] salt)
        {
            byte[] hashedPasswordBytes;
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA512))
            {
                hashedPasswordBytes = await Task.Run(() => pbkdf2.GetBytes(32));
            }
            return Convert.ToBase64String(hashedPasswordBytes);
        }

        public async Task<byte[]> GenerateSaltAsync()
        {
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                await Task.Run(() => rng.GetBytes(salt));
            }
            return salt;
        }
    }
}
