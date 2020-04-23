using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ApplicationCore.Entities;
using ApplicationCore.Helpers;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
namespace Seed_Project.Areas.Identity.Pages.Account
{
  [AllowAnonymous]
  public class LoginModel : PageModel
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ILogger<LoginModel> _logger;


    public LoginModel(SignInManager<ApplicationUser> signInManager,
        ILogger<LoginModel> logger,
        UserManager<ApplicationUser> userManager,
    RoleManager<ApplicationRole> roleManager)
    {
      _roleManager = roleManager;
      _userManager = userManager;
      _signInManager = signInManager;
      _logger = logger;
    }

    [BindProperty]
    public InputModel Input { get; set; }

    public IList<AuthenticationScheme> ExternalLogins { get; set; }

    public string ReturnUrl { get; set; }

    [TempData]
    public string ErrorMessage { get; set; }

    public class InputModel
    {
      [Required]
      public string Username { get; set; }

      [Required]
      [DataType(DataType.Password)]
      public string Password { get; set; }

      [Display(Name = "Remember me?")]
      public bool RememberMe { get; set; }
    }

    public async Task OnGetAsync(string returnUrl = null)
    {
      if (!string.IsNullOrEmpty(ErrorMessage))
      {
        ModelState.AddModelError(string.Empty, ErrorMessage);
      }

      returnUrl = returnUrl ?? Url.Content("~/");

      // Clear the existing external cookie to ensure a clean login process
      await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

      ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

      ReturnUrl = returnUrl;
    }

    private async Task<ApplicationUser> GetCurrentUser()
    {
      var user = _userManager.GetUserAsync(HttpContext.User);
      return await user;
    }
    public async Task<IActionResult> OnPostAsync(string returnUrl = null)
    {
      returnUrl = returnUrl ?? Url.Content("~/");

      if (ModelState.IsValid)
      {
        // This doesn't count login failures towards account lockout
        // To enable password failures to trigger account lockout, set lockoutOnFailure: true
        var result = await _signInManager.PasswordSignInAsync(Input.Username, Input.Password, Input.RememberMe, lockoutOnFailure: false);
        if (result.Succeeded)
        {
          _logger.LogInformation("User logged in.");
          //await _roleManager.AddClaimAsync(adminRole, new Claim(CustomClaimTypes.Permission, Permissions.Dashboards.View));
          //await _roleManager.AddClaimAsync(adminRole, new Claim(CustomClaimTypes.Permission, Permissions.Users.Create));
          // Get User 
          ApplicationUser user = _userManager.Users.FirstOrDefault(x => x.UserName == Input.Username);
          //Get Permissions from database and insert Into Claims
          var claims = new Claim[]
          {
        new Claim(CustomClaimTypes.Permission, Permissions.Dashboards.View),
        new Claim(CustomClaimTypes.Permission, Permissions.Users.Create)
          }; 
          if (user != null)
            await _userManager.AddClaimsAsync(user, claims);
         

          return LocalRedirect(returnUrl);
        }
        if (result.RequiresTwoFactor)
        {
          return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
        }
        if (result.IsLockedOut)
        {
          _logger.LogWarning("User account locked out.");
          return RedirectToPage("./Lockout");
        }
        else
        {
          ModelState.AddModelError(string.Empty, "Invalid login attempt.");
          return Page();
        }
      }

      // If we got this far, something failed, redisplay form
      return Page();
    }
  }
}
