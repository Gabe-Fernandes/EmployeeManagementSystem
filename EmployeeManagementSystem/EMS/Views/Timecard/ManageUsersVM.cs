using EMS.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace EMS.Views.Timecard;

public class ManageUsersVM
{
  [StringLength(30)]
  public string FirstName { get; set; }
  
  [StringLength(30)]
  public string LastName { get; set; }

  public string AppUserToDeleteId { get; set; }
}
