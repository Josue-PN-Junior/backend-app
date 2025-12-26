using backend_app.Models.User;

namespace backend_app.Services.Interface;

public interface IUserService
{
    public void CreatUser(UserCreateDTO user);

    public UserDTO? GetUserById(int id);

    public void DeleteUserById(int id);
}
