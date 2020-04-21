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
  public class UserController : Controller
  {
    private readonly AppIdentityDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public UserController(AppIdentityDbContext context, UserManager<ApplicationUser> userManager)
    {
      _context = context;
      _userManager = userManager;
    }

    // GET: User
    public async Task<IActionResult> Index()
    {
      return View(await _context.Users.ToListAsync());
    }

    // GET: User/Details/5
    public async Task<IActionResult> Details(string id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var applicationUser = await _context.Users
          .FirstOrDefaultAsync(m => m.Id == id);
      if (applicationUser == null)
      {
        return NotFound();
      }

      return View(applicationUser);
    }

    // GET: User/Create
    public IActionResult Create()
    {
      //ViewBag.Roles = new SelectList(_context.Roles, "Id", "Name");
      ViewBag.Roles = _context.Roles.ToListAsync();
      return View();
    }

    // POST: User/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,DOB,CreateDate,ModifyDate,JobTitle,HomePage,UserOfficeID,Photo,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] ApplicationUser applicationUser
                , string[] Roles)
    {
      if (ModelState.IsValid)
      {
        //_context.Add(applicationUser);
        //await _context.SaveChangesAsync();
        var user = new ApplicationUser
        {
          Name = applicationUser.Name,
          DOB = applicationUser.DOB,
          UserName = applicationUser.UserName,
          Email = applicationUser.Email,
          PhoneNumber = applicationUser.PhoneNumber,
          Photo = applicationUser.Photo
        };

        var result = await _userManager.CreateAsync(user, applicationUser.PasswordHash);
        if (result.Succeeded)
        {

          //_logger.LogInformation("User created a new account with password.");
          Log.Logger.Information("A New {ObjectName} Was Added With ID: {ID}, Name: {Name}, Username: {UserName}, Email: {Email}, PhoneNumber: {PhoneNumber}, DateOfBirth: {DateOfBirth}"
                , "User", user.Id, user.Name, user.UserName, user.Email, user.PhoneNumber, user.DOB);

          List<ApplicationUserRole> userRoles = new List<ApplicationUserRole>();

          foreach (string r in Roles)
          {
            userRoles.Add(new ApplicationUserRole { UserId = applicationUser.Id, RoleId = r });
          }
//          _context.Update(user);
 //         await _context.SaveChangesAsync();

        }

        return RedirectToAction(nameof(Index));
      }
      return View(applicationUser);
    }

    // GET: User/Edit/5
    public async Task<IActionResult> Edit(string id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var applicationUser = await _context.Users.FindAsync(id);
      if (applicationUser == null)
      {
        return NotFound();
      }
      //ViewBag.Roles = _context.Roles.ToListAsync();
      ViewBag.Roles = new SelectList(_context.Roles, "Id", "Name");

      return View(applicationUser);
    }

    // POST: User/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, [Bind("Name,DOB,CreateDate,ModifyDate,JobTitle,HomePage,UserOfficeID,Photo,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] ApplicationUser applicationUser
              , string[] Roles)
    {
      if (id != applicationUser.Id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        { 
          List<ApplicationUserRole> userRoles = new List<ApplicationUserRole>(); 

          foreach (string r in Roles)
          {
            userRoles.Add(new ApplicationUserRole { UserId = applicationUser.Id, RoleId = r });
          }
          //applicationUser.UserRoles = userRoles;
          
          _context.Update(applicationUser);
          await _context.SaveChangesAsync();
          
          Log.Logger.Information("A {ObjectName} Was Edited With ID: {ID}, Name: {Name}, Username: {UserName}, Email: {Email}, PhoneNumber: {PhoneNumber}, DateOfBirth: {DateOfBirth}"
              , "User", applicationUser.Id, applicationUser.Name, applicationUser.UserName, applicationUser.Email, applicationUser.PhoneNumber, applicationUser.DOB);

        }
        catch (DbUpdateConcurrencyException)
        {
          if (!ApplicationUserExists(applicationUser.Id))
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
      return View(applicationUser);
    }

    // GET: User/Delete/5
    public async Task<IActionResult> Delete(string id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var applicationUser = await _context.Users
          .FirstOrDefaultAsync(m => m.Id == id);
      if (applicationUser == null)
      {
        return NotFound();
      }

      return View(applicationUser);
    }

    // POST: User/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
      var applicationUser = await _context.Users.FindAsync(id);
      _context.Users.Remove(applicationUser);
      await _context.SaveChangesAsync();
      Log.Logger.Information("A {ObjectName} Was Deleted With ID: {ID}, Name: {Name}, Username: {UserName}, Email: {Email}, PhoneNumber: {PhoneNumber}, DateOfBirth: {DateOfBirth}"
          , "User", applicationUser.Id, applicationUser.Name, applicationUser.UserName, applicationUser.Email, applicationUser.PhoneNumber, applicationUser.DOB);
      return RedirectToAction(nameof(Index));
    }

    private bool ApplicationUserExists(string id)
    {
      return _context.Users.Any(e => e.Id == id);
    }
  }
}
