
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend_app.Core.Application.Interfaces.Services;
using backend_app.Core.Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace backend_app.Core.Application.Services.Implementation;

public class TokenService : ITokenService
{
    public object GenerateToken(UserEntity user)
    {
        var key = Encoding.ASCII.GetBytes("issodeveserumachavedesegurancatopdemias123456789");
        var tokenConfig = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim("userId", user.Id.ToString()),
                new Claim("email", user.Email),
            ]),
            Expires = DateTime.UtcNow.AddHours(3),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenConfig);
        var tokenString = tokenHandler.WriteToken(token);


        return new
        {
            token = tokenString
        };
    }
}
