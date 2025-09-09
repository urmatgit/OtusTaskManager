using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

using TaskboardService.Business.Services.Abstract;
using TaskboardService.Business.Services.Concrete;
using TaskboardService.Business.Settings;

namespace TaskboardService.Business.Extentions
{
    public static class ServiceExtentions
    {
        public static void RegisterSettings(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddOptions<WebAppSettings>().Bind(configuration.GetSection("WebApp"));
        }

        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ITaskboardService, Services.Concrete.TaskboardService>();
            services.AddScoped<ITaskItemService, TaskItemService>();
        }

        public static void RegisterDbContext(this IServiceCollection services, IConfiguration configuration)
        {           
            var connStr = Environment.GetEnvironmentVariable("MongoDb") ?? configuration.GetConnectionString("MongoDb");

            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

            ConventionRegistry.Register("camelCase", 
                new ConventionPack
                {
                    new CamelCaseElementNameConvention(),
                    new IgnoreExtraElementsConvention(true),
                    new IgnoreIfNullConvention(true)
                },
            t => true);

            services.AddSingleton<IMongoClient>(sp => 
            {
                return new MongoClient(connStr);
            });
        }
    }
}