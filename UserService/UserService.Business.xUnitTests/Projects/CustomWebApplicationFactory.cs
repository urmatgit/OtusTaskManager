using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UserService.Business.Services.Auth;
using UserService.DataAccess.Persistence;
using UserService.DataAccess.Persistence.Repositories;

namespace UserService.Business.xUnitTests.Projects
{
    public class CustomWebApplicationFactory<TProgram>: WebApplicationFactory<TProgram> where TProgram : class
    {
        public Mock<ICurrentUser> CurrentUserMock { get; private set; }=new Mock<ICurrentUser>();
        public Mock<IPublisher> PublisherMock { get; private set; }=new Mock<IPublisher>();
        public Mock<IProjectRepository> projectRepository { get;private set; }=new Mock<IProjectRepository>();
        protected override IHost CreateHost(IHostBuilder builder)
        {
            return base.CreateHost(builder);
        }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.AddSingleton(CurrentUserMock.Object);
                services.AddSingleton(PublisherMock.Object);
                services.AddSingleton<IProjectRepository>(projectRepository.Object);
                //var descriptor = services.SingleOrDefault(
                //d => d.ServiceType == typeof(DbContextOptions<TaskboardDbContext>));

                //if (descriptor != null)
                //{
                //    services.Remove(descriptor);
                //}

                //services.RemoveAll<DbContextOptions<TaskboardDbContext>>();
                //services.AddDbContext<TaskboardDbContext>(options =>
                //{
                //    options.UseInMemoryDatabase("memoryDb");
                //});
            });
            base.ConfigureWebHost(builder);
        }
        //protected override void ConfigureWebHost(IWebHostBuilder builder)
        //{
        //    builder.ConfigureTestServices(services =>
        //    {
        //        services.AddSingleton(CurrentUserMock.Object);
        //        services.AddSingleton(PublisherMock.Object);

        //        services.RemoveAll<DbContextOptions<TaskboardDbContext>>();
        //        //// Remove the existing DbContextOptions
        //        //var descriptor = services.SingleOrDefault(
        //        //    d => d.ServiceType == typeof(DbContextOptions<TaskboardDbContext>));

        //        //if (descriptor != null)
        //        //{
        //        //    services.Remove(descriptor);
        //        //}
        //        services.AddDbContext<AppDbContext>(options =>
        //        {
        //            options.UseInMemoryDatabase("memoryDb");
        //        });
        //        //add mapper
        //        //var config = TypeAdapterConfig.GlobalSettings;
        //        //config.Scan(Assembly.GetExecutingAssembly());
        //        //services.AddSingleton(config);
        //        //services.AddScoped<IMapper, ServiceMapper>();

        //    });
        //    base.ConfigureWebHost(builder);
        //}

    }
}
