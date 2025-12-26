using backend_app.Models.User;
using backend_app.Repositories.Interface;

namespace backend_app.Repositories.Implementation;

public class UserRepositoryImpl : IUserRepository
{
    private readonly ConnectionDb _connection;

    public UserRepositoryImpl(ConnectionDb connection)
    {
        _connection = connection ?? throw new ArgumentNullException(nameof(connection));
    }

    public void DeleteUserById(int id)
    {
        var user = _connection.User.Find(id);

        if (user is not null)
        {
            _connection.User.Remove(user);
            _connection.SaveChanges();
        } 
        
    }

    public UserEntity? GetUserById(int id)
    {
        return _connection.User.Find(id);
    }

    public void SetUser(UserEntity user)
    {
        _connection.User.Add(user);
        _connection.SaveChanges();
    }
}
