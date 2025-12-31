using backend_app.Helpers.Exceptions;
using backend_app.Models.Generic.DTOs;
using backend_app.Models.TokenPassword;
using backend_app.Models.TokenPassword.DTOs;
using backend_app.Models.User;
using backend_app.Models.User.DTOs;
using backend_app.Repositories.Interface;
using backend_app.Services.Interface;
using static backend_app.Helpers.Exceptions.CustomizedExceptions;

namespace backend_app.Services.Implementation;

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

    public object UserLogin(string email, string password)
    {
        var user = repository.GetUserByEmail(email)
            ?? throw new LoginExceptions.UserNotFoundException($"Email: {email}");

        if (user.password != password) throw new LoginExceptions.InvalidCredentialsException($"Senha incorreta para email: {email}");

        var token = tokenService.GenerateToken(user)
            ?? throw new LoginExceptions.TokenFailGenerationException();

        return token;
    }

    public void ChangeEmail(EmailChangeDTO data)
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
            var code = new Random().Next(1000, 9999).ToString();

            tokenRepository.SetTokenPassword(
                new TokenPasswordEntity(
                    token: token,
                    expiration: expiration,
                    code: code,
                    userId: _user.id
                )
            );
        }
    }

    public void VerifyCodeReset(VerifyResetCodeDTO data)
    {
        var _user = repository.GetUserByEmail(data.Email)
            ?? throw new UserCodeNotFoundException($"Email: {data.Email}");

        var token = tokenRepository.GetTokenByUserId(_user.id)
            ?? throw new CodeNotFoundException($"UserId: {_user.id}");

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

        var token = tokenRepository.GetTokenByUserId(_user.id)
            ?? throw new CodeNotFoundException($"UserId: {_user.id}");

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
