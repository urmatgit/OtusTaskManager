
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;

using UserService.Api.Middlewares;
using UserService.Business;
using UserService.Business.Services.GRPC;
using UserService.DataAccess;

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
                // Добавление gRPC
                builder.Services.AddGrpc();
               // builder.Services.AddGrpcReflection();

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
                builder.Services.AddRegisCaching(builder.Configuration);
                builder.Services.AddApi(builder.Configuration);
                builder.Services.AddAuth(builder.Configuration);
                builder.Services.AddSwaggerGen(options =>
                {
                    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Пожалуйста, введите токен",
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer"
                    });

                    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new List<string>()
                        }
                    });
                });

                builder.Services.AddCors(options =>
                {
                    options.AddPolicy("AllowAnyOrigin",
                        builder =>
                        {
                            builder.AllowAnyOrigin()
                                   .AllowAnyHeader()
                                   .AllowAnyMethod()
                                   .WithExposedHeaders(
                   "Grpc-Status",
                   "Grpc-Message",
                   "Grpc-Encoding",
                   "Grpc-Accept-Encoding",
                   "X-Grpc-Web",
                   "Content-Type"
               );
                            ;
                        });
                });
                // Add SignalR
                builder.Services.AddSignalR();
                builder.Services.AddResponseCompression(opts =>
                {
                    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/octet-stream" });
                });
                builder.Services.AddBusiness(builder.Configuration);
                builder.Services.AddOpenApiDocument(configure =>
                {
                    configure.Title = "Service ";
                });
                var app = builder.Build();
                app.UseExceptionHandler();
                app.UseCors("AllowAnyOrigin");
                // app.UseMiddleware<ErrorHandlingMiddleware>();
                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }
                //Òîëüêî ïðè ðàáîòû ñ áàçîé 
                await app.Services.InitializeDatabasesAsync();
                //app.UseHttpsRedirection();


                app.UseAuthentication();
                app.UseAuthorization();

                app.MapControllers();
                //signalR
                app.MapHub<ProjectHub>("/projectHub");
                // gRPC endpoint
                app.MapGrpcService<GrpcUserService>()
                    .RequireCors("AllowAll")
                    .EnableGrpcWeb(); 
                //app.MapGrpcReflectionService();  // Enable reflection endpoint
                // Optional: gRPC-Web for browser clients
                // app.MapGrpcService<GrpcUserService>().EnableGrpcWeb();
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
