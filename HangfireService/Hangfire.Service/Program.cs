using Hangfire.Business;

using Microsoft.Extensions.Hosting;

namespace Hangfire.Service
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            builder.Services.RegisterHost(builder.Configuration);

            var app = builder.Build();

            await app.RunAsync();
        }
    }
}