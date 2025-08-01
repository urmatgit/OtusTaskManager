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
using Microsoft.AspNetCore.Authorization;
using UserService.Business.Services.Auth;
using System.Security.Claims;
using System.Text.Json;
using UserService.DataAccess.Enums;
using System.Data;
using Keycloak.Net;
using System.Buffers.Text;


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
        public static IServiceCollection AddKeycloakAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<KeycloakUserService>();
            services.AddScoped<KeycloakUserService>();
            services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = Convert.ToBoolean($"{configuration["Keycloak:require-https"]}");
                //x.MetadataAddress = $"{configuration["Keycloak:server-url"]}/realms/OTUS/.well-known/openid-configuration";
                options.Authority = configuration["Keycloak:Authority"];
                options.Audience = configuration["Keycloak:Audience"];
                options.RequireHttpsMetadata = false; // only for development
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    //ValidateIssuerSigningKey = true,
                    //ValidateIssuer = true,
                    //ValidAudience= configuration["Keycloak:Audience"],
                    //ValidIssuer = configuration["Keycloak:Authority"]
                    
                    
                };
                //options.Events = new JwtBearerEvents
                //{
                //    OnTokenValidated = context =>
                //    {
                //        if (context.Principal.Identity is ClaimsIdentity identity)
                //        {
                //            var realmAccess = context.Principal.FindFirst("realm_access")?.Value;
                //            if (!string.IsNullOrEmpty(realmAccess))
                //            {
                //                foreach (string role in Enum.GetNames(typeof(ProjectRole)))
                //                {
                //                    identity.AddClaim(new Claim(ClaimTypes.Role, role));
                //                }
                //                //var realmAccessObj = JsonSerializer.Deserialize<RealmAccess>(realmAccess);

                //                //foreach (var role in realmAccessObj.Roles)
                //                //{
                //                //    identity.AddClaim(new Claim(ClaimTypes.Role, role));
                //                //}
                //            }
                //        }
                //        return Task.CompletedTask;
                //    }
                //};
                //options.TokenValidationParameters = new TokenValidationParameters
                //{
                //    //RoleClaimType = "groups",
                //    //NameClaimType = $"{configuration["Keycloak:name_claim"]}",
                //    ValidAudience = $"{configuration["Keycloak:audience"]}",
                //    // https://stackoverflow.com/questions/60306175/bearer-error-invalid-token-error-description-the-issuer-is-invalid
                //    ValidateIssuer = Convert.ToBoolean($"{configuration["Keycloak:validate-issuer"]}"),
                //};
            });
            // Add Keycloak admin client
            //services.AddHttpClient("KeycloakAdmin",client =>
            //{
            //    client.BaseAddress = new Uri($"{configuration["Keycloak:baseUrl"]}/admin/realms/{configuration["Keycloak:Realm"]}/");

            //    //keycloakOptions.AuthServerUrl = builder.Configuration["Keycloak:Authority"];
            //    //keycloakOptions.Realm = builder.Configuration["Keycloak:Realm"];
            //    //keycloakOptions.ClientId = builder.Configuration["Keycloak:AdminClientId"];
            //    //keycloakOptions.ClientSecret = builder.Configuration["Keycloak:AdminClientSecret"];

            //});
            //services.AddTransient(provider =>
            //{
            //    var factory = provider.GetRequiredService<IHttpClientFactory>();
            //    return new KeycloakClient(factory.CreateClient("KeycloakAdmin"),Base64Url:$"{configuration["Keycloak:baseUrl"]}")
            //})

            //services.AddScoped<IUserAuthService, UserAuthService>();
            services.AddTransient<ICurrentUser, CurrentUser>();
            services.AddAuthorization(o =>
            {
                //o.DefaultPolicy = new AuthorizationPolicyBuilder()
                //    .RequireAuthenticatedUser()
                //    //.RequireClaim("email_verified", "true")
                //    .Build();
                o.AddPolicy("AdminPolicy", policy =>
                policy.RequireRole("Admin"));
            });
            return services;
        }
    }
}
