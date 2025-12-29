using backend_app.Models.User;

namespace backend_app.Repositories.Interface;

public interface IUserRepository
{
    void SetUser(UserEntity user);

    UserEntity? GetUserById(int id);

    void DeleteUserById(int id);

    void UpdateUser(UserEntity user);
    UserEntity? GetUserByEmail(string email);
}
