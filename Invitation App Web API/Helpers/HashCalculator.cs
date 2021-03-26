using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Invitation_App_Web_API.Helpers
{
    public static class HashCalculator
    {
        private static string GenerateSalt()
        {
            var salt = new byte[128 / 8];
            using var rng = RandomNumberGenerator.Create();

            rng.GetBytes(salt);

            return Convert.ToBase64String(salt);
        }

        public static string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password, GenerateSalt());

        public static bool ValidatePassword(string password, string passwordHash) => BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
}
