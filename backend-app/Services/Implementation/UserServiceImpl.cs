using backend_app.Models.User;
using backend_app.Models.User.DTOs;
using backend_app.Repositories.Interface;
using backend_app.Services.Interface;
using static backend_app.Helpers.Exceptions.CustomizedExceptions;

namespace backend_app.Services.Implementation;

public class UserServiceImpl : IUserService
{
    private readonly IUserRepository repository;

    public UserServiceImpl(IUserRepository repository)
    {
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public void CreatUser(UserCreateDTO user)
    {
        var userEntity = new UserEntity(
                fullName: user.FullName,
                nickname: user.Nickname,
                email: user.Email,
                password: user.Password
        );

        repository.SetUser(userEntity);
    }

    public void DeleteUserById(int id)
    {
        repository.DeleteUserById(id);
    }

    public UserDTO? GetUserById(int id)
    {
        var user = repository.GetUserById(id);
        if (user is null) return null;

        return new UserDTO
        {
            Id = user.id,
            FullName = user.fullName,
            Nickname = user.nickname,
            Email = user.email
        };

    }

    public string? UserLogin(string email, string password)
    {
        var user = repository.GetUserByEmail(email);

        if (user is null) throw new InvalidCredentialsException("Verifique os dados");

        if (user.password != password) throw new InvalidCredentialsException("Verifique os dados");

        return "Logado";
    }
}
