using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Seed_Project.Controllers
{
  [Authorize]
    public class DashboardsController : Controller
    {
      public DashboardsController( )
    {

    }
    //Policy Permissions
    [Authorize("Dashboards.View")]
    public IActionResult Index()
    {
      return View();
    }

    [Authorize("Dashboards.Create")]
    public IActionResult Create()
    {
      return View();
    }
  }
}