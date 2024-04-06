using Exam.Interfaces;
using Exam.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly JwtService jwtService;

        public AuthController(IUserService userService, JwtService jwtService)
        {
            this.userService = userService;
            this.jwtService = jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Models.LoginRequest loginRequest)
        {
            var (token, user) = userService.AuthenticateWithDetails(loginRequest.Email, loginRequest.Password);

            if (token != null && user != null)
            {
                return Ok(new
                {
                    Token = token,
                    user.Id,
                    user.Email,
                    Role = user.roleType.ToString(), // Return role as a string
                    user.Name
                }); ;
            }

            return Unauthorized();
        }
    }
}

