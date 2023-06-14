using System.ComponentModel.DataAnnotations;

namespace EMS.Views.Timecard;

public class ManageUsersVM
{
  [StringLength(30)]
  public string SearchName { get; set; }

  public string AppUserToDeleteId { get; set; }
}
