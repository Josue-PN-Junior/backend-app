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
            _connection.ResetToken.Add(resetToken);
            _connection.SaveChanges();
        }
        catch (Exception ex)
        {
            throw new DatabaseOperationException($"crição de resetToken{resetToken}", ex);
        }

    }
}
