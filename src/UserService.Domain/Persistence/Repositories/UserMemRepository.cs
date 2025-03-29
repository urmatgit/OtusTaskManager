using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Common;
using UserService.DataAccess.Entities;

namespace UserService.DataAccess.Persistence.Repositories
{
    internal class UserMemRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>();

        public UserMemRepository()
        {
         
        }
        public async Task<User?> FindByUserNameAsync(string userName)
        {
            return  await Task.FromResult( _users
                .FirstOrDefault(u => u.UserName == userName));
        }
        public async Task<User?> FindByUserEmailAsync(string email)
        {
            return await Task.FromResult(_users
                .FirstOrDefault(u => u.Email == email));
        }

        public async Task<Result<User>> AddAsync(User user)
        {
            try
            {
                user.Id = Guid.NewGuid();
                _users.Add(user);
            }
            catch (Exception ex)
            {
                return Result<User>.Failure($"Failed to create user: {ex.Message}");
            }
            return Result<User>.Success(user);
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
    }
}
