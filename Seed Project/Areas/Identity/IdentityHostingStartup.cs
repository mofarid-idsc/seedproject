using System;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Seed_Project.Areas.Identity.IdentityHostingStartup))]
namespace Seed_Project.Areas.Identity
{
  public class IdentityHostingStartup : IHostingStartup
  {
    public void Configure(IWebHostBuilder builder)
    {
      builder.ConfigureServices((context, services) => {
      //    services.AddDbContext<AppIdentityDbContext>(options =>
      //        options.UseSqlServer(
      //            context.Configuration.GetConnectionString("IdentityContextConnection")));

      //    services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
      //        .AddEntityFrameworkStores<AppIdentityDbContext>();
      });
    }
  }
}