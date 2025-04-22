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
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        public ProjectRepository(TaskboardDbContext dataContext) : base(dataContext)
        { }

        public async Task<PaginationResponse<Project>> GetAllAsync(int pageIndex, int pageSize, Guid? userId)
        {
            var dbSet = _dataContext.Set<Project>();
            var count = await dbSet.Where(x => !x.IsDeleted).CountAsync();
            var quary =  dbSet
                .AsNoTracking()
                .Include(x => x.Users)
                //если берем все проекты которые участвует заданный юзер
                .Where(x => !x.IsDeleted );
            //если userid задан тогда еще 1 условия добавляем
            if (userId is not null){
                quary = quary.Where(x=> x.UserId == userId || x.Users.Any(y => y.Id == userId));
            }
            var data = await quary
                .PaginateBy<Project>(pageIndex, pageSize)
                .ToListAsync();
            return new PaginationResponse<Project>(data, count, pageIndex, pageSize);
        }
    }
    
}
