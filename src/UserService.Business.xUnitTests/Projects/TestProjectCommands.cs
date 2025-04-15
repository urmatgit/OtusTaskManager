using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Api;
using UserService.Business.Application.Projects.Commands.CreateProject;
using UserService.Business.Application.Projects.Commands.DeleteProject;
using UserService.Business.Application.Projects.Commands.UpdateProject;
using UserService.DataAccess.Entities;
using UserService.DataAccess.Persistence.Repositories.Entities;

namespace UserService.Business.xUnitTests.Projects
{
    public class TestProjectCommands : CQRSIntegrationTests
    {
        public TestProjectCommands(CustomWebApplicationFactory<Program> factory) : base(factory)
        {
        }
        [Fact]
        public async Task Request_CreateProject_ProjectResponse_Success()
        {
            //Arrange
            var userid= Guid.NewGuid();
            var request = new CreateProjectRequest("test project");
            var project= Mapper.Map<Project>(request);
            CurrentUserServiceMock.Setup(x => x.GetUserId()).Returns(userid);
            ProjectRepositoryMock.Setup(x => x.AddAsync(project));
            ProjectRepositoryMock.Setup(x => x.SaveChangesAsync());
            //Act
            var response=await Mediator.Send(request);
            //assert
            response.Should().NotBeNull();
            response.IsSuccess.Should().BeTrue();
            response.Value.owner.Should().Be(userid);

        }
        [Fact]
        public async Task Request_UpdateProject_ProjectResponse_Success()
        {
            //Arrange
            var userid = Guid.NewGuid();
            var projectIid=Guid.NewGuid();
            var request = new UpdateProjectRequest(projectIid,"test project",userid);
            var project = Mapper.Map<Project>(request);
            CurrentUserServiceMock.Setup(x => x.GetUserId()).Returns(userid);
            ProjectRepositoryMock.Setup(x => x.GetByIdAsync(projectIid)).Returns(Task.FromResult(project));
            ProjectRepositoryMock.Setup(x => x.UpdateAsync(project));
            ProjectRepositoryMock.Setup(x => x.SaveChangesAsync());
            
            //Act

            var response = await Mediator.Send(new UpdateProjectRequest(projectIid, "test project1", userid));
            //assert
            response.Should().NotBeNull();
            response.IsSuccess.Should().BeTrue();
            response.Value.owner.Should().Be(userid);

        }
        [Fact]
        public async Task Request_DeleteProject_True_Success()
        {
            //Arrange
            
            var projectIid = Guid.NewGuid();
            var request = new DeleteProjectRequest(projectIid);
            var project = Mapper.Map<Project>(request);
            
            ProjectRepositoryMock.Setup(x => x.GetByIdAsync(projectIid)).Returns(Task.FromResult(project));
            ProjectRepositoryMock.Setup(x => x.DeleteAsync(project));
            ProjectRepositoryMock.Setup(x => x.SaveChangesAsync());

            //Act

            var response = await Mediator.Send(request);
            //assert
            response.Should().NotBeNull();
            response.IsSuccess.Should().BeTrue();
            response.Value.Should().BeTrue();

        }
    }
}
