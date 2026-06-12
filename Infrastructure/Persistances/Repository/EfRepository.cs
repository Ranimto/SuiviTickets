using Application.Common;
using Application.Interfaces.IRepository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistance.Repository
{
    public class EfRepository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;
        protected readonly ILogger<EfRepository<T>> _logger;

        public EfRepository(DbContext context, ILogger<EfRepository<T>> logger)
        {
            _context = context;
            _dbSet = _context.Set<T>();
            _logger = logger;
        }

        public async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                T? entity = await _dbSet.FindAsync(new Guid[] { id }, cancellationToken);
                if (entity == null)
                    throw new RepositoryException($"{typeof(T).Name} with ID {id} not found.");

                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving {Entity} with id {Id}", typeof(T).Name, id);
                throw new RepositoryException("An error occurred while retrieving data.", ex);
            }
        }

        public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                //Inactive the tracking change by AsNoTracking to gain performance
                return await _dbSet.AsNoTracking().ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all entities of type {Entity}", typeof(T).Name);
                throw new RepositoryException("An error occurred while retrieving data.", ex);
            }
        }

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                await _dbSet.AddAsync(entity, cancellationToken);
                await SaveChangesAsync(cancellationToken);

                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding entity of type {Entity}", typeof(T).Name);
                throw new RepositoryException("An error occurred while adding data.", ex);
            }
        }

        public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                _dbSet.Update(entity);
                await SaveChangesAsync(cancellationToken);

                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating entity of type {Entity}", typeof(T).Name);
                throw new RepositoryException("An error occurred while updating data.", ex);
            }
        }

        public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                var entity = await GetByIdAsync(id, cancellationToken);

                _dbSet.Remove(entity);

                await SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting entity of type {Entity} with id {Id}", typeof(T).Name, id);
                throw new RepositoryException("An error occurred while deleting data.", ex);
            }
        }

        public async Task DeleteAllAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var allEntities = await GetAllAsync(cancellationToken);

                _dbSet.RemoveRange(allEntities);

                await SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting all entities of type {Entity}", typeof(T).Name);
                throw new RepositoryException("An error occurred while deleting all data.", ex);
            }
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Database update error for entity {Entity}", typeof(T).Name);
                throw new RepositoryException("A database error occurred while saving changes.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while saving changes for entity {Entity}", typeof(T).Name);
                throw new RepositoryException("An unexpected error occurred while saving changes.", ex);
            }
        }

    }
}
