using MapsterMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Api;
using UserService.Business.Services.Auth;
using UserService.DataAccess.Persistence;
using UserService.DataAccess.Persistence.Repositories;

namespace UserService.Business.xUnitTests.Projects
{
    public abstract class CQRSIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>, IDisposable
    {
        protected readonly CustomWebApplicationFactory<Program> Factory;
        protected readonly HttpClient Client;
        protected readonly IServiceScope Scope;
        protected readonly TaskboardDbContext DbContext;
        protected readonly IMapper Mapper;
        protected readonly IMediator Mediator;

        protected readonly Mock<ICurrentUser> CurrentUserServiceMock;
        protected readonly Mock<IPublisher> PublisherMock;
        protected readonly Mock<IProjectRepository> ProjectRepositoryMock;
        protected CQRSIntegrationTests(CustomWebApplicationFactory<Program> factory)
        {
            Factory = factory;
           // Client = factory.CreateClient();
            Scope = factory.Services.CreateScope();

            // Resolve services
            DbContext = Scope.ServiceProvider.GetRequiredService<TaskboardDbContext>();
            Mapper =  Scope.ServiceProvider.GetRequiredService<IMapper>();
            Mediator = Scope.ServiceProvider.GetRequiredService<IMediator>();

            // Get mock instances
            CurrentUserServiceMock = factory.CurrentUserMock;
            PublisherMock = factory.PublisherMock;
            ProjectRepositoryMock = factory.projectRepository;
            // Reset mocks between tests
            CurrentUserServiceMock.Reset();
            PublisherMock.Reset();

            //InitializeDatabase();
        }
        protected virtual void InitializeDatabase()
        {
            DbContext.Database.EnsureCreated();
        }

        public void Dispose()
        {
         //  DbContext.Database.EnsureDeleted();
            Scope?.Dispose();
            Client?.Dispose();
            GC.SuppressFinalize(this);
        }
    }


}
