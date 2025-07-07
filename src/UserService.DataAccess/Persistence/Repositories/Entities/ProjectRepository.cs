using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Common;
using UserService.DataAccess.Entities;

namespace UserService.DataAccess.Persistence.Repositories.Entities

{
    public class ProjectRepository : BaseRepository<Project,Guid>, IProjectRepository
    {
        public ProjectRepository(TaskboardDbContext dataContext) : base(dataContext)
        { }

        public async Task<PaginationResponse<Project>> GetAllAsync(int pageIndex, int pageSize, Guid? userId)
        {
            var dbSet = Context.Set<Project>();
            var count = await dbSet.CountAsync();
            var quary = dbSet
                .AsNoTracking()
                .Include(x => x.Users)
                .Where(x => x.CreatorId == userId || x.Users.Any(y => y.Id == userId));
            //по строничный
            var data = await quary
                .PaginateBy<Project>(pageIndex, pageSize)
                .ToListAsync();
            return new PaginationResponse<Project>(data, count, pageIndex, pageSize);
        }
    }
    
}
