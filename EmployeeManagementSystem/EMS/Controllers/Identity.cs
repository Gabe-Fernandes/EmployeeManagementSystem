using EMS.Data.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text;
using Microsoft.AspNetCore.Identity.UI.Services;
using EMS.Views.Identity;

namespace EMS.Controllers;
public class Identity : Controller
{
	private readonly SignInManager<AppUser> _signInManager;
	private readonly UserManager<AppUser> _userManager;
	private readonly IEmailSender _emailSender;
	private readonly IUserEmailStore<AppUser> _emailStore;
	private readonly IUserStore<AppUser> _userStore;

	public Identity(SignInManager<AppUser> signInManager,
	UserManager<AppUser> userManager,
	IUserStore<AppUser> userStore,
	IEmailSender emailSender)
	{
		_signInManager = signInManager;
		_userManager = userManager;
		_userStore = userStore;
		_emailStore = (IUserEmailStore<AppUser>)_userStore;
		_emailSender = emailSender;
	}

	[TempData]
	public string ErrorMessage { get; set; }

	[HttpGet]
	public async Task<IActionResult> Login()
	{
		await _signInManager.SignOutAsync();
		await HttpContext.SignOutAsync(IdentityVM.Cookie);
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
				await GenerateSecurityContextAsync(input.Email, HttpContext);
        var user = await _userManager.FindByEmailAsync(input.Email);
        return RedirectToAction("EnterTimecard", "Timecard", user);
			}
		}
		return View();
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
				var callbackUrl = Url.Page(
						"/EnterTimecard",
						pageHandler: null,
						values: new { userId, code },
						protocol: Request.Scheme);

				await _emailSender.SendEmailAsync(input.Email, "Confirm your email",
						$"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

				TempData["Register"] = "Account Created";
				return View();
			}
		}
		return View();
	}

	[HttpGet]
	public IActionResult ResetPassword(string code = null)
	{
		TempData["Code"] = code;
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

			var result = await _userManager.ResetPasswordAsync(user, input.Code, input.Password);
			if (result.Succeeded)
			{
				await _signInManager.PasswordSignInAsync(user.Email, input.Password, isPersistent: false, lockoutOnFailure: false);
				await GenerateSecurityContextAsync(input.Email, HttpContext);
				return RedirectToAction("EnterTimecard", "Timecard");
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
				TempData["Login"] = "recovery_email_sent";
				return RedirectToAction("Login", "Identity");
			}

			var code = await _userManager.GeneratePasswordResetTokenAsync(user);
			code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
			var callbackUrl = Url.Page(
					"/Identity/Login",
					pageHandler: null,
					values: new { code },
					protocol: Request.Scheme);

			await _emailSender.SendEmailAsync(input.Email, "Reset Password",
					$"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

			TempData["Login"] = "recovery_email_sent";
		}
		return RedirectToAction("Login", "Identity");
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
				TempData["Login"] = "conf_email_sent";
				return RedirectToAction("Login", "Identity");
			}

			var userId = await _userManager.GetUserIdAsync(user);
			var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
			code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
			var callbackUrl = Url.Page(
					"/Identity/Login",
					pageHandler: null,
					values: new { userId, code },
					protocol: Request.Scheme);
			await _emailSender.SendEmailAsync(input.Email, "Confirm your email",
					$"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

			TempData["Login"] = "conf_email_sent";
		}
		return RedirectToAction("Login", "Identity");
	}

	public async Task GenerateSecurityContextAsync(string email, HttpContext context)
	{
		var userFromDb = await _userManager.FindByNameAsync(email);

		var claims = new List<Claim>
				{
						new Claim(ClaimTypes.Name, userFromDb.FirstName),
						new Claim("LastName", userFromDb.LastName),
						new Claim("Email", userFromDb.Email),
						new Claim("Id", userFromDb.Id),
						new Claim(ClaimTypes.Role, userFromDb.Role),
				};

		var identity = new ClaimsIdentity(claims, IdentityVM.Cookie);
		ClaimsPrincipal principal = new ClaimsPrincipal(identity);
		await context.SignInAsync(IdentityVM.Cookie, principal);
	}
}
