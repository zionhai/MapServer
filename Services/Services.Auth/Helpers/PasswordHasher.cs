using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Services.Shared.Models;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace Services.Auth.Helpers
{
    public sealed class PasswordHasher : IPasswordHasher
    {
        private const int SaltSize = 16; // 128 bit 
        private const int KeySize = 32; // 256 bit

        public PasswordHasher(IOptions<HashingOptions> options)
        {
            Options = options.Value;
        }

        private HashingOptions Options { get; }

        public string HashPassword(string password)
        {

            using (var algorithm = new Rfc2898DeriveBytes(
              password,
              SaltSize,
              Options.Iterations,
              HashAlgorithmName.SHA256))
            {
                var key = Convert.ToBase64String(algorithm.GetBytes(KeySize));
                var salt = Convert.ToBase64String(algorithm.Salt);

                return $"{Options.Iterations}.{salt}.{key}";
            }
        }


        public PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            var parts = hashedPassword.Split('.', 3);

            if (parts.Length != 3)
            {
                throw new FormatException("Unexpected hash format. " +
                  "Should be formatted as `{iterations}.{salt}.{hash}`");
            }

            var iterations = Convert.ToInt32(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);

            var needsUpgrade = iterations != Options.Iterations;

            using (var algorithm = new Rfc2898DeriveBytes(
              providedPassword,
              salt,
              iterations,
              HashAlgorithmName.SHA256))
            {
                var keyToCheck = algorithm.GetBytes(KeySize);

                var verified = keyToCheck.SequenceEqual(key);

                if (verified)
                    return PasswordVerificationResult.Success;
                return PasswordVerificationResult.Failed;
            }
        }

    }


}
