using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Common;
using UserService.DataAccess.Entities;

namespace UserService.DataAccess.Persistence.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : BaseEntity<Guid>
    {
        protected readonly TaskboardDbContext _dataContext;
        public BaseRepository(TaskboardDbContext dataContext)
        {
            _dataContext = dataContext;   
        }
        public async Task<T> AddAsync(T entity)
        {
            await _dataContext.Set<T>()
                .AddAsync(entity);
            return entity;
        }

        public virtual async Task DeleteAsync(T entity)
        {
            entity.IsDeleted = true;
            await Task.FromResult(_dataContext.Set<T>().Update(entity));

            //_dataContext.Set<T>()
            //    .Remove(entity);
            
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            var entities = await _dataContext.Set<T>()
               .AsNoTracking()
               .Where(x => !x.IsDeleted)
               .ToListAsync();

            

            return entities;
        }

        public async Task<PaginationResponse<T>> GetAllAsync(int pageIndex, int pageSize)
        {
            var dbSet = _dataContext.Set<T>();
            var count = await dbSet.Where(x=>!x.IsDeleted).CountAsync();
            var data = await dbSet
                .AsNoTracking()
                .Where(x => !x.IsDeleted)
                .PaginateBy<T>(pageIndex, pageSize)
                .ToListAsync();
            return new PaginationResponse<T>(data,count,pageIndex,pageSize);
        }

        public  async Task<T> GetByIdAsync(Guid id)
        {
            var entity = await _dataContext.Set<T>()
                .FirstOrDefaultAsync(x => x.Id == id);

            return entity;
        }

        public async Task SaveChangesAsync()
        {
            await _dataContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
              await Task.FromResult(_dataContext.Set<T>().Update(entity));
        }
    }
}
