using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Seed_Project.Areas.Identity.Pages.Account
{
  [AllowAnonymous]
  public class LogoutModel : PageModel
  {
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ILogger<LogoutModel> _logger;
    private readonly UserManager<ApplicationUser> _userManager;

    public LogoutModel(SignInManager<ApplicationUser> signInManager,UserManager<ApplicationUser> userManager, ILogger<LogoutModel> logger)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _logger = logger;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost(string returnUrl = null)
    {

      // Delete Permissions From User
      var user = await _userManager.GetUserAsync(HttpContext.User);
      var userCalims = await _userManager.GetClaimsAsync(user);
      await _userManager.RemoveClaimsAsync(user, userCalims);

      await _signInManager.SignOutAsync();
      _logger.LogInformation("User logged out.");
      if (returnUrl != null)
      {
        return LocalRedirect(returnUrl);
      }
      else
      {
        return RedirectToPage();
      }
    }
  }
}
