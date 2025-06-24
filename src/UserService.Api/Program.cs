
using UserService.DataAccess;
using UserService.Business;
using UserService.Api.Middlewares;
using Serilog;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace UserService.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()

                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json")
                 .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
            try
            {
                var builder = WebApplication.CreateBuilder(args);
                //logger

                

                builder.Logging.AddSerilog();
               
                Log.Information("Starting up");
                //Handling Validation Exceptions in Pipeline
                builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
                builder.Services.AddProblemDetails();
                // Add services to the container.

                builder.Services.AddControllers();
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                // Configure Swagger/OpenAPI
                builder.Services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Taskboard API", Version = "v1" });

                    // Optional: Include XML comments
                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    c.IncludeXmlComments(xmlPath);
                });
                builder.Services.AddHttpContextAccessor();
                builder.Services.AddRabbitMQ(builder.Configuration);
                builder.Services.AddPersistance(builder.Configuration);

                builder.Services.AddApi(builder.Configuration);
                builder.Services.AddAuth(builder.Configuration);

                builder.Services.AddBusiness(builder.Configuration);
                builder.Services.AddOpenApiDocument(configure =>
                {
                    configure.Title = "Service ";
                });
                var app = builder.Build();
                app.UseExceptionHandler();
                app.UseCors("AllowOrigin");
                // app.UseMiddleware<ErrorHandlingMiddleware>();
                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }
                //Только при работы с базой 
                await app.Services.InitializeDatabasesAsync();
                app.UseHttpsRedirection();


                app.UseAuthentication();
                app.UseAuthorization();

                app.MapControllers();
                
                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
