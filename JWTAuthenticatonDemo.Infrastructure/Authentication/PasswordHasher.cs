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
        public async Task<string> HashPasswordAsync(string password)
        {
            byte[] salt = await GenerateSaltAsync();
            byte[] hashedPasswordBytes;
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA512))
            {
                hashedPasswordBytes = await Task.Run(() => pbkdf2.GetBytes(32));
            }
            string hashedPassword = Convert.ToBase64String(hashedPasswordBytes);
            string saltString = Convert.ToBase64String(salt);
            string passwordString = $"{hashedPassword}:{saltString}";
            return passwordString;
        }

        public async Task<bool> VerifyPasswordAsync(string password, string hashedPasswordWithSalt)
        {
            string[] hashedPasswordParts = hashedPasswordWithSalt.Split(':');
            if (hashedPasswordParts.Length != 2)
            {
                return false;
            }
            byte[] hashedPasswordBytes = Convert.FromBase64String(hashedPasswordParts[0]);
            byte[] salt = Convert.FromBase64String(hashedPasswordParts[1]);
            byte[] hashedInputBytes;
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA512))
            {
                hashedInputBytes = await Task.Run(() => pbkdf2.GetBytes(32));
            }
            return hashedPasswordBytes.SequenceEqual(hashedInputBytes);
        }

        private static async Task<byte[]> GenerateSaltAsync()
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
