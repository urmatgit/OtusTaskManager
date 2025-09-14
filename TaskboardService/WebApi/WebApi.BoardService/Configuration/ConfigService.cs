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
        
        /// <summary>
        /// Config Database context
        /// </summary>
        /// <param name="services"></param>
        /// <param name="builder"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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

        /// <summary>
        /// Config Repositories
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureRepository(
            this IServiceCollection services            
            )
        {
            services
                .AddTransient<ITaskBoardRepository, TaskBoardRepository>()
                .AddTransient<IBoardColumnRepository, BoardColumnRepository>()
                .AddTransient<ITaskItemRepository, TaskItemRepository>()
                .AddTransient<ITaskCommentRepository, TaskCommentRepository>()
                .AddTransient<ICheckListRepository, CheckListRepository>()
                ;

            return services;
        }
    }
}
