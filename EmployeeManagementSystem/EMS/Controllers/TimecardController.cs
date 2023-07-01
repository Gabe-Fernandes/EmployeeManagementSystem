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

  public TimecardController(IAppUserRepo appUserRepo,
    IHttpContextAccessor contextAccessor,
    ITimecardRepo timecardRepo)
  {
    _appUserRepo = appUserRepo;
    _contextAccessor = contextAccessor;
    _user = GetUser();
    _timecardRepo = timecardRepo;
  }

  public async Task<IActionResult> MyTimecards(string appUserId)
  {
    var appUser = await _appUserRepo.GetByIdAsync(appUserId) ?? _user;
    string usersOwnId = _contextAccessor.HttpContext.User.FindFirstValue("Id");
    ViewData[Str.Timecards] = await _timecardRepo.GetAllOfUserAsync(appUser.Id);
    ViewData[Str.ViewingOwnTimecards] = (appUser.Id == usersOwnId);
    return View(appUser);
  }

  public async Task<IActionResult> EnterTimecard(int timecardId)
  {
    var timecard = await _timecardRepo.GetByIdAsync(timecardId);
    string appUserId = timecard.AppUserId;
    var appUser = await _appUserRepo.GetByIdAsync(appUserId);
    EnterTimecardVM viewModel = new EnterTimecardVM
    {
      Status = timecard.Status,
      TimecardId = timecard.Id,
      AppUserId = timecard.AppUserId,
      WeeklyHours = timecard.WeeklyHours,
      StartDate = timecard.StartDate,

      TimeInMon = timecard.TimeInMon,
      TimeInTues = timecard.TimeInTues,
      TimeInWed = timecard.TimeInWed,
      TimeInThur = timecard.TimeInThur,
      TimeInFri = timecard.TimeInFri,

      TimeOutMon = timecard.TimeOutMon,
      TimeOutTues = timecard.TimeOutTues,
      TimeOutWed = timecard.TimeOutWed,
      TimeOutThur = timecard.TimeOutThur,
      TimeOutFri = timecard.TimeOutFri
    };

    ViewData[Str.AppUser] = appUser;
    return View(viewModel);
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> SubmitTimecard(EnterTimecardVM clientData)
  {
    Timecard timecardFromDb = await _timecardRepo.GetByIdAsync(clientData.TimecardId);

    // Recall Timecard
    if (timecardFromDb.Status == Str.Submitted || timecardFromDb.Status == Str.Approved)
    {
      timecardFromDb.Status = Str.Incomplete;
      _timecardRepo.Update(timecardFromDb);

      // UpToDate Check - if there are no unapproved timecards, user is up to date
      var appUser = await _appUserRepo.GetByIdAsync(timecardFromDb.AppUserId);
      var unapprovedTimecardsFromUser = await _timecardRepo.GetAllUnapprovedOfUserAsync(appUser.Id);
      appUser.UpToDate = unapprovedTimecardsFromUser.Count == 0;
      _appUserRepo.Update(appUser);
      
      return RedirectToAction(Str.EnterTimecard, Str.Timecard, new { timecardId = timecardFromDb.Id });
    }

    timecardFromDb.Status = (clientData.CardSubmitted) ? Str.Submitted : Str.Incomplete;
    timecardFromDb.WeeklyHours = clientData.WeeklyHours;
    timecardFromDb.TimeInMon = clientData.TimeInMon;
    timecardFromDb.TimeInTues = clientData.TimeInTues;
    timecardFromDb.TimeInWed = clientData.TimeInWed;
    timecardFromDb.TimeInThur = clientData.TimeInThur;
    timecardFromDb.TimeInFri = clientData.TimeInFri;
    timecardFromDb.TimeOutMon = clientData.TimeOutMon;
    timecardFromDb.TimeOutTues = clientData.TimeOutTues;
    timecardFromDb.TimeOutWed = clientData.TimeOutWed;
    timecardFromDb.TimeOutThur = clientData.TimeOutThur;
    timecardFromDb.TimeOutFri = clientData.TimeOutFri;
    _timecardRepo.Update(timecardFromDb);

    return RedirectToAction(Str.MyTimecards, Str.Timecard, new { appUserId = clientData.AppUserId });
  }

  public async Task<IActionResult> ReviewTimecard(int timecardId)
  {
    var timecard = await _timecardRepo.GetByIdAsync(timecardId);
    string appUserId = timecard.AppUserId;
    var appUser = await _appUserRepo.GetByIdAsync(appUserId);
    EnterTimecardVM viewModel = new EnterTimecardVM
    {
      Status = timecard.Status,
      TimecardId = timecard.Id,
      AppUserId = timecard.AppUserId,
      WeeklyHours = timecard.WeeklyHours,
      StartDate = timecard.StartDate,

      TimeInMon = timecard.TimeInMon,
      TimeInTues = timecard.TimeInTues,
      TimeInWed = timecard.TimeInWed,
      TimeInThur = timecard.TimeInThur,
      TimeInFri = timecard.TimeInFri,

      TimeOutMon = timecard.TimeOutMon,
      TimeOutTues = timecard.TimeOutTues,
      TimeOutWed = timecard.TimeOutWed,
      TimeOutThur = timecard.TimeOutThur,
      TimeOutFri = timecard.TimeOutFri
    };

    ViewData[Str.AppUser] = appUser;
    return View(viewModel);
  }

  [HttpPost]
  [ValidateAntiForgeryToken]
  public async Task<IActionResult> ReviewTimecard(EnterTimecardVM clientData)
  {
    Timecard timecardFromDb = await _timecardRepo.GetByIdAsync(clientData.TimecardId);
    timecardFromDb.Status = (clientData.IsApproved) ? Str.Approved : Str.Rejected;
    _timecardRepo.Update(timecardFromDb);

    // UpToDate Check - if there are no unapproved timecards, user is up to date
    var appUser = await _appUserRepo.GetByIdAsync(timecardFromDb.AppUserId);
    var unapprovedTimecardsFromUser = await _timecardRepo.GetAllUnapprovedOfUserAsync(appUser.Id);
    appUser.UpToDate = unapprovedTimecardsFromUser.Count == 0;
    _appUserRepo.Update(appUser);

    return RedirectToAction(Str.MyTimecards, Str.Timecard, new { appUserId = appUser.Id });
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

  [Authorize(Policy = Str.AdminOnlyPolicy)]
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
  
  [Authorize(Policy = Str.AdminOnlyPolicy)]
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
