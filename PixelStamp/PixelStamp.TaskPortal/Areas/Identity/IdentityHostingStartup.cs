using System;

using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(ISolutionTask.Areas.Identity.IdentityHostingStartup))]
namespace ISolutionTask.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
               // services.AddDefaultIdentity<IdentityUser>().AddRoles<IdentityRole>().AddEntityFrameworkStores<UserDbContext>();
            });

        }
        
    }
}