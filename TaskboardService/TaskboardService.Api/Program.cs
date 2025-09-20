using Microsoft.AspNetCore.Builder;

using TaskboardService.Business.Extentions;

namespace TaskboardService.Api
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();

            builder.Services.RegisterSettings(builder.Configuration);
            builder.Services.RegisterDbContext(builder.Configuration);
            builder.Services.RegisterServices();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            var app = builder.Build();

            app.UseCors("AllowReactApp");

            if (app.Environment.IsDevelopment())
            {
                //app.UseSwagger();
                //app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.MapControllers();

            await app.RunAsync();
        }
    }
}