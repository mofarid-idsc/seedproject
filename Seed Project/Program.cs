using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Seed_Project
{
  public class Program
  {
    public static void Main(string[] args)
    {
      CreateMSSqlLoggerUsingJSONFile();
      CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
      return Host.CreateDefaultBuilder(args)
          .UseSerilog()
          .ConfigureWebHostDefaults(webBuilder =>
          {
            webBuilder.UseStartup<Startup>();
          });
    }
    private static void CreateMSSqlLoggerUsingJSONFile()
    {
      var configuration = new ConfigurationBuilder()
      .AddJsonFile("appsettings.json")
      .Build();

      Log.Logger = new LoggerConfiguration()
          .ReadFrom.Configuration(configuration)
          .CreateLogger();
    }
  }
}
