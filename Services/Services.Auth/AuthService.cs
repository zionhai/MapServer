using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Auth.Helpers;
using Services.Data;
using Services.Shared.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository UserRepo;
        private readonly IPasswordHasher PasswordHasher;
        private string SecretKey;

        public AuthService(IUserRepository userRepo, IConfiguration configuration, IPasswordHasher passwordHasher)
        {
            UserRepo = userRepo;
            SecretKey = configuration["SecretKey"].ToString();
            PasswordHasher = passwordHasher;
        }

        public string LoginUser(LoginInfo loginInfo)
        {
            if (loginInfo is null) return null;
            return CreateToken(loginInfo.Username);
            var users = UserRepo.GetUsers();
            var validUser = users.Where(u => u.Email.Equals(loginInfo.Username))
                                 .FirstOrDefault();

            if (validUser is not null)
            {
                var isValid = PasswordHasher.VerifyHashedPassword(validUser.Password, loginInfo.Password);
                if (isValid == PasswordVerificationResult.Success)
                {
                    return CreateToken(loginInfo.Username);
                }    
                
            }
            return string.Empty;

        }

        private string CreateToken(string username)
        {
            var userClaim = new[] { new Claim(ClaimTypes.Name, username) };
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            DateTime cd = DateTime.Now;
            var tokeOptions = new JwtSecurityToken(
                issuer: "http://localhost:4200",
                audience: "http://localhost:4200",
                claims: userClaim,
                expires: DateTime.UtcNow.AddMinutes(100),
                signingCredentials: signinCredentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return token;
        }
    }
}
