using EMS.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace EMS.Controllers;

public class HomeController : Controller
{
  public HomeController()
  {
    
  }

  public IActionResult Index()
  {
    return View();
  }
}
