using backend_app.Core.Application.DTOs.TokenPassword;
using backend_app.Core.Application.DTOs.User;

namespace backend_app.Core.Application.Interfaces.Services;

public interface IUserService
{
    public void CreateUser(UserCreateDTO user);

    public UserDTO? GetUserById(int id);

    public void DeleteUserById(int id);

    public void UpdateUserById(int id, UserUpdateDTO user);

    public object UserLogin(string email, string password);

    public void ChangeEmail(EmailChangeDTO data);

    public void RequestResetPassword(EmailDTO email);

    public void VerifyCodeReset(VerifyResetCodeDTO data);

    public void ResetPassword(ResetPasswordDTO data);
}
