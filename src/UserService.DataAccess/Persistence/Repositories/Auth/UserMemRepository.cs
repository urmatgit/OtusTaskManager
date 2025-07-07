using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Common;
using UserService.DataAccess.Entities;

namespace UserService.DataAccess.Persistence.Repositories.Auth
{
    internal class UserMemRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>();


        public async Task<User?> FindByUserNameAsync(string userName)
        {
            return await Task.FromResult(_users
                .FirstOrDefault(u => u.UserName == userName));
        }
        public async Task<User?> FindByUserEmailAsync(string email)
        {
            return await Task.FromResult(_users
                .FirstOrDefault(u => u.Email == email));
        }

        public async Task<User> AddAsync(User user)
        {

            
            _users.Add(user);
            return user;

        }

        public async Task<bool> ExistsAsync(string email)
        {
            return await Task.FromResult(_users
                .Any(u => u.Email == email));
        }

        public async Task<User?> FindByUserUserAndEmailAsync(string email, string userName)
        {
            return await Task.FromResult(_users
                .FirstOrDefault(u => u.Email == email && u.UserName == userName));
        }



        public async Task DeleteAsync(User entity)
        {
            await Task.FromResult(_users.Remove(entity));
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await Task.FromResult(_users);
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            var entity = await Task.FromResult(_users.FirstOrDefault(u => u.Id == id));

            return entity;
        }

        public async Task UpdateAsync(User entity)
        {
            var user = await Task.FromResult(_users.FirstOrDefault(u => u.Id == entity.Id));
            if (user != null)
            {
                user = entity;
            }
        }

        public async Task SaveChangesAsync()
        {
             await Task.FromResult(0);
        }

        public Task<PaginationResponse<User>> GetAllAsync(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<User> AddProjectToUser(User user, Guid projectId)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserWithProjects(Guid userId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<User> GetAll(bool noTracking = false)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAllAsync(CancellationToken cancellationToken, bool asNoTracking = false)
        {
            throw new NotImplementedException();
        }

        public User? Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public bool DeleteRange(ICollection<User> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }

        public User Add(User entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(List<User> entities)
        {
            throw new NotImplementedException();
        }

        public Task AddRangeAsync(ICollection<User> entities)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public DbSet<User> GetUsersSet()
        {
            throw new NotImplementedException();
        }

        public Task<bool> ConfirmEmailAsync(User user, string code)
        {
            throw new NotImplementedException();
        }
    }
}
