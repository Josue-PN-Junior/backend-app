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
        catch (DbUpdateConcurrencyException ex)
        {
            throw new ConcurrencyException($"deleção do usuário ID: {id}");
        }
        catch (DbUpdateException ex)
        {
            throw new DatabaseConnectionException($"deleção do usuário ID: {id}", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new DatabaseConnectionException($"deleção do usuário ID: {id}", ex);
        }
        catch (Exception ex)
        {
            throw new DatabaseOperationException($"deleção do usuário ID: {id}", ex);
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
            throw new DatabaseConnectionException($"busca do usuário ID {id}", ex);
        }
        catch (Exception ex)
        {
            throw new DatabaseOperationException($"busca do usuário ID: {id}", ex);
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
                    throw new DuplicateKeyException("email", user.email);
                }
                throw new DuplicateKeyException("campo único", "valor duplicado");
            }
            
            throw new DatabaseOperationException($"criação do usuário {user.email}", ex);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new ConcurrencyException($"criação do usuário {user.email}");
        }
        catch (DbUpdateException ex)
        {
            throw new DatabaseOperationException($"criação do usuário {user.email}", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new DatabaseConnectionException($"criação do usuário {user.email}", ex);
        }
        catch (Exception ex)
        {
            throw new DatabaseOperationException($"criação do usuário {user.email}", ex);
        }
    }

    public UserEntity? GetUserByEmail(string email)
    {
        try
        {
            return _connection.User.SingleOrDefault(
                u => u.email == email
            );
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("more than one"))
        {
            throw new DatabaseOperationException($"busca por email {email} - dados inconsistentes", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new DatabaseConnectionException($"busca por email {email}", ex);
        }
        catch (Exception ex)
        {
            throw new DatabaseOperationException($"busca por email {email}", ex);
        }

    }
}
