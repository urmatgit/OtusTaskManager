using FluentValidation;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Namotion.Reflection;
using StackExchange.Redis;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Business.Services.Auth;
using UserService.Business.Services.Radis;
using UserService.DataAccess.Enums;

using UserService.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Microsoft.Extensions.Options;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using FluentValidation;
using StackExchange.Redis;
using UserService.Business.Services.Radis;

namespace UserService.Api
{
    public static class DepencyInjections
    {
        
            public static IServiceCollection AddApi(this IServiceCollection services, Microsoft.Extensions.Configuration.ConfigurationManager configuration)
        {
            //
            //services.AddValidatorsFromAssemblyContaining<Program>();
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.WithOrigins("http://localhost:60983")
                .AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
                //c.AddPolicy("AllowOrigin", options => options.WithOrigins(configuration.GetSection("CORS:Origins").Get<string[]>())
                //    .WithHeaders(configuration.GetSection("CORS:Headers").Get<string[]>())
                //    .WithMethods(configuration.GetSection("CORS:Methods").Get<string[]>()));
            });

            
            return services;
        }
 
        public static IServiceCollection AddRegisCaching(this IServiceCollection services, IConfiguration configuration)
        {
            // Конфигурация Redis
            var RedisOptionconfiguration = configuration.GetSection("RedisOption:Configuration").Get<string>()
                ?? throw new ArgumentNullException("Redis connection string is missing");

            // Регистрация IDistributedCache
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = RedisOptionconfiguration;
                options.InstanceName = "TaskboardProject_";
            });

            services. AddSingleton<IConnectionMultiplexer>(sp =>

             {
                 //configuration.GetSection("CORS:Origins").Get<string[]>(
                 
                 return ConnectionMultiplexer.Connect(RedisOptionconfiguration);
             });
            services.AddScoped<ICacheService, RedisCacheService>();
            return services;
        }

    }
}
