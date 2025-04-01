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

        public async Task AddAsync(User user)
        {

            user.Id = Guid.NewGuid();
            _users.Add(user);

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
    }
}
