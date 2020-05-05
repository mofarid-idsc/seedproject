using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static ApplicationCore.Helpers.Enums;

namespace ApplicationCore.Entities
{
  public class EMS_Permission
  {
    [Key]
    public Guid ID { get; set; }
    public Guid OwnerID { get; set; }
    public string ActionName { get; set; }
    public string ControllerName { get; set; }
    public Permission Permission { get; set; }
    public bool Restricted { get; set; }


    public string RoleId { get; set; }
    [ForeignKey("RoleId")]
    public virtual ApplicationUserRole ApplicationUserRole { get; set; }

  }
}
