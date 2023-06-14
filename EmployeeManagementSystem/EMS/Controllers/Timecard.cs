using EMS.Data.Models;
using EMS.Data.RepoInterfaces;
using EMS.Views.Timecard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace EMS.Controllers;

[Authorize]
public class Timecard : Controller
{
  private readonly IAppUserRepo _appUserRepo;

  public Timecard(IAppUserRepo appUserRepo)
  {
    _appUserRepo = appUserRepo;
  }

  const string AdminOnlyPolicy = "AdminOnly";

  public IActionResult EnterTimecard(AppUser appuser)
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

  public IActionResult PersonalInfo(AppUser appUser)
  {
    return View();
  }

  //[Authorize(Policy = AdminOnlyPolicy)]
  public async Task<IActionResult> ManageUsers(ManageUsersVM manageUsersVM)
  {
    if (ModelState.IsValid)
    {
      if (manageUsersVM.SearchName.IsNullOrEmpty())
      {
        ViewData["SearchMessage"] = "Showing all search results";

      }
      else
      {
        ViewData["SearchMessage"] = $"Showing search results for: \"{manageUsersVM.SearchName}\"";

      }
    }
    ViewData["Users"] = await _appUserRepo.GetAllWithSearchFilterAsync(manageUsersVM.SearchName);
    return View();
  }
  
  //[Authorize(Policy = AdminOnlyPolicy)]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> ManageUsersDelete(ManageUsersVM manageUsersVM)
  {
    AppUser appUserToDelete = await _appUserRepo.GetByIdAsync(manageUsersVM.AppUserToDeleteId);
    _appUserRepo.Delete(appUserToDelete);
    return RedirectToAction("ManageUsers", "Timecard");
  }
}
