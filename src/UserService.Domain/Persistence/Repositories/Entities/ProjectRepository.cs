using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Entities;

namespace UserService.DataAccess.Persistence.Repositories.Entities

{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepsitory
    {
        public ProjectRepository(TaskboardDbContext dataContext) : base(dataContext)
        {
        }
    }
}
