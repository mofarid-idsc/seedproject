using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Resources;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Seed_Project.Models;
using Serilog;

namespace Seed_Project.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;
    private readonly ResourceManager _resourceManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public HomeController(ILogger<HomeController> logger, ResourceManager resourceManager, UserManager<ApplicationUser> userManager,
    RoleManager<ApplicationRole> roleManager)
    {
      _roleManager = roleManager;
      _userManager = userManager;
      //hello github
      //Sara Trial
      _logger = logger;
      _resourceManager = resourceManager;
      //_localizer = localizer;
    }
    private async Task<ApplicationUser> GetCurrentUser()
    {
      var user = _userManager.GetUserAsync(HttpContext.User);
      return await user;
    }
    public async Task<IActionResult> Index()
    {
     

      //_logger.LogInformation("helloinfo");
      //_logger.LogWarning("hellowarning");
      //_logger.LogError("helloerror");
      //_logger.LogCritical("hellocritical");

      Log.Logger.Warning("Warning");
      Log.Logger.Information("Information");
      Log.Logger.Error("Error");
      Log.Logger.Fatal("Fatal");
      ViewBag.value = _resourceManager.GetString("Hello", new System.Globalization.CultureInfo("es"));
      return View();
    }

    public IActionResult Privacy()
    {
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    
    [HttpPost]
    public IActionResult SetLanguage(string culture)
    {
      Response.Cookies.Append(
          CookieRequestCultureProvider.DefaultCookieName,
          CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
      new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
      );

      return RedirectToAction("Index");
    }

    
  }
}
