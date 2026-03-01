using backend_app.Core.Domain.Entities;

namespace backend_app.Core.Application.Interfaces.Repositories;

public interface IUserRepository
{
    void SetUser(UserEntity user);

    UserEntity? GetUserById(int id);

    void DeleteUserById(int id);

    void UpdateUser(UserEntity user);
    UserEntity? GetUserByEmail(string email);
}
