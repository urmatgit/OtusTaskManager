using FluentAssertions;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UserService.Business.Services.Auth;
using UserService.DataAccess.Common.Errors;
using UserService.DataAccess.DTOs.Auth;
using UserService.DataAccess.Entities;
using UserService.DataAccess.Persistence.Repositories.Auth;
using Xunit;
namespace UserService.Business.xUnitTests.Auth
{
    public class TestAuth
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;

        private readonly IUserAuthService _userAuthService;
        private readonly IJwtService _jwtService;

        private readonly Mock<IPasswordHasher> _passwordHasher;
        private readonly Mock<IPublisher> _mockPublisher;
        private readonly JwtSettings jwtSettings;
        public TestAuth()
        {
            jwtSettings = new JwtSettings()
            {
                Secret = "this is secret textthis is secret text",
                Audience = "TaskboardProject",
                Issuer = "TaskboardProject",
                ExpiryMinutes = 60
            };
            _jwtService = new JwtService(Microsoft.Extensions.Options.Options.Create<JwtSettings>(jwtSettings));
            _passwordHasher = new Mock<IPasswordHasher>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _mockPublisher = new Mock<IPublisher>();
            _userAuthService = new UserAuthService(_userRepositoryMock.Object, _jwtService, _passwordHasher.Object, _mockPublisher.Object);
        }
        #region User register tests
        [Fact]
        public async Task Test_Register_NewUser_ReturnUser()
        {
            //Arrage
            var errors = Errors.EmailAlreadyExists;
            var userRequest = new RegisterRequest("Testuser", "Testuserov", "testuser", "test@user.com", "+77778888999", "test@user!", DataAccess.Enums.ProjectRole.User);
            _mockPublisher.Setup(x => x.Publish(It.IsAny<RegisterRequest>(), CancellationToken.None));
            _userRepositoryMock.Setup(x => x.ExistsAsync(userRequest.Email)).Returns(Task.FromResult(false));
            _userRepositoryMock.Setup(x => x.FindByUserNameAsync(userRequest.Username)).Returns(Task.FromResult(default(User)));
            _userRepositoryMock.Setup(x => x.AddAsync(It.IsAny<User>()));
            _userRepositoryMock.Setup(x => x.SaveChangesAsync(CancellationToken.None));
            _passwordHasher.Setup(x => x.Hash(It.IsAny<string>())).Returns(It.IsAny<string>());
            _passwordHasher.Setup(x => x.Verify(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

            //Act
            var userresponse = await _userAuthService.RegisterAsync(userRequest);

            //assert
            userresponse.Should().NotBeNull();
            userresponse.IsSuccess.Should().BeTrue();
            userresponse.Value.Should().NotBeNull();
            userresponse.Value.Token.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Test_Register_NewUser_EmailAlreadyExists()
        {
            //Arrage
            var errors = Errors.EmailAlreadyExists;
            var userRequest = new RegisterRequest("Testuser", "Testuserov", "testuser", "test@user.com", "+77778888999", "test@user!", DataAccess.Enums.ProjectRole.User);
            _mockPublisher.Setup(x => x.Publish(It.IsAny<RegisterRequest>(), CancellationToken.None));
            _userRepositoryMock.Setup(x => x.ExistsAsync(userRequest.Email)).Returns(Task.FromResult(true));
            _userRepositoryMock.Setup(x => x.FindByUserNameAsync(userRequest.Username)).Returns(Task.FromResult(default(User)));
            _userRepositoryMock.Setup(x => x.AddAsync(It.IsAny<User>()));
            _userRepositoryMock.Setup(x => x.SaveChangesAsync(CancellationToken.None));

            //Act
            var userresponse = await _userAuthService.RegisterAsync(userRequest);

            //assert
            userresponse.Should().NotBeNull();
            userresponse.IsFailure.Should().BeTrue();

            userresponse.Error.Should().BeSameAs(errors);
        }
        [Fact]
        public async Task Test_Register_NewUser_UserAlreadyExists()
        {
            //Arrage
            var errors = Errors.UsernameAlreadyExists;
            var userRequest = new RegisterRequest("Testuser", "Testuserov", "testuser", "test@user.com", "+77778888999", "test@user!", DataAccess.Enums.ProjectRole.User);
            _mockPublisher.Setup(x => x.Publish(It.IsAny<RegisterRequest>(), CancellationToken.None));
            _userRepositoryMock.Setup(x => x.ExistsAsync(userRequest.Email)).Returns(Task.FromResult(false));
            _userRepositoryMock.Setup(x => x.FindByUserNameAsync(userRequest.Username)).Returns(Task.FromResult(new User()));
            _userRepositoryMock.Setup(x => x.AddAsync(It.IsAny<User>()));
            _userRepositoryMock.Setup(x => x.SaveChangesAsync(CancellationToken.None));

            //Act
            var userresponse = await _userAuthService.RegisterAsync(userRequest);

            //assert
            userresponse.Should().NotBeNull();
            userresponse.IsFailure.Should().BeTrue();

            userresponse.Error.Should().BeSameAs(errors);
        }
        #endregion
        #region User login tests
        [Fact]
        public async Task Test_UserLogin_Success()
        {
            //arrage
            var userRequest = new LoginRequest("testuser", "test@user!");
            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName=userRequest.Username,
                Email="test@user.com",
                Phone="8888888888",
                Role=DataAccess.Enums.ProjectRole.Admin,

            };
            _userRepositoryMock.Setup(x => x.FindByUserNameAsync(userRequest.Username)).Returns(Task.FromResult(user));
            _passwordHasher.Setup(x => x.Hash(It.IsAny<string>())).Returns(It.IsAny<string>());
            _passwordHasher.Setup(x => x.Verify(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            //act
            var loginResult = await _userAuthService.LoginAsync(userRequest);
            loginResult.Should().NotBeNull();
            loginResult.IsSuccess.Should().BeTrue();
            loginResult.Value.Token.Should().NotBeNullOrEmpty();
        }
        [Fact]
        public async Task Test_UserLogin_Failure_InvalidCredentials()
        
        {
            //arrage
            var errors = Errors.InvalidCredentials;
            var userRequest = new LoginRequest("testuser", "test@user!");
            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = userRequest.Username,
                Email = "test@user.com",
                Phone = "8888888888",
                Role = DataAccess.Enums.ProjectRole.Admin,

            };
            _userRepositoryMock.Setup(x => x.FindByUserNameAsync(userRequest.Username)).Returns(Task.FromResult(default(User)));
            _passwordHasher.Setup(x => x.Hash(It.IsAny<string>())).Returns(It.IsAny<string>());
            _passwordHasher.Setup(x => x.Verify(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            //act
            var loginResult = await _userAuthService.LoginAsync(userRequest);
            loginResult.Should().NotBeNull();
            loginResult.IsFailure.Should().BeTrue();

            loginResult.Error.Should().BeSameAs(errors);
        }
        #endregion
    }
}