using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Witcherius.Infrastructure
{
    public interface IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        /// <summary>
        /// Gets the entity with given id.
        /// </summary>
        Task<TEntity> GetAsync(Guid id);
        
        /// <summary>
        /// Gets the entity with given id.
        /// </summary>
        Task<TEntity> GetAsync(Guid id, params string[] includes);

        /// <summary>
        /// Persists the given entity.
        /// </summary>
        Guid Create(TEntity entity);

        /// <summary>
        /// Updates the given entity.
        /// </summary>
        void Update(TEntity entity);

        /// <summary>
        /// Deletes an entity with the given id.
        /// </summary>
        void Delete(Guid id);

        /// <summary>
        /// Gets all entities.
        /// </summary>
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Gets entities with given expression
        /// </summary>
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression);
    }
}
