using backend_app.Core.Domain.Entities;

namespace backend_app.Core.Application.Interfaces.Services;

public interface ITokenService
{
    object GenerateToken(UserEntity user);
}
