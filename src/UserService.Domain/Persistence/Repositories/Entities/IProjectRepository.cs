using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Common;
using UserService.DataAccess.Entities;

namespace UserService.DataAccess.Persistence.Repositories
{
    public interface IProjectRepository : IRepository<Project,Guid> 
    {
        public Task<PaginationResponse<Project>> GetAllAsync(int pageIndex, int pageSize, Guid? userId);
        public Task<List<Project>> GetWithUsersAllAsync( CancellationToken cancellationToken);
        public Task<Project> GetByIdWithUsersAsync(Guid id);
        public Task<Project> AddUserToProjectAsync(Guid id,Guid UserId);
        public Task<Project> RemoveUserFromProjectAsync(Guid id, Guid UserId);
        public Task<Project> FindByName(string name);
    }
}
