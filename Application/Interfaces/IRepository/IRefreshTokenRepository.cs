using Domain.Entities;

namespace Application.Interfaces.IRepository
{
    public interface IRefreshTokenRepository
    {
        Task AddAsync(RefreshToken token);

        Task UpdateAsync(RefreshToken token);

        Task<RefreshToken?> GetByHashAsync(string tokenHash);

        Task<List<RefreshToken>> GetActiveTokensByUserIdAsync(Guid userId);
    }
}