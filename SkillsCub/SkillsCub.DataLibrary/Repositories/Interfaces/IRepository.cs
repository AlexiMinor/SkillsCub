using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SkillsCub.DataLibrary.Repositories.Interfaces
{
    /// <summary>
    /// The repository.
    /// </summary>
    /// <typeparam name="TEntity">The entity object.</typeparam>
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="obj">The entity object.</param>
        /// <returns>Retyrn database after adding.</returns>
        Task Add(TEntity obj);

        /// <summary>
        /// The get by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>Return object from database by id.</returns>
        Task<TEntity> GetById(Guid id);

        /// <summary>
        /// The get all.
        /// </summary>
        /// <returns>Return all objects from database.</returns>
        Task<IQueryable<TEntity>> GetAll();

        /// <summary>
        /// The find by.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <param name="includes">the incluudes.</param>
        /// <returns>Return object from database.</returns>
        Task<IQueryable<TEntity>> FindBy(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="obj">The entity object.</param>
        /// <returns>Retunr database after update.</returns>
        Task Update(TEntity obj);

        /// <summary>
        /// The remove.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>Return database after remove.</returns>
        Task Remove(Guid id);

        /// <summary>
        /// The save changes.
        /// </summary>
        /// <returns>Return database after save changes.</returns>
        Task<int> SaveChanges();
    }
}
