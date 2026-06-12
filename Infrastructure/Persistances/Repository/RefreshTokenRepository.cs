using Application.Interfaces;
using Application.Interfaces.IRepository;
using Domain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistances.Repository
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ApplicationDbContext _context;

        public RefreshTokenRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(RefreshToken token)
        {
            await _context.RefreshTokens.AddAsync(token);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(RefreshToken token)
        {
            _context.RefreshTokens.Update(token);
            await _context.SaveChangesAsync();
        }

        public async Task<RefreshToken?> GetByHashAsync(string tokenHash)
        {
            return await _context.RefreshTokens
                .FirstOrDefaultAsync(x => x.TokenHash == tokenHash);
        }

        public async Task<List<RefreshToken>> GetActiveTokensByUserIdAsync(Guid userId)
        {
            return await _context.RefreshTokens
                .Where(x => x.AppUserId == userId.ToString() && x.Revoked == null && x.Expires > DateTime.UtcNow)
                .ToListAsync();
        }
    }
}