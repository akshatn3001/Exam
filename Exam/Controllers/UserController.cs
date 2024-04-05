using Exam.Interfaces;
using Exam.Models;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Controllers
{
    [ApiController]
    [Route("api/users")]

    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        //Get Users Controller
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        //Get Users by Id Controller
        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(int id)
        {
            var user = _userService.GetById(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        //Create User Controller
        [HttpPost]
        public ActionResult<User> CreateUser(User user)
        {
            _userService.Add(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        //Update User Controller
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, User user)
        {
            _userService.Update(id, user);
            return NoContent();
        }

        //Delete User Controller
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            _userService.Delete(id);
            return NoContent();
        }
    }
}
