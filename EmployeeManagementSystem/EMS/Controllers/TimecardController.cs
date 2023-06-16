using EMS.Data.Models;
using EMS.Data.RepoInterfaces;
using EMS.Services;
using EMS.Views.Timecard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace EMS.Controllers;

[Authorize]
public class TimecardController : Controller
{
  private readonly IHttpContextAccessor _contextAccessor;
  private readonly AppUser _user;
  private readonly IAppUserRepo _appUserRepo;
  private readonly ITimecardRepo _timecardRepo;
  private readonly IWorkdayRepo _workdayRepo;

  public TimecardController(IAppUserRepo appUserRepo,
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

  public async Task<IActionResult> MyTimecards(string appUserId)
  {
    var appUser = await _appUserRepo.GetByIdAsync(appUserId) ?? _user;
    ViewData[Str.Timecards] = await _timecardRepo.GetAllOfUserAsync(appUser.Id);
		return View(appUser);
  }

  public async Task<IActionResult> EnterTimecard(int timecardId)
  {
    string usersOwnId = _contextAccessor.HttpContext.User.FindFirstValue("Id");
    var timecard = await _timecardRepo.GetByIdAsync(timecardId);
    string appUserId = timecard.AppUserId;
    var appUser = await _appUserRepo.GetByIdAsync(appUserId);
    EnterTimecardVM viewModel = new EnterTimecardVM
    {
      Status = timecard.Status,
      TimecardId = timecard.Id,
      AppUserId = timecard.AppUserId,
      WeeklyHours = timecard.WeeklyHours
    };

    ViewData[Str.AppUser] = appUser;
    ViewData[Str.Workdays] = await _workdayRepo.GetAllFromTimecardAsync(timecardId);
    ViewData[Str.ViewingOwnTimecard] = (appUserId == usersOwnId);
    return View(viewModel);
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> SubmitTimecard(EnterTimecardVM clientData)
  {
    Timecard timecardFromDb = await _timecardRepo.GetByIdAsync(clientData.TimecardId);
    timecardFromDb.Status = (clientData.CardSubmitted) ? Str.Submitted : Str.Incomplete;
    timecardFromDb.WeeklyHours = clientData.WeeklyHours;
    _timecardRepo.Update(timecardFromDb);

    List<Workday> workdays = await _workdayRepo.GetAllFromTimecardAsync(clientData.TimecardId);
    for (int i = 0; i < workdays.Count; i++)
    {
      _workdayRepo.Update(workdays[i]);
    }

    return RedirectToAction(Str.MyTimecards, Str.Timecard, new { appUserId = clientData.AppUserId });
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> ReviewTimecard(EnterTimecardVM clientData)
  {
    Timecard timecardFromDb = await _timecardRepo.GetByIdAsync(clientData.TimecardId);
    timecardFromDb.Status = (clientData.IsApproved) ? Str.Approved : Str.Rejected;
    _timecardRepo.Update(timecardFromDb);

    return RedirectToAction(Str.MyTimecards, Str.Timecard, new { appUserId = clientData.AppUserId });
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
      return RedirectToAction(Str.PersonalInfo, Str.Timecard, new { appUserId = appUserToEdit.Id });
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
        ViewData[Str.SearchMessage] = "Showing all search results";

      }
      else
      {
        ViewData[Str.SearchMessage] = $"Showing search results for: \"{manageUsersVM.SearchName}\"";

      }
    }
    ViewData[Str.Users] = await _appUserRepo.GetAllWithSearchFilterAsync(manageUsersVM.SearchName);
    return View();
  }
  
  //[Authorize(Policy = AdminOnlyPolicy)]
  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> ManageUsersDelete(ManageUsersVM manageUsersVM)
  {
    AppUser appUserToDelete = await _appUserRepo.GetByIdAsync(manageUsersVM.AppUserToDeleteId);
    _appUserRepo.Delete(appUserToDelete);
    return RedirectToAction(Str.ManageUsers, Str.Timecard);
  }

  private AppUser GetUser()
  {
    string myId = _contextAccessor.HttpContext.User.FindFirstValue("Id");
    return _appUserRepo.GetById(myId);
  }
}


/*public void GenerateData()
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
        TimeIn = "9:00",
        TimeOut = "5:00",
        TimecardId = newTimecard.Id
      };
      _workdayRepo.Add(newWorkday);
    }
  }
} */
