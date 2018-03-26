using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SkillsCub.DataLibrary.Repositories.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        Task Add(TEntity obj);
        Task<TEntity> GetById(Guid id);
        Task<IQueryable<TEntity>> GetAll();
        Task<IQueryable<TEntity>> FindBy(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
        Task Update(TEntity obj);
        Task Remove(Guid id);
        Task<int> SaveChanges();
    }
}
