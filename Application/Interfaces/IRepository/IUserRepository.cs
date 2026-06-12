using Application.Interfaces.IRepository;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepos
{
    /// <summary>
    /// User repository
    /// </summary>
    public interface IUserRepository : IRepository<User>
    {

        Task<User> AddUserAsync(User user, string password, CancellationToken cancellationToken = default);
        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    }
}
