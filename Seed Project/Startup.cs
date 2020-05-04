using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Helpers;
using ApplicationCore.Interfaces;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Seed_Project
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }
    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<AppIdentityDbContext>(options =>
          options.UseSqlServer(
            Configuration.GetConnectionString("IdentityContextConnection")));

      services.AddIdentity<ApplicationUser, ApplicationRole>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddEntityFrameworkStores<AppIdentityDbContext>()
        .AddDefaultUI()
        .AddDefaultTokenProviders();
      services.AddSingleton(new ResourceManager("Seed_Project.Resources.Welcome", typeof(Startup).Assembly));
      services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
      services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
      services.AddControllersWithViews();
      //services.AddControllers();
      //services.AddDbContext<AppIdentityDbContext>
      services.AddRazorPages();

     
        services.AddMvc();
      services.AddAuthorization(options =>
      {
        options.AddPolicy("EmployeesOnly", policy => policy.RequireClaim("EmployeeId"));
      });

      var context = services.BuildServiceProvider()
                      .GetService<AppIdentityDbContext>();
      var folderDetails = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\{"Helpers\\Permissions.json"}");
      var JSON = System.IO.File.ReadAllText(folderDetails);
      dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(JSON);
      services.AddAuthorization(options =>
      {
        foreach (var permission in jsonObj["Permissions"])
        {
          // assuming .Permission is enum
          string[] vs =  permission.Actions.ToString().Split(',');
          foreach (var item in vs)
          {
            var permissionvm = new EMS_Permission
            {
              ControllerName = permission["Controller"],
              ActionName = item
            };
            options.AddPolicy(permissionvm.ControllerName.ToString() + '.' + permissionvm.ActionName.ToString(),
                policy => policy.Requirements.Add(new PermissionRequirement(permissionvm.ControllerName.ToString() + '.'
                + permissionvm.ActionName.ToString())));
          }
        
        }
      });


      // Add Permissions In Policy
      //services.AddAuthorization(options =>
      //{
      //  options.AddPolicy(Permissions.Dashboards.View, builder =>
      //  {
      //    builder.AddRequirements(new PermissionRequirement(Permissions.Dashboards.View));
      //  });

      //  options.AddPolicy(Permissions.Users.Create, builder =>
      //  {
      //    builder.AddRequirements(new PermissionRequirement(Permissions.Users.Create));
      //  });

      //  options.AddPolicy(Permissions.Dashboards.Create, builder =>
      //  {
      //    builder.AddRequirements(new PermissionRequirement(Permissions.Dashboards.Create));  
      //  });

      //  options.AddPolicy(Permissions.Users.View, builder =>
      //  {
      //    builder.AddRequirements(new PermissionRequirement(Permissions.Users.View));
      //  });

      //  // The rest omitted for brevity.
      //});


      // The rest omitted for brevity.

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }
      app.UseHttpsRedirection();
      app.UseStaticFiles();
      app.UseSerilogRequestLogging();
      app.UseRouting();
      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller=Home}/{action=Index}/{id?}");
        endpoints.MapRazorPages();
      });

    }
  }
}
