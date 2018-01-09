using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FoxTalesCMS.DataLibrary.Entities.Base;

namespace FoxTalesCMS.DataLibrary.Interfaces
{
    public interface IRepositorySave: IRepository
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
    public interface IRepository
    {
        IQueryable<TEntity> GetAll<TEntity>(
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        int? skip = null,
        int? take = null,
        IEnumerable<string> includes = null)
        where TEntity : class;

        Task<IQueryable<TEntity>> GetAllAsync<TEntity>(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
        IEnumerable<string> includes = null)
            where TEntity : class;

        IEnumerable<TEntity> Get<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
        IEnumerable<string> includes = null)
            where TEntity : class;

        Task<IEnumerable<TEntity>> GetAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
        IEnumerable<string> includes = null)
            where TEntity : class;
        TEntity GetOne<TEntity>(
        Expression<Func<TEntity, bool>> filter = null,
        IEnumerable<string> includes = null)
        where TEntity : class;

        Task<TEntity> GetOneAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
        IEnumerable<string> includes = null)
            where TEntity : class;

        Task<TEntity> GetFirstAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            IEnumerable<string> includes = null)
            where TEntity : class;

       TEntity GetById<TEntity>(object id,
        IEnumerable<string> includes = null)
        where TEntity : class;

        Task<TEntity> GetByIdAsync<TEntity>(object id,
        IEnumerable<string> includes = null)
            where TEntity : class;

        int GetCount<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class;

        Task<int> GetCountAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class;

        bool GetExists<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class;

        Task<bool> GetExistsAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class;

      
        /*
        void Create<TEntity>(TEntity entity)
        where TEntity : class;

        void Update<TEntity>(TEntity entity)
            where TEntity : class;
        
        void Delete<TEntity>(object id)
            where TEntity : class;

        void Delete<TEntity>(TEntity entity)
            where TEntity : class;
        */
    }
}
