using backend_app.Models.User.DTOs;
using backend_app.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace backend_app.Controllers
{
    [Route("api/v1/user")]
    [ApiController]
    public class UserControler : ControllerBase
    {
        private readonly IUserService service;

        public UserControler(IUserService service)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet]
        [Route("{userId}")]
        public IActionResult GetUserById([FromRoute] UserIdDTO userId)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var user = service.GetUserById(userId.Id);

            if (user is null) return BadRequest("Not find");

            return Ok(user);
        }

        [HttpPost]
        [Route("create")]
        public IActionResult CreatUser(UserCreateDTO user)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            service.CreatUser(user);

            return Created();
        }

        [HttpDelete]
        [Route("delete/{userId}")]
        public IActionResult DeleteUserById(UserIdDTO userId)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            service.DeleteUserById(userId.Id);

            return NoContent();
        }

        [HttpPost]
        [Route("login")]
        public IActionResult LoginUser([FromForm] UserLoginDTO userLogin)
        {
            //if(!ModelState.IsValid) return BadRequest(ModelState);

            var login = service.UserLogin(userLogin.Email, userLogin.Password);

            if (login is null) return BadRequest();

            return Ok(login);
        }
    }
}
