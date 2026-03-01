using backend_app.Core.Domain.Entities;

namespace backend_app.Core.Application.Interfaces.Repositories;

public interface ITokenPasswordRepository
{
    void SetTokenPassword(TokenPasswordEntity resetToken);
    
    TokenPasswordEntity? GetTokenByUserId(int id);

    void RemoveTokenPassword(TokenPasswordEntity resetToken);
}
