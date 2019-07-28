using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application.Repository
{
    public interface IBaseRepository
    {
        T Add<T>(T t) where T : class;
        Task<T> AddAsync<T>(T t) where T : class;
        int Count();
        Task<int> CountAsync();
        void Delete<T>(T entity) where T : class;
        Task<int> DeleteAsyn<T>(T entity) where T : class;
        void Dispose();
        T Find<T>(Expression<Func<T, bool>> match) where T : class;
        ICollection<T> FindAll<T>(Expression<Func<T, bool>> match) where T : class;
        Task<ICollection<T>> FindAllAsync<T>(Expression<Func<T, bool>> match) where T : class;
        Task FindAllAsync<T>() where T : class;
        Task<T> FindAsync<T>(Expression<Func<T, bool>> match) where T : class;
        IQueryable<T> FindBy<T>(Expression<Func<T, bool>> predicate) where T : class;
        Task<ICollection<T>> FindByAsyn<T>(Expression<Func<T, bool>> predicate) where T : class;
        T Get<T>(long id) where T : class;
        IQueryable<T> GetAll<T>() where T : class;
        Task<IEnumerable<T>> GetAllAsyn<T>() where T : class;
        IQueryable<T> GetAllIncluding<T>(params Expression<Func<T, object>>[] includeProperties) where T : class;
        Task<T> GetAsync<T>(long id) where T : class;
        void Save();
        Task<int> SaveAsync();
        T Update<T>(T t, object key) where T : class;
        Task<T> UpdateAsyn<T>(T t, object key) where T : class;

    }
}
