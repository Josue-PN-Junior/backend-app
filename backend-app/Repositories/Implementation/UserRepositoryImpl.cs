using backend_app.Models.User;
using backend_app.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using static backend_app.Helpers.Exceptions.DatabaseExceptions;

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
        try
        {
            var user = _connection.User.Find(id);

            if (user is not null)
            {
                _connection.User.Remove(user);
                _connection.SaveChanges();
            }
        }
        catch (DbUpdateConcurrencyException)
        {
            throw new ConcurrencyException($"Deletion of user ID: {id}");
        }
        catch (DbUpdateException ex)
        {
            throw new DatabaseConnectionException($"Deletion of user ID:{id}", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new DatabaseConnectionException($"Deletion of user ID: {id}", ex);
        }
        catch (Exception ex)
        {
            throw new DatabaseOperationException($"Deletion of user ID: {id}", ex);
        }

    }

    public UserEntity? GetUserById(int id)
    {
        try
        {
            return _connection.User.Find(id);
        }
        catch (InvalidOperationException ex)
        {
            throw new DatabaseConnectionException($"Search for user ID. {id}", ex);
        }
        catch (Exception ex)
        {
            throw new DatabaseOperationException($"Search for user ID.: {id}", ex);
        }

    }

    public void SetUser(UserEntity user)
    {
        try
        {
            _connection.User.Add(user);
            _connection.SaveChanges();
        }
        catch (DbUpdateException ex) when (ex.InnerException is PostgresException pgEx)
        {

            // Código 23505 = unique_violation no PostgreSQL
            if (pgEx.SqlState == "23505")
            {
                // Verificar qual campo causou o erro
                if (pgEx.ConstraintName?.Contains("email") == true)
                {
                    throw new DuplicateKeyException("email", user.Email);
                }
                throw new DuplicateKeyException("unique field", "duplicate value");
            }

            throw new DatabaseOperationException($"creation of user  {user.Email}", ex);
        }
        catch (DbUpdateConcurrencyException)
        {
            throw new ConcurrencyException($"creation of user {user.Email}");
        }
        catch (DbUpdateException ex)
        {
            throw new DatabaseOperationException($"creation of user {user.Email}", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new DatabaseConnectionException($"creation of user {user.Email}", ex);
        }
        catch (Exception ex)
        {
            throw new DatabaseOperationException($"creation of user {user.Email}", ex);
        }
    }

    public UserEntity? GetUserByEmail(string email)
    {
        try
        {
            return _connection.User.SingleOrDefault(
                u => u.Email == email
            );
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("more than one"))
        {
            throw new DatabaseOperationException($"search by email {email} - inconsistent data", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new DatabaseConnectionException($"search by email {email}", ex);
        }
        catch (Exception ex)
        {
            throw new DatabaseOperationException($"search by email {email}", ex);
        }

    }

    public void UpdateUser(UserEntity user)
    {
        try
        {
            _connection.User.Update(user);
            _connection.SaveChanges();
        }
        catch (DbUpdateException ex) when (ex.InnerException is PostgresException pgEx)
        {
            // Código 23505 = unique_violation no PostgreSQL
            if (pgEx.SqlState == "23505")
            {
                // Verificar qual campo causou o erro
                if (pgEx.ConstraintName?.Contains("email") == true)
                {
                    throw new DuplicateKeyException("email", user.Email);
                }
                throw new DuplicateKeyException("unique field", "duplicate value");
            }

            throw new DatabaseOperationException($"update of user ID: {user.Id}", ex);
        }
        catch (DbUpdateConcurrencyException)
        {
            throw new ConcurrencyException($"update of user ID: {user.Id}");
        }
        catch (DbUpdateException ex)
        {
            throw new DatabaseOperationException($"update of user ID: {user.Id}", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new DatabaseConnectionException($"update of user ID: {user.Id}", ex);
        }
        catch (Exception ex)
        {
            throw new DatabaseOperationException($"update of user ID: {user.Id}", ex);
        }
    }
}
