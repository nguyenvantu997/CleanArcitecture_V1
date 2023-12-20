using Application.Interfaces.Repositories;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseAuditableEntity
    {
        protected readonly DbContext context;
        private DbSet<T> entities;
        string errorMessage = string.Empty;
        private bool disposed = false;
        public GenericRepository(DbContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }

        #region Methods

        public IQueryable<T> GetAll()
        {
            return this.entities.AsQueryable();
        }

        public virtual T Get(params object[] id)
        {
            return this.entities.Find(id);
        }

        public virtual async Task<T> GetAsync(params object[] id)
        {
            return await this.entities.FindAsync(id);
        }

        public virtual T Add(T t)
        {
            this.entities.Add(t);
            //this.context.SaveChanges();
            return t;
        }

        public virtual T AddRecord(T t)
        {
            this.entities.Add(t);
            return t;
        }

        public virtual async Task<T> AddAsyn(T t)
        {
            try
            {
                this.entities.Add(t);
                // await this.context.SaveChangesAsync();
                return t;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<T>> AddRangeAsyn(IEnumerable<T> arr)
        {
            try
            {
                this.entities.AddRange(arr);
                // await this.context.SaveChangesAsync();
                return arr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual T Find(Expression<Func<T, bool>> match)
        {
            return this.entities.SingleOrDefault(match);
        }

        public IQueryable<T> FindAll(Expression<Func<T, bool>> match)
        {
            return this.entities.Where(match);
        }

        public virtual void Delete(T entity)
        {
            this.entities.Remove(entity);
            //this.context.SaveChanges();
        }

        public virtual bool Delete(params object[] id)
        {
            var router = this.Get(id);
            if (router != null)
            {
                this.entities.Remove(router);
                // this.context.SaveChanges();
                return true;
            }

            return false;
        }

        public virtual async Task DeleteAsyn(T entity)
        {
            this.entities.Remove(entity);

            //int res = await this.context.SaveChangesAsync();

            // return res > 0;
        }

        public virtual T Update(T t, params object[] key)
        {
            if (t == null)
            {
                return null;
            }

            T exist = this.entities.Find(key);
            if (exist != null)
            {
                this.context.Entry(exist).CurrentValues.SetValues(t);
                //this.context.SaveChanges();
            }

            return exist;
        }

        public virtual T UpdateRecord(T t, params object[] key)
        {
            if (t == null)
            {
                return null;
            }

            T exist = this.entities.Find(key);
            if (exist != null)
            {
                this.context.Entry(exist).CurrentValues.SetValues(t);
            }

            return exist;
        }

        public virtual async Task<T> UpdateAsyn(T t, params object[] key)
        {
            if (t == null)
            {
                return null;
            }

            T exist = await this.entities.FindAsync(key);
            if (exist != null)
            {
                this.context.Entry(exist).CurrentValues.SetValues(t);
                //await this.context.SaveChangesAsync();
            }

            return exist;
        }

        public int Count()
        {
            return this.entities.Count();
        }

        //public virtual void Save()
        //{
        //    this.context.SaveChanges();
        //}

        //public async virtual Task<int> SaveAsync()
        //{
        //    return await this.context.SaveChangesAsync();
        //}

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.context.Dispose();
                }

                this.disposed = true;
            }
        }

        #endregion
    }
}
