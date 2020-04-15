using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Seed_Project.Models;

namespace Seed_Project.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;
    private readonly ResourceManager _resourceManager;
   
    public HomeController(ILogger<HomeController> logger, ResourceManager resourceManager)
    {
      //hello github
      _logger = logger;
      _resourceManager = resourceManager;
      //_localizer = localizer;
    }

    public IActionResult Index()
    {
      _logger.LogInformation("helloinfo");
      _logger.LogWarning("hellowarning");
      _logger.LogError("helloerror");
      _logger.LogCritical("hellocritical");
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
