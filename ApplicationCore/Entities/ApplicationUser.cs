using System;
using Microsoft.AspNetCore.Identity;

namespace ApplicationCore.Entities
{
  public class ApplicationUser : IdentityUser
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
  }
}