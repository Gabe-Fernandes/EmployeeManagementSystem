using EMS.Data.Models;
using EMS.Data.RepoInterfaces;
using EMS.Views.Timecard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace EMS.Controllers;

[Authorize]
public class Timecard : Controller
{
  private readonly IHttpContextAccessor _contextAccessor;
  private readonly AppUser _user;
  private readonly IAppUserRepo _appUserRepo;

  public Timecard(IAppUserRepo appUserRepo,
    IHttpContextAccessor contextAccessor)
  {
    _appUserRepo = appUserRepo;
    _contextAccessor = contextAccessor;
    _user = GetUser();
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



  public async Task<IActionResult> PersonalInfo(string appUserId)
  {
    AppUser appUser = await _appUserRepo.GetByIdAsync(appUserId);
    ViewData["UserParam"] = appUser;
    ViewData["UserSelf"] = _user;
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





  private AppUser GetUser()
  {
    string myId = _contextAccessor.HttpContext.User.FindFirstValue("Id");
    return _appUserRepo.GetById(myId);
  }
}
