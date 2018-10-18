using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SkillsCub.DataLibrary.Entities.Implementation;
using SkillsCub.DataLibrary.Repositories.Context;
using SkillsCub.DataLibrary.Repositories.Interfaces;

namespace SkillsCub.DataLibrary.Repositories.Implementation
{
    /// <summary>
    /// The repository.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IBaseEntity
    {
        protected readonly ApplicationDbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        /// <summary>
        /// The repository designer.
        /// </summary>
        /// <param name="context"></param>
        public Repository(ApplicationDbContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        /// <summary>
        /// The add method.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual async Task Add(TEntity obj)
        {
            await DbSet.AddAsync(obj);
        }

        /// <summary>
        /// The get by id method.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetById(Guid id)
        {
            return await DbSet.AsNoTracking().FirstOrDefaultAsync(entity => entity.Id.Equals(id));
        }

        /// <summary>
        /// The get all method.
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IQueryable<TEntity>> GetAll()
        {
            return DbSet;
        }

        /// <summary>
        /// The find by method.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        public async Task<IQueryable<TEntity>> FindBy(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            var result= DbSet.Where(predicate);
            if (includes.Any())
            {
                result = includes.Aggregate(result, (current, include) => current.Include(include));
            }

            return result;
        }

        /// <summary>
        /// The update method.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual async Task Update(TEntity obj)
        {
            DbSet.Update(obj);
        }

        /// <summary>
        /// The remove method.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task Remove(Guid id)
        {
            DbSet.Remove(DbSet.Find(id));
        }

        /// <summary>
        /// The save changes method.
        /// </summary>
        /// <returns></returns>
        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }

        /// <summary>
        /// The dispose method.
        /// </summary>
        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
