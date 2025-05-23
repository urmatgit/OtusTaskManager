using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Microsoft.Extensions.Options;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using FluentValidation;

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
                //c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
                c.AddPolicy("AllowOrigin", options => options.WithOrigins(configuration.GetSection("CORS:Origins").Get<string[]>())
                    .WithHeaders(configuration.GetSection("CORS:Headers").Get<string[]>())
                    .WithMethods(configuration.GetSection("CORS:Methods").Get<string[]>()));
            });

            
            return services;
        }
    }
}
