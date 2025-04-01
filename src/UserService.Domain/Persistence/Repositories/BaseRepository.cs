using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Entities;

namespace UserService.DataAccess.Persistence.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly TaskboardDbContext _dataContext;
        public BaseRepository(TaskboardDbContext dataContext)
        {
            _dataContext = dataContext;   
        }
        public async Task AddAsync(T entity)
        {
            await _dataContext.Set<T>()
                .AddAsync(entity);
            
        }

        public async Task DeleteAsync(T entity)
        {
            _dataContext.Set<T>()
                .Remove(entity);
            
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var entities = await _dataContext.Set<T>()
                .AsNoTracking()
                .ToListAsync();

            return entities;
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
