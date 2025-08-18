using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_basic.Controllers;

[Authorize(Roles ="Admin")]
public class AdminController : Controller
{
  
  public ActionResult Index()
  {
    return View();
  }
}