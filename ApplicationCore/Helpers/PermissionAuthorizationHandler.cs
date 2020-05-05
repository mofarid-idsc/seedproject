using ApplicationCore.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Helpers
{
  public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly  RoleManager<ApplicationRole> _roleManager;

    public PermissionAuthorizationHandler(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
      _userManager = userManager;
      _roleManager = roleManager;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
      if (context.User == null)
      {
        return;
      }

      // Get all the roles the user belongs to and check if any of the roles has the permission required
      // for the authorization to succeed.

      var user = await _userManager.GetUserAsync(context.User);
      var userCalims = await _userManager.GetClaimsAsync(user);


      var permissions = userCalims.Where(x => x.Type == CustomClaimTypes.Permission &&
                                           x.Value == requirement.Permission &&
                                           x.Issuer == "LOCAL AUTHORITY")
                               .Select(x => x.Value);

      if (permissions.Any())
      {
        context.Succeed(requirement);
        return;
      }
      else
      {
        context.Fail();         
      }



      //var userRoleNames = await _userManager.GetRolesAsync(user);
      //var userRoles = _roleManager.Roles.Where(x => userRoleNames.Contains(x.Name));

      //var adminRole = await _roleManager.FindByNameAsync("Administrators");
      //var userClaims = _roleManager.GetClaimsAsync(adminRole);
      //var x = context.User.Claims.ToList();
      //foreach (var user in userCalims)
      //{
      //  var roleClaims = await _roleManager.GetClaimsAsync(role);
      //  var permissions = roleClaims.Where(x => x.Type == CustomClaimTypes.Permission &&
      //                                          x.Value == requirement.Permission &&
      //                                          x.Issuer == "LOCAL AUTHORITY")
      //                              .Select(x => x.Value);

      //  if (permissions.Any())
      //  {
      //    context.Succeed(requirement);
      //    return;
      //  }
      //}
    }
  }
}
