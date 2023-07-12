namespace EMS.Services;

// a static class for string safety
public static class Str
{
  // Identity controllers & actions
  public const string Identity = "Identity";
  public const string Login = "Login";
  public const string Register = "Register";
  public const string ResetPassword = "ResetPassword";
  public const string ConfirmEmail = "ConfirmEmail";

  // Timecard controllers & actions
  public const string Timecard = "Timecard";
  public const string MyTimecards = "MyTimecards";
  public const string PersonalInfo = "PersonalInfo";
  public const string ManageUsers = "ManageUsers";
  public const string EnterTimecard = "EnterTimecard";

  // TempData
  public const string conf_email_sent = "conf_email_sent";
  public const string recovery_email_sent = "recovery_email_sent";
  public const string failed_login_attempt = "failed_login_attempt";

	// ViewData
	public const string Timecards = "Timecards";
  public const string AppUser = "AppUser";
  public const string Workdays = "Workdays";
  public const string ViewingOwnTimecards = "ViewingOwnTimecards";
  public const string SearchMessage = "SearchMessage";
  public const string Users = "Users";
  public const string UsersOwnId = "UsersOwnId";
  public const string CleanLogin = "CleanLogin";

  // Timecard Status
  public const string Incomplete = "Incomplete";
  public const string Submitted = "Submitted";
  public const string Approved = "Approved";
  public const string Rejected = "Rejected";

  // Identity Framework
  public const string AppEmail = "gabe-portfolioapp@outlook.com";
  public const string Cookie = "Cookie";
  public const string Admin = "Admin";
  public const string Employee = "Employee";
  public static string[] Roles = new string[] { Admin, Employee }; // having trouble making this const
  public const string AdminOnlyPolicy = "AdminOnly";
}
