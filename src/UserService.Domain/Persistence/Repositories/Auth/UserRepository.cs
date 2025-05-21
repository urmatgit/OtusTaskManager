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
    public class UserRepository : BaseRepository<User, Guid>, IUserRepository
    {
        
             private readonly DbSet<User> _users;
        public UserRepository(TaskboardDbContext dataContext) : base(dataContext)
        {
            _users = dataContext.Users;
        }

        public async Task<User?> FindByUserNameAsync(string userName)
        {
            return await _users
                .FirstOrDefaultAsync(u => u.UserName == userName);
        }
        public async Task<User?> FindByUserEmailAsync(string email)
        {
            return await _users
                .FirstOrDefaultAsync(u => u.Email == email);
        }


        public async Task<bool> ExistsAsync(string email)
        {
            return await _users
                .AnyAsync(u => u.Email == email);
        }

        public async Task<User?> FindByUserUserAndEmailAsync(string email, string userName)
        {
            return await _users
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
            var project=await Context.Set<Project>().FindAsync(projectId);
            if (project != null)
            {
                user.Projects.Add(project);
            }
            else
                throw new Exception(string.Format(Errors.EntityNotFound, "Project", project.Id));
            
             Update(user);
            await SaveChangesAsync();
            return await GetUserWithProjects(user.Id);
            
        }

        public async Task<User> GetUserWithProjects(Guid userId)
        {
            var user = await _users
                        .AsNoTracking()
                        .Include(u => u.Projects)
                        .SingleOrDefaultAsync(u => u.Id == userId );


            return user;
        }

    }
}
