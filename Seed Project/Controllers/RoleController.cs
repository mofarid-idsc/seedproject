using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Seed_Project.Controllers
{
    public class RoleController : Controller
  {
    private readonly RoleManager<ApplicationRole> _roleManager;
    public RoleController(RoleManager<ApplicationRole> roleManager)
    {
      _roleManager = roleManager;
    }
    public IActionResult Index()
        {
      var roles = _roleManager.Roles.ToList();
            return View(roles);
        }

    public IActionResult Create()
    {
      return View(new IdentityRole());
    }
    [HttpPost]
    public async Task<IActionResult> Create(ApplicationRole role)
    {
      await _roleManager.CreateAsync(role);
      return RedirectToAction("Index");
    }
    }
}