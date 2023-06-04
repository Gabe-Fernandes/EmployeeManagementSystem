using Microsoft.AspNetCore.Mvc;

namespace EMS.Controllers;
public class Identity : Controller
{
  public IActionResult Login()
  {
    return View();
  }
  public IActionResult Register()
  {
    return View();
  }
  public IActionResult ResetPassword()
  {
    return View();
  }
}
