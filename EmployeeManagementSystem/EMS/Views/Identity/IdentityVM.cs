using System.ComponentModel.DataAnnotations;

namespace EMS.Views.Identity;

public class IdentityVM
{
	public static string Cookie = "cookie";
	public static string Admin = "admin";
	public static string Employee = "employee";

	public static string[] Roles = new[] { "Admin", "Employee" };

	[Required(ErrorMessage = "email required")]
	[EmailAddress(ErrorMessage = "invalid email address")]
	[StringLength(40)]
	public string Email { get; set; }

	[Required(ErrorMessage = "password required")]
	[DataType(DataType.Password)]
	[StringLength(40, MinimumLength = 8)]
	public string Password { get; set; }

	[Required(ErrorMessage = "first name required")]
	[StringLength(40)]
	public string FirstName { get; set; }

	[Required(ErrorMessage = "last name required")]
	[StringLength(40)]
	public string LastName { get; set; }

	[Required(ErrorMessage = "phone number required")]
	[StringLength(40)]
	[DataType(DataType.PhoneNumber)]
	[Phone(ErrorMessage = "enter a valid phone number")]
	public string CellPhone { get; set; }

	[Required(ErrorMessage = "DOB required")]
	[DataType (DataType.Date)]
	public DateTime DOB { get; set; }

	[Required(ErrorMessage = "street address required")]
	[StringLength(40)]
	public string StreetAddress { get; set; }

	[Required(ErrorMessage = "city required")]
	[StringLength(40)]
	public string City { get; set; }

	[Required(ErrorMessage = "state required")]
	[StringLength(40)]
	public string State { get; set; }

	[Required(ErrorMessage = "postal code required")]
	[StringLength(40)]
	[DataType (DataType.PostalCode)]
	public string PostalCode { get; set; }

	[Required(ErrorMessage = "role selection required")]
	[StringLength(40)]
	public string Role { get; set; }

  public string Code { get; set; }
}
