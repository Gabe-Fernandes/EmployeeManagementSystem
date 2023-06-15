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
  private readonly ITimecardRepo _timecardRepo;
  private readonly IWorkdayRepo _workdayRepo;

  public Timecard(IAppUserRepo appUserRepo,
    IHttpContextAccessor contextAccessor,
    ITimecardRepo timecardRepo,
    IWorkdayRepo workdayRepo)
  {
    _appUserRepo = appUserRepo;
    _contextAccessor = contextAccessor;
    _user = GetUser();
    _timecardRepo = timecardRepo;
    _workdayRepo = workdayRepo;
  }

  const string AdminOnlyPolicy = "AdminOnly";



  public async Task<IActionResult> EnterTimecard(string appUserId, int timecardId = 7)
  {
    //GenerateData();
    var appUser = await _appUserRepo.GetByIdAsync(appUserId) ?? _user;
    ViewData["Timecard"] = await _timecardRepo.GetByIdAsync(timecardId);
    ViewData["Workdays"] = await _workdayRepo.GetAllFromTimecardAsync(timecardId);
    return View(appUser);
  }

  // 6/15/23 EnterTimecard receives timecard, workday, and appUser data;
  public void GenerateData()
  {
    for (int i = 0; i < 13; i++)
    {
      Data.Models.Timecard newTimecard = new Data.Models.Timecard
      {
        Status = "Incomplete",
        StartDate = DateTime.Now,
        EndDate = DateTime.Now,
        WeeklyHours = 0
      };

      _timecardRepo.Add(newTimecard);

      for (int j = 0; j < 5; j++)
      {
        Workday newWorkday = new Workday
        {
          Date = DateTime.Now,
          DailyHours = 0,
          TimeIn = "7:00",
          TimeOut = "3:00",
          TimecardId = newTimecard.Id
        };
        _workdayRepo.Add(newWorkday);
      }
    }
  }


  public async Task<IActionResult> EditPersonalInfo(string appUserId)
  {
    var appUser = await _appUserRepo.GetByIdAsync(appUserId) ?? _user;
    return View(appUser);
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> EditPersonalInfo(AppUser appUserChanges)
  {
    if (ModelState.IsValid)
    {
      var appUserToEdit = await _appUserRepo.GetByIdAsync(appUserChanges.Id);

      appUserToEdit.FirstName = appUserChanges.FirstName;
      appUserToEdit.LastName = appUserChanges.LastName;
      appUserToEdit.Email = appUserChanges.Email;
      appUserToEdit.UserName = appUserChanges.Email;
      appUserToEdit.NormalizedEmail = appUserChanges.Email.ToUpper();
      appUserToEdit.NormalizedUserName = appUserChanges.Email.ToUpper();
      appUserToEdit.PhoneNumber = appUserChanges.PhoneNumber;
      appUserToEdit.StreetAddress = appUserChanges.StreetAddress;
      appUserToEdit.City = appUserChanges.City;
      appUserToEdit.State = appUserChanges.State;
      appUserToEdit.PostalCode = appUserChanges.PostalCode;
      appUserToEdit.Dob = appUserChanges.Dob;

      _appUserRepo.Update(appUserToEdit);
      return RedirectToAction("PersonalInfo", "Timecard", new { appUserId = appUserToEdit.Id });
    }
    return View();
  }



  public async Task<IActionResult> PersonalInfo(string appUserId)
  {
    var appUser = await _appUserRepo.GetByIdAsync(appUserId) ?? _user;
    return View(appUser);
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
