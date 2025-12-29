using backend_app.Models.User.DTOs;
using backend_app.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using static backend_app.Helpers.Exceptions.CustomizedExceptions;

namespace backend_app.Controllers
{
    [Route("api/v1/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService service;

        public UserController(IUserService service)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet]
        [Route("{userId}")]
        public IActionResult GetUserById([FromRoute] int userId)
        {
            if (userId <= 0)
                return BadRequest("UserId deve ser maior que zero");

            var user = service.GetUserById(userId)
                ?? throw new UserNotFoundException($"Id: {userId}");

            return Ok(user);
        }

        [HttpPost]
        [Route("create")]
        public IActionResult CreateUser([FromBody] UserCreateDTO user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            service.CreateUser(user);

            return Created();
        }

        [HttpDelete]
        [Route("delete/{userId}")]
        public IActionResult DeleteUserById([FromRoute] int userId)
        {
            if (userId <= 0)
                return BadRequest("UserId deve ser maior que zero");

            service.DeleteUserById(userId);

            return NoContent();
        }

        [HttpPatch]
        [Route("update/{userId}")]
        public IActionResult UpdateUserById([FromRoute] int userId, [FromBody] UserUpdateDTO user)
        {
            if (userId <= 0)
                return BadRequest("UserId deve ser maior que zero");
            
            if (!ModelState.IsValid) return BadRequest(ModelState);

            service.UpdateUserById(userId, user);

            return NoContent();
        }

        [HttpPost]
        [Route("login")]
        public IActionResult LoginUser([FromForm] UserLoginDTO userLogin)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var login = service.UserLogin(userLogin.Email, userLogin.Password);

            return Ok(login);
        }
    }
}
