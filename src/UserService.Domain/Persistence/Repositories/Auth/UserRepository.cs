using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Common;
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
    }
}
