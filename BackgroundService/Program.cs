using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace BackgroundService
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            using (IHost host = Host.CreateDefaultBuilder(args)
                .UseWindowsService(options =>
                {
                    // the name set in code __should__ match that in the package service extension, this one is used for start, stop operation logging
                    options.ServiceName = "BackgroundService";
                })
                .ConfigureServices(services =>
                {
                    services.AddSingleton<JokeService>();
                    services.AddHostedService<WindowsBackgroundService>();
                })
                .Build())
            {
                await host.RunAsync();
            }
        }
    }
}
