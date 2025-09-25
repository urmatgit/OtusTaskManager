using Mapster.Utils;

using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Business.Services.Auth;
using UserService.DataAccess.Entities;
using UserService.DataAccess.Persistence;
using UserService.DataAccess.Persistence.Data;
using UserService.DataAccess.Persistence.Repositories.Auth;

namespace UserService.Business.Common
{
    public class BusnessDataInitializer : IDbInitializer
    {
        private readonly TaskboardDbContext _dataContext;

        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;
        public BusnessDataInitializer(TaskboardDbContext dataContext,IPasswordHasher passwordHasher,IUserRepository userRepository)
        {
            _dataContext= dataContext;
            _passwordHasher= passwordHasher;
            _userRepository= userRepository;
        }
        public  async Task InitializeDb()
        {
            await CreateDefaultAdmin();
        }
        private async Task CreateDefaultAdmin()
        {
            if (!_dataContext.Users.Any(x => x.UserName == "admin"))
            {
                var user = new User("admin", "Admin", "Adminov", "adminTask@taskboard.com", "+7777777777", DataAccess.Enums.UserRole.Admin, _passwordHasher.Hash("MyPass@123!"));

                await _userRepository.AddAsync(user);
                await _userRepository.SaveChangesAsync();
            }
        }
    }
}
