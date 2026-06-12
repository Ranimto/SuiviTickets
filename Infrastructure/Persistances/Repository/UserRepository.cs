using Application.Interfaces.IRepository;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Mapster;
using Application.Common;
using Application.Interfaces.IRepos;
using Infrastructure.Identity;

namespace Infrastructure.Persistence.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<UserRepository> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        // -------------------- GET BY ID --------------------
        public async Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                AppUser appUser = await _userManager.FindByIdAsync(id.ToString());
                if (appUser == null)
                    throw new RepositoryException($"User with ID {id} not found.");

                IList<string> roles = await _userManager.GetRolesAsync(appUser);
                appUser.Roles = roles; // temporaire pour Mapster

                return appUser.Adapt<User>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving User with id {Id}", id);
                throw new RepositoryException("An error occurred while retrieving data.", ex);
            }
        }

        // -------------------- GET ALL --------------------
        public async Task<List<User>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var users = _userManager.Users.ToList();
                var domainUsers = new List<User>();

                foreach (var appUser in users)
                {
                    var roles = await _userManager.GetRolesAsync(appUser);
                    appUser.Roles = roles;
                    domainUsers.Add(appUser.Adapt<User>());
                }

                return domainUsers;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all users");
                throw new RepositoryException("An error occurred while retrieving data.", ex);
            }
        }

        // -------------------- ADD --------------------
        public async Task<User> AddUserAsync(User user, string password,CancellationToken cancellationToken = default)
        {
            try
            {
                var appUser = user.Adapt<AppUser>();

                //create user with a hash password
                var result = await _userManager.CreateAsync(appUser, password);
                if (!result.Succeeded)
                    throw new RepositoryException(string.Join(", ", result.Errors.Select(e => e.Description)));

                //Assigned role to user
                if (!string.IsNullOrEmpty(user.Role.ToString()))
                {
                    if (!await _roleManager.RoleExistsAsync(user.Role.ToString()))
                        await _roleManager.CreateAsync(new IdentityRole(user.Role.ToString()));

                    await _userManager.AddToRoleAsync(appUser, user.Role.ToString());
                }

                return user;
            }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding user");
                throw new RepositoryException("An error occurred while adding data.", ex);
            }
        }

        // -------------------- UPDATE --------------------
        public async Task<User> UpdateAsync(User user, CancellationToken cancellationToken = default)
        {
            try
            {
                var appUser = await _userManager.FindByIdAsync(user.Id.ToString());
                if (appUser == null)
                    throw new RepositoryException($"User with ID {user.Id} not found.");

                appUser.Email = user.Email;
                appUser.UserName = user.Email;

                var result = await _userManager.UpdateAsync(appUser);
                if (!result.Succeeded)
                    throw new RepositoryException(string.Join(", ", result.Errors.Select(e => e.Description)));

                // update role
                var currentRoles = await _userManager.GetRolesAsync(appUser);
                if (!currentRoles.Contains(user.Role.ToString()))
                {
                    await _userManager.RemoveFromRolesAsync(appUser, currentRoles);
                    if (!await _roleManager.RoleExistsAsync(user.Role.ToString()))
                        await _roleManager.CreateAsync(new IdentityRole(user.Role.ToString()));
                    await _userManager.AddToRoleAsync(appUser, user.Role.ToString());
                }

                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user");
                throw new RepositoryException("An error occurred while updating data.", ex);
            }
        }

        // -------------------- DELETE BY ID --------------------
        public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                var appUser = await _userManager.FindByIdAsync(id.ToString());
                if (appUser == null)
                    throw new RepositoryException($"User with ID {id} not found.");

                var result = await _userManager.DeleteAsync(appUser);
                if (!result.Succeeded)
                    throw new RepositoryException(string.Join(", ", result.Errors.Select(e => e.Description)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting user with id {Id}", id);
                throw new RepositoryException("An error occurred while deleting data.", ex);
            }
        }

        // -------------------- DELETE ALL --------------------
        public async Task DeleteAllAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var users = _userManager.Users.ToList();
                foreach (var user in users)
                    await _userManager.DeleteAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting all users");
                throw new RepositoryException("An error occurred while deleting all data.", ex);
            }
        }

        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                    throw new ArgumentNullException(nameof(email), "Email cannot be null or empty.");

                var appUser = await _userManager.FindByEmailAsync(email);

                // Cas normal : utilisateur non trouvé
                if (appUser == null)
                    return null;

                var roles = await _userManager.GetRolesAsync(appUser);

                // Assigner temporairement pour Mapster
                appUser.Roles = roles;

                return appUser.Adapt<User>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Error retrieving {Entity} with email {Email}",
                    nameof(User),
                    email);

                throw new RepositoryException(
                    $"An error occurred while retrieving user with email {email}.", ex);
            }
        }

        public Task<User> AddAsync(User entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
