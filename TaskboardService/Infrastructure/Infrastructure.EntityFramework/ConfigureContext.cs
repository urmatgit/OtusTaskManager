using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Infrastructure.EntityFramework
{
    public static class ConfigureDbContext
    {
        public static IServiceCollection ConfigureContext(this IServiceCollection services,
            string connectionString,
            string databaseName
            )
        {
            services.AddDbContext<DatabaseContext>(optionsBuilder => {
                var mongoClient = new MongoClient(connectionString);
                optionsBuilder.UseMongoDB(mongoClient, databaseName);                
                }
            );

            return services;
        }
    }
}
