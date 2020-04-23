using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Helpers
{
  public class PermissionRequirement : IAuthorizationRequirement
  {
    public string Permission { get; private set; }
    public PermissionRequirement()
    {
    }
    public PermissionRequirement(string permission)
    {
      Permission = permission;
    }
  }
}
