using LearningManagementSystem.Services.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace LearningManagementSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DataBaseScriptsHelper.HandleDataBaseScripts();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
