using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Common;
using UserService.DataAccess.Entities;

namespace UserService.DataAccess.Persistence.Repositories.Auth
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> FindByUserEmailAsync(string email);
        Task<User?> FindByUserNameAsync(string userName);
        Task<User?> FindByUserUserAndEmailAsync(string email, string userName);

        Task<bool> ExistsAsync(string email);
        Task<User> AddProjectToUser(User user, Guid projectId);
        Task<User> GetUserWithProjects(Guid userId);


     
    }
}
