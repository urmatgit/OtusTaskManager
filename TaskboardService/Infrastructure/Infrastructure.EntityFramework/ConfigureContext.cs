using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.EntityFramework
{
    public static class ConfigureDbContext
    {
        public static IServiceCollection ConfigureContext(this IServiceCollection services,
            string connectionString,
            string databaseName
            )
        {
            services.AddDbContext<DatabaseContext>(optionsBuilder =>
                optionsBuilder.UseMongoDB(connectionString, databaseName)
            );

            return services;
        }
    }
}
