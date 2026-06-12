using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuthentificationAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // ===============================
        // LOGIN
        // ===============================
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _authService.LoginAsync(request);
            return Ok(result);
        }

        // ===============================
        // REFRESH
        // ===============================
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] string refreshToken)
        {
            var result = await _authService.RefreshTokenAsync(refreshToken);
            return Ok(result);
        }

        // ===============================
        // LOGOUT
        // ===============================
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] string refreshToken)
        {
            await _authService.LogoutAsync(refreshToken);
            return NoContent();
        }
        // ===============================
        // REGISTER
        // ===============================
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var result = await _authService.RegisterAsync(request);
            return Ok(result);
        }

    }
}