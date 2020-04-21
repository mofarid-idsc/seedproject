using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Serilog;

namespace Seed_Project.Controllers
{
  public class ApplicationRoleController : Controller
  {
    private readonly AppIdentityDbContext _context;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public ApplicationRoleController(AppIdentityDbContext context, RoleManager<ApplicationRole> roleManager)
    {
      _context = context;
      _roleManager = roleManager;
    }

    // GET: ApplicationRole
    public async Task<IActionResult> Index()
    {
      return View(await _context.Roles.ToListAsync());
    }

    // GET: ApplicationRole/Details/5
    public async Task<IActionResult> Details(string id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var applicationRole = await _context.Roles
          .FirstOrDefaultAsync(m => m.Id == id);
      if (applicationRole == null)
      {
        return NotFound();
      }

      return View(applicationRole);
    }

    // GET: ApplicationRole/Create
    public IActionResult Create()
    {
      return View();
    }

    // POST: ApplicationRole/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Description,Id,Name,NormalizedName,ConcurrencyStamp")] ApplicationRole applicationRole)
    {
      if (ModelState.IsValid)
      {
        //_context.Add(applicationRole);
        //await _context.SaveChangesAsync();
        var role = new ApplicationRole
        {
          Name = applicationRole.Name,
          Description = applicationRole.Description
        };

        var result = await _roleManager.CreateAsync(role);
        if (result.Succeeded)
        {
          //_logger.LogInformation("User created a new account with password.");
          Log.Logger.Information("A New {ObjectName} Was Added With ID: {ID}, Name: {Name}, Description: {Description}"
                , "Role", role.Id, role.Name, role.Description);
        }

        return RedirectToAction(nameof(Index));
      }
      return View(applicationRole);
    }

    // GET: ApplicationRole/Edit/5
    public async Task<IActionResult> Edit(string id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var applicationRole = await _context.Roles.FindAsync(id);
      if (applicationRole == null)
      {
        return NotFound();
      }
      return View(applicationRole);
    }

    // POST: ApplicationRole/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, [Bind("Description,Id,Name,NormalizedName,ConcurrencyStamp")] ApplicationRole applicationRole)
    {
      if (id != applicationRole.Id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(applicationRole);
          await _context.SaveChangesAsync();
          Log.Logger.Information("A {ObjectName} Was Edited With ID: {ID}, Name: {Name}, Description: {Description}"
          , "Role", applicationRole.Id, applicationRole.Name, applicationRole.Description);
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!ApplicationRoleExists(applicationRole.Id))
          {
            return NotFound();
          }
          else
          {
            throw;
          }
        }
        return RedirectToAction(nameof(Index));
      }
      return View(applicationRole);
    }

    // GET: ApplicationRole/Delete/5
    public async Task<IActionResult> Delete(string id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var applicationRole = await _context.Roles
          .FirstOrDefaultAsync(m => m.Id == id);
      if (applicationRole == null)
      {
        return NotFound();
      }

      return View(applicationRole);
    }

    // POST: ApplicationRole/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
      var applicationRole = await _context.Roles.FindAsync(id);
      _context.Roles.Remove(applicationRole);
      await _context.SaveChangesAsync();
      Log.Logger.Information("A {ObjectName} Was Deleted With ID: {ID}, Name: {Name}, Description: {Description}"
                , "Role", applicationRole.Id, applicationRole.Name, applicationRole.Description); 
      return RedirectToAction(nameof(Index));
    }

    private bool ApplicationRoleExists(string id)
    {
      return _context.Roles.Any(e => e.Id == id);
    }
  }
}
