using Microsoft.AspNetCore.Identity;


namespace Services.Auth.Helpers
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        PasswordVerificationResult VerifyHashedPassword(string hash, string password);
    }

    public sealed class HashingOptions
    {
        public int Iterations { get; set; } = 10000;
    }
}
