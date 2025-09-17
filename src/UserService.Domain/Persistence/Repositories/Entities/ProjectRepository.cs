using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
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
        DbSet<Project> dbSet; 
        public ProjectRepository(TaskboardDbContext dataContext) : base(dataContext)
        {
            dbSet= Context.Set<Project>();
        }

        public async Task<Project> AddUserToProjectAsync(Guid id, Guid UserId)
        {
            var project = await dbSet.Include(x=>x.Users).SingleOrDefaultAsync( x=>x.Id==id);
            var user = await Context.Set<User>().FindAsync(UserId);
            if (project != null && user != null)
            {
                
                project.Users.Add(user);
                await SaveChangesAsync();
            }
            return project;
        }

        public async Task<PaginationResponse<Project>> GetAllAsync(int pageIndex, int pageSize, Guid? userId)
        {
        
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

        public async Task<Project> GetByIdWithUsersAsync(Guid id)
        {
            
            var quary = await dbSet
                .AsNoTracking()
                .Include(x => x.Users)
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();
            return quary;
        }

        public async Task<Project> RemoveUserFromProjectAsync(Guid id, Guid UserId)
        {
            var project = await dbSet.Include(x => x.Users).SingleOrDefaultAsync(x => x.Id == id);
            var user = await Context.Set<User>().FindAsync(UserId);
            if (project!=null && user != null && project.Users!=null)
            {
                project.Users.Remove(user);
                await SaveChangesAsync();
            }

            return project;
        }
    }
    
}
