using EMS.Data.Models;
using EMS.Views.Timecard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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

  [HttpPost]
  [ValidateAntiForgeryToken]
  public IActionResult EditPersonalInfo(AppUser appUser)
  {
    if (ModelState.IsValid)
    {
      return RedirectToAction("PersonalInfo", "Timecard");
    }
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

  [HttpPost]
  [ValidateAntiForgeryToken]
  public IActionResult ManageUsers(ManageUsersVM manageUsersVM)
  {
    if (ModelState.IsValid)
    {
      if (manageUsersVM.FirstName.IsNullOrEmpty() && manageUsersVM.LastName.IsNullOrEmpty())
      {
        ViewData["SearchMessage"] = "Showing all search results";

      }
      else
      {
        ViewData["SearchMessage"] = $"Showing search results for: \"{manageUsersVM.FirstName} {manageUsersVM.LastName}\"";

      }
    }
    return View();
  }
}
