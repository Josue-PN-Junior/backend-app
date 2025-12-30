using backend_app.Models.TokenPassword;

namespace backend_app.Repositories.Interface;

public interface ITokenPasswordRepository
{
    void SetTokenPassword(TokenPasswordEntity resetToken);
    
    TokenPasswordEntity? GetTokenByUserId(int id);

    void RemoveTokenPassword(TokenPasswordEntity resetToken);
}
