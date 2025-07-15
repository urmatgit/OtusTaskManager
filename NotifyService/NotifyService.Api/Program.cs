using System.Text;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using NotifyService.Business;

namespace NotifyService.Api
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder();

            builder.Services.AddLogging(opt =>
            {
                opt.AddSimpleConsole(c =>
                {
                    c.TimestampFormat = "[dd.MM.yyyy HH:mm:ss.fff] ";
                    c.SingleLine = true;
                });
            });

            builder.Services.AddConfigurations(builder.Configuration);
            builder.Services.AddServices();
            builder.Services.AddHostedServices(builder.Configuration);

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var app = builder.Build();

            await app.RunAsync();
        }
    }
}