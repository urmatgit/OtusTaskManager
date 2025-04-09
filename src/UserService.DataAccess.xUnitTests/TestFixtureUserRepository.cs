using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Persistence.Repositories.Auth;
using UserService.DataAccess.Persistence;

namespace UserService.DataAccess.xUnitTests
{
    public class TestFixtureUserRepository: IDisposable
    {
        private readonly DbContextOptions<TaskboardDbContext> _dbContextOptions;
        private readonly TaskboardDbContext _context;
        public readonly IUserRepository _userRepository;

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        
        public TestFixtureUserRepository()
        {
            DbContextOptions<TaskboardDbContext> options;
            var builder = new DbContextOptionsBuilder<TaskboardDbContext>();
            builder.UseInMemoryDatabase("Taskboard");
            _dbContextOptions = builder.Options;
            _context = new TaskboardDbContext(_dbContextOptions);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            _userRepository = new UserRepository(_context); 
        }
        
    }
}
