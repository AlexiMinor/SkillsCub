using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FoxTalesCMS.DataLibrary.Entities;
using FoxTalesCMS.DataLibrary.Entities.Base;
using FoxTalesCMS.DataLibrary.Interfaces;
using Microsoft.EntityFrameworkCore;
using FoxTalesCMS.Utilities.Static;

namespace FoxTalesCMS.DataLibrary.Implementation
{
    public class EFRepository : IRepository
    {
        protected readonly FoxTalesDBContext _context;
        public EFRepository(FoxTalesDBContext context)
        {
            _context = context;
        }

        protected virtual IQueryable<TEntity> GetQueryable<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            IEnumerable<string> includes = null)
            where TEntity : class, IEntity
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            query = query.Where(e => e.IsActive);

            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip !=null)
            {
                query = query.Skip(skip.Value);
            }

            if (take != null)
            {
                query = query.Take(take.Value);
            }

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            

            return query;
        }

        public virtual IQueryable<TEntity> GetAll<TEntity>(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            IEnumerable<string> includes = null)
            where TEntity : class, IEntity
        {
            return GetQueryable<TEntity>(null, orderBy, skip, take, includes);
        }

        public virtual async Task<IQueryable<TEntity>> GetAllAsync<TEntity>(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            IEnumerable<string> includes = null)
            where TEntity : class, IEntity
        {
            return GetQueryable<TEntity>(null, orderBy, skip, take, includes);
        }

        public virtual IEnumerable<TEntity> Get<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            IEnumerable<string> includes = null)
            where TEntity : class, IEntity
        {
            return GetQueryable<TEntity>(filter, orderBy, skip, take, includes).ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            IEnumerable<string> includes = null)
            where TEntity : class, IEntity
        {
            return await GetQueryable<TEntity>(filter, orderBy, skip, take, includes).ToListAsync();
        }

        public virtual TEntity GetOne<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            IEnumerable<string> includes = null)
            where TEntity : class, IEntity
        {
            return GetQueryable<TEntity>(filter, null, null, null, includes).SingleOrDefault();
        }

        public virtual async Task<TEntity> GetOneAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            IEnumerable<string> includes = null)
            where TEntity : class, IEntity
        {
            return await GetQueryable<TEntity>(filter, null, null, null, includes).SingleOrDefaultAsync();
        }

        public virtual TEntity GetFirst<TEntity>(
           Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            IEnumerable<string> includes = null)
           where TEntity : class, IEntity
        {
            return GetQueryable<TEntity>(filter, orderBy, null, null, includes).FirstOrDefault();
        }

        public virtual async Task<TEntity> GetFirstAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            IEnumerable<string> includes = null)
            where TEntity : class, IEntity
        {
            return await GetQueryable<TEntity>(filter, orderBy, null, null, includes).FirstOrDefaultAsync();
        }

        public virtual TEntity GetById<TEntity>(object id,
            IEnumerable<string> includes = null)
            where TEntity : class, IEntity
        {
            var filter = PredicateBuilder.True<TEntity>();
            filter = filter.And(e => (int)e.Id == (int)id);
            return GetQueryable<TEntity>(filter, null, null, null, includes).FirstOrDefault();
        }

        public virtual async Task<TEntity> GetByIdAsync<TEntity>(object id,
            IEnumerable<string> includes = null)
            where TEntity : class, IEntity
        {
            var filter = PredicateBuilder.True<TEntity>();
            filter = filter.And(e => (int)e.Id == (int)id);
            return await GetQueryable<TEntity>(filter, null, null, null, includes).FirstOrDefaultAsync();
        }

        public virtual int GetCount<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IEntity
        {
            return GetQueryable<TEntity>(filter).Count();
        }

        public virtual Task<int> GetCountAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IEntity
        {
            return GetQueryable<TEntity>(filter).CountAsync();
        }

        public virtual bool GetExists<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IEntity
        {
            return GetQueryable<TEntity>(filter).Any();
        }

        public virtual Task<bool> GetExistsAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class, IEntity
        {
            return GetQueryable<TEntity>(filter).AnyAsync();
        }


        public virtual int SaveChanges()
        {
           return  _context.SaveChanges();
        }

        public virtual Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
        /*
        public virtual void Create<TEntity>(TEntity entity)
            where TEntity : class, IEntity
        {
            entity.DateCreated = DateTime.UtcNow;
            entity.LastModified = DateTime.UtcNow;
            context.Set<TEntity>().Add(entity);
        }

        public virtual void Update<TEntity>(TEntity entity)
            where TEntity : class, IEntity
        {
            entity.DateCreated = DateTime.UtcNow;
                entity.LastModified = DateTime.UtcNow;
                context.Set<TEntity>().Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }
        
        public virtual void Delete<TEntity>(object id)
            where TEntity : class, IEntity
        {
            TEntity entity = context.Set<TEntity>().FirstOrDefault(s => s.Id == id);
            if (entity != null)
            {
                Delete(entity);
            }
        }

        public virtual void Delete<TEntity>(TEntity entity)
            where TEntity : class, IEntity
        {
            var dbSet = context.Set<TEntity>();
            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }
        */
    }
}
