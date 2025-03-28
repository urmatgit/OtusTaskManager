using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Common;
using UserService.DataAccess.Entities;

namespace UserService.DataAccess.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TaskboardDbContext _context;

        public UserRepository(TaskboardDbContext context)
        {
            _context = context;
        }
        public async  Task<User?> FindByUserNameAsync(string userName)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == userName);
        }
        public async Task<User?> FindByUserEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Result<User>> AddAsync(User user)
        {
            try
            {
                await _context.Users.AddAsync(user);
            }
            catch (Exception ex)
            {
                return Result<User>.Failure($"Failed to create user: {ex.Message}");
            }
            return Result<User>.Success(user);
        }

        public async Task<bool> ExistsAsync(string email)
        {
            return await _context.Users
                .AnyAsync(u => u.Email == email);
        }

        public async Task<User?> FindByUserUserAndEmailAsync(string email,string userName)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.UserName==userName);
        }
    }
}
