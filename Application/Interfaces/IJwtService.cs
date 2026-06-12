using Application.Models;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateAccessToken(AuthUserModel authUser);
        RefreshTokenResult GenerateRefreshToken();
        string HashToken(string token);
    }
}


