using FluentValidation;
using Mapster;
using MapsterMapper;
using MediatR;
using MediatR.NotificationPublishers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RabbitMq.Connector.Publisher;
using RabbitMQ.Connector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UserService.Business.Common;
using UserService.Business.Services.Auth;
using UserService.Business.Services.GRPC;
using UserService.Business.Services.Mail;
using UserService.Business.Services.RabbitMQ;
using UserService.DataAccess.Common;
using UserService.DataAccess.Entities;
using UserService.DataAccess.Enums;
using UserService.DataAccess.Persistence.Data;
using UserService.DataAccess.Persistence.Repositories;
using UserService.Business.Services.GRPC;


namespace UserService.Business
{
    public static class DepencyInjections
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services, Microsoft.Extensions.Configuration.ConfigurationManager configuration)
        {
            //посредник
            services.AddMediatR(cfg =>
                {
                    cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
                    cfg.NotificationPublisher = new TaskWhenAllPublisher();
                });
            //
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMapping();
            
            services.AddConfigureMailing();
            services.AddScoped<IDbInitializer, BusnessDataInitializer>();
            return services;
        }
        /// <summary>
        /// все зависимости связанные с авторизацией
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddAuth(this IServiceCollection services, Microsoft.Extensions.Configuration.ConfigurationManager configuration)
        {
            var jwtSettings = new JwtSettings();
            configuration.Bind(JwtSettings.SectionName, jwtSettings);
            services.AddSingleton(Options.Create(jwtSettings));
            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));


            // Configure Authorization
        //    public enum ProjectRole
        //{
        //    Owner,
        //    Admin,
        //    User,
        //    Editor
        //}
        services.AddAuthorization(options =>
            {
                foreach(var role in  Enum.GetNames(typeof(UserRole)))
                {
                    if (role == GlobalConstantes .AdminName)
                    {
                        options.AddPolicy($"{role}", policy =>
                            policy.RequireRole($"{role}"));
                    }else
                    {
                        options.AddPolicy($"{role}", policy =>
                        policy.RequireRole($"{role}", GlobalConstantes.AdminName));
                    }
                }
                //options.AddPolicy("RequireAdminRole", policy =>
                //    policy.RequireRole("Admin"));

                //options.AddPolicy("RequireUserRole", policy =>
                //    policy.RequireRole("User", "Admin"));

                //options.AddPolicy("RequireOwnerRole", policy =>
                //    policy.RequireRole("Owner", "Admin"));
                //options.AddPolicy("RequireEditorRole", policy =>
                //    policy.RequireRole("Editor", "Admin"));
            });

            services.AddScoped<IUserAuthService, UserAuthService>();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddSingleton<IJwtService, JwtService>();
            services.AddTransient<ICurrentUser, CurrentUser>();
            
            
            services.AddScoped<GrpcUserService>();

            services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
                });

            return services;
        }
        //Mapper  MapsterMapper
        public static IServiceCollection AddMapping(this IServiceCollection services)

        {
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly());
            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();
            return services;
        }
        public static IServiceCollection AddConfigureMailing(this IServiceCollection services)
        {

            
            services.AddTransient<IMailService, SendMailThroughRabbtiMQService>(); //send mail confirm to rabbitMQ
          //  services.AddTransient<IMailService, SmtpMailService>();
            services.AddOptions<MailOptions>().BindConfiguration(nameof(MailOptions));
            return services;
        }

        public static IServiceCollection AddRabbitMQ(this IServiceCollection services, Microsoft.Extensions.Configuration.ConfigurationManager configuration)
        {
            var rabbitMQConnectionString = configuration.GetConnectionString("RabbitMQ") ?? "amqp://localhost";
            ///временно 
            services.AddSingleton<IRabbitConnectionFactory>(provider =>
            {
                //return new RabbitConnectionFactoryFake(rabbitMQConnectionString);
                return new RabbitConnectionFactory(rabbitMQConnectionString);
            });
            services.AddSingleton<IBrokerPublisher<PublishMassage<Project>>, ProjectRabbitMqPublisher>();
            services.AddSingleton<IBrokerPublisher<PublishMassage<User>>, UserRabbitMqPublisher>();
            services.AddSingleton(typeof(IBrokerPublisher<>), typeof(SendMessageService<>));
            return services;
        }
    }
}
