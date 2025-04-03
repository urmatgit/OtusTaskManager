using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Entities;

namespace UserService.DataAccess.Persistence.Repositories.Entities

{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        public ProjectRepository(TaskboardDbContext dataContext) : base(dataContext)
        { }
            public override async Task<IEnumerable<Project>> GetAllAsync()
        {
            var entities = await _dataContext.Set<Project>()
                .AsNoTracking()
                .Where(x=>!x.IsDeleted)
                .ToListAsync();

            return entities;
        }
    }
    
}
