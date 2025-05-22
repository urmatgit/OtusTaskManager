using System.Linq.Expressions;

using Hangfire.DataAccess.Entities;

namespace Hangfire.Business.Abstract
{
    public interface IBaseService<T> where T : BaseEntity
    {
        Task<T> Create(T entity);
        Task<T> Update(T entity);
        Task<int> Delete(T entity);
        Task<T> Get(Guid guid);
        Task<T> Get(Expression<Func<T, bool>> predicate);
        Task<List<T>> GetList(Expression<Func<T, bool>> predicate);
    }
}