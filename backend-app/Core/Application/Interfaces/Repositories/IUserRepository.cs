using backend_app.Core.Domain.Entities;

namespace backend_app.Core.Application.Interfaces.Repositories;

public interface IUserRepository
{
    UserEntity? GetById(int id);
    UserEntity? GetByEmail(string email);
    void Create(UserEntity user);
    void Update(UserEntity user);
    void Delete(int id);
    bool EmailExists(string email);
}