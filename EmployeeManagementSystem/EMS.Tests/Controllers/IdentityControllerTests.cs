using EMS.Controllers;
using EMS.Data.Models;
using EMS.Views.Identity;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EMS.Tests.Controllers;

public class IdentityControllerTests
{
  /*
  private readonly IdentityController _identityController;

  private readonly SignInManager<AppUser> _signInManager;
  private readonly UserManager<AppUser> _userManager;
  private readonly IUserStore<AppUser> _userStore;
  private readonly IEmailSender _emailSender;

  public IdentityControllerTests()
  {
    // Dependencies
    _signInManager = A.Fake<SignInManager<AppUser>>();
    _userManager = A.Fake<UserManager<AppUser>>();
    _userStore = A.Fake<IUserStore<AppUser>>();
    _emailSender = A.Fake<IEmailSender>();

    // SUT
    //_identityController = new IdentityController(_signInManager, _userManager, _userStore, _emailSender);
  }
  */

  /*
  [Fact]
  public async void LoginGet_ReturnsViewResult()
  {
    // Arrange (empty)
    // Act
    var result = await _identityController.Login();
    // Assert
    Assert.IsType<ViewResult>(result);
  }

  [Fact]
  public async void LoginPostError_ReturnsViewResult()
  {
    // Arrange
    var input = A.Fake<IdentityVM>();
    //_identityController.ModelState.AddModelError("test key", "test exception");
    // Act
    var result = await _identityController.Login(input);
    // Assert
    Assert.IsType<ViewResult>(result);
  }

  [Fact]
  public async void LoginPostCorrect_ReturnsRedirectToActionResult()
  {
    // Arrange
    string action = "Login";
    var input = A.Fake<IdentityVM>();
    // Act
    var result = await _identityController.Login(input);
    // Assert
    var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
    Assert.Equal(action, redirectToActionResult.ActionName);
  }

  // register

  // reset password

  // forgot password

  // resent email conf

  */
}
