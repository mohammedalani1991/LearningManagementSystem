using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(LearningManagementSystem.Areas.Identity.IdentityHostingStartup))]
namespace LearningManagementSystem.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                
            });
        }
    }
}