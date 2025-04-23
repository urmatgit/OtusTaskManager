using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Common;
using UserService.DataAccess.Common.Errors;
using UserService.DataAccess.Entities;

namespace UserService.DataAccess.Persistence.Repositories.Auth
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(TaskboardDbContext dataContext) : base(dataContext)
        {
        }

        public async Task<User?> FindByUserNameAsync(string userName)
        {
            return await _dataContext.Users
                .FirstOrDefaultAsync(u => u.UserName == userName);
        }
        public async Task<User?> FindByUserEmailAsync(string email)
        {
            return await _dataContext.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }


        public async Task<bool> ExistsAsync(string email)
        {
            return await _dataContext.Users
                .AnyAsync(u => u.Email == email);
        }

        public async Task<User?> FindByUserUserAndEmailAsync(string email, string userName)
        {
            return await _dataContext.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.UserName == userName);
        }

        public async Task<User> AddProjectToUser(User user, Guid projectId)
        {

            ///TODO
            ///add project
            //user.UserProjects.Add(new UserProject()
            //{
            //    Id=Guid.NewGuid(),
            //    ProjectId = projectId,
            //    UserId = user.Id
            //});
            var project=await _dataContext.Projects.FindAsync(projectId);
            if (project != null)
            {
                user.Projects.Add(project);
            }
            else
                throw new Exception(string.Format(Errors.EntityNotFound, "Project", project.Id));

             await UpdateAsync(user);
            await SaveChangesAsync();
            return await GetUserWithProjects(user.Id);
            
        }

        public async Task<User> GetUserWithProjects(Guid userId)
        {
            var user = await _dataContext.Users
                        .AsNoTracking()
                        .Include(u => u.Projects)
                        .SingleOrDefaultAsync(u => !u.IsDeleted && u.Id == userId && u.Projects.Any(p => !p.IsDeleted));


            return user;
        }

    }
}
