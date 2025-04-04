using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Common;
using UserService.DataAccess.Entities;

namespace UserService.DataAccess.Persistence
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<PaginationResponse<T>> GetAllAsync(int pageIndex,int pageSize);
        Task<T> GetByIdAsync(Guid id);
        Task AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);
        Task SaveChangesAsync();
    }
}
