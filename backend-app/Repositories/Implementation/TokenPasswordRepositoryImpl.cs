using backend_app.Models.TokenPassword;
using backend_app.Repositories.Interface;
using static backend_app.Helpers.Exceptions.DatabaseExceptions;

namespace backend_app.Repositories.Implementation;

public class TokenPasswordRepositoryImpl : ITokenPasswordRepository
{
    private readonly ConnectionDb _connection;

    public TokenPasswordRepositoryImpl(ConnectionDb connection)
    {
        _connection = connection ?? throw new ArgumentNullException(nameof(connection));
    }
    public void SetTokenPassword(TokenPasswordEntity resetToken)
    {
        try
        {
            var existingTokens = _connection.ResetToken.Where(t => t.UserId == resetToken.UserId).ToList();
            if (existingTokens.Any())
            {
                _connection.ResetToken.RemoveRange(existingTokens);
            }

            _connection.ResetToken.Add(resetToken);
            _connection.SaveChanges();
        }
        catch (Exception ex)
        {
            throw new DatabaseOperationException($"crição de resetToken {resetToken}", ex);
        }

    }

    public TokenPasswordEntity? GetTokenByUserId(int id)
    {
        try
        {
            return _connection.ResetToken.SingleOrDefault(
                u => u.UserId == id
            );
        }
        catch (Exception ex)
        {
            throw new DatabaseOperationException($"encontrar token {id}", ex);
        }
    }

    public void RemoveTokenPassword(TokenPasswordEntity resetToken)
    {
        try
        {
            _connection.ResetToken.Remove(resetToken);
            _connection.SaveChanges();
        }
        catch (Exception ex)
        {
            throw new DeleteCodeException($"excluir token {resetToken.Id}", ex);
        }
    }
}
