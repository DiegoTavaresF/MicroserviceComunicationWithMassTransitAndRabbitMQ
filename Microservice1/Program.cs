using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace Microservice1
{
    public class Program
    {
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
                //.ConfigureLogging(builder => { builder.AddFilter("MassTransit", LogLevel.Debug); });

        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();


        }
    }
}