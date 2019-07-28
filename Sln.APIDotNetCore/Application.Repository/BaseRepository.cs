using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application.Repository
{
    public class BaseRepository<TDbContext> : IBaseRepository where TDbContext : DbContext
    {
        protected TDbContext _dbContext;
        public BaseRepository(TDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<T> GetAll<T>() where T : class
        {
            return _dbContext.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsyn<T>() where T : class
        {

            return await _dbContext.Set<T>().ToListAsync();
        }

        public virtual T Get<T>(long id) where T : class
        {
            return _dbContext.Set<T>().Find(id);
        }

        public virtual async Task<T> GetAsync<T>(long id) where T : class
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public virtual T Add<T>(T t) where T : class
        {

            _dbContext.Set<T>().Add(t);
            _dbContext.SaveChanges();
            return t;
        }

        public virtual async Task<T> AddAsync<T>(T t) where T : class
        {
            _dbContext.Set<T>().Add(t);
            await _dbContext.SaveChangesAsync();
            return t;

        }

        public virtual T Find<T>(Expression<Func<T, bool>> match) where T : class
        {
            return _dbContext.Set<T>().SingleOrDefault(match);
        }

        public virtual async Task<T> FindAsync<T>(Expression<Func<T, bool>> match) where T : class
        {
            return await _dbContext.Set<T>().SingleOrDefaultAsync(match);
        }

        public ICollection<T> FindAll<T>(Expression<Func<T, bool>> match) where T : class
        {
            return _dbContext.Set<T>().Where(match).ToList();
        }

        public async Task<ICollection<T>> FindAllAsync<T>(Expression<Func<T, bool>> match) where T : class
        {
            return await _dbContext.Set<T>().Where(match).ToListAsync();
        }

        public virtual void Delete<T>(T entity) where T : class
        {
            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();
        }

        public virtual async Task<int> DeleteAsyn<T>(T entity) where T : class
        {
            _dbContext.Set<T>().Remove(entity);
            return await _dbContext.SaveChangesAsync();
        }

        public virtual T Update<T>(T t, object key) where T : class
        {
            if (t == null)
                return null;
            T exist = _dbContext.Set<T>().Find(key);
            if (exist != null)
            {
                _dbContext.Entry(exist).CurrentValues.SetValues(t);
                _dbContext.SaveChanges();
            }
            return exist;
        }

        public virtual async Task<T> UpdateAsyn<T>(T t, object key) where T : class
        {
            if (t == null)
                return null;
            T exist = await _dbContext.Set<T>().FindAsync(key);
            if (exist != null)
            {
                _dbContext.Entry(exist).CurrentValues.SetValues(t);
                await _dbContext.SaveChangesAsync();
            }
            return exist;
        }

        public int Count<T>() where T : class
        {
            return _dbContext.Set<T>().Count();
        }

        public async Task<int> CountAsync<T>() where T : class
        {
            return await _dbContext.Set<T>().CountAsync();
        }

        public virtual void Save()
        {

            _dbContext.SaveChanges();
        }

        public async virtual Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public virtual IQueryable<T> FindBy<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            IQueryable<T> query = _dbContext.Set<T>().Where(predicate);
            return query;
        }

        public virtual async Task<ICollection<T>> FindByAsyn<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return await _dbContext.Set<T>().Where(predicate).ToListAsync();
        }

        public IQueryable<T> GetAllIncluding<T>(params Expression<Func<T, object>>[] includeProperties) where T : class
        {

            IQueryable<T> queryable = GetAll<T>();
            foreach (Expression<Func<T, object>> includeProperty in includeProperties)
            {

                queryable = queryable.Include<T, object>(includeProperty);
            }

            return queryable;
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync()
        {
            throw new NotImplementedException();
        }

        public Task FindAllAsync<T>() where T : class
        {
            throw new NotImplementedException();
        }
    }
}
