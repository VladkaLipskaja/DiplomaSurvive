using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DiplomaSurvive.Entities
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Adds asynchronously.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>The entity.</returns>
        Task<T> AddAsync(T entity);

        /// <summary>
        /// Updates asynchronously.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>The method is void.</returns>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Deletes asynchronously.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <returns>The method is void.</returns>
        Task DeleteAsync(T entity);

        /// <summary>
        /// Deletes asynchronously.
        /// </summary>
        /// <param name="entities">The entities to delete.</param>
        /// <returns>The entity.</returns>
        /// <returns>The method is void.</returns>
        Task DeleteAsync(T[] entities);

        void Delete(T[] entities);

        /// <summary>
        /// Lists all asynchronously.
        /// </summary>
        /// <returns>The list of entities.</returns>
        Task<IReadOnlyList<T>> ListAllAsync();

        /// <summary>
        /// Counts asynchronously.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>The number of specified entities.</returns>
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Gets asynchronously.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>The list of entities.</returns>
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);
    }
}