using backend_app.Models.Generic.DTOs;
using backend_app.Models.User;
using backend_app.Models.User.DTOs;

namespace backend_app.Services.Interface;

public interface IUserService
{
    public void CreateUser(UserCreateDTO user);

    public UserDTO? GetUserById(int id);

    public void DeleteUserById(int id);

    public void UpdateUserById(int id, UserUpdateDTO user);

    public string UserLogin(string email, string password);

    public void ChangeEmail(EmailChangeDTO data);
}
