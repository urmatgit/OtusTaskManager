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
using UserService.DataAccess.Persistence.Repositories;

namespace UserService.DataAccess
{
    public static class DepencyInjections
    {
        public static IServiceCollection AddPersistance(this IServiceCollection services, Microsoft.Extensions.Configuration.ConfigurationManager configuration)
        {
            services.AddDbContext<TaskboardDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("TaskboardDb"));

            });

            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
