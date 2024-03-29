﻿using EMS.Data.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text;
using EMS.Views.Identity;
using EMS.Services;
using EMS.Services.Email;
using EMS.Data.RepoInterfaces;

namespace EMS.Controllers;
public class IdentityController : Controller
{
	private readonly SignInManager<AppUser> _signInManager;
	private readonly UserManager<AppUser> _userManager;
	private readonly IMyEmailSender _emailSender;
	private readonly IUserEmailStore<AppUser> _emailStore;
	private readonly IUserStore<AppUser> _userStore;
	private readonly ITimecardRepo _timecardRepo;
  private readonly IAppUserRepo _appUserRepo;

  public IdentityController(SignInManager<AppUser> signInManager,
  UserManager<AppUser> userManager,
  IUserStore<AppUser> userStore,
  IMyEmailSender emailSender,
  ITimecardRepo timecardRepo,
  IAppUserRepo appUserRepo)
  {
    _signInManager = signInManager;
    _userManager = userManager;
    _userStore = userStore;
    _emailStore = (IUserEmailStore<AppUser>)_userStore;
    _emailSender = emailSender;
    _timecardRepo = timecardRepo;
    _appUserRepo = appUserRepo;
  }

  [HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Logout()
	{
		await _signInManager.SignOutAsync();
		await HttpContext.SignOutAsync(Str.Cookie);
    return RedirectToAction(Str.Login, Str.Identity, new { cleanLogin = true });
	}

	[HttpGet]
	public IActionResult Login(bool cleanLogin = false, bool failedLogin = false)
	{
		if (failedLogin)
		{
      TempData[Str.Login] = Str.failed_login_attempt;
    }
    ViewData[Str.CleanLogin] = cleanLogin;
		return View();
  }

  [HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Login(IdentityVM input)
	{
		if (ModelState.GetFieldValidationState("Email") == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid &&
				ModelState.GetFieldValidationState("Password") == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid)
		{
			var result = await _signInManager.PasswordSignInAsync(
					input.Email, input.Password, isPersistent: false, lockoutOnFailure: false);
			if (result.Succeeded)
			{
				var user = await _userManager.FindByEmailAsync(input.Email);
				await GenerateSecurityContextAsync(user, HttpContext);
        return RedirectToAction(Str.MyTimecards, Str.Timecard, new { appUserId = user.Id });
			}
		}
    await _signInManager.SignOutAsync();
    await HttpContext.SignOutAsync(Str.Cookie);
    return RedirectToAction(Str.Login, Str.Identity, new { cleanLogin = true, failedLogin = true });
	}

	[HttpGet] public IActionResult Register() { return View(); }

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Register(IdentityVM input)
	{
		if (ModelState.IsValid && input.DOB < DateTime.Now)
		{
			var user = Activator.CreateInstance<AppUser>();

			user.FirstName = input.FirstName;
			user.LastName = input.LastName;
			user.Role = input.Role;
			user.StreetAddress = input.StreetAddress;
			user.City = input.City;
			user.State = input.State;
			user.PostalCode = input.PostalCode;
			user.PhoneNumber = input.CellPhone;
			user.Dob = input.DOB;

			await _userStore.SetUserNameAsync(user, input.Email, CancellationToken.None);
			await _emailStore.SetEmailAsync(user, input.Email, CancellationToken.None);
			var result = await _userManager.CreateAsync(user, input.Password);

			if (result.Succeeded)
			{
				var userId = await _userManager.GetUserIdAsync(user);
				var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
				code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
				var callbackUrl = Url.Action(
						action: Str.ConfirmEmail,
						controller: Str.Identity,
						values: new { confirmationCode = code, appUserId = userId },
						protocol: Request.Scheme);

				await _emailSender.SendEmailAsync(input.Email, "Confirm your email",
						$"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

				GenerateTimecards(userId);

        TempData[Str.Register] = "Account Created";
				return View();
			}
		}
		return View();
	}

  [HttpGet]
  public async Task <IActionResult> ConfirmEmail(string confirmationCode, string appUserId)
	{
		var appUser = await _appUserRepo.GetByIdAsync(appUserId);

    confirmationCode = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(confirmationCode));
    var result = await _userManager.ConfirmEmailAsync(appUser, confirmationCode);
    if (result.Succeeded)
    {
      await GenerateSecurityContextAsync(appUser, HttpContext);
      return RedirectToAction(Str.MyTimecards, Str.Timecard);
    }
    else
    {
      return RedirectToAction(Str.Login, Str.Identity);
    }
  }

	[HttpGet]
	public IActionResult ResetPassword(string resetPassCode)
	{
		ViewData["Code"] = resetPassCode;
		return View();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
  public async Task<IActionResult> ResetPassword(IdentityVM input)
  {
		if (ModelState.GetFieldValidationState("Email") == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid &&
				ModelState.GetFieldValidationState("Password") == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid)
		{
			var user = await _userManager.FindByEmailAsync(input.Email);
			if (user == null)
			{
				return View();
			}

			string decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(input.Code));
			var result = await _userManager.ResetPasswordAsync(user, decodedToken, input.Password);
			if (result.Succeeded)
			{
				await _signInManager.PasswordSignInAsync(user.Email, input.Password, isPersistent: false, lockoutOnFailure: false);
				await GenerateSecurityContextAsync(user, HttpContext);
				return RedirectToAction(Str.MyTimecards, Str.Timecard, new { appUserId = user.Id });
			}
		}
		return View();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> ForgotPassword(IdentityVM input)
	{
		if (ModelState.GetFieldValidationState("Email") == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid)
		{
			var user = await _userManager.FindByEmailAsync(input.Email);
			if (user == null || !await _userManager.IsEmailConfirmedAsync(user))
			{
				TempData[Str.Login] = Str.recovery_email_sent;
				return RedirectToAction(Str.Login, Str.Identity, new { cleanLogin = true });
			}

			var code = await _userManager.GeneratePasswordResetTokenAsync(user);
			code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
			var callbackUrl = Url.Action(
					action: Str.ResetPassword,
					controller: Str.Identity,
					values: new { resetPassCode = code },
					protocol: Request.Scheme);

			await _emailSender.SendEmailAsync(input.Email, "Reset Password",
					$"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

			TempData[Str.Login] = Str.recovery_email_sent;
		}
		return RedirectToAction(Str.Login, Str.Identity, new { cleanLogin = true });
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> ResendEmailConf(IdentityVM input)
	{
		if (ModelState.GetFieldValidationState("Email") == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Valid)
		{
			var user = await _userManager.FindByEmailAsync(input.Email);
			if (user == null)
			{
				TempData[Str.Login] = Str.conf_email_sent;
				return RedirectToAction(Str.Login, Str.Identity, new { cleanLogin = true });
			}

			var userId = await _userManager.GetUserIdAsync(user);
			var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
			code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
			var callbackUrl = Url.Action(
					action: Str.ConfirmEmail,
					controller: Str.Identity,
					values: new { confirmationCode = code, appUserId = userId },
					protocol: Request.Scheme);
			await _emailSender.SendEmailAsync(input.Email, "Confirm your email",
					$"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

			TempData[Str.Login] = Str.conf_email_sent;
		}
		return RedirectToAction(Str.Login, Str.Identity, new { cleanLogin = true });
	}

	public static async Task GenerateSecurityContextAsync(AppUser appUser, HttpContext context)
	{
		var claims = new List<Claim>
				{
						new Claim(ClaimTypes.Name, appUser.FirstName),
						new Claim("LastName", appUser.LastName),
						new Claim("Email", appUser.Email),
						new Claim("Id", appUser.Id),
						new Claim(ClaimTypes.Role, appUser.Role),
				};

		var identity = new ClaimsIdentity(claims, Str.Cookie);
		ClaimsPrincipal principal = new(identity);
		await context.SignInAsync(Str.Cookie, principal);
	}

  private void GenerateTimecards(string appUserId)
  {
    DateTime monday = new(2024, 1, 1);

    for (int i = 0; i < 10; i++)
    {
      Timecard newTimecard = new()
      {
        Status = "Incomplete",
        StartDate = monday,
        WeeklyHours = 0,
        AppUserId = appUserId
      };

      _timecardRepo.Add(newTimecard);
      monday = monday.AddDays(7);
    }
  }
}
