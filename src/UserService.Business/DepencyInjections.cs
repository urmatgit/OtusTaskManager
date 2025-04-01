using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using FluentValidation;
using System.Configuration;
using UserService.Business.Services.Auth;
using UserService.DataAccess.Entities;
using UserService.DataAccess.Persistence.Repositories;
using Microsoft.Extensions.Options;
using UserService.DataAccess.Enums;
using UserService.DataAccess.Common;
using System.Reflection;
using MediatR;
using UserService.Business.Application.Common;


namespace UserService.Business
{
    public static class DepencyInjections
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services, Microsoft.Extensions.Configuration.ConfigurationManager configuration)
        {
            //посредник
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            //
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
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
                foreach(var role in  Enum.GetNames(typeof(ProjectRole)))
                {
                    if (role == GlobalConstantes .AdminName)
                    {
                        options.AddPolicy($"{role}Role", policy =>
                            policy.RequireRole($"{role}"));
                    }else
                    {
                        options.AddPolicy($"{role}Role", policy =>
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
    }
}
