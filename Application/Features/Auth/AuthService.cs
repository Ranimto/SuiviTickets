using Application.Interfaces;
using Application.Interfaces.IRepos;
using Application.Interfaces.IRepository;
using Application.Models;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System.Data;

namespace Application.Features.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IJwtService _jwtService;
        private readonly IPasswordHasher _passwordHasher;
        public AuthService(
            IUserRepository userRepository,
            IRefreshTokenRepository refreshTokenRepository,
            IJwtService jwtService,
            IPasswordHasher passwordHasher  
            )
        {
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _jwtService = jwtService;
            _passwordHasher = passwordHasher;
        }

        // ===============================
        // 1️⃣ LOGIN 
        // Get User by Email
        // Create an AuthUserModel from the user
        // Create a accessToken by using the AuthUserModel
        // Create a refreshResult by using the AuthUserModel
        // Add refreshTokenEntity to refresh token Table
        // Create an AuthResponse and return it
        // ===============================
        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            User? user = await _userRepository.GetByEmailAsync(request.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid credentials");

            AuthUserModel authUser = new AuthUserModel
            {
                Id = user.Id.ToString(),
                Email = user.Email,
                UserName = user.UserName,
            };
            authUser.Roles.Add(user.Role.ToString());

            var accessToken = _jwtService.GenerateAccessToken(authUser);

            var refreshResult = _jwtService.GenerateRefreshToken();

            var refreshTokenEntity = new RefreshToken
            {
                AppUserId = user.Id.ToString(),
                TokenHash = refreshResult.HashedToken,
                Expires = refreshResult.Expires,
                Created = DateTime.UtcNow,
                IsRevoked = false
            };

            await _refreshTokenRepository.AddAsync(refreshTokenEntity);

            return new AuthResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshResult.Token
            };
        }

        // ===============================
        // 2️⃣ REFRESH TOKEN (Rotation)
        // ===============================
        public async Task<AuthResponse> RefreshTokenAsync(string refreshToken)
        {
            var hashed = _jwtService.HashToken(refreshToken);

            var existingToken =
                await _refreshTokenRepository.GetByHashAsync(hashed);

            if (existingToken == null ||
                existingToken.IsRevoked ||
                existingToken.Expires < DateTime.UtcNow)
                throw new UnauthorizedAccessException("Invalid refresh token");

            // Rotation
            existingToken.IsRevoked = true;
            await _refreshTokenRepository.UpdateAsync(existingToken);

            User? user = await _userRepository.GetByIdAsync(new Guid(existingToken.AppUserId));

            AuthUserModel authUser = new AuthUserModel
            {
                Id = user.Id.ToString(),
                Email = user.Email,
                UserName = user.UserName,
            };
            authUser.Roles.Add(user.Role.ToString());

            var newAccessToken = _jwtService.GenerateAccessToken(authUser);
            var newRefreshResult = _jwtService.GenerateRefreshToken();

            var newRefreshToken = new RefreshToken
            {
                AppUserId = user.Id.ToString(),
                TokenHash = newRefreshResult.HashedToken,
                Expires = newRefreshResult.Expires,
                Created = DateTime.UtcNow,
                IsRevoked = false
            };

            await _refreshTokenRepository.AddAsync(newRefreshToken);

            return new AuthResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshResult.Token
            };
        }

        // ===============================
        // 3️⃣ LOGOUT (Revocation)
        // Hash the refreshToken
        // Get the existingToken by the hashed token if exist 
        // Change the  existingToken's IsRevoked property to true 
        // Update the existingToken
        // ===============================
        public async Task LogoutAsync(string refreshToken)
        {
            var hashed = _jwtService.HashToken(refreshToken);

            var existingToken =
                await _refreshTokenRepository.GetByHashAsync(hashed);

            if (existingToken != null)
            {
                existingToken.IsRevoked = true;
                await _refreshTokenRepository.UpdateAsync(existingToken);
            }
        }


        //public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        //{
        //    // 1️⃣ Vérifier si email existe
        //    var existingUser = await _userRepository.GetByEmailAsync(request.Email);
        //    if (existingUser != null)
        //        throw new Exception("Email already exists");

        //    // 2️⃣ Hasher le mot de passe
        //    var hashedPassword = _passwordHasher.Hash(request.Password);

        //    // 3️⃣ Créer l'utilisateur
        //    var user = new User
        //    {
        //        Id = Guid.NewGuid(),
        //        Email = request.Email,
        //        UserName = request.UserName,
        //        PasswordHash = hashedPassword,
        //        CreatedAt = DateTime.UtcNow
        //    };

        //    // 4️⃣ Ajouter rôle par défaut (ex: User)
        //    user.Role = RoleEnum.User;

        //    await _userRepository.AddUserAsync(user, request.Password);

        //    // 5️⃣ Générer tokens
        //    var authUser = new AuthUserModel
        //    {
        //        Id = user.Id.ToString(),
        //        Email = user.Email,
        //        UserName = user.UserName,
        //    };
        //    authUser.Roles.Add(user.Role.ToString());

        //    var accessToken = _jwtService.GenerateAccessToken(authUser);
        //    var refreshResult = _jwtService.GenerateRefreshToken();

        //    // 6️⃣ Sauvegarder RefreshToken hashé
        //    var refreshToken = new RefreshToken
        //    {
        //        Id = Guid.NewGuid(),
        //        AppUserId = user.Id.ToString(),
        //        TokenHash = refreshResult.HashedToken,
        //        Created = DateTime.UtcNow,
        //        Expires = refreshResult.Expires
        //    };

        //    await _refreshTokenRepository.AddAsync(refreshToken);

        //    // 7️⃣ Retourner tokens
        //    return new AuthResponse
        //    {
        //        AccessToken = accessToken,
        //        RefreshToken = refreshResult.Token
        //    };
        //}

        // Application/Features/Auth/AuthService.cs
        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            // 1️⃣ Vérifier si email existe
            var existingUser = await _userRepository.GetByEmailAsync(request.Email);
            if (existingUser != null)
                throw new Exception("Email already exists");

            // 2️⃣ Créer l'utilisateur (via Infrastructure/UserRepository)
            var newUser = new User // ou AppUser dans l'infra
            {
                Email = request.Email,
                UserName = request.UserName,
                Role = RoleEnum.User
            };

            // On passe le password en clair, Identity hashera
            await _userRepository.AddUserAsync(newUser, request.Password);

            // 3️⃣ Mapper vers AuthUserModel pour la couche Application
            var authUser = new AuthUserModel
            {
                Id = newUser.Id.ToString(),
                Email = newUser.Email,
                UserName = newUser.UserName,
                Roles = new List<string> { "User" }
            };

            // 4️⃣ Générer tokens
            var accessToken = _jwtService.GenerateAccessToken(authUser);
            var refreshResult = _jwtService.GenerateRefreshToken();

            // 5️⃣ Sauvegarder RefreshToken (toujours via Infra)
            var refreshToken = new RefreshToken
            {
                Id = Guid.NewGuid(),
                AppUserId = newUser.Id.ToString(),
                TokenHash = refreshResult.HashedToken,
                Created = DateTime.UtcNow,
                Expires = refreshResult.Expires
            };
            await _refreshTokenRepository.AddAsync(refreshToken);

            // 6️⃣ Retourner tokens
            return new AuthResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshResult.Token
            };
        }



    }
}