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

            var app = builder.Build();

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