using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ApplicationCore;
using ApplicationCore.Entities;
using ApplicationCore.Helpers;
using ApplicationCore.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Seed_Project.ViewModels;

namespace Seed_Project.Controllers
{
    public class PermissionController : Controller
  {
    public static string folderDetails = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\{"Helpers\\Permissions.json"}");
    public static string JSON = System.IO.File.ReadAllText(folderDetails);
    dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(JSON);
    private readonly IRepository<EMS_Permission> repository;
    private readonly AppIdentityDbContext context;
    Enums.Permission permissionsEnum = new Enums.Permission();

    public PermissionController(IRepository<EMS_Permission> _repository, AppIdentityDbContext _context)
    {
      repository = _repository;
      context = _context;
    }

    public IActionResult Index()
    {
      List<string> Controllers = new List<string>();
      foreach (var permission in jsonObj["Permissions"])
      {
        Controllers.Add(permission.Controller.ToString());
      }
    //    List<EMS_Permission> Controllers = context.Permissions.ToList().DistinctBy(x => x.ControllerName).ToList();
      ViewBag.ControllersName = new SelectList(Controllers);
      

      return View("Index",null);
    }
    public IActionResult actionResult(string controllerName)
    {
      List<string> actions = GetActionssNameByControllerName(controllerName);
      List<string> controllers = new List<string>();
      foreach (var permission in jsonObj["Permissions"])
      {
        controllers.Add(permission.Controller.ToString());
      }
      ViewBag.ControllersName = new SelectList(controllers,controllerName);
      //
      var actionsJs = new List<EMS_Permission>();
      foreach (var item in actions)
      {
        actionsJs.Add(
          new EMS_Permission
          {
            ActionName = item,
            RoleId = "be7687a9-494b-4741-812c-a0099ab6f50d",
            ControllerName = controllerName,
            Permission = Enums.Permission.Inherit,
            Restricted = false
          });
      }
      // Get Permissions of Role By Using UserRole 
      // Set RoleId Here
      var actionsDB = context.Permissions.Where(x => x.ControllerName == controllerName && x.RoleId == "be7687a9-494b-4741-812c-a0099ab6f50d")
      .ToList();
      if (actionsDB != null)
      {
        foreach (var x in actionsDB)
        {
          var PermissionToChange = actionsJs.First(d => d.ActionName == x.ActionName && d.ControllerName == x.ControllerName).Permission = x.Permission;
          var RestrictedToChange = actionsJs.First(d => d.ActionName == x.ActionName && d.ControllerName == x.ControllerName).Restricted = x.Restricted;
          var IdToChange = actionsJs.First(d => d.ActionName == x.ActionName && d.ControllerName == x.ControllerName).ID = x.ID;
        }
      }
      var InputModel = actionsJs ;
      return View("Index",InputModel);
    }
    [HttpPost]
    public IActionResult Post(List<EMS_Permission> permissions)
    {
      foreach (var item in permissions)
      {
        var exits = context.Permissions.AsNoTracking().Any(x =>
        x.ActionName == item.ActionName 
        && x.RoleId == item.RoleId
        && x.ControllerName == item.ControllerName
        && x.ID== item.ID);
           if(exits)
        {
          context.Permissions.Update(item);
          continue;
        }
        context.Permissions.Add(item);
      }
      context.SaveChanges();
      return View("Index");
    }

    public List<string> GetActionssNameByControllerName(string ControllerName)
    {

         foreach (var permission in jsonObj["Permissions"])
      {
        if (permission.Controller.ToString()== ControllerName)
        {
          string[] vs = permission.Actions.ToString().Split(',');


          return vs.ToList();

        }
        // assuming .Permission is enum
      }
        return null;
    }
  }
 
}