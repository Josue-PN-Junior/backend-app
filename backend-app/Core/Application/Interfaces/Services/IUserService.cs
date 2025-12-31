using backend_app.Core.Application.DTOs.User;
using backend_app.Core.Application.DTOs.TokenPassword;
using backend_app.Core.Domain.Entities;

namespace backend_app.Core.Application.Interfaces.Services;

public interface IUserService
{
    UserEntity? GetUserById(int userId);
    void CreateUser(UserCreateDTO user);
    void DeleteUserById(int userId);
    void UpdateUserById(int userId, UserUpdateDTO user);
    string UserLogin(string email, string password);
    void ChangeEmail(EmailChangeDTO data);
    void RequestResetPassword(EmailDTO email);
    void VerifyCodeReset(VerifyResetCodeDTO code);
    void ResetPassword(ResetPasswordDTO data);
}