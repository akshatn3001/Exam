using Exam.Interfaces;
using Exam.Models;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;

namespace Exam.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService userService;

        public AuthController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            userService.Add(user);
            return Ok();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Models.LoginRequest loginRequest)
        {
            var (token, user) = userService.AuthenticateWithDetails(loginRequest.Email, loginRequest.Password);

            if (token == null || user == null)
                return Unauthorized();

            return Ok(new
            {
                Token = token,
                user.Id,
                user.Email,
                user.roleType,
                user.Name,
            });
        }    
    }
}
