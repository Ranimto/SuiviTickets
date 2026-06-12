using Application.Models;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> LoginAsync(LoginRequest request);
        Task<AuthResponse> RefreshTokenAsync(string refreshToken);
        Task LogoutAsync(string refreshToken);
        Task<AuthResponse> RegisterAsync(RegisterRequest request);

    }
}