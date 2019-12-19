using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DiplomaSurvive.Entities
{
    /// <summary>
    /// Repository class.
    /// </summary>
    /// <typeparam name="T">Database entity class.</typeparam>
    /// <seealso cref="DiplomaSurvive.Entities.IRepository{T}" />
    public class Repository<T> : IRepository<T> where T : class
    {
        /// <summary>
        /// The database context
        /// </summary>
        protected readonly DiplomaSurviveDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{T}"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public Repository(DiplomaSurviveDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets asynchronously.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>The list of entities.</returns>
        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).ToListAsync();
        }

        /// <summary>
        /// Lists all asynchronously.
        /// </summary>
        /// <returns>The list of entities.</returns>
        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        /// <summary>
        /// Adds asynchronously.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>The entity.</returns>
        public async Task<T> AddAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);

            await _dbContext.SaveChangesAsync();

            return entity;
        }

        /// <summary>
        /// Updates asynchronously.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>The method is void.</returns>
        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();
        }
        
        /// <summary>
        /// Updates asynchronously.
        /// </summary>
        /// <param name="entities">The entities to update.</param>
        /// <returns>The method is void.</returns>
        public async Task UpdateRangeAsync(T[] entities)
        {
            _dbContext.Set<T>().UpdateRange(entities);;

            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes asynchronously.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <returns>The method is void.</returns>
        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);

            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes asynchronously.
        /// </summary>
        /// <param name="entities">The entities to delete.</param>
        /// <returns>The method is void.</returns>
        public async Task DeleteAsync(T[] entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);

            await _dbContext.SaveChangesAsync();
        }
        
        /// <summary>
        /// Deletes entities.
        /// </summary>
        /// <param name="entities">The entities to delete.</param>
        public void Delete(T[] entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);

            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Counts asynchronously.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>The number of specified entities.</returns>
        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).CountAsync();
        }
    }
}