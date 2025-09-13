using Infrastructure.EntityFramework;
using Repository.Abstractions;
using Repository.Implementation;

namespace WebApi.BoardService.Configuration
{
    /// <summary>
    /// Конфигурвция контейнеров
    /// </summary>
    public static class ConfigService
    {
        private static readonly string SConnectionStringNotFound = "Не задана строка подключения к БД. Настройте ConnectionStrings:{Connection=''} в appsettings.json";
        public static IServiceCollection ConfigureDatabaseContext(
            this IServiceCollection services,
            IHostApplicationBuilder builder
            )
        {
            services
                .ConfigureContext(
                    builder.Configuration.GetConnectionString("Connection") ??
                        throw new Exception(SConnectionStringNotFound),
                    builder.Configuration.GetConnectionString("DatabaseName") ??
                        throw new Exception(SConnectionStringNotFound)
                    );

            return services;
        }

        public static IServiceCollection ConfigureRepository(
            this IServiceCollection services            
            )
        {
            services
                .AddTransient<ITaskBoardRepository, TaskBoardRepository>()
                ;

            return services;
        }
    }
}
