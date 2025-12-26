using backend_app.Models.User;
using backend_app.Repositories.Interface;
using backend_app.Services.Interface;

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
                fullName: user.fullName,
                nickname: user.nickname,
                email: user.email,
                password: user.password
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
            id = user.id,
            fullName = user.fullName,
            nickname = user.nickname,
            email = user.email
        };

    }
}
