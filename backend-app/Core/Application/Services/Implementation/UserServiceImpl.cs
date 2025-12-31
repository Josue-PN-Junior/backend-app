using backend_app.Core.Application.DTOs.TokenPassword;
using backend_app.Core.Application.DTOs.User;
using backend_app.Core.Application.Interfaces.Repositories;
using backend_app.Core.Application.Interfaces.Services;
using backend_app.Core.Domain.Entities;
using backend_app.Helpers.Exceptions;
using static backend_app.Helpers.Exceptions.CustomizedExceptions;

namespace backend_app.Core.Application.Services.Implementation;

public class UserServiceImpl : IUserService
{
    private readonly IUserRepository repository;
    private readonly ITokenPasswordRepository tokenRepository;

    private readonly ITokenService tokenService;
    public UserServiceImpl(IUserRepository repository, ITokenPasswordRepository tokenRepository, ITokenService tokenService)
    {
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        this.tokenRepository = tokenRepository ?? throw new ArgumentNullException(nameof(tokenRepository));
        this.tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
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
            Id = user.Id,
            FullName = user.FullName,
            Nickname = user.Nickname,
            Email = user.Email
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

    public object UserLogin(string email, string password)
    {
        var user = repository.GetUserByEmail(email)
            ?? throw new LoginExceptions.UserNotFoundException($"Email: {email}");

        if (user.Password != password) throw new LoginExceptions.InvalidCredentialsException($"Incorrect password for email: {email}");

        var token = tokenService.GenerateToken(user)
            ?? throw new LoginExceptions.TokenFailGenerationException();

        return token;
    }

    public void ChangeEmail(EmailChangeDTO data)
    {
        var _user = repository.GetUserByEmail(data.Email)
            ?? throw new LoginExceptions.UserNotFoundException($"Email: {data.Email}");

        if (_user.Password != data.Password) throw new LoginExceptions.InvalidCredentialsException($"Incorrect password for email: {data.Email}");

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
            var code = new Random().Next(1000, 9999).ToString();

            tokenRepository.SetTokenPassword(
                new TokenPasswordEntity(
                    token: token,
                    expiration: expiration,
                    code: code,
                    userId: _user.Id
                )
            );
        }
    }

    public void VerifyCodeReset(VerifyResetCodeDTO data)
    {
        var _user = repository.GetUserByEmail(data.Email)
            ?? throw new UserCodeNotFoundException($"Email: {data.Email}");

        var token = tokenRepository.GetTokenByUserId(_user.Id)
            ?? throw new CodeNotFoundException($"UserId: {_user.Id}");

        if (token.IsExpired)
        {
            tokenRepository.RemoveTokenPassword(token);
            throw new ExpiredCodeException($"Code: {data.Code}");
        }

        if (!token.Code.Equals(data.Code)) throw new InvalidCodeException($"Code: {data.Code}");

    }

    public void ResetPassword(ResetPasswordDTO data)
    {
        var _user = repository.GetUserByEmail(data.Email)
            ?? throw new UserCodeNotFoundException($"Email: {data.Email}");

        var token = tokenRepository.GetTokenByUserId(_user.Id)
            ?? throw new CodeNotFoundException($"UserId: {_user.Id}");

        if (token.IsExpired)
        {
            tokenRepository.RemoveTokenPassword(token);
            throw new ExpiredCodeException($"Email: {data.Email}");
        }

        if (!token.Code.Equals(data.Code)) throw new InvalidCodeException($"Code: {data.Code}");

        _user.UpdatePassword(data.NewPassword);

        try
        {
            repository.UpdateUser(_user);
        }
        catch (Exception)
        {
            throw;
        }

        try
        {
            tokenRepository.RemoveTokenPassword(token);

        }
        catch (Exception)
        {
        }
    }
}
