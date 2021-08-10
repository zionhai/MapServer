using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Auth;
using Services.Auth.Helpers;
using Services.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MapServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService AuthService;

        private string DefaultError = "Please provide valid username and password";
        public AuthController(IAuthService authService)
        {
            AuthService = authService;

        }

        [ActionName("login")]
        [HttpPost, Route("login")]
        public async Task<IActionResult> Login ([FromBody] LoginInfo loginInfo)
        {
            if (loginInfo is null) return BadRequest(DefaultError);


            var token = AuthService.LoginUser(loginInfo);
            if (string.IsNullOrWhiteSpace(token))
            {
                return BadRequest(DefaultError);
            }
            return Ok(new
            {
                jwt = token
            });
        }
    }
}
