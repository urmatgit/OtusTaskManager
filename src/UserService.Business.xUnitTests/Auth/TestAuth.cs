using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Business.Services.Auth;
using UserService.DataAccess.Persistence.Repositories.Auth;
using Xunit;
namespace UserService.Business.xUnitTests.Auth
{
    public class TestAuth
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        
        private readonly IUserAuthService _userAuthService;
        public TestAuth()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            //_userRepositoryMock = new UserAuthService(_userRepositoryMock.Object,);
        }
        [Fact]
        public void Test_UserRegister_NotException()
        {

        }
    }
}
