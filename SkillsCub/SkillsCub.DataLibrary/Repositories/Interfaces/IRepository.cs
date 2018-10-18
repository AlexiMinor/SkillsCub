using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SkillsCub.DataLibrary.Repositories.Interfaces
{
    /// <summary>
    /// The repository.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task Add(TEntity obj);

        /// <summary>
        /// The get by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetById(Guid id);

        /// <summary>
        /// The get all.
        /// </summary>
        /// <returns></returns>
        Task<IQueryable<TEntity>> GetAll();
        /// <summary>
        /// The find by.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        Task<IQueryable<TEntity>> FindBy(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        Task Update(TEntity obj);

        /// <summary>
        /// The remove.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Remove(Guid id);

        /// <summary>
        /// The save changes.
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChanges();
    }
}
