namespace EMS.Services;

// a static class for string safety
public static class Str
{
  // Identity controllers & actions
  public const string Identity = "IdentityController";
  public const string Login = "Login";
  public const string Register = "Register";

  // Timecard controllers & actions
  public const string Timecard = "Timecard";
  public const string MyTimecards = "MyTimecards";
  public const string PersonalInfo = "PersonalInfo";
  public const string ManageUsers = "ManageUsers";
  public const string EnterTimecard = "EnterTimecard";

  // TempData
  public const string conf_email_sent = "conf_email_sent";
  public const string recovery_email_sent = "recovery_email_sent";

  // ViewData
  public const string Timecards = "Timecards";
  public const string AppUser = "AppUser";
  public const string Workdays = "Workdays";
  public const string ViewingOwnTimecards = "ViewingOwnTimecards";
  public const string SearchMessage = "SearchMessage";
  public const string Users = "Users";

  // Timecard Status
  public const string Incomplete = "Incomplete";
  public const string Submitted = "Submitted";
  public const string Approved = "Approved";
  public const string Rejected = "Rejected";

  // Identity Framework
  public const string Cookie = "Cookie";
  public const string Admin = "Admin";
  public const string Employee = "Employee";
  public static string[] Roles = new string[] { Admin, Employee }; // having trouble making this const
  public const string AdminOnlyPolicy = "AdminOnly";
}
