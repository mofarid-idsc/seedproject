using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ApplicationCore.Entities
{
  public class ApplicationUser : IdentityUser<string>
  {
    [PersonalData]
    public string Name { get; set; }
    [PersonalData]
    public DateTime DOB { get; set; }
    [PersonalData]
    public DateTime CreateDate { get; set; }
    [PersonalData]
    public DateTime ModifyDate { get; set; }
    [PersonalData]
    public string JobTitle { get; set; }
    [PersonalData]
    public string HomePage { get; set; }
    [PersonalData]
    public int UserOfficeID { get; set; }
    [PersonalData]
    public string Photo { get; set; }

    public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }
    public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
    public virtual ICollection<IdentityUserToken<string>> Tokens { get; set; }
    public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
  }

  public class ApplicationRole : IdentityRole<string>
  {
    public string Description { get; set; }
    public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
  }

  public class ApplicationUserRole : IdentityUserRole<string>
  {
    public virtual ApplicationUser User { get; set; }
    public virtual ApplicationRole Role { get; set; }
  }
}