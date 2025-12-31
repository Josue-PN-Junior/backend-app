using backend_app.Models.Generic.DTOs;
using backend_app.Models.TokenPassword.DTOs;
using backend_app.Models.User.DTOs;
using backend_app.Services.Interface;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
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

        [Authorize]
        [HttpDelete]
        [Route("delete/{userId}")]
        public IActionResult DeleteUserById([FromRoute] int userId)
        {
            if (userId <= 0)
                return BadRequest("UserId deve ser maior que zero");

            service.DeleteUserById(userId);

            return NoContent();
        }

        [Authorize]
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

            var token = service.UserLogin(userLogin.Email, userLogin.Password);

            return Ok(token);
        }

        [Authorize]
        [HttpPatch]
        [Route("change-email")]
        public IActionResult ChangeUserEmail(EmailChangeDTO data)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            service.ChangeEmail(data);

            return NoContent();
        }

        [HttpPost]
        [Route("password/request-reset")]
        public IActionResult RequestResetPassword([FromBody] EmailDTO email)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            service.RequestResetPassword(email);

            return NoContent();
        }

        [HttpPost]
        [Route("password/verify-code")]
        public IActionResult VerifyCodeReset(VerifyResetCodeDTO code)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            service.VerifyCodeReset(code);

            return NoContent();
        }

        [HttpPost]
        [Route("password/reset-password")]
        public IActionResult ResetPassword([FromBody] ResetPasswordDTO data)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            service.ResetPassword(data);

            return NoContent();
        }
    }
}
