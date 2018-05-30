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
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IBaseEntity
    {
        protected readonly ApplicationDbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(ApplicationDbContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        public virtual async Task Add(TEntity obj)
        {
            await DbSet.AddAsync(obj);
        }

        public virtual async Task<TEntity> GetById(Guid id)
        {
            return await DbSet.AsNoTracking().FirstOrDefaultAsync(entity => entity.Id.Equals(id));
        }

        public virtual async Task<IQueryable<TEntity>> GetAll()
        {
            return DbSet;
        }

        public async Task<IQueryable<TEntity>> FindBy(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            var result= DbSet.Where(predicate);
            if (includes.Any())
            {
                result = includes.Aggregate(result, (current, include) => current.Include(include));
            }

            return result;
        }

        public virtual async Task Update(TEntity obj)
        {
            DbSet.Update(obj);
        }

        public virtual async Task Remove(Guid id)
        {
            DbSet.Remove(DbSet.Find(id));
        }

        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
