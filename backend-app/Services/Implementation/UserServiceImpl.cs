using backend_app.Helpers.Exceptions;
using backend_app.Models.Generic.DTOs;
using backend_app.Models.TokenPassword;
using backend_app.Models.User;
using backend_app.Models.User.DTOs;
using backend_app.Repositories.Interface;
using backend_app.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using static backend_app.Helpers.Exceptions.CustomizedExceptions;

namespace backend_app.Services.Implementation;

public class UserServiceImpl : IUserService
{
    private readonly IUserRepository repository;
    private readonly ITokenPasswordRepository tokenRepository;

    public UserServiceImpl(IUserRepository repository, ITokenPasswordRepository tokenRepository)
    {
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        this.tokenRepository = tokenRepository ?? throw new ArgumentNullException(nameof(tokenRepository));
    }

    public void CreateUser(UserCreateDTO user)
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

    public void UpdateUserById(int id, UserUpdateDTO user)
    {
        var _user = repository.GetUserById(id)
            ?? throw new UserNotFoundException($"Id: {id}");

        _user.UpdateUserNameAndNickname(
            _fullName: user.FullName,
            _nickname: user.Nickname
        );

        repository.UpdateUser(_user);
    }

    public string UserLogin(string email, string password)
    {
        var user = repository.GetUserByEmail(email)
            ?? throw new LoginExceptions.UserNotFoundException($"Email: {email}");

        if (user.password != password) throw new LoginExceptions.InvalidCredentialsException($"Senha incorreta para email: {email}");

        return $"Logado com {email}";
    }

    public void ChangeEmail([FromBody] EmailChangeDTO data)
    {
        var _user = repository.GetUserByEmail(data.Email)
            ?? throw new LoginExceptions.UserNotFoundException($"Email: {data.Email}");

        if (_user.password != data.Password) throw new LoginExceptions.InvalidCredentialsException($"Senha incorreta para email: {data.Email}");

        _user.UpdateUserEmail(
            data.NewEmail
        );

        repository.UpdateUser(_user);
    }

    public void RequestResetPassword(EmailDTO email)
    {
        var _user = repository.GetUserByEmail(email.Email);

        if (_user != null)
        {
            var token = Guid.NewGuid().ToString();
            var expiration = DateTime.UtcNow.AddMinutes(10);

            tokenRepository.SetTokenPassword(
                new TokenPasswordEntity(
                    token: token,
                    expiration: expiration,
                    userId: _user.id
                )
            );
        }
    }
}
