using backend_app.Models.User;

namespace backend_app.Services.Interface;

public interface ITokenService
{
    object GenerateToken(UserEntity user);
}
