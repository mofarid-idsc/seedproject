using ApplicationCore.Entities;
using ApplicationCore.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ApplicationCore.Helpers.Enums;

namespace Seed_Project.ViewModels
{
  public class PermissionsVM
  {
    public List<EMS_Permission> ActionsList { get; set; }
    public Enums.Permission Permission { get; set; }
  }
}
