using System.Linq.Expressions;

using Hangfire.DataAccess;
using Hangfire.DataAccess.Entities;

namespace Hangfire.Business.Abstract
{
    public abstract class BaseService<T>(HangfireDataContext dataContext) : IBaseService<T> where T : BaseEntity
    {
        protected HangfireDataContext DataContext = dataContext;

        public async Task<T> Create(T entity)
        {
            entity.Id = Guid.NewGuid();
            var result = await DataContext.AddAsync(entity);
            await DataContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<int> Delete(T entity)
        {
            DataContext.Remove(entity);
            var rows = await DataContext.SaveChangesAsync();
            return rows;
        }

        public abstract Task<T> Get(Guid guid);

        public abstract Task<T> Get(Expression<Func<T, bool>> predicate);

        public abstract Task<List<T>> GetList(Expression<Func<T, bool>> predicate);

        public async Task<T> Update(T entity)
        {
            DataContext.Update(entity);
            await DataContext.SaveChangesAsync();
            return entity;
        }
    }
}