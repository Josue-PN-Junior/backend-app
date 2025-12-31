using backend_app.Core.Domain.Entities;

namespace backend_app.Core.Application.Interfaces.Repositories;

public interface ITokenPasswordRepository
{
    void Create(TokenPasswordEntity token);
    TokenPasswordEntity? GetByCode(string code);
    void Update(TokenPasswordEntity token);
    void Delete(int id);
}