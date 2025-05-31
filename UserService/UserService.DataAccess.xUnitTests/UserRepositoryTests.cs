using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Entities;
using UserService.DataAccess.Persistence;
using UserService.DataAccess.Persistence.Repositories.Auth;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Shouldly;
using Xunit;
using UserService.DataAccess.Persistence.Repositories;
namespace UserService.DataAccess.xUnitTests
{
    //Запускать тест полностью!
    public class UserRepositoryTests: IClassFixture<TestFixtureUserRepository>
    {
        IUserRepository _userRepository;
        IProjectRepository _projectRepository;
        public UserRepositoryTests(TestFixtureUserRepository testFixtureUserRepository)
        {
            _userRepository = testFixtureUserRepository._userRepository;
            _projectRepository = testFixtureUserRepository._projectRepository;
        }
        /// <summary>
        /// Test username is not set
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Test_AddUser_UserName_Is_Empty()
        {
            //Arrange
            var email = $"test1@gmail.com";
            var newUser = new User()
            {
                Id = Guid.NewGuid(),
                //UserName = $"TestUser",
                FirstName = $"Testuser",
                LastName = $"TestUserov",
                Email = email,
                DateReg = DateTime.Now,
                Phone = $"777 777777",
                PasswordHash = $"dafadfadfa"


            };
            //Act
            try
            {
                await _userRepository.AddAsync(newUser);
                await _userRepository.SaveChangesAsync();
            }
            catch (Exception ex) {
                
                ex.Message.ShouldContain("UserName");
            }

            //Assert
            
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        
        public async Task AddUser_NotNull_Without_Exception(int number)
        {
            //Arrange

            var newUser = new User()
            {
                Id = Guid.NewGuid(),
                UserName = $"TestUser_{number}",
                FirstName = $"Testuser_{number}",
                LastName = $"TestUserov_{number}",
                Email = $"test{number}@gmail.com",
                DateReg = DateTime.Now,
                Phone=$"777 777777{number}",
                PasswordHash=$"dafadfadfa{number}"


            };
            //Act
            await _userRepository.AddAsync(newUser);
            await _userRepository.SaveChangesAsync();
            //Assert
            Assert.NotNull(newUser);
        }
        [Fact]
        public async Task TestFindUserByEmail_NotEmpty()
        
        {
            //Arrange
            var email = $"test1@gmail.com";
            //var newUser = new User()
            //{
            //    Id = Guid.NewGuid(),
            //    UserName = $"TestUser_1",
            //    FirstName = $"Testuser",
            //    LastName = $"TestUserov",
            //    Email = email,
            //    DateReg = DateTime.Now,
            //    Phone = $"777 777777",
            //    PasswordHash = $"dafadfadfa"


            //};
            //Act
            //await _userRepository.AddAsync(newUser);
            //await _userRepository.SaveChangesAsync();
            var user = await _userRepository.FindByUserEmailAsync(email);

            //Assert
            user.ShouldNotBeNull();
        }
        [Fact]
        public async Task TestGetAll_NotZero()
        {
            //Arrange
            //Act
            var users = await _userRepository.GetAllAsync(CancellationToken.None);
            //Assert
            users.ShouldNotBeNull();
            users.Count().ShouldBeGreaterThan(0);

        }
        [Fact]
        public async Task TestGetAll_PageNotZero()
        {
            //Arrange
            //Act
            var users = await _userRepository.GetAllAsync(1,2);
            //Assert
            users.ShouldNotBeNull();
            users.TotalPages.ShouldBeGreaterThan(0);
            users.CurrentPage.ShouldBe(1);
            users.HasPreviousPage.ShouldBeFalse();
            users.HasNextPage.ShouldBeTrue();
        }
        [Fact]
        public async Task Add_ProjectToUser_ProjectInUserList()
        {

            var newUser = new User()
            {
                Id = Guid.NewGuid(),
                UserName = $"UserWithProject",
                FirstName = $"UserWithProject",
                LastName = $"UserWithProjectov",
                Email = $"UserWithProject@gmail.com",
                DateReg = DateTime.Now,
                Phone = $"777 7777779",
                PasswordHash = $"dafadfadfaUserWithProject"


            };
            var project = new Project()
            {
                Id = Guid.NewGuid(),
                Name="project for add to user"

            };
            //Act
            await _userRepository.AddAsync(newUser);
            await _userRepository.SaveChangesAsync();
            await _projectRepository.AddAsync(project);
            await _projectRepository.SaveChangesAsync();
           var user= await _userRepository.AddProjectToUser(newUser, project.Id);
            //Assert
            user.ShouldNotBeNull();
            user.Projects.Count.ShouldBeGreaterThan(0);


        }

    }
}
