using EMS.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EMS.Controllers;

[Authorize]
public class Timecard : Controller
{
  public Timecard()
  {
    
  }

  public IActionResult EnterTimecard()
  {
    return View();
  }
  public IActionResult EditPersonalInfo()
  {
    return View();
  }
  public IActionResult PersonalInfo()
  {
    return View();
  }
  public IActionResult ManageUsers()
  {
    return View();
  }
}
