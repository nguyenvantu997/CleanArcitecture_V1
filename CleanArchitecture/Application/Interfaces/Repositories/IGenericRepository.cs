using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        T Add(T t);
        T AddRecord(T t);
        Task<T> AddAsyn(T t);
        Task<IEnumerable<T>> AddRangeAsyn(IEnumerable<T> arr);
        int Count();
        void Delete(T entity);
        Task DeleteAsyn(T entity);
        bool Delete(params object[] id);
        void Dispose();
        T Find(Expression<Func<T, bool>> match);
        IQueryable<T> FindAll(Expression<Func<T, bool>> match);
        T Get(params object[] id);
        IQueryable<T> GetAll();
        Task<T> GetAsync(params object[] id);
        // void Save();
        // Task<int> SaveAsync();
        T Update(T t, params object[] key);
        T UpdateRecord(T t, params object[] key);
        Task<T> UpdateAsyn(T t, params object[] key);
    }
}
