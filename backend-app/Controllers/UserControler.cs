using System.Data;
using backend_app.Models.User;
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
        [Route("{id}")]
        public IActionResult GetUserById([FromRoute] int id)
        {
            var user = service.GetUserById(id);

            if (user is null) return BadRequest("Not find");

            return Ok(user);
        }

        [HttpPost]
        [Route("create")]
        public IActionResult CreatUser(UserCreateDTO user)
        {
            service.CreatUser(user);

            return Created();
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult DeleteUserById(int id)
        {
            service.DeleteUserById(id);

            return Ok();
        }

        [HttpPost]
        [Route("login")]
        public IActionResult LoginUser([FromForm] string email,[FromForm] string password)
        {
            var login = service.UserLogin(email, password);

            if (login is null) return BadRequest();

            return Ok(login);
        }
    }
}
