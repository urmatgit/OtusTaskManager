using FluentValidation;
using Keycloak.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Namotion.Reflection;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UserService.Business.Services.Auth;
using UserService.DataAccess.Enums;
using UserService.DataAccess.Persistence;


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
            .AddAuthentication(options=>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
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
                    NameClaimType = "preferred_username",  // или "sub", "email"
                    RoleClaimType = "realm_access.roles" // ✅ Именно это решает проблему!
                    //    //ValidateIssuerSigningKey = true,
                    //    //ValidateIssuer = true,
                    //  ValidAudience= configuration["Keycloak:Audience"],
                    //    //ValidIssuer = configuration["Keycloak:Authority"]


                };
                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var claims = context.Principal?.Claims.ToList();

                        // Ищем claim с типом "realm_access" и значением в JSON
                        var realmAccessClaim = claims?.FirstOrDefault(c => c.Type == "realm_access");
                        if (realmAccessClaim != null)
                        {
                            try
                            {
                                using var doc = JsonDocument.Parse(realmAccessClaim.Value);
                                var rolesElement = doc.RootElement.GetProperty("roles");

                                var roleClaims = new List<Claim>();
                                foreach (var role in rolesElement.EnumerateArray())
                                {
                                    roleClaims.Add(new Claim(ClaimTypes.Role, role.GetString()));
                                }

                                context.Principal?.AddIdentity(new ClaimsIdentity(roleClaims));
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Ошибка парсинга realm_access: " + ex.Message);
                            }
                        }
                        return Task.CompletedTask;
                    }
                };
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
