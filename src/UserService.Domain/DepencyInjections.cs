using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;

namespace UserService.DataAccess
{
    public static class DepencyInjections
    {
        public static IServiceCollection AddPersistance(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddDbContext<TaskboardDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("TaskboardDb"));

            });
            return services;
        }
    }
}
