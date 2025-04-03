using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using UserService.DataAccess.Persistence.Data;
using UserService.DataAccess.Persistence.Repositories.Auth;
using UserService.DataAccess.Persistence.Repositories;
using UserService.DataAccess.Entities;
using UserService.DataAccess.Persistence.Repositories.Entities;

namespace UserService.DataAccess
{
    public static class DepencyInjections
    {
        public static   IServiceCollection AddPersistance(this IServiceCollection services, Microsoft.Extensions.Configuration.ConfigurationManager configuration)
        {
            // если установлен локальный postgrsql server

            services.AddDbContext<TaskboardDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("TaskboardDb"));

            });

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IDbInitializer, TaskboardDbInitializer>();

            //для теста 

            //            services.AddSingleton<IUserRepository, UserMemRepository>();

            return services;
        }
        public static async Task InitializeDatabasesAsync(this IServiceProvider services, CancellationToken cancellationToken = default)
        {
            // Create a new scope to retrieve scoped services
            using var scope = services.CreateScope();

            await scope.ServiceProvider.GetRequiredService<IDbInitializer>()
                .InitializeDb();
        }
    }
}
