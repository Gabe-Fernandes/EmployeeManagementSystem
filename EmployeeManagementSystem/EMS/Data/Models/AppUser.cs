using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EMS.Data.Models;

public class AppUser : IdentityUser
{
  [Required]
  [StringLength(40)]
  public string FirstName { get; set; }

  [Required]
  [StringLength(40)]
  public string LastName { get; set; }

  [Required]
  [StringLength(40)]
  public string StreetAddress { get; set; }

  [Required]
  [StringLength(40)]
  public string City { get; set; }

  [Required]
  [StringLength(40)]
  public string State { get; set; }

  [Required]
  [StringLength(40)]
  [DataType(DataType.PostalCode)]
  public string PostalCode { get; set; }

  public string Role { get; set; }

  [Required]
  [DataType(DataType.Date)]
  public DateTime Dob { get; set; }

  public bool UpToDate { get; set; } = true;
}
