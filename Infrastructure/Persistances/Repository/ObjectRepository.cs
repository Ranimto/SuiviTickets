//using Application.Common;
//using Application.Interfaces.IRepos;
//using Application.Interfaces.IRepository;
//using Domain.Entities;
//using Infrastructure.Persistance.Repository;
//using Microsoft.AspNetCore.Mvc.Filters;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//using Microsoft.IdentityModel.Tokens;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Infrastructure.Persistances.Repository
//{
//    /// <summary>
//    /// User repository 
//    /// </summary>
//    public class UserRepository  : EfRepository<User>, IUserRepository
//    {
//        protected readonly DbContext _context;
//        protected readonly DbSet<User> _dbSet;
//        public UserRepository(DbContext context, ILogger<EfRepository<User>> logger ): base(context, logger)
//        {
//            _context=context;
//            _dbSet = _context.Set<User>();
//        }

//        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
//        {
//            try
//            {
//                if (string.IsNullOrWhiteSpace(email))
//                    throw new ArgumentNullException(nameof(email), "Email cannot be null or empty.");

//                var user = await _dbSet
//                    .AsNoTracking()
//                    .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

//                return user;
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error retrieving {Entity} with email {Email}", typeof(User).Name, email);

//                throw new RepositoryException(
//                    $"An error occurred while retrieving user with email {email}.", ex);
//            }
//        }

//    }
//}
